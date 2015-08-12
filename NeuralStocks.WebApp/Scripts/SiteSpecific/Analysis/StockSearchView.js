var StockSearchView = (function() {
    var classStockSearchView = function() {
        this.searchTable = $("#companySearchTable");
        this.stockChart = $("#companySearchChart");
        this.searchButton = $("#companySearchButton");
        this.searchTextBox = $("#companySearchTextBox");
    };

    classStockSearchView.prototype.initializeView = function(searchPresenter) {
        TableSetupManager.initializeTable(this.searchTable,
            TableOptions.analysisSearchTableOptions);

        ChartSetupManager.initializeStockChart(this.stockChart,
            ChartOptions.analysisSearchChartOptions);

        this.searchButton.click({ context: this }, searchPresenter.searchButtonCallback);
    };

    classStockSearchView.prototype.setTableData = function(tableData) {
        var dataTable = this.searchTable.dataTable();
        dataTable.fnAddData(tableData);
    };

    return classStockSearchView;
})();