import React, { Component } from 'react';
import logo from './logo.svg';
import './App.css';

const SERVICE_ADDRESS='http://localhost:5667/ICalculateEmailsStatsService'

const outlookStatList = [
	{
		id:1,
		Day: '2018.08.22',
		MailCountAdd: 2,
		MailCountSent: 3,
		MailCountProcessed: 5
	},
	{
		id:2,
		Day: '2018.08.23',
		MailCountAdd: 12,
		MailCountSent: 23,
		MailCountProcessed: 25
	}
]

class OutlookStatsTableRow extends Component{
	render(){
		const {row}=this.props
		return (
			<tr>
				<td>{row.Day}</td>
				<td>{row.MailCountAdd}</td>
			</tr>
		)
	}
}

class OutlookStatsTable extends Component {
	render() {
		const { outlookStatList } = this.props
		console.log(this.props)
		return (
			<table>
				<thead>
					<tr>
						<th>Day</th>
						<th>Maill add</th>
					</tr>
					</thead>
					<tbody>
						{outlookStatList.map(function (item) {
							return <OutlookStatsTableRow key={item.Day} row={item}/>
						})}
					</tbody>
			</table>)
	}
}
		
class App extends Component {

		constructor(props) {
			super(props);
      this.state = {
						outlookStatList: outlookStatList,
			};
	}

	setCalculateEmailsStats(calculateEmailsStats){
		console.log(calculateEmailsStats);
		this.setState({calculateEmailsStats});
	}

	componentDidMount(){
		console.log('compoenent mount');
		fetch(`${SERVICE_ADDRESS}`)
		.then(response=>console.log(response))
		.then(result=>this.setCalculateEmailsStats(result))
		.catch(error=>console.log(error));

		console.log(this.state)
	}

    render() {
        return (
            <div className="App">
						<OutlookStatsTable outlookStatList={outlookStatList} />
					</div>
					);
			}
	}
	
	export default App;
