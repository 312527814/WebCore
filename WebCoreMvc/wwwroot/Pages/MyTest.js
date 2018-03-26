define(['text!./MyTest.html'], function (htmlstr) {
    debugger;
    function view(param) {
        var viewModel = {};
        viewModel.Name = ko.observable('222');
        return viewModel;
    }
    return {
        viewModel: view,
        template: htmlstr
    }
});