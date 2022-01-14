using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Web.SessionState;
using HTT;

namespace Web_SFH.Handlers
{
    /// <summary>
    /// Summary description for Handler_CreateCompany
    /// </summary>
    public class Handler_CreateCompany : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            response.ContentType = "text/plain";

            CompanyInfo company = new CompanyInfo(context);

            Boolean isWrite = false;
            String error = String.Empty;
            ResultInfo result = new ResultInfo("Authencation Failed.", "error", "Signin.aspx", 0);
            if (context.Session["id"] != null)
            {

                //Write Activity
                Activities activity = new Activities();
                activity.ID = Lib.get_value_str(context.Session["id"].ToString());
                activity.UserName = Lib.get_value_str(context.Session["Username"].ToString());
                activity.Type = 1;
                activity.Date = DateTime.Now;

                String cmd = Lib.get_value_str(request[FieldKeys.Cmd]);
                string companeName = Lib.get_value_str(request[FieldKeys.CompanyName]);
                if (cmd.Equals("C"))
                {
                    activity.Action = "ADD";
                    activity.Details = companeName;
                    isWrite = true;
                }
                else
                {
                    activity.Action = "EDIT ";
                    activity.Details = CompanyInfo.EditChanged(context);
                    isWrite = (activity.Details.TrimEnd() == companeName ? false : true);
                }
                if (isWrite)
                    activity.Write(ref error, activity);


                company.PostCompany(ref error, context);

                result = new ResultInfo(error, "OK", "", 0);
            }

            response.Write(JsonConvert.SerializeObject(result));
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