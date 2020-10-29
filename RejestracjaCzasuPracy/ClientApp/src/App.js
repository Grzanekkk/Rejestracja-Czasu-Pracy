import React, { Component } from 'react';
import { Login } from './components/Login';
import { Admin } from './components/Admin';
import { Summary } from './components/Summary';
import './components/style.css';
export default class App extends Component {
	constructor(props) {
		super(props);
		this.state = {
			isLogged: false,
			content: '',
			inSummary: false,
			content2: '',
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

	goToSummary = () => {
		this.setState({
			inSummary: true,
			content2: <Summary backToLogin={this.fromSummaryToLogin} />
		})
	}

	fromSummaryToLogin = () => {
		this.setState({
			inSummary: false,
			content: '',
		})
	}
	
	render() {
		if (this.state.isLogged) return this.state.content
		else if (this.state.inSummary) return this.state.content2	
		else return <Login onLoggIn={this.onLoggIn} goToSummary={this.goToSummary} />
	}
}
