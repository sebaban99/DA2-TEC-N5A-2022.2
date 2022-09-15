namespace Vidly.Domain.Entities;

public class Actor
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }

    public List<Movie> Movies { get; set; } = new();
}