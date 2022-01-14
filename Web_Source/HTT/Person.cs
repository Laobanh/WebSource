using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HTT;

namespace HTT
{
    public class Person
    {

        #region Fields
        private String sFirstName = "", sMiddleName = "", sLastName = "", sTitle = "";
        private String sDateOfBirth = "", sGender = "M", sFullName = "";
        #endregion End Fields

        #region Constructors
        public Person()
        {

        }

        public Person(String[] csv)
        {           
            this.sFirstName = Lib.get_value_str(csv[5]);
            this.sMiddleName = Lib.get_value_str("0");
            this.sLastName = Lib.get_value_str(csv[6]);
            this.sTitle = Lib.get_value_str(csv[7]);
            this.sGender = Lib.get_value_str(csv[8]);
            this.sDateOfBirth = Lib.get_value_str(csv[9]);            
            this.sFullName = this.sFirstName + " " + this.sLastName;
        }


        public Person(HttpContext context)
        {
            HttpRequest request = context.Request;
            this.sFirstName = Lib.get_value_str(request[FieldKeys.FirstName]);
            this.sMiddleName = Lib.get_value_str(request[FieldKeys.MiddleName]);
            this.sLastName = Lib.get_value_str(request[FieldKeys.LastName]);
            this.sTitle = Lib.get_value_str(request[FieldKeys.Title]);
            this.sDateOfBirth = Lib.get_value_str(request[FieldKeys.DateOfBirth]);
            this.sGender = Lib.get_value_str(request[FieldKeys.Gender]);
            this.sFullName = this.sFirstName + " " + this.sLastName;
        }
        public string FirstName
        {
            get
            {
                return sFirstName;
            }

            set
            {
                sFirstName = value;
            }
        }

        public string MiddleName
        {
            get
            {
                return sMiddleName;
            }

            set
            {
                sMiddleName = value;
            }
        }

        public string LastName
        {
            get
            {
                return sLastName;
            }

            set
            {
                sLastName = value;
            }
        }

        /// <summary>
        /// Danh xưng Dr, Mr, Ms, Mrs
        /// </summary>
        public string Title
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

        /// <summary>
        /// Sinh nhật
        /// </summary>
        public string DateOfBirth
        {
            get
            {
                return sDateOfBirth;
            }

            set
            {
                sDateOfBirth = value;
            }
        }

        public string Gender
        {
            get
            {
                return sGender;
            }

            set
            {
                sGender = value;
            }
        }

        public string FullName { get => sFullName; set => sFullName = value; }
        #endregion End Constructors

        #region Methods
        #endregion End Methods
    }
}