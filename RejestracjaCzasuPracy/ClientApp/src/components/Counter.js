import React, { Component } from 'react';

const App = (props) => {
  return (
    <div>
      <h1>U are logged in</h1>
      <button onClick={() => !props.submitted}>Log out</button>
    </div>
  )
}

export class Counter extends Component {
  displayName = Counter.name
  
  constructor(props) {
    super(props);
    this.state = { 
      isFormSubmitted: false,
      isCounterActive: false,
    };
  }

  handleFromSubmit = (e) => {
    e.preventDefault();
    if (!this.state.isFormSubmitted) {
      this.setState({
        isFormSubmitted: true,
        isCounterActive: true
      })
    }
  }

  displayCounter = () => {
    if (this.state.isFormSubmitted) {
      return <App/>
    } else {return null}
  }

  handleLogOutButton = () => {
    console.log('dziala')
    this.setState({
      isFormSubmitted: false
    })
  }

  render() {
    const { isFormSubmitted } = this.state
    return (
      <React.Fragment>
        <h1>Sign In</h1>
        <h2>Choose User</h2>
        <form onSubmit={this.handleFromSubmit}>
          <select id="myList">
            <option value="1">Jan Puto 1</option>
            <option value="2">Jan Puto 2</option>
            <option value="3">Jan Puto 3</option>
            <option value="4">Jan Puto 4</option>
          </select>
          <br/>
          <button type="submit">Log in</button> 
        </form>
        {/* {this.displayCounter()} */}
        {/* {this.state.isFormSubmitted && <App submitted={isFormSubmitted}/>} */}
        {/* {isFormSubmitted ? <App submitted={isFormSubmitted}/> : null} */}
        {isFormSubmitted ? 
        <React.Fragment>
            {console.log(this.state.isCounterActive)}
            <h1>Successfull</h1>
           <button onClick={this.handleLogOutButton}>Log Out</button> 
        </React.Fragment> : null}
      </React.Fragment>
    );
  }
}


// class App extends Component {
//   state = {

//   }

//   render() {
//     console.log()
//     return (
//       <div>
//         <h1>U are logged in</h1>
//         <button onClick={this.handleLogOutButton}>Log out</button>
//       </div>
//     )
//   }
// }
