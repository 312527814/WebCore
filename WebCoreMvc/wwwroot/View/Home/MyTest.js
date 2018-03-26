define(function (require) {
    var viewModel = {};
    viewModel.Name = ko.observable('222');
    ko.applyBindings(viewModel);
    return {
        model: viewModel
    }
});