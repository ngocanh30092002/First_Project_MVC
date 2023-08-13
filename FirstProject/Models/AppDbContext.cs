using FirstProject.Models.Contacts;
using Microsoft.EntityFrameworkCore;

namespace FirstProject.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public  AppDbContext(DbContextOptions<AppDbContext> options) :base(options) { } 
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
