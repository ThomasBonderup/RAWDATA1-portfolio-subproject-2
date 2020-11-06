namespace DataAccess
{
    public class TitleListDto
    {
        public string Url { get; set; }
        
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
    }
}