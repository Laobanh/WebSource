using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace HTT
{
    public class AddressInfo
    {
        #region Fields
        private String sAddress = "", sOfficeLocation = "", sCountry = "", sCity = "", sState = "";
        private int iZip = 0;
        #endregion End Fields

        #region Constructors
        public AddressInfo()
        {

        }

        public AddressInfo(String[] csv)
        {           
            this.sAddress = Lib.get_value_str(csv[15]);
            this.sState = Lib.get_value_str(csv[16]);
            this.sCity = Lib.get_value_str(csv[17]);
            this.iZip = Lib.get_value_int(csv[18]);
            this.sCountry = Lib.get_value_str(csv[19]);     
            this.sOfficeLocation = Lib.get_value_str(csv[20]);
        }

        public AddressInfo(HttpContext context)
        {
            HttpRequest request = context.Request;
            this.sAddress = Lib.get_value_str(request[FieldKeys.Address]);
            this.sOfficeLocation = Lib.get_value_str(request[FieldKeys.OfficeLocation]);
            this.sCountry = Lib.get_value_str(request[FieldKeys.Country]);
            this.sCity = Lib.get_value_str(request[FieldKeys.City]);
            this.sState = Lib.get_value_str(request[FieldKeys.State]);
            this.iZip = Lib.get_value_int(request[FieldKeys.Zip]);
        }

        public string Address
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

        public string OfficeLocation
        {
            get
            {
                return sOfficeLocation;
            }

            set
            {
                sOfficeLocation = value;
            }
        }

        public string Country
        {
            get
            {
                return sCountry;
            }

            set
            {
                sCountry = value;
            }
        }

        public string City
        {
            get
            {
                return sCity;
            }

            set
            {
                sCity = value;
            }
        }

        public string State
        {
            get
            {
                return sState;
            }

            set
            {
                sState = value;
            }
        }

        public int Zip
        {
            get
            {
                return iZip;
            }

            set
            {
                iZip = value;
            }
        }
        #endregion End Constructors

        #region Methods
        #endregion End Methods
    }
}