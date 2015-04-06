/**
 * Created by FROST on 22.02.2015.
 */

var React  = require('react');

var MiniButton  = React.createClass( {

    getInitialState : function(){
        return {Loading: false};
    },

    render : function () {

        return (
            <div className="ui primary mini button">
                update
            </div>
        );
    }
});

module.exports = MiniButton;