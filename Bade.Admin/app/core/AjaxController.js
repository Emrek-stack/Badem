var $ = require('jquery');

AjaxController = {
    init: function (url, requestType, paramArray, successCallback, failureCallback) {
        var paramList = '';
        switch (requestType) {
            case RequestType.POST:
                {
                    if (paramArray.length > 0) {
                        for (var i = 0; i < paramArray.length; i++) {
                            if (paramList.length > 0) paramList += ",";
                            paramList += paramArray[i].name + ':"' + paramArray[i].value + '"';
                        }
                        paramList = '{' + paramList + '}';
                    }
                    break;
                }
            case RequestType.GET:
                {
                    if (paramArray != null && paramArray.length > 0) {
                        for (var j = 0; j < paramArray.length; j += 2) {
                            if (paramList.length > 0) paramList += "&";
                            paramList += paramArray[j] + '=' + paramArray[j + 1];
                        }
                    }
                    break;
                }
        }
        this.process(url, requestType, paramList, successCallback, failureCallback);
    },
    process: function (url, requestType, paramList, successCallback, failureCallback) {
        $.ajax({
            type: requestType,
            contentType: 'application/json; charset=utf-8',
            url: url,
            data: paramList,
            dataType: 'json',
            crossDomain: true,
            success: function (data, status, xhr) {
                if (successCallback) {
                    successCallback(data, status, xhr);
                }
            },
            error: function (xhr, status, error) {
                if (failureCallback) {
                    failureCallback(xhr, status, error);
                }
            }
        });
    }
};

module.exports = AjaxController;
