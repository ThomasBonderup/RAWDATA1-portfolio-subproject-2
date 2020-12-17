define([], () => {
   const titleApiUrl = "api/titles";
   const userApiUrl = "api/users";
   const namesApiUrl = "api/names";
   
   // Private function
   let getJSON = (url, callback) => {
      fetch(url, {
         headers : {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            'Authorization': 'Bearer ' + localStorage.getItem('jwtToken')
         }
      }).then(response => response.json().then(callback));
   }
  
   // Private function
   let postJSON = (url, data, callback) => {
      fetch(url, {
         headers : {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            'Authorization': 'Bearer ' + localStorage.getItem('jwtToken')
         },
         body: data,
         method: "POST"
      }).then(response => response.json().then(callback));
   }

   
   let updateUser = (uconst, firstName, lastName, email, password, username, callback) => {
      let url = userApiUrl + "/" + username;
      let data = new Object;
      data.uconst = uconst;
      data.firstName = firstName;
      data.lastName = lastName;
      data.email = email;
      data.password = password;
      data.lastName = lastName;
      data.userName = username;
      
      console.log(data.uconst);
      console.log(data.firstName);
      console.log(data.lastName);
      console.log(data.email);
      console.log(data.password);
      console.log(data.lastName);
      console.log(data.username);
      
      postJSON(url, JSON.stringify(data), callback);
   }
   
   let getUser = (username, callback) => {
      let url = userApiUrl + "/" + username();
      console.log("getUser api url: " + url);
      getJSON(url, callback);
   }
   
   /*
   let getUser = (uconst, callback) =>
   {
      fetch('api/users/' + uconst,{
         header : {
            'content-type': 'application/json',
            'Accept': 'application/json'
         }
      })
          .then(response => response.json())
          .then(callback);
   }
   */

   let userLogin = (username, password, callback) => {
      let url = userApiUrl + "/login";
      let data = new Object;
      data.username = username;
      data.password = password;
      console.log(JSON.stringify(data));
      fetch(url, {
         headers : {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
         },
         body: JSON.stringify(data),
         method: "POST"
      }).then(response => response.json().then(response => localStorage.setItem("jwtToken", response.token)))
   };
   
   let registerUser = (firstname, lastname, email, username, password, callback) => {
     let url = userApiUrl + "/register";
     let data = new Object;
     data.firstname = firstname;
     data.lastname = lastname;
     data.email = email;
     data.username = username;
     data.password = password;
     console.log(JSON.stringify(data));
     
     postJSON(url, JSON.stringify(data), callback);
   };

   let postUser = (callback) =>{
      fetch(user,{
         header:{
            
         }
      })
          .then(response => response.json())
          .then(callback);
      
   }
   
   
   let getTitles = (url, searchString, callback) => {
      if (url === undefined) {
         url = titleApiUrl + "/search-title/" + searchString;
      }
      getJSON(url, callback);
   };
   
   let getRating = (tconst, callback) => {
      let url = titleApiUrl + "/" + tconst().trim() + "/titlerating";
      console.log("getRating api url: " + url);
      getJSON(url, callback);
   }
   
   let getGenres = (tconst, callback) => {
      let url = titleApiUrl + "/" + tconst().trim() + "/titlegenres";
      console.log("getRating api url: " + url);
      getJSON(url, callback);
   }
   
   let getTitlePrincipals = (tconst, callback) => {
      let url = titleApiUrl + "/" + tconst().trim() + "/titleprincipals";
      console.log("getTitlePrincipals" + url);
      getJSON(url, callback);
   }
   
   let giveTitleReview = (uconst, tconst, rating, review, callback) => {
      let url = titleApiUrl + "/titlerating";
      let data = new Object;
      data.uconst = uconst;
      data.tconst = tconst;
      data.rating = parseFloat(rating);
      data.review = review;
      let data1 = JSON.stringify(data);
      console.log(data1);
      postJSON(url, JSON.stringify(data), callback);
   }
   
   let addToBookmarks = (uconst, tconst, callback) => {
      let url = userApiUrl + "/" + uconst + "/titlebookmarks/" + tconst;
      let data = new Object;
      data.uconst = uconst;
      data.tconst = tconst;
      
      postJSON(url, JSON.stringify(data), callback);
   }

   let getActors = (url, searchString, callback) => {
      url = titleApiUrl + "/titleprincipals/" + searchString;
      getJSON(url, callback);
   };
   
   // TODO pageSize feature do not work with searchString
   let getTitlesUrlWithPageSize = size => titleApiUrl + "?pageSize=" + size;
   
   // let getTitles = (searchString, callback) =>
   // {
   //    fetch('api/titles/search-title/' + searchString, {
   //       headers : {
   //          'Content-Type': 'application/json',
   //          'Accept': 'application/json',
   //          'Authorization': 'ui000001'
   //       }
   //    })
   //        .then(response => response.json())
   //        .then(callback);
   // }
   
   let getTitle = (url, tconst, callback) => {
      if (url === undefined) {
         url = titleApiUrl + "/" + tconst;
         
         getJSON(url, callback);
      }
   }
   
   let getNames = (callback) =>
   {
      fetch('api/names')
          .then(response => response.json())
          .then(callback);
   }
   
   let getRatingHistory = (uconst, callback) =>
   {
      var url = userApiUrl + "/" + uconst + "/" + "ratinghistory";
      getJSON(url, callback);
      
   }
   
   let getSearchHistory = (uconst, callback) => 
   {
      var url = userApiUrl + "/" + uconst + "/" + "searchhistory";
      getJSON(url, callback);
   }
   
   
   let getTitleBookmarks = (uconst, callback) =>
   {
      var url = userApiUrl + "/" + uconst + "/" + "titlebookmarks";
      getJSON(url, callback);
   }
   
   
   return {
      getTitles,
      getTitle,
      getTitlesUrlWithPageSize,
      getNames,
      getUser,
      postUser,
      getRating,
      getGenres,
      getTitlePrincipals,
      giveTitleReview,
      addToBookmarks,
      getActors,
      updateUser,
      userLogin,
      registerUser,
      getRatingHistory,
      getSearchHistory,
      getTitleBookmarks,
      
   }
});