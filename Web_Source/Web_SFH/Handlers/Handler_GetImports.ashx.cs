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
    /// Summary description for Handler_GetImports
    /// </summary>
    public class Handler_GetImports : IHttpHandler,IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            String type = Lib.get_value_str(request[FieldKeys.Type]);
            String companyID = Lib.get_value_str(request[FieldKeys.ID]);
            String consortiumName = Lib.get_value_str(request[FieldKeys.NewName]);
            type = type.ToUpper().Contains("COMPANIES") ? "COMPANY" : "PERSON";
            String error = String.Empty;

            ResultInfo result = new ResultInfo("Authencation Failed.", "error", "Signin.aspx", 0);
            if (context.Session["id"] != null)
            {
                if (type.Equals("COMPANY"))
                    CompanyInfo.CreatedSample(ref error, companyID, consortiumName);
                else
                    People.CreatedSample(ref error, companyID, consortiumName);

                result = new ResultInfo("", "OK", error, 0);
               
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