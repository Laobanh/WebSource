using System;
using System.Web;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HTT
{
    public class Mcsa5876
    {

      
        public Mcsa5876()
        {

        }

        public string LastName { get ; set; }
        public string FirstName { get ; set; }
        public string Federal { get ; set; }
        public string WearingCorrective { get ; set; }
        public string WearingHeading { get ; set; }
        public string Waiver { get ; set; }
        public string WaiverEnter { get ; set; }
        public string DrivingWithin { get ; set; }
        public string SkillPerformance { get ; set; }
        public string GrandfatheredFrom { get ; set; }
        public string MedicalEcaminer { get ; set; }
        public string ExaminerTelephone { get ; set; }
        public string DateCertificate { get ; set; }
        public string ExaminerName { get ; set; }
        public string MedicalExaminerInfomation1 { get ; set; }
        public string MedicalExaminerInfomation2 { get ; set; }
        public string ExaminerState { get ; set; }
        public string IssuingState { get ; set; }
        public string NationalRegistryNumber { get ; set; }
        public string DriverLicenseNumber { get ; set; }
        public string IssuingStateProvince { get ; set; }
        public string StreetAddress { get ; set; }
        public string City { get ; set; }
        public string StateProvince { get ; set; }
        public string ZipCode { get ; set; }
        public string CLPCDL { get ; set; }
        public string OtherPractitioner { get; set; }
        public string MedNumber { get; set; }
        public string Ids { get; set; }
        public string QualifiedBy { get; set; }
        public void SaveData(ref String error, Mcsa5876 mcsa)
        {
            try
            {

                FilePath fp = new FilePath(FieldKeys.Mcsa5876Class);
                Mcsa5876 mcsa5876 = Get(mcsa.MedNumber);
                // update 
                if (mcsa5876?.MedNumber?.Trim().ToLower() == mcsa.MedNumber?.Trim().ToLower())
                {
                    mcsa.Ids = mcsa5876.Ids;
                    mcsa5876 = mcsa;
                    var js = JsonConvert.SerializeObject(mcsa5876);

                    js = StringEncryptDecrypt.Encrypt(js, FieldKeys.Mcsa5876Class);
                    String folder = fp.Folder;
                    String fileName = folder + mcsa.Ids + ".json";

                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }

                    Lib.WriteFileJson(ref error, fileName, js);
                }
                // insert 
                else
                {
                    mcsa.Ids = Guid.NewGuid().ToString();
                    var js = JsonConvert.SerializeObject(mcsa);
                    js = StringEncryptDecrypt.Encrypt(js, FieldKeys.Mcsa5876Class);
                    String folder = fp.Folder;
                    String fileName = folder + mcsa.Ids + ".json";

                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }

                    Lib.WriteFileJson(ref error, fileName, js);
                }
            }
            catch (Exception ex)
            {
                Lib.writerLog("MCSA5876", "SaveData", ex.Message, "error");
            }

        }

        public string GetMcsaMedNumber(String medNumber)
        {
            string id = string.Empty;
            try
            {
                String js = String.Empty;
                FilePath filePath = new FilePath(FieldKeys.Mcsa5876Class);

                String folder = filePath.Folder;

                var files = Directory.GetFiles(folder).Select(x => Path.GetFileName(x));

                foreach (var file in files)
                {
                    String filename = folder + @"\" + file;

                    js = File.ReadAllText(filename);

                    js = StringEncryptDecrypt.Decrypt(js, FieldKeys.Mcsa5876Class);
                    if (js.Length > 0)
                    {
                        var mcsa = JsonConvert.DeserializeObject<Mcsa5876>(js);
                        if (mcsa != null)
                        {
                            if (mcsa.MedNumber.Equals(medNumber))
                            {
                                return id = mcsa.Ids.ToString();
                            }
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                Lib.writerLog("DonorInfo", "Gets", ex.Message, "error");

            }
            return id;
        }

        public Mcsa5876 Get(String id)
        {
            var fp = new FilePath("MCSA5876");
            string ids = GetMcsaMedNumber(id);

            if (string.IsNullOrEmpty(ids))
            {
                return null;
            }
            String path = fp.Folder + ids + ".json";
            var js = File.ReadAllText(path);
            js = StringEncryptDecrypt.Decrypt(js, FieldKeys.Mcsa5876Class);
            Mcsa5876 mcsa = JsonConvert.DeserializeObject<Mcsa5876>(js);
            mcsa.Ids = ids;
            return mcsa;
        }
    }
}

