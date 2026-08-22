# 2. Projektziele

## 2.1 Zweck dieses Kapitels

Dieses Kapitel übersetzt die Produktvision in fachliche Projektziele. Es legt
fest, welche Ergebnisse Kairos erreichen soll, welche Wirkungen erwartet werden
und anhand welcher Kriterien die Zielerreichung später beurteilt werden kann.

Die Ziele beschreiben das gewünschte Ergebnis aus Sicht der Nutzer und des
Produkts. Konkrete technische Lösungen werden erst im Pflichtenheft festgelegt.

## 2.2 Übergeordnetes Projektziel

Das übergeordnete Ziel ist die Entwicklung einer digitalen Coaching-Plattform,
die Ausdauersportler über mehrere Sportarten hinweg individuell, datenbasiert und
nachvollziehbar bei der Erreichung langfristiger Trainings- und Wettkampfziele
unterstützt.

Kairos soll verfügbare Trainings-, Erholungs-, Ziel- und Kontextdaten in
verständliche Analysen und konkrete nächste Schritte übersetzen. Das Produkt soll
dem Nutzer nicht lediglich Informationen bereitstellen, sondern ihn beim Treffen
besserer Trainingsentscheidungen unterstützen.

## 2.3 Strategische Produktziele

### Z-STR-01 – Sportartenübergreifende Coaching-Plattform

Kairos soll als Plattform für mehrere Ausdauersportarten konzipiert werden. Die
fachlichen Grundfunktionen für Zielverwaltung, Belastungssteuerung, Erholung,
Planung und Erklärbarkeit sollen sportartenübergreifend nutzbar sein.

Einzelne Sportarten dürfen schrittweise vertieft werden. Der anfängliche
fachliche Fokus liegt auf Radfahren, Krafttraining zur Verbesserung der
Radleistung sowie Rudern. Radfahren bildet dabei den Hauptfokus. Weitere
Sportarten bleiben eine spätere Erweiterungsmöglichkeit. Ein
begrenzter erster Funktionsumfang widerspricht der langfristigen
Plattformausrichtung nicht.

### Z-STR-02 – Mehrwert durch Interpretation statt Datenmenge

Kairos soll sich von reinen Tracking- und Analyseplattformen dadurch abgrenzen,
dass es Daten interpretiert, Zusammenhänge erklärt und daraus konkrete
Handlungsempfehlungen ableitet.

### Z-STR-03 – Langfristige Zielsteuerung

Kairos soll tägliche und wöchentliche Trainingsentscheidungen mit langfristigen,
terminierten Nutzerzielen verbinden. Fortschritt, Risiken, notwendige Teilziele
und Prognoseunsicherheit sollen fortlaufend sichtbar sein.

### Z-STR-04 – Vertrauenswürdige und erklärbare KI

KI-gestützte Aussagen sollen nachvollziehbar, datenbezogen und als Analyse,
Prognose oder Empfehlung erkennbar sein. Kairos soll Vertrauen nicht durch eine
scheinbar sichere Sprache, sondern durch Transparenz, Konsistenz und
Kontrollierbarkeit aufbauen.

### Z-STR-05 – Kontrollierbare Automatisierung

Kairos soll Nutzern die Wahl geben, ob Planänderungen nur vorgeschlagen oder
innerhalb festgelegter Grenzen automatisch durchgeführt werden. Automatisierung
muss ausdrücklich aktiviert, jederzeit deaktivierbar und in ihren Auswirkungen
nachvollziehbar sein.

### Z-STR-06 – Erweiterbare Produktbasis

Das Produkt soll so geplant werden, dass zusätzliche Sportarten, Datenquellen,
Analyseverfahren und Coaching-Funktionen schrittweise ergänzt werden können,
ohne den fachlichen Kern grundlegend neu entwerfen zu müssen.

## 2.4 Fachliche Hauptziele

### Z-FACH-01 – Zentrale Zielverwaltung

Nutzer sollen langfristige und kurzfristige Ziele mit Zielwert, Zieldatum,
Sportart, Priorität und ergänzender Beschreibung erfassen können. Kairos soll den
Fortschritt zu diesen Zielen regelmäßig bewerten.

**Erwartetes Ergebnis:** Der Nutzer erkennt jederzeit, welche Ziele aktiv sind,
wie sich der Fortschritt entwickelt und wo Handlungsbedarf besteht.

### Z-FACH-02 – Zusammenführung relevanter Daten

Kairos soll die für Coaching-Entscheidungen erforderlichen Daten aus geeigneten
Quellen zusammenführen. Dazu gehören je nach Verfügbarkeit insbesondere:

- Aktivitäten und strukturierte Trainings;
- Herzfrequenz, Leistung, Geschwindigkeit und Trittfrequenz;
- Trainingsdauer, Distanz und Höhenprofil;
- Schlaf, HRV, Ruhepuls und Erholungsindikatoren;
- subjektive Rückmeldungen des Nutzers;
- Kalenderereignisse und verfügbare Trainingszeit;
- Wetter- und Umgebungsbedingungen.

**Erwartetes Ergebnis:** Der Nutzer muss entscheidungsrelevante Informationen
nicht dauerhaft manuell aus mehreren Plattformen zusammensuchen.

### Z-FACH-03 – Qualitative Trainingsanalyse

Kairos soll absolvierte Trainingseinheiten sportartspezifisch analysieren. Neben
Gesamtwerten sollen Struktur, Ausführung, Belastungsverlauf, Abweichungen und
Vergleichbarkeit mit früheren Einheiten berücksichtigt werden.

**Erwartetes Ergebnis:** Der Nutzer versteht, was während einer Einheit passiert
ist, wie gut sie zum Trainingszweck passte und was beim nächsten Mal angepasst
werden sollte.

### Z-FACH-04 – Individuelle Trainingsplanung

Kairos soll auf Grundlage von Zielen, aktuellem Leistungsstand, verfügbarer Zeit,
Erholung und bisherigem Trainingsverlauf geeignete Pläne und Einheiten
vorschlagen.

**Erwartetes Ergebnis:** Der Trainingsplan ist nicht statisch, sondern bildet die
individuelle Situation des Nutzers ab.

### Z-FACH-05 – Situative Plananpassung

Kairos soll relevante Veränderungen erkennen und daraus begründete
Plananpassungen ableiten. Mögliche Auslöser sind unter anderem:

- nicht absolvierte oder abweichend absolvierte Einheiten;
- ungewöhnlich hohe Ermüdung oder schlechte Erholung;
- Krankheit oder Verletzungsmeldung;
- neue Kalenderkonflikte;
- ungeeignete Wetterbedingungen;
- veränderte Ziele oder Prioritäten.

Im Vorschlagsmodus muss der Nutzer Änderungen bestätigen. Im optionalen
Automatikmodus darf Kairos Änderungen innerhalb der vom Nutzer festgelegten
Grenzen selbst durchführen.

**Erwartetes Ergebnis:** Der Plan bleibt auch unter realen Alltagsbedingungen
aktuell und umsetzbar.

### Z-FACH-06 – Erklärbare Empfehlungen

Jede wesentliche Empfehlung oder Planänderung soll eine verständliche Begründung
enthalten. Dabei sollen verwendete Daten, Beobachtungen, Unsicherheiten und der
Bezug zum Ziel erkennbar sein.

**Erwartetes Ergebnis:** Nutzer können Empfehlungen prüfen und eine informierte
Entscheidung treffen.

### Z-FACH-07 – Langfristiges Athletenprofil

Kairos soll individuelle Muster, Präferenzen und Reaktionen über die Zeit
berücksichtigen. Nutzer müssen relevante Profildaten einsehen, korrigieren und
löschen können.

**Erwartetes Ergebnis:** Empfehlungen werden mit wachsender, verlässlicher
Datenbasis persönlicher, ohne dem Nutzer die Kontrolle über seine Daten zu
entziehen.

### Z-FACH-08 – Fortschritts- und Risikoerkennung

Kairos soll positive und negative Entwicklungen frühzeitig erkennen. Dazu gehören
Fortschritt, Stagnation, auffällige Leistungsabweichungen, dauerhaft erhöhte
Belastung und gefährdete Zieltermine.

**Erwartetes Ergebnis:** Der Nutzer wird nicht erst am Ende eines Trainingsblocks
oder kurz vor einem Wettkampf auf relevante Abweichungen aufmerksam.

### Z-FACH-09 – Einheitliche Athleten-Timeline

Kairos soll Aktivitäten, Trainingspläne, Ziele und relevante Lebensereignisse in
einem zeitlichen Zusammenhang darstellen. Dazu können Krankheit, Urlaub,
Wettkämpfe, Tests, Trainingslager und Ausrüstungsänderungen gehören.

**Erwartetes Ergebnis:** Entwicklungen lassen sich im Kontext nachvollziehen und
nicht nur als isolierte Messwerte betrachten.

### Z-FACH-10 – Nutzerfeedback als Bestandteil des Coachings

Nutzer sollen Empfehlungen, Trainings und ihren subjektiven Zustand bewerten oder
kommentieren können. Kairos soll diese Rückmeldungen bei späteren Analysen
berücksichtigen.

**Erwartetes Ergebnis:** Coaching basiert nicht ausschließlich auf Sensordaten,
sondern bezieht die Wahrnehmung des Sportlers ein.

### Z-FACH-11 – Spätere trainingsbezogene Ernährungshinweise

Kairos kann später ernährungsbezogene Informationen im Zusammenhang mit
Training, Erholung und Zielen berücksichtigen. Das System kann verständliche
Hinweise geben, welche Art von Ernährung vor, während und nach einer Belastung
sinnvoll sein kann. Ernährung ist jedoch kein Hauptfokus des anfänglichen
Produkts.

Empfehlungen müssen ihren Zweck und ihre Datengrundlage erklären. Sie sind von
medizinischer Ernährungstherapie abzugrenzen und dürfen Allergien,
Unverträglichkeiten oder bekannte gesundheitliche Einschränkungen nicht
ignorieren.

**Erwartetes Ergebnis:** Der Nutzer erhält alltagstaugliche, trainingsbezogene
Ernährungshinweise statt isolierter Kalorien- oder Makronährstoffwerte.

## 2.5 Nutzerbezogene Ziele

### Z-NUTZ-01 – Verständlichkeit

Auch Nutzer ohne sportwissenschaftliche Ausbildung sollen die wichtigsten
Ergebnisse verstehen können. Fachbegriffe und Kennzahlen müssen bei Bedarf
erläutert werden.

### Z-NUTZ-02 – Handlungsorientierung

Eine Analyse soll nach Möglichkeit mit einer klaren Aussage enden, was der Nutzer
beibehalten, ändern oder beobachten sollte.

### Z-NUTZ-03 – Zeitersparnis

Kairos soll den manuellen Aufwand für das Zusammenführen, Vergleichen und
Interpretieren von Trainingsinformationen reduzieren.

### Z-NUTZ-04 – Selbstbestimmung

Der Nutzer behält die Kontrolle über Ziele, Daten, Empfehlungen und
Automatisierungsgrad. Automatische Aktionen dürfen nicht ohne vorherige
Aktivierung erfolgen.

### Z-NUTZ-05 – Kontinuität

Der Nutzer soll über längere Zeiträume begleitet werden und Entscheidungen auch
Monate später anhand ihres damaligen Kontexts nachvollziehen können.

### Z-NUTZ-06 – Sportartenübergreifende Konsistenz

Wenn ein Nutzer mehrere Ausdauersportarten ausübt, soll Kairos Gesamtbelastung,
Erholung und Zielkonflikte sportartenübergreifend betrachten, anstatt jede
Sportart vollständig isoliert zu behandeln.

## 2.6 Qualitätsziele auf Produktebene

Die folgenden Qualitätsziele werden hier zunächst grundsätzlich festgelegt und
in späteren Kapiteln mit messbaren Grenzwerten konkretisiert.

### Z-QUAL-01 – Nachvollziehbarkeit

Der Nutzer muss erkennen können, warum eine Empfehlung oder Änderung entstanden
ist.

### Z-QUAL-02 – Datenrichtigkeit

Importierte und berechnete Daten müssen fachlich korrekt, auf ihre Herkunft
zurückführbar und vor stillen Veränderungen geschützt sein.

### Z-QUAL-03 – Zuverlässigkeit

Fehlende oder verspätete Daten dürfen nicht zu unbegründet sicheren Aussagen
führen. Das System muss Einschränkungen sichtbar machen.

### Z-QUAL-04 – Datenschutz und Datensouveränität

Nutzer müssen Kontrolle über die Verwendung, Speicherung, Korrektur, den Export
und die Löschung ihrer Daten besitzen.

### Z-QUAL-05 – Sicherheit

Gesundheits- und Trainingsdaten müssen vor unberechtigtem Zugriff und
unbeabsichtigter Offenlegung geschützt werden.

### Z-QUAL-06 – Bedienbarkeit

Häufige Aufgaben sollen ohne umfangreiche Einarbeitung möglich sein. Komplexität
soll schrittweise offengelegt werden.

### Z-QUAL-07 – Reversibilität

Automatische Planänderungen müssen dokumentiert und soweit fachlich möglich
rückgängig zu machen sein.

### Z-QUAL-08 – Erweiterbarkeit

Neue Sportarten und Integrationen sollen ergänzt werden können, ohne bestehende
Nutzerabläufe unnötig zu beeinträchtigen.

## 2.7 Ziele des ersten nutzbaren Produktinkrements

Kairos wird agil und iterativ entwickelt. Es bestehen derzeit keine fest
vorgegebenen Produktversionen oder starren Versionsgrenzen. Das erste nutzbare
Inkrement soll dennoch einen vollständigen, überprüfbaren Ablauf bereitstellen:

1. Ein Nutzer kann ein Athletenprofil und mindestens ein Trainingsziel anlegen.
2. Aktivitäten aus Radfahren, unterstützendem Krafttraining und Rudern können aus
   mindestens einer festgelegten Quelle übernommen oder erfasst werden.
3. Kairos speichert und visualisiert die wesentlichen Aktivitätsdaten.
4. Das System erkennt die Struktur beziehungsweise relevante Abschnitte der
   Einheit.
5. Eine begrenzte Menge fachlich validierter Kennzahlen wird berechnet.
6. Kairos erstellt bereits im ersten Inkrement eine nachvollziehbare Analyse mit
   konkretem Trainingsfeedback.
7. Der Nutzer kann die Analyse bewerten und um subjektives Feedback ergänzen.
8. Aktivität, Analyse und Rückmeldung bleiben später nachvollziehbar.
9. Kalender- und Wetterinformationen sollen früh integriert werden, sobald sie
   für eine konkrete Trainingsentscheidung zuverlässig nutzbar sind.
10. Trainingsbezogene Ernährungshinweise können in einem späteren Inkrement in
    denselben Coaching-Ablauf integriert werden.

Die Reihenfolge und Tiefe dieser Fähigkeiten werden laufend anhand von Nutzen,
Risiko, Abhängigkeiten und Nutzerfeedback priorisiert. Das erste Inkrement muss
noch keine vollständige autonome Trainingsplanung, Routenplanung oder
Unterstützung sämtlicher Ausdauersportarten enthalten.

## 2.8 Nichtziele des anfänglichen Entwicklungsumfangs

Folgende Punkte gehören ausdrücklich nicht zwingend zum ersten nutzbaren Umfang:

- vollständige Unterstützung aller Ausdauersportarten;
- medizinische Diagnose oder Therapieempfehlung;
- garantierte Leistungs- oder Wettkampfprognosen;
- vollständig autonome Saisonplanung;
- automatische Änderung von Plänen ohne aktivierten Automatikmodus;
- medizinische Ernährungstherapie oder Behandlung ernährungsbedingter
  Erkrankungen;
- Unterstützung aller Hersteller und Wearables;
- soziale Netzwerkfunktionen;
- Marktplatz für Coaches oder Trainingspläne;
- komplexe Routenoptimierung;
- Monetarisierungs- und Abonnementfunktionen.

Vision unkontrolliert anwächst.
Diese Abgrenzung verhindert, dass der anfängliche Entwicklungsumfang durch die
langfristige Vision unkontrolliert anwächst. Sie wird im agilen Backlog regelmäßig
überprüft und bei einer bewussten Prioritätsentscheidung angepasst.
Vision unkontrolliert anwächst.

## 2.9 Zielkonflikte

Bei der weiteren Planung sind insbesondere folgende Zielkonflikte zu beachten:

### Umfang gegen Geschwindigkeit

Die Unterstützung mehrerer Sportarten ist ein langfristiges strategisches Ziel.
Zunächst werden Radfahren, Krafttraining für Radfahrer und Rudern unterstützt.
Radfahren erhält die größte fachliche Tiefe. Die Plattform wird erweiterbar
gedacht, ohne die aktuelle Entwicklung durch weitere Sportarten zu verwässern.

### Automatisierung gegen Kontrolle

Automatische Anpassung erhöht Komfort und Aktualität, kann aber Vertrauen
gefährden. Deshalb bleibt sie optional, begrenzt, transparent und reversibel.

### Personalisierung gegen Datenschutz

Langfristiges Lernen benötigt historische Daten. Gleichzeitig müssen
Datensparsamkeit, Zweckbindung und Nutzerkontrolle gewahrt bleiben.

### Einfache Bedienung gegen fachliche Tiefe

Kairos soll leicht verständlich sein, ohne wichtige Details zu verbergen. Dafür
sind abgestufte Darstellungen erforderlich: klare Kernaussage zuerst, fachliche
Belege und Detailanalyse auf Wunsch.

### Konkrete Empfehlung gegen Unsicherheit

Nutzer benötigen handlungsorientierte Aussagen. Das System darf Unsicherheit
dennoch nicht verschweigen oder fehlende Daten durch scheinbare Präzision
ersetzen.

## 2.10 Vorläufige Erfolgsindikatoren

Die konkreten Zielwerte werden später festgelegt. Folgende Indikatoren sollen
grundsätzlich zur Bewertung des Produkterfolgs dienen:

- Anteil erfolgreich und vollständig importierter Aktivitäten;
- fachliche Genauigkeit erkannter Trainingsabschnitte;
- Nachvollziehbarkeit der verwendeten Daten je Empfehlung;
- Anteil der Empfehlungen, die Nutzer als hilfreich bewerten;
- Häufigkeit manueller Korrekturen an importierten oder berechneten Daten;
- Anteil angenommener beziehungsweise beibehaltener Plananpassungen;
- Nutzungshäufigkeit über mehrere Wochen und Monate;
- Zeitersparnis gegenüber der manuellen Analyse;
- erkennbare Fortschritte zu definierten Nutzerzielen;
- Anzahl sicher erkannter Fälle mit unzureichender Datenbasis;
- Häufigkeit zurückgenommener automatischer Änderungen.

Diese Indikatoren dürfen nicht isoliert betrachtet werden. Insbesondere darf eine
hohe Annahmerate nicht durch manipulative Gestaltung oder übermäßige
Automatisierung erreicht werden.

## 2.11 Priorisierungsmethode

Die Anforderungen werden zur transparenten Umfangssteuerung in einem laufend
gepflegten Product Backlog nach MoSCoW priorisiert:

- **Must:** zwingend erforderlich, damit die jeweilige Produktstufe ihren Zweck
  erfüllt;
- **Should:** hoher Nutzen, aber bei nachvollziehbarer Begründung verschiebbar;
- **Could:** wünschenswerte Erweiterung ohne kritische Bedeutung;
- **Won't for now:** bewusst nicht Bestandteil der betrachteten Produktstufe.

Die Priorität bezieht sich immer auf den aktuellen Planungshorizont. Eine
langfristig wichtige Funktion kann für das nächste Inkrement dennoch als „Won't
for now“ eingestuft werden. Erkenntnisse aus Nutzung, Tests und Entwicklung
können die Reihenfolge jederzeit verändern; freigegebene Vision und
Sicherheitsgrenzen bleiben davon unberührt.

## 2.12 Festgelegte Leitentscheidungen

1. Der anfängliche Sportartenfokus umfasst Radfahren, Krafttraining zur
   Verbesserung der Radleistung und Rudern. Radfahren ist der Hauptfokus;
   weitere, darüber hinausgehende Sportarten sind vorerst nachrangig.
2. Langfristige Trainingsziele werden bereits früh berücksichtigt.
3. Subjektives Trainingsfeedback gehört zum ersten vollständigen Ablauf.
4. Kalender und Wetter sollen möglichst früh integriert, aber nur auf Grundlage
   belastbarer Analysefunktionen für Entscheidungen verwendet werden.
5. Trainingsbezogene Ernährungsempfehlungen sind eine spätere Nebenfunktion und
   kein Hauptfokus; medizinische Ernährungstherapie bleibt ausgeschlossen.
6. Kairos wird agil in fortlaufenden Produktinkrementen entwickelt. Starre
   Versionsstufen werden nicht vorab festgelegt.

## 2.13 Freigabestatus

Dieses Kapitel wurde inhaltlich mit dem Auftraggeber abgestimmt und fachlich
freigegeben.
