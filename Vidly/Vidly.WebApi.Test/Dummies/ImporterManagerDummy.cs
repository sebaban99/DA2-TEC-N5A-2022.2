using Vidly.Domain.Entities;
using Vidly.IBusinessLogic;

namespace Vidly.WebApi.Test.Dummies;

public class ImporterManagerDummy : IImporterManager
{
    public List<string> GetAllImporters()
    {
        throw new NotImplementedException();
    }

    public List<Movie> ImportMovies(string importerName)
    {
        throw new NotImplementedException();
    }
}