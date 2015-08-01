/// <reference path="../../../../neuralstocks.webapp/scripts/sitespecific/util/tablesortingmanager.js" />
/// <reference path="../../TestHelper/TestHelper.js" />
describe("TableSortingManagerTest", function() {
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

    describe("InitializeTable", function() {
        var $table;
        beforeEach(function() {
            $table = { DataTable: sinon.stub() };
        });

        afterEach(function() {
        });

        it("testSetsUpDataTable_WithCorrectColumnsDisabled", function() {
            assertFalse($table.DataTable.called);

            var expectedArgs = { id: "testingArguments"};

            TableSortingManager.initializeTable($table, expectedArgs);

            assertTrue($table.DataTable.calledOnce);
            assertEquals(expectedArgs, $table.DataTable.getCall(0).args[0]);
        });
    });
});