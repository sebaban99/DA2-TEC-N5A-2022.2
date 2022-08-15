using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using Vidly.WebApi.Domain;
using Vidly.WebApi.Models;

namespace Vidly.WebApi.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IList<Movie> _movies;

        public MovieController()
        {
            this._movies = new List<Movie>();
        }

        [HttpGet("{movieId}", Name = "GetMovie")]
        public IActionResult GetMovie(int movieId)
        {
            var movieSaved = this._movies.FirstOrDefault(m => m.Id == movieId);

            if (movieSaved == null)
            {
                return NotFound(new
                {
                    error = "MovieNotFound",
                    message = "The movie doesn't exist"
                });
            }

            var movieDetail = new MovieDetailModel(movieSaved);

            return Ok(movieDetail);
        }

        [HttpGet]
        public IActionResult GetMovies()
        {
            var modelMovies = this._movies.Select(m => new MovieBasicModel(m));

            return Ok(modelMovies);
        }

        [HttpPost]
        public IActionResult CreateMovie(MovieModel newMovie)
        {
            if (string.IsNullOrEmpty(newMovie.Name) || string.IsNullOrEmpty(newMovie.Description))
            {
                return BadRequest(new
                {
                    error = "IncompleteMovie",
                    message = "Missing information for the movie, please refer to the documentation of the API to see the requiered properties."
                });
            }

            var existMovie = this._movies.Any(m => m.Title == newMovie.Name);

            if (existMovie)
            {
                return BadRequest(new
                {
                    error = "DuplicateMovieName",
                    message = "The movie name all ready exist"
                });
            }

            var movieToSave = new Movie
            {
                Id = this._movies.Count() + 1,
                Title = newMovie.Name,
                Description = newMovie.Description
            };

            this._movies.Add(movieToSave);
            var movieModel = new MovieDetailModel(movieToSave);

            return CreatedAtRoute("GetMovie", new { movieId = movieToSave.Id }, movieToSave);
        }
    }
}
