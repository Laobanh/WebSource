using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Newtonsoft.Json;
using HTT;

namespace Web_SFH.Handlers
{
    /// <summary>
    /// Summary description for Handler_CreateConsortiums
    /// </summary>
    public class Handler_CreateConsortiums : IHttpHandler,IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            String error = String.Empty;
            ResultInfo result = new ResultInfo("Authencation Failed.", "error", "Signin.aspx", 0);
            if (context.Session["id"] != null)
            {
                Consortiums consortiums = new Consortiums();
                consortiums.Post(ref error, context);
                result = new ResultInfo(error, "OK", "", 1);
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