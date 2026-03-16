# Domain-Klassen für Spielzugplanung – sinnvolle Properties

Dieses Dokument ergänzt `BaseAppClasses.md` und beschreibt, **welche Properties** der wichtigsten Domain-Klassen für das Planen und Abspielen von Spielzügen besonders nützlich sind.

Fokus: Field, Team, Player, Play.

Nicht alles davon musst du in Version 1 implementieren – markiere dir, was Pflicht ist und was optional/später kommen kann.

---

## 1. Field (Spielfeld)

`Field` ist in `FieldDesign.md` schon detailliert. Hier nur die wichtigsten Punkte im Kontext der Spielzugplanung:

**Pflicht (für v1):**
- `Id`
- `Name`
- `SportType` (Basketball, Football, Handball, ...)
- `WidthMeters`, `HeightMeters`
- `MetaJson` (für Linien/Zonen, mindestens vorbereitet)

**Optional/nützlich:**
- `DefaultPlayerCount` (z. B. 5, 7, 11) – hilft dem Editor, Standardaufstellungen zu planen.
- `Orientation` (z. B. Angriff nach oben/unten) – für konsistente Darstellung von Plays.
- `RuleSet` (z. B. "FIBA-2010", "NBA-2023") – später nutzbar für regelabhängige Layouts.
- `CreatedAt`, `UpdatedAt`, `IsActive` – Audit und Versionierung von Felddefinitionen.

Für die Spielzugplanung ist vor allem wichtig, dass **jede Position auf dem Feld eindeutig in das 0..1-Koordinatensystem abbildbar** ist.

---

## 2. Team

Ein `Team` liefert den Kontext, für wen ein Play gedacht ist.

**Pflicht (v1):**
- `Id`
- `Name`
- `SportType`
- `DefaultFieldId` (optional Pflicht – sehr sinnvoll, damit Plays ohne Feld-Auswahl geplant werden können)

**Optional/nützlich für Play-Planung:**
- `AgeGroup` (z. B. U12, U16, Erwachsene)
  - Hilft, Plays nach Altersklasse zu filtern und passende Komplexität zu wählen.
- `Level` (z. B. Hobby, Amateur, Semi-Pro, Pro)
  - Dient später zur Filterung von Plays (einfache vs. komplexe Abläufe).
- `PrimaryColor`, `SecondaryColor`
  - Optische Darstellung im PlayViewer (Trikots, Legende).
- `CoachName`, `CoachNotes`
  - Kontext für denjenigen, der Plays erstellt/verwendet.
- `RosterSize` oder `MaxPlayers`
  - Kann für Validierungen im Editor genutzt werden.

Für die Planung von Spielzügen reicht v1: Name, SportType, DefaultFieldId – der Rest erhöht den Komfort und die Präzision bei der Suche/Filterung von Plays.

---

## 3. Player

Ein `Player` repräsentiert eine Person im Team, die in Plays eingesetzt wird.

**Pflicht (v1):**
- `Id`
- `TeamId`
- `FirstName`, `LastName` (oder ein kombinierter `Name`)
- `JerseyNumber`

**Optional/nützlich für Spielzugplanung:**
- `Position`
  - Freitext oder sportartspezifisches Enum (z. B. PG/SG/SF/PF/C im Basketball, ST/MF/DF/GK im Fußball).
  - Wichtig, um Plays auf "Rollen" statt auf konkrete Spieler zu konzipieren.
- `Role` / `RoleType`
  - Grobe Rolle wie Offense, Defense, Special Teams, Allround.
- `PreferredSide`
  - Links, Rechts, Mitte – nützlich für Flügel-/Seiten-spezifische Plays.
- `DominantFoot` / `DominantHand`
  - Insbesondere bei Fußball/Basketball interessant (Rechts-/Linkshänder/Füßer).
- `Height`, `Weight` (optionale physische Daten)
  - Nicht zwingend für v1, aber später hilfreich für Matchups.
- `IsStarter`
  - Kennzeichnet Standard-Startaufstellung (wichtig für Standard-Plays).
- `DefaultOrderInLineup`
  - Reihenfolge in Standardaufstellungen (1–5, 1–11, ...).
- `SkillTags`
  - Liste von Tags wie "Shooter", "Passer", "Cutter", "Defender", "Fast" – nützlich, um Plays für bestimmte Spielerprofile vorzuschlagen.

Wichtig: Für die v1-Playplanung reicht meist eine Zuordnung zu Rollen/Positionen. SkillTags und physische Daten sind eher Vorbereitung für spätere, intelligente Vorschläge.

---

## 4. Play (Spielzug)

`Play` ist das Herzstück der Anwendung: ein geplanter Ablauf, der später mit einer Timeline/Engine abgespielt wird.

**Pflicht (v1):**
- `Id`
- `TeamId`
- `FieldId`
- `Name`
- `Description` (kurze Erklärung)
- `DurationSeconds`
- `CreatedAt`, `CreatedByUserId` (mindestens CreatedAt für Nachvollziehbarkeit)

**Optional/nützlich für Planung und Organisation:**
- `PlayStatus`
  - z. B. Draft, Active, Archived.
  - Hilft, unfertige Entwürfe von produktiv genutzten Plays zu trennen.
- `Category`
  - z. B. Offense, Defense, Transition, Special Situation.
- `SituationContext`
  - Freitext oder Enum für Spielsituationen: Inbound Baseline, Inbound Sideline, Fastbreak, Freistoß, Ecke, Einwurf etc.
- `Difficulty`
  - Einfache Skala (1–5 oder Enum: Beginner, Intermediate, Advanced).
- `AgeGroupRange`
  - Min/Max oder Liste, für welche Altersstufen der Play geeignet ist.
- `Tags`
  - Liste von Stichworten (z. B. "PickAndRoll", "ZoneOffense", "PressBreak").
- `FormationName`
  - Name der Ausgangsformation (z. B. "4-Out 1-In", "3-4-3").
- `IsPublic` / `Visibility`
  - Ob Play nur intern im Team oder auch in einer gemeinsamen Bibliothek sichtbar ist.
- `BasePlayId`
  - Referenz auf einen anderen Play, falls es sich um eine Variation handelt.
- `UsageCount` / `LastUsedAt`
  - Statistik, wie oft ein Play verwendet wurde.

**Bezug zur Timeline (später):**
- In v1 muss `Play` noch keine Keyframes/Events direkt enthalten.
- Langfristig wird `Play` mit einer `PlayTimeline`/`PlayDefinition` verknüpft, die Tracks und Events enthält.
- Diese eigentliche Ablauf-Struktur kann in einer eigenen Entität oder als JSON (z. B. `TimelineJson`) modelliert werden.

---

## 5. Implementierungshinweise (ohne Code)

1. **Pflicht vs. Optional markieren**
   - Implementiere in der ersten Version nur die Pflicht-Eigenschaften, die du wirklich für den ersten End-to-End-Flow brauchst.
   - Dokumentiere in den Klassen-Kommentaren, welche Properties später hinzukommen sollen.

2. **Enums vs. Strings**
   - Für Dinge wie `SportType`, `PlayStatus`, `Category` kann ein Enum helfen, Fehler zu vermeiden.
   - Überlege aber, wie häufig du neue Werte hinzufügen musst (Enums erfordern Code-Deploy, Strings sind flexibler, aber weniger typsicher).

3. **Tags und komplexe Eigenschaften**
   - Für `Tags` oder `SkillTags` kannst du in v1 einfache Strings oder kommaseparierte Listen verwenden.
   - Später kannst du das auf eigene Tabellen (Tag-Entitäten mit Many-to-Many-Beziehungen) ausbauen.

4. **Audit-Felder zentralisieren**
   - Überlege, ob du eine Basisklasse oder ein Interface mit `CreatedAt`, `UpdatedAt`, `IsActive` definierst.
   - So bleibst du konsistent über alle Entitäten hinweg.

5. **DB-Schema inkrementell erweitern**
   - Füge nicht sofort alle optionalen Eigenschaften ins DB-Schema ein.
   - Nutze EF-Migrationen (siehe `MigrationsGuide.md`), um nach Bedarf neue Spalten hinzu zu fügen, wenn du sie wirklich verwendest.

Diese Überlegungen sollen dir helfen, deine Domain-Klassen so zu modellieren, dass sie Spielzugplanung und -visualisierung gut unterstützen, ohne am Anfang zu komplex zu werden.

