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

    describe("AddSortingToTableHeader_CssAndSortAttribute", function () {
        var tableHeader1;
        var tableHeader2;
        var tableHeader3;
        var tableHeaderRow;
        var tableHeader;

        var tableDataRow1;
        var tableDataRow2;
        var tableDataRow3;
        var tableBody;
        var table;

        beforeEach(function () {
            tableHeader1 = document.createElement("th");
            tableHeader1.classList.add("sorting");
            tableHeader2 = document.createElement("th");
            tableHeader2.classList.add("sorting");
            tableHeader3 = document.createElement("th");

            tableHeaderRow = document.createElement("tr");
            tableHeaderRow.appendChild(tableHeader1);
            tableHeaderRow.appendChild(tableHeader2);
            tableHeaderRow.appendChild(tableHeader3);

            tableHeader = document.createElement("thead");
            tableHeader.setAttribute("id", "companySearchTableHeader");
            tableHeader.appendChild(tableHeaderRow);

            tableDataRow1 = document.createElement("tr");
            var tableDataRow1Data1 = document.createElement("td");
            tableDataRow1Data1.innerHTML = "A";
            tableDataRow1.appendChild(tableDataRow1Data1);
            var tableDataRow1Data2 = document.createElement("td");
            tableDataRow1Data2.innerHTML = "5";
            tableDataRow1.appendChild(tableDataRow1Data2);

            tableDataRow2 = document.createElement("tr");
            var tableDataRow2Data1 = document.createElement("td");
            tableDataRow2Data1.innerHTML = "C";
            tableDataRow2.appendChild(tableDataRow2Data1);
            var tableDataRow2Data2 = document.createElement("td");
            tableDataRow2Data2.innerHTML = "1";
            tableDataRow2.appendChild(tableDataRow2Data2);

            tableDataRow3 = document.createElement("tr");
            var tableDataRow3Data1 = document.createElement("td");
            tableDataRow3Data1.innerHTML = "B";
            tableDataRow3.appendChild(tableDataRow3Data1);
            var tableDataRow3Data2 = document.createElement("td");
            tableDataRow3Data2.innerHTML = "9";
            tableDataRow3.appendChild(tableDataRow3Data2);

            tableBody = document.createElement("tbody");
            tableBody.setAttribute("id", "companySearchTableBody");
            tableBody.appendChild(tableDataRow1);
            tableBody.appendChild(tableDataRow2);
            tableBody.appendChild(tableDataRow3);

            table = document.createElement("table");
            table.appendChild(tableHeader);
            table.appendChild(tableBody);
        });

        it("testClickSortsTable", function () {
            TableSortingManager.addSortingToTableHeader(table);

            assertEquals(tableDataRow1, tableBody.childNodes[0]);
            assertEquals(tableDataRow2, tableBody.childNodes[1]);
            assertEquals(tableDataRow3, tableBody.childNodes[2]);

            tableHeader1.onclick();

            assertEquals(tableDataRow1, tableBody.childNodes[0]);
            assertEquals(tableDataRow3, tableBody.childNodes[1]);
            assertEquals(tableDataRow2, tableBody.childNodes[2]);

            tableHeader1.onclick();

            assertEquals(tableDataRow2, tableBody.childNodes[0]);
            assertEquals(tableDataRow3, tableBody.childNodes[1]);
            assertEquals(tableDataRow1, tableBody.childNodes[2]);

            tableHeader1.onclick();

            assertEquals(tableDataRow1, tableBody.childNodes[0]);
            assertEquals(tableDataRow3, tableBody.childNodes[1]);
            assertEquals(tableDataRow2, tableBody.childNodes[2]);

            tableHeader2.onclick();

            assertEquals(tableDataRow2, tableBody.childNodes[0]);
            assertEquals(tableDataRow1, tableBody.childNodes[1]);
            assertEquals(tableDataRow3, tableBody.childNodes[2]);

            tableHeader1.onclick();

            assertEquals(tableDataRow1, tableBody.childNodes[0]);
            assertEquals(tableDataRow3, tableBody.childNodes[1]);
            assertEquals(tableDataRow2, tableBody.childNodes[2]);
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