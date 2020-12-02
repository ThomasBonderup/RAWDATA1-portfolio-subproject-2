define(['knockout','dataservice'], (ko, ds)=> {
    let currentTemplate = ko.observable('user-list');
    return function (params) {
        let user = ()=>
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

        let createUser = () => {
            console.log(selectedUser);
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
            currentTemplate,
            signInBtn
        }
    }
});