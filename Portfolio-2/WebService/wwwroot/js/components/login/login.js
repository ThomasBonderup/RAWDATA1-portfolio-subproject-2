define(["knockout","dataservice"], (ko, ds)=> {
    return function (params) {
        // todo make a return error statement if username allready exists in the system 
        let currentTemplate = ko.observable("login-page");
        let createAccount = () =>
        {
         console.log("submit pressed");
            console.log(user);
            selectedUser(user);
            ds.getUser()
            console.log(selectedUser());
        }
       //let uconst = ko.observable('ui000001');
        
        let selectedUser = ko.observable();
        
        /*ds.getUser(uconst, function (data) {
            selectedUser(data);
        });
        
        console.log(selectedUser());*/
        
        let createUserBtn = () => {
            console.log("create user button clicked");
            //console.log(selectedUser);
            //user.push(selectedUser());
            currentTemplate("createaccount-page");
        }
        
        let cancelUserBtn = () =>{
            console.log("cancel pushed");
            currentTemplate("login-page");
        }

        let signInUserBtn = () =>{
            console.log("sign in button clicked");
            currentTemplate("signIn-page");
        }
        
        let signInUser =()=>{
            
        }
        
        let user = () => {
                selectedUser(
                    {
                        firstName: ko.observable(),
                        lastName: ko.observable(),
                        email: ko.observable(),
                        username: ko.observable(),
                        password: ko.observable()

                    });
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
        /*
        
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
            createUserBtn,
            currentTemplate,
            signInUserBtn,
            selectedUser,
            cancelUserBtn,
            createAccount,
            signInUser,
            user
            /*selectedUser,
            createUser,
            currentTemplate,
            signInBtn,
            selectUser*/            
        }
    }
});