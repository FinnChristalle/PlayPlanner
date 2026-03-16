# PlayPlanner - Vollständige Projekt-Roadmap

## 📊 Projekt-Status Dashboard

**Aktueller Stand:** Phase 1 - Backend-Grundlagen  
**Fortschritt:** 20% (Setup abgeschlossen)  
**Nächster Meilenstein:** CRUD-API fertigstellen

---

## 🎯 Projekt-Übersicht

### Technologie-Stack
- **Backend:** ASP.NET Core 10.0 Web API
- **Frontend:** Angular 19+ (Standalone Components)
- **Datenbank:** PostgreSQL 16
- **ORM:** Entity Framework Core 10.0
- **Container:** Docker & Docker Compose
- **API-Dokumentation:** Swagger/OpenAPI

### Architektur
```
┌─────────────────────────────────────────────────────┐
│                   Angular Frontend                   │
│              (http://localhost:4200)                 │
└──────────────────────┬──────────────────────────────┘
                       │ HTTP/REST
┌──────────────────────▼──────────────────────────────┐
│              ASP.NET Core Web API                    │
│              (http://localhost:5073)                 │
│  ┌─────────────────────────────────────────────┐   │
│  │ Controllers → Application → Domain          │   │
│  │                    ↓                         │   │
│  │              Infrastructure                  │   │
│  └─────────────────────┬───────────────────────┘   │
└────────────────────────┼───────────────────────────┘
                         │ EF Core
┌────────────────────────▼───────────────────────────┐
│         PostgreSQL Database (Docker)                │
│              (localhost:5432)                       │
└─────────────────────────────────────────────────────┘
```

---

## 📅 Phase 1: Backend-Grundlagen (Woche 1-2)

### ✅ 1.1 Projekt-Setup (ABGESCHLOSSEN)
- [x] Solution-Struktur erstellen
- [x] Projekte anlegen (Api, Application, Domain, Infrastructure)
- [x] NuGet-Pakete installieren
- [x] Git-Repository initialisieren

### ✅ 1.2 Datenbank-Setup (ABGESCHLOSSEN)
- [x] Docker Compose für PostgreSQL
- [x] Connection String konfigurieren
- [x] AppDbContext erstellen
- [x] AppDbContextFactory für Migrations

### ✅ 1.3 Erste Entity & Migration (ABGESCHLOSSEN)
- [x] Field Entity erstellen
- [x] FieldConfiguration (Fluent API)
- [x] Migration erstellen und anwenden
- [x] Datenbank-Tabelle verifizieren

### ✅ 1.4 API-Grundlagen (ABGESCHLOSSEN)
- [x] Swagger/OpenAPI konfigurieren
- [x] Program.cs einrichten
- [x] Dependency Injection konfigurieren

### ⏳ 1.5 Domain Models vervollständigen
- [ ] Team Entity erstellen
- [ ] Player Entity erstellen
- [ ] Play Entity erstellen
- [ ] PlayerPosition Entity erstellen
- [ ] Alle Configurations erstellen
- [ ] AppDbContext aktualisieren
- [ ] Migration erstellen und anwenden

**Dokument:** `TODO_01_Domain_Entities.md` bis `TODO_04_Create_Migration.md`

### ⏳ 1.6 DTOs & Mapping
- [ ] FieldDto, CreateFieldDto, UpdateFieldDto
- [ ] TeamDto, CreateTeamDto, UpdateTeamDto
- [ ] PlayerDto, CreatePlayerDto, UpdatePlayerDto
- [ ] PlayDto, CreatePlayDto, UpdatePlayDto
- [ ] Validierungs-Attribute hinzufügen

**Dokument:** `TODO_05_Create_DTOs.md`

### ⏳ 1.7 API Controllers
- [ ] FieldsController (CRUD)
- [ ] TeamsController (CRUD + Relations)
- [ ] PlayersController (CRUD)
- [ ] PlaysController (CRUD)
- [ ] Error Handling implementieren
- [ ] Logging hinzufügen

**Dokument:** `TODO_06_Create_Controllers.md`

### ⏳ 1.8 API Testing
- [ ] Alle Endpoints mit Swagger testen
- [ ] CRUD-Operationen verifizieren
- [ ] Validierung testen
- [ ] Fehlerbehandlung testen
- [ ] Testdaten erstellen

**Dokument:** `TODO_07_Test_With_Swagger.md`

**Meilenstein 1:** ✅ Vollständige, getestete REST-API

---

## 📅 Phase 2: Frontend-Grundlagen (Woche 3-4)

### 2.1 Angular Setup
- [ ] Environment-Konfiguration (dev/prod)
- [ ] HttpClient konfigurieren
- [ ] CORS im Backend aktivieren
- [ ] Proxy-Konfiguration (optional)

### 2.2 Services & Models
- [ ] TypeScript Interfaces (Field, Team, Player, Play)
- [ ] ApiService (Generic HTTP Service)
- [ ] FieldService
- [ ] TeamService
- [ ] PlayerService
- [ ] PlayService
- [ ] Error Handling Service

**Dokument:** `TODO_08_Frontend_Setup.md`

### 2.3 Routing & Navigation
- [ ] App Routes definieren
- [ ] Navigation Component
- [ ] Route Guards (später für Auth)
- [ ] 404 Page

### 2.4 Shared Components
- [ ] Loading Spinner
- [ ] Error Message Component
- [ ] Confirmation Dialog
- [ ] Toast Notifications

### 2.5 Feature Components - Fields
- [ ] Fields List Component
- [ ] Field Detail Component
- [ ] Field Form Component (Create/Edit)
- [ ] Field Delete Confirmation

### 2.6 Feature Components - Teams
- [ ] Teams List Component
- [ ] Team Detail Component
- [ ] Team Form Component
- [ ] Team Players List
- [ ] Team Plays List

### 2.7 Feature Components - Players
- [ ] Players List Component
- [ ] Player Detail Component
- [ ] Player Form Component
- [ ] Player Card Component

### 2.8 Feature Components - Plays
- [ ] Plays List Component
- [ ] Play Detail Component
- [ ] Play Form Component

**Meilenstein 2:** ✅ Vollständiges CRUD-Frontend

---

## 📅 Phase 3: Play Designer (Woche 5-6)

### 3.1 Canvas-Grundlagen
- [ ] HTML5 Canvas Setup
- [ ] Field Rendering (Linien, Markierungen)
- [ ] Zoom & Pan Funktionalität
- [ ] Grid System

### 3.2 Player Positioning
- [ ] Player Icons auf Field platzieren
- [ ] Drag & Drop für Player
- [ ] Position speichern (X, Y Koordinaten)
- [ ] Player Labels (Name, Number)

### 3.3 Routes & Bewegungen
- [ ] Route zeichnen (Linien, Kurven)
- [ ] Pfeil-Richtungen
- [ ] Route-Typen (Pass, Run, Block)
- [ ] Route-Farben

### 3.4 Play Speichern & Laden
- [ ] DiagramData als JSON speichern
- [ ] Play aus Datenbank laden
- [ ] Play rendern
- [ ] Versionierung

### 3.5 Export & Sharing
- [ ] Play als PNG exportieren
- [ ] Play als PDF exportieren
- [ ] Play teilen (Link)
- [ ] Play drucken

**Meilenstein 3:** ✅ Funktionaler Play Designer

---

## 📅 Phase 4: Authentifizierung & Autorisierung (Woche 7)

### 4.1 Backend - Identity
- [ ] ASP.NET Core Identity einrichten
- [ ] User Entity erweitern
- [ ] JWT Token Generation
- [ ] Login/Register Endpoints
- [ ] Password Hashing
- [ ] Refresh Tokens

### 4.2 Backend - Authorization
- [ ] Rollen definieren (Admin, Coach, Player)
- [ ] Policy-based Authorization
- [ ] Team-Ownership prüfen
- [ ] Controller mit [Authorize] schützen

### 4.3 Frontend - Auth
- [ ] Login Component
- [ ] Register Component
- [ ] Auth Service
- [ ] Token Storage (LocalStorage/SessionStorage)
- [ ] HTTP Interceptor für JWT
- [ ] Auth Guard für Routes
- [ ] Logout Funktionalität

### 4.4 User Management
- [ ] User Profile Component
- [ ] Password ändern
- [ ] User Settings
- [ ] Team-Mitgliedschaft verwalten

**Meilenstein 4:** ✅ Sichere Multi-User-Anwendung

---

## 📅 Phase 5: Erweiterte Features (Woche 8-9)

### 5.1 Suche & Filter
- [ ] Backend: Search Endpoints
- [ ] Frontend: Search Component
- [ ] Filter nach Team, Position, Formation
- [ ] Sortierung
- [ ] Pagination

### 5.2 File Upload
- [ ] Team Logo Upload
- [ ] Player Photo Upload
- [ ] File Storage (lokaler Ordner oder Cloud)
- [ ] Image Resize/Optimization

### 5.3 Playbook
- [ ] Playbook Entity (Sammlung von Plays)
- [ ] Playbook CRUD
- [ ] Plays zu Playbook hinzufügen
- [ ] Playbook als PDF exportieren

### 5.4 Statistics & Analytics
- [ ] Play Usage Tracking
- [ ] Most Used Formations
- [ ] Player Statistics
- [ ] Dashboard mit Charts (Chart.js/ngx-charts)

### 5.5 Collaboration
- [ ] Kommentare zu Plays
- [ ] Play Versionen
- [ ] Änderungshistorie
- [ ] Team-Chat (optional)

**Meilenstein 5:** ✅ Feature-Complete Application

---

## 📅 Phase 6: Testing (Woche 10)

### 6.1 Backend Unit Tests
- [ ] xUnit Setup
- [ ] Controller Tests
- [ ] Service Tests
- [ ] Repository Tests
- [ ] Mocking mit Moq

### 6.2 Backend Integration Tests
- [ ] WebApplicationFactory Setup
- [ ] API Endpoint Tests
- [ ] Database Integration Tests
- [ ] Test-Datenbank (TestContainers)

### 6.3 Frontend Unit Tests
- [ ] Jasmine/Karma Setup
- [ ] Component Tests
- [ ] Service Tests
- [ ] Pipe Tests

### 6.4 E2E Tests
- [ ] Playwright/Cypress Setup
- [ ] User Journey Tests
- [ ] CRUD Flow Tests
- [ ] Play Designer Tests

### 6.5 Performance Tests
- [ ] Load Testing (k6 oder JMeter)
- [ ] API Response Times
- [ ] Database Query Optimization
- [ ] Frontend Bundle Size

**Meilenstein 6:** ✅ 80%+ Test Coverage

---

## 📅 Phase 7: Deployment & DevOps (Woche 11)

### 7.1 Docker Containerization
- [ ] Backend Dockerfile
- [ ] Frontend Dockerfile (nginx)
- [ ] Docker Compose für gesamte App
- [ ] Multi-Stage Builds
- [ ] Environment Variables

### 7.2 CI/CD Pipeline
- [ ] GitHub Actions / GitLab CI Setup
- [ ] Build Pipeline
- [ ] Test Pipeline
- [ ] Docker Image Build & Push
- [ ] Automated Deployment

### 7.3 Cloud Deployment
- [ ] Cloud Provider wählen (Azure/AWS/GCP)
- [ ] Database Migration (Managed PostgreSQL)
- [ ] Backend Deployment (App Service/ECS/Cloud Run)
- [ ] Frontend Deployment (Static Hosting/CDN)
- [ ] SSL/TLS Zertifikate

### 7.4 Monitoring & Logging
- [ ] Application Insights / CloudWatch
- [ ] Error Tracking (Sentry)
- [ ] Performance Monitoring
- [ ] Log Aggregation
- [ ] Alerts & Notifications

### 7.5 Backup & Recovery
- [ ] Database Backups (automatisch)
- [ ] Disaster Recovery Plan
- [ ] Data Migration Scripts

**Meilenstein 7:** ✅ Production-Ready Application

---

## 📅 Phase 8: Optimierung & Polish (Woche 12)

### 8.1 Performance Optimization
- [ ] Database Indexing
- [ ] Query Optimization
- [ ] Caching (Redis)
- [ ] CDN für Static Assets
- [ ] Lazy Loading (Frontend)
- [ ] Code Splitting

### 8.2 UX/UI Improvements
- [ ] Responsive Design verfeinern
- [ ] Accessibility (WCAG)
- [ ] Dark Mode
- [ ] Animations & Transitions
- [ ] Loading States
- [ ] Empty States

### 8.3 Documentation
- [ ] API Documentation (Swagger erweitern)
- [ ] User Manual
- [ ] Developer Documentation
- [ ] Architecture Diagrams
- [ ] Deployment Guide

### 8.4 Security Hardening
- [ ] Security Audit
- [ ] OWASP Top 10 Check
- [ ] Rate Limiting
- [ ] Input Sanitization
- [ ] SQL Injection Prevention
- [ ] XSS Prevention

**Meilenstein 8:** ✅ Polished, Production-Grade Application

---

## 🎯 Optionale Features (Backlog)

### Mobile App
- [ ] React Native / Flutter App
- [ ] Offline-Modus
- [ ] Push Notifications

### Advanced Play Designer
- [ ] 3D Visualization
- [ ] Animation Timeline
- [ ] Video Integration
- [ ] AR Visualization

### AI Features
- [ ] Play Recommendation
- [ ] Formation Analysis
- [ ] Opponent Analysis

### Social Features
- [ ] Public Play Library
- [ ] Community Ratings
- [ ] Play Sharing
- [ ] Forums

---

## 📈 Erfolgsmetriken

### Technisch
- ✅ API Response Time < 200ms
- ✅ Frontend Load Time < 2s
- ✅ Test Coverage > 80%
- ✅ Zero Critical Security Issues
- ✅ 99.9% Uptime

### Business
- ✅ User Registration
- ✅ Active Teams
- ✅ Created Plays
- ✅ User Retention Rate

---

## 🛠️ Entwicklungs-Workflow

### Daily
1. Pull latest changes
2. Check TODO-Liste
3. Entwickeln & Testen
4. Commit & Push
5. Code Review (wenn Team)

### Weekly
1. Sprint Planning
2. Progress Review
3. Deployment zu Staging
4. User Feedback sammeln

### Monthly
1. Production Deployment
2. Performance Review
3. Security Audit
4. Roadmap Update

---

## 📚 Ressourcen & Links

### Dokumentation
- [TODO_00_Overview.md](TODO_00_Overview.md) - Detaillierte TODOs
- [Anforderungen.md](Anforderungen.md) - Projekt-Anforderungen
- [MigrationsGuide.md](MigrationsGuide.md) - EF Migrations
- [infra/README.md](../infra/README.md) - Datenbank Setup

### Externe Ressourcen
- ASP.NET Core Docs: https://docs.microsoft.com/aspnet/core
- Angular Docs: https://angular.dev
- PostgreSQL Docs: https://www.postgresql.org/docs
- EF Core Docs: https://docs.microsoft.com/ef/core

---

**Letzte Aktualisierung:** 2026-03-08  
**Version:** 1.0  
**Status:** In Entwicklung 🚀

