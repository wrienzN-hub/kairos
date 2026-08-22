# 17. Rahmenbedingungen und Randbedingungen

## 17.1 Projektmodell

Kairos wird agil und iterativ entwickelt. Es gibt keine vorab festgeschriebene
Folge starrer Produktversionen. Das Product Backlog wird nach Nutzen, Risiko,
Abhängigkeiten und Nutzerfeedback priorisiert. Jedes Inkrement soll einen
prüfbaren End-to-End-Nutzen liefern.

## 17.2 Organisatorische Rahmenbedingungen

- Die Entwicklung beginnt als kleines beziehungsweise einzelnes Projektteam.
- Entscheidungen und Anforderungen werden im Git-Repository dokumentiert.
- GitHub dient als bevorzugte Plattform für Quellcode, Issues und CI/CD.
- Das Lastenheft beschreibt das fachliche Soll; technische Entscheidungen werden
  im Pflichtenheft und in Architecture Decision Records festgehalten.
- Funktionsumfang wird nicht nur nach Wunschliste, sondern nach lieferbarem Nutzen
  priorisiert.

## 17.3 Technologischer Ausgangsrahmen

Als derzeit vorgesehene technische Richtung gelten:

- React für die responsive Weboberfläche;
- ASP.NET Core/.NET für Backend und fachliche Logik;
- PostgreSQL für relationale und fachliche Daten;
- Docker für reproduzierbare Entwicklungs- und Betriebsumgebungen;
- OpenAI für ausgewählte KI-Coach-Funktionen;
- n8n für Orchestrierung, geplante Abläufe und Integrationen;
- GitHub Actions für automatisierte Prüfungen und spätere Auslieferung.

Diese Auswahl ist eine starke Ausgangsannahme, aber keine Erlaubnis, fachliche
Anforderungen an ein ungeeignetes Werkzeug anzupassen. Änderungen werden als ADR
dokumentiert.

## 17.4 Architekturrahmen

Für den Beginn wird ein modularer Monolith bevorzugt. Fachliche Module wie
Import, Aktivitäten, Analyse, Planung, Ziele und Coaching erhalten klare Grenzen.
Microservices werden erst bei einem nachgewiesenen organisatorischen oder
technischen Bedarf eingeführt.

Kerngeschäftslogik verbleibt im Backend. n8n darf Integrationen und Abläufe
orchestrieren, ist aber nicht die maßgebliche Quelle für Trainingsberechnungen,
Berechtigungen oder Coachingregeln.

## 17.5 Entwicklungsumgebung

- Lokale Entwicklung muss ohne produktive personenbezogene Daten möglich sein.
- Abhängigkeiten werden reproduzierbar eingerichtet.
- Geheimnisse liegen nicht im Repository.
- Datenbankmigrationen sind versioniert.
- Beispieldaten sind synthetisch oder wirksam anonymisiert.
- Unterstützte Start- und Testbefehle werden dokumentiert.

## 17.6 Daten- und Hostingrahmen

- Primäres Hosting und primäre Datenverarbeitung erfolgen innerhalb der EU.
- PostgreSQL bildet die dauerhafte fachliche Datenbasis.
- Externe Anbieter werden über kontrollierte Integrationen angebunden.
- Der Nutzer erhält früh einen vollständigen Datenexport.
- Trainingshistorie bleibt standardmäßig bis zur Nutzerlöschung erhalten.
- Backups, Löschung und Wiederherstellung werden vor Produktivbetrieb getestet.

## 17.7 Geräte- und Plattformrahmen

Kairos startet als responsive Web-App. Native Mobil- oder Desktop-Apps sind
keine Voraussetzung für den ersten Entwicklungsabschnitt. Die Web-App muss auf
aktuellen Desktop- und Mobilbrowsern funktionieren.

Der erste Aktivitätsimport erfolgt über FIT-Dateien. Garmin, Concept2, RP3 und
eine geeignete Krafttrainingsintegration werden anschließend anhand verfügbarer,
zulässiger Schnittstellen priorisiert.

## 17.8 Fachliche Grenzen

- Radfahren ist der Hauptfokus.
- Krafttraining dient primär der Unterstützung der Radleistung.
- Rudern bleibt aktive zusätzliche Sportart.
- Ernährung ist eine spätere Nebenfunktion.
- Kairos ist kein Medizinprodukt und keine Notfallüberwachung.
- Laufen, Schwimmen und vollständige Triathlonplanung gehören aktuell nicht zum
  Fokus.

## 17.9 Rechtliche Rahmenbedingungen

DSGVO, relevante nationale Datenschutzregelungen und der EU AI Act sind für den
konkreten Betrieb zu prüfen. Minderjährige dürfen erst nach Umsetzung eines
rechtlich geprüften Schutz- und Einwilligungskonzepts zugelassen werden.

Produktversprechen, Oberfläche und technische Zweckbestimmung müssen die Grenze
zu Diagnose und Behandlung konsistent einhalten.

## 17.10 Kostenrahmen

Ein festes Budget ist derzeit nicht definiert. Deshalb gelten:

- externe Kosten werden je Funktion messbar gemacht;
- KI-Aufrufe werden auf erforderlichen Kontext begrenzt;
- kostenpflichtige Dienste benötigen eine dokumentierte Nutzenentscheidung;
- lokale Entwicklung soll ohne unnötige laufende Cloudkosten möglich sein;
- Skalierung erfolgt anhand realer Nutzung.

## 17.11 Zeitrahmen

Es bestehen keine künstlichen Release-Termine oder starren Versionsgrenzen. Der
erste Fokus liegt auf einem funktionsfähigen vertikalen Ablauf vom FIT-Import bis
zum erklärbaren Trainingsfeedback. Fortschritt wird über abgeschlossene
Inkremente und Akzeptanzkriterien gemessen.

## 17.12 Dokumentationsrahmen

Folgende Artefakte werden mit der Entwicklung gepflegt:

- Lastenheft und späteres Pflichtenheft;
- Product Backlog;
- ADRs;
- Datenmodell und Datenwörterbuch;
- API-Dokumentation;
- Berechnungs- und Analysedefinitionen;
- Datenschutz- und Sicherheitsartefakte;
- Test- und Evaluationsfälle;
- Betriebs- und Wiederherstellungsanleitung.

## 17.13 Schlussentscheidung

Die Rahmenbedingungen gelten als Ausgangsbasis für die technische Planung. Neue
Abhängigkeiten oder grundlegende Abweichungen werden nicht stillschweigend
eingeführt, sondern im Backlog beziehungsweise als ADR nachvollziehbar gemacht.

