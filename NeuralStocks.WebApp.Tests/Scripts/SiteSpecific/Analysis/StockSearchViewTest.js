/// <reference path="../../../../NeuralStocks.WebApp/Scripts/SiteSpecific/Analysis/StockSearchView.js" />
/// <reference path="../../../../neuralstocks.webapp/scripts/sitespecific/components/table/tableoptions.js" />
/// <reference path="../../../../neuralstocks.webapp/scripts/sitespecific/components/table/tablesetupmanager.js" />
/// <reference path="../../../../neuralstocks.webapp/scripts/sitespecific/components/chart/chartoptions.js" />
/// <reference path="../../../../neuralstocks.webapp/scripts/sitespecific/components/chart/chartsetupmanager.js" />
/// <reference path="../../TestHelper/TestHelper.js" />

describe("StockSearchViewTest", function() {
    var mockBody;
    var tempBody;
    var jQuerySelectorStub;

    var initializeTableStub;
    var table;
    var tableId;

    var initializeStockChartStub;
    var chart;
    var chartId;

    var mockPresenter;
    var buttonId;
    var $button;

    beforeEach(function() {
        tempBody = document.body;
        mockBody = document.createElement("body");
        document.body = mockBody;

        initializeTableStub = sinon.stub(TableSetupManager, "initializeTable");
        initializeStockChartStub = sinon.stub(ChartSetupManager, "initializeStockChart");
        mockPresenter = { searchButtonCallback: sinon.stub() };

        tableId = "companySearchTable";
        table = document.createElement("table");

        chartId = "companySearchChart";
        chart = document.createElement("div");

        buttonId = "companySearchButton";
        $button = { click: sinon.stub() };

        jQuerySelectorStub = sinon.stub(window, "$");
        jQuerySelectorStub.withArgs("#" + tableId).returns(table);
        jQuerySelectorStub.withArgs("#" + buttonId).returns($button);
        jQuerySelectorStub.withArgs("#" + chartId).returns(chart);
    });

    afterEach(function() {
        document.body = tempBody;

        initializeTableStub.restore();
        initializeStockChartStub.restore();
        jQuerySelectorStub.restore();
    });

    describe("InitializeView_Table", function() {
        it("testCallsInitializeTableOnTableSetupManager", function() {
            assertFalse(initializeTableStub.called);

            var view = new StockSearchView();
            view.initializeView(mockPresenter);

            assertEquals(1, initializeTableStub.callCount);
            assertEquals(2, initializeTableStub.getCall(0).args.length);
            assertEquals(table, initializeTableStub.getCall(0).args[0]);
        });

        it("testCallsInitializeTableOnTable_WithAnalysisSearchTableOptions", function() {
            assertFalse(initializeTableStub.called);

            var view = new StockSearchView();
            view.initializeView(mockPresenter);

            assertEquals(1, initializeTableStub.callCount);
            assertEquals(2, initializeTableStub.getCall(0).args.length);
            assertEquals(TableOptions.analysisSearchTableOptions,
                initializeTableStub.getCall(0).args[1]);
        });
    });

    describe("InitializeView_Chart", function() {
        it("testCallsInitializeChartOnTableSetupManager", function() {
            assertFalse(initializeStockChartStub.called);

            var view = new StockSearchView();
            view.initializeView(mockPresenter);

            assertEquals(1, initializeStockChartStub.callCount);
            assertEquals(2, initializeStockChartStub.getCall(0).args.length);
            assertEquals(chart, initializeStockChartStub.getCall(0).args[0]);
        });

        it("testCallsInitializeChartOnChartDiv_WithAnalysisSearchTableOptions", function() {
            assertFalse(initializeStockChartStub.called);

            var view = new StockSearchView();
            view.initializeView(mockPresenter);

            assertEquals(1, initializeStockChartStub.callCount);
            assertEquals(2, initializeStockChartStub.getCall(0).args.length);
            assertEquals(ChartOptions.analysisSearchChartOptions,
                initializeStockChartStub.getCall(0).args[1]);
        });
    });

    describe("InitializeView_Button", function() {
        it("testAddsPresenterCallbackToButton", function() {
            assertFalse($button.click.called);

            var view = new StockSearchView();
            view.initializeView(mockPresenter);

            assertEquals(1, $button.click.callCount);
            assertEquals(1, $button.click.getCall(0).args.length);
            assertEquals(mockPresenter.searchButtonCallback, $button.click.getCall(0).args[0]);
        });
    });
});