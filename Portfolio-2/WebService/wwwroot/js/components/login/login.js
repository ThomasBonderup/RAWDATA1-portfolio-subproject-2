define(['knockout','dataservice'], (ko, ds)=> {
    return function (params) {
        let currentTemplate = ko.observable('sign-in-page');
        
        /*let user = ()=>
            selectedUser(
            {
                firstName: ko.observable("Joe"),
                lastName: ko.observable("John"),
                email: ko.observable("john@john.com"),
                username: ko.observable("joejohn"),
                password: ko.observable("1234")
                
            });
        
        currentTemplate("new-User");

        let selectedUser = ko.observable();
        
        let selectUser = (user) =>{
            console.log(user);
            selectedUser(user);
        }

        let createUser = () => {
            console.log(selectedUser);
            user.push(selectedUser());
            currentTemplate("add-user");
        }
         

        //supposed to  - when the user clicks on the button, toggle between hiding and showing the dropdown content(?)
        //      let registerBtn = () => console.log("Login button clicked");

        let signInBtn = () => console.log("Sign in button clicked");*/
        
        let createAccount = () =>
        {

            let currentTemplate = ko.observable('create-account');
        }

        let existingAccount= (info) =>
        {

            let currentTemplate = ko.observable('existing-account');
        }
        
        let register= () =>
        {

        }
        
        let logIn= (info) =>
        {

        }
        
       
        

        return {
            /*user,
            selectedUser,
            createUser,
            currentTemplate,
            signInBtn,
            selectUser*/
            
            
        }
    }
});