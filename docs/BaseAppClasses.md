# Basis-Klassen und Bausteine für PlayPlanner

Dieses Dokument listet die wichtigsten Klassen (bzw. Typen) auf, die du für die **erste Version** von PlayPlanner anlegen solltest. Es ist in Schichten gegliedert (Domain, Application, Infrastructure, API, Frontend), damit du strukturiert vorgehen kannst.

---

## 1. Domain-Schicht (`PlayPlanner.Domain`)

Fachliche Modelle ohne Technik-Abhängigkeiten.

### 1.1 Kern-Entitäten

1. `Field`
   - Beschreibt ein Spielfeld (Geometrie, Sportart, Layout-Metadaten).
2. `Team`
   - Ein Sportteam, das Plays ausführt (Name, Sportart, optional DefaultField).
3. `Player`
   - Spieler eines Teams (Name, Nummer, Rolle/Position).
4. `Play`
   - Geplanter Spielzug mit Bezug auf ein Team und ein Feld (Name, Beschreibung, Dauer).

### 1.2 Unterstützende Typen

5. `SportType`
   - Aufzählung/Typ für Sportarten (Basketball, Football, Handball, etc.).
6. `PlayStatus` (optional, später)
   - Status eines Plays (Entwurf, aktiv, archiviert).
7. Audit-Basis (optional)
   - Gemeinsame Basisklasse oder Interface für `CreatedAt`, `UpdatedAt`, `IsActive`.

---

## 2. Application-Schicht (`PlayPlanner.Application`)

Use Cases und Anwendungslogik, arbeitet mit Domain-Typen.

### 2.1 Use-Case-Klassen (Commands/Queries)

1. `CreateFieldCommand` / Handler
2. `GetFieldsQuery` / Handler
3. `GetFieldByIdQuery` / Handler
4. `CreatePlayCommand` / Handler
5. `GetPlaysForTeamQuery` / Handler

(Diese kannst du als einzelne Klassen oder gruppiert nach Funktionalität anlegen.)

### 2.2 Schnittstellen

6. `IFieldRepository`
   - CRUD-Operationen für Fields.
7. `ITeamRepository`
   - CRUD-Operationen für Teams.
8. `IPlayerRepository`
   - CRUD-Operationen für Spieler.
9. `IPlayRepository`
   - CRUD-Operationen für Plays.

---

## 3. Infrastructure-Schicht (`PlayPlanner.Infrastructure`)

Technische Implementierung: EF Core, Datenbank, Repositories.

### 3.1 Persistenz-Grundlagen

1. `AppDbContext`
   - EF Core DbContext mit DbSets für Field, Team, Player, Play.
2. Konfigurationsklassen (Fluent API)
   - `FieldConfiguration`
   - `TeamConfiguration`
   - `PlayerConfiguration`
   - `PlayConfiguration`

### 3.2 Repository-Implementierungen

3. `FieldRepository` (implementiert `IFieldRepository`)
4. `TeamRepository` (implementiert `ITeamRepository`)
5. `PlayerRepository` (implementiert `IPlayerRepository`)
6. `PlayRepository` (implementiert `IPlayRepository`)

---

## 4. API-Schicht (`PlayPlanner.Api`)

HTTP-Endpoint-Schicht, DTOs und Controller.

### 4.1 DTOs (Requests/Responses)

1. Fields
   - `FieldDto`
   - `CreateFieldRequest`
   - `UpdateFieldRequest` (optional)

2. Teams
   - `TeamDto`
   - `CreateTeamRequest`

3. Players
   - `PlayerDto`
   - `CreatePlayerRequest`

4. Plays
   - `PlayDto`
   - `CreatePlayRequest`
   - `UpdatePlayRequest` (optional)

### 4.2 Controller

5. `FieldsController`
   - Endpunkte zum Verwalten von Feldern.
6. `TeamsController`
   - Endpunkte zum Verwalten von Teams.
7. `PlayersController`
   - Endpunkte zum Verwalten von Spielern.
8. `PlaysController`
   - Endpunkte zum Verwalten von Plays.

### 4.3 Mappings

9. Mapping-Hilfsklassen oder Extension-Methoden
   - Domain-Entitäten ↔ DTOs (z. B. `Field` ↔ `FieldDto`).

---

## 5. PlaySystem-Modul (`PlayPlanner.PlaySystem`)

Hier liegt die spezialisierte Logik für Spielzüge und Animation.

### 5.1 Kern-Typen für den Ablauf

1. `Actor`
   - Abstrakter Akteur (Spieler, Ball).
2. `ActorTrack`
   - Beschreibt Keyframes/Positionen eines Actors über die Zeit.
3. `PlayEvent`
   - Ereignisse wie Pass, Wurf, Screen (Zeit, beteiligte Actoren, Typ).
4. `PlayTimeline`
   - Sammlung von Tracks und Events für ein Play.

### 5.2 Engine/Services

5. `IPlayEngine`
   - Schnittstelle, um aus einer Timeline den Zustand zu einem Zeitpunkt t zu berechnen.
6. `PlayEngine` (Implementierung)
   - Interpolationslogik für Positionen, Auswertung von Events.

Diese Klassen müssen nicht alle sofort implementiert werden, du kannst mit einem minimalen Set starten (z. B. nur `ActorTrack` und eine einfache Interpolation).

---

## 6. Frontend (Angular) – grobe Bausteine

Hier nur die wichtigsten Feature-Module und Services, die zu den Backend-Klassen passen.

### 6.1 Feature-Module

1. `TeamsModule`
   - Komponenten für Team-Liste, Team-Details.
2. `PlayersModule`
   - Komponenten für Spieler-Liste, Spieler-Details.
3. `PlaysModule`
   - Komponenten für Play-Liste, Play-Details, einfaches Anlegen.
4. `PlayViewerModule`
   - Komponenten für Spielfeld-Ansicht, Timeline-Steuerung.

### 6.2 Services (API-Clients)

5. `FieldsApiService`
6. `TeamsApiService`
7. `PlayersApiService`
8. `PlaysApiService`

Diese sollten die HTTP-Aufrufe zu den jeweiligen Controllern kapseln.

---

## 7. Empfohlene Reihenfolge beim Anlegen

1. Domain: `Field`, `Team`, `Player`, `Play`, `SportType`.
2. Infrastructure: `AppDbContext`, `FieldConfiguration`, erste Migration.
3. API: `FieldDto`, `CreateFieldRequest`, `FieldsController`.
4. Application: einfache Use-Cases und Repositories für Fields.
5. Danach Schritt für Schritt: Teams, Players, Plays, dann PlaySystem und Frontend-Module.

