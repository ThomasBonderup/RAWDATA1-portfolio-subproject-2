define([], () => {
   
   let getTitles = (callback) =>
   {
      fetch('api/titles')
          .then(response => response.json())
          .then(callback);
   }
   
   let getTitle = (tconst, callback) => {
      fetch('api/titles/' + tconst)
          .then(response => response.json())
          .then(callback);
   }
   
   return {
      getTitles,
      getTitle
   }
});