define([], () => {
   const titleApiUrl = "api/titles";
   const userApiUrl = "api/users"
   
   let getJSON = (url, callback) => {
      fetch(url, {
         headers : {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            'Authorization': 'ui000001'
         }
      }).then(response => response.json().then(callback));
   }

   let getUser = (uconst, callback) => {
      let url = userApiUrl + "/" + uconst().trim();
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
   }
});