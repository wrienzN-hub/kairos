# 13. Nichtfunktionale Anforderungen

## 13.1 Zweck

Nichtfunktionale Anforderungen bestimmen, wie zuverlässig, sicher, schnell und
wartbar Kairos seine Funktionen bereitstellt. Messwerte sind anfängliche
Zielwerte und werden anhand realer Nutzung iterativ validiert.

## 13.2 Performance

### NFR-PER-001 – Interaktive Reaktion — Must

Übliche Navigation und lokale Eingabe sollen bei normaler Last innerhalb von
zwei Sekunden sichtbar reagieren.

### NFR-PER-002 – Aktivitätsanzeige — Must

Eine bereits verarbeitete Aktivität soll im Regelfall innerhalb von drei Sekunden
geöffnet werden können.

### NFR-PER-003 – Analyseverarbeitung — Should

Der Import und die erste Analyse einer üblichen Aktivität sollen im Regelfall
innerhalb von 60 Sekunden abgeschlossen oder als laufender Prozess sichtbar sein.

### NFR-PER-004 – Coach-Antwort — Should

Der Nutzer soll innerhalb weniger Sekunden eine sichtbare Reaktion erhalten.
Längere Berechnungen müssen Fortschritt oder Zwischenstatus zeigen.

## 13.3 Verfügbarkeit und Robustheit

### NFR-ROB-001 – Kontrollierter Fehlerzustand — Must

Der Ausfall einer externen Integration darf die übrigen Kernfunktionen nicht
unbenutzbar machen.

### NFR-ROB-002 – Wiederholbarkeit — Must

Importe und Hintergrundprozesse müssen gefahrlos erneut ausgeführt werden können,
ohne Aktivitäten oder Belastungen doppelt anzulegen.

### NFR-ROB-003 – Datenintegrität — Must

Bestätigte Nutzerdaten und Planänderungen dürfen bei Fehlern nicht unbemerkt
verloren oder teilweise überschrieben werden.

### NFR-ROB-004 – Wiederherstellung — Must

Für produktive Daten sind überprüfte Sicherungs- und
Wiederherstellungsverfahren erforderlich.

## 13.4 Sicherheit

- Verschlüsselung vertraulicher Daten bei Übertragung und Speicherung;
- sichere Authentifizierung und Sitzungsverwaltung;
- minimal erforderliche Berechtigungen;
- Schutz vor typischen Webangriffen;
- keine Geheimnisse in Quellcode oder Protokollen;
- getrennte Entwicklungs-, Test- und Produktionsumgebungen;
- protokollierte administrative Zugriffe;
- regelmäßige Aktualisierung von Abhängigkeiten;
- sichere Validierung externer und KI-generierter Eingaben.

Konkrete Kontrollmaßnahmen werden im Pflichtenheft festgelegt.

## 13.5 Datenschutz

Kairos muss Datenschutz durch Technikgestaltung und datenschutzfreundliche
Voreinstellungen unterstützen. Optionale Datenquellen bleiben deaktiviert, bis
der Nutzer sie bewusst verbindet. Datenerhebung, Aufbewahrung und Weitergabe sind
zweckgebunden und minimiert.

## 13.6 Bedienbarkeit

### NFR-USAB-001 – Verständlichkeit — Must

Kernaussagen müssen ohne sportwissenschaftliche Ausbildung verständlich sein.

### NFR-USAB-002 – Progressive Offenlegung — Must

Die Oberfläche zeigt zuerst die wesentliche Entscheidung und bietet Details bei
Bedarf.

### NFR-USAB-003 – Konsistenz — Must

Begriffe, Einheiten, Statusfarben und Interaktionen müssen über alle Sportarten
hinweg konsistent sein.

### NFR-USAB-004 – Korrekturmöglichkeit — Must

Der Nutzer muss fehlerhafte Daten und KI-Annahmen an der relevanten Stelle
korrigieren können.

## 13.7 Barrierefreiheit

Die Weboberfläche soll sich an WCAG 2.2 Level AA orientieren. Dazu gehören
Tastaturbedienbarkeit, sichtbarer Fokus, semantische Beschriftung, ausreichender
Kontrast, skalierbare Texte und Alternativen zu rein farblicher Kodierung.

## 13.8 Kompatibilität

Die Anwendung soll aktuelle verbreitete Desktop- und Mobilbrowser unterstützen.
Das responsive Layout soll mindestens Smartphone und Desktop sinnvoll abdecken.
Hersteller- oder betriebssystemspezifische Funktionen dürfen den Kernzugang nicht
verhindern.

## 13.9 Wartbarkeit und Erweiterbarkeit

- fachliche Module besitzen klare Verantwortungen;
- Rad-, Kraft- und Ruderlogik bleiben unterscheidbar;
- externe Anbieter werden über austauschbare Integrationsgrenzen angebunden;
- Berechnungen und Regeln sind automatisiert testbar;
- Änderungen an Kennzahlen sind versioniert;
- öffentliche und interne Schnittstellen sind dokumentiert;
- technische Schulden werden im Backlog sichtbar geführt.

## 13.10 Testbarkeit

Kritische Berechnungen, Berechtigungen, Datenimporte und Planänderungen benötigen
automatisierte Tests. KI-Funktionen benötigen wiederholbare Evaluationsfälle für
Faktentreue, Sicherheit, Erklärbarkeit und Verhalten bei fehlenden Daten.

## 13.11 Beobachtbarkeit

Kairos soll technische Fehler, Laufzeiten, Importstatus und externe Abhängigkeiten
überwachen können. Protokolle dürfen keine unnötigen Gesundheits-, Standort- oder
Coach-Inhalte enthalten. Nutzerbezogene Aktionen müssen über geschützte
Auditdaten nachvollziehbar sein.

## 13.12 Skalierbarkeit

Die Architektur soll wachsende Aktivitätsmengen und Zeitreihen verarbeiten
können. Eine vorzeitige Microservice-Aufteilung ist kein Ziel. Skalierung soll
messwertbasiert und ohne Verlust fachlicher Konsistenz erfolgen.

## 13.13 Portabilität und Anbieterunabhängigkeit

Der Kernverlauf darf nicht von einem einzelnen Trainings-, Cloud- oder
KI-Anbieter abhängig sein. Nutzerexporte und austauschbare Integrationen sollen
einen Anbieterwechsel ermöglichen.

## 13.14 Qualitätsziel für KI

KI-Antworten müssen auf vorhandene Daten zurückführbar sein, Unsicherheit nennen
und Berechtigungsgrenzen einhalten. Für zentrale Coach-Fälle werden vorab
Akzeptanzdatensätze und manuell geprüfte Referenzerwartungen definiert.

## 13.15 Festgelegte Leitentscheidungen

1. Die anfänglichen Performanceziele werden als geeignete Ausgangswerte
   übernommen und später anhand realer Messungen überprüft.
2. WCAG 2.2 Level AA wird als verbindliches Ziel für die Weboberfläche
   festgelegt. Noch nicht erfüllte Kriterien müssen sichtbar dokumentiert und
   priorisiert werden.

## 13.16 Freigabestatus

Dieses Kapitel wurde inhaltlich mit dem Auftraggeber abgestimmt und fachlich
freigegeben.
