using Vidly.Exceptions;
using Vidly.Domain.Entities;
using Vidly.Domain.SearchCriterias;
using Vidly.IBusinessLogic;

namespace Vidly.BusinessLogic;

public class MovieManager : IMovieManager
{
    private static List<Movie> _movies = new List<Movie>()
    {
        new Movie() { Id = 1, Title = "El conjuro", Description = "De terror" },
        new Movie() { Id = 2, Title = "Los minions", Description = "De comedia" }
    };

    public List<Movie> GetAllMovies(MovieSearchCriteria searchCriteria)
    {
        return _movies;
    }

    public Movie GetSpecificMovie(int id)
    {
        var movieSaved = _movies.FirstOrDefault(m => m.Id == id);

        if (movieSaved is null)
            throw new ResourceNotFoundException($"Could not find specified movie {id}");

        return movieSaved;
    }

    public Movie CreateMovie(Movie movie)
    {
        movie.ValidOrFail();
        movie.Id = _movies.Count() + 1;
        _movies.Add(movie);
        return movie;
    }

    public Movie UpdateMovie(int id, Movie updatedMovie)
    {
        updatedMovie.ValidOrFail();
        var movieSaved = _movies.FirstOrDefault(m => m.Id == id);

        if (movieSaved is null)
            throw new ResourceNotFoundException($"Could not find specified movie {id}");

        movieSaved.Description = updatedMovie.Description;
        movieSaved.Title = updatedMovie.Title;

        return movieSaved;
    }

    public void DeleteMovie(int id)
    {
        var movieSaved = _movies.FirstOrDefault(m => m.Id == id);

        if (movieSaved is null)
            throw new ResourceNotFoundException($"Could not find specified movie {id}");

        _movies.Remove(movieSaved);
    }
}