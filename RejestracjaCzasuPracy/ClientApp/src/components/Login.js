import React, { Component } from 'react';

const DpkTitle = () => {
    return (
        <div className="title-section">
            <p className="line anim-typewriter">Welcome to DPK System</p>
        </div>
    )
}

const TitleLoginText = () => {
    return (
        <React.Fragment>
            <h2>Sign In</h2>
            <h3>Choose User</h3>
        </React.Fragment>
    )
}

const LoginForm = props => {
    const { formSubmit, userName, userChange, items, getId, isSelected } = props;
    return (
        <form onSubmit={formSubmit}>
            <select value={userName} onChange={userChange}>
                    <option value=""></option>
                    {items}
            </select>    
            <br/>
            <button type="submit" onClick={getId}>Log in</button> 
            {isSelected ? null : <ValidationMessage text="User is not selected"/> }
        </form>
    )
}

const ValidationMessage = props => {
    return <p>{props.text}</p>
}

export class Login extends Component {
	constructor(props) {
        super(props);
        this.state = {
            userName: '',
            isSelected: true,
            users: '',
            choosenId: '',
        } 
    }

    handleFormSubmit = (e) => {
        e.preventDefault();
		if (this.state.userName) {
            this.props.onLoggIn(this.state.choosenId); 
        } else return this.setState({isSelected: false})  
    }

    handleGetId = () => {
        this.state.users.filter(user => {
            if (user.name === this.state.userName) {
                return (
                    this.setState({
                        choosenId: user.id,
                    })
                )
            } else return null
        })  
    }            

    handleUserChange = (e) => {
        this.setState({
            userName: e.target.value,
        })
    }

    updateUserSelect() {
		fetch('/api/User/GetAllUsers')
			.then(res => res.json())
			.then(data =>
				this.setState({
					users: data
				})
            );
    }
    
    componentDidMount() {
        this.updateUserSelect();
	}
    
    render() {
        const { users, userName, isSelected } = this.state;
        const items = [];
        for (let i = 0; i < users.length; ++i) {
            items.push(<option key={users[i].id} value={users[i].name}>{users[i].name}</option>)
        }
        return (
            <section className='login'>
                <DpkTitle/>
                <div className='login-section'>
                    <TitleLoginText />
                    <LoginForm 
                        formSubmit={this.handleFormSubmit}
                        userName={userName}
                        userChange={this.handleUserChange}
                        items={items}
                        getId={this.handleGetId}
                        isSelected={isSelected}
                    />
                    <button className='go-to-summary' onClick={this.props.goToSummary}>Go to summary of all users</button>
                </div>
            </section>
        )
    }
}

