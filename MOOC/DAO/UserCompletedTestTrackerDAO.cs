using ITM.Courses.DAOBase;
using MySql.Data.MySqlClient;
using System;
using System.Data.Common;

namespace ITM.Courses.DAO
{
    public class UserCompletedTestTrackerDAO
    {
        private string Q_AddUserCompletedTest = "insert into completedtesttracker(testid, userid) values({0},{1})";
        private string Q_GetUserCompletedTest = "select * from completedtesttracker where testid={0} and userid= {1}";

        public void AddUserCompletedTest(int testId, int userId, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string cmdText = string.Format(Q_AddUserCompletedTest, testId, userId);
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

        public UserCompletedTestTracker GetUserCompletedTest(int testId, int userId, string cnxnString, string logPath)
        {
            try
            {
                string cmdText = string.Format(Q_GetUserCompletedTest, testId, userId);
                Database db = new Database();
                UserCompletedTestTracker obj = new UserCompletedTestTracker();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    using (DbDataReader reader = db.Select(cmdText, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                obj.Id = Convert.ToInt32(reader["id"]);
                                obj.TestId = Convert.ToInt32(reader["testid"]);
                                obj.UserId = Convert.ToInt32(reader["userid"]);
                                obj.CompletedDate = Convert.ToDateTime(reader["DateCreated"]);
                            }

                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }

                            
                        }
                    }

                    con.Close();
                    return obj;
                }
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }
    }
}
