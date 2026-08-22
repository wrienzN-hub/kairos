# 19. Agile Roadmap und Entwicklungsinkremente

## 19.1 Grundsatz

Kairos besitzt keine starre Abfolge von Version 1, Version 2 und Version 3. Die
Roadmap beschreibt Ergebnisräume und sinnvolle Abhängigkeiten. Reihenfolge und
Umfang werden anhand tatsächlicher Erkenntnisse laufend angepasst.

## 19.2 Priorisierung

Backlog-Einträge werden bewertet nach:

- unmittelbarem Nutzwert;
- Beitrag zur Produktvision;
- fachlichem und gesundheitlichem Risiko;
- technischer Unsicherheit;
- Abhängigkeiten;
- Datenschutz- und Sicherheitsauswirkung;
- Umsetzungsaufwand;
- Möglichkeit, einen vollständigen Ablauf zu liefern;
- Erkenntnissen aus realer Nutzung.

## 19.3 Entwicklungsprinzipien

- vertikale Schnitte statt isolierter Schichten;
- kleinster sinnvoll nutzbarer Umfang;
- Messung und Feedback nach jedem Inkrement;
- technische Spikes nur für konkrete Unsicherheiten;
- keine frühe Vollautomatisierung;
- wenige validierte Kennzahlen statt breiter Scheingenauigkeit;
- Radfahren zuerst in fachlicher Tiefe;
- Krafttraining und Rudern im gemeinsamen Datenmodell berücksichtigen;
- Dokumentation und Tests wachsen mit dem Produkt.

## 19.4 Inkrement A – Lauffähige Produktbasis

**Ergebnis:** Kairos kann lokal reproduzierbar gestartet und weiterentwickelt
werden.

Umfang:

- Repository- und Projektstruktur;
- React-Web-App;
- ASP.NET-Core-Backend;
- PostgreSQL und Migrationen;
- Docker-Entwicklungsumgebung;
- grundlegende Konfiguration und Geheimnisverwaltung;
- automatisierte Build- und Testpipeline;
- Gesundheitsprüfung und strukturierte Fehlerbehandlung;
- erste Architekturentscheidungen als ADR.

Dieses Inkrement liefert noch kein Coaching, schafft aber die überprüfbare Basis
für den ersten vertikalen Ablauf.

## 19.5 Inkrement B – FIT-Aktivität End-to-End

**Ergebnis:** Ein Nutzer importiert eine Rad-FIT-Datei und sieht eine verlässliche
Aktivität.

Umfang:

- minimaler Nutzer-/Athletenkontext;
- sicherer FIT-Dateiupload;
- Parser für definierte Beispieldateien;
- Aktivität, Quelldaten und Messreihen;
- Duplikaterkennung;
- Datenqualitätsstatus;
- Aktivitätsübersicht und Detailseite;
- Löschung der importierten Aktivität;
- automatisierte Referenztests.

## 19.6 Inkrement C – Erste nützliche Trainingsanalyse

**Ergebnis:** Die importierte Radaktivität erzeugt eine konkrete, prüfbare
Trainingsaussage.

Umfang:

- Segment- beziehungsweise Intervallmodell;
- manuelle Prüfung und Korrektur erkannter Abschnitte;
- kleine Auswahl validierter Kennzahlen;
- strukturierte Analyse ohne freie KI-Abhängigkeit;
- subjektives Feedback bei wichtigen Einheiten;
- Ergebnisstruktur mit Datenbasis und Unsicherheit;
- Bewertung der Analyse durch den Nutzer.

Die genaue Kennzahlauswahl erfolgt anhand des Nutzens verfügbarer
Referenzaktivitäten.

## 19.7 Inkrement D – KI-Coach auf belastbarer Datenbasis

**Ergebnis:** Der Nutzer kann Fragen zu einer Aktivität stellen und erhält eine
erklärbare, datengebundene Antwort.

Umfang:

- kontrollierte Coach-Werkzeuge für Lesezugriffe;
- strukturierter Kontext statt unkontrollierter Rohdatenübergabe;
- Kernaussage, Datennachweis und Unsicherheit;
- Rückfragen bei fehlenden Informationen;
- Evaluationssatz gegen erfundene Fakten;
- Kosten- und Laufzeitmessung;
- deutliche KI-Kennzeichnung.

## 19.8 Inkrement E – Ziele und Vier-Wochen-Plan

**Ergebnis:** Ein Ziel wird mit einem konkreten Vier-Wochen-Trainingsblock
verbunden.

Umfang:

- Ziel- und Teilzielverwaltung;
- geplante Rad-, Kraft- und Rudereinheiten;
- Wochen- und Vier-Wochen-Ansicht;
- Zielbezug jeder Einheit;
- manuelle Sperre und Änderungshistorie;
- zunächst Vorschlagsmodus;
- Konflikt- und Belastungsprüfung.

## 19.9 Inkrement F – Erholung, Kalender und Wetter

**Ergebnis:** Kairos berücksichtigt aktuelle Lebens- und Umweltbedingungen.

Umfang:

- optionale Erholungsabfrage;
- geeignete Erholungsdatenquelle;
- minimierte Kalenderintegration;
- Wetterkontext für Outdoor-Radtraining;
- begründete Anpassungsvorschläge;
- Datenaktualität und Rückfallverhalten.

## 19.10 Inkrement G – Kraft- und Rudervertiefung

**Ergebnis:** Ergänzende Sportarten liefern eigenständigen Nutzen und werden
korrekt mit dem Radfokus koordiniert.

Umfang:

- komfortable Krafttrainingserfassung oder Integration;
- Gewicht-, Satz- und Wiederholungsverlauf;
- Belastungskonflikte mit Radtraining;
- Concept2- und RP3-Importprüfung;
- Ruderintervalle und relevante Kennzahlen;
- sportartenübergreifende Belastung mit sichtbarer Unsicherheit.

## 19.11 Inkrement H – Kontrollierte Automatisierung

**Ergebnis:** Ausgewählte Planänderungen können nach ausdrücklicher Freigabe
automatisch erfolgen.

Voraussetzungen:

- belastbare Vorschlagslogik;
- Nutzervertrauen und ausreichend Verlauf;
- definierte Änderungstypen und Grenzen;
- Audit und Rücknahme;
- Sicherheits- und Fehlerfälle;
- Akzeptanztests.

## 19.12 Spätere Ergebnisräume

Ohne feste Reihenfolge können später folgen:

- Szenarioanalyse und validierte Prognosen;
- Routenplanung;
- trainingsbezogene Ernährungshinweise;
- Coach-Zusammenarbeit;
- weitere Sportarten und Geräte;
- native Anwendungen;
- weiterführende Wettkampfplanung.

## 19.13 Start-Backlog für die Programmierung

Unmittelbar nach Abschluss des Lastenhefts sollen folgende Aufgaben angelegt
werden:

1. Repositoryzustand und lokale Werkzeuge prüfen.
2. Solution- und Monorepo-Struktur festlegen.
3. React-, .NET- und PostgreSQL-Grundgerüst erzeugen.
4. Docker-Compose-Entwicklungsumgebung erstellen.
5. erste ADRs für modularen Monolithen und Datenhaltung schreiben.
6. CI für Build und Tests einrichten.
7. FIT-Beispieldateien und erwartete Referenzwerte definieren.
8. minimales Aktivitätsdomänenmodell entwerfen.
9. sicheren Upload und ersten Parser implementieren.
10. erste Aktivitätsansicht als vertikalen Ablauf liefern.

## 19.14 Erfolgsmessung

Die Roadmap wird nicht an der Anzahl implementierter Funktionen gemessen,
sondern daran, ob Athleten mit verlässlichen Daten bessere Entscheidungen treffen
können. Für jedes Inkrement werden Nutzwert, Fehler, Datenqualität, Laufzeit,
Kosten und offene Risiken überprüft.

## 19.15 Schlussentscheidung

Die Inkremente sind eine priorisierte Arbeitsgrundlage, keine starre
Release-Zusage. Die Programmierung beginnt mit der Produktbasis und dem
FIT-End-to-End-Ablauf. Neue Ideen werden im Backlog bewertet und unterbrechen den
Kernpfad nur bei höherem Nutzen oder Risiko.

