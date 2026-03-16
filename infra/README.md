# PlayPlanner Infrastruktur

## PostgreSQL Datenbank mit Docker

### Voraussetzungen

- Docker Desktop installiert und gestartet

### Datenbank starten

```bash
cd infra
docker compose up -d
```

### Datenbank stoppen

```bash
cd infra
docker compose down
```

### Datenbank stoppen und Daten löschen

```bash
cd infra
docker compose down -v
```

### Datenbank-Status prüfen

```bash
docker compose ps
```

### Datenbank-Logs anzeigen

```bash
docker compose logs db
```

### Verbindung zur Datenbank testen

```bash
docker exec -it playplanner-db psql -U playplanner -d playplanner_dev
```

## Datenbank-Konfiguration

- **Host:** localhost
- **Port:** 5432
- **Datenbank:** playplanner_dev
- **Benutzer:** playplanner
- **Passwort:** localdev

## Entity Framework Migrationen

Nach dem Start der Datenbank musst du die EF Core Migrationen ausführen:

```bash
cd backend/PlayPlanner.Api
dotnet ef database update
```

Falls noch keine Migrationen existieren:

```bash
cd backend/PlayPlanner.Api
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## Hinweise

- Die Datenbank-Daten werden in einem Docker Volume gespeichert und bleiben auch nach einem Neustart erhalten
- Um die Datenbank komplett zurückzusetzen, verwende `docker compose down -v`

