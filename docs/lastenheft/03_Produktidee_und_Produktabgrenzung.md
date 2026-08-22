# 3. Produktidee und Produktabgrenzung

## 3.1 Zweck dieses Kapitels

Dieses Kapitel beschreibt Kairos als Produkt: welche fachlichen Bestandteile
zusammenwirken, wie ein typischer Coaching-Kreislauf aussieht und wodurch sich
Kairos von bestehenden Lösungen abgrenzen soll. Zugleich wird festgelegt, welche
Aufgaben das Produkt ausdrücklich nicht übernimmt.

## 3.2 Produktidee

Kairos ist zunächst eine Coaching-Plattform mit Hauptfokus auf Radfahren. Sie
betrachtet Radtraining, unterstützendes Krafttraining, Rudern, Erholung, Ziele und
Alltagskontext in einem gemeinsamen Modell. Das Produkt soll aus vorhandenen und
vom Nutzer eingegebenen Daten einen fortlaufenden Coaching-Prozess bilden. Die
fachliche Grundlage soll spätere Sportarten ermöglichen, ohne sie im anfänglichen
Umfang zu priorisieren.

Die Produktidee beruht auf fünf zusammenhängenden Fähigkeiten:

1. **Daten zusammenführen:** Kairos übernimmt oder erfasst relevante
   Trainings-, Erholungs-, Kalender-, Wetter- und Zieldaten.
2. **Situation verstehen:** Das System bewertet einzelne Einheiten und erkennt
   Entwicklungen im zeitlichen Verlauf.
3. **Ziele steuern:** Kurzfristige Entscheidungen werden in Beziehung zu
   langfristigen Zielen gesetzt.
4. **Handlungen empfehlen:** Kairos formuliert konkrete, begründete nächste
   Schritte für Training und Erholung.
5. **Kontrolliert anpassen:** Pläne werden vorgeschlagen oder – nach ausdrücklicher
   Aktivierung – innerhalb festgelegter Grenzen automatisch verändert.

## 3.3 Kontinuierlicher Coaching-Kreislauf

Kairos soll nicht als Folge voneinander getrennter Analysefunktionen wirken. Der
fachliche Kern ist ein wiederkehrender Coaching-Kreislauf:

1. **Ziel festlegen:** Der Nutzer definiert ein Leistungs-, Gewohnheits- oder
   Wettkampfziel mit Zeitraum und Priorität.
2. **Ausgangslage erfassen:** Verfügbare Trainingshistorie, Leistungswerte,
   Rahmenbedingungen und Präferenzen werden berücksichtigt.
3. **Training planen:** Kairos schlägt einen realistischen nächsten
   Trainingsabschnitt oder eine konkrete Einheit vor.
4. **Kontext prüfen:** Erholung, Kalender, Wetter, Ernährungssituation und
   weitere relevante Faktoren werden einbezogen.
5. **Training durchführen:** Die Aktivität wird aufgezeichnet oder manuell
   dokumentiert.
6. **Training analysieren:** Kairos bewertet Ausführung, Belastung, Zielerfüllung
   und Abweichungen.
7. **Feedback ergänzen:** Der Nutzer meldet subjektive Belastung, Beschwerden,
   Motivation und weitere Beobachtungen zurück.
8. **Lernen und anpassen:** Erkenntnisse fließen in Athletenprofil, Empfehlung
   und weitere Planung ein.

Der Kreislauf wird iterativ verfeinert. Eine einzelne fehlerhafte oder fehlende
Datenquelle darf nicht unbemerkt den gesamten Prozess bestimmen.

## 3.4 Aktueller Sportartenfokus und Erweiterbarkeit

Der anfängliche Fokus liegt auf:

- Radfahren;
- Krafttraining, soweit es die Radleistung, Belastbarkeit und Verletzungsprävention
  unterstützt;
- Rudern als eigenständig erfasste und analysierte Ausdauersportart.

Rad-, Kraft- und Rudertraining stellen unterschiedliche Anforderungen an
Datenerfassung und Analyse. Gemeinsame Aspekte wie Belastung, Erholung,
Zielbeitrag und subjektives Feedback sollen zusammen betrachtet werden. Die
unterschiedlichen Belastungsformen dürfen nicht durch eine unangemessen einfache
Kennzahl gleichgesetzt werden.

### Radfahren

Beim Radfahren können unter anderem Leistung, Herzfrequenz, Geschwindigkeit,
Trittfrequenz, Höhenprofil, Strecke, Wetter und Intervallausführung berücksichtigt
werden. Indoor- und Outdoor-Einheiten sind fachlich zu unterscheiden.

### Krafttraining

Beim Krafttraining sind Übungen, Sätze, Wiederholungen, Last, Intensität,
Pausenzeiten, subjektive Anstrengung und Trainingsvolumen relevant. Die
Belastungswirkung lässt sich nicht allein aus Dauer und Herzfrequenz ableiten.

### Rudern

Beim Rudern können unter anderem Schlagfrequenz, Pace, Leistung, Herzfrequenz,
Distanz, Intervallstruktur und technische Konstanz relevant sein. Indoor- und
Outdoor-Rudern können unterschiedliche Datenquellen und Analyseanforderungen
besitzen.

Weitere Sportarten sind für den aktuellen Planungshorizont nicht relevant. Die
spätere Erweiterbarkeit bleibt ein Architektur- und Produktprinzip, ist aber kein
Grund, jetzt zusätzliche sportartspezifische Funktionen zu entwickeln.

## 3.5 Zentrale Produktbereiche

### 3.5.1 Ziele und Fortschritt

Der Nutzer verwaltet terminierte Ziele und Teilziele. Kairos bewertet Fortschritt,
Realismus, Risiken und nächste Maßnahmen. Prognosen müssen als unsicher erkennbar
bleiben und dürfen keine Erfolgsgarantie vermitteln.

### 3.5.2 Trainingsplanung

Kairos erstellt oder ergänzt Trainingspläne anhand von Ziel, Ausgangslage,
verfügbarer Zeit, Sportarten, Erholung und bisherigem Verlauf. Planung wird als
anpassbarer Prozess und nicht als unveränderlicher Kalender verstanden.

### 3.5.3 Trainingsanalyse

Absolvierte Einheiten werden sportartspezifisch analysiert. Kairos erklärt, was
gut funktioniert hat, wo relevante Abweichungen bestehen und wie die nächste
vergleichbare Einheit verbessert werden kann.

### 3.5.4 Erholung

Kairos betrachtet objektive und subjektive Erholungssignale. Das Produkt soll
keinen einzelnen Herstellerwert unkritisch übernehmen, sondern Datenherkunft,
individuellen Referenzbereich und Unsicherheit beachten.

### 3.5.5 Ernährung als spätere Ergänzung

Kairos kann später trainingsbezogene Ernährung mit Belastung, Zeitpunkt, Dauer,
Intensität, Erholung und persönlichen Rahmenbedingungen verbinden. Mögliche
Ausgaben können sein:

- Hinweise zur Vorbereitung auf eine bevorstehende Einheit;
- Empfehlungen zur Energie- und Flüssigkeitszufuhr während längerer Belastungen;
- Hinweise zur Regeneration nach dem Training;
- erkennbare Abweichungen zwischen geplantem Training und verfügbarer
  Energiezufuhr;
- alltagstaugliche Vorschläge für Mahlzeiten oder Lebensmittelkategorien;
- Begründungen, weshalb eine Empfehlung zur aktuellen Einheit passt.

Ernährung ist kein Hauptfokus des anfänglichen Produkts. Kairos soll keine
Erkrankungen diagnostizieren, keine medizinische
Ernährungstherapie anbieten und bei bekannten Allergien oder Unverträglichkeiten
keine unpassenden Empfehlungen geben. Detailtiefe und Datenerhebung müssen später
mit Datenschutz und Bedienbarkeit abgestimmt werden.

### 3.5.6 Kalender und Alltag

Kalenderdaten helfen, Training realistisch einzuplanen und Konflikte früh zu
erkennen. Private Termininhalte sollen nur im erforderlichen Umfang verarbeitet
werden. Verfügbare Zeit ist ein Planungsfaktor, kein Qualitätsurteil über den
Nutzer.

### 3.5.7 Wetter und Umgebung

Wetterinformationen sollen Entscheidungen zu Zeitpunkt, Ort, Dauer,
Belastungssteuerung, Flüssigkeitsbedarf und möglicher Indoor-Alternative
unterstützen. Prognosen müssen hinsichtlich Ort, Zeitraum und Aktualität
gekennzeichnet werden.

### 3.5.8 Timeline und Athletenprofil

Eine gemeinsame Timeline verbindet Training, Planung, Ziele, Feedback, Erholung
und relevante Ereignisse. Das Athletenprofil speichert nur Informationen mit
erkennbarem Coaching-Nutzen und bleibt für den Nutzer kontrollierbar.

## 3.6 Interaktionsformen

Kairos soll mehrere Interaktionsformen kombinieren:

- **Dashboard:** aktuelle Situation, wichtigste Ziele, heutige Empfehlung und
  offene Entscheidungen;
- **Detailanalyse:** nachvollziehbare Untersuchung einzelner Aktivitäten und
  Entwicklungen;
- **Planungsansicht:** Kalender, geplante Einheiten und erkannte Konflikte;
- **Coach-Dialog:** natürliche Fragen und Antworten auf Basis der freigegebenen
  Nutzerdaten;
- **Benachrichtigungen:** zeitkritische oder vom Nutzer gewünschte Hinweise;
- **Bestätigungs- und Automatikregeln:** Steuerung, welche Änderungen Kairos
  vorschlagen oder eigenständig durchführen darf.

Der Coach-Dialog ist ein Zugang zum Produkt, aber nicht das gesamte Produkt. Die
zugrunde liegenden Daten, Entscheidungen und Änderungen müssen auch strukturiert
sichtbar sein.

## 3.7 Abgrenzung zu bestehenden Produktarten

### Gegenüber Aktivitätstrackern

Aktivitätstracker erfassen und visualisieren primär Daten. Kairos soll diese Daten
in Zielbezug, Erklärung und nächste Handlung übersetzen.

### Gegenüber sozialen Sportplattformen

Soziale Plattformen fokussieren Austausch, Aktivitätenfeeds und Vergleiche.
Kairos fokussiert persönliche Entwicklung, Coaching und informierte
Entscheidungen. Soziale Funktionen sind kein fachlicher Kern.

### Gegenüber statischen Trainingsplänen

Statische Pläne geben eine vordefinierte Abfolge vor. Kairos soll den Plan anhand
des tatsächlichen Verlaufs und veränderter Rahmenbedingungen weiterentwickeln.

### Gegenüber reinen KI-Chatbots

Ein allgemeiner Chatbot besitzt ohne strukturierte Integration keine dauerhaft
verlässliche Trainingshistorie, keine überprüfbare Fachlogik und keine
kontrollierte Planung. Kairos verbindet den Dialog mit strukturierten Daten,
nachvollziehbaren Berechnungen, Regeln und dokumentierten Aktionen.

### Gegenüber persönlichem Coaching

Kairos soll persönliches menschliches Coaching nicht vollständig ersetzen. Ein
menschlicher Coach kann Technik, Verhalten, Gesundheitszustand und persönliche
Lebensumstände differenzierter beurteilen. Kairos soll kontinuierliche
datenbasierte Unterstützung bieten und kann künftig auch als Werkzeug für die
Zusammenarbeit mit einem Coach dienen.

## 3.8 Fachliche Produktgrenzen

Kairos ist keine Plattform für:

- medizinische Diagnose, Behandlung oder Notfallüberwachung;
- medizinische Ernährungstherapie;
- garantierte Leistungs- oder Wettkampfergebnisse;
- unangekündigte autonome Änderungen ohne Zustimmung;
- manipulative Motivation oder Bestrafung bei nicht absolviertem Training;
- öffentliche Ranglisten als primären Produktzweck;
- ungeprüfte Gleichsetzung unterschiedlicher Sportarten und Belastungsformen;
- vollständigen Ersatz eines Trainers, Arztes oder qualifizierten
  Ernährungsexperten.

## 3.9 Entscheidungs- und Verantwortungsgrenzen

Kairos darf fachliche Empfehlungen und – bei aktivierter Berechtigung –
Planänderungen erzeugen. Die letzte Verantwortung für Durchführung und
gesundheitliche Entscheidungen liegt beim Nutzer.

Für automatische Änderungen gelten folgende Produktprinzipien:

- ausdrückliche Aktivierung durch den Nutzer;
- wählbarer Umfang der Automatisierung;
- dokumentierter Auslöser und Begründung;
- sichtbare Kennzeichnung jeder Änderung;
- Benachrichtigung entsprechend den Nutzereinstellungen;
- Möglichkeit zur Korrektur oder Rücknahme;
- sichere Rückstufung in den Vorschlagsmodus.

## 3.10 Agiles Produktmodell

Kairos wird nicht anhand einer heute vollständig festgelegten Folge von Version
1, Version 2 und Version 3 geplant. Die Entwicklung erfolgt in kleinen,
nutzbaren Inkrementen. Das Product Backlog wird anhand folgender Kriterien laufend
priorisiert:

- erwarteter Nutzen für den Nutzer;
- Beitrag zur Produktvision;
- fachliches und gesundheitliches Risiko;
- Daten- und Systemabhängigkeiten;
- Aufwand und technische Unsicherheit;
- Erkenntnisse aus Tests und Nutzung;
- Qualität der verfügbaren Daten;
- Datenschutz- und Sicherheitsauswirkungen.

Jedes Inkrement soll einen prüfbaren End-to-End-Nutzen liefern. Unvollständige
Funktionen dürfen nicht allein wegen eines vorab festgelegten Versionsplans
veröffentlicht werden.

## 3.11 Produktprinzipien

Für alle weiteren Anforderungen gelten folgende Prinzipien:

1. **Erklären vor behaupten:** Aussagen werden mit Daten und Unsicherheit
   verbunden.
2. **Handeln vor überladen:** Die wichtigste nächste Handlung ist klarer als eine
   unpriorisierte Menge an Kennzahlen.
3. **Nutzerkontrolle vor maximaler Automatisierung:** Automatisierung ist wählbar
   und reversibel.
4. **Individuelle Entwicklung vor allgemeinen Ranglisten:** Der persönliche
   Verlauf ist wichtiger als sozialer Vergleich.
5. **Fachliche Qualität vor Funktionsmenge:** Wenige belastbare Analysen sind
   wertvoller als viele unzuverlässige Aussagen.
6. **Sportartspezifische Tiefe in einem gemeinsamen Modell:** Gemeinsame Konzepte
   werden geteilt, fachliche Unterschiede nicht verwischt.
7. **Datensparsamkeit vor vorsorglicher Sammlung:** Daten werden nur mit klarem
   Zweck erhoben.
8. **Iterativer Nutzen vor starren Versionen:** Planung reagiert auf Erkenntnisse,
   ohne Vision und Sicherheitsgrenzen beliebig zu verändern.

## 3.12 Festgelegte Leitentscheidungen

1. Der aktuelle Hauptfokus liegt auf Radfahren und ergänzendem Krafttraining,
   das die Radleistung unterstützt.
2. Rudern bleibt als weitere aktiv unterstützte Sportart Bestandteil des
   anfänglichen Umfangs.
3. Weitere Sportarten sind im aktuellen Planungshorizont nicht relevant.
4. Ernährung bleibt eine mögliche spätere Ergänzung und erhält zunächst keinen
   eigenen Produktschwerpunkt.
5. Kairos wird agil und iterativ ohne starre Versionsfolge entwickelt.

## 3.13 Freigabestatus

Dieses Kapitel wurde inhaltlich mit dem Auftraggeber abgestimmt und fachlich
freigegeben.
