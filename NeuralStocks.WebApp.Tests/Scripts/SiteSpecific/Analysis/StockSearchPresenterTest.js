/// <reference path="../../../../neuralstocks.webapp/scripts/sitespecific/analysis/stocksearchpresenter.js" />
/// <reference path="../../../../neuralstocks.webapp/scripts/sitespecific/analysis/stocksearchview.js" />
/// <reference path="../../../../neuralstocks.webapp/scripts/sitespecific/util/ajaxrequesthandler.js" />
/// <reference path="../../TestHelper/TestHelper.js" />
describe("StockSearchPresenterTest", function() {
    var mockBody;
    var tempBody;

    var initializeViewStub;
    var mockStockSearchView;

    beforeEach(function() {
        tempBody = document.body;
        mockBody = document.createElement("body");
        document.body = mockBody;
    });

    afterEach(function() {
        document.body = tempBody;
    });

    describe("SearchButtonCallback", function() {
        var requestGetStub;
        var mockEvent;

        beforeEach(function() {
            requestGetStub = sinon.stub(AjaxRequestHandler, "requestGet");

            mockStockSearchView = sinon.stub();
            mockStockSearchView.searchTextBox = { val: sinon.stub() };
            mockStockSearchView.setTableData = sinon.stub();

            window.StockSearchView = function() {
                return mockStockSearchView;
            };
            mockEvent = { data: { context: mockStockSearchView } };
        });

        afterEach(function() {
            requestGetStub.restore();
        });

        it("testPassesCorrectUrlToAjaxRequestHandler", function() {
            var expectedSearchString = "searrrrrrccchhhhhh";

            mockStockSearchView.searchTextBox.val.returns(expectedSearchString);

            var expecetdUrl = "/Analysis/GetCompanyLookup?searchString=" + expectedSearchString;

            assertEquals(0, requestGetStub.callCount);

            var stockSearchPresenter = new StockSearchPresenter();
            stockSearchPresenter.searchButtonCallback(mockEvent);

            assertEquals(1, requestGetStub.callCount);
            assertEquals(2, requestGetStub.getCall(0).args.length);
            assertEquals(expecetdUrl, requestGetStub.getCall(0).args[0]);
        });

        it("testPassesJsonFromAjaxRequestHandlerCallbackToDataTable", function() {
            var expectedStatus = 200;
            var expectedJsonString = "{\"id\":\"json data\"}";

            assertEquals(0, requestGetStub.callCount);

            var stockSearchPresenter = new StockSearchPresenter();
            stockSearchPresenter.searchButtonCallback(mockEvent);

            var callbackStub = requestGetStub.getCall(0).args[1];

            var setTableDataStub = mockStockSearchView.setTableData;
            assertFalse(setTableDataStub.called);

            callbackStub(expectedStatus, expectedJsonString);

            assertEquals(1, setTableDataStub.callCount);
            assertEquals(1, setTableDataStub.getCall(0).args.length);
            assertEquals(JSON.parse(expectedJsonString), setTableDataStub.getCall(0).args[0]);
        });
    });

    describe("Initialize", function() {
        beforeEach(function() {
            initializeViewStub = sinon.stub();
            mockStockSearchView = sinon.stub();

            window.StockSearchView = function() {
                mockStockSearchView.initializeView = initializeViewStub;
                return mockStockSearchView;
            };
        });

        afterEach(function() {
        });

        it("testCallsInitializeView", function() {
            assertFalse(initializeViewStub.calledOnce);

            var stockSearchPresenter = new StockSearchPresenter();
            stockSearchPresenter.initialize();

            assertTrue(initializeViewStub.calledOnce);
            assertEquals(1, initializeViewStub.getCall(0).args.length);
            assertEquals(stockSearchPresenter, initializeViewStub.getCall(0).args[0]);
        });
    });
});