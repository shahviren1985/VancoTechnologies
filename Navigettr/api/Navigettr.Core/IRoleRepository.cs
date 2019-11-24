using Navigettr.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navigettr.Core
{

    interface IRoleRepository
    {
        List<usp_Roles_Get_Result> GetRoles(int id);
    }
}
