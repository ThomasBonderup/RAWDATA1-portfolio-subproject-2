using System;
using System.Data;
using Xunit;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using DataAccess;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
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

        

        ///api/titles

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
            var titles = service.GetTitles(55076, 55076);
            Assert.Equal(55076, titles.Count);
            Assert.Equal("Çocuk", titles.First().PrimaryTitle);
        }
        
        
        //api/users
        [Fact]
        public void GetUserByUconst_ValidUconst()
        {
            var service = new DataService();
            var users = service.GetUser("ui000001");
            Assert.Equal("Alex",users.FirstName);
            Assert.Equal("Tao",users.LastName);
        }

        [Fact]
        public void CreateNewUser()
        {
            var service = new DataService();
            var users = service.CreateUser("John","Doe","jodo@dummy.data","jodo01pw","JoDo");
            Assert.True(users.Uconst != null);
            Assert.Equal("John", users.FirstName);
            Assert.Equal("Doe", users.LastName);
            Assert.Equal("jodo@dummy.data", users.Email);
            Assert.Equal("jodo01pw", users.Password);
            Assert.Equal("JoDo", users.UserName);
            
            //clean up
            service.DeleteUser(users.Uconst);
        }

        [Fact]
        public void DeleteUser()
        {
            var service = new DataService();
            var user = service.CreateUser("John","Doe","jodo@dummy.data","jodo01pw","JoDo");
            var result = service.DeleteUser(user.Uconst); // result should be a boolean?
            user = service.GetUser(user.Uconst);
            Assert.True(result); 
            Assert.Null(user);

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