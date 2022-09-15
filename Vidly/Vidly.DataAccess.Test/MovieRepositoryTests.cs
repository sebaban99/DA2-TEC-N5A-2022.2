using Vidly.Domain.Entities;

namespace Vidly.DataAccess.Test;

[TestClass]
public class MovieRepositoryTests
{
    private MovieRepository _repository;
    private VidlyContext _context;

    [TestInitialize]
    public void SetUp()
    {
        _context = ContextFactory.GetNewContext(ContextType.Memory);
        _repository = new MovieRepository(_context);
    }

    [TestCleanup]
    public void CleanUp()
    {
        _context.Database.EnsureDeleted();
    }

    [TestMethod]
    public void GetAllMovies_DbWithMovies_ReturnsMatchingMovies()
    {
        LoadSeeds();
        var movies = CreateDummyMovies();

        var retrievedMovies = _repository.GetAllByExpression(m => m.Title.ToLower().Contains("el conjuro"));

        CollectionAssert.AreEquivalent(movies, retrievedMovies.ToList());
    }

    [TestMethod]
    public void InsertMovie_MovieNotExists_ReturnsVoid()
    {
        LoadSeeds();
        var actorInDb = _context.Actors.First();
        var newMovie = new Movie()
        {
            Title = "Interstellar",
            Description = "Muy buena",
            Actors = new List<Actor>() { actorInDb }
        };

        _repository.InsertOne(newMovie);
        _context.SaveChanges();

        var movieInDb = _context.Movies.First(m => m.Title.Equals(newMovie.Title));
        Assert.AreEqual(movieInDb, newMovie);
    }


    private void LoadSeeds()
    {
        var actorJuan = new Actor()
        {
            Name = "Juan Carlos"
        };
        var movies = CreateDummyMovies();
        movies.First().Actors.Add(actorJuan);
        actorJuan.Movies.Add(movies.First());

        movies.ForEach(m => _context.Movies.Add(m));
        _context.SaveChanges();
    }

    private List<Movie> CreateDummyMovies()
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
                Title = "El conjuro 8",
                Description = "mas de lo mismo"
            }
        };
    }
}