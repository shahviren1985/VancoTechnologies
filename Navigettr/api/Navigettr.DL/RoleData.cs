using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navigettr.Data
{
   public class RoleData
    {

        NavigettrEntities Objdb = new NavigettrEntities();


        public List<usp_Roles_Get_Result> GetRoles(int id)
        {
            try
            {
                return Objdb.usp_Roles_Get(id).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
