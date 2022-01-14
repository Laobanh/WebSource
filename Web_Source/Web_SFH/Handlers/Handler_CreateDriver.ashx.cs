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
    /// Summary description for Handler_CreateDriver
    /// </summary>
    public class Handler_CreateDriver : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpResponse Response = context.Response;
            Response.ContentType = "text/plain";
            People people = new People();
            String error = String.Empty;
            if (context.Session["id"] != null)
                people.Post(ref error, context);
            else
                error = JsonConvert.SerializeObject(new ResultInfo("Authencation Failed.", "Failed", "Signin.aspx"));
            Response.Write(error);
            Response.End();
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