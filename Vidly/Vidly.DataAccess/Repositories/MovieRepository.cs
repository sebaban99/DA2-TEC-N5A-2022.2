using Vidly.Domain.Entities;

namespace Vidly.DataAccess;

public class MovieRepository : BaseRepository<Movie>
{
    public MovieRepository(VidlyContext context) : base(context) { }
}