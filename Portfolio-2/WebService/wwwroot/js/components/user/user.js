define(['knockout'], ['dataservice'], (ko, ds, user) =>{
   
    let userImg = ko.observable();
    let firstName = ko.observable("Django");
    let lastName = ko.observable("Bang-Bang");
    
    let ratingHistory = ko.observableArray();
    let bookmarks = ko.observableArray();
    
    
    return {
        
        userImg,
        firstName,
        lastName,
        ratingHistory,
        bookmarks,
    };
    
});