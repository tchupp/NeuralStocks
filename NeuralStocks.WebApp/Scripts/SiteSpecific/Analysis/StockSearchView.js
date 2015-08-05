var StockSearchView = (function() {
    var classStockSearchView = function () {
        this.searchTable = $("#companySearchTable");
        this.stockChart = $("#companySearchChart");
        this.searchButton = $("#companySearchButton");
    };

    classStockSearchView.prototype.initializeView = function(searchPresenter) {
        TableSetupManager.initializeTable(this.searchTable,
            TableOptions.analysisSearchTableOptions);

        ChartSetupManager.initializeStockChart(this.stockChart,
            ChartOptions.analysisSearchChartOptions);

        this.searchButton.click(searchPresenter.searchButtonCallback);
    };

    return classStockSearchView;
})();