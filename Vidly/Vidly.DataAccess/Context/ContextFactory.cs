using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Vidly.DataAccess;

public enum ContextType { Memory, SQL }

public class ContextFactory : IDesignTimeDbContextFactory<VidlyContext>
{
   
    public VidlyContext CreateDbContext(string[] args)
    {
        return GetNewContext();
    }

    public static VidlyContext GetNewContext(ContextType type = ContextType.SQL)
    {
        var builder = new DbContextOptionsBuilder<VidlyContext>();
        DbContextOptions options = null;

        if (type == ContextType.Memory)
        {
            options = GetMemoryConfig(builder);
        }
        else
        {
            options = GetSqlConfig(builder);
        }

        return new VidlyContext(options);
    }

    private static DbContextOptions GetMemoryConfig(DbContextOptionsBuilder builder)
    {
        builder.UseInMemoryDatabase("VidlyDB");

        return builder.Options;
    }

    private static DbContextOptions GetSqlConfig(DbContextOptionsBuilder builder)
    {
        //Gets directory from startup project being used, NOT this class's path 
        var directory = Directory.GetCurrentDirectory();

        var configuration = new ConfigurationBuilder()
            .SetBasePath(directory)
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("VidlyDB");
        builder.UseSqlServer(connectionString);
        return builder.Options;
    }
}