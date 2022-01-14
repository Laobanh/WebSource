using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HTT;
using Newtonsoft.Json;

namespace Web_SFH.Handlers
{
    /// <summary>
    /// Summary description for Handler_CheckPeople
    /// </summary>
    public class Handler_CheckPeople : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            response.ContentType = "text/plain";
            string id = Lib.get_value_str(request[FieldKeys.ID]);
            var result = new ResultInfo("No Login", "error", "");
            var donor = new DonorInfo();
            var donors = donor.Gets();
            List<Items> donorIds = new List<Items>();
            var coms = CompanyInfo.Gets();
            if (!id.Equals("0"))
            {
               
                var kq = donors.Exists(x => x.PrimaryID.Equals(id));
                if (kq)
                {
                    foreach (var d in donors)
                    {
                        if (d.PrimaryID.Equals(id))
                        {
                            string[] ids = d.ID.Split('_');
                            string name = coms.Single(x => x.CompanyID.Equals(Lib.get_value_int(ids[0]))).CompanyName;
                            donorIds.Add(new Items(d.ID, name));
                        }
                    }
                    result = new ResultInfo("Success", "OK", DateTime.Now.ToString("yyMMddHHmmss"), donorIds);
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

    public class Items
    {
        private string id = "", name = "";

        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public Items(String sid,String sname)
        {
            this.id = sid;
            this.name = sname;
        }
    }
}