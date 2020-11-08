using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class DataService : IDataService
    {
        public DBContext ctx { get; set; }
        
      //  private IList<Name> names = new List<Name>(); 

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
        
        
        
        public User CreateUser(string firstName, string lastName, string emial, string password, string userName)
        {
            
            
            var user = new User
            {
            FirstName = firstName,
            LastName = lastName,
            Uconst = AssignMaxUconst(),
            Email = emial,
            Password = password,
            UserName = userName
            };

            ctx.Users.Add(user);
            return user;
        }

        public bool DeleteUser(string uconst)
        {

            var user = ctx.Users.Find(uconst);

            if (user != null)
            {
                ctx.Users.Remove(user);
                return true;
            }

            return false;


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

        public User GetUser(string uconst)
        {
            var user = ctx.Users.Find(uconst);
            
            if (user != null)
            {
                return user;
            }
            return null;
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
        
        // Utils 
        
        public string AssignMaxUconst()
        {

            var maxUconstInt = 0;
            
            foreach (var user in ctx.Users)
            {

                var uconst = user.Uconst;
                var trimmedUconst = uconst.Remove(0, 2);
                int intUconst = Int32.Parse(trimmedUconst);

                if (intUconst > maxUconstInt)
                {
                    maxUconstInt = intUconst;

                }
            }
            maxUconstInt++;
            var stringUconst = "tt" + maxUconstInt.ToString();

            return stringUconst;
        }

        public string AssignMaxTconst()
        {

            var maxTconstInt = 0;
            
            foreach (var title in ctx.Titles)
            {

                var tconst = title.Tconst;
                var trimmedUconst = tconst.Remove(0, 2);
                int intUconst = Int32.Parse(trimmedUconst);

                if (intUconst > maxTconstInt)
                {
                    maxTconstInt = intUconst;
                }
            }
            maxTconstInt++;
            var stringUconst = "tt" + maxTconstInt.ToString();

            return stringUconst;
        }
        
        public string AssignMaxNconst()
        {

            var maxNconstInt = 0;
            
            foreach (var name in ctx.Names)
            {

                var tconst = name.Nconst;
                var trimmedUconst = tconst.Remove(0, 2);
                int intUconst = Int32.Parse(trimmedUconst);

                if (intUconst > maxNconstInt)
                {
                    maxNconstInt = intUconst;
                }
            }
            maxNconstInt++;
            var stringUconst = "tt" + maxNconstInt.ToString();

            return stringUconst;
        }
        
        
    }
}