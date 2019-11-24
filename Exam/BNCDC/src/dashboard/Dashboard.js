import React, { Fragment } from 'react';
import { connect } from 'react-redux'
import { reset, setPapers, setApp } from '../store/actions/app';
import { removeUserFromLocalStorage } from '../util';
import './style.css';
import { getCourse } from '../store/actions/course';
import { getUserFromLocalStorage, getUniqueId, getFileName, getTime } from '../util';
import Info from './Info'
import Nav from './Nav';
import { setMarksheet, resetMarksheet, setRows } from '../store/actions/marksheet';
import Details from './Details';
import cloneDeep from 'lodash/cloneDeep'
import axios from 'axios';
import Swal from 'sweetalert2/dist/sweetalert2.js'
import 'sweetalert2/dist/sweetalert2.css'
import classNames from 'classnames';
import Marksheet from '../marksheet/Marksheet';
import Report from '../report/Report';

class Dashboard extends React.Component {
    constructor(props) {
        super(props);
        this.logout = this.logout.bind(this);
        this.onChange = this.onChange.bind(this);
        this.handleDateChange = this.handleDateChange.bind(this);
        this.handleYearChange = this.handleYearChange.bind(this);
        this.addRow = this.addRow.bind(this);
        this.removeRow = this.removeRow.bind(this);
        this.onInfoChange = this.onInfoChange.bind(this);
        this.onPaperInfoChange = this.onPaperInfoChange.bind(this);
        this.saveStudentInfo = this.saveStudentInfo.bind(this);
        this.saveMarks = this.saveMarks.bind(this);
        this.printMarksheet = this.printMarksheet.bind(this);
        this.printLedgerReport = this.printLedgerReport.bind(this);
        this.reload = this.reload.bind(this);
        this.state = {
            errors: {},
            saveStudentInfoFlag: false,
            saveMarksFlag: false,
            displayErrors: {
                grace: '',
                required: '',
                marks: '',
                rollNumber: '',
                examName: '',
                year: '',
                date: '',
            },
            view: 'default',
            apiUrl: '',
        };
        this.skipWords = ['ABS', 'NP', 'CC', 'NA'];
        this.backToDashboard = this.backToDashboard.bind(this);
    }

    componentDidMount() {
        const user = getUserFromLocalStorage();
        const code = user.courseCode;
        this.props.getCourse(code);
    }

    reload() {
        window.location.reload();
    }

    logout() {
        removeUserFromLocalStorage();
        this.props.reset();
        this.props.resetMarksheet();
    }

    onChange(e) {
        this.props.setMarksheet({ [e.target.name]: e.target.value });
    }

    handleDateChange(e) {
        this.props.setMarksheet({ date: e });
    }

    handleYearChange(e) {
        const { user } = this.props;
        this.props.resetMarksheet();
        const year = e.target.value;
        const course = this.props.course[year] || {};
        const papers = (course && course.papers) || [];
        this.props.setMarksheet({ year });
        this.props.setApp({ papers: [...papers], maxGrace: course.maxGrace });
        const code = user.courseCode;
        const fileName = getFileName(code, year);
        const apiUrl = `./api/${fileName}`;
        this.setState({apiUrl});
        axios.get(`${apiUrl}?nocache=${getTime()}`).then((res) => {
            this.props.setMarksheet(res.data);
        })
            .catch((error) => console.log(error));
    }

    addRow() {
        const { marksheet } = this.props;
        let { papers } = this.props;
        const row = {};
        let _papers = cloneDeep(papers);
        if (papers.length === 0) {
            return;
        }
        row.id = getUniqueId();
        row.rollNumber = '';
        row.name = '';
        row.papers = _papers;
        this.props.setRows([...marksheet.rows, row]);

    }

    removeRow(index) {
        Swal.fire({
            // title: 'Are you sure?',
            text: "Are you sure you want to permanently delete this student record?",
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes',
            cancelButtonText: "No"
        }).then((result) => {
            if (result.value) {
                this.remove(index);
            }
        })
    }

    remove(index) {
        const rows = [...this.props.marksheet.rows]
        rows.splice(index, 1);
        this.props.setMarksheet({
            rows
        });
    }

    onInfoChange(index, e) {
        const { marksheet } = this.props;
        const rows = [...marksheet.rows];
        let value = e.target.value;
        if (e.target.name === 'name') {
            value = `${value}`.toUpperCase();
        }
        rows[index][e.target.name] = value;
        this.props.setRows(rows);
    }

    onPaperInfoChange(rowIndex, paperIndex, detailIndex, e) {
        const { marksheet } = this.props;
        const rows = [...marksheet.rows];
        const papers = [...rows[rowIndex].papers];
        papers[paperIndex].paperDetails[detailIndex][e.target.name] = e.target.value;
        rows[rowIndex].papers = papers;
        this.props.setRows(rows);

    }

    saveStudentInfo() {
        this.resetError();
        this.setState({ saveStudentInfoFlag: true });
        const { marksheet, user } = this.props;
        const code = user.courseCode;
        const fileName = getFileName(code, marksheet.year);
        axios.post('./api/save.php', { fileName: fileName, data: marksheet }).then(res => {
            this.success();
            this.setState({ saveStudentInfoFlag: false });
        })
            .catch((error) => {
                this.error();
                this.setState({ saveStudentInfoFlag: false });
            });
    }

    saveMarks() {
        this.resetError();
        const { marksheet, user } = this.props;
        if (this.checkErrors(marksheet)) {
            this.setState({ saveMarksFlag: true });
            const code = user.courseCode;
            const fileName = getFileName(code, marksheet.year);
            axios.post('./api/save.php', { fileName: fileName, data: marksheet }).then(res => {
                this.success();
                this.setState({ saveMarksFlag: false });
            }).catch((error) => {
                this.error();
                this.setState({ saveMarksFlag: false });
            });
        }
        else {
            setTimeout(() => {
                this.error('Validation failed', this.getErrorDisplayMessage());
            })
        }
    }

    getErrorDisplayMessage() {
        const displayErrorMessage = Object.values(this.state.displayErrors);
        return displayErrorMessage.join(`<br>`);
    }

    checkErrors(marksheet) {
        const errors = {};
        const displayErrors = {};
        if (!marksheet.examName) {
            errors.examName = 'Required';
            this.setState({ displayErrors: { ...this.state.displayErrors, examName: 'Please fill in Exam name' } })
            displayErrors.examName = 'Please fill in Exam name';
        }
        if (!marksheet.examYear) {
            errors.examYear = 'Required';
            displayErrors.year = 'Please fill in year';

        }
        if (!marksheet.date) {
            errors.date = 'Required';
            displayErrors.date = 'Please fill in date of result';

        }

        marksheet.rows.forEach((row, index) => {
            if (!row.rollNumber) {
                errors[`rollNumber${index}`] = 'Required';
                displayErrors.required = 'Fields are required';
            }
            else if (isNaN(row.rollNumber) || row.rollNumber <= 0) {
                errors[`rollNumber${index}`] = 'Invalid roll number';
                displayErrors.rollNumber = 'Please enter valid Roll number';
            }
            if (!row.name) {
                errors[`name${index}`] = 'Required';
                displayErrors.required = 'Fields are required';
            }

            let graceMarksCounter = 0;
            row.papers.forEach((paper, i) => {
                paper.paperDetails.forEach((detail, detailIndex) => {
                    if (detail.isRequired && !detail.marksObtained) {
                        errors[`${detail.title}_${index}_${i}`] = 'Required';
                        displayErrors.required = 'Fields are required';
                    }
                    else if (detail.marksObtained) {
                        if(detail.title == "Language" || detail.title == "Grade" || detail.title == "Elective")
                        {
                            return;
                        }
                        if (this.skipWords.indexOf(`${detail.marksObtained}`.toUpperCase()) !== -1) {
                            return;
                        }

                        if (isNaN(detail.marksObtained)) {
                            errors[`${detail.title}_${index}_${i}`] = 'Invalid Value';
                            displayErrors.marks = 'Please correct the entered marks';
                        }
                        else if (detail.isGrace) {
                            graceMarksCounter += +detail.marksObtained;
                            if (graceMarksCounter > this.props.maxGrace) {
                                errors[`${detail.title}_${index}_${i}`] = 'Grace marks is larger';
                                displayErrors.marks = 'Grace marks is exceeding limit';
                            }
                        }
                        else if (+detail.marksObtained < 0 || +detail.marksObtained > +detail.maximum) {
                            errors[`${detail.title}_${index}_${i}`] = 'Not in range';
                            displayErrors.marks = 'Please correct the entered marks';
                        }

                    }

                })
            })
        })
        // console.log(errors);
        if (Object.keys(errors).length) {
            this.setState({ errors, displayErrors });
            return false;
        }

        return true;
    }

    resetError() {
        this.setState({ errors: {} })
    }


    printMarksheet() {

        this.setState({view: 'marksheet'})
    }

    printLedgerReport() {
        this.setState({view: 'report'})
    }

    success(message = 'Records have been saved successfully!') {
        Swal.fire({
            type: 'success',
            title: message,
            showConfirmButton: false,
            timer: 1500
        })
    }

    error(message = 'Something went wrong', text = '') {
        Swal.fire({
            type: 'error',
            title: message,
            html: text
        })
    }

    backToDashboard()
    {
        // this.setState({view: 'default'})
        this.reload();
    }

    render() {
        const { course, user } = this.props;
        const { years } = course;
        const { marksheet, papers } = this.props;
        const { errors, saveStudentInfoFlag, saveMarksFlag, view, apiUrl } = this.state;
        const grades = course.grades || [];
        return <div>
            <div className={classNames('full-height', {'has-background-grey-lighter': view === 'default'})}>
                <Nav logout={this.logout} reload={this.reload} user={user} />
                {view === 'default' && 
                <Fragment>
                    <Info
                        course={course}
                        years={years}
                        marksheet={marksheet}
                        onChange={this.onChange}
                        handleDateChange={this.handleDateChange}
                        handleYearChange={this.handleYearChange}
                        errors={errors}
                    />
                    <Details
                        marksheet={marksheet}
                        papers={papers}
                        addRow={this.addRow}
                        removeRow={this.removeRow}
                        onInfoChange={this.onInfoChange}
                        onPaperInfoChange={this.onPaperInfoChange}
                        saveStudentInfo={this.saveStudentInfo}
                        saveMarks={this.saveMarks}
                        errors={errors}
                        printMarksheet={this.printMarksheet}
                        printLedgerReport={this.printLedgerReport}
                        saveStudentInfoFlag={saveStudentInfoFlag}
                        saveMarksFlag={saveMarksFlag}
                    />
                </Fragment>
                }
                {
                    view === 'marksheet' &&
                    <Marksheet grades={grades} backToDashboard={this.backToDashboard} apiUrl={apiUrl} marksheetTemplate={course.marksheetTemplate}/>
                }
                {
                    view === 'report' &&
                    <Report  grades={grades} backToDashboard={this.backToDashboard} apiUrl={apiUrl} reportTemplate={course.reportTemplate}/>
                }
            </div>
        </div>
    }
}

const mapStateToProps = (state) => {
    return {
        course: state.course,
        marksheet: state.marksheet,
        user: state.app.user,
        papers: state.app.papers,
        maxGrace: state.app.maxGrace
    }
}

const mapDispatchToProps = (dispatch) => {
    return {
        reset: () => dispatch(reset()),
        getCourse: (code) => dispatch(getCourse(code)),
        setMarksheet: (data) => dispatch(setMarksheet(data)),
        resetMarksheet: () => dispatch(resetMarksheet()),
        setRows: (data) => dispatch(setRows(data)),
        setPapers: (papers) => dispatch(setPapers(papers)),
        setApp: (payload) => dispatch(setApp(payload))
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(Dashboard);