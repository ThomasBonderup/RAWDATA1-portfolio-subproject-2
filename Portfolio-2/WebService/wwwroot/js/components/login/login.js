define(['knockout','dataservice'], (ko, ds)=> {
    return function (params) {
        let currentTemplate = ko.observable('login-page');
        let createAccount = () =>
        {
         
        }
        let selectedUser = ko.observable();
        
        let createUser = () => {
            console.log("create user button clicked")
            //console.log(selectedUser);
            //user.push(selectedUser());
            currentTemplate("create-account");
        }

        let signInUser = () =>{
            console.log("sign in button clicked");
            currentTemplate("signIn-page")
        }

        let existingAccount= (info) =>
        {
            let currentTemplate = ko.observable('existing-account');
        }

        let signUp= () =>
        {

        }

        let logIn= (info) =>
        {

        }
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

        //supposed to  - when the user clicks on the button, toggle between hiding and showing the dropdown content(?)
        //      let registerBtn = () => console.log("Login button clicked");

        let signInBtn = () => console.log("Sign in button clicked");*/

        return {
            createUser,
            currentTemplate,
            signInUser,
            selectedUser
            /*user,
            selectedUser,
            createUser,
            currentTemplate,
            signInBtn,
            selectUser*/            
        }
    }
});