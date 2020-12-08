using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.IO.Enumeration;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security;
using Microsoft.EntityFrameworkCore;
using Npgsql;

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
            string startYear, string endYear, int? runtimeMinutes, string poster, string awards, string plot)
        {
            var newTconst = AssignMaxTconst();
            var newTitle = new Title
            {
                Tconst = newTconst,
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
            ctx.Titles.Add(newTitle);
            //ctx.Database.ExecuteSqlInterpolated($"INSERT INTO movie_data_model.title VALUES ({newTconst},{titleType},{primaryTitle},{originalTitle},{isAdult},{startYear},{endYear},{runtimeMinutes},{poster},{awards},{plot})");
            ctx.SaveChanges();
            return GetTitle(newTconst);
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
            ctx.SaveChanges();
            return true;
        }



        public bool DeleteTitle(string tconst)
        {
            var title = ctx.Titles.Find(tconst);
            if (title == null)
            {
                return false;
            }
            //ctx.Database.ExecuteSqlInterpolated($"DELETE FROM movie_data_model.title WHERE title.tconst = {tconst}");
            ctx.Titles.Remove(title);
            ctx.SaveChanges();
            return true;
        }



        public IList<Title> GetTitles(int page, int pageSize)
        {
            // Reflection: Async and await if we have time after the frontend is created
            // TODO Create new db context for each method call
            // if not working send email to Henrik include db script so he can recreate db
            using var ctx1 = new DBContext();
            return ctx1.Titles.ToList()
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

        public TitlePrincipals GetTitlePrincipal(string tconst, string nconst)
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
            
            using var ctx1 = new DBContext();
            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Uconst = AssignMaxUconst(),
                Email = email,
                Password = password,
                UserName = userName
            };

            ctx1.Users.Add(user);
            ctx1.SaveChanges();
            return user;
        }

        public bool DeleteUser(string uconst)
        {

            using var ctx1 = new DBContext();
            
            var user = ctx1.Users.Find(uconst);

            if (user != null)
            {
                ctx1.Users.Remove(user);
                ctx1.SaveChanges();
                return true;
            }

            return false;
        }

        public User GetUser(string uconst)
        {
            using var ctx1 = new DBContext();
            var user = ctx1.Users.Find(uconst);

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

        public bool UpdateUser(string uconst, string firstName, string lastName, string email, 
            string password, string userName)
        {
            var user = ctx.Users.Find(uconst);
            if (user == null)
            {
                return false;
            }

            user.FirstName = firstName;
            user.LastName = lastName;
            user.Email = email;
            user.Password = password;
            user.UserName = userName;
            ctx.SaveChanges();
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

        public Name CreateName(string primaryName, string birthYear, string deathYear)
        {
            var name = new Name()
            {
                Nconst = AssignMaxNconst(),
                PrimaryName = primaryName,
                BirthYear = birthYear,
                DeathYear = deathYear
            };

            ctx.Names.Add(name);
            ctx.SaveChanges();
            return name;
        }

        public bool DeleteName(string nconst)
        {
            var name = ctx.Names.Find(nconst);

            if (name != null)
            {
                ctx.Names.Remove(name);
                ctx.SaveChanges();
                return true;
            }

            return false;
        }

        public bool UpdateName(string nconst, string primaryName, string birthYear, string deathYear)
        {
            var name = ctx.Names.Find(nconst);
            if (name == null)
            {
                return false;
            }

            name.PrimaryName = primaryName;
            name.BirthYear = birthYear;
            name.DeathYear = deathYear;
            ctx.SaveChanges();
            
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

        public IList<KnownForTitle> GetKnownForTitles(string nconst)
        {
            var result = new List<KnownForTitle>();

            foreach (var kft in ctx.KnownForTitles)
            {
                if (kft.Nconst.Trim() == nconst)
                {
                    result.Add(kft);
                }
            }
            return result;
        }
        
        /*
         * title principals
         */
        public IList<TitlePrincipals> GetTitlePrincipalsList(int page, int pageSize)
        {
            return ctx.TitlePrincipals.ToList()
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();
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

        public int NumberOfSearchResults()
        {
            return ctx.SearchResults.Count();
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
            var stringUconst = "ui" + maxUconstInt.ToString();

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
            var stringNconst = "nm" + maxNconstInt.ToString();

            return stringNconst;
        }

        public bool IsValidTconst(string tconst)
        {
            var title = ctx.Titles.Find(tconst);
            if (title != null)
            {
                return true;
            }
            return false;
        }
        
        public bool IsValidUconst(string uconst)
        {
            var title = ctx.Users.Find(uconst);
            if (title != null)
            {
                return true;
            }
            return false;
        }
        
        public bool IsValidNconst(string nconst)
        {
            var title = ctx.Names.Find(nconst);
            if (title != null)
            {
                return true;
            }
            return false;
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
        
        // TODO: Create insert userid and search string to search history for dynamic search with variadic array
        public IList<SearchResult> SearchDynamicBestMatch(string searchString, int page, int pageSize)
        {
            var result = ctx.SearchResults
                .FromSqlInterpolated($"SELECT * FROM movie_data_model.dynamic_bestmatch({searchString})")
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

        public NameRating GetNameRating(string nconst)
        {
            var result = ctx.NameRatings.Find(nconst);
            return result;
        }
        
        // ratings by users
        public IList<RatingByUser> GetRatingsByUser(string uconst, int page, int pageSize)
        {
            var result = new List<RatingByUser>();

            foreach (var r in ctx.RatingsByUser)
            {
                if (r.Uconst.Trim() == uconst)
                { 
                    result.Add(r); 
                }
            }
            
            return result.ToList();

        }

        public RatingByUser GetRatingByUser(string uconst, string tconst)
        {

            var result = ctx.RatingsByUser.Find(uconst, tconst);
            return result;

        }

        public IList<TitlePrincipals> GetTitlePrincipalsByName(string nconst, int page, int pageSize)
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

        public IList<SearchHistory> GetSearchHistory(string uconst)
        {
            List<SearchHistory> result = new List<SearchHistory>();

            foreach (var sh in ctx.SearchHistories)
            {
                if (sh.Uconst.Trim() == uconst)
                {
                    result.Add(sh);
                }
            }
            return result;
        }

        public SearchResult CreateSearch(string search)
        {
             
            return new SearchResult();
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
            ctx.SaveChanges();
            return true;
        }
        
        public TitleBookmark CreateTitleBookmark(string uconst, string tconst)
        {
            if (!IsValidUconst(uconst) || !IsValidTconst(tconst)) return null;
            
            var result = new TitleBookmark
            {
                Uconst = uconst,
                Tconst = tconst,
                Timestamp = DateTime.Now
            };
            ctx.TitleBookmarks.Add(result);
            ctx.SaveChanges();
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
            ctx.SaveChanges();
            return true;
        }
        
        public NameBookmark CreateNameBookmark(string uconst, string nconst)
        {
            if (!IsValidUconst(uconst) || !IsValidNconst(nconst)) return null;

            var result = new NameBookmark
            {
                Uconst = uconst,
                Nconst = nconst,
                Timestamp = DateTime.Now
            };
            ctx.NameBookmarks.Add(result);
            ctx.SaveChanges();
            return result;
        }

        public TitleRatings GetTitleRating(string tconst)
        {
            using var ctx1 = new DBContext();
            var result = ctx1.TitleRatings.Find(tconst);
            return result;
        }

        public IList<TitleRatings> GetTitleRatings()
        {
            using var ctx1 = new DBContext();
            var result = ctx1.TitleRatings;
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
            
            using var ctx1 = new DBContext();
            
            foreach (var tg in ctx1.TitleGenres)
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
            ctx.SaveChanges();
            return true;
        }
        
        public NameNotes CreateNameNote(string uconst, string nconst, string notes)
        {
            if (!IsValidUconst(uconst) || !IsValidNconst(nconst)) return null;

            var response = new NameNotes()
            {
                Uconst = uconst,
                Nconst = nconst,
                Notes = notes
            };
            ctx.NameNotes.Add(response);
            ctx.SaveChanges();
            return response;
        }

        public bool UpdateNameNote(string uconst, string nconst, string notes)
        {
            var note = ctx.NameNotes.Find(uconst, nconst);
            if (note == null)
            {
                return false;
            }
            note.Notes = notes;
            ctx.SaveChanges();
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
            ctx.SaveChanges();
            return true;
        }
        
        public TitleNotes CreateTitleNote(string uconst, string tconst, string notes)
        {
            if (!IsValidUconst(uconst) || !IsValidTconst(tconst)) return null;
            var response = new TitleNotes()
            {
                Uconst = uconst,
                Tconst = tconst,
                Notes = notes
            };
            ctx.TitleNotes.Add(response);
            ctx.SaveChanges();
            return response;
        }

        public bool UpdateTitleNote(string uconst, string tconst, string notes)
        {
            var note = ctx.TitleNotes.Find(uconst, tconst);
            if (note == null)
            {
                return false;
            }
            note.Notes = notes;
            ctx.SaveChanges();
            return true;
        }
        

    }
}