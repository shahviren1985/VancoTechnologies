using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Xml;
using System.Configuration;
using ITM.ExceptionManager;


namespace ITM.ExceptionManager
{
    public static class ExceptionMapper
    {
        public static Hashtable Exceptions = null;

        static ExceptionMapper()
        {
            try
            {
                if (Exceptions == null)
                {
                    Exceptions = PopulateExceptions();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("23061");
            }
        }

        public static void Init()
        {
            try
            {

                if (Exceptions == null)
                {
                    Exceptions = PopulateExceptions();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("23061");
            }
        }
        private static Hashtable PopulateExceptions()
        {
            XmlDocument doc = new XmlDocument();
            Hashtable table = new Hashtable();

            try
            {
                doc.Load(ConfigurationManager.AppSettings["BASE_PATH"] + "/Release/ErrorSetting.xml");

                XmlNodeList errors = doc.SelectNodes("/ErrorMessages/Error");
                foreach (XmlNode error in errors)
                {
                    string key = error.Attributes["id"].InnerText;
                    string value = error.Attributes["message"].InnerText;

                    if (!table.Contains(key))
                    {
                        table.Add(key, value);
                    }
                }

                return table;
            }
            catch (Exception ex)
            {
                throw new Exception("23062");
            }
        }
    }
}
