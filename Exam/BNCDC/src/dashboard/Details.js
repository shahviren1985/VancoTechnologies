import React, { Fragment } from 'react';
import classNames from 'classnames';

const Details = (props) => <section className="section">
    <div>
        <div className="has-text-centered p-15">
            <span className="subtitle"><strong>Student Details</strong></span>
            <button className="button is-link is-pulled-right" onClick={props.addRow}>&#10010;</button>
        </div>
        {
            props.marksheet.rows.length > 0 &&
            <Fragment>
                <div className="scroll-y">
                    <table className="table is-bordered is-fullwidth">
                        <thead>
                            <tr>
                                <th rowSpan="2" className="has-text-centered">Roll #</th>
                                <th rowSpan="2" className="has-text-centered">Full Name</th>
                                <Fragment>
                                    {
                                        props.papers.map((paper, index) => (
                                            <th colSpan={paper.paperDetails.length} className="has-text-centered no-wrap" key={index}>
                                                <span>{paper.paperTitle}</span>
                                            </th>
                                        ))
                                    }

                                </Fragment>
                                <th rowSpan="2">&nbsp;</th>
                            </tr>
                            <tr>
                                {
                                    props.papers.map((paper, index) => (
                                        <Fragment key={index}>
                                            {
                                                paper.paperDetails.map((detail, detailIndex) => <th key={detailIndex}  className="has-text-centered ">{detail.title}</th>)
                                            }
                                        </Fragment>
                                    ))
                                }
                            </tr>
                        </thead>
                        <tbody>
                            {
                                props.marksheet.rows.map((row, index) => (

                                    <tr key={row.id}>
                                        <td className="has-text-centered"><input type="text" className={classNames("input w-50", { "is-danger": props.errors[`rollNumber${index}`] })} name="rollNumber" value={row.rollNumber} onChange={(e) => props.onInfoChange(index, e)} /></td>
                                        <td className="has-text-centered"><input type="text" className={classNames("input w-200", { "is-danger": props.errors[`name${index}`] })} name="name" value={row.name} onChange={(e) => props.onInfoChange(index, e)} /></td>
                                        {
                                            row.papers.map((paper, paperIndex) => (
                                                <Fragment key={`${paper.code}_${paperIndex}`}>
                                                    {
                                                        paper.paperDetails.map((detail, detailIndex) => (
                                                            <td key={detailIndex} className="has-text-centered">
                                                                <input type="text" className={classNames("input w-70", { "is-danger": props.errors[`${detail.title}_${index}_${paperIndex}`] })} value={detail.marksObtained} name="marksObtained" onChange={(e) => props.onPaperInfoChange(index, paperIndex, detailIndex, e)} disabled={detail.isDisabled === true}/>
                                                            </td>))
                                                    }
                                                </Fragment>
                                            ))
                                        }
                                        <td>
                                            <button className="button  is-danger " onClick={() => props.removeRow(index)}>&#10006;</button>
                                        </td>
                                    </tr>
                                ))
                            }
                        </tbody>
                    </table>
                </div>
                <div className="mt-20">
                    <button className={classNames("button is-primary", { "is-loading": props.saveStudentInfoFlag })} onClick={props.saveStudentInfo}> Save Student Info </button> &nbsp;
                    <button className={classNames("button is-link", { "is-loading": props.saveMarksFlag })} onClick={props.saveMarks}> Save Marks </button>  &nbsp;
                    <button className="button is-success" onClick={props.printMarksheet}>Print Marksheet </button>  &nbsp;
                    <button className="button is-warning" onClick={props.printLedgerReport}> Print Ledger Report </button>  &nbsp;
                </div>
            </Fragment>
        }
    </div>
</section>


export default Details;