# 6. Funktionale Anforderungen

## 6.1 Zweck und Verbindlichkeit

Dieses Kapitel beschreibt die fachlichen Funktionen, die Kairos bereitstellen
soll. Jede Anforderung besitzt eine stabile Kennung und eine vorläufige
Priorität. Die Prioritäten werden im agilen Product Backlog laufend überprüft.

- **Must:** für einen nutzbaren End-to-End-Coaching-Ablauf erforderlich;
- **Should:** hoher Nutzen, nach einem belastbaren Kern umzusetzen;
- **Could:** sinnvolle spätere Ergänzung;
- **Won't for now:** im aktuellen Planungshorizont bewusst ausgeschlossen.

Die Anforderungen legen das gewünschte Verhalten fest, nicht die technische
Umsetzung.

## 6.2 Konto, Profil und Einstellungen

### FR-PRO-001 – Athletenprofil verwalten — Must

Der Nutzer muss ein Athletenprofil anlegen und bearbeiten können. Es umfasst
mindestens Zeitzone, bevorzugte Einheiten, sportliche Erfahrung, verfügbare
Sportarten und relevante Trainingspräferenzen.

**Akzeptanzkriterien:**

- Änderungen werden nachvollziehbar gespeichert.
- Pflicht- und optionale Angaben sind klar gekennzeichnet.
- Fehlende optionale Daten werden nicht als bekannte Fakten behandelt.

### FR-PRO-002 – Sportarten und Fokus festlegen — Must

Der Nutzer muss Radfahren, Krafttraining und Rudern aktivieren und Radfahren als
primären Fokus festlegen können.

### FR-PRO-003 – Leistungsbereiche verwalten — Should

Der Nutzer soll sportartspezifische Bereiche wie Herzfrequenz- oder
Leistungszonen hinterlegen, importieren und korrigieren können. Herkunft und
Gültigkeitszeitraum müssen erkennbar sein.

### FR-PRO-004 – Einschränkungen dokumentieren — Should

Der Nutzer soll relevante Beschwerden, Verletzungen, Unverträglichkeiten oder
andere Einschränkungen dokumentieren können. Kairos muss sie bei Empfehlungen
berücksichtigen, ohne daraus eine Diagnose abzuleiten.

### FR-PRO-005 – Automatisierungsmodus steuern — Must

Der Nutzer muss zwischen manuellem, Vorschlags- und begrenzt automatischem Modus
wählen können. Automatische Änderungen dürfen erst nach ausdrücklicher
Aktivierung erfolgen.

## 6.3 Ziele

### FR-ZIE-001 – Ziel anlegen — Must

Der Nutzer muss ein Ziel mit Bezeichnung, Zieltyp, Sportart, Zielwert,
Zieldatum und Priorität anlegen können.

### FR-ZIE-002 – Teilziele verwalten — Should

Ein Ziel soll in messbare Teilziele und Meilensteine untergliedert werden können.

### FR-ZIE-003 – Zielstatus bewerten — Must

Kairos muss den Status eines aktiven Ziels anhand verfügbarer Daten darstellen.
Messwert, Berechnung und Prognose sind getrennt zu kennzeichnen.

### FR-ZIE-004 – Zielkonflikte erkennen — Should

Kairos soll Konflikte zwischen mehreren Zielen, verfügbarer Trainingszeit und
Erholung erkennen und erläutern.

### FR-ZIE-005 – Ziel ändern oder pausieren — Must

Der Nutzer muss Ziele ändern, pausieren, abschließen und löschen können. Die
Auswirkung auf bestehende Pläne muss vor einer Änderung sichtbar sein.

## 6.4 Datenquellen und Import

### FR-IMP-001 – Aktivitätsquelle verbinden — Must

Der Nutzer muss mindestens eine unterstützte Aktivitätsquelle verbinden oder
einen unterstützten Dateiimport verwenden können.

### FR-IMP-002 – Aktivitäten synchronisieren — Must

Kairos muss neue und aktualisierte Aktivitäten übernehmen und ihrem Nutzer
zuordnen können.

### FR-IMP-003 – FIT-Datei importieren — Should

Der Nutzer soll FIT-Dateien manuell importieren können, sofern deren Inhalt
unterstützt wird.

### FR-IMP-004 – Duplikate erkennen — Must

Kairos muss wahrscheinliche Duplikate erkennen und eine doppelte Berücksichtigung
der Belastung verhindern.

### FR-IMP-005 – Datenherkunft anzeigen — Must

Für importierte Daten müssen Quelle, Zeitpunkt und Status nachvollziehbar sein.

### FR-IMP-006 – Importfehler behandeln — Must

Fehlgeschlagene oder unvollständige Importe müssen verständlich angezeigt und
erneut angestoßen werden können.

### FR-IMP-007 – Daten korrigieren — Must

Der Nutzer muss fehlerhafte Zuordnungen und ausgewählte Werte korrigieren können,
ohne die ursprüngliche Herkunft zu verschleiern.

## 6.5 Radtraining

### FR-RAD-001 – Radaktivität darstellen — Must

Kairos muss mindestens Zeitpunkt, Dauer, Distanz und verfügbare Herzfrequenz-,
Leistungs-, Geschwindigkeits-, Trittfrequenz- und Höhendaten darstellen können.

### FR-RAD-002 – Abschnitte und Intervalle erkennen — Must

Kairos muss relevante Belastungs- und Erholungsabschnitte erkennen oder vom
Nutzer korrigieren lassen.

### FR-RAD-003 – Intervallqualität bewerten — Must

Kairos muss Intervalle hinsichtlich Zielbereich, Konstanz, Verlauf und
vollständiger Durchführung bewerten können. Fehlende Zielvorgaben müssen als
Einschränkung gekennzeichnet werden.

### FR-RAD-004 – Vergleichbare Einheiten gegenüberstellen — Should

Der Nutzer soll ähnliche Radaktivitäten anhand geeigneter Kriterien vergleichen
können.

### FR-RAD-005 – Radbezogene Kennzahlen berechnen — Must

Kairos muss eine zunächst begrenzte und fachlich validierte Menge radbezogener
Kennzahlen berechnen. Definition, Einheit und Datengrundlage müssen zugänglich
sein.

### FR-RAD-006 – Strukturiertes Radtraining planen — Should

Kairos soll Radtrainings mit Aufwärmen, Belastungsabschnitten, Pausen und
Abkühlen beschreiben können.

### FR-RAD-007 – Training an Gerät übertragen — Could

Kairos kann strukturierte Trainings an einen unterstützten externen Dienst oder
ein Gerät übertragen, sofern eine zulässige Schnittstelle verfügbar ist.

## 6.6 Krafttraining für Radfahrer

### FR-KRA-001 – Krafttraining erfassen — Must

Der Nutzer muss Übungen, Sätze, Wiederholungen, Last und subjektive Anstrengung
erfassen oder importieren können.

### FR-KRA-002 – Trainingszweck zuordnen — Must

Eine Kraftübung oder Einheit muss einem Zweck wie Maximalkraft, Stabilität,
Beweglichkeit, Belastbarkeit oder Verletzungsprävention zugeordnet werden können.

### FR-KRA-003 – Volumen und Entwicklung darstellen — Should

Kairos soll Trainingsvolumen und Leistungsentwicklung je Übung oder
Bewegungsmuster darstellen können.

### FR-KRA-004 – Konflikt mit Radtraining erkennen — Must

Kairos muss geplante hohe Beinbelastung im Krafttraining bei der Planung
intensiver Radtrainings berücksichtigen.

### FR-KRA-005 – Unterstützenden Kraftplan vorschlagen — Should

Kairos soll passend zu Radziel, Trainingsphase, Erfahrung und verfügbarer Zeit
einen ergänzenden Kraftplan vorschlagen können.

### FR-KRA-006 – Technikgrenzen kommunizieren — Must

Kairos darf aus unzureichenden Daten keine sichere Bewertung der
Bewegungsausführung ableiten. Bei Technikfragen muss es seine Beobachtungsgrenze
klar nennen.

## 6.7 Rudern

### FR-RUD-001 – Ruderaktivität erfassen — Must

Kairos muss eine Ruderaktivität importieren oder manuell erfassen können.

### FR-RUD-002 – Ruderkennzahlen darstellen — Must

Je nach Datenquelle müssen mindestens Dauer und Distanz sowie verfügbare Pace-,
Leistungs-, Herzfrequenz- und Schlagfrequenzdaten dargestellt werden können.

### FR-RUD-003 – Ruderintervalle analysieren — Should

Kairos soll Intervallstruktur, Konstanz und Belastungsverlauf einer Rudereinheit
analysieren können.

### FR-RUD-004 – Ruderbelastung einbeziehen — Must

Die Belastung einer Rudereinheit muss bei Erholung und weiterer Trainingsplanung
berücksichtigt werden.

### FR-RUD-005 – Indoor und Outdoor unterscheiden — Should

Kairos soll Indoor-Ergometer und Outdoor-Rudern unterscheiden und nur passende
Kennzahlen vergleichen.

## 6.8 Subjektives Trainingsfeedback

### FR-FBK-001 – Feedback nach Aktivität erfassen — Must

Kairos muss nach einer Aktivität kurzes Feedback zu wahrgenommener Anstrengung,
Erholung, Beschwerden und optionalen Anmerkungen erfassen können.

### FR-FBK-002 – Feedback später ergänzen — Must

Der Nutzer muss Feedback nachträglich ergänzen oder korrigieren können.

### FR-FBK-003 – Feedback in Analyse einbeziehen — Must

Kairos muss subjektives Feedback bei späteren Analysen und Empfehlungen
berücksichtigen und dessen Einfluss erläutern können.

### FR-FBK-004 – Auffällige Beschwerden behandeln — Must

Bei gemeldeten Schmerzen oder gesundheitlichen Warnzeichen darf Kairos keine
unverändert aggressive Trainingssteigerung empfehlen. Es muss auf die Grenze zur
medizinischen Abklärung hinweisen.

## 6.9 Trainingsanalyse und Coaching

### FR-COA-001 – Aktivitätsanalyse erzeugen — Must

Kairos muss für eine ausreichend vollständige Aktivität eine strukturierte
Analyse erzeugen können.

### FR-COA-002 – Positives und Abweichungen erklären — Must

Die Analyse muss gelungene Aspekte, relevante Abweichungen und deren Bedeutung
verständlich darstellen.

### FR-COA-003 – Nächste Handlung empfehlen — Must

Eine Analyse soll mindestens eine konkrete nächste Handlung oder Beobachtung
enthalten, sofern die Datenbasis dies zulässt.

### FR-COA-004 – Datenbasis offenlegen — Must

Jede wesentliche Empfehlung muss auf die verwendeten Daten und deren Zeitraum
zurückführbar sein.

### FR-COA-005 – Unsicherheit anzeigen — Must

Kairos muss fehlende Daten, geringe Vergleichbarkeit und unsichere Schlussfolgerung
sichtbar kennzeichnen.

### FR-COA-006 – Analyse bewerten lassen — Must

Der Nutzer muss angeben können, ob eine Analyse hilfreich oder unzutreffend war,
und optional einen Grund nennen können.

### FR-COA-007 – Coach-Dialog anbieten — Should

Der Nutzer soll natürliche Fragen zu Zielen, Aktivitäten, Erholung und Planung
stellen können.

### FR-COA-008 – Quellenkontext im Dialog bewahren — Must

Der Coach-Dialog darf nicht behaupten, auf Daten zuzugreifen, die nicht vorhanden
oder nicht freigegeben sind.

## 6.10 Trainingsplanung

### FR-PLA-001 – Trainingseinheit planen — Must

Kairos muss eine geplante Einheit mit Sportart, Zweck, Dauer, Struktur,
Intensität und Zielbezug verwalten können.

### FR-PLA-002 – Wochenansicht bereitstellen — Must

Der Nutzer muss geplante und absolvierte Einheiten in einer zeitlichen Übersicht
sehen können.

### FR-PLA-003 – Verfügbarkeit berücksichtigen — Should

Kairos soll verfügbare Trainingszeit und bekannte Kalenderkonflikte bei
Vorschlägen berücksichtigen.

### FR-PLA-004 – Gesamtbelastung berücksichtigen — Must

Die Planung muss Rad-, Kraft- und Rudertraining sowie verfügbare Erholungsdaten
gemeinsam betrachten.

### FR-PLA-005 – Ausgefallene Einheit behandeln — Must

Bei einer nicht absolvierten Einheit muss Kairos deren Bedeutung bewerten und
eine begründete nächste Handlung vorschlagen, statt sie automatisch nachzuholen.

### FR-PLA-006 – Planänderung begründen — Must

Jede vorgeschlagene oder automatische Planänderung muss Auslöser, betroffene
Einheiten und erwartete Auswirkung darstellen.

### FR-PLA-007 – Automatische Änderung begrenzen — Must

Kairos darf im Automatikmodus nur Änderungstypen ausführen, die der Nutzer zuvor
freigegeben hat.

### FR-PLA-008 – Änderung rückgängig machen — Must

Der Nutzer muss automatische Planänderungen soweit fachlich möglich zurücknehmen
können.

### FR-PLA-009 – Änderungshistorie anzeigen — Must

Alle Planänderungen müssen mit Zeitpunkt, Auslöser und Urheber nachvollziehbar
sein.

## 6.11 Erholung und Belastungssteuerung

### FR-ERH-001 – Erholungsdaten übernehmen — Should

Kairos soll verfügbare Schlaf-, HRV-, Ruhepuls- und weitere Erholungsdaten
übernehmen können.

### FR-ERH-002 – Subjektiven Tageszustand erfassen — Must

Der Nutzer muss Energie, Ermüdung, Muskelzustand und Beschwerden in kurzer Form
angeben können.

### FR-ERH-003 – Individuellen Referenzbereich verwenden — Should

Erholungswerte sollen, soweit fachlich sinnvoll, gegen den individuellen Verlauf
statt ausschließlich gegen allgemeine Grenzwerte bewertet werden.

### FR-ERH-004 – Veraltete Daten kennzeichnen — Must

Kairos muss Aktualität und Lücken der Erholungsdaten sichtbar machen.

### FR-ERH-005 – Belastungswarnung begründen — Must

Eine Warnung muss die auslösenden Daten und die empfohlene Reaktion nennen, ohne
eine medizinische Diagnose zu stellen.

## 6.12 Kalender und Wetter

### FR-KAL-001 – Kalender verbinden — Should

Der Nutzer soll einen unterstützten Kalender mit klar definiertem Zugriffsumfang
verbinden können.

### FR-KAL-002 – Zeitkonflikte erkennen — Should

Kairos soll Konflikte zwischen geplantem Training und verfügbarer Zeit erkennen.

### FR-KAL-003 – Termindaten minimieren — Must

Kairos darf nur die für die Trainingsplanung erforderlichen Kalenderdaten
verwenden und muss den Zugriff transparent darstellen.

### FR-WET-001 – Wetterkontext abrufen — Should

Kairos soll Wetterinformationen für Ort und Zeitraum einer geplanten
Outdoor-Einheit abrufen können.

### FR-WET-002 – Wetterrisiko bewerten — Should

Kairos soll relevante Bedingungen wie Hitze, Kälte, Wind, Starkregen und Gewitter
für das Training einordnen.

### FR-WET-003 – Alternative vorschlagen — Should

Bei ungeeigneten Bedingungen soll Kairos eine begründete zeitliche, inhaltliche
oder Indoor-Alternative vorschlagen.

## 6.13 Timeline und Nachvollziehbarkeit

### FR-TIM-001 – Gemeinsame Timeline anzeigen — Should

Kairos soll Aktivitäten, Pläne, Ziele, Feedback, Analysen und relevante
Ereignisse chronologisch darstellen.

### FR-TIM-002 – Ereignis manuell erfassen — Should

Der Nutzer soll Ereignisse wie Krankheit, Urlaub, Wettkampf oder
Ausrüstungsänderung ergänzen können.

### FR-TIM-003 – Historischen Analysestand nachvollziehen — Should

Es soll erkennbar sein, auf welcher damaligen Datenbasis eine Analyse oder
Entscheidung beruhte.

## 6.14 Ernährung als spätere Nebenfunktion

### FR-ERN-001 – Trainingsbezogenen Hinweis geben — Could

Kairos kann später allgemeine, trainingsbezogene Hinweise zu Energie,
Flüssigkeit, Timing und Regeneration geben.

### FR-ERN-002 – Einschränkungen berücksichtigen — Must, sobald Ernährung aktiv ist

Sobald Ernährungsfunktionen angeboten werden, müssen bekannte Allergien,
Unverträglichkeiten und ausdrücklich ausgeschlossene Lebensmittel berücksichtigt
werden.

### FR-ERN-003 – Medizinische Grenze anzeigen — Must, sobald Ernährung aktiv ist

Kairos muss Ernährungshinweise klar von medizinischer Ernährungstherapie
abgrenzen.

Eine detaillierte Ernährungserfassung, Mahlzeitenplanung oder externe
Ernährungs-App ist im aktuellen Fokus nicht erforderlich.

## 6.15 Benachrichtigungen

### FR-BEN-001 – Relevante Hinweise senden — Should

Kairos soll den Nutzer über abgeschlossene Analysen, notwendige Entscheidungen
und relevante Planänderungen informieren können.

### FR-BEN-002 – Benachrichtigungen konfigurieren — Must

Kanal, Anlass, Häufigkeit und Ruhezeiten müssen steuerbar sein.

### FR-BEN-003 – Keine manipulative Dringlichkeit erzeugen — Must

Benachrichtigungen dürfen keine unbegründete Angst oder Schuld erzeugen.

## 6.16 Datenkontrolle

### FR-DAT-001 – Daten einsehen — Must

Der Nutzer muss die über ihn gespeicherten fachlichen Daten einsehen können.

### FR-DAT-002 – Daten exportieren — Should

Der Nutzer soll seine Daten in einem dokumentierten, weiterverwendbaren Format
exportieren können.

### FR-DAT-003 – Daten löschen — Must

Der Nutzer muss Daten und Konto im Rahmen rechtlicher Aufbewahrungspflichten
löschen können.

### FR-DAT-004 – Verbindungen widerrufen — Must

Externe Datenquellen und Berechtigungen müssen jederzeit getrennt beziehungsweise
widerrufen werden können.

### FR-DAT-005 – Automatisierungsaktionen prüfen — Must

Der Nutzer muss erkennen können, welche Aktionen Kairos automatisch ausgeführt
hat.

## 6.17 Ausdrücklich ausgeschlossene Funktionen

Im aktuellen Planungshorizont werden folgende Funktionen nicht gefordert:

- medizinische Diagnose oder Therapie;
- automatische Vollkontrolle ohne konfigurierbare Grenzen;
- vollständige Lauf-, Schwimm- oder Triathlonplanung;
- soziale Feeds und öffentliche Ranglisten als Kernfunktion;
- medizinische Ernährungstherapie;
- garantierte Leistungsprognosen;
- automatische Bewertung von Krafttrainingstechnik ohne geeignete Sensordaten.

## 6.18 Festgelegte und spätere Entscheidungen

Der erste End-to-End-Ablauf fokussiert die Rad-Kernanalyse. Krafttraining wird
zunächst über Gewicht, Wiederholungen und Sätze beschrieben und benötigt später
eine komfortable Integration. Rudern bleibt aktiv vorgesehen und wird nach dem
Rad-Kern vertieft. Der Coach-Dialog gehört zu den Kernfunktionen. Geräteübertragung
und konkrete automatische Änderungstypen werden später im Backlog priorisiert.

## 6.19 Freigabestatus

Dieses Kapitel wurde inhaltlich mit dem Auftraggeber abgestimmt und fachlich
freigegeben.
