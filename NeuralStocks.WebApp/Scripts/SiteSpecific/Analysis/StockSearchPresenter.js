var StockSearchPresenter = (function() {

    var modualStockSearchPresenter = function() {
        this.stockSearchView = new StockSearchView();
    };

    modualStockSearchPresenter.prototype.initialize = function() {
        this.stockSearchView.initializeView(this);
    };

    modualStockSearchPresenter.searchButtonCallback = function() {

    };

    return modualStockSearchPresenter;
})();