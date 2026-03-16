# PlayPlanner - Entwicklungs-Roadmap

## 📋 Übersicht

Dieses Dokument gibt einen Überblick über alle TODO-Schritte für die Entwicklung des PlayPlanner-Projekts.

## 🎯 Projektziel

Eine vollständige Webanwendung zur Planung und Verwaltung von American Football Spielzügen mit:
- Backend: ASP.NET Core Web API
- Frontend: Angular
- Datenbank: PostgreSQL (Docker)

## 📊 Aktueller Status

### ✅ Abgeschlossen
- Docker-Datenbank Setup
- Entity Framework Core Konfiguration
- Swagger Integration
- Field Entity und Migration
- Projekt-Struktur

### ⏳ In Arbeit
- Domain Models vervollständigen
- API Controllers implementieren

### 📝 Geplant
- Frontend-Entwicklung
- Testing
- Deployment

## 🗺️ Roadmap

### Phase 1: Backend-Grundlagen (JETZT)

| Nr | Dokument | Beschreibung | Status |
|----|----------|--------------|--------|
| 01 | [TODO_01_Domain_Entities.md](TODO_01_Domain_Entities.md) | Team, Player, Play, PlayerPosition Entities erstellen | ⏳ Offen |
| 02 | [TODO_02_Entity_Configurations.md](TODO_02_Entity_Configurations.md) | EF Core Konfigurationen für alle Entities | ⏳ Offen |
| 03 | [TODO_03_Update_DbContext.md](TODO_03_Update_DbContext.md) | AppDbContext mit neuen Entities erweitern | ⏳ Offen |
| 04 | [TODO_04_Create_Migration.md](TODO_04_Create_Migration.md) | Migration erstellen und auf DB anwenden | ⏳ Offen |
| 05 | [TODO_05_Create_DTOs.md](TODO_05_Create_DTOs.md) | DTOs für alle Entities erstellen | ⏳ Offen |
| 06 | [TODO_06_Create_Controllers.md](TODO_06_Create_Controllers.md) | REST-API Controllers implementieren | ⏳ Offen |
| 07 | [TODO_07_Test_With_Swagger.md](TODO_07_Test_With_Swagger.md) | Alle Endpoints mit Swagger testen | ⏳ Offen |

### Phase 2: Frontend-Entwicklung

| Nr | Dokument | Beschreibung | Status |
|----|----------|--------------|--------|
| 08 | [TODO_08_Frontend_Setup.md](TODO_08_Frontend_Setup.md) | Angular Services und Models einrichten | ⏳ Offen |
| 09 | TODO_09_Create_Components.md | Angular Komponenten erstellen | ⏳ Offen |
| 10 | TODO_10_Routing.md | Angular Routing konfigurieren | ⏳ Offen |
| 11 | TODO_11_Forms.md | Formulare für CRUD-Operationen | ⏳ Offen |

### Phase 3: Erweiterte Features

| Nr | Dokument | Beschreibung | Status |
|----|----------|--------------|--------|
| 12 | TODO_12_Play_Designer.md | Spielzug-Designer UI implementieren | ⏳ Offen |
| 13 | TODO_13_Authentication.md | Benutzer-Authentifizierung | ⏳ Offen |
| 14 | TODO_14_Authorization.md | Rollen und Berechtigungen | ⏳ Offen |

### Phase 4: Testing & Deployment

| Nr | Dokument | Beschreibung | Status |
|----|----------|--------------|--------|
| 15 | TODO_15_Unit_Tests.md | Unit Tests für Backend | ⏳ Offen |
| 16 | TODO_16_Integration_Tests.md | Integration Tests | ⏳ Offen |
| 17 | TODO_17_E2E_Tests.md | End-to-End Tests | ⏳ Offen |
| 18 | TODO_18_Docker_Compose.md | Vollständiges Docker Setup | ⏳ Offen |
| 19 | TODO_19_CI_CD.md | CI/CD Pipeline | ⏳ Offen |
| 20 | TODO_20_Deployment.md | Production Deployment | ⏳ Offen |

## 🚀 Schnellstart

### Aktuell empfohlener Workflow:

1. **Starte mit Phase 1** (Backend-Grundlagen)
2. **Arbeite die TODOs 01-07 nacheinander ab**
3. **Teste alles mit Swagger** bevor du zum Frontend gehst
4. **Dann Phase 2** (Frontend-Entwicklung)

### Befehle zum Starten:

```bash
# Datenbank starten
cd infra
docker compose up -d

# Backend starten
cd backend/PlayPlanner.Api
dotnet watch run

# Swagger öffnen
# Browser: http://localhost:5073
```

## 📚 Wichtige Dokumentation

- [Anforderungen.md](Anforderungen.md) - Projekt-Anforderungen
- [BaseAppClasses.md](BaseAppClasses.md) - Basis-Klassen Übersicht
- [MigrationsGuide.md](MigrationsGuide.md) - EF Migrations Guide
- [FieldImplementationGuide.md](FieldImplementationGuide.md) - Field Implementation
- [infra/README.md](../infra/README.md) - Datenbank Setup

## 🎯 Nächster Schritt

**Beginne mit:** [TODO_01_Domain_Entities.md](TODO_01_Domain_Entities.md)

Erstelle die fehlenden Domain Entities (Team, Player, Play, PlayerPosition).

## 💡 Tipps

- ✅ Arbeite die TODOs **in der Reihenfolge** ab
- ✅ Teste jeden Schritt **bevor** du zum nächsten gehst
- ✅ Nutze **Swagger** zum Testen der API
- ✅ Committe regelmäßig in Git
- ✅ Dokumentiere Änderungen

## 📞 Hilfe

Bei Problemen:
1. Prüfe die Logs (Backend-Konsole, Browser DevTools)
2. Prüfe ob Datenbank läuft: `docker compose ps`
3. Prüfe die entsprechende TODO-Datei für Details
4. Schaue in die bestehende Dokumentation

---

**Viel Erfolg! 🏈**

