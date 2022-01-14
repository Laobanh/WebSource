using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTT
{
    public class FieldKeys
    {
        //Encrypt key
        public const String PassKey = "68KhongMatKhau86";
        public const String  PassKeyLID = "168168"; 
        public const String Cmd = "cmd", FilePath = "FilePath", FileName= "FileName";
        //class
        public const String UserClass = "user", DriverClass = "driver", CompanyClass = "company", DonorClass = "donor", MroReports = "MroReports", PeopleClass = "People", Mcsa5875Class = "Mcsa5875";
        public const String ConsortiumClass = "Consortium", ScheduleClass = "Schedules", ReportClass = "Reports", Exam = "Exam";

        public const String ID = "id", UserID = "userid", OldID = "oldid", NewID = "newid", OldName = "oldname", NewName = "Newname";

        //Account Info
        public const String Username = "username", Password = "password", Group = "group", isChangePassword = "isChangePassword";

        //Personal Info
        //Person info
        public const String FirstName = "firstname", MiddleName = "middlename", LastName = "lastname", Title = "title";
        public const String DateOfBirth = "dateofbirth", Gender = "gender";
        //Contact Method
        public const String HomePhone = "homephone", WorkPhone = "workphone", MobilePhone = "mobilephone";
        public const String Email = "email", Website = "website";

        //Address Info
        public const String Address = "address", OfficeLocation = "officelaction", Country = "country", City = "city", State = "state", Zip = "zip";
        //DonorInfo
        public const String DonorId = "DonorId";
        //Donor
        public const String PrimaryID = "primaryid", PrimaryIDType = "primaryidtype", PrimaryIDExpirationDate = "ExpirationDate", AlternateID = "alternateid", AltIDType = "altidtype", AltIDExpirationDate = "altidexp", Category = "category", Mode = "mode", UseStateDLIDDrugTestOnly = "usdliddto";
        public const String donorSelectionType = "DonorType";

        //Company
        public const String CompanyName = "companyname", ConsortiumID = "consortiumid", Plan = "Plan", SumDriver = "SumDriver", Bill = "Bill";
        public const String CompanyID = "CompanyID";

        public const String CreatedDate = "createddate", ModifyDate = "modifydate", ExpirationDate = "ExpirationDate";


        //DonorList
        public const String ExcludeFromSelection = "ExcludeFromSelection", NotActive = "NotActive", NotAvilable = "NotAvilable";
        public const String NotActiveDate = "NotActiveDate", NotAvilableDate = "NotAvilableDate", NotActiveReason = "NotActiveReason", NotAvilableReason = "NotAvilableReason";

        //Activities
        public const String ClassActivity = "Activities";
        public const String Comment = "Comment";
        public const String Action = "Action";
        public const String Details = "Details";
        public const String Type = "Type";

        //Ipmport
        public const String ImportClass = "Imports";

        public const String ListID = "ListID";

        //Schedule
        public const String IsDot = "IsDot", RunNow = "RunNow", Repeat = "Repeat", NumberOfTimes = "NumberOfTimes", EndOn = "EndOn", RunTime = "RunTime", DayOfWeek = "DayOfWeek", RandomOnly = "RandomOnly";
        //Random Report
        public const String ReportType = "ReportType";

        //Specimen
        public const String SpecimenType1 = "SpecimenType1", CollectionSite1 = "CollectionSite1", MRO1 = "MRO1", Lab1 = "Lab1", NumberDonor1 = "NumberDonor1", NumberDonorType1 = "NumberDonorType1", SelectionMethod1 = "SelectionMethod1";
        public const String SpecimenType2 = "SpecimenType2", CollectionSite2 = "CollectionSite2", MRO2 = "MRO2", Lab2 = "Lab2", NumberDonor2 = "NumberDonor2", NumberDonorType2 = "NumberDonorType2", SelectionMethod2 = "SelectionMethod2";
        //Alternate specimen
        public const String AlternateNumberDonor1 = "AlternateNumberDonor1", AlternateNumberDonorType1 = "AlternateNumberDonorType1", AlternateSpecimenType1 = "AlternateSpecimenType1";
        //Alternate specimen
        public const String AlternateNumberDonor2 = "AlternateNumberDonor2", AlternateNumberDonorType2 = "AlternateNumberDonorType2", AlternateSpecimenType2 = "AlternateSpecimenType2";

        //Email
        public const String Content = "Content", Name = "Name", Notice = "Notice", Server = "Server", Cc = "Cc", CcName = "CcName", Bcc = "Bcc", BccName = "BccName";
        public const String From = "From", FromName = "FromName", To = "To", ToName = "ToName", IsUseContent = "isUseContent", Days = "Days";

        //MCSA 5875


        public FieldKeys()
        {
            
        }
    }

}