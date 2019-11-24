using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITM.Courses.DAO
{
    public class UserTimeEstimateChart
    {
        public List<string> labels { get; set; }
        public List<UTEDataSets> datasets { get; set; }
    }

    public class UTEDataSets
    {
        public string fillColor { get; set; }
        public string strokeColor { get; set; }        
        public List<decimal> data { get; set; }
    }

    public class PrepareTimeEstimateChart
    {
        public UserTimeEstimateChart GetUserTimeEstimateChartData(int userId, int courseId,string cnxnString, string logPath)
        {
            try
            {
                List<ChapterDetails> chapters = new ChapterDetailsDAO().GetAllChaptersByCourse(courseId, cnxnString, logPath);

                if (chapters != null)
                {
                    List<string> label = new List<string>();
                    List<UTEDataSets> chartDataList = new List<UTEDataSets>();

                    List<decimal> dataTimeEstimateList = new List<decimal>();
                    List<decimal> dataTimeTakenUserList = new List<decimal>();

                    foreach (ChapterDetails chapter in chapters)
                    {
                        label.Add(chapter.Title);

                        decimal timeEstimate = new ChapterSectionDAO().GetChapterEstimateTimeInHours(chapter.Id, cnxnString, logPath);
                        decimal timeTakenUser = new UserTimeTrackerDAO().GetTimeTakenByUserInHours(userId, chapter.Id, cnxnString, logPath);

                        dataTimeEstimateList.Add(timeEstimate);
                        dataTimeTakenUserList.Add(timeTakenUser);
                    }

                    UTEDataSets dataSetsActual = new UTEDataSets();
                    dataSetsActual.fillColor = "rgba(146, 144, 144, 0.5)";
                    dataSetsActual.strokeColor = "rgba(146, 144, 144, 1)";                    
                    dataSetsActual.data = dataTimeEstimateList;

                    UTEDataSets dataSetsStudent = new UTEDataSets();
                    dataSetsStudent.fillColor = "rgba(151,187,205,0.5)";
                    dataSetsStudent.strokeColor = "rgba(151,187,205,1)";                    
                    dataSetsStudent.data = dataTimeTakenUserList;

                    chartDataList.Add(dataSetsActual);
                    chartDataList.Add(dataSetsStudent);

                    UserTimeEstimateChart chart = new UserTimeEstimateChart();
                    chart.labels = label;
                    chart.datasets = chartDataList;

                    return chart;
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
