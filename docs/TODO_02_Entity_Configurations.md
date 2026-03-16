# TODO: Entity Framework Konfigurationen erstellen

## Status: ⏳ Offen

## Ziel
Fluent API Konfigurationen für alle Entities erstellen.

## Bereits vorhanden
- ✅ `FieldConfiguration`

## Zu erstellen

### 1. TeamConfiguration
**Datei:** `backend/PlayPlanner.Infrastructure/Persistence/Configurations/TeamConfiguration.cs`

```csharp
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlayPlanner.Domain.Entities;

namespace PlayPlanner.Infrastructure.Persistence.Configurations;

public class TeamConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.ToTable("Teams");
        builder.HasKey(t => t.Id);
        
        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(t => t.Description)
            .HasMaxLength(500);
        
        builder.Property(t => t.LogoUrl)
            .HasMaxLength(500);
        
        builder.Property(t => t.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
        
        builder.HasMany(t => t.Players)
            .WithOne(p => p.Team)
            .HasForeignKey(p => p.TeamId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(t => t.Plays)
            .WithOne(p => p.Team)
            .HasForeignKey(p => p.TeamId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
```

### 2. PlayerConfiguration
**Datei:** `backend/PlayPlanner.Infrastructure/Persistence/Configurations/PlayerConfiguration.cs`

```csharp
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlayPlanner.Domain.Entities;

namespace PlayPlanner.Infrastructure.Persistence.Configurations;

public class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.ToTable("Players");
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.FirstName)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(p => p.LastName)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(p => p.JerseyNumber)
            .HasMaxLength(10);
        
        builder.Property(p => p.Position)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(p => p.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
        
        builder.HasOne(p => p.Team)
            .WithMany(t => t.Players)
            .HasForeignKey(p => p.TeamId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
```

### 3. PlayConfiguration
**Datei:** `backend/PlayPlanner.Infrastructure/Persistence/Configurations/PlayConfiguration.cs`

```csharp
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlayPlanner.Domain.Entities;

namespace PlayPlanner.Infrastructure.Persistence.Configurations;

public class PlayConfiguration : IEntityTypeConfiguration<Play>
{
    public void Configure(EntityTypeBuilder<Play> builder)
    {
        builder.ToTable("Plays");
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(p => p.Description)
            .HasMaxLength(500);
        
        builder.Property(p => p.Formation)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(p => p.DiagramData)
            .HasColumnType("jsonb"); // PostgreSQL JSON
        
        builder.Property(p => p.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
        
        builder.HasOne(p => p.Team)
            .WithMany(t => t.Plays)
            .HasForeignKey(p => p.TeamId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(p => p.Field)
            .WithMany()
            .HasForeignKey(p => p.FieldId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
```

### 4. PlayerPositionConfiguration
**Datei:** `backend/PlayPlanner.Infrastructure/Persistence/Configurations/PlayerPositionConfiguration.cs`

```csharp
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlayPlanner.Domain.Entities;

namespace PlayPlanner.Infrastructure.Persistence.Configurations;

public class PlayerPositionConfiguration : IEntityTypeConfiguration<PlayerPosition>
{
    public void Configure(EntityTypeBuilder<PlayerPosition> builder)
    {
        builder.ToTable("PlayerPositions");
        builder.HasKey(pp => pp.Id);
        
        builder.Property(pp => pp.PositionX)
            .IsRequired();
        
        builder.Property(pp => pp.PositionY)
            .IsRequired();
        
        builder.Property(pp => pp.Route)
            .HasColumnType("jsonb");
        
        builder.HasOne(pp => pp.Play)
            .WithMany(p => p.PlayerPositions)
            .HasForeignKey(pp => pp.PlayId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(pp => pp.Player)
            .WithMany(p => p.PlayerPositions)
            .HasForeignKey(pp => pp.PlayerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
```

## Checkliste
- [ ] TeamConfiguration.cs erstellen
- [ ] PlayerConfiguration.cs erstellen
- [ ] PlayConfiguration.cs erstellen
- [ ] PlayerPositionConfiguration.cs erstellen

## Nächster Schritt
Nach Fertigstellung → `TODO_03_Update_DbContext.md`

