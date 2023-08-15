using App.Models;
using FirstProject.Models.Contacts;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FirstProject.Models
{
    public class AppDbContext : IdentityDbContext<AppUser>
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

            foreach(var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();

                if (tableName.Contains("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }
        }
    }
}
