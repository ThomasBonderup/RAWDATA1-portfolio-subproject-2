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

        User CreateUser(string firstName, string lastName, string Emial, string password, string userName);

        bool DeleteUser(string uconst);

        int NumberOfNames();

        Title CreateTitle(string primaryTitle);
    }
}