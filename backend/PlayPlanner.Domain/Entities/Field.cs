namespace PlayPlanner.Domain.Entities;

public class Field
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public SportType Sport { get; set; }
    public decimal WidthMeters { get; set; }
    public decimal HeightMeters { get; set; }
    public string? MetaJson { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}