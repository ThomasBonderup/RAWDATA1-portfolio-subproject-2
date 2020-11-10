using System;
using System.Data;
using Xunit;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using DataAccess;
using Microsoft.VisualStudio.TestPlatform.Common;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit.Abstractions;


namespace UnitTests
{
    public class WebServiceTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public WebServiceTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        // -------------------------------- api paths  -------------------------------- 
        private const string TitlesApi = "http://localhost:5001/api/titles";
        private const string UsersApi = "http://localhost:5001/api/users"; 
        private const string NamesApi = "http://localhost:5001/api/names";
        
        // ------ authentication...
        // TODO fix authentication error
        // ui000001  
        

        // -------------------------------- api/titles -------------------------------- 
       [Fact]
       public void ApiTitles_GetWithNoArguments_OkAndAllTitles()
       {
           var (data, statusCode) = GetResponseWithPaging(TitlesApi);
           Assert.Equal(HttpStatusCode.OK, statusCode);
           Assert.Equal(10, data.Count());
           Assert.Equal("tt10850402", data.First()["tconst"]);
           Assert.Equal("tt2583620 ", data.Last()["tconst"]);
       }

       [Fact]
       public void ApiTitles_GetWithValidTitleId_OkAndTitle()
       {
           var (title, statusCode) = GetObject($"{TitlesApi}/tt9999998");
           Assert.Equal(HttpStatusCode.OK, statusCode);
           Assert.Equal("Test 1 primarytitle", title["primaryTitle"]);
       }
       
       [Fact]
       public void ApiTitles_GetWithInvalidTitleId_NotFound()
       {
           var (_, statusCode) = GetObject($"{TitlesApi}/tt00000000");
           
           Assert.Equal(HttpStatusCode.NotFound, statusCode);
       }
       
       [Fact]
        public void ApiTitles_PostWithTitle_Created()
        {
            var newTitle = new
            {
                Titletype = "titleTypeTest",
                PrimaryTitle = "primaryTitle Test",
                OriginalTitle = "originalTitle Test",
                IsAdult = false,
                StartYear = 2020,
                EndYear = 2020,
                RunTimeMinutes = 160,
                Poster = "poster N/A",
                Awards = "null",
                Plot = ""
            };
            _testOutputHelper.WriteLine(newTitle.ToString());

            var (title, statusCode) = PostData(TitlesApi, newTitle);


            Assert.Equal(HttpStatusCode.Created, statusCode);

            DeleteData($"{TitlesApi}/{title["tconst"]}");
        }

        [Fact]
        public void ApiTitles_PutWithValidTitle_Ok()
        {

            var data = new
            {
                Titletype = "titleTypeTestt",
                PrimaryTitle = "primaryTitle Test 2",
                OriginalTitle = "originalTitle Test 2",
                IsAdult = false,
                StartYear = 2021,
                EndYear = 2023,
                RunTimeMinutes = 161,
                Poster = "poster N/A",
                Awards = "null",
                Plot = "gg"
            };
            
            var (title, _) = PostData($"{TitlesApi}", data);

            var update = new
            {
                Tconst = title["tconst"],
                Titletype = title["titleType"] + "Updated",
                PrimaryTitle = title["primaryTitle"] + "Updated",
                OriginalTitle = title["originalTitle"] + "Updated",
                IsAdult = title["isAdult"],
                StartYear = title["startYear"] + "2020",
                EndYear = title["endYear"] + "2020",
                RunTimeMinutes = title["runTimeMinutes"] + "1",
                Poster = title["poster"] + "Updated",
                Awards = title["awards"] + "Updated",
                Plot = title["plot"] + "Updated",
            };

            var statusCode = PutData($"{TitlesApi}/{title["tconst"]}", update);

            Assert.Equal(HttpStatusCode.OK, statusCode);

            var (titleCheck, _) = GetObject($"{TitlesApi}/{title["tconst"]}");

            Assert.Equal(title["name"] + "Updated", titleCheck["name"]);
            Assert.Equal(title["titleType"] + "Updated", titleCheck["titleType"]);

            DeleteData($"{TitlesApi}/{title["tconst"]}");
        }

        [Fact]
        public void ApiTitles_PutWithInvalidTitle_NotFound()
        {
            var update = new
            {
                Tconst = -1,
                Titletype = "Updated",
                PrimaryTitle = "Updated",
                OriginalTitle = "Updated",
                IsAdult = true,
                StartYear = 2020,
                EndYear = 2021,
                RunTimeMinutes = 10,
                Poster = "Updated",
                Awards = "Updated",
                Plot = "Updated",
            };

            var statusCode = PutData($"{TitlesApi}", update);

            Assert.Equal(HttpStatusCode.NotFound, statusCode);
        }

        [Fact]
        public void ApiTitles_DeleteWithValidId_Ok()
        {

            var data = new
            {
                Titletype = "titleTypeTestt",
                PrimaryTitle = "primaryTitle Test 2",
                OriginalTitle = "originalTitle Test 2",
                IsAdult = false,
                StartYear = "2021",
                EndYear = "2023",
                RunTimeMinutes = "161",
                Poster = "poster N/A",
                Awards = "null",
                Plot = "gg"
            };
            
            var (title, _) = PostData($"{TitlesApi}", data);

            var statusCode = DeleteData($"{TitlesApi}/{title["tconst"]}");

            Assert.Equal(HttpStatusCode.OK, statusCode);
        }

        [Fact]
        public void ApiTitles_DeleteWithInvalidId_NotFound()
        {

            var statusCode = DeleteData($"{TitlesApi}/-1");

            Assert.Equal(HttpStatusCode.NotFound, statusCode);
        }


       // -------------------------------- api/users  -------------------------------- 
       [Fact]
       public void ApiUsers_GetWithNoArguments_OkAndAllUsers()
       {
           var (data, statusCode) = GetResponseWithPaging(UsersApi);
           Assert.Equal(HttpStatusCode.OK, statusCode);
           Assert.Equal(6, data.Count());
           Assert.Equal("ui000001  ", data.First()["uconst"]);
           Assert.Equal("ui000006  ", data.Last()["uconst"]);
       }
       
       [Fact]
       public void ApiUsers_GetWithValidUserId_OkAndUser()
       {
           var (user, statusCode) = GetObject($"{UsersApi}/ui000002");
           Assert.Equal(HttpStatusCode.OK, statusCode);
           Assert.Equal("Nils", user["firstName"]);
       }
       
       [Fact]
       public void ApiUsers_GetWithInvalidUserId_NotFound()
       {
           var (_, statusCode) = GetObject($"{UsersApi}/ui000000");
           
           Assert.Equal(HttpStatusCode.NotFound, statusCode);
       }
       
       
       
       
       // -------------------------------- api/names -------------------------------- 
       [Fact]
       public void ApiNames_GetWithNoArguments_OkAndAllNames()
       {
           var (data, statusCode) = GetResponseWithPaging(NamesApi);

           Assert.Equal(HttpStatusCode.OK, statusCode);
           Assert.Equal(10, data.Count());
           Assert.Equal("nm0000001 ", data.First()["nconst"]);
           Assert.Equal("nm0000025 ", data.Last()["nconst"]);
       }
       
       [Fact]
       public void ApiUsers_GetWithValidNameId_OkAndName()
       {
           var (user, statusCode) = GetObject($"{NamesApi}/nm0000001");
           
           Assert.Equal(HttpStatusCode.OK, statusCode);
           Assert.Equal("Fred Astaire", user["primaryName"]);
       }
       
       [Fact]
       public void ApiUsers_GetWithInvalidNameId_NotFound()
       {
           var (_, statusCode) = GetObject($"{NamesApi}/nm000000");
           
           Assert.Equal(HttpStatusCode.NotFound, statusCode);
       }


       // --------------------------------  Helper methods for tests
       (JArray, HttpStatusCode) GetArray(string url)
       {
           var client = new HttpClient();
           var response = client.GetAsync(url).Result;
           var data = response.Content.ReadAsStringAsync().Result;
           return ((JArray)JsonConvert.DeserializeObject(data), response.StatusCode);
       }
       
       (JToken, HttpStatusCode) GetResponseWithPaging(string url)
       {
           var client = new HttpClient();
           var response = client.GetAsync(url).Result;
           var result = (JObject) JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
           var data= result["items"];
           return (data, response.StatusCode);
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
           //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", "ui000001");
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