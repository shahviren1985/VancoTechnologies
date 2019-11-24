using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Util;
using System.IO;
using System.Configuration;
namespace AA.DAO
{

    public class CreateStaffFolders
    {
        int parentId = -1;

        public int CrateStaffFlodersSubFolders(string folderName, string username, int userId, string cnxnString, string logPath, string college)
        {
            UserDetails details = new UserDetailsDAO().GetUserDetailList(username, cnxnString, logPath);
            FolderDetailsDAO sdao = new FolderDetailsDAO();
            FolderDetails details2 = sdao.GetSingleFileDetails("Staff", cnxnString, logPath);
            if (details2 == null)
            {
                this.parentId = -1;
                Directory.CreateDirectory(Path.Combine("~/Directories/" + college, Path.Combine(new string[] { folderName })));
                sdao.CreateFolderDetails("Staff", "For Staff Details", "Staff", DateTime.Now, DateTime.Now, userId, "<Users accesstype='Read'>All</Users>", -1, "", "Staff", "Staff", "Read", "Staff", "", cnxnString, logPath);
                details2 = sdao.GetSingleFileDetails("Staff", cnxnString, logPath);
            }
            this.parentId = details2.Id;
            FolderDetails details3 = sdao.GetSingleFileDetails(folderName, cnxnString, logPath);
            FolderHistoryManager manager = new FolderHistoryManager();
            manager.Date = DateTime.Now;
            manager.Activity = "Folder Created";
            manager.User = username;
            string str = FolderHistoryManager.CreateHistory(manager);
            FolderHistoryManager manager2 = new FolderHistoryManager();
            return details3.Id;
        }


        public string GetPathHirarchy(string cxnString, string logPath)
        {
            try
            {
                FolderDetailsDAO fDetails = new FolderDetailsDAO();
                string path = "/";
                int id = parentId;
                for (; ; )
                {
                    FolderDetails folder = fDetails.GetSingleFileDetails(id, cxnString, logPath);
                    path += folder.FolderName + "/";

                    if (folder.ParentFolderId == -1)
                    {
                        break;
                    }
                    id = folder.ParentFolderId;
                }

                string[] pathArry = path.Split(new char[] { '/' });

                path = string.Empty;
                path = "";

                for (int i = pathArry.Length - 1; i >= 0; i--)
                {
                    if (!string.IsNullOrEmpty(pathArry[i]))
                    {
                        path += pathArry[i] + "/";
                    }
                }

                return path;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void UpdateLastModifyDate(FolderHistoryManager his, string cxnString, string logPath)
        {
            if (parentId != 0)
            {
                try
                {
                    string history = "";

                    FolderDetailsDAO fd = new FolderDetailsDAO();
                    FolderDetails folder = fd.GetSingleFileDetails(parentId, cxnString, logPath);
                    if (folder != null)
                    {
                        if (!string.IsNullOrEmpty(folder.History))
                        {
                            //hfFolderHistory.Value = folder.History;
                            history = folder.History;
                        }
                    }

                    history = FolderHistoryManager.AppendHistory(history, his);

                    fd.UpdateModifyDate(parentId, history, cxnString, logPath);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
    }

}
