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
    /// Summary description for Handler_GetFieldKey
    /// </summary>
    public class Handler_GetFieldKey : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpResponse response = context.Response;
            response.ContentType = "text/plain";
            String kq = String.Empty;
            List<FieldVaules> list = new List<FieldVaules>();
            list.Add(new FieldVaules());

            response.Write(JsonConvert.SerializeObject(list).ToString());
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