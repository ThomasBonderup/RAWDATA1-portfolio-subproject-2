using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    public class Title
    {
        public string tconst { get; set; }

        public string titletype { get; set; }

        public string primaryTitle { get; set; }

        public string originalTitle { get; set; }
        
        public bool isAdult { get; set; } 
        
        public int startYear { get; set; }

        public int endYear { get; set; }

        public int runTimeMinutes { get; set; }

        public string genre { get; set;}
    }
}