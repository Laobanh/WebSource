using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HTT;

namespace Web_SFH.Handlers
{
    /// <summary>
    /// Summary description for Handler_GetAge
    /// </summary>
    public class Handler_GetAge : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            string sDob = Lib.get_value_str(request["dob"]);
            string[] dobs = sDob.Split('/');
            DateTime birthDay = DateTime.Now;
            DateTime d = DateTime.Now;
            if (dobs.Length == 3)
            {
                int year = Lib.get_value_int(dobs[2]);
                int month = Lib.get_value_int(dobs[0]);
                int day = Lib.get_value_int(dobs[1]);
                birthDay = new DateTime(year, month, day);
            }

            int years = DateTime.Now.Year - birthDay.Year;

            if ((birthDay.Month > DateTime.Now.Month) || (birthDay.Month == DateTime.Now.Month && birthDay.Day > DateTime.Now.Day))
                years--;

            response.ContentType = "text/plain";
            response.Write(years.ToString());
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