using System.Collections.Generic;

namespace DataAccess
{
    public interface IDataService
    {
        IList<Title> GetTitles();

        IList<Name> GetNames();

        Title GetTitle(string tconst);

        Name GetName(string nconst);
    }
}