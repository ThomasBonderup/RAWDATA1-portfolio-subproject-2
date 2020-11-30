define(['knockout'], function(login){
    var newUser = login.observable();
    
    function loginFunction(){
        document.getElementById("dropdownLogin").classList.toggle("show");
    }
    
   /* let loginBtn = () => console.log("Login button clicked");
    
    let signInBtn = () => console.log("Sign in button clicked");*/
    
    return{
        newUser,
        loginFunction
        //loginBtn,
        //signInBtn
    }
    
});