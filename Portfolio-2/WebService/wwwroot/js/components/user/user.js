define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) =>{

    return function(params){
        
        let editMode = ko.observable(false);
        let uconst = ko.observable('ui000001');
        let user = ko.observable();
        
        let firstName = ko.observable();
        let lastName = ko.observable();
        let email = ko.observable();
        let password = ko.observable();
        let userName = ko.observable();
        
        let showUserInfo = ko.observable(true);
        let showEditUser = ko.observable(false);
        
      
   

    ds.getUser(uconst, function (data){
        user(data);
        firstName(user().firstName);
        lastName(user().lastName);
        email(user().email);
        password(user().password);
        userName(user().username);
        
    });
    
    /*
    postman.subscribe('getUser', user => {
       
        user(user);
        console.log(user());
        
    });
    */
        
        let saveSettings = function() {
            
            if(editMode() === true){
                //SAVE SETTINGS
                editMode(false);
                showUserInfo(true);
                showEditUser(false);
            } else{
          //   editMode(true);
          //   showUserInfo(false);
          //   showEditUser(true);
                
            }
        }
        
        let isEditMode = function () {
            
            if(editMode() === false){
                editMode(true);
                showUserInfo(false);
                showEditUser(true);
            }else{
            //    editMode(false);
             //   showUserInfo(true);
             //   showEditUser(false);
            }
        }
        
        return {
            
            user,
            firstName,
            lastName,
            email,
            password,
            userName,
            editMode,
            showUserInfo,
            showEditUser,
            isEditMode,
            saveSettings,
     
        }
    }
});