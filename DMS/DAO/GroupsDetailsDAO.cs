using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using AA.DAOBase;

namespace AA.DAO
{
    public class GroupsDetailsDAO
    {
        private string Q_AddGroups = "Insert into groups(OwnerUserId, GroupName, DateCreated,Users) values('{0}','{1}','{2}','{3}')";
        private string Q_GetGroupsByUser = "Select * from groups where OwnerUserId= '{0}'";
        private string Q_GetGroupsByID = "Select * from groups where id= {0}";

        public void AddGroups(string ownerUserid, string groupName, DateTime date, string users, string cxnString, string logPath)
        {
            try
            {
                string cmd = string.Format(Q_AddGroups, ownerUserid, groupName, date.ToString("yyyy/MM/dd hh:mm:ss.fff"), users);
                Database db = new Database();
                db.Insert(cmd, cxnString, logPath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GroupsDetails> GetGroupsByUser(string ownerUserId, string cxnString, string logPath)
        {
            try
            {
                string cmd = string.Format(Q_GetGroupsByUser, ownerUserId);
                Database db = new Database();
                DbDataReader reader = db.Select(cmd, cxnString, logPath);
                if (reader.HasRows)
                {
                    List<GroupsDetails> groups = new List<GroupsDetails>();

                    while (reader.Read())
                    {
                        GroupsDetails group = new GroupsDetails();
                        group.Id = Convert.ToInt32(reader["Id"]);
                        group.DateCreated = Convert.ToDateTime(reader["DateCreated"]);
                        group.GroupName = reader["GroupName"].ToString();
                        group.OwnerUserId = reader["OwnerUserId"].ToString();
                        group.Users = reader["Users"].ToString();

                        groups.Add(group);
                    }

                    return groups;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return null;
        }

        public GroupsDetails GetGroupById(int groupId, string cxnString, string logPath)
        {
            try
            {
                string cmd = string.Format(Q_GetGroupsByID, groupId);

                Database db = new Database();
                DbDataReader reader = db.Select(cmd, cxnString, logPath);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        GroupsDetails group = new GroupsDetails();
                        group.Id = Convert.ToInt32(reader["Id"]);
                        group.DateCreated = Convert.ToDateTime(reader["DateCreated"]);
                        group.GroupName = reader["GroupName"].ToString();
                        group.OwnerUserId = reader["OwnerUserId"].ToString();
                        group.Users = reader["Users"].ToString();

                        return group;
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
