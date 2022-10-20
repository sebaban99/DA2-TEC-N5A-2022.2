namespace Vidly.WebApi.Domain;

public class Actor
{
    public int Id { get; set; }

    public string Name { get; set; }
    
    public int Age { get; set; }
    
    public List<Movie> Movies { get; set; }

    public Actor()
    {
        Movies = new List<Movie>();
    }
}
