﻿
namespace ITM.InventoryXmlConfiguration
{
    public class EmailDetails
    {
        public string EmailAddress { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Port { get; set; }
        public string Server { get; set; }
        public bool IsSSLEnable { get; set; }
        public bool IsHTMLResponse { get; set; }
    }
}
