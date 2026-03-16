# TODO: Frontend Setup

## Status: ⏳ Offen

## Ziel
Angular Frontend vorbereiten und mit dem Backend verbinden.

## Voraussetzungen
- ✅ Backend-API läuft und ist getestet
- ✅ Alle Endpoints funktionieren
- ✅ Swagger-Dokumentation ist vollständig

## Schritte

### 1. Frontend-Struktur prüfen

```bash
cd frontend/PlayPlanner
```

Prüfe, ob folgende Struktur vorhanden ist:
```
frontend/PlayPlanner/
├── src/
│   ├── app/
│   ├── environments/
│   └── ...
├── angular.json
├── package.json
└── ...
```

### 2. Environment-Konfiguration

**Datei:** `src/environments/environment.ts`

```typescript
export const environment = {
  production: false,
  apiUrl: 'http://localhost:5073/api'
};
```

**Datei:** `src/environments/environment.prod.ts`

```typescript
export const environment = {
  production: true,
  apiUrl: 'https://your-production-api.com/api'
};
```

### 3. HTTP Client konfigurieren

**Datei:** `src/app/app.config.ts`

```typescript
import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient } from '@angular/common/http';

import { routes } from './app.routes';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideHttpClient()  // ← Hinzufügen
  ]
};
```

### 4. API Service erstellen

**Datei:** `src/app/services/api.service.ts`

```typescript
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  // Generic GET
  get<T>(endpoint: string): Observable<T> {
    return this.http.get<T>(`${this.apiUrl}/${endpoint}`);
  }

  // Generic POST
  post<T>(endpoint: string, data: any): Observable<T> {
    return this.http.post<T>(`${this.apiUrl}/${endpoint}`, data);
  }

  // Generic PUT
  put<T>(endpoint: string, data: any): Observable<T> {
    return this.http.put<T>(`${this.apiUrl}/${endpoint}`, data);
  }

  // Generic DELETE
  delete<T>(endpoint: string): Observable<T> {
    return this.http.delete<T>(`${this.apiUrl}/${endpoint}`);
  }
}
```

### 5. Models/Interfaces erstellen

**Ordner:** `src/app/models/`

**field.model.ts**
```typescript
export interface Field {
  id: number;
  name: string;
  type: string;
  width: number;
  length: number;
  createdAt: Date;
}

export interface CreateFieldDto {
  name: string;
  type: string;
  width: number;
  length: number;
}

export interface UpdateFieldDto {
  name: string;
  type: string;
  width: number;
  length: number;
}
```

**team.model.ts**
```typescript
export interface Team {
  id: number;
  name: string;
  description?: string;
  logoUrl?: string;
  playerCount: number;
  playCount: number;
  createdAt: Date;
}

export interface CreateTeamDto {
  name: string;
  description?: string;
  logoUrl?: string;
}
```

**player.model.ts**
```typescript
export interface Player {
  id: number;
  firstName: string;
  lastName: string;
  fullName: string;
  jerseyNumber?: string;
  position: string;
  teamId: number;
  teamName: string;
  createdAt: Date;
}

export interface CreatePlayerDto {
  firstName: string;
  lastName: string;
  jerseyNumber?: string;
  position: string;
  teamId: number;
}
```

### 6. Feature Services erstellen

**Datei:** `src/app/services/field.service.ts`

```typescript
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api.service';
import { Field, CreateFieldDto, UpdateFieldDto } from '../models/field.model';

@Injectable({
  providedIn: 'root'
})
export class FieldService {
  private endpoint = 'fields';

  constructor(private api: ApiService) { }

  getAll(): Observable<Field[]> {
    return this.api.get<Field[]>(this.endpoint);
  }

  getById(id: number): Observable<Field> {
    return this.api.get<Field>(`${this.endpoint}/${id}`);
  }

  create(field: CreateFieldDto): Observable<Field> {
    return this.api.post<Field>(this.endpoint, field);
  }

  update(id: number, field: UpdateFieldDto): Observable<void> {
    return this.api.put<void>(`${this.endpoint}/${id}`, field);
  }

  delete(id: number): Observable<void> {
    return this.api.delete<void>(`${this.endpoint}/${id}`);
  }
}
```

Analog für:
- `team.service.ts`
- `player.service.ts`
- `play.service.ts`

### 7. CORS im Backend konfigurieren

**Datei:** `backend/PlayPlanner.Api/Program.cs`

```csharp
// Nach builder.Services.AddSwaggerGen()
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Nach app.UseSwaggerUI()
app.UseCors("AllowAngular");
```

### 8. Frontend starten

```bash
cd frontend/PlayPlanner
npm install
ng serve
```

**URL:** http://localhost:4200

## Checkliste
- [ ] Environment-Dateien konfiguriert
- [ ] HttpClient in app.config.ts hinzugefügt
- [ ] ApiService erstellt
- [ ] Models/Interfaces erstellt
- [ ] Feature Services erstellt (Field, Team, Player, Play)
- [ ] CORS im Backend konfiguriert
- [ ] Frontend startet ohne Fehler
- [ ] API-Calls funktionieren (in Browser DevTools prüfen)

## Nächster Schritt
Nach Fertigstellung → `TODO_09_Create_Components.md`

