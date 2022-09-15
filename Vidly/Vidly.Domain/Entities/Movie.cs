using System.ComponentModel.DataAnnotations;
using Vidly.Exceptions;

namespace Vidly.Domain.Entities;

public class Movie
{
    public int Id { get; set; }
    
    public string Title { get; set; } = "DefaultTitle";

    public string Description { get; set; } = "DefaultDesc";

    public List<Actor> Actors { get; set; } = new();

    public void ValidOrFail()
    {
        if (string.IsNullOrEmpty(Title) || string.IsNullOrEmpty(Description))
            throw new InvalidResourceException("Title or description empty");
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Movie)obj);
    }
    
    protected bool Equals(Movie other)
    {
        return Title == other?.Title;
    }

    public override int GetHashCode()
    {
        return Title.GetHashCode();
    }
}