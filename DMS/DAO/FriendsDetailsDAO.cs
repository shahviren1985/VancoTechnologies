using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using AA.DAOBase;

namespace AA.DAO
{
    public class FriendsDetailsDAO
    {
        private string Q_AddFriends = "Insert into friends(OwnerUserId, DateCreated, Users) value('{0}','{1}','{2}')";
        private string Q_GetFriendsByUserId = "Select * from friends where OwnerUserId='{0}'";
        private string Q_UpdateUserByUserId = "Update friends set Users='{0}' where Id='{1}'";

        public void AddFriends(string ownerUserId, DateTime createdDate, string users, string cxnString, string logPath)
        {
            try
            {
                string cmd = string.Format(Q_AddFriends, ownerUserId, createdDate.ToString("yyyy/MM/dd hh:mm:ss.fff"), users);
                Database db = new Database();
                db.Insert(cmd, cxnString, logPath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateUsersByUserId(int id, string users,string cxnString,string logPath)
        {
            try
            {
                string cmd = string.Format(Q_UpdateUserByUserId, users, id);
                Database db = new Database();
                db.Update(cmd, cxnString, logPath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FriendsDetails GetFriendsByUserId(string ownerUserId, string cxnString, string logPath)
        {
            try
            {
                string cmd = string.Format(Q_GetFriendsByUserId, ownerUserId);
                Database db = new Database();

                DbDataReader reader = db.Select(cmd, cxnString, logPath);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        FriendsDetails friend = new FriendsDetails();
                        friend.DateCreated = Convert.ToDateTime(reader["DateCreated"].ToString());
                        friend.Id = Convert.ToInt32(reader["Id"].ToString());
                        friend.OwnerUserId = reader["OwnerUserId"].ToString();
                        friend.Users = reader["Users"].ToString();

                        return friend;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
    }
}
