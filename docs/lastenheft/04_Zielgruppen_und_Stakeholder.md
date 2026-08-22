# 4. Zielgruppen und Stakeholder

## 4.1 Zweck dieses Kapitels

Dieses Kapitel beschreibt die Personen und Gruppen, deren Bedürfnisse,
Entscheidungen oder Rahmenbedingungen Kairos beeinflussen. Es legt fest, für wen
das Produkt zunächst entwickelt wird und welche Interessen bei späteren
Anforderungen berücksichtigt werden müssen.

## 4.2 Primäre Zielgruppe

Die primäre Zielgruppe sind ambitionierte Freizeit- und Amateursportler mit
Hauptfokus auf Radfahren. Sie trainieren regelmäßig, verwenden bereits digitale
Trainingsgeräte oder Plattformen und möchten ihre Leistung strukturiert
verbessern, ohne sämtliche Trainingsdaten selbst sportwissenschaftlich auswerten
zu müssen.

Typische Merkmale dieser Zielgruppe:

- regelmäßiges Radtraining mit konkretem Leistungs- oder Wettkampfziel;
- Interesse an Kennzahlen wie Herzfrequenz, Leistung, FTP, Trainingsbelastung und
  Erholung;
- Nutzung eines Fahrradcomputers, einer Sportuhr, eines Smarttrainers oder
  Leistungsmessers;
- Bereitschaft, subjektives Trainingsfeedback zu geben;
- Wunsch nach konkreteren Erklärungen als in reinen Tracking-Plattformen;
- begrenzte Zeit für Planung und manuelle Analyse;
- ergänzendes Krafttraining zur Steigerung der Radleistung, Belastbarkeit oder
  Verletzungsprävention;
- optionales Rudern als weitere Ausdauersportart.

Die Zielgruppe umfasst sowohl datenaffine Nutzer als auch Sportler, die eine
einfache Handlungsempfehlung bevorzugen. Kairos muss daher Kernaussagen einfach
darstellen und fachliche Details bei Bedarf zugänglich machen.

## 4.3 Primäre Nutzerrollen

### STK-01 – Athlet

Der Athlet ist die wichtigste Nutzerrolle und zugleich Eigentümer seiner
persönlichen Daten. Er definiert Ziele, verbindet Datenquellen, dokumentiert
Training und Feedback, betrachtet Analysen und entscheidet über Empfehlungen.

**Hauptinteressen:**

- bessere Radleistung und verlässlicher Fortschritt;
- verständliche Trainingsanalyse;
- realistischer, anpassbarer Trainingsplan;
- Berücksichtigung von Krafttraining, Rudern, Erholung und Alltag;
- Kontrolle über Daten und Automatisierung;
- geringer manueller Verwaltungsaufwand.

**Verantwortung:**

- korrekte Angabe persönlicher Informationen;
- Prüfung von Empfehlungen im eigenen gesundheitlichen Kontext;
- Entscheidung über den Automatisierungsgrad;
- Einholung professioneller Hilfe bei medizinischen Fragen.

### STK-02 – Neuer oder weniger erfahrener Athlet

Diese Rolle besitzt Trainingsmotivation, aber nur begrenzte Erfahrung mit
Trainingssteuerung und Kennzahlen. Sie benötigt Erklärungen, sichere
Voreinstellungen und klare Grenzen.

**Besondere Bedürfnisse:**

- verständliche Einführung;
- Erläuterung von Fachbegriffen;
- Vermeidung scheinbar präziser oder überfordernder Empfehlungen;
- konservative Standardwerte;
- deutliche Hinweise bei unzureichender Datenbasis.

### STK-03 – Erfahrener, datenorientierter Athlet

Diese Rolle kennt Trainingszonen, Leistungsdiagnostik und Belastungskennzahlen
und möchte Annahmen sowie Berechnungen prüfen können.

**Besondere Bedürfnisse:**

- Zugriff auf Roh- und Detaildaten;
- transparente Berechnungsmethoden;
- konfigurierbare Bereiche und Schwellenwerte;
- Vergleiche ähnlicher Einheiten;
- Exportmöglichkeiten und nachvollziehbare Historie.

## 4.4 Sekundäre Zielgruppen

### STK-04 – Menschlicher Trainer oder Coach

Ein Coach ist zunächst keine zwingende Rolle im ersten Inkrement, aber ein
relevanter späterer Stakeholder. Mit ausdrücklicher Freigabe des Athleten könnte
er Ziele, Pläne, Analysen und Feedback einsehen oder kommentieren.

Kairos soll menschliches Coaching unterstützen, nicht heimlich ersetzen oder die
Verantwortungsverteilung unklar machen.

### STK-05 – Administrator und Betreiber

Der Betreiber verantwortet Verfügbarkeit, Sicherheit, Datenschutz,
Integrationen, Fehlerbehandlung und Support. Administrative Zugriffe auf
Nutzerdaten müssen auf das erforderliche Minimum beschränkt, protokolliert und
kontrolliert werden.

### STK-06 – Produktentwicklung

Produktverantwortliche, Entwickler, Designer und Tester pflegen das Product
Backlog, überprüfen Annahmen und liefern kleine, nutzbare Inkremente. Sie
benötigen überprüfbare Anforderungen, fachliche Definitionen und anonymisierte
oder synthetische Testdaten.

### STK-07 – Sportwissenschaftliche Fachberatung

Fachberater unterstützen bei Trainingsmodellen, Kennzahlen, Grenzwerten,
Validierung und Interpretation. KI-generierte Inhalte dürfen fachliche
Validierung nicht ersetzen.

### STK-08 – Datenschutz- und Sicherheitsverantwortliche

Diese Stakeholder bewerten Datenflüsse, Einwilligungen, Aufbewahrung,
Drittanbieter, Zugriffsschutz und Löschkonzepte. Ihre Anforderungen gelten nicht
erst vor Veröffentlichung, sondern während der gesamten Entwicklung.

## 4.5 Externe Stakeholder

### STK-09 – Daten- und Geräteanbieter

Garmin und mögliche weitere Plattformen oder Gerätehersteller stellen Daten und
Schnittstellen bereit. Verfügbarkeit, Nutzungsbedingungen, Authentifizierung und
Datenqualität liegen teilweise außerhalb der Kontrolle von Kairos.

### STK-10 – Wetter-, Kalender- und Kartendienste

Externe Dienste liefern Kontextinformationen. Kairos muss Herkunft, Aktualität
und Ausfälle berücksichtigen und darf externe Prognosen nicht als sichere Fakten
darstellen.

### STK-11 – KI-Dienstanbieter

Ein KI-Dienst kann Erklärungen, Dialog und strukturierte Auswertungen
unterstützen. Kairos bleibt für Datenschutz, fachliche Grenzen, Validierung und
das Verhalten des Gesamtprodukts verantwortlich.

### STK-12 – Regulatorische und rechtliche Stellen

Datenschutzrecht, Verbraucherrecht und mögliche regulatorische Einordnungen
setzen Grenzen für Verarbeitung, Kommunikation und Produktversprechen.

## 4.6 Stakeholder-Ziele und mögliche Konflikte

| Stakeholder | Wichtigstes Ziel | Möglicher Konflikt |
| --- | --- | --- |
| Athlet | hilfreiches, persönliches Coaching | Personalisierung benötigt Daten |
| Neuer Athlet | einfache und sichere Nutzung | Vereinfachung kann Details verbergen |
| Erfahrener Athlet | Transparenz und Kontrolle | Detailtiefe kann Bedienung erschweren |
| Coach | vollständiger Trainingskontext | Zugriff erfordert klare Freigabe |
| Betreiber | stabiler, wirtschaftlicher Betrieb | Kostenbegrenzung kann Analyseumfang beschränken |
| Entwicklung | schnelle Lernzyklen | Geschwindigkeit darf Qualität nicht verdrängen |
| Fachberatung | fachlich belastbare Aussagen | konservative Freigabe kann Innovation verlangsamen |
| Datenschutz | Datensparsamkeit | Langzeitcoaching profitiert von Historie |
| Datenanbieter | Einhaltung ihrer Bedingungen | Änderungen können Funktionen einschränken |

Konflikte werden nicht allein zugunsten maximaler Funktionalität entschieden.
Gesundheitliche Sicherheit, Nutzerkontrolle, Datenschutz und transparente
Kommunikation bilden verbindliche Grenzen.

## 4.7 Nicht adressierte Zielgruppen im aktuellen Umfang

Vorerst nicht im Mittelpunkt stehen:

- Profiteams und deren komplexe Mehrathleten-Steuerung;
- medizinische Einrichtungen und Rehabilitation;
- Kinder ohne gesondertes Schutz- und Einwilligungskonzept;
- Fitnessstudios und allgemeines Bodybuilding;
- ausschließlich soziale oder wettbewerbsorientierte Sportnetzwerke;
- vollständige Triathlon-, Lauf- oder Schwimmplanung;
- Anbieter medizinischer Ernährungstherapie.

## 4.8 Zugangs- und Schutzbedürfnisse

Bei der Produktgestaltung sind unterschiedliche Erfahrung, Sprache,
Sehfähigkeit, motorische Fähigkeiten und technische Ausstattung zu
berücksichtigen. Zentrale Informationen dürfen nicht ausschließlich über Farbe
kommuniziert werden. Empfehlungen müssen auch ohne sportwissenschaftliches
Vorwissen verständlich bleiben.

Besondere Vorsicht gilt bei sensiblen Gesundheitsdaten, Krankheit,
Verletzungshinweisen und stark belastenden Trainingsempfehlungen. Kairos muss
Unsicherheit sichtbar machen und darf Nutzer nicht zur Missachtung körperlicher
Warnsignale motivieren.

## 4.9 Spätere Backlog-Entscheidungen

Die primäre Zielgruppe „ambitionierte Freizeit- und Amateurradfahrer“ wird
übernommen. Berechtigungen eines menschlichen Coaches, eine mögliche Erweiterung
auf völlige Trainingseinsteiger und weitere Stakeholderrollen werden erst bei
konkreter Priorisierung der jeweiligen Funktion entschieden.

## 4.10 Freigabestatus

Dieses Kapitel wurde inhaltlich mit dem Auftraggeber abgestimmt und fachlich
freigegeben.
