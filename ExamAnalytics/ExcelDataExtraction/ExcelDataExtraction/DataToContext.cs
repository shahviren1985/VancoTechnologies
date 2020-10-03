using ExcelDataExtraction.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace ExcelDataExtraction
{
    public static class DataToContext
    {
        public static void FeedStudentDetailsData(List<StudentDetails> studentDetails)
        {
            var connectionSrtring =
                    System.Configuration.ConfigurationManager.
                    ConnectionStrings["DataContext"].ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionSrtring))
            {
                using (ExcelDataExtractionDbContext contextDB = new ExcelDataExtractionDbContext(connection, false))
                {
                    contextDB.Database.CreateIfNotExists();
                }
                connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    using (ExcelDataExtractionDbContext contextDB = new ExcelDataExtractionDbContext(connection, false))
                    {
                        contextDB.Database.UseTransaction(transaction);
                        contextDB.StudentDetails.AddRange(studentDetails);
                        contextDB.SaveChanges();
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public static void FeedResultSummaryData(List<ResultSummary> resultSummary)
        {
            var connectionSrtring =
                    System.Configuration.ConfigurationManager.
                    ConnectionStrings["DataContext"].ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionSrtring))
            {
                using (ExcelDataExtractionDbContext contextDB = new ExcelDataExtractionDbContext(connection, false))
                {
                    contextDB.Database.CreateIfNotExists();
                }
                connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    using (ExcelDataExtractionDbContext contextDB = new ExcelDataExtractionDbContext(connection, false))
                    {
                        contextDB.Database.UseTransaction(transaction);
                        contextDB.ResultSummaries.AddRange(resultSummary);
                        contextDB.SaveChanges();
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
