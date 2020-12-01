define(['knockout'], function(ko) {
    
    let selectedComponent = ko.observable('user');
    
    
    var user = ko.observable({firstName: "Test", lastName: "Testesen", userRole: "pub_user"});

    
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
    
    let chgComponent = () => {
        
        if(selectedComponent() === 'user'){
            selectedComponent('title-list');
        }else{
            selectedComponent('user');
        }
    };

    let advSearchButton = () => console.log("Advanced presssed");
    
    

    var selectedGenre = ko.observable();

    
    
    let searchBtn = () => console.log("Search button clicked");
    let loginBtn = () => console.log("Login button clicked");
    
    return {
        user,
        chgComponent,
        selectedComponent,
        genres,
        selectedGenre,
        searchInput,
        searchBtn,
        loginBtn,
        advSearchButton,
    };
});