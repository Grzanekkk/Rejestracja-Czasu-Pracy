import React, { Component } from 'react'

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
        const { users } = this.state;
        const items = [];
        for (let i = 0; i < users.length; ++i) {
            items.push(<option key={users[i].id} value={users[i].name}>{users[i].name}</option>)
        }
        return (
            <section>
                <div className="title-section">
                    <p className="line anim-typewriter">Welcome to DPK System</p>
                </div>
                <div className='login-section'>
                    <h2>Sign In</h2>
                    <h3>Choose User</h3>
                    <form onSubmit={this.handleFormSubmit}>
                        <select value={this.state.userName} onChange={this.handleUserChange}>
                            <option value=""></option>
                            {items}
                        </select>    
                        <br/>
                        <button type="submit" onClick={this.handleGetId}>Log in</button> 
                        {this.state.isSelected ? null : <ValidationMessage text="User is not selected"/> }
                    </form>
                    <div className="logo"><img src="src/img/DPKLogo.png" alt="DPK Logo"/></div>
                </div>
            </section>
        )
    }
}
