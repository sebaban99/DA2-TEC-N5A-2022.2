using Vidly.Domain.Entities;
using Vidly.ImporterInterface;

namespace Vidly.JSONImporter;

// Este proyecto podria estar en otra solucion tranquilamente, solo necesito el dll
// que resulta de compilar el proyecto para ponerlo en la carpeta Importers
public class JSONImporter : IImporter
{
    public string GetName()
    {
        return "Json Importer";
    }

    // Aca obviamente va a leer de un JSON, no devolver algo hardcodeado
    public List<Movie> ImportMovies()
    {
        return new List<Movie>() { new Movie() { Id = 1, Title = "Movie desde JSON Importer", Description = "desc" } };
    }
}