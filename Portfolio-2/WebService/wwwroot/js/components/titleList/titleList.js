define(['knockout', 'dataservice'], (ko, ds) =>  {
   return function (params) {
       let titles = ko.observableArray([]);
       let searchString = params.searchInput;
       let prev = ko.observable();
       let next = ko.observable();
       
       let getData = url => {
           ds.getTitles(url, searchString(), function (data) {
               prev(data.prev);
               next(data.next);
               titles(data);
           });
       }

       let showPrev = title => {
           console.log(prev());
           getData(prev());
       }

       let showNext = title => {
           console.log(next());
           getData(next());
       }
       
       getData();
       
       
       
       //debugger;
       //console.log(ko.toJSON(titles));
       return {
           showPrev,
           showNext,
           titles
       }
   } 
});