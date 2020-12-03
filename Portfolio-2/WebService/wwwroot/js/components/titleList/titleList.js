define(['knockout', 'dataservice'], (ko, ds) =>  {
   return function (params) {
       let titles = ko.observableArray([]);
       let searchString = params.searchInput;
       ds.getTitles(ko.unwrap(searchString), function (data) { titles(data)});
       //debugger;
       //console.log(ko.toJSON(titles));
       return {
           titles
       }
   } 
});