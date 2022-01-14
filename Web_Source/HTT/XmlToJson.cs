using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.IO;
using System.Xml;

namespace HTT
{
    public class XmlToJson
    {

        public static String json()
        {
            String kq = String.Empty;
            String xmlPath = System.Web.HttpContext.Current.Server.MapPath("~") + "/Data/2021070201.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlPath);
            kq = JsonConvert.SerializeXmlNode(doc);
            return kq;
        }
    }
}