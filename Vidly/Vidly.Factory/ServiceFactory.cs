using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Vidly.BusinessLogic;
using Vidly.DataAccess;
using Vidly.Domain.Entities;
using Vidly.IBusinessLogic;
using Vidly.IDataAccess;

namespace Vidly.Factory;

public static class ServiceFactory
{
    public static void RegisterBusinessLogicServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IMovieService, MovieService>();
    }
    
    public static void RegisterDataAccessServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IRepository<Movie>, MovieRepository>();
        serviceCollection.AddDbContext<DbContext, VidlyContext>();
    }
}