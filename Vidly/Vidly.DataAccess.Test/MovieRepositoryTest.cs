using System.Linq.Expressions;
using Vidly.DataAccess.Contexts;
using Vidly.Domain.Entities;

namespace Vidly.DataAccess.Test;

[TestClass]
public class MovieRepository
{
    private BaseRepository<Movie> _repository;
    private VidlyContext _context;

    [TestInitialize]
    public void SetUp()
    {
        _context = ContextFactory.GetNewContext(ContextType.Memory);
        _repository = new BaseRepository<Movie>(_context);
    }

    [TestCleanup]
    public void CleanUp()
    {
        _context.Database.EnsureDeleted();
    }

    [TestMethod]
    public void GetAllMoviesReturnsAsExpected()
    {
        Expression<Func<Movie, bool>> expression = m => m.Title.ToLower().Contains("el conjuro");
        var movies = CreateMovies();
        var eligibleMovies = movies.Where(expression.Compile()).ToList();
        LoadMovies(movies);
     
        var retrievedMovies = _repository.GetAllBy(expression);
        CollectionAssert.AreEquivalent(eligibleMovies, retrievedMovies.ToList());
    }

    [TestMethod]
    public void InsertNewMovie()
    {
        var movies = CreateMovies();
        LoadMovies(movies);
        var newMovie = new Movie()
        {
            Title = "Interstellar",
            Description = "Muy buena",
        };
    
        _repository.Insert(newMovie);
        _repository.Save();
    
        // Voy directo al contexto a buscarla
        var movieInDb = _context.Movies.FirstOrDefault(m => m.Title.Equals(newMovie.Title));
        Assert.IsNotNull(movieInDb);
    }


    private void LoadMovies(List<Movie> movies)
    {
        movies.ForEach(m => _context.Movies.Add(m));
        _context.SaveChanges();
    }

    private List<Movie> CreateMovies()
    {
        return new List<Movie>()
        {
            new()
            {
                Title = "El conjuro 2",
                Description = "De terror",
            },
            new()
            {
                Title = "Minions",
                Description = "esta buena"
            }
        };
    }
}