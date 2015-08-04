/// <reference path="../../../../neuralstocks.webapp/scripts/sitespecific/analysis/analysismain.js" />
/// <reference path="../../../../neuralstocks.webapp/scripts/sitespecific/util/TableSetupManager.js" />
/// <reference path="../../../../NeuralStocks.WebApp/Scripts/SiteSpecific/Analysis/StockSearchView.js" />
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
        var initializeViewStub;
        var mockStockSearchView;

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

        it("testMainCallsInitializeViewOnPresenter", function() {
            assertFalse(initializeViewStub.calledOnce);

            AnalysisMain.main();

            assertTrue(initializeViewStub.calledOnce);
        });
    });
});