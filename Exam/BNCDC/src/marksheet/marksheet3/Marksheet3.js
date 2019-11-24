import React, {Fragment} from 'react';
import './style.css';
import {getVersion3Data} from '../../util';

class Marksheet3 extends React.Component {
    render() {
        const state = getVersion3Data(this.props.state, this.props.grades);
        console.log(state);
        return  <Fragment> 
            {
                state.rows.map((row, i) => 
               
            <div className="page_1" key={i}>
            <div className="id1_1">
                <p className="p0 ft0 bold">Statement of Marks</p>
                <p className="p1 ft1 bold">{state.examName}</p>
                <p className="p2 ft1 bold">Examination held in {state.examYear}</p>
                <table cellPadding={0} cellSpacing={0} className="t0">
                <tbody>
                    <tr>
                        <td colSpan={3} className="tr0 td0"><p className="p3 ft2 bold">NAME OF THE STUDENT: { row.name }</p></td>
                        <td className="tr0 td1"><p className="p3 ft3">&nbsp;</p></td>
                        <td className="tr0 td2"><p className="p3 ft3">&nbsp;</p></td>
                        <td className="tr0 td3"><p className="p3 ft3">&nbsp;</p></td>
                        <td className="tr0 td4"><p className="p3 ft3">&nbsp;</p></td>
                        <td colSpan={2} className="tr0 td5"><p className="p4 ft4 bold">ROLL NO: {row.rollNumber}</p></td>
                    </tr>
                    <tr>
                        <td className="tr1 td6"><p className="p3 ft3">&nbsp;</p></td>
                        <td className="tr1 td7"><p className="p5 ft5 bold">Paper Name</p></td>
                        <td className="tr1 td8"><p className="p3 ft3">&nbsp;</p></td>
                        <td className="tr1 td9"><p className="p3 ft3">&nbsp;</p></td>
                        <td className="tr1 td10"><p className="p6 ft5 bold">Internal<br/>Marks</p></td>
                        <td className="tr1 td11"><p className="p7 ft5 bold">External<br/>Marks</p></td>
						<td className="tr1 td11"><p className="p7 ft5 bold">Academic<br/>Performance</p></td>
                        <td className="tr1 td12"><p className="p6 ft5 bold">Total Marks</p></td>
                        <td className="tr1 td13"><p className="p8 ft5 bold">Grade</p></td>
                    </tr>
                    <tr>
                        <td className="tr2 td14"><p className="p3 ft3">&nbsp;</p></td>
                        <td className="tr2 td15"><p className="p3 ft3">&nbsp;</p></td>
                        <td className="tr2 td16"><p className="p3 ft3">&nbsp;</p></td>
                        <td className="tr2 td1"><p className="p3 ft3">&nbsp;</p></td>
                        <td className="tr2 td17"><p className="p3 ft3">&nbsp;</p></td>
                        <td className="tr2 td17"><p className="p3 ft3">&nbsp;</p></td>
                        <td className="tr2 td18"><p className="p3 ft3">&nbsp;</p></td>
                        <td className="tr2 td19"><p className="p3 ft3">&nbsp;</p></td>
                        <td className="tr2 td20"><p className="p3 ft3">&nbsp;</p></td>
                    </tr>
                    {
                        row.papers.map((paper, paperindex) => 
                        <Fragment  key={paperindex}>
                        <tr>
                        <td colSpan={2} className="tr3 td21"><p className="p9 ft5">{paper.paperTitle}</p></td>
                        <td className="tr3 td8"><p className="p3 ft3">&nbsp;</p></td>
                        <td colSpan={2} className="tr3 td22"><p className="p8 ft5">{isNaN(paper._internal) ? paper._internal : `${paper._internal} / ${paper._internal_max}` }</p></td>
                        <td className="tr3 td11"><p className="p8 ft5">{isNaN(paper._external) ? paper._external : (paper._grace ? `${paper._external} + ${paper._grace} / ${paper._external_max}` : `${paper._external} / ${paper._external_max}`) }</p></td>
						<td className="tr3 td22"><p className="p8 ft5">{isNaN(paper._academic) ? paper._academic : `${paper._academic} / ${paper._academic_max}` }</p></td>
                        <td className="tr3 td12"><p className="p7 ft4">{paper._totalObtained} / {paper._total}</p></td>
                        <td className="tr3 td13"><p className="p8 ft5">{paper._grade}</p></td>
                    </tr>
                      <tr>
                      <td className="tr2 td14"><p className="p3 ft3">&nbsp;</p></td>
                      <td colSpan={2} className="tr2 td23"><p className="p3 ft3">&nbsp;</p></td>
                      <td className="tr2 td1"><p className="p3 ft3">&nbsp;</p></td>
                      <td className="tr2 td17"><p className="p3 ft3">&nbsp;</p></td>
                      <td className="tr2 td17"><p className="p3 ft3">&nbsp;</p></td>
                      <td className="tr2 td18"><p className="p3 ft3">&nbsp;</p></td>
                      <td className="tr2 td19"><p className="p3 ft3">&nbsp;</p></td>
                      <td className="tr2 td20"><p className="p3 ft3">&nbsp;</p></td>
                  </tr>
                  </Fragment>
                        )
                    }
                   
                  
                  
                    <tr>
                        <td className="tr4 td6"><p className="p3 ft3">&nbsp;</p></td>
                        <td className="tr4 td7"><p className="p11 ft6 bold">Total</p></td>
                        <td className="tr4 td8"><p className="p3 ft3">&nbsp;</p></td>
                        <td colSpan={2} className="tr4 td22"><p className="p7 ft2 bold">{row._internal_total_o}</p></td>
                        <td className="tr4 td11"><p className="p7 ft2 bold">{row._external_total_o}</p></td>
						<td className="tr4 td11"><p className="p7 ft2 bold">{row._academic_total_o}</p></td>
                        <td className="tr4 td12"><p className="p7 ft2 bold">{row._grand_total} / {row._total}</p></td>
                        <td className="tr4 td13"><p className="p8 ft2 bold">{row._grade}</p></td>
                    </tr>
                    <tr>
                        <td className="tr5 td14"><p className="p3 ft7">&nbsp;</p></td>
                        <td className="tr5 td15"><p className="p3 ft7">&nbsp;</p></td>
                        <td className="tr5 td16"><p className="p3 ft7">&nbsp;</p></td>
                        <td className="tr5 td1"><p className="p3 ft7">&nbsp;</p></td>
                        <td className="tr5 td17"><p className="p3 ft7">&nbsp;</p></td>
                        <td className="tr5 td3"><p className="p3 ft7">&nbsp;</p></td>
                        <td className="tr5 td18"><p className="p3 ft7">&nbsp;</p></td>
                        <td className="tr5 td19"><p className="p3 ft7">&nbsp;</p></td>
                        <td className="tr5 td20"><p className="p3 ft7">&nbsp;</p></td>
                    </tr>
                    <tr>
                        <td className="tr6 td27"><p className="p3 ft8">&nbsp;</p></td>
                        <td className="tr6 td15"><p className="p3 ft8">&nbsp;</p></td>
                        <td className="tr6 td28"><p className="p3 ft8">&nbsp;</p></td>
                        <td className="tr6 td1"><p className="p3 ft8">&nbsp;</p></td>
                        <td className="tr6 td2"><p className="p3 ft8">&nbsp;</p></td>
                        <td className="tr6 td3"><p className="p3 ft8">&nbsp;</p></td>
                        <td className="tr6 td4"><p className="p3 ft8">&nbsp;</p></td>
                        <td className="tr6 td29"><p className="p3 ft8">&nbsp;</p></td>
                        <td className="tr6 td30"><p className="p3 ft8">&nbsp;</p></td>
                    </tr>
                    <tr>
                        <td className="tr7 td31"><p className="p12 ft2 bold">Grade</p></td>
                        <td className="tr7 td7"><p className="p3 ft3">&nbsp;</p></td>
                        <td className="tr7 td32"><p className="p13 ft2 bold">Grand Total</p></td>
                        <td className="tr7 td33"><p className="p3 ft3">&nbsp;</p></td>
                        <td className="tr7 td34"><p className="p14 ft2 bold">Result</p></td>
                        <td className="tr7 td35"><p className="p3 ft3">&nbsp;</p></td>
                        <td className="tr7 td36"><p className="p3 ft3">&nbsp;</p></td>
                        <td className="tr7 td37"><p className="p15 ft2 bold">Percentage</p></td>
                        <td className="tr7 td13"><p className="p3 ft3">&nbsp;</p></td>
                    </tr>
                    <tr>
                        <td className="tr8 td38"><p className="p3 ft9">&nbsp;</p></td>
                        <td className="tr8 td15"><p className="p3 ft9">&nbsp;</p></td>
                        <td className="tr8 td28"><p className="p3 ft9">&nbsp;</p></td>
                        <td className="tr8 td39"><p className="p3 ft9">&nbsp;</p></td>
                        <td className="tr8 td2"><p className="p3 ft9">&nbsp;</p></td>
                        <td className="tr8 td40"><p className="p3 ft9">&nbsp;</p></td>
                        <td className="tr8 td4"><p className="p3 ft9">&nbsp;</p></td>
                        <td className="tr8 td29"><p className="p3 ft9">&nbsp;</p></td>
                        <td className="tr8 td20"><p className="p3 ft9">&nbsp;</p></td>
                    </tr>
                    <tr>
                        <td className="tr9 td31"><p className="p16 ft2 bold">{row._grade}</p></td>
                        <td className="tr9 td7"><p className="p3 ft3">&nbsp;</p></td>
                        <td className="tr9 td32"><p className="p17 ft2 bold">{row._grand_total} / {row._total}</p></td>
                        <td className="tr9 td33"><p className="p3 ft3">&nbsp;</p></td>
                        <td className="tr9 td34"><p className="p14 ft10 bold">{row._result}</p></td>
                        <td className="tr9 td35"><p className="p3 ft3">&nbsp;</p></td>
                        <td className="tr9 td36"><p className="p3 ft3">&nbsp;</p></td>
                        <td className="tr9 td37"><p className="p18 ft2 bold">{row._percent}</p></td>
                        <td className="tr9 td13"><p className="p3 ft3">&nbsp;</p></td>
                    </tr>
                    <tr>
                        <td className="tr8 td38"><p className="p3 ft9">&nbsp;</p></td>
                        <td className="tr8 td15"><p className="p3 ft9">&nbsp;</p></td>
                        <td className="tr8 td28"><p className="p3 ft9">&nbsp;</p></td>
                        <td className="tr8 td39"><p className="p3 ft9">&nbsp;</p></td>
                        <td className="tr8 td2"><p className="p3 ft9">&nbsp;</p></td>
                        <td className="tr8 td40"><p className="p3 ft9">&nbsp;</p></td>
                        <td className="tr8 td4"><p className="p3 ft9">&nbsp;</p></td>
                        <td className="tr8 td29"><p className="p3 ft9">&nbsp;</p></td>
                        <td className="tr8 td20"><p className="p3 ft9">&nbsp;</p></td>
                    </tr>
                    <tr>
                        <td colSpan={2} className="tr0 td41"><p className="p3 ft4 bold">Place : Vile Parle (W), Mumbai</p></td>
                        <td className="tr0 td32"><p className="p3 ft3">&nbsp;</p></td>
                        <td className="tr0 td9"><p className="p3 ft3">&nbsp;</p></td>
                        <td className="tr0 td34"><p className="p3 ft3">&nbsp;</p></td>
                        <td className="tr0 td25"><p className="p3 ft3">&nbsp;</p></td>
                        <td className="tr0 td36"><p className="p3 ft3">&nbsp;</p></td>
                        <td className="tr0 td37"><p className="p3 ft3">&nbsp;</p></td>
                        <td className="tr0 td42"><p className="p3 ft3">&nbsp;</p></td>
                    </tr>
                    <tr>
                        <td colSpan={2} className="tr2 td41"><p className="p3 ft4 bold">Date : {state.date}</p></td>
                        <td className="tr2 td32"><p className="p3 ft3">&nbsp;</p></td>
                        <td className="tr2 td9"><p className="p3 ft3">&nbsp;</p></td>
                        <td className="tr2 td34"><p className="p3 ft3">&nbsp;</p></td>
                        <td className="tr2 td25"><p className="p3 ft3">&nbsp;</p></td>
                        <td className="tr2 td36"><p className="p3 ft3">&nbsp;</p></td>
                        <td className="tr2 td37"><p className="p3 ft3">&nbsp;</p></td>
                        <td className="tr2 td42"><p className="p3 ft3">&nbsp;</p></td>
                    </tr>
                    <tr>
                        <td colSpan={3} className="tr10 td43"><p className="p19 ft5 bold">Signature Of The Principal</p></td>
                        <td className="tr10 td9"><p className="p3 ft3">&nbsp;</p></td>
                        <td className="tr10 td34"><p className="p3 ft3">&nbsp;</p></td>
                        <td className="tr10 td25"><p className="p3 ft3">&nbsp;</p></td>
                        <td colSpan={2} className="tr10 td44"><p className="p20 ft5 bold">Faculty Incharge</p></td>
                        <td className="tr10 td42"><p className="p3 ft3">&nbsp;</p></td>
                    </tr>
                    </tbody>
                </table>
            </div>
            <div className="id1_2">
                <p className="p21 ft11">Note: * indicates current appearance, + indicates grace mark, RR = Reserved, ABS = Absent, NA = Not Applicable, F = Fail.</p>
            </div>
        </div>
         )
        }
        </Fragment>
    }
}

export default Marksheet3;