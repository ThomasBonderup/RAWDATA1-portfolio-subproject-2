using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class DBContext : DbContext
    {
        public DbSet<Title> Titles { get; set; }

        public DbSet<Name> Names { get; set; }

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
            //modelBuilder.Entity<Title>().Property(x => x.RunTimeMinutes).HasColumnName("runtimeminutes");
            modelBuilder.Entity<Title>().Property(x => x.Poster).HasColumnName("poster");
            modelBuilder.Entity<Title>().Property(x => x.Awards).HasColumnName("awards");
            modelBuilder.Entity<Title>().Property(x => x.Plot).HasColumnName("plot");

            modelBuilder.Entity<Name>().ToTable("name");
            modelBuilder.Entity<Name>().Property(x => x.Nconst).HasColumnName("nconst");
            modelBuilder.Entity<Name>().Property(x => x.PrimaryName).HasColumnName("primaryname");
            modelBuilder.Entity<Name>().Property(x => x.BirthYear).HasColumnName("birthyear");
            modelBuilder.Entity<Name>().Property(x => x.DeathYear).HasColumnName("deathyear");

            modelBuilder.Entity<TitleRatings>().ToTable("title_ratings");
            modelBuilder.Entity<TitleRatings>().Property(x => x.Tconst).HasColumnName("tconst");
            modelBuilder.Entity<TitleRatings>().Property(x => x.AverageRating).HasColumnName("averagerating");
            modelBuilder.Entity<TitleRatings>().Property(x => x.NumVotes).HasColumnName("numvotes");

            modelBuilder.Entity<TitleEpisode>().ToTable("title_episode");
            modelBuilder.Entity<TitleEpisode>().Property(x => x.Tconst).HasColumnName("tconst");
            modelBuilder.Entity<TitleEpisode>().Property(x => x.ParentTconst).HasColumnName("parenttconst");
            modelBuilder.Entity<TitleEpisode>().Property(x => x.EpisodeNumber).HasColumnName("episodenumber");
            modelBuilder.Entity<TitleEpisode>().Property(x => x.SeasonNumber).HasColumnName("seasonnumber");

            modelBuilder.Entity<LocalTitles>().ToTable("local_titles");
            modelBuilder.Entity<LocalTitles>().Property(x => x.TitleId).HasColumnName("titleid");
            modelBuilder.Entity<LocalTitles>().Property(x => x.Ordering).HasColumnName("ordering");
            modelBuilder.Entity<LocalTitles>().Property(x => x.Title).HasColumnName("title");
            modelBuilder.Entity<LocalTitles>().Property(x => x.Region).HasColumnName("region");
            modelBuilder.Entity<LocalTitles>().Property(x => x.Language).HasColumnName("language");
            modelBuilder.Entity<LocalTitles>().Property(x => x.Types).HasColumnName("types");
            modelBuilder.Entity<LocalTitles>().Property(x => x.Attributes).HasColumnName("attributes");
            modelBuilder.Entity<LocalTitles>().Property(x => x.IsOriginalTitle).HasColumnName("isoriginaltitle");
        }
        
        
    }
}