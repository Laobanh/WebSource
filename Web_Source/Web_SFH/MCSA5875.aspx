<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MCSA5875.aspx.cs" Inherits="Web_SFH.MCSA5875" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Form MCSA 5875</title>
    <script src="Scripts/jquery-3.6.0.min.js"></script>
    <script src="js/lib.js"></script>
    <script src="js/Mcsa5875.js?v=21102101"></script>
    <script type="text/javascript">
        var mcsa = new MCSA5875();
        $(document).ready(function () {
            mcsa.GetLang("en", "#tbPage2");
            onChangeLanguage();
            ShowPages();
            GetState();
            var donorId = getUrlVars().DonorId;
            var id = getUrlVars().id;
            var lic = getUrlVars().lic;
            var data = {
                id: id,
                donorId: donorId,
                lic:lic
            };

            //Gets(data);

            OnkeyOut();

            OnClickToolBar();
        });

        function onChangeLanguage() {
            $('#flag').find('p').on('click', function () {
                $('#flag').find('p').removeClass('selected');
                var id = $(this).attr('id');
                $(this).addClass('selected');
                mcsa.GetLang(id,"#tbPage2");
            });
        }
        function ShowPages() {
            $('#selectPages').find('p').on('click', function () {
                $('#selectPages').find('p').removeClass('select');
                var id = $(this);
                id.addClass('select');
                for (var i = 1; i < 6; i++) {
                    var page = $('#page' + i );
                    if (i === parseInt(id.html())) {
                        page.show();
                    } else {
                        page.hide();
                    }

                    if (id.html() === 'All')
                        page.show();
                }
                console.log(id.html())
            });
        }

        function GetState() {
            $.getJSON('data/states_hash.json', function (data) {
                var str = "";
                Object.keys(data).map(function (v) {
                    var sel = "selected='selected'";
                    str += "<option value='" + v + "' " + (v === 'CA' ? sel : '') + ">" + v + "</option>";
                    //str += "<option value='" + v + "' " + ">" + v + "</option>";
                });
                $('#selDriverState').html(str);
                $('#selMedicalState4').html(str);
                $('#selIssueState4').html(str);
                 $('#selMedicalState5').html(str);
                $('#selIssueState5').html(str);               

            });
        }
        function Gets(data) {
            $.ajax({
                type: 'POST',
                url: 'Handlers/Handler_CreateExamNumber.ashx',
                dataType: 'json',
                data: data,
                success: function (msg) {
                    if (msg.Status === "OK") {
                        FillData(msg);
                    }                    
                },
                error: function () {

                }
            });
        }
        function FillData(idata) {
            if (idata.Data !== undefined) {
                var data = idata.Data;
                var value = data.MedNumber;
                value = value !== "0" ? value : "";
                $('#txtExamNumber').val(value);

                value = data.NameLast;
                value = value !== "0" ? value : "";
                $('#txtLastName').val(value);
                $('#txtLastName2').val(value);
                $('#txtLastName3').val(value);
                $('#txtLastName4').val(value);
                $('#txtLastName5').val(value);

                value = data.NameFirst;
                value = value !== "0" ? value : "";
                $('#txtFirstName').val(value);
                $('#txtFirstName2').val(value);
                $('#txtFirstName3').val(value);
                $('#txtFirstName4').val(value);
                 $('#txtFirstName5').val(value);

                //NameInitial
                value = data.NameInitial;
                value = value !== "0" ? value : "";
                $('#txtMiddleName').val(value);
                //BirthDate
                value = data.BirthDate;
                value = value !== "0" ? value : "";
                $('#txtDOB1').val(value);
                $('#txtDOB2').val(value);
                $('#txtDOB3').val(value);
                $('#txtDOB4').val(value);
                $('#txtDOB5').val(value);
                //DriverAge
                value = data.DriverAge;
                value = value !== "0" ? value : "";
                $('#txtAge').val(value);

                //DriverLicense
                value = data.DriverLicense;
                value = value !== "0" ? value : "";
                $('#txtDriverLicense').val(value);
                $('#txtDriverVerify').val(value);
                //DriverAddress
                value = data.DriverAddress;
                value = value !== "0" ? value : "";
                $('#txtDriverAddress').val(value);
                //Driver Zip code
                value = data.DriverZip;
                value = value !== "0" ? value : "";
                $('#txtDriverZip').val(value);
                //Driver Phone
                value = data.DriverPhone;
                value = value !== "0" ? value : "";
                $('#txtDriverPhone').val(value);
                //GenderButtons
                value = data.GenderButtons;
                value = value !== "0" ? value : "";
                $("input[name=gender][value=" + parseInt(value) + "]").attr("checked", "checked");
                //DriverCity
                value = data.DriverCity;
                value = value !== "0" ? value : "";
                $("#txtDriverCity").val(value);
                //DriverState
                value = data.DriverState;
                value = value !== "0" ? value : "CA";
                $('#selStates1').val("CA");
                
            } 
        }

        function OnkeyOut() {
            $('div').find('input').focusout(function () {
                var id = $(this).attr('id');
                //console.log('key out: ' + id);
                switch (id) {
                    case 'txtDOB1':
                        GetAge();                        
                        break;
                }
            });
        }

        function GetAge() {            
            $.ajax({
                type: 'POST',
                url: 'Handlers/Handler_GetAge.ashx',
                //dataType: 'json',
                data: {
                    dob: $('#txtDOB1').val()
                },
                success: function (msg) {    
                  
                    $('#txtAge').val(msg);
                },
                error: function () {

                }
            });
        }

        function OnClickToolBar() {
            $('#toolbar').find('p').on('click', function () {
                var id = $(this).attr('id');
                switch (id) {
                    case 'Save':
                        Save();
                        break;
                    case 'loadData':
                        Get();
                        break;
                    case 'ResetForm':
                        ResetForm();
                        break;
                }
            });
        }

        function ResetForm() {
            $('#ctl01').find(':input').not(':button, :submit, :reset, :hidden, :checkbox, :radio').val('');
            $('#ctl01').find(':checkbox, :radio').prop('checked', false);
        }

        (function ($) {
            $.fn.serializeFormJSON = function () {

                var o = {};
                var a = this.serializeArray();
                $.each(a, function () {
                    if (o[this.name]) {
                        if (!o[this.name].push) {
                            o[this.name] = [o[this.name]];
                        }
                        o[this.name].push(this.value || '');
                    } else {
                        o[this.name] = this.value || '';
                    }
                });
                return o;
            };
        })(jQuery);

        function Save() {
            var dataForm = $("#frmIndex").serializeFormJSON();
            ///Page2
            dataForm["DriverState"] = $("#selStates1 :selected").val();
            ///EndPage2
            ////End Page 1
            //Page 4
            dataForm["MedicalState4"] = $("#selMedicalState4 :selected").val();
            dataForm["IssueState4"] = $("#selIssueState4 :selected").val();
            //End Page 4
            //Page 5
            dataForm["MedicalState5"] = $("#selMedicalState5 :selected").val();
            dataForm["IssueState5"] = $("#selIssueState5 :selected").val();
            //End Page 5

            $.ajax({
                type: 'POST',
                url: 'Handlers/Handler_MCSA5875.ashx',
                data: dataForm,
                success: function (msg) {
                    alert(msg);

                },
                error: function () {
                    error.html("Signin Failed.");
                }
            });
        }

        function Get() {
            var id = $("#txtExamNumber").val();
            if (!id)
                return false;

            var dataForm = $("#frmIndex").serializeFormJSON();
            dataForm["Id"] = id;
           
            $.ajax({
                type: 'POST',
                url: 'Handlers/Handler_MCSA5875_LoadData.ashx',
                data: dataForm,
                success: function (data) {
                    const obj = JSON.parse(data);
                    console.log(obj);
                    for (var key in obj) {
                        var type = $('input[name="' + key + '"]').attr('type');
                        var checked = obj[key] == "on" ? true : false;
                        var value = obj[key] == "0" ? "" : obj[key];
                        if (type == 'radio') 
                            $('input[name="' + key + '"][value="' + obj[key] + '"]').prop('checked', true)
                        if (type == 'checkbox') {
                            $('input[name="' + key + '"]').prop('checked', checked);
                        }
                        else if (type == "text") {
                            $('input[name="' + key + '"]').val(value);
                        }
                        else {
                            $('textarea[name="' + key + '"]').val(value);
                            if (key == "IssueState4" || key == "MedicalState4" || key == "MedicalState5" || key == "IssueState5" || key=="DriverState") {
                                $(`#sel${key} option[value=${value}]`).attr('selected', 'selected');
                            }
                        }
                     
                    }

                },
                error: function (e) {
                    error.html("Signin Failed.");
                }
            });
        }


    </script>
    <style type="text/css">
        body {
            width: 100%;
            height: 100%;
            margin: 0;
            padding: 10px;
            background-color: #e6e6e6;
            font-size: 12pt ;
        }
        * {
            box-sizing: border-box;
            -moz-box-sizing: border-box;
        }
        .page {
            width: 1024px;            
            height:940px;    
            margin:auto;
            border: 1px #D3D3D3 solid;
            border-radius: 5px;
            background: white;
            box-shadow: 0 0 5px rgba(0, 0, 0, 0.1);
            font-size:11pt;
        }
        .logo {            
            width: 1024px;   
                  
            margin:0;
           padding:15px 5px 10px 15px;
            
        }       
        input[type=text]{
            min-width:20px;
            text-align:left;
        }
        .txt{
            background-color:#dee5ff;
            border:0;
            border-bottom:solid 2px #808080;
            font-size:1em;
            padding:5px;
            text-align:center;
            
        } 
        .txt2{
            background-color:#dee5ff;
            border:0;           
            font-size:1em;
            padding:5px;
            text-align:left;
        }
        .title{
            width:990px;            
            margin-left:15px; 
            border:solid 1px #012d9a; 
            padding:3px 0 0 5px;
            background-color:#012d9a; 
            color:#fff; 
            font-size:0.8em;  
            margin-top:5px;
        }
        .content{
            width:990px;
            border:solid 1px #012d9a; 
            margin-left:15px;           
        }

        .title2{
            width:990px;            
            margin-left:15px; 
            border:solid 1px #007c3e; 
            padding:3px 0 0 5px;
            background-color:#007c3e; 
            color:#fff; 
            font-size:0.8em;  
            margin-top:5px;
        }
        .content2{
            width:990px;
            border:solid 1px #007c3e; 
            margin-left:15px;           
        }
        .area{
             border:solid 1px #012d9a; 
        }
        .area2{
             border:solid 1px #007c3e; 
        }
        th{
            border:0;
            margin:0;
            padding:0;
            font-size:9pt;
            font-weight:bolder;
            vertical-align:bottom;
        }
        td{
            border:0;
            padding:0;
            margin:0;
            font-size:10pt;
        }
        
        p{
            margin:5px 15px 3px 15px;
            border:0;
            padding:0;
        }
        .td1{
             margin:0;
             background-color:#dee5ff;   
             height:30px;
             border:0;
        }        
        span{
            font-size:10pt;
        }
        @page{
            size:A4;
            margin:0;
        }
        @media print {
            html, body {
                width: 210mm;
                height: 297mm;
            }

            .page {
                margin: 0;
                border: initial;
                border-radius: initial;
                width: initial;
                min-height: initial;
                box-shadow: initial;
                background: initial;
                page-break-after: always;
            }
        }
        #page1{
            /*display:none;*/
            height:auto;
            padding-bottom:20px;
        }
        #page2
        {
            /*display:none;*/
            height:auto;
            padding-bottom:20px;
        }
        #page3
        {
            /*display:none;*/
            height:auto;
            padding-bottom:20px;
        }
        #page4
        {
            /*display:none;*/
            height:auto;
            padding-bottom:20px;
            margin-top:5px;
        }
         #page5
        {
            /*display:none;*/
            height:auto;
            padding-bottom:20px;
            margin-top:5px;
        }
        .toolbars{
            width: 245px;
            height:auto;
            position:fixed;  
            top:10px;
            z-index:1;
        }
        .flag{
            width:46px;
            height:32px;           
            float:left;            
            border-radius:8px;
            padding:0;
            cursor:pointer;
           
        }
        .flag-us{           
            background:url(images/ic_flag_us.png)center no-repeat;
            background-size:auto;
           
        }

        .flag-cn{           
            background:url(images/ic_flag_china.png)center no-repeat;
            background-size:auto;           
        }
        .flag-es{           
            background:url(images/ic_flag_es.png)center no-repeat;
            background-size:auto;  
            display:none;
        }
        .toolbars p.pages{
            padding:5px;
            background-color:#808080;
            color:#fff;
            border-radius:3px;
            text-align:center;
        }
        .toolbars p.select{            
            background-color:#007c3e;            
        }
        .toolbars p.pages:hover{
            cursor:pointer;
            text-decoration:underline;
        }

        .selected{
            border:solid 2px #35792a;
        }
        #toolbar{
            display:block;
            position:absolute;           
            width:1024px;            
            margin:0 35% auto;
            margin-bottom:3px;
        }
            #toolbar p {
                width: 120px;
                height: 30px;
                border-radius: 3px;
                background-color: #007c3e;
                color:#fff;
                float:left;
                text-align:center;
                padding-top:5px;
            }

            #toolbar p:hover{
                background-color:#35792a;
                text-decoration:underline;
                font-weight:bolder;
                cursor:pointer;
            }
    </style>
</head>
<body>  
    <div class="toolbars">
        <div style="width:80px; height:100%; float:left;" id="flag">
            <p id="en" class="flag flag-us selected" title="US"></p>
            <p id="es" class="flag flag-es" title="US"></p>
            <p id="cn" class="flag flag-cn " title="CN"></p>
        </div>
        <div style="width:80px; height:100%; float:left;" id="selectPages">
            <p class="select  pages" id="p0">All</p>
            <p class="pages" id="p1">1</p>
            <p class="pages" id="p2">2</p>
            <p class="pages" id="p3">3</p>
            <p class="pages" id="p4">4</p>
            <p class="pages" id="p5">5</p>
        </div>
    </div>
    <div id="toolbar">
        <p id="loadData">
            Load data
        </p>        
        <p id="ResetForm">
            Reset Form
        </p>
        <p id="Save">
            Save
        </p>
    </div>
    <form class="frmIndex" name="frmIndex" method="post" id="frmIndex" runat="server">
              <!-- Page 1-->
    <div class="page" id="page1">            
        <div class="logo">
            <img src="Images/5875_header_1.png" style="width:1000px; height:120px; text-align:center;" />
        </div>
        <table style="width:1024px;">
            <tr>
                <td style="width:50%; padding-left:15px; vertical-align:bottom;">
                    <span style="font-weight:bolder; vertical-align:bottom;">SECTION 1. Driver Information</span>
                    <span style="font-size:0.8em;">(to be filled out by the driver)</span>
                </td>
                <td style="width:50%;">
                    <div style="width:180px;border:solid 2px #dd898d; text-align:center; float:right; margin-right:20px;">
                        <p>
                            <b>
                                MEDICAL RECORD # 
                            </b>
                        </p>
                        <p>
                            <input class="txt" name="examNumber" type="text" style="width:90%;" id="txtExamNumber" />
                        </p>
                        <p style="font-size:1em;">
                            (or sticker)
                        </p>
                    </div>
                </td>
            </tr>                
        </table>
        <div class="title" id="infomation">
            PERSONAL INFORMATION
        </div>
        <div class="content">
            <table style="width:980px;">                 
                <tr>
                    <td>
                        <%-- Last name --%>
                        <span id="lblLastName" class="lastName">Last Name:</span>                    
                        <span><input id="txtLastName" name="NameLast" type="text" class="txt" style="width:216px;"/></span>
                         <%-- First name --%>
                        <span id="lblFirstName"  class="firstName">First name:&nbsp;</span>                    
                        <span><input id="txtFirstName" name="NameFirst" type="text" class="txt" style="width:150px;"/></span>
                         <%-- Middle name --%>
                        <span id="lblMiddleName" class="middleName">Middle Initial:&nbsp;</span>                    
                        <span><input id="txtMiddleName" name="NameInitial" type="text" class="txt" style="width:80px;" /></span>
                         <%-- Date of Birth --%>
                        <span id="lblDateOfBirth" class="dob">Date of Birth:&nbsp;</span>                    
                        <span><input id="txtDOB1" name="BirthDate" type="text" class="txt" placeholder="MM/dd/yyyy" style="width:140px;"/></span>
                         <%-- Age --%>
                        <span id="lblAge" class="age">Age:&nbsp;</span>                    
                        <span><input id="txtAge" name="DriverAge" type="text" class="txt" style="width:30px;" disabled="disabled"/></span>
                    </td>
                </tr>
               <tr>
                   <td>
                        <%-- Street Address --%>
                        <span id="lblStreetAddress" class="address">Street Address:</span>                  
                        <span><input id="txtDriverAddress" name="DriverAddress" type="text" class="txt" style="width:260px;" /></span>
                        <%-- City --%>
                        <span id="lblCity" class="city">City:&nbsp;</span>                  
                        <span><input id="txtDriverCity" name="DriverCity" type="text" class="txt" style="width:190px;"/></span>
                        <%-- State --%>
                        <span id="lblState" class="state">State/Province:&nbsp;</span>                   
                        <select id="selDriverState" style="width:160px;" class="txt2">
                            
                        </select>
                        <%-- Zip code --%>
                        <span id="lblZip" class="zip">Zip Code:&nbsp;</span>                   
                        <span><input id="txtDriverZip" name="DriverZip" type="text" class="txt"  style="width:60px;" /></span>
                   </td>
               </tr>
               
                
                <tr>
                    <td>
                        <!-- Driver's License Number-->
                       <span id="lblDriverLicense" class="license">Driver's License Number:&nbsp;</span>
                       <span>
                           <input id="txtDriverLicense" name="DriverLicense" type="text" class="txt" style="width:280px;"/>
                       </span>
                        <!-- Issuing State/Province -->
                       <span id="lblLicenseState" class="licenseState">Issuing State/Province:&nbsp;</span>
                       <span>
                           <input id="txtLicenseState" name="LicenseState" type="text" class="txt" style="width:80px;"/>
                       </span>
                        <!-- Phone-->
                       <span id="lblPhone" class="phone">Phone:&nbsp;</span>
                       <span>
                           <input id="txtDriverPhone" name="DriverPhone" type="text" class="txt" style="width:160px;"/>
                       </span>
                        <!-- Gender -->
                       <span id="lblGender" class="gender">Gender:&nbsp;</span>
                        <span>
                            <input type="radio" id="txtM" value="1" name="GenderButtons" /><label id="M" for="txtM" style="margin-left:1px;vertical-align:central">M</label>
                        </span>
                        <span>
                            <input type="radio" id="txtF" value="2" name="GenderButtons"/><label id="F" for="txtF"  style="margin-left:1px;">F</label>
                        </span>
                   </td>
                </tr>
                 <tr>
                    <td>
                        <!-- Email -->
                        <span id="lblEmail" class="email">E-mail</span><span style="font-size:9pt;"> (optional)</span>:
                        <span>
                            <input type="text" class="txt" id="txtDriverEmail" name ="EmailAddress" style="width:370px;"/>
                        </span>
                        <!-- CLD -->
                        <span id="lblCdl" class="cdl">CLP/CDL Applicant/Holder*</span>
                        <span>
                            <input type="radio" id="cdl1" name="CdlButtonList" value="1" /><label for="cdl1" id="lblCdl1" class="yes">Yes</label>
                            <input type="radio" id="cdl2" name="CdlButtonList" value="2" /><label for="cdl2" id="lblCdl2" class="no">No</label>
                        </span>
                    </td>
                </tr>
                <tr>
                    <td style="float:right">
                        <!-- Driver ID Verified By-->
                        <span id="lblDriverVerify" class="verify">Driver ID Verified By**:&nbsp;</span>
                        <span>
                            <input id="txtDriverVerify" name="DriverVerify" type="text" class="txt" style="width:360px;"/>
                        </span>
                    </td>
                </tr>                
                <tr>
                    <td>
                        <%-- Has your USDOT/FMCSA medical certificate ever been denied or issued for lessthan 2 years? --%>
                        <span id="lblcertDeny" class="certDeny">Has your USDOT/FMCSA medical certificate ever been denied or issued for lessthan 2 years?</span>
                        <span>
                            <input type="radio" id="certDeny1" name="CertDenyButtons" value="1" /><label for="cdl1" id="lblCertDeny1" class="yes">Yes</label>
                            <input type="radio" id="certDeny2" name="CertDenyButtons" value="2" /><label for="cdl2" id="lblCertDeny2" class="no">No</label>
                            <input type="radio" id="certDeny3" name="CertDenyButtons" value="3" /><label for="cdl2" id="lblCertDeny3" class="noSure">No Sure</label>
                        </span>
                    </td>
                </tr>
                <tr>
                    <td style="font-size:5pt;">
                        &nbsp;
                    </td>
                </tr>
            </table>
            
        </div>
        <div style="margin-bottom:10px;">                
            <span style="font-size:6pt; padding:3px 300px 3px 12px;">*CLP/CDL Applicant/Holder: See instructions for definitions</span>                    
            <span style="font-size:6pt">**Driver ID Verified By: Record what type of photo ID was used to verify the identity of the driver, e.g., CDL, driver's license, passport</span>                    
        </div>
        <div class="title driverHistory" id="history">
            DRIVER HEALTH HISTORY
        </div>
        <div class="content">
            <table>
                <tr>
                    <td>
                        <!-- Have you ever had surgery?If "yes" please list and explain below. -->
                        <span style="padding:3px 400px 3px 12px;" class="surgery">Have you ever had surgery?If "yes" please list and explain below.</span>
                        <span>
                            <!-- sugery radio button-->
                            <input type="radio" id="lblSugery1" name="SurgeryButtons" value="1" /><label for="lblSugery1" id="sugery1" class="yes">Yes</label>
                            <input type="radio" id="lblSugery2" name="SurgeryButtons" value="2" /><label for="lblSugery2" id="sugery2" class="no">No</label>
                            <input type="radio" id="lblSugery3" name="SurgeryButtons" value="3" /><label for="lblSugery3" id="sugery3" class="noSure">No Sure</label>
                        </span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <!-- sugery text-->
                        <textarea id="txtSugery" name="SurgeryDescribe" style="width:965px; border:solid 2px #808080; margin-left:10px;" rows="8"></textarea>
                    </td>
                </tr>
                <tr>
                    <td>
                        <!-- Are you currently taking medications?If Yes please describe below. -->
                        <span style="padding:3px 0 3px 12px;" class="takingMedications">Are you currently taking medications?If Yes please describe below.</span>
                        <span style="font-size:7pt; padding-right:140px;" class="takingMedicationsSub">(prescription, over-the-counter, herbal remedies, diet supplements)</span>
                        <span>
                             <!-- takingMedications radio button-->
                            <input type="radio" id="takingMedications1" name="MedicineButtons" value="1" /><label for="takingMedications1" id="lblTakingMedications1" class="yes">Yes</label>
                            <input type="radio" id="takingMedications2" name="MedicineButtons" value="2" /><label for="takingMedications2" id="lblTakingMedications2" class="no">No</label>
                            <input type="radio" id="takingMedications3" name="MedicineButtons" value="3" /><label for="ltakingMedications3" id="lblTakingMedications3" class="noSure">No Sure</label>
                        </span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <!-- TakingMedications  text-->
                        <textarea id="txtTakingMedication" name="MedicineDescribe" style="width:965px; border:solid 2px #808080; margin-left:10px;" rows="8"></textarea>
                    </td>
                </tr>
            </table>
        </div>
        <div style="width:98%;padding:10px;font-size:8pt; text-align:right;">            
                (Attach additional sheets if necessary)            
        </div>
        <div style="width:98%;padding-left:10px; ">
            <span>**This document contains sensitive information and is for official use only. Improper handling of this information could negatively affect individuals. Handle and secure this
information appropriately to prevent inadvertent disclosure by keeping the documents under the control of authorized persons. Properly dispose of this document when
no longer required to be maintained by regulatory requirements.** </span>
        </div>
         <div style="  margin-top:10px;">
            <table>
                <tr>
                    <td style="text-align:right; width:1024px;">
                        <span style="padding-right:20px;">Page 1</span>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <!-- Page 2 -->
    <div class="page" id="page2" style="margin-top:5px;">
        <div style="margin:8px 0 3px 0;">
            <span style="padding:3px 700px 3px 10px; font-size:7pt;"><b>Form MCSA-5875</b></span>
            <span  style=" font-size:7pt;">OMB No. 2126-0006 Expiration Date: 12/31/2024</span>
        </div>
        <div style="border:solid 2px #ff0000;margin-left:13px; width:990px; padding:5px 0 2px 0;">
            <table>
                <tr>
                    <td>
                        <span id="lblLastName2" class="lastName">Last Name:</span>
                        <span><input name="NameLastHead2" id="txtLastName2" type="text" class="txt" style="width:240px;" /></span>
                        <span id="lblFirstName2" class="FirstName">First Name:</span>
                        <span><input id="txtFirstName2" name="NameFirstHead2" type="text" class="txt" style="width:180px;" /></span>
                        <span id="lblDob2" class="dob2">DOB:</span>
                        <span><input id="txtDOB2" name="DateBirth2" type="text" class="txt" style="width:180px;" /></span>
                        <span id="lblExamDate2" class="examDate">Exam Date:</span>
                        <span><input type="text" name="dateForm2" id="examDate" class="txt" style="width:100px;"/></span>
                    </td>
                </tr>
            </table>
        </div>
        <div class="title">
            <span id="driverHistory" class="driverHistory">DRIVER HEALTH HISTORY</span>
            <span style="font-size:9pt;">(continued)</span>
        </div>
        <div class="content">
            <table id="tbPage2">

            </table>
        </div>
        <div style="padding:10px;">
            <table style="width:1000px;">
                <tr>
                    <td>
                        <span class="ohc">Other health condition(s) not described above:</span>
                    </td>
                    <td style="float:right;">
                        <input id="ohc1" type="radio" name="OtherButtons" value="1" />
                        <label for="ohc1" class="yes">Yes</label>
                        <input id="ohc2" type="radio" name="OtherButtons" value="2" />
                        <label for="ohc2" class="no">No</label>
                        <input id="ohc3" type="radio" name="OtherButtons" value="3" />
                        <label for="ohc3" class="noSure">No Sure</label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <textarea id="ohc" name="OtherDescribe" rows="5" style="width:1000px;" class="area"></textarea>
                    </td>
                </tr>

                <tr>
                    <td>
                        <span class="answer">Did you answer "yes" to any of questions 1-32? If so, please comment further on those health conditions below.</span>
                    </td>
                    <td style="float:right">
                        <input id="answer1" type="radio" name="commentButton" value="1" />
                        <label for="answer1" class="yes">Yes</label>
                        <input id="answer2" type="radio" name="commentButton" value="2" />
                        <label for="answer2" class="no">No</label>
                        <input id="answer3" type="radio" name="commentButton" value="3" />
                        <label for="answer3" class="noSure">No Sure</label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <textarea id="answer" name="CommentDescribe" rows="5" style="width:1000px;" class="area"></textarea>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td style="float:right;">
                        <span>(Attach additional sheets if necessary)</span>
                    </td>
                </tr>
            </table>
        </div>
        <div class="title" style="margin-top:-8px;">CMV DRIVER'S SIGNATURE</div>
        <div class="content">
            <table>
                <tr>
                    <td colspan="3">
                        I certify that the above information is accurate and complete. I understand that inaccurate, false or missing information may invalidate the examination
and my Medical Examiner's Certificate, that submission of fraudulent or intentionally false information is a violation of <span style="color:blue;">49 CFR 390.35</span>, and that submission
of fraudulent or intentionally false information may subject me to civil or criminal penalties under <span style="color:blue;">49 CFR 390.37</span> and <span style="color:blue;">49 CFR 386</span> Appendices A and B.
                    </td>
                </tr>
                <tr>
                    <td style="width:600px; text-align:right;">
                        <span class="driverSignature" style="float:left;">Driver's Signature:</span>              
                        <p style="width:300px; min-height:14px; border:0; border-bottom:solid 2px #808080; float:left;"></p>
                    </td>
                    <td class="date" style="text-align:right;">
                        Date
                    </td>
                    <td>
                        <input type="text" name="SignatureDate" class="txt" />
                    </td>
                </tr>
            </table>
        </div>
        <table style="width:1024px;">
            <tr>
                <td style="width:50%; padding-left:15px; vertical-align:bottom;">
                    <span style="font-weight:bolder; vertical-align:bottom;">SECTION 2. Examination Report </span>
                    <span style="font-size:0.8em;">(to be filled out by the medical examiner)</span>
                </td>                
            </tr>                
        </table>
        <div class="title2">
            DRIVER HEALTH HISTORY REVIEW
        </div>
        <div class="content2">
            <table>
                <tr>
                    <td>
                        <span style="font-size:0.8em; color:gray;">
                            Review and discuss pertinent driver answers and any available medical records. Comment on the driver's responses to the "health history" questions that may affect the
driver's safe operation of a commercial motor vehicle (CMV).
                        </span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <textarea rows="5" name="ExaminerComment" class="area2" style="width:983px;"></textarea>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:right; color:#808080; padding-bottom:10px;">
                        (Attach additional sheets if necessary)
                    </td>
                </tr>
            </table>
        </div>
        <div style="  margin-top:10px;">
            <table>
                <tr>
                    <td style="text-align:right; width:1024px;">
                        <span style="padding-right:20px;">Page 2</span>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <!-- Page 3 -->
    <div class="page" id="page3" style="margin-top:5px;">
        <div style="margin:8px 0 3px 0;">
            <span style="padding:3px 700px 3px 10px; font-size:7pt;"><b>Form MCSA-5875</b></span>
            <span  style=" font-size:7pt;">OMB No. 2126-0006 Expiration Date: 12/31/2024</span>
        </div>
        <div style="border:solid 2px #ff0000;margin-left:13px; width:990px; padding:5px 0 2px 0;">
            <table>
                <tr>
                    <td>
                        <span>Last Name:</span>
                        <span><input id="txtLastName3" type="text" class="txt" style="width:240px;" name="LastName3"/></span>
                        <span>First Name:</span>
                        <span><input id="txtFirstName3" type="text" class="txt" style="width:180px;" name="FirstName3"/></span>
                        <span>DOB:</span>
                        <span><input id="txtDOB3" type="text" class="txt" style="width:180px;" name="DOB3"/></span>
                        <span>Exam Date:</span>
                        <span><input type="text" class="txt" style="width:100px;" name="ExamDate3"/></span>
                    </td>
                </tr>
            </table>
        </div>        
        <%-- TESTING --%>
        <div class="title2">
            TESTING
        </div>
        <div class="content2">
            <table style="width:1000px; border-collapse:collapse; ">
                <%-- Row 1 --%>
                <tr>
                     <%-- column left --%>
                    <td style="width:500px;">
                        <table>
                            <tr>                               
                                <td>
                                    <span>Pulse rate:</span>
                                </td>
                                <td>
                                    <input type="text" class="txt" name="PulseRate3" />
                                </td>
                                <td colspan="5">
                                    <span>Pulse rhythm regular:</span>
                                    <input type="radio" name="pulseRhythm13" id="pulseRhythm1" value="1"/>
                                    <label for="pulseRhythm1">Yes</label>
                                    <input type="radio" name="pulseRhythm13" id="pulseRhythm2" value="2"/>
                                    <label for="pulseRhythm2">No</label>
                                </td>
                            </tr>                             
                        </table>
                    </td>
                     <%-- Column right --%>
                    <td style="width:500px; ">
                        <table >
                            <tr>                        
                                <td colspan="2">
                                   <span> Height:</span>
                                    <span><input type="text" class="txt" name="Height3" style="width:20px;" /></span>
                                    <span>feet</span>
                                    <span><input type="text" class="txt" name="Feet3" style="width:20px;" /></span>
                                    <span>inches</span>
                                </td>                                
                                <td colspan="4">
                                    <span>Weight:</span>
                                    <span> <input type="text" name="Weight3" class="txt" /></span>
                                    <span>pounds</span>                                    
                                </td>
                            </tr>                            
                        </table>
                    </td>
                </tr>
                <%-- space line --%>
                <tr>
                    <td colspan="2">
                       <p style="width:985px; height:2px; border-bottom:solid 1px #007c3e; margin:1px 2px 3px 2px;">&nbsp;</p>
                    </td>
                </tr>
                <%-- Row 2 --%>
                <tr>
                    <td>
                         <%-- column left --%>
                        <table style="width:100%;  border-collapse:collapse;">
                            <tr >
                                <td style="width:40%; color:#007c3e; font-weight:bolder; border-bottom:solid 1px #000;">
                                    Blood Pressure
                                </td>
                                <td style="width:30%;border-bottom:solid 1px #000;">
                                    Systolic
                                </td>
                                <td style=" padding-left:5px; width:30%;border-bottom:solid 1px #000;border-left:solid 1px #000;">
                                    Diastolic
                                </td>
                            </tr>
                            <tr>
                                <td style="width:30%;border-bottom:solid 1px #000;">
                                    Sitting
                                </td>
                                <td class="td1" style="width:30%;border-bottom:solid 1px #000;">
                                    <input type="text" class="txt2" name="SittingSystolic3" />
                                </td>
                                <td class="td1" style="width:30%;border-bottom:solid 1px #000;border-left:solid 1px #000;">
                                     <input type="text" class="txt2" name="SittingDiastolic3"/>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:30%;border-bottom:solid 1px #000;">
                                    Second reading (optional)
                                </td>
                                <td class="td1" style="width:30%;border-bottom:solid 1px #000;">
                                     <input type="text" class="txt2" name="SecondSystolic3"  />
                                </td>
                                <td class="td1" style="width:30%;border-bottom:solid 1px #000;border-left:solid 1px #000;">
                                     <input type="text" class="txt2"  name="SecondSitting3"  />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    Other testing if indicated
                                </td>                                
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <textarea rows="2" style="width:100%; background-color:#dee5ff;border:solid 1px #808080;" name="OtherTesting3"></textarea>
                                </td>                                
                            </tr>
                        </table>
                    </td>
                    <td style="vertical-align:top;">
                         <%-- Column right --%>
                        <table style="width:96%; margin-left:3px;  border-collapse:collapse;">
                            <tr>
                                <td style="width:40%; color:#007c3e; font-weight:bolder; border-bottom:solid 1px #000;">
                                    Urinalysis
                                </td>
                                <td style="width:15%;border-bottom:solid 1px #000;">
                                    Sp. Gr.
                                </td>
                                <td style=" padding-left:5px; width:15%;border-bottom:solid 1px #000;border-left:solid 1px #000;">
                                    Protein
                                </td>
                                <td style=" padding-left:5px; width:15%;border-bottom:solid 1px #000;border-left:solid 1px #000;">
                                    Blood
                                </td>
                                <td style=" padding-left:5px; width:15%;border-bottom:solid 1px #000;border-left:solid 1px #000;">
                                    Sugar
                                </td>
                            </tr>    
                            <tr>
                                <td style="width:40%;border-bottom:solid 1px #000;">
                                    Urinalysis is required.<br />Numerical readings <br />must be recorded.
                                </td>
                                <td class="td1" style="width:15%; border-bottom:solid 1px #000;">
                                    <input type="text" class="txt2" style="width:96%;" name="UrinalysisSP3" />
                                </td>
                                <td class="td1" style=" padding-left:5px; width:15%;border-bottom:solid 1px #000;border-left:solid 1px #000;">
                                     <input type="text" class="txt2" style="width:96%;"  name="UrinalysisProtein3" />
                                </td>
                                <td class="td1" style=" padding-left:5px; width:15%;border-bottom:solid 1px #000;border-left:solid 1px #000;">
                                     <input type="text" class="txt2" style="width:96%;" name="UrinalysisBlood3"  />
                                </td>
                                <td class="td1" style=" padding-left:5px; width:15%;border-bottom:solid 1px #000;border-left:solid 1px #000;">
                                     <input type="text" class="txt2" style="width:96%;" name="UrinalysisSugar3"/>
                                </td>
                            </tr>  
                            <tr>
                                <td colspan="5" style="padding-top:5px;">
                                    <span style="font-size:0.8em;">
                                        Protein, blood, or sugar in the urine may be an indication for further testing to rule out any underlying medical problem.
                                    </span>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <%-- space line --%>
                <tr>
                    <td colspan="2">
                       <p style="width:985px; height:2px; border-bottom:solid 1px #007c3e; margin:1px 2px 3px 2px;"></p>
                    </td>
                </tr>
                <%-- Row 3 --%>
                <tr>
                    <%-- Column left --%>
                    <td>
                        <table style="width:100%;  border-collapse:collapse;">
                            <tr>
                                <td colspan="5" style="color:#007c3e; font-weight:bolder; padding-left:3px;">
                                    Vision
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5" style="font-size:0.8em; font-style:italic; padding-left:3px;">
                                    Standard is at least 20/40 acuity (Snellen) in each eye with or without correction. At
least 70° field of vision in horizontal meridian measured in each eye. The use of corrective lenses should be noted on the Medical Examiner's Certificate
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>Acuity</b>
                                </td>
                                <td>
                                    Uncorrected
                                </td>
                                <td>
                                    Corrected
                                </td>
                                <td colspan="2">
                                    Horizontal Field of Vision
                                </td>                                
                            </tr>
                            <tr>
                                <td style="width:30%;">
                                    Right Eye:
                                </td>
                                <td style="width:15%;">
                                    <span>20/</span>
                                    <span><input type="text" class="txt" style="width:30px;" name="RightEyeUncorrected3"/></span>
                                </td>
                                <td style="width:15%;">
                                    <span>20/</span>
                                    <span><input type="text" class="txt" style="width:30px;" name="RightEyeCorrected3"/></span>
                                </td>
                                <td style="width:20%;">
                                    <span>Right Eye:</span>
                                    <span><input type="text" class="txt" style="width:35px;"  name="HorizontalRight3"/></span>                                    
                                </td>
                                <td style="width:20%;">
                                    <span>degrees</span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Left Eye:
                                </td>
                                <td>
                                    <span>20/</span>
                                    <span><input type="text" class="txt" style="width:30px;" name="LeftEyeUncorrected3"/></span>
                                </td>
                                <td>
                                    <span>20/</span>
                                    <span><input type="text" class="txt" style="width:30px;"  name="LeftEyeCorrected3"/></span>
                                </td>
                                <td>
                                    <span>Left Eye:&nbsp;&nbsp;</span>
                                    <span><input type="text" class="txt" style="width:35px;" name="HorizontalLeftEye3" /></span>                                    
                                </td>
                                <td>
                                    <span>degrees</span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Both Eyes:
                                </td>
                                <td>
                                    <span>20/</span>
                                    <span><input type="text" class="txt" style="width:30px;" name="BothEyesUncorrected3"/></span>
                                </td>
                                <td>
                                    <span>20/</span>
                                    <span><input type="text" class="txt" style="width:30px;" name="BothEyesCorrected3"/></span>
                                </td>
                                <td>

                                </td>
                                <td>                                    
                                    <span style="padding:5px 3px 0 3px; font-weight:bolder;">Yes</span>
                                    <span style="padding:5px 3px 0 3px;font-weight:bolder;">No</span>
                                </td>
                                
                            </tr>
                            <tr>
                                <td colspan="4">
                                    Applicant can recognize and distinguish among traffic control signals and devices showing red, green, and amber colors
                                </td>
                                <td>
                                    <span><input type="radio" name="Distinguish3" value="1"/></span>
                                    <span><input type="radio" name="Distinguish3" value="2"/></span>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    Monocular vision
                                </td>
                                <td>
                                    <span><input type="radio" name="Monocular3" value="1" /></span>
                                    <span><input type="radio" name="Monocular3" value="2"/></span>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    Referred to ophthalmologist or optometrist?
                                </td>
                                <td>
                                    <span><input type="radio" name="Referred3" value="1"/></span>
                                    <span><input type="radio" name="Referred3" value="2"/></span>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    Received documentation from ophthalmologist or optometrist?
                                </td>
                                <td>
                                    <span><input type="radio" name="Document3" value="1"/></span>
                                    <span><input type="radio" name="Document3" value="2"/></span>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <%-- Column right --%>
                    <td style="vertical-align:top;">
                        <table style="width:98%;  border-collapse:collapse;">
                            <tr>
                                <td colspan="6" style="color:#007c3e; font-weight:bolder; padding-left:3px;">
                                    Hearing
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6" style="font-size:0.8em; font-style:italic; padding-left:3px;">
                                    Standard: Must first perceive whispered voice at not less than 5 feet OR average hearing loss of less than or equal to 40 dB, in better ear (with or without hearing aid)
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <span>Check if hearing aid used for test:</span>
                                    <span><input type="checkbox" id="rightEar" name="RightEar3"/></span>
                                    <span>Right Ear</span>
                                    <span><input type="checkbox" id="leftEar" name="LeftEar3" /></span>
                                    <span>Left Ear</span>
                                    <span><input type="checkbox" id="NeitherEar" name="NeitherEar3" /></span>
                                    <span>Neither</span>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <b>Whisper Test Results</b>
                                </td>
                                <td>
                                    Right Ear
                                </td>
                                <td>
                                    Left Ear
                                </td>
                            </tr>
                            
                            <tr>
                                <td colspan="4">
                                    Record distance (in feet) from driver at which a forced whispered voice can first be heard
                                </td>
                                <td>
                                    <input type="text" class="txt" style="width:50px;" name="RecordRightEye3" />
                                </td>
                                <td>
                                    <input type="text" class="txt" style="width:50px;" name="RecordLeftEye3"/>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6" style="color:#007c3e; font-weight:bolder; padding-left:3px;">
                                    OR
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <b>Audiometric Test Results</b>
                                </td>                                
                            </tr>
                            <tr>
                                <td colspan="3">
                                    Right Ear
                                </td>
                                 <td colspan="3">
                                     Left Ear
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    500Hz
                                </td>
                                <td>
                                    1000Hz
                                </td>
                                <td>
                                    2000Hz
                                </td>                           
                                <td>
                                    500Hz
                                </td>
                                <td>
                                    1000Hz
                                </td>
                                <td>
                                    2000Hz
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input type="text" style="width:80px;" class="txt" id="txtRight500" name="Right500Hz3" />
                                </td>
                                <td>
                                    <input type="text" style="width:80px;" class="txt" id="txtRight1000" name="Right1000Hz3"/>
                                </td>
                                <td>
                                    <input type="text" style="width:80px;" class="txt" id="txtRight2000" name="Right2000Hz3"/>
                                </td>                           
                                <td>
                                    <input type="text" style="width:80px;" class="txt" id="txtLeft500" name="Left500Hz3"/>
                                </td>
                                <td>
                                    <input type="text" style="width:65px;" class="txt" id="txtLeft1000" name="Left1000Hz3"/>
                                </td>
                                <td>
                                    <input type="text" style="width:65px;" class="txt" id="txtLeft2000" name="Left2000Hz3"/>
                                </td>
                            </tr>
                            <tr style="height:40px;">
                                <td colspan="3">
                                    <span>Average (right): </span>
                                    <span><input type="text" class="txt" style="width:100px;" name="AverageRight3" /></span>
                                </td>
                                <td colspan="3">
                                    <span>Average (left): </span>
                                    <span><input type="text" class="txt" style="width:100px;" name="AverageLeft3" /></span>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            
        </div>
        <%-- PHYSICAL EXAMINATION --%>
        <div class="title2">
            PHYSICAL EXAMINATIO
        </div>
        <div class="content2">
            <table style="width:1000px;">
                <tr>
                    <td colspan="6">
                        The presence of a certain condition may not necessarily disqualify a driver, particularly if the condition is controlled adequately, is not likely to worsen, or
is readily amenable to treatment. Even if a condition does not disqualify a driver, the Medical Examiner may consider deferring the driver temporarily.
Also, the driver should be advised to take the necessary steps to correct the condition as soon as possible, particularly if neglecting the condition could
result in a more serious illness that might affect driving
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        Check the body systems for abnormalities.
                    </td>
                </tr>
                <tr>
                    <td style="width:30%">
                        <b>Body System</b>
                    </td>
                    <td style="width:10%; text-align:center;">
                        Normal
                    </td>
                    <td style="width:10%; text-align:center;">
                        Abnormal
                    </td>
                    <td style="width:30%">
                        <b>Body System</b>
                    </td>
                    <td style="width:10%;text-align:center;" >
                        Normal
                    </td>
                    <td style="width:10%; text-align:center;">
                        Abnormal
                    </td>
                </tr>
                <%-- Body 1 --%>
                 <tr>
                    <td style="width:30%">
                        1. General
                    </td>
                    <td style="width:10%; text-align:center;">
                       <input type="radio" name="General3" value="1" />
                    </td>
                    <td style="width:10%; text-align:center;">
                         <input type="radio" name="General3" value="2" />
                    </td>
                    <td style="width:30%">
                        8. Abdomen
                    </td>
                    <td style="width:10%; text-align:center;">
                         <input type="radio" name="Abdomen3" value="1"/>
                    </td>
                    <td style="width:10%; text-align:center;">
                         <input type="radio" name="Abdomen3" value="2"/>
                    </td>
                </tr>                
               <%-- Body 2 --%>
               <tr>
                    <td style="width:30%">
                        2. Skin
                    </td>
                    <td style="width:10%; text-align:center;">
                       <input type="radio" name="Skin3" value="1" />
                    </td>
                    <td style="width:10%; text-align:center;">
                         <input type="radio" name="Skin3" value="2"/>
                    </td>
                    <td style="width:30%">
                        9. Genito-urinary system including hernias
                    </td>
                    <td style="width:10%; text-align:center;">
                         <input type="radio" name="Hernia3" value="1" />
                    </td>
                    <td style="width:10%; text-align:center;">
                         <input type="radio" name="Hernia3" value="2"/>
                    </td>
                </tr>
                <%-- Body 3 --%>
                 <tr>
                    <td style="width:30%">
                        3. Eyes
                    </td>
                    <td style="width:10%; text-align:center;">
                       <input type="radio" name="Eyes3" value="1"/>
                    </td>
                    <td style="width:10%; text-align:center;">
                         <input type="radio" name="Eyes3" value="2"/>
                    </td>
                    <td style="width:30%">
                        10. Back/Spine
                    </td>
                    <td style="width:10%; text-align:center;">
                         <input type="radio"  name="Back3" value="1"/>
                    </td>
                    <td style="width:10%; text-align:center;">
                         <input type="radio" name="Back3" value="2"/>
                    </td>
                </tr>                
                <%-- Body 4 --%>
               <tr>
                    <td style="width:30%">
                        4. Ears
                    </td>
                    <td style="width:10%; text-align:center;">
                       <input type="radio" name="Ears3" value="1"/>
                    </td>
                    <td style="width:10%; text-align:center;">
                         <input type="radio" name="Ears3" value="2"/>
                    </td>
                    <td style="width:30%">
                        11. Extremities/joints
                    </td>
                    <td style="width:10%; text-align:center;">
                         <input type="radio" name="Joint3" value="1"/>
                    </td>
                    <td style="width:10%; text-align:center;">
                         <input type="radio" name="Joint3" value="2"/>
                    </td>
                </tr>
                 <%-- Body 5 --%>
               <tr>
                    <td style="width:30%">
                         5. Mouth/throat
                    </td>
                    <td style="width:10%; text-align:center;">
                       <input type="radio" name="Mouth3" value="1"/>
                    </td>
                    <td style="width:10%; text-align:center;">
                         <input type="radio" name="Mouth3" value="2"/>
                    </td>
                    <td style="width:30%">
                        12. Neurological system including reflexes
                    </td>
                    <td style="width:10%; text-align:center;">
                         <input type="radio" name="Neuro3" value="1"/>
                    </td>
                    <td style="width:10%; text-align:center;">
                         <input type="radio" name="Neuro3" value="2"/>
                    </td>
                </tr>
                <%-- Body 6 --%>
                 <tr>
                    <td style="width:30%">
                        6. Cardiovascular
                    </td>
                    <td style="width:10%; text-align:center;">
                       <input type="radio" name="Heart3" value="1"/>
                    </td>
                    <td style="width:10%; text-align:center;">
                         <input type="radio" name="Heart3" value="2"/>
                    </td>
                    <td style="width:30%">
                        13. Gait
                    </td>
                    <td style="width:10%; text-align:center;">
                         <input type="radio" name="Gait3" value="1"/>
                    </td>
                    <td style="width:10%; text-align:center;">
                         <input type="radio" name="Gait3" value="2"/>
                    </td>
                </tr>                
                <%-- Body 7 --%>
               <tr>
                    <td style="width:30%">
                       7. Lungs/chest
                    </td>
                    <td style="width:10%; text-align:center;">
                       <input type="radio" name="Chest3" value="1" />
                    </td>
                    <td style="width:10%; text-align:center;">
                         <input type="radio" name="Chest3" value="2" />
                    </td>
                    <td style="width:30%">
                        14. Vascular system
                    </td>
                    <td style="width:10%; text-align:center;">
                         <input type="radio" name="Vascular3" value="1" />
                    </td>
                    <td style="width:10%; text-align:center;">
                         <input type="radio" name="Vascular3" value="2" />
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <span><i>
                            Discuss any abnormal answers in detail in the space below and indicate whether it would affect the driver's ability to operate a CMV.
Enter applicable item number before each comment.
                              </i>
                        </span>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <textarea rows="4" style="width:98%;" id="examComment" name="ExamComment3"></textarea>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" style="text-align:right; font-size:0.8em; font-style:italic; padding-right:20px;">
                        (Attach additional sheets if necessary)
                    </td>
                </tr>
            </table>            
        </div>
        <div style="width:1000px; text-align:right; margin-top:5px;">
            <span>Page 3</span>
        </div>
    </div>
    <!-- Page 4 -->
    <div class="page" id="page4">
        <div style="margin:8px 0 3px 0;">
            <span style="padding:3px 700px 3px 10px; font-size:7pt;"><b>Form MCSA-5875</b></span>
            <span  style=" font-size:7pt;">OMB No. 2126-0006 Expiration Date: 12/31/2024</span>
        </div>
        <div style="border:solid 2px #ff0000;margin-left:13px; width:990px; padding:5px 0 2px 0;">
            <table>
                <tr>
                    <td>
                        <span>Last Name:</span>
                        <span><input id="txtLastName4" name="lastname4" type="text" class="txt" style="width:240px;" /></span>
                        <span>First Name:</span>
                        <span><input id="txtFirstName4"  name="firstname4" type="text" class="txt" style="width:180px;" /></span>
                        <span>DOB:</span>
                        <span><input id="txtDOB4" name="dob4" type="text" class="txt" style="width:180px;" /></span>
                        <span>Exam Date:</span>
                        <span><input type="text" name="examdate4" id="txtExamDate4" class="txt" style="width:100px;"/></span>
                    </td>
                </tr>
            </table>
        </div>
        <div style="margin:5px 0 5px 10px;">
            <span style="color:#bc131a; font-weight:bolder;">Please complete only one of the following (Federal or State) Medical Examiner Determination sections:</span> 
        </div>
        <%-- MEDICAL EXAMINER DETERMINATION (Federal) --%>
        <div class="title2">
            MEDICAL EXAMINER DETERMINATION (Federal)
        </div>
        <div class="content2">
            <table style="width:1000px;">
                <tr>
                    <td>
                       <span style="font-size:0.9em; font-style:italic; color:#bc131a;">
                           Use this section for examinations performed in accordance with the Federal Motor Carrier Safety Regulations 
                       </span>
                        <span style="font-size:0.9em; font-style:italic; color:blue;">
                            (49 CFR 391.41-391.49):
                        </span>                       
                    </td>                    
                </tr>
                <tr>
                    <td>
                       <span><input type="radio" name="MeetStandard4" id="rdoDoNotMeetStandard4" value="1" /></span>
                        <span>Does not meet standards </span>
                        <span style="font-size:0.9em; font-style:italic;" >(specify reason):</span>
                        <input type="text" style="width:740px; border:0; border-bottom:solid 1px #808080;" name="NotStandardsWhy4" id="txtNotStandardsWhy" />
                    </td>
                </tr>
                 <tr>
                    <td>
                       <span><input type="radio" name="MeetStandard4" id="rdoMeetStandard4" value="2"/></span>
                        <span>Meets standards in   </span>
                        <span style="font-size:0.9em; font-style:italic; color:blue;" >49 CFR 391.41;</span>
                        <span>qualifies for 2-year certificate</span>                        
                    </td>
                </tr>
                <tr>
                    <td>
                       <span><input type="radio" name="MeetStandard4" value="3"/></span>
                        <span>Meets standards, but periodic monitoring required</span>
                        <span style="font-size:0.9em; font-style:italic;" >(specify reason):</span>
                        <input type="text" style="width:605px; border:0; border-bottom:solid 1px #808080;" name="MeetStandardButPeriodicWhy4" id="txtButStandardsWhy" />
                    </td>
                </tr>
                <tr>
                    <td style="padding-left:20px;">
                        <span>Driver qualified for: </span>
                        <span>
                            <input type="radio" name="DriverQualified4" value="1" />
                        </span>
                        <span>
                            3 months
                        </span>
                         <span>
                            <input type="radio" name="DriverQualified4" value="2"/>
                        </span>
                        <span>
                            6 months
                        </span>
                         <span>
                            <input type="radio" name="DriverQualified4" value="3"/>
                        </span>
                        <span>
                            1 year
                        </span>
                         <span>
                            <input type="radio" name="DriverQualified4" value="4"/>
                        </span>
                        <span>
                            other
                        </span>
                        <span style="font-size:0.9em; font-style:italic;">
                            (specify):
                        </span>
                        <input type="text" style="width:535px; border:0; border-bottom:solid 1px #808080;" name="DriverQualifiedOtherWhy4" id="txtOtherQualify" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="checkbox" id="chkCorrectLenses" name="CorrectLenses4" />
                        <span>Wearing corrective lenses</span>
                        <input type="checkbox" id="chkHearingAid" name="HearingAid4"/>
                        <span>Wearing hearing aid</span>
                        <input type="checkbox" id="chkWaiverQualify" name="WaiverQualify4"/>
                        <span>Accompanied by a waiver/exemption </span>
                         <span style="font-size:0.9em; font-style:italic;">
                            (specify type):
                        </span>
                        <input type="text" style="width:390px; border:0; border-bottom:solid 1px #808080;" name="WaiverEnter4" id="txtWaiverEnter" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="checkbox" id="chkSpeQualify" name="SpeQualify4"/>
                        <span>Accompanied by a Skill Performance Evaluation (SPE) Certificate </span>
                        <input type="checkbox" id="chkCfrQualify" name="QualifiedOperation4"/>
                        <span>Qualified by operation of </span>
                        <span style="font-size:0.9em; font-style:italic; color:blue;">
                            49 CFR 391.64 (Federal)
                        </span>                        
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="checkbox" id="chkExemptQualify" name="ExemptQualify4"/>
                        <span>Driving within an exempt intracity zone </span>                        
                        <span style="font-size:0.9em; font-style:italic; color:blue;">
                            (see 49 CFR 391.62) (Federal)
                        </span>                        
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="checkbox" id="chkDeterPending" name="DeterPending4"/>
                        <span>Determination pending </span>                        
                        <span style="font-size:0.9em; font-style:italic;">
                            (specify reason):
                        </span>         
                        <input type="text" id="txtPendingWhy" name="PendingWhy4" style="width:750px; border:0; border-bottom:solid 1px #808080;"/>
                    </td>
                </tr>
                <tr>
                    <td style="padding-left:20px;">
                        <input type="checkbox" id="chkReturnExam" name="ReturnExam4"/>
                        <span>Return to medical exam office for follow-up on </span>                        
                        <span style="font-size:0.9em; font-style:italic;">
                            (must be 45 days or less):
                        </span>         
                        <input type="text" id="txtReturnDate" name="ReturnDate4" style="width:200px; border:0; border-bottom:solid 1px #808080;"/>
                    </td>
                </tr>
                <tr>
                    <td style="padding-left:20px;">
                        <input type="checkbox" id="chkReportAmend" name="ReportAmend4"/>
                        <span>Medical Examination Report amended:</span>                        
                        <span style="font-size:0.9em; font-style:italic;">
                             (specify reason):
                        </span>         
                        <input type="text" id="txtAmendWhy" name="AmendWhy4" style="width:643px; border:0; border-bottom:solid 1px #808080;"/>
                    </td>
                </tr>
                <tr style="height:30px;">
                    <td style="padding-left:60px; vertical-align:bottom;">
                       <span style="float:left;">(if amended) Medical Examiner's Signature:</span> 
                        <p style="width:200px; border:0; border-bottom:solid 1px #808080; float:left; height:10px;" id="ifAmendSignature"></p>
                        <span style="float:left;">Date:</span> 
                        <p style="width:200px; border:0; border-bottom:solid 1px #808080; float:left; height:10px;" id="ifAmendDate"></p>
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="checkbox" id="chkExamIncomplete"  name="ExamIncomplete4"/>
                        <span>Incomplete examination </span>
                        <span style="font-size:0.9em; font-style:italic;">(specify reason):</span>
                        <input type="text" id="txtIncompleteWhy" name="IncompleteWhy4" style="width:743px; border:0; border-bottom:solid 1px #808080;"/>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:center;">
                        <p style="width:90%; margin:5px auto; border:solid 2px #bc131a; padding:5px; font-weight:bolder;">
                            If the driver meets the standards outlined in <span style="color:blue;">49 CFR 391.41</span>, 
                            then complete a Medical Examiner's Certificate as stated in 
                            <span style="color:blue">49 CFR 391.43(h)</span>, as appropriate.
                        </p>
                    </td>
                </tr>
                <tr>
                    <td style="padding:5px;">
                        <span >
                            I have performed this evaluation for certification. I have personally reviewed all available records and recorded information pertaining to this evaluation,
and attest that to the best of my knowledge, I believe it to be true and correct.
                        </span>
                        
                    </td>
                </tr>
                <tr>
                    <td style="padding-left:3px;">
                        <span style="float:left;">Medical Examiner's Signature:</span> 
                        <p style="width:200px; border:0; border-bottom:solid 1px #808080; float:left; height:10px;" id="examSignature"></p>
                    </td>
                </tr>
                <tr>
                    <td style="padding-left:3px;">
                        <span>Medical Examiner's Name</span> 
                        <span style="font-size:0.9em; font-style:italic;"> (please print or type):</span>
                        <input type="text" id="txtExamName" class="txt" style="width:500px;" name="ExamName4" />
                    </td>
                </tr>
                <tr>
                    <td style="padding-left:3px;">
                        <span>Medical Examiner's Address:</span>                         
                        <input type="text" id="txtMedicalAddress" class="txt" style="width:400px;" name="MedicalAddress4"/>
                        <span>City:</span>                         
                        <input type="text" id="txtMedicalCity" class="txt" style="width:160px;" name="MedicalCity4"/>
                        <span>State:</span>
                        <select id="selMedicalState4">                            
                        </select>
                        <span>Zip code:</span>                         
                        <input type="text" id="txtMedicalZip" class="txt" style="width:90px;" name="MedicalZip4"/>
                    </td>
                </tr>
                <tr>
                    <td style="padding-left:3px;">
                        <span>Medical Examiner's Telephone Number:</span>
                        <input type="text" id="txtMedicalPhone" style="width:345px;" class="txt" name="MedicalPhone4"/>
                        <span>Date Certificate Signed:</span>
                        <input type="text" id="txtexamDate" style="width:282px;" class="txt" name="ExamDate4"/>
                    </td>
                </tr>
                <tr>
                    <td style="padding-left:3px;">
                        <span>Medical Examiner's State License, Certificate, or Registration Number:</span>
                        <input type="text" id="txtCertNumber" style="width:470px;" class="txt" name="CertNumber4"/>
                        <span>Issuing State:</span>
                        <select id="selIssueState4">
                            
                        </select>
                    </td>
                </tr>
                <tr>
                    <td style="padding-left:3px;">                        
                        <input type="checkbox" id="chkMd" name="MD4"/>
                        <span>MD</span>

                        <input type="checkbox" id="chkDo" name="DO4"/>
                        <span>DO</span>

                        <input type="checkbox" id="chkPhysAssist" name="PhysAssist4"/>
                        <span>Physician Assistant</span>

                        <input type="checkbox" id="chkChiroPractor" name="ChiroPractor4" />
                        <span>Chiropractor</span>

                        <input type="checkbox" id="chkPracNurse" name="PracNurse4"/>
                        <span>Advanced Practice Nurse</span>
                    </td>
                </tr>
                <tr>
                    <td style="padding-left:3px;">                        
                        <input type="checkbox" id="chkOtherPrac" name="OtherPrac4"/>
                        <span>Other Practitioner </span>
                        <span style="font-size:0.9em; font-style:italic;">(specify):</span>
                        <input type="text" id="txtOtherPracSpecify" class="txt" style="width:310px;" name="OtherPracSpecify4"/>
                        
                    </td>
                </tr>
                <tr>
                    <td>
                        <p style="float:left; width:50%;">
                            <span>National Registry Number:</span>
                            <input type="text" class="txt" id="txtNationalRegister" name="NationalRegister4"/>
                        </p>
                        <p style="float:right; text-align:center; border:solid 1px #ff0000; padding:3px;">
                            <span>
                                Medical Examiner's Certificate Expiration Date:
                            </span>
                            <input type="text" class="txt" style="width:80px;" name="CertificateExpiration4"/>
                        </p>
                    </td>
                </tr>
            </table>
        </div>
        <div style="width:1000px; text-align:right; margin-top:5px;">
            <span>Page 4</span>
        </div>
    </div>
    <!-- Page 5 -->
    <div class="page" id="page5">
        <div style="margin:8px 0 3px 0;">
            <span style="padding:3px 700px 3px 10px; font-size:7pt;"><b>Form MCSA-5875</b></span>
            <span  style=" font-size:7pt;">OMB No. 2126-0006 Expiration Date: 12/31/2024</span>
        </div>
        <div style="border:solid 2px #ff0000;margin-left:13px; width:990px; padding:5px 0 2px 0;">
            <table>
                <tr>
                    <td>
                        <span>Last Name:</span>
                        <span><input id="txtLastName5" type="text" class="txt" style="width:240px;" name="LastName5"/></span>
                        <span>First Name:</span>
                        <span><input id="txtFirstName5" type="text" class="txt" style="width:180px;" name="FirstName5"/></span>
                        <span>DOB:</span>
                        <span><input id="txtDOB5" type="text" class="txt" style="width:180px;" name="DOB5"/></span>
                        <span>Exam Date:</span>
                        <span><input type="text" class="txt" style="width:100px;" name="ExamDate5"/></span>
                    </td>
                </tr>
            </table>
        </div>
        <div style="margin:5px 0 5px 10px;">
            <span style="color:#bc131a; font-weight:bolder;">Please complete only one of the following (Federal or State) Medical Examiner Determination sections:</span> 
        </div>
        <%-- MEDICAL EXAMINER DETERMINATION (State) --%>
        <div class="title2">
            MEDICAL EXAMINER DETERMINATION (State)
        </div>
        <div class="content2">
            <table style="width:1000px;">
                <tr>
                    <td>
                       <span style="font-size:0.9em; font-style:italic; color:#bc131a;">
                           Use this section for examinations performed in accordance with the Federal Motor Carrier Safety Regulations 
                       </span>
                        <span style="font-size:0.9em; font-style:italic; color:blue;">
                            (49 CFR 391.41-391.49)
                        </span>   
                        <span style="font-size:0.9em; font-style:italic; color:#bc131a;">
                            with any applicable State variances (which will only be valid for intrastate operations): 
                        </span>
                    </td>                    
                </tr>
                <tr>
                    <td>
                       <span><input type="radio"  name="MeetStandards5" value="1" /></span>
                        <span>Does not meet standards </span>
                        <span style="font-size:0.9em; font-style:italic;" >(specify reason):</span>
                        <input type="text" style="width:740px; border:0; border-bottom:solid 1px #808080;"  name="DonotMeetStandardsWhy" id="DonotMeetStandardsWhy5" />
                    </td>
                </tr>
                 <tr>
                    <td>
                       <span><input type="radio" name="MeetStandards5" value="2" /></span>
                        <span>Meets standards in   </span>
                        <span style="font-size:0.9em; font-style:italic; color:blue;" >49 CFR 391.41;</span>
                        <span>qualifies for 2-year certificate</span>                        
                    </td>
                </tr>
                <tr>
                    <td>
                       <span><input type="radio" name="MeetStandards5" value="3" /></span>
                        <span>Meets standards, but periodic monitoring required</span>
                        <span style="font-size:0.9em; font-style:italic;" >(specify reason):</span>
                        <input type="text" style="width:605px; border:0; border-bottom:solid 1px #808080;" name="MeetStandardsButPeriodicWhy5"  id="txtButStandardsWhy5" />
                    </td>
                </tr>
                <tr>
                    <td style="padding-left:20px;">
                        <span>Driver qualified for: </span>
                        <span>
                            <input type="radio" name="DriverQualified3m5" value="1"/>
                        </span>
                        <span>
                            3 months
                        </span>
                         <span>
                            <input type="radio" name="DriverQualified3m5" value="2"/>
                        </span>
                        <span>
                            6 months
                        </span>
                         <span>
                            <input type="radio" name="DriverQualified1y5" value="3"/>
                        </span>
                        <span>
                            1 year
                        </span>
                         <span>
                            <input type="radio" name="DriverQualified1y5" value="4"/>
                        </span>
                        <span>
                            other
                        </span>
                        <span style="font-size:0.9em; font-style:italic;">
                            (specify):
                        </span>
                        <input type="text" style="width:535px; border:0; border-bottom:solid 1px #808080;" name="OtherQualify5" id="txtOtherQualify5" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="checkbox" id="chkP5CorrectLenses" name="CorrectLenses5"/>
                        <span>Wearing corrective lenses</span>
                        <input type="checkbox" id="chkP5HearingAid" name="HearingAid5" />
                        <span>Wearing hearing aid</span>
                        <input type="checkbox" id="chkP5WaiverQualify" name="WaiverQualify5"/>
                        <span>Accompanied by a waiver/exemption </span>
                         <span style="font-size:0.9em; font-style:italic;">
                            (specify type):
                        </span>
                        <input type="text" style="width:390px; border:0; border-bottom:solid 1px #808080;" name="WaiverEnter5" id="txtWaiverEnter5" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="checkbox" id="chkSpeQualify5" name="SpeQualify5"/>
                        <span>Accompanied by a Skill Performance Evaluation (SPE) Certificate </span>
                        <input type="checkbox" id="chkCfrQualify5" name="QualifyOperation5" />
                        <span>Qualified by operation of </span>
                        <span style="font-size:0.9em; font-style:italic; color:blue;">
                            49 CFR 391.64 (Federal)
                        </span>                        
                    </td>
                </tr>             
                <tr>
                    <td style="text-align:center;">
                        <p style="width:90%; margin:5px auto; border:solid 2px #bc131a; padding:5px; font-weight:bolder;">
                            If the driver meets the standards outlined in <span style="color:blue;">49 CFR 391.41</span>, 
                            then complete a Medical Examiner's Certificate as stated in 
                            <span style="color:blue">49 CFR 391.43(h)</span>, as appropriate.
                        </p>
                    </td>
                </tr>
                <tr>
                    <td style="padding-left:3px;">
                        I have performed this evaluation for certification. I have personally reviewed all available records and recorded information pertaining to this evaluation,
and attest that to the best of my knowledge, I believe it to be true and correct.
                    </td>
                </tr>
                <tr>
                    <td style="padding-left:3px;">
                        <span style="float:left;">Medical Examiner's Signature:</span> 
                        <p style="width:200px; border:0; border-bottom:solid 1px #808080; float:left; height:10px;" id="examSignature5"></p>
                    </td>
                </tr>
                <tr>
                    <td style="padding-left:3px;">
                        <span>Medical Examiner's Name</span> 
                        <span style="font-size:0.9em; font-style:italic;"> (please print or type):</span>
                        <input type="text" id="txtExamName5" class="txt" style="width:500px;" name="ExamName5"/>
                    </td>
                </tr>
                <tr>
                    <td style="padding-left:3px;">
                        <span>Medical Examiner's Address:</span>                         
                        <input type="text" id="txtMedicalAddress5" class="txt" style="width:400px;" name="MedicalAddress5" />
                        <span>City:</span>                         
                        <input type="text" id="txtMedicalCity5" class="txt" style="width:160px;" name="MedicalCity5"/>
                        <span>State:</span>
                        <select id="selMedicalState5">                            
                        </select>
                        <span>Zip code:</span>                         
                        <input type="text" id="txtMedicalZip5" class="txt" style="width:85px;" name="MedicalZip5"/>
                    </td>
                </tr>
                <tr>
                    <td style="padding-left:3px;">
                        <span>Medical Examiner's Telephone Number:</span>
                        <input type="text" id="txtMedicalPhone5" style="width:345px;" class="txt" name="MedicalPhone5"/>
                        <span>Date Certificate Signed:</span>
                        <input type="text" id="txtexamDate5" style="width:282px;" class="txt" name="MedicalDate5"/>
                    </td>
                </tr>
                <tr>
                    <td style="padding-left:3px;">
                        <span>Medical Examiner's State License, Certificate, or Registration Number:</span>
                        <input type="text" id="txtCertNumber5" style="width:470px;" class="txt" name="CertNumber5"/>
                        <span>Issuing State:</span>
                        <select id="selIssueState5">                            
                        </select>
                    </td>
                </tr>
                <tr>
                    <td style="padding-left:3px;">                        
                        <input type="checkbox" id="chkMD5" name="MD5"/>
                        <span>MD</span>

                        <input type="checkbox" id="chkDO5" name="DO5"/>
                        <span>DO</span>

                        <input type="checkbox" id="chkPhysAssist5" name="PhysAssist5"/>
                        <span>Physician Assistant</span>

                        <input type="checkbox" id="chkChiroPractor5" name="ChiroPractor5"/>
                        <span>Chiropractor</span>

                        <input type="checkbox" id="chkPracNurse5" name="PracNurse5"/>
                        <span>Advanced Practice Nurse</span>
                    </td>
                </tr>
                <tr>
                    <td style="padding-left:3px;">                        
                        <input type="checkbox" id="chkOtherPrac5" name="OtherPrac5"/>
                        <span>Other Practitioner </span>
                        <span style="font-size:0.9em; font-style:italic;">(specify):</span>
                        <input type="text" id="txtOtherPracSpecify5" class="txt" name="OtherPracSpecify5" style="width:310px;" />
                        
                    </td>
                </tr>
                <tr>
                    <td>
                        <p style="float:left; width:50%;">
                            <span>National Registry Number:</span>
                            <input type="text" class="txt" id="txtNationalRegister5" name="NationalRegister5" />
                        </p>
                        <p style="float:right; text-align:center; border:solid 1px #ff0000; padding:3px;">
                            <span>
                                Medical Examiner's Certificate Expiration Date:
                            </span>
                            <input type="text" class="txt" style="width:80px;"  name="MedicalEcaminer5"/>
                        </p>
                    </td>
                </tr>
            </table>
        </div>
        <div style="width:1000px; text-align:right; margin-top:5px;">
            <span>Page 5</span>
        </div>
    </div>
    </form>
 
</body>
</html>
