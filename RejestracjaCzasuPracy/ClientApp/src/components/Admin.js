import React, { Component } from 'react';

export default class Admin extends Component {
    constructor(props) {
        super(props);
    }
    render() {
        return (
			<div>
				<h1>Witaj {this.props.user}</h1>
                <button onClick={this.props.onLoggOut} >Log out</button>
            </div>
        )
    }
}
