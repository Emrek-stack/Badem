var ApplicationStore = require('./ApplicationStore');
var Dispatcher = require('../dispatchers/Dispatcher');
var AppConstants = require('../constants/AppConstants');
var ApplicationConfigConstant = require('../constants/ApplicationConfigConstant');
var config = require('../core/AppConfig');

var resourceUrl = config.apiUrl + 'ApplicationConfig/';

function addApplicationConfig(applicationConfig) {

    var successCallback = function (data, statusText, xhr) {
        this.ApplicationConfigList.push(data);
        this.Error = null;
        this.Status = AppConstants.CREATED;
        this.emit(AppConstants.STORE_CHANGE);
    }.bind(ApplicationConfigStore);

    var failureCallback = function (xhr) {
        this.Error = { Message: "An error occured while saving the location. Please try after some time", StatusCode: xhr.status };
        this.ApplicationConfigList = null;
        this.ApplicationConfig = null;
        this.emit(AppConstants.STORE_ERROR);
    }.bind(ApplicationConfigStore);

    ApplicationConfigStore.postJson(resourceUrl + "/GetListByApplicationId", successCallback, failureCallback);
}

var getApplicationConfigListByApplicationId = function (applicationId) {

    var url = resourceUrl + "GetListByApplicationId?id=" + 3;
    var successCallback = function (data, statusText, xhr) {
        this.ApplicationConfigList = data;
        this.Error = null;
        this.emit(AppConstants.STORE_CHANGE);
    }.bind(ApplicationConfigStore);

    var failureCallback = function (xhr) {
        this.ApplicationConfigList = null;
        this.Error = { Message: "An error occured while retrieving the resources. Please try after some time", StatusCode: xhr.status };
        this.emit(AppConstants.STORE_ERROR);
    }.bind(ApplicationConfigStore);


    ApplicationConfigStore.getJson(url, [], successCallback, failureCallback);
};

var ApplicationConfigStore = function () {
    var store = new ApplicationStore();

    store.getApplicationConfigListState = function () {
        return {ApplicationConfigList: ApplicationConfigStore.ApplicationConfigList};
    };
    store.getApplicationConfigState = function () {
        return {ApplicationConfig: ApplicationConfigStore.ApplicationConfig};
    };

    store.getErrorState = function () {
        return Error;
    };

    return store;
}();

Dispatcher.register(function (actionType, payload) {
    switch (actionType) {

        case ApplicationConfigConstant.ADD_KEY:
        {
            addApplicationConfig(payload);
            break;
        }
        case ApplicationConfigConstant.LIST_BY_APPLICATIONID:
        {
            getApplicationConfigListByApplicationId(payload);
            break;
        }
        default:
        {
            break;
        }
    }

});


module.exports = ApplicationConfigStore;

