var EventEmitter = require('events').EventEmitter;
var $ = require('jquery');
var ajaxController = require('../core/AjaxController');
var requestType = require('../constants/RequestTypeConstants');
var AppConstants = require('../constants/AppConstants');
var dispatcher = require('../dispatchers/Dispatcher');
var config = require('../core/AppConfig');

var ApplicationStore = function () {
    this.AccessToken = ""; //to be set from Account Login
};

ApplicationStore.prototype.addSuccessListner = function (callback) {
    this.addListener(AppConstants.STORE_CHANGE, callback);
};

ApplicationStore.prototype.removeSuccessListner = function (callback) {
    this.removeListener(AppConstants.STORE_CHANGE, callback);
};

ApplicationStore.prototype.addChangeListner = function (callback) {
    this.addListener(AppConstants.STORE_CHANGE, callback);
};

ApplicationStore.prototype.removeChangeListner = function (callback) {
    this.removeListener(AppConstants.STORE_CHANGE, callback);
};

ApplicationStore.prototype.addFailureListner = function (callback) {
    this.addListener(AppConstants.STORE_ERROR, callback);
};

ApplicationStore.prototype.removeFailureListner = function (callback) {
    this.removeListener(AppConstants.STORE_ERROR, callback);
};

ApplicationStore.prototype.postJson = function (url, paramArray, successCallback, failureCallback) {
    ajaxController.init(url, requestType.POST, paramArray, successCallback,failureCallback);
};

ApplicationStore.prototype.getJson = function (url, paramArray, successCallback, failureCallback) {
    ajaxController.init(url, requestType.GET, paramArray, successCallback,failureCallback);
};




$.extend(ApplicationStore.prototype, EventEmitter.prototype);

module.exports = ApplicationStore;