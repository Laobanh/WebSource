using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Web.SessionState;
using HTT;
using System.Web.Services;

namespace Web_SFH.Handlers
{
    /// <summary>
    /// Summary description for Handler_SignIn
    /// </summary>
    public class Handler_MCSA5876 : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            Mcsa5876 mcsa = new Mcsa5876();
            // page 1
            mcsa.FirstName = Lib.get_value_str(request["FirstName"]);
            mcsa.LastName = Lib.get_value_str(request["LastName"]);
            mcsa.Federal = Lib.get_value_str(request["Federal"]);
            mcsa.WearingCorrective = Lib.get_value_str(request["WearingCorrective"]);
            mcsa.Waiver = Lib.get_value_str(request["Waiver"]);
            mcsa.WaiverEnter = Lib.get_value_str(request["WaiverEnter"]);
            mcsa.DrivingWithin = Lib.get_value_str(request["DrivingWithin"]);
            mcsa.QualifiedBy = Lib.get_value_str(request["QualifiedBy"]);
            mcsa.GrandfatheredFrom = Lib.get_value_str(request["GrandfatheredFrom"]);
            mcsa.WearingHeading = Lib.get_value_str(request["WearingHeading"]);
            mcsa.SkillPerformance = Lib.get_value_str(request["SkillPerformance"]);
            mcsa.MedicalEcaminer = Lib.get_value_str(request["MedicalEcaminer"]);
            mcsa.ExaminerName = Lib.get_value_str(request["ExaminerName"]);
            mcsa.ExaminerState = Lib.get_value_str(request["ExaminerState"]);
            mcsa.ExaminerTelephone = Lib.get_value_str(request["ExaminerTelephone"]);
            mcsa.ExaminerTelephone = Lib.get_value_str(request["ExaminerTelephone"]);
            mcsa.MedicalExaminerInfomation1 = Lib.get_value_str(request["MedicalExaminerInfomation1"]);
            mcsa.MedicalExaminerInfomation2 = Lib.get_value_str(request["MedicalExaminerInfomation2"]);
            mcsa.OtherPractitioner = Lib.get_value_str(request["OtherPractitioner"]);
            mcsa.IssuingState = Lib.get_value_str(request["IssuingState"]);
            mcsa.NationalRegistryNumber = Lib.get_value_str(request["NationalRegistryNumber"]);
            mcsa.StreetAddress = Lib.get_value_str(request["StreetAddress"]);
            mcsa.DriverLicenseNumber = Lib.get_value_str(request["DriverLicenseNumber"]);
            mcsa.IssuingStateProvince = Lib.get_value_str(request["IssuingStateProvince"]);
            mcsa.City = Lib.get_value_str(request["City"]);
            mcsa.StateProvince = Lib.get_value_str(request["StateProvince"]);
            mcsa.ZipCode = Lib.get_value_str(request["ZipCode"]);
            mcsa.CLPCDL = Lib.get_value_str(request["CLPCDL"]);
            mcsa.MedNumber = Lib.get_value_str(request["examNumber"]);
            mcsa.DateCertificate = Lib.get_value_str(request["DateCertificate"]);
            string error = string.Empty;
            var result = new ResultInfo("Fialed", "error", "", 0);
            mcsa.SaveData(ref error, mcsa);
            result = new ResultInfo("Success", "OK", "", mcsa);
            response.Write(JsonConvert.SerializeObject(result));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}