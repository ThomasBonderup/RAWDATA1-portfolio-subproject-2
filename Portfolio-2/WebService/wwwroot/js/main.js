require.config({
    baseUrl: "js",
    paths : {
        knockout: "lib/knockout/knockout-latest",
        text: "lib/require-text/text.min",
        dataservice: "services/dataService"
    }
});

require(['knockout', 'text'], (ko) => {
    ko.components.register("title-list",
        {
            viewModel: { require: "components/titleList/titleList" },
            template: { require: "text!components/titleList/titleList.html"}
        });

    ko.components.register("title-details",
        {
            viewModel: { require: "components/titleDetails/titleDetails" },
            template: { require: "text!components/titleDetails/titleDetails.html" }
        });
    
    ko.components.register("userlist",
        {
        viewModel: {require: "component/login/login"},
        template:{require: "text!components/login/login.html"}
    })
});

require(['knockout', 'viewModel'], function (ko, vm) {
    ko.applyBindings(vm);
});