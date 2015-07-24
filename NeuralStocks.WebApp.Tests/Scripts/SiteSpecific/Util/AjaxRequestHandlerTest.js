/// <reference path="../../TestHelper/TestHelper.js" />
/// <reference path="../../../../neuralstocks.webapp/scripts/sitespecific/util/ajaxrequesthandler.js" />

describe("AjaxRequestHandlerTest", function() {
    describe("RequestGet", function() {
        var httpRequestStub;
        var openStub;
        var sendStub;

        beforeEach(function() {
            httpRequestStub = sinon.stub();
            openStub = sinon.stub();
            sendStub = sinon.stub();

            window.XMLHttpRequest = (function() {
                httpRequestStub.prototype.open = openStub;
                httpRequestStub.prototype.send = sendStub;
                return httpRequestStub;
            })();
        });

        it("testExpectedArgsArePassedToOpen", function() {
            var expectedUrl = "/GetMe/Stocks";

            assertEquals(0, openStub.callCount);

            AjaxRequestHandler.requestGet(expectedUrl, {});

            assertEquals(1, openStub.callCount);
            assertEquals(3, openStub.getCall(0).args.length);

            assertEquals("GET", openStub.getCall(0).args[0]);
            assertEquals(expectedUrl, openStub.getCall(0).args[1]);
            assertEquals(true, openStub.getCall(0).args[2]);
        });

        it("testSendIsCalled", function() {
            assertEquals(0, sendStub.callCount);

            AjaxRequestHandler.requestGet("/GetMe/Stocks", {});

            assertEquals(1, sendStub.callCount);
            assertEquals(0, sendStub.getCall(0).args.length);
        });
    });

    describe("RequestGetCallback", function () {
        var mockCallback;

        beforeEach(function() {
            var requestList = this.requests = [];
            this.httpRequestStub = sinon.useFakeXMLHttpRequest();
            mockCallback = sinon.spy();

            this.httpRequestStub.onCreate = function (req) {
                requestList.push(req);
            };
        });

        afterEach(function() {
            this.httpRequestStub.restore();
        });

        it("testStatusAndResponseTextArePassedToCallback", function() {
            var expectedStatus = 1234;
            var expectedText = "This is a response";

            assertEquals(0, this.requests.length);
            assertEquals(0, mockCallback.callCount);
            AjaxRequestHandler.requestGet("/GetMe/Stocks", mockCallback);

            assertEquals(1, this.requests.length);

            this.requests[0].respond(expectedStatus,
                { "Content-Type": "application/json" },
                expectedText);

            assertEquals(1, mockCallback.callCount);
            assertEquals(2, mockCallback.getCall(0).args.length);

            assertEquals(expectedStatus, mockCallback.getCall(0).args[0]);
            assertEquals(expectedText, mockCallback.getCall(0).args[1]);
        });
    });
});