using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeExamService.Models
{
    public class MarksEntryViewModel
    {
        [JsonProperty("CollegeRegistrationNumber")]
        public string CollegeRegistrationNumber { get; set; }

        [JsonProperty("SeatNumber")]
        public string SeatNumber { get; set; }

        [JsonProperty("RollNumber")]
        public int RollNumber { get; set; }

        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [JsonProperty("FatherName")]
        public string FatherName { get; set; }

        [JsonProperty("MotherName")]
        public string MotherName { get; set; }

        [JsonProperty("PRN")]
        public string PRN { get; set; }

        [JsonProperty("Course")]
        public string Course { get; set; }

        [JsonProperty("Specialisation")]
        public string Specialisation { get; set; }

        [JsonProperty("SubCourse")]
        public string SubCourse { get; set; }

        [JsonProperty("ExamType")]
        public string ExamType { get; set; }

        [JsonProperty("Semester")]
        public int Semester { get; set; }

        [JsonProperty("Date")]
        public string Date { get; set; }

        [JsonProperty("TotalCredits")]
        public string TotalCredits { get; set; }

        [JsonProperty("GPA")]
        public string GPA { get; set; }

        [JsonProperty("OverallGrade")]
        public string OverallGrade { get; set; }

        [JsonProperty("GrandTotal")]
        public string GrandTotal { get; set; }

        [JsonProperty("Result")]
        public string Result { get; set; }

        [JsonProperty("Remarks")]
        public string Remarks { get; set; }

        [JsonProperty("Percentage")]
        public string Percentage { get; set; }

        [JsonProperty("Paper1Appeared")]
        public string Paper1Appeared { get; set; }

        [JsonProperty("Code1")]
        public string Code1 { get; set; }

        [JsonProperty("Credit1")]
        public string Credit1 { get; set; }

        [JsonProperty("InternalC1")]
        public float InternalC1 { get; set; }

        [JsonProperty("ExternalSection1C1")]
        public string ExternalSection1C1 { get; set; }

        [JsonProperty("ExternalSection2C1")]
        public string ExternalSection2C1 { get; set; }

        [JsonProperty("ExternalTotalC1")]
        public float ExternalTotalC1 { get; set; }

        [JsonProperty("PracticalMarksC1")]
        public string PracticalMarksC1 { get; set; }

        [JsonProperty("Grace1")]
        public string Grace1 { get; set; }

        [JsonProperty("Total1")]
        public float Total1 { get; set; }

        [JsonProperty("Grade1")]
        public string Grade1 { get; set; }

        [JsonProperty("PaperTitle1")]
        public string PaperTitle1 { get; set; }

        [JsonProperty("PaperType1")]
        public string PaperType1 { get; set; }

        [JsonProperty("IsElective1")]
        public bool IsElective1 { get; set; }

        [JsonProperty("InternalPassingMarks1")]
        public int InternalPassingMarks1 { get; set; }

        [JsonProperty("InternalTotalMarks1")]
        public int InternalTotalMarks1 { get; set; }

        [JsonProperty("ExternalPassingMarks1")]
        public int ExternalPassingMarks1 { get; set; }

        [JsonProperty("PracticalMaxMarks1")]
        public int PracticalMaxMarks1 { get; set; }

        [JsonProperty("ExternalMaxMarks1")]
        public int ExternalMaxMarks1 { get; set; }

        [JsonProperty("GP1")]
        public string GP1 { get; set; }

        [JsonProperty("Attempt1")]
        public string Attempt1 { get; set; }

        [JsonProperty("Paper2Appeared")]
        public string Paper2Appeared { get; set; }

        [JsonProperty("Code2")]
        public string Code2 { get; set; }

        [JsonProperty("Credit2")]
        public string Credit2 { get; set; }

        [JsonProperty("InternalC2")]
        public float InternalC2 { get; set; }

        [JsonProperty("ExternalSection1C2")]
        public string ExternalSection1C2 { get; set; }

        [JsonProperty("ExternalSection2C2")]
        public string ExternalSection2C2 { get; set; }

        [JsonProperty("ExternalTotalC2")]
        public float ExternalTotalC2 { get; set; }

        [JsonProperty("PracticalMarksC2")]
        public string PracticalMarksC2 { get; set; }

        [JsonProperty("Grace2")]
        public string Grace2 { get; set; }

        [JsonProperty("Total2")]
        public float Total2 { get; set; }

        [JsonProperty("Grade2")]
        public string Grade2 { get; set; }

        [JsonProperty("PaperTitle2")]
        public string PaperTitle2 { get; set; }

        [JsonProperty("PaperType2")]
        public string PaperType2 { get; set; }

        [JsonProperty("IsElective2")]
        public bool IsElective2 { get; set; }

        [JsonProperty("InternalPassingMarks2")]
        public int InternalPassingMarks2 { get; set; }

        [JsonProperty("InternalTotalMarks2")]
        public int InternalTotalMarks2 { get; set; }

        [JsonProperty("ExternalPassingMarks2")]
        public int ExternalPassingMarks2 { get; set; }

        [JsonProperty("PracticalMaxMarks2")]
        public int PracticalMaxMarks2 { get; set; }

        [JsonProperty("ExternalMaxMarks2")]
        public int ExternalMaxMarks2 { get; set; }

        [JsonProperty("GP2")]
        public string GP2 { get; set; }

        [JsonProperty("Attempt2")]
        public string Attempt2 { get; set; }

        [JsonProperty("Paper3Appeared")]
        public string Paper3Appeared { get; set; }

        [JsonProperty("Credit3")]
        public string Credit3 { get; set; }

        [JsonProperty("Code3")]
        public string Code3 { get; set; }

        [JsonProperty("InternalC3")]
        public float InternalC3 { get; set; }

        [JsonProperty("ExternalSection1C3")]
        public string ExternalSection1C3 { get; set; }

        [JsonProperty("ExternalSection2C3")]
        public string ExternalSection2C3 { get; set; }

        [JsonProperty("ExternalTotalC3")]
        public float ExternalTotalC3 { get; set; }

        [JsonProperty("PracticalMarksC3")]
        public string PracticalMarksC3 { get; set; }

        [JsonProperty("Grace3")]
        public string Grace3 { get; set; }

        [JsonProperty("Total3")]
        public float Total3 { get; set; }

        [JsonProperty("Grade3")]
        public string Grade3 { get; set; }

        [JsonProperty("PaperTitle3")]
        public string PaperTitle3 { get; set; }

        [JsonProperty("PaperType3")]
        public string PaperType3 { get; set; }

        [JsonProperty("IsElective3")]
        public bool IsElective3 { get; set; }

        [JsonProperty("InternalPassingMarks3")]
        public int InternalPassingMarks3 { get; set; }

        [JsonProperty("InternalTotalMarks3")]
        public int InternalTotalMarks3 { get; set; }

        [JsonProperty("ExternalPassingMarks3")]
        public int ExternalPassingMarks3 { get; set; }

        [JsonProperty("PracticalMaxMarks3")]
        public int PracticalMaxMarks3 { get; set; }

        [JsonProperty("ExternalMaxMarks3")]
        public int ExternalMaxMarks3 { get; set; }

        [JsonProperty("GP3")]
        public string GP3 { get; set; }

        [JsonProperty("Attempt3")]
        public string Attempt3 { get; set; }

        [JsonProperty("Paper4Appeared")]
        public string Paper4Appeared { get; set; }

        [JsonProperty("Code4")]
        public string Code4 { get; set; }

        [JsonProperty("Credit4")]
        public string Credit4 { get; set; }

        [JsonProperty("InternalC4")]
        public float InternalC4 { get; set; }

        [JsonProperty("ExternalSection1C4")]
        public string ExternalSection1C4 { get; set; }

        [JsonProperty("ExternalSection2C4")]
        public string ExternalSection2C4 { get; set; }

        [JsonProperty("ExternalTotalC4")]
        public float ExternalTotalC4 { get; set; }

        [JsonProperty("PracticalMarksC4")]
        public string PracticalMarksC4 { get; set; }

        [JsonProperty("Grace4")]
        public string Grace4 { get; set; }

        [JsonProperty("Total4")]
        public float Total4 { get; set; }

        [JsonProperty("Grade4")]
        public string Grade4 { get; set; }

        [JsonProperty("PaperTitle4")]
        public string PaperTitle4 { get; set; }

        [JsonProperty("PaperType4")]
        public string PaperType4 { get; set; }

        [JsonProperty("IsElective4")]
        public bool IsElective4 { get; set; }

        [JsonProperty("InternalPassingMarks4")]
        public int InternalPassingMarks4 { get; set; }

        [JsonProperty("InternalTotalMarks4")]
        public int InternalTotalMarks4 { get; set; }

        [JsonProperty("ExternalPassingMarks4")]
        public int ExternalPassingMarks4 { get; set; }

        [JsonProperty("PracticalMaxMarks4")]
        public int PracticalMaxMarks4 { get; set; }

        [JsonProperty("ExternalMaxMarks4")]
        public int ExternalMaxMarks4 { get; set; }

        [JsonProperty("GP4")]
        public string GP4 { get; set; }

        [JsonProperty("Attempt4")]
        public string Attempt4 { get; set; }

        [JsonProperty("Paper5Appeared")]
        public string Paper5Appeared { get; set; }

        [JsonProperty("Code5")]
        public string Code5 { get; set; }

        [JsonProperty("Credit5")]
        public string Credit5 { get; set; }

        [JsonProperty("InternalC5")]
        public float InternalC5 { get; set; }

        [JsonProperty("ExternalSection1C5")]
        public string ExternalSection1C5 { get; set; }

        [JsonProperty("ExternalSection2C5")]
        public string ExternalSection2C5 { get; set; }

        [JsonProperty("ExternalTotalC5")]
        public float ExternalTotalC5 { get; set; }

        [JsonProperty("PracticalMarksC5")]
        public string PracticalMarksC5 { get; set; }

        [JsonProperty("Grace5")]
        public string Grace5 { get; set; }

        [JsonProperty("Total5")]
        public float Total5 { get; set; }

        [JsonProperty("Grade5")]
        public string Grade5 { get; set; }

        [JsonProperty("PaperTitle5")]
        public string PaperTitle5 { get; set; }

        [JsonProperty("PaperType5")]
        public string PaperType5 { get; set; }

        [JsonProperty("IsElective5")]
        public bool IsElective5 { get; set; }

        [JsonProperty("InternalPassingMarks5")]
        public int InternalPassingMarks5 { get; set; }

        [JsonProperty("InternalTotalMarks5")]
        public int InternalTotalMarks5 { get; set; }

        [JsonProperty("ExternalPassingMarks5")]
        public int ExternalPassingMarks5 { get; set; }

        [JsonProperty("PracticalMaxMarks5")]
        public int PracticalMaxMarks5 { get; set; }

        [JsonProperty("ExternalMaxMarks5")]
        public int ExternalMaxMarks5 { get; set; }

        [JsonProperty("GP5")]
        public string GP5 { get; set; }

        [JsonProperty("Attempt5")]
        public string Attempt5 { get; set; }

        [JsonProperty("Paper6Appeared")]
        public string Paper6Appeared { get; set; }

        [JsonProperty("Code6")]
        public string Code6 { get; set; }

        [JsonProperty("Credit6")]
        public string Credit6 { get; set; }

        [JsonProperty("InternalC6")]
        public float InternalC6 { get; set; }

        [JsonProperty("ExternalSection1C6")]
        public string ExternalSection1C6 { get; set; }

        [JsonProperty("ExternalSection2C6")]
        public string ExternalSection2C6 { get; set; }

        [JsonProperty("ExternalTotalC6")]
        public float ExternalTotalC6 { get; set; }

        [JsonProperty("PracticalMarksC6")]
        public string PracticalMarksC6 { get; set; }

        [JsonProperty("Grace6")]
        public string Grace6 { get; set; }

        [JsonProperty("Total6")]
        public float Total6 { get; set; }

        [JsonProperty("Grade6")]
        public string Grade6 { get; set; }


        [JsonProperty("PaperTitle6")]
        public string PaperTitle6 { get; set; }

        [JsonProperty("PaperType6")]
        public string PaperType6 { get; set; }

        [JsonProperty("IsElective6")]
        public bool IsElective6 { get; set; }

        [JsonProperty("InternalPassingMarks6")]
        public int InternalPassingMarks6 { get; set; }

        [JsonProperty("InternalTotalMarks6")]
        public int InternalTotalMarks6 { get; set; }

        [JsonProperty("ExternalPassingMarks6")]
        public int ExternalPassingMarks6 { get; set; }

        [JsonProperty("PracticalMaxMarks6")]
        public int PracticalMaxMarks6 { get; set; }

        [JsonProperty("ExternalMaxMarks6")]
        public int ExternalMaxMarks6 { get; set; }

        [JsonProperty("GP6")]
        public string GP6 { get; set; }

        [JsonProperty("Attempt6")]
        public string Attempt6 { get; set; }

        [JsonProperty("Paper7Appeared")]
        public string Paper7Appeared { get; set; }

        [JsonProperty("Code7")]
        public string Code7 { get; set; }

        [JsonProperty("Credit7")]
        public string Credit7 { get; set; }

        [JsonProperty("InternalC7")]
        public float InternalC7 { get; set; }

        [JsonProperty("ExternalSection1C7")]
        public string ExternalSection1C7 { get; set; }

        [JsonProperty("ExternalSection2C7")]
        public string ExternalSection2C7 { get; set; }

        [JsonProperty("ExternalTotalC7")]
        public float ExternalTotalC7 { get; set; }

        [JsonProperty("PracticalMarksC7")]
        public string PracticalMarksC7 { get; set; }

        [JsonProperty("Grace7")]
        public string Grace7 { get; set; }

        [JsonProperty("Total7")]
        public float Total7 { get; set; }

        [JsonProperty("Grade7")]
        public string Grade7 { get; set; }


        [JsonProperty("PaperTitle7")]
        public string PaperTitle7 { get; set; }

        [JsonProperty("PaperType7")]
        public string PaperType7 { get; set; }

        [JsonProperty("IsElective7")]
        public bool IsElective7 { get; set; }

        [JsonProperty("InternalPassingMarks7")]
        public int InternalPassingMarks7 { get; set; }

        [JsonProperty("InternalTotalMarks7")]
        public int InternalTotalMarks7 { get; set; }

        [JsonProperty("ExternalPassingMarks7")]
        public int ExternalPassingMarks7 { get; set; }

        [JsonProperty("PracticalMaxMarks7")]
        public int PracticalMaxMarks7 { get; set; }

        [JsonProperty("ExternalMaxMarks7")]
        public int ExternalMaxMarks7 { get; set; }

        [JsonProperty("GP7")]
        public string GP7 { get; set; }

        [JsonProperty("Attempt7")]
        public string Attempt7 { get; set; }

        [JsonProperty("Paper8Appeared")]
        public string Paper8Appeared { get; set; }

        [JsonProperty("Code8")]
        public string Code8 { get; set; }

        [JsonProperty("Credit8")]
        public string Credit8 { get; set; }

        [JsonProperty("InternalC8")]
        public float InternalC8 { get; set; }

        [JsonProperty("ExternalSection1C8")]
        public string ExternalSection1C8 { get; set; }

        [JsonProperty("ExternalSection2C8")]
        public string ExternalSection2C8 { get; set; }

        [JsonProperty("ExternalTotalC8")]
        public float ExternalTotalC8 { get; set; }

        [JsonProperty("PracticalMarksC8")]
        public string PracticalMarksC8 { get; set; }

        [JsonProperty("Grace8")]
        public string Grace8 { get; set; }

        [JsonProperty("Total8")]
        public float Total8 { get; set; }

        [JsonProperty("Grade8")]
        public string Grade8 { get; set; }

        [JsonProperty("PaperTitle8")]
        public string PaperTitle8 { get; set; }

        [JsonProperty("PaperType8")]
        public string PaperType8 { get; set; }

        [JsonProperty("IsElective8")]
        public bool IsElective8 { get; set; }

        [JsonProperty("InternalPassingMarks8")]
        public int InternalPassingMarks8 { get; set; }

        [JsonProperty("InternalTotalMarks8")]
        public int InternalTotalMarks8 { get; set; }

        [JsonProperty("ExternalPassingMarks8")]
        public int ExternalPassingMarks8 { get; set; }

        [JsonProperty("PracticalMaxMarks8")]
        public int PracticalMaxMarks8 { get; set; }

        [JsonProperty("ExternalMaxMarks8")]
        public int ExternalMaxMarks8 { get; set; }

        [JsonProperty("GP8")]
        public string GP8 { get; set; }

        [JsonProperty("Attempt8")]
        public string Attempt8 { get; set; }


        [JsonProperty("Paper9Appeared")]
        public string Paper9Appeared { get; set; }

        [JsonProperty("Code9")]
        public string Code9 { get; set; }

        [JsonProperty("Credit9")]
        public string Credit9 { get; set; }

        [JsonProperty("InternalC9")]
        public float InternalC9 { get; set; }

        [JsonProperty("ExternalSection1C9")]
        public string ExternalSection1C9 { get; set; }

        [JsonProperty("ExternalSection2C9")]
        public string ExternalSection2C9 { get; set; }

        [JsonProperty("ExternalTotalC9")]
        public float ExternalTotalC9 { get; set; }

        [JsonProperty("PracticalMarksC9")]
        public string PracticalMarksC9 { get; set; }

        [JsonProperty("Grace9")]
        public string Grace9 { get; set; }

        [JsonProperty("Total9")]
        public float Total9 { get; set; }

        [JsonProperty("Grade9")]
        public string Grade9 { get; set; }

        [JsonProperty("PaperTitle9")]
        public string PaperTitle9 { get; set; }

        [JsonProperty("PaperType9")]
        public string PaperType9 { get; set; }

        [JsonProperty("IsElective9")]
        public bool IsElective9 { get; set; }

        [JsonProperty("InternalPassingMarks9")]
        public int InternalPassingMarks9 { get; set; }

        [JsonProperty("InternalTotalMarks9")]
        public int InternalTotalMarks9 { get; set; }

        [JsonProperty("ExternalPassingMarks9")]
        public int ExternalPassingMarks9 { get; set; }

        [JsonProperty("PracticalMaxMarks9")]
        public int PracticalMaxMarks9 { get; set; }

        [JsonProperty("ExternalMaxMarks9")]
        public int ExternalMaxMarks9 { get; set; }

        [JsonProperty("GP9")]
        public string GP9 { get; set; }

        [JsonProperty("Attempt9")]
        public string Attempt9 { get; set; }


        [JsonProperty("Paper10Appeared")]
        public string Paper10Appeared { get; set; }

        [JsonProperty("Code10")]
        public string Code10 { get; set; }

        [JsonProperty("Credit10")]
        public string Credit10 { get; set; }

        [JsonProperty("InternalC10")]
        public float InternalC10 { get; set; }

        [JsonProperty("ExternalSection1C10")]
        public string ExternalSection1C10 { get; set; }

        [JsonProperty("ExternalSection2C10")]
        public string ExternalSection2C10 { get; set; }

        [JsonProperty("ExternalTotalC910")]
        public float ExternalTotalC10 { get; set; }

        [JsonProperty("PracticalMarksC10")]
        public string PracticalMarksC10 { get; set; }

        [JsonProperty("Grace10")]
        public string Grace10 { get; set; }

        [JsonProperty("Total10")]
        public float Total10 { get; set; }

        [JsonProperty("Grade10")]
        public string Grade10 { get; set; }


        [JsonProperty("PaperTitle10")]
        public string PaperTitle10 { get; set; }

        [JsonProperty("PaperType10")]
        public string PaperType10 { get; set; }

        [JsonProperty("IsElective10")]
        public bool IsElective10 { get; set; }

        [JsonProperty("InternalPassingMarks10")]
        public int InternalPassingMarks10 { get; set; }

        [JsonProperty("InternalTotalMarks10")]
        public int InternalTotalMarks10 { get; set; }

        [JsonProperty("ExternalPassingMarks10")]
        public int ExternalPassingMarks10 { get; set; }

        [JsonProperty("PracticalMaxMarks10")]
        public int PracticalMaxMarks10 { get; set; }

        [JsonProperty("ExternalMaxMarks10")]
        public int ExternalMaxMarks10 { get; set; }

        [JsonProperty("GP10")]
        public string GP10 { get; set; }

        [JsonProperty("Attempt10")]
        public string Attempt10 { get; set; }
    }

}