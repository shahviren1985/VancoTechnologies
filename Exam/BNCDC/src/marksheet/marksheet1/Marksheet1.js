import React, { Fragment } from 'react';
import './style.css';
import { getVersion1Data } from '../../util';

class Marksheet1 extends React.Component {

    render() {

        const state = getVersion1Data(this.props.state, this.props.grades)
        return <Fragment>
            {
                state.rows.map((row, index) =>
                    <div className="page_1" key={index}>
                        <div className="tx1"><span className="ft0">Miss. / Mrs.</span></div>

                        <div className="id1_1">
                            <p className="p0 ft1">Statement showing marks obtained by:</p>
                            <p className="p1 ft1"><strong><u>{row.name}</u></strong></p>
                            <p className="p2 ft3">In <span className="ft2"><strong>{state.examName}</strong> </span>examinations during the year {state.examYear}.</p>
                            <table cellPadding={0} cellSpacing={0} className="t0">
                                <tbody>
                                    <tr>
                                        <td className="tr0 td0"><p className="p3 ft4">&nbsp;</p></td>
                                        <td className="tr0 td1"><p className="p4 ft2"><strong>Examination</strong></p></td>
                                        <td className="tr0 td2"><p className="p5 ft2"><strong>Marks Obtained</strong></p></td>
                                        <td className="tr0 td3"><p className="p6 ft2"><strong> Out of </strong></p></td>
                                    </tr>
                                    <tr>
                                        <td className="tr3 td4"><p className="p7 ft2">1.</p></td>
                                        <td className="tr3 td5"><p className="p4 ft2">Internal</p></td>
                                        <td className="tr3 td6"><p className="p8 ft2">{row._internal}</p></td>
                                        <td className="tr3 td7"><p className="p9 ft5">{row._internal_max}</p></td>
                                    </tr>
                                    <tr>
                                        <td className="tr2 td8"><p className="p3 ft6">&nbsp;</p></td>
                                        <td className="tr2 td9"><p className="p3 ft6">&nbsp;</p></td>
                                        <td className="tr2 td10"><p className="p3 ft6">&nbsp;</p></td>
                                        <td className="tr2 td11"><p className="p3 ft6">&nbsp;</p></td>
                                    </tr>
                                    <tr>
                                        <td className="tr1 td4"><p className="p7 ft2">2.</p></td>
                                        <td className="tr1 td5"><p className="p4 ft2">External</p></td>
                                        <td className="tr1 td6"><p className="p8 ft2">{row._external}</p></td>
                                        <td className="tr1 td7"><p className="p9 ft5">{row._external_max}</p></td>
                                    </tr>
                                    <tr>
                                        <td className="tr4 td8"><p className="p3 ft7">&nbsp;</p></td>
                                        <td className="tr4 td9"><p className="p3 ft7">&nbsp;</p></td>
                                        <td className="tr4 td10"><p className="p3 ft7">&nbsp;</p></td>
                                        <td className="tr4 td11"><p className="p3 ft7">&nbsp;</p></td>
                                    </tr>
                                    <tr>
                                        <td className="tr5 td4"><p className="p3 ft4">&nbsp;</p></td>
                                        <td className="tr5 td5"><p className="p4 ft2"><strong> Total</strong></p></td>
                                        <td className="tr5 td6"><p className="p8 ft2"><strong> {row._totalObtained}</strong></p></td>
                                        <td className="tr5 td7"><p className="p9 ft5"> <strong> {row._total}</strong></p></td>
                                    </tr>
                                    <tr>
                                        <td className="tr6 td8"><p className="p3 ft8">&nbsp;</p></td>
                                        <td className="tr6 td9"><p className="p3 ft8">&nbsp;</p></td>
                                        <td className="tr6 td10"><p className="p3 ft8">&nbsp;</p></td>
                                        <td className="tr6 td11"><p className="p3 ft8">&nbsp;</p></td>
                                    </tr>
                                </tbody>
                            </table>
                            <table cellPadding={0} cellSpacing={0} className="t1">
                                <tbody>
                                    <tr>
                                        <td className="tr7 td12"><p className="p3 ft2">Result: {row._result}</p></td>
                                        <td className="tr7 td13"><p className="p3 ft2">Percentage: {row._percent} %</p></td>
                                        <td className="tr7 td14"><p className="p3 ft5">Grade: {row._grade}</p></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div className="id1_2">
                            <table cellPadding={0} cellSpacing={0} className="t2">
                                <tbody>
                                    <tr>
                                        <td className="tr8 td15"><p className="p3 ft3">Date: {state.date}</p></td>
                                        <td className="tr8 td16"><p className="p3 ft3">Signature Of The Principal</p></td>
                                        <td className="tr8 td15"><p className="p3 ft3">Faculty Incharge</p></td>
                                        <td className="tr8 td17"><p className="p3 ft9">College Seal</p></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                )
            }

        </Fragment>
    }
}

export default Marksheet1;