import React, { Fragment } from 'react';
import './style.css';
import {getVersion2Data} from '../../util';

class Marksheet2 extends React.Component {
    render() {
        return <Content {...this.props} />
    }
}




const Content = (props) => {
    const state = getVersion2Data(props.state, props.grades);
    
    return <Fragment>
        {
            state.rows.map((row) =>

                <div className="page_1" key={row.id}>
                    <div className="id1_1">
                        <p className="p0 ft0">Statement of Marks</p>
                        <p className="p1 ft1">{state.examName}</p>
                        <p className="p2 ft1">Examination held in {state.examYear}</p>
                        <table cellPadding={0} cellSpacing={0} className="t0">
                            <tbody>
                                <tr>
                                    <td colSpan={5} className="tr0 td0">
                                        <p className="p3 ft2">NAME OF THE STUDENT: {row.name}</p>
                                    </td>
                                    <td className="tr0 td1">
                                        <p className="p3 ft3">&nbsp;</p>
                                    </td>
                                    <td className="tr0 td2">
                                        <p className="p3 ft3">&nbsp;</p>
                                    </td>
                                    <td className="tr0 td3">
                                        <p className="p3 ft3">&nbsp;</p>
                                    </td>
                                    <td className="tr0 td4">
                                        <p className="p3 ft3">&nbsp;</p>
                                    </td>
                                    <td className="tr0 td5">
                                        <p className="p3 ft3">&nbsp;</p>
                                    </td>
                                    <td colSpan={2} className="tr0 td6">
                                        <p className="p4 ft4">ROLL NO: {row.rollNumber}</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td className="tr1 td7">
                                        <p className="p5 ft4">Paper Code</p>
                                    </td>
                                    <td className="tr1 td8">
                                        <p className="p3 ft3">&nbsp;</p>
                                    </td>
                                    <td className="tr1 td9">
                                        <p className="p3 ft3">&nbsp;</p>
                                    </td>
                                    <td colSpan={2} className="tr1 td10">
                                        <p className="p6 ft5">Paper Name</p>
                                    </td>
                                    <td className="tr1 td11">
                                        <p className="p7 ft5">Credits</p>
                                    </td>
                                    <td className="tr1 td12">
                                        <p className="p3 ft3">&nbsp;</p>
                                    </td>
                                    <td colSpan={2} className="tr1 td13">
                                        <p className="p8 ft5">Internal Marks</p>
                                    </td>
                                    <td className="tr1 td14">
                                        <p className="p9 ft4">External Marks</p>
                                    </td>
                                    <td className="tr1 td15">
                                        <p className="p10 ft5">Total Marks</p>
                                    </td>
                                    <td className="tr1 td16">
                                        <p className="p11 ft4">Grade</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td className="tr2 td17">
                                        <p className="p3 ft6">&nbsp;</p>
                                    </td>
                                    <td className="tr2 td18">
                                        <p className="p3 ft6">&nbsp;</p>
                                    </td>
                                    <td colSpan={2} className="tr2 td19">
                                        <p className="p3 ft6">&nbsp;</p>
                                    </td>
                                    <td className="tr2 td20">
                                        <p className="p3 ft6">&nbsp;</p>
                                    </td>
                                    <td className="tr2 td1">
                                        <p className="p3 ft6">&nbsp;</p>
                                    </td>
                                    <td className="tr2 td21">
                                        <p className="p3 ft6">&nbsp;</p>
                                    </td>
                                    <td className="tr2 td3">
                                        <p className="p3 ft6">&nbsp;</p>
                                    </td>
                                    <td className="tr2 td22">
                                        <p className="p3 ft6">&nbsp;</p>
                                    </td>
                                    <td className="tr2 td23">
                                        <p className="p3 ft6">&nbsp;</p>
                                    </td>
                                    <td className="tr2 td24">
                                        <p className="p3 ft6">&nbsp;</p>
                                    </td>
                                    <td className="tr2 td25">
                                        <p className="p3 ft6">&nbsp;</p>
                                    </td>
                                </tr>
                                {
                                    row.papers.map((paper) =>
                                        <Fragment key={paper.code}>
                                            <tr>
                                                <td className="tr3 td7">
                                                    <p className="p5 ft7">{paper.code}</p>
                                                </td>
                                                <td colSpan={4} className="tr3 td26">
                                                    <p className="p12 ft8">{paper.paperTitle}</p>
                                                </td>
                                                <td className="tr3 td11">
                                                    <p className="p13 ft8">{paper.credits}</p>
                                                </td>
                                                <td className="tr3 td12">
                                                    <p className="p3 ft3">&nbsp;</p>
                                                </td>
                                                <td className="tr3 td27">
                                                    <p className="p14 ft7">{isNaN(paper._internal) ? paper._internal : `${paper._internal} / ${paper._internal_max}` } </p>
                                                </td>
                                                <td className="tr3 td28">
                                                    <p className="p3 ft3">&nbsp;</p>
                                                </td>
                                                <td className="tr3 td14">
                                                    <p className="p9 ft7">{isNaN(paper._external) ? paper._external : (paper._grace ? `${paper._external} + ${paper._grace} / ${paper._external_max}` : `${paper._external} / ${paper._external_max}`) }</p>
                                                </td>
                                                <td className="tr3 td15">
                                                    <p className="p15 ft7">{paper._totalObtained} / {paper._total}</p>
                                                </td>
                                                <td className="tr3 td16">
                                                    <p className="p11 ft8">{paper._grade}</p>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td className="tr2 td17">
                                                    <p className="p3 ft6">&nbsp;</p>
                                                </td>
                                                <td className="tr2 td18">
                                                    <p className="p3 ft6">&nbsp;</p>
                                                </td>
                                                <td colSpan={2} className="tr2 td19">
                                                    <p className="p3 ft6">&nbsp;</p>
                                                </td>
                                                <td className="tr2 td20">
                                                    <p className="p3 ft6">&nbsp;</p>
                                                </td>
                                                <td className="tr2 td1">
                                                    <p className="p3 ft6">&nbsp;</p>
                                                </td>
                                                <td className="tr2 td21">
                                                    <p className="p3 ft6">&nbsp;</p>
                                                </td>
                                                <td className="tr2 td3">
                                                    <p className="p3 ft6">&nbsp;</p>
                                                </td>
                                                <td className="tr2 td22">
                                                    <p className="p3 ft6">&nbsp;</p>
                                                </td>
                                                <td className="tr2 td23">
                                                    <p className="p3 ft6">&nbsp;</p>
                                                </td>
                                                <td className="tr2 td24">
                                                    <p className="p3 ft6">&nbsp;</p>
                                                </td>
                                                <td className="tr2 td25">
                                                    <p className="p3 ft6">&nbsp;</p>
                                                </td>
                                            </tr>
                                        </Fragment>
                                    )
                                }
                                <tr>
                                    <td className="tr4 td34">
                                        <p className="p3 ft3">&nbsp;</p>
                                    </td>
                                    <td className="tr4 td8">
                                        <p className="p3 ft3">&nbsp;</p>
                                    </td>
                                    <td className="tr4 td9">
                                        <p className="p3 ft3">&nbsp;</p>
                                    </td>
                                    <td colSpan={2} className="tr4 td10">
                                        <p className="p3 ft9">Total</p>
                                    </td>
                                    <td className="tr4 td11">
                                        <p className="p16 ft1">{row._credits}</p>
                                    </td>
                                    <td className="tr4 td12">
                                        <p className="p3 ft3">&nbsp;</p>
                                    </td>
                                    <td className="tr4 td27">
                                        <p className="p17 ft2">{row._internal_total_o}/{row._internal_total}</p>
                                    </td>
                                    <td className="tr4 td28">
                                        <p className="p3 ft3">&nbsp;</p>
                                    </td>
                                    <td className="tr4 td14">
                                        <p className="p9 ft1">{row._external_total_o}/{row._external_total}</p>
                                    </td>
                                    <td className="tr4 td15">
                                        <p className="p9 ft1">{row._grand_total}/{row._total}</p>
                                    </td>
                                    <td className="tr4 td16">
                                        <p className="p11 ft2">{row._grade}</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td className="tr5 td35">
                                        <p className="p3 ft10">&nbsp;</p>
                                    </td>
                                    <td className="tr5 td18">
                                        <p className="p3 ft10">&nbsp;</p>
                                    </td>
                                    <td className="tr5 td29">
                                        <p className="p3 ft10">&nbsp;</p>
                                    </td>
                                    <td className="tr5 td30">
                                        <p className="p3 ft10">&nbsp;</p>
                                    </td>
                                    <td className="tr5 td20">
                                        <p className="p3 ft10">&nbsp;</p>
                                    </td>
                                    <td colSpan={2} className="tr5 td36">
                                        <p className="p3 ft10">&nbsp;</p>
                                    </td>
                                    <td className="tr5 td3">
                                        <p className="p3 ft10">&nbsp;</p>
                                    </td>
                                    <td className="tr5 td22">
                                        <p className="p3 ft10">&nbsp;</p>
                                    </td>
                                    <td className="tr5 td23">
                                        <p className="p3 ft10">&nbsp;</p>
                                    </td>
                                    <td className="tr5 td24">
                                        <p className="p3 ft10">&nbsp;</p>
                                    </td>
                                    <td className="tr5 td25">
                                        <p className="p3 ft10">&nbsp;</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td colSpan={2} className="tr6 td37">
                                        <p className="p3 ft11">&nbsp;</p>
                                    </td>
                                    <td className="tr6 td29">
                                        <p className="p3 ft11">&nbsp;</p>
                                    </td>
                                    <td className="tr6 td30">
                                        <p className="p3 ft11">&nbsp;</p>
                                    </td>
                                    <td className="tr6 td38">
                                        <p className="p3 ft11">&nbsp;</p>
                                    </td>
                                    <td colSpan={3} className="tr6 td39">
                                        <p className="p3 ft11">&nbsp;</p>
                                    </td>
                                    <td className="tr6 td4">
                                        <p className="p3 ft11">&nbsp;</p>
                                    </td>
                                    <td colSpan={2} className="tr6 td40">
                                        <p className="p3 ft11">&nbsp;</p>
                                    </td>
                                    <td className="tr6 td41">
                                        <p className="p3 ft11">&nbsp;</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td colSpan={2} className="tr7 td42">
                                        <p className="p5 ft2">Grade</p>
                                    </td>
                                    <td className="tr7 td9">
                                        <p className="p3 ft3">&nbsp;</p>
                                    </td>
                                    <td className="tr7 td43">
                                        <p className="p18 ft2">Grand Total</p>
                                    </td>
                                    <td className="tr7 td44">
                                        <p className="p3 ft3">&nbsp;</p>
                                    </td>
                                    <td colSpan={3} className="tr7 td45">
                                        <p className="p19 ft2">Result</p>
                                    </td>
                                    <td className="tr7 td46">
                                        <p className="p3 ft3">&nbsp;</p>
                                    </td>
                                    <td colSpan={2} className="tr7 td47">
                                        <p className="p20 ft12">Percentage</p>
                                    </td>
                                    <td className="tr7 td16">
                                        <p className="p3 ft3">&nbsp;</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td className="tr2 td35">
                                        <p className="p3 ft6">&nbsp;</p>
                                    </td>
                                    <td className="tr2 td48">
                                        <p className="p3 ft6">&nbsp;</p>
                                    </td>
                                    <td className="tr2 td29">
                                        <p className="p3 ft6">&nbsp;</p>
                                    </td>
                                    <td className="tr2 td49">
                                        <p className="p3 ft6">&nbsp;</p>
                                    </td>
                                    <td className="tr2 td38">
                                        <p className="p3 ft6">&nbsp;</p>
                                    </td>
                                    <td className="tr2 td1">
                                        <p className="p3 ft6">&nbsp;</p>
                                    </td>
                                    <td colSpan={2} className="tr2 td50">
                                        <p className="p3 ft6">&nbsp;</p>
                                    </td>
                                    <td className="tr2 td4">
                                        <p className="p3 ft6">&nbsp;</p>
                                    </td>
                                    <td colSpan={2} className="tr2 td40">
                                        <p className="p3 ft6">&nbsp;</p>
                                    </td>
                                    <td className="tr2 td25">
                                        <p className="p3 ft6">&nbsp;</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td className="tr8 td34">
                                        <p className="p21 ft2">{row._grade}</p>
                                    </td>
                                    <td className="tr8 td51">
                                        <p className="p3 ft3">&nbsp;</p>
                                    </td>
                                    <td className="tr8 td9">
                                        <p className="p3 ft3">&nbsp;</p>
                                    </td>
                                    <td className="tr8 td43">
                                        <p className="p22 ft2">{row._grand_total} / {row._total}</p>
                                    </td>
                                    <td className="tr8 td44">
                                        <p className="p3 ft3">&nbsp;</p>
                                    </td>
                                    <td className="tr8 td11">
                                        <p className="p3 ft3">&nbsp;</p>
                                    </td>
                                    <td colSpan={2} className="tr8 td52">
                                        <p className="p23 ft12">{row._result}</p>
                                    </td>
                                    <td className="tr8 td46">
                                        <p className="p3 ft3">&nbsp;</p>
                                    </td>
                                    <td colSpan={2} className="tr8 td47">
                                        <p className="p24 ft2">{row._percent}%</p>
                                    </td>
                                    <td className="tr8 td16">
                                        <p className="p3 ft3">&nbsp;</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td className="tr2 td35">
                                        <p className="p3 ft6">&nbsp;</p>
                                    </td>
                                    <td className="tr2 td48">
                                        <p className="p3 ft6">&nbsp;</p>
                                    </td>
                                    <td className="tr2 td29">
                                        <p className="p3 ft6">&nbsp;</p>
                                    </td>
                                    <td className="tr2 td49">
                                        <p className="p3 ft6">&nbsp;</p>
                                    </td>
                                    <td className="tr2 td38">
                                        <p className="p3 ft6">&nbsp;</p>
                                    </td>
                                    <td className="tr2 td1">
                                        <p className="p3 ft6">&nbsp;</p>
                                    </td>
                                    <td className="tr2 td2">
                                        <p className="p3 ft6">&nbsp;</p>
                                    </td>
                                    <td className="tr2 td53">
                                        <p className="p3 ft6">&nbsp;</p>
                                    </td>
                                    <td className="tr2 td4">
                                        <p className="p3 ft6">&nbsp;</p>
                                    </td>
                                    <td className="tr2 td5">
                                        <p className="p3 ft6">&nbsp;</p>
                                    </td>
                                    <td className="tr2 td54">
                                        <p className="p3 ft6">&nbsp;</p>
                                    </td>
                                    <td className="tr2 td25">
                                        <p className="p3 ft6">&nbsp;</p>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <p className="p25 ft4">Place : Vile Parle (W), Mumbai</p>
                        <p className="p26 ft4">Date : {state.date}</p>
                        <p className="p27 ft5">Signature Of The Principal</p>
                    </div>
                    <div className="id1_2">
                        <p className="p28 ft13">Note: * indicates current appearance, + indicates grace mark, RR = Reserved, ABS = Absent, NA =
            Not Applicable, F = Fail.</p>
                    </div>
                </div>
            )}
    </Fragment>
}


export default Marksheet2;