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
        var addSortingToTableHeaderStub;
        var table;
        var tableId;
        var jQuerySelectorStub;
        beforeEach(function() {
            addSortingToTableHeaderStub = sinon.stub(TableSortingManager, "addSortingToTable");

            tableId = "companySearchTable";
            table = document.createElement("table");
            table.setAttribute("id", tableId);
            mockBody.appendChild(table);

            jQuerySelectorStub = sinon.stub(window, "$");
        });

        afterEach(function() {
            addSortingToTableHeaderStub.restore();
            jQuerySelectorStub.restore();

            $(mockBody).empty();
        });

        it("testMainAppliesSortingManagerToCompanySearchTable", function () {
            jQuerySelectorStub.withArgs("#" + tableId).returns(table);
            assertFalse(addSortingToTableHeaderStub.called);

            AnalysisMain.main();

            assertTrue(addSortingToTableHeaderStub.calledOnce);
            assertEquals(1, addSortingToTableHeaderStub.getCall(0).args.length);
            assertEquals(table, addSortingToTableHeaderStub.getCall(0).args[0]);
        });
    });
});