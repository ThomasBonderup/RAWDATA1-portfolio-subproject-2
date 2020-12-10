define(['knockout', 'postman'], function (ko, postman) {

    let selectedComponent = ko.observable('home');
    let selectedUser = ko.observable();
    let searchInput = ko.observable();
    let currentParams = ko.observable({searchInput});
    let menuElements = ["Home", "Movies", "Actors", "About", "Login", "User"];
    let advSearchBtn = () => console.log("Advanced clicked");
    var selectedGenre = ko.observable();

    var genresList = [
        "Comedy", "Horror", "Reality-TV", "Sport", "Talk-Show", "Crime",
        "Mystery", "News", "Action", "History", "Animation", "Thriller",
        "Short", "War", "Game-Show", "Sci-Fi", "Family", "Documentary",
        "Adventure", "Adult", "Biography", "Musical", "Music",
    ];


    let isActive = element => {
        return element.toLowerCase() === selectedComponent() ? "active" : "";
    }

    let isActiveDrop = genre => {
        return genre.toLowerCase() === selectedGenre() ? "active" : "";
    }

    postman.subscribe("changeContent", component => {
        changeContent(component);
    });

    postman.subscribe("changeTitle", component => {
        changeContent('title-details');
    });

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
        selectedGenre,
        searchInput,
        searchBtn,
        loginBtn,
        advSearchBtn,
        isActive,
        menuElements,
        changeContent,
        currentParams,
        genresList,
        isActiveDrop,
    };
});