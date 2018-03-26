(function () {

    requirejs.config({
        baseUrl: '/',
        paths: {
            'text': 'js/text',
            'director': 'js/director',
            'componentsRegister': 'js/componentsRegister'
        }
    });
    //var s = window.location.pathname.split('/');
    //var m = "View/" + s[1] + "/" + s[2];
    //require([m], function (s) {

    //});

    require(['director', 'componentsRegister'], function () {
        var author = function () { console.log("author"); },
            books = function () { console.log("books"); },
            viewBook = function (bookId) { console.log("viewBook: bookId is populated: " + bookId); };

        var menu = [
            {
                url: '/author', componet: 'x01',
            },
            { url: '/books/view/:bookId', componet: 'X01', params: ["bookId"] }
        ]
        var viewModel = {};
        viewModel.ComponentName = ko.observable();
        viewModel.RouterParams = ko.observable();
        var routes = {};
        menu.forEach(function (item) {
            routes[item.url] = function () {
                var json = {};
                for (var i = 0; i < arguments.length; i++) {
                    json[item.params[i]] = arguments[i];
                }
                viewModel.RouterParams = json;
                viewModel.ComponentName(item.componet);
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