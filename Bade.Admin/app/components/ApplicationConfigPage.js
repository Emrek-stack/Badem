/** @jsx React.DOM */


var React = require('react');
var ApplicationConfigStore = require('../store/ApplicationConfigStore');
var ApplicationConfigActions = require('../actions/ApplicationConfigActions');

var MiniButton = require('./MiniButton');
var RequiredTextbox = require('./Requiredtextbox');
var LoadingIndicator = require('./LoadingIndicator.react');


var ApplicationConfigPage = React.createClass({
    getInitialState: function () {
        return {ApplicationConfigList: null, ErrorMessage: null, Loading: true};
    },
    componentWillMount: function () {
        ApplicationConfigActions.listByApplicationId(3);
    },
    componentDidMount: function () {
        ApplicationConfigStore.addSuccessListner(this.onSuccess);
        ApplicationConfigStore.addFailureListner(this.onSuccess);
    },
    componentWillUnmount: function () {
        ApplicationConfigStore.removeFailureListner(this.onSuccess);
        ApplicationConfigStore.removeSuccessListner(this.onSuccess);
    },
    onSuccess: function () {
        var resourceState = ApplicationConfigStore.getApplicationConfigListState();
        this.setState({ApplicationConfigList: resourceState.ApplicationConfigList, Loading: false});
    },
    onFailure: function () {
        var errorState = ApplicationConfigStore.getErrorState();
        this.setState({Loading: false, ErrorMessage: erroState.Message, Loading: false});
    },
    //render: function () {
    //    return  (<div className="ui active loader">
    //        <div className="ui mini text loader">Loading..</div>
    //    </div>);
    //}
    render: function () {

        var error = function (component) {
            if (component.state.ErrorMessage) {
                return( <div className="error-message">{component.state.ErrorMessage}</div>);
            }
            return;
        }(this);


        var configTable = function (component) {


            if (component.state.ApplicationConfigList) {

                var configItems = function () {


                    if (!component.state.ApplicationConfigList.ApplicationConfigList.length) {
                        return <tr className="danger">
                            <td colSpan="3"> There are no resources</td>
                        </tr>;
                    }


                    return component.state.ApplicationConfigList.ApplicationConfigList.map(function (r) {
                        return <tr>
                            <td>{r.Key}</td>
                            <td>
                                <RequiredTextbox id="value" placeholder="value" ref="value" value={r.Value} />
                       </td>
                            <td>
                                <MiniButton/>
                            </td>
                        </tr>;
                    });
                }();

                return <table className="ui small table segment">
                    <thead>
                        <tr>
                            <th>
                                Name
                            </th>
                            <th>
                                Value
                            </th>
                            <th>
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        {configItems}
                    </tbody>
                </table>;

            }
            return;

        }(this);

        return (<div>
            <h5 className="ui header">Home</h5>
            <div className="ui divider"></div>
        {error} {configTable}</div>);
    }
});

module.exports = ApplicationConfigPage;

