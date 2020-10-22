import React, { Component } from 'react';
// import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
// import { FetchData } from './components/FetchData';
// import { Counter } from './components/Counter';
import { Login } from './components/Login';
import Admin from './components/Admin';
import Logout from './components/Logout';
import { Link, Switch, Route} from 'react-router-dom';


export default class App extends Component {
	constructor(props) {
		super(props);
		this.state = {
			isLogged: false,
			content: ''
		}
		this.onLoggIn = this.onLoggIn.bind(this);
	}
	onLoggIn(token) {
		this.setState({
			isLogged: true,
			content: <Admin token={token} />
		});
	}

	render() {
		if (this.state.isLogged) {
			return (
				this.state.content
			);
		} else {
			return (
				<Login onLoggIn={this.onLoggIn}/>
			);
		}
	}
}
