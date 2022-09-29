using Vidly.Domain.Entities;
using Vidly.ImporterInterface;

namespace Vidly.XMLImporter;

// Este proyecto podria estar en otra solucion tranquilamente, solo necesito el dll
// que resulta de compilar el proyecto para ponerlo en la carpeta Importers
public class XMLImporter : IImporter
{
    public string GetName()
    {
        return "XML Importer";
    }

    // Aca obviamente va a leer de un JSON, no devolver algo hardcodeado
    public List<Movie> ImportMovies()
    {
        return new List<Movie>() { new Movie() { Id = 2, Title = "Movie desde XML Importer", Description = "desc" } };
    }
}