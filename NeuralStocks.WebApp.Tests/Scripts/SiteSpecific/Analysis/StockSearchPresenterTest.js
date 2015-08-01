/// <reference path="../../../../NeuralStocks.WebApp/Scripts/SiteSpecific/Analysis/StockSearchPresenter.js" />
/// <reference path="../../../../neuralstocks.webapp/scripts/sitespecific/util/TableSetupManager.js" />
/// <reference path="../../TestHelper/TestHelper.js" />

describe("StockSearchPresenterTest", function () {

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

    describe("InitializeView", function () {
        var initializeTableStub;
        var table;
        var tableId;
        var jQuerySelectorStub;

        beforeEach(function() {
            initializeTableStub = sinon.stub(TableSetupManager, "initializeTable");
            jQuerySelectorStub = sinon.stub(window, "$");

            tableId = "companySearchTable";
            table = document.createElement("table");
            table.setAttribute("id", tableId);
            mockBody.appendChild(table);
        });

        afterEach(function() {
            initializeTableStub.restore();
            jQuerySelectorStub.restore();

            $(mockBody).empty();
        });

        it("testCallsInitializeTableOnTableSetupManager", function () {
            jQuerySelectorStub.withArgs("#" + tableId).returns(table);
            assertFalse(initializeTableStub.called);

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

            var presenter = new StockSearchPresenter({}, {});
            presenter.initializeView();

            assertTrue(initializeTableStub.calledOnce);
            assertEquals(2, initializeTableStub.getCall(0).args.length);
            assertEquals(table, initializeTableStub.getCall(0).args[0]);
        });
    });
});