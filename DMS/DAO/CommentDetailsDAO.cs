using AA.DAOBase;
using AA.LogManager;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace AA.DAO
{
    public class CommentDetailsDAO
    {
        Logger logger = new Logger();

        public string SELECT_COMMENTS_BY_USER_ID = "SELECT Id, DateCreated,IsActive, IsDeleted,CreatedBy,UserComment,DocumentId,username FROM comments WHERE username={0} order by DateCreated desc";
        public string SELECT_COMMENTS_BY_DOCUMENT_ID = "SELECT Id, DateCreated,IsActive, IsDeleted,CreatedBy,UserComment,DocumentId,username FROM comments WHERE documentid={0} order by DateCreated desc";

        public List<CommentDetails> GetCommentsByUserName(string username, string cxnString, string logPath)
        {
            List<CommentDetails> comments = new List<CommentDetails>();

            try
            {
                logger.Debug("CommentDetailsDAO", "GetCommentsByUserName", " Getting comments by user id", logPath);

                Database db = new Database();
                DbDataReader reader = db.Select(string.Format(SELECT_COMMENTS_BY_USER_ID, username), cxnString, logPath);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int count = int.Parse(reader["count"].ToString());
                        if (count == 0)
                        {
                            logger.Debug("CommentDetailsDAO", "GetCommentsByUserName", " Unable to get comments by user id", logPath);
                            //return false;
                        }
                        else
                        {
                            CommentDetails comment = new CommentDetails();

                            logger.Debug("CommentDetailsDAO", "GetCommentsByUserName", " Getting comments for user id", logPath);
                            comment.Id = int.Parse(reader["Id"].ToString());
                            comment.DateCreated = DateTime.Parse(reader["DateCreated"].ToString());
                            comment.IsActive = bool.Parse(reader["IsActive"].ToString());
                            comment.IsDeleted = bool.Parse(reader["IsDeleted"].ToString());
                            comment.CreatedBy = reader["CreatedBy"].ToString();
                            comment.UserComment = reader["UserComment"].ToString();
                            comment.DocumentId = int.Parse(reader["DocumentId"].ToString());
                            comment.UserName = reader["username"].ToString();

                            comments.Add(comment);
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
                logger.Error("CommentDetailsDAO", "GetCommentsByUserName", " Error occurred while getting comments by User id", ex, logPath);
                throw ex;
            }

            return comments;
        }

        public List<CommentDetails> GetCommentsByDocumentId(int documentId, string cxnString, string logPath)
        {
            List<CommentDetails> comments = new List<CommentDetails>();

            try
            {
                logger.Debug("CommentDetailsDAO", "GetCommentsByUserName", " Getting comments by user id", logPath);

                Database db = new Database();
                DbDataReader reader = db.Select(string.Format(SELECT_COMMENTS_BY_DOCUMENT_ID, documentId), cxnString, logPath);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CommentDetails comment = new CommentDetails();

                        logger.Debug("CommentDetailsDAO", "GetCommentsByUserName", " Getting comments for user id", logPath);
                        comment.Id = int.Parse(reader["Id"].ToString());
                        comment.DateCreated = DateTime.Parse(reader["DateCreated"].ToString());
                        comment.ProcessedDateCreated = comment.DateCreated.ToString("dd-MM-yyyy");
                        comment.IsActive = reader["IsActive"].ToString() == "1" ? true : false;
                        comment.IsDeleted = reader["IsDeleted"].ToString() == "1" ? true : false;
                        comment.CreatedBy = reader["CreatedBy"].ToString();
                        comment.UserComment = reader["UserComment"].ToString();
                        comment.DocumentId = int.Parse(reader["DocumentId"].ToString());
                        comment.UserName = reader["username"].ToString();

                        comments.Add(comment);
                    }

                    if (!reader.IsClosed)
                    {
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("CommentDetailsDAO", "GetCommentsByUserName", " Error occurred while getting comments by User id", ex, logPath);
                throw ex;
            }

            return comments;
        }

        public CommentDetails InsertComments(string userName, int documentId, string comment, string dateCreated, bool isActive, bool isDeleted, string createdBy, string cnxnString, string logPath)
        {
            Database db = new Database();
            CommentDetails cd = new CommentDetails();
            string cmdText = "INSERT INTO comments (DateCreated,IsActive, IsDeleted,CreatedBy,UserComment,DocumentId,username) VALUES ('{0}',{1},{2},'{3}','{4}',{5},'{6}')";
            cmdText = string.Format(cmdText, dateCreated, isActive, isDeleted, createdBy, comment, documentId, userName);
            db.Insert(cmdText, cnxnString, logPath);

            cd.CreatedBy = createdBy;
            
            cd.DateCreated = DateTime.Parse(dateCreated);
            cd.ProcessedDateCreated = cd.DateCreated.ToString("dd-MM-yyyy");
            cd.DocumentId = documentId;
            cd.IsActive = isActive;
            cd.IsDeleted = isDeleted;
            cd.UserComment = comment;
            cd.UserName = userName;
            return cd;
        }

        public void DeleteComment(int id, string cnxnString, string logPath)
        {
            Database db = new Database();
            string cmdText = "DELETE FROM comments WHERE Id=" + id;
            db.Delete(cmdText, cnxnString, logPath);
        }
    }
}
