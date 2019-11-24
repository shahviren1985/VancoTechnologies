using SpreadsheetLight;
using SpreadsheetLight.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Drawing.Spreadsheet;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using QuarterlyReports.Models;

namespace QuarterlyReports.Helpers
{
    public static class SpreadSheetLightHelper
    {
        public static void CreatePieChart(string filePath, string filename, List<AnwerChartReport> listOfAnwerChartReport, string sheetName = null)
        {

            SLDocument sl = new SLDocument();
            int fChartHeight = 15;
            int fChartWidth = 8;
            //string worksheetName = "Sheet1";
            int columnStart = 2;
            int rowStart = 2;
            int columnWidth = 9;
            int rowWidth = 16;
            SLGroupDataLabelOptions gdloptions;



            //Random rand = new Random();
            //for (int i = 3; i <= 6; ++i)
            //{
            //    for (int j = 3; j <= 7; ++j)
            //    {
            //        sl.SetCellValue(i, j, 9000 * rand.NextDouble() + 1000);
            //    }
            //}

            int ActualRowStart = 2;
            int ActualColumnStart = 3;
            int ActualRowEnd = 0;
            int ActualColumnEnd = 0;
            for (int i = 1; i <= listOfAnwerChartReport.Count; i++)
            {
                int count = ActualColumnStart;
                sl.SetCellValue(rowStart, count, "Options"); count++;
                foreach (string dict in listOfAnwerChartReport[i - 1].DicAnwers.Keys)
                {
                    sl.SetCellValue(rowStart, count, dict);
                    count++;
                }

                int AnswersCount = ActualColumnStart;
                sl.SetCellValue(rowStart + 1, AnswersCount, "Answers"); AnswersCount++;
                foreach (int dict in listOfAnwerChartReport[i - 1].DicAnwers.Values)
                {
                    sl.SetCellValue(rowStart + 1, AnswersCount, dict);
                    AnswersCount++;
                }
                ActualRowEnd = rowStart + 1;
                ActualColumnEnd = AnswersCount;
                SLStyle hideText = sl.CreateStyle();
                hideText.Font.FontColor = System.Drawing.Color.White;
                sl.SetCellStyle(ActualRowStart, ActualColumnStart, ActualRowEnd, ActualColumnEnd - 1, hideText);
                SLChart chart = sl.CreateChart(ActualRowStart, ActualColumnStart, ActualRowEnd, ActualColumnEnd - 1);
                //Adding label
                gdloptions = chart.CreateGroupDataLabelOptions();
                gdloptions.ShowValue = true;
                chart.SetGroupDataLabelOptions(1, gdloptions);
                //end of label
                SLFont ft;
                SLRstType rst = sl.CreateRstType();
                ft = sl.CreateFont();
                ft.SetFont(FontSchemeValues.Minor, 12);
                ft.Bold = false;
                ft.SetFontThemeColor(SLThemeColorIndexValues.Dark1Color);
                rst.AppendText(listOfAnwerChartReport[i - 1].Title, ft);
                chart.Title.SetTitle(rst);
                // set true for title to overlap the plot area
                chart.ShowChartTitle(false);

                chart.SetChartType(SLPieChartType.Pie);
                //chart.SetChartPosition(rowStart, columnStart, rowStart + fChartHeight, columnStart + fChartWidth);
                chart.SetChartPosition(ActualRowStart - 1, ActualColumnStart - 1, ActualRowStart + 15, ActualColumnStart + 6);
                sl.InsertChart(chart);
                if (i % 2 != 0)
                {
                    columnStart = columnStart + 10;
                    ActualColumnStart += 10;
                }
                else
                {

                    rowStart = rowStart + 18;
                    columnStart = 1;
                    ActualRowStart += 18;
                    ActualColumnStart = 3;
                }
            }

            /*if (!string.IsNullOrEmpty(sheetName))
            {
                sl.RenameWorksheet(SLDocument.DefaultFirstSheetName, sheetName);
            }*/

            sl.SaveAs(filePath + "\\" + filename);

            Console.WriteLine("End of Pie Chart");
        }

        public static void CreatePieChart_BigSize(string filePath, string filename, List<AnwerChartReport> listOfAnwerChartReport)
        {

            SLDocument sl = new SLDocument();
            int fChartHeight = 15;
            int fChartWidth = 8;
            //string worksheetName = "Sheet1";
            int columnStart = 2;
            int rowStart = 2;
            int columnWidth = 9;
            int rowWidth = 16;
            SLGroupDataLabelOptions gdloptions;



            //Random rand = new Random();
            //for (int i = 3; i <= 6; ++i)
            //{
            //    for (int j = 3; j <= 7; ++j)
            //    {
            //        sl.SetCellValue(i, j, 9000 * rand.NextDouble() + 1000);
            //    }
            //}

            int ActualRowStart = 2;
            int ActualColumnStart = 3;
            int ActualRowEnd = 0;
            int ActualColumnEnd = 0;
            for (int i = 1; i <= listOfAnwerChartReport.Count; i++)
            {
                int count = ActualColumnStart;
                sl.SetCellValue(rowStart, count, "Options"); count++;
                foreach (string dict in listOfAnwerChartReport[i - 1].DicAnwers.Keys)
                {
                    sl.SetCellValue(rowStart, count, dict);
                    count++;
                }

                int AnswersCount = ActualColumnStart;
                sl.SetCellValue(rowStart + 1, AnswersCount, "Answers"); AnswersCount++;
                foreach (int dict in listOfAnwerChartReport[i - 1].DicAnwers.Values)
                {
                    sl.SetCellValue(rowStart + 1, AnswersCount, dict);
                    AnswersCount++;
                }
                ActualRowEnd = rowStart + 1;
                ActualColumnEnd = AnswersCount;
                SLStyle hideText = sl.CreateStyle();
                hideText.Font.FontColor = System.Drawing.Color.White;
                sl.SetCellStyle(ActualRowStart, ActualColumnStart, ActualRowEnd, ActualColumnEnd - 1, hideText);
                SLChart chart = sl.CreateChart(ActualRowStart, ActualColumnStart, ActualRowEnd, ActualColumnEnd - 1);
                //Adding label
                gdloptions = chart.CreateGroupDataLabelOptions();
                gdloptions.ShowValue = true;
                chart.SetGroupDataLabelOptions(1, gdloptions);
                //end of label
                SLFont ft;
                SLRstType rst = sl.CreateRstType();
                ft = sl.CreateFont();
                ft.SetFont(FontSchemeValues.Minor, 12);
                ft.Bold = false;
                ft.SetFontThemeColor(SLThemeColorIndexValues.Dark1Color);
                rst.AppendText(listOfAnwerChartReport[i - 1].Title, ft);
                chart.Title.SetTitle(rst);
                // set true for title to overlap the plot area
                chart.ShowChartTitle(false);

                chart.SetChartType(SLPieChartType.Pie);
                //chart.SetChartPosition(rowStart, columnStart, rowStart + fChartHeight, columnStart + fChartWidth);
                chart.SetChartPosition(ActualRowStart - 1, ActualColumnStart - 1, ActualRowStart + 20, ActualColumnStart + 6);
                sl.InsertChart(chart);
                if (i % 2 != 0)
                {
                    columnStart = columnStart + 10;
                    ActualColumnStart += 10;
                }
                else
                {

                    rowStart = rowStart + 22;
                    columnStart = 1;
                    ActualRowStart += 22;
                    ActualColumnStart = 3;
                }
            }

            sl.SaveAs(filePath + "\\" + filename);

            Console.WriteLine("End of Pie Chart");
        }

        public static bool CreateSpreadSheetData(string filePath, string filename, ReportCard rc)
        {
            try
            {
                SLDocument sl = new SLDocument();

                sl.SetCellValue("A1", rc.AcedemicYear);
                sl.MergeWorksheetCells("A1", "F1");
                SLStyle style = sl.CreateStyle();
                style.SetFont("Arial", 12);
                style.Font.Bold = true;
                style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
                sl.SetCellStyle("A1", style);

                string name = string.IsNullOrEmpty(rc.TeacherCode) ? rc.TeacherName : rc.TeacherCode;

                sl.SetCellValue("B2", "PERFORMANCE APPRAISAL REPORT FOR " + name);
                sl.MergeWorksheetCells("B2", "F2");
                sl.SetCellStyle("B2", style);

                SLStyle style2 = sl.CreateStyle();
                style2.SetFont("Arial", 10);
                style2.Font.Bold = true;
                style2.Alignment.Horizontal = HorizontalAlignmentValues.Left;

                style.SetFont("Arial", 10);
                sl.SetCellValue("B3", "Table 1");
                sl.SetCellStyle("B3", style);
                sl.SetCellValue("B4", "The information in the following table has been graphically represented below");
                sl.MergeWorksheetCells("B4", "F4");
                style2.SetWrapText(true);
                sl.SetCellStyle("B4", style2);


                sl.SetCellValue("B6", "Parameters");
                sl.SetCellStyle("B6", style);
                sl.SetCellValue("B7", "Max. Score");
                sl.SetCellStyle("B7", style);
                sl.SetCellValue("B8", "Score Obtained");
                sl.SetCellStyle("B8", style);
                sl.SetCellValue("B9", "Performance %");
                sl.SetCellStyle("B9", style);
                sl.AutoFitColumn("B6", "B9");

                string type = !string.IsNullOrEmpty(rc.TeacherCode) ? "Teaching Style" : "Leadership Style";
                sl.SetCellValue("C6", type);
                sl.SetCellStyle("C6", style);
                type = !string.IsNullOrEmpty(rc.TeacherCode) ? "Subject Knowledge" : "Administration";
                sl.SetCellValue("D6", type);
                sl.SetCellStyle("D6", style);
                type = !string.IsNullOrEmpty(rc.TeacherCode) ? "Personal Interactions" : "Communication";
                sl.SetCellValue("E6", type);
                sl.SetCellStyle("E6", style);
                sl.AutoFitColumn("C6", "E6");

                sl.SetCellValue("C7", rc.TSMaxScore);
                sl.SetCellStyle("C7", style);
                sl.SetCellValue("D7", rc.SKMaxScore);
                sl.SetCellStyle("D7", style);
                sl.SetCellValue("E7", rc.PIMaxScore);
                sl.SetCellStyle("E7", style);

                sl.SetCellValue("C8", rc.TSScoreObtain);
                sl.SetCellStyle("C8", style);
                sl.SetCellValue("D8", rc.SKScoreObtain);
                sl.SetCellStyle("D8", style);
                sl.SetCellValue("E8", rc.PIScoreObtain);
                sl.SetCellStyle("E8", style);

                sl.SetCellValue("C9", rc.TSPerformance.ToString() + "%");
                sl.SetCellStyle("C9", style);
                sl.SetCellValue("D9", rc.SKPerformance.ToString() + "%");
                sl.SetCellStyle("D9", style);
                sl.SetCellValue("E9", rc.PIPerformance.ToString() + "%");
                sl.SetCellStyle("E9", style);

                sl.SetCellValue("B11", "GRAPH 1");
                sl.SetCellStyle("B11", style);
                type = !string.IsNullOrEmpty(rc.TeacherCode) ? "This graph is a comparison of the teachers ideal performance versus the actual performance on subject knowledge, teaching skills & personal interaction." : "This graph is a comparison of the teachers ideal performance versus the actual performance on Leadership Style, Administration & Communication.";
                sl.SetCellValue("B12", type);
                sl.MergeWorksheetCells("B12", "F13");
                sl.SetCellStyle("B12", style2);
                sl.AutoFitColumn("B12", "F12");

                SLChart chart;
                chart = sl.CreateChart("B6", "E8");
                chart.SetChartType(SLLineChartType.LineWithMarkers);
                chart.SetChartPosition(14, 1, 28, 6);
                SLGroupDataLabelOptions gdloptions2 = chart.CreateGroupDataLabelOptions();
                gdloptions2.ShowValue = true;
                chart.SetGroupDataLabelOptions(gdloptions2);
                sl.InsertChart(chart);


                sl.SetCellValue("B31", "GRAPH 2");
                sl.SetCellStyle("B31", style);
                type = !string.IsNullOrEmpty(rc.TeacherCode) ? "This graph points towards the parameters the teacher needs to work on." : "This graph points towards the parameters the administrator needs to work on.";
                sl.SetCellValue("B32", type);
                sl.MergeWorksheetCells("B32", "F32");
                sl.SetCellStyle("B32", style2);
                sl.AutoFitColumn("B32", "F32");

                sl.SetCellValue("C34", "1");
                sl.SetCellValue("D34", "2");
                sl.SetCellValue("E34", "3");
                sl.SetCellValue("B35", "value");
                sl.SetCellValue("C35", rc.TSPerformance);
                sl.SetCellValue("D35", rc.SKPerformance);
                sl.SetCellValue("E35", rc.PIPerformance);

                SLStyle ss1 = sl.CreateStyle();
                //ss1.FormatCode = "0.00%";
                sl.SetCellStyle("C35", ss1);
                sl.SetCellStyle("D35", ss1);
                sl.SetCellStyle("E35", ss1);

                chart = sl.CreateChart("B34", "E35");
                chart.SetChartType(SLBarChartType.ClusteredBar);

                SLGroupDataLabelOptions gdloptions = chart.CreateGroupDataLabelOptions();
                gdloptions.ShowValue = true;
                gdloptions.FormatCode = "0.00";
                // set to false so the data don't link to the source data for the number format
                gdloptions.SourceLinked = false;
                // 4th data series
                chart.SetGroupDataLabelOptions(gdloptions);
                chart.SetChartPosition(33, 1, 47, 6);
                sl.InsertChart(chart);

                sl.SetCellValue("B50", "Table 2");
                sl.SetCellStyle("B50", style);
                type = !string.IsNullOrEmpty(rc.TeacherCode) ? "The following table is a measure of the ideal SATISFACTION INDEX and the score obtained by the teacher" : "The following table is a measure of the ideal SATISFACTION INDEX and the score obtained by the administrator";
                sl.SetCellValue("B51", type);
                sl.MergeWorksheetCells("B51", "F51");
                sl.SetCellStyle("B51", style2);
                sl.AutoFitColumn("B51", "F51");


                sl.SetCellValue("B52", "MAX. SATISFACTION INDEX");
                sl.SetCellStyle("B52", style);
                sl.SetCellValue("B53", "SCORE OBTAINED");
                sl.SetCellStyle("B53", style);
                sl.SetCellValue("B54", "STUDENT SATISFACTION %");
                sl.SetCellStyle("B54", style);
                sl.AutoFitColumn("B52", "B54");


                sl.SetCellValue("C52", rc.GrandMaxScore);
                sl.SetCellStyle("C52", style);
                sl.SetCellValue("C53", rc.GrandMean);
                sl.SetCellStyle("C53", style);
                sl.SetCellValue("C54", rc.GrandPercentage);
                sl.SetCellStyle("C54", style);

                //sl.SetPrintArea("A1", "F55");
                SLPageSettings settings = new SLPageSettings();
                settings.ScalePage(1, 100);
                sl.SetPageSettings(settings);

                sl.SaveAs(filePath + "\\" + filename);

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public static bool CreateSpreadSheetData2(string filePath, string filename, ReportCard rc)
        {
            try
            {
                SLDocument sl = new SLDocument();

                sl.SetCellValue("A1", rc.AcedemicYear);
                sl.MergeWorksheetCells("A1", "F1");
                SLStyle style = sl.CreateStyle();
                style.SetFont("Arial", 12);
                style.Font.Bold = true;
                style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
                sl.SetCellStyle("A1", style);

                string name = string.IsNullOrEmpty(rc.TeacherCode) ? rc.TeacherName : rc.TeacherCode;

                sl.SetCellValue("B2", "PERFORMANCE APPRAISAL REPORT FOR " + name);
                sl.MergeWorksheetCells("B2", "F2");
                sl.SetCellStyle("B2", style);

                SLStyle style2 = sl.CreateStyle();
                style2.SetFont("Arial", 10);
                style2.Font.Bold = true;
                style2.Alignment.Horizontal = HorizontalAlignmentValues.Left;

                style.SetFont("Arial", 10);
                sl.SetCellValue("B3", "Table 1");
                sl.SetCellStyle("B3", style);
                sl.SetCellValue("B4", "The information in the following table has been graphically represented below");
                sl.MergeWorksheetCells("B4", "F4");
                style2.SetWrapText(true);
                sl.SetCellStyle("B4", style2);


                sl.SetCellValue("B6", "Parameters");
                sl.SetCellStyle("B6", style);
                sl.SetCellValue("B7", "Max. Score");
                sl.SetCellStyle("B7", style);
                sl.SetCellValue("B8", "Score Obtained");
                sl.SetCellStyle("B8", style);
                sl.SetCellValue("B9", "Performance %");
                sl.SetCellStyle("B9", style);
                sl.AutoFitColumn("B6", "B9");

                string type = !string.IsNullOrEmpty(rc.TeacherCode) ? "Teachers Ethics" : "Leadership Style";
                sl.SetCellValue("C6", type);
                sl.SetCellStyle("C6", style);
                type = !string.IsNullOrEmpty(rc.TeacherCode) ? "Teaching Style" : "Administration";
                sl.SetCellValue("D6", type);
                sl.SetCellStyle("D6", style);

                type = !string.IsNullOrEmpty(rc.TeacherCode) ? "Student - Oriented approach" : "Communication";
                sl.SetCellValue("E6", type);
                sl.SetCellStyle("E6", style);
                sl.AutoFitColumn("C6", "E6");

                type = !string.IsNullOrEmpty(rc.TeacherCode) ? "Overall Impression" : "Overall Impression";
                sl.SetCellValue("F6", type);
                sl.SetCellStyle("F6", style);
                sl.AutoFitColumn("C6", "F6");

                sl.SetCellValue("C7", rc.TSMaxScore);
                sl.SetCellStyle("C7", style);
                sl.SetCellValue("D7", rc.SKMaxScore);
                sl.SetCellStyle("D7", style);
                sl.SetCellValue("E7", rc.PIMaxScore);
                sl.SetCellStyle("E7", style);
                sl.SetCellValue("F7", rc.OIMaxScore);
                sl.SetCellStyle("F7", style);

                sl.SetCellValue("C8", rc.TSScoreObtain);
                sl.SetCellStyle("C8", style);
                sl.SetCellValue("D8", rc.SKScoreObtain);
                sl.SetCellStyle("D8", style);
                sl.SetCellValue("E8", rc.PIScoreObtain);
                sl.SetCellStyle("E8", style);
                sl.SetCellValue("F8", rc.OIScoreObtain);
                sl.SetCellStyle("F8", style);

                sl.SetCellValue("C9", rc.TSPerformance.ToString() + "%");
                sl.SetCellStyle("C9", style);
                sl.SetCellValue("D9", rc.SKPerformance.ToString() + "%");
                sl.SetCellStyle("D9", style);
                sl.SetCellValue("E9", rc.PIPerformance.ToString() + "%");
                sl.SetCellStyle("E9", style);
                sl.SetCellValue("F9", rc.OIPerformance.ToString() + "%");
                sl.SetCellStyle("F9", style);

                sl.SetCellValue("B11", "GRAPH 1");
                sl.SetCellStyle("B11", style);
                type = !string.IsNullOrEmpty(rc.TeacherCode) ? "This graph is a comparison of the teachers ideal performance versus the actual performance on subject knowledge, teaching skills & personal interaction." : "This graph is a comparison of the teachers ideal performance versus the actual performance on Leadership Style, Administration & Communication.";
                sl.SetCellValue("B12", type);
                sl.MergeWorksheetCells("B12", "F13");
                sl.SetCellStyle("B12", style2);
                sl.AutoFitColumn("B12", "F12");

                SLChart chart;
                chart = sl.CreateChart("B6", "F8");
                chart.SetChartType(SLLineChartType.LineWithMarkers);
                chart.SetChartPosition(14, 1, 28, 6);
                SLGroupDataLabelOptions gdloptions2 = chart.CreateGroupDataLabelOptions();
                gdloptions2.ShowValue = true;
                chart.SetGroupDataLabelOptions(gdloptions2);
                sl.InsertChart(chart);


                sl.SetCellValue("B31", "GRAPH 2");
                sl.SetCellStyle("B31", style);
                type = !string.IsNullOrEmpty(rc.TeacherCode) ? "This graph points towards the parameters the teacher needs to work on." : "This graph points towards the parameters the administrator needs to work on.";
                sl.SetCellValue("B32", type);
                sl.MergeWorksheetCells("B32", "F32");
                sl.SetCellStyle("B32", style2);
                sl.AutoFitColumn("B32", "F32");

                sl.SetCellValue("C34", "1");
                sl.SetCellValue("D34", "2");
                sl.SetCellValue("E34", "3");
                sl.SetCellValue("F34", "4");

                sl.SetCellValue("B35", "value");
                sl.SetCellValue("C35", rc.TSPerformance);
                sl.SetCellValue("D35", rc.SKPerformance);
                sl.SetCellValue("E35", rc.PIPerformance);
                sl.SetCellValue("F35", rc.OIPerformance);

                SLStyle ss1 = sl.CreateStyle();
                //ss1.FormatCode = "0.00%";
                sl.SetCellStyle("C35", ss1);
                sl.SetCellStyle("D35", ss1);
                sl.SetCellStyle("E35", ss1);
                sl.SetCellStyle("F35", ss1);

                chart = sl.CreateChart("B34", "F35");
                chart.SetChartType(SLBarChartType.ClusteredBar);

                SLGroupDataLabelOptions gdloptions = chart.CreateGroupDataLabelOptions();
                gdloptions.ShowValue = true;
                gdloptions.FormatCode = "0.00";
                // set to false so the data don't link to the source data for the number format
                gdloptions.SourceLinked = false;
                // 4th data series
                chart.SetGroupDataLabelOptions(gdloptions);
                chart.SetChartPosition(33, 1, 47, 6);
                sl.InsertChart(chart);

                sl.SetCellValue("B50", "Table 2");
                sl.SetCellStyle("B50", style);
                type = !string.IsNullOrEmpty(rc.TeacherCode) ? "The following table is a measure of the ideal SATISFACTION INDEX and the score obtained by the teacher" : "The following table is a measure of the ideal SATISFACTION INDEX and the score obtained by the administrator";
                sl.SetCellValue("B51", type);
                sl.MergeWorksheetCells("B51", "F51");
                sl.SetCellStyle("B51", style2);
                sl.AutoFitColumn("B51", "F51");


                sl.SetCellValue("B52", "MAX. SATISFACTION INDEX");
                sl.SetCellStyle("B52", style);
                sl.SetCellValue("B53", "SCORE OBTAINED");
                sl.SetCellStyle("B53", style);
                sl.SetCellValue("B54", "STUDENT SATISFACTION %");
                sl.SetCellStyle("B54", style);
                sl.AutoFitColumn("B52", "B54");


                sl.SetCellValue("C52", rc.GrandMaxScore);
                sl.SetCellStyle("C52", style);
                sl.SetCellValue("C53", rc.GrandMean);
                sl.SetCellStyle("C53", style);
                sl.SetCellValue("C54", rc.GrandPercentage);
                sl.SetCellStyle("C54", style);

                //sl.SetPrintArea("A1", "F55");
                SLPageSettings settings = new SLPageSettings();
                settings.ScalePage(1, 100);
                sl.SetPageSettings(settings);

                sl.SaveAs(filePath + "\\" + filename);

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        public static bool CreateMNWCSpreadSheetData2(string filePath, string filename, ReportCard rc)
        {
            try
            {
                SLDocument sl = new SLDocument();

                sl.SetCellValue("A1", rc.AcedemicYear);
                sl.MergeWorksheetCells("A1", "F1");
                SLStyle style = sl.CreateStyle();
                style.SetFont("Arial", 12);
                style.Font.Bold = true;
                style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
                sl.SetCellStyle("A1", style);

                string name = string.IsNullOrEmpty(rc.TeacherCode) ? rc.TeacherName : rc.TeacherCode;

                sl.SetCellValue("B2", "PERFORMANCE APPRAISAL REPORT FOR " + name);
                sl.MergeWorksheetCells("B2", "F2");
                sl.SetCellStyle("B2", style);

                SLStyle style2 = sl.CreateStyle();
                style2.SetFont("Arial", 10);
                style2.Font.Bold = true;
                style2.Alignment.Horizontal = HorizontalAlignmentValues.Left;

                style.SetFont("Arial", 10);
                sl.SetCellValue("B3", "Table 1");
                sl.SetCellStyle("B3", style);
                sl.SetCellValue("B4", "The information in the following table has been graphically represented below");
                sl.MergeWorksheetCells("B4", "F4");
                style2.SetWrapText(true);
                sl.SetCellStyle("B4", style2);


                sl.SetCellValue("B6", "Parameters");
                sl.SetCellStyle("B6", style);
                sl.SetCellValue("B7", "Max. Score");
                sl.SetCellStyle("B7", style);
                sl.SetCellValue("B8", "Score Obtained");
                sl.SetCellStyle("B8", style);
                sl.SetCellValue("B9", "Performance %");
                sl.SetCellStyle("B9", style);
                sl.AutoFitColumn("B6", "B9");

                string type = "Leadership Style";
                sl.SetCellValue("C6", type);
                sl.SetCellStyle("C6", style);
                type = "Administration";
                sl.SetCellValue("D6", type);
                sl.SetCellStyle("D6", style);

                type = "Communication";
                sl.SetCellValue("E6", type);
                sl.SetCellStyle("E6", style);
                sl.AutoFitColumn("C6", "E6");

                

                sl.SetCellValue("C7", rc.TSMaxScore);
                sl.SetCellStyle("C7", style);
                sl.SetCellValue("D7", rc.SKMaxScore);
                sl.SetCellStyle("D7", style);
                sl.SetCellValue("E7", rc.PIMaxScore);
                sl.SetCellStyle("E7", style);
                
                sl.SetCellValue("C8", rc.TSScoreObtain);
                sl.SetCellStyle("C8", style);
                sl.SetCellValue("D8", rc.SKScoreObtain);
                sl.SetCellStyle("D8", style);
                sl.SetCellValue("E8", rc.PIScoreObtain);
                sl.SetCellStyle("E8", style);
                

                sl.SetCellValue("C9", rc.TSPerformance.ToString() + "%");
                sl.SetCellStyle("C9", style);
                sl.SetCellValue("D9", rc.SKPerformance.ToString() + "%");
                sl.SetCellStyle("D9", style);
                sl.SetCellValue("E9", rc.PIPerformance.ToString() + "%");
                sl.SetCellStyle("E9", style);
                

                sl.SetCellValue("B11", "GRAPH 1");
                sl.SetCellStyle("B11", style);
                type = !string.IsNullOrEmpty(rc.TeacherCode) ? "This graph is a comparison of the teachers ideal performance versus the actual performance on subject knowledge, teaching skills & personal interaction." : "This graph is a comparison of the teachers ideal performance versus the actual performance on Leadership Style, Administration & Communication.";
                sl.SetCellValue("B12", type);
                sl.MergeWorksheetCells("B12", "E13");
                sl.SetCellStyle("B12", style2);
                sl.AutoFitColumn("B12", "E12");

                SLChart chart;
                chart = sl.CreateChart("B6", "E8");
                chart.SetChartType(SLLineChartType.LineWithMarkers);
                chart.SetChartPosition(14, 1, 28, 6);
                SLGroupDataLabelOptions gdloptions2 = chart.CreateGroupDataLabelOptions();
                gdloptions2.ShowValue = true;
                chart.SetGroupDataLabelOptions(gdloptions2);
                sl.InsertChart(chart);


                sl.SetCellValue("B31", "GRAPH 2");
                sl.SetCellStyle("B31", style);
                type = !string.IsNullOrEmpty(rc.TeacherCode) ? "This graph points towards the parameters the teacher needs to work on." : "This graph points towards the parameters the administrator needs to work on.";
                sl.SetCellValue("B32", type);
                sl.MergeWorksheetCells("B32", "E32");
                sl.SetCellStyle("B32", style2);
                sl.AutoFitColumn("B32", "E32");

                sl.SetCellValue("C34", "1");
                sl.SetCellValue("D34", "2");
                sl.SetCellValue("E34", "3");
                

                sl.SetCellValue("B35", "value");
                sl.SetCellValue("C35", rc.TSPerformance);
                sl.SetCellValue("D35", rc.SKPerformance);
                sl.SetCellValue("E35", rc.PIPerformance);
                

                SLStyle ss1 = sl.CreateStyle();
                //ss1.FormatCode = "0.00%";
                sl.SetCellStyle("C35", ss1);
                sl.SetCellStyle("D35", ss1);
                sl.SetCellStyle("E35", ss1);
                

                chart = sl.CreateChart("B34", "E35");
                chart.SetChartType(SLBarChartType.ClusteredBar);

                SLGroupDataLabelOptions gdloptions = chart.CreateGroupDataLabelOptions();
                gdloptions.ShowValue = true;
                gdloptions.FormatCode = "0.00";
                // set to false so the data don't link to the source data for the number format
                gdloptions.SourceLinked = false;
                // 4th data series
                chart.SetGroupDataLabelOptions(gdloptions);
                chart.SetChartPosition(33, 1, 47, 6);
                sl.InsertChart(chart);

                sl.SetCellValue("B50", "Table 2");
                sl.SetCellStyle("B50", style);
                type = !string.IsNullOrEmpty(rc.TeacherCode) ? "The following table is a measure of the ideal SATISFACTION INDEX and the score obtained by the teacher" : "The following table is a measure of the ideal SATISFACTION INDEX and the score obtained by the administrator";
                sl.SetCellValue("B51", type);
                sl.MergeWorksheetCells("B51", "F51");
                sl.SetCellStyle("B51", style2);
                sl.AutoFitColumn("B51", "F51");


                sl.SetCellValue("B52", "MAX. SATISFACTION INDEX");
                sl.SetCellStyle("B52", style);
                sl.SetCellValue("B53", "SCORE OBTAINED");
                sl.SetCellStyle("B53", style);
                sl.SetCellValue("B54", "STUDENT SATISFACTION %");
                sl.SetCellStyle("B54", style);
                sl.AutoFitColumn("B52", "B54");


                sl.SetCellValue("C52", rc.GrandMaxScore);
                sl.SetCellStyle("C52", style);
                sl.SetCellValue("C53", rc.GrandMean);
                sl.SetCellStyle("C53", style);
                sl.SetCellValue("C54", rc.GrandPercentage);
                sl.SetCellStyle("C54", style);

                //sl.SetPrintArea("A1", "F55");
                SLPageSettings settings = new SLPageSettings();
                settings.ScalePage(1, 100);
                sl.SetPageSettings(settings);

                sl.SaveAs(filePath + "\\" + filename);

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
    }
}