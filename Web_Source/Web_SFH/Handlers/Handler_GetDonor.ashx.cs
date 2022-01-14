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
    /// Summary description for Handler_GetDonor
    /// </summary>
    public class Handler_GetDonor : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpResponse response = context.Response;
            HttpRequest request = context.Request;
            String id = Lib.get_value_str(request[FieldKeys.ID]);
            String consortiumID = Lib.get_value_str(request[FieldKeys.NewID]);
            response.ContentType = "text/plain";
            ResultInfo result = new ResultInfo("Authencation Failed.", "error", "Signin.aspx");
            //Lib.writerLog("Handler_GetDonor", "ProcessRequest", id + " " + consortiumID, "error");
            if (context.Session["id"] != null)
            {
                DonorInfo donorInfo = new DonorInfo();

                List<DonorInfo> list = new List<DonorInfo>();
                if (!consortiumID.Equals("0"))
                {
                    var comList = CompanyInfo.Gets();
                    comList = comList.FindAll(x => x.ConsortiumId.Equals(consortiumID));
                    foreach (var com in comList)
                    {
                        foreach (var donor in donorInfo.Gets(com.CompanyID.ToString()))
                        {
                            list.Add(donor);
                        }
                    }

                }
                else
                    list = donorInfo.Gets(id);

                for (int i = 0; i < list.Count; i++)
                {
                    donorInfo = list[i];
                    //Get Company name
                    CompanyInfo companyInfo = CompanyInfo.GetCompany(donorInfo.CompanyID);

                    list[i].CompanyName = companyInfo.CompanyName;
                    list[i].DerEmail = companyInfo.PersonalInfo.Contact.Email;
                    list[i].DerMobilePhone = companyInfo.PersonalInfo.Contact.MobilePhone;
                    list[i].DerFirstName = companyInfo.PersonalInfo.Person.FirstName;
                    list[i].DerLastName = companyInfo.PersonalInfo.Person.LastName;

                    //Get People Object
                    People people = People.Get(donorInfo.PrimaryID);
                    //Set data in DonoInfo
                    list[i].PeopleID = people.Driver.PrimaryID;
                    list[i].Firstname = people.PersonalInfo.Person.FirstName;
                    list[i].Lastname = people.PersonalInfo.Person.LastName;
                    list[i].Mode = people.Driver.Mode;
                    list[i].Category = people.Driver.Category;
                    list[i].DateOfBirth = people.PersonalInfo.Person.DateOfBirth;
                    list[i].MobilePhone = people.PersonalInfo.Contact.MobilePhone;


                }
                list.Sort((x, y) => x.CompanyName.CompareTo(y.CompanyName));
                //response.Write(JsonConvert.SerializeObject(donorInfos));
                result = new ResultInfo("Success", "OK", "", list);
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