var AnalysisMain = (function() {
    var classAnalysisMain = {};

    classAnalysisMain.main = function () {
        TableSortingManager.addSortingToTable($("#companySearchTable"));
    };

    return classAnalysisMain;
})();