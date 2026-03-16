# TODO: DTOs (Data Transfer Objects) erstellen

## Status: ⏳ Offen

## Ziel
DTOs für alle Entities erstellen, um die API-Schnittstelle von der Domain zu trennen.

## Warum DTOs?
- ✅ Trennung von Domain und API
- ✅ Kontrolle über welche Daten übertragen werden
- ✅ Vermeidung von Circular References
- ✅ Validierung auf API-Ebene
- ✅ Versionierung der API möglich

## Struktur
Alle DTOs kommen in: `backend/PlayPlanner.Application/DTOs/`

## Zu erstellen

### 1. Field DTOs
**Ordner:** `backend/PlayPlanner.Application/DTOs/Field/`

**FieldDto.cs** (für GET-Requests)
```csharp
namespace PlayPlanner.Application.DTOs.Field;

public class FieldDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public double Width { get; set; }
    public double Length { get; set; }
    public DateTime CreatedAt { get; set; }
}
```

**CreateFieldDto.cs** (für POST-Requests)
```csharp
using System.ComponentModel.DataAnnotations;

namespace PlayPlanner.Application.DTOs.Field;

public class CreateFieldDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(50)]
    public string Type { get; set; } = string.Empty;
    
    [Range(0, 1000)]
    public double Width { get; set; }
    
    [Range(0, 1000)]
    public double Length { get; set; }
}
```

**UpdateFieldDto.cs** (für PUT-Requests)
```csharp
using System.ComponentModel.DataAnnotations;

namespace PlayPlanner.Application.DTOs.Field;

public class UpdateFieldDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(50)]
    public string Type { get; set; } = string.Empty;
    
    [Range(0, 1000)]
    public double Width { get; set; }
    
    [Range(0, 1000)]
    public double Length { get; set; }
}
```

### 2. Team DTOs
**Ordner:** `backend/PlayPlanner.Application/DTOs/Team/`

**TeamDto.cs**
```csharp
namespace PlayPlanner.Application.DTOs.Team;

public class TeamDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? LogoUrl { get; set; }
    public int PlayerCount { get; set; }
    public int PlayCount { get; set; }
    public DateTime CreatedAt { get; set; }
}
```

**CreateTeamDto.cs**
```csharp
using System.ComponentModel.DataAnnotations;

namespace PlayPlanner.Application.DTOs.Team;

public class CreateTeamDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [MaxLength(500)]
    public string? Description { get; set; }
    
    [MaxLength(500)]
    [Url]
    public string? LogoUrl { get; set; }
}
```

**UpdateTeamDto.cs**
```csharp
using System.ComponentModel.DataAnnotations;

namespace PlayPlanner.Application.DTOs.Team;

public class UpdateTeamDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [MaxLength(500)]
    public string? Description { get; set; }
    
    [MaxLength(500)]
    [Url]
    public string? LogoUrl { get; set; }
}
```

### 3. Player DTOs
**Ordner:** `backend/PlayPlanner.Application/DTOs/Player/`

**PlayerDto.cs**
```csharp
namespace PlayPlanner.Application.DTOs.Player;

public class PlayerDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => $"{FirstName} {LastName}";
    public string? JerseyNumber { get; set; }
    public string Position { get; set; } = string.Empty;
    public int TeamId { get; set; }
    public string TeamName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
```

**CreatePlayerDto.cs**
```csharp
using System.ComponentModel.DataAnnotations;

namespace PlayPlanner.Application.DTOs.Player;

public class CreatePlayerDto
{
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;
    
    [MaxLength(10)]
    public string? JerseyNumber { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Position { get; set; } = string.Empty;
    
    [Required]
    public int TeamId { get; set; }
}
```

**UpdatePlayerDto.cs** - Analog zu CreatePlayerDto

## Checkliste
- [ ] Field DTOs erstellt (FieldDto, CreateFieldDto, UpdateFieldDto)
- [ ] Team DTOs erstellt (TeamDto, CreateTeamDto, UpdateTeamDto)
- [ ] Player DTOs erstellt (PlayerDto, CreatePlayerDto, UpdatePlayerDto)
- [ ] Play DTOs erstellt (PlayDto, CreatePlayDto, UpdatePlayDto)
- [ ] Validierungs-Attribute hinzugefügt

## Nächster Schritt
Nach Fertigstellung → `TODO_06_Create_Controllers.md`

