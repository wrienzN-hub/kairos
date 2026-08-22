# 20. Glossar und Anhänge

## 20.1 Zweck

Dieses Kapitel vereinheitlicht zentrale Begriffe und verweist auf ergänzende
Artefakte. Das Glossar wird mit Produkt und Pflichtenheft fortlaufend erweitert.

## 20.2 Produkt- und Planungsbegriffe

**Athlet:** Nutzer, dessen Training, Ziele und Daten in Kairos verwaltet werden.

**Kairos:** KI-gestützte, erklärbare Coaching-Plattform mit Hauptfokus Radfahren,
unterstützendem Krafttraining und Rudern.

**Ziel:** Messbarer oder eindeutig beschreibbarer gewünschter Zustand mit
Zeitbezug und Priorität.

**Teilziel:** Überprüfbarer Zwischenschritt auf dem Weg zu einem Ziel.

**Trainingsblock:** Zusammenhängender Planungsabschnitt mit einem fachlichen
Schwerpunkt. Kairos plant konkret in Vier-Wochen-Blöcken.

**Schlüsseleinheit:** Einheit mit besonders hohem Beitrag zu einem priorisierten
Ziel.

**Automatisierungsmodus:** Festlegung, ob Kairos nur analysiert, Änderungen
vorschlägt oder ausgewählte Änderungen selbst ausführt.

**Product Backlog:** Laufend priorisierte Liste fachlicher und technischer Arbeit.

**Inkrement:** Nutzbarer, überprüfbarer Entwicklungsfortschritt ohne starre
Versionsbindung.

## 20.3 Trainingsbegriffe

**FTP:** Functional Threshold Power; modellabhängige Schätzung einer über einen
längeren Zeitraum haltbaren Radleistung. Methode und Gültigkeitszeitraum müssen
angegeben werden.

**VO2max:** Maximale Sauerstoffaufnahme beziehungsweise deren geräte- oder
modellbasierte Schätzung. Messung und Schätzung sind zu unterscheiden.

**Herzfrequenzzone:** Individueller Intensitätsbereich auf Grundlage einer
dokumentierten Methode.

**Leistungszone:** Intensitätsbereich auf Grundlage von Leistung und einer
definierten Referenz wie FTP.

**Intervall:** Geplanter oder erkannter Belastungsabschnitt innerhalb einer
Trainingseinheit.

**RPE:** Rate of Perceived Exertion; subjektive Bewertung der Anstrengung.

**RIR:** Repetitions in Reserve; subjektive Schätzung verbleibender
Wiederholungen im Krafttraining.

**HRV:** Heart Rate Variability beziehungsweise Herzratenvariabilität; ihre
Interpretation benötigt individuellen Verlauf und Kontext.

**Trainingsbelastung:** Modellbasierte Beschreibung des Trainingsreizes. Werte
unterschiedlicher Modelle sind nicht automatisch vergleichbar.

**Aerobe Entkopplung/Drift:** Veränderung des Verhältnisses von äußerer Leistung
zu innerer Reaktion während längerer Belastung; Berechnungsmethode ist zu
dokumentieren.

**Pace:** Zeit pro Distanz, insbesondere beim Rudern relevant.

**Schlagfrequenz:** Ruderschläge pro Minute.

## 20.4 Daten- und Analysebegriffe

**Rohdaten:** Möglichst unveränderte Daten aus einer Quelle.

**Normalisierte Daten:** In ein einheitliches Modell, eine Zeitzone oder Einheit
überführte Daten.

**Provenienz:** Nachweis von Herkunft, Zeitpunkt und Verarbeitung eines Datums.

**Datenqualität:** Eignung der verfügbaren Daten für eine konkrete Auswertung.

**Messwert:** Direkt erfasster Wert aus einer benannten Quelle.

**Berechnung:** Reproduzierbar aus dokumentierten Eingangsdaten erzeugter Wert.

**Schätzung:** Abgeleiteter Wert mit Unsicherheit.

**Hypothese:** Plausible, aber nicht bestätigte Erklärung.

**Prognose:** Modellbasierte Aussage über eine mögliche zukünftige Entwicklung
unter Annahmen.

**Szenario:** Vergleich einer möglichen Entwicklung bei veränderten Annahmen.

**FIT:** Binäres Dateiformat für Fitness- und Aktivitätsdaten.

## 20.5 KI-Begriffe

**KI-Coach:** Interaktions- und Erklärungsebene, die strukturierte Kairos-Daten
verwendet. Sie ersetzt keine verbindlichen Berechnungs- oder Sicherheitsregeln.

**LLM:** Large Language Model; Sprachmodell zur Verarbeitung und Erzeugung von
Text beziehungsweise strukturierten Ausgaben.

**Tool Calling:** Kontrollierter Aufruf freigegebener Daten- oder
Anwendungsfunktionen durch ein KI-System.

**Memory/Athleten-Memory:** Sichtbare, korrigierbare langfristige Informationen
und Muster über den Athleten.

**Erklärbarkeit:** Nachvollziehbarkeit von Datenbasis, Methode, Unsicherheit und
Handlungsgrund einer Aussage.

**Halluzination:** Nicht durch vorhandene Daten gedeckte, vom KI-System erzeugte
Behauptung.

**MCP:** Model Context Protocol; standardisierte Möglichkeit, einer KI Werkzeuge
und Datenquellen anzubieten. MCP ersetzt nicht Kairos-Datenmodell,
Berechtigungsprüfung oder Geschäftslogik.

## 20.6 Technikbegriffe

**Modularer Monolith:** Eine gemeinsam ausgelieferte Anwendung mit klar getrennten
fachlichen Modulen.

**ADR:** Architecture Decision Record; kurze versionierte Dokumentation einer
Architekturentscheidung und ihrer Begründung.

**API:** Programmierschnittstelle zwischen Systemen oder Komponenten.

**CI/CD:** Automatisierte Prüfung und Auslieferung von Softwareänderungen.

**Migration:** Versionierte Änderung des Datenbankschemas.

**Idempotenz:** Eigenschaft, dass eine wiederholte Operation nicht zu
unerwünschten Mehrfachwirkungen führt.

**Audit Trail:** Nachvollziehbare Historie relevanter Änderungen und Zugriffe.

## 20.7 Datenschutzbegriffe

**Personenbezogene Daten:** Informationen, die sich auf eine identifizierte oder
identifizierbare Person beziehen.

**Gesundheitsdaten:** Besonders geschützte personenbezogene Daten zum körperlichen
oder geistigen Gesundheitszustand.

**Einwilligung:** Freiwillige, informierte, eindeutige und widerrufbare Zustimmung
zu einem bestimmten Verarbeitungszweck.

**Datenminimierung:** Verarbeitung nur der für einen Zweck erforderlichen Daten.

**DPIA/DSFA:** Datenschutz-Folgenabschätzung für voraussichtlich risikoreiche
Verarbeitungen.

**Pseudonymisierung:** Trennung direkter Identifikatoren; die Daten bleiben
personenbezogen, wenn eine Zuordnung möglich ist.

**Anonymisierung:** Irreversible Entfernung des Personenbezugs nach dem jeweils
anwendbaren rechtlichen Maßstab.

## 20.8 Anforderungsbegriffe

**Must:** Für den betrachteten nutzbaren Ablauf zwingend erforderlich.

**Should:** Hoher Nutzen, aber bei begründeter Priorisierung verschiebbar.

**Could:** Sinnvolle spätere Ergänzung.

**Won't for now:** Im aktuellen Planungshorizont bewusst ausgeschlossen.

**Akzeptanzkriterium:** Überprüfbare Bedingung für die fachliche Abnahme.

**Definition of Done:** Gemeinsame Qualitätsbedingungen, die für erledigte Arbeit
gelten.

## 20.9 Anhänge und weiterführende Artefakte

Zum Lastenheft gehören beziehungsweise daraus entstehen:

- [Projektkontext](../PROJECT_CONTEXT.md)
- [Kapitelübersicht](README.md)
- Pflichtenheft mit technischer Umsetzung;
- Product Backlog und erste Issues;
- Architecture Decision Records;
- System- und Datenflussdiagramme;
- Datenmodell und Datenwörterbuch;
- OpenAPI-Beschreibung;
- FIT-Referenzdateien und erwartete Werte;
- sportwissenschaftliche Berechnungsdefinitionen;
- KI-Evaluationsdatensatz;
- Datenschutz- und Sicherheitsdokumentation;
- Betriebs- und Wiederherstellungsanleitung.

## 20.10 Dokumentstatus

Mit diesem Kapitel ist die geplante Struktur des Kairos-Lastenhefts vollständig.
Die Kapitel 1 bis 15 wurden bereits einzeln fachlich freigegeben. Die Kapitel 16
bis 20 bilden den Abschlussentwurf zur gemeinsamen Schlussabnahme.

Nach der Schlussabnahme wird aus dem Lastenheft das technische Pflichtenheft und
der konkrete Start-Backlog abgeleitet. Die erste Programmierung beginnt mit der
lauffähigen Produktbasis und dem FIT-End-to-End-Ablauf.

