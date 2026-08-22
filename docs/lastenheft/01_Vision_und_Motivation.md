# 1. Vision und Motivation

## 1.1 Zweck dieses Kapitels

Dieses Kapitel beschreibt, warum Kairos entwickelt werden soll, welches
grundlegende Problem das Produkt löst und welches langfristige Zielbild verfolgt
wird. Es legt die fachliche Leitidee fest, an der spätere Anforderungen,
Priorisierungen und Produktentscheidungen gemessen werden.

Das Kapitel beschreibt bewusst noch keine konkrete technische Umsetzung. Die
Auswahl von Technologien, Systemkomponenten und Schnittstellen ist Gegenstand des
späteren Pflichtenhefts.

## 1.2 Ausgangssituation

Ambitionierte Ausdauersportler erfassen heute eine große Menge an Trainings- und
Gesundheitsdaten. Fahrradcomputer, Sportuhren, Leistungsmesser und Plattformen
wie Garmin Connect oder Strava liefern unter anderem Herzfrequenz, Leistung,
Geschwindigkeit, Trittfrequenz, Trainingsbelastung, Schlaf, HRV und
Erholungswerte.

Die bloße Verfügbarkeit dieser Daten führt jedoch nicht automatisch zu besseren
Trainingsentscheidungen. Viele Systeme zeigen Kennzahlen und Diagramme, lassen
den Sportler aber mit den entscheidenden Fragen weitgehend allein:

- Warum war eine bestimmte Einheit besser oder schlechter als eine vergleichbare
  Einheit?
- Wurden Intervalle in der richtigen Intensität und mit ausreichender Qualität
  durchgeführt?
- Welche konkrete Anpassung ist für die nächste Einheit sinnvoll?
- Passt der aktuelle Trainingsverlauf zu einem langfristigen Leistungs- oder
  Wettkampfziel?
- Sollte ein geplantes Training wegen unzureichender Erholung, Krankheit,
  Terminkonflikten oder Wetterbedingungen verändert werden?
- Welche Folgen hat eine heutige Entscheidung voraussichtlich für die kommenden
  Wochen und Monate?

Professionelles Coaching kann diese Lücke schließen, ist jedoch nicht für jeden
Sportler dauerhaft verfügbar oder finanzierbar. Statische Trainingspläne
berücksichtigen den tatsächlichen Tageszustand, kurzfristige Änderungen und die
individuelle Reaktion auf vergangene Belastungen nur eingeschränkt.

## 1.3 Problemstellung

Das zentrale Problem besteht nicht in einem Mangel an Daten, sondern in einem
Mangel an verständlicher, individueller und handlungsorientierter Interpretation.

Die relevanten Informationen sind häufig:

- auf mehrere Plattformen verteilt;
- nur als isolierte Kennzahlen verfügbar;
- ohne Bezug zu langfristigen Zielen dargestellt;
- nicht mit Alltag, Kalender und Umweltbedingungen verbunden;
- rückblickend statt vorausschauend aufbereitet;
- nicht ausreichend begründet oder personalisiert.

Dadurch muss der Sportler selbst aus komplexen und teilweise widersprüchlichen
Signalen eine Trainingsentscheidung ableiten. Fehlinterpretationen können zu
ineffektivem Training, Überlastung, unzureichender Erholung, Motivationsverlust
oder einem Verfehlen langfristiger Ziele führen.

## 1.4 Produktvision

Kairos soll ein persönlicher, datenbasierter und erklärbarer KI-Ausdauercoach für
mehrere Ausdauersportarten werden. Das Produkt soll Trainingsdaten nicht nur
darstellen, sondern den Sportler fortlaufend dabei unterstützen, im richtigen
Moment die richtige Entscheidung zu treffen.

> **Kairos hilft Ausdauersportlern, langfristige Ziele durch individuell
> abgestimmte, nachvollziehbare und anpassungsfähige Trainingsentscheidungen zu
> erreichen.**

Der Name „Kairos“ bezeichnet den richtigen oder günstigen Zeitpunkt. Diese
Bedeutung bildet den Kern der Produktidee: Nicht allein die Menge des Trainings
entscheidet, sondern die passende Belastung zur passenden Zeit unter
Berücksichtigung des individuellen Zustands und des angestrebten Ziels.

Kairos soll daher langfristig in der Lage sein:

1. den aktuellen Zustand des Sportlers aus verfügbaren Daten abzuleiten;
2. absolvierte Einheiten fachlich zu analysieren;
3. Entwicklungen und wiederkehrende Muster zu erkennen;
4. den Fortschritt zu messbaren Zielen zu bewerten;
5. geeignete nächste Schritte vorzuschlagen;
6. bestehende Pläne bei relevanten Änderungen anzupassen;
7. jede wesentliche Empfehlung verständlich zu begründen;
8. aus dem Verlauf und dem Feedback des Sportlers zu lernen;
9. Trainingspläne selbstständig anzupassen, wenn der Nutzer diese Form der
   Automatisierung ausdrücklich aktiviert hat.

## 1.5 Leitbild: vom Datenspeicher zum aktiven Coach

Kairos soll ausdrücklich kein weiteres reines Trainingsdashboard sein. Die
Darstellung von Aktivitäten, Kurven und Kennzahlen ist notwendig, aber nicht der
eigentliche Produktzweck.

Der angestrebte Mehrwert entsteht in vier aufeinander aufbauenden Ebenen:

### Ebene 1 – Beobachten

Kairos führt relevante Trainings-, Gesundheits-, Ziel- und Kontextinformationen
zusammen und macht deren Herkunft und Aktualität sichtbar.

### Ebene 2 – Verstehen

Kairos interpretiert einzelne Einheiten und längerfristige Entwicklungen. Das
System soll beispielsweise erkennen, ob Intervalle gleichmäßig durchgeführt
wurden, ob die physiologische Reaktion von früheren Einheiten abweicht oder ob
sich Anzeichen kumulierter Ermüdung zeigen.

### Ebene 3 – Empfehlen

Kairos übersetzt die Analyse in eine konkrete und priorisierte Handlung. Eine
Empfehlung soll nicht nur „heute locker trainieren“ lauten, sondern erklären,
welche Beobachtungen zu dieser Empfehlung geführt haben.

### Ebene 4 – Anpassen

Kairos soll Trainingsplanung als fortlaufenden Prozess verstehen. Ein
ausgelassenes Training, schlechte Erholung, Krankheit, ein Termin oder ungeeignete
Wetterbedingungen können eine Anpassung auslösen. Nutzer können wählen, ob Kairos
Änderungen lediglich vorschlägt oder innerhalb festgelegter Grenzen selbstständig
vornimmt. Automatische Änderungen müssen transparent, nachvollziehbar und
rückgängig zu machen sein.

## 1.6 Langfristige Zielorientierung

Kairos soll sowohl kurzfristige Trainingsentscheidungen als auch mehrjährige
Ziele unterstützen. Nutzer sollen konkrete, terminierte Ziele erfassen können,
zum Beispiel:

- eine FTP von 400 Watt bis 2027;
- eine VO2max von 65 bis 2028;
- die erfolgreiche Teilnahme an einem mehrjährigen Ausdauerwettkampfziel.

Ein Ziel soll nicht lediglich als Text oder Endwert gespeichert werden. Kairos
soll den Weg zum Ziel strukturiert begleiten und mindestens folgende Fragen
beantworten können:

- Wie ist der aktuelle Fortschritt?
- Ist der zeitliche Verlauf realistisch?
- Welche Teilziele sind erforderlich?
- Welche Faktoren fördern oder gefährden das Ziel?
- Wie sicher ist eine Prognose und auf welchen Annahmen beruht sie?
- Welche nächste Maßnahme besitzt aktuell den größten erwarteten Nutzen?

Dabei muss das System zwischen gemessenen Fakten, berechneten Kennzahlen,
Prognosen und KI-generierten Einschätzungen klar unterscheiden.

## 1.7 Erklärbarkeit als Grundprinzip

Kairos soll keine Blackbox sein. Empfehlungen müssen für den Nutzer prüfbar und
verständlich sein. Das System soll bei wesentlichen Aussagen offenlegen:

- welche Daten verwendet wurden;
- aus welchem Zeitraum die Daten stammen;
- welche Veränderungen oder Muster erkannt wurden;
- welche Unsicherheiten oder fehlenden Daten bestehen;
- weshalb eine bestimmte Empfehlung ausgesprochen wird;
- welche Alternativen grundsätzlich möglich wären.

Beispiel einer angestrebten Erklärung:

> Die geplante Intervalleinheit wird auf morgen verschoben, weil deine HRV unter
> deinem individuellen Referenzbereich liegt, du deutlich weniger als üblich
> geschlafen hast und die Belastung der vergangenen Tage erhöht war. Für heute
> wird eine lockere Einheit empfohlen.

Die konkrete Formulierung und die verwendeten Schwellenwerte sind später
fachlich und technisch zu spezifizieren. Entscheidend ist an dieser Stelle das
Prinzip, dass Empfehlungen nicht ohne nachvollziehbare Begründung erfolgen.

## 1.8 Individualisierung und lernendes Athletenprofil

Allgemeine Trainingsregeln reichen für eine langfristige persönliche Begleitung
nicht aus. Kairos soll deshalb ein fortlaufendes Athletenprofil aufbauen, das
individuelle Reaktionen und Rahmenbedingungen berücksichtigt.

Dazu können langfristig beispielsweise gehören:

- Reaktionen auf bestimmte Trainingsformen;
- typische Erholungsdauer nach hoher Belastung;
- persönliche Leistungs- und Herzfrequenzbereiche;
- verfügbare Trainingszeiten und wiederkehrende Termine;
- subjektives Belastungs- und Erholungsempfinden;
- Krankheiten, Verletzungen, Reisen und Trainingspausen;
- bevorzugte Strecken, Sportarten und Trainingsumgebungen;
- Rückmeldungen zu früheren Empfehlungen.

Das lernende Profil darf nicht dazu führen, dass Annahmen unbemerkt als Fakten
behandelt werden. Nutzer müssen relevante gespeicherte Informationen einsehen,
korrigieren und löschen können.

## 1.9 Angestrebte Nutzererfahrung

Die Nutzung von Kairos soll sich wie die Zusammenarbeit mit einem aufmerksamen
Coach anfühlen. Das System soll komplexe Analysen im Hintergrund durchführen,
die Ergebnisse jedoch klar, ruhig und handlungsorientiert präsentieren.

Die Nutzererfahrung soll insbesondere folgende Eigenschaften besitzen:

- **verständlich:** Fachbegriffe werden erklärt und Aussagen eindeutig
  formuliert;
- **konkret:** Empfehlungen enthalten einen umsetzbaren nächsten Schritt;
- **persönlich:** Aussagen beziehen sich auf den individuellen Verlauf;
- **transparent:** Datenbasis, Unsicherheit und Begründung bleiben sichtbar;
- **konsistent:** Empfehlungen widersprechen einander nicht ohne Erklärung;
- **kontrollierbar:** Der Nutzer behält die Entscheidungshoheit;
- **motivierend:** Fortschritt wird sichtbar, ohne Risiken oder Unsicherheit zu
  beschönigen.

Eine später konfigurierbare Coach-Persönlichkeit kann Ton und Detailgrad anpassen.
Sie darf jedoch keine fachlichen Aussagen oder Sicherheitsgrenzen verändern.

## 1.10 Produktabgrenzung auf Visionsebene

Kairos soll auf Visionsebene nicht als Ersatz für folgende Leistungen verstanden
werden:

- medizinische Diagnose oder Behandlung;
- Notfall- oder Gesundheitsüberwachung;
- physiotherapeutische oder ernährungsmedizinische Beratung;
- garantierte Leistungsprognosen;
- vollständig autonome Entscheidungen ohne Zustimmung des Nutzers.

Das Produkt unterstützt Trainingsentscheidungen. Es muss Grenzen der verfügbaren
Daten und der eigenen Aussagekraft deutlich machen. Bei erkennbaren gesundheitlich
kritischen Konstellationen soll es keine vermeintlich medizinische Sicherheit
vermitteln, sondern auf professionelle Abklärung verweisen.

## 1.11 Erwarteter Nutzen

Für den Sportler soll Kairos folgenden Nutzen schaffen:

- bessere Einordnung absolvierter Einheiten;
- verständliche und konkrete Verbesserungsvorschläge;
- realistischere Trainingsplanung im Zusammenspiel mit dem Alltag;
- frühzeitiges Erkennen ungünstiger Entwicklungen;
- nachvollziehbare Verbindung zwischen täglichem Training und langfristigen
  Zielen;
- weniger manuelle Zusammenführung verteilter Informationen;
- bessere Grundlage für eigenverantwortliche Entscheidungen;
- langfristig höhere Trainingsqualität und Zieltreue.

Für die Produktentwicklung liefert diese Vision einen klaren Prüfmaßstab: Eine
Funktion besitzt nur dann hohen Wert, wenn sie dem Sportler hilft, seinen Zustand
besser zu verstehen, eine bessere Entscheidung zu treffen oder den Weg zu einem
Ziel verlässlicher zu steuern.

## 1.12 Visionäre Erfolgskriterien

Die Vision gilt langfristig als erreicht, wenn Nutzer Kairos nicht primär wegen
der Menge angezeigter Daten verwenden, sondern weil das Produkt:

- relevante Veränderungen zuverlässig erkennt;
- Empfehlungen nachvollziehbar aus individuellen Daten ableitet;
- Pläne sinnvoll an veränderte Bedingungen anpasst;
- Fortschritt und Risiken gegenüber langfristigen Zielen verständlich einordnet;
- Vertrauen durch Transparenz, Kontrolle und fachliche Konsistenz aufbaut;
- dauerhaft einen erkennbaren Mehrwert gegenüber isolierten Plattformdaten und
  statischen Trainingsplänen bietet.

Messbare Produkt- und Qualitätskennzahlen werden in späteren Kapiteln definiert.

## 1.13 Festgelegte Leitentscheidungen

Für die weitere Ausarbeitung gelten folgende Entscheidungen:

1. Kairos richtet sich in der Produktvision an mehrere Ausdauersportarten. Ein
   engerer MVP-Fokus kann in der Roadmap dennoch festgelegt werden.
2. Kairos darf Trainingspläne selbstständig verändern, wenn der Nutzer diese
   Funktion ausdrücklich aktiviert hat.
3. Triathlon und Ironman sind mögliche Anwendungsfälle, aber kein bestimmender
   Bestandteil der Kernvision.
4. Kairos wird klar von medizinischer Diagnose und Behandlung abgegrenzt.
5. Neben den bereits beschriebenen Beweggründen wird derzeit kein weiterer
   persönlicher Hauptbeweggrund aufgenommen.

## 1.14 Freigabestatus

Dieses Kapitel wurde inhaltlich mit dem Auftraggeber abgestimmt und fachlich
freigegeben.
