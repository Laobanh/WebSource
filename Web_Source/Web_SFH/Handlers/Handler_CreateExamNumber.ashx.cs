using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HTT;
using Newtonsoft.Json;
using System.Threading;

namespace Web_SFH.Handlers
{
    /// <summary>
    /// Summary description for Handler_CreateExamNumber
    /// </summary>
    public class Handler_CreateExamNumber : IHttpHandler
    {
        //http://localhost/Handlers/Handler_CreateExamNumber.ashx?id=211101122030&donorid=1012_CAY3000798

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            response.ContentType = "text/plain";

            String error = String.Empty;
            String id = Lib.get_value_str(request[FieldKeys.ID]);
            String donorID = Lib.get_value_str(request[FieldKeys.DonorId]);
            String lic = Lib.get_value_str(request["lic"]);
            String[] donorids = donorID.Split('_');
            string peopleId = "0", companyId = donorids[0];
            if (donorids.Length == 2)
                peopleId = donorids[1];

            var people = People.Get(peopleId);

            MyOrganization organization = new MyOrganization();
            Mcsa5875 mcsa = new Mcsa5875();
            var d = DateTime.Now;
            String examNumber = d.ToString("yyMMddHHmmss");
            var result = new ResultInfo("Fialed", "error", "", 0);
            Boolean kq = false;
            if (id.Equals("0"))
            {
                mcsa.Created2(ref error, examNumber, lic);
                Thread.Sleep(2000);
                mcsa = mcsa.Get(examNumber);
                kq = true;
            }
            else
            {
                var fp = new FilePath(FieldKeys.Exam);
                var path = fp.Folder + id + ".json";
                if (File.Exists(path))
                    mcsa = mcsa.Get(id);
                else
                {
                    mcsa.Created(ref error, id, Lib.get_value_int(companyId), people, organization);
                    mcsa = mcsa.Get(id);
                }

                kq = true;
            }
            if (kq)
                result = new ResultInfo("Success", "OK", "", mcsa);
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