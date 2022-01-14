using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Newtonsoft.Json;
using HTT;
using System.IO;


namespace Web_SFH.Handlers
{
    /// <summary>
    /// Summary description for Handler_GetRandomReports
    /// </summary>
    public class Handler_GetRandomReports : IHttpHandler, IRequiresSessionState
    {
        //http://localhost/handlers/handler_GetRandomReports.ashx?id=sf60002_0&donortype=2&reporttype=s
        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            response.ContentType = "text/plain";
            RandomReports.folder = new FilePath("").Folder;
            String scheduleIds = Lib.get_value_str(request[FieldKeys.ID]);

            scheduleIds = scheduleIds.Equals("0") ? "SF60001_0" : scheduleIds.ToUpper();
            String[] ids = scheduleIds.Split('_');

            int donorSelectionType = Lib.get_value_int(request[FieldKeys.donorSelectionType]);

            //Get schedule
            Schedules schedule = Schedules.ScheduleGet(ids);
            //Get companies
            List<CompanyInfo> companies = CompanyInfo.Gets(schedule, ids);

            String s = Lib.get_value_str(request[FieldKeys.ReportType]);
            String link = String.Empty;
            Boolean kq = false;
            var result = new ResultInfo("Please Signin first.", "error", "Signout.aspx", 0);
            if (context.Session["id"] != null)
            {
                FilePath fp = new FilePath(FieldKeys.ReportClass);
                string fileName = Lib.RetFileNameByType(s, scheduleIds, donorSelectionType);
                String path = fp.Folder + fileName;
                link = @"../data/reports/" + fileName + "?v=" + DateTime.Now.ToString("yyyyMMddHHmmss");
                if (File.Exists(path))                
                    kq = true;                
                else
                {
                    switch (s.ToUpper())
                    {
                        case FieldVaules.BaseList:
                            companies = CompanyInfo.Gets(schedule);
                            kq = RandomReports.PdfCreatedBaseListMaintained(schedule, companies, ids);
                            break;
                        case FieldVaules.NotificationLetters:
                            kq = RandomReports.PdfCreatedNotificationLetter(schedule, companies, ids);
                            //RandomReports.PdfCreatedNotificationLetterByCompany(schedule, companies, ids);
                            break;
                        case FieldVaules.RandomSummary:
                            kq = RandomReports.PdfCreatedRandomSummary(schedule, companies, ids);
                            break;
                        case FieldVaules.RandomList:
                            companies = CompanyInfo.Gets(schedule, ids, donorSelectionType);
                            kq = RandomReports.PdfCreatedRandomList(schedule, companies, ids, donorSelectionType);
                            break;
                        case FieldVaules.Notificationslip:
                            kq = RandomReports.PdfCreatedNotificationslip(schedule, companies, ids);
                            break;
                    }
                }


                result = new ResultInfo("Success", "OK", link, 1);
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