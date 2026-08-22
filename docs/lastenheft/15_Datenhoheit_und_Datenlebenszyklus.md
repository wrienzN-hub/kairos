# 15. Datenhoheit und Datenlebenszyklus

## 15.1 Ziel

Der Athlet soll nachvollziehen und kontrollieren können, welche Daten Kairos
besitzt, woher sie stammen, wofür sie verwendet werden, wie lange sie bestehen
und an wen sie übermittelt werden. Datenhoheit ist ein Produktmerkmal und nicht
nur eine rechtliche Pflicht.

## 15.2 Datenklassen

Kairos unterscheidet mindestens:

- Konto- und Kontaktdaten;
- Athletenprofil und Einstellungen;
- Ziele und Trainingspläne;
- importierte Aktivitäts- und Zeitreihendaten;
- Kraft- und Ruderdaten;
- Erholungs- und Gesundheitsdaten;
- Standort-, Routen-, Kalender- und Wetterdaten;
- subjektives Feedback und freie Notizen;
- berechnete Kennzahlen;
- erkannte Muster und Athletenannahmen;
- KI-generierte Analysen und Empfehlungen;
- Einwilligungs-, Änderungs- und Sicherheitsnachweise;
- technische Betriebsdaten.

## 15.3 Herkunft und Provenienz

Jeder fachlich relevante Datensatz soll nach Möglichkeit besitzen:

- Quelle;
- ursprünglichen Erfassungszeitpunkt;
- Import- oder Erstellungszeitpunkt;
- Einheit und Zeitzone;
- Original- oder abgeleiteten Status;
- Qualitätskennzeichnung;
- Versions- beziehungsweise Änderungsverweis;
- Zweck und betroffene Funktionen.

## 15.4 Lebenszyklus

### Erhebung

Daten werden nur für benannte Zwecke importiert oder eingegeben. Optionale
Quellen sind standardmäßig nicht verbunden.

### Validierung

Kairos prüft Format, Plausibilität, Vollständigkeit und Duplikate. Fehlerhafte
Daten werden gekennzeichnet und nicht stillschweigend repariert.

### Nutzung

Zugriffe folgen Zweck und Berechtigung. Analysen erhalten nur die erforderlichen
Daten. Eine neue Nutzung benötigt eine neue Prüfung und gegebenenfalls
Einwilligung.

### Änderung

Nutzerkorrekturen überschreiben die Herkunft nicht. Bei relevanten Änderungen
wird nachvollziehbar, welche Analysen neu berechnet wurden.

### Archivierung

Historische Daten können für Langzeitvergleiche erforderlich sein. Der Nutzen
muss gegen Speicherbegrenzung und Nutzerwunsch abgewogen werden.

### Export

Der Nutzer kann Stammdaten, Aktivitäten, Ziele, Pläne, Feedback und abgeleitete
Ergebnisse in dokumentierten, maschinenlesbaren Formaten exportieren.

### Löschung

Löschung erfasst Primärdaten, Ableitungen, Suchindizes, KI-Memory und soweit
möglich Drittanbieter-Kopien. Backups werden nach dokumentierten Fristen
ausrotiert. Gesetzlich erforderliche Nachweise werden getrennt und minimiert
aufbewahrt.

## 15.5 Originaldaten und Ableitungen

Originale Importdaten, Nutzerkorrekturen, deterministische Berechnungen und
KI-Ausgaben bleiben unterscheidbar. Wird eine Quelle korrigiert oder gelöscht,
muss Kairos prüfen, welche Ableitungen ungültig werden. Historische Coach-Antworten
dürfen erhalten bleiben, müssen aber ihren damaligen Datenstand erkennen lassen.

## 15.6 Athleten-Memory

Gespeicherte Muster und Annahmen benötigen:

- verständliche Bezeichnung;
- zugrunde liegende Beobachtungen;
- Erstellungszeitpunkt;
- Bestätigungs- oder Unsicherheitsstatus;
- letzte Verwendung;
- Korrektur- und Löschmöglichkeit.

Einmalige Beobachtungen dürfen nicht automatisch zu dauerhaften Eigenschaften
des Athleten werden.

## 15.7 Aufbewahrung

Aufbewahrungsfristen werden je Datenklasse festgelegt. Grundsätze:

- Trainingshistorie bleibt erhalten, solange der Nutzer sie für Coaching nutzt;
- Rohzeitreihen können gesonderte, konfigurierbare Fristen erhalten;
- fehlgeschlagene Importdateien werden kurzzeitig und nicht unbegrenzt gehalten;
- technische Protokolle werden möglichst kurz gespeichert;
- Sicherheits- und Einwilligungsnachweise folgen dokumentierten Anforderungen;
- inaktive Konten erhalten einen transparenten Erinnerungs- und Löschprozess.

Konkrete Fristen werden vor produktivem Betrieb festgelegt und in der
Datenschutzinformation veröffentlicht.

## 15.8 Trennung einer Datenquelle

Beim Trennen einer Quelle entscheidet der Nutzer getrennt:

- zukünftige Synchronisation stoppen;
- lokal übernommene Aktivitäten behalten;
- lokal übernommene Rohdaten löschen;
- daraus erzeugte Analysen neu bewerten oder löschen.

Kairos muss die Auswirkungen vor der Bestätigung erklären.

## 15.9 Export und Portabilität

Ein Export soll mindestens enthalten:

- verständliche Index- und Metadaten;
- strukturierte Profildaten, Ziele und Pläne;
- Aktivitäten in einem geeigneten Format;
- Feedback und Timeline-Ereignisse;
- berechnete Kennzahlen mit Definition oder Versionsangabe;
- KI-Analysen und ihre Datenbezüge;
- Liste verbundener Quellen und Einwilligungen.

Proprietäre Formate sollen durch dokumentierte offene Formate ergänzt werden.

## 15.10 Löschkonzept

Der Löschvorgang muss:

1. Umfang und Folgen anzeigen;
2. Identität angemessen bestätigen;
3. aktive Synchronisation stoppen;
4. Primär- und abgeleitete Daten erfassen;
5. Löschung bei Auftragsverarbeitern anstoßen;
6. unvermeidbare Backup-Restlaufzeiten erklären;
7. Abschluss oder begründete Einschränkung bestätigen.

## 15.11 Test- und Entwicklungsdaten

Produktive personenbezogene Daten dürfen nicht ungeprüft für Entwicklung oder
Tests kopiert werden. Bevorzugt werden synthetische oder wirksam anonymisierte
Daten. Pseudonymisierung allein ist keine Anonymisierung und erfordert weiterhin
Schutzmaßnahmen.

## 15.12 Datenweitergabe

Weitergabe an einen menschlichen Coach oder andere Personen erfolgt nur nach
ausdrücklicher, widerrufbarer Freigabe. Der Nutzer bestimmt Datentyp, Zeitraum und
Zugriffsart. Öffentliche Freigabelinks sind standardmäßig nicht vorgesehen.

## 15.13 Abnahmekriterien

- Jede zentrale Information besitzt eine erkennbare Herkunft.
- Nutzer können falsche Daten korrigieren.
- Trennung einer Quelle und Löschung sind unterschiedliche, verständliche
  Aktionen.
- Export umfasst auch abgeleitete Daten und KI-Inhalte.
- Löschung berücksichtigt Athleten-Memory und Ableitungen.
- Testumgebungen verwenden keine unkontrollierten Produktivkopien.
- Aufbewahrungsfristen sind je Datenklasse dokumentiert.

## 15.14 Festgelegte Leitentscheidungen

1. Die Trainingshistorie bleibt standardmäßig erhalten, bis der Nutzer sie
   löscht oder eine abweichende Aufbewahrung wählt. Rechtliche und technische
   Ausnahmen müssen transparent dokumentiert werden.
2. Ein vollständiger, maschinenlesbarer Datenexport wird früh umgesetzt und ist
   kein ausschließlich späteres Komfortmerkmal.

## 15.15 Freigabestatus

Dieses Kapitel wurde inhaltlich mit dem Auftraggeber abgestimmt und fachlich
freigegeben.
