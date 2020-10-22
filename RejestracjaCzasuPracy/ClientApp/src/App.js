import React, { Component } from 'react';
// import { Route } from 'react-router';
import { Layout } from './components/Layout';
// import { Home } from './components/Home';
// import { FetchData } from './components/FetchData';
// import { Counter } from './components/Counter';
import { Login } from './components/Login';
import Admin from './components/Admin';
import Logout from './components/Logout';
import { Link, Switch, Route} from 'react-router-dom';


export default class App extends Component {
  displayName = App.name

  render() {
    return (
      <Switch>
        {/* <Route exact path='/' component={Home} />
        <Route path='/counter' component={Counter} />
        <Route path='/fetchdata' component={FetchData} /> */}
        <Route path='/login' component={Login} />
        <Route path='/admin' component={Admin} />
        <Route path='/logout' component={Logout} />
        {/* <Admin/>
        <Logout/> */}
      </Switch>
    );
  }
}
