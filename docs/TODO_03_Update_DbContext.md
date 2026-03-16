# TODO: AppDbContext aktualisieren

## Status: ⏳ Offen

## Ziel
AppDbContext mit allen neuen Entities und Konfigurationen erweitern.

## Datei
`backend/PlayPlanner.Infrastructure/Persistence/AppDbContext.cs`

## Änderungen

### 1. DbSets hinzufügen

```csharp
namespace PlayPlanner.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    { }
    
    // DbSets
    public DbSet<Field> Fields { get; set; }
    public DbSet<Team> Teams { get; set; }              // ← NEU
    public DbSet<Player> Players { get; set; }          // ← NEU
    public DbSet<Play> Plays { get; set; }              // ← NEU
    public DbSet<PlayerPosition> PlayerPositions { get; set; }  // ← NEU
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Konfigurationen anwenden
        modelBuilder.ApplyConfiguration(new FieldConfiguration());
        modelBuilder.ApplyConfiguration(new TeamConfiguration());           // ← NEU
        modelBuilder.ApplyConfiguration(new PlayerConfiguration());         // ← NEU
        modelBuilder.ApplyConfiguration(new PlayConfiguration());           // ← NEU
        modelBuilder.ApplyConfiguration(new PlayerPositionConfiguration()); // ← NEU
    }
}
```

### 2. Using-Statements prüfen

Stelle sicher, dass folgende Usings vorhanden sind:

```csharp
using Microsoft.EntityFrameworkCore;
using PlayPlanner.Domain.Entities;
using PlayPlanner.Infrastructure.Persistence.Configurations;
```

## Checkliste
- [ ] DbSet<Team> hinzufügen
- [ ] DbSet<Player> hinzufügen
- [ ] DbSet<Play> hinzufügen
- [ ] DbSet<PlayerPosition> hinzufügen
- [ ] TeamConfiguration in OnModelCreating registrieren
- [ ] PlayerConfiguration in OnModelCreating registrieren
- [ ] PlayConfiguration in OnModelCreating registrieren
- [ ] PlayerPositionConfiguration in OnModelCreating registrieren
- [ ] Projekt kompiliert ohne Fehler

## Nächster Schritt
Nach Fertigstellung → `TODO_04_Create_Migration.md`

