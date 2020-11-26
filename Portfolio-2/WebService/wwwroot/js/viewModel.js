define(['knockout'], function(ko) {
    
  //  var self = this;
    
   // var genres = ko.observableArray();
    var selectedGenre = ko.observable();
    
    
    
    function Genre(genre){
        this.genre = ko.observable(genre);
    }
    
    var genres = ([
        
        new Genre("Comedy"),
        new Genre("Horror"),
        new Genre("Reality-TV"),
        new Genre("Sport"),
        new Genre("Talk-Show"),
        new Genre("Crime"),
        new Genre("Mystery"),
        new Genre("News"),
        new Genre("Action"),
        new Genre("History"),
        new Genre("Animation"),
        new Genre("Thriller"),
        new Genre("Short"),
        new Genre("War"),
        new Genre("Game-Show"),
        new Genre("Sci-Fi"),
        new Genre("Family"),
        new Genre("Documentary"),
        new Genre("Adventure"),
        new Genre("Adult"),
        new Genre("Biography"),
        new Genre("Musical"),
        new Genre("Music"),
        
        ]);
    
    let searchInput = ko.observable("search...");
    
    let searchBtn = () => console.log("Search button clicked");
    let advSearchBtn = () => console.log("Filtered search");
    let loginBtn = () => console.log("Login button clicked");
    
    return {
        genres,
        selectedGenre,
        searchInput,
        searchBtn,
        advSearchBtn,
    };
});