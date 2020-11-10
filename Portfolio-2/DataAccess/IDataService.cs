using System.Collections.Generic;

namespace DataAccess
{
    public interface IDataService
    {
        IList<Title> GetTitles(int page, int pageSize);

        Title GetTitle(string tconst);

        User GetUser(string uconst);

        Name GetName(string nconst);

        IList<Name> GetNames(int page, int pageSize);

        IList<User> GetUsers(int page, int pageSize);

        User CreateUser(string firstName, string lastName, string Email, string password, string userName);

        User UpdateUser(string uconst);

        bool DeleteUser(string uconst);

        int NumberOfNames();

        int NumberOfTitles();

        Title CreateTitle(string titleType, string primaryTitle, string originalTitle, bool isAdult, string startYear, string endYear, int runtimeMinutes,
            string poster, string awards, string plot);
        
        bool UpdateTitle(string tconst, string titleType, string primaryTitle, string originalTitle, bool isAdult, string startYear, string endYear, int? runtimeMinutes,
            string poster, string awards, string plot);
    }
}