define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) =>  {
    return function (params) {
        let actors = ko.observableArray([]);
        let searchString = ko.observable();
        
        let getData = url => {
            ds.getActors(url, searchString(), function (data) {
                //prev(data.prev || undefined);
                //next(data.next || undefined);
                actors(data);
                console.log(actors());
                //console.log(selectedTitle());
            });
        }
        
        getData();
        
        return {
            actors,
            nconst
        }
    }
});