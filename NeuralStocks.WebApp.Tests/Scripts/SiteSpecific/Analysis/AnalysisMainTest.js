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
        var initializeStub;
        var mockStockSearchPresenter;

        beforeEach(function() {
            initializeStub = sinon.stub();
            mockStockSearchPresenter = sinon.stub();

            window.StockSearchPresenter = function() {
                mockStockSearchPresenter.initialize = initializeStub;
                return mockStockSearchPresenter;
            };
        });

        afterEach(function() {
        });

        it("testMainCallsInitializePresenterOnPresenter", function() {
            assertFalse(initializeStub.calledOnce);

            AnalysisMain.main();

            assertTrue(initializeStub.calledOnce);
        });
    });
});