using System;
using System.Data;
using Xunit;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UnitTests
{
    public class UnitTest1
    {
        // TITLES
        private const string TitlesApi = "http://localhost:5001/api/titles";
        private const string TitlesPrincipalsApi = "http://localhost:5001/api/titles/titleprincipals";
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
        private const string PrimaryProfessionApi = "http://localhost:5001/api/name/primaryprofession";
        private const string TitlePrincipalsApi = "http://localhost:5001/api/names/titleprincipals";
        
        
       // /api/titles
        
        [Fact]
        public void ApiTitles_GetWithNoArgument_OkAndAllTitles()
        {
            var (data, statusCode) = GetArray(TitlesApi);

            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.Equal(28417, data.Count);
            Assert.Equal("Çocuk", data.First()["primaryname"]);
        }

        [Fact]

        public void ApiTitles_GetWithValidTitleId_OkAndTitle()
        {
            var (title, statusCode) = GetObject($"{TitlesApi}/tt10850402");
            
            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.Equal("Çocuk", title["primarytitle"]);
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