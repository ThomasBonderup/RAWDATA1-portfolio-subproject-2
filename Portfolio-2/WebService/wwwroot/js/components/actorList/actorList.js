define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) =>  {
    return function (params) {
        let actors = ko.observable();
        let nconst = ko.observable('nm0000093');
        
        let getData = url => {
            ds.getActors(url, nconst(), function (data) {
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