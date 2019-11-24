import React, { Fragment } from 'react';
import './style.css';
import { getVersion4Data } from '../../util';

class Marksheet4 extends React.Component {
    render() {
        const state = getVersion4Data(this.props.state, this.props.grades);
        return <Fragment>
            {
                state.rows.map((row, index) =>
                    <div className="page_1" key={index}>
                        <table cellPadding={0} cellSpacing={0} className="t0">
                            <tbody>
                                <tr>
                                    <td className="tr0 td0"><p className="p0 ft0">Student Name</p></td>
                                    <td className="tr0 td1"><p className="p1 ft0">Roll Number</p></td>
                                    <td className="tr0 td2"><p className="p2 ft0">Course Name</p></td>
                                </tr>
                                <tr>
                                    <td className="tr1 td3"><p className="p3 ft1">{row.name}</p></td>
                                    <td className="tr1 td4"><p className="p4 ft1">{row.rollNumber}</p></td>
                                    <td className="tr1 td5"><p className="p5 ft1">{state.examName} ({"Semester " + state.year.charAt(4)})</p></td>
                                </tr>
                                <tr>
                                    <td className="tr2 td6"><p className="p6 ft3">&nbsp;</p></td>
                                    <td className="tr2 td7"><p className="p6 ft3">&nbsp;</p></td>
                                    <td className="tr2 td8"><p className="p6 ft3">&nbsp;</p></td>
                                </tr>
                            </tbody>
                        </table>
                        <p className="p7 ft4">Statement Of Marks</p>
                        <table cellPadding={0} cellSpacing={0} className="t1">
                            <tbody>
                                <tr>
                                    <td className="tr3 td9"><p className="p8 ft1"><span className="ft0">Year: </span>{state.examYear}</p></td>
                                    <td className="tr3 td10"><p className="p6 ft5">&nbsp;</p></td>
                                    <td className="tr3 td11"><p className="p6 ft5">&nbsp;</p></td>
                                    <td className="tr3 td11"><p className="p6 ft5">&nbsp;</p></td>
                                    <td className="tr3 td11"><p className="p6 ft5">&nbsp;</p></td>
                                    <td className="tr3 td11"><p className="p6 ft5">&nbsp;</p></td>
                                    
                                </tr>
                                <tr>
                                    <td className="tr4 td12"><p className="p9 ft0">Paper Title</p></td>
                                    <td className="tr4 td13"><p className="p10 ft0">Internal</p></td>
                                    <td className="tr4 td14"><p className="p11 ft0">External</p></td>
                                    <td className="tr4 td14"><p className="p12 ft6">Attendance</p></td>
                                    <td className="tr4 td14"><p className="p5 ft6">Total</p></td>
                                    <td className="tr4 td13"><p className="p5 ft6">Grade</p></td>
                                </tr>
                                <tr>
                                    <td className="tr5 td15"><p className="p6 ft7">&nbsp;</p></td>
                                    <td className="tr5 td16"><p className="p6 ft7">&nbsp;</p></td>
                                    <td className="tr5 td17"><p className="p6 ft7">&nbsp;</p></td>
                                    <td className="tr5 td17"><p className="p6 ft7">&nbsp;</p></td>
                                    <td className="tr5 td17"><p className="p6 ft7">&nbsp;</p></td>
                                    <td className="tr5 td17"><p className="p6 ft7">&nbsp;</p></td>
                                </tr>
                                {
                                    row.papers.map((paper, paperIndex) => 
                                    <Fragment key={paperIndex}>
                                        <tr>
                                            <td className="tr4 td12"><p className="p13 ft1">{paper.paperTitle}</p></td>
                                            <td className="tr4 td14"><p className="p15 ft1">{isNaN(paper._internal) ? paper._internal: `${paper._internal}/${paper._internal_max}` }</p></td>
                                            <td className="tr4 td14"><p className="p15 ft1">{isNaN(paper._external) ? paper._external: isNaN(paper._grace) || paper._grace == 0 ? `${paper._external}/${paper._external_max}` : `${paper._external}+${paper._grace}/${paper._external_max}` }</p></td>
                                            <td className="tr4 td13"><p className="p14 ft1">{isNaN(paper._attendance) ? paper._attendance: `${paper._attendance}/${paper._attendance_max}` }</p></td>
                                            <td className="tr4 td14"><p className="p16 ft1">{`${paper._totalObtained}/${paper._total}`}</p></td>
                                            <td className="tr4 td13"><p className="p5 ft1">{paper._grade}</p></td>
                                        </tr>
                                        <tr>
                                            <td className="tr2 td15"><p className="p6 ft3">&nbsp;</p></td>
                                            <td className="tr2 td16"><p className="p6 ft3">&nbsp;</p></td>
                                            <td className="tr2 td17"><p className="p6 ft3">&nbsp;</p></td>
                                            <td className="tr2 td17"><p className="p6 ft3">&nbsp;</p></td>
                                            <td className="tr2 td17"><p className="p6 ft3">&nbsp;</p></td>
                                            <td className="tr2 td17"><p className="p6 ft3">&nbsp;</p></td>
                                        </tr>
                                    </Fragment>
                                    )
                                }
                               
                            </tbody>
                        </table>
                        <table cellPadding={0} cellSpacing={0} className="t2">
                            <tbody>
                                <tr>
                                    <td className="tr10 td18"><p className="p6 ft0">Result: <span className="ft1">{row._result}</span></p></td>
                                    <td className="tr10 td19"><p className="p6 ft0">Percentage: <span className="ft1">{row._percent}{ row._percent == "-" ? "" : "%"}</span></p></td>
                                    <td className="tr10 td20"><p className="p6 ft0">Grade: <span className="ft1">{row._grade}</span></p></td>
                                    <td className="tr10 td21"><p className="p6 ft6">Grand Total: <span className="ft11">{row._grand_total}/{row._total}</span></p></td>
                                </tr>
                            </tbody>
                        </table>
                        <table cellPadding={0} cellSpacing={0} className="t3">
                            <tbody>
                                <tr>
                                    <td className="tr10 td18"><p className="p6 ft0">Date :{state.date}</p></td>
                                    <td className="tr10 td19"><p className="p6 ft0">Signature Of The Principal</p></td>
                                    <td className="tr10 td20"><p className="p6 ft0">Faculty Incharge</p></td>
                                    <td className="tr10 td22"><p className="p6 ft6">College Seal</p></td>
                                </tr>
                            </tbody>
                        </table>
                        <p className="p17 ft1">Note: * indicates current appearance, + indicates grace mark, RR = Reserved, ABS = Absent, NP = Not Permitted, F = Fail</p>
                    </div>
                )
            }

        </Fragment>;
    }
}

export default Marksheet4;