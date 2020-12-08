define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) =>{

    return function(params){
        
    let fName = ko.observable("Test");
    let lastName = ko.observable('Testesen');
    let email = ko.observable("test@gmail.com");
    let userRole = ko.observable('Admin');
    
    
    
    let editBtn = ko.observable();
    let editMode = ko.observable(false);
    
    
   // let isEditMode = ko.observable(false);

    
   

        
        let user = ko.observableArray();
        //let uconst = params.uconst;
        let uconst = 'ui000001';
        
        ds.getUser(uconst, function(data) {user(data)});
        
        let isEditMode = function () {

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
            fName,
            lastName,
            email,
            userRole,
            user,
            isEditMode,
     
        }
    }
});