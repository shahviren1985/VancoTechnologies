using QuarterlyReports.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
namespace QuarterlyReports.Helpers
{
    public class CommonFeedbackDBOperations
    {
        MySqlConnection con;
        MySqlCommand cmd = new MySqlCommand();
        string getIdQuery = "Select @@Identity";
        string connection = ConfigurationManager.AppSettings.Get("dbConn").ToString();

        public MySqlConnection GetConnection()
        {
            con = new MySqlConnection(connection);
            con.Open();
            cmd.Connection = con;
            return con;
        }

        public int ExcuteQuery(string query)
        {
            try
            {
                using (con = GetConnection())
                {
                    using (cmd = new MySqlCommand(query, con))
                    {
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = getIdQuery;
                        return (int)Convert.ToInt32(cmd.ExecuteScalar().ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ExcuteUpdateQuery(string query)
        {
            try
            {
                using (con = GetConnection())
                {
                    using (cmd = new MySqlCommand(query, con))
                    {
                        cmd.ExecuteNonQuery();
                        return 1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataSet GetQuery(string query)
        {
            try
            {
                using (con = GetConnection())
                {
                    using (cmd = new MySqlCommand(query, con))
                    {
                        DataSet dsItems = new DataSet();
                        MySqlDataAdapter adapter = new MySqlDataAdapter(query, con);
                        adapter.Fill(dsItems);
                        return dsItems;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertFeedback(TeacherFeedback feedback)
        {
            string insertInsertTeacherFeedbackQuery = "INSERT INTO commonfeedback (userid,usertype," +
                                            "collegecode,a1,a2,a3,a4,a5,a6,a7,a8,a9,a10,a11,a12,a13,a14,a15,a16,a17,a18,a19,a20,a21," +
                                            "a22,a23,a24,a25,ipaddress,fwdipaddress) VALUES ('" + feedback.UserId + "','" + feedback.UserType + "'," +
                                            "'" + feedback.CollegeCode + "','" + feedback.A1 + "','" + feedback.A2 + "','" + feedback.A3 + "','" + feedback.A4 + "','" + feedback.A5 + "','" + feedback.A6 + "','" + feedback.A7 + "'," +
                                            "'" + feedback.A8 + "','" + feedback.A9 + "','" + feedback.A10 + "','" + feedback.A11 + "','" + feedback.A12 + "','" + feedback.A13 + "','" + feedback.A14 + "','" + feedback.A15 + "','" + feedback.A16 + "'," +
                                            "'" + feedback.A17 + "','" + feedback.A18 + "','" + feedback.A19 + "','" + feedback.A20 + "','" + feedback.A_21 + "','" + feedback.A_22 + "','" + feedback.A23 + "','" + feedback.A24 + "','" + feedback.A25 + "','" + feedback.IPAddress + "','" + feedback.FwdIPAddresses + "');";

            using (con = GetConnection())
            {
                cmd.CommandText = insertInsertTeacherFeedbackQuery;
                cmd.ExecuteNonQuery();
                cmd.CommandText = getIdQuery;
                return (int)Convert.ToInt32(cmd.ExecuteScalar().ToString());
            }
            //return ExcuteQuery(insertInsertTeacherFeedbackQuery);
        }

        public string GetUserDetails(string userId, string collegeId)
        {
            string getUserQuery = "SELECT roletype FROM tbluser where userid='" + userId + "' and collegecode='" + collegeId + "'";
            try
            {
                using (con = GetConnection())
                {
                    cmd = new MySqlCommand(getUserQuery, con);
                    return cmd.ExecuteScalar().ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetDepartmentsForPeerReview(string collegeCode)
        {
            string getDepartmentQuery = string.Format("SELECT distinct a16 FROM commonfeedback where collegeCode={0} and usertype in ('PeerAnyDepartment','PeerOtherDepartment','PeerOwnDepartment')", collegeCode);
            try
            {
                return GetQuery(getDepartmentQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetAcademicYearsAdministrators(string collegeCode)
        {
            string getDepartmentQuery = string.Format("SELECT distinct a32 FROM academicfeedback where collegeCode={0}", collegeCode);
            try
            {
                return GetQuery(getDepartmentQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetAcademicAdministrators(string collegeCode)
        {
            string getQuery = string.Format("SELECT distinct a1 FROM academicfeedback where collegecode={0}", collegeCode);
            try
            {
                return GetQuery(getQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertClsAlumini(ClsAlumini feedback)
        {
            string iQuery = "INSERT INTO clsalumini (userid,collegecode,a1,a2,a3,a4,a5,a6,a7," +
                "a8,a9,a10,a11,a12,a13,a14,a15,a16,a17,a18,a19,a20,a21,a22,a23,a24,a25,a26) VALUES (" +
                "'" + feedback.UserId + "'," + "'" + feedback.CollegeCode + "','" + feedback.A1 + "'," +
                "'" + feedback.A2 + "','" + feedback.A3 + "','" + feedback.A4 + "','" + feedback.A5 + "'," +
                "'" + feedback.A6 + "'," +
                "'" + feedback.A7 + "'," + "'" + feedback.A8 + "','" + feedback.A9 + "','" + feedback.A10 + "'," +
                "'" + feedback.A11 + "','" + feedback.A12 + "','" + feedback.A13 + "','" + feedback.A14 + "'," +
                "'" + feedback.A15 + "','" + feedback.A16 + "'," + "'" + feedback.A17 + "','" + feedback.A18 + "'," +
                "'" + feedback.A19 + "','" + feedback.A20 + "','" + feedback.A21 + "','" + feedback.A22 + "'," +
                "'" + feedback.A23 + "','" + feedback.A24 + "','" + feedback.A25 + "','" + feedback.A26 + "');";

            return ExcuteQuery(iQuery);
        }

        public int InsertClsPosition(ClsPosition feedback)
        {
            string iQuery = "INSERT INTO clspositions (clsaluminiid,a1,a2,a3) VALUES (" +
                "'" + feedback.ClsAluminiId + "','" + feedback.A1 + "'," +
                "'" + feedback.A2 + "','" + feedback.A3 + "');";
            return ExcuteQuery(iQuery);
        }

        public int InsertClsRating(ClsRating feedback)
        {
            string iQuery = "INSERT INTO clsrating (clsaluminiid,a1,a2,a3,a4,a5,a6,a7," +
                "a8,a9,a10,a11,a12,a13,a14,a15,a16,a17,a18,a19,a20,a21,a22,a23,a24,a25) VALUES (" +
                "'" + feedback.ClsAluminiId + "','" + feedback.A1 + "'," +
                "'" + feedback.A2 + "','" + feedback.A3 + "','" + feedback.A4 + "','" + feedback.A5 + "'," +
                "'" + feedback.A6 + "'," +
                "'" + feedback.A7 + "'," + "'" + feedback.A8 + "','" + feedback.A9 + "','" + feedback.A10 + "'," +
                "'" + feedback.A11 + "','" + feedback.A12 + "','" + feedback.A13 + "','" + feedback.A14 + "'," +
                "'" + feedback.A15 + "','" + feedback.A16 + "'," + "'" + feedback.A17 + "','" + feedback.A18 + "'," +
                "'" + feedback.A19 + "','" + feedback.A20 + "','" + feedback.A21 + "','" + feedback.A22 + "'," +
                "'" + feedback.A23 + "','" + feedback.A24 + "','" + feedback.A25 + "');";

            return ExcuteQuery(iQuery);
        }

        public int InsertClsActivities(ClsActivities feedback)
        {
            string iQuery = "INSERT INTO clsactivities (clsaluminiid,a1,a2,a3,a4,a5,a6,a7," +
                "a8,a9,a10,a11,a12,a13,a14,a15,a16,a17,a18,a19,a20,a21,a22,a23,a24,a25) VALUES (" +
                "'" + feedback.ClsAluminiId + "','" + feedback.A1 + "'," +
                "'" + feedback.A2 + "','" + feedback.A3 + "','" + feedback.A4 + "','" + feedback.A5 + "'," +
                "'" + feedback.A6 + "'," +
                "'" + feedback.A7 + "'," + "'" + feedback.A8 + "','" + feedback.A9 + "','" + feedback.A10 + "'," +
                "'" + feedback.A11 + "','" + feedback.A12 + "','" + feedback.A13 + "','" + feedback.A14 + "'," +
                "'" + feedback.A15 + "','" + feedback.A16 + "'," + "'" + feedback.A17 + "','" + feedback.A18 + "'," +
                "'" + feedback.A19 + "','" + feedback.A20 + "','" + feedback.A21 + "','" + feedback.A22 + "'," +
                "'" + feedback.A23 + "','" + feedback.A24 + "','" + feedback.A25 + "');";

            return ExcuteQuery(iQuery);
        }

        public int InsertClsProfessional(ClsProfessional feedback)
        {
            string iQuery = "INSERT INTO clsprofessional (clsaluminiid,a1,a2,a3,a4,a5,a6,a7," +
                "a8,a9) VALUES (" +
                "'" + feedback.ClsAluminiId + "','" + feedback.A1 + "'," +
                "'" + feedback.A2 + "','" + feedback.A3 + "','" + feedback.A4 + "','" + feedback.A5 + "'," +
                "'" + feedback.A6 + "'," +
                "'" + feedback.A7 + "'," + "'" + feedback.A8 + "','" + feedback.A9 + "');";

            return ExcuteQuery(iQuery);
        }

        public int InsertAcedemicFeedback(AcademicAdministrator feedback)
        {
            string IQuery = "INSERT INTO academicfeedback (userid,usertype," +
                                            "collegecode,a1,a2,a3,a4,a5,a6,a7,a8,a9,a10,a11,a12,a13,a14,a15,a16,a17,a18,a19,a20,a21," +
                                            "a22,a23,a24,a25,a26,a27,a28,a29,a30,a31,a32,a33,a34,a35,ipaddress,fwdipaddress) VALUES ('" + feedback.UserId + "','" + feedback.UserType + "'," +
                                            "'" + feedback.CollegeCode + "','" + feedback.A1 + "','" + feedback.A2 + "','" + feedback.A3 + "','" + feedback.A4 + "','" + feedback.A5 + "','" + feedback.A6 + "','" + feedback.A7 + "'," +
                                            "'" + feedback.A8 + "','" + feedback.A9 + "','" + feedback.A10 + "','" + feedback.A11 + "','" + feedback.A12 + "','" + feedback.A13 + "','" + feedback.A14 + "','" + feedback.A15 + "','" + feedback.A16 + "'," +
                                            "'" + feedback.A17 + "','" + feedback.A18 + "','" + feedback.A19 + "','" + feedback.A20 + "','" + feedback.A21 + "','" + feedback.A22 + "','" + feedback.A23 + "','" + feedback.A24 + "','" + feedback.A25 + "'," +
            "'" + feedback.A26 + "','" + feedback.A27 + "','" + feedback.A28 + "','" + feedback.A29 + "','" + feedback.A30 + "'," +
            "'" + feedback.A31 + "','" + feedback.A32 + "','" + feedback.A33 + "','" + feedback.A34 + "','" + feedback.A35 + "'," +
            "'" + feedback.IPAddress + "','" + feedback.FwdIPAddresses + "');";

            return ExcuteQuery(IQuery);
        }


    }
}