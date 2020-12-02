define(['knockout','dataservice'], (ko, ds)=> {
    return function (params) {
        var newUser =
            {
                firstName: ko.observable(),
                lastName: ko.observable(),
                email: ko.observable(),
                username: ko.observable(),
                password: ko.observable()
            }

        let selectedUser = ko.observable();

        let createNewUser = () => {
            newUser.push(selectedUser());
        }
        let registerBtn = () =>
            console.log("registerbtn clicked");

        //supposed to  - when the user clicks on the button, toggle between hiding and showing the dropdown content(?)
        //      let registerBtn = () => console.log("Login button clicked");

        let signInBtn = () => console.log("Sign in button clicked");

        return {
            newUser,
            selectedUser,
            createNewUser,
            registerBtn
        }
    }
});