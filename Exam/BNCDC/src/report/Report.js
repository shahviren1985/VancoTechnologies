import React, { Fragment } from 'react';
import axios from 'axios';
import {getTime} from '../util';
import Loadable from 'react-loadable';

const Report2 = Loadable({
    loader: () => import( './report2/Report2'),
    loading: () => <div>Loading ...</div>
});

const Report1 =  Loadable({
    loader: () => import( './report1/Report1'),
    loading: () => <div>Loading ...</div>
});

const Report3 = Loadable({
    loader: () => import( './report3/Report3'),
    loading: () => <div>Loading ...</div>
})

const Report4 = Loadable({
    loader: () => import( './report4/Report4'),
    loading: () => <div>Loading ...</div>
})


const Report5 = Loadable({
    loader: () => import( './report5/Report5'),
    loading: () => <div>Loading ...</div>
})

const Report6 = Loadable({
    loader: () => import( './report6/Report6'),
    loading: () => <div>Loading ...</div>
})

const Report7 = Loadable({
    loader: () => import( './report7/Report7'),
    loading: () => <div>Loading ...</div>
})

const Report8 = Loadable({
    loader: () => import( './report8/Report8'),
    loading: () => <div>Loading ...</div>
})

const Report9 = Loadable({
    loader: () => import( './report9/Report9'),
    loading: () => <div>Loading ...</div>
})

class Report extends React.Component {
    constructor(props)
    {
        super(props);
        this.state = {date: '', examName:'', examYear: '', rows: []};
    }
    componentDidMount()
    {
        axios.get(`${this.props.apiUrl}?nocache=${getTime()}`).then((res) => {
            this.setState({...res.data});
        })
        .catch((error) => console.log(error));
    }


    render() {
        const reportTemplate = this.props.reportTemplate;
        return <Fragment>
            <div className="no-print">
                <button className="button is-dark back-button" onClick={this.props.backToDashboard} title="Back">&larr;</button>
                <button className="button is-link back-button" onClick={() => window.print()} title="Print">&#128438;</button>
            </div>
            {
                reportTemplate === '2' &&
                <Report2 grades={this.props.grades} state={this.state}/>
            }
            {
                reportTemplate === '1' &&
                <Report1  grades={this.props.grades} state={this.state}/>
            }
            {
                reportTemplate === '3' &&
                <Report3  grades={this.props.grades} state={this.state}/>
            }
            {
                reportTemplate === '4' && 
                <Report4 grades={this.props.grades} state={this.state}/>
            }

            {
                reportTemplate === '5' && 
                <Report5 grades={this.props.grades} state={this.state}/>
            }
			{
                reportTemplate === '6' && 
                <Report6 grades={this.props.grades} state={this.state}/>
            }
			{
                reportTemplate === '7' && 
                <Report7 grades={this.props.grades} state={this.state}/>
            }
			{
                reportTemplate === '8' && 
                <Report8 grades={this.props.grades} state={this.state}/>
            }
			{
                reportTemplate === '9' && 
                <Report9 grades={this.props.grades} state={this.state}/>
            }
        </Fragment>
    }
}

export default Report;