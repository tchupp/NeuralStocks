/// <reference path="../../../../NeuralStocks.WebApp/Scripts/SiteSpecific/Analysis/StockSearchPresenter.js" />
/// <reference path="../../../../neuralstocks.webapp/scripts/sitespecific/util/table/tableoptions.js" />
/// <reference path="../../../../neuralstocks.webapp/scripts/sitespecific/util/table/tablesetupmanager.js" />
/// <reference path="../../../../neuralstocks.webapp/scripts/sitespecific/util/chart/chartoptions.js" />
/// <reference path="../../../../neuralstocks.webapp/scripts/sitespecific/util/chart/chartsetupmanager.js" />
/// <reference path="../../TestHelper/TestHelper.js" />

describe("StockSearchPresenterTest", function() {
    var mockBody;
    var tempBody;
    var jQuerySelectorStub;

    var initializeTableStub;
    var table;
    var tableId;

    var initializeStockChartStub;
    var chart;
    var chartId;

    beforeEach(function() {
        tempBody = document.body;
        mockBody = document.createElement("body");
        document.body = mockBody;

        initializeTableStub = sinon.stub(TableSetupManager, "initializeTable");
        initializeStockChartStub = sinon.stub(ChartSetupManager, "initializeStockChart");
        jQuerySelectorStub = sinon.stub(window, "$");

        tableId = "companySearchTable";
        table = document.createElement("table");
        table.setAttribute("id", tableId);
        mockBody.appendChild(table);

        chartId = "companySearchChart";
        chart = document.createElement("div");
        chart.setAttribute("id", chartId);
        mockBody.appendChild(chart);
    });

    afterEach(function() {
        document.body = tempBody;

        initializeTableStub.restore();
        initializeStockChartStub.restore();
        jQuerySelectorStub.restore();
    });

    describe("InitializeView_Table", function() {
        it("testCallsInitializeTableOnTableSetupManager", function() {
            jQuerySelectorStub.withArgs("#" + tableId).returns(table);
            assertFalse(initializeTableStub.called);

            var presenter = new StockSearchPresenter();
            presenter.initializeView();

            assertTrue(initializeTableStub.calledOnce);
            assertEquals(2, initializeTableStub.getCall(0).args.length);
            assertEquals(table, initializeTableStub.getCall(0).args[0]);
        });

        it("testCallsInitializeTableOnTable_WithAnalysisSearchTableOptions", function() {
            assertFalse(initializeTableStub.called);

            var presenter = new StockSearchPresenter();
            presenter.initializeView();

            assertTrue(initializeTableStub.calledOnce);
            assertEquals(2, initializeTableStub.getCall(0).args.length);
            assertEquals(TableOptions.analysisSearchTableOptions,
                initializeTableStub.getCall(0).args[1]);
        });
    });

    describe("InitializeView_Chart", function () {
        it("testCallsInitializeChartOnTableSetupManager", function () {
            jQuerySelectorStub.withArgs("#" + chartId).returns(chart);
            assertFalse(initializeStockChartStub.called);

            var presenter = new StockSearchPresenter();
            presenter.initializeView();

            assertTrue(initializeStockChartStub.calledOnce);
            assertEquals(2, initializeStockChartStub.getCall(0).args.length);
            assertEquals(chart, initializeStockChartStub.getCall(0).args[0]);
        });

        it("testCallsInitializeChartOnChartDiv_WithAnalysisSearchTableOptions", function () {
            assertFalse(initializeStockChartStub.called);

            var presenter = new StockSearchPresenter();
            presenter.initializeView();

            assertTrue(initializeStockChartStub.calledOnce);
            assertEquals(2, initializeStockChartStub.getCall(0).args.length);
            assertEquals(ChartOptions.analysisSearchChartOptions,
                initializeStockChartStub.getCall(0).args[1]);
        });
    });
});