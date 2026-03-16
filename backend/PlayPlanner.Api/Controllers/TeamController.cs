using Microsoft.AspNetCore.Mvc;
using PlayPlanner.Domain.Entities;
using PlayPlanner.Infrastructure.Persistence;

namespace PlayPlanner.Api.Controllers;



[ApiController]
[Route("api/[controller]")]
public class TeamController
{
    
    private readonly AppDbContext _dbContext;
    public TeamController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    [HttpGet]
    public IEnumerable<string> GetAll()
    {
        return _dbContext.Teams.Select(t => t.Name);
    }
    
    [HttpGet("{id}")]
    public string GetById(Guid id)
    {
        return _dbContext.Teams.FirstOrDefault(t => t.Id == id)?.Name ?? "Not found";
    }
    
    [HttpGet("{name}")]
    public string getByName(string name)
    {
        return _dbContext.Teams.FirstOrDefault(t => t.Name == name)?.Name ?? "Not found";
    }
    
    [HttpPost]
    public void Create([FromBody] Team team)
    {
        _dbContext.Teams.Add(team);
        _dbContext.SaveChanges();
    }
    
    [HttpDelete("{id}")]
    public void Delete(Guid id)
    {
        _dbContext.Teams.Remove(_dbContext.Teams.FirstOrDefault(t => t.Id == id)!);
        _dbContext.SaveChanges();
    }
    
    [HttpPut("{id}")]
    public void Update(Guid id, [FromBody] Team team)
    {
        _dbContext.Teams.Update(team);
        _dbContext.SaveChanges();
    }
    
}