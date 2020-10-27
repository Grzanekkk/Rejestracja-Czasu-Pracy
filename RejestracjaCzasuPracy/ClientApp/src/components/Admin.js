import React, { Component } from 'react';

export class Admin extends Component {
    constructor(props) {
        super(props);
        this.state = {
            currentId: this.props.id,
            currentUser: '',
            summaryUserMinutes: '',
            isWorking: '',
            isOnBreak: '',
        }
    }

    sendUserId() {
		fetch('/api/User/GetUser?memberID=' + this.state.currentId)
            .then(res => res.json())
			.then(data =>
				this.setState({
					currentUser: data
				})
            );

    }

    getMinutesToCatchUp() {
		fetch('/api/Event/GetUserTimeToCatchUp?memberID=' + this.state.currentId)
            .then(res => res.json())
			.then(data =>
				this.setState({
					summaryUserMinutes: data
				})
            );
    }
    
    isWorking() {
		fetch('/api/Event/IsWorking?memberID=' + this.state.currentId)
            .then(res => res.json())
			.then(data =>
				this.setState({
					isWorking: data
				})
            );
    }

    workButtonClick = () => {
        fetch('/api/Event/WorkButton?memberID=' + this.state.currentId)
            .then(res => res.json())
            .then(data =>
				this.setState({
					isWorking: data
				})
            );
            
            this.getMinutesToCatchUp();
            this.isOnBreak();
    }

    isOnBreak() {
		fetch('/api/Event/IsOnBreak?memberID=' + this.state.currentId)
            .then(res => res.json())
			.then(data =>
				this.setState({
					isOnBreak: data
				})
            );
    }

    breakButtonClick = () => {
        fetch('/api/Event/BreakButton?memberID=' + this.state.currentId)
            .then(res => res.json())
            .then(data =>
				this.setState({
					isOnBreak: data
				})
            );
            
    }

    componentDidMount() {
        this.sendUserId();
        this.getMinutesToCatchUp();
        this.isWorking();
        this.isOnBreak();
	}

    render() {
        return (
			<div>
				<h1>Welcome to your profile {this.state.currentUser.name}</h1> 
                <h2>Your Minutes to catch up</h2>
                <h2>{this.state.summaryUserMinutes} minutes</h2>
                <h2>Work hours</h2>
                <p>9 - 17</p>
                <input type="number"/><button>Add new record</button>
                <button>Go Home</button>
                <button onClick={this.workButtonClick}>{this.state.isWorking ? 'Finish your work' : 'Start Working'}</button>
                {this.state.isWorking ? <button onClick={this.breakButtonClick}>{this.state.isOnBreak ? 'Finish a break' : 'Take a break'}</button> : null}
                <button>Go to summary of all users</button>
                <button>Update records</button>
                <button onClick={this.props.onLoggOut} >Log out</button>
            </div>
        )
    }
}
