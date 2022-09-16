using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Vidly.Domain.Entities;

namespace Vidly.DataAccess;

public class VidlyContext : DbContext
{
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Actor> Actors { get; set; }
    
    public VidlyContext(DbContextOptions options) : base(options) { }
    public VidlyContext() : base() { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var directory = Directory.GetCurrentDirectory();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(directory)
                .AddJsonFile("appsettings.json")
                .Build();


            var connectionString = configuration.GetConnectionString("VidlyDB");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}