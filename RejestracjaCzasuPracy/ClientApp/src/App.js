import React, { Component } from 'react';
import { Login } from './components/Login';
import { Admin } from './components/Admin';
import './components/style.css';

export default class App extends Component {
	constructor(props) {
		super(props);
		this.state = {
			isLogged: false,
			content: '',
		}
	}

	onLoggIn = currentId => {
		this.setState({
			isLogged: true,
			content: <Admin id={currentId} onLoggOut={this.onLoggOut} />
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
			);
		} else {
			return (
				<Login onLoggIn={this.onLoggIn} />
			);
		}
	}
}
