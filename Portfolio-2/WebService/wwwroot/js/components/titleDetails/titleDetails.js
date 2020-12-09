define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        //let title = ko.observableArray();
        //let tconst = params.tconst;

        //ds.getTitle(tconst, function (data) { title(data) });
        
        let titles = ko.observable();
        
        let tconst = ko.observable();
        
        let rating = ko.observable();
        
        let genres = ko.observable();
        
        let titlePrincipals = ko.observableArray();
        
        let actors = ko.observableArray();
        
        let titleRating = ko.observable(5);
        
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
        
        ds.getGenres(tconst, function (data) {
           genres(data);
           console.log(genres());
        });
        
        ds.getTitlePrincipals(tconst, function (data) {
            titlePrincipals(data);
            console.log(titlePrincipals());
        });
        
        return {
            titles,
            rating,
            genres,
            titlePrincipals,
            titleRating
        }
    }
});