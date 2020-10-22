import React, { Component } from 'react'
import { Redirect } from 'react-router-dom'

export class Login extends Component {
    state = {
        loggedIn: false
    }

    handleFormSubmit = (e) => {
        e.preventDefault();
        if(!this.state.loggedIn) {
            this.setState({
                loggedIn: true,
            })
        }
    }

    render() {
        const { loggedIn } = this.state;
        if(loggedIn) {
            return <Redirect to ="/admin"/>
        }
        return (
            <div>
                <h1>Sign In</h1>
                <h2>Choose User</h2>
                <form onSubmit={this.handleFormSubmit}>
                    <select id="myList">
                        <option value="1">Jan Puto</option>
                        <option value="2">Huan Putong</option>
                        <option value="3">Johny Pute</option>
                        <option value="4">Jenek Putea</option>
                    </select>           
                    <br/>
                    <button type="submit">Log in</button> 
                </form>
            </div>
        )
    }
}
