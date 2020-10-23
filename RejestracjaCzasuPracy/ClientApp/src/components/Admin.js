import React, { Component } from 'react';



export default class Admin extends Component {
    constructor(props) {
        super(props);
        this.state = {

        }
    }

    
    render() {
        return (
<<<<<<< Updated upstream
            <div>
                <h1>Admin page</h1>
                <Link to="/logout">Logout</Link>
=======
			<div>
				<h1>Witaj {this.props.user}</h1>
                <button onClick={this.props.onLoggOut} >Log out</button>
>>>>>>> Stashed changes
            </div>
        )
    }
}
