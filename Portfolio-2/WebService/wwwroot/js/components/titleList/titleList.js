define(['knockout', 'dataservice'], (ko, ds) =>  {
   return function (params) {
       let titles = ko.observableArray([]);
       
       ds.getTitles(function (data) { titles(data)});
       
       return {
           titles
       }
   } 
});