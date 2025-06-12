using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ElfshockRPG.Data.Models;

namespace ElfshockRPG.Data
{
    public class ElfshockRPGDbContext : DbContext
    {
        // Parameterless constructor - needed for some tools and scenarios
        public ElfshockRPGDbContext()
        {
            
        }

        // Constructor that accepts options
        public ElfshockRPGDbContext(DbContextOptions<ElfshockRPGDbContext> options) : base(options)
        {
            
        }

        // DbSet represents the Characters table in the database
        public DbSet<GameCharacter> Characters { get; set; }

        // Configures the database provider and connection string
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Only configure if it's not already configured
            if (!optionsBuilder.IsConfigured)
            {
                // Load configuration settings from json file
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        // Configure the model
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map GameCharacter entity to the table named "Characters"
            modelBuilder.Entity<GameCharacter>().ToTable("Characters");
        }
    }
}
