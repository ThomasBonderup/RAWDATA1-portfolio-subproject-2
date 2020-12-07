define(['knockout', 'dataservice'], (ko, ds) =>  {
   return function (params) {
       let titles = ko.observableArray([]);
       let searchString = params.searchInput;
       let prev = ko.observable();
       let next = ko.observable();
       
       let getData = url => {
           ds.getTitles(url, searchString(), function (data) {
               prev(data.prev || undefined);
               next(data.next || undefined);
               titles(data);
           });
       }

       let showPrev = title => {
           console.log(prev());
           getData(prev());
       }
       
       let enablePrev = ko.computed(() => prev() !== undefined);

       let showNext = title => {
           console.log(next());
           getData(next());
       }
       
       let enableNext = ko.computed(() => next() !== undefined);
       
       getData();
       
       
       
       //debugger;
       //console.log(ko.toJSON(titles));
       return {
           showPrev,
           showNext,
           titles,
           enablePrev,
           enableNext,
       }
   } 
});