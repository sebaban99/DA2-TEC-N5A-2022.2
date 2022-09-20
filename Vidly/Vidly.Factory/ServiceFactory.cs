using Microsoft.Extensions.DependencyInjection;
using Vidly.BusinessLogic;
using Vidly.DataAccess;
using Vidly.DataAccess.Contexts;
using Vidly.Domain.Entities;
using Vidly.IBusinessLogic;
using Vidly.IDataAccess;

namespace Vidly.Factory;

public class ServiceFactory
{
    public void RegisterServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IMovieManager, MovieManager>();
        serviceCollection.AddTransient<IRepository<Movie>, BaseRepository<Movie>>();
        serviceCollection.AddTransient<IRepository<Actor>, ActorRepository>();
        serviceCollection.AddDbContext<VidlyContext>();
    }
}