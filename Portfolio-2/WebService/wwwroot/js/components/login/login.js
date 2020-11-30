define(['knockout'], function(login){
    var newUser = login.observable();
    
    //supposed to  - when the user clicks on the button, toggle between hiding and showing the dropdown content(?)
    function loginFunction(){
        document.getElementById("dropdownLogin").classList.toggle("show");
    }
    
    window.onclick = function(event){
        if(!event.target.matches('.dropBtn')){
            var dropdowns = document.getElementsByClassName("dropdown-content");
            var i;
            for (i=0; i<dropdowns.length; i++){
                var openDropdown = dropdowns[i];
                if(openDropdown.classList.contains('show')){
                    openDropdown.classList.remove('show');
                }
            }
        }
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