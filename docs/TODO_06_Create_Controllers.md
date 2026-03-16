# TODO: API Controllers erstellen

## Status: ⏳ Offen

## Ziel
REST-API Controllers für alle Entities mit CRUD-Operationen erstellen.

## Struktur
Alle Controller kommen in: `backend/PlayPlanner.Api/Controllers/`

## Zu erstellen

### 1. FieldsController
**Datei:** `backend/PlayPlanner.Api/Controllers/FieldsController.cs`

**Endpoints:**
- `GET /api/fields` - Alle Fields abrufen
- `GET /api/fields/{id}` - Ein Field abrufen
- `POST /api/fields` - Neues Field erstellen
- `PUT /api/fields/{id}` - Field aktualisieren
- `DELETE /api/fields/{id}` - Field löschen

**Beispiel-Struktur:**
```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlayPlanner.Infrastructure.Persistence;
using PlayPlanner.Domain.Entities;
using PlayPlanner.Application.DTOs.Field;

namespace PlayPlanner.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FieldsController : ControllerBase
{
    private readonly AppDbContext _context;

    public FieldsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FieldDto>>> GetFields()
    {
        var fields = await _context.Fields
            .Select(f => new FieldDto
            {
                Id = f.Id,
                Name = f.Name,
                Type = f.Type,
                Width = f.Width,
                Length = f.Length,
                CreatedAt = f.CreatedAt
            })
            .ToListAsync();
        
        return Ok(fields);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<FieldDto>> GetField(int id)
    {
        var field = await _context.Fields.FindAsync(id);
        
        if (field == null)
            return NotFound();
        
        var fieldDto = new FieldDto
        {
            Id = field.Id,
            Name = field.Name,
            Type = field.Type,
            Width = field.Width,
            Length = field.Length,
            CreatedAt = field.CreatedAt
        };
        
        return Ok(fieldDto);
    }

    [HttpPost]
    public async Task<ActionResult<FieldDto>> CreateField(CreateFieldDto dto)
    {
        var field = new Field
        {
            Name = dto.Name,
            Type = dto.Type,
            Width = dto.Width,
            Length = dto.Length,
            CreatedAt = DateTime.UtcNow
        };
        
        _context.Fields.Add(field);
        await _context.SaveChangesAsync();
        
        var fieldDto = new FieldDto
        {
            Id = field.Id,
            Name = field.Name,
            Type = field.Type,
            Width = field.Width,
            Length = field.Length,
            CreatedAt = field.CreatedAt
        };
        
        return CreatedAtAction(nameof(GetField), new { id = field.Id }, fieldDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateField(int id, UpdateFieldDto dto)
    {
        var field = await _context.Fields.FindAsync(id);
        
        if (field == null)
            return NotFound();
        
        field.Name = dto.Name;
        field.Type = dto.Type;
        field.Width = dto.Width;
        field.Length = dto.Length;
        field.UpdatedAt = DateTime.UtcNow;
        
        await _context.SaveChangesAsync();
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteField(int id)
    {
        var field = await _context.Fields.FindAsync(id);
        
        if (field == null)
            return NotFound();
        
        _context.Fields.Remove(field);
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
}
```

### 2. TeamsController
**Datei:** `backend/PlayPlanner.Api/Controllers/TeamsController.cs`

**Endpoints:**
- `GET /api/teams` - Alle Teams
- `GET /api/teams/{id}` - Ein Team
- `GET /api/teams/{id}/players` - Alle Spieler eines Teams
- `GET /api/teams/{id}/plays` - Alle Plays eines Teams
- `POST /api/teams` - Neues Team
- `PUT /api/teams/{id}` - Team aktualisieren
- `DELETE /api/teams/{id}` - Team löschen

### 3. PlayersController
**Datei:** `backend/PlayPlanner.Api/Controllers/PlayersController.cs`

**Endpoints:**
- `GET /api/players` - Alle Spieler
- `GET /api/players/{id}` - Ein Spieler
- `POST /api/players` - Neuer Spieler
- `PUT /api/players/{id}` - Spieler aktualisieren
- `DELETE /api/players/{id}` - Spieler löschen

### 4. PlaysController
**Datei:** `backend/PlayPlanner.Api/Controllers/PlaysController.cs`

**Endpoints:**
- `GET /api/plays` - Alle Plays
- `GET /api/plays/{id}` - Ein Play
- `POST /api/plays` - Neuer Play
- `PUT /api/plays/{id}` - Play aktualisieren
- `DELETE /api/plays/{id}` - Play löschen

## Best Practices

### 1. Immer DTOs verwenden
```csharp
// ✅ Gut
return Ok(new FieldDto { ... });

// ❌ Schlecht
return Ok(field); // Entity direkt zurückgeben
```

### 2. Async/Await verwenden
```csharp
public async Task<ActionResult<FieldDto>> GetField(int id)
{
    var field = await _context.Fields.FindAsync(id);
    // ...
}
```

### 3. Korrekte HTTP-Status-Codes
- `200 OK` - Erfolgreiche GET-Requests
- `201 Created` - Erfolgreiche POST-Requests
- `204 No Content` - Erfolgreiche PUT/DELETE-Requests
- `404 Not Found` - Ressource nicht gefunden
- `400 Bad Request` - Validierungsfehler

## Checkliste
- [ ] FieldsController erstellt
- [ ] TeamsController erstellt
- [ ] PlayersController erstellt
- [ ] PlaysController erstellt
- [ ] Alle Endpoints verwenden DTOs
- [ ] Validierung funktioniert
- [ ] Projekt kompiliert

## Nächster Schritt
Nach Fertigstellung → `TODO_07_Test_With_Swagger.md`

