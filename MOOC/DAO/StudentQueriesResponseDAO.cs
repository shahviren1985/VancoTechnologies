using ITM.Courses.DAOBase;
using MySql.Data.MySqlClient;
using System;

namespace ITM.Courses.DAO
{
    public class StudentQueriesResponseDAO
    {
        private string Q_AddStudentQueryResponse = "insert into studentsqueryresponse(QueryId,ResponsedBy,ResponsedDate,Comments) values({0},{1},'{2}','{3}')";

        public void AddStudentQueryResponse(int queryId, int responsedUserId, string comments, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string cmdText = string.Format(Q_AddStudentQueryResponse, queryId, responsedUserId, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"), comments);
                    Database db = new Database();
                    db.Insert(cmdText, logPath, con);
                    con.Close();
                }
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }
    }
}
