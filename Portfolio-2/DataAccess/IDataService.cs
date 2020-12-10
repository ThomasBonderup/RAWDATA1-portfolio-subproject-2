using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess
{
    public interface IDataService
    {
        IList<Title> GetTitles(int page, int pageSize);
        
        //IList<TitlePrincipals> GetTitlePrincipalsList(int page, int pageSize);

        TitlePrincipals GetTitlePrincipal(string tconst, string nconst);

        IList<PrimaryProfession> GetProfessions(string nconst);

        IList<KnownForTitle> GetKnownForTitles(string nconst);

        IList<TitlePrincipals> GetTitlePrincipalsByTitle(string tconst, int page, int pageSize);

        IList<TitlePrincipals> GetTitlePrincipalsByName(string nconst, int page, int pageSize);
        
        Title GetTitle(string tconst);

        bool DeleteName(string nconst);

        Name CreateName(string PrimaryName, string BirthYear, string DeathYear);

        bool DeleteTitle(string tconst);

        //User GetUser(string uconst);
        
        User GetUser(string username);

        Name GetName(string nconst);

        IList<Name> GetNames(int page, int pageSize);

        IList<User> GetUsers(int page, int pageSize);

        User CreateUser(string firstName, string lastName, string Email, string userName, string password);
        
        User CreateUser(string firstName, string lastName, string Email, string userName, string password = null, string salt =null);

        bool UpdateUser(string uconst, string FirstName, string LastName, string Email, string Password, string UserName);

        bool DeleteUser(string uconst);

        bool UpdateName(string Nconst, string PrimaryName, string BirthYear, string DeathYear);
        
        int NumberOfNames();
        
        int NumberOfUsers();

        int NumberOfTitles();
        
        int NumberOfTitlePrincipals();

        int NumberOfSearchResults();

        Title CreateTitle(string titleType, string primaryTitle, string originalTitle, bool isAdult, string startYear,
            string endYear, int? runtimeMinutes,
            string poster, string awards, string plot);
        
        bool UpdateTitle(string tconst, string titleType, string primaryTitle, string originalTitle, bool isAdult, string startYear, string endYear, int? runtimeMinutes,
            string poster, string awards, string plot);
        
        IList<Title> SearchTitles(string searchString, string uconst, int page, int pageSize);

        IList<TitlePrincipals> SearchTitlePrincipals(string searchString, string uconst, int page, int pageSize);

        IList<SearchResult> SearchDynamicBestMatch(string searchString, int page, int pageSize);
        
        IList<string> GetGenres(string tconst);

        IList<string> GetLocalTitle(string tconst);
        
        IList<TitleRatings> GetTitleRatings();

        TitleRatings GetTitleRating(string tconst);

        TitleBookmark CreateTitleBookmark(string uconst, string tconst);

        bool DeleteTitleBookmark(string uconst, string tconst);

        TitleBookmark GetTitleBookmark(string uconst, string tconst);

        IList<TitleBookmark> GetTitleBookmarks(string uconst);

        RatingByUser GetRatingByUser(string uconst, string tconst);

        IList<RatingByUser> GetRatingsByUser(string uconst, int page, int pageSize);

        bool CreateRatingByUser(string uconst, string tconst, float rating, string review);
        
        IList<RatingHistory> GetRatingHistory(string uconst);

        IList<RatingHistory> GetAllRatingHistory(string uconst, string tconst);

        NameRating GetNameRating(string nconst);

        IList<SearchHistory> GetSearchHistory(string uconst);
        
        NameBookmark CreateNameBookmark(string uconst, string nconst);

        bool DeleteNameBookmark(string uconst, string nconst);

        NameBookmark GetNameBookmark(string uconst, string nconst);

        IList<NameBookmark> GetNameBookmarks(string uconst);
        
        NameNotes CreateNameNote(string uconst, string nconst, string notes);

        bool DeleteNameNote(string uconst, string nconst);

        bool UpdateNameNote(string uconst, string tconst, string notes);

        NameNotes GetNameNote(string uconst, string nconst);

        IList<NameNotes> GetNameNotes(string uconst);
        
        TitleNotes CreateTitleNote(string uconst, string tconst, string notes);

        bool DeleteTitleNote(string uconst, string tconst);
        
        bool UpdateTitleNote(string uconst, string tconst, string notes);

        TitleNotes GetTitleNote(string uconst, string tconst);

        IList<TitleNotes> GetTitleNotes(string uconst);
        
    }
}