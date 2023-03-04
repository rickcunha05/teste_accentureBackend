using Microsoft.EntityFrameworkCore;

namespace CrudAspAccenture.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Company> Company { get; set; }
        public DbSet<Suppliers> Suppliers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           optionsBuilder.UseSqlServer("Data Source=GodRick;Initial Catalog=CrudAspAccenture;Integrated Security=True; TrustServerCertificate=True");
        }
    }
}
