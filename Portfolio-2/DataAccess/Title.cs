using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    public class Title
    {
        public string Tconst { get; set; }

        public string Titletype { get; set; }

        public string PrimaryTitle { get; set; }

        public string OriginalTitle { get; set; }
        
        public bool IsAdult { get; set; } 
        
        public int StartYear { get; set; }

        public int EndYear { get; set; }

        public int RunTimeMinutes { get; set; }

        public string Genre { get; set;}

        public string poster { get; set; }

        public string awards { get; set; }

        public string plot { get; set; }

        public TitleRatings TitleRatings { get; set; }
    }
}