var AnalysisMain = (function() {
    var classAnalysisMain = {};

    classAnalysisMain.main = function () {
        var tableParameters = {
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

        TableSortingManager.initializeTable($("#companySearchTable"), tableParameters);
    };

    return classAnalysisMain;
})();