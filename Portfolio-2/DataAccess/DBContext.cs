using System;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess
{
    public class DBContext : DbContext
    {
        public DbSet<Title> Titles { get; set; }

        public DbSet<PrimaryProfession> Professions { get; set; }

        public DbSet<TitlePrincipals> TitlePrincipals { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Name> Names { get; set; }
        
        public DbSet<SearchResult> SearchResults { get; set; }

        public DbSet<TitleGenres> TitleGenres { get; set; }

        public DbSet<LocalTitles> LocalTitle { get; set; }

        public DbSet<TitleRatings> TitleRatings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           // optionsBuilder.UseNpgsql();
           IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
               .AddJsonFile("config.json")
               .Build();
           //optionsBuilder.UseNpgsql("host=localhost;db=raw1;uid=postgres;pwd=postgres");
           optionsBuilder.UseNpgsql(configuration.GetConnectionString("RAW1DbContext"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("movie_data_model");
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
            modelBuilder.Entity<Title>().HasKey(x => new {x.Tconst}); //manuelt defineret

            modelBuilder.Entity<TitleGenres>().ToTable("title_genres");
            modelBuilder.Entity<TitleGenres>().Property(x => x.Tconst).HasColumnName("tconst");
            modelBuilder.Entity<TitleGenres>().Property(x => x.Genre).HasColumnName("genre");
            modelBuilder.Entity<TitleGenres>().HasKey(x => new {x.Tconst});

            modelBuilder.Entity<TitlePrincipals>().ToTable("title_principals");
            modelBuilder.Entity<TitlePrincipals>().Property(x => x.Tconst).HasColumnName("tconst");
            modelBuilder.Entity<TitlePrincipals>().Property(x => x.Nconst).HasColumnName("nconst");
            modelBuilder.Entity<TitlePrincipals>().Property(x => x.Ordering).HasColumnName("ordering");
            modelBuilder.Entity<TitlePrincipals>().Property(x => x.Category).HasColumnName("category");
            modelBuilder.Entity<TitlePrincipals>().Property(x => x.Job).HasColumnName("job");
            modelBuilder.Entity<TitlePrincipals>().Property(x => x.Characters).HasColumnName("characters");
            modelBuilder.Entity<TitlePrincipals>().HasKey(x => new {x.Tconst, x.Nconst});
     
          
            
            modelBuilder.Entity<Name>().ToTable("name");
            modelBuilder.Entity<Name>().Property(x => x.Nconst).HasColumnName("nconst");
            modelBuilder.Entity<Name>().Property(x => x.PrimaryName).HasColumnName("primaryname");
            modelBuilder.Entity<Name>().Property(x => x.BirthYear).HasColumnName("birthyear");
            modelBuilder.Entity<Name>().Property(x => x.DeathYear).HasColumnName("deathyear");
            modelBuilder.Entity<Name>().HasKey(x => new {x.Nconst}); //den her burde kun have en primary key så hvorfor skal den have den defineret?

            modelBuilder.Entity<TitleRatings>().ToTable("title_ratings");
            modelBuilder.Entity<TitleRatings>().Property(x => x.Tconst).HasColumnName("tconst");
            modelBuilder.Entity<TitleRatings>().Property(x => x.AverageRating).HasColumnName("averagerating");
            modelBuilder.Entity<TitleRatings>().Property(x => x.NumVotes).HasColumnName("numvotes");
            modelBuilder.Entity<TitleRatings>().HasKey(x => new {x.Tconst}); //manually defined

            modelBuilder.Entity<TitleEpisode>().ToTable("title_episode");
            modelBuilder.Entity<TitleEpisode>().Property(x => x.Tconst).HasColumnName("tconst");
            modelBuilder.Entity<TitleEpisode>().Property(x => x.ParentTconst).HasColumnName("parenttconst");
            modelBuilder.Entity<TitleEpisode>().Property(x => x.EpisodeNumber).HasColumnName("episodenumber");
            modelBuilder.Entity<TitleEpisode>().Property(x => x.SeasonNumber).HasColumnName("seasonnumber");
            modelBuilder.Entity<TitleEpisode>().HasKey(x => new {x.Tconst, x.ParentTconst}); 

            modelBuilder.Entity<LocalTitles>().ToTable("local_title");
            modelBuilder.Entity<LocalTitles>().Property(x => x.TitleId).HasColumnName("titleid");
            modelBuilder.Entity<LocalTitles>().Property(x => x.Ordering).HasColumnName("ordering");
            modelBuilder.Entity<LocalTitles>().Property(x => x.Title).HasColumnName("title");
            modelBuilder.Entity<LocalTitles>().Property(x => x.Region).HasColumnName("region");
            modelBuilder.Entity<LocalTitles>().Property(x => x.Language).HasColumnName("language");
            modelBuilder.Entity<LocalTitles>().Property(x => x.Types).HasColumnName("types");
            modelBuilder.Entity<LocalTitles>().Property(x => x.Attributes).HasColumnName("attributes");
            modelBuilder.Entity<LocalTitles>().Property(x => x.IsOriginalTitle).HasColumnName("isoriginaltitle");
            modelBuilder.Entity<LocalTitles>().HasKey(x => new {x.TitleId});
            //modelBuilder.Entity<LocalTitles>().HasKey(x => new {x.TitleId, x.Ordering, x.Region});

            modelBuilder.Entity<User>().ToTable("user");
            modelBuilder.Entity<User>().Property(x => x.Uconst).HasColumnName("uconst");
            modelBuilder.Entity<User>().Property(x => x.FirstName).HasColumnName("firstname");
            modelBuilder.Entity<User>().Property(x => x.LastName).HasColumnName("lastname");
            modelBuilder.Entity<User>().Property(x => x.Email).HasColumnName("email");
            modelBuilder.Entity<User>().Property(x => x.Password).HasColumnName("password");
            modelBuilder.Entity<User>().Property(x => x.UserName).HasColumnName("username");
            modelBuilder.Entity<User>().HasKey(x => new {x.Uconst});

            modelBuilder.Entity<SearchResult>().HasNoKey();
            modelBuilder.Entity<SearchResult>().Property(x => x.Tconst).HasColumnName("tconst");
            modelBuilder.Entity<SearchResult>().Property(x => x.PrimaryTitle).HasColumnName("primarytitle");

            modelBuilder.Entity<PrimaryProfession>().ToTable("primaryprofession");
            modelBuilder.Entity<PrimaryProfession>().Property(x => x.Nconst).HasColumnName("nconst");
            modelBuilder.Entity<PrimaryProfession>().Property(x => x.Profession).HasColumnName("profession");
            modelBuilder.Entity<PrimaryProfession>().HasKey(x => new{x.Nconst});
        }
        
        
    }
}