using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.IO;

namespace HTT
{
    public class People
    {

        #region Fields

        private readonly DateTime d = DateTime.Now;

        private String sID = "0";
        private Driver driver;
        private PersonalInfo personalInfo;
        private DateTime sCreatedDate;       

        #endregion End Fields

        #region Constructors
        public People() { }

        public People(HttpContext context)
        {
            HttpRequest request = context.Request;

            String id = Lib.get_value_str(request[FieldKeys.ID]);
            id = id.Replace(" ", "");
            this.sID = id;
            this.driver = new Driver(context);
            this.PersonalInfo = new PersonalInfo(context);
            String sCmd = Lib.get_value_str(request[FieldKeys.Cmd]);
            this.sCreatedDate = d;
        }

        public People(String[] csv)
        {
            String id = Lib.get_value_str(csv[0]);
            id = id.Replace(" ", "");
            this.sID = id;
            this.driver = new Driver(csv);
            this.PersonalInfo = new PersonalInfo(csv);            
            this.sCreatedDate = d;
        }


        public string ID { get => sID; set => sID = value; }
        public Driver Driver
        {
            get
            {
                return driver;
            }

            set
            {
                driver = value;
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

        #endregion End Constructors

        #region Methods
        public void Created(ref String error, People people)
        {
            this.Write(ref error, people);
        }

        public void Modify(ref String error, People people)
        {
            this.Write(ref error, people);
        }

        public void Post(ref String error, HttpContext context)
        {
            error = "Success";

            String cmd = Lib.get_value_str(context.Request[FieldKeys.Cmd]);
            var people = new People(context);
            people.ID = people.Driver.PrimaryID.Replace(" ", "");
            this.Created(ref error, people);
        }

        public void Write(ref String error, People people)
        {
            String primanyID = people.sID.Replace(" ", "");

            FilePath filePath = new FilePath(FieldKeys.PeopleClass);

            String folder = filePath.Folder;

            String filename = folder + @"\" + primanyID + ".json";

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
                //DirectoryInfo di = Directory.CreateDirectory(folder);
                // di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }

            if (!String.IsNullOrEmpty(primanyID) && !primanyID.Equals("0"))
            {
                //driver.sID = primanyID;

                String data = JsonConvert.SerializeObject(people);
                //Lib.writerLog("People", "write", data, "error");
                //Encrypt file
                data = StringEncryptDecrypt.Encrypt(data, FieldKeys.PassKey);
                //Write a file json
                Lib.WriteFileJson(ref error, filename, data);
            }
            else
                error = "PrimanyID is required.";


        }

        public static People Get(String id)
        {
            People people = new People();
            try
            {
                String js = String.Empty;
                FilePath fp = new FilePath(FieldKeys.PeopleClass);
                String folder = fp.Folder;
                String filePath = folder + @"\" + id.Replace(" ", "") + ".json";

                js = File.ReadAllText(filePath);

                js = StringEncryptDecrypt.Decrypt(js, FieldKeys.PassKey);

                if (js.Length > 0)
                {
                    people = JsonConvert.DeserializeObject<People>(js);
                }

            }
            catch (Exception ex)
            {
                Lib.writerLog("People", "Get By ID", ex.Message, "error");

            }
            return people;
        }

        public List<People> Gets()
        {
            List<People> Peoples = new List<People>();

            People people = new People();
            try
            {

                String js = String.Empty;
                FilePath filePath = new FilePath(FieldKeys.PeopleClass);

                String folder = filePath.Folder;

                var files = Directory.GetFiles(folder).Select(x => Path.GetFileName(x));

                foreach (var file in files)
                {
                    String filename = folder + @"\" + file;

                    js = File.ReadAllText(filename);

                    js = StringEncryptDecrypt.Decrypt(js, FieldKeys.PassKey);
                    if (js.Length > 0)
                    {
                        people = JsonConvert.DeserializeObject<People>(js);
                        Peoples.Add(people);
                    }

                }

            }
            catch (Exception ex)
            {
                Lib.writerLog("People", "Gets", ex.Message, "error");

            }
            return Peoples;
        }
        
        public List<People> Gets(String byIDs)
        {
            List<People> list = new List<People>();
            String[] IDs = Lib.get_value_str(byIDs).Split(',', '-');
            foreach (String s in IDs)
            {
                People people = People.Get(s);
                list.Add(people);
            }

            return list;
        }

        public static List<People> PeopleGets(String folder)
        {
            List<People> list = new List<People>();
            People people = new People();

            //var folder = @"D:\Web_Source\Web_SFH\Web_SFH\DATA\People\";
            var files = Directory.GetFiles(folder).Select(x => Path.GetFileName(x));
            String js = String.Empty;
            foreach (var file in files)
            {
                String filename = folder + @"\" + file;

                js = File.ReadAllText(filename);

                js = StringEncryptDecrypt.Decrypt(js, FieldKeys.PassKey);
                if (js.Length > 0)
                {
                    people = JsonConvert.DeserializeObject<People>(js);
                    list.Add(people);
                }

            }

            return list;
        }
        public static String EditChanged(HttpContext context)
        {
            String kq = String.Empty;
            People New = new People(context);
            String primanryID = New.ID;
            HttpRequest request = context.Request;
            String cmd = Lib.get_value_str(request[FieldKeys.Cmd]);
            if(cmd.ToUpper().Equals("M"))
            {
                string[] array = New.ID.Split('_');
                if (array.Length.Equals(2))
                    primanryID = array[1];
            }

            //Get company exits
            var Old = People.Get(primanryID);

            //kq = JsonConvert.SerializeObject(comOld) + " " + JsonConvert.SerializeObject(comNew);
            //Lib.writerLog("companyinfo", "Editchanged", kq, "error");
            //kq = "";
            kq = Old.PersonalInfo.Person.LastName + " " + Old.PersonalInfo.Person.FirstName + " ";

            if (!New.Driver.PrimaryID.Equals(Old.Driver.PrimaryID))
                kq += Lib.ChangedValue("PrimaryID", Old.Driver.PrimaryID, New.Driver.PrimaryID);

            if(!New.Driver.Mode.Equals(Old.Driver.Mode))
                kq += Lib.ChangedValue("Mode", Old.Driver.Mode, New.Driver.Mode);

            if (!New.Driver.Category.Equals(Old.Driver.Category))
                kq += Lib.ChangedValue("Category", Old.Driver.Category, New.Driver.Category);

            if (!New.Driver.PrimaryIDExpirationDate.Equals(Old.Driver.PrimaryIDExpirationDate))
                kq += Lib.ChangedValue("Primary Expiration Date", Old.Driver.PrimaryIDExpirationDate, New.Driver.PrimaryIDExpirationDate);

            if (!New.PersonalInfo.Person.FirstName.Equals(Old.PersonalInfo.Person.FirstName))
                kq += Lib.ChangedValue("First name", Old.PersonalInfo.Person.FirstName, New.PersonalInfo.Person.FirstName);

            if (!New.PersonalInfo.Person.LastName.Equals(Old.PersonalInfo.Person.LastName))
                kq += Lib.ChangedValue("Last name", Old.PersonalInfo.Person.LastName, New.PersonalInfo.Person.LastName);

            if (!New.PersonalInfo.Person.Title.Equals(Old.PersonalInfo.Person.Title))
                kq += Lib.ChangedValue("DER title", Old.PersonalInfo.Person.Title, New.PersonalInfo.Person.Title);

            if (!New.PersonalInfo.Person.Gender.Equals(Old.PersonalInfo.Person.Gender))
                kq += Lib.ChangedValue("DER Gender", Old.PersonalInfo.Person.Gender, New.PersonalInfo.Person.Gender);

            if (!New.PersonalInfo.Contact.MobilePhone.Equals(Old.PersonalInfo.Contact.MobilePhone))
                kq += Lib.ChangedValue("Mobile phone", Old.PersonalInfo.Contact.MobilePhone, New.PersonalInfo.Contact.MobilePhone);

            if (!New.PersonalInfo.Contact.HomePhone.Equals(Old.PersonalInfo.Contact.HomePhone))
                kq += Lib.ChangedValue("Home phone", Old.PersonalInfo.Contact.HomePhone, New.PersonalInfo.Contact.HomePhone);

            if (!New.PersonalInfo.Contact.WorkPhone.Equals(Old.PersonalInfo.Contact.WorkPhone))
                kq += Lib.ChangedValue("Work phone", Old.PersonalInfo.Contact.WorkPhone, New.PersonalInfo.Contact.WorkPhone);

            if (!New.PersonalInfo.Contact.Email.Equals(Old.PersonalInfo.Contact.Email))
                kq += Lib.ChangedValue("Email", Old.PersonalInfo.Contact.Email, New.PersonalInfo.Contact.Email);

            if (!New.PersonalInfo.Contact.Website.Equals(Old.PersonalInfo.Contact.Website))
                kq += Lib.ChangedValue("Website", Old.PersonalInfo.Contact.Website, New.PersonalInfo.Contact.Website);

            if (!New.PersonalInfo.Address.Address.Equals(Old.PersonalInfo.Address.Address))
                kq += Lib.ChangedValue("Address", Old.PersonalInfo.Address.Address, New.PersonalInfo.Address.Address);

            if (!New.PersonalInfo.Address.State.Equals(Old.PersonalInfo.Address.State))
                kq += Lib.ChangedValue("State", Old.PersonalInfo.Address.State, New.PersonalInfo.Address.State);

            if (!New.PersonalInfo.Address.City.Equals(Old.PersonalInfo.Address.City))
                kq += Lib.ChangedValue("City", Old.PersonalInfo.Address.City, New.PersonalInfo.Address.City);

            if (!New.PersonalInfo.Address.Zip.Equals(Old.PersonalInfo.Address.Zip))
                kq += Lib.ChangedValue("Zip", Old.PersonalInfo.Address.Zip.ToString(), New.PersonalInfo.Address.Zip.ToString());

            if (!New.PersonalInfo.Address.Country.Equals(Old.PersonalInfo.Address.Country))
                kq += Lib.ChangedValue("Country", Old.PersonalInfo.Address.Country, New.PersonalInfo.Address.Country);

            if (!New.PersonalInfo.Address.OfficeLocation.Equals(Old.PersonalInfo.Address.OfficeLocation))
                kq += Lib.ChangedValue("Office location", Old.PersonalInfo.Address.OfficeLocation, New.PersonalInfo.Address.OfficeLocation);

            return kq;
        }

        public static void Imports(ref String error, String csv, String userID, String username)
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
                                People people = new People(s);
                                people.Write(ref error, people);

                                DonorInfo donorInfo = new DonorInfo(s[1], people.ID);
                                donorInfo.Write(ref error, donorInfo);

                                Activities activity = new Activities();
                                activity.ID = Lib.get_value_str(userID);
                                activity.UserName = Lib.get_value_str(username);
                                activity.Type = 1;
                                activity.Action = "ADD";
                                activity.Details = people.PersonalInfo.Person.FirstName + " " + people.PersonalInfo.Person.LastName;
                                if (error.ToUpper().Equals("SUCCESS"))
                                    activity.Write(ref error, activity);
                            }

                        }

                        i++;

                    }
                }

            }
        }

        public static void CreatedSample(ref String error,String companyID,String consortiumID)
        {
            FilePath fp = new FilePath(FieldKeys.ImportClass);
            String filePath = fp.Folder + "PeopleSample.csv";
            String csv = String.Empty;
            String value = String.Empty;
            if (!Directory.Exists(fp.Folder))
            {
                Directory.CreateDirectory(fp.Folder);
            }

            String[] array = new string[21];
            array[0] = "PrimaryID";
            array[1] = "CompanyID";
            array[2] = "Mode";
            array[3] = "Category";
            array[4] = "Expiration Date";
            array[5] = "First name";
            array[6] = "Last name";
            array[7] = "Title";
            array[8] = "Gender";
            array[9] = "DateOfBirth";
            array[10] = "Mobile phone";
            array[11] = "Home phone";
            array[12] = "Work phone";
            array[13] = "Email";
            array[14] = "Website";
            array[15] = "Address";
            array[16] = "State";
            array[17] = "City";
            array[18] = "Zip";
            array[19] = "Country";
            array[20] = "Office Location";
            
            csv = String.Join(",", array);

            DonorInfo donorInfo = new DonorInfo();
            List<DonorInfo> list = new List<DonorInfo>();

            if (!consortiumID.Equals("0"))
            {
                var comList = CompanyInfo.Gets();
                comList = comList.FindAll(x => x.ConsortiumId.Equals(companyID));
                foreach (var com in comList)
                {
                    foreach (var donor in donorInfo.Gets())
                    {
                        if (donor.CompanyID.Equals(com.CompanyID.ToString()))
                            list.Add(donor);
                    }
                }

            }
            else            
                list = donorInfo.Gets().FindAll(x => x.CompanyID.Equals(companyID));




           // Lib.writerLog("People", "CreateSample", companyID + "/" + consortiumID + " data: " + JsonConvert.SerializeObject(list), "error");
            if (list.Count > 0)
            {
                csv += Environment.NewLine;
                foreach (var donor in list)
                {
                    var people = People.Get(donor.PrimaryID);
                    array = new string[21];
                    array[0] = donor.PrimaryID;
                    value = donor.CompanyID;
                    array[1] = value.Equals("0") ? String.Empty : value;
                    array[2] = people.Driver.Mode;
                    array[3] = people.Driver.Category;

                    value = people.Driver.PrimaryIDExpirationDate;
                    array[4] = value.Equals("0") ? String.Empty : value;

                    array[5] = people.PersonalInfo.Person.FirstName;
                    array[6] = people.PersonalInfo.Person.LastName;
                    array[7] = people.PersonalInfo.Person.Title;
                    array[8] = people.PersonalInfo.Person.Gender;
                    array[9] = people.PersonalInfo.Person.DateOfBirth;

                    array[10] = people.PersonalInfo.Contact.MobilePhone;

                    value = people.PersonalInfo.Contact.HomePhone;
                    array[11] = value.Equals("0") ? String.Empty : value;

                    value = people.PersonalInfo.Contact.WorkPhone;
                    array[12] = value.Equals("0") ? String.Empty : value;

                    value = people.PersonalInfo.Contact.Email;
                    array[13] = value.Equals("0") ? String.Empty : value;

                    value = people.personalInfo.Contact.Website;
                    array[14] = value.Equals("0") ? String.Empty : value;

                    value = people.PersonalInfo.Address.Address;
                    array[15] = value.Equals("0") ? String.Empty : value;

                    value = people.PersonalInfo.Address.State;
                    array[16] = value.Equals("0") ? String.Empty : value;

                    value = people.PersonalInfo.Address.City;
                    array[17] = value.Equals("0") ? String.Empty : value;

                    value = people.PersonalInfo.Address.Zip.ToString();
                    array[18] = value.Equals("0") ? String.Empty : value;

                    value = people.PersonalInfo.Address.Country;
                    array[19] = value.Equals("0") ? String.Empty : value;

                    value = people.PersonalInfo.Address.OfficeLocation;
                    array[20] = value.Equals("0") ? String.Empty : value;

                    csv += String.Join(",", array);
                    csv += Environment.NewLine;
                }

            }

            File.WriteAllText(filePath, csv);
            error = @"../Data/Imports/PeopleSample.csv";
        }

       
        #endregion End Methods
    }
}
