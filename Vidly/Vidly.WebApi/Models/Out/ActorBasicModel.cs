using Vidly.WebApi.Domain;

namespace Vidly.WebApi.Models.Out;

public class ActorBasicModel
{
    public int Id { get; set; }

    public string Name { get; set; }
    
    public int Age { get; set; }

    public ActorBasicModel(Actor actor)
    {
        Id = actor.Id;
        Name = actor.Name;
        Age = actor.Age;
    }
}