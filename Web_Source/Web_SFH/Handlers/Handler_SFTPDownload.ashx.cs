using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Newtonsoft.Json;
using Renci.SshNet;
using System.IO;
using System.Text;
using HTT;
namespace Web_SFH.Handlers
{
    /// <summary>
    /// Summary description for Handler_SFTPDownload
    /// </summary>
    public class Handler_SFTPDownload : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            response.ContentType = "text/plain";
            if (context.Session["id"] != null)
            {

                StringBuilder sb = new StringBuilder();
                String kq = String.Empty;

                SFTPDownload sftp = new SFTPDownload();
                sftp = sftp.GetSFTP("DRS");

                var ftpClient = new SftpClient(sftp.IPAddress, sftp.Port, sftp.Username, sftp.Password);

                ftpClient.Connect();

                if (ftpClient.IsConnected)
                {
                    //Get SFTP Directory File name List
                    var remoteFiles = ftpClient.ListDirectory(sftp.RemoteDirectory);
                    List<String> remoteFileList = new List<string>();
                    foreach (var file in remoteFiles)
                    {
                        string fileName = file.Name;
                        if(fileName.Length > 8)
                        {
                            fileName = fileName.Substring(3);
                            int year = Lib.get_value_int(fileName.Substring(4, 4));
                            int month = Lib.get_value_int(fileName.Substring(0, 2));
                            int day = Lib.get_value_int(fileName.Substring(2, 2));
                            DateTime dateOfFile= new DateTime(year, month, day);
                            DateTime dateOfGet = new DateTime(2019, 12, 29);
                            TimeSpan t = dateOfFile - dateOfGet;
                            if (t.TotalDays >= 0)
                                remoteFileList.Add(file.Name);
                            

                        }
                        
                    }

                    //Get Local Directory File name List
                    String LocalDir = sftp.LocalDirectory + @"DRS\";
                    if (!Directory.Exists(LocalDir))
                        Directory.CreateDirectory(LocalDir);

                    var localFiles = Directory.GetFiles(LocalDir, "*.xml").Select(file => Path.GetFileName(file));

                    var list = remoteFileList.Except(localFiles).ToList();


                    if (list.Count > 0)
                    {
                        foreach (String s in list)
                        {
                            sftp.Download(ref kq, s, ftpClient);
                        }
                    }
                    else
                    {
                        kq = "No more files to download.";
                    }
                }
                else
                {
                    sb.AppendLine("SFTP Server Connected Faild.");
                }


                response.Write(kq);
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