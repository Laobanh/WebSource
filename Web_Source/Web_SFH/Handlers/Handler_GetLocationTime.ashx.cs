using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HTT;

namespace Web_SFH.Handlers
{
    /// <summary>
    /// Summary description for Handler_GetLocationTime
    /// </summary>
    public class Handler_GetLocationTime : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            DateTime d = DateTime.Now;
            TimeZone timeZone = TimeZone.CurrentTimeZone;
            TimeSpan currentOfset = timeZone.GetUtcOffset(d);
            String kq = String.Empty;
            String sD = d.ToString("yyyy/MM/dd HH:mm:ss");
            kq = "Server </br>&emsp;Current Time: " + sD;
            kq += "<br/>&emsp;UTC Time: " + Lib.Get_UTCTime(sD).ToString("yyyy/MM/dd HH:mm:ss");
            kq += String.Format("{0:-30}{1}", "<br/>&emsp;UTC Time Zone: ", currentOfset);

            context.Response.Write(kq);
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