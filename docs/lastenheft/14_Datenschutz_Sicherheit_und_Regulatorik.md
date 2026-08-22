# 14. Datenschutz, Sicherheit und regulatorische Anforderungen

## 14.1 Einordnung

Kairos verarbeitet Trainings-, Erholungs-, Standort- und möglicherweise
Gesundheitsdaten. Gesundheitsdaten zählen nach der DSGVO zu besonders geschützten
Datenkategorien. Ihre Verarbeitung benötigt neben einer Rechtsgrundlage nach
Artikel 6 zusätzlich eine zulässige Ausnahme nach Artikel 9; für Kairos ist vor
produktiver Einführung insbesondere die Eignung einer ausdrücklichen Einwilligung
rechtlich zu prüfen.

Dieses Kapitel ist eine Produktanforderung und keine abschließende Rechtsberatung.
Vor öffentlichem Betrieb ist eine Prüfung für die konkret bedienten Länder,
Anbieter und Funktionen erforderlich.

## 14.2 Datenschutzprinzipien

Kairos muss folgende Prinzipien umsetzen:

- Rechtmäßigkeit, Transparenz und faire Verarbeitung;
- klar definierte Zwecke;
- Datenminimierung;
- sachliche Richtigkeit und Korrekturmöglichkeit;
- begrenzte Aufbewahrung;
- Integrität und Vertraulichkeit;
- nachweisbare Verantwortlichkeit;
- Datenschutz durch Technikgestaltung und Voreinstellungen.

## 14.3 Einwilligung und Wahlfreiheit

Einwilligungen müssen klar, zweckspezifisch, verständlich und von allgemeinen
Bedingungen unterscheidbar sein. Der Nutzer soll getrennt entscheiden können über:

- Trainingsdaten;
- Erholungs- und Gesundheitsdaten;
- Kalender;
- Standort und Routen;
- KI-Verarbeitung;
- Benachrichtigungen;
- optionale Weitergabe an einen Coach;
- automatische Planänderungen.

Ein Widerruf muss so einfach wie die Erteilung sein. Nicht erforderliche
Funktionen dürfen nicht an pauschale Einwilligungen gekoppelt werden.

## 14.4 Betroffenenrechte

Kairos muss Verfahren für Auskunft, Berichtigung, Löschung, Einschränkung,
Widerspruch und – soweit anwendbar – Datenübertragbarkeit bereitstellen. Exporte
sollen strukturiert, gebräuchlich und maschinenlesbar sein. Anfragen und ihre
Bearbeitung müssen nachweisbar, aber datensparsam dokumentiert werden.

## 14.5 Profiling und automatisierte Entscheidungen

Kairos erstellt Athletenprofile und kann automatisierte Empfehlungen erzeugen.
Der Nutzer muss über Zweck, verwendete Daten und wesentliche Logik informiert
werden. Er kann Annahmen korrigieren und automatisierte Planänderungen
bestreiten oder zurücknehmen.

Ob eine Funktion eine ausschließlich automatisierte Entscheidung mit rechtlicher
oder ähnlich erheblicher Wirkung darstellt, ist vor Einführung zu prüfen. Kairos
soll bereits produktseitig menschliche Kontrolle, Stellungnahme und Anfechtung
ermöglichen.

## 14.6 Datenschutz-Folgenabschätzung

Vor produktiver Verarbeitung umfangreicher Gesundheits-, Standort- oder
Profilingdaten ist zu prüfen und zu dokumentieren, ob eine
Datenschutz-Folgenabschätzung erforderlich ist. Aufgrund der Kombination
sensibler Langzeitdaten und KI-gestützter Bewertung ist eine frühe DPIA-Prüfung
als verbindlicher Arbeitspunkt vorzusehen.

## 14.7 Auftragsverarbeitung und Drittstaaten

Für Hosting, KI, E-Mail, Monitoring und externe Integrationen sind Rollen,
Verträge, Unterauftragnehmer, Speicherorte und internationale Übermittlungen zu
prüfen. Daten dürfen erst übertragen werden, wenn Rechtsgrundlage,
Schutzmaßnahmen und Nutzerinformation dokumentiert sind.

## 14.8 Sicherheitsanforderungen

Mindestens erforderlich sind:

- starke Authentifizierung und sichere Kontowiederherstellung;
- rollen- und zweckgebundene Zugriffe;
- Verschlüsselung während Übertragung und Speicherung;
- getrennte Geheimnisverwaltung;
- Protokollierung sicherheitsrelevanter Zugriffe;
- Schutz vor Missbrauch, automatisierten Angriffen und Datenabfluss;
- regelmäßige Sicherheitsupdates und Schwachstellenmanagement;
- getestete Backups und Wiederherstellung;
- Verfahren für Sicherheitsvorfälle und Datenschutzverletzungen;
- Löschung sensibler Inhalte aus Diagnoseprotokollen.

## 14.9 KI-Transparenz

Der Nutzer muss erkennen, dass er mit einem KI-System interagiert. Generierte
Analysen und Empfehlungen werden entsprechend gekennzeichnet. Seit August 2026
gelten die Transparenzregeln des EU AI Act für bestimmte KI-Interaktionen; die
konkrete Rolle von Kairos als Anbieter oder Betreiber und die Risikoklasse jeder
Funktion sind vor Markteinführung zu bewerten.

## 14.10 Medizinische und produktrechtliche Abgrenzung

Kairos wird als Trainingsunterstützung konzipiert, nicht zur Diagnose,
Behandlung, Überwachung oder Vorhersage von Krankheiten. Texte, Marketing,
Funktionen und technische Zweckbestimmung müssen diese Grenze konsistent
einhalten. Neue Funktionen – insbesondere Verletzungsbewertung, klinische
Entscheidung oder medizinische Ernährung – erfordern vor Umsetzung eine neue
regulatorische Bewertung.

## 14.11 Minderjährige

Kairos wird nicht grundsätzlich auf volljährige Nutzer beschränkt. Minderjährige
dürfen jedoch erst aufgenommen werden, wenn ein eigenes Alters-, Einwilligungs-,
Eltern-/Sorgeberechtigten-, Schutz- und Profilingkonzept rechtlich geprüft und
technisch umgesetzt ist. Bis diese Voraussetzungen erfüllt sind, darf die
Registrierung der betroffenen Altersgruppen nicht freigeschaltet werden.

## 14.12 Sicherheitsvorfälle

Kairos benötigt einen dokumentierten Prozess für Erkennung, Eindämmung,
Bewertung, Beweissicherung, Benachrichtigung und Nachbearbeitung. Gesetzliche
Melde- und Informationsfristen müssen im konkreten Betrieb geprüft und technisch
unterstützt werden.

## 14.13 Nachweise und Governance

Folgende Artefakte sind vor produktivem Betrieb erforderlich:

- Verzeichnis der Verarbeitungstätigkeiten;
- Datenfluss- und Empfängerübersicht;
- Rechtsgrundlagen- und Einwilligungsmatrix;
- Lösch- und Aufbewahrungskonzept;
- Rollen- und Berechtigungskonzept;
- Auftragsverarbeitungs- und Drittanbieterübersicht;
- DPIA-Prüfung beziehungsweise Folgenabschätzung;
- Sicherheits- und Vorfallkonzept;
- KI-Risiko- und Transparenzbewertung;
- verständliche Datenschutzinformation.

## 14.14 Offizielle Grundlagen

- [Europäische Kommission: besondere Kategorien personenbezogener Daten](https://commission.europa.eu/law/law-topic/data-protection/information-individuals_en)
- [Europäische Kommission: Rechtsgrundlagen und Gesundheitsdaten](https://commission.europa.eu/law/law-topic/data-protection/information-business-and-organisations/legal-grounds-processing-data_en)
- [Europäische Kommission: Datenschutz-Folgenabschätzung](https://commission.europa.eu/law/law-topic/data-protection/information-business-and-organisations/obligations/when-data-protection-impact-assessment-dpia-required_en)
- [Europäische Kommission: AI Act](https://digital-strategy.ec.europa.eu/en/policies/regulatory-framework-ai)
- [EU-Leitlinien zu KI-Transparenzpflichten](https://digital-strategy.ec.europa.eu/en/library/guidelines-transparency-obligations-providers-and-deployers-ai-systems)

## 14.15 Festgelegte Leitentscheidungen

1. Kairos soll nicht dauerhaft auf volljährige Nutzer beschränkt sein. Für
   Minderjährige gelten vor einer Freischaltung die besonderen Anforderungen aus
   Abschnitt 14.11.
2. Hosting und primäre Datenverarbeitung innerhalb der EU werden als
   verbindliches Ziel festgelegt. Unvermeidbare Drittlandübermittlungen erfordern
   eine gesonderte rechtliche, technische und vertragliche Prüfung.

## 14.16 Freigabestatus

Dieses Kapitel wurde inhaltlich mit dem Auftraggeber abgestimmt und fachlich
freigegeben. Die spätere juristische Prüfung der konkreten Umsetzung bleibt
erforderlich.
