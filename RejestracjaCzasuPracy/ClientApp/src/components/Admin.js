import React, { Component } from 'react';

export class Admin extends Component {
    state = {

    }
    render() {
        // console.log(this.props.user)
        return (
			<div>
				<h1>Welcome to your profile {this.props.user}</h1> 
                <h2>Your Minutes to catch up</h2>
                <h2>...</h2>
                <h2>Work hours</h2>
                <p>9 - 17</p>
                <input type="number"/><button>Add new record</button>
                <button>Go Home</button>
                <button>Start Working</button>
                <button>Finish your break</button>
                <button>Go to summary of all users</button>
                <button>Update records</button>
                <button onClick={this.props.onLoggOut} >Log out</button>
            </div>
        )
    }
}
