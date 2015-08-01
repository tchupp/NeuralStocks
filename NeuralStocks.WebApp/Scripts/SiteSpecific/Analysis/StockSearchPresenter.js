var StockSearchPresenter = (function() {
    var classStockSearchPresenter = function() {
        this.tableParameters = {
            info: false,
            columns: [
                { "title": "Name", "class": "col-sm-7" },
                { "title": "Symbol", "class": "col-sm-3" },
                { "title": "Show", "class": "col-sm-2 datatable-nosort" }
            ],
            columnDefs: [
                {
                    targets: "datatable-nosort",
                    orderable: false
                }
            ]
        };
    };

    classStockSearchPresenter.prototype.initializeView = function() {
        TableSetupManager.initializeTable($("#companySearchTable"), this.tableParameters);
    };

    return classStockSearchPresenter;
})();