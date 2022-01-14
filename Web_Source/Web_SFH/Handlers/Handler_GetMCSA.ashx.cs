using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using HTT;
using System.IO;

namespace Web_SFH.Handlers
{
    /// <summary>
    /// Summary description for Handler_GetMCSA
    /// </summary>
    public class Handler_GetMCSA : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            //response.AddHeader("Access-Control-Allow-Origin", "*");
            //response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT");
            //response.AddHeader("Access-Control-Allow-Headers", "Content-Type");

            string lng = Lib.get_value_str(request["lng"]);
            lng = lng.Equals("0") ? "en" : lng;
            FilePath fp = new FilePath("");
            string path = fp.Folder + "5875_" + lng + ".json";
            var js = File.ReadAllText(path);
            //js = JsonConvert.SerializeObject(js);
            response.ContentType = "text/plain";
            response.Write(js);
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