using System.Collections.Generic;
using ITM.Courses.ConfigurationsManager;

namespace ITM.Courses.ConfigurationsManager
{
    public class PageAccessDAO
    {
        Configurations configManage = new Configurations();

        public bool IsPageExist(string roleName, string pageName, string configFilePath)
        {
            bool flag = false;
            List<MappingPagesConfig> details = configManage.GetPageList(configFilePath);
            foreach (MappingPagesConfig page in details)
            {
                if (page.Name == pageName)
                {
                    foreach (string name in page.RoleName)
                    {
                        if (name == roleName)
                        {
                            flag = true;
                        }
                    }
                }
            }
            if (flag == false)
            {
                if (roleName == "Admin" || roleName == "Principal")
                {
                    flag = true;
                }
            }

            return flag;
        }
    }
}
