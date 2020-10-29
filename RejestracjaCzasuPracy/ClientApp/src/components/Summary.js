import React, { Component } from 'react'

const SummaryDataTable = props => {
    return (
        <table>
            <thead>
                <tr>
                    <th>Bilans</th>
                    <th>Name</th>
                </tr>
            </thead>
            <tbody>
                {props.summary} 
            </tbody>
        </table>
    )
}

export class Summary extends Component {
    constructor(props) {
        super(props)
        this.state = {
            summaryDataTable: '',
        }
    }

    getSummaryDataTable() {       
        fetch('/api/Event/GetSummaryForAllUsers')
            .then(res => res.json())
	 		.then(data =>
	 			this.setState({
                    summaryDataTable: data,
	 			})
            );
     }     
     
     componentDidMount() {
        this.getSummaryDataTable();
    }

    render() {
        const { summaryDataTable } = this.state;
        const summary = [];
        for (let i = 0; i < summaryDataTable.length; i++) {
            summary.push( 
                    <tr key={summaryDataTable[i].memberID}>
                        <td>{summaryDataTable[i].bilans}</td>
                        <td>{summaryDataTable[i].name}</td>
                    </tr>
            )
        }
        return (
            <div>
                <SummaryDataTable summary={summary}/>
                <button className='back-to-login' onClick={this.props.backToLogin}>Return to login</button>
            </div>
        )
    }
}