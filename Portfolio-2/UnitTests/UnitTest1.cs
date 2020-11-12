using System;
using System.Data;
using Xunit;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using AutoMapper;
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
            var users = service.GetUsers(0, 6);
            Assert.Equal(6, users.Count);
            Assert.Equal("Alex", users.First().FirstName);

        }

        [Fact]
        public void GetAllNames_NoArgument_ReturnsAllNames()
        {
            
            var service = new DataService();
            var names = service.GetNames(0, 234484);
            Assert.Equal(234484, names.Count);
            Assert.Equal("Fred Astaire", names.First().PrimaryName);

        }
        
        // Get a title, name or user BY t-, n-, u-const

        [Fact]
        public void GetTitleByTconst_InvalidTconst()
        {
            var service = new DataService();
            var title = service.GetTitle("tt10850");
            var title2 = service.GetTitle("nt10850402");
            Assert.Null(title);
            Assert.Null(title2);
        }
        
        [Fact]
        public void GetTitleByTconst_ValidTconst()
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

        // Title principals
        [Fact]

        public void GetTitlePrincipalsByTconstAndNconst_ValidTconstNconst()
        {
            
            var service = new DataService();
            var titlePrincipals = service.GetTitlePrincipals("tt0052520", "nm0580565");
            Assert.Equal(8, titlePrincipals.Ordering);
            Assert.Equal("actor", titlePrincipals.Category);
            
        }

        [Fact]

        public void GetTitlePrincipalsByTitle_ValidTconst()
        {
            var service = new DataService();
            var titlePrincipalsList = service.GetTitlePrincipalsByTitle("tt0052520", 0, 10);
            Assert.True(titlePrincipalsList.Count ==  10);
            Assert.Equal("nm0026930", titlePrincipalsList[0].Nconst.Trim());
            Assert.Equal("nm0580565", titlePrincipalsList[1].Nconst.Trim());
            Assert.Equal("nm0001430", titlePrincipalsList[2].Nconst.Trim());
        }


        [Fact]

        public void GetGenresByTconst_ValidTconst_GetTitleGenresList()
        {
            var service = new DataService();
            var titleGenres = service.GetGenres("tt9055052");
            Assert.True(titleGenres.Count == 3);
            Assert.Equal("Comedy", titleGenres[0]);

        }

        [Fact]

        public void GetTitleRating_ValidTconst_ReturnsSingleRating()
        {
            var service = new DataService();
            var titleRating = service.GetTitleRating("tt0247050");
            Assert.Equal(7.9, Math.Round(titleRating.AverageRating, 1));
            Assert.Equal(24, titleRating.NumVotes);

        }

        [Fact]

        public void GetTitleRatings_ReturnsAllRatings()
        {
            var service = new DataService();
            var titleRatings = service.GetTitleRatings();
            Assert.Equal(46703, titleRatings.Count);

        }

        [Fact]

        public void GetLocalTitles_ValidTconst_ReturnsLocalTitle()
        {
            var service = new DataService();
            var localTitle = service.GetLocalTitle("tt0052520");
            Assert.Equal("Зона на Самракот", localTitle[0]);

        }

        // Title
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

        public void UpdateTitle_InvalidID_ReturnsFalse()
        {
            var service = new DataService();
            var result = service.UpdateTitle("tt001", "test", "Test", "Testy", true, "2015", "2015", 20, null, null, null);
            Assert.False(result);
            
        }

        [Fact]

        public void UpdateTitle_ValidID_ReturnsTrue()
        {
            var service = new DataService();
            var title = service.GetTitle("tt10850402");
            var result = service.UpdateTitle(title.Tconst, title.Titletype, "Testing", title.OriginalTitle,
                title.IsAdult, title.StartYear, title.EndYear, title.RunTimeMinutes, title.Poster, title.Awards, title.Plot);
            Assert.Equal("tt10850402", title.Tconst);
            Assert.Equal("Testing", title.PrimaryTitle);
            Assert.True(result);
            
            service.UpdateTitle(title.Tconst, title.Titletype, "Çocuk", title.OriginalTitle,
                title.IsAdult, title.StartYear, title.EndYear, title.RunTimeMinutes, title.Poster, title.Awards, title.Plot);
            
            
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

        //User tests
        
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
        
        [Fact]
        public void UpdateUser()
        { var service = new DataService();
            var user = service.CreateUser("John", "Doe", "jodo@dummy.dk", "jodo01pw", "JoDo");
            var result = service.UpdateUser(user.Uconst, "UpdatedFirstName", "UpdatedLastName", "UpdatedEmail",
                "UpdatedPassword", "UpdatedUserName");
            Assert.True(result);

            user = service.GetUser(user.Uconst);
            
            Assert.Equal("UpdatedFirstName", user.FirstName);
            Assert.Equal("UpdatedLastName", user.LastName);
            Assert.Equal("UpdatedEmail", user.Email);
            Assert.Equal("UpdatedPassword", user.Password);
            Assert.Equal("UpdatedUserName", user.UserName);
            
            //cleanup
            service.DeleteUser(user.Uconst);

        }
        //Name tests
        [Fact]
        public void CreateNewName()
        {
            var service = new DataService();
            var name = service.CreateName("Mickey Mouse", "1926", null);
            Assert.True(name.Nconst != null);
            Assert.Equal("Mickey Mouse", name.PrimaryName );
            Assert.Equal("1926", name.BirthYear);
        }

        [Fact]
        public void DeleteName()
        {
            var service = new DataService();
            var name = service.CreateName("Mickey Mouse", "1926", null); //hvis vi både laver et navn
            var result = service.DeleteName(name.Nconst);
            name = service.GetName(name.Nconst); // og henter et navn?? burde vi ikke kun gøre en af delene???
            Assert.True(result);
            Assert.Null(name);
        }

        [Fact]

        public void UpdateName()
        {
            var service = new DataService();
            var name = service.CreateName("Mickey Mouse", "1926", null);
            var result = service.UpdateName(name.Nconst, "UpdatedName", "UpdatedBirthyear", "UpdatedDeathyear");
            Assert.True(result);

            name = service.GetName(name.Nconst);
            
            Assert.Equal("UpdatedName", name.PrimaryName);
            Assert.Equal("UpdatedBirthyear", name.BirthYear);
            Assert.Equal("UpdatedDeathyear", name.DeathYear);
            
            //cleanup
            service.DeleteUser(name.Nconst);
        }

        /*[Fact]
        public void GetProfessions()
        {
            var service = new DataService();
            var profession = service.GetPrimaryProfession("nm0000001");
            //Assert.Equal("actor",profession.First().Profession);
            //Assert.Equal("soundtrack", profession.Last().Profession);
        }*/
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