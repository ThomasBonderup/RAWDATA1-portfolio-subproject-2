define([], () => {
   
   let getTitles = (callback) =>
   {
      fetch('api/titles')
          .then(response => response.json())
          .then(callback);
   }    
   
   return {
      getTitles
   }
});