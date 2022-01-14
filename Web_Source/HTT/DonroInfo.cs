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
    public class DonorInfo
    {
        //localhost/Handlers/Handler_CreateDonor.ashx?ID=1001&ListID=CAY3000798,CAY6238116,CAE1800340&Cmd=C
        //localhost/Handlers/Handler_ModifyDonor.ashx?cmd=m&ID=1002&PrimaryID=CAF2281979&NotActive=1&NotAvilabel=0&NotActiveReason=NO LONGER TO COMPANY&NotAvilabelReason&NotActiveDate=7/23/2021
        #region Fields
        private String sID = "0";
        private DateTime dDateCreate;
        private String sCompanyName = "";
        private String sDerFirstName = "", sDerLastName = "", sDerEmail = "", sDerMobilePhone = "";
        private String sPrimaryID = "", sFirstname = "", sLastname = "", sMode = "", sCategory = "", sMobilePhone = "", sDateOfBirth = "";
        private int iExcludeFromSelection = 0, iNotActive = 0, iNotAvilable = 0;
        private String sNotActiveDate = "0", sNotAvilableDate = "0", sNotActiveReason = "0", sNotAvilableReason = "0";
        private String sPeopleID = "0";
        private String sCompanyID = "";
        #endregion End fields

        #region Constructors
        public DonorInfo()
        {

        }

        public DonorInfo(String id, String primaryID)
        {
            String donorId = id + "_" + primaryID;
            this.sID = Lib.get_value_str(donorId).Replace(" ", "");
            this.sCompanyID = id;
            this.sPrimaryID = Lib.get_value_str(primaryID);
            this.dDateCreate = DateTime.Now;
        }

        public DonorInfo(HttpContext context)
        {
            HttpRequest request = context.Request;
            this.sID = Lib.get_value_str(request[FieldKeys.ID]);
            this.sCompanyID = Lib.get_value_str(request[FieldKeys.CompanyID]);
            this.sCompanyName = Lib.get_value_str(request[FieldKeys.CompanyName]);
            this.sPrimaryID = Lib.get_value_str(request[FieldKeys.PrimaryID]);
            this.iNotActive = Lib.get_value_int(request[FieldKeys.NotActive]);

            this.iNotAvilable = Lib.get_value_int(request[FieldKeys.NotAvilable]);
            this.iExcludeFromSelection = this.iNotActive | this.iNotAvilable;

            this.sNotActiveDate = Lib.get_value_str(request[FieldKeys.NotActiveDate]);
            this.sNotAvilableDate = Lib.get_value_str(request[FieldKeys.NotAvilableDate]);

            this.sNotActiveReason = Lib.get_value_str(request[FieldKeys.NotActiveReason]);
            this.sNotAvilableReason = Lib.get_value_str(request[FieldKeys.NotAvilableReason]);
            this.dDateCreate = DateTime.Now;        
        }


        public string ID { get => sID; set => sID = value; }
        public string CompanyID { get => sCompanyID; set => sCompanyID = value; }
        public string CompanyName { get => sCompanyName; set => sCompanyName = value; }
        public string PrimaryID { get => sPrimaryID; set => sPrimaryID = value; }
        public string Firstname { get => sFirstname; set => sFirstname = value; }
        public string Lastname { get => sLastname; set => sLastname = value; }
        public string Mode { get => sMode; set => sMode = value; }
        public string Category { get => sCategory; set => sCategory = value; }
        public int ExcludeFromSelection { get => iExcludeFromSelection; set => iExcludeFromSelection = value; }
        public int NotActive { get => iNotActive; set => iNotActive = value; }
        public int NotAvilable { get => iNotAvilable; set => iNotAvilable = value; }
        public string NotActiveDate { get => sNotActiveDate; set => sNotActiveDate = value; }
        public string NotAvilableDate { get => sNotAvilableDate; set => sNotAvilableDate = value; }
        public string NotActiveReason { get => sNotActiveReason; set => sNotActiveReason = value; }
        public string NotAvilableReason { get => sNotAvilableReason; set => sNotAvilableReason = value; }
        public DateTime DateCreate { get => dDateCreate; set => dDateCreate = value; }
        public string MobilePhone { get => sMobilePhone; set => sMobilePhone = value; }
        public string DateOfBirth { get => sDateOfBirth; set => sDateOfBirth = value; }
        public string DerFirstName { get => sDerFirstName; set => sDerFirstName = value; }
        public string DerLastName { get => sDerLastName; set => sDerLastName = value; }
        public string DerEmail { get => sDerEmail; set => sDerEmail = value; }
        public string DerMobilePhone { get => sDerMobilePhone; set => sDerMobilePhone = value; }
        public string PeopleID { get => sPeopleID; set => sPeopleID = value; }



        #endregion End Constructors

        #region Methods        

        public void Modified(ref String error, HttpContext context)
        {
            //Init List DonorInfo
            List<DonorInfo> donorInfos = new List<DonorInfo>();

            //Fields
            String data = String.Empty;

            String js = String.Empty;

            //init donorInfo new 
            DonorInfo donorInfo = new DonorInfo(context);

            string[] ids = donorInfo.ID.Split('_');

            donorInfo.CompanyID = ids[0];

            if (donorInfo.NotActive.Equals(0))
            {
                donorInfo.NotActiveReason = String.Empty;
                donorInfo.NotActiveDate = String.Empty;
            }

            if (donorInfo.NotAvilable.Equals(0))
            {
                donorInfo.NotAvilableReason = String.Empty;
                donorInfo.NotAvilableDate = String.Empty;
            }

            //Get file path
            FilePath filePath = new FilePath(FieldKeys.DonorClass);

            String folder = filePath.Folder;

            String fileName = folder + donorInfo.ID + ".json";

            //Convert to json string
            data = JsonConvert.SerializeObject(donorInfo);
            //Lib.writerLog("DonorInfo", "Modified", data, "error");
            //Encrypt file
            data = StringEncryptDecrypt.Encrypt(data, FieldKeys.PassKey);

            //Write to system
            Lib.WriteFileJson(ref error, fileName, data);

        }

        public void Modified2(ref String error, HttpContext context)
        {
            //Init List DonorInfo
            List<DonorInfo> donorInfos = new List<DonorInfo>();

            //Fields
            String data = String.Empty;

            String js = String.Empty;

            //init donorInfo new 
            DonorInfo donorInfo = new DonorInfo(context);

            //Get file path
            FilePath filePath = new FilePath(FieldKeys.DonorClass);

            String folder = filePath.Folder;

            String fileName = folder + donorInfo.ID + ".json";


            if (File.Exists(fileName))//File exists system
            {
                //Read file by filepath
                js = File.ReadAllText(fileName);

                //Decrypt file to json string
                js = StringEncryptDecrypt.Decrypt(js, FieldKeys.PassKey);

                //convert to DonorInfo List
                List<DonorInfo> donorInfoLocal = JsonConvert.DeserializeObject<List<DonorInfo>>(js);


                int i = 0;
                foreach (DonorInfo donor in donorInfoLocal)
                {

                    //DonorList ID
                    string s = donorInfo.PrimaryID;

                    //Check Donor ID exists in file Company
                    if (donor.PrimaryID.Equals(s))
                    {
                        //Set Primary ID;
                        donorInfoLocal[i].NotActive = donorInfo.NotActive;
                        donorInfoLocal[i].NotAvilable = donorInfo.NotAvilable;

                        donorInfoLocal[i].NotActiveDate = donorInfo.NotActive.Equals(0) ? "" : donorInfo.NotActiveDate;
                        donorInfoLocal[i].NotAvilableDate = donorInfo.NotAvilable.Equals(0) ? "" : donorInfo.NotAvilableDate;

                        donorInfoLocal[i].NotActiveReason = donorInfo.NotActive.Equals(0) ? "" : donorInfo.NotActiveReason;
                        donorInfoLocal[i].NotAvilableReason = donorInfo.NotAvilable.Equals(0) ? "" : donorInfo.NotAvilableReason;

                        donorInfoLocal[i].ExcludeFromSelection = donorInfo.ExcludeFromSelection;
                    }

                    i++;
                }

                //Convert to json string
                data = JsonConvert.SerializeObject(donorInfoLocal);
                //Lib.writerLog("DonorInfo", "Modified", data, "error");
                //Encrypt file
                data = StringEncryptDecrypt.Encrypt(data, FieldKeys.PassKey);

                //Write to system
                Lib.WriteFileJson(ref error, fileName, data);
            }
        }

        public void Created(ref String error, HttpContext context)
        {
            this.Write(ref error, context);
        }

        public void Post(ref String error, HttpContext context)
        {
            error = "Success";

            DonorInfo donorInfo = new DonorInfo(context);

            String cmd = Lib.get_value_str(context.Request[FieldKeys.Cmd]);
            cmd = cmd.ToUpper();
            if (cmd.Equals("C"))
                this.Created(ref error, context);
            else if (cmd.Equals("M"))
                this.Modified(ref error, context);            
        }

        public void Write(ref String error, HttpContext context)
        {
            
            //Init List DonorInfo
            List<DonorInfo> donorInfos = new List<DonorInfo>();

            //Fields
            String data = String.Empty;

            String js = String.Empty;

            HttpRequest request = context.Request;

            String[] listID = Lib.get_value_str(request[FieldKeys.ListID]).Split(',');

            //Get file path
            FilePath filePath = new FilePath(FieldKeys.DonorClass);

            String folder = filePath.Folder;

            String fileName = folder + ID + ".json";

            //Check folder exist
            if (!Directory.Exists(folder))
            {
                //Create Folder
                //DirectoryInfo di = Directory.CreateDirectory(folder);
                // di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                Directory.CreateDirectory(folder);
            }

            //Check file exists
            if (!File.Exists(fileName)) //File not exists system
            {
                foreach (string s in listID)
                {
                    //init donorInfo new 
                    DonorInfo donorInfo = new DonorInfo(context);
                    //Set Primary ID;
                    donorInfo.sPrimaryID = s;
                    //Add donorInfo new to donorInfo old
                    donorInfos.Add(donorInfo);

                }
                //convert to json string
                data = JsonConvert.SerializeObject(donorInfos);
                //Encrypt file
                data = StringEncryptDecrypt.Encrypt(data, FieldKeys.PassKey);
                //Write file to system
                Lib.WriteFileJson(ref error, fileName, data);

            }
            else//File exists system
            {
                //Read file by filepath
                js = File.ReadAllText(fileName);

                //Decrypt file to json string
                js = StringEncryptDecrypt.Decrypt(js, FieldKeys.PassKey);

                //convert to DonorInfo List
                List<DonorInfo> donorInfoLocal = JsonConvert.DeserializeObject<List<DonorInfo>>(js);

                //Loop DonorList ID
                foreach (string s in listID)
                {
                    //Check Donor ID exists in file Company
                    if (!donorInfoLocal.Exists(x => x.sPrimaryID.Equals(s)))
                    {
                        //init donorInfo new 
                        DonorInfo donorInfo = new DonorInfo(context);
                        //Set Primary ID;
                        donorInfo.sPrimaryID = s;
                        //Add donorInfo new to donorInfo old
                        donorInfoLocal.Add(donorInfo);
                    }
                }
                //Convert to json string
                data = JsonConvert.SerializeObject(donorInfoLocal);

                //Encrypt file
                data = StringEncryptDecrypt.Encrypt(data, FieldKeys.PassKey);

                //Write to system
                Lib.WriteFileJson(ref error, fileName, data);
            }

        }

        public void Write(ref String error, DonorInfo donor)
        {
            //Fields
            String data = String.Empty;

            String js = String.Empty;

            //Get file path
            FilePath filePath = new FilePath(FieldKeys.DonorClass);

            String folder = filePath.Folder;           

            String fileName = folder + donor.ID + ".json";

            //Check folder exist
            if (!Directory.Exists(folder))
            {
                //Create Folder               
                Directory.CreateDirectory(folder);
            }

            //convert to json string
            data = JsonConvert.SerializeObject(donor);
            //Encrypt file
            data = StringEncryptDecrypt.Encrypt(data, FieldKeys.PassKey);
            //Write file to system            
            Lib.WriteFileJson(ref error, fileName, data);

        }

        public void Write2(ref String error, DonorInfo donor)
        {

            //Init List DonorInfo
            List<DonorInfo> donorInfos = new List<DonorInfo>();

            //Fields
            String data = String.Empty;

            String js = String.Empty;

            //Get file path
            FilePath filePath = new FilePath(FieldKeys.DonorClass);

            String folder = filePath.Folder;

            //String id = donor.ID + "_" + donor.PrimaryID;

            String fileName = folder + donor.ID + ".json";

            //Check folder exist
            if (!Directory.Exists(folder))
            {
                //Create Folder
                //DirectoryInfo di = Directory.CreateDirectory(folder);
                // di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                Directory.CreateDirectory(folder);
            }

            //Check file exists
            if (!File.Exists(fileName)) //File not exists system
            {
                //Add donorInfo new to donorInfo old
                donorInfos.Add(donor);

                //convert to json string
                data = JsonConvert.SerializeObject(donorInfos);
                //Encrypt file
                data = StringEncryptDecrypt.Encrypt(data, FieldKeys.PassKey);
                //Write file to system
                //Lib.WriteFileJson(ref error, fileName, data);
            }
            /*
            else//File exists system
            {
                //Read file by filepath
                js = File.ReadAllText(fileName);

                //Decrypt file to json string
                js = StringEncryptDecrypt.Decrypt(js, FieldKeys.PassKey);

                //convert to DonorInfo List
                List<DonorInfo> donorInfoLocal = JsonConvert.DeserializeObject<List<DonorInfo>>(js);


                //Check Donor ID exists in file Company
                if (!donorInfoLocal.Exists(x => x.ID.Equals(donor.ID)))
                {
                    //Add donorInfo new to donorInfo old
                    donorInfoLocal.Add(donor);
                }

                //Convert to json string
                data = JsonConvert.SerializeObject(donorInfoLocal);

                //Encrypt file
                data = StringEncryptDecrypt.Encrypt(data, FieldKeys.PassKey);               
            }
            */
            //Write to system
            Lib.WriteFileJson(ref error, fileName, data);

        }

        public void Remove(ref String error,List<DonorInfo> list,DonorInfo donor, String CompanyID)
        {
            Boolean kq = list.Exists(x=>x.PrimaryID.Equals(donor.PrimaryID));
            if (kq)
            {
                list.Remove(donor);
                Lib.writerLog("ChangedCopyPersonnal", "ProcessRequest", JsonConvert.SerializeObject(list), "error");
                //Get file path
                FilePath fp = new FilePath(FieldKeys.DonorClass);
                //Get file name
                String filename = fp.Folder + CompanyID + ".json";
                //Convert object to string
                String js = JsonConvert.SerializeObject(list);
                //Encrypt string
                js = StringEncryptDecrypt.Encrypt(js, FieldKeys.PassKey);
                //Write to file json
                Lib.WriteFileJson(ref error, filename, js);
            }

        }

        public void Remove(String id)
        {
            //Get file path
            FilePath fp = new FilePath(FieldKeys.DonorClass);
            //Get file name
            String filename = fp.Folder + id + ".json";

            File.Delete(filename);
        }
        public List<DonorInfo> Gets2(String id)
        {            
            //Init donorinfo list
            List<DonorInfo> list = new List<DonorInfo>();
            try
            {
                if (!String.IsNullOrEmpty(id) && !id.Equals("0"))
                {
                    //Init DonorInfo Get to file 
                    List<DonorInfo> donors = new List<DonorInfo>();

                    //Get file path
                    FilePath filePath = new FilePath(FieldKeys.DonorClass);
                    //Get folder
                    string folder = filePath.Folder;
                    //Get file name to folder
                    //var files = Directory.GetFiles(folder).Select(x => Path.GetFileName(x));

                    String file = folder + Lib.get_value_str(id) + ".json";
                    //foreach (var file in files)
                    //{
                    //Read file to file path
                    String js = File.ReadAllText(file);

                    //Decrypt file to sjon string
                    js = StringEncryptDecrypt.Decrypt(js, FieldKeys.PassKey);
                    //Lib.writerLog("DonorInfo", "Gets Decrypt", js, "error");

                    donors = JsonConvert.DeserializeObject<List<DonorInfo>>(js);

                    foreach (DonorInfo donor in donors)
                    {
                        //Add in donorinfo list
                        list.Add(donor);
                    }

                    // }
                }
            }
            catch { }
            return list;
        }

        public List<DonorInfo> Gets()
        {
            List<DonorInfo> list = new List<DonorInfo>();

            DonorInfo donor = new DonorInfo();
            try
            {

                String js = String.Empty;
                FilePath filePath = new FilePath(FieldKeys.DonorClass);

                String folder = filePath.Folder;

                var files = Directory.GetFiles(folder).Select(x => Path.GetFileName(x));

                foreach (var file in files)
                {
                    String filename = folder + @"\" + file;

                    js = File.ReadAllText(filename);

                    js = StringEncryptDecrypt.Decrypt(js, FieldKeys.PassKey);
                    if (js.Length > 0)
                    {
                        donor = JsonConvert.DeserializeObject<DonorInfo>(js);
                        list.Add(donor);
                    }

                }

            }
            catch (Exception ex)
            {
                Lib.writerLog("DonorInfo", "Gets", ex.Message, "error");

            }
            return list;
        }

        public static List<DonorInfo> GetsByFolder(String folder)
        {
            List<DonorInfo> list = new List<DonorInfo>();

            DonorInfo donor = new DonorInfo();
            try
            {

                String js = String.Empty;

                var files = Directory.GetFiles(folder).Select(x => Path.GetFileName(x));

                foreach (var file in files)
                {
                    String filename = folder + @"\" + file;

                    js = File.ReadAllText(filename);

                    js = StringEncryptDecrypt.Decrypt(js, FieldKeys.PassKey);
                    if (js.Length > 0)
                    {
                        donor = JsonConvert.DeserializeObject<DonorInfo>(js);
                        list.Add(donor);
                    }

                }

            }
            catch (Exception ex)
            {
                Lib.writerLog("DonorInfo", "Gets", ex.Message, "error");

            }
            return list;
        }

        public List<DonorInfo> Gets(String id)
        {
            List<DonorInfo> list = new List<DonorInfo>();

            DonorInfo donor = new DonorInfo();
            try
            {

                String js = String.Empty;
                FilePath filePath = new FilePath(FieldKeys.DonorClass);

                String folder = filePath.Folder;

                var files = Directory.GetFiles(folder).Select(x => Path.GetFileName(x));

                foreach (var file in files)
                {
                    String filename = folder + @"\" + file;

                    js = File.ReadAllText(filename);

                    js = StringEncryptDecrypt.Decrypt(js, FieldKeys.PassKey);
                    if (js.Length > 0)
                    {
                        donor = JsonConvert.DeserializeObject<DonorInfo>(js);
                        if (donor.CompanyID.Equals(id))
                            list.Add(donor);
                    }

                }

            }
            catch (Exception ex)
            {
                Lib.writerLog("DonorInfo", "Gets", ex.Message, "error");

            }
            return list;
        }

        public DonorInfo Get(String id)
        {

            DonorInfo donor = new DonorInfo();
            try
            {

                String js = String.Empty;
                FilePath filePath = new FilePath(FieldKeys.DonorClass);

                String folder = filePath.Folder;


                String filename = folder + @"\" + id + ".json";

                js = File.ReadAllText(filename);

                js = StringEncryptDecrypt.Decrypt(js, FieldKeys.PassKey);
                if (js.Length > 0)
                {
                    donor = JsonConvert.DeserializeObject<DonorInfo>(js);

                }
            }
            catch (Exception ex)
            {
                Lib.writerLog("DonorInfo", "Get", ex.Message, "error");

            }
            return donor;
        }
        public String EditChanged(HttpContext context)
        {
            String kq = String.Empty;
            return kq;
        }


       
        #endregion End Methods

    }

    
}