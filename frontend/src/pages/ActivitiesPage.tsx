import { useCallback, useEffect, useRef, useState } from "react";
import { Link, useNavigate } from "react-router-dom";

import {
  type ActivityListItem,
  listActivities,
  uploadAndImportActivity,
} from "../api/activities";
import { useAuthentication } from "../auth/authentication-state";

const typeLabels: Record<string, string> = {
  cycling: "Radfahren",
  rowing: "Rudern",
  strength_training: "Krafttraining",
};

const qualityLabels = {
  eligible: "Vollständig",
  limited: "Eingeschränkt",
  blocked: "Prüfung nötig",
};

function formatDate(value: string) {
  return new Intl.DateTimeFormat("de-AT", {
    dateStyle: "medium",
    timeStyle: "short",
  }).format(new Date(value));
}

function formatDuration(start: string, end: string) {
  const minutes = Math.max(
    0,
    Math.round((new Date(end).getTime() - new Date(start).getTime()) / 60000),
  );
  return minutes >= 60
    ? `${Math.floor(minutes / 60)} h ${minutes % 60} min`
    : `${minutes} min`;
}

export function ActivitiesPage() {
  const authentication = useAuthentication();
  const navigate = useNavigate();
  const fileInput = useRef<HTMLInputElement>(null);
  const [activities, setActivities] = useState<ActivityListItem[]>([]);
  const [state, setState] = useState<"loading" | "ready" | "error">("loading");
  const [uploadState, setUploadState] = useState<
    "idle" | "uploading" | "error"
  >("idle");

  const load = useCallback(async () => {
    await Promise.resolve();
    if (!authentication.authenticated) {
      setState("ready");
      return;
    }
    setState("loading");
    try {
      const token = await authentication.getAccessToken();
      if (!token) throw new Error("Missing access token.");
      setActivities(await listActivities(token));
      setState("ready");
    } catch {
      setState("error");
    }
  }, [authentication]);

  useEffect(() => {
    const timeout = window.setTimeout(() => void load(), 0);
    return () => window.clearTimeout(timeout);
  }, [load]);

  const importFile = async (file?: File) => {
    if (!file) return;
    setUploadState("uploading");
    try {
      const token = await authentication.getAccessToken();
      if (!token) throw new Error("Missing access token.");
      const imported = await uploadAndImportActivity(file, token);
      await navigate(`/activities/${imported.id}`);
    } catch {
      setUploadState("error");
    } finally {
      if (fileInput.current) fileInput.current.value = "";
    }
  };

  if (!authentication.authenticated) {
    return (
      <main className="activities-page">
        <section className="page-heading compact-heading">
          <p className="eyebrow">Trainingstagebuch</p>
          <h1>Deine Aktivitäten an einem Ort.</h1>
          <p>
            Melde dich an, um FIT-Dateien sicher zu importieren und deine
            Messreihen zu prüfen.
          </p>
          <button type="button" onClick={() => void authentication.login()}>
            Jetzt anmelden
          </button>
        </section>
      </main>
    );
  }

  return (
    <main className="activities-page">
      <section className="page-heading">
        <div>
          <p className="eyebrow">Trainingstagebuch</p>
          <h1>Aktivitäten</h1>
          <p>
            Importierte Einheiten mit nachvollziehbarer Quelle und sichtbarer
            Datenqualität.
          </p>
        </div>
        <div className="import-action">
          <input
            ref={fileInput}
            className="visually-hidden"
            id="fit-file"
            type="file"
            accept=".fit,application/fit,application/vnd.ant.fit"
            onChange={(event) => void importFile(event.target.files?.[0])}
          />
          <label className="primary-action" htmlFor="fit-file">
            {uploadState === "uploading"
              ? "Wird importiert …"
              : "FIT importieren"}
          </label>
          {uploadState === "error" && (
            <p role="alert">Die FIT-Datei konnte nicht importiert werden.</p>
          )}
        </div>
      </section>

      {state === "loading" && (
        <section className="activity-state" aria-live="polite">
          <span className="auth-spinner" aria-hidden="true" />
          <p>Aktivitäten werden geladen …</p>
        </section>
      )}

      {state === "error" && (
        <section className="activity-state" role="alert">
          <h2>Aktivitäten gerade nicht erreichbar</h2>
          <p>Deine Daten bleiben unverändert. Versuche es erneut.</p>
          <button type="button" onClick={() => void load()}>
            Erneut laden
          </button>
        </section>
      )}

      {state === "ready" && activities.length === 0 && (
        <section className="activity-state empty-state">
          <span className="empty-icon" aria-hidden="true">
            ↗
          </span>
          <h2>Noch keine Aktivität</h2>
          <p>
            Importiere deine erste FIT-Datei. Auch Fahrten ohne Watt- oder
            Trittfrequenzdaten werden unterstützt.
          </p>
        </section>
      )}

      {state === "ready" && activities.length > 0 && (
        <section className="activity-list" aria-label="Importierte Aktivitäten">
          {activities.map((activity) => (
            <Link
              className="activity-card"
              key={activity.id}
              to={`/activities/${activity.id}`}
            >
              <div className="activity-card-date">
                <span>{formatDate(activity.startUtc)}</span>
                <strong>{typeLabels[activity.type] ?? activity.type}</strong>
              </div>
              <dl className="activity-card-metrics">
                <div>
                  <dt>Dauer</dt>
                  <dd>{formatDuration(activity.startUtc, activity.endUtc)}</dd>
                </div>
                <div>
                  <dt>Distanz</dt>
                  <dd>
                    {activity.distanceMeters === null
                      ? "–"
                      : `${(activity.distanceMeters / 1000).toFixed(1)} km`}
                  </dd>
                </div>
              </dl>
              <div className="activity-card-meta">
                <span className={`quality-badge ${activity.analysisStatus}`}>
                  {qualityLabels[activity.analysisStatus]}
                </span>
                <span>
                  {activity.originalFileName ?? "Importierte Aktivität"}
                </span>
                <span aria-hidden="true">→</span>
              </div>
            </Link>
          ))}
        </section>
      )}
    </main>
  );
}
