using AA.DAOBase;
using AA.LogManager;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace DAO
{
    public class ReminderStatusDAO
    {
        Logger logger = new Logger();

        public string SELECT_REMINDER_STATUS_DETAILS = "SELECT Id,DocId,ReminderSentTo,EmailAddresses,DateSent FROM reminderstatus group by docid order by Id desc";
        public string INSERT_REMINDER_STATUS_DETAILS = "INSERT INTO reminderstatus(DocId,ReminderSentTo,EmailAddresses,DateSent) VALUES ({0},'{1}','{2}','{3}')";

        public List<ReminderStatus> GetReminderStatusDetails(int documentId, string cxnString, string logPath)
        {
            List<ReminderStatus> reminders = new List<ReminderStatus>();

            try
            {
                logger.Debug("ReminderStatusDAO", "GetReminderStatusDetails", " Getting reminder status", logPath);

                Database db = new Database();
                DbDataReader reader = db.Select(string.Format(SELECT_REMINDER_STATUS_DETAILS, documentId), cxnString, logPath);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ReminderStatus reminder = new ReminderStatus();

                        logger.Debug("ReminderStatusDAO", "GetReminderStatusDetails", " Getting single reminder status", logPath);
                        reminder.Id = int.Parse(reader["Id"].ToString());
                        reminder.DocumentId = int.Parse(reader["DocId"].ToString());
                        reminder.ReminderSentTo = reader["ReminderSentTo"].ToString();
                        reminder.EmailAddresses = reader["EmailAddresses"].ToString();
                        reminder.DateSent = reader["DateSent"].ToString();

                        reminders.Add(reminder);
                    }

                    if (!reader.IsClosed)
                    {
                        reader.Close();
                    }
                }


            }
            catch (Exception ex)
            {
                logger.Error("ReminderStatusDAO", "GetReminderStatusDetails", " Error occurred while getting reminder status", ex, logPath);
                throw ex;
            }

            return reminders;
        }

        public bool AddReminderStatusDetails(int documentId, string reminderSentTo, string emailAddresses, string dateSent,  string cxnString, string logPath)
        {
            try
            {
                Database db = new Database();
                string cmdText = string.Format(INSERT_REMINDER_STATUS_DETAILS, documentId, reminderSentTo, emailAddresses, dateSent);
                db.Insert(cmdText, cxnString, logPath);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
