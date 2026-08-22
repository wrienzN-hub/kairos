# 5. Nutzungskontext und Systemübersicht

## 5.1 Zweck dieses Kapitels

Dieses Kapitel beschreibt, in welchen Situationen Kairos genutzt wird, welche
Informationen das System empfängt und ausgibt und wo seine fachlichen Grenzen zu
externen Diensten liegen. Es handelt sich um eine fachliche Systemübersicht, noch
nicht um eine technische Architektur.

## 5.2 Typischer Nutzungskontext

Kairos wird über längere Zeiträume regelmäßig verwendet. Die Nutzung findet
nicht nur während einer Trainingseinheit statt, sondern vor, nach und zwischen
Einheiten.

### Vor dem Training

Der Nutzer prüft die geplante Einheit, Zielsetzung, Dauer, Intensität und
Begründung. Kairos berücksichtigt verfügbare Erholungs-, Kalender- und
Wetterinformationen und weist auf relevante Abweichungen hin.

### Während des Trainings

Die eigentliche Aufzeichnung erfolgt zunächst überwiegend durch vorhandene
Geräte oder Trainingsplattformen. Kairos kann strukturierte Trainings zur
Übertragung bereitstellen und später ausgewählte Hinweise liefern. Es ist keine
Notfallüberwachung.

### Nach dem Training

Aktivitätsdaten werden importiert oder erfasst. Kairos analysiert die Einheit und
fordert kurzes subjektives Feedback an, zum Beispiel wahrgenommene Belastung,
Bein- oder Gesamtermüdung, Schmerzen, Motivation und besondere Umstände.

### Zwischen Trainingseinheiten

Kairos aktualisiert Fortschritt, Belastungsentwicklung und Planung. Der Nutzer
kann Fragen stellen, Ziele ändern, Daten korrigieren und den Automatisierungsgrad
steuern.

## 5.3 Zentrale Nutzungsszenarien

### NK-01 – Radtraining analysieren

Ein Nutzer synchronisiert eine Radaktivität. Kairos erkennt relevante Abschnitte
oder Intervalle, berechnet validierte Kennzahlen, vergleicht die Einheit mit
ihrem Ziel und ähnlichen Aktivitäten und liefert konkretes Feedback.

### NK-02 – Krafttraining als Ergänzung einplanen

Kairos berücksichtigt Übungen, Volumen und Belastung eines Krafttrainings bei der
Planung des Radtrainings. Eine schwere Beineinheit soll nicht unbemerkt mit einer
hochintensiven Radeinheit kollidieren.

### NK-03 – Rudertraining erfassen

Eine Rudereinheit wird importiert oder erfasst und anhand verfügbarer Daten wie
Dauer, Pace, Leistung, Schlagfrequenz, Herzfrequenz und Intervallstruktur
analysiert. Die Belastung fließt in die Gesamtbetrachtung ein.

### NK-04 – Tagesplan an Erholung anpassen

Kairos erkennt eine relevante Abweichung bei Erholung oder subjektivem Zustand.
Im Vorschlagsmodus empfiehlt es eine Änderung. Im aktivierten Automatikmodus darf
es innerhalb der Nutzerregeln umplanen und dokumentiert die Änderung.

### NK-05 – Kalenderkonflikt behandeln

Ein neuer Termin reduziert die verfügbare Trainingszeit. Kairos schlägt eine
zeitlich passende Alternative, Verschiebung oder Priorisierung vor, ohne private
Termindetails unnötig zu speichern.

### NK-06 – Wetterbedingte Anpassung

Wind, Hitze, Kälte, Starkregen oder Gewitter beeinflussen eine geplante
Outdoor-Einheit. Kairos erläutert die Auswirkung und schlägt Zeitpunkt,
Streckenart, Intensität oder Indoor-Alternative vor.

### NK-07 – Langfristiges Ziel überprüfen

Der Nutzer betrachtet Fortschritt, relevante Teilziele, Unsicherheit und
gefährdende Faktoren. Kairos unterscheidet klar zwischen Messwert, Berechnung und
Prognose.

### NK-08 – Coach-Dialog verwenden

Der Nutzer fragt beispielsweise, warum seine Herzfrequenz in einer Einheit
niedriger war als sonst. Kairos verwendet freigegebene Daten, nennt die
wahrscheinlichsten Erklärungen, weist auf Unsicherheit hin und erfindet keine
fehlenden Messwerte.

## 5.4 Fachliche Systemgrenze

Innerhalb der fachlichen Verantwortung von Kairos liegen:

- Verwaltung des Athletenprofils und der Nutzereinstellungen;
- Ziel- und Teilzielverwaltung;
- Übernahme, Normalisierung und Zuordnung von Trainingsdaten;
- Speicherung der Kairos-relevanten Historie;
- fachlich validierte Kennzahlen und Analysen;
- Trainingsfeedback und begründete Empfehlungen;
- Planung und kontrollierte Anpassung von Training;
- Zusammenführung von Rad-, Kraft- und Ruderbelastung;
- Einbindung von Kalender- und Wetterkontext;
- Nutzerfeedback, Timeline und Änderungsverlauf;
- Dialog auf Basis der freigegebenen Daten;
- Steuerung von Einwilligungen und Automatisierung.

Außerhalb der unmittelbaren Verantwortung liegen:

- Messgenauigkeit externer Sensoren;
- Verfügbarkeit und Vertragsbedingungen externer Dienste;
- medizinische Diagnose und Therapie;
- physische Durchführung eines Trainings;
- sichere Wettkampf- oder Leistungsresultate;
- vollständige Richtigkeit externer Wetterprognosen;
- Entscheidungen, die der Nutzer außerhalb von Kairos trifft.

Kairos muss externe Einschränkungen erkennen und transparent kommunizieren.

## 5.5 Externe Systeme und Datenflüsse

### Trainingsgeräte und Plattformen

Mögliche Quellen sind Fahrradcomputer, Sportuhren, Leistungsmesser, Smarttrainer,
Ruderergometer, Garmin Connect und dateibasierte Importe wie FIT. Die konkrete
Reihenfolge der Integrationen wird agil priorisiert.

**Eingaben:** Aktivitäten, Zeitreihen, Geräteinformationen, Gesundheits- und
Erholungsdaten, strukturierte Trainings.

**Ausgaben:** je nach unterstützter Schnittstelle geplante oder strukturierte
Trainings.

### Kalenderdienste

Kalender liefern Verfügbarkeit und Konflikte. Kairos soll möglichst mit
Zeitfenstern und Kategorien arbeiten und Inhalte nur verarbeiten, wenn sie für
die Planung erforderlich sind.

### Wetterdienste

Wetterdienste liefern orts- und zeitbezogene Prognosen beziehungsweise
Beobachtungen. Herkunft und Aktualisierungszeitpunkt müssen nachvollziehbar sein.

### KI-Dienst

Ein KI-Dienst kann natürliche Sprache, Zusammenfassung und begründete Vorschläge
unterstützen. Deterministische Berechnungen und verbindliche Sicherheitsregeln
dürfen nicht ausschließlich von einem Sprachmodell abhängen.

### Benachrichtigungsdienste

E-Mail, Push oder andere Kanäle können Hinweise ausliefern. Der Nutzer steuert
Kanal, Häufigkeit und Ruhezeiten.

## 5.6 Fachliche Informationsbereiche

Kairos verarbeitet folgende Hauptbereiche:

- **Identität und Einstellungen:** Konto, Einheiten, Sprache, Zeitzone,
  Benachrichtigungen und Automatisierung;
- **Athletenprofil:** Erfahrung, Präferenzen, Leistungsbereiche und relevante
  Einschränkungen;
- **Ziele:** Zieltyp, Zielwert, Datum, Priorität, Teilziele und Status;
- **Aktivitäten:** Sportart, Zeit, Dauer, Distanz, Messwerte, Abschnitte und
  Quelldaten;
- **Krafttraining:** Übung, Satz, Wiederholung, Last, Pause, RPE und Zielbezug;
- **Rudern:** Pace, Leistung, Schlagfrequenz, Distanz und Intervallstruktur;
- **Erholung:** Schlaf, HRV, Ruhepuls, subjektiver Zustand und Datenqualität;
- **Planung:** geplante Einheiten, Zweck, Belastung, Zeitfenster und Änderungen;
- **Kontext:** Kalender, Wetter, Reise, Krankheit, Beschwerden und Ereignisse;
- **Coaching:** Analysen, Empfehlungen, Begründungen, Unsicherheit und Feedback;
- **Historie:** Timeline, Datenkorrekturen, Planänderungen und Berechtigungen.

## 5.7 Betriebs- und Nutzungsvoraussetzungen

Für den vollen Nutzen benötigt Kairos:

- ein Nutzerkonto und verständliche Einwilligungen;
- mindestens eine Aktivitätsquelle oder manuelle Erfassung;
- ausreichend verlässliche Daten für die gewünschte Analyse;
- Internetzugang für Synchronisation und externe Dienste;
- korrekte Zeitzone, Einheiten und grundlegende Profildaten;
- aktive Freigabe für optionale Kalender-, Wetter- oder Automatikfunktionen.

Ohne verbundene Datenquelle muss ein eingeschränkter manueller Betrieb möglich
sein. Fehlende Daten müssen als solche dargestellt werden.

## 5.8 Datenqualität und Aktualität

Jeder relevante Datensatz soll, soweit möglich, Quelle, Erfassungszeit,
Importzeit, Einheit und Qualitätsstatus besitzen. Kairos soll insbesondere
erkennen oder kennzeichnen:

- fehlende Zeitabschnitte;
- unrealistische Messspitzen;
- doppelte Aktivitäten;
- nachträglich veränderte Quelldaten;
- nicht synchronisierte oder veraltete Erholungswerte;
- unvollständige Wetter- oder Kalenderinformationen;
- widersprüchliche Werte verschiedener Quellen.

Eine Analyse muss sich mit der Änderung ihrer Datengrundlage nachvollziehbar
aktualisieren oder als historischer Stand erhalten bleiben.

## 5.9 Automatisierungsmodi

Kairos soll mindestens folgende fachliche Modi unterstützen:

1. **Manuell:** Kairos analysiert, nimmt aber keine Planänderung vor.
2. **Vorschlag:** Kairos erstellt eine begründete Änderung, die bestätigt werden
   muss.
3. **Begrenzt automatisch:** Kairos darf zuvor definierte Änderungstypen innerhalb
   festgelegter Grenzen durchführen.

Mögliche Grenzen sind betroffene Sportart, maximale Verschiebung, erlaubte
Intensitätsänderung, Ruhezeiten, Wettkampfnähe oder bestimmte Auslöser. Eine
unbegrenzte Vollautomatik ist nicht vorgesehen.

## 5.10 Fehler- und Ausnahmesituationen

Das System muss verständlich reagieren, wenn:

- eine Synchronisation fehlschlägt;
- eine Aktivität doppelt oder unvollständig ist;
- Messwerte fehlen oder offensichtlich unplausibel sind;
- kein belastbarer Vergleich existiert;
- ein externer Dienst nicht erreichbar ist;
- Kalender und Trainingsplan widersprüchliche Zeitzonen verwenden;
- der Automatikmodus eine Änderung außerhalb seiner Grenzen erfordern würde;
- eine Empfehlung wegen Beschwerden oder möglicher gesundheitlicher Risiken
  nicht verantwortbar ist.

In diesen Fällen soll Kairos keine Daten oder Gewissheit vortäuschen, sondern den
Status erklären und eine sichere nächste Handlung anbieten.

## 5.11 Festgelegte und spätere Entscheidungen

Der erste Import erfolgt über FIT-Dateien. Krafttraining benötigt neben manueller
Erfassung früh eine Integrationsmöglichkeit. Für Indoor-Rudern werden Concept2
und RP3 berücksichtigt. Die drei Automatisierungsmodi bilden die Ausgangsbasis.
Rückübertragung geplanter Trainings und weitere Ruderformen werden bei konkreter
Priorisierung entschieden.

## 5.12 Freigabestatus

Dieses Kapitel wurde inhaltlich mit dem Auftraggeber abgestimmt und fachlich
freigegeben.
