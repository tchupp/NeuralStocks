var TableSortingManager = (function() {
    var classTableSortingManager = {};

    classTableSortingManager.initializeTable = function ($table, tableParameters) {
        $table.DataTable(tableParameters);
    };

    return classTableSortingManager;
})();