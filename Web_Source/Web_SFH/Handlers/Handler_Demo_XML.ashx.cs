using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Web;
using System.Xml;
using System.Xml.Schema;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using HTT;

namespace Web_SFH.Handlers
{
    /// <summary>
    /// Summary description for Handler_Demo_XML
    /// </summary>
    public class Handler_Demo_XML : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            String kq = String.Empty;
            List<String> ls = new List<string>();
            String xmlPath = System.Web.HttpContext.Current.Server.MapPath("~") + @"Data\MROReports\";
            String fileName = xmlPath + @"DRS\sfh07062021041125.xml";


            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);


            ReportDRS reports = new ReportDRS(doc);

            String reportFolder = xmlPath + "SFH";
            if (!Directory.Exists(reportFolder))
                Directory.CreateDirectory(reportFolder);

            foreach (MROResult result in reports.MRORes) { 

            //Write MRO Report PDF file
            String fileNameDestanition = reportFolder + @"\" + result.SpecimenNumber + ".pdf";
            Lib.WriteFilePDF(ref kq, fileNameDestanition, Convert.FromBase64String(result.ReportBinary.Data));

            //Create Report data
            String error = String.Empty;

            String jsonFileName = reportFolder + @"\" + result.SpecimenNumber + ".json";

                result.ReportBinary = new ReportBinary("Data/MROReports/SFH/" + result.SpecimenNumber + ".json", "");

            String sData = JsonConvert.SerializeObject(reports);

            //Write Json file
            Lib.WriteFileJson(ref error, jsonFileName, sData);
        }
            
           // context.Response.Write(sData.ToString());
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}