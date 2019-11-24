using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GreenNub.Models
{
    public class logmodel
    {
        public logmodel()
        {
            Listmodel = new List<logmodel>();
        }

        public string Description { get; set; }
        public string PageEvent { get; set; }
        public string Type { get; set; }
        public DateTime createdate { get; set; }
       
        public List<logmodel> Listmodel { get; set; }
    }
}