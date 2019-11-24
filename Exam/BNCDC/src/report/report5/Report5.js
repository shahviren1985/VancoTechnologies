import React, { Fragment } from 'react';
import './style.css';
import logo from '../../images/Logo_BNCDC.jpg';
import { getVersion5Data } from '../../util';
import chunk from 'lodash/chunk';

class Report5 extends React.Component {
    render() {
        const state = getVersion5Data(this.props.state, this.props.grades);
        const chunks = chunk(state.rows, 2);
        return <Fragment>
            {
                chunks.map((chunk, index) =>
                    <div className="page_1" key={index}>
                        <div>
                            <div className="main">
                                <div><img src={logo} className="Logo" alt="Logo" width="80" height="80" /></div>
                                <p className="p_0 f_t0">Dr. BHANUBEN NANAVATI CAREER DEVELOPMENT CENTRE</p>
                                <p className="p_1 f_t1">COLLEGE : SHREE CHANDULAL NANAVATI WOMEN'S INSTITUTE AND GIRL'S HIGH SCHOOL, VILE PARLE</p>
                                <p className="p_2 f_t2">Ledger Report</p>
                            </div>
                        </div>

                        <div className="dclr"></div>
                        <table cellPadding={0} cellSpacing={0} className="t0">
                            <tbody>
                                <tr>
                                    <td className="tr0 td0"><p className="p0 ft0">{state.examName}</p></td>
                                    <td className="tr0 td1"><p className="p0 ft0">Academic year {state.examYear}</p></td>
                                </tr>
                            </tbody>
                        </table>
                        {
                            chunk.map((row, rowIndex) => 
                           <Fragment key={rowIndex}>
                        <table cellPadding={0} cellSpacing={0} className="t1">
                            <tbody>
                                <tr>
                                    <td className="tr1 td2"></td>
                                    <td colSpan={2} className="tr1 td3"><p className="p1 ft0"><span className="ft1">STUDENT NAME: </span>{row.name}</p></td>
                                    <td className="tr1 td4"><p className="p0 ft2">&nbsp;</p></td>
                                    <td className="tr1 td5"><p className="p2 ft1">ROLL NUMBER: <span className="ft0">{row.rollNumber}</span></p></td>
                                    <td className="tr1 td6"><p className="p0 ft2">&nbsp;</p></td>
                                </tr>
                                <tr>
                                    <td className="tr2 td2"></td>
                                    <td className="tr2 td7"><p className="p0 ft3">&nbsp;</p></td>
                                    <td className="tr2 td8"><p className="p0 ft3">&nbsp;</p></td>
                                    <td className="tr2 td9"><p className="p0 ft3">&nbsp;</p></td>
                                    <td className="tr0 td5"><p className="p0 ft2">&nbsp;</p></td>
                                    <td className="tr0 td6"><p className="p0 ft2">&nbsp;</p></td>
                                </tr>
                                <tr>
                                    <td className="tr2 td2"></td>
                                    <td className="tr2 td10"><p className="p3 ft4">Paper Title</p></td>
                                    <td className="tr2 td11"><p className="p4 ft4">Marks Obtained</p></td>
                                    <td className="tr2 td12"><p className="p4 ft4">Out Of Marks</p></td>
                                    <td className="tr2 td5"><p className="p0 ft3">&nbsp;</p></td>
                                    <td className="tr2 td6"><p className="p0 ft3">&nbsp;</p></td>
                                </tr>
                                <tr>
                                    <td className="tr3 td2"></td>
                                    <td className="tr3 td13"><p className="p0 ft5">&nbsp;</p></td>
                                    <td className="tr3 td14"><p className="p0 ft5">&nbsp;</p></td>
                                    <td className="tr3 td15"><p className="p0 ft5">&nbsp;</p></td>
                                    <td className="tr4 td5"><p className="p0 ft6">&nbsp;</p></td>
                                    <td className="tr4 td6"><p className="p0 ft6">&nbsp;</p></td>
                                </tr>
                                <tr>
                                    <td className="tr5 td2"></td>
                                    <td className="tr5 td13"><p className="p1 ft7">Written</p></td>
                                    <td className="tr5 td14"><p className="p5 ft0">{row._written}</p></td>
                                    <td className="tr5 td15"><p className="p6 ft7">{row._written_max}</p></td>
                                    <td rowSpan={2} className="tr6 td5"><p className="p2 ft1">Result:</p></td>
                                    <td rowSpan={2} className="tr6 td6"><p className="p7 ft0">{row._result}</p></td>
                                </tr>
                                <tr>
                                    <td className="tr7 td2"></td>
                                    <td rowSpan={2} className="tr5 td13"><p className="p1 ft7">Submission</p></td>
                                    <td rowSpan={2} className="tr5 td14"><p className="p5 ft0">{row._submission}</p></td>
                                    <td rowSpan={2} className="tr5 td15"><p className="p6 ft7">{row._submission_max}</p></td>
                                </tr>
                                <tr>
                                    <td className="tr8 td2"></td>
                                    <td className="tr8 td5"><p className="p0 ft8">&nbsp;</p></td>
                                    <td className="tr8 td6"><p className="p0 ft8">&nbsp;</p></td>
                                </tr>
                                <tr>
                                    <td className="tr5 td2"></td>
                                    <td className="tr5 td13"><p className="p1 ft7">Practical</p></td>
                                    <td className="tr5 td14"><p className="p5 ft0">{row._practical}</p></td>
                                    <td className="tr5 td15"><p className="p6 ft7">{row._practical_max}</p></td>
                                    <td className="tr9 td5"><p className="p2 ft1">Percentage:</p></td>
                                    <td className="tr9 td6"><p className="p7 ft0">{row._percent}%</p></td>
                                </tr>
                                <tr>
                                    <td className="tr10 td2"></td>
                                    <td className="tr10 td13"><p className="p1 ft9">General</p></td>
                                    <td className="tr10 td14"><p className="p5 ft0">{row._general}</p></td>
                                    <td className="tr10 td15"><p className="p6 ft9">{row._general_max}</p></td>
                                    <td rowSpan={2} className="tr11 td5"><p className="p2 ft1">Grade:</p></td>
                                    <td rowSpan={2} className="tr11 td6"><p className="p7 ft0">{row._grade}</p></td>
                                </tr>
                                <tr>
                                    <td className="tr12 td2"></td>
                                    <td rowSpan={2} className="tr5 td13"><p className="p8 ft10">Total</p></td>
                                    <td rowSpan={2} className="tr5 td14"><p className="p5 ft1">{row._totalObtained}</p></td>
                                    <td rowSpan={2} className="tr5 td15"><p className="p6 ft10">{row._total}</p></td>
                                </tr>
                                <tr>
                                    <td className="tr4 td2"></td>
                                    <td className="tr4 td5"><p className="p0 ft6">&nbsp;</p></td>
                                    <td className="tr4 td6"><p className="p0 ft6">&nbsp;</p></td>
                                </tr>
                            </tbody>
                        </table>
                        {
                            rowIndex === 0 && chunk.length === 2  &&
                            <Fragment>
                            <hr className="line" />
                            <div className="clearfix"></div>
                            </Fragment>
                        }
                        </Fragment>
                        )
                    }
                       
                        <hr className="line" />
                        <div className="clearfix"></div>

                        <table cellPadding={0} cellSpacing={0} className="t3">
                            <tbody>
                                <tr>
                                    <td className="tr5 td21"><p className="p0 ft7">Date: {state.date}</p></td>
                                    <td className="tr5 td22"><p className="p0 ft7">Signature Of The Principal</p></td>
                                    <td className="tr5 td23"><p className="p0 ft7">Faculty Incharge</p></td>
                                    <td className="tr5 td24"><p className="p0 ft14">College Seal</p></td>
                                </tr>
                            </tbody>
                        </table>
                        <p className="p10 ft15">Note: * indicates current appearance, + indicates grace mark, RR = Reserved, AB = Absent, NP = Not Permitted, F = Fail.</p>
                    </div>
                )
            }
        </Fragment>
    }
}

export default Report5;