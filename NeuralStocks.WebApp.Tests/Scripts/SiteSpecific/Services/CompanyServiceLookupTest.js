/// <reference path="../../TestHelper/TestHelper.js" />

describe("CompanyServiceLookupTest", function () {

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

    describe("GetCompanyLookup", function() {
        it("test", function() {
            
        });
    });
});