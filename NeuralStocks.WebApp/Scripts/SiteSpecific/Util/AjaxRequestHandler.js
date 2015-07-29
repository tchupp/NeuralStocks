var AjaxRequestHandler = (function() {
    var classAjaxRequestHandler = {};

    classAjaxRequestHandler.requestGet = function(url, callback) {
        var request = new XMLHttpRequest();

        request.open("GET", url, true);
        request.onload = function () {
            callback(request.status, request.responseText);
        };

        request.send();
    };

    return classAjaxRequestHandler;
})();
