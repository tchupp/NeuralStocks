var StockSearchPresenter = (function() {
    var classStockSearchPresenter = function() {
    };

    classStockSearchPresenter.prototype.initializeView = function() {
        TableSetupManager.initializeTable($("#companySearchTable"),
            TableOptions.analysisSearchTableOptions);

        ChartSetupManager.initializeStockChart($("#companySearchChart"),
            ChartOptions.analysisSearchChartOptions);
    };

    return classStockSearchPresenter;
})();