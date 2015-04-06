var React = require('react');

var ApplicationConfigRow = React.createClass({
    handleDelete:function(){
        //var id= <this className="props config ApplicationId"></this>;
        //this.props.onDelete(id);
    },
    render: function() {

        var config = this.props.config;
        return
        (<tr>
        <td>{config.ApplicationId}</td>
        <td>{config.Key}</td>
    <td>{config.Value}</td>
        </tr>);
    }

});

module.exports = ApplicationConfigRow;