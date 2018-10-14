import React, { Component } from 'react';
import logo from './logo.svg';
import styles from'./App.css';

//const SERVICE_ADDRESS = 'http://localhost:9667/stats?startDate=2018.08.24&endDate=2019.01.01'
const SERVICE_ADDRESS = 'http://localhost:9667/stats'
const START_DATE_PARAM ='?startDate=';
const END_DATE_PARAM='&endDate='

class DateTimeTools{
	
	constructor(){
		var today = new Date();
		var dd = today.getDate();
		var mm = today.getMonth()+1; //January is 0!
		var yyyy = today.getFullYear();
		
		var today = yyyy +'-'+mm+'-'+dd;
		this.Today= today;
	}
	GetDate(){
		return this.Today;
	}
	
	AddDays(count){
		var parts = this.Today.split('-');
		var dd=parts[2];
		var mm=parts[1];
		var yyyy=parts[0];
		
		dd=parseInt(dd)+parseInt(count);
		var today = yyyy +'-'+mm+'-'+dd;
		this.Today= today;
		return this;
  // new Date(year, month [, day [, hours[, minutes[, seconds[, ms]]]]])
		//return new Date(parts[0], parts[1]-1, parts[2]); // Note: months are 0-based
	}
	
	AddLeadingZero(number){
		if (number<10){
			return "0"+number;
		}
		else
		{
			return number;
		}
	}
	
	FormatDate(date){
		var d = new Date(date)
		var year = d.getFullYear();
		var month = this.AddLeadingZero(d.getMonth()+1);
		
		var day = this.AddLeadingZero(d.getDate());
		var result=year + '.'+ month +'.'+day;
		return result;
	}
}

class Button extends Component{
	render(){
		const{onclick,children}=this.props;
		return(
			<button type="button" className="Button"  onClick={onclick} value="Get">{children}</button>
		)
	}
}


class DateContainer extends Component{
	render(){
		const {value,onChange,children}=this.props
		console.log("onchange")
		console.log(onChange)
		return(
			<div className="DateContainer">
				<label className="Label">{children}</label>
				<input className="DateInput" type="text" value={value} onChange={onChange}></input>
			</div>
		)
	}
}


class OutlookStatsTableRow extends Component {
	constructor(props){
		super(props);

	}

	formatDate(datestring){
		const now=new DateTimeTools().FormatDate(datestring);
		return now;
	}

	render() {
		const { row } = this.props
		return (
			<tr>
				<td>{this.formatDate(row.Date)}</td>
				<td>{row.MailCountAdd}</td>
				<td>{row.MailCountSent}</td>
				<td>{row.MailCountProcessed}</td>
				<td>{row.TaskCountAdded}</td>
				<td>{row.TaskCountFinished}</td>
				<td>{row.TaskCountRemoved}</td>
			</tr>
		)
	}
}

class OutlookStatsTable extends Component {
	render() {
		const { outlookStatList } = this.props
		console.log(this.props)
		return (
			<table className="blueTable">
				<thead>
					<tr>
						<th>Date</th>
						<th>Maill add</th>
						<th>Mail sent</th>
						<th>Mail processed</th>
						<th>Task added</th>
						<th>Task finished</th>
						<th>Task removed</th>
					</tr>
				</thead>
				<tbody>
					{outlookStatList.map(function (item) {
						return <OutlookStatsTableRow key={item.Date} row={item} />
					})}
				</tbody>
			</table>)
	}
}

class App extends Component {

	constructor(props) {
		super(props);
		this.state = {
			outlookStatList: null,
		};
		this.fetchCalculateStats=this.fetchCalculateStats.bind(this)
		this.onChange=this.onChange.bind(this)
	}

	setCalculateEmailsStats(calculateEmailsStats) {
		console.log("setCalculateEmailsStats")
		console.log(calculateEmailsStats);
		this.setState({ outlookStatList: calculateEmailsStats });
	}
	
	fetchCalculateStats(startDate,endDate){
		const addres=`${SERVICE_ADDRESS}${START_DATE_PARAM}${startDate}${END_DATE_PARAM}${endDate}`
		fetch(`${addres}`)
			.then(response => response.json())
			.then(result => this.setCalculateEmailsStats(result))
			.catch(error => console.log(error));
	}

	componentDidMount() {
		console.log('compoenent mount');
		const now=new DateTimeTools().GetDate();
		console.log(now);
		const weekbefore=new DateTimeTools().AddDays(-7).GetDate();
		console.log(weekbefore);
		this.fetchCalculateStats(weekbefore,now);
		this.setState({startDate:weekbefore,endDate:now})
		//fetch(`${SERVICE_ADDRESS}`)
		//.then(response => response.json())
			//.then(result => this.setCalculateEmailsStats(result))
		//	.catch(error => console.log(error));

		console.log(this.state)
	}
	
	onChange(target,event){
		console.log("eventXXXX");
		console.log(target);
		this.setState({ [target]: event.target.value })
	}

	render() {
		const { outlookStatList,startDate,endDate } = this.state
		console.log("StartDate");
		console.log(startDate);
		return (
			<div className="App">
				<div className="Title">ProductivityTools - Calculate Emails</div>
				<div className="Content">
					<DateContainer value={startDate} onChange={(e)=>this.onChange('startDate',e)}>From</DateContainer>
					<DateContainer value={endDate} onChange={(e)=>this.onChange('endDate',e)}>To</DateContainer>
					<Button onclick={()=>this.fetchCalculateStats(startDate,endDate)}>Get</Button>
					<hr/>
					{outlookStatList? <OutlookStatsTable outlookStatList={outlookStatList} />:null	}
					
				</div>
			</div>
		);
	}
}

export default App;
