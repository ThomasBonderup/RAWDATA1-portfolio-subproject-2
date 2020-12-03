define(['knockout', 'dataservice'], (ko, ds) =>  {
   return function (params) {
       let titles = ko.observableArray([]);
       let searchString = params.searchInput;
       let prev = ko.observable();
       let next = ko.observable();
       
       let showPrev = title => {
           console.log(prev());
       }

       let showNext = title => {
           console.log(next());
       }
       
       ds.getTitles(ko.unwrap(searchString), function (data) {
           prev(data.prev);
           next(data.next);
           titles(data);
       });
       //debugger;
       //console.log(ko.toJSON(titles));
       return {
           showPrev,
           showNext,
           titles
       }
   } 
});