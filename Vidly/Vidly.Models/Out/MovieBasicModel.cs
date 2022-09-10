using Vidly.Domain.Entities;

namespace Vidly.Models.Out
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
        
        public override bool Equals(object? obj)
        {
            if (obj is MovieBasicModel)
            {
                var otherMovie = obj as MovieBasicModel;

                return Id == otherMovie.Id;
            }
            else
            {
                return false;
            }
        }
    }
}
