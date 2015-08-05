/// <reference path="../../../../neuralstocks.webapp/scripts/sitespecific/analysis/stocksearchpresenter.js" />
/// <reference path="../../../../neuralstocks.webapp/scripts/sitespecific/analysis/stocksearchview.js" />
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

    describe("SearchButtonCallback", function() {
        it("test", function() {
            
        });
    });

    describe("Initialize", function () {
        var initializeViewStub;
        var mockStockSearchView;

        beforeEach(function () {
            initializeViewStub = sinon.stub();
            mockStockSearchView = sinon.stub();

            window.StockSearchView = function () {
                mockStockSearchView.initializeView = initializeViewStub;
                return mockStockSearchView;
            };
        });

        afterEach(function () {
        });

        it("testCallsInitializeView", function () {
            assertFalse(initializeViewStub.calledOnce);

            var stockSearchPresenter = new StockSearchPresenter();
            stockSearchPresenter.initialize();

            assertTrue(initializeViewStub.calledOnce);
            assertEquals(1, initializeViewStub.getCall(0).args.length);
            assertEquals(stockSearchPresenter, initializeViewStub.getCall(0).args[0]);
        });
    });
});