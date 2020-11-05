using System;
using System.Data;
using Xunit;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using DataAccess;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit.Abstractions;

namespace UnitTests
{
    public class WebServiceTests
    {
        // api paths 
        // TITLES
        private const string TitlesApi = "http://localhost:5001/api/titles";
        private const string TitlePrincipalsApi = "http://localhost:5001/api/titles/titleprincipals";
        private const string TitleRatingsApi = "http://localhost:5001/api/titles/titleratings";
        // USERS 
        private const string UsersApi = "http://localhost:5001/api/users";
        private const string TitleNotesApi = "http://localhost:5001/api/users/titlenotes";
        private const string TitleBookmarksApi = "http://localhost:5001/api/users/titlebookmarks";
        private const string NameNotesApi = "http://localhost:5001/api/users/namenotes";
        private const string NameBookmarks = "http://localhost:5001/api/users/namebookmarks";
        private const string SearchHistoryApi = "http://localhost:5001/api/users/searchhistory";
        private const string RatingApi = "http://localhost:5001/api/users/rating";
        private const string RatingHistoryApi = "http://localhost:5001/api/users/rating/ratinghistory";
        // NAMES
        private const string NamesApi = "http://localhost:5001/api/names";
        private const string KnownForTitles = "http://localhost:5001/api/names/knownfortitles";
        private const string NameRatingApi = "http://localhost:5001/api/names/namerating";
        private const string PrimaryProfessionApi = "http://localhost:5001/api/names/primaryprofession";
       // private const string TitlePrincipalsApi = "http://localhost:5001/api/names/titleprincipals";
        
        
    }
}