import React, { Fragment } from 'react';
import './style.css';
import logo from '../../images/logo.png';
import { getVersion6Data } from '../../util';
import chunk from 'lodash/chunk';

class Report6 extends React.Component {
    render() {
        const state = getVersion6Data(this.props.state, this.props.grades);
        const chunks = chunk(state.rows, 30);
        return <Fragment>
            {
                chunks.map((chunk, index) =>
                    <div className="page_1" key={index}>
                        <div>
                            <div className="main">
                                <div><img src={logo} className="Logo" alt="Logo" width="80" height="80" /></div>
                                <p className="p_0 f_t0">Sir Vithaldas Thackersey College of Home Science</p>
                                <p className="p_1 f_t1">S.N.D.T. WOMEN'S UNIVERSITY, SVT College, Juhu Road, Santacruz (W), Mumbai - 400049.</p>
                                <p className="p_2 f_t2">Consolidated Marksheet - Division - {JSON.parse(localStorage["user"]).division}</p>
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
						<table cellPadding={0} cellSpacing={0} className="t1">
                            <tbody>
                                <tr>
									<td className="rollNumber">Roll#</td>
									<td className="studentName">Student Name</td>
									<td>
										<table className="subjectName">
											<tbody><tr><td colSpan="6">English</td></tr>
											<tr><td>UT1</td><td>UT2</td><td>T1</td><td>T2</td><td>Tot.</td><td>Avg</td></tr></tbody>
										</table>
									</td>
									<td>
										<table className="subjectName">
											<tbody><tr><td colSpan="6">Language</td></tr>
											<tr><td>UT1</td><td>UT2</td><td>T1</td><td>T2</td><td>Tot.</td><td>Avg</td></tr></tbody>
										</table>
									</td>
									<td>
										<table className="subjectName">
											<tbody><tr><td colSpan="6">{ JSON.parse(localStorage["user"]).division == "D" ? "MAT/PSY/SOC" : "Sociology"}</td></tr>
											<tr><td>UT1</td><td>UT2</td><td>T1</td><td>T2</td><td>Tot.</td><td>Avg</td></tr></tbody>
										</table>
									</td>
									<td>
										<table className="subjectName">
											<tbody><tr><td colSpan="6">{ JSON.parse(localStorage["user"]).division == "D" ? "Physics" : "Psychology"}</td></tr>
											<tr><td>UT1</td><td>UT2</td><td>T1</td><td>T2</td><td>Tot.</td><td>Avg</td></tr></tbody>
										</table>
									</td>
									<td>
										<table className="subjectName">
											<tbody><tr><td colSpan="6">Chemistry</td></tr>
											<tr><td>UT1</td><td>UT2</td><td>T1</td><td>T2</td><td>Tot.</td><td>Avg</td></tr></tbody>
										</table>
									</td>
									<td>
										<table className="subjectName">
											<tbody>
											<tr><td colSpan="6">Biology</td></tr>
											<tr><td>UT1</td><td>UT2</td><td>T1</td><td>T2</td><td>Tot.</td><td>Avg</td></tr>
											</tbody>
										</table>
									</td>
									<td className="finalColumns">EVS</td>
									<td className="finalColumns">Gr. Tot.</td>
									<td className="finalColumns">P.T.</td>
									<td className="finalColumns">Result</td>
									<td className="finalColumns">Grade</td>
									<td className="finalColumns">%</td>
								</tr>
                        {
                            chunk.map((row, rowIndex) => 
                           <Fragment key={rowIndex}>
								
								<tr>
									<td className="rollNumber">{row.rollNumber}</td>
									<td className="studentName">{row.name}</td>
									<td className={rowIndex % 29 == 0 && rowIndex > 0 ? "lastRow": ""}>
										<div>{row.papers[0].paperDetails[0].marksObtained}</div>
										<div>{row.papers[0].paperDetails[1].marksObtained}</div>
										<div>{row.papers[0].paperDetails[2].marksObtained}</div>
										<div>{row.papers[0].paperDetails[3].marksObtained}</div>
										<div>{row.papers[0]._obtained}</div>
										<div className="averageMarks">{row.papers[0]._totalObtained}</div>
									</td>
									<td className={rowIndex % 29 == 0 && rowIndex > 0 ? "lastRow": ""}>
										<div>{row.papers[1].paperDetails[JSON.parse(localStorage["user"]).division == "A" ? 0 : 1].marksObtained}</div>
										<div>{row.papers[1].paperDetails[JSON.parse(localStorage["user"]).division == "A" ? 1 : 2].marksObtained}</div>
										<div>{row.papers[1].paperDetails[JSON.parse(localStorage["user"]).division == "A" ? 2 : 3].marksObtained}</div>
										<div>{row.papers[1].paperDetails[JSON.parse(localStorage["user"]).division == "A" ? 3 : 4].marksObtained}</div>
										<div>{row.papers[1]._obtained}</div>
										<div className="averageMarks">{row.papers[1]._totalObtained}</div>
									</td>
									<td className={rowIndex % 29 == 0 && rowIndex > 0 ? "lastRow": ""}>
										<div>{row.papers[2].paperDetails[JSON.parse(localStorage["user"]).division == "D" ? 1 : 0].marksObtained}</div>
										<div>{row.papers[2].paperDetails[JSON.parse(localStorage["user"]).division == "D" ? 2 : 1].marksObtained}</div>
										<div>{row.papers[2].paperDetails[JSON.parse(localStorage["user"]).division == "D" ? 3 : 2].marksObtained}</div>
										<div>{row.papers[2].paperDetails[JSON.parse(localStorage["user"]).division == "D" ? 4 : 3].marksObtained}</div>
										<div>{row.papers[2]._obtained}</div>
										<div className="averageMarks">{row.papers[2]._totalObtained}</div>
									</td>
									<td className={rowIndex % 29 == 0 && rowIndex > 0 ? "lastRow": ""}>
										<div>{row.papers[3].paperDetails[0].marksObtained}</div>
										<div>{row.papers[3].paperDetails[1].marksObtained}</div>
										<div>{row.papers[3].paperDetails[2].marksObtained}</div>
										<div>{row.papers[3].paperDetails[3].marksObtained}</div>
										<div>{row.papers[3]._obtained}</div>
										<div className="averageMarks">{row.papers[3]._totalObtained}</div>
									</td>
									<td className={rowIndex % 29 == 0 && rowIndex > 0 ? "lastRow": ""}>
										<div>{row.papers[4].paperDetails[0].marksObtained}</div>
										<div>{row.papers[4].paperDetails[1].marksObtained}</div>
										<div>{row.papers[4].paperDetails[2].marksObtained}</div>
										<div>{row.papers[4].paperDetails[3].marksObtained}</div>
										<div>{row.papers[4]._obtained}</div>
										<div className="averageMarks">{row.papers[4]._totalObtained}</div>
									</td>
									<td className={rowIndex % 29 == 0 && rowIndex > 0 ? "lastRow": ""}>
										<div>{row.papers[5].paperDetails[0].marksObtained}</div>
										<div>{row.papers[5].paperDetails[1].marksObtained}</div>
										<div>{row.papers[5].paperDetails[2].marksObtained}</div>
										<div>{row.papers[5].paperDetails[3].marksObtained}</div>
										<div>{row.papers[5]._obtained}</div>
										<div className="averageMarks">{row.papers[5]._totalObtained}</div>
									</td>
									<td className="finalColumns">
										{row.papers[6]._obtained}
									</td>
									<td className="finalColumns totalMarks">
										{ (row._totalGrace=="0") ? row._grand_total : row._result == "FAIL" ? row._grand_total : (row._grand_total + "+" + row._totalGrace)}	
										
									</td>
									<td className="finalColumns">
										{row.papers[7].paperDetails[0].marksObtained}
									</td>
									<td className="finalColumns">
										<span className={row._result=="FAIL"?"fail":""}>
										{row._result}
										</span>
									</td>
									<td className="finalColumns">
										{row._grade == "I WITH DISTINCTION" ? "DIST" : row._grade}
									</td>
									<td className="finalColumns">
										{row._percent}
									</td>
								</tr>
                        </Fragment>
                        )}
						</tbody>
                        </table>
                    
                       
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
                        <p className="p10 ft15">Note: * indicates current appearance, + indicates grace mark, RR = Reserved, ABS = Absent, NP = Not Permitted, F = Fail.</p>
                    </div>
                )
            }
        </Fragment>
    }
}

export default Report6;