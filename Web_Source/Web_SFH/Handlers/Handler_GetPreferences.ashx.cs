using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using HTT;
using Newtonsoft.Json;

namespace Web_SFH.Handlers
{
    /// <summary>
    /// Summary description for Handler_GetPreferences
    /// </summary>
    public class Handler_GetPreferences : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            response.ContentType = "text/plain";

            Preferences preference = Preferences.Get();              
            
            String error = String.Empty;
            var Result = new ResultInfo("Please Signin", "error", "Signout.aspx", 0);

            if (context.Session["id"] != null)
            {
                
                Result = new ResultInfo("Success", "OK", "", preference);
            }

            response.Write(JsonConvert.SerializeObject(Result));
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