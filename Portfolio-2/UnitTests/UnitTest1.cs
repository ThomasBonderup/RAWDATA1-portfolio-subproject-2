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
    public class UnitTest1
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public UnitTest1(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        // USE THESE IN WEB SERVICE TEST
        /*
        // TITLES
        private const string TitlesApi = "http://localhost:5001/api/titles";
        private const string TitlePrincipalsApi = "http://localhost:5001/api/titles/titleprincipals";
        private const string TitleGenresApi = "http://localhost:5001/api/titles/titlegenres";
        private const string LocalTitlesApi = "http://localhost:5001/api/titles/localtitle";
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
        */

        // /api/titles

        [Fact]
        public void Title_Object_HasTconstAndPrimaryTitle()
        {
            var title = new Title();
            Assert.Null(title.Tconst);
            Assert.Null(title.PrimaryTitle);
        }

        [Fact]
        public void GetAllTitles_NoArgument_ReturnsAllTitles()
        {
            var service = new DataService();
            var titles = service.GetTitles();
            _testOutputHelper.WriteLine(titles.Count.ToString());
            Assert.Equal(8, titles.Count);
            Assert.Equal("Test primarytitle", titles.First().PrimaryTitle);

        }


        // Helpers 
        
        (JArray, HttpStatusCode) GetArray(string url)
        {
            var client = new HttpClient();
            var response = client.GetAsync(url).Result;
            var data = response.Content.ReadAsStringAsync().Result;
            return ((JArray)JsonConvert.DeserializeObject(data), response.StatusCode);
        }

        (JObject, HttpStatusCode) GetObject(string url)
        {
            var client = new HttpClient();
            var response = client.GetAsync(url).Result;
            var data = response.Content.ReadAsStringAsync().Result;
            return ((JObject)JsonConvert.DeserializeObject(data), response.StatusCode);
        }

        (JObject, HttpStatusCode) PostData(string url, object content)
        {
            var client = new HttpClient();
            var requestContent = new StringContent(
                JsonConvert.SerializeObject(content),
                Encoding.UTF8,
                "application/json");
            var response = client.PostAsync(url, requestContent).Result;
            var data = response.Content.ReadAsStringAsync().Result;
            return ((JObject)JsonConvert.DeserializeObject(data), response.StatusCode);
        }

        HttpStatusCode PutData(string url, object content)
        {
            var client = new HttpClient();
            var response = client.PutAsync(
                url,
                new StringContent(
                    JsonConvert.SerializeObject(content),
                    Encoding.UTF8,
                    "application/json")).Result;
            return response.StatusCode;
        }

        HttpStatusCode DeleteData(string url)
        {
            var client = new HttpClient();
            var response = client.DeleteAsync(url).Result;
            return response.StatusCode;
        }
        
        
        
    }
}