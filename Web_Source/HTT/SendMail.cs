using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace HTT
{
    public class SendMail
    {
        #region Fields
        private string sServer = "", sFrom = "", sTo = "", sCc = "truongthinhltd@yahoo.com.vn", sFromName = "", sToName = "", sCcName = "", sBody = "", _sSubject = "",sUsername = "", sPassword = "";
        private int iPort = 25;
        private bool isEnablessl = true, isBodyHTML = true;
        private List<String> listFile;
        private String sBcc = "", sBccName = "";
        #endregion End Fields

        #region Constructors
        public SendMail()
        {
            this.listFile = new List<string>();
        }

        public string Server { get => sServer; set => sServer = value; }
        public int Port { get => iPort; set => iPort = value; }
        public string Username { get => sUsername; set => sUsername = value; }
        public string Password { get => sPassword; set => sPassword = value; }

        public string From { get => sFrom; set => sFrom = value; }
        public string FromName { get => sFromName; set => sFromName = value; }
        public string To { get => sTo; set => sTo = value; }

        public string ToName { get => sToName; set => sToName = value; }
        public string Cc { get => sCc; set => sCc = value; }    
        public string CcName { get => sCcName; set => sCcName = value; }
        public string Bcc { get => sBcc; set => sBcc = value; }
        public string BccName { get => sBccName; set => sBccName = value; }
        public string Body { get => sBody; set => sBody = value; }
        public string Subject { get => _sSubject; set => _sSubject = value; }
       
        
        public bool IsEnablessl { get => isEnablessl; set => isEnablessl = value; }
        public bool IsBodyHTML { get => isBodyHTML; set => isBodyHTML = value; }
        public List<string> ListFile { get => listFile; set => listFile = value; }
        
        #endregion End Constructors

        #region Methods

        public Boolean Send()
        {
            Boolean kq = false;
            MailAddress from = new MailAddress(this.From, this.FromName);
            MailAddress to = new MailAddress(this.To, this.ToName);
            MailMessage message = new MailMessage(from, to);
            if (!this.Bcc.Equals("0"))
                message.Bcc.Add(new MailAddress(this.Bcc, this.BccName));
            if (!this.Cc.Equals("0"))
                message.CC.Add(new MailAddress(this.Cc, this.CcName));
            message.Subject = this.Subject;

            message.Body = this.Body;

            message.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient(this.Server, this.Port);
            smtp.EnableSsl = true;
            NetworkCredential cr = new NetworkCredential(this.Username, this.Password);
            if (this.listFile.Count > 0)
            {
                foreach(var file in this.listFile)
                {
                    message.Attachments.Add(new Attachment(file));
                }
            }
            
            smtp.Credentials = cr;
            try
            {
                smtp.Send(message);
                kq = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);

            }

            return kq;
        }
        #endregion End Methods
    }
}
