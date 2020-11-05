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

        public IList<Title> GetTitles()
        {
            return ctx.Titles.ToList();
        }

        public IList<Name> GetNames(int page, int pageSize)
        {
            //return names;
            return ctx.Names.ToList();
        }
    }
}