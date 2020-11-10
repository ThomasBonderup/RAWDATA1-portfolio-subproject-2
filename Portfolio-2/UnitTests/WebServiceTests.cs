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
        private readonly ITestOutputHelper _testOutputHelper;

        public WebServiceTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        // api paths 
        private const string TitlesApi = "http://localhost:5001/api/titles";
        private const string UsersApi = "http://localhost:5001/api/users"; 
        private const string NamesApi = "http://localhost:5001/api/names";


        // api/titles
       [Fact]
       public void ApiTitles_GetWithNoArguments_OkAndAllTitles()
       {
           var (data, statusCode) = GetObject(TitlesApi);
           var list = data["items"];
           //_testOutputHelper.WriteLine(list.ToString());
           
           Assert.Equal(HttpStatusCode.OK, statusCode);
           Assert.Equal(10, list.Count());
           Assert.Equal("tt10850402", list.First()["tconst"]);
           Assert.Equal("tt2583620 ", list.Last()["tconst"]);
       }

       /*[Fact]
       public void ApiTitles_GetWithValidTitleId_OkAndTitle()
       {
           var (title, statusCode) = GetObject($"{TitlesApi}/tt9999998");
           
           Assert.Equal(HttpStatusCode.OK, statusCode);
           Assert.Equal("Test 1 primarytitle", title["primaryname"]);
       }
       
       [Fact]
       public void ApiTitles_GetWithInvalidTitleId_NotFound()
       {
           var (_, statusCode) = GetObject($"{TitlesApi}/tt00000000");
           
           Assert.Equal(HttpStatusCode.NotFound, statusCode);
       }


       // api/users
       [Fact]
       public void ApiUsers_GetWithNoArguments_OkAndAllUsers()
       {
           var (data, statusCode) = GetArray(UsersApi);
           
           Assert.Equal(HttpStatusCode.OK, statusCode);
           Assert.Equal(6, data.Count);
           Assert.Equal("ui000001", data.First()["uconst"]);
           Assert.Equal("ui000006", data.Last()["uconst"]);
       }
       
       [Fact]
       public void ApiUsers_GetWithValidUserId_OkAndUser()
       {
           var (user, statusCode) = GetObject($"{UsersApi}/ui000002");
           
           Assert.Equal(HttpStatusCode.OK, statusCode);
           Assert.Equal("Nils", user["firstmame"]);
       }
       
       [Fact]
       public void ApiUsers_GetWithInvalidUserId_NotFound()
       {
           var (_, statusCode) = GetObject($"{UsersApi}/ui000000");
           
           Assert.Equal(HttpStatusCode.NotFound, statusCode);
       }
       
       
       [Fact]
       // api/names
       public void ApiNames_GetWithNoArguments_OkAndAllNames()
       {
           var (data, statusCode) = GetArray(NamesApi);
           
           Assert.Equal(HttpStatusCode.OK, statusCode);
           Assert.Equal(234485, data.Count);
           Assert.Equal("nm0000001", data.First()["nconst"]);
           Assert.Equal("nm99999999", data.Last()["nconst"]);
       }*/
       
       
       
       
       // Helper methods for tests

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