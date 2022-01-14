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
    /// Summary description for Handler_GetDonorWorkAtCompanies
    /// </summary>
    public class Handler_GetDonorWorkAtCompanies : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            response.ContentType = "text/plain";
            var result = new ResultInfo("Authencation Failed.", "error", "Signout.aspx");
            if (context.Session["id"] != null)
            {
                string[] array = Lib.get_value_str(request[FieldKeys.ID]).Split('_');
                string primaryID = String.Empty, companyID = String.Empty;
                if (array.Length.Equals(2))
                {
                    //1008_CAY3000798
                    companyID = array[0];
                    primaryID = array[1];
                }
                CompanyInfo company = new CompanyInfo();
                FilePath path = new FilePath(FieldKeys.CompanyClass);
                List<CompanyInfo> coms = company.GetListCompanies(path.FilePathName);
                List<CompanyInfo> list = new List<CompanyInfo>();
                DonorInfo donorInfo = new DonorInfo();
                List<DonorInfo> donorInfos = donorInfo.Gets();               
                foreach (DonorInfo donor in donorInfos)
                {
                    string id = donor.PrimaryID.Replace(" ", "");                    
                    if (id.Equals(primaryID))
                    {
                        company = CompanyInfo.GetCompany(donor.CompanyID);
                        company.DonorWorking = donor.NotActive.Equals(0) ? 1 : 0;
                        list.Add(company);
                    }
                        
                }
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