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
});

require(['knockout', 'viewModel'], function (ko, vm) {
    console.log(vm.name);
    ko.applyBindings(vm);
});