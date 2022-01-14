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
    /// Summary description for Handler_GetActivities
    /// </summary>
    public class Handler_GetActivities : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            response.ContentType = "text/plain";
            int type = Lib.get_value_int(request[FieldKeys.Type]);
            ResultInfo resultInfo = new ResultInfo("Please Signin first.", "error", "SignOut.aspx", 0);
            String error = String.Empty;
            if (context.Session["id"] != null)
            {
                Activities activity = new Activities(context);
                List<Activities> list = activity.Gets();
                list = list.FindAll(x => x.Type.Equals(type));
                list.Sort((x, y) => y.Date.CompareTo(x.Date));
               
                resultInfo = new ResultInfo("", "OK", "", list);                

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