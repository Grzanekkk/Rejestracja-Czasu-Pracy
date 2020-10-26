import React, { Component } from 'react';
import { Login } from './components/Login';
import { Admin } from './components/Admin';



export default class App extends Component {
	constructor(props) {
		super(props);
		this.state = {
			isLogged: false,
			content: '',
		}
	}
	onLoggIn = user => {
		this.setState({
			isLogged: true,
			content: <Admin user={user} onLoggOut={this.onLoggOut} />
		});
	}
	onLoggOut = () => {
		this.setState({
			isLogged: false,
			content: ''
			
		});
	}

	render() {
		if (this.state.isLogged) {
			return (
				this.state.content
				// <Login onLoggIn={this.onLoggIn} />
			);
		} else {
			return (
				<Login onLoggIn={this.onLoggIn} />
			);
		}
	}
}
