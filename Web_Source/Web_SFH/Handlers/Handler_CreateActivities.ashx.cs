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
    /// Summary description for Handler_CreateActivities
    /// </summary>
    public class Handler_CreateActivities : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpResponse response = context.Response;
            response.ContentType = "text/plain";
            ResultInfo resultInfo = new ResultInfo("Please Signin first.", "NO", "Signout.aspx", 0);
            String error = String.Empty;
            if (context.Session["id"] != null)
            {
                Activities activity = new Activities(context);
                activity.UserName = Lib.get_value_str(context.Session["Username"].ToString());
                activity.Post(ref error, activity);
                resultInfo = new ResultInfo("Comment was successfully Add.", "OK", "", 1);

            }
            response.Write(JsonConvert.SerializeObject(resultInfo));
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