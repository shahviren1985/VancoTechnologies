var PaperPerMarksheet = 5;
var TotalMarks = "500";
var MarkSheetDate = "01/12/2018";
//var MarkSheetDate = "26/04/2017"; //24/11/2017 
var is35Passing = true;
//var CourseExamName = "BACHELOR OF ARTS (B.A.) Semester-II";
//var CourseExamName = "BACHELOR OF COMMERCE (B.Com) Semester-II";
var CourseExamName = "BACHELOR OF COMMERCE WITH ACCOUNTANCY, FINANCE AND INSURANCE";
//var CourseExamName = "BACHELOR OF MANAGEMENT STUDIES (B.M.S) Semester - I";

////var ExamYear = "October/November - 2017";
//var ExamYear = "March - 2015";
var ExamYear = "October/November - 2018";
var Medium = "ENGLISH";
//var Medium = "GUJARATI";
var Institution = "027";
var Center = "008 - VILE PARLE (W)";
var Place = "Mumbai";
var PaperCodes = 
{
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
	"200324":{"PaperTitle":"TTM","Credits":"4"},
	"200352":{"PaperTitle":"OMSP","Credits":"4"},
	"204206":{"PaperTitle":"COMMERCE","Credits":"4"},
	"210201":{"PaperTitle":"ENGLISH CC PAPER (H.L.) EMPOWERING ENGLISH- II","Credits":"4"},
	//"      ":{"PaperTitle":"ENGLISH CC PAPER (H.L.) EMPOWERING ENGLISH- II","Credits":"4"},
	"230300":{"PaperTitle":"PERSONALITY DEVELOPMENT","Credits":"4"},
	"230400":{"PaperTitle":"ENVIRONMENTAL STUDIES","Credits":"4"},
	"240205":{"PaperTitle":"BUSINESS ECONOMICS","Credits":"4"},
	"240206":{"PaperTitle":"COMMERCE","Credits":"4"},
	"240207":{"PaperTitle":"ACCOUNTANCY","Credits":"4"},
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
	"240519":{"PaperTitle":"RECENT TRENDS IN MANAGEMENT","Credits":"4"},
	"1001":{"PaperTitle":"PRINCIPLES OF MANAGEMENT","Credits":"4"},
	"1002":{"PaperTitle":"BUSINESS COMMUNICATION","Credits":"4"},
	"1003":{"PaperTitle":"FINANCIAL ACCOUNTING","Credits":"4"},
	"1004":{"PaperTitle":"PRINCIPLES OF MARKETING","Credits":"4"},
	"1005":{"PaperTitle":"ECONOMICS - I","Credits":"4"},
	"140119":{"PaperTitle":"BASICS OF ACCOUNTANCY","Credits":"4"},
	"140219":{"PaperTitle":"FINANCIAL ACCOUNTANCY","Credits":"4"},
	"140319":{"PaperTitle":"FOUNDATION COURSE & INFORMATION TECHNOLOGY","Credits":"4"},
	"140419":{"PaperTitle":"BASICS OF BUSINESS COMMUNICATION","Credits":"4"},
	"140519":{"PaperTitle":"MANAGEMENT THEORY & PRACTICE","Credits":"4"}
};

/*{
	"110101" : { "PaperTitle":"ENGLISH CC PAPER (H.L.) EMPOWERING ENGLISH", "Credits":"4" },
	"130100" : { "PaperTitle":"FC PAPER I-HISTORY AS HERITAGE", "Credits":"4" },
	"130200" : { "PaperTitle":"FC PAPER II-WOMEN IN CHANGING INDIA", "Credits":"4" },
	"140101" : { "PaperTitle":"DC PAPER-I BASIC CONCEPTS IN LITERA...", "Credits":"4"},
	"140201" : { "PaperTitle":"DC PAPER-II EXPLORING LITERARY STUDIES", "Credits":"4"},
	"180127":{ "PaperTitle":"CAPC PAPER-I CHILD DEVELOPMENT", "Credits":"4"}
};*/
