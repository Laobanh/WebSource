using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Newtonsoft.Json;
using System.Web.SessionState;


namespace HTT
{
    public class CompanyInfo
    {
        //localhost/Handlers/Handler_CreateCompany.ashx?cmd=C&CompanyName=HTT
        #region Fields
        private readonly DateTime d = DateTime.Now;
        private int iCompanyID = 1;
        private int iSumDriver = 0;
        private String sCompanyName = "";
        private PersonalInfo personalInfo;
        private DateTime sCreatedDate, sModifiedDate;
        private String sConsortiumId = "";
        private String sPlan = "";
        private String sExpirationDate = "0";
        private int iBill = 0;
        private String Cmd = "A";
        private String sConsortiumName = "";
        private int iDonorWorking = 0;
        #endregion End Fields

        #region Constructors
        public CompanyInfo() { }   
        
        public CompanyInfo(String[] csv)
        {
            //this.iCompanyID = Lib.get_value_int(csv[0]);

            this.sCompanyName = Lib.get_value_str(csv[0]);            

            this.sConsortiumId = Lib.get_value_str(csv[1]);

            this.sPlan = Lib.get_value_str(csv[2]);

            this.iBill = Lib.get_value_int(csv[3]);

            this.sExpirationDate = Lib.get_value_str(csv[4]);

            this.personalInfo = new PersonalInfo(csv);
        }

        public CompanyInfo(HttpContext context)
        {
            HttpRequest request = context.Request;

            this.iCompanyID = Lib.get_value_int(request[FieldKeys.ID]);

            this.sCompanyName = Lib.get_value_str(request[FieldKeys.CompanyName]);

            this.personalInfo = new PersonalInfo(context);

            this.sConsortiumId = Lib.get_value_str(request[FieldKeys.ConsortiumID]);

            this.sPlan = Lib.get_value_str(request[FieldKeys.Plan]);

            this.iBill = Lib.get_value_int(request[FieldKeys.Bill]);

            this.sExpirationDate = Lib.get_value_str(request[FieldKeys.ExpirationDate]);

            String sCmd = Lib.get_value_str(request[FieldKeys.Cmd]);
            this.Cmd = sCmd.ToUpper();
            if (this.Cmd.Equals("C"))
                this.sCreatedDate = d;
            else if (this.Cmd.Equals("M"))
                this.sModifiedDate = d;
        }

        public int CompanyID
        {
            get
            {
                return iCompanyID;
            }

            set
            {
                iCompanyID = value;
            }
        }

        public string CompanyName
        {
            get
            {
                return sCompanyName;
            }

            set
            {
                sCompanyName = value;
            }
        }
               
        public PersonalInfo PersonalInfo
        {
            get
            {
                return personalInfo;
            }

            set
            {
                personalInfo = value;
            }
        }

        public DateTime CreatedDate
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

        public DateTime ModifiedDate
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

        public string ConsortiumId { get => sConsortiumId; set => sConsortiumId = value; }
        public string Plan { get => sPlan; set => sPlan = value; }
        public int SumDriver { get => iSumDriver; set => iSumDriver = value; }
        public string ExpirationDate { get => sExpirationDate; set => sExpirationDate = value; }
        public int Bill { get => iBill; set => iBill = value; }
        public string ConsortiumName { get => sConsortiumName; set => sConsortiumName = value; }
        public int DonorWorking { get => iDonorWorking; set => iDonorWorking = value; }

        #endregion Constructors

        #region Methods
        public void CreatedCompany(ref String error, HttpContext context, CompanyInfo com)
        {
            this.WriteCompany(ref error, com);
        }

        public void ModifyCompany(ref String error, HttpContext context, CompanyInfo com)
        {
            try
            {
                FilePath fp = new FilePath(FieldKeys.CompanyClass);

                String filePath = fp.FilePathName;

                //CompanyInfo comModify = new CompanyInfo(context);

                List<CompanyInfo> companies = this.GetListCompanies(filePath);


                var itemToRemove = companies.Single(x => x.CompanyID.Equals(com.CompanyID));


                com.sCreatedDate = itemToRemove.sCreatedDate;

                com.ConsortiumId = itemToRemove.ConsortiumId;

                companies.Remove(itemToRemove);

                companies.Add(com);

                companies.Sort((a, b) => a.CompanyID.CompareTo(b.CompanyID));

                String data = JsonConvert.SerializeObject(companies);
                //Base64Encoding
                // data = Lib.Base64Encoding(data);  
                data = StringEncryptDecrypt.Encrypt(data, FieldKeys.PassKey);
                Lib.WriteFileJson(ref error, filePath, data);

            }
            catch
            {
                error = "Company does not exist, Please check again.";
            }
        }

        public void ModifyConsortium(ref String error, HttpContext context)
        {
            try
            {
                FilePath fp = new FilePath(FieldKeys.CompanyClass);

                String filePath = fp.FilePathName;

                CompanyInfo com = new CompanyInfo(context);

                List<CompanyInfo> list = this.GetListCompanies(filePath);
                int i = 0;
                foreach(var c in list)
                {
                    if (c.CompanyID.Equals(com.CompanyID))
                        list[i].ConsortiumId = com.ConsortiumId;
                    i++;                    
                }                

                list.Sort((a, b) => a.CompanyID.CompareTo(b.CompanyID));

                String data = JsonConvert.SerializeObject(list);
                //Base64Encoding
                // data = Lib.Base64Encoding(data);  
                data = StringEncryptDecrypt.Encrypt(data, FieldKeys.PassKey);
                Lib.WriteFileJson(ref error, filePath, data);

            }
            catch
            {
                error = "Company does not exist, Please check again.";
            }
        }

        public void PostCompany(ref String error, HttpContext context)
        {
            error = "Success";

            var com = new CompanyInfo(context);

            String cmd = com.Cmd.ToUpper();

            if (cmd.Equals("C"))
                this.CreatedCompany(ref error, context, com);
            else if (cmd.Equals("M"))
            {
                this.ModifyCompany(ref error, context, com);
            }
            else if (cmd.Equals("E"))
            {
                this.ModifyConsortium(ref error, context);
            }
            else error = "No Execute Command";
        }

        public void WriteCompany(ref String error, CompanyInfo com)
        {
            Boolean kq = false;

            List<CompanyInfo> companies = new List<CompanyInfo>();

            FilePath fp = new FilePath(FieldKeys.CompanyClass);

            String fileName = fp.FileName;

            String folder = fp.Folder;

            String filePath = fp.FilePathName;

            if (!Directory.Exists(folder))
            {
                //DirectoryInfo di = Directory.CreateDirectory(folder);
                //di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                Directory.CreateDirectory(folder);
            }

            String name = com.sCompanyName;
            if (!String.IsNullOrEmpty(name) && !name.Equals("0"))
            {
                int classNumber = 1000;
                if (!File.Exists(filePath))
                {
                    com.iCompanyID = 1 + classNumber;
                    companies.Add(com);
                }
                else //Read data from file json exists
                {

                    companies = this.GetListCompanies(filePath);

                    int count = companies.Count + 1 + classNumber;

                    com.CompanyID = count;

                    companies.Add(com);
                }

                String data = JsonConvert.SerializeObject(companies);
                //Base64Encoding
                // data = Lib.Base64Encoding(data);
                data = StringEncryptDecrypt.Encrypt(data,FieldKeys.PassKey);
                if (!kq)
                    Lib.WriteFileJson(ref error, filePath, data);
            }
            else
                error = "Company name is required.";
        }
        public List<CompanyInfo> GetListCompanies(String filePath)
        {
            List<CompanyInfo> list = new List<CompanyInfo>();
            try
            {

                String js = String.Empty;
                using (StreamReader read = new StreamReader(filePath))
                {
                    js = read.ReadToEnd();
                    //Base64Decoding
                    //js = Lib.Base64Decoding(js);
                    js = StringEncryptDecrypt.Decrypt(js, FieldKeys.PassKey);
                    if (js.Length > 0)
                        list = JsonConvert.DeserializeObject<List<CompanyInfo>>(js);
                }


            }
            catch (Exception ex)
            {
                Lib.writerLog("CompanyInfo", "GetListCompanies", ex.Message, "error");

            }
            return list;
        }

        public static List<CompanyInfo> Gets()
        {
            FilePath fp = new FilePath(FieldKeys.CompanyClass);

            String filePath = fp.FilePathName;

            CompanyInfo com = new CompanyInfo();

            List<CompanyInfo> companies = com.GetListCompanies(filePath);
            int i = 0;
            foreach(var c in companies)
            {
                var kq = Consortiums.Gets().Exists(x => x.ID.Equals(c.ConsortiumId));
                if (kq)
                    companies[i].ConsortiumName = Consortiums.Gets().Single(x => x.ID.Equals(c.ConsortiumId)).Name;
                i++;
            }
            companies.Sort((x, y) => y.ConsortiumName.CompareTo(x.ConsortiumName));
            return companies;
        }

        public static List<CompanyInfo> Gets(String folder)
        {

            String filePath = folder + @"\" + Lib.EncoderStringSH1("companySF") + ".json";

            List<CompanyInfo> list = new List<CompanyInfo>();
            try
            {

                String js = String.Empty;
                using (StreamReader read = new StreamReader(filePath))
                {
                    js = read.ReadToEnd();
                    //Base64Decoding
                    //js = Lib.Base64Decoding(js);
                    js = StringEncryptDecrypt.Decrypt(js, FieldKeys.PassKey);
                    if (js.Length > 0)
                        list = JsonConvert.DeserializeObject<List<CompanyInfo>>(js);
                }


            }
            catch (Exception ex)
            {
                Lib.writerLog("CompanyInfo", "GetListCompanies", ex.Message, "error");

            }
            return list;           
        }

        public static List<CompanyInfo> Gets(Schedules schedule)
        {
            List<CompanyInfo> list = new List<CompanyInfo>();

            FilePath fp = new FilePath(FieldKeys.CompanyClass);

            //String filepath = @"D:\Web_Source\Web_SFH\Web_SFH\DATA\Company";
            list = CompanyInfo.Gets(fp.Folder);
            if (schedule.Type.Equals(1))
                list = list.FindAll(x => x.ConsortiumId.Equals(schedule.ConsortiumID.ToString()));
            else
                list = list.FindAll(x => x.CompanyID.Equals(Lib.get_value_int(schedule.CompanyID)));

            return list;
        }

        public static List<CompanyInfo> Gets(Schedules schedule, String[] ids)
        {
            List<CompanyInfo> list = new List<CompanyInfo>();

            FilePath fp = new FilePath(FieldKeys.CompanyClass);

            //String filepath = @"D:\Web_Source\Web_SFH\Web_SFH\DATA\Company";
            var list2 = CompanyInfo.Gets(fp.Folder);
            if (schedule.Type.Equals(1))
            {
                var list3 = list2.FindAll(x => x.ConsortiumId.Equals(schedule.ConsortiumID.ToString()));
                foreach (var com in list3)
                {
                    int position = Lib.get_value_int(ids[1]);
                    List<Specimen> donors = schedule.Selections[position].DonorSpecimenList.FindAll(x => x.Selected.Equals(true) || x.IsAlternate.Equals(true));
                    Boolean kq = donors.Exists(x => x.CompanyID.Equals(com.CompanyID.ToString()));
                    if (kq)
                        list.Add(com);

                }
            }
            else
            {
                list = list2.FindAll(x => x.CompanyID.Equals(Lib.get_value_int(schedule.CompanyID)));
            }
            return list;
        }
        /// <summary>
        /// Get Company list
        /// </summary>
        /// <param name="schedule">Schedules</param>
        /// <param name="ids">Array ID</param>
        /// <param name="type">0: Alternate donors 1: Selection donors 2: Both</param>
        /// <returns></returns>
        public static List<CompanyInfo> Gets(Schedules schedule, String[] ids, int type)
        {
            List<CompanyInfo> list = new List<CompanyInfo>();
            FilePath fp = new FilePath(FieldKeys.CompanyClass);
            //String filepath = @"D:\Web_Source\Web_SFH\Web_SFH\DATA\Company";
            var list2 = CompanyInfo.Gets(fp.Folder);
            if (schedule.Type.Equals(1))
            {
                var list3 = list2.FindAll(x => x.ConsortiumId.Equals(schedule.ConsortiumID.ToString()));
                foreach (var com in list3)
                {
                    int position = Lib.get_value_int(ids[1]);
                    List<Specimen> donors = schedule.Selections[position].DonorSpecimenList.FindAll(x => x.Selected.Equals(true) || x.IsAlternate.Equals(true));
                    switch (type)
                    {
                        case 0:
                            donors = schedule.Selections[position].DonorSpecimenList.FindAll(x => x.IsAlternate.Equals(true));
                            break;
                        case 1:
                            donors = schedule.Selections[position].DonorSpecimenList.FindAll(x => x.Selected.Equals(true));
                            break;
                    }

                    Boolean kq = donors.Exists(x => x.CompanyID.Equals(com.CompanyID.ToString()));
                    if (kq)
                        list.Add(com);

                }
            }
            else
            {
                list = list2.FindAll(x => x.CompanyID.Equals(Lib.get_value_int(schedule.CompanyID)));
            }
            return list;
        }

        public static CompanyInfo GetCompany(String id)
        {
            CompanyInfo com = new CompanyInfo();
            com.CompanyID = Lib.get_value_int(id);
            FilePath filePath = new FilePath(FieldKeys.CompanyClass);

            List<CompanyInfo> list = com.GetListCompanies(filePath.FilePathName);

            var kq = list.Exists(x => x.iCompanyID.Equals(Lib.get_value_int(id)));
            if (kq)
                com = list.Single(x => x.iCompanyID.Equals(Lib.get_value_int(id)));
            return com;
        }

        public static String EditChanged(HttpContext context)
        {
            String kq = String.Empty;
            CompanyInfo comNew = new CompanyInfo(context);

            //Get company exits
            CompanyInfo comOld = CompanyInfo.GetCompany(comNew.CompanyID.ToString());

            //kq = JsonConvert.SerializeObject(comOld) + " " + JsonConvert.SerializeObject(comNew);
            //Lib.writerLog("companyinfo", "Editchanged", kq, "error");
            //kq = "";
            kq = comOld.CompanyName + " ";
            if (!comNew.CompanyName.Equals(comOld.CompanyName))
                kq = Lib.ChangedValue("name", comOld.CompanyName, comNew.CompanyName);

            if (!comNew.Plan.Equals(comOld.Plan))
                kq += Lib.ChangedValue("Plan", comOld.Plan, comNew.Plan);

            if (!comNew.Bill.Equals(comOld.Bill))
                kq += Lib.ChangedValue("Bill", (comOld.Bill == 1 ? "checked" : "unchecked"), (comNew.Bill == 1 ? "checked" : "unchecked"));

            if (!comNew.ExpirationDate.Equals(comOld.ExpirationDate))
                kq += Lib.ChangedValue("Expiration date", comOld.ExpirationDate, comNew.ExpirationDate);


            if (!comNew.PersonalInfo.Person.FirstName.Equals(comOld.PersonalInfo.Person.FirstName))
                kq += Lib.ChangedValue("First name", comOld.PersonalInfo.Person.FirstName, comNew.PersonalInfo.Person.FirstName);

            if (!comNew.PersonalInfo.Person.LastName.Equals(comOld.PersonalInfo.Person.LastName))
                kq += Lib.ChangedValue("Last name", comOld.PersonalInfo.Person.LastName, comNew.PersonalInfo.Person.LastName);

            if (!comNew.PersonalInfo.Person.Title.Equals(comOld.PersonalInfo.Person.Title))
                kq += Lib.ChangedValue("DER title", comOld.PersonalInfo.Person.Title, comNew.PersonalInfo.Person.Title);

            if (!comNew.PersonalInfo.Person.Gender.Equals(comOld.PersonalInfo.Person.Gender))
                kq += Lib.ChangedValue("DER Gender", comOld.PersonalInfo.Person.Gender, comNew.PersonalInfo.Person.Gender);

            if (!comNew.PersonalInfo.Contact.MobilePhone.Equals(comOld.PersonalInfo.Contact.MobilePhone))
                kq += Lib.ChangedValue("Mobile phone", comOld.PersonalInfo.Contact.MobilePhone, comNew.PersonalInfo.Contact.MobilePhone);

            if (!comNew.PersonalInfo.Contact.HomePhone.Equals(comOld.PersonalInfo.Contact.HomePhone))
                kq += Lib.ChangedValue("Home phone", comOld.PersonalInfo.Contact.HomePhone, comNew.PersonalInfo.Contact.HomePhone);

            if (!comNew.PersonalInfo.Contact.WorkPhone.Equals(comOld.PersonalInfo.Contact.WorkPhone))
                kq += Lib.ChangedValue("Work phone", comOld.PersonalInfo.Contact.WorkPhone, comNew.PersonalInfo.Contact.WorkPhone);

            if (!comNew.PersonalInfo.Contact.Email.Equals(comOld.PersonalInfo.Contact.Email))
                kq += Lib.ChangedValue("Email", comOld.PersonalInfo.Contact.Email, comNew.PersonalInfo.Contact.Email);

            if (!comNew.PersonalInfo.Contact.Website.Equals(comOld.PersonalInfo.Contact.Website))
                kq += Lib.ChangedValue("Website", comOld.PersonalInfo.Contact.Website, comNew.PersonalInfo.Contact.Website);

            if (!comNew.PersonalInfo.Address.Address.Equals(comOld.PersonalInfo.Address.Address))
                kq += Lib.ChangedValue("Address", comOld.PersonalInfo.Address.Address, comNew.PersonalInfo.Address.Address);

            if (!comNew.PersonalInfo.Address.State.Equals(comOld.PersonalInfo.Address.State))
                kq += Lib.ChangedValue("State", comOld.PersonalInfo.Address.State, comNew.PersonalInfo.Address.State);

            if (!comNew.PersonalInfo.Address.City.Equals(comOld.PersonalInfo.Address.City))
                kq += Lib.ChangedValue("City", comOld.PersonalInfo.Address.City, comNew.PersonalInfo.Address.City);

            if (!comNew.PersonalInfo.Address.Zip.Equals(comOld.PersonalInfo.Address.Zip))
                kq += Lib.ChangedValue("Zip", comOld.PersonalInfo.Address.Zip.ToString(), comNew.PersonalInfo.Address.Zip.ToString());

            if (!comNew.PersonalInfo.Address.Country.Equals(comOld.PersonalInfo.Address.Country))
                kq += Lib.ChangedValue("Country", comOld.PersonalInfo.Address.Country, comNew.PersonalInfo.Address.Country);

            if (!comNew.PersonalInfo.Address.OfficeLocation.Equals(comOld.PersonalInfo.Address.OfficeLocation))
                kq += Lib.ChangedValue("Office location", comOld.PersonalInfo.Address.OfficeLocation, comNew.PersonalInfo.Address.OfficeLocation);

            return kq;
        }

        public static void Imports(ref String error, String csv, String userID,String username)
        {
            if (!String.IsNullOrEmpty(csv))
            {
                String[] array = csv.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                if (array.Length > 0)
                {
                    //Lib.writerLog("CompanyInfo", "Imports", String.Join(",", array), "error");
                    int i = 0;
                    foreach (String c in array)
                    {                        
                        if (i > 0)
                        {
                            String[] s = c.Split(',');
                            if (s.Length > 20)
                            {
                                CompanyInfo com = new CompanyInfo(s);
                                com.WriteCompany(ref error, com);                                

                                Activities activity = new Activities();
                                activity.ID = Lib.get_value_str(userID);
                                activity.UserName = Lib.get_value_str(username);
                                activity.Type = 1;
                                activity.Action = "ADD";
                                activity.Details = com.CompanyName;
                                if (error.ToUpper().Equals("SUCCESS"))
                                    activity.Write(ref error, activity);
                            }

                        }

                        i++;

                    }
                }

            }
        }
       
        public static void CreatedSample(ref String error,String ID, String consortiumName)
        {
            FilePath fp = new FilePath(FieldKeys.ImportClass);
            String filePath = fp.Folder + "CompanySample.csv";
            String csv = String.Empty;
            String value = String.Empty;
            if (!Directory.Exists(fp.Folder))
            {
                Directory.CreateDirectory(fp.Folder);
            }

            String[] coms = new string[21];
            coms[0] = "CompanyName";
            coms[1] = "ConsortiumID";
            coms[2] = "Plan";
            coms[3] = "Bill";
            coms[4] = "Expiration Date";
            coms[5] = "First name";
            coms[6] = "Last name";
            coms[7] = "Title";
            coms[8] = "Gender";
            coms[9] = "DateOfIncorporation";
            coms[10] = "Mobile phone";
            coms[11] = "Home phone";
            coms[12] = "Work phone";
            coms[13] = "Email";
            coms[14] = "Website";
            coms[15] = "Address";
            coms[16] = "State";
            coms[17] = "City";
            coms[18] = "Zip";
            coms[19] = "Country";
            coms[20] = "Office Location";
            csv = String.Join(",", coms);

            var list = CompanyInfo.Gets();
            if (!consortiumName.Equals("0"))
                list = list.FindAll(x => x.ConsortiumId.Equals(ID));
            if(list.Count > 0)
            {
                csv += Environment.NewLine;
                foreach(var com in list)
                {
                    coms = new string[21];
                    coms[0] = com.CompanyName;
                    value = com.ConsortiumId;
                    coms[1] = value.Equals("0") ? String.Empty : value;
                    coms[2] = com.Plan;
                    coms[3] = com.Bill.ToString();
                    coms[4] = com.ExpirationDate;
                    
                    coms[5] = com.PersonalInfo.Person.FirstName;
                    coms[6] = com.PersonalInfo.Person.LastName;
                    coms[7] = com.PersonalInfo.Person.Title;
                    coms[8] = com.PersonalInfo.Person.Gender;
                    coms[9] = String.Empty;
                    coms[10] = com.PersonalInfo.Contact.MobilePhone;

                    value = com.PersonalInfo.Contact.HomePhone;
                    coms[11] = value.Equals("0") ? String.Empty : value;

                    value = com.PersonalInfo.Contact.WorkPhone;
                    coms[12] = value.Equals("0") ? String.Empty : value;

                    value = com.PersonalInfo.Contact.Email;
                    coms[13] = value.Equals("0") ? String.Empty : value;

                    value= com.personalInfo.Contact.Website;
                    coms[14] = value.Equals("0") ? String.Empty : value;

                    value= com.PersonalInfo.Address.Address;
                    coms[15] = value.Equals("0") ? String.Empty : value;

                    value = com.PersonalInfo.Address.State;
                    coms[16] = value.Equals("0") ? String.Empty : value;

                    value = com.PersonalInfo.Address.City;
                    coms[17] = value.Equals("0") ? String.Empty : value;

                    value = com.PersonalInfo.Address.Zip.ToString();
                    coms[18] = value.Equals("0") ? String.Empty : value;

                    value = com.PersonalInfo.Address.Country;
                    coms[19] = value.Equals("0") ? String.Empty : value;

                    value = com.PersonalInfo.Address.OfficeLocation;
                    coms[20] = value.Equals("0") ? String.Empty : value;

                    csv += String.Join(",", coms);
                    csv += Environment.NewLine;
                }
                
            }
            
            File.WriteAllText(filePath, csv);
            error = @"../Data/Imports/CompanySample.csv";
        }
        
        #endregion End Methods
    }
}