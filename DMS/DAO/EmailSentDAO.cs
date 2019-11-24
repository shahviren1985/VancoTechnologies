using AA.DAOBase;
using AA.LogManager;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AA.DAO
{
    public class EmailSentDAO
    {
        Logger logger = new Logger();

        public string SELECT_EMAIL_SENT_DETAILS = "SELECT Id,From,To,Subject,Text,Attachments FROM emailsent order by Id desc";
        public string INSERT_EMAIL_SENT_DETAILS = "INSERT INTO emailsent(From,To,Subject,Text,Attachments) VALUES ('{0}','{1}','{2}','{3}','{4}')";

        public List<EmailSent> GetEmailSentDetails(int documentId, string cxnString, string logPath)
        {
            List<EmailSent> emails = new List<EmailSent>();

            try
            {
                logger.Debug("EmailSentDAO", "GetEmailSentDetails", " Getting email status", logPath);

                Database db = new Database();
                DbDataReader reader = db.Select(SELECT_EMAIL_SENT_DETAILS, cxnString, logPath);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        EmailSent email = new EmailSent();

                        logger.Debug("EmailSentDAO", "GetEmailSentDetails", " Getting single email status", logPath);
                        email.Id = int.Parse(reader["Id"].ToString());
                        email.From = reader["From"].ToString();
                        email.To = reader["To"].ToString();
                        email.EmailBody = reader["Text"].ToString();
                        email.Attachments = reader["Attachments"].ToString();

                        emails.Add(email);
                    }

                    if (!reader.IsClosed)
                    {
                        reader.Close();
                    }
                }


            }
            catch (Exception ex)
            {
                logger.Error("EmailSentDAO", "GetEmailSentDetails", " Error occurred while getting email status", ex, logPath);
                throw ex;
            }

            return emails;
        }

        public bool AddEmailSentDetails(string from, string to, string subject, string body, string attachments, string cxnString, string logPath)
        {
            try
            {
                Database db = new Database();
                string cmdText = string.Format(INSERT_EMAIL_SENT_DETAILS, from, to, subject, body, attachments);
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
