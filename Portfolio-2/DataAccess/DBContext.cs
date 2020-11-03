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
            modelBuilder.Entity<Title>().ToTable("title");
            modelBuilder.Entity<Title>().Property(x => x.Tconst).HasColumnName("tconst");
            modelBuilder.Entity<Title>().Property(x => x.Titletype).HasColumnName("titletype");
            modelBuilder.Entity<Title>().Property(x => x.PrimaryTitle).HasColumnName("primarytitle");
            modelBuilder.Entity<Title>().Property(x => x.OriginalTitle).HasColumnName("originaltitle");
            modelBuilder.Entity<Title>().Property(x => x.IsAdult).HasColumnName("isadult");
            modelBuilder.Entity<Title>().Property(x => x.StartYear).HasColumnName("startyear");
            modelBuilder.Entity<Title>().Property(x => x.EndYear).HasColumnName("endyear");
            modelBuilder.Entity<Title>().Property(x => x.RunTimeMinutes).HasColumnName("runtimeminutes");
            modelBuilder.Entity<Title>().Property(x => x.Poster).HasColumnName("poster");
            modelBuilder.Entity<Title>().Property(x => x.Awards).HasColumnName("awards");
            modelBuilder.Entity<Title>().Property(x => x.Plot).HasColumnName("plot");
        }
    }
}