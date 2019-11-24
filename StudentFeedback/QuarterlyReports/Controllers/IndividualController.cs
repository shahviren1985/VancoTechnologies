using Newtonsoft.Json;
using QuarterlyReports.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QuarterlyReports.Controllers
{
    public class IndividualController : ApiController
    {
          GetDataFromDatabase db;
          public IndividualController()
        {
            db = new GetDataFromDatabase();
        }

        public HttpResponseMessage AddIndividual(Individual model)
        {
            try
            {
                if (model == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Data");

                string json = JsonConvert.SerializeObject(model);
                string folderPath = System.Web.Configuration.WebConfigurationManager.AppSettings["folderPath"];
                string fileName = folderPath + typeof(Individual).Name + "_" + DateTime.Now.ToString("yyyy-MM-dd HH mm ss") + ".json";

                //write string to file
                System.IO.File.WriteAllText(fileName, json);
                string replacedJson = json.Replace("'", "|");
                model = JsonConvert.DeserializeObject<Individual>(replacedJson);


                int cId = db.InsertIndividual(model);
                if (cId > 0 && model.Publications != null)
                {
                    if (model.Publications.ArticlesInJournals != null && model.Publications.ArticlesInJournals.Count > 0)
                    {
                        foreach (ArticlesInJournal articlesInJournal in model.Publications.ArticlesInJournals)
                        {
                            articlesInJournal.IndividualId = cId;
                            bool isInserted = db.InsertArticlesInJournals(articlesInJournal);
                            if (!isInserted)
                                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Data for ArticlesInJournals");
                        }
                    }
                    if (model.Publications.ProcInConferenceSeminar != null && model.Publications.ProcInConferenceSeminar.Count > 0)
                    {
                        foreach (ProcInConferenceSeminar procInConferenceSeminar in model.Publications.ProcInConferenceSeminar)
                        {
                            procInConferenceSeminar.IndividualId = cId;
                            bool isInserted = db.InsertProcInConferenceSeminar(procInConferenceSeminar);
                            if (!isInserted)
                                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Data for ProcInConferenceSeminar");
                        }
                    }
                    if (model.Publications.ChapterInBooks != null && model.Publications.ChapterInBooks.Count > 0)
                    {
                        foreach (ChapterInBook chapterInBooks in model.Publications.ChapterInBooks)
                        {
                            chapterInBooks.IndividualId = cId;
                            bool isInserted = db.InsertChapterInBooks(chapterInBooks);
                            if (!isInserted)
                                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Data for ChapterInBooks");
                        }
                    }
                    if (model.Publications.BookEditedWritten != null && model.Publications.BookEditedWritten.Count > 0)
                    {
                        foreach (BookEditedWritten bookEditedWritten in model.Publications.BookEditedWritten)
                        {
                            bookEditedWritten.IndividualId = cId;
                            bool isInserted = db.InsertBookEditedWritten(bookEditedWritten);
                            if (!isInserted)
                                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Data for BookEditedWritten");
                        }
                    }
                    if (model.Publications.Articles != null && model.Publications.Articles.Count > 0)
                    {
                        foreach (Article articles in model.Publications.Articles)
                        {
                            articles.IndividualId = cId;
                            bool isInserted = db.InsertArticle(articles);
                            if (!isInserted)
                                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Data for BookEditedWritten");
                        }
                    }
                    if (model.Publications.PaperPresentations != null && model.Publications.PaperPresentations.Count > 0)
                    {
                        foreach (PaperPresentation paperPresentation in model.Publications.PaperPresentations)
                        {
                            paperPresentation.IndividualId = cId;
                            bool isInserted = db.InsertPaperPresentations(paperPresentation);
                            if (!isInserted)
                                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Data for PaperPresentation");
                        }
                    }
                    if (model.Publications.TrainingProgram != null && model.Publications.TrainingProgram.Count > 0)
                    {
                        foreach (TrainingProgram trainingProgram in model.Publications.TrainingProgram)
                        {
                            trainingProgram.IndividualId = cId;
                            bool isInserted = db.InsertTrainingProgram(trainingProgram);
                            if (!isInserted)
                                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Data for TrainingProgram");
                        }
                    }
                    if (model.Publications.Seminars != null && model.Publications.Seminars.Count > 0)
                    {
                        foreach (IndividualSeminar seminar in model.Publications.Seminars)
                        {
                            seminar.IndividualId = cId;
                            bool isInserted = db.InsertIndividualSeminar(seminar);
                            if (!isInserted)
                                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Data for IndividualSeminars");
                        }
                    }
                    if (model.Publications.FacultyDevelopementProgram != null && model.Publications.FacultyDevelopementProgram.Count > 0)
                    {
                        foreach (FacultyDevelopementProgram facultyDevelopementProgram in model.Publications.FacultyDevelopementProgram)
                        {
                            facultyDevelopementProgram.IndividualId = cId;
                            bool isInserted = db.InsertFacultyDevelopementProgram(facultyDevelopementProgram);
                            if (!isInserted)
                                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Data for FacultyDevelopementProgram");
                        }
                    }
                    if (model.Publications.HonoursAndAwards != null && model.Publications.HonoursAndAwards.Count > 0)
                    {
                        foreach (HonoursAndAward honoursAndAward in model.Publications.HonoursAndAwards)
                        {
                            honoursAndAward.IndividualId = cId;
                            bool isInserted = db.InsertHonoursAndAwards(honoursAndAward);
                            if (!isInserted)
                                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Data for HonoursAndAwards");
                        }
                    }
                    if (model.Publications.PostGraduateTraining != null && model.Publications.PostGraduateTraining.Count > 0)
                    {
                        foreach (PostGraduateTraining postGraduateTraining in model.Publications.PostGraduateTraining)
                        {
                            postGraduateTraining.IndividualId = cId;
                            bool isInserted = db.InsertPostGraduateTraining(postGraduateTraining);
                            if (!isInserted)
                                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Data for PostGraduateTraining");
                        }
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, "Record inserted successfully");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
