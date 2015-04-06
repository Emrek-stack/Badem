/**@jsx React.DOM */

var React = require('react');
var Addons = require('react-addons');

var RequiredTextbox = React.createClass({

    getInitialState: function(){
        return {value : this.props && this.props.value ,valid: this.props && this.props.value && this.props.value.length};
    },
    getDefaultProps:function(){
        return {type:"text"};
    },
    onChange:function(e){
        this.setState({value:e.target.value, valid:e.target.value.length});
    },
    render: function () {

        var controlClass = Addons.classSet({
            'form-control':!this.state.valid,
            'form-control':this.state.valid
        });

        return this.transferPropsTo(<input onChange={this.onChange} value={this.state.value} className={controlClass}  />);
    }

});


module.exports = RequiredTextbox;

