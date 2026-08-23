using System.Buffers.Binary;
using System.Security.Cryptography;
using System.Text.Json;

namespace Kairos.UnitTests;

public sealed class FitFixtureContractTests
{
    private static readonly string FixtureDirectory = Path.Combine(
        AppContext.BaseDirectory,
        "Fixtures",
        "Fit"
    );

    [Fact]
    public void Fixture_manifest_covers_the_required_synthetic_cases()
    {
        using var manifest = LoadManifest();
        var fixtures = manifest.RootElement.GetProperty("fixtures").EnumerateArray().ToArray();

        Assert.Equal(1, manifest.RootElement.GetProperty("schema_version").GetInt32());
        Assert.Equal(5, fixtures.Length);
        var classifications = fixtures
            .Select(fixture => fixture.GetProperty("classification").GetString()!)
            .Order()
            .ToArray();
        Assert.Equal(
            ["corrupted", "incomplete", "interval", "minimal", "valid"],
            classifications
        );

        foreach (var fixture in fixtures)
        {
            Assert.Equal(
                "synthetic-kairos-generator",
                fixture.GetProperty("provenance").GetString()
            );
            Assert.True(fixture.TryGetProperty("start_time_utc", out _));
            Assert.True(fixture.TryGetProperty("end_time_utc", out _));
            Assert.True(fixture.TryGetProperty("duration_seconds", out _));
            Assert.True(fixture.TryGetProperty("distance_meters", out _));
            Assert.Equal(JsonValueKind.Array, fixture.GetProperty("available_streams").ValueKind);
        }

        var withoutPowerMeter = fixtures.Single(fixture =>
            fixture.GetProperty("id").GetString() == "incomplete-cycling"
        );
        var availableStreams = withoutPowerMeter
            .GetProperty("available_streams")
            .EnumerateArray()
            .Select(stream => stream.GetString()!)
            .ToArray();
        Assert.Equal(
            [
                "timestamp",
                "position",
                "altitude",
                "distance",
                "speed",
                "heart_rate",
                "temperature",
            ],
            availableStreams
        );
        Assert.DoesNotContain("cadence", availableStreams);
        Assert.DoesNotContain("power", availableStreams);
    }

    [Fact]
    public void Fixture_files_match_manifest_hashes_and_expected_fit_integrity()
    {
        using var manifest = LoadManifest();

        foreach (var fixture in manifest.RootElement.GetProperty("fixtures").EnumerateArray())
        {
            var identifier = fixture.GetProperty("id").GetString();
            var path = Path.Combine(
                FixtureDirectory,
                fixture.GetProperty("file").GetString()!
            );
            var content = File.ReadAllBytes(path);

            Assert.Equal(fixture.GetProperty("size_bytes").GetInt32(), content.Length);
            Assert.Equal(
                fixture.GetProperty("sha256").GetString(),
                Convert.ToHexStringLower(SHA256.HashData(content))
            );
            Assert.Equal(".FIT", System.Text.Encoding.ASCII.GetString(content, 8, 4));
            Assert.Equal(14, content[0]);

            var declaredDataSize = BinaryPrimitives.ReadUInt32LittleEndian(content.AsSpan(4, 4));
            Assert.Equal(content.Length, checked((int)(content[0] + declaredDataSize + 2)));
            Assert.Equal(
                fixture.GetProperty("integrity_expected").GetBoolean(),
                HasValidCrc(content)
            );

            if (fixture.GetProperty("classification").GetString() == "corrupted")
            {
                Assert.Equal("crc_mismatch", fixture.GetProperty("expected_failure").GetString());
            }
            else
            {
                Assert.True(
                    fixture.GetProperty("parse_expected").GetBoolean(),
                    $"{identifier} must be accepted by the future parser"
                );
            }
        }
    }

    private static JsonDocument LoadManifest()
    {
        return JsonDocument.Parse(
            File.ReadAllText(Path.Combine(FixtureDirectory, "expectations.json"))
        );
    }

    private static bool HasValidCrc(byte[] content)
    {
        var headerCrc = BinaryPrimitives.ReadUInt16LittleEndian(content.AsSpan(12, 2));
        if (headerCrc != CalculateCrc(content.AsSpan(0, 12)))
        {
            return false;
        }

        var fileCrc = BinaryPrimitives.ReadUInt16LittleEndian(content.AsSpan(content.Length - 2));
        return fileCrc == CalculateCrc(content.AsSpan(0, content.Length - 2));
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
