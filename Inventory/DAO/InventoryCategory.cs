using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITM.DAOBase;
using System.Data.Common;

namespace ITM.DAO
{
    public class InventoryCategory
    {
        LogManager.Logger logger = new LogManager.Logger();

        public string Id { get; set; }
        public string Category { get; set; }

        private string Q_GetAllCategories = "Select * from itemcategory";

        public List<InventoryCategory> GetAllItemCategories(string cnxnString, string logPath)
        {
            try
            {
                List<InventoryCategory> catList = new List<InventoryCategory>();
                string result = string.Format(Q_GetAllCategories);
                Database db = new Database();
                DbDataReader reader = db.Select(result, cnxnString, logPath);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        InventoryCategory cat = new InventoryCategory();
                        cat.Category = reader["Name"].ToString();
                        cat.Id = reader["Id"].ToString();

                        catList.Add(cat);
                    }
                }

                return catList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
