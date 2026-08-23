"""Verify Kairos FIT fixtures with Garmin's official Python FIT SDK.

Install the optional validator with ``pip install garmin-fit-sdk``. The package
is deliberately not an application dependency; Kairos will choose its parser in
a later ticket.
"""

from __future__ import annotations

import hashlib
import json
import math
from datetime import UTC, datetime
from pathlib import Path
from typing import Any

try:
    from garmin_fit_sdk import Decoder, Stream
except ImportError as error:
    raise SystemExit(
        "Garmin FIT SDK missing. Install it with: pip install garmin-fit-sdk"
    ) from error


REPOSITORY_ROOT = Path(__file__).resolve().parents[2]
FIXTURE_DIRECTORY = (
    REPOSITORY_ROOT / "backend" / "tests" / "Kairos.UnitTests" / "Fixtures" / "Fit"
)
MANIFEST_PATH = FIXTURE_DIRECTORY / "expectations.json"


def require(condition: bool, message: str) -> None:
    if not condition:
        raise AssertionError(message)


def utc_iso(value: datetime) -> str:
    if value.tzinfo is None:
        value = value.replace(tzinfo=UTC)
    return value.astimezone(UTC).isoformat().replace("+00:00", "Z")


def detect_streams(records: list[dict[str, Any]]) -> list[str]:
    fields = set().union(*(record.keys() for record in records))
    streams: list[str] = []
    if "timestamp" in fields:
        streams.append("timestamp")
    if {"position_lat", "position_long"}.issubset(fields):
        streams.append("position")
    streams.extend(
        field
        for field in (
            "altitude",
            "distance",
            "speed",
            "heart_rate",
            "cadence",
            "power",
            "temperature",
        )
        if field in fields
    )
    return streams


def verify_summary(entry: dict[str, Any], messages: dict[str, list[dict[str, Any]]]) -> None:
    sessions = messages.get("session_mesgs", [])
    require(len(sessions) == 1, f"{entry['id']}: expected exactly one session")
    session = sessions[0]

    require(
        utc_iso(session["start_time"]) == entry["start_time_utc"],
        f"{entry['id']}: start timestamp differs",
    )
    require(
        utc_iso(session["timestamp"]) == entry["end_time_utc"],
        f"{entry['id']}: end timestamp differs",
    )
    require(
        math.isclose(session["total_elapsed_time"], entry["duration_seconds"]),
        f"{entry['id']}: duration differs",
    )

    actual_distance = session.get("total_distance")
    expected_distance = entry["distance_meters"]
    if expected_distance is None:
        require(actual_distance is None, f"{entry['id']}: distance should be absent")
    else:
        require(
            actual_distance is not None
            and math.isclose(actual_distance, expected_distance),
            f"{entry['id']}: distance differs",
        )

    records = messages.get("record_mesgs", [])
    require(
        detect_streams(records) == entry["available_streams"],
        f"{entry['id']}: available streams differ",
    )

    laps = messages.get("lap_mesgs", [])
    require(len(laps) == len(entry["laps"]), f"{entry['id']}: lap count differs")
    for expected, actual in zip(entry["laps"], laps, strict=True):
        require(
            utc_iso(actual["start_time"]) == expected["start_time_utc"],
            f"{entry['id']}: lap start differs",
        )
        require(
            utc_iso(actual["timestamp"]) == expected["end_time_utc"],
            f"{entry['id']}: lap end differs",
        )
        require(
            math.isclose(actual["total_elapsed_time"], expected["duration_seconds"]),
            f"{entry['id']}: lap duration differs",
        )
        expected_lap_distance = expected["distance_meters"]
        actual_lap_distance = actual.get("total_distance")
        if expected_lap_distance is None:
            require(
                actual_lap_distance is None,
                f"{entry['id']}: lap distance should be absent",
            )
        else:
            require(
                actual_lap_distance is not None
                and math.isclose(actual_lap_distance, expected_lap_distance),
                f"{entry['id']}: lap distance differs",
            )


def main() -> None:
    manifest = json.loads(MANIFEST_PATH.read_text(encoding="utf-8"))
    for entry in manifest["fixtures"]:
        path = FIXTURE_DIRECTORY / entry["file"]
        content = path.read_bytes()
        require(len(content) == entry["size_bytes"], f"{entry['id']}: size differs")
        require(
            hashlib.sha256(content).hexdigest() == entry["sha256"],
            f"{entry['id']}: SHA-256 differs",
        )

        require(Decoder(Stream.from_file(str(path))).is_fit(), f"{entry['id']}: not FIT")
        integrity = Decoder(Stream.from_file(str(path))).check_integrity()
        require(
            integrity is entry["integrity_expected"],
            f"{entry['id']}: unexpected integrity result",
        )

        if not entry["parse_expected"]:
            print(f"OK {entry['id']}: rejected with {entry['expected_failure']}")
            continue

        messages, errors = Decoder(Stream.from_file(str(path))).read()
        require(not errors, f"{entry['id']}: decoder errors: {errors}")
        verify_summary(entry, messages)
        print(f"OK {entry['id']}: decoded values match expectations")


if __name__ == "__main__":
    main()
