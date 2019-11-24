using AA.DAOBase;
using AA.LogManager;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace AA.DAO
{
    public class DocumentReadStatusDAO
    {
        Logger logger = new Logger();

        public string SELECT_DOCUMENT_READ_STATUS_DETAILS = "SELECT Id,DocId,DateRead,ReadBy FROM documentreadstatus group by docid order by Id desc";
        public string INSERT_DOCUMENT_READ_STATUS_DETAILS = "INSERT INTO documentreadstatus(DocId,DateRead,ReadBy) VALUES ({0},'{1}','{2}')";
        public string SELECT_DOCUMENT_STATUS_BY_ID = "SELECT Id,DocId,DateRead,ReadBy FROM documentreadstatus WHERE DocId={0}";

        public List<DocumentReadStatus> GetDocumentStatusDetails(int documentId, string cxnString, string logPath)
        {
            List<DocumentReadStatus> documents = new List<DocumentReadStatus>();

            try
            {
                logger.Debug("DocumentReadStatusDAO", "GetDocumentStatusDetails", " Getting document status", logPath);

                Database db = new Database();
                DbDataReader reader = db.Select(string.Format(SELECT_DOCUMENT_STATUS_BY_ID, documentId), cxnString, logPath);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DocumentReadStatus doc = new DocumentReadStatus();

                        logger.Debug("DocumentReadStatusDAO", "GetDocumentStatusDetails", " Getting single document status", logPath);
                        doc.Id = int.Parse(reader["Id"].ToString());
                        doc.DocumentId = int.Parse(reader["DocId"].ToString());
                        doc.DateRead = reader["DateRead"].ToString();
                        doc.ReadBy = reader["ReadBy"].ToString();

                        documents.Add(doc);
                    }

                    if (!reader.IsClosed)
                    {
                        reader.Close();
                    }
                }


            }
            catch (Exception ex)
            {
                logger.Error("DocumentReadStatusDAO", "GetDocumentStatusDetails", " Error occurred while getting document status", ex, logPath);
                throw ex;
            }

            return documents;
        }

        public bool AddDocumentStatusDetails(int documentId, string dateRead, string readBy,string cxnString, string logPath)
        {
            try
            {
                Database db = new Database();
                string cmdText = string.Format(INSERT_DOCUMENT_READ_STATUS_DETAILS, documentId, dateRead, readBy);
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
