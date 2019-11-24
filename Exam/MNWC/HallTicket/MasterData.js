var PaperPerMarksheet = 6;
var TotalMarks = "600";
var MarkSheetDate = "01/12/2019";
var is35Passing = true;
var CourseExamName = "BACHELOR OF ARTS (B.A.) Semester-I";
//var CourseExamName = "BACHELOR OF COMMERCE (B.Com) Semester-II";
//var CourseExamName = "BACHELOR OF COMMERCE WITH ACCOUNTANCY, FINANCE AND INSURANCE Semester-I";
//var CourseExamName = "BACHELOR OF MANAGEMENT STUDIES (B.M.S) Semester-I";
var ExamYear = "November 2019 ATKT";
var Medium = "ENGLISH";
//var Medium = "GUJARATI";
var Institution = "027";
var Center = "008 - VILE PARLE (W)";
var Place = "Vile Parle (W)";
var PaperCodes = 
{
	"140119":{"PaperTitle":"Basics of Accountancy","Credits":"4"},	
	"100124":{"PaperTitle":"TOURISM BUSINESS PAPER-I","Credits":"4"},
	"100152":{"PaperTitle":"SHORTHAND & TYPING PAPER-I","Credits":"4"},
	"100224":{"PaperTitle":"TOURISM ORGANISATIONS PAPER-II","Credits":"4"},
	"100252":{"PaperTitle":"OFFICE MANAGEMENT PAPER-II","Credits":"4"},
	"110101":{"PaperTitle":"CC ENGLISH PAPER-I","Credits":"4"},
	//"      ":{"PaperTitle":"ENGLISH CC PAPER (H.L.) EMPOWERING ENGLISH - I","Credits":"4"},
	"130100":{"PaperTitle":"FC PAPER -I HISTORY AS HERITAGE","Credits":"4"},
	"130200":{"PaperTitle":"FC PAPER-II WOMEN IN CHANGING INDIA","Credits":"4"},
	"140101":{"PaperTitle":"DC PAPER -I BASIC CONCEPTS GENRE OF LITERARY STUDY/ INTRODUCTION TO LITERARY STUDIES CONCEPTS & GENRE","Credits":"4"},
	"140105":{"PaperTitle":"ECONOMICS PAPER-I","Credits":"4"},
	"140106":{"PaperTitle":"PRINCIPLES OF BUSINESS MANAGEMENT","Credits":"4"},
	"140107":{"PaperTitle":"ACCOUNTING-I FINANCIAL ACCOUNTING","Credits":"4"},
	"140108":{"PaperTitle":"BUSINESS MATHEMATICS & STATISTICS","Credits":"4"},
	"140109":{"PaperTitle":"COMMERCIAL GEOGRAPHY","Credits":"4"},
	"140201":{"PaperTitle":"DC PAPER-II EXPLORING LITERARY STUDIES LITERARY TERMS & CRITICAL APPROACHES","Credits":"4"},
	"145102":{"PaperTitle":"DC GUJARATI PAPER-I BHASHA KAUSHAL, GADHYA SWARUP ANE NIYAT KRUTIO NO ABHYAS","Credits":"4"},
	"145106":{"PaperTitle":"DC ECONOMICS PAPER-I ECONOMY OF MAHARASHTRA","Credits":"4"},
	"145110":{"PaperTitle":"DC PSYCHOLOGY PAPER-I GENERAL PSYCHOLOGY","Credits":"4"},
	"145111":{"PaperTitle":"SOCIOLOGY PAPER-I SOCIOLOGY OF INDIAN SOCIETY","Credits":"4"},
	"145202":{"PaperTitle":"DC GUJARATI PAPER-II ANUVADKALA ANE ANUDIT KRUTIO NO ABHYAS","Credits":"4"},
	"145206":{"PaperTitle":"DC ECONOMICS PAPER-II PRINCIPLES OF ECONOMICS","Credits":"4"},
	"145210":{"PaperTitle":"DC PSYCHOLOGY PAPER-II DEVELOPMENT PSYCHOLOGY","Credits":"4"},
	"145211":{"PaperTitle":"SOCIOLOGY PAPER-II FOUNDATION TO SOCIOLOGY","Credits":"4"},
	"150101":{"PaperTitle":"ENGLISH PAPER (L.L.) STARTING WITH ENGLISH","Credits":"4"},
	"175103":{"PaperTitle":"AC HINDI PAPER-I AADHUNIK GADHYA HINDI KAHANI","Credits":"4"},
	"180126":{"PaperTitle":"CAPC PAPER-I FUNDAMENTAL OF FOOD SCIENCE","Credits":"4"},
	"180127":{"PaperTitle":"CAPC PAPER-I  CHILD DEVELOPMENT","Credits":"4"},
	"200324":{"PaperTitle":"TOURISM & TRAVEL MANAGEMENT PAPER","Credits":"4"},
	"200352":{"PaperTitle":"OFFICE MANAGEMENT& SECRETARIAL PRACTICE PAPER","Credits":"4"},
	"204206":{"PaperTitle":"COMMERCE","Credits":"4"},
	"210201":{"PaperTitle":"CC ENGLISH PAPER-II HL ELEMENTARY COURSE IN LANG & COMM.SKILLS","Credits":"4"},
	//"      ":{"PaperTitle":"ENGLISH CC PAPER (H.L.) EMPOWERING ENGLISH- II","Credits":"4"},
	"230300":{"PaperTitle":"PERSONALITY DEVELOPMENT","Credits":"4"},
	"230400":{"PaperTitle":"ENVIRONMENTAL STUDIES","Credits":"4"},
	"240205":{"PaperTitle":"BUSINESS ECONOMICS PAPER-II ANALYSIS OF MARKETS","Credits":"4"},
	"240206":{"PaperTitle":"COMMERCE PAPER-II PRINCIPLES OF MARKETING MANAGEMENT","Credits":"4"},
	"240207":{"PaperTitle":"ACCOUNTANCY PAPER-II ADVANCE FINANCIAL ACCOUNTING","Credits":"4"},
	"240208":{"PaperTitle":"BUSINESS STATISTICS","Credits":"4"},
	"240209":{"PaperTitle":"ENVIRONMENTAL STUDIES","Credits":"4"},
	"240301":{"PaperTitle":"ENGLISH PAPER-III THE NINETEENTH CENTURY NOVEL","Credits":"4"},
	"240401":{"PaperTitle":"ENGLISH PAPER-IV ROMANTIC & VICTORIAN POETRY","Credits":"4"},
	"245302":{"PaperTitle":"GUJARATI PAPER -III PADHYA SWARUP ANE KRUTINO ABHYAS","Credits":"4"},
	"245306":{"PaperTitle":"ECONOMICS PAPER-IIIA MACRO LEVEL PROBLEM IN THE ECONOMY OF MAHARASHTRA","Credits":"4"},
	"245310":{"PaperTitle":"PSYCHOLOGY PAPER-III GENERAL PSYCHOLOGY-BASIC COGNITIVE PROCESS","Credits":"4"},
	"245311":{"PaperTitle":"SOCIOLOGY PAPER-III SOCIAL PROBLEM IN INDIA","Credits":"4"},
	"245402":{"PaperTitle":"GUJARATI PAPER-IV LOKNATYA BHAVAI","Credits":"4"},
	"245406":{"PaperTitle":"ECONOMICS PAPER-IV BASICS OF MONEY , BANKING ,INTERNATIONAL TRADE & PUBLIC FINANCE","Credits":"4"},
	"245410":{"PaperTitle":"PSYCHOLOGY PAPER-IV DEVELOPMENTAL PSYCHOLOGY ","Credits":"4"},
	"245411":{"PaperTitle":"SOCIOLOGY PAPER-IV INTRODUCTION TO SOCIOLOGY","Credits":"4"},
	"250201":{"PaperTitle":"ENGLISH CC PAPER (L.L.) EXPLORING ENGLISH","Credits":"4"},
	"275203":{"PaperTitle":"AC HINDI PAPER-II, AADHUNIK PADHYA HINDI KAVITA","Credits":"4"},
	"280226":{"PaperTitle":"CAPC PAPER - II FUNDAMENTAL OF NUTRITION","Credits":"4"},
	"280227":{"PaperTitle":"CAPC PAPER-II - ADOLESCENT DEVELOPMENT","Credits":"4"},
	"2001":{"PaperTitle":"MARKETING MANAGEMENT","Credits":"4"},
	"2002":{"PaperTitle":"ORGANIZATIONAL BEHAVIOR","Credits":"4"},
	"2003":{"PaperTitle":"ECONOMICS - II","Credits":"4"},
	"2004":{"PaperTitle":"INTRODUCTION TO ICT","Credits":"4"},
	"2005":{"PaperTitle":"INTRODUCTION TO QUANTITATIVE TECHNIQUES","Credits":"4"},
	"240119":{"PaperTitle":"ACCOUNTANCY","Credits":"4"},
	"240219":{"PaperTitle":"MANAGEMENT ACCOUNTING","Credits":"4"},
	"240319":{"PaperTitle":"ENVIRONMENTAL STUDIES","Credits":"4"},
	"240419":{"PaperTitle":"BUSINESS CORRESPONDENCE","Credits":"4"},
	"240519":{"PaperTitle":"RECENT TRENDS IN MANAGEMENT","Credits":"4"}
};

/*{
	"110101" : { "PaperTitle":"ENGLISH CC PAPER (H.L.) EMPOWERING ENGLISH", "Credits":"4" },
	"130100" : { "PaperTitle":"FC PAPER I-HISTORY AS HERITAGE", "Credits":"4" },
	"130200" : { "PaperTitle":"FC PAPER II-WOMEN IN CHANGING INDIA", "Credits":"4" },
	"140101" : { "PaperTitle":"DC PAPER-I BASIC CONCEPTS IN LITERA...", "Credits":"4"},
	"140201" : { "PaperTitle":"DC PAPER-II EXPLORING LITERARY STUDIES", "Credits":"4"},
	"180127":{ "PaperTitle":"CAPC PAPER-I CHILD DEVELOPMENT", "Credits":"4"}
};*/
var papers1 = [{
	"CourseName": "B.M.S. - I",
   "SubCourse": "Regular",
   "Semester": "II",
   "PaperCode": 2001,
   "PaperTitle": "MARKETING MANAGEMENT"
},
{
	"CourseName": "B.M.S. - I",
   "SubCourse": "Regular",
   "Semester": "II",
   "PaperCode": 2002,
   "PaperTitle": "ORGANIZATIONAL BEHAVIOR"
},
{
	"CourseName": "B.M.S. - I",
   "SubCourse": "Regular",
   "Semester": "II",
   "PaperCode": 2003,
   "PaperTitle": "ECONOMICS - II"
},
{
	"CourseName": "B.M.S. - I",
   "SubCourse": "Regular",
   "Semester": "II",
   "PaperCode": 2004,
   "PaperTitle": "INTRODUCTION TO ICT"
},
{
	"CourseName": "B.M.S. - I",
   "SubCourse": "Regular",
   "Semester": "II",
   "PaperCode": 2005,
   "PaperTitle": "INTRODUCTION TO QUANTITATIVE TECHNIQUES"
},
{
	"CourseName": "B.M.S. - I",
   "SubCourse": "Regular",
   "Semester": "I",
   "PaperCode": 1001,
   "PaperTitle": "PRINCIPLES OF MANAGEMENT"
},
{
	"CourseName": "B.M.S. - I",
   "SubCourse": "Regular",
   "Semester": "I",
   "PaperCode": 1002,
   "PaperTitle": "BUSINESS COMMUNICATION"
},
{
	"CourseName": "B.M.S. - I",
   "SubCourse": "Regular",
   "Semester": "I",
   "PaperCode": 1003,
   "PaperTitle": "FINANCIAL ACCOUNTING"
},
{
	"CourseName": "B.M.S. - I",
   "SubCourse": "Regular",
   "Semester": "I",
   "PaperCode": 1004,
   "PaperTitle": "PRINCIPLES OF MARKETING"
},
{
	"CourseName": "B.M.S. - I",
   "SubCourse": "Regular",
   "Semester": "I",
   "PaperCode": 1005,
   "PaperTitle": "ECONOMICS - I"
},
{
   "CourseName": "F.Y.B.A.F.I.",
   "SubCourse": "Regular",
   "Semester": "II",
   "PaperCode": 240419,
   "PaperTitle": "BUSINESS CORRESPONDENCE"
 },
 {
   "CourseName": "F.Y.B.A.F.I.",
   "SubCourse": "Regular",
   "Semester": "II",
   "PaperCode": 240519,
   "PaperTitle": "RECENT TRENDS IN MANAGEMENT"
 },
{
	"CourseName": "B.Com. - I",
   "SubCourse": "Regular",
   "Semester": "II",
   "PaperCode": 245205,
   "PaperTitle": "BUSINESS ECONOMICS PAPER II THEORY OF PRODUCTION, COSTS AND COMPETITIVE MARKETS"
},
{
	"CourseName": "B.Com. - I",
   "SubCourse": "Regular",
   "Semester": "II",
   "PaperCode": 245206,
   "PaperTitle": "HUMAN RESOURCE MANAGEMENT"
},
{
	"CourseName": "B.Com. - I",
   "SubCourse": "Regular",
   "Semester": "II",
   "PaperCode": 245207,
   "PaperTitle": "ACCOUNTANCY PAPER - II ADVANCED FINANCIAL ACCOUNTING"
},
{
	"CourseName": "B.Com. - I",
   "SubCourse": "Regular",
   "Semester": "II",
   "PaperCode": 245208,
   "PaperTitle": "BUSINESS STATISTICS PAPER II"
},{
	"CourseName": "B.Com. - I",
   "SubCourse": "Regular",
   "Semester": "II",
   "PaperCode": 205324,
   "PaperTitle": "TOURISM & TRAVEL MANAGEMENT TOURISM PRODUCTS - INDIA (PAPER III)"
},
{
	"CourseName": "B.Com. - I",
   "SubCourse": "Regular",
   "Semester": "II",
   "PaperCode": 245209,
   "PaperTitle": "ENVIRONMENTAL STUDIES"
},
{
	"CourseName": "B.Com. - I",
   "SubCourse": "Regular",
   "Semester": "II",
   "PaperCode": 205352,
   "PaperTitle": "OFFICE MANAGEMENT AND SECRETARIAL PRACTICE- PAPER III TYPING"
},
 {
   "CourseName": "F.Y.B.A.F.I.",
   "SubCourse": "Regular",
   "Semester": "I",
   "PaperCode": 140119,
   "PaperTitle": "Basics of Accountancy"
 },
 {
   "CourseName": "F.Y.B.A.F.I.",
   "SubCourse": "Regular",
   "Semester": "I",
   "PaperCode": 140219,
   "PaperTitle": "Financial Accountancy"
 },
 {
   "CourseName": "F.Y.B.A.F.I.",
   "SubCourse": "Regular",
   "Semester": "I",
   "PaperCode": 140319,
   "PaperTitle": "Foundation Course & Information Technology"
 },
 {
   "CourseName": "F.Y.B.A.F.I.",
   "SubCourse": "Regular",
   "Semester": "I",
   "PaperCode": 140419,
   "PaperTitle": "Basics of Business Communication"
 },
 {
   "CourseName": "F.Y.B.A.F.I.",
   "SubCourse": "Regular",
   "Semester": "I",
   "PaperCode": 140519,
   "PaperTitle": "Management Theory & Practice"
 },
 {
   "CourseName": "B.A. I  (GUJ.MED.)",
   "SubCourse": "ECO /  FN",
   "Semester": "I",
   "PaperCode": 130100,
   "PaperTitle": "FC PAPER-I HISTORY AS HERITAGE"
 },
 {
   "CourseName": "B.A. I  (GUJ.MED.)",
   "SubCourse": "ECO /  FN",
   "Semester": "I",
   "PaperCode": 130200,
   "PaperTitle": "FC PAPER-II WOMEN IN CHANGING INDIA"
 },
 {
   "CourseName": "B.A. I  (GUJ.MED.)",
   "SubCourse": "ECO /  FN",
   "Semester": "I",
   "PaperCode": 145106,
   "PaperTitle": "DC ECONOMICS PAPER-I ECONOMY OF MAHARASHTRA"
 },
 {
   "CourseName": "B.A. I  (GUJ.MED.)",
   "SubCourse": "ECO /  FN",
   "Semester": "I",
   "PaperCode": 145206,
   "PaperTitle": "DC ECONOMICS PAPER-II PRINCIPLES OF ECONOMICS"
 },
 {
   "CourseName": "B.A. I  (GUJ.MED.)",
   "SubCourse": "ECO /  FN",
   "Semester": "I",
   "PaperCode": 150101,
   "PaperTitle": "ENGLISH PAPER (L.L.) STARTING WITH ENGLISH"
 },
 {
   "CourseName": "B.A. I  (GUJ.MED.)",
   "SubCourse": "ECO /  FN",
   "Semester": "I",
   "PaperCode": 180126,
   "PaperTitle": "CAPC PAPER-I FUNDAMENTALS OF FOOD SCIENCE"
 },
 {
   "CourseName": "B.A. I  (GUJ.MED.)",
   "SubCourse": "ECO / HN",
   "Semester": "I",
   "PaperCode": 130100,
   "PaperTitle": "FC PAPER-I HISTORY AS HERITAGE"
 },
 {
   "CourseName": "B.A. I  (GUJ.MED.)",
   "SubCourse": "ECO / HN",
   "Semester": "I",
   "PaperCode": 130200,
   "PaperTitle": "FC PAPER-II WOMEN IN CHANGING INDIA"
 },
 {
   "CourseName": "B.A. I  (GUJ.MED.)",
   "SubCourse": "ECO / HN",
   "Semester": "I",
   "PaperCode": 145106,
   "PaperTitle": "DC ECONOMICS PAPER-I ECONOMY OF MAHARASHTRA"
 },
 {
   "CourseName": "B.A. I  (GUJ.MED.)",
   "SubCourse": "ECO / HN",
   "Semester": "I",
   "PaperCode": 145206,
   "PaperTitle": "DC ECONOMICS PAPER-II PRINCIPLES OF ECONOMICS"
 },
 {
   "CourseName": "B.A. I  (GUJ.MED.)",
   "SubCourse": "ECO / HN",
   "Semester": "I",
   "PaperCode": 150101,
   "PaperTitle": "ENGLISH PAPER (L.L.) STARTING WITH ENGLISH"
 },
 {
   "CourseName": "B.A. I  (GUJ.MED.)",
   "SubCourse": "ECO / HN",
   "Semester": "I",
   "PaperCode": 175103,
   "PaperTitle": "AC HINDI PAPER-I AADHUNIK GADHYA HINDI KAHANI"
 },
 {
   "CourseName": "B.A. I  (GUJ.MED.)",
   "SubCourse": "GUJ. / HN",
   "Semester": "I",
   "PaperCode": 130100,
   "PaperTitle": "FC PAPER-I HISTORY AS HERITAGE"
 },
 {
   "CourseName": "B.A. I  (GUJ.MED.)",
   "SubCourse": "GUJ. / HN",
   "Semester": "I",
   "PaperCode": 130200,
   "PaperTitle": "FC PAPER-II WOMEN IN CHANGING INDIA"
 },
 {
   "CourseName": "B.A. I  (GUJ.MED.)",
   "SubCourse": "GUJ. / HN",
   "Semester": "I",
   "PaperCode": 145102,
   "PaperTitle": "DC GUJARATI PAPER-I BHASHA KAUSHAL, GADHYA SWARUP ANE NIYAT KRUTIO NO ABHYAS"
 },
 {
   "CourseName": "B.A. I  (GUJ.MED.)",
   "SubCourse": "GUJ. / HN",
   "Semester": "I",
   "PaperCode": 145202,
   "PaperTitle": "DC GUJARATI PAPER-II ANUVADKALA ANE ANUDIT KRUTIO NO ABHYAS"
 },
 {
   "CourseName": "B.A. I  (GUJ.MED.)",
   "SubCourse": "GUJ. / HN",
   "Semester": "I",
   "PaperCode": 150101,
   "PaperTitle": "ENGLISH PAPER (L.L.) STARTING WITH ENGLISH"
 },
 {
   "CourseName": "B.A. I  (GUJ.MED.)",
   "SubCourse": "GUJ. / HN",
   "Semester": "I",
   "PaperCode": 175103,
   "PaperTitle": "AC HINDI PAPER-I AADHUNIK GADHYA HINDI KAHANI"
 },
 {
   "CourseName": "B.A. I  (GUJ.MED.)",
   "SubCourse": "GUJ. / FN",
   "Semester": "I",
   "PaperCode": 130100,
   "PaperTitle": "FC PAPER-I HISTORY AS HERITAGE"
 },
 {
   "CourseName": "B.A. I  (GUJ.MED.)",
   "SubCourse": "GUJ. / FN",
   "Semester": "I",
   "PaperCode": 130200,
   "PaperTitle": "FC PAPER-II WOMEN IN CHANGING INDIA"
 },
 {
   "CourseName": "B.A. I  (GUJ.MED.)",
   "SubCourse": "GUJ. / FN",
   "Semester": "I",
   "PaperCode": 145102,
   "PaperTitle": "DC GUJARATI PAPER-I BHASHA KAUSHAL, GADHYA SWARUP ANE NIYAT KRUTIO NO ABHYAS"
 },
 {
   "CourseName": "B.A. I  (GUJ.MED.)",
   "SubCourse": "GUJ. / FN",
   "Semester": "I",
   "PaperCode": 145202,
   "PaperTitle": "DC GUJARATI PAPER-II ANUVADKALA ANE ANUDIT KRUTIO NO ABHYAS"
 },
 {
   "CourseName": "B.A. I  (GUJ.MED.)",
   "SubCourse": "GUJ. / FN",
   "Semester": "I",
   "PaperCode": 150101,
   "PaperTitle": "ENGLISH PAPER (L.L.) STARTING WITH ENGLISH"
 },
 {
   "CourseName": "B.A. I  (GUJ.MED.)",
   "SubCourse": "GUJ. / FN",
   "Semester": "I",
   "PaperCode": 180126,
   "PaperTitle": "CAPC PAPER-I FUNDAMENTALS OF FOOD SCIENCE"
 },
 {
   "CourseName": "B.A. I  (GUJ.MED.)",
   "SubCourse": "ECO /  FN",
   "Semester": "II",
   "PaperCode": 230300,
   "PaperTitle": "PERSONALITY DEVELOPMENT"
 },
 {
   "CourseName": "B.A. I  (GUJ.MED.)",
   "SubCourse": "ECO /  FN",
   "Semester": "II",
   "PaperCode": 230400,
   "PaperTitle": "ENVIRONMENTAL STUDIES"
 },
 {
   "CourseName": "B.A. I  (GUJ.MED.)",
   "SubCourse": "ECO /  FN",
   "Semester": "II",
   "PaperCode": 245306,
   "PaperTitle": "ECONOMICS PAPER-IIIA MACRO LEVEL PROBLEM IN THE ECONOMY OF MAHARASHTRA"
 },
 {
   "CourseName": "B.A. I  (GUJ.MED.)",
   "SubCourse": "ECO /  FN",
   "Semester": "II",
   "PaperCode": 245406,
   "PaperTitle": "ECONOMICS PAPER-IV BASICS OF MONEY , BANKING ,INTERNATIONAL TRADE & PUBLIC FINANCE"
 },
 {
   "CourseName": "B.A. I  (GUJ.MED.)",
   "SubCourse": "ECO /  FN",
   "Semester": "II",
   "PaperCode": 250201,
   "PaperTitle": "ENGLISH CC PAPER (L.L.) EXPLORING ENGLISH"
 },
 {
   "CourseName": "B.A. I  (GUJ.MED.)",
   "SubCourse": "ECO /  FN",
   "Semester": "II",
   "PaperCode": 280226,
   "PaperTitle": "CAPC PAPER - II FUNDAMENTAL OF NUTRITION"
 },
 {
   "CourseName": "B.A. I  (GUJ.MED.)",
   "SubCourse": "ECO / HN",
   "Semester": "II",
   "PaperCode": 230300,
   "PaperTitle": "PERSONALITY DEVELOPMENT"
 },
 {
   "CourseName": "B.A. I  (GUJ.MED.)",
   "SubCourse": "ECO / HN",
   "Semester": "II",
   "PaperCode": 230400,
   "PaperTitle": "ENVIRONMENTAL STUDIES"
 },
 {
   "CourseName": "B.A. I  (GUJ.MED.)",
   "SubCourse": "ECO / HN",
   "Semester": "II",
   "PaperCode": 245306,
   "PaperTitle": "ECONOMICS PAPER-IIIA MACRO LEVEL PROBLEM IN THE ECONOMY OF MAHARASHTRA"
 },
 {
   "CourseName": "B.A. I  (GUJ.MED.)",
   "SubCourse": "ECO / HN",
   "Semester": "II",
   "PaperCode": 245406,
   "PaperTitle": "ECONOMICS PAPER-IV BASICS OF MONEY , BANKING ,INTERNATIONAL TRADE & PUBLIC FINANCE"
 },
 {
   "CourseName": "B.A. I  (GUJ.MED.)",
   "SubCourse": "ECO / HN",
   "Semester": "II",
   "PaperCode": 250201,
   "PaperTitle": "ENGLISH CC PAPER (L.L.) EXPLORING ENGLISH"
 },
 {
   "CourseName": "B.A. I  (GUJ.MED.)",
   "SubCourse": "ECO / HN",
   "Semester": "II",
   "PaperCode": 275203,
   "PaperTitle": "AC HINDI PAPER-II, AADHUNIK PADHYA HINDI KAVITA"
 },
 {
   "CourseName": "B.A. I  (GUJ.MED.)",
   "SubCourse": "GUJ. / HN",
   "Semester": "II",
   "PaperCode": 230300,
   "PaperTitle": "PERSONALITY DEVELOPMENT"
 },
 {
   "CourseName": "B.A. I  (GUJ.MED.)",
   "SubCourse": "GUJ. / HN",
   "Semester": "II",
   "PaperCode": 230400,
   "PaperTitle": "ENVIRONMENTAL STUDIES"
 },
 {
   "CourseName": "B.A. I  (GUJ.MED.)",
   "SubCourse": "GUJ. / HN",
   "Semester": "II",
   "PaperCode": 245302,
   "PaperTitle": " GUJARATI PAPER -III PADHYA SWARUP ANE KRUTINO ABYAAS"
 },
 {
   "CourseName": "B.A. I  (GUJ.MED.)",
   "SubCourse": "GUJ. / HN",
   "Semester": "II",
   "PaperCode": 245402,
   "PaperTitle": "GUJARATI PAPER-IV LOKNATYA BHAVAI"
 },
 {
   "CourseName": "B.A. I  (GUJ.MED.)",
   "SubCourse": "GUJ. / HN",
   "Semester": "II",
   "PaperCode": 250201,
   "PaperTitle": "ENGLISH CC PAPER (L.L.) EXPLORING ENGLISH"
 },
 {
   "CourseName": "B.A. I  (GUJ.MED.)",
   "SubCourse": "GUJ. / HN",
   "Semester": "II",
   "PaperCode": 275203,
   "PaperTitle": "AC HINDI PAPER-II, AADHUNIK PADHYA HINDI KAVITA"
 },
 {
   "CourseName": "B.A. I  (GUJ.MED.)",
   "SubCourse": "GUJ. / FN",
   "Semester": "II",
   "PaperCode": 230300,
   "PaperTitle": "PERSONALITY DEVELOPMENT"
 },
 {
   "CourseName": "B.A. I  (GUJ.MED.)",
   "SubCourse": "GUJ. / FN",
   "Semester": "II",
   "PaperCode": 230400,
   "PaperTitle": "ENVIRONMENTAL STUDIES"
 },
 {
   "CourseName": "B.A. I  (GUJ.MED.)",
   "SubCourse": "GUJ. / FN",
   "Semester": "II",
   "PaperCode": 245302,
   "PaperTitle": " GUJARATI PAPER -III PADHYA SWARUP ANE KRUTINO ABHYAS"
 },
 {
   "CourseName": "B.A. I  (GUJ.MED.)",
   "SubCourse": "GUJ. / FN",
   "Semester": "II",
   "PaperCode": 245402,
   "PaperTitle": "GUJARATI PAPER-IV LOKNATYA BHAVAI"
 },
 {
   "CourseName": "B.A. I  (GUJ.MED.)",
   "SubCourse": "GUJ. / FN",
   "Semester": "II",
   "PaperCode": 250201,
   "PaperTitle": "ENGLISH CC PAPER (L.L.) EXPLORING ENGLISH"
 },
 {
   "CourseName": "B.A. I  (GUJ.MED.)",
   "SubCourse": "GUJ. / FN",
   "Semester": "II",
   "PaperCode": 280226,
   "PaperTitle": "CAPC PAPER-II FUNDAMENTAL OF NUTRITION"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "ENG / CD",
   "Semester": "I",
   "PaperCode": 110101,
   "PaperTitle": "ENGLISH CC PAPER (H.L.) EMPOWERING ENGLISH - I"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "ENG / CD",
   "Semester": "I",
   "PaperCode": 130100,
   "PaperTitle": "FC PAPER -I HISTORY AS HERITAGE"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "ENG / CD",
   "Semester": "I",
   "PaperCode": 130200,
   "PaperTitle": "FC PAPER-II WOMEN IN CHANGING INDIA"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "ENG / CD",
   "Semester": "I",
   "PaperCode": 140101,
   "PaperTitle": "DC PAPER -I BASIC CONCEPTS GENRE OF LITERARY STUDY/ INTRODUCTION TO LITERARY STUDIES CONCEPTS & GENRE"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "ENG / CD",
   "Semester": "I",
   "PaperCode": 140201,
   "PaperTitle": "DC PAPER-II EXPLORING LITERARY STUDIES LITERARY TERMS & CRITICAL APPROACHES"
 },
{
"CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "Psy / CD",
   "Semester": "I",
   "PaperCode": 140210,
   "PaperTitle": "DC PSYCHOLOGY PAPER-II DEVELOPMENT PSYCHOLOGY"
},
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "ENG / CD",
   "Semester": "I",
   "PaperCode": 180127,
   "PaperTitle": "CAPC PAPER-I CHILD DEVELOPMENT"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "ENG / FN",
   "Semester": "I",
   "PaperCode": 110101,
   "PaperTitle": "ENGLISH CC PAPER (H.L.) EMPOWERING ENGLISH - I"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "ENG / FN",
   "Semester": "I",
   "PaperCode": 130100,
   "PaperTitle": "FC PAPER -I HISTORY AS HERITAGE"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "ENG / FN",
   "Semester": "I",
   "PaperCode": 130200,
   "PaperTitle": "FC PAPER-II WOMEN IN CHANGING INDIA"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "ENG / FN",
   "Semester": "I",
   "PaperCode": 140101,
   "PaperTitle": "DC PAPER -I BASIC CONCEPTS GENRE OF LITERARY STUDY/ INTRODUCTION TO LITERARY STUDIES CONCEPTS & GENRE"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "ENG / FN",
   "Semester": "I",
   "PaperCode": 140201,
   "PaperTitle": "DC PAPER-II EXPLORING LITERARY STUDIES LITERARY TERMS & CRITICAL APPROACHES"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "ENG / FN",
   "Semester": "I",
   "PaperCode": 180126,
   "PaperTitle": "CAPC PAPER-I FUNDAMENTALS OF FOOD SCIENCE"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "ENG / HIN",
   "Semester": "I",
   "PaperCode": 110101,
   "PaperTitle": "ENGLISH CC PAPER (H.L.) EMPOWERING ENGLISH - I"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "ENG / HIN",
   "Semester": "I",
   "PaperCode": 130100,
   "PaperTitle": "FC PAPER -I HISTORY AS HERITAGE"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "ENG / HIN",
   "Semester": "I",
   "PaperCode": 130200,
   "PaperTitle": "FC PAPER-II WOMEN IN CHANGING INDIA"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "ENG / HIN",
   "Semester": "I",
   "PaperCode": 140101,
   "PaperTitle": "DC PAPER -I BASIC CONCEPTS GENRE OF LITERARY STUDY/ INTRODUCTION TO LITERARY STUDIES CONCEPTS & GENRE"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "ENG / HIN",
   "Semester": "I",
   "PaperCode": 140201,
   "PaperTitle": "DC PAPER-II EXPLORING LITERARY STUDIES LITERARY TERMS & CRITICAL APPROACHES"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "ENG / HIN",
   "Semester": "I",
   "PaperCode": 175103,
   "PaperTitle": "AC HINDI PAPER-I AADHUNIK GADHYA HINDI KAHANI"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "PSY / CD",
   "Semester": "I",
   "PaperCode": 110101,
   "PaperTitle": "ENGLISH CC PAPER (H.L.) EMPOWERING ENGLISH - I"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "PSY / CD",
   "Semester": "I",
   "PaperCode": 130100,
   "PaperTitle": "FC PAPER -I HISTORY AS HERITAGE"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "PSY / CD",
   "Semester": "I",
   "PaperCode": 130200,
   "PaperTitle": "FC PAPER-II WOMEN IN CHANGING INDIA"
 },
{
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "PSY / CD",
   "Semester": "I",
   "PaperCode": 140110,
   "PaperTitle": "DC PSYCHOLOGY PAPER-I GENERAL PSYCHOLOGY - I"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "PSY / CD",
   "Semester": "I",
   "PaperCode": 145110,
   "PaperTitle": "DC PSYCHOLOGY PAPER-I GENERAL PSYCHOLOGY"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "PSY / CD",
   "Semester": "I",
   "PaperCode": 145210,
   "PaperTitle": "DC PSYCHOLOGY PAPER-II DEVELOPMENT PSYCHOLOGY"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "PSY / CD",
   "Semester": "I",
   "PaperCode": 180127,
   "PaperTitle": "CAPC PAPER-I  CHILD DEVELOPMENT"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "PSY / FN",
   "Semester": "I",
   "PaperCode": 110101,
   "PaperTitle": "ENGLISH CC PAPER (H.L.) EMPOWERING ENGLISH - I"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "PSY / FN",
   "Semester": "I",
   "PaperCode": 130100,
   "PaperTitle": "FC PAPER -I HISTORY AS HERITAGE"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "PSY / FN",
   "Semester": "I",
   "PaperCode": 130200,
   "PaperTitle": "FC PAPER-II WOMEN IN CHANGING INDIA"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "PSY / FN",
   "Semester": "I",
   "PaperCode": 145110,
   "PaperTitle": "DC PSYCHOLOGY PAPER-I GENERAL PSYCHOLOGY"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "PSY / FN",
   "Semester": "I",
   "PaperCode": 145210,
   "PaperTitle": "DC PSYCHOLOGY PAPER-II DEVELOPMENT PSYCHOLOGY"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "PSY / FN",
   "Semester": "I",
   "PaperCode": 180126,
   "PaperTitle": "CAPC PAPER-I FUNDAMENTALS OF FOOD SCIENCE"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "SOCIO /CD",
   "Semester": "I",
   "PaperCode": 110101,
   "PaperTitle": "ENGLISH CC PAPER (H.L.) EMPOWERING ENGLISH - I"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "SOCIO /CD",
   "Semester": "I",
   "PaperCode": 130100,
   "PaperTitle": "FC PAPER -I HISTORY AS HERITAGE"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "SOCIO /CD",
   "Semester": "I",
   "PaperCode": 130200,
   "PaperTitle": "FC PAPER-II WOMEN IN CHANGING INDIA"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "SOCIO /CD",
   "Semester": "I",
   "PaperCode": 145111,
   "PaperTitle": "SOCIOLOGY PAPER-I SOCIOLOGY OF INDIAN SOCIETY"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "SOCIO /CD",
   "Semester": "I",
   "PaperCode": 145211,
   "PaperTitle": "SOCIOLOGY PAPER-II FOUNDATION TO SOCIOLOGY"
 },
{
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "SOCIO /CD",
   "Semester": "I",
   "PaperCode": 140211,
   "PaperTitle": "DC SOCIOLOGY PAPER-II Introduction to Sociology"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "SOCIO /CD",
   "Semester": "I",
   "PaperCode": 180127,
   "PaperTitle": "CAPC PAPER-I CHILD DEVELOPMENT"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "SOCIO/ FN",
   "Semester": "I",
   "PaperCode": 110101,
   "PaperTitle": "ENGLISH CC PAPER (H.L.) EMPOWERING ENGLISH - I"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "SOCIO/ FN",
   "Semester": "I",
   "PaperCode": 130100,
   "PaperTitle": "FC PAPER -I HISTORY AS HERITAGE"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "SOCIO/ FN",
   "Semester": "I",
   "PaperCode": 130200,
   "PaperTitle": "FC PAPER-II WOMEN IN CHANGING INDIA"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "SOCIO/ FN",
   "Semester": "I",
   "PaperCode": 145111,
   "PaperTitle": "SOCIOLOGY PAPER-I SOCIOLOGY OF INDIAN SOCIETY"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "SOCIO/ FN",
   "Semester": "I",
   "PaperCode": 145211,
   "PaperTitle": "SOCIOLOGY PAPER-II FOUNDATIONS TO SOCIOLOGY"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "SOCIO/ FN",
   "Semester": "I",
   "PaperCode": 180126,
   "PaperTitle": "CAPC PAPER-I FUNDAMENTAL OF FOOD SCIENCE"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "SOCIO / HN",
   "Semester": "I",
   "PaperCode": 110101,
   "PaperTitle": "ENGLISH CC PAPER (H.L.) EMPOWERING ENGLISH - I"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "SOCIO / HN",
   "Semester": "I",
   "PaperCode": 130100,
   "PaperTitle": "FC PAPER -I HISTORY AS HERITAGE"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "SOCIO / HN",
   "Semester": "I",
   "PaperCode": 130200,
   "PaperTitle": "FC PAPER-II WOMEN IN CHANGING INDIA"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "SOCIO / HN",
   "Semester": "I",
   "PaperCode": 145111,
   "PaperTitle": "SOCIOLOGY PAPER-I SOCIOLOGY OF INDIAN SOCIETY"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "SOCIO / HN",
   "Semester": "I",
   "PaperCode": 145211,
   "PaperTitle": "SOCIOLOGY PAPER-II FOUNDATIONS TO SOCIOLOGY"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "SOCIO / HN",
   "Semester": "I",
   "PaperCode": 175103,
   "PaperTitle": "AC HINDI PAPER-I AADHUNIK GADHYA HINDI KAHANI"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "ENG / CD",
   "Semester": "II",
   "PaperCode": 210201,
   "PaperTitle": "ENGLISH CC PAPER (H.L.) EMPOWERING ENGLISH- II"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "ENG / CD",
   "Semester": "II",
   "PaperCode": 230300,
   "PaperTitle": "PERSONALITY DEVELOPMENT"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "ENG / CD",
   "Semester": "II",
   "PaperCode": 230400,
   "PaperTitle": "ENVIRONMENTAL STUDIES"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "ENG / CD",
   "Semester": "II",
   "PaperCode": 240301,
   "PaperTitle": "ENGLISH PAPER-III THE NINETEENTH CENTURY NOVEL."
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "ENG / CD",
   "Semester": "II",
   "PaperCode": 240401,
   "PaperTitle": "ENGLISH PAPER-IV ROMANTIC & VICTORIAN POETRY"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "ENG / CD",
   "Semester": "II",
   "PaperCode": 280227,
   "PaperTitle": "CAPC PAPER-II - ADOLESCENT DEVELOPMENT"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "ENG / FN",
   "Semester": "II",
   "PaperCode": 210201,
   "PaperTitle": "ENGLISH CC PAPER (H.L.) EMPOWERING ENGLISH- II"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "ENG / FN",
   "Semester": "II",
   "PaperCode": 230300,
   "PaperTitle": "PERSONALITY DEVELOPMENT"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "ENG / FN",
   "Semester": "II",
   "PaperCode": 230400,
   "PaperTitle": "ENVIRONMENTAL STUDIES"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "ENG / FN",
   "Semester": "II",
   "PaperCode": 240301,
   "PaperTitle": "ENGLISH PAPER-III THE NINETEENTH CENTURY NOVEL."
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "ENG / FN",
   "Semester": "II",
   "PaperCode": 240401,
   "PaperTitle": "ENGLISH PAPER-IV ROMANTIC & VICTORIAN POETRY"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "ENG / FN",
   "Semester": "II",
   "PaperCode": 280226,
   "PaperTitle": "CAPC PAPER-II - FUNDAMENTALS OF NUTRITION"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "ENG / HIN",
   "Semester": "II",
   "PaperCode": 210201,
   "PaperTitle": "ENGLISH CC PAPER (H.L.) EMPOWERING ENGLISH- II"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "ENG / HIN",
   "Semester": "II",
   "PaperCode": 230300,
   "PaperTitle": "PERSONALITY DEVELOPMENT"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "ENG / HIN",
   "Semester": "II",
   "PaperCode": 230400,
   "PaperTitle": "ENVIRONMENTAL STUDIES"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "ENG / HIN",
   "Semester": "II",
   "PaperCode": 240301,
   "PaperTitle": "ENGLISH PAPER-III THE NINETEENTH CENTURY NOVEL."
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "ENG / HIN",
   "Semester": "II",
   "PaperCode": 240401,
   "PaperTitle": "ENGLISH PAPER-IV ROMANTIC & VICTORIAN POETRY"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "ENG / HIN",
   "Semester": "II",
   "PaperCode": 275203,
   "PaperTitle": "AC HINDI PAPER-II, AADHUNIK PADHYA HINDI KAVITA"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "PSY / CD",
   "Semester": "II",
   "PaperCode": 210201,
   "PaperTitle": "ENGLISH CC PAPER (H.L.) EMPOWERING ENGLISH- II"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "PSY / CD",
   "Semester": "II",
   "PaperCode": 230300,
   "PaperTitle": "PERSONALITY DEVELOPMENT"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "PSY / CD",
   "Semester": "II",
   "PaperCode": 230400,
   "PaperTitle": "ENVIRONMENTAL STUDIES"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "PSY / CD",
   "Semester": "II",
   "PaperCode": 245310,
   "PaperTitle": "PSYCHOLOGY PAPER-III GENERAL PSYCHOLOGY-BASIC COGNITIVE PROCESS"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "PSY / CD",
   "Semester": "II",
   "PaperCode": 245410,
   "PaperTitle": "PSYCHOLOGY PAPER-IV DEVELOPMENTAL PSYCHOLOGY "
 },
{
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "PSY / CD",
   "Semester": "II",
   "PaperCode": 240310,
   "PaperTitle": "PSYCHOLOGY PAPER-III GENERAL PSYCHOLOGY-BASIC COGNITIVE PROCESS"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "PSY / CD",
   "Semester": "II",
   "PaperCode": 240410,
   "PaperTitle": "PSYCHOLOGY PAPER-IV DEVELOPMENTAL PSYCHOLOGY "
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "PSY / CD",
   "Semester": "II",
   "PaperCode": 280227,
   "PaperTitle": "CAPC PAPER-II- ADOLESCENT DEVELOPMENT"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "PSY / FN",
   "Semester": "II",
   "PaperCode": 210201,
   "PaperTitle": "ENGLISH CC PAPER (H.L.) EMPOWERING ENGLISH- II"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "PSY / FN",
   "Semester": "II",
   "PaperCode": 230300,
   "PaperTitle": "PERSONALITY DEVELOPMENT"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "PSY / FN",
   "Semester": "II",
   "PaperCode": 230400,
   "PaperTitle": "ENVIRONMENTAL STUDIES"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "PSY / FN",
   "Semester": "II",
   "PaperCode": 245310,
   "PaperTitle": "PSYCHOLOGY PAPER-III GENERAL PSYCHOLOGY-BASIC COGNITIVE PROCESS"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "PSY / FN",
   "Semester": "II",
   "PaperCode": 245410,
   "PaperTitle": "PSYCHOLOGY PAPER-IV DEVELOPMENTAL PSYCHOLOGY "
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "PSY / FN",
   "Semester": "II",
   "PaperCode": 280226,
   "PaperTitle": "CAPC PAPER-II- FUNDAMENTALS OF NUTRITION"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "SOCIO /CD",
   "Semester": "II",
   "PaperCode": 210201,
   "PaperTitle": "ENGLISH CC PAPER (H.L.) EMPOWERING ENGLISH- II"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "SOCIO /CD",
   "Semester": "II",
   "PaperCode": 230300,
   "PaperTitle": "PERSONALITY DEVELOPMENT"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "SOCIO /CD",
   "Semester": "II",
   "PaperCode": 230400,
   "PaperTitle": "ENVIRONMENTAL STUDIES"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "SOCIO /CD",
   "Semester": "II",
   "PaperCode": 245311,
   "PaperTitle": "SOCIOLOGY PAPER-III SOCIAL PROBLEM IN INDIA"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "SOCIO /CD",
   "Semester": "II",
   "PaperCode": 245411,
   "PaperTitle": "SOCIOLOGY PAPER-IV INTRODUCTION TO SOCIOLOGY"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "SOCIO /CD",
   "Semester": "II",
   "PaperCode": 280227,
   "PaperTitle": "CAPC PAPER-II- ADOLESCENT DEVELOPMENT"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "SOCIO/ FN",
   "Semester": "II",
   "PaperCode": 210201,
   "PaperTitle": "ENGLISH CC PAPER (H.L.) EMPOWERING ENGLISH- II"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "SOCIO/ FN",
   "Semester": "II",
   "PaperCode": 230300,
   "PaperTitle": "PERSONALITY DEVELOPMENT"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "SOCIO/ FN",
   "Semester": "II",
   "PaperCode": 230400,
   "PaperTitle": "ENVIRONMENTAL STUDIES"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "SOCIO/ FN",
   "Semester": "II",
   "PaperCode": 245311,
   "PaperTitle": "SOCIOLOGY PAPER-III SOCIAL PROBLEM IN INDIA"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "SOCIO/ FN",
   "Semester": "II",
   "PaperCode": 245411,
   "PaperTitle": "SOCIOLOGY PAPER-IV INTRODUCTION TO SOCIOLOGY"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "SOCIO/ FN",
   "Semester": "II",
   "PaperCode": 280226,
   "PaperTitle": "CAPC PAPER-II- FUNDAMENTALS OF NUTRITION"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "SOCIO / HN",
   "Semester": "II",
   "PaperCode": 210201,
   "PaperTitle": "ENGLISH CC PAPER (H.L.) EMPOWERING ENGLISH- II"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "SOCIO / HN",
   "Semester": "II",
   "PaperCode": 230300,
   "PaperTitle": "PERSONALITY DEVELOPMENT"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "SOCIO / HN",
   "Semester": "II",
   "PaperCode": 230400,
   "PaperTitle": "ENVIRONMENTAL STUDIES"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "SOCIO / HN",
   "Semester": "II",
   "PaperCode": 245311,
   "PaperTitle": "SOCIOLOGY PAPER-III SOCIAL PROBLEM IN INDIA"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "SOCIO / HN",
   "Semester": "II",
   "PaperCode": 245411,
   "PaperTitle": "SOCIOLOGY PAPER-IV INTRODUCTION TO SOCIOLOGY"
 },
 {
   "CourseName": "B.A. I  (ENG.MED.)",
   "SubCourse": "SOCIO / HN",
   "Semester": "II",
   "PaperCode": 275203,
   "PaperTitle": "AC HINDI PAPER-II, AADHUNIK PADHYA HINDI KAVITA"
 },
 {
   "CourseName": "B.COM. I",
   "SubCourse": "REGULAR",
   "Semester": "I",
   "PaperCode": 110101,
   "PaperTitle": "CC ENGLISH PAPER-I"
 },
 {
   "CourseName": "B.COM. I",
   "SubCourse": "REGULAR",
   "Semester": "I",
   "PaperCode": 140105,
   "PaperTitle": "ECONOMICS PAPER-I"
 },
 {
   "CourseName": "B.COM. I",
   "SubCourse": "REGULAR",
   "Semester": "I",
   "PaperCode": 140106,
   "PaperTitle": "PRINCIPLES OF BUSINESS MANAGEMENT"
 },
 {
   "CourseName": "B.COM. I",
   "SubCourse": "REGULAR",
   "Semester": "I",
   "PaperCode": 140107,
   "PaperTitle": "ACCOUNTING-I FINANCIAL ACCOUNTING"
 },
{
   "CourseName": "B.COM. I",
   "SubCourse": "REGULAR",
   "Semester": "I",
   "PaperCode": 145107,
   "PaperTitle": "ACCOUNTANCY PAPER-I FINANCIAL ACCOUNTING"
 },
 {
   "CourseName": "B.COM. I",
   "SubCourse": "REGULAR",
   "Semester": "I",
   "PaperCode": 140108,
   "PaperTitle": "BUSINESS MATHEMATICS & STATISTICS"
 },
{
   "CourseName": "B.COM. I",
   "SubCourse": "REGULAR",
   "Semester": "I",
   "PaperCode": 145108,
   "PaperTitle": "BUSINESS MATHEMATICS PAPER - I"
 },
 {
   "CourseName": "B.COM. I",
   "SubCourse": "REGULAR",
   "Semester": "I",
   "PaperCode": 140109,
   "PaperTitle": "COMMERCIAL GEOGRAPHY"
 },
{
   "CourseName": "B.COM. I",
   "SubCourse": "REGULAR",
   "Semester": "I",
   "PaperCode": 145109,
   "PaperTitle": "BUSINESS ENVIRONMENT"
 },
 {
   "CourseName": "B.COM. I",
   "SubCourse": "OMSP",
   "Semester": "I",
   "PaperCode": 100152,
   "PaperTitle": "SHORTHAND & TYPING PAPER-I"
 },
{
   "CourseName": "B.COM. I",
   "SubCourse": "OMSP",
   "Semester": "I",
   "PaperCode": 105152,
   "PaperTitle": "OFFICE MANAGEMENT AND SECRETARIAL PRACTICE - PAPER-I TYPING-I"
 },
 {
   "CourseName": "B.COM. I",
   "SubCourse": "OMSP",
   "Semester": "I",
   "PaperCode": 100252,
   "PaperTitle": "OFFICE MANAGEMENT PAPER-II"
 },
{
   "CourseName": "B.COM. I",
   "SubCourse": "OMSP",
   "Semester": "I",
   "PaperCode": 105252,
   "PaperTitle": "OFFICE MANAGEMENT AND SECRETARIAL PRACTICE - PAPER-II THEORY-I"
 },


 {
   "CourseName": "B.COM. I",
   "SubCourse": "OMSP",
   "Semester": "I",
   "PaperCode": 110101,
   "PaperTitle": "CC ENGLISH PAPER-I"
 },
 {
   "CourseName": "B.COM. I",
   "SubCourse": "OMSP",
   "Semester": "I",
   "PaperCode": 140105,
   "PaperTitle": "ECONOMICS PAPER-I"
 },
 {
   "CourseName": "B.COM. I",
   "SubCourse": "OMSP",
   "Semester": "I",
   "PaperCode": 140106,
   "PaperTitle": "PRINCIPLES OF BUSINESS MANAGEMENT"
 },
 {
   "CourseName": "B.COM. I",
   "SubCourse": "OMSP",
   "Semester": "I",
   "PaperCode": 140107,
   "PaperTitle": "ACCOUNTING-I FINANCIAL ACCOUNTING"
 },
 {
   "CourseName": "B.COM. I",
   "SubCourse": "T.T.M.",
   "Semester": "I",
   "PaperCode": 100124,
   "PaperTitle": "TOURISM BUSINESS PAPER-I"
 },
{
   "CourseName": "B.COM. I",
   "SubCourse": "T.T.M.",
   "Semester": "I",
   "PaperCode": 105124,
   "PaperTitle": "TOURISM & TRAVEL MANAGEMENT TOURISM BUSINESS (PAPER - I)"
 },
 {
   "CourseName": "B.COM. I",
   "SubCourse": "T.T.M.",
   "Semester": "I",
   "PaperCode": 100224,
   "PaperTitle": "TOURISM ORGANISATIONS PAPER-II"
 },
{
   "CourseName": "B.COM. I",
   "SubCourse": "T.T.M.",
   "Semester": "I",
   "PaperCode": 105224,
   "PaperTitle": "TOURISM & TRAVEL MANAGEMENT TOURISM ORGANISATIONS (PAPER-II)"
 },
 {
   "CourseName": "B.COM. I",
   "SubCourse": "T.T.M.",
   "Semester": "I",
   "PaperCode": 110101,
   "PaperTitle": "CC ENGLISH PAPER-I"
 },
 {
   "CourseName": "B.COM. I",
   "SubCourse": "T.T.M.",
   "Semester": "I",
   "PaperCode": 140105,
   "PaperTitle": "ECONOMICS PAPER-I"
 },

 {
   "CourseName": "B.COM. I",
   "SubCourse": "T.T.M.",
   "Semester": "I",
   "PaperCode": 145105,
   "PaperTitle": "BUSINESS ECONOMICS PAPER I THEORY OF CONSUMER BEHAVIOUR AND DEMAND"
 },
 {
   "CourseName": "B.COM. I",
   "SubCourse": "T.T.M.",
   "Semester": "I",
   "PaperCode": 140106,
   "PaperTitle": "PRINCIPLES OF BUSINESS MANAGEMENT"
 },
 {
   "CourseName": "B.COM. I",
   "SubCourse": "T.T.M.",
   "Semester": "I",
   "PaperCode": 140107,
   "PaperTitle": "ACCOUNTING-I FINANCIAL ACCOUNTING"
 },
 {
   "CourseName": "B.COM. I",
   "SubCourse": "REGULAR",
   "Semester": "II",
   "PaperCode": 210201,
   "PaperTitle": "ENGLISH CC (H.L.)"
 },
 {
   "CourseName": "B.COM. I",
   "SubCourse": "REGULAR",
   "Semester": "II",
   "PaperCode": 240205,
   "PaperTitle": "BUSINESS ECONOMICS"
 },
 {
   "CourseName": "B.COM. I",
   "SubCourse": "REGULAR",
   "Semester": "II",
   "PaperCode": 240206,
   "PaperTitle": "COMMERCE"
 },
 {
   "CourseName": "B.COM. I",
   "SubCourse": "REGULAR",
   "Semester": "II",
   "PaperCode": 240207,
   "PaperTitle": "ACCOUNTANCY"
 },
 {
   "CourseName": "B.COM. I",
   "SubCourse": "REGULAR",
   "Semester": "II",
   "PaperCode": 240208,
   "PaperTitle": "BUSINESS STATISTICS"
 },
 {
   "CourseName": "B.COM. I",
   "SubCourse": "REGULAR",
   "Semester": "II",
   "PaperCode": 240209,
   "PaperTitle": "ENVIRONMENTAL STUDIES"
 },
 {
   "CourseName": "B.COM. I",
   "SubCourse": "OMSP",
   "Semester": "II",
   "PaperCode": 200352,
   "PaperTitle": "OMSP"
 },
 {
   "CourseName": "B.COM. I",
   "SubCourse": "OMSP",
   "Semester": "II",
   "PaperCode": 210201,
   "PaperTitle": "ENGLISH CC "
 },
 {
   "CourseName": "B.COM. I",
   "SubCourse": "OMSP",
   "Semester": "II",
   "PaperCode": 240205,
   "PaperTitle": "ECONOMICS"
 },
 {
   "CourseName": "B.COM. I",
   "SubCourse": "OMSP",
   "Semester": "II",
   "PaperCode": 240206,
   "PaperTitle": "COMMERCE"
 },
 {
   "CourseName": "B.COM. I",
   "SubCourse": "OMSP",
   "Semester": "II",
   "PaperCode": 240207,
   "PaperTitle": "ACCOUNTANCY"
 },
 {
   "CourseName": "B.COM. I",
   "SubCourse": "OMSP",
   "Semester": "II",
   "PaperCode": 240209,
   "PaperTitle": "ENVIRONMENTAL STUDIES"
 },
 {
   "CourseName": "B.COM. I",
   "SubCourse": "T.T.M.",
   "Semester": "II",
   "PaperCode": 200324,
   "PaperTitle": "TTM"
 },
 {
   "CourseName": "B.COM. I",
   "SubCourse": "T.T.M.",
   "Semester": "II",
   "PaperCode": 210201,
   "PaperTitle": "ENGLISH CC "
 },
 {
   "CourseName": "B.COM. I",
   "SubCourse": "T.T.M.",
   "Semester": "II",
   "PaperCode": 240205,
   "PaperTitle": "BUSINESS ECONOMICS"
 },
 {
   "CourseName": "B.COM. I",
   "SubCourse": "T.T.M.",
   "Semester": "II",
   "PaperCode": 204206,
   "PaperTitle": "COMMERCE"
 },
 {
   "CourseName": "B.COM. I",
   "SubCourse": "T.T.M.",
   "Semester": "II",
   "PaperCode": 240207,
   "PaperTitle": "ACCOUNTANCY"
 },
 {
   "CourseName": "B.COM. I",
   "SubCourse": "T.T.M.",
   "Semester": "II",
   "PaperCode": 240209,
   "PaperTitle": "ENVIRONMENTAL STUDIES"
 }
];