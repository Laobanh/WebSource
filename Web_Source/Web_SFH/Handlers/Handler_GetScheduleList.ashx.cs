using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HTT;
using Newtonsoft.Json;
using System.Web.SessionState;

namespace Web_SFH.Handlers
{
    /// <summary>
    /// Summary description for Handler_GetScheduleList
    /// </summary>
    public class Handler_GetScheduleList : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            response.ContentType = "text/plain";

            var result = new ResultInfo("Authencation Failed.", "error", "Signout.aspx");
            String error = String.Empty;
            if (context.Session["id"] != null)
            {
                List<Schedules> list = Schedules.Gets();
                if (list.Count > 0)
                {
                    result = new ResultInfo("success", "OK", "", list);
                }

            }

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