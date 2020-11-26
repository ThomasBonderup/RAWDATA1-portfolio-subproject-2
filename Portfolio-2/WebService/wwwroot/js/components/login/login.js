define(['knockout'], function(login){
    var newUser = login.observable();
    
    function signIn(signIn){
        this.signIn = login.observable(signIn);
    }
    
    let loginBtn = () => console.log("Login button clicked");
    
    return{
        newUser,
        loginBtn
    }
    
});