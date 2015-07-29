var TableSortingManager = (function() {
    var classTableSortingManager = {};

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
            return 1;
        } else {
            direction = "desc";
            context.setAttribute("aria-sort", direction + "ending");
            context.className += direction;
            return -1;
        }
    };

    var sortColumn = function (context) {
        var direction = updateIcon(context);

        var headerRow = context.parentNode;
        var sortableHeaders = headerRow.getElementsByClassName("sorting");
        var index = $.inArray(context, sortableHeaders);

        var tableHeader = headerRow.parentNode;
        var tableNode = tableHeader.parentNode;
        var tableBody = tableNode.querySelectorAll("#companySearchTableBody")[0];
        var items = [];
        var tableBodyRows = $(tableBody).find("tr");

        tableBodyRows.each(function () {
            var item = {};
            item.tr = this;
            var tableData = $(this).find("td")[index];
            item.val = $(tableData).text();
            items.push(item);
        });

        var sortItems = function (first, second) {
            if (first.val < second.val) {
                return -1 * direction;
            }
            if (first.val > second.val) {
                return 1 * direction;
            }
            return 0;
        };

        items.sort(sortItems);

        tableBody.innerHTML = "";

        for (var j = 0; j < items.length; j++) {
            tableBody.appendChild(items[j].tr);
        }
    };

    classTableSortingManager.addSortingToTableHeader = function (table) {
        var tableHeader = table.querySelectorAll("#companySearchTableHeader")[0];
        var sortableHeaders = tableHeader.getElementsByClassName("sorting");

        for (var i = 0; i < sortableHeaders.length; i++) {
            var tempHeader = sortableHeaders[i];
            tempHeader.setAttribute("aria-sort", "none");
            tempHeader.onclick = function() {
                sortColumn(this);
            };
        }
    };

    return classTableSortingManager;
})();