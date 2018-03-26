define(function (html) {
    function aa(params) {
        debugger;
        var viewModel = {};
        viewModel.Name = ko.observable('dd');
        ko.applyBindings(viewModel);
    }
    
    return {
        model: aa
    }
});