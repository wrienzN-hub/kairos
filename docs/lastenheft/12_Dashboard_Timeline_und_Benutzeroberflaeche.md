# 12. Dashboard, Timeline und Benutzeroberfläche

## 12.1 Zielbild

Die Benutzeroberfläche soll komplexe Trainingsinformationen in klare
Entscheidungen übersetzen. Sie darf weder wie eine unpriorisierte Datensammlung
noch wie ein reiner Chat wirken. Die wichtigste Information steht zuerst;
Details und Berechnungen bleiben erreichbar.

## 12.2 Navigationsbereiche

Kairos soll mindestens folgende Bereiche besitzen:

- **Heute:** aktuelle Empfehlung, geplantes Training und offene Entscheidungen;
- **Training:** Aktivitäten, Analysen und Vergleiche;
- **Plan:** Vier-Wochen-Block und Kalender;
- **Ziele:** Fortschritt, Teilziele und Risiken;
- **Timeline:** Training und relevante Ereignisse im zeitlichen Kontext;
- **Coach:** Dialog und begründete Rückfragen;
- **Profil und Daten:** Sportarten, Zonen, Quellen, Datenschutz und
  Automatisierung.

Die endgültige Navigation wird durch Prototypen und Nutzungstests validiert.

## 12.3 Dashboard „Heute“

Das Dashboard soll auf einen Blick zeigen:

1. heutige Trainingseinheit oder Ruhetag;
2. kurze Begründung;
3. aktuelle Erholung und Datenaktualität;
4. relevante Kalender- oder Wetterhinweise;
5. Status des wichtigsten Ziels;
6. offene Bestätigung oder fehlendes Feedback;
7. Zugang zum Coach-Dialog.

Es soll keine künstliche Gesamtpunktzahl dominieren, deren Bedeutung nicht
erklärbar ist.

## 12.4 Aktivitätsansicht

Eine Aktivitätsansicht enthält:

- Kernaussage der Analyse;
- Trainingszweck und Zielerfüllung;
- Aktivitäts- und Datenqualitätsstatus;
- erkannte Abschnitte oder Intervalle;
- relevante Diagramme und Kennzahlen;
- positive Beobachtungen und Abweichungen;
- subjektives Feedback;
- konkrete nächste Handlung;
- Vergleich mit geeigneten Einheiten;
- Korrektur- und Bewertungsmöglichkeit.

Rad-, Kraft- und Ruderaktivitäten erhalten sportartspezifische Darstellungen.

## 12.5 Vier-Wochen-Plan

Die Planungsansicht stellt vier Wochen konkret dar. Geplante, absolvierte,
ausgefallene und geänderte Einheiten sind eindeutig unterscheidbar. Der Nutzer
kann:

- Einheiten öffnen und bearbeiten;
- manuell sperren;
- Vorschläge annehmen oder ablehnen;
- Änderungen zurücknehmen;
- Konflikte und Begründungen ansehen;
- zwischen Rad-, Kraft- und Rudertraining unterscheiden;
- die erwartete Belastungsverteilung nachvollziehen.

Langfristige Ziele werden oberhalb des konkreten Planungshorizonts als
Orientierung dargestellt.

## 12.6 Timeline

Die Timeline verbindet:

- Aktivitäten und Trainingsblöcke;
- Ziel- und Leistungsänderungen;
- Erholungs- und Feedbackereignisse;
- Krankheit, Beschwerden, Urlaub oder Trainingspause;
- Wettkämpfe und Tests;
- Planänderungen;
- Ausrüstungs- oder Datenquellenwechsel.

Filter verhindern Überladung. Sensitive Ereignisse sind standardmäßig privat und
werden nicht unnötig in Übersichten eingeblendet.

## 12.7 Ziele und Prognosen

Die Zielansicht zeigt Ausgangslage, aktuellen Stand, Verlauf, Teilziele,
Datenqualität und gefährdende Faktoren. Prognosen müssen visuell klar von
Messwerten getrennt sein. Prozentanzeigen sind nur zulässig, wenn ihre Bedeutung
erklärbar ist.

## 12.8 Coach-Dialog

Der Dialog zeigt deutlich, dass der Nutzer mit einer KI interagiert. Antworten
beginnen mit einer kurzen Kernaussage. Datenbasis, Unsicherheit und Details sind
aufklappbar. Verweise führen zu den betroffenen Aktivitäten oder Plänen.

Wenn Informationen fehlen, darf Kairos gezielte Rückfragen stellen. Eine
Planänderung erfordert eine separate, strukturierte Aktion und darf nicht nur im
Fließtext versteckt sein.

## 12.9 Feedback und Eingabe

Feedback wird besonders nach wichtigen Einheiten angefragt und bleibt
konfigurierbar. Die Eingabe soll in wenigen Schritten möglich sein. Typische
Felder sind Anstrengung, muskuläre Ermüdung, Beschwerden, Reserven und Notiz.

Krafttraining benötigt eine schnelle Satz- und Wiederholungserfassung. Häufige
Übungen und letzte Werte können vorgeschlagen, aber nicht ungeprüft übernommen
werden.

## 12.10 Zustände und Transparenz

Die Oberfläche muss klar unterscheiden:

- aktuell, veraltet, synchronisierend und fehlerhaft;
- gemessen, berechnet, geschätzt und KI-generiert;
- geplant, vorgeschlagen, automatisch geändert und manuell festgelegt;
- vollständig, eingeschränkt analysierbar und unzureichend.

## 12.11 Barrierefreiheit und Responsivität

Zentrale Funktionen sollen auf Desktop und Mobilgerät nutzbar sein.
Informationen dürfen nicht nur durch Farbe vermittelt werden. Tastaturbedienung,
ausreichende Kontraste, verständliche Beschriftungen und skalierbare Texte sind
vorzusehen.

## 12.12 Fehlertoleranz

Fehlerseiten sollen Ursache, betroffene Daten und nächste Handlung nennen.
Nutzereingaben dürfen bei vorübergehenden Verbindungsproblemen nicht unnötig
verloren gehen. Destruktive Aktionen erfordern eine klare Bestätigung oder
wiederherstellbare Zwischenstufe.

## 12.13 Abnahmekriterien

- Die heutige Empfehlung ist ohne Suche auffindbar.
- Messung, Prognose und KI-Empfehlung sind unterscheidbar.
- Der Vier-Wochen-Plan zeigt jede automatische Änderung.
- Eine wichtige Aktivität kann mit wenigen Eingaben bewertet werden.
- Datenqualität und Aktualität sind sichtbar.
- Die Kernfunktionen sind mobil nutzbar.

## 12.14 Festgelegte Leitentscheidungen

1. Kairos wird zunächst als responsive Web-App umgesetzt.
2. „Heute“ wird als zentrale Startseite und primärer Einstieg verwendet.

## 12.15 Freigabestatus

Dieses Kapitel wurde inhaltlich mit dem Auftraggeber abgestimmt und fachlich
freigegeben.
