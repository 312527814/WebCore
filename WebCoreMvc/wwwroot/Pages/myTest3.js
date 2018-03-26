define(['text!./myTest3.html'], function (htmlstr) {
    function view(param) {
        var viewModel = {};
        viewModel.id = ko.observable(param.id);
        viewModel.name = ko.observable(param.name);
        return viewModel;
    }
    return {
        viewModel: view,
        template: htmlstr
    }
});