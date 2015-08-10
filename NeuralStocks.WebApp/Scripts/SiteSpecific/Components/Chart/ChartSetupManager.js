var ChartSetupManager = (function() {
    var modualChartSetupManager = {};

    modualChartSetupManager.initializeStockChart = function($chart, chartOptions) {
        $chart.highcharts("StockChart", chartOptions);
    };

    return modualChartSetupManager;
})();