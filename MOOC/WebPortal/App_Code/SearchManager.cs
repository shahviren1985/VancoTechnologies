using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Configuration;
using System.IO;


/// <summary>
/// Summary description for SearchManager
/// </summary>
public class SearchManager
{
	public SearchManager()
	{
		// TODO: Add constructor logic here		
	}

    public SearchResultData GetSearchResult(string searchText, string course)
    {
        try
        {
            string folderPath = ConfigurationManager.AppSettings["BASE_PATH"] + "Course\\" + course;
            //string folderPath = course;

            List<string> searchedFiles = new List<string>();

            if (Directory.Exists(folderPath))
            {
                string[] dirs = Directory.GetDirectories(folderPath);
                DirectoryInfo dirCourseInfo = new DirectoryInfo(folderPath);

                foreach (string dir in dirs)
                {                    
                    DirectoryInfo dirChapter = new DirectoryInfo(dir);

                    string[] files = Directory.GetFiles(dir);

                    foreach (string file in files)
                    {
                        if (File.Exists(file))
                        {
                            using (StreamReader reader = new StreamReader(file))
                            {                                
                                string sAll = reader.ReadToEnd();
                                
                                if (sAll.ToLower().Contains(searchText.ToLower()))
                                {
                                    FileInfo fileInfo = new FileInfo(file);

                                    string fileFullName = "Content/" + dirCourseInfo.Name + "/" + dirChapter.Name + "/" + fileInfo.Name;

                                    searchedFiles.Add(fileFullName);
                                }
                            }
                        }
                    }
                }

                SearchResultData searchResult = new SearchResultData();
                searchResult.SearchedFiles = searchedFiles;
                searchResult.SearchTextPara = ""; // In future add whole paragraph which contains searchText

                return searchResult;
            }
        }
        catch (Exception ex)
        {            
            throw ex;
        }

        return null;
    }
}

public class SearchResultData
{
    public string SearchTextPara { get; set; }
    public List<string> SearchedFiles { get; set; }
}

public class SearchData
{
 
}