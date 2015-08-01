var TableSetupManager = (function() {
    var classTableSetupManager = {};

    classTableSetupManager.initializeTable = function ($table, tableParameters) {
        $table.DataTable(tableParameters);
    };

    return classTableSetupManager;
})();