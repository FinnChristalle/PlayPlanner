namespace PlayPlanner.Domain.Entities;

public class Player
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public int JerseyNumber { get; set; }
    public Position DefaultPosition { get; set; }
    public Guid TeamId { get; set; }
    public Team Team { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}	

public class Position
{
    public string Name { get; set; } = default!;
    public decimal X { get; set; }
    public decimal Y { get; set; }
}