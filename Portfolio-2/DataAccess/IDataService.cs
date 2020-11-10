using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess
{
    public interface IDataService
    {
        IList<Title> GetTitles(int page, int pageSize);

        public TitlePrincipals GetTitlePrincipals(string tconst, string nconst);

        public IList<TitlePrincipals> GetTitlePrincipalsByTitle(string tconst);

        Title GetTitle(string tconst);

        User GetUser(string uconst);

        Name GetName(string nconst);

        IList<Name> GetNames(int page, int pageSize);

        IList<User> GetUsers(int page, int pageSize);

        User CreateUser(string firstName, string lastName, string Email, string password, string userName);

        bool UpdateUser(string uconst, string FirstName, string LastName, string Email, string Password, string UserName);

        bool DeleteUser(string uconst);
        
        int NumberOfNames();
        
        int NumberOfUsers();

        int NumberOfTitles();

        Title CreateTitle(string titleType, string primaryTitle, string originalTitle, bool isAdult, string startYear, string endYear, int runtimeMinutes,
            string poster, string awards, string plot);
        
        bool UpdateTitle(string tconst, string titleType, string primaryTitle, string originalTitle, bool isAdult, string startYear, string endYear, int? runtimeMinutes,
            string poster, string awards, string plot);
        
        IList<Title> SearchTitles(string searchString, string uconst, int page, int pageSize);
    }
}