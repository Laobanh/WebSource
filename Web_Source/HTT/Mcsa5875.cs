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
    public class Mcsa5875
    {

        #region Fields
        private int iCompanyId = 0;
        //Page 1
        private String medNumber = "0", nameLast = "0", nameFirst = "0", nameInitial = "0", birthDate = "0", driverAge = "0", driverAddress = "0", driverCity = "0", driverState = "0", driverZip = "0", driverLicense = "0", licenseState = "0", driverPhone = "0",
            genderButtons = "0", emailAddress = "0", cdlButtonList = "0", driverVerify = "0", certDenyButtons = "0", surgeryButtons = "0", surgeryDescribe = "0", medicineDescribe = "0", medicineButtons = "0";
        //Page2
        private String nameLastHead2 = "0", nameFirstHead2 = "0", dateBirth2 = "0", dateForm2 = "0", headButtons = "0", seizeButtons = "0", eyeButtons = "0", earButtons = "0", heartButtons = "0", paceButtons = "0", highButtons = "0",
            cholesterolButtons = "0", breathButtons = "0", lungButtons = "0", kidneyButtons = "0", stomachButtons = "0", sugarButtons = "0", insulinButtons = "0", mentalButtons = "0", faintButtons = "0", dizzyButtons
            = "0", weightButtons = "0", strokeButtons = "0", uselimitButtons = "0", neckbackButtons = "0", boneButtons = "0", bloodButtons = "0", cancerButtons = "0", infectButtons = "0", sleepDisconer = "0", apneaButtons = "0", sleeptestButtons = "0", hospitalButtons
            = "0", brokenButtons = "0", tobaccoButtons = "0", alcoholButtons = "0", illegalButtons = "0", failedButtons = "0", otherButtons = "0", otherDescribe = "0", commentButtons = "0", commentDescribe = "0",
            signatureDriver = "0", signatureDate = "0", examinerComment = "0";
        //Page 3
        private String nameLastHead3 = "0", nameFirstHead3 = "0", dateForm3 = "0", dateBirth3 = "0", pulseMeasure = "0", pulserhythmButtons = "0", feetHeight = "0", inchesHeight = "0", poundsWeight = "0", sitSys = "0", sitDias = "0", secSys = "0",
            secDias = "0", otherTesting = "0", spgrNumber = "1.020", proteinNumber = "neg", bloodNumber = "neg", sugarNumber = "neg", uncorrectRight = "0", correctRight = "0", fieldRight = "80", uncorrectLeft = "0", correctLeft = "0", fieldLeft
            = "80", uncorrectBoth = "0", correctBoth = "0", distinguishButtons = "1", monocularButtons = "2", referredButtons = "2", documentButtons = "2", rightBox = "0", leftBox = "0", nichtBox = "0", whisperRight = ">5",
whisperLeft = ">5", right500 = "0", right1000 = "0", right2000 = "0", left500 = "0", left1000 = "0", left2000 = "0", rightAverage = "0", leftAverage = "0", generalButtons = "1", skinButtons = "1", eyesButtons = "1", earsButtons = "1", mouthButtons = "1",
            heart3Buttons = "1", chestButtons = "1", abdomenButtons = "1", herniaButtons = "1", backButtons = "1", jointsButtons = "1", neuroButtons = "1", gaitButtons = "1", vascularButtons = "1", examComment = "0";
        //Page 4
        private String nameLastHead4 = "0", nameFirstHead4 = "0", dateForm4 = "0", dateBirth4 = "0", standardButtonList = "0", notStandardsWhy = "0", butStandardsWhy = "0", qualifiedButtonList = "0", otherQualify = "0",
            correctLenses = "0", hearingAid = "0", waiverQualify = "0", waiverEnter = "0", speQualify = "0", cfrQualify = "0", exemptQualify = "0", deterPending = "0", pendingWhy = "0", returnExam = "0", returnDate = "0",
            reportAmend = "0", amendWhy = "0", ifAmendSignature = "0", ifAmendDate = "0", examIncomplete = "0", incompleteWhy = "0", examSignature = "0", examName = "0", medicalAddress = "0", medicalCity = "0", medicalState = "0", medicalZip = "0",
            medicalPhone = "0", examDate = "0", certNumber = "0", issueState = "0", md = "0", sdo = "0", physAssist = "0", chiroPractor = "0", pracNurse = "0", otherPrac = "0", otherPracSpecify = "0", nationalRegister = "0", expireDate = "0";
        //Page 5
        private String nameLastHead5 = "0", nameFirstHead5 = "0", dateForm5 = "0", dateBirth5 = "0", standardButtonListState = "0", notStandardsWhyState = "0", butStandardsWhyState = "0", qualifiedButtonListState = "0",
            otherQualifyState = "0", correctLensesState = "0", hearingAidState = "0", waiverQualifyState = "0", waiverEnterState = "0", speQualifyState = "0", grandQualifyState = "0", examSignatureState = "0",
examNameState = "0", examDateState = "0", medicalAddressState = "0", medicalCityState = "0", medicalStateState = "0", medicalZipState = "0", medicalPhoneState = "0", certNumberState = "0", issueStateState = "0", mdState = "0", doState = "0",
physAssistState = "0", chiroPractorState = "0", pracNurseState = "0", otherPracState = "0", otherSpec = "0", nationalRegisterState = "0", expireDateState = "0";
        private Guid Id = Guid.NewGuid();
        #endregion End Fields

        #region Constructors
        public Mcsa5875()
        {

        }
        public Mcsa5875(String examnumber,String lic)
        {            
            this.medNumber = examnumber;
            this.driverLicense = lic;
        }
        public Mcsa5875(String examnumber, int companyId, People people, MyOrganization organization)
        {
            var d = DateTime.Now;

            String value = "";        
            this.iCompanyId = companyId;
            this.medNumber = examnumber;
            String lastname = people.PersonalInfo.Person.LastName;
            //Last name
            this.nameLast = lastname;
            this.nameLastHead2 = lastname;
            this.nameLastHead3 = lastname;
            this.nameLastHead4 = lastname;
            this.NameLastHead5 = lastname;
            //First name
            String firstName = people.PersonalInfo.Person.FirstName;
            this.nameFirst = firstName;
            this.nameFirstHead2 = firstName;
            this.nameFirstHead3 = firstName;
            this.nameFirstHead4 = firstName;
            this.nameFirstHead5 = firstName;

            //Middle name
            String middleName = people.PersonalInfo.Person.MiddleName;
            this.nameInitial = middleName.Equals("0") ? "" : middleName;
            //DOB
            String dob = people.PersonalInfo.Person.DateOfBirth;
            this.birthDate = dob;
            this.dateBirth2 = dob;
            this.dateBirth3 = dob;
            this.dateBirth4 = dob;
            this.dateBirth5 = dob;

            //Get driver Age 
            var dateOfBirth = Lib.ret_date(dob);
            this.driverAge = Lib.CalAge(dateOfBirth).ToString();

            //Exam Date
            String examDate = d.ToString("MM/dd/yyyy");
            this.dateForm2 = examDate;
            this.dateForm3 = examDate;
            this.dateForm4 = examDate;
            this.dateForm5 = examDate;

            //address
            value = Lib.get_value_str(people.PersonalInfo.Address.Address);
            value = value.Equals("0") ? "" : value;
            this.driverAddress = value;
            //city
            value = Lib.get_value_str(people.PersonalInfo.Address.City);
            value = value.Equals("0") ? "" : value;
            this.driverCity = value;
            //States
            value = Lib.get_value_str(people.PersonalInfo.Address.State);
            value = value.Equals("0") ? "" : value;
            var fp = new FilePath("");
            value = Lib.Get_Sates_hash(fp.Folder + @"\states_hash.json", value);
            this.driverState = value;
            //zipcode
            value = Lib.get_value_str(people.PersonalInfo.Address.Zip.ToString());
            value = value.Equals("0") ? "" : value;
            this.driverZip = value;
            //primary ID
            value = Lib.get_value_str(people.Driver.PrimaryID);
            value = value.Equals("0") ? "" : value;
            this.driverLicense = value;
            this.driverVerify = value;
            //Phone
            value = Lib.get_value_str(people.PersonalInfo.Contact.MobilePhone);
            value = value.Equals("0") ? "" : value;
            this.driverPhone = value;

            //Gender
            value = Lib.get_value_str(people.PersonalInfo.Person.Gender);
            value = value.Equals("0") ? "" : value;
            this.genderButtons = value.Equals("M") ? "1" : "2";


            //Exam

        }
        public Mcsa5875 (Mcsa5875 mcsa)
        {
            
        }

        public Mcsa5875(HttpRequest request)
        {
            this.medNumber = Lib.get_value_str(request[FieldKeys.Exam]);
            this.nameLast = Lib.get_value_str(request[FieldKeys.LastName]);
            this.nameFirst = Lib.get_value_str(request[FieldKeys.FirstName]);
            this.nameInitial = Lib.get_value_str(request[FieldKeys.MiddleName]);
        }
        public string MedNumber { get => medNumber; set => medNumber = value; }
        public string NameLast { get => nameLast; set => nameLast = value; }
        public string NameFirst { get => nameFirst; set => nameFirst = value; }
        public string NameInitial { get => nameInitial; set => nameInitial = value; }
        public string BirthDate { get => birthDate; set => birthDate = value; }
        public string DriverAge { get => driverAge; set => driverAge = value; }
        public string DriverAddress { get => driverAddress; set => driverAddress = value; }
        public string DriverCity { get => driverCity; set => driverCity = value; }
        public string DriverState { get => driverState; set => driverState = value; }
        public string DriverZip { get => driverZip; set => driverZip = value; }
        public string DriverLicense { get => driverLicense; set => driverLicense = value; }
        public string LicenseState { get => licenseState; set => licenseState = value; }
        public string DriverPhone { get => driverPhone; set => driverPhone = value; }
        public string GenderButtons { get => genderButtons; set => genderButtons = value; }
        public string EmailAddress { get => emailAddress; set => emailAddress = value; }
        public string CdlButtonList { get => cdlButtonList; set => cdlButtonList = value; }
        public string DriverVerify { get => driverVerify; set => driverVerify = value; }
        public string CertDenyButtons { get => certDenyButtons; set => certDenyButtons = value; }
        public string SurgeryButtons { get => surgeryButtons; set => surgeryButtons = value; }
        public string SurgeryDescribe { get => surgeryDescribe; set => surgeryDescribe = value; }
        public string MedicineDescribe { get => medicineDescribe; set => medicineDescribe = value; }
        public string MedicineButtons { get => medicineButtons; set => medicineButtons = value; }
        public string NameLastHead2 { get => nameLastHead2; set => nameLastHead2 = value; }
        public string NameFirstHead2 { get => nameFirstHead2; set => nameFirstHead2 = value; }
        public string DateBirth2 { get => dateBirth2; set => dateBirth2 = value; }
        public string DateForm2 { get => dateForm2; set => dateForm2 = value; }
        public string HeadButtons { get => headButtons; set => headButtons = value; }
        public string SeizeButtons { get => seizeButtons; set => seizeButtons = value; }
        public string EyeButtons { get => eyeButtons; set => eyeButtons = value; }
        public string EarButtons { get => earButtons; set => earButtons = value; }
        public string HeartButtons { get => heartButtons; set => heartButtons = value; }
        public string PaceButtons { get => paceButtons; set => paceButtons = value; }
        public string HighButtons { get => highButtons; set => highButtons = value; }
        public string CholesterolButtons { get => cholesterolButtons; set => cholesterolButtons = value; }
        public string BreathButtons { get => breathButtons; set => breathButtons = value; }
        public string LungButtons { get => lungButtons; set => lungButtons = value; }
        public string KidneyButtons { get => kidneyButtons; set => kidneyButtons = value; }
        public string StomachButtons { get => stomachButtons; set => stomachButtons = value; }
        public string SugarButtons { get => sugarButtons; set => sugarButtons = value; }
        public string InsulinButtons { get => insulinButtons; set => insulinButtons = value; }
        public string MentalButtons { get => mentalButtons; set => mentalButtons = value; }
        public string FaintButtons { get => faintButtons; set => faintButtons = value; }
        public string DizzyButtons { get => dizzyButtons; set => dizzyButtons = value; }
        public string WeightButtons { get => weightButtons; set => weightButtons = value; }
        public string StrokeButtons { get => strokeButtons; set => strokeButtons = value; }
        public string UselimitButtons { get => uselimitButtons; set => uselimitButtons = value; }
        public string NeckbackButtons { get => neckbackButtons; set => neckbackButtons = value; }
        public string BoneButtons { get => boneButtons; set => boneButtons = value; }
        public string BloodButtons { get => bloodButtons; set => bloodButtons = value; }
        public string CancerButtons { get => cancerButtons; set => cancerButtons = value; }
        public string InfectButtons { get => infectButtons; set => infectButtons = value; }
        public string ApneaButtons { get => apneaButtons; set => apneaButtons = value; }
        public string SleeptestButtons { get => sleeptestButtons; set => sleeptestButtons = value; }
        public string SleepDisconer { get => sleepDisconer; set => sleepDisconer = value; }
        public string HospitalButtons { get => hospitalButtons; set => hospitalButtons = value; }
        public string BrokenButtons { get => brokenButtons; set => brokenButtons = value; }
        public string TobaccoButtons { get => tobaccoButtons; set => tobaccoButtons = value; }
        public string AlcoholButtons { get => alcoholButtons; set => alcoholButtons = value; }
        public string IllegalButtons { get => illegalButtons; set => illegalButtons = value; }
        public string FailedButtons { get => failedButtons; set => failedButtons = value; }
        public string OtherButtons { get => otherButtons; set => otherButtons = value; }
        public string OtherDescribe { get => otherDescribe; set => otherDescribe = value; }
        public string CommentButtons { get => commentButtons; set => commentButtons = value; }
        public string CommentDescribe { get => commentDescribe; set => commentDescribe = value; }
        public string SignatureDriver { get => signatureDriver; set => signatureDriver = value; }
        public string SignatureDate { get => signatureDate; set => signatureDate = value; }
        public string ExaminerComment { get => examinerComment; set => examinerComment = value; }
        public string NameLastHead3 { get => nameLastHead3; set => nameLastHead3 = value; }
        public string NameFirstHead3 { get => nameFirstHead3; set => nameFirstHead3 = value; }
        public string DateForm3 { get => dateForm3; set => dateForm3 = value; }
        public string DateBirth3 { get => dateBirth3; set => dateBirth3 = value; }
        public string PulseMeasure { get => pulseMeasure; set => pulseMeasure = value; }
        public string PulserhythmButtons { get => pulserhythmButtons; set => pulserhythmButtons = value; }
        public string FeetHeight { get => feetHeight; set => feetHeight = value; }
        public string InchesHeight { get => inchesHeight; set => inchesHeight = value; }
        public string PoundsWeight { get => poundsWeight; set => poundsWeight = value; }
        public string SitSys { get => sitSys; set => sitSys = value; }
        public string SitDias { get => sitDias; set => sitDias = value; }
        public string SecSys { get => secSys; set => secSys = value; }
        public string SecDias { get => secDias; set => secDias = value; }
        public string OtherTesting { get => otherTesting; set => otherTesting = value; }
        public string SpgrNumber { get => spgrNumber; set => spgrNumber = value; }
        public string ProteinNumber { get => proteinNumber; set => proteinNumber = value; }
        public string BloodNumber { get => bloodNumber; set => bloodNumber = value; }
        public string SugarNumber { get => sugarNumber; set => sugarNumber = value; }
        public string UncorrectRight { get => uncorrectRight; set => uncorrectRight = value; }
        public string CorrectRight { get => correctRight; set => correctRight = value; }
        public string FieldRight { get => fieldRight; set => fieldRight = value; }
        public string UncorrectLeft { get => uncorrectLeft; set => uncorrectLeft = value; }
        public string CorrectLeft { get => correctLeft; set => correctLeft = value; }
        public string FieldLeft { get => fieldLeft; set => fieldLeft = value; }
        public string UncorrectBoth { get => uncorrectBoth; set => uncorrectBoth = value; }
        public string CorrectBoth { get => correctBoth; set => correctBoth = value; }
        public string DistinguishButtons { get => distinguishButtons; set => distinguishButtons = value; }
        public string MonocularButtons { get => monocularButtons; set => monocularButtons = value; }
        public string ReferredButtons { get => referredButtons; set => referredButtons = value; }
        public string DocumentButtons { get => documentButtons; set => documentButtons = value; }
        public string RightBox { get => rightBox; set => rightBox = value; }
        public string LeftBox { get => leftBox; set => leftBox = value; }
        public string NichtBox { get => nichtBox; set => nichtBox = value; }
        public string WhisperRight { get => whisperRight; set => whisperRight = value; }
        public string WhisperLeft { get => whisperLeft; set => whisperLeft = value; }
        public string Right500 { get => right500; set => right500 = value; }
        public string Right1000 { get => right1000; set => right1000 = value; }
        public string Right2000 { get => right2000; set => right2000 = value; }
        public string Left500 { get => left500; set => left500 = value; }
        public string Left1000 { get => left1000; set => left1000 = value; }
        public string Left2000 { get => left2000; set => left2000 = value; }
        public string RightAverage { get => rightAverage; set => rightAverage = value; }
        public string LeftAverage { get => leftAverage; set => leftAverage = value; }
        public string GeneralButtons { get => generalButtons; set => generalButtons = value; }
        public string SkinButtons { get => skinButtons; set => skinButtons = value; }
        public string EyesButtons { get => eyesButtons; set => eyesButtons = value; }
        public string EarsButtons { get => earsButtons; set => earsButtons = value; }
        public string MouthButtons { get => mouthButtons; set => mouthButtons = value; }
        public string Heart3Buttons { get => heart3Buttons; set => heart3Buttons = value; }
        public string ChestButtons { get => chestButtons; set => chestButtons = value; }
        public string AbdomenButtons { get => abdomenButtons; set => abdomenButtons = value; }
        public string HerniaButtons { get => herniaButtons; set => herniaButtons = value; }
        public string BackButtons { get => backButtons; set => backButtons = value; }
        public string JointsButtons { get => jointsButtons; set => jointsButtons = value; }
        public string NeuroButtons { get => neuroButtons; set => neuroButtons = value; }
        public string GaitButtons { get => gaitButtons; set => gaitButtons = value; }
        public string VascularButtons { get => vascularButtons; set => vascularButtons = value; }
        public string ExamComment { get => examComment; set => examComment = value; }
        public string NameLastHead4 { get => nameLastHead4; set => nameLastHead4 = value; }
        public string NameFirstHead4 { get => nameFirstHead4; set => nameFirstHead4 = value; }
        public string DateForm4 { get => dateForm4; set => dateForm4 = value; }
        public string DateBirth4 { get => dateBirth4; set => dateBirth4 = value; }
        public string StandardButtonList { get => standardButtonList; set => standardButtonList = value; }
        public string NotStandardsWhy { get => notStandardsWhy; set => notStandardsWhy = value; }
        public string ButStandardsWhy { get => butStandardsWhy; set => butStandardsWhy = value; }
        public string QualifiedButtonList { get => qualifiedButtonList; set => qualifiedButtonList = value; }
        public string OtherQualify { get => otherQualify; set => otherQualify = value; }
        public string CorrectLenses { get => correctLenses; set => correctLenses = value; }
        public string HearingAid { get => hearingAid; set => hearingAid = value; }
        public string WaiverQualify { get => waiverQualify; set => waiverQualify = value; }
        public string WaiverEnter { get => waiverEnter; set => waiverEnter = value; }
        public string SpeQualify { get => speQualify; set => speQualify = value; }
        public string CfrQualify { get => cfrQualify; set => cfrQualify = value; }
        public string ExemptQualify { get => exemptQualify; set => exemptQualify = value; }
        public string DeterPending { get => deterPending; set => deterPending = value; }
        public string PendingWhy { get => pendingWhy; set => pendingWhy = value; }
        public string ReturnExam { get => returnExam; set => returnExam = value; }
        public string ReturnDate { get => returnDate; set => returnDate = value; }
        public string ReportAmend { get => reportAmend; set => reportAmend = value; }
        public string AmendWhy { get => amendWhy; set => amendWhy = value; }
        public string IfAmendSignature { get => ifAmendSignature; set => ifAmendSignature = value; }
        public string IfAmendDate { get => ifAmendDate; set => ifAmendDate = value; }
        public string ExamIncomplete { get => examIncomplete; set => examIncomplete = value; }
        public string IncompleteWhy { get => incompleteWhy; set => incompleteWhy = value; }
        public string ExamSignature { get => examSignature; set => examSignature = value; }
        public string ExamName { get => examName; set => examName = value; }
        public string MedicalAddress { get => medicalAddress; set => medicalAddress = value; }
        public string MedicalCity { get => medicalCity; set => medicalCity = value; }
        public string MedicalState { get => medicalState; set => medicalState = value; }
        public string MedicalZip { get => medicalZip; set => medicalZip = value; }
        public string MedicalPhone { get => medicalPhone; set => medicalPhone = value; }
        public string ExamDate { get => examDate; set => examDate = value; }
        public string CertNumber { get => certNumber; set => certNumber = value; }
        public string IssueState { get => issueState; set => issueState = value; }
        public string Md { get => md; set => md = value; }
        public string Sdo { get => sdo; set => sdo = value; }
        public string PhysAssist { get => physAssist; set => physAssist = value; }
        public string ChiroPractor { get => chiroPractor; set => chiroPractor = value; }
        public string PracNurse { get => pracNurse; set => pracNurse = value; }
        public string OtherPrac { get => otherPrac; set => otherPrac = value; }
        public string OtherPracSpecify { get => otherPracSpecify; set => otherPracSpecify = value; }
        public string NationalRegister { get => nationalRegister; set => nationalRegister = value; }
        public string ExpireDate { get => expireDate; set => expireDate = value; }
        public string NameLastHead5 { get => nameLastHead5; set => nameLastHead5 = value; }
        public string NameFirstHead5 { get => nameFirstHead5; set => nameFirstHead5 = value; }
        public string DateForm5 { get => dateForm5; set => dateForm5 = value; }
        public string DateBirth5 { get => dateBirth5; set => dateBirth5 = value; }
        public string StandardButtonListState { get => standardButtonListState; set => standardButtonListState = value; }
        public string NotStandardsWhyState { get => notStandardsWhyState; set => notStandardsWhyState = value; }
        public string ButStandardsWhyState { get => butStandardsWhyState; set => butStandardsWhyState = value; }
        public string QualifiedButtonListState { get => qualifiedButtonListState; set => qualifiedButtonListState = value; }
        public string OtherQualifyState { get => otherQualifyState; set => otherQualifyState = value; }
        public string CorrectLensesState { get => correctLensesState; set => correctLensesState = value; }
        public string HearingAidState { get => hearingAidState; set => hearingAidState = value; }
        public string WaiverQualifyState { get => waiverQualifyState; set => waiverQualifyState = value; }
        public string WaiverEnterState { get => waiverEnterState; set => waiverEnterState = value; }
        public string SpeQualifyState { get => speQualifyState; set => speQualifyState = value; }
        public string GrandQualifyState { get => grandQualifyState; set => grandQualifyState = value; }
        public string ExamSignatureState { get => examSignatureState; set => examSignatureState = value; }
        public string ExamNameState { get => examNameState; set => examNameState = value; }
        public string ExamDateState { get => examDateState; set => examDateState = value; }
        public string MedicalAddressState { get => medicalAddressState; set => medicalAddressState = value; }
        public string MedicalCityState { get => medicalCityState; set => medicalCityState = value; }
        public string MedicalStateState { get => medicalStateState; set => medicalStateState = value; }
        public string MedicalZipState { get => medicalZipState; set => medicalZipState = value; }
        public string MedicalPhoneState { get => medicalPhoneState; set => medicalPhoneState = value; }
        public string CertNumberState { get => certNumberState; set => certNumberState = value; }
        public string IssueStateState { get => issueStateState; set => issueStateState = value; }
        public string MdState { get => mdState; set => mdState = value; }
        public string DoState { get => doState; set => doState = value; }
        public string PhysAssistState { get => physAssistState; set => physAssistState = value; }
        public string ChiroPractorState { get => chiroPractorState; set => chiroPractorState = value; }
        public string PracNurseState { get => pracNurseState; set => pracNurseState = value; }
        public string OtherPracState { get => otherPracState; set => otherPracState = value; }
        public string OtherSpec { get => otherSpec; set => otherSpec = value; }
        public string NationalRegisterState { get => nationalRegisterState; set => nationalRegisterState = value; }
        public string ExpireDateState { get => expireDateState; set => expireDateState = value; }
        public int CompanyId { get => iCompanyId; set => iCompanyId = value; }
        public Guid Ids { get => Id; set => Id = value; }

        // Page 3
        public string LastName3 { get; set; }
        public string FirstName3 { get; set; }
        public string DOB3 { get; set; }
        public string ExamDate3 { get; set; }
        public string pulseRhythm13 { get; set; }
        public string pulseRhythm23 { get; set; }
        public string PulseRate3 { get; set; }
        public string Height3 { get; set; }
        public string Feet3 { get; set; }
        public string Weight3 { get; set; }
        public string SittingSystolic3 { get; set; }
        public string SittingDiastolic3 { get; set; }
        public string SecondSystolic3 { get; set; }
        public string SecondSitting3 { get; set; }
        public string UrinalysisSP3 { get; set; }
        public string UrinalysisProtein3 { get; set; }
        public string UrinalysisBlood3 { get; set; }
        public string UrinalysisSugar3 { get; set; }
        public string RightEyeUncorrected3 { get; set; }
        public string RightEyeCorrected3 { get; set; }
        public string HorizontalRight3 { get; set; }
        public string LeftEyeUncorrected3 { get; set; }
        public string LeftEyeCorrected3 { get; set; }
        public string HorizontalLeftEye3 { get; set; }
        public string BothEyesUncorrected3 { get; set; }
        public string BothEyesCorrected3 { get; set; }
        public string DistinguishYes3 { get; set; }
        public string DistinguishNo3 { get; set; }
        public string MonocularYes3 { get; set; }
        public string MonocularNo3 { get; set; }
        public string ReferredYes3 { get; set; }    
        public string ReferredNo3 { get; set; }    
        public string DocumentYes3 { get; set; }    
        public string DocumentNo3 { get; set; }    
        public string RightEar3 { get; set; }    
        public string LeftEar3 { get; set; }    
        public string NeitherEar3 { get; set; }    
        public string RecordRightEye3 { get; set; }    
        public string RecordLeftEye3 { get; set; }    
        public string Right500Hz3 { get; set; }    
        public string Right1000Hz3 { get; set; }    
        public string Right2000Hz3 { get; set; }    
        public string Left500Hz3 { get; set; }    
        public string Left1000Hz3 { get; set; }    
        public string Left2000Hz3 { get; set; }    
        public string AverageRight3 { get; set; }    
        public string AverageLeft3 { get; set; }    
        public string GeneralNormarl3 { get; set; }    
        public string GeneralAbnormarl3 { get; set; }    
        public string AbdomenNormarl3 { get; set; }    
        public string AbdomenAbnormarl3 { get; set; }    
        public string SkinNormal3 { get; set; }    
        public string SkinAbnormal3 { get; set; }    
        public string HerniaNormal3 { get; set; }    
        public string HerniaAbnormal3 { get; set; }    
        public string EyesNormal3 { get; set; }    
        public string EyesAbnormal3 { get; set; }    
        public string BackNormal3 { get; set; }    
        public string BackAbnormal3 { get; set; }    
        public string EarsNormal3 { get; set; }    
        public string EarsAbnormal3 { get; set; }    
        public string JointNormal3 { get; set; }    
        public string JointAbnormal3 { get; set; }    
        public string MouthNormal3 { get; set; }    
        public string MouthAbnormal3 { get; set; }    
        public string NeuroNormal3 { get; set; }    
        public string NeuroAbnormal3 { get; set; }    
        public string HeartNormal3 { get; set; }    
        public string HeartAbnormal3 { get; set; }    
        public string GaitNormal3 { get; set; }    
        public string GaitAbnormal3  { get; set; }    
        public string ChestNormal3 { get; set; }    
        public string ChestAbnormal3 { get; set; }    
        public string VascularNormal3 { get; set; }    
        public string VascularAbnormal3 { get; set; }    
        public string ExamComment3 { get; set; }    
        public string OtherTesting3 { get; set; }    
        // End Page 3
        //page4
        public string lastname4 { get; set; }
        public string firstname4 { get; set; }
        public string dob4 { get; set; }
        public string examdate4 { get; set; }
        public string NotStandardsWhy4 { get; set; }
        public string DoNotMeetStandard4 { get; set; }
        public string MeetStandardQualifies4 { get; set; }
        public string MeetStandardButPeriodic4 { get; set; }
        public string MeetStandardButPeriodicWhy4 { get; set; }
        public string DriverQualified3m4 { get; set; }
        public string DriverQualified6m4 { get; set; }
        public string DriverQualified1y4 { get; set; }
        public string DriverQualifiedOther4 { get; set; }
        public string DriverQualifiedOtherWhy4 { get; set; }
        public string CorrectLenses4 { get; set; }
        public string HearingAid4 { get; set; }
        public string WaiverQualify4 { get; set; }
        public string WaiverEnter4 { get; set; }
        public string SpeQualify4 { get; set; }
        public string QualifiedOperation4 { get; set; }
        public string ExemptQualify4 { get; set; }
        public string DeterPending4 { get; set; }
        public string PendingWhy4 { get; set; }
        public string ReturnExam4 { get; set; }
        public string ReturnDate4 { get; set; }
        public string ReportAmend4 { get; set; }
        public string AmendWhy4 { get; set; }
        public string ExamIncomplete4 { get; set; }
        public string IncompleteWhy4 { get; set; }
        public string ExamName4 { get; set; }
        public string MedicalAddress4 { get; set; }
        public string MedicalCity4 { get; set; }
        public string MedicalState4 { get; set; }
        public string MedicalZip4 { get; set; }
        public string MedicalPhone4 { get; set; }
        public string ExamDate4 { get; set; }
        public string CertNumber4 { get; set; }
        public string IssueState4 { get; set; }
        public string MD4 { get; set; }
        public string DO4 { get; set; }
        public string PhysAssist4 { get; set; }
        public string ChiroPractor4 { get; set; }
        public string PracNurse4 { get; set; }
        public string OtherPrac4 { get; set; }
        public string OtherPracSpecify4 { get; set; }
        public string NationalRegister4 { get; set; }
        public string CertificateExpiration4 { get; set; }

        // endpage4

        //page5
        public string Lastname5 { get; set; }
        public string Firstname5 { get; set; }
        public string DOB5 { get; set; }
        public string DonotMeetStandards5 { get; set; }
        public string MeetStandards5 { get; set; }
        public string MeetStandardsButPeriodic5 { get; set; }
        public string MeetStandardsButPeriodicWhy5 { get; set; }
        public string DonotMeetStandardsWhy5 { get; set; }
        public string NotStandardsWhy5 { get; set; }
        public string OtherQualify5 { get; set; }
        public string MedicalDate5 { get; set; }
        public string MeetStandardQualifies5 { get; set; }
        public string MeetStandardButPeriodic5 { get; set; }
        public string MeetStandardButPeriodicWhy5 { get; set; }
        public string DriverQualified3m5 { get; set; }
        public string DriverQualified6m5 { get; set; }
        public string DriverQualified1y5 { get; set; }
        public string DriverQualifiedOther5 { get; set; }
        public string DriverQualifiedOtherWhy5 { get; set; }
        public string CorrectLenses5 { get; set; }
        public string HearingAid5 { get; set; }
        public string WaiverQualify5 { get; set; }
        public string WaiverEnter5 { get; set; }
        public string SpeQualify5 { get; set; }
        public string QualifiedOperation5 { get; set; }
        public string MedicalEcaminer5 { get; set; }
        public string ExemptQualify5 { get; set; }
        public string DeterPending5 { get; set; }
        public string PendingWhy5 { get; set; }
        public string ReturnExam5 { get; set; }
        public string ReturnDate5 { get; set; }
        public string ReportAmend5 { get; set; }
        public string AmendWhy5 { get; set; }
        public string ExamIncomplete5 { get; set; }
        public string IncompleteWhy5 { get; set; }
        public string ExamName5 { get; set; }
        public string MedicalAddress5 { get; set; }
        public string MedicalCity5 { get; set; }
        public string MedicalState5 { get; set; }
        public string MedicalZip5 { get; set; }
        public string MedicalPhone5 { get; set; }
        public string ExamDate5 { get; set; }
        public string CertNumber5 { get; set; }
        public string IssueState5 { get; set; }
        public string MD5 { get; set; }
        public string DO5 { get; set; }
        public string PhysAssist5 { get; set; }
        public string ChiroPractor5 { get; set; }
        public string PracNurse5 { get; set; }
        public string OtherPrac5 { get; set; }
        public string OtherPracSpecify5 { get; set; }
        public string NationalRegister5 { get; set; }
        public string CertificateExpiration5 { get; set; }

        // end page5

        #endregion End Constructors

        #region Methods
        public void Created(ref String error, string examNumber, int companyId, People people, MyOrganization organization)
        {
            try
            {
                var mcsa = new Mcsa5875(examNumber, companyId, people, organization);
                var js = JsonConvert.SerializeObject(mcsa);

                //js = StringEncryptDecrypt.Encrypt(js, FieldKeys.PassKey);
                FilePath fp = new FilePath("Exam");
                String path = fp.Folder + mcsa.medNumber + ".json";

                Lib.WriteFileJson(ref error, path, js);
            }
            catch(Exception ex)
            {
                Lib.writerLog("MCSA5875", "Created", ex.Message, "error");
            }
        }

        public void Created2(ref String error, string examNumber,String lic)
        {
            try
            {
                var mcsa = new Mcsa5875(examNumber, lic);
                var js = JsonConvert.SerializeObject(mcsa);

                //js = StringEncryptDecrypt.Encrypt(js, FieldKeys.PassKey);
                FilePath fp = new FilePath("Exam");
                String path = fp.Folder + mcsa.medNumber + ".json";

                Lib.WriteFileJson(ref error, path, js);
            }
            catch (Exception ex)
            {
                Lib.writerLog("MCSA5875", "Created", ex.Message, "error");
            }
        }

        public void Create3(ref String error, Mcsa5875 mcsa)
        {
            try
            {
                var js = JsonConvert.SerializeObject(mcsa);
                js = StringEncryptDecrypt.Encrypt(js, FieldKeys.Mcsa5875Class);
                FilePath fp = new FilePath(FieldKeys.Mcsa5875Class);
                //mcsa.Ids = Guid.NewGuid();
                String folder = fp.Folder;

                String fileName = folder + mcsa.Ids + ".json";

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                Lib.WriteFileJson(ref error, fileName, js);
            }
            catch (Exception ex)
            {
                Lib.writerLog("MCSA5875", "Created", ex.Message, "error");
            }
 
        }

        public string GetMcsaMedNumber(String medNumber)
        {
            Mcsa5875 mcsa = new Mcsa5875();
            string id = string.Empty;
            try
            {
                String js = String.Empty;
                FilePath filePath = new FilePath(FieldKeys.Mcsa5875Class);

                String folder = filePath.Folder;

                var files = Directory.GetFiles(folder).Select(x => Path.GetFileName(x));

                foreach (var file in files)
                {
                    String filename = folder + @"\" + file;

                    js = File.ReadAllText(filename);

                    js = StringEncryptDecrypt.Decrypt(js, FieldKeys.Mcsa5875Class);
                    if (js.Length > 0)
                    {
                        mcsa = JsonConvert.DeserializeObject<Mcsa5875>(js);
                        if(mcsa != null)
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

        public Mcsa5875 Get(String id)
        {
            var fp = new FilePath("MCSA5875");
            string ids = GetMcsaMedNumber(id);
            if(string.IsNullOrEmpty(ids))
            {
                return null;
            }    
            String path = fp.Folder + ids + ".json";
            var js = File.ReadAllText(path);
            js = StringEncryptDecrypt.Decrypt(js, FieldKeys.Mcsa5875Class);
            Mcsa5875 mcsa = JsonConvert.DeserializeObject<Mcsa5875>(js);
            return mcsa;
        }
        #endregion End Methods

    }
}
