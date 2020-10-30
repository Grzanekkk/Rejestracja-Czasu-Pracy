import React, { Component } from 'react';

const Informations = props => {
    return (
        <div className='informations'>
            <h1 className='welcome'>Welcome to your profile <span>{props.userName}</span> </h1> 
            <h2 className='minutes'>Bilans: <span>{props.summaryUserHours}</span> hours <span>{props.summaryUserHours != 0 ? Math.abs(props.summaryUserMinutes) : props.summaryUserMinutes}</span> minutes</h2>
            <h2 className='work-hours'>Work hours: <span>9 - 17</span></h2>
        </div>
    )
}

const Buttons = props => {
    const { inputValue, inputChange, addNewEvent, goHome, startWork, isWorking, breakClick, isOnBreak } = props;
    return (
        <React.Fragment>
            <div className='record-wrap'>
                <input type="number" value={inputValue} onChange={inputChange}/><button className='new-record samebtn' onClick={addNewEvent}>Add new record</button>
            </div>
            <button className='go-home samebtn' onClick={goHome}>Go Home</button>
            <button className='start-finish-work samebtn' onClick={startWork}>{isWorking ? 'Finish your work' : 'Start Working'}</button>
            {isWorking ? <button className='take-finish-break samebtn' onClick={breakClick}>{isOnBreak ? 'Finish a break' : 'Take a break'}</button> : null}
        </React.Fragment>
    )
}

const DataTable = props => {
    return (
            <table className='admin-table'>
                <thead>
                    <tr>
                        <th>Date</th>
                        <th>Balance</th>
                        <th>BreakTime</th>
                        <th>Delete</th>
                    </tr>
                </thead>
                <tbody>
                    {props.events} 
                </tbody>
            </table>
    )
}

export class Admin extends Component {
    constructor(props) {
        super(props);
        this.state = {
            currentId: this.props.id,
            currentUser: '',
            summaryUserMinutes: '',
            summaryUserHours: '',
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
         this.updateDataAfter();
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

    goHomeButton = () => {
        // eslint-disable-next-line no-restricted-globals
        const alertConfirm = confirm('Are you sure you want to go home?')
        if (alertConfirm) {
            fetch('/api/Event/GoHome?memberID=' + this.state.currentId)
            this.updateDataAfter();
        } else return null;
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
                        summaryUserHours: Math.trunc(data.balance/60),
                        summaryUserMinutes: data.balance%60,
                    })
                )
     }       
    
    handleDeleteTable = (id) => {
        // eslint-disable-next-line no-restricted-globals
        const alertConfirm = confirm('Are you sure you want to delete this record?')
        if (alertConfirm) {
            fetch('/api/Event/DeleteEvent?eventID=' + id + '&memberID=' + this.state.currentId)
			.then(result => {
				if (result.ok) {    
					this.updateDataAfter();
				} else {
					return result.text().then(message => {
						throw message;
					});
				}
			});
        } else return null
    }

    componentDidMount() {
        this.sendUserId();
        this.isWorking();
        this.isOnBreak();
        this.updateDataAfter();
    }

    render() {
        const { dataTable, currentUser , summaryUserMinutes, summaryUserHours ,inputValue, isWorking, isOnBreak } = this.state;
        const events = [];
        for (let i = 0; i < dataTable.length; i++) {
            const id = dataTable[i].eventID;
            events.push( 
                    <tr key={id}>
                        <td>{dataTable[i].date}</td>
                        <td>{dataTable[i].balance}</td>
                        <td>{dataTable[i].breakTime}</td>
                        <td><button className='delete' onClick={() => this.handleDeleteTable(id)}>X</button></td>
                    </tr>
            )
        }

        return (
                <div className='admin'>
                    <Informations userName={currentUser.name} summaryUserMinutes={summaryUserMinutes} summaryUserHours={summaryUserHours} />
                    <Buttons 
                        inputValue={inputValue}
                        inputChange={this.handleInputChange}
                        addNewEvent={this.addNewEvent}
                        goHome={this.goHomeButton}
                        startWork={this.workButtonClick}
                        isWorking={isWorking}
                        breakClick={this.breakButtonClick}
                        isOnBreak={isOnBreak}
                    />
                    <DataTable events={events} />
                    <button className='logout' onClick={this.props.onLoggOut}>Log out</button>
                </div>
        )
    }
}