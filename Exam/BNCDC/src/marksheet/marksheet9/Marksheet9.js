import React, { Fragment } from 'react';
import './style.css';
import { getVersion9Data } from '../../util';

class Marksheet9 extends React.Component {
    render() {

        const state = getVersion9Data(this.props.state, this.props.grades);
        return <Fragment>
            {
                state.rows.map((row, index) =>

                    <div className="page_1" key={index}>

                        <table cellPadding={0} cellSpacing={0} className="t0">
                            <tbody>
                                <tr>
                                    <td className="tr0 td0"><p className="p0 ft0">&nbsp;</p></td>
                                    <td className="tr0 td1"><p className="p1 ft1">STUDENT NAME</p></td>
                                    <td className="tr0 td2"><p className="p0 ft0">&nbsp;</p></td>
                                    <td className="tr0 td3"><p className="p0 ft0">&nbsp;</p></td>
                                    <td className="tr0 td4"><p className="p2 ft1">ROLL NUMBER</p></td>
                                    <td className="tr0 td5"><p className="p0 ft0">&nbsp;</p></td>
                                    <td className="tr0 td6"><p className="p0 ft0">&nbsp;</p></td>
                                    <td className="tr0 td7"><p className="p3 ft1">COURSE NAME</p></td>
                                    <td className="tr1 td8"><p className="p0 ft2">&nbsp;</p></td>
                                </tr>
                                <tr>
                                    <td className="tr2 td9"><p className="p0 ft2">&nbsp;</p></td>
                                    <td rowSpan={2} className="tr3 td10"><p className="p4 ft3">{row.name}</p></td>
                                    <td className="tr2 td11"><p className="p0 ft2">&nbsp;</p></td>
                                    <td colSpan={2} rowSpan={2} className="tr3 td12"><p className="p5 ft3">{row.rollNumber}</p></td>
                                    <td className="tr2 td13"><p className="p0 ft2">&nbsp;</p></td>
                                    <td colSpan={2} className="tr2 td14"><p className="p6 ft4">{state.examName}</p></td>
                                    <td className="tr2 td8"><p className="p0 ft2">&nbsp;</p></td>
                                </tr>
                                <tr>
                                    <td className="tr4 td9"><p className="p0 ft5">&nbsp;</p></td>
                                    <td className="tr4 td11"><p className="p0 ft5">&nbsp;</p></td>
                                    <td className="tr4 td13"><p className="p0 ft5">&nbsp;</p></td>
                                    <td className="tr4 td15"><p className="p0 ft5">&nbsp;</p></td>
                                    <td rowSpan={2} className="tr1 td16"><p className="p7 ft3"></p></td>
                                    <td className="tr4 td8"><p className="p0 ft5">&nbsp;</p></td>
                                </tr>
                                <tr>
                                    <td className="tr4 td17"><p className="p0 ft5">&nbsp;</p></td>
                                    <td className="tr4 td18"><p className="p0 ft5">&nbsp;</p></td>
                                    <td className="tr4 td19"><p className="p0 ft5">&nbsp;</p></td>
                                    <td className="tr4 td20"><p className="p0 ft5">&nbsp;</p></td>
                                    <td className="tr4 td21"><p className="p0 ft5">&nbsp;</p></td>
                                    <td className="tr4 td22"><p className="p0 ft5">&nbsp;</p></td>
                                    <td className="tr4 td23"><p className="p0 ft5">&nbsp;</p></td>
                                    <td className="tr5 td8"><p className="p0 ft6">&nbsp;</p></td>
                                </tr>
                                <tr>
                                    <td className="tr6 td24"><p className="p0 ft2">&nbsp;</p></td>
                                    <td className="tr6 td10"><p className="p0 ft2">&nbsp;</p></td>
                                    <td colSpan={3} className="tr6 td25"><p className="p0 ft7">Statement of Marks</p></td>
                                    <td className="tr6 td26"><p className="p0 ft2">&nbsp;</p></td>
                                    <td className="tr6 td15"><p className="p0 ft2">&nbsp;</p></td>
                                    <td className="tr6 td27"><p className="p0 ft2">&nbsp;</p></td>
                                    <td className="tr6 td8"><p className="p0 ft2">&nbsp;</p></td>
                                </tr>
                                <tr>
                                    <td className="tr7 td24"><p className="p0 ft2">&nbsp;</p></td>
                                    <td className="tr1 td18"><p className="p2 ft3">Year: {state.examYear}</p></td>
                                    <td className="tr1 td28"><p className="p0 ft2">&nbsp;</p></td>
                                    <td className="tr1 td20"><p className="p0 ft2">&nbsp;</p></td>
                                    <td className="tr1 td21"><p className="p0 ft2">&nbsp;</p></td>
                                    <td className="tr1 td29"><p className="p0 ft2">&nbsp;</p></td>
                                    <td className="tr1 td23"><p className="p0 ft2">&nbsp;</p></td>
                                    <td className="tr1 td30"><p className="p0 ft2">&nbsp;</p></td>
                                    <td className="tr1 td31"><p className="p0 ft2">&nbsp;</p></td>
                                </tr>
                                <tr>
                                    <td className="tr8 td32"><p className="p0 ft2">&nbsp;</p></td>
                                    <td className="tr8 td10"><p className="p8 ft8">Paper Title</p></td>
                                    <td className="tr8 td33"><p className="p0 ft2">&nbsp;</p></td>
                                    <td className="tr8 td34"><p className="p0 ft2">&nbsp;</p></td>
                                    <td colSpan={3} className="tr8 td35"><p className="p9 ft8">Marks Obtained</p></td>
                                    <td className="tr8 td27"><p className="p10 ft8">Out Of Marks</p></td>
                                    <td className="tr8 td36"><p className="p0 ft2">&nbsp;</p></td>
                                </tr>
                                <tr>
                                    <td className="tr9 td32"><p className="p0 ft9">&nbsp;</p></td>
                                    <td className="tr10 td18"><p className="p0 ft10">&nbsp;</p></td>
                                    <td className="tr10 td28"><p className="p0 ft10">&nbsp;</p></td>
                                    <td className="tr10 td37"><p className="p0 ft10">&nbsp;</p></td>
                                    <td className="tr10 td21"><p className="p0 ft10">&nbsp;</p></td>
                                    <td className="tr10 td29"><p className="p0 ft10">&nbsp;</p></td>
                                    <td className="tr10 td38"><p className="p0 ft10">&nbsp;</p></td>
                                    <td className="tr10 td30"><p className="p0 ft10">&nbsp;</p></td>
                                    <td className="tr10 td39"><p className="p0 ft10">&nbsp;</p></td>
                                </tr>
                               <tr>
                                    <td className="tr12 td32"><p className="p0 ft2">&nbsp;</p></td>
                                    <td className="tr12 td10"><p className="p2 ft3">Written</p></td>
                                    <td className="tr12 td33"><p className="p0 ft2">&nbsp;</p></td>
                                    <td className="tr12 td34"><p className="p0 ft2">&nbsp;</p></td>
                                    <td className="tr12 td40"><p className="p0 ft2">&nbsp;</p></td>
                                    <td className="tr12 td26"><p className="p11 ft4">{row._written}</p></td>
                                    <td className="tr12 td41"><p className="p0 ft2">&nbsp;</p></td>
                                    <td className="tr12 td27"><p className="p12 ft3">{row._written_max}</p></td>
                                    <td className="tr12 td36"><p className="p0 ft2">&nbsp;</p></td>
                                </tr>
                                <tr>
                                    <td className="tr11 td32"><p className="p0 ft11">&nbsp;</p></td>
                                    <td className="tr5 td18"><p className="p0 ft6">&nbsp;</p></td>
                                    <td className="tr5 td28"><p className="p0 ft6">&nbsp;</p></td>
                                    <td className="tr5 td37"><p className="p0 ft6">&nbsp;</p></td>
                                    <td className="tr5 td21"><p className="p0 ft6">&nbsp;</p></td>
                                    <td className="tr5 td29"><p className="p0 ft6">&nbsp;</p></td>
                                    <td className="tr5 td38"><p className="p0 ft6">&nbsp;</p></td>
                                    <td className="tr5 td30"><p className="p0 ft6">&nbsp;</p></td>
                                    <td className="tr5 td39"><p className="p0 ft6">&nbsp;</p></td>
                                </tr>
                                <tr>
                                    <td className="tr6 td32"><p className="p0 ft2">&nbsp;</p></td>
                                    <td className="tr6 td10"><p className="p2 ft3">Submission</p></td>
                                    <td className="tr6 td33"><p className="p0 ft2">&nbsp;</p></td>
                                    <td className="tr6 td34"><p className="p0 ft2">&nbsp;</p></td>
                                    <td className="tr6 td40"><p className="p0 ft2">&nbsp;</p></td>
                                    <td className="tr6 td26"><p className="p11 ft4">{row._submission}</p></td>
                                    <td className="tr6 td41"><p className="p0 ft2">&nbsp;</p></td>
                                    <td className="tr6 td27"><p className="p12 ft3">{row._submission_max}</p></td>
                                    <td className="tr6 td36"><p className="p0 ft2">&nbsp;</p></td>
                                </tr>
                                <tr>
                                    <td className="tr5 td32"><p className="p0 ft6">&nbsp;</p></td>
                                    <td className="tr4 td18"><p className="p0 ft5">&nbsp;</p></td>
                                    <td className="tr4 td28"><p className="p0 ft5">&nbsp;</p></td>
                                    <td className="tr4 td37"><p className="p0 ft5">&nbsp;</p></td>
                                    <td className="tr4 td21"><p className="p0 ft5">&nbsp;</p></td>
                                    <td className="tr4 td29"><p className="p0 ft5">&nbsp;</p></td>
                                    <td className="tr4 td38"><p className="p0 ft5">&nbsp;</p></td>
                                    <td className="tr4 td30"><p className="p0 ft5">&nbsp;</p></td>
                                    <td className="tr4 td39"><p className="p0 ft5">&nbsp;</p></td>
                                </tr>
                                <tr>
                                    <td className="tr6 td32"><p className="p0 ft2">&nbsp;</p></td>
                                    <td className="tr6 td10"><p className="p2 ft3">Practical</p></td>
                                    <td className="tr6 td33"><p className="p0 ft2">&nbsp;</p></td>
                                    <td className="tr6 td34"><p className="p0 ft2">&nbsp;</p></td>
                                    <td className="tr6 td40"><p className="p0 ft2">&nbsp;</p></td>
                                    <td className="tr6 td26"><p className="p11 ft4">{row._practical}</p></td>
                                    <td className="tr6 td41"><p className="p0 ft2">&nbsp;</p></td>
                                    <td className="tr6 td27"><p className="p12 ft3">{row._practical_max}</p></td>
                                    <td className="tr6 td36"><p className="p0 ft2">&nbsp;</p></td>
                                </tr>
                                <tr>
                                    <td className="tr5 td32"><p className="p0 ft6">&nbsp;</p></td>
                                    <td className="tr4 td18"><p className="p0 ft5">&nbsp;</p></td>
                                    <td className="tr4 td28"><p className="p0 ft5">&nbsp;</p></td>
                                    <td className="tr4 td37"><p className="p0 ft5">&nbsp;</p></td>
                                    <td className="tr4 td21"><p className="p0 ft5">&nbsp;</p></td>
                                    <td className="tr4 td29"><p className="p0 ft5">&nbsp;</p></td>
                                    <td className="tr4 td38"><p className="p0 ft5">&nbsp;</p></td>
                                    <td className="tr4 td30"><p className="p0 ft5">&nbsp;</p></td>
                                    <td className="tr4 td39"><p className="p0 ft5">&nbsp;</p></td>
                                </tr>
								 <tr>
                                    <td className="tr6 td32"><p className="p0 ft2">&nbsp;</p></td>
                                    <td className="tr6 td10"><p className="p2 ft3">General</p></td>
                                    <td className="tr6 td33"><p className="p0 ft2">&nbsp;</p></td>
                                    <td className="tr6 td34"><p className="p0 ft2">&nbsp;</p></td>
                                    <td className="tr6 td40"><p className="p0 ft2">&nbsp;</p></td>
                                    <td className="tr6 td26"><p className="p11 ft4">{row._general}</p></td>
                                    <td className="tr6 td41"><p className="p0 ft2">&nbsp;</p></td>
                                    <td className="tr6 td27"><p className="p12 ft3">{row._general_max}</p></td>
                                    <td className="tr6 td36"><p className="p0 ft2">&nbsp;</p></td>
                                </tr>
                                <tr>
                                    <td className="tr4 td32"><p className="p0 ft5">&nbsp;</p></td>
                                    <td className="tr13 td18"><p className="p0 ft12">&nbsp;</p></td>
                                    <td className="tr13 td28"><p className="p0 ft12">&nbsp;</p></td>
                                    <td className="tr13 td37"><p className="p0 ft12">&nbsp;</p></td>
                                    <td className="tr13 td21"><p className="p0 ft12">&nbsp;</p></td>
                                    <td className="tr13 td29"><p className="p0 ft12">&nbsp;</p></td>
                                    <td className="tr13 td38"><p className="p0 ft12">&nbsp;</p></td>
                                    <td className="tr13 td30"><p className="p0 ft12">&nbsp;</p></td>
                                    <td className="tr13 td39"><p className="p0 ft12">&nbsp;</p></td>
                                </tr>
                                <tr>
                                    <td className="tr12 td32"><p className="p0 ft2">&nbsp;</p></td>
                                    <td className="tr12 td10"><p className="p13 ft8">Total</p></td>
                                    <td className="tr12 td33"><p className="p0 ft2">&nbsp;</p></td>
                                    <td className="tr12 td34"><p className="p0 ft2">&nbsp;</p></td>
                                    <td className="tr12 td40"><p className="p0 ft2">&nbsp;</p></td>
                                    <td className="tr12 td26"><p className="p11 ft1">{row._totalObtained}</p></td>
                                    <td className="tr12 td41"><p className="p0 ft2">&nbsp;</p></td>
                                    <td className="tr12 td27"><p className="p12 ft8">{row._total}</p></td>
                                    <td className="tr12 td36"><p className="p0 ft2">&nbsp;</p></td>
                                </tr>
                                <tr>
                                    <td className="tr4 td32"><p className="p0 ft5">&nbsp;</p></td>
                                    <td className="tr13 td18"><p className="p0 ft12">&nbsp;</p></td>
                                    <td className="tr13 td28"><p className="p0 ft12">&nbsp;</p></td>
                                    <td className="tr13 td37"><p className="p0 ft12">&nbsp;</p></td>
                                    <td className="tr13 td21"><p className="p0 ft12">&nbsp;</p></td>
                                    <td className="tr13 td29"><p className="p0 ft12">&nbsp;</p></td>
                                    <td className="tr13 td38"><p className="p0 ft12">&nbsp;</p></td>
                                    <td className="tr13 td30"><p className="p0 ft12">&nbsp;</p></td>
                                    <td className="tr13 td39"><p className="p0 ft12">&nbsp;</p></td>
                                </tr>
                            </tbody>
                        </table>
                        <table cellPadding={0} cellSpacing={0} className="t1">
                            <tbody>

                                <tr>
                                    <td className="tr2 td42"><p className="p0 ft8">Result: {row._result}</p></td>
                                    <td className="tr2 td43"><p className="p0 ft8">Percentage: {row._percent}%</p></td>
                                    <td className="tr2 td44"><p className="p0 ft8">Grade: {row._grade}</p></td>
                                </tr>
                            </tbody>
                        </table>
                        <table cellPadding={0} cellSpacing={0} className="t2">
                            <tbody>

                                <tr>
                                    <td className="tr2 td45"><p className="p0 ft3">Date: {state.date}</p></td>
                                    <td className="tr2 td46"><p className="p0 ft3">Signature Of The Principal</p></td>
                                    <td className="tr2 td47"><p className="p0 ft3">Faculty Incharge</p></td>
                                    <td className="tr2 td48"><p className="p0 ft4">College Seal</p></td>
                                </tr>
                            </tbody>
                        </table>
                        <p className="p14 ft13">Note: * indicates current appearance, + indicates grace mark, RR = Reserved, AB = Absent, NP = Not Permitted, F = Fail.</p>
                    </div>
                )
            }
        </Fragment>
    }
}

export default Marksheet9;