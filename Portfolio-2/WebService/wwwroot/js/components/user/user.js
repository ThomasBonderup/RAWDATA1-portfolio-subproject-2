define(['knockout'], ['dataservice'], (ko, ds) =>{
    
    let user = ko.observable();
    
   
    return function(params){
        
        
        let firstName = ko.observable("Test");
        let lastName = ko.observable("Testesen");
        let role = ko.observable("Admin");


        return {
            user,
            firstName,
            lastName,
            role,
        };
        
    };
    
});