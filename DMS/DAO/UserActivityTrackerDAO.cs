using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using AA.LogManager;
using System.IO;
using Util;
using AA.DAOBase;
using System.Data.Common;
using AA.Util;


namespace AA.DAO
{

    public class UserActivityTrackerDAO

    {
        public string INSERT_Activity = "INSERT INTO UserActivityTracker (Activity,ActivityOn,ObjectId,ActivityDate,UserId) VALUES ('{0}','{1}','{2}','{3}','{4}')";
        public string GET_TOP_5_ACTIVITIES = "SELECT  Id,Activity,ActivityOn,ObjectId,ActivityDate,UserId FROM UserActivityTracker where UserId='{0}' order by ActivityDate desc limit 5";
        public string Q_SelectUserbyUserName = "select count(*) as count from UserActivityTracker where UserId='{0}'";
        public string GET_ACTIVITIES = "SELECT Id,Activity,ActivityOn,ObjectId,ActivityDate,UserId,SendedReminder FROM UserActivityTracker where ActivityDate like '{0}%'";
        public string UPDATE_REMINDER_FLAG = "Update UserActivityTracker set SendedReminder = {0} where Id = {1}";
        Logger logger = new Logger();

        public List<UserActivityTracker> GetTopFiveActivityDetails(string UserId, string cxnString, string logPath)
        {
            try
            {
                logger.Debug("UserActivityTrackerDAO", "GetTopFiveActivityDetails", "Selecting User Activity Details from database", logPath);
                string result = string.Format(GET_TOP_5_ACTIVITIES, UserId);
                Database db = new Database();
                DbDataReader reader = db.Select(result, cxnString, logPath);
                if (reader.HasRows)
                {
                    List<UserActivityTracker> activityList = new List<UserActivityTracker>();

                    while (reader.Read())
                    {
                        UserActivityTracker Activities = new UserActivityTracker();
                        Activities.Id = Convert.ToInt32(reader["Id"]);
                        Activities.Activity = reader["Activity"].ToString();
                        Activities.ActivityOn = reader["ActivityOn"].ToString();
                        Activities.UserId = reader["UserId"].ToString();
                        Activities.ObjectId = int.Parse(reader["ObjectId"].ToString());
                        Activities.ActivityDate = Convert.ToDateTime(reader["ActivityDate"]);
                        activityList.Add(Activities);
                    }
                    if (reader != null)
                    {
                        reader.Close();
                    }

                    logger.Debug("UserActivityTrackerDAO", "GetTopFiveActivityDetails", " returning File Details from database", logPath);
                    return activityList;
                }
            }
            catch (Exception ex)
            {
                logger.Error("UserActivityTrackerDAO", "GetTopFiveActivityDetails", " Error occurred while Get Single File Details", ex, logPath);
                throw new Exception("11279");
            }
            finally
            {
                Database.KillConnections();
            }

            return null;
        }

        public bool checkUser(string userName, string cxnString, string logPath)
        {
            bool isAuthenticate = false;
            try
            {
                string cmdText = string.Format(Q_SelectUserbyUserName, ParameterFormater.FormatParameter(userName));
                Database db = new Database();
                DbDataReader reader = db.Select(cmdText, cxnString, logPath);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int count = int.Parse(reader["count"].ToString());
                        if (count == 0)
                        {

                            //return false;
                        }
                        else
                        {

                            isAuthenticate = true;
                        }
                    }

                    if (!reader.IsClosed)
                    {
                        reader.Close();
                    }
                }

            }
            catch (Exception ex)
            {

                throw new Exception("");
            }
            finally
            {
                Database.KillConnections();
            }

            return isAuthenticate;
        }


        public UserActivityTracker CreateUserActivityDetails(string activity, string activityOn, int objectId, DateTime activityDate, string userId, string cxnString, string logPath)
        {
            try
            {
                Hashtable args = GetHashTable(0, activity, activityOn, objectId, activityDate, userId, logPath);
                logger.Debug("UserActivityTrackerDAO", "CreateUserActivityDetails", "Inserting File Details", args);
                string result = string.Format(INSERT_Activity, ParameterFormater.FormatParameter(activity), ParameterFormater.FormatParameter(activityOn), objectId, activityDate.ToString("yyyy/MM/dd hh:mm:ss.fff"), userId);
                Database db = new Database();

                db.Insert(result, cxnString, logPath);

                UserActivityTracker activities = new UserActivityTracker();
                activities.Activity = activity;
                activities.ActivityOn = activityOn;
                activities.ObjectId = objectId;
                activities.ActivityDate = activityDate;
                activities.UserId = userId;


                logger.Debug("UserActivityTrackerDAO", "CreateUserActivityDetails", "Department Details Saved", logPath);
                return activities;
            }
            catch (Exception ex)
            {
                logger.Error("UserActivityTrackerDAO", "CreateCourse", " Error occurred while Add Department", ex, logPath);
                throw new Exception("11276");
            }
            finally
            {
                Database.KillConnections();
            }
        }

        public List<UserActivityTracker> GetAllUserActivityDetails(string CreatedDate, string cxnString, string logPath)
        {
            try
            {
                logger.Debug("UserActivityTrackerDAO", "GetAllUserActivityDetails", "Selecting All User Activity Details from database", logPath);
                string result = string.Format(GET_ACTIVITIES, CreatedDate);
                Database db = new Database();
                DbDataReader reader = db.Select(result, cxnString, logPath);
                if (reader.HasRows)
                {
                    List<UserActivityTracker> activityList = new List<UserActivityTracker>();

                    while (reader.Read())
                    {
                        UserActivityTracker Activities = new UserActivityTracker();
                        Activities.Id = Convert.ToInt32(reader["Id"]);
                        Activities.Activity = reader["Activity"].ToString();
                        Activities.ActivityOn = reader["ActivityOn"].ToString();
                        Activities.UserId = reader["UserId"].ToString();
                        Activities.ObjectId = int.Parse(reader["ObjectId"].ToString());
                        Activities.ActivityDate = Convert.ToDateTime(reader["ActivityDate"]);
                        Activities.SendedReminder = int.Parse(reader["SendedReminder"].ToString());
                        activityList.Add(Activities);
                    }
                    if (reader != null)
                    {
                        reader.Close();
                    }

                    logger.Debug("UserActivityTrackerDAO", "GetAllUserActivityDetails", " returning All User Activity Details from database", logPath);
                    return activityList;
                }
            }
            catch (Exception ex)
            {
                logger.Error("UserActivityTrackerDAO", "GetAllUserActivityDetails", " Error occurred while Get All User Activity Details", ex, logPath);
                throw new Exception("11279");
            }
            finally
            {
                Database.KillConnections();
            }
            return null;
        }

        public bool UpdateReminderFlag(int flag, int id, string cxnString, string logPath)
        {
            try
            {
                string result = string.Format(UPDATE_REMINDER_FLAG, flag, id);
                Database db = new Database();

                db.Update(result, cxnString, logPath);
                logger.Debug("UserActivityTrackerDAO", "UpdateReminderFlag", "Reminder Flag has  updated", logPath);

                return true;
            }
            catch (Exception ex)
            {
                logger.Error("UserActivityTrackerDAO", "UpdateReminderFlag", " Error occurred while Updating Reminder Flag", ex, logPath);
                throw new Exception("11276");
            }
            finally
            {
                Database.KillConnections();
            }
        }
        public Hashtable GetHashTable(int id, string activity, string activityOn, int objectId, DateTime activityDate, string userId, string logPath)
        {
            try
            {
                Hashtable args = new Hashtable();
                logger.Debug("UserActivityTrackerDAO", "GetHashTable", "Adding data to hash table", args);
                if (id != 0)
                {
                    args.Add("Id", id);
                }
                if (!string.IsNullOrEmpty(activity))
                {
                    args.Add("activity", activity);
                }
                if (!string.IsNullOrEmpty(activityOn))
                {
                    args.Add("activityOn", activityOn);
                }
                if (activityDate != null)
                {
                    args.Add("activityDate", activityDate.ToString());
                }

                if (objectId != 0)
                {
                    args.Add("objectId", objectId);
                }

                if (!string.IsNullOrEmpty(userId))
                {
                    args.Add("userId", userId);
                }
                logger.Debug("UserActivityTrackerDAO", "GetHashTable", "Added data to hash table", logPath);
                return args;
            }
            catch (Exception ex)
            {
                logger.Error("UserActivityTrackerDAO", "GetHashTable", " Error occurred while getting hash table", ex, logPath);
                throw new Exception("11281");
            }
        }


    }
}
