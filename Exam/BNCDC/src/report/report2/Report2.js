import React, { Fragment } from 'react';
import './style.css';
import logo from '../../images/Logo_BNCDC.jpg';
import { getVersion2Data } from '../../util';
import chunk from 'lodash/chunk';

class Report2 extends React.Component {
  render() {
    const state = getVersion2Data(this.props.state, this.props.grades);
    const chunks = chunk(state.rows, 2);
    return <div id="MarksheetContainer" className="MarksheetContainer">
      {
        chunks.map((rows, index) =>
          <Fragment key={index}>
            <div id="HeaderDetails0" className="HeaderDetails">
              <div><img src={logo} className="Logo" alt="Logo" width="80" height="80" /></div>
              <div className="HeaderDetailsName">Dr. BHANUBEN NANAVATI CAREER DEVELOPMENT CENTRE</div>
              <div className="CollegeName">COLLEGE: SHREE CHANDULAL NANAVATI WOMEN'S INSTITUTE AND GIRL'S HIGH SCHOOL, MUMBAI - 400
            056.</div>
              <div className="Results">COLLEGE RESULT SHEET FOR <span id="CourseName">{state.examName}</span>,
            EXAMINATION HELD IN <span id="ExamYear">{state.examYear}</span>.</div>
            </div>
            {

              rows.map((row) => (
                <div key={row.id}>
                  <div id="StudentRecord">
                    <div className="sname"><span>NAME:</span>
                      <div id="StudentName">{row.name} </div>
                    </div>
                    <div className="sdetails"><span>ROLL NO:</span>
                      <div id="SeatNumber">{row.rollNumber}</div>
                    </div>
                  </div>
                  <div className="markswrapper">
                    <div className="studentmarkstable">
                      <table className="markstable">
                        <thead>
                          <tr>
                            <td width="5%;">Paper Code</td>
                            <td width="30%;">Paper Title</td>
                            <td width="5%;">Credits</td>
                            <td width="5%;">Internal Marks</td>
                            <td width="5%;">External Marks</td>
                            <td width="5%;">Total Marks</td>
                            <td width="5%;">Grade</td>
                          </tr>
                        </thead>
                        <tbody>
                          {
                            row.papers.map((paper, index) => 
                            <tr key={index}>
                            <td>{paper.code}</td>
                            <td style={{ textAlign: "left" }}>{paper.paperTitle}</td>
                            <td>{paper.credits}</td>
                            <td>{isNaN(paper._internal) ? paper._internal : `${paper._internal} / ${paper._internal_max}` }</td>
                            <td>{isNaN(paper._external) ? paper._external : (paper._grace ? `${paper._external} + ${paper._grace} / ${paper._external_max}` : `${paper._external} / ${paper._external_max}`) }</td>
                            <td>{paper._totalObtained} / {paper._total}</td>
                            <td>{paper._grade}</td>
                          </tr>
                            )
                          }
                          <tr className="TotalRecord">
                            <td colSpan="2">Total</td>
                            <td>{row._credits}</td>
                            <td>{row._internal_total_o}/{row._internal_total}</td>
                            <td>{row._external_total_o}/{row._external_total}</td>
                            <td>{row._grand_total}/{row._total}</td>
                            <td>{row._grade}</td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                    <div className="summarydiv">
                      <table id="ResultSummary">
                        <tbody>
                          <tr>
                            <td width="50%">GRADE</td>
                            <td>: {row._grade}</td>
                          </tr>
                          <tr>
                            <td width="50%">TOTAL MARKS</td>
                            <td>: {row._grand_total} / {row._total}</td>
                          </tr>
                          <tr>
                            <td width="50%">PERCENTAGE</td>
                            <td>:{row._percent}%</td>
                          </tr>
                          <tr>
                            <td width="50%">RESULT</td>
                            <td>: {row._result}</td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              ))
            }
            <table className="FooterDetails" id="FooterDetails">
              <tbody>
                <tr>
                  <td align="left" style={{ textAlign: "left" }} className="footerplace">PLACE </td>
                  <td align="left" style={{ textAlign: "left" }}>: &nbsp;<span id="Place">Vile Parle (W), Mumbai</span></td>
                  <td align="center" rowSpan="2">&nbsp;<br /> &nbsp;</td>
                  <td align="center" rowSpan="2"> PRINCIPAL <br /> DR. RAJSHREE TRIVEDI<br /></td>
                </tr>
                <tr>
                  <td align="left" style={{ textAlign: "left" }}>DATE </td>
                  <td align="left" style={{ textAlign: "left" }}>: &nbsp;<span id="MarkSheetDate">{state.date}</span></td>
                </tr>
                <tr>
                  <td colSpan="4" align="left" className="notes">Note: * indicates current appearance, +
                  indicates grace mark, RR = Reserved, ABS = Absent, NA = Not Applicable, F = Fail.</td>
                </tr>
              </tbody>
            </table>
            <footer></footer>
          </Fragment>
        )
      }
    </div>
  }
}


export default  Report2;