var StockSearchPresenter = (function() {

    var modualStockSearchPresenter = function() {
    };

    modualStockSearchPresenter.prototype.initialize = function() {
        var stockSearchView = new StockSearchView();
        stockSearchView.initializeView(this);
    };

    modualStockSearchPresenter.searchButtonCallback = function() {

    };

    return modualStockSearchPresenter;
})();