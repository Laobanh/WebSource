using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using HTT;

namespace HTT
{
    public class User
    {
        #region Fields
        //User
        private readonly DateTime d = DateTime.Now;
        private int iId = 0;
        private String sUsername = "username", sPassword = "password", sDoctorName = "doctorname", sTitle = "title", sAddress = "address", sPhoneHome = "phonehome", sPhoneWork = "phonework", sPhoneMobile = "phonemobile",  sEmail = "email";
        private DateTime sCreatedDate, sModifiedDate;
        #endregion End Fields


        #region Constructors
        public User()
        {
        }
        public User(HttpContext context)
        {
            HttpRequest request = context.Request;
            this.iId = Lib.get_value_int(request[FieldKeys.ID]);
            this.sUsername = Lib.EncoderStringSH1(Lib.get_value_str(request[FieldKeys.Username]));
            this.sPassword = Lib.EncoderStringSH1(Lib.get_value_str(request[FieldKeys.Password]));
            //this.sDoctorName = Lib.get_value_str(FieldKeys.DoctorName);
            this.sTitle = Lib.get_value_str(FieldKeys.Title);
            this.sAddress = Lib.get_value_str(FieldKeys.Address);
            this.sPhoneHome = Lib.get_value_str(FieldKeys.HomePhone);
            this.sPhoneWork = Lib.get_value_str(FieldKeys.WorkPhone);
            this.sPhoneMobile = Lib.get_value_str(FieldKeys.MobilePhone);
            this.sEmail = Lib.get_value_str(FieldKeys.Email);
            this.sCreatedDate = d;
            this.sModifiedDate = d;


        }      

        public string SUsername
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

        public string SPassword
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

        public string SDoctorName
        {
            get
            {
                return sDoctorName;
            }

            set
            {
                sDoctorName = value;
            }
        }

        public string SAddress
        {
            get
            {
                return sAddress;
            }

            set
            {
                sAddress = value;
            }
        }

        public string SPhoneHome
        {
            get
            {
                return sPhoneHome;
            }

            set
            {
                sPhoneHome = value;
            }
        }

        public string SPhoneWork
        {
            get
            {
                return sPhoneWork;
            }

            set
            {
                sPhoneWork = value;
            }
        }

        public string SPhoneMobile
        {
            get
            {
                return sPhoneMobile;
            }

            set
            {
                sPhoneMobile = value;
            }
        }

        public string STitle
        {
            get
            {
                return sTitle;
            }

            set
            {
                sTitle = value;
            }
        }

        public string SEmail
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

        public DateTime SCreatedDate
        {
            get
            {
                return sCreatedDate;
            }

            set
            {
                sCreatedDate = value;
            }
        }

        public DateTime SModified
        {
            get
            {
                return sModifiedDate;
            }

            set
            {
                sModifiedDate = value;
            }
        }

        public int IId
        {
            get
            {
                return iId;
            }

            set
            {
                iId = value;
            }
        }



        #endregion End Constructor


        #region Methods
        public void CreatedUser(ref String error, HttpContext context)
        {
            Boolean kq = true;
            error = "Success";
            var user = new User(context);
            var data = JsonConvert.SerializeObject(user);
            String fileName = Lib.EncoderStringSH1("userSF") + ".json";
            String path = String.Format("~/Data");
            // kq = Lib.WriteFileJson(ref error,fileName, data.ToString());
            if (!kq)
                error = "Failed";

            //return kq;
        }
        #endregion End Methods

    }
}