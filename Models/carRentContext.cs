using Microsoft.EntityFrameworkCore;

namespace lab5.Models
{
    public class carRentContext:DbContext
    {
        public carRentContext(DbContextOptions<carRentContext> options): base(options)
        {
        Database.EnsureCreated();
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Document> Documents { get; set; }
    }
}
