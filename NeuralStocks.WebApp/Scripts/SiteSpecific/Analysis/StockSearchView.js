var StockSearchView = (function() {
    var classStockSearchView = function() {
    };

    classStockSearchView.prototype.initializeView = function(searchPresenter) {
        TableSetupManager.initializeTable($("#companySearchTable"),
            TableOptions.analysisSearchTableOptions);

        ChartSetupManager.initializeStockChart($("#companySearchChart"),
            ChartOptions.analysisSearchChartOptions);

        $("#companySearchButton").click(searchPresenter.searchButtonCallback);
    };

    return classStockSearchView;
})();