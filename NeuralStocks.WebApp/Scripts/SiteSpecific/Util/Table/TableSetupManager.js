var TableSetupManager = (function() {
    var modualTableSetupManager = {};

    modualTableSetupManager.initializeTable = function ($table, tableOptions) {
        $table.DataTable(tableOptions);
    };

    return modualTableSetupManager;
})();