using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using HTT;
using Newtonsoft.Json;

namespace Web_SFH.Handlers
{
    /// <summary>
    /// Summary description for Handler_GetSchedules
    /// </summary>
    public class Handler_GetSchedules : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            var result = new ResultInfo("Authencation Failed.", "error", "Signout.aspx");
            String error = String.Empty;
            if (context.Session["id"] != null)
            {
                List<Schedules> list = Schedules.Gets();
                People people = new People();
                List<People> peoples = people.Gets();

                int i = 0, k = 0, n = 0;
                foreach (Schedules schedule in list)
                {
                    k = 0; n = 0;
                    foreach (Schedule_Selections selection in schedule.Selections)
                    {
                        n = 0;
                        foreach (Specimen specimen in selection.DonorSpecimenList)
                        {
                            String[] Ids = specimen.DonorID.Split('_');
                            if (Ids.Length == 2)
                            {
                                if (peoples.Exists(x => x.ID.Equals(Ids[1])))
                                {
                                    var companyName = CompanyInfo.GetCompany(specimen.CompanyID).CompanyName;
                                    people = peoples.Single(x => x.ID.Equals(Ids[1]));
                                    list[i].Selections[k].DonorSpecimenList[n].PrimaryID = people.Driver.PrimaryID;
                                    list[i].Selections[k].DonorSpecimenList[n].FullName = people.PersonalInfo.Person.FullName;
                                    list[i].Selections[k].DonorSpecimenList[n].CompanyName = companyName;
                                }
                            }
                            n++;
                        }
                        k++;
                    }
                    i++;

                }
                list.Sort((x, y) => y.Started.CompareTo(x.Started));

                result = new ResultInfo("Authencation Failed.", "OK", "", list);
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