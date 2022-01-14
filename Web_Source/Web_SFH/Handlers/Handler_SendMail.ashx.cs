using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HTT;
using System.IO;
using Newtonsoft.Json;

namespace Web_SFH.Handlers
{
    /// <summary>
    /// Summary description for Handler_SendMail
    /// </summary>
    public class Handler_SendMail : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            var result = new ResultInfo("Login Failded", "error", "Signout.aspx", 0);
            //Get company list id
            String[] companyIds = Lib.get_value_str(request[FieldKeys.ListID]).Split(',');

            //Get is use content 
            Boolean isUseContent = Lib.get_value_int(request[FieldKeys.IsUseContent]).Equals(0) ? false : true;            

            //Get Extension days
            int extensionDays = Lib.get_value_int(request[FieldKeys.Days]);
            extensionDays = extensionDays.Equals(0) ? 7 : extensionDays;

            //Subject
            String subject = Lib.get_value_str(request[FieldKeys.Name]);

            //donor Select Type
            int donorSelectionType = Lib.get_value_int(request[FieldKeys.donorSelectionType]);

            //Schedule Random Report Type
            String s = Lib.get_value_str(request[FieldKeys.ReportType]);

            //Mail Content Body
            String body = Lib.get_value_str(request[FieldKeys.Content]);

            //Schedule Selection ID
            String scheduleIds = Lib.get_value_str(request[FieldKeys.ID]);
            String[] ids = scheduleIds.Split('_');
            int position = Lib.get_value_int(ids[1]);

            Schedules schedule = Schedules.ScheduleGet(ids);

            var companies = CompanyInfo.Gets();

            String donor = String.Empty;
            People people = new People();
            var peoples = people.Gets();
            foreach (string id in companyIds)
            {
                CompanyInfo com = companies.Single(x => x.CompanyID.Equals(Lib.get_value_int(id)));

                var specimens = schedule.Selections[position].DonorSpecimenList.FindAll(x => x.CompanyID.Equals(id));
                specimens = specimens.FindAll(x => x.Selected.Equals(true) || x.IsAlternate.Equals(true));
                donor = String.Empty;
                foreach (var specimen in specimens)
                {
                    String[] donorIDs = specimen.DonorID.Split('_');
                    people = peoples.Single(x => x.ID.Equals(donorIDs[1]));
                    //donor += "<p>";
                    donor += "<span style='padding:3px; width:300px; background-color:yellow;'>";
                    donor += people.PersonalInfo.Person.LastName + "&nbsp;&nbsp;&nbsp;" + people.PersonalInfo.Person.FirstName;
                    donor += "</span><br>";
                    //donor += "</p>";
                }

                scheduleIds = scheduleIds.ToUpper();

                

                FilePath fp = new FilePath(FieldKeys.ReportClass);

                string fileName = Lib.RetFileNameByType(s, scheduleIds, id, donorSelectionType, schedule.Type);

                String path = fp.Folder + fileName;

                //Get Email Setting
                MyOrganization myOrg = Preferences.Get().MyOrganization;

                SendMail mail = new SendMail();
                mail.Server = myOrg.Server;
                mail.Port = myOrg.Port;
                mail.Username = myOrg.Username;
                mail.Password = myOrg.Password;
                mail.From = myOrg.From;
                mail.FromName = myOrg.FromName;
                mail.To = com.PersonalInfo.Contact.Email;
                mail.ToName = com.CompanyName;
                mail.Cc = myOrg.Cc;
                mail.CcName = myOrg.CcName;
                mail.Bcc = myOrg.Bcc;
                mail.BccName = myOrg.BccName;
                mail.Subject = subject;
                String sdate = DateTime.Now.AddDays(extensionDays).ToString("MM/dd/yyyy");
                if (!isUseContent)
                {
                    body = myOrg.Content;
                    body = body.Replace("#Date#", sdate);
                    body = body.Replace("#CompanyName#", com.CompanyName);
                    body = body.Replace("#DonorList#", donor);
                }
                body += Lib.Signature(myOrg);

                mail.Body = body;
                if (File.Exists(path))
                {
                    mail.ListFile.Add(path);
                }
                Boolean kq = false;
                if (specimens.Count > 0)
                    kq = mail.Send();
                if (kq)
                {
                    result = new ResultInfo("Success", "OK", "", 1);
                }
            }

            response.ContentType = "text/plain";
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