import React, { Fragment } from 'react';
import './style.css';
import logo from '../../images/Logo_BNCDC.jpg';
import { getVersion8Data } from '../../util';
import chunk from 'lodash/chunk';

class Report8 extends React.Component {
    render() {
        const state = getVersion8Data(this.props.state, this.props.grades);
        const chunks = chunk(state.rows, 2);
        return <Fragment>
            {
                chunks.map((chunk, i) => (
                    <div className="page_1" key={i}>
                        <div><img src={logo} className="Logo" alt="Logo" width="100" height="100" /></div>
                        <p className="p0 ft0">Dr. BHANUBEN NANAVATI CAREER DEVELOPMENT CENTER</p>
                        <p className="p1 ft1">(CONDUCTED BY : SHREE CHANDULAL NANAVATI WOMEN’S INSTITUTE & GIRLS’ HIGH SCHOOL)</p>
                        <p className="p2 ft2">VALLABHAI ROAD , VILE PARLE (WEST) MUMBAI – 400 056</p>
                        <hr className="line" />
                        <table cellPadding={0} cellSpacing={0} className="t0">
                            <tbody>
                                {
                                    chunk.map((row, index) => (
                                        <Fragment key={index}>
                                            <tr>
                                                <td className="tr0 td0"><p className="p3 ft3">&nbsp;</p></td>
                                                <td className="tr0 td1"><p className="p3 ft4">Name of Student : {row.name}</p></td>
                                                <td colSpan={4} className="tr0 td2"><p className="p4 ft4">Course Name : {state.examName} {state.examYear}</p></td>
                                            </tr>
                                            <tr>
                                                <td className="tr1 td0"><p className="p3 ft3">&nbsp;</p></td>
                                                <td className="tr2 td3"><p className="p3 ft3">&nbsp;</p></td>
                                                <td className="tr2 td4"><p className="p3 ft3">&nbsp;</p></td>
                                                <td className="tr2 td5"><p className="p3 ft3">&nbsp;</p></td>
                                                <td className="tr1 td6"><p className="p3 ft3">&nbsp;</p></td>
                                                <td className="tr1 td7"><p className="p3 ft3">&nbsp;</p></td>
                                            </tr>
                                            <tr>
                                                <td className="tr2 td8"><p className="p3 ft3">&nbsp;</p></td>
                                                <td className="tr2 td9"><p className="p5 ft4">Paper Title</p></td>
                                                <td className="tr2 td10"><p className="p6 ft4">Marks</p></td>
                                                <td className="tr2 td11"><p className="p7 ft5">Out Of</p></td>
                                                <td rowSpan={2} className="tr3 td6"><p className="p8 ft4">Total</p></td>
                                                <td rowSpan={2} className="tr3 td7"><p className="p9 ft4">: {row._totalObtained}</p></td>
                                            </tr>
                                            <tr>
                                                <td className="tr4 td8"><p className="p3 ft6">&nbsp;</p></td>
                                                <td className="tr4 td9"><p className="p3 ft6">&nbsp;</p></td>
                                                <td rowSpan={2} className="tr1 td12"><p className="p6 ft5">Obtained</p></td>
                                                <td className="tr4 td11"><p className="p3 ft6">&nbsp;</p></td>
                                            </tr>
                                            <tr>
                                                <td className="tr5 td8"><p className="p3 ft7">&nbsp;</p></td>
                                                <td className="tr6 td13"><p className="p3 ft8">&nbsp;</p></td>
                                                <td className="tr6 td14"><p className="p3 ft8">&nbsp;</p></td>
                                                <td rowSpan={2} className="tr1 td6"><p className="p8 ft4">Percentage</p></td>
                                                <td rowSpan={2} className="tr1 td7"><p className="p9 ft4">: {row._percent} %</p></td>
                                            </tr>
                                            <tr>
                                                <td className="tr8 td8"><p className="p3 ft11">&nbsp;</p></td>
                                                <td rowSpan={2} className="tr9 td9"><p className="p10 ft12">Internal</p></td>
                                                <td rowSpan={2} className="tr9 td10"><p className="p6 ft12">{row._internal}</p></td>
                                                <td rowSpan={2} className="tr9 td11"><p className="p6 ft12">{row._internal_max}</p></td>
                                            </tr>
											
                                            <tr>
                                                <td className="tr6 td8"><p className="p3 ft8">&nbsp;</p></td>
                                                <td rowSpan={3} className="tr1 td6"><p className="p8 ft4">Grade</p></td>
                                                <td rowSpan={3} className="tr1 td7"><p className="p9 ft4">: {row._grade}</p></td>
                                            </tr>
                                            <tr>
                                                <td className="tr8 td8"><p className="p3 ft11">&nbsp;</p></td>
                                                <td className="tr5 td13"><p className="p3 ft7">&nbsp;</p></td>
                                                <td className="tr5 td12"><p className="p3 ft7">&nbsp;</p></td>
                                                <td className="tr5 td14"><p className="p3 ft7">&nbsp;</p></td>
                                            </tr>
                                            <tr>
                                                <td className="tr7 td8"><p className="p3 ft9">&nbsp;</p></td>
                                                <td rowSpan={2} className="tr2 td9"><p className="p10 ft10">Practical</p></td>
                                                <td rowSpan={2} className="tr2 td10"><p className="p6 ft10">{row._practical}</p></td>
                                                <td rowSpan={2} className="tr2 td11"><p className="p6 ft10">{row._practical_max}</p></td>
                                            </tr>
											<tr>
                                                <td className="tr10 td8"><p className="p3 ft13">&nbsp;</p></td>
                                                <td rowSpan={2} className="tr9 td6"><p className="p8 ft14">&nbsp;</p></td>
                                                <td rowSpan={2} className="tr9 td7"><p className="p9 ft14">&nbsp;</p></td>
                                            </tr>
											 <tr>
                                                <td className="tr8 td8"><p className="p3 ft11">&nbsp;</p></td>
                                                <td className="tr5 td13"><p className="p3 ft7">&nbsp;</p></td>
                                                <td className="tr5 td12"><p className="p3 ft7">&nbsp;</p></td>
                                                <td className="tr5 td14"><p className="p3 ft7">&nbsp;</p></td>
                                            </tr>
											<tr>
                                                <td className="tr7 td8"><p className="p3 ft9">&nbsp;</p></td>
                                                <td rowSpan={2} className="tr2 td9"><p className="p10 ft10">External</p></td>
                                                <td rowSpan={2} className="tr2 td10"><p className="p6 ft10">{row._external}</p></td>
                                                <td rowSpan={2} className="tr2 td11"><p className="p6 ft10">{row._external_max}</p></td>
                                            </tr>
											
											<tr>
                                                <td className="tr10 td8"><p className="p3 ft13">&nbsp;</p></td>
                                                <td rowSpan={2} className="tr9 td6"><p className="p8 ft14">RESULT</p></td>
                                                <td rowSpan={2} className="tr9 td7"><p className="p9 ft14">: {row._result}</p></td>
                                            </tr>
                                            <tr>
                                                <td className="tr8 td8"><p className="p3 ft11">&nbsp;</p></td>
                                                <td className="tr5 td13"><p className="p3 ft7">&nbsp;</p></td>
                                                <td className="tr5 td12"><p className="p3 ft7">&nbsp;</p></td>
                                                <td className="tr5 td14"><p className="p3 ft7">&nbsp;</p></td>
                                            </tr>
                                            {
                                                index + 1 !== chunk.length &&
                                                <tr>
                                                    <td className="tr11 td15"><p className="p3 ft3">&nbsp;</p></td>
                                                    <td className="tr11 td3"><p className="p3 ft3">&nbsp;</p></td>
                                                    <td className="tr11 td4"><p className="p3 ft3">&nbsp;</p></td>
                                                    <td className="tr11 td5"><p className="p3 ft3">&nbsp;</p></td>
                                                    <td className="tr11 td16"><p className="p3 ft3">&nbsp;</p></td>
                                                    <td className="tr11 td17"><p className="p3 ft3">&nbsp;</p></td>
                                                </tr>
                                            }
                                        </Fragment>
                                    ))

                                }

                            </tbody>

                        </table>
                        <table cellPadding={0} cellSpacing={0} className="t1">
                            <tbody>
                                <tr>
                                    <td className="tr16 td18"><p className="p3 ft19">DATE</p></td>
                                    <td className="tr16 td19"><p className="p11 ft19">PRINCIPAL</p></td>
                                    <td className="tr16 td20"><p className="p3 ft19">COLLEGE SEAL</p></td>
                                    <td className="tr16 td21"><p className="p3 ft19">FACULTY INCHARGE</p></td>
                                </tr>
                                <tr>
                                    <td className="tr9 td18"><p className="p3 ft3">&nbsp;</p></td>
                                    <td className="tr9 td19"><p className="p12 ft19">DR. RAJSHREE TRIVEDI</p></td>
                                    <td className="tr9 td20"><p className="p3 ft3">&nbsp;</p></td>
                                    <td className="tr9 td21"><p className="p3 ft3">&nbsp;</p></td>
                                </tr>
                            </tbody>
                        </table>
                        <p className="p13 ft20">Note: + indicates grace mark, RR = Reserved, AB = Absent, NP = Not Permitted, F = Fail.</p>
                    </div>
                ))
            }
        </Fragment>
    }
}

export default Report8;