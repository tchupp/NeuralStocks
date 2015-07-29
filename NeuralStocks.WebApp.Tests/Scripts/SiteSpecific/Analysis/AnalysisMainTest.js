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
        beforeEach(function() {
            addSortingToTableHeaderStub = sinon.stub(TableSortingManager, "addSortingToTableHeader");

            table = document.createElement("table");
            table.setAttribute("id", "companySearchTable");
            mockBody.appendChild(table);
        });

        afterEach(function() {
            addSortingToTableHeaderStub.restore();
            $(mockBody).empty();
        });

        it("testMainAppliesSortingManagerToCompanySearchTable", function() {
            assertFalse(addSortingToTableHeaderStub.called);

            AnalysisMain.main();

            assertTrue(addSortingToTableHeaderStub.calledOnce);
            assertEquals(1, addSortingToTableHeaderStub.getCall(0).args.length);
            assertEquals(table, addSortingToTableHeaderStub.getCall(0).args[0]);
        });
    });
});