/// <reference path="../../../../neuralstocks.webapp/scripts/sitespecific/analysis/analysismain.js" />
/// <reference path="../../../../neuralstocks.webapp/scripts/sitespecific/util/TableSetupManager.js" />
/// <reference path="../../../../NeuralStocks.WebApp/Scripts/SiteSpecific/Analysis/StockSearchPresenter.js" />
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
        var mockStockSearchPresenter;

        beforeEach(function() {
            initializeViewStub = sinon.stub();
            mockStockSearchPresenter = sinon.stub();

            window.StockSearchPresenter = function() {
                mockStockSearchPresenter.initializeView = initializeViewStub;
                return mockStockSearchPresenter;
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