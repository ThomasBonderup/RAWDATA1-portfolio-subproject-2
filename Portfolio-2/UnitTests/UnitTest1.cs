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
        

        [Fact]
        public void Title_Object_HasTconstAndPrimaryTitle()
        {
            var title = new Title();
            Assert.Null(title.Tconst);
            Assert.Null(title.PrimaryTitle);
        }
        
        // Get ALL titles, names, and users

        [Fact]
        public void GetAllTitles_NoArgument_ReturnsAllTitles()
        {
            var service = new DataService();
            var titles = service.GetTitles(55076, 55076);
            Assert.Equal(55076, titles.Count);
            Assert.Equal("Çocuk", titles.First().PrimaryTitle);
        }

        [Fact]
        public void GetAllUsers_NoArgument_ReturnsAllUsers()
        {
            
            var service = new DataService();
            var users = service.GetUsers(6, 6);
            Assert.Equal(6, users.Count);
            Assert.Equal("Alex", users.First().FirstName);

        }

        [Fact]
        public void GetAllNames_NoAargument_ReturnsAllNames()
        {
            
            var service = new DataService();
            var names = service.GetNames(234484, 234484);
            Assert.Equal(234484, names.Count);
            Assert.Equal("Fred Astaire", names.First().PrimaryName);

        }
        
        // Get a title, name or user BY t-, n-, u-const

        [Fact]
        public void GetTitleByTconst_validTconst()
        {
            var service = new DataService();
            var title = service.GetTitle("tt10850402");
            Assert.Equal("Çocuk", title.PrimaryTitle);
        }

        
        [Fact]
        public void GetUserByUconst_ValidUconst()
        {
            var service = new DataService();
            var users = service.GetUser("ui000001");
            Assert.Equal("Alex",users.FirstName);
            Assert.Equal("Tao",users.LastName);
        }

        [Fact]
        public void GetNameByNconst_ValidNconst()
        {
            var service = new DataService();
            var name = service.GetName("nm0000001");
            Assert.Equal("Fred Astaire", name.PrimaryName);
        }

        [Fact]

        public void CreateNewTitle()
        {
            var service = new DataService();
            var title = service.CreateTitle("Movie", "Ultra Testing 3", "UT3", 
                true, "2020", "2020", 120, null, null,
                null);
            Assert.True(title.Tconst != null);
            Assert.Equal("Movie", title.Titletype);
            Assert.Equal("Ultra Testing 3", title.PrimaryTitle);
            Assert.Equal("UT3", title.OriginalTitle);
            Assert.True(title.IsAdult);
            Assert.Equal("2020", title.StartYear);
            Assert.Equal("2020", title.EndYear);
            Assert.Equal(120, title.RunTimeMinutes);

            service.DeleteTitle(title.Tconst);
        }

        [Fact]

        public void DeleteTitle()
        {
            
            var service = new DataService();
            var title = service.CreateTitle("Movie", "Ultra Testing 3", "UT3", 
                true, "2020", "2020", 120, null, null,
                null);
            var result = service.DeleteTitle(title.Tconst);
            title = service.GetTitle(title.Tconst);
            Assert.True(result);
            Assert.Null(title);
            

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