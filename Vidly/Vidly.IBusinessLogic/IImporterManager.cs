using Vidly.Domain.Entities;

namespace Vidly.IBusinessLogic;

public interface IImporterManager
{
    List<string> GetAllImporters();
    List<Movie> ImportMovies(string importerName);
    
}