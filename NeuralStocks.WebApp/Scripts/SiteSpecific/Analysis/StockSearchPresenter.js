var StockSearchPresenter = (function() {

    var classStockSearchPresenter = function() {
        this.stockSearchView = new StockSearchView();
    };

    classStockSearchPresenter.prototype.initialize = function() {
        this.stockSearchView.initializeView(this);
    };

    classStockSearchPresenter.prototype.searchButtonCallback = function (event) {
        debugger;
        var view = event.data.context;
        var searchText = view.searchTextBox.val();
        var url = "/Analysis/GetCompanyLookup?searchString=" + searchText;

        AjaxRequestHandler.requestGet(url, function (status, response) {
            console.log(response);
            var jsonData = JSON.parse(response);
            view.setTableData(jsonData);
        });
    };

    return classStockSearchPresenter;
})();