import React, { Component } from 'react';
import { Link } from 'react-router-dom';


export default class Admin extends Component {
    render() {
        return (
            <div>
                <h1>Admin page</h1>
                <Link to="/logout">Logout</Link>
            </div>
        )
    }
}
