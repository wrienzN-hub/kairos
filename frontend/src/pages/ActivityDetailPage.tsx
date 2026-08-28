import { useCallback, useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";

import {
  type ActivityDetail,
  type ActivityMetric,
  getActivity,
} from "../api/activities";
import { useAuthentication } from "../auth/authentication-state";

const metricLabels: Record<string, string> = {
  duration: "Dauer",
  distance: "Distanz",
  average_speed: "Ø Geschwindigkeit",
  maximum_speed: "Max. Geschwindigkeit",
  average_heart_rate: "Ø Herzfrequenz",
  maximum_heart_rate: "Max. Herzfrequenz",
  average_cadence: "Ø Trittfrequenz",
  maximum_cadence: "Max. Trittfrequenz",
  average_power: "Ø Leistung",
  maximum_power: "Max. Leistung",
  calories: "Energie",
  speed: "Geschwindigkeit",
  heart_rate: "Herzfrequenz",
  cadence: "Trittfrequenz",
  power: "Leistung",
  altitude: "Höhe",
  temperature: "Temperatur",
};

function metricValue(metric: ActivityMetric) {
  if (metric.code === "distance") {
    return `${(metric.value / 1000).toFixed(2)} km`;
  }
  if (metric.code === "duration") {
    const minutes = Math.round(metric.value / 60);
    return `${Math.floor(minutes / 60)}:${String(minutes % 60).padStart(2, "0")} h`;
  }
  return `${Number(metric.value.toFixed(1))} ${metric.unit.symbol}`;
}

export function ActivityDetailPage() {
  const { id } = useParams();
  const authentication = useAuthentication();
  const [activity, setActivity] = useState<ActivityDetail | null>(null);
  const [state, setState] = useState<"loading" | "ready" | "error">("loading");

  const load = useCallback(async () => {
    await Promise.resolve();
    if (!id || !authentication.authenticated) {
      setState("error");
      return;
    }
    setState("loading");
    try {
      const token = await authentication.getAccessToken();
      if (!token) throw new Error("Missing access token.");
      setActivity(await getActivity(id, token));
      setState("ready");
    } catch {
      setState("error");
    }
  }, [authentication, id]);

  useEffect(() => {
    const timeout = window.setTimeout(() => void load(), 0);
    return () => window.clearTimeout(timeout);
  }, [load]);

  if (state === "loading") {
    return (
      <main className="activity-detail-page activity-state" aria-live="polite">
        <span className="auth-spinner" aria-hidden="true" />
        <p>Aktivität wird geladen …</p>
      </main>
    );
  }

  if (state === "error" || !activity) {
    return (
      <main className="activity-detail-page">
        <section className="activity-state" role="alert">
          <h1>Aktivität nicht verfügbar</h1>
          <p>
            Sie wurde nicht gefunden oder konnte gerade nicht geladen werden.
          </p>
          <button type="button" onClick={() => void load()}>
            Erneut laden
          </button>
          <Link to="/activities">Zur Übersicht</Link>
        </section>
      </main>
    );
  }

  const sampleMetricCodes = Array.from(
    new Set(
      activity.samples.flatMap((sample) =>
        sample.metrics.map((metric) => metric.code),
      ),
    ),
  ).filter((code) => !["latitude", "longitude"].includes(code));

  return (
    <main className="activity-detail-page">
      <Link className="back-link" to="/activities">
        ← Alle Aktivitäten
      </Link>
      <section className="detail-heading">
        <div>
          <p className="eyebrow">Aktivitätsdetail</p>
          <h1>{activity.type === "cycling" ? "Radfahrt" : activity.type}</h1>
          <p>
            {new Intl.DateTimeFormat("de-AT", {
              dateStyle: "full",
              timeStyle: "short",
            }).format(new Date(activity.timeRange.start.instantUtc))}
          </p>
        </div>
        <span
          className={`quality-badge large ${activity.quality.analysisStatus}`}
        >
          {activity.quality.analysisStatus === "eligible"
            ? "Vollständig"
            : activity.quality.analysisStatus === "limited"
              ? "Eingeschränkt"
              : "Prüfung nötig"}
        </span>
      </section>

      {activity.quality.findings.length > 0 && (
        <section className="quality-panel" aria-labelledby="quality-heading">
          <div>
            <p className="eyebrow">Datenqualität</p>
            <h2 id="quality-heading">Was mit diesen Daten möglich ist</h2>
          </div>
          <ul>
            {activity.quality.findings.map((finding) => (
              <li className={finding.severity} key={finding.code}>
                <strong>
                  {finding.severity === "error" ? "Prüfen" : "Hinweis"}
                </strong>
                <span>{finding.message}</span>
              </li>
            ))}
          </ul>
        </section>
      )}

      <section className="summary-grid" aria-label="Zusammenfassung">
        {activity.summary.map((metric) => (
          <article className="summary-card" key={metric.code}>
            <span>{metricLabels[metric.code] ?? metric.code}</span>
            <strong>{metricValue(metric)}</strong>
            <small className={`origin ${metric.provenance.origin}`}>
              {metric.provenance.origin === "derived"
                ? "Berechnet"
                : "Importiert"}
            </small>
          </article>
        ))}
      </section>

      <section className="detail-grid">
        <article className="detail-panel source-panel">
          <p className="eyebrow">Quelle</p>
          <h2>Nachvollziehbarer Import</h2>
          <dl>
            <div>
              <dt>Datei</dt>
              <dd>{activity.source.originalFileName ?? "–"}</dd>
            </div>
            <div>
              <dt>Anbieter</dt>
              <dd>{activity.source.provider}</dd>
            </div>
            <div>
              <dt>Importiert</dt>
              <dd>
                {new Date(activity.source.importedAtUtc).toLocaleString(
                  "de-AT",
                )}
              </dd>
            </div>
            <div>
              <dt>SHA-256</dt>
              <dd className="hash-value">
                {activity.source.contentHashSha256 ?? "–"}
              </dd>
            </div>
          </dl>
        </article>
        <article className="detail-panel">
          <p className="eyebrow">Abschnitte</p>
          <h2>{activity.segments.length} aufgezeichnete Laps</h2>
          {activity.segments.length === 0 ? (
            <p>Diese Aktivität enthält keine separaten Abschnitte.</p>
          ) : (
            <ol className="lap-list">
              {activity.segments.map((segment) => (
                <li key={segment.index}>
                  <strong>Lap {segment.index + 1}</strong>
                  <span>{segment.summary.map(metricValue).join(" · ")}</span>
                </li>
              ))}
            </ol>
          )}
        </article>
      </section>

      <section className="series-panel" aria-labelledby="series-heading">
        <div>
          <p className="eyebrow">Messreihen</p>
          <h2 id="series-heading">Aufgezeichnete Zeitreihe</h2>
          <p>Gemessene Werte stammen direkt aus der FIT-Datei.</p>
        </div>
        {activity.samples.length === 0 ? (
          <p className="partial-state">Keine einzelnen Messpunkte vorhanden.</p>
        ) : (
          <div className="series-table-wrap">
            <table>
              <thead>
                <tr>
                  <th>Zeit</th>
                  {sampleMetricCodes.map((code) => (
                    <th key={code}>{metricLabels[code] ?? code}</th>
                  ))}
                </tr>
              </thead>
              <tbody>
                {activity.samples.map((sample) => (
                  <tr key={sample.timestampUtc}>
                    <th>
                      {new Date(sample.timestampUtc).toLocaleTimeString(
                        "de-AT",
                      )}
                    </th>
                    {sampleMetricCodes.map((code) => {
                      const metric = sample.metrics.find(
                        (value) => value.code === code,
                      );
                      return (
                        <td key={code}>{metric ? metricValue(metric) : "–"}</td>
                      );
                    })}
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        )}
      </section>
    </main>
  );
}
