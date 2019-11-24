using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITM.Courses.DAO
{

    public class ChapterwisePercentialList
    {
        public decimal value { get; set; }
        public string color { get; set; }
    }

    public class PreparAdminChapterwiseChart
    {
        public List<ChapterwisePercentialList> getData(int chapterID, List<UserDetails> userList, string cnxnString, string logPath)
        {
            try
            {
                //int rightCount = 0;
                //int wrongCount = 0;
                //int totalCount = 0;

                ChapterQuizMasterDAO objChapterQuizMasterDAO = new ChapterQuizMasterDAO();
                //UserDetailsDAO objUserDetailsDAO = new UserDetailsDAO();
                ChapterQuizResponseDAO objChapterQuizResponseDAO = new ChapterQuizResponseDAO();

                List<ChapterQuizMaster> questionCount = objChapterQuizMasterDAO.GetChapterQuizByChapterId(chapterID, cnxnString, logPath);
                //List<UserDetails> userList = objUserDetailsDAO.GetUserByUserTypeAndOtherStatus(true, cnxnString, logPath);

                if (questionCount != null && userList != null)
                {
                    foreach (UserDetails user in userList)
                    {
                        int rightCount = 0;
                        int wrongCount = 0;
                        int totalCount = 0;

                        foreach (ChapterQuizMaster quiz in questionCount)
                        {
                            totalCount++;

                            ChapterQuizResponse response = objChapterQuizResponseDAO.GetChapterQuizByUserQuesionId(user.Id, quiz.Id, cnxnString, logPath);

                            if (response == null)
                            {
                                wrongCount++;
                            }
                            else
                            {
                                if (response.IsCorrect)
                                {
                                    rightCount++;
                                }
                                else
                                {
                                    wrongCount++;
                                }
                            }
                        }

                        //decimal completePer = Math.Round((Convert.ToDecimal(rightCount) / Convert.ToDecimal(questionCount.Count)) * 100, 2);
                        decimal completePer = Math.Round((Convert.ToDecimal(rightCount) / Convert.ToDecimal(totalCount)) * 100, 2);

                        user.Percentage = completePer;
                    }

                    int less40 = 0;
                    int greaterOrequal40_and_less50 = 0;
                    int greaterOrequal50_and_less60 = 0;
                    int greaterOrequal60_and_less70 = 0;
                    int greaterOrequal70 = 0;

                    foreach (UserDetails userdetails in userList)
                    {
                        if (userdetails.Percentage < 40)
                        {
                            less40++;
                        }
                        else if (userdetails.Percentage >= 40 && userdetails.Percentage < 50)
                        {
                            greaterOrequal40_and_less50++;
                        }
                        else if (userdetails.Percentage >= 50 && userdetails.Percentage < 60)
                        {
                            greaterOrequal50_and_less60++;
                        }
                        else if (userdetails.Percentage >= 60 && userdetails.Percentage < 70)
                        {
                            greaterOrequal60_and_less70++;
                        }
                        else if (userdetails.Percentage >= 70)
                        {
                            greaterOrequal70++;
                        }
                    }

                    List<ChapterwisePercentialList> courseComlpetionChart = new List<ChapterwisePercentialList>();

                    courseComlpetionChart.Add(new ChapterwisePercentialList() { value = less40, color = "#FF0000" });
                    courseComlpetionChart.Add(new ChapterwisePercentialList() { value = greaterOrequal40_and_less50, color = "#FE9A2E" });
                    courseComlpetionChart.Add(new ChapterwisePercentialList() { value = greaterOrequal50_and_less60, color = "#5858FA" });
                    courseComlpetionChart.Add(new ChapterwisePercentialList() { value = greaterOrequal60_and_less70, color = "#F4FA58" });
                    courseComlpetionChart.Add(new ChapterwisePercentialList() { value = greaterOrequal70, color = "#3ADF00" });

                    return courseComlpetionChart;
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
