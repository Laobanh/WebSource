using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Web.SessionState;
using HTT;
using System.Web.Services;

namespace Web_SFH.Handlers
{
    /// <summary>
    /// Summary description for Handler_SignIn
    /// </summary>
    public class Handler_MCSA5875_LoadData : IHttpHandler, IRequiresSessionState
    {
        [WebMethod]
        public void ProcessRequest(HttpContext context)
        {
            Mcsa5875 mcsa = new Mcsa5875();

            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            var id = Lib.get_value_str(request["Id"]);
            var result = mcsa.Get(id);
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