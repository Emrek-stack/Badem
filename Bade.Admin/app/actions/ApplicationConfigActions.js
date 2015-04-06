var dispatcher = require('../dispatchers/Dispatcher');
var applicationConfigConstants = require('../constants/ApplicationConfigConstant');


var addKeyAction = function (payload) {
    dispatcher.dispatch(applicationConfigConstants.ADD_KEY, payload);
};

var listByApplicationIdAction = function (payload) {
    dispatcher.dispatch(applicationConfigConstants.LIST_BY_APPLICATIONID, payload);
};


var ApplicationConfigActions = {
    add: addKeyAction,
    listByApplicationId :listByApplicationIdAction
};


module.exports = ApplicationConfigActions;