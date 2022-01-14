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
    /// Summary description for Handler_GetCompanies
    /// </summary>
    public class Handler_GetCompanies : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            String consortiumID = Lib.get_value_str(request[FieldKeys.ID]);
            response.ContentType = "text/plain";
            ResultInfo resultInfo = new ResultInfo("Authencation Failed.", "error", "SignOut.aspx");
            if (context.Session["id"] != null)
            {
                CompanyInfo com = new CompanyInfo();
                FilePath path = new FilePath(FieldKeys.CompanyClass);
                // List<CompanyInfo> coms = com.GetListCompanies(path.FilePathName);
                var coms = CompanyInfo.Gets();
                DonorInfo donor = new DonorInfo();
                int i = 0;
                var donors = donor.Gets();               
                foreach (CompanyInfo c in coms)
                {
                    coms[i].SumDriver = donors.FindAll(x => x.CompanyID.Equals(c.CompanyID.ToString()) && x.NotActive.Equals(0)).Count;
                    i++;
                }

               
                if (!String.IsNullOrEmpty(consortiumID) && !consortiumID.Equals("0"))
                    coms = coms.FindAll(x => x.ConsortiumId.Equals(consortiumID));
               
                resultInfo = new ResultInfo("", "OK", "", coms);
            }

            response.Write(JsonConvert.SerializeObject(resultInfo));
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