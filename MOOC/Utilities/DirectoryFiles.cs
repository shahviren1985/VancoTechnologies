using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITM.Courses.Utilities
{
    public class DirectoryFiles
    {
        public string FileName { get; set; }

        public List<DirectoryFiles> GetDirectoryFiles(string extention)
        {
            string[] tempArr = { ".html", ".html", ".html", ".html", ".html" };
            try
            {
                List<DirectoryFiles> dirFiles = new List<DirectoryFiles>();

                for (int i = 0; i < tempArr.Length; i++)
                {
                    DirectoryFiles dirFile = new DirectoryFiles();
                    dirFile.FileName = "DomyFile_" + i + tempArr[i];

                    dirFiles.Add(dirFile);
                }

                return dirFiles;
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }
    }
}
