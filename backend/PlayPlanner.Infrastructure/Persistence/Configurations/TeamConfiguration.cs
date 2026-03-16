using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlayPlanner.Domain.Entities;

namespace PlayPlanner.Infrastructure.Persistence.Configurations;

public class TeamConfiguration: IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.ToTable("Teams");
        
        builder.HasKey(t => t.Id);
        
        builder.Property(t => t.Name).IsRequired().HasMaxLength(100);
        builder.Property(t => t.Sport).IsRequired();
        builder.Property(t => t.DefaultPlayerCount).IsRequired();
        builder.Property(t => t.AdjustedPlayerCount).IsRequired();
        builder.Property(t => t.CreatedAt).IsRequired();
        builder.Property(t => t.UpdatedAt).IsRequired();
        
        builder.HasMany(t => t.Players).WithOne(p => p.Team).HasForeignKey(p => p.TeamId);
        builder.HasMany(t => t.Plays);
    }
}