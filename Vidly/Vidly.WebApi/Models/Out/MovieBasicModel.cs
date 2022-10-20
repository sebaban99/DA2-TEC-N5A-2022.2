using Vidly.WebApi.Domain;

namespace Vidly.WebApi.Models.Out
{
    public class MovieBasicModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public MovieBasicModel(Movie movie)
        {
            Id = movie.Id;
            Title = movie.Title;
        }
    }
}
