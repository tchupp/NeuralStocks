var AnalysisMain = (function() {
    var classAnalysisMain = {};

    classAnalysisMain.main = function () {
        var stockSearchView = new StockSearchView();
        stockSearchView.initializeView();
    };

    return classAnalysisMain;
})();