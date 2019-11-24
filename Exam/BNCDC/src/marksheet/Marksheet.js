import React, { Fragment } from 'react';
import axios from 'axios';
import { getTime } from '../util';
import Loadable from 'react-loadable';

const Marksheet1 = Loadable({
    loader: () => import('./marksheet1/Marksheet1'),
    loading: () => <div>Loading ...</div>
});

const Marksheet2 = Loadable({
    loader: () => import('./marksheet2/Marksheet2'),
    loading: () => <div>Loading ...</div>
});

const Marksheet3 = Loadable({
    loader: () => import('./marksheet3/Marksheet3'),
    loading: () => <div>Loading ...</div>

})

const Marksheet4 = Loadable({
    loader: () => import('./marksheet4/Marksheet4'),
    loading: () => <div>Loading ...</div>

})

const Marksheet5 = Loadable({
    loader: () => import('./marksheet5/Marksheet5'),
    loading: () => <div>Loading ...</div>

})

const Marksheet6 = Loadable({
    loader: () => import('./marksheet6/Marksheet6'),
    loading: () => <div>Loading ...</div>

})

const Marksheet7 = Loadable({
    loader: () => import('./marksheet7/Marksheet7'),
    loading: () => <div>Loading ...</div>

})

const Marksheet8 = Loadable({
    loader: () => import('./marksheet8/Marksheet8'),
    loading: () => <div>Loading ...</div>
})

const Marksheet9 = Loadable({
    loader: () => import('./marksheet9/Marksheet9'),
    loading: () => <div>Loading ...</div>
})

class Marksheet extends React.Component {
    constructor(props) {
        super(props);
        this.state = { date: '', examName: '', examYear: '', rows: [] };
    }

    componentDidMount() {
        axios.get(`${this.props.apiUrl}?nocache=${getTime()}`).then((res) => {
            this.setState({ ...res.data });
        })
            .catch((error) => console.log(error));
    }

    render() {
        const marksheetTemplate = this.props.marksheetTemplate;
        return <Fragment>
            <div className="no-print">
                <button className="button is-dark back-button" onClick={this.props.backToDashboard} title="Back">&larr;</button>
                <button className="button is-link back-button" onClick={() => window.print()} title="Print">&#128438;</button>
            </div>
            <div className="clearfix no-print"></div>
            {
                marksheetTemplate === "2" &&
                <Marksheet2 grades={this.props.grades} state={this.state} />
            }
            {
                marksheetTemplate === "1" &&
                <Marksheet1 grades={this.props.grades} state={this.state} />
            }
            {
                marksheetTemplate === "3" &&
                <Marksheet3 grades={this.props.grades} state={this.state} />
            }
            {
                marksheetTemplate === "4" &&
                <Marksheet4 grades={this.props.grades} state={this.state} />
            }
            {
                marksheetTemplate === "5" &&
                <Marksheet5 grades={this.props.grades} state={this.state} />
            }

            {
                marksheetTemplate === "6" &&
                <Marksheet6 grades={this.props.grades} state={this.state} />
            }
			
			{
                marksheetTemplate === "7" &&
                <Marksheet7 grades={this.props.grades} state={this.state} />
            }
			
			{
                marksheetTemplate === "8" &&
                <Marksheet8 grades={this.props.grades} state={this.state} />
            }
            {
                marksheetTemplate === "9" &&
                <Marksheet9 grades={this.props.grades} state={this.state} />
            }
        </Fragment>
    }
}

export default Marksheet;