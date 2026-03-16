using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlayPlanner.Domain.Entities;

namespace PlayPlanner.Infrastructure.Persistence.Configurations;

public class FieldConfiguration : IEntityTypeConfiguration<Field>
{
    public void Configure(EntityTypeBuilder<Field> builder)
    {
        builder.ToTable("fields");
        builder.HasKey(f => f.Id);
        builder.Property(f => f.Name).IsRequired().HasMaxLength(100);
        builder.Property(f => f.Sport).IsRequired();
        builder.Property(f => f.WidthMeters).IsRequired();
        builder.Property(f => f.HeightMeters).IsRequired();
    }
}