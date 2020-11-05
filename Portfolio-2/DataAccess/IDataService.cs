using System.Collections.Generic;

namespace DataAccess
{
    public interface IDataService
    {
        IList<Title> GetTitles();

        Title GetTitle(string tconst);

        Name GetName(string nconst);

        IList<Name> GetNames(int page, int pageSize);
    }
}