import React, { Fragment } from 'react';
import './style.css';
import logo from '../../images/Logo_BNCDC.jpg';
import { getVersion4Data } from '../../util';
import chunk from 'lodash/chunk';

class Report4 extends React.Component {
    render() {
        const state = getVersion4Data(this.props.state, this.props.grades);
        const chunks = chunk(state.rows, 2);
        return <Fragment>
            {
                chunks.map((chunk, index) =>

                    <div className="page_1" key={index}>
                        <div className="id1_1">
                            <div className="main">
                                <div><img src={logo} className="Logo" alt="Logo" width="85" height="85" /></div>
                                <p className="p0 ft0">Dr. BHANUBEN NANAVATI CAREER DEVELOPMENT CENTRE</p>
                                <p className="p1 ft1">COLLEGE : SHREE CHANDULAL NANAVATI WOMEN'S INSTITUTE AND GIRL'S HIGH SCHOOL, VILE PARLE</p>
                                <p className="p2 ft2">Ledger Report</p>
                            </div>
                            {
                                chunk.map((row, rowIndex) =>

                                    <div className="t0" key={rowIndex}>
                                        <div>
                                            <span className="name">NAME OF THE STUDENT: {row.name} </span>
                                            <span className="right">
                                                <span className="rollno">
                                                    ROLL NO: {row.rollNumber} </span>

                                                <span className="bold">COURSE NAME: </span> {state.examName} ({"Semester " + state.year.charAt(4)})
                                            </span>
                                        </div>
                                        <div className="clearfix"></div>
                                        <div className="right">
                                            <p className="total pb-15">
                                                <span className="bold">TOTAL:  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;</span>{row._grand_total}/{row._total}</p>
                                            <p className=" pb-15">
                                                <span className="bold">PERCENTAGE: &nbsp;</span>{row._percent}{ row._percent == "-" ? "" : "%"}
		                    </p>
                                            <p>
                                                <span className="bold">RESULT: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>{row._result}
                                            </p>
                                        </div>
                                        <table cellPadding={0} cellSpacing={0} border="1" className="t0-w">
                                            <tbody>
                                                <tr>
                                                    <td width="40%" className="bold center">Paper Title</td>
                                                    <td width="10%" className="bold center">Internal</td>
                                                    <td width="10%" className="bold center">External</td>
                                                    <td width="10%" className="bold center">Attendance</td>
                                                    <td width="10%" className="bold center">Total</td>
                                                    <td width="10%" className="bold center">Grade</td>
                                                </tr>
                                                {
                                                    row.papers.map((paper, paperIndex) =>
                                                        <tr key={paperIndex}>
                                                            <td>{paper.paperTitle}</td>
                                                            <td className="center">{isNaN(paper._internal) ? paper._internal : `${paper._internal}/${paper._internal_max}`}</td>
                                                            <td className="center">{isNaN(paper._external) ? paper._external : isNaN(paper._grace) || paper._grace == 0 ? `${paper._external}/${paper._external_max}` : `${paper._external}+${paper._grace}/${paper._external_max}`}</td>
                                                            <td className="center">{isNaN(paper._attendance) ? paper._attendance : `${paper._attendance}/${paper._attendance_max}`}</td>
                                                            <td className="center">{`${paper._totalObtained}/${paper._total}`}</td>
                                                            <td className="center">{paper._grade}</td>
                                                        </tr>
                                                    )
                                                }

                                            </tbody>
                                        </table>
                                        <div className="clearfix"></div>

                                    </div>
                                )
                            }
                            <table cellPadding={0} cellSpacing={0} className="t2">
                                <tbody>
                                    <tr>
                                        <td className="tr0 td14"><p className="p4 ft4">DATE : {state.date}</p></td>
                                        <td className="tr0 td15"><p className="p17 ft4">PRINCIPAL</p></td>
                                        <td className="tr0 td16"><p className="p4 ft4">COLLEGE SEAL</p></td>
                                        <td className="tr0 td17"><p className="p4 ft4">Faculty Incharge</p></td>
                                    </tr>
                                    <tr>
                                        <td className="tr0 td14"><p className="p4 ft3">&nbsp;</p></td>
                                        <td className="tr0 td15"><p className="p17 ft4">DR. RAJSHREE TRIVEDI</p></td>
                                        <td className="tr0 td16"><p className="p4 ft3">&nbsp;</p></td>
                                        <td className="tr0 td17"><p className="p4 ft3">&nbsp;</p></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div className="id1_2">
                            <p className="p18 ft9">Note: + indicates grace mark, RR = Reserved, AB = Absent, NP = Not Permitted, F = Fail</p>
                        </div>
                    </div>
                )
            }
        </Fragment>;
    }
}

export default Report4;