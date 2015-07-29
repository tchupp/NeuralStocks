var AnalysisMain = (function() {
    var classAnalysisMain = {};

    classAnalysisMain.main = function () {
        var companySearchTable = document.getElementById("companySearchTable");
        TableSortingManager.addSortingToTableHeader(companySearchTable);
    };

    return classAnalysisMain;
})();