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
    /// Summary description for Handler_GetDrivers
    /// </summary>
    public class Handler_GetDrivers : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpResponse response = context.Response;
            response.ContentType = "text/plain";
            if (context.Session["id"] != null)
            {
                People people = new People();
            List<People> list = people.Gets();
            list.Sort((x, y) => y.CreatedDate.CompareTo(x.CreatedDate));
            string js = JsonConvert.SerializeObject(list);
            response.Write(js);
            }
            else
                response.Write(JsonConvert.SerializeObject(new ResultInfo("Authencation Failed.", "Failed", "Signin.aspx")));

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