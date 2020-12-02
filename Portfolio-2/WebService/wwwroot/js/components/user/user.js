define(['knockout'], ['dataservice', 'postman'], (ko, ds, postman) =>{
    
    let _user = ko.observable();
    
   
    return function(params){
        
        let selectedUser = params.selectedUser;
        
        let selectUser = user => {
            selectedUser(user);
            postman.publish('changeUser', user);
            
        }
        
      ds.getUser(function (data){_user(data)});
        
        
        return {
            _user,
          selectedUser,
            selectUser,
        }
    }
});