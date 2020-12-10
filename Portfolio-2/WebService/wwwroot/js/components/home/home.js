define(["knockout","dataservice"], (ko, ds)=> {
    return function (params) {

        let username = ko.observable();
        let password = ko.observable();

        let loginUser = (function(data) {
            console.log(username());
            console.log(password());
            ds.userLogin(username(), password());
        });

        return {
            username,
            password,
            loginUser
        }
    }
});