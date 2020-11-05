using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class DataService : IDataService
    {
        public DBContext ctx { get; set; }

        public DataService()
        {
            ctx = new DBContext();
        }

        public IList<Title> GetTitles()
        {
            return ctx.Titles.ToList();
        }

        public IList<Name> GetNames()
        {
            return ctx.Names.ToList();
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
        
    }
}