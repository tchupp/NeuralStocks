var ChartOptions = (function() {
    var modualChartOptions = {};

    modualChartOptions.analysisSearchChartOptions = {
        rangeSelector: {
            selected: 0
        },
        title: {
            text: "Stock Price:"
        },
        series: [
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
        ]
    };

    return modualChartOptions;
})();