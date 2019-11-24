using System;
using System.Collections.Generic;
using System.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using ITM.Courses.DAO;
using System.IO;

/// <summary>
/// Summary description for PDFStudentTypingStatus
/// </summary>
public class PDFStudentTypingStatus
{
    private static int LOGO_IMAGE_SCALE = 60;
    private static int LOGO_POSITION_X = 70;
    private static int LOGO_POSITION_Y = 500;
    private static int COLLEGE_NAME_FONT_SIZE = 16;
    private static int FONT_SIZE = 9;

    public PDFStudentTypingStatus()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static void CreateNPStudentHeader(ref PdfWriter writer, ref Document document, ApplicationLogoHeader config, string reportName)
    {
        try
        {
            //if (!config.IsHeaderRequired)
            //{
            //    return;
            //}

            using (Stream logoStream = new FileStream(config.LogoImagePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                Image image = Image.GetInstance(logoStream);
                image.Alt = config.CollegeName;
                image.ScalePercent(LOGO_IMAGE_SCALE);
                image.SetAbsolutePosition(LOGO_POSITION_X, LOGO_POSITION_Y);
                document.Add(image);
            }


            PdfPTable table = new PdfPTable(1);
            Paragraph colleges = new Paragraph();
            Paragraph address = new Paragraph();
            Paragraph tagLine = new Paragraph();
            Paragraph tagLine1 = new Paragraph();
            Paragraph tagLine2 = new Paragraph();

            PdfPCell cell = new PdfPCell();
            float[] widths = new float[] { 554f };
            table.TotalWidth = 720f;

            table.LockedWidth = true;
            table.HorizontalAlignment = 0;
            table.SetWidths(widths);
            cell.Padding = 5;


            Phrase textColleges = new Phrase(config.LogoText.Trim().ToUpper(), new Font(Font.FontFamily.HELVETICA, COLLEGE_NAME_FONT_SIZE, Font.BOLD));
            colleges.Add(textColleges);
            colleges.Add(new Phrase(Environment.NewLine));

            Phrase textTagLine = new Phrase(config.LogoText.Trim() + Environment.NewLine + Environment.NewLine, new Font(Font.FontFamily.HELVETICA, COLLEGE_NAME_FONT_SIZE, Font.NORMAL));
            tagLine.Add(textTagLine);
            tagLine.Add(new Phrase(Environment.NewLine));

            Phrase textTagLine1 = new Phrase(reportName, new Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL));
            tagLine1.Add(textTagLine1);
            tagLine1.Add(new Phrase(Environment.NewLine));

            //Phrase textTagLine2 = new Phrase(config.TagLine2.Trim(), new Font(Font.FontFamily.HELVETICA, 9, Font.NORMAL));
            //tagLine2.Add(textTagLine2);
            ////tagLine2.Add(new Phrase(Environment.NewLine));

            //Phrase textAddress = new Phrase(config.Address.Trim(), new Font(Font.FontFamily.HELVETICA, 9, Font.NORMAL));
            //address.Add(textAddress);
            ////address.Add(new Phrase(Environment.NewLine));

            colleges.Alignment = Element.ALIGN_CENTER;
            //address.Alignment = Element.ALIGN_CENTER;
            tagLine.Alignment = Element.ALIGN_CENTER;
            tagLine1.Alignment = Element.ALIGN_CENTER;
            //tagLine2.Alignment = Element.ALIGN_CENTER;

            //cell.AddElement(colleges);
            cell.AddElement(tagLine);
            cell.AddElement(tagLine1);
            //cell.AddElement(tagLine2);
            //cell.AddElement(address);

            table.AddCell(cell);
            PdfObject obj = PdfName.NONE;
            table.SetAccessibleAttribute(PdfName.BORDER, obj);
            ColumnText tab = new ColumnText(writer.DirectContent);
            //tab.SetSimpleColumn(400, 570, 800, 100);
            tab.SetSimpleColumn(60, 570, 800, 100);
            tab.AddElement(table);
            //tab.AddElement(textColleges);
            tab.Go();
        }
        catch (Exception ex)
        {
            // TODO - log an exception
            return;
        }
    }

    public static void CreateNPStudentSubHeader(ref PdfWriter writer, ref Document document, string course, string subcourse)
    {
        try
        {
            Phrase courseName = new Phrase("Course : " + course + " - " + subcourse, new Font(Font.FontFamily.HELVETICA, 9, Font.NORMAL));
            Phrase month = new Phrase("Month : ", new Font(Font.FontFamily.HELVETICA, 9, Font.NORMAL));
            Phrase year = new Phrase("Year : ", new Font(Font.FontFamily.HELVETICA, 9, Font.NORMAL));
            Phrase examname = new Phrase("Exam Name : ", new Font(Font.FontFamily.HELVETICA, 9, Font.NORMAL));

            Phrase date = new Phrase("Date : ", new Font(Font.FontFamily.HELVETICA, 9, Font.NORMAL));
            Phrase supervisor = new Phrase("Supervisor Name : ", new Font(Font.FontFamily.HELVETICA, 9, Font.NORMAL));

            ColumnText courseNameCT = new ColumnText(writer.DirectContent);
            ColumnText yearCT = new ColumnText(writer.DirectContent);
            ColumnText examnameCT = new ColumnText(writer.DirectContent);
            ColumnText monthCT = new ColumnText(writer.DirectContent);
            ColumnText paperNameCT = new ColumnText(writer.DirectContent);
            ColumnText papercodeCT = new ColumnText(writer.DirectContent);
            ColumnText dateCT = new ColumnText(writer.DirectContent);
            ColumnText supervisorCT = new ColumnText(writer.DirectContent);

            courseNameCT.Alignment = Element.ALIGN_LEFT;
            monthCT.Alignment = Element.ALIGN_LEFT;
            yearCT.Alignment = Element.ALIGN_LEFT;
            examnameCT.Alignment = Element.ALIGN_LEFT;
            paperNameCT.Alignment = Element.ALIGN_LEFT;
            papercodeCT.Alignment = Element.ALIGN_LEFT;
            dateCT.Alignment = Element.ALIGN_LEFT;
            supervisorCT.Alignment = Element.ALIGN_LEFT;

            courseNameCT.SetSimpleColumn(60, 470, 800, 100);
            monthCT.SetSimpleColumn(200, 470, 800, 100);
            yearCT.SetSimpleColumn(300, 470, 800, 100);
            examnameCT.SetSimpleColumn(450, 470, 800, 100);
            paperNameCT.SetSimpleColumn(200, 450, 800, 100);
            papercodeCT.SetSimpleColumn(60, 450, 800, 100);
            dateCT.SetSimpleColumn(450, 430, 800, 100);
            supervisorCT.SetSimpleColumn(60, 430, 800, 100);

            courseNameCT.AddText(courseName);
            monthCT.AddText(month);
            yearCT.AddText(year);
            examnameCT.AddText(examname);
            //paperNameCT.AddText(paperName);
            //papercodeCT.AddText(papercode);
            dateCT.AddText(date);
            supervisorCT.AddText(supervisor);

            courseNameCT.Go();
            yearCT.Go();
            examnameCT.Go();
            monthCT.Go();
            //paperNameCT.Go();
            //papercodeCT.Go();
            //dateCT.Go();
            //supervisorCT.Go();

        }
        catch (Exception ex)
        {

            return;
        }
    }

    public static void GetTopperListContent(ref PdfWriter writer, ref Document document, List<StudentTypingStats> studentTypingStatusList, int reportCount, ApplicationLogoHeader config, string reportName)
    {
        try
        {
            Paragraph srno = new Paragraph();
            Paragraph CourseName = new Paragraph();
            Paragraph student = new Paragraph();
            Paragraph UserName = new Paragraph();
            Paragraph Level = new Paragraph();
            Paragraph TimeSpanInNormal = new Paragraph();
            Paragraph Accuracy = new Paragraph();
            Paragraph GrossWPM = new Paragraph();
            Paragraph NetWPM = new Paragraph();

            srno.Alignment = Element.ALIGN_CENTER;
            CourseName.Alignment = Element.ALIGN_CENTER;
            student.Alignment = Element.ALIGN_CENTER;
            UserName.Alignment = Element.ALIGN_CENTER;
            Level.Alignment = Element.ALIGN_CENTER;
            TimeSpanInNormal.Alignment = Element.ALIGN_CENTER;
            Accuracy.Alignment = Element.ALIGN_CENTER;
            GrossWPM.Alignment = Element.ALIGN_CENTER;
            NetWPM.Alignment = Element.ALIGN_CENTER;

            srno.Add(new Phrase("SrNo", new Font(Font.FontFamily.HELVETICA, FONT_SIZE, Font.BOLD)));
            CourseName.Add(new Phrase("Course Name", new Font(Font.FontFamily.HELVETICA, FONT_SIZE, Font.BOLD)));
            student.Add(new Phrase("Student", new Font(Font.FontFamily.HELVETICA, FONT_SIZE, Font.BOLD)));
            UserName.Add(new Phrase("User Name", new Font(Font.FontFamily.HELVETICA, FONT_SIZE, Font.BOLD)));
            Level.Add(new Phrase("Level", new Font(Font.FontFamily.HELVETICA, FONT_SIZE, Font.BOLD)));
            TimeSpanInNormal.Add(new Phrase("Time (M : S)", new Font(Font.FontFamily.HELVETICA, FONT_SIZE, Font.BOLD)));
            Accuracy.Add(new Phrase("Accuracy", new Font(Font.FontFamily.HELVETICA, FONT_SIZE, Font.BOLD)));
            GrossWPM.Add(new Phrase("Gross WPM", new Font(Font.FontFamily.HELVETICA, FONT_SIZE, Font.BOLD)));
            NetWPM.Add(new Phrase("Net WPM", new Font(Font.FontFamily.HELVETICA, FONT_SIZE, Font.BOLD)));

            int count = 0;
            int counter = 0;
            int marklist = studentTypingStatusList.Count();
            PdfPTable table = null;

            foreach (StudentTypingStats m in studentTypingStatusList)
            {
                counter++;

                if ((count % (reportCount + 1)) == 0)
                {
                    table = new PdfPTable(9);
                    float[] widths = new float[] { 40f, 100f, 200f, 100f, 50f, 80f, 60f, 70f, 70f };
                    table.TotalWidth = 720f;
                    table.LockedWidth = true;
                    table.HorizontalAlignment = 0;
                    table.SetWidths(widths);

                    PdfPCell cell1 = new PdfPCell();
                    cell1.Padding = 5;
                    cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell1.AddElement(srno);

                    PdfPCell cell2 = new PdfPCell();
                    cell2.Padding = 5;
                    cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell2.AddElement(CourseName);

                    PdfPCell cell3 = new PdfPCell();
                    cell3.Padding = 5;
                    cell3.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell3.AddElement(student);

                    PdfPCell cell4 = new PdfPCell();
                    cell4.Padding = 5;
                    cell4.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell4.AddElement(UserName);

                    PdfPCell cell5 = new PdfPCell();
                    cell5.Padding = 5;
                    cell5.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell5.AddElement(Level);

                    PdfPCell cell6 = new PdfPCell();
                    cell6.Padding = 5;
                    cell6.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell6.AddElement(TimeSpanInNormal);

                    PdfPCell cell7 = new PdfPCell();
                    cell7.Padding = 5;
                    cell7.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell7.AddElement(Accuracy);

                    PdfPCell cell8 = new PdfPCell();
                    cell8.Padding = 5;
                    cell8.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell8.AddElement(GrossWPM);

                    PdfPCell cell9 = new PdfPCell();
                    cell9.Padding = 5;
                    cell9.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell9.AddElement(NetWPM);

                    table.AddCell(cell1);
                    table.AddCell(cell2);
                    table.AddCell(cell3);
                    table.AddCell(cell4);
                    table.AddCell(cell5);
                    table.AddCell(cell6);
                    table.AddCell(cell7);
                    table.AddCell(cell8);
                    table.AddCell(cell9);
                    count++;
                }

                Phrase p1 = new Phrase(counter.ToString(), new Font(Font.FontFamily.HELVETICA, FONT_SIZE, Font.NORMAL));
                PdfPCell cellp1 = new PdfPCell(p1);
                cellp1.Padding = 5;
                cellp1.HorizontalAlignment = Element.ALIGN_CENTER;

                Phrase p2 = new Phrase(m.Course, new Font(Font.FontFamily.HELVETICA, FONT_SIZE, Font.NORMAL));
                PdfPCell cellp2 = new PdfPCell(p2);
                cellp2.Padding = 5;
                cellp2.HorizontalAlignment = Element.ALIGN_CENTER;

                Phrase p3 = new Phrase(m.LastName + " " + m.FirstName + " " + m.FatherName + " " + m.MotherName, new Font(Font.FontFamily.HELVETICA, FONT_SIZE, Font.NORMAL));
                PdfPCell cellp3 = new PdfPCell(p3);
                cellp3.Padding = 5;
                cellp3.HorizontalAlignment = Element.ALIGN_LEFT;

                Phrase p4 = new Phrase(m.UserName.ToString(), new Font(Font.FontFamily.HELVETICA, FONT_SIZE, Font.NORMAL));
                PdfPCell cellp4 = new PdfPCell(p4);
                cellp4.Padding = 5;
                cellp4.HorizontalAlignment = Element.ALIGN_CENTER;

                //Phrase p5 = new Phrase(m.Grade, new Font(Font.FontFamily.HELVETICA, 8, Font.NORMAL));
                Phrase p5 = new Phrase(m.Level.ToString(), new Font(Font.FontFamily.HELVETICA, FONT_SIZE, Font.NORMAL));
                PdfPCell cellp5 = new PdfPCell(p5);
                cellp5.Padding = 5;
                cellp5.HorizontalAlignment = Element.ALIGN_CENTER;

                Phrase p6 = new Phrase(m.TimeSpanInNormal.ToString(), new Font(Font.FontFamily.HELVETICA, FONT_SIZE, Font.NORMAL));
                PdfPCell cellp6 = new PdfPCell(p6);
                cellp6.Padding = 5;
                cellp6.HorizontalAlignment = Element.ALIGN_CENTER;

                Phrase p7 = new Phrase(m.Accuracy.ToString() + "%", new Font(Font.FontFamily.HELVETICA, FONT_SIZE, Font.NORMAL));
                PdfPCell cellp7 = new PdfPCell(p7);
                cellp7.Padding = 5;
                cellp7.HorizontalAlignment = Element.ALIGN_CENTER;

                Phrase p8 = new Phrase(m.GrossWPM.ToString(), new Font(Font.FontFamily.HELVETICA, FONT_SIZE, Font.NORMAL));
                PdfPCell cellp8 = new PdfPCell(p8);
                cellp8.Padding = 5;
                cellp8.HorizontalAlignment = Element.ALIGN_CENTER;

                Phrase p9 = new Phrase(m.NetWPM.ToString(), new Font(Font.FontFamily.HELVETICA, FONT_SIZE, Font.NORMAL));
                PdfPCell cellp9 = new PdfPCell(p9);
                cellp9.Padding = 5;
                cellp9.HorizontalAlignment = Element.ALIGN_CENTER;

                table.AddCell(cellp1);
                table.AddCell(cellp2);
                table.AddCell(cellp3);
                table.AddCell(cellp4);
                table.AddCell(cellp5);
                table.AddCell(cellp6);
                table.AddCell(cellp7);
                table.AddCell(cellp8);
                table.AddCell(cellp9);

                if (count == reportCount)
                {
                    ColumnText tab1 = new ColumnText(writer.DirectContent);
                    //tab1.SetSimpleColumn(20, 820, 800, 80);
                    tab1.SetSimpleColumn(60, 490, 800, 80);
                    tab1.AddElement(table);
                    tab1.Go();
                    document.NewPage();
                    CreateNPStudentHeader(ref writer, ref document, config, reportName);
                    count = 0;
                }
                else
                {
                    count++;
                }
                if (counter == marklist)
                {
                    ColumnText tab0 = new ColumnText(writer.DirectContent);
                    //tab0.SetSimpleColumn(20, 820, 800, 80);
                    tab0.SetSimpleColumn(60, 480, 800, 80);
                    tab0.AddElement(table);
                    tab0.Go();
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    
}