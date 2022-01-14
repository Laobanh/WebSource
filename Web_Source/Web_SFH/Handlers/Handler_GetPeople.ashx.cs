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
    /// Summary description for Handler_GetPeople
    /// </summary>
    public class Handler_GetPeople : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpResponse response = context.Response;
            HttpRequest request = context.Request;
            String id = Lib.get_value_str(request[FieldKeys.ID]);
            String consortiumID = Lib.get_value_str(request[FieldKeys.NewID]);
            response.ContentType = "text/plain";
            ResultInfo result = new ResultInfo("Authencation Failed.", "error", "Signin.aspx");
            //Lib.writerLog("Handler_GetDonor", "ProcessRequest", id + " " + consortiumID, "error");
            if (context.Session["id"] != null)
            {
                People people = new People();
                result = new ResultInfo("", "OK", "", people.Gets());
            }
            response.Write(JsonConvert.SerializeObject(result));
            //response.End();
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