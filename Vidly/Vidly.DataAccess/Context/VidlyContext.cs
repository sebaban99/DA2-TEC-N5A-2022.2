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
            Console.WriteLine($"DIRECTORY {directory}");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(directory)
                .AddJsonFile("appsettings.json")
                .Build();


            var connectionString = configuration.GetConnectionString("VidlyDB");
            Console.WriteLine($"CONNECTION STRING {connectionString}");
            optionsBuilder.UseSqlServer(@"Server=127.0.0.1,1433; Database=VidlyDB; User=sa; Password=mystrongPassword1234$!");
        }
    }
}