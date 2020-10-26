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
	// onLoggIn = currentUser => {
	// 	this.setState({
	// 		isLogged: true,
	// 		content: <Admin user={currentUser} onLoggOut={this.onLoggOut} />
	// 	});
	// }
	onLoggOut = () => {
		this.setState({
			isLogged: false,
			content: ''
			
		});
	}

	// getCurrentUserToAdmin = currentUser => {
	// 	this.setState({
	// 		currentUser: currentUser,
	// 	})
	// }	

	render() {
		if (this.state.isLogged) {
			return (
				// this.state.content
				<Login onLoggIn={this.onLoggIn} />
			);
		} else {
			return (
				<Login onLoggIn={this.onLoggIn} getCurrentUserToAdmin={this.getCurrentUserToAdmin}/>
			);
		}
	}
}
