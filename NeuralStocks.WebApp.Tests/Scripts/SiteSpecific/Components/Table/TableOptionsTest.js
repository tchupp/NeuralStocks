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
                    { "title": "Symbol", "class": "col-sm-3 stopselect", "data": "Symbol" },
                    { "title": "Name", "class": "col-sm-9 stopselect", "data": "Name" }
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