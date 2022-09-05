using Microsoft.Extensions.DependencyInjection;
using Vidly.BusinessLogic;
using Vidly.IBusinessLogic;

namespace Vidly.Factory;

public class ServiceFactory
{
    public void RegisterServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IMovieManager, MovieManager>();
        // ......
    }
}