define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) =>{

    return function(params){
        
        
        let uconst = ko.observable('ui000001');    
    let firstName = ko.observable();
    let lastName = ko.observable("Jacko");
    let email = ko.observable();
    let userRole = ko.observable();
    let password = ko.observable();
    let userName = ko.observable();
    
    let user = ko.observable();
    
    ds.getUser(uconst, function (data){
        user(data);
    });
    
    
    
  
    
    
    let editBtn = ko.observable();
    let editMode = ko.observable(false);
    
      
      
        
        ds.getUser(uconst, function(data) {user(data)});
        let userDetails = ko.observableArray(user.getData(user).splitText(":", ","));
        firstName(userDetails[1]);
       
        
        let isEditMode = function () {

            console.log(firstName);
            
            if(editMode() === false){
                editMode(true);
            }else{
                editMode(false);
            }
            console.log(editMode());
        }
        
        return {
         
            editBtn,
            editMode,
            firstName,
            lastName,
            email,
            userRole,
            user,
            isEditMode,
     
        }
    }
});