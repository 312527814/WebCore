define(['text!./myTest2.html'], function (htmlstr) {
    function view(param) {
        var viewModel = {};
        viewModel.id = ko.observable(param.id);
        return viewModel;
    }
    return {
        viewModel: view,
        template: htmlstr
    }
});