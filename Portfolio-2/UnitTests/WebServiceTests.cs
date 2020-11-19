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
           Assert.Equal("tt10850888", data.First()["tconst"]);
           Assert.Equal("tt7006666 ", data.Last()["tconst"]);
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
                StartYear = "2020",
                EndYear = "2020",
                RunTimeMinutes = 160,
                Poster = "poster N/A",
                Awards = "null",
                Plot = "te"
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
                StartYear = "2021",
                EndYear = "2023",
                RunTimeMinutes = 161,
                Poster = "poster N/A",
                Awards = "null",
                Plot = "gg"
            };
            
            var (title, _) = PostData($"{TitlesApi}", data);

            var update = new
            {
                Tconst = title["tconst"].ToString(),
                Titletype = title["titleType"] + "Updated",
                PrimaryTitle = title["primaryTitle"] + "Updated",
                OriginalTitle = title["originalTitle"] + "Updated",
                IsAdult = title["isAdult"].Value<bool>(),
                StartYear = "2020",
                EndYear = "2020",
                RunTimeMinutes = title["runTimeMinutes"].Value<int?>(),
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
                Tconst = "1",
                Titletype = "Updated",
                PrimaryTitle = "Updated",
                OriginalTitle = "Updated",
                IsAdult = true,
                StartYear = "2020",
                EndYear = "2021",
                RunTimeMinutes = 10,
                Poster = "Updated",
                Awards = "Updated",
                Plot = "Updated",
            };

            var statusCode = PutData($"{TitlesApi}/{update.Tconst}", update);

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
                RunTimeMinutes = 161,
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
           Assert.Equal(7, data.Count());
           Assert.Equal("ui000001  ", data.First()["uconst"]);
           Assert.Equal("ui000006  ", data[5]["uconst"]);
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
           var (_,statusCode) = GetObject($"{UsersApi}/ui000000");
           
           Assert.Equal(HttpStatusCode.NotFound, statusCode);
       }

       [Fact]
       public void ApiUsers_DeleteUserWithValidUserId_Ok()
       {
           var data = new
           {
               Uconst = "ui0000000",
               FirstName = "John",
               LastName = "Doe",
               Email ="johndoe@mail.com",
               Password = "JohnDoe01",
               UserName = "JD01"
           };
           
           var (user, _) = PostData($"{UsersApi}",data);

           var statusCode = DeleteData($"{UsersApi}/{user["uconst"]}");

           Assert.Equal(HttpStatusCode.OK,statusCode);
           
           
       }

       [Fact]
       public void ApiUsers_DeleteUserWithInvalidId_NotFound()
       {
           var statusCode = DeleteData($"{UsersApi}/-1");
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
       public void ApiNames_GetWithValidNameId_OkAndName()
       {
           var (user, statusCode) = GetObject($"{NamesApi}/nm0000001");
           
           Assert.Equal(HttpStatusCode.OK, statusCode);
           Assert.Equal("Fred Astaire", user["primaryName"]);
       }
       
       [Fact]
       public void ApiNames_GetWithInvalidNameId_NotFound()
       {
           var (_, statusCode) = GetObject($"{NamesApi}/nm0000000");
           
           Assert.Equal(HttpStatusCode.NotFound, statusCode);
       }

       [Fact]

       public void ApiNames_GetKnownForTitlesWithValidNameId_OkAndTitles()
       {
           var (data, statusCode) = GetResponseWithPaging("{NamesApi}/nconst/knownfortitles");

           Assert.Equal(HttpStatusCode.OK, statusCode);
           Assert.Equal(2, data.Count());
           Assert.Equal("tt0372784  ", data.First()["nconst"]);
           Assert.Equal("tt0468569 ", data.Last()["nconst"]);
       }
       
       [Fact]
       public void ApiNames_GetKnownForTitlesWithInvalidId_NotFound()
       {
       }

       [Fact]
       public void ApiNames_DeleteNameWithValidNameId_Ok()
       {
           var data = new
           {
               Nconst = "nm0000000",
               PrimaryName = "John Doe",
               BirthYear = "0000",
               DeathYear = "0000"
           };
           
           var (name, _) = PostData($"{NamesApi}",data);

           var statusCode = DeleteData($"{NamesApi}/{name["nconst"]}");

           Assert.Equal(HttpStatusCode.OK,statusCode);
       }

       [Fact]
       public void ApiNames_DeleteNameWithInvalidId_NotFound()
       {
           var statusCode = DeleteData($"{NamesApi}/-1");
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
           client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "ui000001");
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
           client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "ui000001");
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
           client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "ui000001");
           var response = client.DeleteAsync(url).Result;
           return response.StatusCode;
       }
    }
}