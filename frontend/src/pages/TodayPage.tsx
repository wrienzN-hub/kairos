const nextSteps = [
  "Ziel und aktuelle Trainingsverfügbarkeit erfassen",
  "Erste Radfahraktivität als FIT-Datei importieren",
  "Trainingsfeedback gemeinsam auswerten",
];

export function TodayPage() {
  return (
    <main className="today-page">
      <section className="hero-card" aria-labelledby="today-heading">
        <div>
          <p className="eyebrow">Dein Training · Heute</p>
          <h1 id="today-heading">Bereit für den nächsten guten Reiz.</h1>
          <p className="hero-copy">
            Kairos verbindet Radtraining, unterstützendes Krafttraining und
            Rudern zu einem nachvollziehbaren Plan, der sich mit deinem Feedback
            weiterentwickelt.
          </p>
        </div>
        <div className="readiness" aria-label="Systemstatus">
          <span className="status-dot" aria-hidden="true" />
          Produktbasis bereit
        </div>
      </section>

      <section className="content-grid" aria-label="Tagesübersicht">
        <article className="panel focus-panel">
          <p className="eyebrow">Heutiger Fokus</p>
          <h2>Noch keine Einheit geplant</h2>
          <p>
            Sobald dein Profil und deine ersten Aktivitäten vorhanden sind,
            erscheint hier die priorisierte Tageseinheit mit Begründung.
          </p>
          <button type="button" disabled>
            Training besprechen
          </button>
        </article>

        <article className="panel">
          <p className="eyebrow">Erste Schritte</p>
          <h2>Grundlage für deinen Coach</h2>
          <ol className="steps-list">
            {nextSteps.map((step) => (
              <li key={step}>{step}</li>
            ))}
          </ol>
        </article>
      </section>
    </main>
  );
}
