using ITM.Courses.DAOBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITM.Courses.DAO
{

    public class TypingStatsChart
    {
        public List<string> labels { get; set; }
        public List<TypingStatsData> datasets { get; set; }
    }

    public class TypingStatsData
    {
        public string fillColor { get; set; }
        public string strokeColor { get; set; }
        public string pointColor { get; set; }
        public string pointStrokeColor { get; set; }
        public List<int> data { get; set; }
    }

    public class PrepareTypingStatsChart
    {
        public TypingStatsChart GetTypingStatsChartData(int userId, bool isLimit, int limit, string cnxnString, string logPath)
        {
            try
            {
                List<StudentTypingStats> studentsTypingStats = new StudentTypingStatsDAO().GetTypingStatsByUserId(userId, isLimit, limit, cnxnString, logPath);

                if (studentsTypingStats != null)
                {
                    studentsTypingStats = studentsTypingStats.OrderBy(st => st.Level).ToList();

                    List<string> label = new List<string>();
                    List<TypingStatsData> chartDataSet = new List<TypingStatsData>();

                    List<int> userStatsData = new List<int>();
                    List<int> acualStatsData = new List<int>();

                    foreach (StudentTypingStats stats in studentsTypingStats)
                    {
                        label.Add("L-" + stats.Level.ToString());
                        userStatsData.Add(stats.GrossWPM);
                        acualStatsData.Add(50);
                    }

                    TypingStatsData dataSetsActual = new TypingStatsData();
                    dataSetsActual.fillColor = "rgba(220,220,220,0.5)";
                    dataSetsActual.strokeColor = "rgba(220,220,220,1)";
                    dataSetsActual.pointColor = "rgba(220,220,220,1)";
                    dataSetsActual.pointStrokeColor = "#fff";
                    dataSetsActual.data = acualStatsData;

                    TypingStatsData dataSetsStudent = new TypingStatsData();
                    dataSetsStudent.fillColor = "rgba(151,187,205,0.5)";
                    dataSetsStudent.strokeColor = "rgba(151,187,205,1)";
                    dataSetsStudent.pointColor = "rgba(151,187,205,1)";
                    dataSetsStudent.pointStrokeColor = "#fff";
                    dataSetsStudent.data = userStatsData;

                    chartDataSet.Add(dataSetsActual);
                    chartDataSet.Add(dataSetsStudent);

                    TypingStatsChart chart = new TypingStatsChart();
                    chart.labels = label;
                    chart.datasets = chartDataSet;

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
