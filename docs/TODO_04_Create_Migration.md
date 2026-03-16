# TODO: Migration erstellen und ausführen

## Status: ⏳ Offen

## Ziel
Eine neue Migration für alle neuen Entities erstellen und auf die Datenbank anwenden.

## Voraussetzungen
- ✅ Datenbank läuft (Docker Container)
- ✅ Alle Entities erstellt
- ✅ Alle Konfigurationen erstellt
- ✅ AppDbContext aktualisiert

## Schritte

### 1. Migration erstellen

```bash
cd backend/PlayPlanner.Api
dotnet ef migrations add AddTeamPlayerPlayEntities
```

**Erwartetes Ergebnis:**
- Neue Migration-Datei in `PlayPlanner.Infrastructure/Persistence/Migrations/`
- Datei enthält `Up()` und `Down()` Methoden
- Tabellen: Teams, Players, Plays, PlayerPositions werden erstellt

### 2. Migration überprüfen

Öffne die generierte Migration-Datei und prüfe:
- [ ] `CreateTable` für Teams
- [ ] `CreateTable` für Players
- [ ] `CreateTable` für Plays
- [ ] `CreateTable` für PlayerPositions
- [ ] Foreign Keys sind korrekt
- [ ] Indizes sind vorhanden

### 3. Migration anwenden

```bash
dotnet ef database update
```

**Erwartetes Ergebnis:**
```
Applying migration '20260308XXXXXX_AddTeamPlayerPlayEntities'.
Done.
```

### 4. Datenbank überprüfen

```bash
docker exec -it playplanner-db psql -U playplanner -d playplanner_dev
```

In PostgreSQL:
```sql
\dt                    -- Alle Tabellen anzeigen
\d "Teams"            -- Teams-Struktur
\d "Players"          -- Players-Struktur
\d "Plays"            -- Plays-Struktur
\d "PlayerPositions"  -- PlayerPositions-Struktur
```

**Erwartete Tabellen:**
- Fields
- Teams
- Players
- Plays
- PlayerPositions
- __EFMigrationsHistory

## Troubleshooting

### Fehler: "Build failed"
```bash
cd backend/PlayPlanner.Api
dotnet build
```
→ Kompilierungsfehler beheben

### Fehler: "No DbContext was found"
→ Prüfe, ob `AppDbContextFactory.cs` korrekt ist

### Fehler: "Connection failed"
→ Prüfe, ob Docker-Container läuft:
```bash
cd infra
docker compose ps
```

## Checkliste
- [ ] Migration erstellt
- [ ] Migration-Datei überprüft
- [ ] Migration auf Datenbank angewendet
- [ ] Tabellen in Datenbank vorhanden
- [ ] Foreign Keys funktionieren

## Nächster Schritt
Nach Fertigstellung → `TODO_05_Create_DTOs.md`

