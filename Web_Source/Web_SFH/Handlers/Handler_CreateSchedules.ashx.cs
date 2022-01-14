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
    /// Summary description for Handler_CreateSchedules
    /// </summary>
    public class Handler_CreateSchedules : IHttpHandler, IRequiresSessionState 
    {
        //http://localhost/Handlers/Handler_CreateSchedules.ashx?Cmd=C&Type=1&CompanyID=0&ConsortiumID=4001&newname=Random%20Selections%20for%20consortium%20OO
        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            var result = new ResultInfo("Authencation Failed.", "error", "Signout.aspx");
            String error = String.Empty;
            if (context.Session["id"] != null)
            {
                Schedules schedule = new Schedules(context);                
                schedule.Selections = Schedule_Selections.Gets(context);

                schedule.Post(ref error, schedule, context);
                result = new ResultInfo("", "OK", "", schedule);

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