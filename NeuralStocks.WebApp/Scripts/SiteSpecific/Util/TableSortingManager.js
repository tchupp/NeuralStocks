var TableSortingManager = (function() {
    var classTableSortingManager = {};

    classTableSortingManager.addSortingToTable = function ($table) {
        $table.DataTable({
            info: false,
            columnDefs: [
                {
                    targets: "datatable-nosort",
                    orderable: false
                }
            ]
        });
    };

    return classTableSortingManager;
})();