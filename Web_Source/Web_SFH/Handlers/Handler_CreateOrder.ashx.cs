using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using HTT;

namespace Web_SFH.Handlers
{
    /// <summary>
    /// Summary description for Handler_CreateOrder
    /// </summary>
    public class Handler_CreateOrder : IHttpHandler,IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
            String username = String.Empty, password = String.Empty, orderXML = String.Empty;
            String createLabOrderResault = String.Empty, createOrderResault = String.Empty;
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