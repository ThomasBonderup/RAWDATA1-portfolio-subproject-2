require.config({
    baseUrl: "js",
    paths : {
        knockout: "lib/knockout/knockout-latest.debug",
        text: "lib/require-text/text.min",
        dataservice: "services/dataService",
        jquery: "lib/jquery/jquery.min",
        bootstrap: "../css/lib/twitter-bootstrap/js/bootstrap.bundle.min",
        postman: "services/postman"
    },
    shim: {
        bootstrap: ['jquery']
    }
    
});

require(['knockout', 'text'], (ko) => {
    ko.components.register("title-list",
        {
            viewModel: { require: "components/titleList/titleList" },
            template: { require: "text!components/titleList/titleList.html"}
        });

    ko.components.register("user",
        {
            viewModel: {require: "components/user/user"},
            template: {require: "text!components/user/user.html"} 

        });

    ko.components.register("title-details",
        {
            viewModel: { require: "components/titleDetails/titleDetails" },
            template: { require: "text!components/titleDetails/titleDetails.html" }
        });
    
    ko.components.register("login",
        {
        viewModel: {require: "components/login/login"},
        template:{require: "text!components/login/login.html"}
    })

    ko.components.register("about",
        {
            viewModel: {require: "components/about/about"},
            template:{require: "text!components/about/about.html"}
        })

    ko.components.register("home",
        {
            viewModel: {require: "components/home/home"},
            template:{require: "text!components/home/home.html"}
        })
});



require(['knockout', 'viewModel', 'bootstrap'], function (ko, vm) {
    ko.applyBindings(vm);
});