/// <reference path="../../../../../neuralstocks.webapp/scripts/sitespecific/components/table/tablesetupmanager.js" />
/// <reference path="../../../TestHelper/TestHelper.js" />

describe("TableSetupManagerTest", function() {
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
        var $tableBody;
        var jQuerySelectorStub;
        beforeEach(function() {
            $table = { DataTable: sinon.stub(), find: sinon.stub() };
            $tableBody = { on: sinon.stub() };

            $table.find.returns($tableBody);
            jQuerySelectorStub = sinon.stub(window, "$");

        });

        afterEach(function () {
            jQuerySelectorStub.restore();
        });

        it("testSetsUpDataTable_WithCorrectTableOptions", function() {
            assertFalse($table.DataTable.called);

            var expectedArgs = { id: "testingArguments" };

            TableSetupManager.initializeTable($table, expectedArgs);

            assertTrue($table.DataTable.calledOnce);
            assertEquals(expectedArgs, $table.DataTable.getCall(0).args[0]);
        });

        it("testAddsOnClickToTableBody_WithCorrectArgs", function() {
            assertFalse($table.find.called);
            assertFalse($tableBody.on.called);

            TableSetupManager.initializeTable($table, {});

            assertEquals(1, $table.find.callCount);
            assertEquals(1, $table.find.getCall(0).args.length);
            assertEquals("tbody", $table.find.getCall(0).args[0]);

            assertEquals(1, $tableBody.on.callCount);
            assertEquals(3, $tableBody.on.getCall(0).args.length);
            assertEquals("click", $tableBody.on.getCall(0).args[0]);
            assertEquals("tr", $tableBody.on.getCall(0).args[1]);
        });

        it("testAddsOnClickToTableBody_ClickTogglesTrClass", function () {
            assertFalse($table.find.called);
            assertFalse($tableBody.on.called);

            TableSetupManager.initializeTable($table, {});

            assertEquals(1, $tableBody.on.callCount);
            assertEquals(3, $tableBody.on.getCall(0).args.length);

            var clickCallback = $tableBody.on.getCall(0).args[2];

            var mockTableRow = { clickCallback: clickCallback };
            var $mockTableRow = { toggleClass: sinon.stub() };
            jQuerySelectorStub.returns($mockTableRow);

            assertFalse(jQuerySelectorStub.called);
            assertFalse($mockTableRow.toggleClass.called);

            mockTableRow.clickCallback();

            assertEquals(1, jQuerySelectorStub.callCount);
            assertEquals(1, jQuerySelectorStub.getCall(0).args.length);
            assertEquals(mockTableRow, jQuerySelectorStub.getCall(0).args[0]);

            assertEquals(1, $mockTableRow.toggleClass.callCount);
            assertEquals(1, $mockTableRow.toggleClass.getCall(0).args.length);
            assertEquals("selected", $mockTableRow.toggleClass.getCall(0).args[0]);
        });
    });
});