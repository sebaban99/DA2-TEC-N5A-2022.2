using Vidly.WebApi.Domain;

namespace Vidly.WebApi.Models.Out
{
    public class MovieBasicModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public MovieBasicModel(Movie movie)
        {
            this.Id = movie.Id;
            this.Name = movie.Title;
        }
    }
}
