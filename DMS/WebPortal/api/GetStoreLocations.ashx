<%@ WebHandler Language="C#" Class="GetStoreLocations" %>

using System;
using System.Web;
using System.Web.Script.Serialization;
using AA.LogManager;
using System.Web.SessionState;
using AA.DAO;
using System.Collections.Generic;

public class GetStoreLocations : IHttpHandler, IRequiresSessionState
{
    Logger logger = new Logger();
    JavaScriptSerializer jss = new JavaScriptSerializer();
    string logPath = string.Empty;

    public void ProcessRequest(HttpContext context)
    {
        string logPath = context.Session["LogFilePath"].ToString();
        string cnxnString = context.Session["ConnectionString"].ToString();

        LocationDetailsDAO dao = new LocationDetailsDAO();

        try
        {
            List<Location> locations = dao.GetLocationDetails(cnxnString, logPath);
            context.Response.Write(jss.Serialize(locations));
        }
        catch (Exception ex)
        {

        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}