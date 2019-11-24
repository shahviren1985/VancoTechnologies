import React, { Fragment } from 'react';
import { getVersion6Data } from '../../util';
import logo from '../../images/logo.png';
import './style.css';

class Marksheet6 extends React.Component {
    render() {
        const state = getVersion6Data(this.props.state, this.props.grades)
        return <Fragment>
            {
                state.rows.map((row, index) =>
                    <div className="page_1" key={index}>
                        <div className="header">
                            <div className="logo"><img src={logo} style={{ width: "100px", height: "80px" }} alt="LOGO" /></div>
                            <div className="college">
                                <h3><b>S.N.D.T. WOMEN'S UNIVERSITY</b></h3>
                                <h4>Sir Vithaldas Thackersey College of Home Science</h4>
                                <p>S.N.D.T. WOMEN'S UNIVERSITY, SVT College, Juhu Road, Santacruz (W), Mumbai - 400049.</p>
                                <h2>JUNIOR COLLEGE</h2>
                                <p>Certificate showing the number of marks obtained at F.Y.J.C. (Science) - 2018-19 exam held in Year 2019</p>
                            </div>
                        </div>
                        <div className="nameContainer">
                            <div className="part20 norightborder">Division / Roll No.</div>
                            <div className="part80 nobottomborder">Name of the Candidate: <b>{row.name}</b></div>
                            <div className="part20 notopborder norightborder">{JSON.parse(localStorage["user"]).division} / {row.rollNumber}</div>
                            <div className="part80 notopborder">&nbsp;</div>
                        </div>
                        <div className="Marksheet">
                            <div className="MarksHeader">
                                <div className="percent20 rightborder">&nbsp;<br />SUBJECT<br />&nbsp;</div>
                                <div className="percent10 rightborder">UT I<br />(A)<br />25</div>
                                <div className="percent10 rightborder">UT 2<br />(B)<br />25</div>
                                <div className="percent10 rightborder">TERM I<br />(C)<br />50</div>
                                <div className="percent10 rightborder">TERM II<br />(D)<br />100</div>
                                <div className="percent20 rightborder">TOTAL<br />(E)<br />A+B+C+D=200</div>
                                <div className="percent30" style={{width: "38%"}}>
                                    <div className="bottomborder" style={{ width: "100%" }}><span>AVERAGE MARKS <br /> (E/2)</span></div>
                                    <div style={{ width: "100%" }}><span className="percent30 rightborder  pad5">Max.</span><span className="percent30 rightborder  pad5">Min.</span><span className=" pad5">Obtained</span></div>
                                </div>
                            </div>
                            {
                                row.papers.map((paper, paperindex) => <Fragment key={paperindex}>
                                    <div className="MarksRow">
                                        <div className="percent20 rightborder left"><b>{paper.paperTitle == "PHYSICAL EDUCATION" ? "PHY. EDU." : paper.paperTitle.toUpperCase()}</b></div>
                                        <div className="percent10 rightborder"><b>{paper._unit1 == undefined ? "-" : paper._unit1}</b></div>
                                        <div className="percent10 rightborder"><b>{paper._unit2 == undefined ? "-" : paper._unit2}</b></div>
                                        <div className="percent10 rightborder"><b>{paper._term1 == undefined ? "-" : paper._term1}</b></div>
                                        <div className="percent10 rightborder"><b>{paper._term2 == undefined ? "-" : paper._term2}</b></div>
                                        <div className="percent20 rightborder"><b>{paper.paperTitle =="PHYSICAL EDUCATION" ? "-" : paper._obtained}</b></div>
                                        <div className="percent30"  style={{width: "38%"}}>
                                            <div className="percent30 rightborder pad5"><b>{paper.paperTitle =="PHYSICAL EDUCATION" ? "-" : paper._max}</b></div>
                                            <div className="percent30 rightborder pad5"><b>{paper.paperTitle =="PHYSICAL EDUCATION" ? "-" : paper._min}</b></div>
                                            <div className="percent40 pad5"><b>{paper._grace && row._result != "FAIL" ? `${(paper._totalObtained)} + ${paper._grace}` : paper.paperTitle =="PHYSICAL EDUCATION" ? paper.paperDetails[0].marksObtained : paper._totalObtained}</b></div>
                                        </div>
                                    </div>
                                </Fragment>)
                            }
							    <div className="MarksRow">
                                        <div className="percent20 rightborder left"><b>GRAND TOTAL</b></div>
                                        <div className="percent10 rightborder">&nbsp;</div>
                                        <div className="percent10 rightborder">&nbsp;</div>
                                        <div className="percent10 rightborder">&nbsp;</div>
                                        <div className="percent10 rightborder">&nbsp;</div>
                                        <div className="percent20 rightborder">&nbsp;</div>
                                        <div className="percent30"  style={{width: "38%"}}>
                                            <div className="percent30 rightborder pad5"><b>650</b></div>
                                            <div className="percent30 rightborder pad5">-</div>
                                            <div className="percent40 pad5"><b>{ row._totalGrace == "0" || row._result == "FAIL" ? row._grand_total : (row._grand_total + " + " + row._totalGrace) }</b></div>
                                        </div>
                                </div>
                        </div>
                        <div className="Footer">
                            <div className="FooterDetails">
                                <div className="Result">Result Grade: <span>{row._grade == "I" ? "FIRST CLASS" : row._grade == "II" ? "SECOND CLASS" : row._grade == "F" ? "FAIL" : row._result == "GRACE" ? "PASSES WITH GRACE" : row._grade }</span></div>
								<div className="Percent">Percentage: <span><b>{row._result == "FAIL" ? "-" : row._result == "GRACE" ? "-" : row._percent + "%"}</b></span></div>
								<br />
                                <div>Entered By: <span>&nbsp;</span></div><br />
                                <div>Place: <span>Mumbai</span></div><br />
                                <div>Date: <span>24-04-2019</span></div>
                            </div>
                            <div className="FooterSignature">
									<div>Class Teacher</div>
									<div>Supervisor</div>
									<div>Principal</div>
								</div>

                        </div>
                        <div className="FooterNote">
                            <div>Result Grade: 35.00 to 44.99% - PASS, 45.00 to 59.99% - II, 60.00 to 74.99% - I, 75.00% or more - I WITH DIST.
													AA:Absent, F:Failure, +:Grace Marks, @:Condomed As per Govt. Circular No. SB / BR High Sec./Exam/7.3.2007
							</div>
                        </div>
                    </div>
                )
            }

        </Fragment>
    }
}

export default Marksheet6;