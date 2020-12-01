define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let names = ko.observableArray([]);
        let selectedName = params.selectedName;

        let selectName = name => {
            selectedName(name);
            postman.publish('changeName', name);
        }

        ds.getNames(function (data) { names(data) });

        return {
            names,
            selectName,
            selectedName
        }
    }
});