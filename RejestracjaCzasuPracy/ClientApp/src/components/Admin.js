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
            inputValue: '',
            dataTable: '',
        }
    }

    handleInputChange = e => {
        this.setState({
            inputValue: e.target.value,
        })
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
            this.isOnBreak();
            setTimeout(() => {
                this.getMinutesToCatchUp();
            }, 50);
            // this.updateData();
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

    getUserDataTable() {
		fetch('/api/Event/GetUserEvents?memberID=' + this.state.currentId)
            .then(res => res.json())
			.then(data =>
				this.setState({
					dataTable: data
				})
            );
    }

    goHomeButton = () => {
        fetch('/api/Event/GoHome?memberID=' + this.state.currentId)
        // this.updateData();
    }
    
    addNewEvent = () => {
        fetch('/api/Event/AddNewEvent?memberID=' + this.state.currentId + '&minutes=' + this.state.inputValue)
        this.setState({
            inputValue: '',
        })
        this.updateDataAfter();
    }

    updateDataAfter = () => {
        setTimeout(() => {
            this.updateData();
        }, 50);
    }


    updateData() {       
        fetch('/api/Event/RefreshData?memberID=' + this.state.currentId)
              .then(res => res.json())
	 		.then(data =>
	 			this.setState({
                     dataTable: data.userEvents,
                     summaryUserMinutes: data.minutesToCatchUp,   
	 			})
             );
             console.log(this.dataTable, this.summaryUserMinutes)
     }       

    componentDidMount() {
        this.sendUserId();
        this.getMinutesToCatchUp();
        this.isWorking();
        this.isOnBreak();
        this.getUserDataTable();
    }
    
    handleDeleteTable = (id) => {
        // eslint-disable-next-line no-restricted-globals
        const alertConfirm = confirm('Are you sure you want to delete this record?')
        if (alertConfirm) {
            fetch('/api/Event/DeleteEvent?eventID=' + id + '&memberID=' + this.state.currentId)
			.then(result => {
				if (result.ok) {    
					this.getUserDataTable();
				} else {
					return result.text().then(message => {
						throw message;
					});
				}
			});
        } else return null
    }

    render() {
        const { dataTable } = this.state;
        const events = [];
        for (let i = 0; i < dataTable.length; i++) {
            const id = dataTable[i].eventID;
            events.push( 
                    <tr key={id}>
                        <td>{dataTable[i].date}</td>
                        <td>{dataTable[i].minutesToCatchUp}</td>
                        <td>{dataTable[i].breakTime}</td>
                        <td><button onClick={() => this.handleDeleteTable(id)}>Delete</button></td>
                    </tr>
            )
        }
        return (
			<div>
				<h1>Welcome to your profile {this.state.currentUser.name}</h1> 
                <h2>Your Minutes to catch up: {this.state.summaryUserMinutes} minutes</h2>
                <h2>Work hours: 9 - 17</h2>
                <input type="number" value={this.state.inputValue} onChange={this.handleInputChange}/><button onClick={this.addNewEvent}>Add new record</button>
                <button onClick={this.goHomeButton}>Go Home</button>
                <button onClick={this.workButtonClick}>{this.state.isWorking ? 'Finish your work' : 'Start Working'}</button>
                {this.state.isWorking ? <button onClick={this.breakButtonClick}>{this.state.isOnBreak ? 'Finish a break' : 'Take a break'}</button> : null}
                <button>Go to summary of all users</button>
                <table>
                    <thead>
                        <tr>
                            <th>Date</th>
                            <th>MinutesToCatchUp</th>
                            <th>BreakTime</th>
                        </tr>
                    </thead>
                    <tbody>
                        {events} 
                    </tbody>
                </table>
                <button onClick={this.props.onLoggOut}>Log out</button>
            </div>
        )
    }
}