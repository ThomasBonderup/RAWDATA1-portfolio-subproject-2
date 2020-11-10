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
        
        public string StartYear { get; set; }

        public string EndYear { get; set; }

        public int? RunTimeMinutes { get; set; }

        public string Poster { get; set; }

        public string Awards { get; set; }

        public string Plot { get; set; }

        //public string Genre { get; set;}
        
        //public TitleRatings TitleRatings { get; set; }

        //public LocalTitles LocalTitles { get; set; }

        public override string ToString()
        {
            return $"Tconst={Tconst}, PrimaryTitle{PrimaryTitle}, OriginalTitle {OriginalTitle}, IsAdult {IsAdult}, StartYear{StartYear}" +
                   $"EndYear {EndYear}, Poster {Poster}, Awards {Awards}, Plot{Plot}" 
                   //$"TitleRatings{TitleRatings}, LocalTitles{LocalTitles}"
                   ;
        }
    }
}