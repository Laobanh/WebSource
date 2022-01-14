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
    /// Summary description for Handler_CreateDonor
    /// </summary>
    public class Handler_CreateDonor : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpResponse response = context.Response;
            HttpRequest request = context.Request;
            response.ContentType = "text/plain";
            ResultInfo result = new ResultInfo("Authencation Failed.", "Failed", "Signin.aspx");
            if (context.Session["id"] != null)
            {
                DonorInfo donorInfo = new DonorInfo(context);
                String error = String.Empty;
                donorInfo.Post(ref error, context);
                // error = request[FieldKeys.ID] + "/" + request[FieldKeys.ListID];
                result = new ResultInfo("Success", "OK", "", 1);
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