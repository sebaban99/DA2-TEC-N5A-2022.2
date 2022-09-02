using Vidly.WebApi.Domain;

namespace Vidly.WebApi.Models.Out;

public class ActorDetailModel
{
    public int Id { get; set; }

    public string Name { get; set; }
    
    public int Age { get; set; }
    
    public List<MovieBasicModel> Movies { get; set; }
    
    public ActorDetailModel(Actor actor)
    {
        Id = actor.Id;
        Name = actor.Name;
        Age = actor.Age;
        Movies = new List<MovieBasicModel>();

        foreach (var movie in actor.Movies)
        {
            Movies.Add(new MovieBasicModel(movie));
        }
    }
}