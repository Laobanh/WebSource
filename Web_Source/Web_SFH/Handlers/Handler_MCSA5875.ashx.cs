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
    public class Handler_MCSA5875 : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            Mcsa5875 mcsa = new Mcsa5875();
            // page 1
            mcsa.NameLast = Lib.get_value_str(request["NameLast"]);
            mcsa.NameFirst = Lib.get_value_str(request["NameFirst"]);
            mcsa.GenderButtons = Lib.get_value_str(request["GenderButtons"]);
            mcsa.BirthDate = Lib.get_value_str(request["BirthDate"]);
            mcsa.DriverAge = Lib.get_value_str(request["DriverAge"]);
            mcsa.DriverAddress = Lib.get_value_str(request["DriverAddress"]);
            mcsa.DriverCity = Lib.get_value_str(request["DriverCity"]);
            mcsa.DriverState = Lib.get_value_str(request["DriverState"]);
            mcsa.DriverZip = Lib.get_value_str(request["DriverZip"]);
            mcsa.DriverLicense = Lib.get_value_str(request["DriverLicense"]);
            mcsa.LicenseState = Lib.get_value_str(request["LicenseState"]);
            mcsa.DriverPhone = Lib.get_value_str(request["DriverPhone"]);
            mcsa.EmailAddress = Lib.get_value_str(request["EmailAddress"]);
            mcsa.DriverVerify = Lib.get_value_str(request["DriverVerify"]);
            mcsa.SurgeryButtons = Lib.get_value_str(request["SurgeryButtons"]);
            mcsa.SurgeryDescribe = Lib.get_value_str(request["SurgeryDescribe"]);
            mcsa.MedicineDescribe = Lib.get_value_str(request["MedicineDescribe"]);
            mcsa.MedicineButtons = Lib.get_value_str(request["MedicineButtons"]);
            mcsa.CertDenyButtons = Lib.get_value_str(request["CertDenyButtons"]);
            mcsa.CdlButtonList = Lib.get_value_str(request["CdlButtonList"]);
            mcsa.MedNumber = Lib.get_value_str(request["examNumber"]);
            mcsa.NameInitial = Lib.get_value_str(request["NameInitial"]);
            // end page 1

            //page 2
            mcsa.NameFirstHead2 = Lib.get_value_str(request["nameFirstHead2"]);
            mcsa.NameLastHead2 = Lib.get_value_str(request["nameLastHead2"]);
            mcsa.DateBirth2 = Lib.get_value_str(request["dateBirth2"]);
            mcsa.HeadButtons = Lib.get_value_str(request["r1"]);
            mcsa.SeizeButtons = Lib.get_value_str(request["r2"]);
            mcsa.EyeButtons = Lib.get_value_str(request["r3"]);
            mcsa.EarButtons = Lib.get_value_str(request["r4"]);
            mcsa.HeartButtons = Lib.get_value_str(request["r5"]);
            mcsa.PaceButtons = Lib.get_value_str(request["r6"]);
            mcsa.HighButtons = Lib.get_value_str(request["r7"]);
            mcsa.CholesterolButtons = Lib.get_value_str(request["r8"]);
            mcsa.BreathButtons = Lib.get_value_str(request["r9"]);
            mcsa.LungButtons = Lib.get_value_str(request["10"]);
            mcsa.KidneyButtons = Lib.get_value_str(request["11"]);
            mcsa.StomachButtons = Lib.get_value_str(request["12"]);
            mcsa.SugarButtons = Lib.get_value_str(request["13"]);
            mcsa.InsulinButtons = Lib.get_value_str(request["r13B"]);
            mcsa.MentalButtons = Lib.get_value_str(request["r14"]);
            mcsa.FaintButtons = Lib.get_value_str(request["r15"]);
            mcsa.DizzyButtons = Lib.get_value_str(request["r16"]);
            mcsa.WeightButtons = Lib.get_value_str(request["r17"]);
            mcsa.StrokeButtons = Lib.get_value_str(request["r18"]);
            mcsa.UselimitButtons = Lib.get_value_str(request["r19"]);
            mcsa.NeckbackButtons = Lib.get_value_str(request["r20"]);
            mcsa.BoneButtons = Lib.get_value_str(request["r21"]);
            mcsa.BloodButtons = Lib.get_value_str(request["r22"]);
            mcsa.CancerButtons = Lib.get_value_str(request["r23"]);
            mcsa.InfectButtons = Lib.get_value_str(request["r24"]);
            mcsa.SleepDisconer = Lib.get_value_str(request["r25"]);
            mcsa.SleeptestButtons = Lib.get_value_str(request["r26"]);
            mcsa.HospitalButtons = Lib.get_value_str(request["r27"]);
            mcsa.BrokenButtons = Lib.get_value_str(request["r28"]);
            mcsa.TobaccoButtons = Lib.get_value_str(request["r29"]);
            mcsa.AlcoholButtons = Lib.get_value_str(request["r30"]);
            mcsa.OtherDescribe = Lib.get_value_str(request["otherDescribe"]);
            mcsa.OtherDescribe = Lib.get_value_str(request["otherDescribe"]);
            mcsa.OtherButtons = Lib.get_value_str(request["otherButtons"]);
            mcsa.CommentDescribe = Lib.get_value_str(request["commentDescribe"]);
            mcsa.SignatureDate = Lib.get_value_str(request["signatureDate"]);
            mcsa.ExaminerComment = Lib.get_value_str(request["examinerComment"]);
            //end pag 2

            //Papge 3
            mcsa.LastName3 = Lib.get_value_str(request["LastName3"]);
            mcsa.FirstName3 = Lib.get_value_str(request["FirstName3"]);
            mcsa.DOB3 = Lib.get_value_str(request["DOB3"]);
            mcsa.ExamDate3 = Lib.get_value_str(request["ExamDate3"]);
            mcsa.pulseRhythm1 = Lib.get_value_str(request["pulseRhythm1"]);
            mcsa.pulseRhythm2 = Lib.get_value_str(request["pulseRhythm2"]);
            mcsa.Height3 = Lib.get_value_str(request["Height3"]);
            mcsa.Feet3 = Lib.get_value_str(request["Feet3"]);
            mcsa.Weight3 = Lib.get_value_str(request["Weight3"]);
            mcsa.SittingSystolic3 = Lib.get_value_str(request["SittingSystolic3"]);
            mcsa.SittingDiastolic3 = Lib.get_value_str(request["SittingDiastolic3"]);
            mcsa.SecondSystolic3 = Lib.get_value_str(request["SecondSystolic3"]);
            mcsa.SecondSitting3 = Lib.get_value_str(request["SecondSitting3"]);
            mcsa.UrinalysisSP3 = Lib.get_value_str(request["UrinalysisSP3"]);
            mcsa.UrinalysisProtein3 = Lib.get_value_str(request["UrinalysisProtein3"]);
            mcsa.UrinalysisBlood3 = Lib.get_value_str(request["UrinalysisBlood3"]);
            mcsa.UrinalysisSugar3 = Lib.get_value_str(request["UrinalysisSugar3"]);
            mcsa.RightEyeUncorrected3 = Lib.get_value_str(request["RightEyeUncorrected3"]);
            mcsa.RightEyeCorrected3 = Lib.get_value_str(request["RightEyeCorrected3"]);
            mcsa.HorizontalRight3 = Lib.get_value_str(request["HorizontalRight3"]);
            mcsa.LeftEyeUncorrected3 = Lib.get_value_str(request["LeftEyeUncorrected3"]);
            mcsa.LeftEyeCorrected3 = Lib.get_value_str(request["LeftEyeCorrected3"]);
            mcsa.HorizontalLeftEye3 = Lib.get_value_str(request["HorizontalLeftEye3"]);
            mcsa.BothEyesUncorrected3 = Lib.get_value_str(request["BothEyesUncorrected3"]);
            mcsa.BothEyesCorrected3 = Lib.get_value_str(request["BothEyesCorrected3"]);
            mcsa.DistinguishYes3 = Lib.get_value_str(request["DistinguishYes3"]);
            mcsa.DistinguishNo3 = Lib.get_value_str(request["DistinguishNo3"]);
            mcsa.MonocularYes3 = Lib.get_value_str(request["MonocularYes3"]);
            mcsa.MonocularNo3 = Lib.get_value_str(request["MonocularNo3"]);
            mcsa.ReferredYes3 = Lib.get_value_str(request["ReferredYes3"]);
            mcsa.ReferredNo3 = Lib.get_value_str(request["ReferredNo3"]);
            mcsa.DocumentYes3 = Lib.get_value_str(request["DocumentYes3"]);
            mcsa.DocumentNo3 = Lib.get_value_str(request["DocumentNo3"]);
            mcsa.RightEar3 = Lib.get_value_str(request["RightEar3"]);
            mcsa.LeftEar3 = Lib.get_value_str(request["LeftEar3"]);
            mcsa.NeitherEar3 = Lib.get_value_str(request["NeitherEar3"]);
            mcsa.RecordRightEye3 = Lib.get_value_str(request["RecordRightEye3"]);
            mcsa.RecordLeftEye3 = Lib.get_value_str(request["RecordLeftEye3"]);
            mcsa.Right500Hz3 = Lib.get_value_str(request["Right500Hz3"]);
            mcsa.Right1000Hz3 = Lib.get_value_str(request["Right1000Hz3"]);
            mcsa.Right2000Hz3 = Lib.get_value_str(request["Right2000Hz3"]);
            mcsa.Left500Hz3 = Lib.get_value_str(request["Left500Hz3"]);
            mcsa.Left1000Hz3 = Lib.get_value_str(request["Left1000Hz3"]);
            mcsa.Left2000Hz3 = Lib.get_value_str(request["Left2000Hz3"]);
            mcsa.AverageRight3 = Lib.get_value_str(request["AverageRight3"]);
            mcsa.AverageLeft3 = Lib.get_value_str(request["AverageLeft3"]);
            mcsa.GeneralNormarl3 = Lib.get_value_str(request["GeneralNormarl3"]);
            mcsa.GeneralAbnormarl3 = Lib.get_value_str(request["GeneralAbnormarl3"]);
            mcsa.AbdomenNormarl3 = Lib.get_value_str(request["AbdomenNormarl3"]);
            mcsa.AbdomenAbnormarl3 = Lib.get_value_str(request["AbdomenAbnormarl3"]);
            mcsa.SkinNormal3 = Lib.get_value_str(request["SkinNormal3"]);
            mcsa.SkinAbnormal3 = Lib.get_value_str(request["SkinAbnormal3"]);
            mcsa.HerniaNormal3 = Lib.get_value_str(request["HerniaNormal3"]);
            mcsa.HerniaAbnormal3 = Lib.get_value_str(request["HerniaAbnormal3"]);
            mcsa.EyesNormal3 = Lib.get_value_str(request["EyesNormal3"]);
            mcsa.EyesAbnormal3 = Lib.get_value_str(request["EyesAbnormal3"]);
            mcsa.BackNormal3 = Lib.get_value_str(request["BackNormal3"]);
            mcsa.BackAbnormal3 = Lib.get_value_str(request["BackAbnormal3"]);
            mcsa.EarsNormal3 = Lib.get_value_str(request["EarsNormal3"]);
            mcsa.EarsAbnormal3 = Lib.get_value_str(request["EarsAbnormal3"]);
            mcsa.JointNormal3 = Lib.get_value_str(request["JointNormal3"]);
            mcsa.JointAbnormal3 = Lib.get_value_str(request["JointAbnormal3"]);
            mcsa.MouthNormal3 = Lib.get_value_str(request["MouthNormal3"]);
            mcsa.MouthAbnormal3 = Lib.get_value_str(request["MouthAbnormal3"]);
            mcsa.NeuroNormal3 = Lib.get_value_str(request["NeuroNormal3"]);
            mcsa.NeuroAbnormal3 = Lib.get_value_str(request["NeuroAbnormal3"]);
            mcsa.HeartNormal3 = Lib.get_value_str(request["HeartNormal3"]);
            mcsa.HeartAbnormal3 = Lib.get_value_str(request["HeartAbnormal3"]);
            mcsa.GaitNormal3 = Lib.get_value_str(request["GaitNormal3"]);
            mcsa.GaitAbnormal3 = Lib.get_value_str(request["GaitAbnormal3"]);
            mcsa.ChestNormal3 = Lib.get_value_str(request["ChestNormal3"]);
            mcsa.ChestAbnormal3 = Lib.get_value_str(request["ChestAbnormal3"]);
            mcsa.VascularNormal3 = Lib.get_value_str(request["VascularNormal3"]);
            mcsa.VascularAbnormal3 = Lib.get_value_str(request["VascularAbnormal3"]);
            mcsa.ExamComment3 = Lib.get_value_str(request["ExamComment3"]);
            mcsa.OtherTesting3 = Lib.get_value_str(request["OtherTesting3"]);
            //End Page 3
            // page 4
            mcsa.lastname4 = Lib.get_value_str(request["lastname4"]);
            mcsa.firstname4 = Lib.get_value_str(request["firstname4"]);
            mcsa.dob4 = Lib.get_value_str(request["dob4"]);
            mcsa.examdate4 = Lib.get_value_str(request["examdate4"]);
            mcsa.NotStandardsWhy4 = Lib.get_value_str(request["NotStandardsWhy4"]);
            mcsa.MeetStandardQualifies4 = Lib.get_value_str(request["MeetStandardQualifies4"]);
            mcsa.MeetStandardButPeriodic4 = Lib.get_value_str(request["MeetStandardButPeriodic4"]);
            mcsa.MeetStandardButPeriodicWhy4 = Lib.get_value_str(request["MeetStandardButPeriodicWhy4"]);
            mcsa.DriverQualified3m4 = Lib.get_value_str(request["DriverQualified3m4"]);
            mcsa.DriverQualified6m4 = Lib.get_value_str(request["DriverQualified6m4"]);
            mcsa.DriverQualified1y4 = Lib.get_value_str(request["DriverQualified1y4"]);
            mcsa.DriverQualifiedOther4 = Lib.get_value_str(request["DriverQualifiedOther4"]);
            mcsa.DriverQualifiedOtherWhy4 = Lib.get_value_str(request["DriverQualifiedOtherWhy4"]);
            mcsa.CorrectLenses4 = Lib.get_value_str(request["CorrectLenses4"]);
            mcsa.HearingAid4 = Lib.get_value_str(request["HearingAid4"]);
            mcsa.WaiverQualify4 = Lib.get_value_str(request["WaiverQualify4"]);
            mcsa.WaiverEnter4 = Lib.get_value_str(request["WaiverEnter4"]);
            mcsa.SpeQualify4 = Lib.get_value_str(request["SpeQualify4"]);
            mcsa.QualifiedOperation4 = Lib.get_value_str(request["QualifiedOperation4"]);
            mcsa.ExemptQualify4 = Lib.get_value_str(request["ExemptQualify4"]);
            mcsa.DeterPending4 = Lib.get_value_str(request["DeterPending4"]);
            mcsa.PendingWhy4 = Lib.get_value_str(request["PendingWhy4"]);
            mcsa.ReturnExam4 = Lib.get_value_str(request["ReturnExam4"]);
            mcsa.ReturnDate4 = Lib.get_value_str(request["ReturnDate4"]);
            mcsa.ReportAmend4 = Lib.get_value_str(request["ReportAmend4"]);
            mcsa.AmendWhy4 = Lib.get_value_str(request["AmendWhy4"]);
            mcsa.ExamIncomplete4 = Lib.get_value_str(request["ExamIncomplete4"]);
            mcsa.IncompleteWhy4 = Lib.get_value_str(request["IncompleteWhy4"]);
            mcsa.ExamName4 = Lib.get_value_str(request["ExamName4"]);
            mcsa.MedicalAddress4 = Lib.get_value_str(request["MedicalAddress4"]);
            mcsa.MedicalCity4 = Lib.get_value_str(request["MedicalCity4"]);
            mcsa.MedicalState4 = Lib.get_value_str(request["MedicalState4"]);
            mcsa.MedicalZip4 = Lib.get_value_str(request["MedicalZip4"]);
            mcsa.MedicalPhone4 = Lib.get_value_str(request["MedicalPhone4"]);
            mcsa.ExamDate4 = Lib.get_value_str(request["ExamDate4"]);
            mcsa.CertNumber4 = Lib.get_value_str(request["CertNumber4"]);
            mcsa.IssueState4 = Lib.get_value_str(request["IssueState4"]);
            mcsa.MD4 = Lib.get_value_str(request["MD4"]);
            mcsa.DO4 = Lib.get_value_str(request["DO4"]);
            mcsa.PhysAssist4 = Lib.get_value_str(request["PhysAssist4"]);
            mcsa.ChiroPractor4 = Lib.get_value_str(request["ChiroPractor4"]);
            mcsa.PracNurse4 = Lib.get_value_str(request["PracNurse4"]);
            mcsa.OtherPrac4 = Lib.get_value_str(request["OtherPrac4"]);
            mcsa.OtherPracSpecify4 = Lib.get_value_str(request["OtherPracSpecify4"]);
            mcsa.NationalRegister4 = Lib.get_value_str(request["NationalRegister4"]);
            mcsa.CertificateExpiration4 = Lib.get_value_str(request["CertificateExpiration4"]);
            // end page 4
            // page 5
            mcsa.lastname5 = Lib.get_value_str(request["lastname5"]);
            mcsa.firstname5 = Lib.get_value_str(request["firstname5"]);
            mcsa.dob5 = Lib.get_value_str(request["dob5"]);
            mcsa.examdate5 = Lib.get_value_str(request["examdate5"]);
            mcsa.NotStandardsWhy5 = Lib.get_value_str(request["NotStandardsWhy5"]);
            mcsa.MeetStandardQualifies5 = Lib.get_value_str(request["MeetStandardQualifies5"]);
            mcsa.MeetStandardButPeriodic5 = Lib.get_value_str(request["MeetStandardButPeriodic5"]);
            mcsa.MeetStandardButPeriodicWhy5 = Lib.get_value_str(request["MeetStandardButPeriodicWhy5"]);
            mcsa.DriverQualified3m5 = Lib.get_value_str(request["DriverQualified3m5"]);
            mcsa.DriverQualified6m5 = Lib.get_value_str(request["DriverQualified6m5"]);
            mcsa.DriverQualified1y5 = Lib.get_value_str(request["DriverQualified1y5"]);
            mcsa.DriverQualifiedOther5 = Lib.get_value_str(request["DriverQualifiedOther5"]);
            mcsa.DriverQualifiedOtherWhy5 = Lib.get_value_str(request["DriverQualifiedOtherWhy5"]);
            mcsa.CorrectLenses5 = Lib.get_value_str(request["CorrectLenses5"]);
            mcsa.HearingAid5 = Lib.get_value_str(request["HearingAid5"]);
            mcsa.WaiverQualify5 = Lib.get_value_str(request["WaiverQualify5"]);
            mcsa.WaiverEnter5 = Lib.get_value_str(request["WaiverEnter5"]);
            mcsa.SpeQualify5 = Lib.get_value_str(request["SpeQualify5"]);
            mcsa.QualifiedOperation5 = Lib.get_value_str(request["QualifiedOperation5"]);
            mcsa.ExemptQualify5 = Lib.get_value_str(request["ExemptQualify5"]);
            mcsa.DeterPending5 = Lib.get_value_str(request["DeterPending5"]);
            mcsa.PendingWhy5 = Lib.get_value_str(request["PendingWhy5"]);
            mcsa.ReturnExam5 = Lib.get_value_str(request["ReturnExam5"]);
            mcsa.ReturnDate5 = Lib.get_value_str(request["ReturnDate5"]);
            mcsa.ReportAmend5 = Lib.get_value_str(request["ReportAmend5"]);
            mcsa.AmendWhy5 = Lib.get_value_str(request["AmendWhy5"]);
            mcsa.ExamIncomplete5 = Lib.get_value_str(request["ExamIncomplete5"]);
            mcsa.IncompleteWhy5 = Lib.get_value_str(request["IncompleteWhy5"]);
            mcsa.ExamName5 = Lib.get_value_str(request["ExamName5"]);
            mcsa.MedicalAddress5 = Lib.get_value_str(request["MedicalAddress5"]);
            mcsa.MedicalCity5 = Lib.get_value_str(request["MedicalCity5"]);
            mcsa.MedicalState5 = Lib.get_value_str(request["MedicalState5"]);
            mcsa.MedicalZip5 = Lib.get_value_str(request["MedicalZip5"]);
            mcsa.MedicalPhone5 = Lib.get_value_str(request["MedicalPhone5"]);
            mcsa.ExamDate5 = Lib.get_value_str(request["ExamDate5"]);
            mcsa.CertNumber5 = Lib.get_value_str(request["CertNumber5"]);
            mcsa.IssueState5 = Lib.get_value_str(request["IssueState5"]);
            mcsa.MD5 = Lib.get_value_str(request["MD5"]);
            mcsa.DO5 = Lib.get_value_str(request["DO5"]);
            mcsa.PhysAssist5 = Lib.get_value_str(request["PhysAssist5"]);
            mcsa.ChiroPractor5 = Lib.get_value_str(request["ChiroPractor5"]);
            mcsa.PracNurse5 = Lib.get_value_str(request["PracNurse5"]);
            mcsa.OtherPrac5 = Lib.get_value_str(request["OtherPrac5"]);
            mcsa.OtherPracSpecify5 = Lib.get_value_str(request["OtherPracSpecify5"]);
            mcsa.NationalRegister5 = Lib.get_value_str(request["NationalRegister5"]);
            mcsa.CertificateExpiration5 = Lib.get_value_str(request["CertificateExpiration5"]);
            // end page 5
            string error = string.Empty;
            var result = new ResultInfo("Fialed", "error", "", 0);
            mcsa.Create3(ref error, mcsa);
            result = new ResultInfo("Success", "OK", "", mcsa);


            response.Write(JsonConvert.SerializeObject(result));
        }


        [WebMethod]
        public string GetData(HttpContext context)
        {
            Mcsa5875 mcsa = new Mcsa5875();
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            var result = mcsa.Get(string.Empty);
            response.Write(JsonConvert.SerializeObject(result));
            return string.Empty;
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