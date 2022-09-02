using Vidly.WebApi.Domain;

namespace Vidly.WebApi.Models.Out
{
    public class MovieDetailModel
    {
        public int Id { get; set; }

        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public List<ActorBasicModel> Actors { get; set; }

        public MovieDetailModel(Movie movie)
        {
            Id = movie.Id;
            Title = movie.Title;
            Description = movie.Description;
            Actors = new List<ActorBasicModel>();

            foreach (var actor in movie.Actors)
            {
                Actors.Add(new ActorBasicModel(actor));
            }
        }
    }
}
