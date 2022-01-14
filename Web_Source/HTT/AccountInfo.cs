using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace HTT
{
    public class AccountInfo
    {
        #region Field
        private String sUsername = "username", sPassword = "password", sGroup = "";
        #endregion End Fields

        #region Properties
        #endregion End Prorpeties

        #region Constructors
        public AccountInfo()
        {

        }

        public AccountInfo(HttpContext context)
        {
            HttpRequest request = context.Request;
            String user = Lib.get_value_str(request[FieldKeys.Username]);
            String pwd = Lib.get_value_str(request[FieldKeys.Password]);
            String cmd = Lib.get_value_str(request[FieldKeys.Cmd]);            
            pwd = Lib.EncoderStringSH1(pwd);
           // user = Lib.EncoderStringSH1(user);            
            this.sUsername = user;
            this.sPassword = pwd;
            this.sGroup = Lib.get_value_str(request[FieldKeys.Group]);
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

        public string Group { get => sGroup; set => sGroup = value; }
        #endregion End Constructors


    }
}