function FieldKeys() {
    this.data = {
        "Cmd": "Cmd",
        FilePath: "FilePath",
        "ID": "ID",
        "ListID": "ListID",
        "UserID": "UserID",
        "Username": "Username",
        "Password": "Password",
        "Group": "Group",
        "AccountGroup": ["Admin", "Agent", "Guest"],
        "isChangePassword": "isChangePassword",
        "FirstName": "FirstName",
        "MiddleName": "MiddleName",
        "LastName": "LastName",
        "Title": "Title",
        "DateOfBirth": "DateOfBirth",
        "Gender": "Gender",
        "HomePhone": "Homephone",
        "WorkPhone": "Workphone",
        "MobilePhone": "Mobilephone",
        "Email": "Email",
        "Website": "Website",
        "Address": "Address",
        "OfficeLocation": "Officelaction",
        "Country": "Country",
        "City": "City",
        "State": "State",
        "Zip": "Zip",
        "PrimaryID": "PrimaryID",
        "PrimaryIDType": "Primaryidtype",
        "PrimaryIDExpirationDate": "ExpirationDate",
        "AlternateID": "Alternateid",
        "AltIDType": "Altidtype",
        "AltIDExpirationDate": "Altidexp",
        "Category": "Category",
        "Mode": "Mode",
        "UseStateDLIDDrugTestOnly": "USDLIIDDTO",
        "CompanyName": "CompanyName",
        "ConsortiumID": "ConsortiumID",
        "Plan": "Plan",
        "Plans": ["A", "B", "C", "D"],
        "PersonGender": ["Male", "Female", "Other"],
        "PersonTitle": ["Mr", "Mrs", "Ms", "Dr"],
        //DonorInfo
        "ExcludeFromSelection": "ExcludeFromSelection",
        "NotActive": "NotActive",
        "NotAvilable": "NotAvilable",

        "NotActiveDate": "NotActiveDate",
        "NotAvilableDate": "NotAvilableDate",

        "NotActiveReason": "NotActiveReason",
        "NotAvilableReason": "NotAvilableReason",
        //company
        CompanyID: "CompanyID",
        "ExpirationDate": "ExpirationDate",
        "Bill": "Bill",
        "OldName": "oldname",
        "NewName": "newname",
        OldID: "oldid",
        "NewID": "newid",
        //Activity
        "Details": "Details",
        "Type": "Type",
        "Action": "Action",
        //Schedule Details
        IsDot: "IsDot",
        RunNow: "RunNow",
        Repeat: "Repeat",
        NumberOfTimes: "NumberOfTimes",
        EndOn: "EndOn",
        RunTime: "RunTime",
        DayOfWeek: "DayOfWeek",
        RandomOnly: "RandomOnly",
        //Specimen 1
        SpecimenType1: "SpecimenType1",
        CollectionSite1: "CollectionSite1",
        Mro1: "Mro1",
        Lab1: "Lab1",
        NumberDonor1: "NumberDonor1",
        NumberDonorType1: "NumberDonorType1",
        SelectionMethod1: "SelectionMethod1",
        //Alternate specimen 1
        AlternateNumberDonor1: "AlternateNumberDonor1",
        AlternateNumberDonorType1: "AlternateNumberDonorType1",
        AlternateSpecimenType1: "AlternateSpecimenType1",
        //Specimen 2
        SpecimenType2: "SpecimenType2",
        CollectionSite2: "CollectionSite2",
        Mro2: "Mro2",
        Lab2: "Lab2",
        NumberDonor2: "NumberDonor2",
        NumberDonorType2: "NumberDonorType2",
        SelectionMethod2: "SelectionMethod2",
        //Alternate specimen 2
        AlternateNumberDonor2: "AlternateNumberDonor2",
        AlternateNumberDonorType2: "AlternateNumberDonorType2",
        AlternateSpecimenType2: "AlternateSpecimenType2",
        //Preferences
        Content: "Content", Name: "Name", Notice: "Notice", Server: "Server",Port:"Port", Cc: "Cc", CcName: "CcName", Bcc: "Bcc", BccName: "BccName",
        From: "From", FromName: "FromName", To: "To", ToName: "ToName",Content:"Content",

        //LAB
        "LAB": ["Phamtech", "CRL", "Lab Corp", "Quest"],
        "TestType": ["Pre-employment", "Random", "Post Accident", "Reasonable Suspecision/Cause", "Physical exam", "Return to Duty", "Follow-up", "Other"],
        "Role": ["Client", "Collection Facility", "Lab", "MRO", "SAP"],
        "Permissions": ["Basic Client", "Order Tests", "Schedule", "Edit People", "Add People", "View Results", "Alternate List", "Reports"],
        "Occupations": ["CDL/Cross border driver (FMCSA)", "Driver (FMCSA)", "Aircraft Maintenance (FAA)", "Air Traffic Controller (FAA)", "Aviation Screener (FAA)", "Flight Attendant (FAA)", "Flight Crewmember (FAA)", "Flight Instructor (FAA)", "Ground Security Coordinator (FAA)", " Dispatch/Operator (FRA)", "Engine Service (FRA)", "Maintenance of Way (FRA)", "Signal Service (FRA)", "Train Service (FRA)", "Armed Security Personnel (FTA)", "CDL/Non-Revenue Vehicle (FTA)", "Revenue Vehicle Control/Dispatch (FTA)", "Revenue Vehicle & Equipment Maintenance (FTA)", "Revenue Vehicle Operation (FTA)", "Hazardous Material (PHMSA)", "Operation/Maintenance/Emergency (PHMSA)", "Pipeline/FMCSA (PHMSA)", "Crewmember (USCG)", "Administrative Poisition", "Administrative Position", "Apache", "Childcare", "Collector", "DER", "Donna", "FMCSA/FTA-Driver", "FTA-Non Driver", "Laborer", "Linda", "Non-DOT", "Non-Mandated Worker", "Private/Court Random Testing Participant", "Shill", "Transit Vehicle Mechanic", "XTO"]

    };
    //Load FieldKeys
    this.GetFieldKeys = function () {
        $.ajax({
            type: 'POST',
            url: '../Handlers/Handler_GetFieldKey.ashx',
            data: {},
            success: function (msg) {
                msg = JSON.parse(msg);
                return msg;
            },
            error: function () {
                console.log("Load Field key Failed...");
            }
        });
    }

}