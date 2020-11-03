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
    }
}