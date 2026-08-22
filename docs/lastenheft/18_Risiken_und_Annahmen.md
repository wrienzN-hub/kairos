# 18. Risiken und Annahmen

## 18.1 Zweck

Dieses Kapitel macht Unsicherheit sichtbar und verhindert, dass Annahmen
unbemerkt zu Anforderungen oder Fakten werden. Risiken werden während der agilen
Entwicklung regelmäßig neu bewertet.

## 18.2 Bewertungsmodell

Risiken werden qualitativ nach Eintrittswahrscheinlichkeit und Auswirkung als
niedrig, mittel oder hoch bewertet. Hohe Risiken benötigen früh eine
Gegenmaßnahme oder bewusste Akzeptanz.

## 18.3 Zentrale Risiken

| ID | Risiko | Wahrscheinlichkeit | Auswirkung | Gegenmaßnahme |
| --- | --- | --- | --- | --- |
| R-01 | Garmin- oder andere Schnittstellen sind eingeschränkt oder instabil | Hoch | Hoch | FIT-Import als unabhängiger Einstieg; Integrationen austauschbar halten |
| R-02 | Messdaten sind unvollständig oder fehlerhaft | Hoch | Hoch | Datenqualitätsstatus, Plausibilitätsprüfung, Nutzerkorrektur |
| R-03 | Trainingskennzahlen werden fachlich falsch umgesetzt | Mittel | Hoch | dokumentierte Definitionen, Referenztests, fachliche Prüfung |
| R-04 | KI erfindet Fakten oder übertreibt Sicherheit | Hoch | Hoch | strukturierte Datenzugriffe, Evaluationssatz, Quellenbezug, Unsicherheitsregeln |
| R-05 | Empfehlungen führen zu ungeeigneter Belastung | Mittel | Hoch | konservative Grenzen, Nutzerkontrolle, Warnsignale, kein medizinischer Anspruch |
| R-06 | Datenschutzverletzung sensibler Langzeitdaten | Mittel | Hoch | EU-Hosting, Verschlüsselung, minimale Zugriffe, Vorfallkonzept, DPIA-Prüfung |
| R-07 | Standortdaten offenbaren sensible Routinen | Mittel | Hoch | Minimierung, Schutz regelmäßiger Orte, getrennte Freigaben |
| R-08 | Umfang wächst schneller als lieferbarer Nutzen | Hoch | Hoch | vertikale Inkremente, klares Backlog, Rad-Kernfokus |
| R-09 | Alleinige Entwicklung führt zu Wissens- und Qualitätsengpässen | Hoch | Mittel | Dokumentation, Automatisierung, kleine Module, externe Fachprüfung |
| R-10 | KI- und Cloudkosten wachsen unkontrolliert | Mittel | Mittel | Kostenmessung, Kontextbegrenzung, Caching, Budgets |
| R-11 | Sportartenübergreifende Belastung wird zu stark vereinfacht | Mittel | Hoch | getrennte Modelle, sichtbare Unsicherheit, keine naive Addition |
| R-12 | Automatische Planung verliert Nutzervertrauen | Mittel | Hoch | Vorschlagsmodus, explizite Freigabe, Änderungsverlauf, Rücknahme |
| R-13 | Concept2/RP3-Daten sind nicht einheitlich integrierbar | Mittel | Mittel | Adapter, Datei-/Manuallösung, frühe Schnittstellenanalyse |
| R-14 | Krafttrainingserfassung ist zu aufwendig | Hoch | Mittel | schnelle Eingabe, Vorlagen, frühe Integrationssuche |
| R-15 | Rechtliche Einordnung verändert sich durch neue Funktionen | Mittel | Hoch | Zweckbestimmung prüfen, Rechtsprüfung vor Gesundheits-/Minderjährigenfunktionen |
| R-16 | Nutzer interpretieren Prognosen als Garantie | Mittel | Hoch | Ergebnisbereiche, Annahmen, klare Kennzeichnung und Sprache |

## 18.4 Fachliche Risiken im Detail

### Vergleichbarkeit

Geräte, Sensoren, Umgebungen und Leistungsbereiche verändern sich. Historische
Vergleiche können dadurch irreführend werden. Kairos muss Gerätewechsel,
Indoor-/Outdoor-Kontext und damalige Zonen berücksichtigen.

### Herzfrequenzinterpretation

Herzfrequenz wird durch Temperatur, Müdigkeit, Stress, Hydration und Messfehler
beeinflusst. Sie darf nicht isoliert als sichere Ursache oder Leistungsbewertung
verwendet werden.

### Prognosequalität

Langfristige Leistungsentwicklung ist nicht linear. Frühe Prognosen besitzen
wenig individuelle Daten. Kairos soll zunächst Fortschritt beschreiben und
Szenarien erst nach belastbarer Basisplanung ergänzen.

## 18.5 Technische Risiken im Detail

### FIT-Komplexität

FIT-Dateien unterscheiden sich nach Hersteller, Profil und enthaltenen Feldern.
Der Import beginnt mit klar definierten unterstützten Varianten und muss
unbekannte optionale Felder robust behandeln.

### Zeitreihendaten

Lange Aktivitäten erzeugen viele Messpunkte. Speicherung, Abfragen und Diagramme
können langsam oder teuer werden. Das Datenmodell muss Rohdaten, normalisierte
Werte und abgeleitete Zusammenfassungen bewusst trennen.

### Externe Abhängigkeiten

Wetter, Kalender, KI und Trainingsanbieter können ausfallen oder Bedingungen
ändern. Kairos benötigt kontrollierte Rückfallzustände und darf Kernhistorie nicht
nur extern halten.

## 18.6 Produkt- und Nutzungsrisiken

- zu viele Kennzahlen überfordern Nutzer;
- zu wenig Details enttäuschen erfahrene Athleten;
- Benachrichtigungen können Druck oder Schuld erzeugen;
- manuelle Krafttrainingseingabe kann nicht dauerhaft akzeptiert werden;
- ein Chat-first-Design könnte strukturierte Nachvollziehbarkeit verdecken;
- frühe Automatisierung kann Vertrauen zerstören;
- fehlende sportwissenschaftliche Validierung kann falsche Autorität erzeugen.

Die Gegenstrategie ist progressive Offenlegung, konfigurierbare Interaktion und
frühe Tests mit realen Nutzungsszenarien.

## 18.7 Annahmen

### A-01

Der anfängliche Nutzer ist bereit, FIT-Dateien bereitzustellen und Aktivitätsdaten
zu prüfen.

### A-02

Radaktivitäten enthalten häufig ausreichend Zeit-, Herzfrequenz- oder
Leistungsdaten für mindestens eine nützliche Analyse.

### A-03

Der Nutzer gibt bei wichtigen Einheiten subjektives Feedback, wenn die Abfrage
kurz und konfigurierbar ist.

### A-04

Ein Vier-Wochen-Planungshorizont ist für konkrete Planung praktikabel.

### A-05

React, .NET und PostgreSQL sind für den ersten modularen Produktkern geeignet.

### A-06

OpenAI kann ausgewählte Coach-Aufgaben unterstützen, während Berechnungen und
Berechtigungen außerhalb des Sprachmodells bleiben.

### A-07

EU-Hosting ist technisch und wirtschaftlich umsetzbar.

### A-08

Concept2 und RP3 können mindestens über eine dokumentierbare Import- oder
Erfassungsmöglichkeit berücksichtigt werden.

### A-09

Krafttraining benötigt mittelfristig eine komfortablere Lösung als ausschließlich
manuelle Eingabe.

## 18.8 Validierung von Annahmen

Jede Annahme erhält bei Relevanz einen Backlog-Eintrag mit Prüfmethode. Geeignete
Methoden sind technische Spikes, Prototypen, Referenzdateien, Nutzertests,
Kostenmessungen und rechtliche beziehungsweise fachliche Prüfung.

Widerlegte Annahmen führen zu einer dokumentierten Anpassung von Backlog,
Pflichtenheft oder Architektur. Die Produktvision wird nur bei einer bewussten
Entscheidung verändert.

## 18.9 Risikoreview

Risiken werden mindestens vor Beginn eines größeren Inkrements, vor produktiver
Verarbeitung neuer Datenklassen und vor Aktivierung automatischer Aktionen
überprüft. Kritische neue Risiken können laufende Entwicklung neu priorisieren.

## 18.10 Schlussentscheidung

Die größten frühen Risiken sind Integrationsabhängigkeit, Datenqualität,
KI-Faktentreue und zu großer Funktionsumfang. Der erste Entwicklungsabschnitt
adressiert sie durch FIT-Import, wenige validierte Analysen, klare Datenherkunft
und einen begrenzten End-to-End-Ablauf.

