using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class DBContext : DbContext
    {
        public DbSet<Title> Titles { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}