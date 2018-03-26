(function () {

    requirejs.config({
        baseUrl: '/',
        paths: {
            'text': 'js/text',
            'director': 'js/director',
            'componentsRegister': 'js/componentsRegister',
            'routeMap': 'js/routeMap'
        },
        urlArgs: 'v=1.0' + new Date().getTime()
    });
    require(['director', 'componentsRegister', 'routeMap'], function () {
        var viewModel = {};
        viewModel.componentName = ko.observable();
        viewModel.params = ko.observable();
        var routes = {};
        routeMaps.forEach(function (item) {
            routes[item.url] = function () {
                var json = {};
                for (var i = 0; i < arguments.length; i++) {
                    json[item.params[i]] = arguments[i];
                }
                viewModel.componentName(item.componet);
                viewModel.params(json);
            };
        });

        //var routes = {
        //    '/author': author,
        //    '/books': [books, function () { console.log("An inline route handler."); }],
        //    '/books/view/:bookId': function () {
        //        debugger;
        //        var ar = arguments;
        //        //viewModel.ComponentName('X01');
        //        //viewModel.RouterParam({ bookId: bookId });
        //    },
        //    '/books/view/:bookId/:ddd': function () {
        //        debugger;
        //        var ar = arguments;
        //        //viewModel.ComponentName('X01');
        //        //viewModel.RouterParam({ bookId: bookId });
        //    }
        //};

        var router = Router(routes);
        router.init();
        ko.applyBindings(viewModel);
    });
})();