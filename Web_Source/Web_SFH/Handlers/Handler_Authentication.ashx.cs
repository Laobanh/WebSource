using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using HTT;

namespace Web_SFH.Handlers
{
    /// <summary>
    /// Summary description for Handler_Authentication
    /// </summary>
    public class Handler_Authentication : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            UserInfo user = new UserInfo();
            user.PostAuth(context);
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