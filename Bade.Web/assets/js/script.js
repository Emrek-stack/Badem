WallController = {
    init: function () {

        var wallResponseHubProxy = $.connection.wallResponseHub;
        wallResponseHubProxy.client.addContosoChatMessageToPage = function(name, message) {
            console.log(name + ' ' + message);
        };

        $.connection.hub.start()
            .done(function() { console.log('Now connected, connection ID=' + $.connection.hub.id); })
            .fail(function() { console.log('Could not Connect!'); });
    },
    bindResult: function(surveyId, answerIdToIncrement) {

    },
    showSurvey: function(surveyId) {

    },
}


//WallController.init();