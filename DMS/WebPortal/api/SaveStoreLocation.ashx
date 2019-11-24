<%@ WebHandler Language="C#" Class="SaveStoreLocation" %>

using AA.DAO;
using AA.LogManager;
using System;
using System.Web;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Web.SessionState;

public class SaveStoreLocation : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {

        long ticks = DateTime.Now.Ticks;
        JavaScriptSerializer jss = new JavaScriptSerializer();
        LocationDetailsDAO dao = new LocationDetailsDAO();
        string logPath = context.Session["LogFilePath"].ToString();
        string cnxnString = context.Session["ConnectionString"].ToString();

        try
        {
            string roomNumber = context.Request.Params[0];
            string Cupboard = context.Request.Params[1];
            string shelf = context.Request.Params[2];
            string fileName = context.Request.Params[3];
            string comments = context.Request.Params[4];
            string mode = context.Request.Params[5];

            if (!string.IsNullOrEmpty(mode))
            {
                if (mode.ToLower() == "create")
                {
                    
                }
                else if (mode.ToLower() == "update")
                {
                    
                }
            }
        }
        catch (Exception ex)
        {
            context.Response.Write("Error");
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