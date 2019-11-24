using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuarterlyReports.Models
{
    public class SideMenu
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }
    }

    public class AnonymousMenuList
    {

        [JsonProperty("sideMenu")]
        public List<SideMenu> SideMenu { get; set; }
    }

    public class SecureMenuList
    {

        [JsonProperty("sideMenu")]
        public List<SideMenu> SideMenu { get; set; }

        [JsonProperty("logo")]
        public string Logo { get; set; }

        [JsonProperty("collegeName")]
        public string CollegeName { get; set; }
    }


}