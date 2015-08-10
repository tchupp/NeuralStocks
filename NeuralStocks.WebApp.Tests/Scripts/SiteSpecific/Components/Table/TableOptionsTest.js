/// <reference path="../../../../../neuralstocks.webapp/scripts/sitespecific/components/table/tableoptions.js" />
/// <reference path="../../../TestHelper/TestHelper.js" />

describe("TableOptionsTest", function() {

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

    describe("TableOptions", function() {
        it("testAnalysisSearchTable", function() {
            var expected = {
                info: false,
                columns: [
                    { "title": "Name", "class": "col-sm-7" },
                    { "title": "Symbol", "class": "col-sm-3" },
                    { "title": "Show", "class": "col-sm-2 datatable-nosort" }
                ],
                columnDefs: [
                    {
                        targets: "datatable-nosort",
                        orderable: false
                    }
                ]
            };

            assertEquals(expected, TableOptions.analysisSearchTableOptions);
        });
    });
});