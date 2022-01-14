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
    /// Summary description for Handler_GetPersonal
    /// </summary>
    public class Handler_GetPersonal : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            response.ContentType = "text/plain";
            String id = Lib.get_value_str(request["id"]);
            var result = new ResultInfo("Not Login", "error", "Signout.aspx", 0);
            if (context.Session["id"] != null)
            {
                var people = People.Get(id);
                result = new ResultInfo("Success", "OK", "", people);
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