using ExcelDataExtraction.Model;
using ExcelDataExtraction.Services;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace ExcelDataExtraction
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                bool first = int.TryParse(args[1], out int firstPage);
                bool last = int.TryParse(args[2], out int lastPage);
                var date = DateTime.TryParse(args[3], out var res);

                #region Constants
                const int STUDENT_DATA_ROW = 0;
                const int STUDENT_DATA_COLUMN = 0;
                const int PROGRAM_DATA_ROW_FIRST_SEMESTER = 3;
                const int PROGRAM_DATA_COLUMN_FIRST_SEMESTER = 1;
                const int PROGRAM_DATA_ROW_SECOND_SEMESTER = 10;
                const int PROGRAM_DATA_COLUMN_SECOND_SEMESTER = 1;

                const int TOTAL_EGP_DATA_ROW_FIRST_SEMESTER = 9;
                const int TOTAL_EGP_DATA_COLUMN_FIRST_SEMESTER = 2;
                const int SGPA_DATA_ROW_FIRST_SEMESTER = 9;
                const int SGPA_DATA_COLUMN_FIRST_SEMESTER = 6;
                const int GRADE_DATA_ROW_FIRST_SEMESTER = 9;
                const int GRADE_DATA_COLUMN_FIRST_SEMESTER = 9;
                const int GRAND_TOTAL_ROW_FIRST_SEMESTER = 9;
                const int GRAND_TOTAL_COLUMN_FIRST_SEMESTER = 11;
                const int PERCENTAGE_TOTAL_ROW_FIRST_SEMESTER = 9;
                const int PERCENTAGE_TOTAL_COLUMN_FIRST_SEMESTER = 14;
                const int TOTAL_CREDITS_ROW_FIRST_SEMESTER = 3;
                const int TOTAL_CREDITS_COLUMN_FIRST_SEMESTER = 1;

                const int TOTAL_EGP_DATA_ROW_SECOND_SEMESTER = 18;
                const int TOTAL_EGP_DATA_COLUMN_SECOND_SEMESTER = 2;
                const int SGPA_DATA_ROW_SECOND_SEMESTER = 18;
                const int SGPA_DATA_COLUMN_SECOND_SEMESTER = 6;
                const int GRADE_DATA_ROW_SECOND_SEMESTER = 18;
                const int GRADE_DATA_COLUMN_SECOND_SEMESTER = 9;
                const int GRAND_TOTAL_ROW_SECOND_SEMESTER = 18;
                const int GRAND_TOTAL_COLUMN_SECOND_SEMESTER = 11;
                const int PERCENTAGE_TOTAL_ROW_SECOND_SEMESTER = 18;
                const int PERCENTAGE_TOTAL_COLUMN_SECOND_SEMESTER = 14;
                const int TOTAL_CREDITS_ROW_SECOND_SEMESTER = 10;
                const int TOTAL_CREDITS_COLUMN_SECOND_SEMESTER = 1;

                #endregion

                if (first && last)
                {
                    #region StudentTableDataExtraction
                    List<StudentDetails> studentsDetails = new List<StudentDetails>();
                    List<ResultSummary> resultSummary = new List<ResultSummary>();
                    List<string> programNamesArray = new List<string>();

                    using (var excelReader = new ExcelReader($@"{args[0]}"))
                    {
                        var worksheets = excelReader.GetWorksheets(firstPage, lastPage);

                        foreach (var workSheet in worksheets)
                        {
                            #region GetStudentCommonDetails
                            var studentDataString = excelReader
                                .GetDataStringByColumnAndRowIndexes(workSheet.Index, STUDENT_DATA_ROW, STUDENT_DATA_COLUMN);
                            var studentDataDictionary = StringConverter.StringProceed(studentDataString);
                            var finalDictionary = StringConverter.GetCollegeInfo(studentDataString, studentDataDictionary);
                            finalDictionary.Add("Year", "2019");
                            finalDictionary.Add("Month", "April");
                            finalDictionary.Add("Exam Type", "Regular");
                            if (args.Length > 3)
                                finalDictionary.Add("Result Date", args[3]);
                            else
                                finalDictionary.Add("Result Date", DateTime.Now.Date.ToString());
                            #endregion

                            #region GetProgramNamesForFirstSemester
                            var programDataStringFirstSemester = excelReader
                                .GetDataStringByColumnAndRowIndexes(workSheet.Index, PROGRAM_DATA_ROW_FIRST_SEMESTER, PROGRAM_DATA_COLUMN_FIRST_SEMESTER);
                            programNamesArray = StringConverter.ExtractProgramNames(programDataStringFirstSemester);
                            studentsDetails.AddRange(StudentDataSerializer.SerializeStudentDetailsData(finalDictionary, programNamesArray, 1));

                            var finalString = excelReader
                              .GetDataStringByColumnAndRowIndexes(workSheet.Index, TOTAL_EGP_DATA_ROW_FIRST_SEMESTER, TOTAL_EGP_DATA_COLUMN_FIRST_SEMESTER);
                            finalString +=
                                $"_{excelReader.GetDataStringByColumnAndRowIndexes(workSheet.Index, SGPA_DATA_ROW_FIRST_SEMESTER, SGPA_DATA_COLUMN_FIRST_SEMESTER)}";
                            finalString +=
                                $"_{excelReader.GetDataStringByColumnAndRowIndexes(workSheet.Index, GRADE_DATA_ROW_FIRST_SEMESTER, GRADE_DATA_COLUMN_FIRST_SEMESTER)}";
                            var str =
                                $"_{excelReader.GetDataStringByColumnAndRowIndexes(workSheet.Index, GRAND_TOTAL_ROW_FIRST_SEMESTER, GRAND_TOTAL_COLUMN_FIRST_SEMESTER)}";
                            finalString += $"_{ StringConverter.SeparateGrandTotal(str)}";
                            finalString +=
                                $"_{excelReader.GetDataStringByColumnAndRowIndexes(workSheet.Index, PERCENTAGE_TOTAL_ROW_FIRST_SEMESTER, PERCENTAGE_TOTAL_COLUMN_FIRST_SEMESTER)}";
                            finalString +=
                                $"_{StringConverter.GetTotalCredits(excelReader.GetDataStringByColumnAndRowIndexes(workSheet.Index, TOTAL_CREDITS_ROW_FIRST_SEMESTER, TOTAL_CREDITS_COLUMN_FIRST_SEMESTER))}";
                            studentDataDictionary.TryGetValue("PRN", out string value);
                            finalString +=
                                $"_PRN:{value}";
                            finalString +=
                                $"_Semester:1";
                            resultSummary.Add(ResultSummarySerializer.SerializeData(StringConverter.StringProceed(finalString)));

                            finalString = string.Empty;
                            #endregion


                            #region GetProgramNamesForSecondSemester
                            var programDataStringSecondSemester = excelReader
                                .GetDataStringByColumnAndRowIndexes(workSheet.Index, PROGRAM_DATA_ROW_SECOND_SEMESTER, PROGRAM_DATA_COLUMN_SECOND_SEMESTER);
                            programNamesArray = StringConverter.ExtractProgramNames(programDataStringSecondSemester);
                            studentsDetails.AddRange(StudentDataSerializer.SerializeStudentDetailsData(finalDictionary, programNamesArray, 2));

                            finalString = excelReader
                              .GetDataStringByColumnAndRowIndexes(workSheet.Index, TOTAL_EGP_DATA_ROW_SECOND_SEMESTER, TOTAL_EGP_DATA_COLUMN_SECOND_SEMESTER);
                            finalString +=
                                $"_{excelReader.GetDataStringByColumnAndRowIndexes(workSheet.Index, SGPA_DATA_ROW_SECOND_SEMESTER, SGPA_DATA_COLUMN_SECOND_SEMESTER)}";
                            finalString +=
                                $"_{excelReader.GetDataStringByColumnAndRowIndexes(workSheet.Index, GRADE_DATA_ROW_SECOND_SEMESTER, GRADE_DATA_COLUMN_SECOND_SEMESTER)}";
                            var bufferstr =
                                $"_{excelReader.GetDataStringByColumnAndRowIndexes(workSheet.Index, GRAND_TOTAL_ROW_SECOND_SEMESTER, GRAND_TOTAL_COLUMN_SECOND_SEMESTER)}";
                            finalString += $"_{ StringConverter.SeparateGrandTotal(str)}";
                            finalString +=
                                $"_{excelReader.GetDataStringByColumnAndRowIndexes(workSheet.Index, PERCENTAGE_TOTAL_ROW_SECOND_SEMESTER, PERCENTAGE_TOTAL_COLUMN_SECOND_SEMESTER)}";
                            finalString +=
                                $"_{StringConverter.GetTotalCredits(excelReader.GetDataStringByColumnAndRowIndexes(workSheet.Index, TOTAL_CREDITS_ROW_SECOND_SEMESTER, TOTAL_CREDITS_COLUMN_SECOND_SEMESTER))}";
                            finalString +=
                                $"_PRN:{value}";
                            finalString +=
                                $"_Semester:2";
                            var summuryDataSecondSemester = StringConverter.StringProceed(finalString);
                            resultSummary.Add(ResultSummarySerializer.SerializeData(StringConverter.StringProceed(finalString)));
                            finalString = string.Empty;
                            #endregion
                        }
                    }
                    #endregion

                    #region FeedingDataToDb

                    DataToContext.FeedStudentDetailsData(studentsDetails);
                    DataToContext.FeedResultSummaryData(resultSummary);

                    #endregion
                }
                else
                {
                    Console.WriteLine("Please provide correct arguments (File path, first page, last page, date (MM.dd.yyyy))");
                }
            }
            else
            {
                Console.WriteLine("Please provide required arguments (File path, first page, last page, date (MM.dd.yyyy))");
            }
        }
    }
}
