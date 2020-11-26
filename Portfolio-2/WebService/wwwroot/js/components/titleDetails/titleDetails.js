define(['knockout', 'dataservice'], (ko, ds) => {
    return function (params) {
        let title = ko.observableArray();
        let tconst = params.tconst;

        ds.getTitle(tconst, function (data) { title(data) });
        console.log(ko.toJSON(title));
        return {
            title
        }
    }
});