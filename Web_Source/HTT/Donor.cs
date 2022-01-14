using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Web;

namespace HTT
{
    public class Donor
    {
        #region Fields
        private String sPrimaryID = "", sFirstname = "", sLastname = "", sMode = "", sCategory = "";
        private String sID = "";
        private int iExcludeFromSelection = 0, iNotActive = 0, iNotAvilabel = 0;
        private String sNotActiveDate = "0", sNotAvilabelDate = "0", sNotActiveReason = "0", sNotAvilabelReason = "0";
        #endregion End Fields

        #region Constructors
        public Donor()
        {

        }

        public Donor(string id)
        {
            this.sID = id;
        }

        public Donor(People people, String id)
        {
            this.sID = id;
            this.sPrimaryID = people.Driver.PrimaryID;
            this.sMode = people.Driver.Mode;
            this.sCategory = people.Driver.Category;
            this.sFirstname = people.PersonalInfo.Person.FirstName;
            this.sLastname = people.PersonalInfo.Person.LastName;
        }

        public string ID { get => sID; set => sID = value; }
        public string PrimaryID { get => sPrimaryID; set => sPrimaryID = value; }
        public string Firstname { get => sFirstname; set => sFirstname = value; }
        public string Lastname { get => sLastname; set => sLastname = value; }
        public string Mode { get => sMode; set => sMode = value; }
        public string Category { get => sCategory; set => sCategory = value; }
        public int ExcludeFromSelection { get => iExcludeFromSelection; set => iExcludeFromSelection = value; }
        public int NotActive { get => iNotActive; set => iNotActive = value; }
        public int NotAvilabel { get => iNotAvilabel; set => iNotAvilabel = value; }
        public string SNotActiveDate { get => sNotActiveDate; set => sNotActiveDate = value; }
        public string SNotAvilabelDate { get => sNotAvilabelDate; set => sNotAvilabelDate = value; }
        public string SNotActiveReason { get => sNotActiveReason; set => sNotActiveReason = value; }
        public string SNotAvilabelReason { get => sNotAvilabelReason; set => sNotAvilabelReason = value; }

        #endregion End Constructors

        #region Methods
        public static List<Donor> GetDonorList(String CompanyID, HttpContext context)
        {
            List<Donor> list = new List<Donor>();
            //Donor donor;
            String js = String.Empty;
            FilePath filePath = new FilePath(FieldKeys.DonorClass);

            string filename = filePath.Folder + CompanyID + ".json";

            //Add Donor New
            HttpRequest request = context.Request;
            String[] driverListID = Lib.get_value_str(request[FieldKeys.ListID]).Split(',', '-');
            /*
            if (File.Exists(filename))
            {
                js = File.ReadAllText(filename);
                js = StringEncryptDecrypt.Decrypt(js, FieldKeys.PassKey);
                var donors = JsonConvert.DeserializeObject<DonorInfo>(js);
                list = donors.DonorList.ToList();
                //Add Donor New                
                if (driverListID.Length > 0)
                {
                    foreach (String s in driverListID)
                    {
                        filePath = new FilePath(FieldKeys.DriverClass);
                        filename = filePath + s + ".json";
                        if (File.Exists(filename))
                        {
                            js = File.ReadAllText(filename);
                            js = StringEncryptDecrypt.Decrypt(js, FieldKeys.PassKey);
                            var driver = JsonConvert.DeserializeObject<DriverInfo>(js);

                            donor = new Donor(driver, s);

                            list.Add(donor);
                        }
                    }
                }

            }
            else
            {
                if (driverListID.Length > 0)
                {
                    foreach (String s in driverListID)
                    {
                        filePath = new FilePath(FieldKeys.DriverClass);
                        filename = filePath.Folder + s + ".json";


                        if (File.Exists(filename))
                        {
                            js = File.ReadAllText(filename);
                            js = StringEncryptDecrypt.Decrypt(js, FieldKeys.PassKey);

                            var driver = JsonConvert.DeserializeObject<DriverInfo>(js);

                            donor = new Donor(driver, s);

                            list.Add(donor);
                        }
                    }
                }
            }
            */
            return list;
        }
        #endregion End Methods
    }
}
