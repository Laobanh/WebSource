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
    /// Summary description for Handler_GetTestResult
    /// </summary>
    public class Handler_GetTestResult : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            response.ContentType = "text/plain";       
            string[] array = Lib.get_value_str(request[FieldKeys.ID]).Split('_');
            string primaryID = String.Empty, companyID = String.Empty;
            if (array.Length.Equals(2))
            {
                //1008_CAY3000798
                companyID = array[0];
                primaryID = array[1];
            }
            String kq = JsonConvert.SerializeObject(new ResultInfo("Data Not found " + primaryID, "error", ""));
            var result = new ResultInfo("Data Not found " + primaryID, "error", "Signout.aspx");
            if(context.Session["id"]!= null)
            {
                if (!primaryID.Equals("0"))
                {
                    MROResult mROResult = new MROResult();
                    List<MROResult> list = mROResult.Gets(primaryID);

                    result = new ResultInfo("Success ", "OK", "", list);
                }
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