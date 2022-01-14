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
    /// Summary description for Handler_CreatedPreferences
    /// </summary>
    public class Handler_CreatedPreferences : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            
            String error = String.Empty;
            var Result = new ResultInfo("Please Signin", "error", "Signout.aspx", 0);
            if (context.Session["id"] != null)
            {
                Preferences.Created(ref error, context);
                if (error.ToLower().Equals("success"))
                    Result = new ResultInfo("Success", "OK", "", 1);
            }
            response.ContentType = "text/plain";
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