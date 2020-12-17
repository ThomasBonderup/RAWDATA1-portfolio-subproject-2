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
        let userName = ko.observable();
        
        // RATING HISTORY
        let ratingHistory = ko.observable();
        let tConstRating = ko.observable();
        let timestampRating = ko.observable();
        let rating = ko.observable();
        let review = ko.observable();
        let ratingHistoryList = ko.observableArray();
        let finalRatingList = ko.observableArray();
        
        // SEARCH HISTORY
        let searchHistory = ko.observable();
        let searchValue = ko.observable();
        let timestampSearch = ko.observable();
        let searchHistoryList = ko.observableArray();
        let finalSearchList = ko.observableArray();
        
        // BOOKMARKS
        let titleBookmark = ko.observable();
        let titleBookmarkList = ko.observableArray();
        let finalTitleBookmarksList = ko.observableArray();
        
        
        let timestamp = ko.observable();
        let tconst = ko.observable();
        
        let showUserInfo = ko.observable(true);
        let showEditUser = ko.observable(false);
        let showBookmarks = ko.observable(false);
        let showSearchHistory = ko.observable(false);
        let showRatings = ko.observable(false);
        
        
        ds.getTitleBookmarks('ui000001', function (data){
            
            titleBookmark(data);
            tconst(titleBookmark.tconst);
            timestamp(titleBookmark.timestamp);
            
            for(var elem in titleBookmark)
            {
                titleBookmarkList.push(titleBookmark[elem]);
            }
            finalTitleBookmarksList(titleBookmarkList()[0]);
            
        })
        
        ds.getSearchHistory('ui000001', function(data){
            
            searchHistory(data);
            searchValue(searchHistory().search);
            timestamp(searchHistory().timeStamp);
            
            for(var elem in searchHistory)
            {
                searchHistoryList.push(searchHistory[elem]);
            }
            finalSearchList(searchHistoryList()[0]);
            
        });
        
        ds.getRatingHistory('ui000001', function(data){
           
            ratingHistory(data);
            uconst(ratingHistory().uconst);
            rating(ratingHistory().rating);
            review(ratingHistory().review);
            timestamp(ratingHistory().timestamp);
            
            for(var elem in ratingHistory){

                ratingHistoryList.push(ratingHistory[elem]);
            }
            finalRatingList(ratingHistoryList()[0]);
        });
        
    ds.getUser(testUsername, function (data){
        
        user(data);
        uconst(user().uconst);
        firstName(user().firstName);
        lastName(user().lastName);
        email(user().email);
        password(user().password);
        password2(user().password);
        userName(user().username);
       
        
        console.log(uconst());
        console.log(firstName());
        console.log(lastName());
        console.log(email());
        console.log(password());
        console.log(userName());
        
    });
    
    /*
    postman.subscribe('getUser', user => {
       
        user(user);
        console.log(user());
        
    });
    */
        
        let saveSettings = function() {
            
            if(editMode() === true){
                
                ds.updateUser(uconst(), firstName(), lastName(), email(), password(), userName());

                settingsSaved(true);
                //SAVE SETTINGS
                editMode(false);
                showUserInfo(true);
                showEditUser(false);
           
            } 
        }
        
        let isSearchHistoryMode = function () {
            
            if(showSearchHistory() === false){
                showSearchHistory(true);
            }else{
                showSearchHistory(false);
            }
            
            console.log(showSearchHistory());
        }
        
        let isRatingsMode = function (){
            
            // Get ratings 
            if(showRatings() === false){
                showRatings(true);
            }else{
                showRatings(false);
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
            userName,
            editMode,
            showUserInfo,
            showEditUser,
            showBookmarks,
            showSearchHistory,
            showRatings,
            finalRatingList,
            tConstRating,
            tconst,
            review,
            rating,
            timestampRating,
            timestampSearch,
            timestamp,
            searchValue,
            finalSearchList,
            titleBookmark,
            finalTitleBookmarksList,
            settingsSaved,
            isBookmarksMode,
            isSearchHistoryMode,
            isEditMode,
            saveSettings,
            isRatingsMode, 
     
        }
    }
});