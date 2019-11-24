using AA.DAOBase;
using AA.LogManager;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace AA.DAO
{
    

    public class LocationDetailsDAO
    {
        public string INSERT_LOCATION_DETAILS = "INSERT INTO location (RoomNumber,Cupbord,Shelf,FileName,Comments) VALUES ('{0}','{1}','{2}','{3}','{4}')";
        private Logger logger = new Logger();
        public string SELECT_ACTIVE_LOCATION_DETAILS = "SELECT Id, RoomNumber,Cupbord,Shelf,FileName,Comments FROM location where enabled='1'";
        public string SELECT_LOCATION_DETAIL_BY_ID = "SELECT Id, RoomNumber,Cupbord,Shelf,FileName,Comments FROM location where id = {0}";
        public string SELECT_LOCATION_DETAILS = "SELECT Id, RoomNumber,Cupbord,Shelf,FileName,Comments FROM location order by Cupbord asc";
        public string UPDATE_LOCATION_DETAILS = "UPDATE location SET RoomNumber='{1}', Cupbord='{2}', Shelf='{3}', FileName='{4}', Comments='{5}' WHERE Id={0}";

        public Location AddLocationDetails(string roomNumber, string cupBoard, string shelf, string fileName, string comments, string cxnString, string logPath)
        {
            try
            {
                new Database().Insert(string.Format(this.INSERT_LOCATION_DETAILS, new object[] { roomNumber, cupBoard, shelf, fileName, comments }), cxnString, logPath);
            }
            catch (Exception)
            {
                return null;
            }
            return new Location
            {
                Comments = comments,
                Cupboard = cupBoard,
                FileName = fileName,
                RoomNumber = roomNumber,
                Shelf = shelf
            };
        }

        public List<Location> GetActiveLocationDetails(string cxnString, string logPath)
        {
            List<Location> list = new List<Location>();
            try
            {
                this.logger.Debug("LocationDetailsDAO", "GetLocationDetails", " Getting locations", logPath);
                DbDataReader reader = new Database().Select(this.SELECT_ACTIVE_LOCATION_DETAILS, cxnString, logPath);
                if (!reader.HasRows)
                {
                    return list;
                }
                while (reader.Read())
                {
                    Location item = new Location();
                    this.logger.Debug("LocationDetailsDAO", "GetLocationDetails", " Getting single location", logPath);
                    item.Id = int.Parse(reader["Id"].ToString());
                    item.RoomNumber = reader["RoomNumber"].ToString();
                    item.Cupboard = reader["Cupbord"].ToString();
                    item.Shelf = reader["Shelf"].ToString();
                    item.FileName = reader["FileName"].ToString();
                    item.Comments = reader["Comments"].ToString();
                    list.Add(item);
                }
                if (!reader.IsClosed)
                {
                    reader.Close();
                }
            }
            catch (Exception exception)
            {
                this.logger.Error("LocationDetailsDAO", "IsAuthenticated", " Error occurred while Authenticating User", exception, logPath);
                throw exception;
            }
            return list;
        }

        public Location GetLocationDetailById(int id, string cxnString, string logPath)
        {
            try
            {
                this.logger.Debug("LocationDetailsDAO", "GetLocationDetailById", " Getting location by id = " + id, logPath);
                Database database = new Database();
                string cmdText = string.Format(this.SELECT_LOCATION_DETAIL_BY_ID, id);
                DbDataReader reader = database.Select(cmdText, cxnString, logPath);
                if (reader.HasRows)
                {
                    Location location = new Location();
                    while (reader.Read())
                    {
                        this.logger.Debug("LocationDetailsDAO", "GetLocationDetails", " Getting single location", logPath);
                        location.RoomNumber = reader["RoomNumber"].ToString();
                        location.Cupboard = reader["Cupbord"].ToString();
                        location.Shelf = reader["Shelf"].ToString();
                        location.FileName = reader["FileName"].ToString();
                        location.Comments = reader["Comments"].ToString();
                        location.Id = Convert.ToInt32(reader["Id"]);
                    }
                    if (!reader.IsClosed)
                    {
                        reader.Close();
                    }
                    return location;
                }
            }
            catch (Exception exception)
            {
                this.logger.Error("LocationDetailsDAO", "GetLocationDetailById", " Error occurred while Authenticating User", exception, logPath);
                throw exception;
            }
            return null;
        }

        public List<Location> GetLocationDetails(string cxnString, string logPath)
        {
            List<Location> list = new List<Location>();
            try
            {
                this.logger.Debug("LocationDetailsDAO", "GetLocationDetails", " Getting locations", logPath);
                DbDataReader reader = new Database().Select(this.SELECT_LOCATION_DETAILS, cxnString, logPath);
                if (!reader.HasRows)
                {
                    return list;
                }
                while (reader.Read())
                {
                    Location item = new Location();
                    this.logger.Debug("LocationDetailsDAO", "GetLocationDetails", " Getting single location", logPath);
                    item.Id = int.Parse(reader["Id"].ToString());
                    item.RoomNumber = reader["RoomNumber"].ToString();
                    item.Cupboard = reader["Cupbord"].ToString();
                    item.Shelf = reader["Shelf"].ToString();
                    item.FileName = reader["FileName"].ToString();
                    item.Comments = reader["Comments"].ToString();
                    list.Add(item);
                }
                if (!reader.IsClosed)
                {
                    reader.Close();
                }
            }
            catch (Exception exception)
            {
                this.logger.Error("LocationDetailsDAO", "IsAuthenticated", " Error occurred while Authenticating User", exception, logPath);
                throw exception;
            }
            return list;
        }

        public void UpdateLocationDetails(int locId, string roomNumber, string cupBoard, string shelf, string fileName, string comments, string cxnString, string logPath)
        {
            try
            {
                new Database().Update(string.Format(this.UPDATE_LOCATION_DETAILS, new object[] { locId, roomNumber, cupBoard, shelf, fileName, comments }), cxnString, logPath);
            }
            catch (Exception)
            {
            }
        }
    }
}
