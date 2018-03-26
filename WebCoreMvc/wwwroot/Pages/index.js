define(['text!./index.html'], function (htmlstr) {
    function view(param) {
        var viewModel = {};
        viewModel.text = ko.observable('欢迎来到我的地盘');
        return viewModel;
    }
    return {
        viewModel: view,
        template: htmlstr
    }
});