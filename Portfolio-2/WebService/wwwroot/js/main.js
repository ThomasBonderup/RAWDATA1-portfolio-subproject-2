require.config({
    baseUrl: "js",
    paths : {
        knockout: "lib/knockout/knockout-latest",
        dataservice: "services/dataService"
    }
});


require(['knockout', 'viewModel'], function (ko, vm) {
    console.log(vm.name);
    ko.applyBindings(vm);
});