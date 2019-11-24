using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Navigettr.Data;


namespace Navigettr.Core
{
    public class RoleRepository : IRoleRepository
    {
        RoleData Objdb = new RoleData();
        public List<usp_Roles_Get_Result> GetRoles(int id)
        {
            try
            {
                return Objdb.GetRoles(id).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
