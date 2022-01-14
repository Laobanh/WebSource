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
    /// Summary description for Handler_GetConsortiums
    /// </summary>
    public class Handler_GetConsortiums : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpResponse response = context.Response;
            Consortiums consortiums = new Consortiums();
            ResultInfo result = new ResultInfo("Authencation Failed.", "error", "SignOut.aspx");
            if (context.Session["id"] != null)
            {
                var list = Consortiums.Gets();
                result = new ResultInfo("", "OK", "", list);
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