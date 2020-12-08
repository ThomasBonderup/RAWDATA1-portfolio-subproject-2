define(['knockout', 'postman'], function(ko, postman) {
    
    let selectedComponent = ko.observable('home');
    let selectedUser = ko.observable();
    let searchInput = ko.observable();
    let currentParams = ko.observable({searchInput});
    let menuElements = ["About", "Login", "User"];
    let advSearchBtn = () => console.log("Advanced clicked");
    var selectedGenre = ko.observable();

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
    
    
    let isActive = element => {
        return element.toLowerCase() === selectedComponent() ? "active" : "";
    }
    
    postman.subscribe("changeContent", component => {
       changeContent(component);
    });
    
    postman.subscribe("changeTitle", component => {
       changeContent('title-details'); 
    });
    
    function Genre(genre){
        this.genre = ko.observable(genre);
    }
    

    let searchBtn = () => {
        console.log("Search button clicked");
        currentParams({searchInput});
        selectedComponent('title-list');
    }
    
    let loginBtn = () => {
        console.log("Login button clicked");
    }

    let changeContent = element => {
        selectedComponent(element.toLowerCase());
    }
    
    /*let changeContent = () => {
        console.log("Change component");
        if(selectedComponent() === 'user'){
            currentParams({_user: selectedUser});
            selectedComponent('login');
        }else{
            currentParams({selectedUser});
            selectedComponent('user');
        }
    };*/
    
    return {
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
        currentParams,
    };
});