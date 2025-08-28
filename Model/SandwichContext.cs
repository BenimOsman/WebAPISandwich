using Microsoft.EntityFrameworkCore;

namespace WebAPISandwich.Model
{
    public class SandwichContext : DbContext
    {
        public SandwichContext(DbContextOptions<SandwichContext> options) : base(options) { }

        public DbSet<Sandwich> Sandwiches { get; set; }
    }
}