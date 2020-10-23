import React, { Component } from 'react';
// import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
// import { FetchData } from './components/FetchData';
// import { Counter } from './components/Counter';
import { Login } from './components/Login';
import Admin from './components/Admin';


export default class App extends Component {
<<<<<<< Updated upstream
  displayName = App.name
=======
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
>>>>>>> Stashed changes

  render() {
    return (
      <Switch>
        {/* <Route exact path='/' component={Home} />
        <Route path='/counter' component={Counter} />
        <Route path='/fetchdata' component={FetchData} /> */}
        <Route path='/login' component={Login} />
        <Route path='/admin' component={Admin} />
        <Route path='/logout' component={Logout} />
      </Switch>
      // <Layout>
      //   <Login/>
      // </Layout>
    );
  }
}
