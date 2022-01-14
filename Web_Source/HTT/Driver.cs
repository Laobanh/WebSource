using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace HTT
{
    public class Driver
    {
        #region Fields
        private String sPrimaryID = "", sPrimaryIDType = "", sPrimaryIDExpirationDate = "", sAlternateID = "", sAltIDType = "", sAltIDExpirationDate = "", sCategory = "", sMode = "", sUseStateDLIDDrugTestOnly = "Y";
        #endregion End Fields

        #region Constructors
        public Driver()
        {

        }

        public Driver(HttpContext context)
        {
            HttpRequest request = context.Request;
            this.sPrimaryID = Lib.get_value_str(request[FieldKeys.PrimaryID]).Replace(" ", "");            
            this.sPrimaryIDType = Lib.get_value_str(request[FieldKeys.PrimaryIDType]);
            this.sPrimaryIDExpirationDate = Lib.get_value_str(request[FieldKeys.PrimaryIDExpirationDate]);
            this.sAlternateID = Lib.get_value_str(request[FieldKeys.AlternateID]);
            this.sAltIDType = Lib.get_value_str(request[FieldKeys.AltIDType]);
            this.sAltIDExpirationDate = Lib.get_value_str(request[FieldKeys.AltIDExpirationDate]);
            this.sMode = Lib.get_value_str(request[FieldKeys.Mode]);
            this.sCategory = Lib.get_value_str(request[FieldKeys.Category]);
            this.sUseStateDLIDDrugTestOnly = Lib.get_value_str(request[FieldKeys.UseStateDLIDDrugTestOnly]);
        }

        public Driver(String[] csv)
        {
            this.sPrimaryID = Lib.get_value_str(csv[0]).Replace(" ", "");
            this.sMode = Lib.get_value_str(csv[2]);
            this.sCategory = Lib.get_value_str(csv[3]);
            this.sAltIDExpirationDate = Lib.get_value_str(csv[4]);           
        }

        public string PrimaryID { get => sPrimaryID; set => sPrimaryID = value; }
        public string PrimaryIDType { get => sPrimaryIDType; set => sPrimaryIDType = value; }
        public string PrimaryIDExpirationDate { get => sPrimaryIDExpirationDate; set => sPrimaryIDExpirationDate = value; }
        public string AlternateID { get => sAlternateID; set => sAlternateID = value; }
        public string AltIDType { get => sAltIDType; set => sAltIDType = value; }
        public string AltIDExpirationDate { get => sAltIDExpirationDate; set => sAltIDExpirationDate = value; }
        public string Category { get => sCategory; set => sCategory = value; }
        public string Mode { get => sMode; set => sMode = value; }
        public string UseStateDLIDDrugTestOnly { get => sUseStateDLIDDrugTestOnly; set => sUseStateDLIDDrugTestOnly = value; }


        #endregion End Constructors

        #region Methods
        #endregion End Methods
    }
}