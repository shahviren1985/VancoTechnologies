using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using ITM.Courses.DAO;
using System.Configuration;

/// <summary>
/// Summary description for PDFCertificateGenerator
/// </summary>
public class PDFCertificateGenerator
{
    private static int LOGO_IMAGE_COLLEGE_SCALE = 40;
    private static int LOGO_IMAGE_ADMINAPP_SCALE = 40;

    private static int LOGO_COLLEGE_POSITION_X = 70;
    private static int LOGO_ADMINAPP_POSITION_X = 600;

    private static int LOGO_POSITION_Y = 470;
    private static int COLLEGE_NAME_FONT_SIZE = 24;
    //private static int FONT_SIZE = 8;
    private static int FONT_SIZE = 9;
    private static int X_CO = 5;
    private static int RESULT = 75;

    public PDFCertificateGenerator()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    // create Pdf header
    public static void GenerateCertificateHeader(ref PdfWriter writer, ref Document document, ApplicationLogoHeader config)
    {
        try
        {
            string certificateBorder = @"D:\SVN-Projects\trunk\Course\WebPortal\static\images\certificate border4.jpg";

            // border
            /*using (Stream logoStream = new FileStream(certificateBorder, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                Image image = Image.GetInstance(logoStream);
                image.Alt = config.CollegeName;

                image.ScalePercent(36);
                image.SetAbsolutePosition(22, -5);
                document.Add(image);
            }*/

            // college Logog
            // commented by vasim on 19-03-2014
            /*using (Stream logoStream = new FileStream(config.LogoImagePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                Image image = Image.GetInstance(logoStream);
                image.Alt = config.CollegeName;

                image.ScalePercent(LOGO_IMAGE_COLLEGE_SCALE);
                image.SetAbsolutePosition(LOGO_COLLEGE_POSITION_X, LOGO_POSITION_Y);
                document.Add(image);
            }
            //end

            // adminapps logo
            using (Stream logoStream = new FileStream(config.LogoImagePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                Image image = Image.GetInstance(logoStream);
                image.Alt = config.CollegeName;
                image.ScalePercent(LOGO_IMAGE_ADMINAPP_SCALE);

                image.SetAbsolutePosition(LOGO_ADMINAPP_POSITION_X, LOGO_POSITION_Y);
                document.Add(image);
            }
            */
            //end
            Paragraph certificateHeaderPara = new Paragraph();
            certificateHeaderPara.Alignment = Element.ALIGN_CENTER;

            //Phrase certificateHeader = new Phrase("Certificate of Marit", new Font(Font.FontFamily.HELVETICA, COLLEGE_NAME_FONT_SIZE, Font.BOLD));
            certificateHeaderPara.Add(new Phrase("Certificate of Merit", new Font(Font.FontFamily.HELVETICA, COLLEGE_NAME_FONT_SIZE, Font.BOLD)));

            ColumnText collegeNameCT = new ColumnText(writer.DirectContent);

            collegeNameCT.SetSimpleColumn(300, 510, 800, 100);
            collegeNameCT.AddText(certificateHeaderPara);

            //collegeNameCT.Go();
        }
        catch (Exception ex)
        {
            // TODO - log an exception
            return;
        }
    }

    // create front certificate
    /*public static void GenerateCertificateContent(ref PdfWriter writer, ref Document document, UserDetails user, CourseDetail course)
    {
        try
        {
            Paragraph certificateContentPara = new Paragraph();
            Paragraph signatureOfPrincipalPara = new Paragraph();
            Paragraph datePara = new Paragraph();

            certificateContentPara.Alignment = Element.ALIGN_CENTER;
            signatureOfPrincipalPara.Alignment = Element.ALIGN_CENTER;
            datePara.Alignment = Element.ALIGN_CENTER;

            //certificateContentPara.Add(new Phrase("This is to certify that Mr./Ms. Vasim Ahmed" + Environment.NewLine + Environment.NewLine +            
            //"has successfully completed Fundamental of Computers." + Environment.NewLine + Environment.NewLine + "He/She has competed training of 100 hours.",
            //new Font(Font.FontFamily.HELVETICA, 20, Font.BOLDITALIC)));

            certificateContentPara.Add(new Phrase("This is to certify that Ms. " + user.FirstName.ToUpper() + " " + user.LastName.ToUpper() + Environment.NewLine + Environment.NewLine +
                "has successfully completed " + course.CourseName + "." + Environment.NewLine + Environment.NewLine + "She has completed training of 100 hours."
                + Environment.NewLine + Environment.NewLine + "She is completed course with " + user.Percentage + "% and '" + user.RollNumber + "' Grade.",
                new Font(Font.FontFamily.HELVETICA, 20, Font.BOLDITALIC)));

            signatureOfPrincipalPara.Add(new Phrase("Signature of Principal", new Font(Font.FontFamily.HELVETICA, 16, Font.BOLDITALIC)));
            datePara.Add(new Phrase("Date:-__________", new Font(Font.FontFamily.HELVETICA, 16, Font.BOLDITALIC)));

            ColumnText certificateContentCT = new ColumnText(writer.DirectContent);
            ColumnText signatureCT = new ColumnText(writer.DirectContent);
            ColumnText dateCT = new ColumnText(writer.DirectContent);

            certificateContentCT.SetSimpleColumn(150, 400, 800, 100);
            signatureCT.SetSimpleColumn(150, 200, 800, 100);
            dateCT.SetSimpleColumn(550, 200, 800, 100);

            certificateContentCT.AddText(certificateContentPara);
            signatureCT.AddText(signatureOfPrincipalPara);
            dateCT.AddText(datePara);

            certificateContentCT.Go();
            signatureCT.Go();
            dateCT.Go();
        }
        catch (Exception ex)
        {
            // TODO - log an exception
            return;
        }
    }*/

    public static void GenerateCertificateContent(ref PdfWriter writer, ref Document document, UserDetails user, CourseDetail course)
    {
        try
        {
            Paragraph certificateContentPara = new Paragraph();
            Paragraph coordPara = new Paragraph();


            Paragraph signatureOfPrincipalPara = new Paragraph();
            Paragraph datePara = new Paragraph();

            //This Below line is added by Rajesh 
            Paragraph coordName = new Paragraph();
            Paragraph principleName = new Paragraph();

            Paragraph instituteName = new Paragraph();
            Paragraph academy = new Paragraph();
            //Ends 

            certificateContentPara.Alignment = Element.ALIGN_CENTER;
            coordPara.Alignment = Element.ALIGN_CENTER;
            signatureOfPrincipalPara.Alignment = Element.ALIGN_CENTER;
            datePara.Alignment = Element.ALIGN_CENTER;

            //certificateContentPara.Add(new Phrase("This is to certify that Mr./Ms. Vasim Ahmed" + Environment.NewLine + Environment.NewLine +            
            //"has successfully completed Fundamental of Computers." + Environment.NewLine + Environment.NewLine + "He/She has competed training of 100 hours.",
            //new Font(Font.FontFamily.HELVETICA, 20, Font.BOLDITALIC)));

            course.EstimateTime = string.IsNullOrEmpty(course.EstimateTime) ? "100" : course.EstimateTime;
            /* Commented by Rajesh for new content
            certificateContentPara.Add(new Phrase("This is to certify that Ms. " + user.FirstName.ToUpper() + " " + user.LastName.ToUpper() + Environment.NewLine + Environment.NewLine +
                "has successfully completed " + course.CourseName + "." + Environment.NewLine + Environment.NewLine + "She has completed training of 100 hours."
                + Environment.NewLine + Environment.NewLine + "She has completed course with " + user.Percentage + "% and '" + user.RollNumber + "' Grade.",
                new Font(Font.FontFamily.HELVETICA, 20, Font.BOLDITALIC)));
            */
            /*
            Phrase phrase1 = new Phrase();
            Phrase phrase2 = new Phrase();

            phrase1.Add(new Chunk("This is to certify that Ms. " + user.FirstName.ToUpper() + " " + user.LastName.ToUpper() + " has successfully completed ", new Font(Font.FontFamily.HELVETICA, 18)));
            phrase2.Add(new Chunk("\"" + course.CourseName + ", internet and social media.\"", new Font(Font.FontFamily.HELVETICA, 18)));
            certificateContentPara.SetLeading(25f, 20f); 
            certificateContentPara.Add(phrase1);
            certificateContentPara.Add(phrase2);

            */

            certificateContentPara.Add(new Phrase("This is to certify that Ms. " + user.FirstName.ToUpper() + " " + user.LastName.ToUpper() + Environment.NewLine + Environment.NewLine +
                "has" + (user.RollNumber != "F"? " successfully" : "") + " completed " + '"' + course.CourseName + ", internet" + Environment.NewLine + Environment.NewLine + "and social media." + '"'
                + Environment.NewLine + Environment.NewLine + Environment.NewLine + "She has completed the course with 100 hours training," + Environment.NewLine + Environment.NewLine + "securing " + user.Percentage + "% with '" + user.RollNumber + "' Grade.",
                new Font(Font.FontFamily.HELVETICA, 18)));




            // Commented by Rajesh 
            // signatureOfPrincipalPara.Add(new Phrase("Signature of Principal", new Font(Font.FontFamily.HELVETICA, 16, Font.BOLDITALIC)));
            //coordPara.Add(new Phrase("Course Coordinator (MOOC Academy)", new Font(Font.FontFamily.HELVETICA, 16, Font.BOLDITALIC)));

            signatureOfPrincipalPara.Add(new Phrase("Principal", new Font(Font.FontFamily.HELVETICA, 14)));
            principleName.Add(new Phrase("Dr. Rajshree Trivedi", new Font(Font.FontFamily.HELVETICA, 14)));
            instituteName.Add(new Phrase("M.N.W.C.", new Font(Font.FontFamily.HELVETICA, 14)));

            coordPara.Add(new Phrase("Course Coordinator", new Font(Font.FontFamily.HELVETICA, 14)));
            coordName.Add(new Phrase("Viren Shah", new Font(Font.FontFamily.HELVETICA, 14)));
            academy.Add(new Phrase("MOOC Academy", new Font(Font.FontFamily.HELVETICA, 14)));

            //datePara.Add(new Phrase("Date: April 22, 2014", new Font(Font.FontFamily.HELVETICA, 16)));
            datePara.Add(new Phrase("Date: " + user.LastActivityDate, new Font(Font.FontFamily.HELVETICA, 16)));

            ColumnText certificateContentCT = new ColumnText(writer.DirectContent);
            ColumnText coOrdCT = new ColumnText(writer.DirectContent);
            ColumnText signatureCT = new ColumnText(writer.DirectContent);
            ColumnText dateCT = new ColumnText(writer.DirectContent);

            //Added by Rajesh

            ColumnText princpleNameCT = new ColumnText(writer.DirectContent);
            ColumnText instituteNameCT = new ColumnText(writer.DirectContent);
            ColumnText coordNameCT = new ColumnText(writer.DirectContent);
            ColumnText academyCT = new ColumnText(writer.DirectContent);

            //ends

            /*Rajesh
            certificateContentCT.SetSimpleColumn(160, 380, 800, 100);
            coOrdCT.SetSimpleColumn(290, 180, 800, 100);
            signatureCT.SetSimpleColumn(70, 180, 800, 100);
            dateCT.SetSimpleColumn(610, 180, 800, 100);
            */

            certificateContentCT.SetSimpleColumn(155, 370, 680, 30);
            //  certificateContentCT.SetLeading(1f, 25f);

            coOrdCT.SetSimpleColumn(365, 150, 800, 100);
            coordNameCT.SetSimpleColumn(390, 120, 800, 100); //Added
            academyCT.SetSimpleColumn(372, 90, 800, 30); //Added

            signatureCT.SetSimpleColumn(155, 150, 800, 100);
            princpleNameCT.SetSimpleColumn(115, 120, 800, 100); // Added
            instituteNameCT.SetSimpleColumn(155, 90, 800, 30); //Addeed

            dateCT.SetSimpleColumn(610, 150, 800, 100);

            certificateContentCT.AddText(certificateContentPara);

            coOrdCT.AddText(coordPara);
            coordNameCT.AddText(coordName);
            academyCT.AddText(academy);

            signatureCT.AddText(signatureOfPrincipalPara);
            princpleNameCT.AddText(principleName);
            instituteNameCT.AddText(instituteName);

            dateCT.AddText(datePara);


            certificateContentCT.Go();
            coOrdCT.Go();
            coordNameCT.Go();
            academyCT.Go();
            signatureCT.Go();
            princpleNameCT.Go();
            instituteNameCT.Go();
            dateCT.Go();
        }
        catch (Exception ex)
        {
            // TODO - log an exception
            return;
        }
    }

    // create back certificate
    /*public static void GenerateStudentCertificateBackPage(ref PdfWriter writer, ref Document document, PrintStudentCertifiacateBackProps students)
    {
        try
        {
            /* Creating student name paragraph * /
            Phrase studentName = new Phrase("Student Name : " + students.StudentFullName, new Font(Font.FontFamily.HELVETICA, 11, Font.BOLD));
            ColumnText studentNameCT = new ColumnText(writer.DirectContent);
            studentNameCT.Alignment = Element.ALIGN_LEFT;
            studentNameCT.SetSimpleColumn(70, 560, 800, 100);
            studentNameCT.AddText(studentName);
            studentNameCT.Go();

            / * Creating chapter performance table's header* /
            Phrase cpHeader = new Phrase("Performance Details", new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD));
            ColumnText cpHeaderCT = new ColumnText(writer.DirectContent);
            cpHeaderCT.Alignment = Element.ALIGN_LEFT;
            cpHeaderCT.SetSimpleColumn(70, 530, 800, 100);
            cpHeaderCT.AddText(cpHeader);
            cpHeaderCT.Go();

            Paragraph chapterName = new Paragraph();
            Paragraph chapterQuizScore = new Paragraph();

            chapterName.Alignment = Element.ALIGN_CENTER;
            chapterQuizScore.Alignment = Element.ALIGN_CENTER;

            chapterName.Add(new Phrase("Chapter Name", new Font(Font.FontFamily.HELVETICA, FONT_SIZE, Font.BOLD)));
            chapterQuizScore.Add(new Phrase("Quiz Score (%)", new Font(Font.FontFamily.HELVETICA, FONT_SIZE, Font.BOLD)));

            int count = 0;
            int counter = 0;

            int marklist = students.ChaptersPerformances.Count();
            PdfPTable table = null;

            foreach (ChapterPerformanceDetails m in students.ChaptersPerformances)
            {
                counter++;

                if (counter == 1)
                {
                    table = new PdfPTable(2);
                    float[] widths = new float[] { 160, 90 };
                    table.TotalWidth = 250f;
                    table.LockedWidth = true;
                    table.HorizontalAlignment = 0;
                    table.SetWidths(widths);

                    PdfPCell cell1 = new PdfPCell();
                    cell1.Padding = 5;
                    cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell1.AddElement(chapterName);

                    PdfPCell cell2 = new PdfPCell();
                    cell2.Padding = 5;
                    cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell2.AddElement(chapterQuizScore);

                    table.AddCell(cell1);
                    table.AddCell(cell2);

                    count++;
                }

                Phrase p1 = new Phrase(m.ChapterName, new Font(Font.FontFamily.HELVETICA, FONT_SIZE, Font.NORMAL));
                PdfPCell cellp1 = new PdfPCell(p1);
                cellp1.Padding = 5;
                cellp1.HorizontalAlignment = Element.ALIGN_LEFT;

                Phrase p2 = new Phrase(m.ChapterQuizScore.ToString(), new Font(Font.FontFamily.HELVETICA, FONT_SIZE, Font.NORMAL));
                PdfPCell cellp2 = new PdfPCell(p2);
                cellp2.Padding = 5;
                cellp2.HorizontalAlignment = Element.ALIGN_CENTER;

                table.AddCell(cellp1);
                table.AddCell(cellp2);

                count++;

                if (counter == marklist)
                {
                    ColumnText tab0 = new ColumnText(writer.DirectContent);
                    //tab0.SetSimpleColumn(20, 820, 800, 80);
                    tab0.SetSimpleColumn(70, 505, 800, 80);
                    tab0.AddElement(table);
                    tab0.Go();
                }
            }

            Phrase tpHeader = new Phrase("Typing Practice Performance", new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD));
            ColumnText tpHeaderCT = new ColumnText(writer.DirectContent);
            tpHeaderCT.Alignment = Element.ALIGN_LEFT;
            tpHeaderCT.SetSimpleColumn(360, 530, 800, 100);
            tpHeaderCT.AddText(tpHeader);
            tpHeaderCT.Go();

            Paragraph level = new Paragraph();
            Paragraph speed_wpm = new Paragraph();
            Paragraph accuracy = new Paragraph();

            level.Alignment = Element.ALIGN_CENTER;
            speed_wpm.Alignment = Element.ALIGN_CENTER;
            accuracy.Alignment = Element.ALIGN_CENTER;

            level.Add(new Phrase("Level", new Font(Font.FontFamily.HELVETICA, FONT_SIZE, Font.BOLD)));
            speed_wpm.Add(new Phrase("Speed (words/minute)", new Font(Font.FontFamily.HELVETICA, FONT_SIZE, Font.BOLD)));
            accuracy.Add(new Phrase("Accuracy (%)", new Font(Font.FontFamily.HELVETICA, FONT_SIZE, Font.BOLD)));

            int tpCount = 0;
            int tpCounter = 0;
            int tpNewTabCounter = 0;

            int tplist = students.TypingPerformances.Count();
            PdfPTable tpTable = null;

            foreach (TypingPerformanceDetails m in students.TypingPerformances)
            {
                tpCounter++;
                tpNewTabCounter++;
                //if (tpCounter == 1)
                if (tpNewTabCounter == 1)
                {
                    tpTable = new PdfPTable(3);
                    float[] widths = new float[] { 50, 90, 70 };
                    tpTable.TotalWidth = 190f;
                    tpTable.LockedWidth = true;
                    tpTable.HorizontalAlignment = 0;
                    tpTable.SetWidths(widths);

                    PdfPCell cell1 = new PdfPCell();
                    cell1.Padding = 5;
                    cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell1.AddElement(level);

                    PdfPCell cell2 = new PdfPCell();
                    cell2.Padding = 5;
                    cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell2.AddElement(speed_wpm);

                    PdfPCell cell3 = new PdfPCell();
                    cell3.Padding = 5;
                    cell3.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell3.AddElement(accuracy);

                    tpTable.AddCell(cell1);
                    tpTable.AddCell(cell2);
                    tpTable.AddCell(cell3);

                    tpCount++;
                }

                Phrase p1 = new Phrase(m.Level.ToString(), new Font(Font.FontFamily.HELVETICA, FONT_SIZE, Font.NORMAL));
                PdfPCell cellp1 = new PdfPCell(p1);
                cellp1.Padding = 5;
                cellp1.HorizontalAlignment = Element.ALIGN_CENTER;

                Phrase p2 = new Phrase(m.Speed_WPM.ToString(), new Font(Font.FontFamily.HELVETICA, FONT_SIZE, Font.NORMAL));
                PdfPCell cellp2 = new PdfPCell(p2);
                cellp2.Padding = 5;
                cellp2.HorizontalAlignment = Element.ALIGN_CENTER;

                Phrase p3 = new Phrase(m.Accuracy.ToString(), new Font(Font.FontFamily.HELVETICA, FONT_SIZE, Font.NORMAL));
                PdfPCell cellp3 = new PdfPCell(p3);
                cellp3.Padding = 5;
                cellp3.HorizontalAlignment = Element.ALIGN_CENTER;

                tpTable.AddCell(cellp1);
                tpTable.AddCell(cellp2);
                tpTable.AddCell(cellp3);

                tpCount++;

                if (tplist > 12)
                {
                    if (tpNewTabCounter == 12)
                    {
                        ColumnText tab0 = new ColumnText(writer.DirectContent);
                        //tab0.SetSimpleColumn(20, 820, 800, 80);
                        tab0.SetSimpleColumn(360, 505, 800, 80);
                        tab0.AddElement(tpTable);
                        tab0.Go();
                        tpNewTabCounter = 0;
                        tpTable = null;
                    }
                    else if (tpCounter == tplist)
                    {
                        ColumnText tab0 = new ColumnText(writer.DirectContent);
                        //tab0.SetSimpleColumn(20, 820, 800, 80);
                        tab0.SetSimpleColumn(585, 505, 800, 80);
                        tab0.AddElement(tpTable);
                        tab0.Go();
                    }
                }
                else
                {
                    if (tpCounter == tplist)
                    {
                        ColumnText tab0 = new ColumnText(writer.DirectContent);
                        //tab0.SetSimpleColumn(20, 820, 800, 80);
                        tab0.SetSimpleColumn(360, 505, 800, 80);
                        tab0.AddElement(tpTable);
                        tab0.Go();
                    }
                }
            }
        }
        catch (Exception ex)
        {
        }
    }*/

    public static void GenerateStudentCertificateBackPage(ref PdfWriter writer, ref Document document, PrintStudentCertifiacateBackProps students)
    {
        try
        {
            /* Creating student name paragraph */
            Phrase studentName = new Phrase("Student Name : " + students.StudentFullName, new Font(Font.FontFamily.HELVETICA, 11, Font.BOLD));
            ColumnText studentNameCT = new ColumnText(writer.DirectContent);
            studentNameCT.Alignment = Element.ALIGN_LEFT;
            studentNameCT.SetSimpleColumn(70, 560, 800, 100);
            studentNameCT.AddText(studentName);
            studentNameCT.Go();

            /* Creating chapter performance table's header*/
            Phrase cpHeader = new Phrase("Performance Details", new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD));
            ColumnText cpHeaderCT = new ColumnText(writer.DirectContent);
            cpHeaderCT.Alignment = Element.ALIGN_LEFT;
            cpHeaderCT.SetSimpleColumn(70, 540, 800, 100);
            cpHeaderCT.AddText(cpHeader);
            cpHeaderCT.Go();

            Paragraph chapterName = new Paragraph();
            Paragraph chapterQuizScore = new Paragraph();

            chapterName.Alignment = Element.ALIGN_CENTER;
            chapterQuizScore.Alignment = Element.ALIGN_CENTER;

            chapterName.Add(new Phrase("Chapter Name", new Font(Font.FontFamily.HELVETICA, FONT_SIZE, Font.BOLD)));
            chapterQuizScore.Add(new Phrase("Quiz Score (%)", new Font(Font.FontFamily.HELVETICA, FONT_SIZE, Font.BOLD)));

            int count = 0;
            int counter = 0;

            int marklist = students.ChaptersPerformances.Count();
            PdfPTable table = null;

            foreach (ChapterPerformanceDetails m in students.ChaptersPerformances)
            {
                counter++;

                if (counter == 1)
                {
                    table = new PdfPTable(2);
                    float[] widths = new float[] { 160, 90 };
                    table.TotalWidth = 250f;
                    table.LockedWidth = true;
                    table.HorizontalAlignment = 0;
                    table.SetWidths(widths);

                    PdfPCell cell1 = new PdfPCell();
                    cell1.Padding = 5;
                    cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell1.AddElement(chapterName);

                    PdfPCell cell2 = new PdfPCell();
                    cell2.Padding = 5;
                    cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell2.AddElement(chapterQuizScore);

                    table.AddCell(cell1);
                    table.AddCell(cell2);

                    count++;
                }

                Phrase p1 = new Phrase(m.ChapterName, new Font(Font.FontFamily.HELVETICA, FONT_SIZE, Font.NORMAL));
                PdfPCell cellp1 = new PdfPCell(p1);
                cellp1.Padding = 5;
                cellp1.HorizontalAlignment = Element.ALIGN_LEFT;

                Phrase p2 = new Phrase(m.ChapterQuizScore.ToString(), new Font(Font.FontFamily.HELVETICA, FONT_SIZE, Font.NORMAL));
                PdfPCell cellp2 = new PdfPCell(p2);
                cellp2.Padding = 5;
                cellp2.HorizontalAlignment = Element.ALIGN_CENTER;

                table.AddCell(cellp1);
                table.AddCell(cellp2);

                count++;

                if (counter == marklist)
                {
                    ColumnText tab0 = new ColumnText(writer.DirectContent);
                    //tab0.SetSimpleColumn(20, 820, 800, 80);
                    tab0.SetSimpleColumn(70, 515, 800, 80);
                    tab0.AddElement(table);
                    tab0.Go();
                }
            }

            Phrase tpHeader = new Phrase("Typing Practice Performance", new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD));
            ColumnText tpHeaderCT = new ColumnText(writer.DirectContent);
            tpHeaderCT.Alignment = Element.ALIGN_LEFT;
            tpHeaderCT.SetSimpleColumn(360, 540, 800, 100);
            tpHeaderCT.AddText(tpHeader);

            //if (students.TypingPerformances.Count() > 0)
            tpHeaderCT.Go();

            Paragraph level = new Paragraph();
            Paragraph speed_wpm = new Paragraph();
            Paragraph accuracy = new Paragraph();

            level.Alignment = Element.ALIGN_CENTER;
            speed_wpm.Alignment = Element.ALIGN_CENTER;
            accuracy.Alignment = Element.ALIGN_CENTER;

            level.Add(new Phrase("Level", new Font(Font.FontFamily.HELVETICA, FONT_SIZE, Font.BOLD)));
            speed_wpm.Add(new Phrase("Speed (words/minute)", new Font(Font.FontFamily.HELVETICA, FONT_SIZE, Font.BOLD)));
            accuracy.Add(new Phrase("Accuracy (%)", new Font(Font.FontFamily.HELVETICA, FONT_SIZE, Font.BOLD)));

            int tpCount = 0;
            int tpCounter = 0;
            int tpNewTabCounter = 0;

            int tplist = students.TypingPerformances.Count();
            PdfPTable tpTable = null;

            foreach (TypingPerformanceDetails m in students.TypingPerformances)
            {
                tpCounter++;
                tpNewTabCounter++;
                //if (tpCounter == 1)
                if (tpNewTabCounter == 1)
                {
                    tpTable = new PdfPTable(3);
                    float[] widths = new float[] { 50, 90, 70 };
                    tpTable.TotalWidth = 190f;
                    tpTable.LockedWidth = true;
                    tpTable.HorizontalAlignment = 0;
                    tpTable.SetWidths(widths);

                    PdfPCell cell1 = new PdfPCell();
                    cell1.Padding = 5;
                    cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell1.AddElement(level);

                    PdfPCell cell2 = new PdfPCell();
                    cell2.Padding = 5;
                    cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell2.AddElement(speed_wpm);

                    PdfPCell cell3 = new PdfPCell();
                    cell3.Padding = 5;
                    cell3.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell3.AddElement(accuracy);

                    tpTable.AddCell(cell1);
                    tpTable.AddCell(cell2);
                    tpTable.AddCell(cell3);

                    tpCount++;
                }

                Phrase p1 = new Phrase(m.Level.ToString(), new Font(Font.FontFamily.HELVETICA, FONT_SIZE, Font.NORMAL));
                PdfPCell cellp1 = new PdfPCell(p1);
                cellp1.Padding = 5;
                cellp1.HorizontalAlignment = Element.ALIGN_CENTER;

                Phrase p2 = new Phrase(m.Speed_WPM.ToString(), new Font(Font.FontFamily.HELVETICA, FONT_SIZE, Font.NORMAL));
                PdfPCell cellp2 = new PdfPCell(p2);
                cellp2.Padding = 5;
                cellp2.HorizontalAlignment = Element.ALIGN_CENTER;

                Phrase p3 = new Phrase(m.Accuracy.ToString(), new Font(Font.FontFamily.HELVETICA, FONT_SIZE, Font.NORMAL));
                PdfPCell cellp3 = new PdfPCell(p3);
                cellp3.Padding = 5;
                cellp3.HorizontalAlignment = Element.ALIGN_CENTER;

                tpTable.AddCell(cellp1);
                tpTable.AddCell(cellp2);
                tpTable.AddCell(cellp3);

                tpCount++;

                if (tplist > 12)
                {
                    if (tpNewTabCounter == 12)
                    {
                        ColumnText tab0 = new ColumnText(writer.DirectContent);
                        //tab0.SetSimpleColumn(20, 820, 800, 80);
                        tab0.SetSimpleColumn(360, 515, 800, 80);
                        tab0.AddElement(tpTable);
                        tab0.Go();
                        tpNewTabCounter = 0;
                        tpTable = null;
                    }
                    else if (tpCounter == tplist)
                    {
                        ColumnText tab0 = new ColumnText(writer.DirectContent);
                        //tab0.SetSimpleColumn(20, 820, 800, 80);
                        tab0.SetSimpleColumn(585, 515, 800, 80);
                        tab0.AddElement(tpTable);
                        tab0.Go();
                    }
                }
                else
                {
                    if (tpCounter == tplist)
                    {
                        ColumnText tab0 = new ColumnText(writer.DirectContent);
                        //tab0.SetSimpleColumn(20, 820, 800, 80);
                        tab0.SetSimpleColumn(360, 515, 800, 80);
                        tab0.AddElement(tpTable);
                        tab0.Go();
                    }
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
}