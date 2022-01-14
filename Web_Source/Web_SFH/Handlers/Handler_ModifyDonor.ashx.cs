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
    /// Summary description for Handler_ModifyDonor
    /// </summary>
    public class Handler_ModifyDonor : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpResponse response = context.Response;
            response.ContentType = "text/plain";
            DonorInfo donorInfo = new DonorInfo();
            String error = String.Empty;
            ResultInfo result = new ResultInfo("Authencation Failed.", "error", "Signin.aspx");

            if (context.Session["id"] != null)
            {
                donorInfo.Post(ref error, context);

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