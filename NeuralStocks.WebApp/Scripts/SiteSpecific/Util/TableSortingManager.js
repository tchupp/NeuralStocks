var TableSortingManager = (function() {
    var classTableSortingManager = {};

    var sortColumn = function(context) {
        var headerRow = context.parentNode;
        var sortableHeaders = headerRow.getElementsByClassName("sorting");

        var tableBody = headerRow.parentNode.parentNode.childNodes[1];
    };

    var updateIcon = function(context) {
        var currentSortDirection = context.getAttribute("aria-sort");
        var headerRow = context.parentNode;
        var sortableHeaders = headerRow.getElementsByClassName("sorting");

        for (var i = 0; i < sortableHeaders.length; i++) {
            sortableHeaders[i].className = "sorting ";
            sortableHeaders[i].setAttribute("aria-sort", "none");
        }

        var direction = "";
        if (currentSortDirection === "descending" || currentSortDirection === "none") {
            direction = "asc";
            context.setAttribute("aria-sort", direction + "ending");
            context.className += direction;
        } else if (currentSortDirection === "ascending") {
            direction = "desc";
            context.setAttribute("aria-sort", direction + "ending");
            context.className += direction;
        }
    };

    classTableSortingManager.addSortingToTableHeader = function (table) {
        var tableHeader = table.querySelectorAll("#companySearchTableHeader")[0];
        var sortableHeaders = tableHeader.getElementsByClassName("sorting");

        for (var i = 0; i < sortableHeaders.length; i++) {
            var tempHeader = sortableHeaders[i];
            tempHeader.setAttribute("aria-sort", "none");
            tempHeader.onclick = function() {
                updateIcon(this);
                sortColumn(this);
            };
        }
    };

    return classTableSortingManager;
})();