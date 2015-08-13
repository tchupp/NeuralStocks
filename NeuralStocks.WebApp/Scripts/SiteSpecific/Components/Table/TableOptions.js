var TableOptions = (function() {
    var modualTableOptions = {};

    modualTableOptions.analysisSearchTableOptions = {
        info: false,
        columns: [
            { "title": "Symbol", "class": "col-sm-3 stopselect", "data": "Symbol" },
            { "title": "Name", "class": "col-sm-9 stopselect", "data": "Name" }
        ],
        columnDefs: [
            {
                targets: "datatable-nosort",
                orderable: false
            }
        ]
    };

    return modualTableOptions;
})();