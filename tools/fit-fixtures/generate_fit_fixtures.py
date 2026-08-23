"""Generate deterministic, synthetic FIT activity fixtures for Kairos.

The generator intentionally uses only the Python standard library. It implements
the small FIT protocol subset needed by ticket #10; it is not the application
parser planned for a later ticket.
"""

from __future__ import annotations

import hashlib
import json
import struct
from dataclasses import dataclass
from datetime import UTC, datetime, timedelta
from pathlib import Path
from typing import Any


REPOSITORY_ROOT = Path(__file__).resolve().parents[2]
OUTPUT_DIRECTORY = (
    REPOSITORY_ROOT / "backend" / "tests" / "Kairos.UnitTests" / "Fixtures" / "Fit"
)
MANIFEST_PATH = OUTPUT_DIRECTORY / "expectations.json"

FIT_EPOCH = datetime(1989, 12, 31, tzinfo=UTC)
PROTOCOL_VERSION = 0x20
PROFILE_VERSION = 21205

BASE_ENUM = 0x00
BASE_SINT8 = 0x01
BASE_UINT8 = 0x02
BASE_UINT16 = 0x84
BASE_SINT32 = 0x85
BASE_UINT32 = 0x86
BASE_UINT32Z = 0x8C

GLOBAL_FILE_ID = 0
GLOBAL_SESSION = 18
GLOBAL_LAP = 19
GLOBAL_RECORD = 20
GLOBAL_EVENT = 21
GLOBAL_ACTIVITY = 34

LOCAL_FILE_ID = 0
LOCAL_EVENT = 1
LOCAL_RECORD = 2
LOCAL_LAP = 3
LOCAL_SESSION = 4
LOCAL_ACTIVITY = 5


@dataclass(frozen=True)
class Field:
    number: int
    size: int
    base_type: int
    struct_format: str

    def encode(self, value: int) -> bytes:
        return struct.pack("<" + self.struct_format, value)


FILE_ID_FIELDS = (
    Field(0, 1, BASE_ENUM, "B"),
    Field(1, 2, BASE_UINT16, "H"),
    Field(2, 2, BASE_UINT16, "H"),
    Field(3, 4, BASE_UINT32Z, "I"),
    Field(4, 4, BASE_UINT32, "I"),
)

EVENT_FIELDS = (
    Field(253, 4, BASE_UINT32, "I"),
    Field(0, 1, BASE_ENUM, "B"),
    Field(1, 1, BASE_ENUM, "B"),
)

RECORD_FIELD_CATALOG = {
    "timestamp": Field(253, 4, BASE_UINT32, "I"),
    "position_lat": Field(0, 4, BASE_SINT32, "i"),
    "position_long": Field(1, 4, BASE_SINT32, "i"),
    "altitude": Field(2, 2, BASE_UINT16, "H"),
    "heart_rate": Field(3, 1, BASE_UINT8, "B"),
    "cadence": Field(4, 1, BASE_UINT8, "B"),
    "distance": Field(5, 4, BASE_UINT32, "I"),
    "speed": Field(6, 2, BASE_UINT16, "H"),
    "power": Field(7, 2, BASE_UINT16, "H"),
    "temperature": Field(13, 1, BASE_SINT8, "b"),
}

LAP_FIELDS_WITH_DISTANCE = (
    Field(254, 2, BASE_UINT16, "H"),
    Field(253, 4, BASE_UINT32, "I"),
    Field(2, 4, BASE_UINT32, "I"),
    Field(7, 4, BASE_UINT32, "I"),
    Field(8, 4, BASE_UINT32, "I"),
    Field(9, 4, BASE_UINT32, "I"),
)

LAP_FIELDS_WITHOUT_DISTANCE = LAP_FIELDS_WITH_DISTANCE[:-1]

SESSION_FIELDS_WITH_DISTANCE = (
    Field(254, 2, BASE_UINT16, "H"),
    Field(253, 4, BASE_UINT32, "I"),
    Field(2, 4, BASE_UINT32, "I"),
    Field(5, 1, BASE_ENUM, "B"),
    Field(6, 1, BASE_ENUM, "B"),
    Field(7, 4, BASE_UINT32, "I"),
    Field(8, 4, BASE_UINT32, "I"),
    Field(9, 4, BASE_UINT32, "I"),
    Field(25, 2, BASE_UINT16, "H"),
    Field(26, 2, BASE_UINT16, "H"),
)

SESSION_FIELDS_WITHOUT_DISTANCE = (
    *SESSION_FIELDS_WITH_DISTANCE[:7],
    *SESSION_FIELDS_WITH_DISTANCE[8:],
)

ACTIVITY_FIELDS = (
    Field(253, 4, BASE_UINT32, "I"),
    Field(0, 4, BASE_UINT32, "I"),
    Field(1, 2, BASE_UINT16, "H"),
)


def fit_timestamp(value: datetime) -> int:
    return int((value - FIT_EPOCH).total_seconds())


def parse_utc(value: str) -> datetime:
    return datetime.fromisoformat(value.replace("Z", "+00:00"))


def semicircles(degrees: float) -> int:
    return round(degrees * (2**31) / 180)


def raw_altitude(meters: float) -> int:
    return round((meters + 500) * 5)


def raw_distance(meters: float) -> int:
    return round(meters * 100)


def raw_duration(seconds: float) -> int:
    return round(seconds * 1000)


def raw_speed(meters_per_second: float) -> int:
    return round(meters_per_second * 1000)


def crc16(data: bytes, initial: int = 0) -> int:
    crc_table = (
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
    )

    crc = initial
    for byte in data:
        temporary = crc_table[crc & 0xF]
        crc = ((crc >> 4) & 0x0FFF) ^ temporary ^ crc_table[byte & 0xF]
        temporary = crc_table[crc & 0xF]
        crc = (
            ((crc >> 4) & 0x0FFF)
            ^ temporary
            ^ crc_table[(byte >> 4) & 0xF]
        )
    return crc


def definition_message(local_number: int, global_number: int, fields: tuple[Field, ...]) -> bytes:
    header = bytes((0x40 | local_number, 0, 0))
    field_definitions = b"".join(
        bytes((field.number, field.size, field.base_type)) for field in fields
    )
    return header + struct.pack("<H", global_number) + bytes((len(fields),)) + field_definitions


def data_message(local_number: int, fields: tuple[Field, ...], values: tuple[int, ...]) -> bytes:
    if len(fields) != len(values):
        raise ValueError("Every FIT field requires exactly one value")
    return bytes((local_number,)) + b"".join(
        field.encode(value) for field, value in zip(fields, values, strict=True)
    )


def build_fit_file(data_records: bytes) -> bytes:
    header_without_crc = (
        bytes((14, PROTOCOL_VERSION))
        + struct.pack("<H", PROFILE_VERSION)
        + struct.pack("<I", len(data_records))
        + b".FIT"
    )
    # The protocol permits a zero header CRC, but the current official SDK's
    # integrity checker requires the calculated value. Use the stricter form.
    header = header_without_crc + struct.pack("<H", crc16(header_without_crc))
    file_without_crc = header + data_records
    return file_without_crc + struct.pack("<H", crc16(file_without_crc))


def record_fields(streams: list[str]) -> tuple[Field, ...]:
    names: list[str] = ["timestamp"]
    if "position" in streams:
        names.extend(("position_lat", "position_long"))
    names.extend(stream for stream in streams if stream not in {"timestamp", "position"})
    return tuple(RECORD_FIELD_CATALOG[name] for name in names)


def encode_record_values(record: dict[str, Any], streams: list[str]) -> tuple[int, ...]:
    values: list[int] = [fit_timestamp(record["timestamp"])]
    if "position" in streams:
        values.extend((semicircles(record["latitude"]), semicircles(record["longitude"])))
    for stream in streams:
        if stream in {"timestamp", "position"}:
            continue
        value = record[stream]
        if stream == "altitude":
            values.append(raw_altitude(value))
        elif stream == "distance":
            values.append(raw_distance(value))
        elif stream == "speed":
            values.append(raw_speed(value))
        else:
            values.append(value)
    return tuple(values)


def build_activity_fixture(specification: dict[str, Any]) -> bytes:
    start = specification["start"]
    end = specification["end"]
    duration_seconds = int((end - start).total_seconds())
    distance = specification["distance_meters"]
    streams = specification["streams"]
    records = specification["records"]
    laps = specification["laps"]

    chunks: list[bytes] = []
    chunks.append(definition_message(LOCAL_FILE_ID, GLOBAL_FILE_ID, FILE_ID_FIELDS))
    chunks.append(
        data_message(
            LOCAL_FILE_ID,
            FILE_ID_FIELDS,
            (4, 255, 1, specification["serial_number"], fit_timestamp(start)),
        )
    )

    chunks.append(definition_message(LOCAL_EVENT, GLOBAL_EVENT, EVENT_FIELDS))
    chunks.append(data_message(LOCAL_EVENT, EVENT_FIELDS, (fit_timestamp(start), 0, 0)))

    fields_for_records = record_fields(streams)
    chunks.append(definition_message(LOCAL_RECORD, GLOBAL_RECORD, fields_for_records))
    for record in records:
        chunks.append(
            data_message(
                LOCAL_RECORD,
                fields_for_records,
                encode_record_values(record, streams),
            )
        )

    lap_fields = LAP_FIELDS_WITH_DISTANCE if distance is not None else LAP_FIELDS_WITHOUT_DISTANCE
    chunks.append(definition_message(LOCAL_LAP, GLOBAL_LAP, lap_fields))
    for index, lap in enumerate(laps):
        lap_values = [
            index,
            fit_timestamp(lap["end"]),
            fit_timestamp(lap["start"]),
            raw_duration((lap["end"] - lap["start"]).total_seconds()),
            raw_duration((lap["end"] - lap["start"]).total_seconds()),
        ]
        if distance is not None:
            lap_values.append(raw_distance(lap["distance_meters"]))
        chunks.append(data_message(LOCAL_LAP, lap_fields, tuple(lap_values)))

    chunks.append(data_message(LOCAL_EVENT, EVENT_FIELDS, (fit_timestamp(end), 0, 4)))

    session_fields = (
        SESSION_FIELDS_WITH_DISTANCE if distance is not None else SESSION_FIELDS_WITHOUT_DISTANCE
    )
    session_values = [
        0,
        fit_timestamp(end),
        fit_timestamp(start),
        2,
        0,
        raw_duration(duration_seconds),
        raw_duration(duration_seconds),
    ]
    if distance is not None:
        session_values.append(raw_distance(distance))
    session_values.extend((0, len(laps)))
    chunks.append(definition_message(LOCAL_SESSION, GLOBAL_SESSION, session_fields))
    chunks.append(data_message(LOCAL_SESSION, session_fields, tuple(session_values)))

    chunks.append(definition_message(LOCAL_ACTIVITY, GLOBAL_ACTIVITY, ACTIVITY_FIELDS))
    chunks.append(
        data_message(
            LOCAL_ACTIVITY,
            ACTIVITY_FIELDS,
            (fit_timestamp(end), raw_duration(duration_seconds), 1),
        )
    )
    return build_fit_file(b"".join(chunks))


def at(start: datetime, seconds: int) -> datetime:
    return start + timedelta(seconds=seconds)


def fixture_specifications() -> list[dict[str, Any]]:
    valid_start = parse_utc("2026-01-15T06:00:00Z")
    minimal_start = parse_utc("2026-01-16T12:00:00Z")
    interval_start = parse_utc("2026-01-17T09:00:00Z")
    incomplete_start = parse_utc("2026-01-18T07:30:00Z")

    return [
        {
            "id": "valid-cycling",
            "file": "valid-cycling.fit",
            "classification": "valid",
            "description": "Vollständige synthetische Radaktivität mit üblichen Messreihen.",
            "serial_number": 1001,
            "start": valid_start,
            "end": at(valid_start, 1800),
            "distance_meters": 10000,
            "streams": [
                "timestamp",
                "position",
                "altitude",
                "distance",
                "speed",
                "heart_rate",
                "cadence",
                "power",
                "temperature",
            ],
            "records": [
                {
                    "timestamp": at(valid_start, 0),
                    "latitude": 48.2082,
                    "longitude": 16.3738,
                    "altitude": 171,
                    "distance": 0,
                    "speed": 5.5,
                    "heart_rate": 118,
                    "cadence": 82,
                    "power": 145,
                    "temperature": 8,
                },
                {
                    "timestamp": at(valid_start, 600),
                    "latitude": 48.2140,
                    "longitude": 16.3920,
                    "altitude": 176,
                    "distance": 3000,
                    "speed": 6.1,
                    "heart_rate": 132,
                    "cadence": 88,
                    "power": 182,
                    "temperature": 8,
                },
                {
                    "timestamp": at(valid_start, 1200),
                    "latitude": 48.2200,
                    "longitude": 16.4100,
                    "altitude": 183,
                    "distance": 6500,
                    "speed": 6.4,
                    "heart_rate": 141,
                    "cadence": 91,
                    "power": 205,
                    "temperature": 9,
                },
                {
                    "timestamp": at(valid_start, 1800),
                    "latitude": 48.2260,
                    "longitude": 16.4280,
                    "altitude": 174,
                    "distance": 10000,
                    "speed": 5.9,
                    "heart_rate": 136,
                    "cadence": 86,
                    "power": 168,
                    "temperature": 9,
                },
            ],
            "laps": [
                {
                    "start": valid_start,
                    "end": at(valid_start, 1800),
                    "distance_meters": 10000,
                }
            ],
        },
        {
            "id": "minimal-cycling",
            "file": "minimal-cycling.fit",
            "classification": "minimal",
            "description": "Kleinste unterstützte Radaktivität mit Zeit- und Distanzdaten.",
            "serial_number": 1002,
            "start": minimal_start,
            "end": at(minimal_start, 300),
            "distance_meters": 1000,
            "streams": ["timestamp", "distance"],
            "records": [
                {"timestamp": minimal_start, "distance": 0},
                {"timestamp": at(minimal_start, 300), "distance": 1000},
            ],
            "laps": [
                {
                    "start": minimal_start,
                    "end": at(minimal_start, 300),
                    "distance_meters": 1000,
                }
            ],
        },
        {
            "id": "interval-cycling",
            "file": "interval-cycling.fit",
            "classification": "interval",
            "description": "Radaktivität mit zwei synthetischen Intervallen beziehungsweise Laps.",
            "serial_number": 1003,
            "start": interval_start,
            "end": at(interval_start, 1200),
            "distance_meters": 8000,
            "streams": [
                "timestamp",
                "distance",
                "speed",
                "heart_rate",
                "cadence",
                "power",
            ],
            "records": [
                {
                    "timestamp": at(interval_start, 0),
                    "distance": 0,
                    "speed": 5.0,
                    "heart_rate": 112,
                    "cadence": 80,
                    "power": 130,
                },
                {
                    "timestamp": at(interval_start, 300),
                    "distance": 1500,
                    "speed": 7.0,
                    "heart_rate": 151,
                    "cadence": 96,
                    "power": 270,
                },
                {
                    "timestamp": at(interval_start, 600),
                    "distance": 3500,
                    "speed": 5.2,
                    "heart_rate": 128,
                    "cadence": 84,
                    "power": 145,
                },
                {
                    "timestamp": at(interval_start, 900),
                    "distance": 5700,
                    "speed": 7.4,
                    "heart_rate": 158,
                    "cadence": 99,
                    "power": 292,
                },
                {
                    "timestamp": at(interval_start, 1200),
                    "distance": 8000,
                    "speed": 5.3,
                    "heart_rate": 134,
                    "cadence": 85,
                    "power": 152,
                },
            ],
            "laps": [
                {
                    "start": interval_start,
                    "end": at(interval_start, 600),
                    "distance_meters": 3500,
                },
                {
                    "start": at(interval_start, 600),
                    "end": at(interval_start, 1200),
                    "distance_meters": 4500,
                },
            ],
        },
        {
            "id": "incomplete-cycling",
            "file": "incomplete-cycling.fit",
            "classification": "incomplete",
            "description": "Strukturell gültige Aktivität ohne Leistung und Trittfrequenz.",
            "serial_number": 1004,
            "start": incomplete_start,
            "end": at(incomplete_start, 900),
            "distance_meters": 4500,
            "streams": [
                "timestamp",
                "position",
                "altitude",
                "distance",
                "speed",
                "heart_rate",
                "temperature",
            ],
            "records": [
                {
                    "timestamp": incomplete_start,
                    "latitude": 48.1980,
                    "longitude": 16.3500,
                    "altitude": 168,
                    "distance": 0,
                    "speed": 4.8,
                    "heart_rate": 105,
                    "temperature": 11,
                },
                {
                    "timestamp": at(incomplete_start, 450),
                    "latitude": 48.2050,
                    "longitude": 16.3720,
                    "altitude": 175,
                    "distance": 2200,
                    "speed": 5.2,
                    "heart_rate": 126,
                    "temperature": 11,
                },
                {
                    "timestamp": at(incomplete_start, 900),
                    "latitude": 48.2120,
                    "longitude": 16.3950,
                    "altitude": 170,
                    "distance": 4500,
                    "speed": 5.0,
                    "heart_rate": 119,
                    "temperature": 12,
                },
            ],
            "laps": [
                {
                    "start": incomplete_start,
                    "end": at(incomplete_start, 900),
                    "distance_meters": 4500,
                }
            ],
        },
    ]


def manifest_entry(specification: dict[str, Any], content: bytes) -> dict[str, Any]:
    laps = [
        {
            "index": index,
            "start_time_utc": lap["start"].isoformat().replace("+00:00", "Z"),
            "end_time_utc": lap["end"].isoformat().replace("+00:00", "Z"),
            "duration_seconds": int((lap["end"] - lap["start"]).total_seconds()),
            "distance_meters": lap["distance_meters"],
        }
        for index, lap in enumerate(specification["laps"])
    ]
    return {
        "id": specification["id"],
        "file": specification["file"],
        "classification": specification["classification"],
        "description": specification["description"],
        "provenance": "synthetic-kairos-generator",
        "integrity_expected": True,
        "parse_expected": True,
        "start_time_utc": specification["start"].isoformat().replace("+00:00", "Z"),
        "end_time_utc": specification["end"].isoformat().replace("+00:00", "Z"),
        "duration_seconds": int(
            (specification["end"] - specification["start"]).total_seconds()
        ),
        "distance_meters": specification["distance_meters"],
        "available_streams": specification["streams"],
        "laps": laps,
        "expected_failure": None,
        "size_bytes": len(content),
        "sha256": hashlib.sha256(content).hexdigest(),
    }


def write_fixtures() -> None:
    OUTPUT_DIRECTORY.mkdir(parents=True, exist_ok=True)
    entries: list[dict[str, Any]] = []
    valid_content: bytes | None = None

    for specification in fixture_specifications():
        content = build_activity_fixture(specification)
        (OUTPUT_DIRECTORY / specification["file"]).write_bytes(content)
        entries.append(manifest_entry(specification, content))
        if specification["id"] == "valid-cycling":
            valid_content = content

    if valid_content is None:
        raise RuntimeError("The valid fixture is required to derive the corrupted fixture")

    corrupted_content = bytearray(valid_content)
    corrupted_content[-3] ^= 0x01
    corrupted_bytes = bytes(corrupted_content)
    corrupted_file = "corrupted-crc.fit"
    (OUTPUT_DIRECTORY / corrupted_file).write_bytes(corrupted_bytes)
    entries.append(
        {
            "id": "corrupted-crc",
            "file": corrupted_file,
            "classification": "corrupted",
            "description": "Von valid-cycling abgeleitete Datei mit absichtlich ungültiger CRC.",
            "provenance": "synthetic-kairos-generator",
            "derived_from": "valid-cycling",
            "integrity_expected": False,
            "parse_expected": False,
            "start_time_utc": None,
            "end_time_utc": None,
            "duration_seconds": None,
            "distance_meters": None,
            "available_streams": [],
            "laps": [],
            "expected_failure": "crc_mismatch",
            "size_bytes": len(corrupted_bytes),
            "sha256": hashlib.sha256(corrupted_bytes).hexdigest(),
        }
    )

    manifest = {
        "schema_version": 1,
        "generator": "tools/fit-fixtures/generate_fit_fixtures.py",
        "fit_protocol_version": "2.0",
        "fit_profile_version": "21.205",
        "protocol_reference": "https://developer.garmin.com/fit/protocol/",
        "data_policy": "All fixtures are deterministic synthetic data created for Kairos; no athlete or device recording was used.",
        "fixtures": entries,
    }
    MANIFEST_PATH.write_text(
        json.dumps(manifest, indent=2, ensure_ascii=False) + "\n",
        encoding="utf-8",
        newline="\n",
    )


if __name__ == "__main__":
    write_fixtures()
