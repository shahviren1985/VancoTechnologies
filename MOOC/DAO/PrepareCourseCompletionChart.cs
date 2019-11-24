using ITM.Courses.DAOBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITM.Courses.DAO
{
    public class CourseCompletionPercentChart
    {
        public decimal value { get; set; }
        public string color { get; set; }
    }

    public class PrepareCourseCompletionPercentChart
    {
        public List<CourseCompletionPercentChart> GetCourseCompletionChartData(int userId, int courseId, string cnxnString, string logPath)
        {
            try
            {
                List<ChapterDetails> chapters = new ChapterDetailsDAO().GetAllChaptersByCourse(courseId, cnxnString, logPath);

                if (chapters != null)
                {
                    int totalChapters = 0, completeCount = 0, pendingCount = 0;

                    //get all sections by course
                    List<ChapterSection> chaptersSections = new ChapterSectionDAO().GetAllSectionsByCourse(courseId, cnxnString, logPath);
                    if (chaptersSections == null) chaptersSections = new List<ChapterSection>();

                    //get all chapter quiz by course
                    List<ChapterQuizMaster> chapterQuiz = new ChapterQuizMasterDAO().GetAllChapterQuizsByCourse(courseId, cnxnString, logPath);
                    if (chapterQuiz == null) chapterQuiz = new List<ChapterQuizMaster>();

                    //get all quiz responce by course
                    List<ChapterQuizResponse> quizResponce = new ChapterQuizResponseDAO().GetAllChapterQuizResponseByUserCourse(courseId, userId, cnxnString, logPath);
                    if (quizResponce == null) quizResponce = new List<ChapterQuizResponse>();

                    //get all userchapter status by course
                    List<UserChapterStatus> userChapters = new UserChapterStatusDAO().GetChapterStatusByCourseUserId(courseId, userId, cnxnString, logPath);
                    if (userChapters == null) userChapters = new List<UserChapterStatus>();

                    foreach (ChapterDetails chapter in chapters)
                    {
                        totalChapters++;

                        if (IsUserCompleteChapter(chapter.Id, userId, chaptersSections, chapterQuiz, quizResponce, userChapters, cnxnString, logPath))
                        {
                            completeCount++;
                        }
                    }

                    pendingCount = totalChapters - completeCount;

                    decimal completePer = Math.Round((decimal.Parse(completeCount.ToString()) / decimal.Parse(totalChapters.ToString())) * 100, 2);
                    decimal pendingPer = Math.Round((decimal.Parse(pendingCount.ToString()) / decimal.Parse(totalChapters.ToString())) * 100, 2);

                    List<CourseCompletionPercentChart> courseComlpetionChart = new List<CourseCompletionPercentChart>();

                    courseComlpetionChart.Add(new CourseCompletionPercentChart() { value = completePer, color = "#7FFF00" });
                    courseComlpetionChart.Add(new CourseCompletionPercentChart() { value = pendingPer, color = "#F7464A" });

                    return courseComlpetionChart;
                }
                else
                {
                    List<CourseCompletionPercentChart> courseComlpetionChart = new List<CourseCompletionPercentChart>();

                    courseComlpetionChart.Add(new CourseCompletionPercentChart() { value = 0, color = "#7FFF00" });
                    courseComlpetionChart.Add(new CourseCompletionPercentChart() { value = 100, color = "#F7464A" });

                    return courseComlpetionChart;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                /* killing sleeping connections */
                Database.KillConnections(cnxnString, logPath);
            }
        }

        public bool IsUserCompleteChapter(int chapterId, int userId, List<ChapterSection> _sections, List<ChapterQuizMaster> _quiz, List<ChapterQuizResponse> _quizResponce, List<UserChapterStatus> _userChapters, string cnxnString, string logPath)
        {
            try
            {
                //List<ChapterSection> chapterSections = new ChapterSectionDAO().GetChapterSectionsByChapterId(chapterId, cnxnString, logPath);
                List<ChapterSection> chapterSections = _sections.FindAll(s => s.ChapterId == chapterId);

                if (chapterSections != null)
                {
                    foreach (ChapterSection section in chapterSections)
                    {
                        //List<ChapterQuizMaster> chapterQuizs = new ChapterQuizMasterDAO().GetChapterQuizByChapterSection(chapterId, section.Id, cnxnString, logPath);
                        List<ChapterQuizMaster> chapterQuizs = _quiz.FindAll(q => q.ChapterId == chapterId && q.SectionId == section.Id);

                        if (chapterQuizs != null && chapterQuizs.Count > 0)
                        {
                            foreach (ChapterQuizMaster chpQiz in chapterQuizs)
                            {
                                //ChapterQuizResponse chptQuzRes = new ChapterQuizResponseDAO().GetChapterQuizByUserQuesionId(userId, chpQiz.Id, cnxnString, logPath);
                                ChapterQuizResponse chptQuzRes = _quizResponce.Find(qr => qr.UserId == userId && qr.QuestionId == chpQiz.Id);

                                if (chptQuzRes == null)
                                {
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            //UserChapterStatus userChapterStatus = new UserChapterStatusDAO().GetUserChapterSectionByUserChapterSectionId(userId, chapterId, section.Id, cnxnString, logPath);
                            UserChapterStatus userChapterStatus = _userChapters.Find(uc => uc.UserId == userId && uc.ChapterId == chapterId && uc.SectionId == section.Id);

                            if (userChapterStatus == null)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }


        /// <summary>
        /// Get User's Course status(which section is completed or not.), Added by vasim on 08-dec-14
        /// </summary>
        /// <param name="chapterId"></param>
        /// <param name="userId"></param>
        /// <param name="sectionId"></param>
        /// <param name="_quiz"></param>
        /// <param name="_quizResponce"></param>
        /// <param name="_userChapters"></param>
        /// <param name="cnxnString"></param>
        /// <param name="logPath"></param>
        /// <returns></returns>
        public bool IsUserCompleteChapterSection(int chapterId, int userId, int sectionId, List<ChapterQuizMaster> _quiz, List<ChapterQuizResponse> _quizResponce, List<UserChapterStatus> _userChapters, string cnxnString, string logPath, out string completedDate)
        {
            completedDate = string.Empty;

            try
            {
                List<ChapterQuizMaster> chapterQuizs = _quiz.FindAll(q => q.ChapterId == chapterId && q.SectionId == sectionId);

                if (chapterQuizs != null && chapterQuizs.Count > 0)
                {
                    foreach (ChapterQuizMaster chpQiz in chapterQuizs)
                    {
                        ChapterQuizResponse chptQuzRes = _quizResponce.Find(qr => qr.UserId == userId && qr.QuestionId == chpQiz.Id);

                        if (chptQuzRes == null)
                        {
                            return false;
                        }

                        completedDate = new ITM.Courses.Utilities.TimeFormats().GetIndianStandardTime(chptQuzRes.DateTime).ToString("dd-MMM-yyyy");
                    }
                }
                else
                {
                    UserChapterStatus userChapterStatus = _userChapters.Find(uc => uc.UserId == userId && uc.ChapterId == chapterId && uc.SectionId == sectionId);

                    if (userChapterStatus == null)
                    {
                        return false;
                    }

                    completedDate = new ITM.Courses.Utilities.TimeFormats().GetIndianStandardTime(userChapterStatus.DateCreated).ToString("dd/MMM/yyyy");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }
    }
}
