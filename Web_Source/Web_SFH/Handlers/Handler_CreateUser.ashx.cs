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
    /// Summary description for Handler_CreateUser
    /// </summary>
    public class Handler_CreateUser : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            UserInfo user = new UserInfo();
            String error = String.Empty;
            if (context.Session["id"] != null)
                user.PostUser(ref error, context);
            else
                error = JsonConvert.SerializeObject(new ResultInfo("Authencation Failed.", "Failed", "Signin.aspx"));

            //user.PostUser(ref error, context);
            response.Write(error);
            response.End();

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