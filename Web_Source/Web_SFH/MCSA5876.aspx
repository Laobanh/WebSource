<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MCSA5876.aspx.cs" Inherits="Web_SFH.MCSA5876" %>
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.5.3/jspdf.min.js"></script>
<script type="text/javascript" src="https://html2canvas.hertzen.com/dist/html2canvas.js"></script>
<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Form MCSA 5876</title>
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
            GetStateFullName();
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

        function printPdf() {
            var HTML_Width = $("#frmIndex").width();
            var HTML_Height = $("#frmIndex").height();
            var top_left_margin = 15;
            var PDF_Width = HTML_Width + (top_left_margin * 3);
            var PDF_Height = (PDF_Width * 1.5) + (top_left_margin * 2);
            var canvas_image_width = HTML_Width;
            var canvas_image_height = HTML_Height;

            var totalPDFPages = Math.ceil(HTML_Height / PDF_Height) - 1;

            html2canvas($("#frmIndex")[0]).then(function (canvas) {
                var imgData = canvas.toDataURL("image/jpeg", 1.0);
                var pdf = new jsPDF('p', 'pt', [PDF_Width, PDF_Height]);
                pdf.addImage(imgData, 'JPG', top_left_margin, top_left_margin, canvas_image_width, canvas_image_height);
                for (var i = 1; i <= totalPDFPages; i++) {
                    pdf.addPage(PDF_Width, PDF_Height);
                    pdf.addImage(imgData, 'JPG', top_left_margin, -(PDF_Height * i) + (top_left_margin * 4), canvas_image_width, canvas_image_height);
                }
                pdf.save("print.pdf");
            });
        }


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

        function GetStateFullName() {
            $.getJSON('data/states_hash.json', function (data) {
                var str = "";
                $.each(data,function (i, val) {
                     var sel = "selected='selected'";
                    str += "<option value='" + val + "' " + (val === 'CA' ? sel : '') + ">" + val + "</option>";
                });
                //Object.keys(data).map(function (v) {
                    
                //    var sel = "selected='selected'";
                //    str += "<option value='" + v + "' " + (v === 'CA' ? sel : '') + ">" + v + "</option>";
                //    //str += "<option value='" + v + "' " + ">" + v + "</option>";
                //});

                $('#slIssuingStateProvince').html(str);               
                $('#slIssuingState').html(str);               

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

                $('#slStateProvince').html(str);

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

        String.format = function () {
            var s = arguments[0];
            for (var i = 0; i < arguments.length - 1; i++) {
                var reg = new RegExp("\\{" + i + "\\}", "gm");
                s = s.replace(reg, arguments[i + 1]);
            }

            return s;
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
            $('html').find(':input').not(':button, :submit, :reset, :hidden, :checkbox, :radio').val('');
            $('html').find(':checkbox, :radio').prop('checked', false);
            GetState();
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
            dataForm["DriverState"] = $("#selDriverState :selected").val();
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
                success: function (data) {
                    var obj = JSON.parse(data);
                    alert(obj.Message)

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
                    if (data == 'null') {
                        alert(String.format('Not found data with medical record {0}', id));
                        return;
                    }
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
            width: 1120px;            
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
            .name-check
            {
                    position: relative;
                    top: -3px;
            }
    </style>
</head>
<body>  
    <div id="toolbar">
        <p id="printPdf" onclick="printPdf();" >PDF</p>
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
    <!-- Page 5 -->
    <div class="page" id="page5">
        <div class="logo">
            <img src="Images/5876_header.png" style="width:1000px; height:120px; text-align:center;" />
        </div>
        <div style="margin:8px 0 3px 0;">
            <span style="padding:3px 700px 3px 10px; font-size:7pt;"><b>Form MCSA-5876</b></span>
            <span  style=" font-size:7pt;">OMB No. 2126-0006 Expiration Date: 12/31/2024</span>
        </div>
        <div style="border:solid 2px #ff0000;margin-left:13px; width:990px; padding:5px 0 2px 0;width:98%">
            <table>
                <tr>
                    <td style="padding-left:8px;">
                        <h3 style="color:#bc131a; font-weight:bolder;">CMV DRIVER CERTIFICATION</h3>
                    </td>
                </tr>
                <tr>
                    <td style="padding-left:8px;">
                        <span> I certify that I have examined </span> <span style="font-weight:bold;"><i>(last Name)</i> </span>
                        <span><input id="txtLastName" type="text" class="txt" style="width:240px;" name="LastName"/></span>
                        <span style="font-weight:bold;"><i>(first Name)</i> </span>
                        <span><input id="txtFirstName" type="text" class="txt" style="width:180px;" name="FirstName"/> in accordance with (please check only one): </span>

                    </td>
                </tr>
                <tr>
                    <td>
                        <span><input type="radio" name="Federal" value="1" /></span>
                        <span>the Federal Motor Carrier Safety Regulations   </span>
                        <span style="font-size:0.9em; font-style:italic; color:blue;" > (49 CFR 391.41-391.49)</span>
                        <span>and, with knowledge of the driving duties, I find this person is qualified, and, if applicable, only when</span>
                        <i> (check all that apply)</i>
                        <span style="color:#bc131a; font-weight:bolder;"><i>OR</i></span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span><input type="radio" name="Federal" value="2" /></span>
                        <span>the Federal Motor Carrier Safety Regulations </span>
                        <span style="font-size:0.9em; font-style:italic; color:blue;" > (49 CFR 391.41-391.49)</span>
                        <span>with any applicable State variances (which will only be valid for intrastate operations), and, with knowledge of the driving duties,</span>
                        <span style="padding-left:24px;"> I find this person is qualified, and, if applicable, only when</span>
                        <i> (check all that apply)</i>
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="checkbox"  name="WearingCorrective"/>
                        <span>Wearing corrective lenses</span>
                        <input type="checkbox"  name="Waiver"/>
                        <span>Accompanied by a waiver/exemption </span>
                        <span style="font-size:0.9em; font-style:italic;">
                            (specify type):
                        </span>
                        <input type="text" style="width:250px; border:0; border-bottom:solid 1px #808080;" name="WaiverEnter"  />
                        <span><input type="checkbox"  name="DrivingWithin"/>
                        <span style="">Driving within an exempt intracity zone         
                        <span style="font-size:0.9em; font-style:italic; color:blue;" > (49 CFR 391.62)</span> <span style="font-size:0.9em; font-style:italic;">(Federal)</span> </span></span>
               
                    </td>
                </tr>
                <tr>
                    <td style="float:left">
                        <input type="checkbox"  name="WearingHeading"/>
                        <span>Wearing hearing aid</span>
                        <input type="checkbox"  name="SkillPerformance"/>
                        <span>Accompanied by a Skill Performance Evaluation (SPE) Certificate</span>
                    </td>
                    <td style="float:right;padding-right:86px;">
  
                        <span><input type="checkbox"  name="QualifiedBy"/>
                        <span>Qualified by operation of <span style="font-size:0.9em; font-style:italic; color:blue;" > (49 CFR 391.62)</span><span style="font-size:0.9em; font-style:italic;">(Federal)</span></span></span>
                    </td>
                </tr>
                <tr>
                    <td style="float:right;padding-right:108px;">
                        <span><input type="checkbox"  name="GrandfatheredFrom"/>
                        <span>Grandfathered from State requirements <span style="font-size:0.9em; font-style:italic;">(State)</span></span></span>
                    </td>
                </tr>
                <tr>
                    <td style="float: left;width: 63%; padding-left: 5px;padding-top: 20px;">
                        <span style="font-size:1em; font-style:italic;">
                            The information I have provided regarding this physical examination is true and complete. A complete Medical Examination Report Form, MCSA-5875, with any attachments, embodies my findings completely and correctly, and is on file in my office.
                        </span>
                    </td>
                    <td style="float: left;padding-top: 20px;padding-left: 29px;">
                             <span style="color:#bc131a; font-weight:bolder;font-size:12px;">
                                Medical Examiner's Certificate Expiration Date:
                            </span>
                            <br />
                           <p style="float:left; text-align:center; border:solid 1px #ff0000; padding:3px;">
              
                            <input type="text" class="txt" style="width:232px;" name="MedicalEcaminer"/>
                            </p>
                    </td>
                </tr>
            </table>
        </div>
        <div style="border:solid 2px #3f5ea7;margin:15px 0px 0px 13px; width:990px; padding:5px 0 2px 0;width:98%">
            <table>
                <tr>
                     <td style="padding-left:8px;">
                        <h3 style="color:#3f5ea7; font-weight:bolder;">MEDICAL EXAMINER INFORMATION</h3>
                    </td>
                </tr>
                <tr>
                    <td style="padding-left:8px;">
                        <span style="font-weight:bold">Medical Examiner’s Signature</span>
                    </td>
                    <td style="padding-left:5px;">
                       <span style="font-weight:bold;">Medical Examiner’s Telephone Number</span>
                    </td>
                     <td style="padding-left:5px;">
                       <span style="font-weight:bold;">Date Certificate Signed</span>
                    </td>
                </tr>
                <tr>
                  <td style="padding-left:8px;">
                   <span style="width:470px; min-height:28px; border:0; border-bottom:solid 2px #808080; float:left;"></span>
                  </td>
                    <td style="padding-left:5px;">
                         <span><input id="ExaminerTelephone" type="text" class="txt" style="width:290px;" name="ExaminerTelephone"/></span>
                    </td>
                     <td style="padding-left:5px;">
                         <span><input id="DateCertificate" type="text" class="txt" style="width:290px;" name="DateCertificate"/></span>
                    </td>
                </tr>
                <tr>
                    <td style="padding-left:8px;">
                        <span style="font-weight:bold">Medical Examiner’s Name </span>
                    </td>
                    <td style="margin-left: 8px;" colspan="2">
                          <span>
                               <input type="radio" name="MedicalExaminerInfomation1" value="1" id="MD" />
                               <span class="name-check">MD</span>
                          </span>
                          <span style="margin-left:20px">
                               <input type="radio" name="MedicalExaminerInfomation1" value="2" id="PhysicianAssistant" />
                               <span class="name-check">Physician Assistant</span>
                          </span>

                           <span style="margin-left:20px">
                               <input type="radio" name="MedicalExaminerInfomation1" value="3" id="AdvancedPracticeNurse" />
                               <span class="name-check">Advanced Practice Nurse</span>
                          </span>
                         
                    </td>
                    
                </tr>
                <tr>
                    <td style="padding-left:8px;">
                        <span><input id="ExaminerName" type="text" class="txt" style="width:470px;" name="ExaminerName"/></span>
                    </td>
                     <td style="margin-left: 8px;" colspan="2">
                          <span>
                               <input type="radio" name="MedicalExaminerInfomation2" value="1" id="DO" />
                               <span class="name-check">DO</span>
                          </span>
                          <span style="margin-left:22px">
                               <input type="radio" name="MedicalExaminerInfomation2" value="2" id="Chiropractor" />
                               <span class="name-check">Chiropractor</span>
                          </span>

                           <span style="margin-left:55.1px">
                               <input type="radio" name="MedicalExaminerInfomation2" value="3" id="OtherPractitioner" />
                               <span class="name-check">Other Practitioner (specify)</span>
                           
                          </span>
                           <span style="position: relative;">
                               
                               <span style="width: 196px;
                                min-height: 28px;
                                border: 0;
                                border-bottom: solid 2px #808080;
                                position: absolute;
                                left: 5px;
                                top: -18px;"></span>
                           
                          </span>
                    </td>
                </tr>
                    <tr>
                    <td style="padding-left:8px;">
                        <span style="font-weight:bold">Medical Examiner’s State License, Certificate, or Registration Number </span>
                    </td>
                         <td style="padding-left:8px;">
                        <span style="font-weight:bold">Issuing State</span>
                    </td>
                         <td style="padding-left:8px;">
                        <span style="font-weight:bold">National Registry Number</span>
                    </td>
                </tr>
                <tr>
                    <td style="padding-left:8px;">
                        <span><input id="ExaminerState" type="text" class="txt" style="width:470px;" name="ExaminerState"/></span>
                    </td>
                     <td style="padding-left:8px;">
                        <span>
                            <select id="slIssuingState" style="width:288px;height: 27px;">
                            </select>

                        </span>
                    </td>
                     <td style="padding-left:8px;">
                        <span>
                            <input type="text" name="NationalRegistryNumber" class="txt" style="width:290px;"  id="NationalRegistryNumber"/>
                        </span>
                    </td>
                </tr>
            </table>
        </div>
      <div style="border:solid 2px #3f5ea7;margin:15px 0px 0px 13px; width:990px; padding:5px 0 2px 0;width:98%">
            <table>
                <tr>
                     <td style="padding-left:8px;">
                        <h3 style="color:#3f5ea7; font-weight:bolder;">CMV DRIVER INFORMATION</h3>
                    </td>
                </tr>
                <tr>
                    <td style="padding-left:8px;">
                        <span style="font-weight:bold">Driver’s Signature</span>
                    </td>
                    <td style="padding-left:5px;">
                       <span style="font-weight:bold;">Driver’s License Number</span>
                    </td>
                     <td style="padding-left:5px;">
                       <span style="font-weight:bold;">Issuing State/Province</span>
                    </td>
                </tr>
                <tr>
                      <td style="padding-left:8px;">
                       <span style="width:470px; min-height:28px; border:0; border-bottom:solid 2px #808080; float:left;"></span>
                      </td>
                        <td style="padding-left:5px;">
                             <span><input id="DriverLicenseNumber" type="text" class="txt" style="width:290px;" name="DriverLicenseNumber"/></span>
                        </td>
                         <td style="padding-left:5px;">
                             <span>
                                 <select id="slIssuingStateProvince" style="width: 294px;height: 27px;">
                                   
                                 </select>
                              </span>
                        </td>
                </tr>
               <tr>
                    <td style="padding-left:8px;" colspan="2">
                        <span style="font-weight:bold">Driver’s Address</span>
                    </td>
                     <td style="padding-left:5px;padding-left: 5px;position: absolute;right: 197px;width: 200px;">
                       <span style="font-weight:bold;">CLP/CDL Applicant/Holder</span>
                    </td>
                </tr>
                 <tr>
                      <td style="padding-left:8px;" colspan="3">
                       <span>Street Address:</span>
                          <input id="StreetAddress" type="text" class="txt" style="width:350px;" name="StreetAddress"/>
                          <span>City:</span>
                          <input id="City" type="text" class="txt" style="width:160px;" name="City"/>
                           <span>State/Province:</span>
                          <select id="slStateProvince" style="width:45px">
                             
                          </select>
                           <span>Zip Code:</span>
                           <input id="ZipCode" type="text" class="txt" style="width:70px;" name="ZipCode"/>
                         
                      </td>
                       
                         <td style="position:absolute;right: 301px; width: 100px;padding: 5px 3px;">
                             <span><input type="radio" name="CLPCDL" value="1" /></span>
                             <span class="name-check">Yes</span>
                             <span><input type="radio" name="CLPCDL" value="2"/></span>
                              <span class="name-check">No</span>
                        </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
 
</body>
</html>
