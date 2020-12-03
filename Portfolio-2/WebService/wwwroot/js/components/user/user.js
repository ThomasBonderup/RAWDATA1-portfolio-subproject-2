define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) =>{
    
    let _user = ko.observable();
    
   
    return function(params){
        
        let user = ko.observableArray();
        //let uconst = params.uconst;
        let uconst = 'ui000001';
        ds.getUser(uconst, function(data) {user(data)});
        
        /*
        let selectedUser = params.selectedUser;
        
        let selectUser = user => {
            selectedUser(user);
            postman.publish('changeUser', user);
            
        }
        
      ds.getUser(function (data){_user(data)});
        */
        
        let firstName = ko.observable('Test');
        let lastName = ko.observable('Testesen');
        let userRole = ko.observable('Admin');
        
        
        return {
            _user,
            //selectedUser,
            //selectUser,
            firstName,
            lastName,
            userRole,
            user
        }
    }
});