using System.ComponentModel.DataAnnotations;
using Vidly.Exceptions;

namespace Vidly.Domain.Entities;

public class Movie
{
    public int Id { get; set; }
    
    public string Title { get; set; } = "DefaultTitle";

    public string Description { get; set; } = "DefaultDesc";

    public void ValidOrFail()
    {
        if (string.IsNullOrEmpty(Title) || string.IsNullOrEmpty(Description))
            throw new InvalidResourceException("Title or description empty");
    }
}