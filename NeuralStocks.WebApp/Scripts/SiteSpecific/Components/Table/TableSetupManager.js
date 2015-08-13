var TableSetupManager = (function() {
    var modualTableSetupManager = {};

    modualTableSetupManager.initializeTable = function ($table, tableOptions) {
        $table.DataTable(tableOptions);

        $table.find("tbody").on("click", "tr", function () {
            $(this).toggleClass("selected");
        });
    };

    return modualTableSetupManager;
})();