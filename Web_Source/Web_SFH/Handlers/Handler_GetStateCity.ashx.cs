using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using HTT;
using System.IO;
using System.Text.RegularExpressions;



namespace Web_SFH.Handlers
{
    /// <summary>
    /// Summary description for Handler_GetStateCity
    /// </summary>
    public class Handler_GetStateCity : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            context.Response.ContentType = "text/plain";
            String state = Lib.get_value_str(request["state"]);
            String fileName = System.Web.HttpContext.Current.Server.MapPath("~") + @"\Data\StateCity.json";
            String kq = String.Empty;
            if (File.Exists(fileName))
            {
                var js = File.ReadAllText(fileName);
                //kq = Regex.Replace(js, @"\t|\n|\r", "");
                kq = js.ToUpper();
            }

            context.Response.Write(kq);
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