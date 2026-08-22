# 11. Datenquellen und Integrationen

## 11.1 Zweck

Dieses Kapitel legt fest, welche externen Daten Kairos benötigt, wie
Integrationen fachlich behandelt werden und welche Qualitäts- und
Kontrollanforderungen gelten. Konkrete Bibliotheken, Protokolle und Anbieter sind
im Pflichtenheft beziehungsweise im Product Backlog zu entscheiden.

## 11.2 Grundprinzipien

Für jede Integration gelten:

- ausdrückliche Verbindung durch den Nutzer;
- minimal erforderlicher Berechtigungsumfang;
- sichtbare Quelle und letzte erfolgreiche Synchronisation;
- Widerruf und Trennung jederzeit möglich;
- Originaldaten bleiben von Kairos-Korrekturen unterscheidbar;
- Ausfälle externer Systeme dürfen bestehende Daten nicht beschädigen;
- keine einzelne Integration darf zur fachlich unkontrollierten Wahrheit werden;
- Import, Berechnung und KI-Interpretation bleiben getrennte Schritte.

## 11.3 Priorisierte Trainingsdaten

### 11.3.1 Garmin und FIT

Garmin-nahe Daten sind für den anfänglichen Radfokus besonders relevant.
Kairos soll Aktivitäten und verfügbare Gesundheits- beziehungsweise
Erholungsdaten übernehmen können. Ein manueller FIT-Dateiimport soll als
kontrollierbarer Einstieg oder Rückfallweg verfügbar sein.

Vor Nutzung einer Garmin-Anbindung sind Zulässigkeit, Authentifizierung,
Stabilität, Datenumfang und Nutzungsbedingungen zu prüfen. Inoffizielle
Schnittstellen oder Community-MCP-Server dürfen nicht ohne Risikoentscheidung zur
Produktionsgrundlage werden.

### 11.3.2 Radgeräte und Indoor-Plattformen

Mögliche Datenquellen sind Fahrradcomputer, Leistungsmesser, Herzfrequenzsensoren,
Smarttrainer und Indoor-Plattformen. Kairos soll Geräteinformationen übernehmen,
wenn sie für Vergleichbarkeit oder Datenqualität relevant sind.

### 11.3.3 Krafttraining

Krafttraining wird über Gewicht, Wiederholungen, Sätze und optionale Notizen
erfasst. Eine manuelle Erfassung soll als Rückfallweg verfügbar sein, reicht aber
nicht als alleinige Lösung. Eine geeignete Import- oder App-Integration wird früh
im Product Backlog berücksichtigt. Das Modell bleibt für spätere RPE-, RIR- oder
weitere Trainingsdaten erweiterbar.

### 11.3.4 Rudern

Für Indoor-Rudern werden Concept2 und RP3 priorisiert untersucht. Relevante Daten
sind insbesondere Zeit, Distanz, Pace, Leistung, Schlagfrequenz, Herzfrequenz und
Intervalle. Kairos muss hersteller- oder gerätespezifische Messunterschiede
kennzeichnen.

## 11.4 Kalender

Kalenderintegrationen sollen verfügbare Zeitfenster und Konflikte liefern. Der
Nutzer bestimmt, welche Kalender berücksichtigt werden. Kairos soll möglichst
nur Beginn, Ende, Belegungsstatus und optional eine freigegebene Kategorie
verwenden. Private Titel, Beschreibungen und Teilnehmer sind für die
Trainingsplanung grundsätzlich nicht erforderlich.

## 11.5 Wetter

Wetterdaten sollen Ort, Gültigkeitszeitraum, Abrufzeit und Anbieter enthalten.
Relevante Werte können Temperatur, gefühlte Temperatur, Niederschlag, Wind,
Gewitterrisiko und Warnungen umfassen. Prognosen sind veränderlich; eine darauf
basierende Planentscheidung muss ihren damaligen Prognosestand nachvollziehbar
machen.

## 11.6 Standort und Routen

Standort- und Routendaten sind besonders sensibel. Kairos darf sie nur für klar
benannte Funktionen verwenden. Wohn- oder regelmäßige Startorte sollen nicht
unnötig sichtbar gemacht werden. Eine spätere Routenintegration kann Distanz,
Höhenprofil, Untergrund und Wetter berücksichtigen, ist aber nicht Voraussetzung
für die erste Programmierphase.

## 11.7 KI-Dienste

Ein KI-Dienst erhält nur den für eine konkrete Aufgabe erforderlichen Kontext.
Personenbezug und Rohzeitreihen sollen reduziert werden, soweit der Zweck dies
zulässt. Anbieter, Region, Aufbewahrung, Trainingsnutzung und vertragliche
Datenschutzbedingungen sind vor produktiver Nutzung zu prüfen.

KI-Ausgaben werden nicht ungeprüft als Mess- oder Stammdaten gespeichert. Sie
bleiben als generierte Analyse, Hypothese oder Empfehlung gekennzeichnet.

## 11.8 Synchronisationsverhalten

Kairos soll:

- initialen und inkrementellen Import unterstützen;
- Zeitzonen und Einheiten normalisieren;
- Duplikate erkennen;
- geänderte Quellaktivitäten erkennen;
- Wiederholungsversuche kontrolliert durchführen;
- den letzten erfolgreichen und fehlgeschlagene Läufe anzeigen;
- Teilimporte als unvollständig markieren;
- keine stillen Datenverluste verursachen.

## 11.9 Konflikte zwischen Quellen

Bei widersprüchlichen Daten gilt keine pauschale Anbieterpriorität. Kairos soll
Quelle, Messmethode, Aktualität und Nutzerwahl berücksichtigen. Der Nutzer kann
eine bevorzugte Quelle je Datentyp bestimmen. Abweichungen werden dokumentiert;
Originalwerte bleiben erhalten.

## 11.10 Rückübertragung

Geplante Trainings können später an Garmin oder andere Geräte übertragen werden,
wenn eine zulässige und stabile Schnittstelle verfügbar ist. Schreibzugriffe
erfordern eine separate Freigabe. Vor dem Senden werden Inhalt, Zielsystem und
mögliche Überschreibungen angezeigt.

## 11.11 Ausfall und Anbieterwechsel

Kairos muss auch bei vorübergehendem Ausfall externer Systeme nutzbar bleiben.
Kernhistorie und bereits erzeugte Analysen dürfen nicht ausschließlich extern
gespeichert sein. Integrationen sollen austauschbar bleiben, damit ein
Anbieterwechsel nicht zum Verlust des Athletenverlaufs führt.

## 11.12 Abnahmekriterien

- Nutzer erkennen verbundene Quellen und Berechtigungen.
- Jede importierte Aktivität besitzt Quelle und Synchronisationsstatus.
- Duplikate beeinflussen die Belastung nicht doppelt.
- Trennung einer Quelle löscht nicht ohne Bestätigung historische Kairos-Daten.
- Fehler sind sichtbar und wiederholbar behandelbar.
- Concept2 und RP3 sind im Integrationsmodell vorgesehen.
- Schreibzugriffe sind getrennt von Lesezugriffen freizugeben.

## 11.13 Festgelegte Leitentscheidungen

1. Der erste praktisch umgesetzte Aktivitätsimport soll mit FIT-Dateien beginnen.
2. Eine ausschließlich manuelle Krafttrainingserfassung reicht nicht aus. Neben
   einem manuellen Rückfallweg soll früh eine geeignete Import- oder
   Integrationsmöglichkeit vorgesehen werden.

## 11.14 Freigabestatus

Dieses Kapitel wurde inhaltlich mit dem Auftraggeber abgestimmt und fachlich
freigegeben.
