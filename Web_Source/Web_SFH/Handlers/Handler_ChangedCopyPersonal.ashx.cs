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
    /// Summary description for Handler_ChangedCopyPersonal
    /// </summary>
    public class Handler_ChangedCopyPersonal : IHttpHandler,IRequiresSessionState
    {
        //http://localhost/Handlers/Handler_ChangedCopyPersonal.ashx?ID=CV1452585&oldid=1001&newid=1008&oldname=%22ADA%20TRANSOPRTATIONC&newname=KL%20STAR%20INC&Cmd=Changed
        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            response.ContentType = "text/plain";
            FilePath fp = new FilePath(FieldKeys.DonorClass);
            String id = Lib.get_value_str(request[FieldKeys.ID]);
            String[] ids = id.Split('_');
            String primaryID = String.Empty;
            if (ids.Length == 2)
                primaryID = ids[1];

            String companyID_Old = Lib.get_value_str(request[FieldKeys.OldID]);
            String companyID_New = Lib.get_value_str(request[FieldKeys.NewID]);
            String companyName_Old = Lib.get_value_str(request[FieldKeys.OldName]);
            String companyName_New = Lib.get_value_str(request[FieldKeys.NewName]);
            String Cmd = Lib.get_value_str(request[FieldKeys.Cmd]);
            String error = String.Empty;
            ResultInfo result = new ResultInfo("Authencation Failed.", "Failed", "Signout.aspx");
            Activities activities = new Activities();
            DonorInfo donorInfo = new DonorInfo();
            var donor = new DonorInfo();
            if (context.Session["id"]!= null)
            {
                activities.ID = Lib.get_value_str(context.Session["id"].ToString());
                activities.UserName = Lib.get_value_str(context.Session["Username"].ToString());
                activities.Type = 1;
                activities.Date = DateTime.Now;

                var list = donorInfo.Gets(companyID_Old);
                var isExists = list.Exists(x => x.PrimaryID.Equals(primaryID));
                
                if (Cmd.ToUpper().Equals("CHANGED") && isExists)
                {
                    //Get Donorinfo by PrimaryID
                    donor = list.Single(x => x.PrimaryID.Equals(primaryID));

                    //Remove donor from list
                    donorInfo.Remove(id);

                    //Init new donor
                    donor = new DonorInfo(companyID_New, primaryID);

                    //Write donorinfo to json file
                    donorInfo.Write(ref error, donor);

                    var people = People.Get(primaryID);

                    activities.Action = "Changed Company";                    

                    activities.Details = "Moved " + people.PersonalInfo.Person.FirstName + " " + people.PersonalInfo.Person.LastName + " from " + companyName_Old + " to " + companyName_New;
                   
                }
                else
                {
                    //Init new donor
                    donor = new DonorInfo(companyID_New, primaryID);
                    
                    //Write donorinfo to json file
                    donorInfo.Write(ref error, donor);

                    var people = People.Get(primaryID);

                    activities.Action = "Copy person";

                    activities.Details = "Copy " + people.PersonalInfo.Person.FirstName + " " + people.PersonalInfo.Person.LastName + " to " + companyName_New;

                  

                }
                if (error.ToUpper().Equals("SUCCESS"))
                {
                    result = new ResultInfo(activities.Details, "OK", "", 1);
                }

                activities.Write(ref error, activities);               
            }

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