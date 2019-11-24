import React, { Fragment } from 'react';
import './style.css';
import logo from '../../images/Logo_BNCDC.jpg';
import { getVersion3Data } from '../../util';
import chunk from 'lodash/chunk';

class Report3 extends React.Component {
    render() {
        const state = getVersion3Data(this.props.state, this.props.grades);
        const chunks = chunk(state.rows, 2);
        return <Fragment>
            {
                chunks.map((chunk, index) =>

                    <div className="page_1" key={index}>
                        <div className="id1_1">
                            <div className="main">
                                <div><img src={logo} className="Logo" alt="Logo" width="80" height="80" /></div>
                                <p className="p0 ft0 bold">DR. BHANUBEN NANAVATI CAREER DEVELOPMENT CENTRE</p>
                                <p className="p1 ft1 bold">COLLEGE: SHREE CHANDULAL NANAVATI WOMEN'S INSTITUTE AND GIRL'S HIGH SCHOOL, MUMBAI ­ 400 056.</p>
                                <p className="p2 ft2 bold">COLLEGE RESULT SHEET FOR {state.examName}, EXAMINATION HELD IN {state.examYear}.</p>
                            </div>
                            <div className="clearfix"></div>
                            {
                                chunk.map((row, i) =>

                                    <table className="t0" border="1" cellSpacing="0" cellPadding="0" key={i}>
                                        <tbody>
                                            <tr>
                                                <td colSpan="7">
                                                    <span className="bold">NAME: {row.name}</span>
                                                    <span className="rollnumer">ROLL NO: {row.rollNumber}</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="45%" className="bold center">Subject</td>
                                                <td width="10%" className="bold center">Internal Marks</td>
                                                <td width="10%" className="bold center">External Marks</td>
												<td width="10%" className="bold center">Academic Performance</td>
                                                <td width="5%" className="bold center">Grace</td>
                                                <td width="10%" className="bold center">Total<br />Marks</td>
                                                <td width="5%" className="bold center">Grade</td>
                                                <td width="15%" rowSpan="6">
                                                    <div className="padding-div"><pre>GRADE                  : {row._grade}</pre></div>
                                                    <div className="padding-div"><pre>TOTAL MARKS     : {row._grand_total} / {row._total}</pre></div>
                                                    <div className="padding-div"><pre>PERCENTAGE      : {row._percent}%</pre></div>
                                                    <div className="padding-div"><pre>RESULT                 : {row._result}</pre></div>
                                                </td>
                                            </tr>
                                            {
                                                row.papers.map((paper, paperIndex) =>
                                                    <tr key={paperIndex}>
                                                        <td>{paper.paperTitle}</td>
                                                        <td className="center">{isNaN(paper._internal) ? paper._internal : `${paper._internal} / ${paper._internal_max}`}</td>
                                                        <td className="center">{isNaN(paper._external) ? paper._external : (paper._grace ? `${paper._external} + ${paper._grace} / ${paper._external_max}` : `${paper._external} / ${paper._external_max}`)}</td>
														<td className="center">{isNaN(paper._academic) ? paper._academic : `${paper._academic} / ${paper._academic_max}`}</td>
                                                        <td className="center">{paper._grace > 0 ? paper._grace : '-'}</td>
                                                        <td className="center">{paper._totalObtained} / {paper._total}</td>
                                                        <td className="center">{paper._grade}</td>
                                                    </tr>
                                                )
                                            }


                                            <tr>
                                                <td className="bold center">TOTAL</td>
                                                <td className="bold center">{row._internal_total_o} </td>
                                                <td className="bold center">{row._external_total_o} </td>
												<td className="bold center">{row._academic_total_o} </td>
                                                <td className="bold center">{row._total_grace > 0 ? row._total_grace : '-'}</td>
                                                <td className="bold center">{row._grand_total} / {row._total}</td>
                                                <td className="bold center">{row._grade}</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                )
                            }
                            <table cellPadding={0} cellSpacing={0} className="t1">
                                <tbody>
                                    <tr>
                                        <td className="tr16 td38">
                                            <p className="p12 ft17">PLACE</p>
                                        </td>
                                        <td className="tr16 td39">
                                            <p className="p13 ft17">: VILE PARLE (W), MUMBAI</p>
                                        </td>
                                        <td className="tr16 td40">
                                            <p className="p20 ft17">PRINCIPAL</p>
                                        </td>
                                        <td rowSpan={2} className="tr24 td41">
                                            <p className="p12 ft17">FACULTY INCHARGE</p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td className="tr15 td38">
                                            <p className="p12 ft12">&nbsp;</p>
                                        </td>
                                        <td className="tr15 td39">
                                            <p className="p12 ft12">&nbsp;</p>
                                        </td>
                                        <td rowSpan={2} className="tr25 td40">
                                            <p className="p21 ft17">DR. RAJSHREE TRIVEDI</p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td rowSpan={2} className="tr11 td38">
                                            <p className="p12 ft17">DATE</p>
                                        </td>
                                        <td rowSpan={2} className="tr11 td39">
                                            <p className="p13 ft17">: {state.date}</p>
                                        </td>
                                        <td className="tr21 td41">
                                            <p className="p12 ft15">&nbsp;</p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td className="tr26 td40">
                                            <p className="p12 ft18">&nbsp;</p>
                                        </td>
                                        <td className="tr26 td41">
                                            <p className="p12 ft18">&nbsp;</p>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div className="id1_2">
                            <p className="p22 ft19">NOTE: * INDICATES CURRENT APPEARANCE, + INDICATES GRACE MARK, RR = RESERVED, ABS = ABSENT, NA =
				NOT APPLICABLE, F = FAIL.</p>
                        </div>
                    </div>
                )
            }
        </Fragment>
    }

}

export default Report3;