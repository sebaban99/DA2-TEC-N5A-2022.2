using System.Linq.Expressions;
using Vidly.Exceptions;
using Vidly.Domain.Entities;
using Vidly.Domain.SearchCriterias;
using Vidly.IBusinessLogic;
using Vidly.IDataAccess;

namespace Vidly.BusinessLogic;

public class MovieService : IMovieService
{
    private readonly IRepository<Movie> _movieRepository;
    
    public MovieService(IRepository<Movie> movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public List<Movie> GetAllMovies(MovieSearchCriteria searchCriteria)
    {
        var titleCriteria = searchCriteria.Title?.ToLower() ?? string.Empty;
        var descriptionCritera = searchCriteria.Description?.ToLower() ?? string.Empty;
        
        Expression<Func<Movie, bool>> moviesFilter = movie =>
            movie.Title.ToLower().Contains(titleCriteria) &&
            movie.Title.ToLower().Contains(titleCriteria);
        
        return _movieRepository.GetAllByExpression(moviesFilter).ToList();
    }

    public Movie GetSpecificMovie(int id)
    {
        var movieSaved = _movieRepository.GetOneByExpression(m => m.Id == id);

        if (movieSaved is null)
            throw new ResourceNotFoundException($"Could not find specified movie {id}");

        return movieSaved;
    }

    public Movie CreateMovie(Movie movie)
    {
        movie.ValidOrFail();
        
        _movieRepository.InsertOne(movie);
        _movieRepository.Save();
        
        return movie;
    }

    public Movie UpdateMovie(int id, Movie updatedMovie)
    {
        updatedMovie.ValidOrFail();
        
        var movieStored = GetSpecificMovie(id);
        
        movieStored.Description = updatedMovie.Description;
        movieStored.Title = updatedMovie.Title;
        
        _movieRepository.UpdateOne(movieStored);
        _movieRepository.Save();
        
        return movieStored;
    }

    public void DeleteMovie(int id)
    {
        var movieStored = GetSpecificMovie(id);
        
        _movieRepository.DeleteOne(movieStored);
        _movieRepository.Save();
    }
}