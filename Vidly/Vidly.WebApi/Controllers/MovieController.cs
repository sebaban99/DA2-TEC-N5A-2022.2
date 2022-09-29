using Microsoft.AspNetCore.Mvc;
using Vidly.IBusinessLogic;
using Vidly.Models.In;
using Vidly.Models.Out;
using Vidly.WebApi.Filters;

namespace Vidly.WebApi.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieManager _movieManager;
        private readonly IImporterManager _importerManager;
 
        public MovieController(IMovieManager manager, IImporterManager importerManager)
        {
            _movieManager = manager;
            _importerManager = importerManager;
        }

        // Index - Get all movies (/api/movies)
        [HttpGet]
        public IActionResult GetMovies([FromQuery] MovieSearchCriteriaModel searchCriteria)
        {
            var retrievedMovies = _movieManager.GetAllMovies(searchCriteria.ToEntity());
            return Ok(retrievedMovies.Select(m => new MovieBasicModel(m)));
        }

        // Show - Get specific movie (/api/movies/{id})
        [HttpGet("{movieId}", Name = "GetMovie")]
        public IActionResult GetMovie(int movieId)
        {
            var retrievedMovie = _movieManager.GetSpecificMovie(movieId);
            return Ok(new MovieDetailModel(retrievedMovie));
        }

        // Create - Create new movie (/api/movies)
        [HttpPost]
        public IActionResult CreateMovie([FromBody] MovieModel newMovie)
        {
            var createdMovie = _movieManager.CreateMovie(newMovie.ToEntity());
            var movieModel = new MovieDetailModel(createdMovie);
            return CreatedAtRoute("GetMovie", new { movieId = movieModel.Id }, movieModel);
        }

        // Update - Update specific movie (/api/movies/{id})
        [HttpPut("{movieId}")]
        public IActionResult Update(int movieId, [FromBody] MovieModel updatedMovie)
        {
            var retrievedMovie = _movieManager.UpdateMovie(movieId, updatedMovie.ToEntity());
            return Ok(new MovieDetailModel(retrievedMovie));
        }

        // Delete - Delete specific movie (/api/movies/{id})
        [HttpDelete("{movieId}")]
        public IActionResult Delete(int movieId)
        {
            _movieManager.DeleteMovie(movieId);
            return Ok();
        }
        
        // TODO: Pensar si es mejor moverlo a otro controller
        [HttpGet("importers")]
        public IActionResult GetImporters()
        {
            List<string> retrievedImporters = _importerManager.GetAllImporters();
            return Ok(retrievedImporters);
        }

        [HttpPost("import")]
        public IActionResult ImportMovies([FromBody] string importerName)
        {
            List<MovieBasicModel> importedMovies = _importerManager.ImportMovies(importerName).Select(pet => new MovieBasicModel(pet)).ToList();
            return Ok(importedMovies);
        }
    }
}
