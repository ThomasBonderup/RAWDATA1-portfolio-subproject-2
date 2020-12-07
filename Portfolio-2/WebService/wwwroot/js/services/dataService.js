define([], () => {
   const titleApiUrl = "api/titles";
   
   let getJSON = (url, callback) => {
      fetch(url, {
         headers : {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            'Authorization': 'ui000001'
         }
      }).then(response => response.json().then(callback));
   }
   
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
   
   let getTitle = (tconst, callback) => {
      fetch('api/titles/' + tconst, {
         headers : {
            'Content-Type': 'application/json',
            'Accept': 'application/json'
         }
      })
          .then(response => response.json())
          .then(callback);
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
      getNames,
      getUser,
      postUser
   }
});