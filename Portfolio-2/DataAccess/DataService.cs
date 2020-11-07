using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class DataService : IDataService
    {
        public DBContext ctx { get; set; }
        
        private IList<Name> names = new List<Name>(); 

        public DataService()
        {
            ctx = new DBContext();
        }

        public IList<Name> GetNames(int page, int pageSize)
        {
            return ctx.Names.ToList()
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public IList<User> GetUsers(int page, int pageSize)
        {
            return ctx.Users.ToList()
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public User CreateUser(string firstName, string lastName, string Emial, string password, string userName)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteUser(string uconst)
        {
            throw new System.NotImplementedException();
        }

        public IList<Title> GetTitles(int page, int pageSize)
        {
            return ctx.Titles.ToList()
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();
        }
        
        

        public Title GetTitle(string tconst)
        {
            var title = ctx.Titles.Find(tconst);

            if (title != null)
            {
                return title;
            }
            return null;
        }

        public User GetUser(string Uconst)
        {
            throw new System.NotImplementedException();
        }

        public Name GetName(string nconst)
        {
            var name = ctx.Names.Find(nconst);

            if (name != null)
            {
                return name;
            }
            return null;
        }

        public int NumberOfNames()
        {
            return ctx.Names.Count();

        }

        public Title CreateTitle(string primaryTitle)
        {
            throw new System.NotImplementedException();
        }
    }
}