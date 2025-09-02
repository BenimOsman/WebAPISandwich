using Microsoft.EntityFrameworkCore;                            // Provides DbContext, DbSet, and EF Core functionality

namespace WebAPISandwich.Model
{
    public class SandwichContext : DbContext                    // Inherits from DbContext to interact with the database
    {
        // Constructor accepts options and passes them to the base DbContext class
        public SandwichContext(DbContextOptions<SandwichContext> options) : base(options) { }

        public DbSet<Sandwich> Sandwiches { get; set; }         // Represents the Sandwich table in the database
    }
}