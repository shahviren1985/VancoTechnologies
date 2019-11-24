using RoshniAppWebApi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Web;

namespace RoshniAppWebApi.Helpers
{
    public class DatabaseOperations
    {

        OdbcConnection obcon;
        OdbcDataAdapter odbAdp;
        OdbcCommand odbCommand;
        string getIdQuery = "Select @@Identity";
        int ID;

        public OdbcConnection CreateConnection()
        {
            string connection = ConfigurationManager.AppSettings.Get("mySqlConn").ToString();
            return new OdbcConnection(connection);
        }

        public int ExcuteQuery(string query)
        {
            try
            {
                using (obcon = CreateConnection())
                using (odbCommand = new OdbcCommand(query, obcon))
                {
                    if (obcon.State == ConnectionState.Closed)
                    {
                        obcon.Open();
                    }
                    odbCommand.ExecuteNonQuery();
                    odbCommand.CommandText = getIdQuery;
                    ID = (int)Convert.ToInt32(odbCommand.ExecuteScalar().ToString());
                    return ID;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (obcon.State == ConnectionState.Open)
                {
                    obcon.Close();
                }
            }
        }

        public int ExcuteUpdateQuery(string query)
        {
            try
            {
                using (obcon = CreateConnection())
                using (odbCommand = new OdbcCommand(query, obcon))
                {
                    if (obcon.State == ConnectionState.Closed)
                    {
                        obcon.Open();
                    }
                    odbCommand.ExecuteNonQuery();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (obcon.State == ConnectionState.Open)
                {
                    obcon.Close();
                }
            }
        }

        public DataSet GetQuery(string query)
        {
            try
            {
                using (obcon = CreateConnection())
                using (odbCommand = new OdbcCommand(query, obcon))
                {
                    if (obcon.State == ConnectionState.Closed)
                    {
                        obcon.Open();
                    }
                    DataSet dsItems = new DataSet();
                    odbAdp = new OdbcDataAdapter(query, obcon); // need to add examtype
                    odbAdp.Fill(dsItems);
                    return dsItems;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (obcon.State == ConnectionState.Open)
                {
                    obcon.Close();
                }
            }
        }

        public int RegisterUser(UserDetails user)
        {
            string inserq = "INSERT INTO userdetails (mobilenumber,isvalidated,dateofbirth,usertype,Emergency1,Emergency2, " +
                "Emergency3,Emergency4,Emergency5) VALUES ('" + user.MobileNumber + "'," + user.IsValidated + "," +
                "'" + user.DateOfBirth.ToString("yyyy-MM-dd") + "','" + user.UserType + "','" + user.Emergency1 + "','" + user.Emergency2 + "'," +
                "'" + user.Emergency3 + "','" + user.Emergency4 + "','" + user.Emergency5 + "') ";

            return ExcuteQuery(inserq);
        }

        public int UpdateImegencyContact(UserDetails user)
        {
            string updateUserQuery = "UPDATE userdetails SET Emergency1 = '" + user.Emergency1 + "'," +
"Emergency2 = '" + user.Emergency2 + "',Emergency3 = '" + user.Emergency3 + "',Emergency4 ='" + user.Emergency4 + "'," +
"Emergency5 = '" + user.Emergency5 + "' WHERE mobilenumber = '" + user.MobileNumber + "';";
            return ExcuteUpdateQuery(updateUserQuery);
        }

        public int InsertOTPLogs(OTPLogs oTPLogs)
        {
            string inserq = " INSERT INTO otplogs(userid,senttomobilenumber,otpmessage,otpcode,isvalidated, " +
                                    "sentat) VALUES ('" + oTPLogs.UserId + "','" + oTPLogs.SentToMobileNumber + "'," +
    "'" + oTPLogs.OtpMessage + "','" + oTPLogs.OtpCode + "'," + oTPLogs.IsValidated + ",'" + oTPLogs.SentAt.ToString("yyyy-MM-ddTHH:mm:ss") + "');";
            return ExcuteQuery(inserq);
        }

        public int InsertSOSLogs(SOSLogs sosLogs)
        {
            string inserq = " INSERT INTO soslogs(userid,senttomobilenumber,sosmessage,sentat) VALUES " +
                "('" + sosLogs.UserId + "','" + sosLogs.SentToMobileNumber + "','" + sosLogs.SosMessage + "','" + sosLogs.SentAt.ToString("yyyy-MM-ddTHH:mm:ss") + "');";
            return ExcuteQuery(inserq);
        }

        public int CheckIfMobileNumberExist(string mobileNumber)
        {
            DataSet dsItems = new DataSet();
            string getExitQuery = "SELECT userId FROM userdetails where mobilenumber='" + mobileNumber + "' and isvalidated=true";
            dsItems = GetQuery(getExitQuery);
            if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
            {
                return Convert.ToInt32(dsItems.Tables[0].Rows[0]["userid"]);
            }
            else
            {
                return 0;
            }
        }

        public void UpdateMobileNumber(string mobileNumber)
        {
            string updateUserQuery = "UPDATE userdetails SET mobilenumber = '" + mobileNumber + "_0' WHERE mobilenumber = '" + mobileNumber + "';";
            ExcuteUpdateQuery(updateUserQuery);
        }

        public int updateOTPDetails(string mobileNumber, string otpcode)
        {
            DataSet dsItems = new DataSet();
            string updateQ = "UPDATE otplogs SET isvalidated = true,validatedat ='" + DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss") + "' " +
                " WHERE senttomobilenumber='" + mobileNumber + "' and otpcode='" + otpcode + "';";

            string updateUserQuery = "UPDATE userdetails SET isvalidated = true WHERE mobilenumber = '" + mobileNumber + "';";
            ExcuteUpdateQuery(updateUserQuery);

            return ExcuteUpdateQuery(updateQ);
        }

        public bool ValidateOTP(string mobileNumber, string otpcode)
        {
            DataSet dsItems = new DataSet();
            string getQuery = "select otplogsid from otplogs WHERE senttomobilenumber='" + mobileNumber + "' and otpcode='" + otpcode + "';";
            dsItems = GetQuery(getQuery);
            if (dsItems.Tables[0] != null && dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }

        public UserDetails GetUserDetails(string mobileNumber)
        {
            DataSet dsItems = new DataSet();
            UserDetails userDetails = null;
            string getExitQuery = "SELECT * FROM userdetails where mobilenumber='" + mobileNumber + "'";
            dsItems = GetQuery(getExitQuery);
            if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
            {
                userDetails = new UserDetails();
                userDetails.UserId = Convert.ToInt32(dsItems.Tables[0].Rows[0]["userid"]);
                userDetails.MobileNumber = dsItems.Tables[0].Rows[0]["mobilenumber"].ToString();
                userDetails.IsValidated = Convert.ToBoolean(dsItems.Tables[0].Rows[0]["isvalidated"]);
                userDetails.DateOfBirth = Convert.ToDateTime(dsItems.Tables[0].Rows[0]["dateofbirth"]);
                userDetails.UserType = dsItems.Tables[0].Rows[0]["usertype"].ToString();
                userDetails.Emergency1 = dsItems.Tables[0].Rows[0]["emergency1"].ToString();
                userDetails.Emergency2 = dsItems.Tables[0].Rows[0]["emergency2"].ToString();
                userDetails.Emergency3 = dsItems.Tables[0].Rows[0]["emergency3"].ToString();
                userDetails.Emergency4 = dsItems.Tables[0].Rows[0]["emergency4"].ToString();
                userDetails.Emergency5 = dsItems.Tables[0].Rows[0]["emergency5"].ToString();

            }
            return userDetails;
        }

    }
}