using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITM.Courses.DAO
{
    /// <summary>
    /// For line chart
    /// </summary>
    public class ChapterwisePerformanceChart
    {
        public List<string> labels { get; set; }
        public List<DataSets> datasets { get; set; }
    }

    public class DataSets
    {
        public string fillColor { get; set; }
        public string strokeColor { get; set; }
        public string pointColor { get; set; }
        public string pointStrokeColor { get; set; }
        public List<int> data { get; set; }
    }


    public class PrepareChapterPerformnaceChart
    {
        public ChapterwisePerformanceChart GetChapterwiseChartData(int userId, string cnxnString, string logPath)
        {
            ChapterQuizResponseDAO chpQuzResDAO = new ChapterQuizResponseDAO();

            try
            {
                List<string> Labels = new List<string>();
                List<DataSets> chartDataSet = new List<DataSets>();

                List<int> chapterIds = chpQuzResDAO.GetDistinctChapterIdsbyUserId(userId, cnxnString, logPath);

                if (chapterIds != null)
                {
                    chapterIds.Sort();

                    if (chapterIds != null && chapterIds.Count > 0)
                    {
                        List<int> dataActualPercentList = new List<int>();
                        List<int> dataStudentGotList = new List<int>();

                        foreach (int chapterId in chapterIds)
                        {
                            int dataActualPercent = 0;
                            int dataStudentGot = 0;

                            ChapterDetails objChapter = new ChapterDetailsDAO().GetChapterDetails(chapterId, cnxnString, logPath);
                            Labels.Add(objChapter.Title);

                            int totalQuizInChapter = 0;
                            int userPassCount = 0;
                            int userFailCount = 0;

                            List<ChapterSection> chapterSections = new ChapterSectionDAO().GetChapterSectionsByChapterId(chapterId, cnxnString, logPath);
                            if (chapterSections != null)
                            {
                                foreach (ChapterSection section in chapterSections)
                                {
                                    List<ChapterQuizMaster> chapterQuizs = new ChapterQuizMasterDAO().GetChapterQuizByChapterSection(chapterId, section.Id, cnxnString, logPath);

                                    if (chapterQuizs != null)
                                    {
                                        foreach (ChapterQuizMaster chpQiz in chapterQuizs)
                                        {
                                            totalQuizInChapter++;

                                            ChapterQuizResponse chptQuzRes = new ChapterQuizResponseDAO().GetChapterQuizByUserQuesionId(userId, chpQiz.Id, cnxnString, logPath);
                                            if (chptQuzRes == null)
                                            {
                                                userFailCount++;
                                            }
                                            else
                                            {
                                                if (chptQuzRes.IsCorrect)
                                                {
                                                    userPassCount++;
                                                }
                                                else
                                                {
                                                    userFailCount++;
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            // calculating percentage
                            if (userPassCount > 0 && totalQuizInChapter > 0)
                            {
                                dataActualPercent = 100;
                                dataStudentGot = (userPassCount * 100) / totalQuizInChapter;

                                int failPer = (userFailCount * 100) / totalQuizInChapter;
                                //decimal passPer = Math.Round((decimal.Parse(userPassCount.ToString()) / decimal.Parse(totalQuizInChapter.ToString())) * 100, 2);
                                //decimal failPer = Math.Round((decimal.Parse(userFailCount.ToString()) / decimal.Parse(totalQuizInChapter.ToString())) * 100, 2);
                            }
                            else if (totalQuizInChapter > 0)
                            {
                                dataStudentGot = 0;
                                dataActualPercent = 100;
                            }
                            else
                            {
                                dataStudentGot = 0;
                                dataActualPercent = 0;
                            }

                            dataActualPercentList.Add(dataActualPercent);
                            dataStudentGotList.Add(dataStudentGot);
                        }

                        DataSets dataSetsActual = new DataSets();
                        dataSetsActual.fillColor = "rgba(220,220,220,0.5)";
                        dataSetsActual.strokeColor = "rgba(220,220,220,1)";
                        dataSetsActual.pointColor = "rgba(220,220,220,1)";
                        dataSetsActual.pointStrokeColor = "#fff";
                        dataSetsActual.data = dataActualPercentList;

                        DataSets dataSetsStudent = new DataSets();
                        dataSetsStudent.fillColor = "rgba(151,187,205,0.5)";
                        dataSetsStudent.strokeColor = "rgba(151,187,205,1)";
                        dataSetsStudent.pointColor = "rgba(151,187,205,1)";
                        dataSetsStudent.pointStrokeColor = "#fff";
                        dataSetsStudent.data = dataStudentGotList;

                        chartDataSet.Add(dataSetsActual);
                        chartDataSet.Add(dataSetsStudent);

                        ChapterwisePerformanceChart chart = new ChapterwisePerformanceChart();
                        chart.labels = Labels;
                        chart.datasets = chartDataSet;

                        return chart;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return null;
        }
    }
}
