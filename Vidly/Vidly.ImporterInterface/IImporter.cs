using Vidly.Domain.Entities;

namespace Vidly.ImporterInterface;

// Teniendo esto separado de los otros proyectos, si queremos que un tercero
// nos desarrolle una implementacion de un nuevo importador no tengo porque compartirle
// todo el resto (business logic, domain etc)
public interface IImporter
{
    string GetName();

    // 2 Cosas a notar aqui:
    // - Hacemos referencia a Movie, por lo que deberiamos compartir el proyecto domain tambien a 
    // diferencia de lo que dijimos arriba, aca podemos crear otros DTOs para esto y usar el patron Adapter
    // - No recibimos parametros, estaria bueno representar los parametros de alguna forma bastante generica
    // para pasar info necesaria a cada importador
    List<Movie> ImportMovies();
    
    // Ej de posible manejo de parametros
    // List<Parameter> GetNeededParameters();
}