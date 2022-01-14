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
    /// Summary description for Handler_CreatePersonal
    /// </summary>
    public class Handler_CreatePersonal : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            response.ContentType = "text/plain";
            var result = new ResultInfo("Authencation Failed.", "error", "Signout.aspx");
            Boolean isWrite = false;
            String error = String.Empty;
            People people = new People();
            if (context.Session["id"] != null)
            {
                //Write Activity
                Activities activity = new Activities();
                activity.ID = Lib.get_value_str(context.Session["id"].ToString());
                activity.UserName = Lib.get_value_str(context.Session["Username"].ToString());
                activity.Type = 2;
                activity.Date = DateTime.Now;

                String cmd = Lib.get_value_str(request[FieldKeys.Cmd]);
                string fullName = Lib.get_value_str(request[FieldKeys.LastName]) + " " + Lib.get_value_str(request[FieldKeys.FirstName]);
                if (cmd.Equals("C"))
                {
                    activity.Action = "ADD";
                    activity.Details = fullName + " PrimaryID: " + Lib.get_value_str(request[FieldKeys.PrimaryID]);
                    isWrite = true;

                    //Add DonorInfo when Add new People
                    String companeID = Lib.get_value_str(request[FieldKeys.ID]);
                    String primaryID = Lib.get_value_str(request[FieldKeys.PrimaryID]);
                    DonorInfo donor = new DonorInfo(companeID, primaryID);
                    donor.Write(ref error, donor);
                }
                else
                {
                    activity.Action = "EDIT ";
                    activity.Details = People.EditChanged(context);
                    isWrite = (activity.Details.TrimEnd() == fullName ? false : true);
                }
                if (isWrite)
                    activity.Write(ref error, activity);

                people.Post(ref error, context);

                result = new ResultInfo(error, "OK", "", 1);
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