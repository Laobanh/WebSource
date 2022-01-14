using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Newtonsoft.Json;
using HTT;
using System.Threading;

namespace Web_SFH.Handlers
{
    /// <summary>
    /// Summary description for Handler_Imports
    /// </summary>
    public class Handler_Imports : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            String companyID = Lib.get_value_str(request[FieldKeys.ID]);
            String type = Lib.get_value_str(request[FieldKeys.Type]);            
            type = type.ToUpper();
            String name = type.Contains("COMPANIES") ? "Company" : "Personal";

            String error = String.Empty;            
            String csv = String.Empty;            
            ResultInfo result = new ResultInfo("Authencation Failed.", "error", "Signin.aspx", 0);
            if (context.Session["id"] != null)
            {
                FilePath fp = new FilePath(FieldKeys.ImportClass);
                String filePath = String.Empty;
                //Upload CSV File
                foreach (string s in context.Request.Files)
                {
                    HttpPostedFile file = request.Files[s];
                    string fileName = file.FileName;
                    string fileExtension = "";// file.ContentType;
                    if (!String.IsNullOrEmpty(fileName))
                    {
                        fileExtension = Path.GetExtension(fileName);
                        filePath = fp.Folder + name + DateTime.Now.ToString("yyyyMMddHHmmss") + fileExtension;
                        file.SaveAs(filePath);
                        Thread.Sleep(2000);
                    }
                }

                //Import CSv file        
                if (File.Exists(filePath))
                {
                    csv = File.ReadAllText(filePath);
                    if (type.Contains("COMPANIES"))
                        CompanyInfo.Imports(ref error, csv, context.Session["id"].ToString(), context.Session["username"].ToString());
                    else
                        People.Imports(ref error, csv, context.Session["id"].ToString(), context.Session["username"].ToString());

                    result = new ResultInfo(error, "OK", "", 1);
                }
            }
            response.ContentType = "text/plain";
            response.Write(JsonConvert.SerializeObject(result));
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