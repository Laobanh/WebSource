using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_SFH.Handlers
{
    /// <summary>
    /// Summary description for Handler_ModifyMcsa5875
    /// </summary>
    public class Handler_ModifyMcsa5875 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
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