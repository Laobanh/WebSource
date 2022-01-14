using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Newtonsoft.Json;
using System.Web.SessionState;
using HTT;

namespace Web_SFH.Handlers
{
    /// <summary>
    /// Summary description for Handler_GetMROReports
    /// </summary>
    public class Handler_GetMROReports : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            response.ContentType = "text/plain";

            if (context.Session["id"] != null)
            {
                List<MROResult> list = new List<MROResult>();

                SFTPDownload sftp = new SFTPDownload();

                sftp = sftp.GetSFTP("DRS");

                var jsonDirectory = sftp.LocalDirectory + @"\SFH\JSON";

                var files = Directory.GetFiles(jsonDirectory).Select(x => Path.GetFileName(x));

                foreach (var file in files)
                {
                    String filePath = jsonDirectory + @"\" + file;
                    String json = File.ReadAllText(filePath);

                    MROResult rep = JsonConvert.DeserializeObject<MROResult>(json);
                    if (Lib.CompareDateInBetween(context, rep.CollectionDate))
                        list.Add(rep);
                }

                list.Sort((x, y) => Lib.ret_date(y.CollectionDate).CompareTo(Lib.ret_date(x.CollectionDate)));
                response.Write(JsonConvert.SerializeObject(list));
            }
            else
                response.Write(JsonConvert.SerializeObject(new ResultInfo("Authencation Failed.", "Failed", "Signin.aspx")));

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