import React, { Component } from 'react'

const ValidationMessage = (props) => {
    return <p>{props.text}</p>
}

export class Login extends Component {
<<<<<<< Updated upstream
    state = {
        loggedIn: false
=======
	constructor(props) {
        super(props);
        this.state = {
            userName: '',
            isSelected: true,
        }
>>>>>>> Stashed changes
    }

    handleFormSubmit = (e) => {
        e.preventDefault();
<<<<<<< Updated upstream
        if(!this.state.loggedIn) {
            localStorage.setItem("token", "x")
            this.setState({
                loggedIn: true,
            })
        }
=======
        if (this.state.userName) this.props.onLoggIn(this.state.userName);
        else return this.setState({isSelected: false})
    }

    handleUserChange = (e) => {
        this.setState({
            userName: e.target.value,
        })
>>>>>>> Stashed changes
    }

    render() {
        const { loggedIn } = this.state;
        if(loggedIn) {
            return <Redirect to ="/admin"/>
            // return <Admin/>
        }
        return (
            <div>
                <h1>Sign In</h1>
                <h3>Choose User</h3>
                <form onSubmit={this.handleFormSubmit}>
                    <select value={this.state.userName} onChange={this.handleUserChange}>
                        <option value=""></option>
                        <option value="Jan Puto">Jan Puto</option>
                        <option value="Huan Putong">Huan Putong</option>
                        <option value="Johny Pute">Johny Pute</option>
                        <option value="Jenek Putea">Jenek Putea</option>
                    </select>           
                    <br/>
                    <button type="submit">Log in</button> 
                    {this.state.isSelected ? null : <ValidationMessage text="User is not selected"/> }
                </form>
            </div>
        )
    }
}
