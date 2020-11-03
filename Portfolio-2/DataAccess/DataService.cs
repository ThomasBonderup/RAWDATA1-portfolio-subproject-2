namespace DataAccess
{
    public class DataService : IDataService
    {
        public DBContext ctx { get; set; }

        public DataService()
        {
            ctx = new DBContext();
        }
    }
}