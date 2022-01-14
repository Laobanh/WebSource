using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using Newtonsoft.Json;

namespace HTT
{
    public class MyOrganization
    {
        #region Fields
        private String sCompanyName = "", sNotice="", sUsername = "", sPassword = "", sServer = "", sCc = "", sCcName = "", sBcc = "", sBccName = "", sContent = "";
        private String sFrom = "", sFromName = "", sTo = "", sToName = "";
        private int iPortSSL = 0, iPort = 0;
        private String sFirstName, sLastName, sMobilePhone, sWorkPhone, sAddress, sState, sCity, sEmail, sWebsite;
        private int iZip;        
        #endregion End Fields
       
        #region Constructors
        public MyOrganization()
        {
            
        }

        public MyOrganization(HttpContext context)
        {
            HttpRequest request = context.Request;
            this.sCompanyName = Lib.get_value_str(request[FieldKeys.CompanyName]);
            this.sFirstName = Lib.get_value_str(request[FieldKeys.FirstName]);
            this.sLastName = Lib.get_value_str(request[FieldKeys.LastName]);
            this.sMobilePhone = Lib.get_value_str(request[FieldKeys.MobilePhone]);
            this.sWorkPhone = Lib.get_value_str(request[FieldKeys.WorkPhone]);
            this.sAddress = Lib.get_value_str(request[FieldKeys.Address]);
            this.sState = Lib.get_value_str(request[FieldKeys.State]);
            this.sCity = Lib.get_value_str(request[FieldKeys.City]);
            this.iZip = Lib.get_value_int(request[FieldKeys.Zip]);
            this.sEmail = Lib.get_value_str(request[FieldKeys.Email]);
            this.sWebsite = Lib.get_value_str(request[FieldKeys.Website]);
            this.sNotice = Lib.get_value_str(request[FieldKeys.Notice]);
            this.sPassword = Lib.get_value_str(request[FieldKeys.Password]);
            this.sUsername = Lib.get_value_str(request[FieldKeys.Username]);
            this.sServer = Lib.get_value_str(request[FieldKeys.Server]);
            this.sCc = Lib.get_value_str(request[FieldKeys.Cc]);
            this.sCcName = Lib.get_value_str(request[FieldKeys.CcName]);
            this.sBcc = Lib.get_value_str(request[FieldKeys.Bcc]);
            this.sBccName = Lib.get_value_str(request[FieldKeys.BccName]);
            this.sContent = Lib.get_value_str(request[FieldKeys.Content]);
            this.sFrom = Lib.get_value_str(request[FieldKeys.From]);
            this.sFromName = Lib.get_value_str(request[FieldKeys.FromName]);
            this.sTo = Lib.get_value_str(request[FieldKeys.To]);
            this.sToName = Lib.get_value_str(request[FieldKeys.ToName]);
            this.iPort = 587;
            this.iPortSSL = 465;
            
        }

        public string CompanyName { get => sCompanyName; set => sCompanyName = value; }
       
        public string Username { get => sUsername; set => sUsername = value; }
        public string Password { get => sPassword; set => sPassword = value; }
        public string Server { get => sServer; set => sServer = value; }
        public string From { get => sFrom; set => sFrom = value; }
        public string FromName { get => sFromName; set => sFromName = value; }
        public string To { get => sTo; set => sTo = value; }
        public string ToName { get => sToName; set => sToName = value; }
        public string Cc { get => sCc; set => sCc = value; }
        public string CcName { get => sCcName; set => sCcName = value; }
        public string Bcc { get => sBcc; set => sBcc = value; }
        public string BccName { get => sBccName; set => sBccName = value; }        
        public int PortSSL { get => iPortSSL; set => iPortSSL = value; }
        public int Port { get => iPort; set => iPort = value; }       
        public int Zip { get => iZip; set => iZip = value; }
        public string FirstName { get =>sFirstName; set => sFirstName = value; }
        public string LastName { get => sLastName; set => sLastName = value; }
        public string MobilePhone { get => sMobilePhone; set => sMobilePhone = value; }
        public string WorkPhone { get => sWorkPhone; set => sWorkPhone = value; }
        public string Address { get => sAddress; set => sAddress = value; }
        public string State { get => sState; set => sState = value; }
        public string City { get => sCity; set => sCity = value; }
        public string Notice { get => sNotice; set => sNotice = value; }
        public string Content { get => sContent; set => sContent = value; }       
        public string Email { get => sEmail; set => sEmail = value; }
        public string Website { get => sWebsite; set => sWebsite = value; }

        #endregion End Constructors

        #region Methods
        public static MyOrganization Get()
        {
            MyOrganization myOrganization = new MyOrganization();
            
            return myOrganization;
        }
        #endregion End Methods

    }
}
