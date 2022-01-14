using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.IO;
using HTT;

namespace Web_SFH.Handlers
{
    /// <summary>
    /// Summary description for Handler_GetConfigurations
    /// </summary>
    public class Handler_GetConfigurations : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            response.ContentType = "text/plain";
            String kq = String.Empty;
            FilePath fp = new FilePath("");
            String filepath = fp.Folder + "Configurations.json";
            if(File.Exists(filepath))
            {
                kq = File.ReadAllText(filepath);
               // kq = JsonConvert.SerializeObject(kq);

            }
            response.Write(kq);
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