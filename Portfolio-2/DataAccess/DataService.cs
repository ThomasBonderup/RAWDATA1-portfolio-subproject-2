using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class DataService : IDataService
    {
        public DBContext ctx { get; set; }
        


        public DataService()
        {
            ctx = new DBContext();
        }

        
        /**
         *  TITLES
         */
        
        public Title CreateTitle(string titleType, string primaryTitle, string originalTitle, bool isAdult, string startYear, string endYear, 
            int runtimeMinutes, string poster, string awards, string plot)
        {
            var title = new Title
            {
                Tconst = AssignMaxTconst(),
                Titletype = titleType,
                PrimaryTitle = primaryTitle,
                OriginalTitle = originalTitle,
                IsAdult = isAdult,
                StartYear = startYear,
                EndYear = endYear,
                RunTimeMinutes = runtimeMinutes,
                Poster = poster,
                Awards = awards,
                Plot = plot
            };
            ctx.Titles.Add(title);
            return title;
        }

        public bool UpdateTitle(string tconst, string titleType, string primaryTitle, string originalTitle, bool isAdult, string startYear, string endYear,
            int? runtimeMinutes, string poster, string awards, string plot)
        {

            var title = ctx.Titles.Find(tconst);

            if (title == null || tconst.Length != 10 || !tconst.StartsWith("t"))
            {
                return false;
            }

            title.Titletype = titleType;
            title.PrimaryTitle = primaryTitle;
            title.OriginalTitle = originalTitle;
            title.IsAdult = isAdult;
            title.StartYear = startYear;
            title.EndYear = endYear;
            title.RunTimeMinutes = runtimeMinutes;
            title.Poster = poster;
            title.Awards = awards;
            title.Plot = plot;
            
            return true;
        }

    

        public bool DeleteTitle(string tconst)
        {
            var title = ctx.Titles.Find(tconst);

            if (title != null)
            {
                ctx.Titles.Remove(title);
                return true;
            }

            return false;
        }
        
      
        
        public IList<Title> GetTitles(int page, int pageSize)
        {
            return ctx.Titles.ToList()
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();
        }
        
        public Title GetTitle(string tconst)
        {
            var title = ctx.Titles.Find(tconst);

            if (title != null)
            {
                return title;
            }
            return null;
        }

        public TitlePrincipals GetTitlePrincipals(string tconst, string nconst)
        {
            var titleProncipals = ctx.TitlePrincipals.Find(tconst, nconst);

            if (titleProncipals != null)
            {
                return titleProncipals;
            }
            return null;
        }

        public IList<TitlePrincipals> GetTitlePrincipalsByTitle(string tconst)
        {
            IList<TitlePrincipals> result = new List<TitlePrincipals>();
            foreach (var titlePrincipal in ctx.TitlePrincipals)
            {
                if (titlePrincipal.Tconst == tconst)
                {
                    result.Add(titlePrincipal);
                    
                }
            }
            return result;
        }


        /**
         *  USER
         */
        
        public User CreateUser(string firstName, string lastName, string email, string password, string userName)
        {
            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Uconst = AssignMaxUconst(),
                Email = email,
                Password = password,
                UserName = userName
            };

            ctx.Users.Add(user);
            return user;
        }

        public bool DeleteUser(string uconst)
        {

            var user = ctx.Users.Find(uconst);

            if (user != null)
            {
                ctx.Users.Remove(user);
                return true;
            }

            return false;
        }
        
        public User GetUser(string uconst)
        {
            var user = ctx.Users.Find(uconst);
            
            if (user != null)
            {
                return user;
            }
            return null;
        }
        
        public IList<User> GetUsers(int page, int pageSize)
        {
            return ctx.Users.ToList()
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public bool UpdateUser(string uconst, string FirstName, string LastName, string Email, string Password, string UserName)
        {
            var user = GetUser(uconst);
            if (user == null)
            {
                return false;
            }

            user.FirstName = FirstName;
            user.LastName = LastName;
            user.Email = Email;
            user.Password = Password;
            user.UserName = UserName;
            
            return true;
        }

        /**
         * NAME 
         */
        
        public Name GetName(string nconst)
        {
            var name = ctx.Names.Find(nconst);

            if (name != null)
            {
                return name;
            }
            return null;
        }
        
        public IList<Name> GetNames(int page, int pageSize)
        {
            return ctx.Names.ToList()
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public Name CreateName(string PrimaryName, string Birthyear, string DeathYear)
        {
            var name = new Name()
            {
                Nconst = AssignMaxNconst(),
                PrimaryName = PrimaryName,
                BirthYear = Birthyear,
                DeathYear = DeathYear
            };

            ctx.Names.Add(name);
            
            return name;
        }

        public Name DeleteName(string nconst, string primaryName, string birthYear, string deathYear)
        {
            var name = new Name();

            return name;
        }



        public int NumberOfTitles()
        {
            return ctx.Titles.Count();
        }

        public int NumberOfNames()
        {
            return ctx.Names.Count();

        }

        
        public Title CreateTitle(string primaryTitle)
        {
            throw new System.NotImplementedException();
        }
        
        
        // Utils 
        
        public string AssignMaxUconst()
        {

            var maxUconstInt = 0;
            
            foreach (var user in ctx.Users)
            {

                var uconst = user.Uconst;
                var trimmedUconst = uconst.Remove(0, 2);
                int intUconst = Int32.Parse(trimmedUconst);

                if (intUconst > maxUconstInt)
                {
                    maxUconstInt = intUconst;

                }
            }
            maxUconstInt++;
            var stringUconst = "tt" + maxUconstInt.ToString();

            return stringUconst;
        }

        public string AssignMaxTconst()
        {

            int maxTconstInt = 0;
            
            foreach (var title in ctx.Titles)
            {
                var tconst = title.Tconst;
                var trimmedUconst = tconst.Remove(0, 2);
                int intUconst = Int32.Parse(trimmedUconst);

                if (intUconst >= maxTconstInt)
                {
                    maxTconstInt = intUconst;
                }
            }

            maxTconstInt += 1;
            var stringUconst = "tt" + maxTconstInt;

            return stringUconst;
        }
        
        public string AssignMaxNconst()
        {

            var maxNconstInt = 0;
            
            foreach (var name in ctx.Names)
            {

                var tconst = name.Nconst;
                var trimmedUconst = tconst.Remove(0, 2);
                int intUconst = Int32.Parse(trimmedUconst);

                if (intUconst > maxNconstInt)
                {
                    maxNconstInt = intUconst;
                }
            }
            maxNconstInt++;
            var stringUconst = "tt" + maxNconstInt.ToString();

            return stringUconst;
        }

        public IList<SearchResult> SearchTitles(string searchString, string uConst, int page, int pageSize)
        {
            var result = ctx.SearchResults
                .FromSqlInterpolated($"select * from string_search({searchString}, {uConst})")
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();
            return result;
        }
    }
}