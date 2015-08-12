var AnalysisMain = (function() {
    var classAnalysisMain = function() {
        this.stockSearchPresenter = new StockSearchPresenter();
    };

    classAnalysisMain.prototype.main = function() {
        this.stockSearchPresenter.initialize();
    };

    return classAnalysisMain;
})();