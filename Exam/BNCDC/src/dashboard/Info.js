import React from 'react';
import DatePicker from 'react-datepicker';
import "react-datepicker/dist/react-datepicker.css";
import classNames from 'classnames';


const Info = (props) => {
const date = props.marksheet.date ? new Date(props.marksheet.date) : props.marksheet.date;
return <section className="section ">
    <div className="container">
        <div className="columns ">
            <div className="column is-45">
                <div className="field is-horizontal">
                    <div className="field-label is-normal">
                        <label className="label">Course</label>
                    </div>
                    <div className="field-body">
                        <div className="field">
                            <div className="control">
                                <input className="input" readOnly value={props.course.title} />
                            </div>
                        </div>
                    </div>
                </div>
                <div className="field is-horizontal">
                    <div className="field-label is-normal">
                        <label className="label">Sub Course</label>
                    </div>
                    <div className="field-body">
                        <div className="field">
                            <div className="control">
                                <div className="select is-fullwidth">
                                    <select onChange={props.handleYearChange} value={props.marksheet.year}>
                                        <option>Select dropdown</option>
                                        {
                                            props.years.map((year) => (
                                                <option key={year.code} value={year.code}>{year.value}</option>
                                            ))
                                        }
                                    </select>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
            <div className="column is-45">

                <div className="field is-horizontal">
                    <div className="field-label is-normal">
                        <label className="label">Exam Name</label>
                    </div>
                    <div className="field-body">
                        <div className="field">
                            <div className="control">
                                <input className={classNames("input", { "is-danger": props.errors.examName })} placeholder="Exam Name" name="examName" value={props.marksheet.examName} onChange={props.onChange} />
                            </div>
                        </div>
                    </div>
                </div>
                <div className="field is-horizontal">
                    <div className="field-label is-normal">
                        <label className="label">Exam Year</label>
                    </div>
                    <div className="field-body">
                        <div className="field">
                            <div className="control">
                                <input className={classNames("input", { "is-danger": props.errors.examYear })} placeholder="Exam Year" name="examYear" value={props.marksheet.examYear} onChange={props.onChange} />
                            </div>
                        </div>
                    </div>
                </div>
                <div className="field is-horizontal">
                    <div className="field-label is-normal">
                        <label className="label">Date</label>
                    </div>
                    <div className="field-body">
                        <div className="field">
                            <div className="control is-fullwidth">
                                <DatePicker
                                    onChange={props.handleDateChange}
                                    selected={date}
                                    className={classNames("input", { "is-danger": props.errors.examYear })}
                                    dateFormat="dd/MM/yyyy"
                                />

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</section>
}

export default Info;