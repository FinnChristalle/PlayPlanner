# PlayPlanner – Anforderungen, Architektur & Setup

## 1. Projektziel & Überblick

PlayPlanner ist eine Webapplikation, mit der Nutzer **Abläufe/Workflows für ihre Teams** anlegen, speichern und verwalten können.

- **Frontend**: Angular SPA
- **Backend**: ASP.NET Core Web API
- **Datenbank**: PostgreSQL
- **Kommunikation**: REST (JSON)
- **Deployment-Ziel**: Container-basiert (Docker, Docker Compose, optional Cloud)

---

## 2. Fachliche Anforderungen

### 2.1 Kernfunktionen

- Workflows für Teams definieren:
  - Name, Beschreibung, zugehöriges Team
  - Schritte mit Reihenfolge, Verantwortlichen, optionaler Dauer/Deadline
- Workflows anlegen, bearbeiten, löschen
- Workflows einem Team zuordnen
- Anzeige:
  - Liste aller Workflows eines Teams
  - Detailansicht inkl. Schritte

### 2.2 Erweiterbare Anforderungen (optional)

- Versionierung von Workflows (Entwurf vs. aktiv)
- Templates/Standard-Workflows pro Team
- Rollen/Berechtigungen
- Fortschrittsverfolgung je Schritt

---

## 3. Systemarchitektur

### 3.1 Komponenten

- **Angular-Frontend**
  - SPA mit Routing und Feature-Modulen
  - Kommunikation mit Backend über `/api`-Endpunkte
- **.NET Web API**
  - REST-API, .NET 8+ empfohlen
  - Schichten: API, Application, Domain, Infrastructure
- **PostgreSQL**
  - Persistenz aller Domain-Entitäten
  - EF Core + Npgsql

### 3.2 Projektstruktur (Empfehlung)

```text
PlayPlanner/
  frontend/      # Angular-App
  backend/       # ASP.NET Core Web API
  infra/         # Docker, Compose, Deployment, IaC
  docs/          # Architektur, Anforderungen, ADRs
```

---

## 4. Entwicklungsumgebung

### 4.1 Voraussetzungen

- Node.js (LTS) + npm oder pnpm
- Angular CLI (`@angular/cli`)
- .NET SDK (LTS, z. B. 8.x)
- Docker Desktop
- Git

### 4.2 Angular-Frontend initialisieren (Beispiel)

```bash
cd frontend
ng new playplanner-frontend --routing --style=scss
```

### 4.3 .NET Web API initialisieren (Beispiel)

```bash
cd backend
dotnet new webapi -n PlayPlanner.Api
cd PlayPlanner.Api
```

### 4.4 Lokale Postgres-Datenbank via Docker (Dev)

```yaml
version: "3.9"
services:
  db:
    image: postgres:16
    environment:
      POSTGRES_USER: playplanner
      POSTGRES_PASSWORD: localdev
      POSTGRES_DB: playplanner_dev
    ports:
      - "5432:5432"
```

Start:

```bash
cd infra
docker compose up -d db
```

### 4.5 Backend ↔ Postgres (Dev, Beispiel)

`appsettings.Development.json` (Auszug):

```json
{
  "ConnectionStrings": {
    "Default": "Host=localhost;Port=5432;Database=playplanner_dev;Username=playplanner;Password=localdev"
  }
}
```

EF Core-Registrierung (Auszug):

```csharp
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));
```

Migrationen (wenn EF eingerichtet ist):

```bash
cd backend/PlayPlanner.Api
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 4.6 Lokales Development – Starten

Backend:

```bash
cd backend/PlayPlanner.Api
dotnet watch run
```

Frontend (mit Proxy):

```bash
cd frontend
ng serve --proxy-config proxy.conf.json
```

Beispiel `proxy.conf.json`:

```json
{
  "/api": {
    "target": "https://localhost:5001",
    "secure": false,
    "changeOrigin": true
  }
}
```

---

## 5. Konfiguration & Umgebungen

- Angular: `environment.ts` / `environment.prod.ts` mit `apiBaseUrl`
- .NET: `appsettings.*.json` für Dev/Prod (ConnectionStrings, Logging, CORS)
- Produktions-Secrets über Umgebungsvariablen/Secret-Store, nicht im Repo

---

## 6. Containerisierung & Deployment (Überblick)

- **Backend-Dockerfile** (Multi-Stage Build):
  - Build-Stage mit `dotnet/sdk`
  - `dotnet publish -c Release -o /app/publish`
  - Runtime-Stage mit `aspnet`-Image, `ENTRYPOINT ["dotnet", "PlayPlanner.Api.dll"]`
- **Frontend-Dockerfile**:
  - Build-Stage mit `node` und `ng build`
  - Runtime-Stage mit `nginx:alpine`, statische Dateien ausliefern, `/api` an Backend proxen
- **Docker Compose** (z. B. `infra/docker-compose.yml`):
  - Services: `frontend`, `api`, `db`
  - Ports, Volumes, Umgebungsvariablen definiert
- Einfaches Deployment: eine VM mit Docker Compose, Images aus Registry ziehen

---

## 7. Domänenmodell (Kurzüberblick)

- **Team**: `Id`, `Name`, Beschreibung
- **Workflow**: `Id`, `Name`, `Beschreibung`, `TeamId`, optional `Status`, `Version`
- **WorkflowStep**: `Id`, `WorkflowId`, `Titel`, `Beschreibung`, `Order`, optional Verantwortlicher, Dauer, Status

Typische Use-Cases:

- `CreateWorkflow(teamId, data)`
- `UpdateWorkflow(workflowId, data)`
- `DeleteWorkflow(workflowId)`
- `GetWorkflow(workflowId)`
- `ListWorkflowsByTeam(teamId)`

Diese Datei beschreibt die Anforderungen, Architektur und das grobe Setup für Entwicklung und Deployment von PlayPlanner.
