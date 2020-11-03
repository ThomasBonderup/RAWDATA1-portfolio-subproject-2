using System;
using System.Data;
using Xunit;

namespace UnitTests
{
    public class UnitTest1
    {
        // TITLES
        private const string TitlesApi = "http://localhost:5001/api/titles";
        private const string TitlesPrincipalsApi = "http://localhost:5001/api/titles/titleprincipals";
        private const string TitleGenresApi = "/api/titles/titlegenres";
        private const string LocalTitlesApi = "/api/titles/localtitle";
        private const string TitleRatingsApi = "/api/titles/titleratings";
        // USERS 
        private const string UsersApi = "/api/users";
        private const string TitleNotesApi = "/api/users/titlenotes";
        private const string TitleBookmarksApi = "/api/users/titlebookmarks";
        private const string NameNotesApi = "/api/users/namenotes";
        private const string NameBookmarks = "/api/users/namebookmarks";
        private const string SearchHistoryApi = "/api/users/searchhistory";
        private const string RatingApi = "/api/users/rating";
        private const string RatingHistoryApi = "/api/users/rating/ratinghistory";
        // NAMES
        private const string NamesApi = "/api/names";
        private const string KnownForTitles = "/api/names/knownfortitles";
        private const string NameRatingApi = "/api/names/namerating";
        private const string PrimaryProfessionApi = "/api/name/primaryprofession";
        private const string TitlePrincipalsApi = "/api/names/titleprincipals";
        
        
        [Fact]
        public void Test1()
        {
        }
    }
}