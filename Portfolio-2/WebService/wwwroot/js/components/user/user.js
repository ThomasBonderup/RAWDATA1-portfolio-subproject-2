define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) =>{
    
  //  let _user = ko.observable();
    
    let editBtn = ko.observable();
    let editMode = false;
    
    

   
    return function(params){
        
        let user = ko.observableArray();
        //let uconst = params.uconst;
        let uconst = 'ui000001';
        
        ds.getUser(uconst, function(data) {user(data)});
        
       
        let selectedUser = params.user;
        
      
        
        /*
        let selectedUser = params.selectedUser;
        
        let selectUser = user => {
            selectedUser(user);
            postman.publish('changeUser', user);
            
        }
        
      ds.getUser(function (data){_user(data)});
        */
        
        
        
        let fName = ko.observable("Test");
        let lastName = ko.observable('Testesen');
        let email = ko.observable("test@gmail.com");
        let userRole = ko.observable('Admin');

        let isEditMode = function () {

            if(editMode === false){
                editMode = true;
                
            }else{
                editMode = false;
            }
            console.log(editMode);
        }
        
        
        
        
        return {
            isEditMode,
            editBtn,
            editMode,
          //  _user,
            //selectedUser,
            //selectUser,
            fName,
            lastName,
            email,
            userRole,
            user,
        }
    }
});