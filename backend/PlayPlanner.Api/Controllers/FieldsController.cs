using Microsoft.AspNetCore.Mvc;
using PlayPlanner.Domain.Entities;
using PlayPlanner.Infrastructure.Persistence;

namespace PlayPlanner.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class FieldsController : ControllerBase
{
    
    private readonly AppDbContext _dbContext;
    public FieldsController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    [HttpGet]
    public IEnumerable<string> GetAll()
    {
        return _dbContext.Fields.Select(f => f.Name);
    }
    
    [HttpGet("{id}")]
    public string GetById(Guid id)
    {
        return _dbContext.Fields.FirstOrDefault(f => f.Id == id)?.Name ?? "Not found";
    }
    
    [HttpGet("{name}")]
    public string getByName(string name)
    {
        return _dbContext.Fields.FirstOrDefault(f => f.Name == name)?.Name ?? "Not found";
    }
    
    [HttpPost]
    public void Create([FromBody] Field field)
    {
        _dbContext.Fields.Add(field);
        _dbContext.SaveChanges();
    }
    
    [HttpDelete("{id}")]
    public void Delete(Guid id)
    {
        _dbContext.Fields.Remove(_dbContext.Fields.FirstOrDefault(f => f.Id == id)!);
        _dbContext.SaveChanges();
    }
    
    [HttpPut("{id}")]
    public void Update(Guid id, [FromBody] Field field)
    {
        _dbContext.Fields.Update(field);
        _dbContext.SaveChanges();
    }
    
    
}