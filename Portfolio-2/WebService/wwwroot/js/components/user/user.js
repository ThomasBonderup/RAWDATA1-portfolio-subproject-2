define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) =>{

    return function(params){
        
        let editMode = ko.observable(false);
        let uconst = ko.observable();
        let user = ko.observable();
        let settingsSaved = ko.observable(false);
        let firstName = ko.observable();
        let lastName = ko.observable();
        let email = ko.observable();
        let password = ko.observable();
        let password2 = ko.observable();
        let testUsername = ko.observable('alta01');
        let username = ko.observable();
        
        
        let showUserInfo = ko.observable(true);
        let showEditUser = ko.observable(false);
        let showBookmarks = ko.observable(false);
        let showSearchHistory = ko.observable(false);
        
    ds.getUser(testUsername, function (data){
        user(data);
        uconst(user().uconst);
        firstName(user().firstName);
        lastName(user().lastName);
        email(user().email);
        password(user().password);
        password2(user().password)
        username(user().username);
        
        
        console.log(uconst());
        console.log(firstName());
        console.log(lastName());
        console.log(email());
        console.log(password());
        console.log(username());
        
    });
    
    /*
    postman.subscribe('getUser', user => {
       
        user(user);
        console.log(user());
        
    });
    */
        
        let saveSettings = function() {
            
            if(editMode() === true){
                
                ds.updateUser(uconst(), firstName(), lastName(), email(), password(), username());

                settingsSaved(true);
                //SAVE SETTINGS
                editMode(false);
                showUserInfo(true);
                showEditUser(false);
           
            } 
        }
        
        let isSearchHistoryMode = function () {
            
            if(showSearchHistory === false){
                showSearchHistory(true);
            }else{
                showSearchHistory(false);
            }
        }
        
        let isBookmarksMode = function () {
            // get bookmarkslist
            
            
            // ON / OFF SWITCH
      
            if(showBookmarks() === false){
                showBookmarks(true);
                
            }else{
                showBookmarks(false);
            }
            console.log("Bookmarks mode " + showBookmarks());
        }
        
        let isEditMode = function () {
            
            settingsSaved(false);
            
            if(editMode() === false){
                editMode(true);
                showUserInfo(false);
                showEditUser(true);
            }
        }
        
        return {
            
            user,
            firstName,
            lastName,
            email,
            password,
            password2,
            username,
            editMode,
            showUserInfo,
            showEditUser,
            showBookmarks,
            showSearchHistory,
            settingsSaved,
            isBookmarksMode,
            isSearchHistoryMode,
            isEditMode,
            saveSettings,
     
        }
    }
});