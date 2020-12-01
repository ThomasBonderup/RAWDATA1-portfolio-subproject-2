define(['knockout', 'postman'], function(ko, postman) {
    
    let selectedComponent = ko.observable('user');
    let selectedCategory = ko.observable();
    let currentParams = ko.observable({selectedCategory});
    let menuElements = ["About us", "Register", "Login" ];
    
    let changeContent = element => {
        selectedComponent(element.toLowerCase());
    }
    
    let isActive = element => {
        return element.toLowerCase() === selectedComponent() ? "active" : "";
    }
    
    postman.subscribe("changeContent", component => {
       changeContent(component);
        
    });
    
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
        console.log("Change component");
        if(selectedComponent() === 'user'){
            currentParams({genre: selectedGenre});
            selectedComponent('login');
        }else{
            currentParams({selectedGenre});
            selectedComponent('user');
        }
    };

    let advSearchBtn = () => console.log("Advanced clicked");
    
    

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
        advSearchBtn,
        isActive,
        menuElements,
        changeContent,
    };
});