import React, { Component } from 'react'
import { Redirect } from 'react-router-dom'

export class Login extends Component {
	constructor(props) {
		super(props);
		this.handleFormSubmit = this.handleFormSubmit.bind(this);
	}

    handleFormSubmit = (e) => {
		e.preventDefault();
		this.props.onLoggIn("Jan Puto");
    }

    render() {
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
