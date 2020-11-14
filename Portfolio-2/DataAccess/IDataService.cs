using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess
{
    public interface IDataService
    {
        IList<Title> GetTitles(int page, int pageSize);
        
        IList<TitlePrincipals> GetTitlePrincipalsList(int page, int pageSize);

        public TitlePrincipals GetTitlePrincipals(string tconst, string nconst);

        public IList<PrimaryProfession> GetProfessions(string nconst);

        public IList<KnownForTitle> GetKnownForTitles(string nconst);

        public IList<TitlePrincipals> GetTitlePrincipalsByTitle(string tconst, int page, int pageSize);

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
        public IList<string> GetGenres(string tconst);

        public IList<string> GetLocalTitle(string tconst);
        public IList<TitleRatings> GetTitleRatings();

        public TitleRatings GetTitleRating(string tconst);
        public TitleBookmark CreateTitleBookmark(string uconst, string tconst);

        public bool DeleteTitleBookmark(string uconst, string tconst);

        public TitleBookmark GetTitleBookmark(string uconst, string tconst);

        public IList<TitleBookmark> GetTitleBookmarks(string uconst);

        public RatingByUser GetRatingByUser(string uconst, string tconst);

        public IList<RatingByUser> GetRatingsByUser(string uconst, int page, int pageSize);
        public IList<RatingHistory> GetRatingHistory(string uconst);

        public IList<RatingHistory> GetAllRatingHistory(string uconst, string tconst);

        public NameRating GetNameRating(string nconst);
    }
}