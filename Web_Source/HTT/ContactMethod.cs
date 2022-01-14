using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTT
{
    public class ContactMethod
    {
        #region Fields
        private String sHomePhone = "", sWorkPhone = "", sMobilePhone = "", sEmail = "";
        private String sWebsite = "";
        #endregion End Fields

        #region Constructors
        public ContactMethod()
        {

        }

        public ContactMethod(String[] csv)
        {
            this.sMobilePhone = Lib.get_value_str(csv[10]);
            this.sHomePhone = Lib.get_value_str(csv[11]);
            this.sWorkPhone = Lib.get_value_str(csv[12]);            
            this.sEmail = Lib.get_value_str(csv[13]);
            this.sWebsite = Lib.get_value_str(csv[14]);
        }

        public ContactMethod(HttpContext context)
        {
            HttpRequest request = context.Request;
            this.sHomePhone = Lib.get_value_str(request[FieldKeys.HomePhone]);
            this.sWorkPhone = Lib.get_value_str(request[FieldKeys.WorkPhone]);
            this.sMobilePhone = Lib.get_value_str(request[FieldKeys.MobilePhone]);
            this.sEmail = Lib.get_value_str(request[FieldKeys.Email]);
            this.sWebsite = Lib.get_value_str(request[FieldKeys.Website]);
        }

        public string HomePhone
        {
            get
            {
                return sHomePhone;
            }

            set
            {
                sHomePhone = value;
            }
        }

        public string WorkPhone
        {
            get
            {
                return sWorkPhone;
            }

            set
            {
                sWorkPhone = value;
            }
        }

        public string MobilePhone
        {
            get
            {
                return sMobilePhone;
            }

            set
            {
                sMobilePhone = value;
            }
        }

        public string Email
        {
            get
            {
                return sEmail;
            }

            set
            {
                sEmail = value;
            }
        }

        public string Website
        {
            get
            {
                return sWebsite;
            }

            set
            {
                sWebsite = value;
            }
        }
        #endregion End Constructors

        #region Methods
        #endregion End Methods
    }
}