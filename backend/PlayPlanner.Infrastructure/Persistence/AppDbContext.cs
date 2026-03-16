using Microsoft.EntityFrameworkCore;
using PlayPlanner.Domain.Entities;
using PlayPlanner.Infrastructure.Persistence.Configurations;

namespace PlayPlanner.Infrastructure.Persistence;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    { }
    
    public DbSet<Field> Fields { get; set; }
    public DbSet<Team> Teams { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new FieldConfiguration());
        modelBuilder.ApplyConfiguration(new TeamConfiguration());
    }
    
    
    
}