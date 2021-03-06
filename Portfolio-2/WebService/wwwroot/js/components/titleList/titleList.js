define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) =>  {
   return function (params) {
       let titles = ko.observableArray([]);
       let pageSizes = [5, 10, 15, 20, 25];
       let selectedPageSize = ko.observableArray([10]);
       let searchString = params.searchInput;
       let prev = ko.observable();
       let next = ko.observable();
       let selectedTitle = ko.observable();
       
       let selectTitle = title => {
           selectedTitle(title);
           //console.log(selectedTitle());
           postman.publish('changeTitle', selectedTitle());
       }
       
       let getData = url => {
           ds.getTitles(url, searchString(), function (data) {
               prev(data.prev || undefined);
               next(data.next || undefined);
               titles(data.items);
               console.log(titles());
               //console.log(selectedTitle());
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
       
       selectedPageSize.subscribe(() => {
           let size = selectedPageSize()[0];
           getData(ds.getTitlesUrlWithPageSize(size)); 
       });
       
       getData();
       
       
       
       return {
           pageSizes,
           selectedPageSize,
           showPrev,
           showNext,
           titles,
           enablePrev,
           enableNext,
           selectTitle,
           selectedTitle
       }
   } 
});