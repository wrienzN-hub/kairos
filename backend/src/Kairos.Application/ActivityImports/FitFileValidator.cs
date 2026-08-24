using System.Buffers.Binary;

namespace Kairos.Application.ActivityImports;

public static class FitFileValidator
{
    private const int MinimumHeaderSize = 12;
    private const int HeaderWithCrcSize = 14;
    private const int FileCrcSize = 2;

    public static void Validate(ReadOnlySpan<byte> content)
    {
        if (content.Length < MinimumHeaderSize + FileCrcSize)
        {
            throw Invalid("invalid_fit_header", "Die Datei ist zu kurz für eine FIT-Datei.");
        }

        var headerSize = content[0];
        if (headerSize is not MinimumHeaderSize and not HeaderWithCrcSize)
        {
            throw Invalid("invalid_fit_header", "Die FIT-Datei hat eine nicht unterstützte Kopfgröße.");
        }

        if (!content.Slice(8, 4).SequenceEqual(".FIT"u8))
        {
            throw Invalid("invalid_fit_signature", "Die Datei besitzt keine gültige FIT-Signatur.");
        }

        var protocolMajorVersion = content[1] >> 4;
        if (protocolMajorVersion is < 1 or > 2)
        {
            throw Invalid(
                "unsupported_fit_version",
                "Die FIT-Protokollversion wird noch nicht unterstützt."
            );
        }

        var declaredDataSize = BinaryPrimitives.ReadUInt32LittleEndian(content.Slice(4, 4));
        var expectedSize = (long)headerSize + declaredDataSize + FileCrcSize;
        if (expectedSize != content.Length)
        {
            throw Invalid(
                "invalid_fit_size",
                "Die deklarierte FIT-Dateigröße stimmt nicht mit dem Upload überein."
            );
        }

        if (headerSize == HeaderWithCrcSize)
        {
            var declaredHeaderCrc = BinaryPrimitives.ReadUInt16LittleEndian(content.Slice(12, 2));
            if (declaredHeaderCrc != 0 && declaredHeaderCrc != CalculateCrc(content[..12]))
            {
                throw Invalid("invalid_fit_crc", "Die FIT-Datei ist beschädigt (Header-Prüfsumme).");
            }
        }

        var declaredFileCrc = BinaryPrimitives.ReadUInt16LittleEndian(content[^FileCrcSize..]);
        if (declaredFileCrc != CalculateCrc(content[..^FileCrcSize]))
        {
            throw Invalid("invalid_fit_crc", "Die FIT-Datei ist beschädigt (Datei-Prüfsumme).");
        }
    }

    private static FitUploadException Invalid(string code, string message)
    {
        return new FitUploadException(code, message, 400);
    }

    private static ushort CalculateCrc(ReadOnlySpan<byte> content)
    {
        ReadOnlySpan<ushort> crcTable =
        [
            0x0000,
            0xCC01,
            0xD801,
            0x1400,
            0xF001,
            0x3C00,
            0x2800,
            0xE401,
            0xA001,
            0x6C00,
            0x7800,
            0xB401,
            0x5000,
            0x9C01,
            0x8801,
            0x4400,
        ];

        ushort crc = 0;
        foreach (var value in content)
        {
            var temporary = crcTable[crc & 0xF];
            crc = (ushort)(((crc >> 4) & 0x0FFF) ^ temporary ^ crcTable[value & 0xF]);
            temporary = crcTable[crc & 0xF];
            crc = (ushort)(
                ((crc >> 4) & 0x0FFF) ^ temporary ^ crcTable[(value >> 4) & 0xF]
            );
        }

        return crc;
    }
}
