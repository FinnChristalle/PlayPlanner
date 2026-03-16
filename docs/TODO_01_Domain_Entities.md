# TODO: Domain Entities erstellen

## Status: ⏳ Offen

## Ziel
Alle fehlenden Domain-Entities für das PlayPlanner-Projekt erstellen.

## Bereits vorhanden
- ✅ `Field` Entity

## Zu erstellen

### 1. Team Entity
**Datei:** `backend/PlayPlanner.Domain/Entities/Team.cs`

```csharp
namespace PlayPlanner.Domain.Entities;

public class Team
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? LogoUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    // Navigation Properties
    public ICollection<Player> Players { get; set; } = new List<Player>();
    public ICollection<Play> Plays { get; set; } = new List<Play>();
}
```

### 2. Player Entity
**Datei:** `backend/PlayPlanner.Domain/Entities/Player.cs`

```csharp
namespace PlayPlanner.Domain.Entities;

public class Player
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? JerseyNumber { get; set; }
    public string Position { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    // Foreign Keys
    public int TeamId { get; set; }
    
    // Navigation Properties
    public Team Team { get; set; } = null!;
    public ICollection<PlayerPosition> PlayerPositions { get; set; } = new List<PlayerPosition>();
}
```

### 3. Play Entity
**Datei:** `backend/PlayPlanner.Domain/Entities/Play.cs`

```csharp
namespace PlayPlanner.Domain.Entities;

public class Play
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Formation { get; set; } = string.Empty;
    public string? DiagramData { get; set; } // JSON für Spielzug-Diagramm
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    // Foreign Keys
    public int TeamId { get; set; }
    public int? FieldId { get; set; }
    
    // Navigation Properties
    public Team Team { get; set; } = null!;
    public Field? Field { get; set; }
    public ICollection<PlayerPosition> PlayerPositions { get; set; } = new List<PlayerPosition>();
}
```

### 4. PlayerPosition Entity (Verknüpfung)
**Datei:** `backend/PlayPlanner.Domain/Entities/PlayerPosition.cs`

```csharp
namespace PlayPlanner.Domain.Entities;

public class PlayerPosition
{
    public int Id { get; set; }
    public int PlayId { get; set; }
    public int PlayerId { get; set; }
    public double PositionX { get; set; }
    public double PositionY { get; set; }
    public string? Route { get; set; } // JSON für Route/Bewegung
    
    // Navigation Properties
    public Play Play { get; set; } = null!;
    public Player Player { get; set; } = null!;
}
```

## Checkliste
- [ ] Team.cs erstellen
- [ ] Player.cs erstellen
- [ ] Play.cs erstellen
- [ ] PlayerPosition.cs erstellen
- [ ] Alle Entities kompilieren ohne Fehler

## Nächster Schritt
Nach Fertigstellung → `TODO_02_Entity_Configurations.md`

