/// <reference path="../../../../neuralstocks.webapp/scripts/sitespecific/analysis/analysismain.js" />
/// <reference path="../../../../neuralstocks.webapp/scripts/sitespecific/util/tablesortingmanager.js" />
/// <reference path="../../TestHelper/TestHelper.js" />
describe("AnalysisTest", function() {
    var mockBody;
    var tempBody;
    beforeEach(function() {
        tempBody = document.body;
        mockBody = document.createElement("body");
        document.body = mockBody;
    });

    afterEach(function() {
        document.body = tempBody;
    });

    describe("Main", function() {
        var initializeTableStub;
        var table;
        var tableId;
        var jQuerySelectorStub;
        beforeEach(function() {
            initializeTableStub = sinon.stub(TableSortingManager, "initializeTable");

            tableId = "companySearchTable";
            table = document.createElement("table");
            table.setAttribute("id", tableId);
            mockBody.appendChild(table);

            jQuerySelectorStub = sinon.stub(window, "$");
        });

        afterEach(function() {
            initializeTableStub.restore();
            jQuerySelectorStub.restore();

            $(mockBody).empty();
        });

        it("testMainAppliesSortingManagerToCompanySearchTable", function () {
            jQuerySelectorStub.withArgs("#" + tableId).returns(table);
            assertFalse(initializeTableStub.called);

            AnalysisMain.main();

            assertTrue(initializeTableStub.calledOnce);
            assertEquals(2, initializeTableStub.getCall(0).args.length);
            assertEquals(table, initializeTableStub.getCall(0).args[0]);
        });

        it("testMainCallsInitializeTableWithCorrectArgs", function() {
            var expectedArgs = {};
            expectedArgs.info = false;
            expectedArgs.columns = [
                { "title": "Name", "class": "col-sm-7" },
                { "title": "Symbol", "class": "col-sm-3" },
                { "title": "Show", "class": "col-sm-2 datatable-nosort" }
            ];
            expectedArgs.columnDefs = [
                {
                    targets: "datatable-nosort",
                    orderable: false
                }
            ];

            assertFalse(initializeTableStub.called);

            AnalysisMain.main();

            assertTrue(initializeTableStub.calledOnce);
            assertEquals(2, initializeTableStub.getCall(0).args.length);
            assertEquals(expectedArgs, initializeTableStub.getCall(0).args[1]);
        });
    });
});