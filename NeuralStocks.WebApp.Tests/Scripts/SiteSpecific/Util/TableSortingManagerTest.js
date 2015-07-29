/// <reference path="../../../../neuralstocks.webapp/scripts/sitespecific/util/tablesortingmanager.js" />
/// <reference path="../../TestHelper/TestHelper.js" />

describe("TableSortingManagerTest", function () {
    var mockBody;
    var tempBody;
    beforeEach(function () {
        tempBody = document.body;
        mockBody = document.createElement("body");
        document.body = mockBody;
    });

    afterEach(function() {
        document.body = tempBody;
    });

    describe("AddSortingToTableHeader", function () {
        var tableHeader1;
        var tableHeader2;
        var tableHeader3;
        var tableRow;
        var tableHeader;
        var table;
        beforeEach(function() {
            tableHeader1 = document.createElement("th");
            tableHeader1.classList.add("sorting");
            tableHeader2 = document.createElement("th");
            tableHeader2.classList.add("sorting");
            tableHeader3 = document.createElement("th");

            tableRow = document.createElement("tr");
            tableRow.appendChild(tableHeader1);
            tableRow.appendChild(tableHeader2);
            tableRow.appendChild(tableHeader3);

            tableHeader = document.createElement("thead");
            tableHeader.setAttribute("id", "companySearchTableHeader");
            tableHeader.appendChild(tableRow);

            table = document.createElement("table");
            table.appendChild(tableHeader);
        });

        afterEach(function() {

        });

        it("testAddsAriaSortAttributeToElementsWithSortingClass", function () {
            TableSortingManager.addSortingToTableHeader(table);

            assertEquals("none", tableHeader1.getAttribute("aria-sort"));
            assertEquals("none", tableHeader2.getAttribute("aria-sort"));
        });

        it("testClickChangesAriaSortAttribute", function () {
            TableSortingManager.addSortingToTableHeader(table);

            assertEquals("none", tableHeader1.getAttribute("aria-sort"));
            assertEquals("none", tableHeader2.getAttribute("aria-sort"));

            tableHeader1.onclick();

            assertEquals("ascending", tableHeader1.getAttribute("aria-sort"));
            assertEquals("none", tableHeader2.getAttribute("aria-sort"));

            tableHeader1.onclick();

            assertEquals("descending", tableHeader1.getAttribute("aria-sort"));
            assertEquals("none", tableHeader2.getAttribute("aria-sort"));

            tableHeader1.onclick();

            assertEquals("ascending", tableHeader1.getAttribute("aria-sort"));
            assertEquals("none", tableHeader2.getAttribute("aria-sort"));

            tableHeader2.onclick();

            assertEquals("none", tableHeader1.getAttribute("aria-sort"));
            assertEquals("ascending", tableHeader2.getAttribute("aria-sort"));

            tableHeader1.onclick();

            assertEquals("ascending", tableHeader1.getAttribute("aria-sort"));
            assertEquals("none", tableHeader2.getAttribute("aria-sort"));
        });

        it("testClickChangesClassName", function () {
            TableSortingManager.addSortingToTableHeader(table);

            assertEquals("sorting", tableHeader1.className);
            assertEquals("sorting", tableHeader2.className);

            tableHeader1.onclick();

            assertEquals("sorting asc", tableHeader1.className);
            assertEquals("sorting ", tableHeader2.className);

            tableHeader1.onclick();

            assertEquals("sorting desc", tableHeader1.className);
            assertEquals("sorting ", tableHeader2.className);

            tableHeader1.onclick();

            assertEquals("sorting asc", tableHeader1.className);
            assertEquals("sorting ", tableHeader2.className);

            tableHeader2.onclick();

            assertEquals("sorting ", tableHeader1.className);
            assertEquals("sorting asc", tableHeader2.className);

            tableHeader1.onclick();

            assertEquals("sorting asc", tableHeader1.className);
            assertEquals("sorting ", tableHeader2.className);
        });
    });
});