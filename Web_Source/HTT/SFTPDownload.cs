using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Renci.SshNet;
using System.Threading;


namespace HTT
{
    public class SFTPDownload
    {
        private String sName = "DRS", sIPAddress = "167.206.247.250", sUsername = "", sPassword = "";
        private String sRemoteDirectory = "", sLocalDirectory = "";
        private int iPort = 22;


        public SFTPDownload()
        {

        }

        public string Name
        {
            get
            {
                return sName;
            }

            set
            {
                sName = value;
            }
        }

        public string IPAddress
        {
            get
            {
                return sIPAddress;
            }

            set
            {
                sIPAddress = value;
            }
        }

        public int Port
        {
            get
            {
                return iPort;
            }

            set
            {
                iPort = value;
            }
        }

        public string Username
        {
            get
            {
                return sUsername;
            }

            set
            {
                sUsername = value;
            }
        }

        public string Password
        {
            get
            {
                return sPassword;
            }

            set
            {
                sPassword = value;
            }
        }

        public string RemoteDirectory
        {
            get
            {
                return sRemoteDirectory;
            }

            set
            {
                sRemoteDirectory = value;
            }
        }

        public string LocalDirectory
        {
            get
            {
                return sLocalDirectory;
            }

            set
            {
                sLocalDirectory = value;
            }
        }


        #region Methods
        public List<SFTPDownload> GetSFTPs()
        {
            List<SFTPDownload> list = new List<SFTPDownload>();
            String xmlPath = System.Web.HttpContext.Current.Server.MapPath("~") + @"Data\";
            String fileName = xmlPath + @"sftp.json";           
            String js = String.Empty;
            try
            {
                using (StreamReader read = new StreamReader(fileName))
                {
                    js = read.ReadToEnd();
                    //Base64Decoding
                    //js = Lib.Base64Decoding(js);
                    if (js.Length > 0)
                        list = JsonConvert.DeserializeObject<List<SFTPDownload>>(js);
                }               
            }
            catch(Exception ex)
            {
                Lib.writerLog("SFTPDownload", "GetSFTPs", ex.Message, "error");
            }
            return list;
        }


        public SFTPDownload GetSFTP(String name)
        {
            SFTPDownload sftp = new SFTPDownload();
            List<SFTPDownload> sFTPDownloads = this.GetSFTPs();
            if(sFTPDownloads.Count > 0)
            {
                sftp = (SFTPDownload)sFTPDownloads.Find(x => x.Name.Contains(name));
            }
            return sftp;
        }

        public void Download(ref String kq, String fileName,SftpClient ftpClient)
        {
            SFTPDownload sftp = new SFTPDownload();
            sftp = sftp.GetSFTP("DRS");

           // var ftpClient = new SftpClient(sftp.IPAddress, sftp.Port, sftp.Username, sftp.Password);

          //  ftpClient.Connect();

            if (ftpClient.IsConnected)
            {
                
                String LocalDir = sftp.LocalDirectory + @"DRS\";

                if (!Directory.Exists(LocalDir))
                    Directory.CreateDirectory(LocalDir);


                string remoteFileName = fileName;
                String localFileName = LocalDir + remoteFileName;
                if ((!fileName.StartsWith(".")))//&& ((file.LastWriteTime.Date == DateTime.Today))
                {
                    using (Stream file1 = File.OpenWrite(localFileName))
                    {
                        ftpClient.DownloadFile(sftp.RemoteDirectory + remoteFileName, file1);
                    }
                }
                Thread.Sleep(3000);
                ReportDRS reportDRS = new ReportDRS();
                reportDRS.CreateJsonPDF(ref kq, sftp.LocalDirectory, remoteFileName);
                kq = ("Write Json PDF: " + kq);
            }
            else
            {
                kq = ("SFTP Server Connected Faild.");
            }
        }
        
        #endregion End Methods
    }
}