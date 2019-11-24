export function setUserInLocalStorage(user)
{
    localStorage.setItem('user', JSON.stringify(user));
}

export function getUserFromLocalStorage()
{
    const user =  localStorage.getItem('user');
    return user ? JSON.parse(user) : null;
}

export function removeUserFromLocalStorage()
{
    localStorage.removeItem('user');
}

export function getUniqueId()
{
    // Math.random should be unique because of its seeding algorithm.
    // Convert it to base 36 (numbers + letters), and grab the first 9 characters
    // after the decimal.
    return '_' + Math.random().toString(36).substr(2, 9);
}

export function getFileName(code, year)
{
    return  `${code}-${year}.json`;
}

export function getTime()
{
    return (new Date()).getTime();
}

export function getDate(date)
{
    let today = new Date(date);
    let dd = today.getDate();
    let mm = today.getMonth() + 1; //January is 0!

    let yyyy = today.getFullYear();
    if (dd < 10) {
    dd = '0' + dd;
    } 
    if (mm < 10) {
    mm = '0' + mm;
    } 
    return dd + '/' + mm + '/' + yyyy;
}

export function getValue(value) {
    return isNaN(value) ? 0 : +value;
}

export function getVersion2Data(state, grades)
{
    state.date = state.date ? getDate(state.date) : '';

    const getGrade = (percent) => grades.find((grade) => grade.start <= percent && grade.end >= percent) || {};

    state.rows.forEach((row) => {
        let credit = 0;
        let gto = 0;
        let gt = 0
        let ito = 0;
        let it = 0;
        let eto = 0;
        let et = 0;
		let failCounter = 0;
        let result = 'PASS';
		let isATKT = false;
		
        row.papers.forEach((paper) => {
            credit += paper.credits;
            const internal = paper.paperDetails.find((detail) => detail.title === 'Internal') || {};
            const external = paper.paperDetails.find((detail) => detail.title === 'External') || {};
            const grace = paper.paperDetails.find((detail) => detail.isGrace) || {};
            paper._internal = internal.marksObtained;
            paper._internal_max = internal.maximum;
            
            ito += getValue(paper._internal);
            it += getValue(paper._internal_max);

            paper._external = external.marksObtained;
            paper._external_max = external.maximum;
            paper._grace = grace.marksObtained || 0;
           
            eto += getValue(paper._external) + getValue(paper._grace);
            et += getValue(paper._external_max);

            paper._totalObtained = getValue(paper._internal) + getValue(paper._external) + getValue(paper._grace);
            paper._total = +internal.maximum + +external.maximum;
            const percent = (paper._totalObtained * 100) / paper._total;
            const grade = getGrade(Math.round(percent));
            paper._grade = grade.code;
            if(paper._grade === 'F')
            {
                result = 'FAIL';
				isATKT = true;
				failCounter =  failCounter + 1;
            }   
            gto +=  paper._totalObtained;
            gt += paper._total ;
        })
        row._internal_total_o = ito;
        row._internal_total = it;

        row._external_total_o = eto;
        row._external_total = et;

        row._credits = credit;
        row._grand_total = gto;
        row._total = gt; 
        
        row._percent = ((row._grand_total * 100) / row._total).toFixed(2);
        const grade =  getGrade(Math.round(row._percent));
        row._grade = grade.code;
        
		if(row._grade === 'F' || isATKT)
        {
            result = 'ATKT';
			row._grade = "-";
			row._percent = "-";
        }

		if(row.papers.length == failCounter){
			result = 'FAIL';
		}		
		
        row._result = result;
    })
    

    return state;
}

export function getVersion1Data(state, grades)
{   
    state.date = state.date ? getDate(state.date) : '';
    const getGrade = (percent) => grades.find((grade) => grade.start <= percent && grade.end >= percent) || {};
    state.rows.forEach((row) => {
        row._result = 'PASS';
        row.papers.forEach((paper) => {
            const internal = paper.paperDetails.find((detail) => detail.title === 'Internal') || {};
            const external = paper.paperDetails.find((detail) => detail.title === 'External') || {};
            const grace = paper.paperDetails.find((detail) => detail.isGrace) || {};
            row._internal = internal.marksObtained;
            row._internal_max = internal.maximum;
            
          
            row._external = external.marksObtained;
            row._external_max = external.maximum;
            row._grace = grace.marksObtained || 0;
            row._totalObtained = getValue(row._internal) + getValue(row._external) + getValue(row._grace);
            row._total = +internal.maximum + +external.maximum;
            
            if(row._grace > 0)
            {
                row._external = `${row._external} + ${row._grace}`;
            }

            const percent = (row._totalObtained * 100) / row._total;
            row._percent = percent.toFixed(2);
            const grade = getGrade(Math.round(percent));
            row._grade = grade.code;
            if(row._grade === 'F')
            {
                row._result = 'FAIL';
            }   
        })

    })
    return state;
}

export function getVersion3Data(state, grades)
{
    state.date = state.date ? getDate(state.date) : '';

    const getGrade = (percent) => grades.find((grade) => grade.start <= percent && grade.end >= percent) || {};

    state.rows.forEach((row) => {
        let gto = 0;
        let gt = 0
        let ito = 0;
        let it = 0;
        let eto = 0;
        let et = 0;
		let ato = 0;
		let at = 0;
		
        let result = 'PASS';
		let failCounter = 0;
		let isATKT = false;
        let t_g = 0;

        row.papers.forEach((paper) => {
            const internal = paper.paperDetails.find((detail) => detail.title === 'Internal') || {};
            const external = paper.paperDetails.find((detail) => detail.title === 'External') || {};
			const academic = paper.paperDetails.find((detail) => detail.title === 'Academic Performance') || {};
            const grace = paper.paperDetails.find((detail) => detail.isGrace) || {};
            paper._internal = internal.marksObtained;
            paper._internal_max = internal.maximum;
            
            ito += getValue(paper._internal);
            it += getValue(paper._internal_max);

            paper._external = external.marksObtained;
            paper._external_max = external.maximum;
			
			paper._academic = academic.marksObtained;
			paper._academic_max = academic.maximum;
			
            paper._grace = grace.marksObtained || 0;

            t_g +=  getValue(paper._grace);
           
            eto += getValue(paper._external) + getValue(paper._grace);
            et += getValue(paper._external_max);
			
			ato +=  getValue(paper._academic);
			at += getValue(paper._academic_max);

            paper._totalObtained = getValue(paper._internal) + getValue(paper._external) + getValue(paper._academic) + getValue(paper._grace);
            paper._total = +internal.maximum + +external.maximum + +academic.maximum;
            const percent = (paper._totalObtained * 100) / paper._total;
            const grade = getGrade(Math.round(percent));
            paper._grade = grade.code;
            if(paper._grade === 'F')
            {
				failCounter =  failCounter + 1;
				isATKT = true;
                result = 'FAIL';
            }   
            gto +=  paper._totalObtained;
            gt += paper._total ;
        })
        row._internal_total_o = ito;
        row._internal_total = it;

        row._external_total_o = eto;
        row._external_total = et;
		
		row._academic_total_o = ato;
		row._academic_total = at;

        row._grand_total = gto;
        row._total = gt; 

        row._total_grace = t_g;
        
        row._percent = ((row._grand_total * 100) / row._total).toFixed(2);
        const grade =  getGrade(Math.round(row._percent));
		row._percent = row._percent + "%";
        row._grade = grade.code;
        if(row._grade === 'F' || isATKT)
        {
            result = 'ATKT';
			row._grade = "-";
			row._percent = "-";
        } 
		
		if(row.papers.length == failCounter){
			result = 'FAIL';
		}
		
        row._result = result;
    })
    

    return state;
}

export function getVersion4Data(state, grades)
{
    state.date = state.date ? getDate(state.date) : '';

    const getGrade = (percent) => grades.find((grade) => grade.start <= percent && grade.end >= percent) || {};

    state.rows.forEach((row) => {
        let gto = 0;
        let gt = 0
        let result = 'PASS';
		let failCounter = 0;
		let isATKT = false;
		
        row.papers.forEach((paper) => {

            const attendance = paper.paperDetails.find((detail) => detail.title === 'Attendance') || {}
            const internal = paper.paperDetails.find((detail) => detail.title === 'Internal') || {}
            const external = paper.paperDetails.find((detail) => detail.title === 'External') || {}
            //const semester = paper.paperDetails.find((detail) => detail.title === 'Semester') || {}
            const grace = paper.paperDetails.find((detail) => detail.isGrace) || {};

            paper._attendance = attendance.marksObtained;
            paper._attendance_max = attendance.maximum;

            paper._internal = internal.marksObtained;
            paper._internal_max = internal.maximum;

            paper._external = external.marksObtained;
            paper._external_max = external.maximum;

            //paper._semester = semester.marksObtained;
            //paper._semester_max = semester.maximum;

            paper._grace = grace.marksObtained || 0;

			paper._totalObtained = getValue( paper._attendance) + getValue(paper._internal) + getValue(paper._external) + getValue(paper._grace);
            //paper._totalObtained = getValue( paper._attendance) + getValue(paper._unitTest) + getValue(paper._assesment) + getValue(paper._semester) +  getValue(paper._grace);
            paper._total = +attendance.maximum + +internal.maximum + +external.maximum;// + +semester.maximum;
            const percent = (paper._totalObtained * 100) / paper._total;
            const grade = getGrade(Math.round(percent));
            paper._grade = grade.code;
            if(paper._grade === 'F')
            {
                failCounter =  failCounter + 1;
				isATKT = true;
                result = 'FAIL';
            }
			
            gto +=  paper._totalObtained;
            gt += paper._total ;
        })

        row._grand_total = gto;
        row._total = gt; 

        row._percent = ((row._grand_total * 100) / row._total).toFixed(2);
        const grade =  getGrade(Math.round(row._percent));
        row._grade = grade.code;
        if(row._grade === 'F' || isATKT)
        {
            result = 'ATKT';
			row._grade = "-";
			row._percent = "-";
        } 
		
		if(row.papers.length == failCounter){
			result = 'FAIL';
		}
		
        row._result = result;
    })
    

    return state;
}

export function getVersion5Data(state, grades)
{   
    state.date = state.date ? getDate(state.date) : '';
    const getGrade = (percent) => grades.find((grade) => grade.start <= percent && grade.end >= percent) || {};
    state.rows.forEach((row) => {
        row._result = 'PASS';
        row.papers.forEach((paper) => {
            const written = paper.paperDetails.find((detail) => detail.title === 'Written') || {};
            const submission = paper.paperDetails.find((detail) => detail.title === 'Submission') || {};
            const grace = paper.paperDetails.find((detail) => detail.isGrace) || {};
            const practical = paper.paperDetails.find((detail) => detail.title === 'Practical') || {};
            const general = paper.paperDetails.find((detail) => detail.title === 'General') || {};


            row._written = written.marksObtained;
            row._written_max = written.maximum;
            
            row._submission = submission.marksObtained;
            row._submission_max = submission.maximum;
            
            row._practical = practical.marksObtained;
            row._practical_max = practical.maximum;

            row._general = general.marksObtained;
            row._general_max = general.maximum;

            row._grace = grace.marksObtained || 0;
            
            
            row._totalObtained = getValue(row._written) + getValue(row._submission) +  getValue(row._practical) +  getValue(row._general) + getValue(row._grace);
            row._total = +written.maximum + +submission.maximum + +practical.maximum + +general.maximum;
            
            if(row._grace > 0)
            {
                row._general = `${row._general} + ${row._grace}`;
            }

            const percent = (row._totalObtained * 100) / row._total;
            row._percent = percent.toFixed(2);
            const grade = getGrade(Math.round(percent));
            row._grade = grade.code;
            if(row._grade === 'F')
            {
                row._result = 'FAIL';
            }   
        })

    })
    return state;
}

export function getVersion7Data(state, grades)
{   
    state.date = state.date ? getDate(state.date) : '';
    const getGrade = (percent) => grades.find((grade) => grade.start <= percent && grade.end >= percent) || {};
    state.rows.forEach((row) => {
        row._result = 'PASS';
        row.papers.forEach((paper) => {
            const theory = paper.paperDetails.find((detail) => detail.title === 'Theory') || {};
            //const submission = paper.paperDetails.find((detail) => detail.title === 'Submission') || {};
            //const grace = paper.paperDetails.find((detail) => detail.isGrace) || {};
            const practical = paper.paperDetails.find((detail) => detail.title === 'Practical') || {};
            const internal = paper.paperDetails.find((detail) => detail.title === 'Internal') || {};


            row._theory = theory.marksObtained;
            row._theory_max = theory.maximum;
            
            row._internal = internal.marksObtained;
            row._internal_max = internal.maximum;
            
            row._practical = practical.marksObtained;
            row._practical_max = practical.maximum;

            
            //row._grace = grace.marksObtained || 0;
            
            
            row._totalObtained = getValue(row._theory) + getValue(row._internal) +  getValue(row._practical);
            row._total = +theory.maximum + +practical.maximum + +internal.maximum;
            
            /*if(row._grace > 0)
            {
                row._general = `${row._general} + ${row._grace}`;
            }*/

            const percent = (row._totalObtained * 100) / row._total;
            row._percent = percent.toFixed(2);
            const grade = getGrade(Math.round(percent));
            row._grade = grade.code;
            if(row._grade === 'F')
            {
                row._result = 'FAIL';
            }   
        })

    })
    return state;
}

export function getVersion6Data(state, grades)
{
    state.date = state.date ? getDate(state.date) : '';

    const getGrade = (percent) => grades.find((grade) => grade.start <= percent && grade.end >= percent) || {};

    state.rows.forEach((row) => {
        let credit = 0;
        let gto = 0;
        let gt = 0
        let finalGrade = "PASS";
        let result = 'PASS';
		let totalGrace = 0;
		let graceCounter = 0;
		let paperGrace = 0;
		let paperGraceCounter = 0;
		
        row.papers.forEach((paper) => {
            const grace = paper.paperDetails.find((detail) => detail.isGrace) || {};
			const unit1 = paper.paperDetails.find((detail) => detail.title === 'UT I (A)') || {};
			const unit2 = paper.paperDetails.find((detail) => detail.title === 'UT II (B)') || {};
            const term1 = paper.paperDetails.find((detail) => detail.title === 'TERM I (C)') || {};
            const term2 = paper.paperDetails.find((detail) => detail.title === 'TERM II (D)') || {};
            
			
            const grade = paper.paperDetails.find((detail) => detail.title === "Grade") || {};
			const language = paper.paperDetails.find((detail) => detail.title === 'Language') || {};
			const elective = paper.paperDetails.find((detail) => detail.title === 'Elective') || {};
			
			paper._unit1 = unit1.marksObtained;
			paper._unit1_max = unit1.maximum;
			paper._unit2 = unit2.marksObtained;
			paper._unit2_max = unit2.maximum;
			
			paper._term1 = term1.marksObtained;
			paper._term1_max = term1.maximum;
            paper._term2 = term2.marksObtained;
			paper._term2_max = term2.maximum;
			
            paper._grace = grace.marksObtained || 0;
			totalGrace += Math.round(paper._grace);
           if(paper.paperTitle === "Physical Education" || paper.paperTitle === "PHY. EDU.")
           {
                paper._grade = grade.marksObtained;
                paper._unit1 = '-';
                paper._unit2 = '-';
                paper._term1 = '-';
                paper._term2 = '-';
                paper._max = '-';
                paper._min = '-';
				paper._obtained = '-';
                paper._totalObtained = grade.marksObtained;
           }
           else if(paper.paperTitle === "E.V.S.")
           {
                paper._obtained = getValue(paper._term2);
                paper._max =  term2.maximum;
                paper._total = getValue(term2.maximum);
                paper._min = term2.passing;
                paper._totalObtained = getValue(paper._obtained);
                
                let percent = (paper._totalObtained * 100) / paper._total;
                let grade = getGrade(Math.round(percent));
                paper._grade = grade.code;

                paper._unit1 = '-';
                paper._unit2 = '-';
                paper._term1 = '-';
                paper._term2 = '-';
           }
           else
           {  
			   	
               paper._obtained = getValue(paper._unit1) + getValue(paper._unit2) + getValue(paper._term1) + getValue(paper._term2);
               paper._max = (getValue(unit1.maximum) + getValue(unit2.maximum)  + getValue(term1.maximum) + getValue(term2.maximum)) / 2;
               paper._total = (getValue(unit1.maximum) + getValue(unit2.maximum)  + getValue(term1.maximum) + getValue(term2.maximum)) / 2;
               paper._min = paper._total * (35/100);
               paper._totalObtained = Math.round(getValue(paper._obtained) / 2);
               
			   if(paper._totalObtained < 35){
				   paperGrace = 35 - paper._totalObtained;
				   if(paperGrace <= 10 && (graceCounter + paperGrace) <= 15 && paperGraceCounter < 4){
					   paper._grace = paperGrace;
					   graceCounter += paperGrace;
					   paperGraceCounter += 1;
				   }
			   }
			   
			   if(language && language.title == "Language"){
				   paper.paperTitle = language.marksObtained;
			   }
			   else if(elective && elective.title == "Elective"){
				   paper.paperTitle = elective.marksObtained;
			   }
			   
               let  percent = (paper._totalObtained * 100) / paper._total;
			   let mo = Math.round(Math.round(Math.round(paper._totalObtained) + Math.round(paper._grace))  * 100);
               let grade = getGrade(Math.round(mo / paper._total));
               paper._grade = grade.code;
            }
            if(paper._grade === 'FAIL')
            {
                result = 'FAIL';
            }
			
            gto +=  getValue(paper._totalObtained);
            gt += getValue(paper._total) ;
        })
        
        row._credits = credit;
        row._grand_total = gto;
        row._total = gt; 
        row._totalGrace = graceCounter;
        row._percent = ((row._grand_total * 100) / row._total).toFixed(2);
        const grade =  getGrade(Math.round(row._percent));
        
		if(result == "FAIL")
		{
			row._grade = "F";
			row._result = result;
		}
		else if	(result == "PASS" && Math.round(row._totalGrace) > 0){
			row._grade = "";
			row._result = "GRACE";		
		}
		else
		{
			row._grade = grade.code;
			row._result = result;
		}
		
        /*if(row._grade === 'FAIL')
        {
            result = 'FAIL';
        }*/ 
        
    })
    

    return state;
}

export function getVersion8Data(state, grades)
{   
    state.date = state.date ? getDate(state.date) : '';
    const getGrade = (percent) => grades.find((grade) => grade.start <= percent && grade.end >= percent) || {};
    state.rows.forEach((row) => {
        row._result = 'PASS';
        row.papers.forEach((paper) => {
            const internal = paper.paperDetails.find((detail) => detail.title === 'Internal') || {};
            const external = paper.paperDetails.find((detail) => detail.title === 'Theory') || {};
			const practical = paper.paperDetails.find((detail) => detail.title === 'Practical') || {};
            const grace = paper.paperDetails.find((detail) => detail.isGrace) || {};
            
			row._internal = internal.marksObtained;
            row._internal_max = internal.maximum;
			
            row._external = external.marksObtained;
            row._external_max = external.maximum;
			
			row._practical = practical.marksObtained;
            row._practical_max = practical.maximum;
			
            row._grace = grace.marksObtained || 0;
            row._totalObtained = getValue(row._internal) + getValue(row._external) + getValue(row._practical) + getValue(row._grace);
            row._total = +internal.maximum + +external.maximum + +practical.maximum;
            
            /*if(row._grace > 0)
            {
                row._external = `${row._external} + ${row._grace}`;
            }*/

            const percent = (row._totalObtained * 100) / row._total;
            row._percent = percent.toFixed(2);
            const grade = getGrade(Math.round(percent));
            row._grade = grade.code;
            if(row._grade === 'F')
            {
                row._result = 'FAIL';
            }   
        })

    })
    return state;
}

export function getVersion9Data(state, grades)
{   
    state.date = state.date ? getDate(state.date) : '';
    const getGrade = (percent) => grades.find((grade) => grade.start <= percent && grade.end >= percent) || {};
    state.rows.forEach((row) => {
        row._result = 'PASS';
        row.papers.forEach((paper) => {
            const written = paper.paperDetails.find((detail) => detail.title === 'Written') || {};
            //const submission = paper.paperDetails.find((detail) => detail.title === 'Submission') || {};
            //const grace = paper.paperDetails.find((detail) => detail.isGrace) || {};
            const practical = paper.paperDetails.find((detail) => detail.title === 'Practical') || {};
            const submission = paper.paperDetails.find((detail) => detail.title === 'Submission') || {};
            const general = paper.paperDetails.find((detail) => detail.title === 'General') || {};


            row._written = written.marksObtained;
            row._written_max = written.maximum;
            
            row._submission = submission.marksObtained;
            row._submission_max = submission.maximum;
            
            row._practical = practical.marksObtained;
            row._practical_max = practical.maximum;

            row._general = general.marksObtained;
            row._general_max = general.maximum;

            //row._grace = grace.marksObtained || 0;
            
            
            row._totalObtained = getValue(row._written) + getValue(row._submission) +  getValue(row._practical) + getValue(row._general);
            row._total = +written.maximum + +practical.maximum + +submission.maximum + +general.maximum;
            
            /*if(row._grace > 0)
            {
                row._general = `${row._general} + ${row._grace}`;
            }*/

            const percent = (row._totalObtained * 100) / row._total;
            row._percent = percent.toFixed(2);
            const grade = getGrade(Math.round(percent));
            row._grade = grade.code;
            if(row._grade === 'F')
            {
                row._result = 'FAIL';
            }   
        })

    })
    return state;
}