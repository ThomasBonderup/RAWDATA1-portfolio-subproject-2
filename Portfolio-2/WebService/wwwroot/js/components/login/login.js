define(['knockout'], function(login){
    var newUser = [
        {firstName: login.observable(), lastName:login.observable(), email:login.observable(),username:login.observable(), password:login.observable()}
    ];
    let selectedUser = login.observable();
    
    let createNewUser = () =>{
        newUser.push(selectedUser());
    }
    let registerBtn = function(){
        console.log("registerbtn clicked");
    };
    //supposed to  - when the user clicks on the button, toggle between hiding and showing the dropdown content(?)
  //  let registerBtn = () => console.log("Login button clicked");
    
    let signInBtn = () => console.log("Sign in button clicked");
    
    return{
        newUser,
        selectedUser,
      createNewUser,
       registerBtn
    }
    
});