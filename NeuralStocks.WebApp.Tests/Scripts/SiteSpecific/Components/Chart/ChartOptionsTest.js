/// <reference path="../../../../../neuralstocks.webapp/scripts/sitespecific/components/chart/chartoptions.js" />
/// <reference path="../../../TestHelper/TestHelper.js" />

describe("ChartOptionsTest", function() {

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

    describe("ChartOptions", function() {
        it("testAnalysisSearchChart", function() {
            var expected = {};
            expected.rangeSelector = {
                selected: 0
            };
            expected.title = {
                text: "Stock Price:"
            };
            expected.series = [
                {
                    type: "candlestick",
                    name: "Stock Price:",
                    dataGrouping: {
                        units: [
                            [
                                "week", [1]
                            ], [
                                "month", [1, 2, 3, 4, 6]
                            ]
                        ]
                    }
                }
            ];

            assertEquals(expected, ChartOptions.analysisSearchChartOptions);
        });
    });
});