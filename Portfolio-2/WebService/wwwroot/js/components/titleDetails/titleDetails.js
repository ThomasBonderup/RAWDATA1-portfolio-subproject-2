define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        //let title = ko.observableArray();
        //let tconst = params.tconst;

        //ds.getTitle(tconst, function (data) { title(data) });
        
        let title = ko.observable();
        //
        postman.subscribe('changeTitle', title => {
            console.log(title);
        });
        
        return {
            title
        }
    }
});