# 7. Use Cases und User Journeys

## 7.1 Zweck dieses Kapitels

Dieses Kapitel beschreibt die wichtigsten Abläufe aus Sicht des Athleten. Die
Use Cases konkretisieren die funktionalen Anforderungen, ohne eine technische
Umsetzung vorzugeben. Radfahren bildet den Hauptfokus; Krafttraining unterstützt
die Radleistung, Rudern bleibt als weitere aktive Sportart enthalten.

## 7.2 Rollen

- **Athlet:** verwaltet Daten, Ziele, Training und Entscheidungen.
- **Kairos:** analysiert, erklärt, empfiehlt und plant innerhalb erteilter
  Berechtigungen.
- **Externe Datenquelle:** liefert Aktivitäts-, Erholungs-, Kalender- oder
  Wetterdaten.
- **Menschlicher Coach:** mögliche spätere, ausdrücklich autorisierte Rolle.

## 7.3 UC-01 – Ersteinrichtung durchführen

**Ziel:** Der Athlet schafft eine ausreichende Grundlage für personalisierte
Analysen.

**Vorbedingungen:** Ein Nutzerkonto besteht.

**Standardablauf:**

1. Der Athlet legt Einheiten, Zeitzone und Sprache fest.
2. Er wählt Radfahren als Hauptfokus und aktiviert optional Krafttraining und
   Rudern.
3. Er gibt Erfahrung, verfügbare Trainingstage und bekannte Leistungsbereiche
   an.
4. Er verbindet mindestens eine Datenquelle oder wählt manuellen Import.
5. Er bestimmt Datenschutz-, Benachrichtigungs- und Automatisierungseinstellungen.
6. Kairos zeigt, welche Funktionen bereits nutzbar sind und welche Daten fehlen.

**Alternativen und Fehlerfälle:** Eine Einrichtung ohne externe Datenquelle muss
möglich bleiben. Unvollständige Angaben führen zu gekennzeichneten
Einschränkungen, nicht zu erfundenen Standardwerten.

**Ergebnis:** Ein transparentes Athletenprofil mit dokumentierten Freigaben liegt
vor.

## 7.4 UC-02 – Langfristiges Ziel anlegen

**Ziel:** Der Athlet erfasst ein messbares Ziel als Grundlage der Planung.

**Standardablauf:**

1. Der Athlet wählt Zieltyp und Sportart.
2. Er erfasst Zielwert, Zieldatum und Priorität.
3. Kairos prüft Vollständigkeit und erkennt mögliche Zielkonflikte.
4. Kairos zeigt den bekannten Ausgangswert und bestehende Datenlücken.
5. Der Athlet bestätigt oder korrigiert das Ziel.
6. Kairos legt erste Teilziele oder notwendige Standortbestimmungen vor.

**Ergebnis:** Das Ziel ist aktiv und jede Prognose bleibt von gemessenen Fakten
unterscheidbar.

## 7.5 UC-03 – Radaktivität importieren und analysieren

**Ziel:** Der Athlet erhält zeitnah verständliches und konkretes Feedback.

**Vorbedingungen:** Eine Aktivität ist verfügbar und dem Athleten zugeordnet.

**Standardablauf:**

1. Kairos übernimmt die Aktivität und prüft Vollständigkeit sowie Plausibilität.
2. Das System erkennt Intervalle, Pausen und weitere relevante Abschnitte.
3. Fachlich freigegebene Kennzahlen werden berechnet.
4. Die Durchführung wird gegen den Trainingszweck und geeignete Vergleichsdaten
   bewertet.
5. Der Athlet ergänzt subjektive Anstrengung, Ermüdung und Besonderheiten.
6. Kairos erstellt eine Erklärung mit positiven Aspekten, Abweichungen,
   Unsicherheit und nächster Handlung.
7. Der Athlet bewertet oder korrigiert die Analyse.

**Fehlerfälle:** Bei fehlender Leistung, fehlerhafter Herzfrequenz oder
unbekanntem Trainingsziel wird nur der belastbare Teil analysiert. Eine doppelte
Aktivität darf die Belastungsbilanz nicht verändern.

**Ergebnis:** Aktivität, Analyse, Datenbasis und Feedback sind dauerhaft
nachvollziehbar verbunden.

## 7.6 UC-04 – Unterstützendes Krafttraining dokumentieren

**Ziel:** Krafttraining wird als Bestandteil der Radentwicklung und
Gesamtbelastung berücksichtigt.

**Standardablauf:**

1. Der Athlet erfasst oder importiert Übungen, Sätze, Wiederholungen, Last und
   subjektive Anstrengung.
2. Er ordnet der Einheit einen Zweck zu.
3. Kairos bewertet Volumen und zeitliche Nähe zu Radschlüsseleinheiten.
4. Das System weist auf wahrscheinliche Belastungskonflikte hin.
5. Die Einheit fließt in Erholungsbewertung und weitere Planung ein.

**Grenze:** Ohne geeignete Bild- oder Sensordaten erfolgt keine scheinbar sichere
Technikbewertung.

## 7.7 UC-05 – Rudereinheit analysieren

**Ziel:** Eine Rudereinheit wird eigenständig analysiert und in der
Gesamtbelastung berücksichtigt.

**Standardablauf:**

1. Kairos übernimmt oder erfasst die Einheit.
2. Indoor- und Outdoor-Rudern werden unterschieden.
3. Verfügbare Werte wie Pace, Leistung, Herzfrequenz, Schlagfrequenz und Distanz
   werden geprüft.
4. Intervalle und Konstanz werden analysiert.
5. Der Athlet ergänzt subjektives Feedback.
6. Kairos berücksichtigt die Belastung in der weiteren Planung.

## 7.8 UC-06 – Tagesempfehlung erhalten

**Ziel:** Der Athlet erkennt, welches Training aktuell sinnvoll ist.

**Standardablauf:**

1. Kairos betrachtet Plan, kürzliche Belastung, Erholung, Feedback, Kalender und
   Wetter.
2. Das System prüft Aktualität und Widersprüche der Daten.
3. Es bestätigt die geplante Einheit oder schlägt eine Änderung vor.
4. Kairos nennt Auslöser, Zielbezug, Unsicherheit und Alternativen.
5. Je nach Automatisierungsmodus bestätigt der Athlet oder Kairos setzt eine
   erlaubte Änderung um.

**Ergebnis:** Empfehlung und Entscheidung werden protokolliert.

## 7.9 UC-07 – Trainingsplan automatisch anpassen

**Ziel:** Ein aktivierter Automatikmodus hält den Plan unter realen Bedingungen
umsetzbar.

**Vorbedingungen:** Der Nutzer hat Art und Grenzen automatischer Änderungen
ausdrücklich freigegeben.

**Standardablauf:**

1. Kairos erkennt einen relevanten Auslöser, etwa ausgefallenes Training,
   Kalenderkonflikt oder mangelhafte Erholung.
2. Es prüft, ob eine Änderung innerhalb der Freigabe liegt.
3. Es verändert nur betroffene Einheiten und wahrt Ziel, Belastungsverteilung und
   Erholung.
4. Der Nutzer erhält eine verständliche Änderungsübersicht.
5. Er kann die Änderung korrigieren, zurücknehmen oder Automatisierung aussetzen.

**Abbruch:** Außerhalb der Freigabe erstellt Kairos lediglich einen Vorschlag.

## 7.10 UC-08 – Vergleichsfrage im Coach-Dialog stellen

**Ziel:** Der Athlet erhält eine datengestützte Antwort auf eine natürliche Frage.

**Beispiel:** „Warum war mein Puls bei den heutigen Intervallen niedriger als
letzte Woche?“

**Standardablauf:**

1. Kairos ermittelt relevante Aktivitäten und Vergleichsbedingungen.
2. Es prüft Unterschiede bei Leistung, Dauer, Temperatur, Erholung und
   Datenqualität.
3. Es trennt Beobachtung von möglichen Ursachen.
4. Die Antwort nennt verwendete Daten, plausible Erklärungen, Unsicherheit und
   eine sinnvolle nächste Prüfung oder Handlung.

**Grenze:** Korrelationen dürfen nicht als gesicherte medizinische Ursachen
dargestellt werden.

## 7.11 UC-09 – Kalender- oder Wetterkonflikt behandeln

**Ziel:** Eine geplante Outdoor-Einheit wird realistisch angepasst.

1. Kairos erkennt eine zeitliche oder wetterbedingte Einschränkung.
2. Es bewertet Relevanz für den konkreten Trainingszweck.
3. Es bietet eine zeitliche Verschiebung, verkürzte Variante, Indoor-Alternative
   oder unveränderte Durchführung mit Hinweisen an.
4. Der gewählte Weg wird im Plan dokumentiert.

## 7.12 UC-10 – Daten und Annahmen korrigieren

**Ziel:** Der Athlet behält Kontrolle über die Wissensgrundlage.

1. Der Athlet öffnet einen Messwert, eine Profilannahme oder ein gespeichertes
   Ereignis.
2. Kairos zeigt Quelle und bisherige Verwendung.
3. Der Athlet korrigiert, löscht oder bestätigt die Information.
4. Kairos erläutert, welche Analysen oder Pläne neu bewertet werden.
5. Historische Entscheidungen bleiben mit ihrem damaligen Datenstand
   nachvollziehbar.

## 7.13 Übergreifende User Journey

Die zentrale User Journey verläuft nicht linear, sondern als Lernschleife:

**Ziel setzen → Training planen → Kontext prüfen → Training absolvieren → Daten
prüfen → Feedback geben → Analyse verstehen → Planung anpassen → Fortschritt
bewerten.**

Jeder Durchlauf soll die nächste Entscheidung verbessern. Dabei darf wachsende
Datenmenge nicht automatisch zu höherer behaupteter Sicherheit führen.

## 7.14 Abnahmekriterien für zentrale Journeys

- Der Athlet erkennt jederzeit den aktuellen Schritt und die nächste notwendige
  Entscheidung.
- Keine automatische Planänderung erfolgt außerhalb einer aktiven Freigabe.
- Jede Analyse führt zu Datenbasis, Zeitpunkt und Aktivität zurück.
- Fehlende Daten werden sichtbar und verhindern unbegründete Aussagen.
- Rad-, Kraft- und Ruderbelastung werden nicht doppelt gezählt.
- Nutzerkorrekturen wirken auf zukünftige Analysen und bleiben nachvollziehbar.

## 7.15 Festgelegte Leitentscheidungen

1. Die zehn beschriebenen Use Cases decken den erwarteten Hauptablauf ab.
2. Eine kurze Erholungsabfrage ist optional und wird nicht verpflichtend
   vorausgesetzt.
3. Kairos soll insbesondere nach wichtigen Einheiten aktiv Feedback anfordern.
   Der Nutzer kann konfigurieren, für welche Einheiten und wie häufig diese
   Abfrage erfolgt.
4. Welche Arten von Planänderungen zuerst automatisch erlaubt werden, wird noch
   nicht festgelegt. Bis zu einer späteren Entscheidung gelten der manuelle und
   der Vorschlagsmodus als sichere Ausgangsbasis.

## 7.16 Freigabestatus

Dieses Kapitel wurde inhaltlich mit dem Auftraggeber abgestimmt und fachlich
freigegeben.
