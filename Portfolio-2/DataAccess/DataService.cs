using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class DataService : IDataService
    {
        public DBContext ctx { get; set; }
        
       // private IList<Name> names = new List<Name>(); ?? 

        public DataService()
        {
            ctx = new DBContext();
        }

        public IList<Title> GetTitles()
        {
            return ctx.Titles.ToList();
        }

        public IList<Name> GetNames(int page, int pageSize)
        {
            return names
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();
            //return ctx.Names.ToList(); //this???
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
           // return names.Count;
            return ctx.Names.Count();

        }
    }
}