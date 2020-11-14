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

        bool DeleteTitle(string tconst);

        User GetUser(string uconst);

        Name GetName(string nconst);

        IList<Name> GetNames(int page, int pageSize);

        IList<User> GetUsers(int page, int pageSize);

        User CreateUser(string firstName, string lastName, string Email, string password, string userName);

        bool UpdateUser(string uconst, string FirstName, string LastName, string Email, string Password, string UserName);

        bool DeleteUser(string uconst);
        
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
        
        IList<RatingHistory> GetRatingHistory(string uconst);

        IList<RatingHistory> GetAllRatingHistory(string uconst, string tconst);

        NameRating GetNameRating(string nconst);

        IList<SearchHistory> GetSearchHistory(string uconst);
    }
}