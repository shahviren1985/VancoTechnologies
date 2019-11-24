using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    public static class DocumentManager
    {
        //Exam,Socio,Psy
        //one,two,three
        //Naresh Lad (naresh),Harshada Rathod (harshada)
        private static string GetUserName(string user)
        {
            try
            {
                int start = user.IndexOf("(");
                int end = user.IndexOf(")");
                return user.Substring(start + 1, end - start - 1);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static string GetUserNames(string userList)
        {

            string finalUserList = string.Empty;

            try
            {
                string[] users = userList.Split(',');
                foreach (string user in users)
                {
                    finalUserList += GetUserName(user) + ",";
                }

                // remove last comma from the string
                if (finalUserList.Length > 1)
                {
                    finalUserList = finalUserList.Substring(0, finalUserList.Length - 1);
                }
            }
            catch (Exception ex)
            {
                // Log the exception
            }

            return finalUserList;
        }

        public static List<string> GetUserList(string userName)
        {
            List<string> userList = new List<string>();
            try
            {
                string[] users = userName.Split(',');
                foreach (string user in users)
                {
                    userList.Add(GetUserName(user));
                }
            }
            catch (Exception ex)
            {
                // Log the exception
            }

            return userList;
        }

        public static bool IsUserPresent(string currentUserName, List<string> taggedUserList)
        {
            return taggedUserList.Contains(currentUserName);
        }

        public static List<string> GetFileTagList(string tagItems)
        {
            List<string> tagList = new List<string>();
            try
            {
                string[] tags = tagItems.Split(',');
                foreach (string tag in tags)
                {
                    tagList.Add(tag);
                }
            }
            catch (Exception ex)
            {
                // Log the exception
            }

            return tagList;
        }

        public static bool IsTagPresent(string keyword, List<string> tagList)
        {
            return tagList.Contains(keyword);
        }

        public static string GetDocumentType(bool isEmpty, bool isScan, bool isContent, string scanPath)
        {
            if (isEmpty)
                return "EMPTY";
            else if (isContent)
                return "HTML";
            else if (isScan)
            {
                string path = Path.GetExtension(scanPath).ToLower();
                if (path.Contains("jpg") || path.Contains("jpeg") || path.Contains("png") || path.Contains("gif") || path.Contains("bmp"))
                {
                    return "IMAGE";
                }
                else if (path.Contains("docx") || path.Contains("doc") || path.Contains("pdf") || path.Contains("xlsx") || path.Contains("xls"))
                {
                    return "DOCUMENT";
                }

            }

            return "OTHER";
        }
    }
}
