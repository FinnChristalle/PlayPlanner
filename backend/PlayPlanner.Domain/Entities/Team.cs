namespace PlayPlanner.Domain.Entities;

public class Team
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public SportType Sport { get; set; }
    public int DefaultPlayerCount { get; set; }
    public int AdjustedPlayerCount { get; set; }
    public Guid? DefaultFieldId { get; set; }
    public Field? DefaultField { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<Player> Players { get; set; } = new List<Player>();
    public List<Play> Plays { get; set; } = new List<Play>();
}

public enum SportType
{
    Basketball,
    Football,
    Handball,
    Volleyball
}