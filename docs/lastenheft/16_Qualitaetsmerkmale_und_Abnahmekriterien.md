# 16. Qualitätsmerkmale und Abnahmekriterien

## 16.1 Zweck

Dieses Kapitel definiert, wann eine Kairos-Funktion als fachlich nutzbar gilt.
Abnahme bedeutet nicht nur, dass eine Oberfläche vorhanden ist. Daten,
Berechnungen, Erklärungen, Sicherheit und Fehlerverhalten müssen gemeinsam einen
vollständigen Nutzen liefern.

## 16.2 Abnahmeprinzipien

- Jede Anforderung besitzt überprüfbare Akzeptanzkriterien.
- Ein Inkrement wird anhand eines End-to-End-Ablaufs abgenommen.
- Kritische Berechnungen werden gegen bekannte Referenzwerte geprüft.
- KI-Ausgaben werden mit wiederholbaren Evaluationsfällen bewertet.
- Fehlende oder fehlerhafte Daten sind Bestandteil der Abnahme.
- Datenschutz und Berechtigungen sind keine nachgelagerte Zusatzprüfung.
- Bekannte Abweichungen werden dokumentiert und priorisiert.
- Die fachliche Abnahme erfolgt getrennt von der technischen Fertigstellung.

## 16.3 Qualitätsmerkmale

### QM-01 – Fachliche Richtigkeit

Importierte Werte, Einheiten, Zeitbezüge und Berechnungen müssen mit ihrer Quelle
beziehungsweise einer dokumentierten Referenz übereinstimmen. Rundungen und
Toleranzen werden je Kennzahl festgelegt.

### QM-02 – Nachvollziehbarkeit

Der Nutzer muss erkennen können, welche Daten zu einer Analyse, Empfehlung oder
Planänderung geführt haben. Messwert, Berechnung, Schätzung und KI-Aussage sind
unterscheidbar.

### QM-03 – Handlungsnutzen

Eine Analyse gilt nur dann als wertvoll, wenn sie eine verständliche Einordnung
oder eine konkrete nächste Handlung liefert. Zusätzliche Kennzahlen ohne
erkennbaren Nutzen erhöhen den Abnahmegrad nicht.

### QM-04 – Nutzerkontrolle

Nutzer können Daten korrigieren, Quellen trennen, Automatisierung steuern und
automatische Änderungen nachvollziehen beziehungsweise rückgängig machen.

### QM-05 – Robustheit

Ausfälle, Duplikate, Datenlücken, unrealistische Messwerte und unterbrochene
Synchronisation führen zu kontrollierten Zuständen statt stillen Fehlern.

### QM-06 – Datenschutz und Sicherheit

Zugriff, Verarbeitung und Weitergabe folgen den erteilten Berechtigungen.
Sensible Daten erscheinen nicht unnötig in Protokollen, Fehlermeldungen oder
externen KI-Anfragen.

### QM-07 – Bedienbarkeit und Barrierefreiheit

Kernaufgaben sind verständlich, responsiv und nach WCAG 2.2 Level AA gestaltet.
Offene Abweichungen müssen vor Veröffentlichung bewertet werden.

### QM-08 – Wartbarkeit

Fachliche Regeln, Integrationen und Sportarten bleiben testbar und voneinander
abgrenzbar. Änderungen an Kennzahlen oder Analyseverfahren sind versioniert.

## 16.4 Abnahme des ersten End-to-End-Inkrements

Das erste vollständige Inkrement gilt als fachlich abnehmbar, wenn:

1. ein Athletenprofil und ein Ziel angelegt werden können;
2. eine gültige FIT-Datei importiert wird;
3. die Aktivität ohne doppelte Belastungszählung gespeichert wird;
4. wesentliche Radaktivitätsdaten korrekt angezeigt werden;
5. relevante Abschnitte oder Intervalle dargestellt und korrigiert werden können;
6. mindestens eine fachlich validierte Analyse erzeugt wird;
7. Datenqualität und fehlende Werte sichtbar sind;
8. die Analyse eine konkrete, begründete Aussage liefert;
9. der Nutzer Feedback ergänzen und die Analyse bewerten kann;
10. Aktivität, Analyse und Feedback später erneut nachvollziehbar sind;
11. Löschung und Export der betroffenen Daten grundsätzlich funktionieren;
12. zentrale Abläufe automatisiert und manuell geprüft wurden.

## 16.5 Abnahme FIT-Import

- Unterstützte FIT-Dateien werden reproduzierbar eingelesen.
- Zeitzone, Startzeit, Dauer, Distanz und vorhandene Messreihen werden korrekt
  zugeordnet.
- Unbekannte optionale Felder verhindern den Import nicht.
- Beschädigte Dateien erzeugen eine verständliche Fehlermeldung.
- Derselbe Import erzeugt keine zweite Aktivität.
- Quelldatei und normalisierte Daten bleiben nachvollziehbar.
- Große Dateien führen nicht zu unkontrolliertem Ressourcenverbrauch.

## 16.6 Abnahme Trainingsanalyse

- Die Analyse verwendet ausschließlich vorhandene oder klar geschätzte Daten.
- Der Trainingszweck ist bekannt oder als unbekannt markiert.
- Erkannte Intervalle können geprüft und korrigiert werden.
- Vergleichseinheiten werden nach erklärbaren Kriterien gewählt.
- Subjektives Feedback beeinflusst die Einordnung sichtbar.
- Unsicherheit wird konkret und nicht nur pauschal genannt.
- Eine fachlich ungeeignete Datenbasis führt zu keiner scheinbar sicheren
  Empfehlung.

## 16.7 Abnahme KI-Coach

Für einen definierten Evaluationssatz muss der Coach:

- relevante Fakten korrekt wiedergeben;
- keine fehlenden Werte erfinden;
- Beobachtungen und Hypothesen trennen;
- Rückfragen stellen, wenn entscheidende Angaben fehlen;
- auf die betroffenen Aktivitäten oder Daten verweisen;
- gesundheitliche Grenzen einhalten;
- keine Aktion außerhalb der Nutzerberechtigung auslösen;
- vergleichbare Fragen konsistent behandeln;
- eine kurze Kernaussage und verständliche Details liefern.

Eine einzelne gute Beispielantwort reicht nicht zur Freigabe.

## 16.8 Abnahme Planung und Automatisierung

- Der Plan stellt vier Wochen konkret dar.
- Radfahren besitzt den Hauptfokus; Kraft und Rudern werden berücksichtigt.
- Änderungen nennen Auslöser und erwartete Auswirkung.
- Manuell gesperrte Einheiten bleiben unverändert.
- Ohne aktive Freigabe entsteht höchstens ein Vorschlag.
- Automatische Änderungen sind sichtbar und rücknehmbar.
- Eine ausgefallene Einheit wird nicht automatisch unreflektiert nachgeholt.

## 16.9 Performance- und Betriebsabnahme

Die in Kapitel 13 definierten Zielwerte werden in einer dokumentierten
Testumgebung gemessen. Zusätzlich werden Importwiederholung, Wiederherstellung,
externe Ausfälle, parallele Zugriffe und fehlerhafte Daten getestet. Abweichungen
benötigen eine bewusste Freigabe.

## 16.10 Definition of Done

Eine Backlog-Position gilt als erledigt, wenn:

- Akzeptanzkriterien erfüllt sind;
- Code und fachliche Regeln geprüft wurden;
- relevante automatisierte Tests bestehen;
- Datenschutz- und Sicherheitsauswirkungen bewertet sind;
- Oberfläche und Fehlerzustände umgesetzt sind;
- Dokumentation aktualisiert ist;
- Monitoring beziehungsweise Diagnose vorhanden ist;
- keine kritischen bekannten Fehler offen sind;
- ein nutzbarer Ablauf in einer geeigneten Umgebung demonstriert wurde.

## 16.11 Freigaberollen

In der anfänglichen Entwicklung übernimmt der Auftraggeber die fachliche
Produktabnahme. Technische Änderungen werden durch Code-Review und automatisierte
Prüfungen abgesichert. Sportwissenschaftlich kritische Modelle benötigen vor
breiter Nutzung eine geeignete fachliche Prüfung.

## 16.12 Schlussentscheidung

Die beschriebenen Qualitätsmerkmale und Abnahmekriterien bilden die verbindliche
Ausgangsbasis. Konkrete Toleranzen und Tests werden während der Implementierung
ergänzt, ohne die Anforderungen stillschweigend abzuschwächen.

