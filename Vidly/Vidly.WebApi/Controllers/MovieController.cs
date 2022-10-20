using Vidly.WebApi.Domain;
using Microsoft.AspNetCore.Mvc;
using Vidly.WebApi.Models.In;
using Vidly.WebApi.Models.Out;

namespace Vidly.WebApi.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private static List<Movie> _movies = new List<Movie>()
        {
            new Movie() { Id = 1, Title = "El conjuro", Description = "De terror" },
            new Movie() { Id = 2, Title = "Minions", Description = "Amarillo" },
            new Movie() { Id = 3, Title = "El señor de los anillos", Description = "Un solo anillo" }
        };
        private static List<Actor> _actors = new List<Actor>()
        {
            new Actor() { Id = 1, Name = "Brad Pitt", Age = 50 },
            new Actor() { Id = 2, Name = "Angelina Jolie", Age = 50 }
        };

        public MovieController()
        {
            _movies.ForEach(m => m.Actors = new List<Actor>(_actors));
            _actors.ForEach(a => a.Movies = new List<Movie>(_movies));
        }

        // Index - Get all movies (/api/movies)
        [HttpGet]
        public IActionResult GetMovies([FromQuery] MovieSearchCriteria searchCriteria)
        {
            var filteredMovies = _movies.Where(searchCriteria.Criteria);
            var movieModels = filteredMovies.Select(m => new MovieBasicModel(m));
            
            return Ok(movieModels);
        }

        // Show - Get specific movie (/api/movies/{id})
        [HttpGet("{id}", Name = "GetMovie")]
        public IActionResult GetMovie(int id)
        {
            var movieSaved = _movies.FirstOrDefault(m => m.Id == id);

            if (movieSaved == null)
            {
                return NotFound(new
                {
                    error = "MovieNotFound",
                    message = "The movie doesn't exist"
                });
            }

            return Ok(new MovieDetailModel(movieSaved));
        }

        // Create - Create new movie (/api/movies)
        [HttpPost]
        public IActionResult CreateMovie([FromBody] MovieModel newMovie)
        {
            if (string.IsNullOrEmpty(newMovie.Title) || string.IsNullOrEmpty(newMovie.Description))
            {
                return BadRequest(new
                {
                    error = "IncompleteMovie",
                    message = "Missing information for the movie, please refer to the documentation of the API to see the requiered properties."
                });
            }

            var existMovie = _movies.Any(m => m.Title == newMovie.Title);

            if (existMovie)
            {
                // 400
                return BadRequest(new
                {
                    error = "DuplicateMovieName",
                    message = "The movie name all ready exist"
                });
            }

            var movieToSave = new Movie
            {
                Id = _movies.Count() + 1,
                Title = newMovie.Title,
                Description = newMovie.Description
            };

            _movies.Add(movieToSave);

            return CreatedAtRoute("GetMovie", new { id = movieToSave.Id }, movieToSave);
        }

        // Update - Update specific movie (/api/movies/{id})
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] MovieModel updatedMovie)
        {
            if (string.IsNullOrEmpty(updatedMovie.Title) || string.IsNullOrEmpty(updatedMovie.Description))
            {
                return BadRequest(new
                {
                    error = "IncompleteMovie",
                    message = "Missing information for the movie, please refer to the documentation of the API to see the requiered properties."
                });
            }

            var movieSaved = _movies.FirstOrDefault(m => m.Id == id);

            if (movieSaved == null)
            {
                return NotFound(new
                {
                    error = "MovieNotFound",
                    message = "The movie doesn't exist"
                });
            }

            // Workaround - como no puedo editar el elemento directamente en List, lo elimino y lo vuelvo a insertar actualizado
            var newMovie = new Movie()
            {
                Id = movieSaved.Id, Description = updatedMovie.Description,
                Title = updatedMovie.Title
            };

            _movies.Remove(movieSaved);
            _movies.Add(newMovie);
         
            return Ok(newMovie);
        }

        // Delete - Delete specific movie (/api/movies/{id})
        [HttpDelete("{id}")]
        public IActionResult Update(int id)
        {
            var movieSaved = _movies.FirstOrDefault(m => m.Id == id);

            if (movieSaved == null)
            {
                return NotFound(new
                {
                    error = "MovieNotFound",
                    message = "The movie doesn't exist"
                });
            }

            _movies.Remove(movieSaved);
            
            return Ok();
        }
    }
}
