define(['knockout','dataservice'], (ko, ds)=> {
    let currentTemplate = ko.observable('user-list');
    return function (params) {
        let user = ()=>
            selectedUser(
            {
                firstName: ko.observable(),
                lastName: ko.observable(),
                email: ko.observable(),
                username: ko.observable(),
                password: ko.observable()
            });
        currentTemplate("new-User");

        let selectedUser = ko.observable();

        let createUser = () => {
            console.log("registerbtn clicked");
            user.push(selectedUser());
            currentTemplate("user-list");
        }
         

        //supposed to  - when the user clicks on the button, toggle between hiding and showing the dropdown content(?)
        //      let registerBtn = () => console.log("Login button clicked");

        let signInBtn = () => console.log("Sign in button clicked");

        return {
            user,
            selectedUser,
            createUser,
            currentTemplate
        }
    }
});