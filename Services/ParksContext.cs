using lab5.Models;
using Microsoft.EntityFrameworkCore;

namespace lab5.Services
{
    public class ParksContext : DbContext
    {
        public ParksContext(DbContextOptions<ParksContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Park> Parks { get; set; }
        public DbSet<Planting> Plantings { get; set; }
        public DbSet<Fountain> Fountains { get; set; }
        public DbSet<Pavilion> Pavilions { get; set; }
    }
}
