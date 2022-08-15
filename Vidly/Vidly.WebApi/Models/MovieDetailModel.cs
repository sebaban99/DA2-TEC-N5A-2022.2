using Vidly.WebApi.Domain;

namespace Vidly.WebApi.Models
{
    public class MovieDetailModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Description { get; set; }

        public MovieDetailModel(Movie movie)
        {
            this.Id = movie.Id;
            this.Name = movie.Title;
            this.Description = movie.Description;
        }
    }
}
