# TODO: API mit Swagger testen

## Status: ⏳ Offen

## Ziel
Alle API-Endpoints mit Swagger UI testen und sicherstellen, dass CRUD-Operationen funktionieren.

## Voraussetzungen
- ✅ Datenbank läuft
- ✅ Backend läuft (`dotnet watch run`)
- ✅ Swagger UI ist verfügbar

## Swagger UI öffnen
**URL:** http://localhost:5073

## Test-Szenarien

### 1. Fields testen

#### 1.1 Field erstellen (POST)
**Endpoint:** `POST /api/fields`

**Request Body:**
```json
{
  "name": "Hauptfeld",
  "type": "American Football",
  "width": 53.3,
  "length": 120
}
```

**Erwartetes Ergebnis:**
- Status: `201 Created`
- Response enthält `id` und alle Felder
- Location-Header zeigt auf `/api/fields/{id}`

#### 1.2 Alle Fields abrufen (GET)
**Endpoint:** `GET /api/fields`

**Erwartetes Ergebnis:**
- Status: `200 OK`
- Array mit allen Fields
- Mindestens das gerade erstellte Field

#### 1.3 Ein Field abrufen (GET)
**Endpoint:** `GET /api/fields/1`

**Erwartetes Ergebnis:**
- Status: `200 OK`
- Field-Objekt mit ID 1

#### 1.4 Field aktualisieren (PUT)
**Endpoint:** `PUT /api/fields/1`

**Request Body:**
```json
{
  "name": "Hauptfeld - Aktualisiert",
  "type": "American Football",
  "width": 53.3,
  "length": 120
}
```

**Erwartetes Ergebnis:**
- Status: `204 No Content`

#### 1.5 Field löschen (DELETE)
**Endpoint:** `DELETE /api/fields/1`

**Erwartetes Ergebnis:**
- Status: `204 No Content`
- Field ist nicht mehr in der Datenbank

### 2. Teams testen

#### 2.1 Team erstellen
**Endpoint:** `POST /api/teams`

**Request Body:**
```json
{
  "name": "Berlin Bears",
  "description": "Unser Hauptteam",
  "logoUrl": "https://example.com/logo.png"
}
```

#### 2.2 Alle Teams abrufen
**Endpoint:** `GET /api/teams`

#### 2.3 Team mit Spielern abrufen
**Endpoint:** `GET /api/teams/1/players`

**Erwartetes Ergebnis:**
- Leeres Array (noch keine Spieler)

### 3. Players testen

#### 3.1 Spieler erstellen
**Endpoint:** `POST /api/players`

**Request Body:**
```json
{
  "firstName": "Max",
  "lastName": "Mustermann",
  "jerseyNumber": "12",
  "position": "Quarterback",
  "teamId": 1
}
```

#### 3.2 Alle Spieler abrufen
**Endpoint:** `GET /api/players`

#### 3.3 Spieler eines Teams abrufen
**Endpoint:** `GET /api/teams/1/players`

**Erwartetes Ergebnis:**
- Array mit dem gerade erstellten Spieler

### 4. Plays testen

#### 4.1 Play erstellen
**Endpoint:** `POST /api/plays`

**Request Body:**
```json
{
  "name": "Hail Mary",
  "description": "Langer Pass zum Endzone",
  "formation": "Shotgun",
  "teamId": 1,
  "fieldId": 1
}
```

#### 4.2 Alle Plays abrufen
**Endpoint:** `GET /api/plays`

#### 4.3 Plays eines Teams abrufen
**Endpoint:** `GET /api/teams/1/plays`

## Validierung testen

### Test 1: Pflichtfeld fehlt
**Endpoint:** `POST /api/fields`

**Request Body:**
```json
{
  "type": "American Football",
  "width": 53.3,
  "length": 120
}
```

**Erwartetes Ergebnis:**
- Status: `400 Bad Request`
- Fehlermeldung: "The Name field is required."

### Test 2: Ungültiger Wert
**Endpoint:** `POST /api/fields`

**Request Body:**
```json
{
  "name": "Test",
  "type": "American Football",
  "width": -10,
  "length": 120
}
```

**Erwartetes Ergebnis:**
- Status: `400 Bad Request`
- Fehlermeldung: Validierungsfehler für Width

### Test 3: Nicht existierende Ressource
**Endpoint:** `GET /api/fields/999`

**Erwartetes Ergebnis:**
- Status: `404 Not Found`

## Checkliste

### Fields
- [ ] POST /api/fields funktioniert
- [ ] GET /api/fields funktioniert
- [ ] GET /api/fields/{id} funktioniert
- [ ] PUT /api/fields/{id} funktioniert
- [ ] DELETE /api/fields/{id} funktioniert
- [ ] Validierung funktioniert

### Teams
- [ ] POST /api/teams funktioniert
- [ ] GET /api/teams funktioniert
- [ ] GET /api/teams/{id} funktioniert
- [ ] GET /api/teams/{id}/players funktioniert
- [ ] GET /api/teams/{id}/plays funktioniert
- [ ] PUT /api/teams/{id} funktioniert
- [ ] DELETE /api/teams/{id} funktioniert

### Players
- [ ] POST /api/players funktioniert
- [ ] GET /api/players funktioniert
- [ ] GET /api/players/{id} funktioniert
- [ ] PUT /api/players/{id} funktioniert
- [ ] DELETE /api/players/{id} funktioniert

### Plays
- [ ] POST /api/plays funktioniert
- [ ] GET /api/plays funktioniert
- [ ] GET /api/plays/{id} funktioniert
- [ ] PUT /api/plays/{id} funktioniert
- [ ] DELETE /api/plays/{id} funktioniert

## Troubleshooting

### Swagger zeigt keine Endpoints
→ Prüfe, ob Controller mit `[ApiController]` und `[Route]` annotiert sind

### 500 Internal Server Error
→ Prüfe Backend-Logs in der Konsole

### Validierung funktioniert nicht
→ Prüfe, ob DTOs `[Required]`, `[MaxLength]` etc. haben

## Nächster Schritt
Nach erfolgreichem Test → `TODO_08_Frontend_Setup.md`

