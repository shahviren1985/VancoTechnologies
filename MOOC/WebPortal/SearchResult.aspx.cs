using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;

public partial class SearchResult : PageBase
{
    private JavaScriptSerializer jss = new JavaScriptSerializer();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString.Count > 0)
            {
                string searchText = Request.QueryString["searchText"].ToString();

                SearchResultData searchResult = new SearchManager().GetSearchResult(searchText, "FOC");
                //SearchResultData searchResult = new SearchManager().GetSearchResult(searchText, Server.MapPath("Course/courseFOC"));
                hfSearchResult.Value = jss.Serialize(searchResult);
                this.Title = "Search Result for " + searchText;

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "startup", "<script type='text/javascript'>PopulateSearchResult('" + searchText + "');</script>", false);
            }
        }
        catch (Exception ex)
        {            
            throw ex;
        }
    }
}