/// <reference path="../../../../../neuralstocks.webapp/scripts/sitespecific/components/chart/chartsetupmanager.js" />
/// <reference path="../../../TestHelper/TestHelper.js" />

describe("ChartSetupManagerTest", function() {
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

    describe("InitializeChart", function() {
        var $chart;
        beforeEach(function() {
            $chart = { highcharts: sinon.stub() };
        });

        afterEach(function() {
        });

        it("testSetsUpStockChart_WithCorrectChartOptions", function() {
            assertFalse($chart.highcharts.called);

            var expectedArgs = { id: "testingArguments" };

            ChartSetupManager.initializeStockChart($chart, expectedArgs);

            assertTrue($chart.highcharts.calledOnce);
            assertEquals(2, $chart.highcharts.getCall(0).args.length);
            assertEquals("StockChart", $chart.highcharts.getCall(0).args[0]);
            assertEquals(expectedArgs, $chart.highcharts.getCall(0).args[1]);
        });
    });
});