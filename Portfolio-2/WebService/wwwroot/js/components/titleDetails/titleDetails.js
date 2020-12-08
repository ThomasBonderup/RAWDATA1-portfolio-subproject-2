define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        //let title = ko.observableArray();
        //let tconst = params.tconst;

        //ds.getTitle(tconst, function (data) { title(data) });
        
        let titles = ko.observable();
        
        let tconst = ko.observable();
        let rating = ko.observable();
        
        postman.subscribe('changeTitle', title => {
            titles(title);
            tconst(titles().tconst);
            console.log(titles());
            console.log(tconst());
        });
        
        ds.getRating(tconst, function (data) {
            rating(data);
            console.log(rating());
        });
        
        return {
            titles,
            rating
        }
    }
});