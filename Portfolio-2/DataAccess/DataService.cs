using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.IO.Enumeration;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security;
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

        public Title CreateTitle(string titleType, string primaryTitle, string originalTitle, bool isAdult,
            string startYear, string endYear,
            int? runtimeMinutes, string poster, string awards, string plot)
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

        public bool UpdateTitle(string tconst, string titleType, string primaryTitle, string originalTitle,
            bool isAdult, string startYear, string endYear,
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

        public IList<TitlePrincipals> GetTitlePrincipalsByTitle(string tconst, int page, int pageSize)
        {

            var result = ctx.TitlePrincipals
                .FromSqlInterpolated($"SELECT * FROM movie_data_model.title_principals WHERE tconst = {tconst}")
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();
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

        public bool UpdateUser(string uconst, string FirstName, string LastName, string Email, string Password,
            string UserName)
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

        public IList<Name> GetNames(int page, int pageSize) //se om denne kan bruges til primary profession?
        {
            return ctx.Names
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

        public bool DeleteName(string nconst)
        {
            var name = ctx.Names.Find(nconst);

            if (name != null)
            {
                ctx.Names.Remove(name);
                return true;
            }

            return false;
        }

        public bool UpdateName(string nconst, string primaryName, string birthYear, string deathYear)
        {
            var name = GetName(nconst);
            if (name == null)
            {
                return false;
            }

            name.PrimaryName = primaryName;
            name.BirthYear = birthYear;
            name.DeathYear = deathYear;

            return true;
        }

        public IList<PrimaryProfession> GetProfessions(string nconst)
        {
            
            IList<PrimaryProfession> result = new List<PrimaryProfession>();

            foreach (var p in ctx.PrimaryProfessions)
            {
                if (p.Nconst.Trim() == nconst)
                {
                    result.Add(p);
                }
                
            }

            return result;
        }

       

        //pagination use
        public int NumberOfTitles()
        {
            return ctx.Titles.Count();
        }
        
        public int NumberOfTitlePrincipals()
        {
            return ctx.TitlePrincipals.Count();
        }
        
        public int NumberOfUsers()
        {
            return ctx.Users.Count();
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
                var trimmedTconst = tconst.Remove(0, 2);
                int intTconst = Int32.Parse(trimmedTconst);

                if (intTconst >= maxTconstInt)
                {
                    maxTconstInt = intTconst;
                }
            }

            maxTconstInt += 1;
            var stringTconst = "tt" + maxTconstInt;

            return stringTconst;
        }
        
        public string AssignMaxNconst()
        {

            var maxNconstInt = 0;
            
            foreach (var name in ctx.Names)
            {

                var nconst = name.Nconst;
                var trimmedNconst = nconst.Remove(0, 2);
                int intNconst = Int32.Parse(trimmedNconst);

                if (intNconst > maxNconstInt)
                {
                    maxNconstInt = intNconst;
                }
            }
            maxNconstInt++;
            var stringNconst = "tt" + maxNconstInt.ToString();

            return stringNconst;
        }

        public IList<Title> SearchTitles(string searchString, string uConst, int page, int pageSize)
        {
            var result = ctx.Titles
                .FromSqlInterpolated($"SELECT * FROM movie_data_model.string_search({searchString}, {uConst})")
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();
            return result;
        }
        
        public IList<TitlePrincipals> SearchTitlePrincipals(string searchString, string uConst, int page, int pageSize)
        {
            var result = ctx.TitlePrincipals
                .FromSqlInterpolated($"SELECT * FROM movie_data_model.actor_search({searchString}, {uConst})")
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();
            return result;
        }

        public IList<RatingHistory> GetAllRatingHistory(string uconst, string tconst)
        {
            IList<RatingHistory> result = new List<RatingHistory>();
            
            foreach (var rh in ctx.RatingHistories)
            {
                if (rh.Uconst.Trim() == uconst && rh.Tconst.Trim() == tconst)
                {
                 result.Add(rh);   
                }
            }

            return result;
        }

        public IList<RatingHistory> GetRatingHistory(string uconst)
        {
            var result = new List<RatingHistory>();

            foreach (var rh in ctx.RatingHistories)
            {
                if (rh.Uconst.Trim() == uconst)
                {
                    result.Add(rh);
                }
            }
            return result;
        }

        public IList<RatingByUser> GetRatings(string uconst)
        {
            var result = new List<RatingByUser>();

            foreach (var rating in ctx.RatingsByUser)
            {
                if (rating.Uconst.Trim() == uconst)
                {
                    result.Add(rating);
                }
            }
            return result;
        }

        public NameRating GetNameRating(string nconst)
        {
            var result = ctx.NameRatings.Find(nconst);
            return result;
        }

        public RatingByUser GetRating(string uconst, string tconst)
        {

            var result = ctx.RatingsByUser.Find(uconst, tconst);
            return result;

        }

        public IList<TitlePrincipals> GetTitlePricipalsByName(string nconst)
        {
            List<TitlePrincipals> result = new List<TitlePrincipals>();

            foreach (var tpbn in ctx.TitlePrincipals)
            {
                if (tpbn.Nconst.Trim() == nconst)
                { 
                    result.Add(tpbn); 
                }
            }

            return result;
        }

        // ------------------------------- title bookmark -----------------------------
        public IList<TitleBookmark> GetTitleBookmarks(string uconst)
        {
            IList<TitleBookmark> result = new List<TitleBookmark>();
            foreach (var tb in ctx.TitleBookmarks)
            {
                if (tb.Uconst.Trim() == uconst)
                {
                    result.Add(tb);
                }
            }
            return result;
        }

        public TitleBookmark GetTitleBookmark(string uconst, string tconst)
        {
            return ctx.TitleBookmarks.Find(uconst, tconst);
        }

        public bool DeleteTitleBookmark(string uconst, string tconst)
        {
            var titleBookmark = ctx.TitleBookmarks.Find(uconst, tconst);
            if (titleBookmark == null)
            {
                return false;
            }
            ctx.TitleBookmarks.Remove(titleBookmark);
            return true;
        }
        
        public TitleBookmark CreateTitleBookmark(string uconst, string tconst)
        {
            var result = new TitleBookmark
            {
                Uconst = uconst,
                Tconst = tconst,
                Timestamp = DateTime.Now
            };
            ctx.TitleBookmarks.Add(result);
            return result;
        }
        
        // ------------------------------------- name bookmark -----------------------------------
        public IList<NameBookmark> GetNameBookmarks(string uconst)
        {
            IList<NameBookmark> result = new List<NameBookmark>();
            foreach (var tb in ctx.NameBookmarks)
            {
                if (tb.Uconst.Trim() == uconst)
                {
                    result.Add(tb);
                }
            }
            return result;
        }

        public NameBookmark GetNameBookmark(string uconst, string nconst)
        {
            return ctx.NameBookmarks.Find(uconst, nconst);
        }

        public bool DeleteNameBookmark(string uconst, string nconst)
        {
            var nameBookmark = ctx.NameBookmarks.Find(uconst, nconst);
            if (nameBookmark == null)
            {
                return false;
            }
            ctx.NameBookmarks.Remove(nameBookmark);
            return true;
        }
        
        public NameBookmark CreateNameBookmark(string uconst, string nconst)
        {
            var result = new NameBookmark
            {
                Uconst = uconst,
                Nconst = nconst,
                Timestamp = DateTime.Now
            };
            ctx.NameBookmarks.Add(result);
            return result;
        }

        public TitleRatings GetTitleRating(string tconst)
        {
            var result = ctx.TitleRatings.Find(tconst);
            return result;
        }

        public IList<TitleRatings> GetTitleRatings()
        {

            var result = ctx.TitleRatings;
            return result.ToList();

        }

        public IList<string> GetLocalTitle(string tconst)
        {
            
            IList<string> result = new List<string>();
            foreach (var lt in ctx.LocalTitles)
            {
                if (lt.TitleId.Trim() == tconst)
                {
                    result.Add(lt.Title);
                }
            }

            return result;
        }

        public IList<string> GetGenres(string tconst)
        {
            IList<string> result = new List<string>();
            
            foreach (var tg in ctx.TitleGenres)
            {
                if (tg.Tconst.Trim() == tconst)
                {
                    result.Add(tg.Genre);
                }
            }
            return result;
        }
        
        // ---------------------- name notes -----------------------
        public IList<NameNotes> GetNameNotes(string uconst)
        {
            IList<NameNotes> result = new List<NameNotes>();
            foreach (var nm in ctx.NameNotes)
            {
                if (nm.Uconst.Trim() == uconst)
                {
                    result.Add(nm);
                }
            }
            return result;
        }
        
        public NameNotes GetNameNote(string uconst, string nconst)
        {
            return ctx.NameNotes.Find(uconst, nconst);
        }
        
        public bool DeleteNameNote(string uconst, string nconst)
        {
            var note = ctx.NameNotes.Find(uconst, nconst);
            if (note == null)
            {
                return false;
            }
            ctx.NameNotes.Remove(note);
            return true;
        }
        
        public NameNotes CreateNameNote(string uconst, string nconst, string notes)
        {
            var response = new NameNotes()
            {
                Uconst = uconst,
                Nconst = nconst,
                Notes = notes
            };
            ctx.NameNotes.Add(response);
            return response;
        }

        public bool UpdateNameNote(string uconst, string nconst, string notes)
        {
            var note = GetNameNote(uconst, nconst);
            if (note == null)
            {
                return false;
            }
            note.Notes = notes;
            return true;
        }
        
        // ---------------------- title notes -----------------------
        public IList<TitleNotes> GetTitleNotes(string uconst)
        {
            IList<TitleNotes> result = new List<TitleNotes>();
            foreach (var nm in ctx.TitleNotes)
            {
                if (nm.Uconst.Trim() == uconst)
                {
                    result.Add(nm);
                }
            }
            return result;
        }
        
        public TitleNotes GetTitleNote(string uconst, string tconst)
        {
            return ctx.TitleNotes.Find(uconst, tconst);
        }
        
        public bool DeleteTitleNote(string uconst, string tconst)
        {
            var note = ctx.TitleNotes.Find(uconst, tconst);
            if (note == null)
            {
                return false;
            }
            ctx.TitleNotes.Remove(note);
            return true;
        }
        
        public TitleNotes CreateTitleNote(string uconst, string tconst, string notes)
        {
            var response = new TitleNotes()
            {
                Uconst = uconst,
                Tconst = tconst,
                Notes = notes
            };
            ctx.TitleNotes.Add(response);
            return response;
        }

        public bool UpdateTitleNote(string uconst, string tconst, string notes)
        {
            var note = GetTitleNote(uconst, tconst);
            if (note == null)
            {
                return false;
            }
            note.Notes = notes;
            return true;
        }
        

    }
}