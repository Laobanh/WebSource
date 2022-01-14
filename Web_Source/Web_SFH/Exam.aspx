<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Exam.aspx.cs" Inherits="Web_SFH.Exam" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Exam</title>
    <meta content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=5, user-scalable=1" name="viewport"/>
    <script src="Scripts/jquery-3.6.0.min.js"></script>
    <style type="text/css">
        body{
            width:100%;
            height:100%;
            margin:0;
            border:0;
            padding:0;
            background-color:#fff;
            font-size:14px;
        }
        #container{
            width:500px;
            height:400px;
            margin:10% auto;                     
        }
        .button{
            width:220px;
            height:100px;           
            float:left;
            margin:10px;
            border:solid 1px #808080;
            border-radius:5px;
        }
        .exam {
            background: url(images/ic_doctor_100_1.png) no-repeat;
            background-position: 5% 50%;
            background-size: 48px 48px;
            background-color: #35792a;
        }
        .testing {
            background: url(images/ic_flask_64_1.png) no-repeat;
            background-size: 48px 48px;
            background-position:5% 50%;
             background-color:#35792a;
        }
        .exam:hover,.testing:hover{
            background-color:#6da331;
            cursor:pointer;
        }
        .title{
            color:#fff;
            padding:20px 0 0 70px;
            
        }
        #popup {
            width: 100%;
            height: 100%;
            background-color: rgba(0,0,0,0.8);
            position: absolute;
            top: 0;
            left: 0;           
            display: none;           
            z-index: 20;
        }

        .dialog{
            width:360px;
            min-height:100px;
            background-color:#fff;
            margin:10% auto;
            padding:10px;
            border:solid 1px #808080;
            border-radius:5px;
        }
        span.next{
            padding:5px;
            float:right;
            text-decoration:underline;
            font-weight:bolder;           
        }
            span.next:hover {
                color: #35792a;
                cursor: pointer;
            }
        .dialog p {
            margin: 0;
            padding: 5px 5px 20px 5px;
            font-size:1em;
        }
        .dialog div.close {
            width: 24px;
            height: 24px;
            background: url(images/ic_close_24_3.png) no-repeat;
            background-position: 90% 2%;
            background-size: 14px 14px;            
            margin-top:-5px;
           cursor:pointer;
           float:right;
        }
        .error{
            color:#f00;
            font-style:italic;
            font-size:0.9em;
        }
        input{
            padding:5px;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#container').find('div').on('click', function () {
                var id = $(this).attr('id');
                $('#popup').show();
                StepOne(id);
            });

            $('.close').on('click', function () {
                $('#popup').hide();
            });

           
        });

        function StepOne(id) {
            var str = "";
            str += "<p>You choose " + (id === 'exam' ? 'Physical examination' : 'Test') + ".</p>";
            str += "<p><span>Driver's License Number:&nbsp;</span><input type='text' id='txtNumber' /></p>";
            str += "<p><span class='next'>Next</span></p>";
            $('.content').html(str);
            $('#txtNumber').focus();
            NextClick(1);
        }

        function StepTwo(data,id) {
            if (data.Status === "OK") {
                var idata = data.Data;
                var str = "<p>What company are you currently working at?</p>";
                for (var i = 0; i < idata.length; i++) {
                   //console.log(idata[i].Id + "/" + idata[i].Name);
                    str += "<p>";
                    str += "<input type='radio' name='com' value='" + idata[i].Id + "' id='" + i + "' />";
                    str += "<label for='" + i + "' ><b>" + idata[i].Name + "</b></label>";
                    str += "</p>";
                }
                str += "<p><span class='next'>Next</span></p>";
                $('.content').html(str);
                NextClick(2, data.Link);
            }
            else {
                window.location = "Mcsa5875.aspx?lic=" + id;
            }
        }

        function NextClick(step,id) {
            $('.error').html("");
            $('.dialog').find('.next').on('click', function () {
                if (step === 1) {
                    var number = $('#txtNumber');
                    var kq = false;
                    kq = number.val() === "" ? false : true;
                    if (kq)
                        CheckPeople(number.val());
                    else {
                        $('.error').html("Please enter Driver's License Number");
                        number.focus();
                    }
                }
                else {
                    var donorId = $('input[name=com]:checked').val();
                    window.location = "Mcsa5875.aspx?DonorId=" + donorId + "&id=" + id;
                }
                
            });
        }

        function CheckPeople(id) {
            $.ajax({
                url: 'Handlers/Handler_CheckPeople.ashx',
                dataType: 'json',
                data: { id: id },
                success: function (msg) {                    
                    StepTwo(msg, id);
                },
                error: function () {

                }
            });
        }
        
    </script>
</head>
    
<body>
    <div id="container">
        <div class="button exam" id="exam">
            <p class="title">Physical examination</p>
        </div>
        <div class="button testing" id="testing">
            <p class="title">Test</p>
        </div>
    </div>
    <div id="popup">    
        
        <div class="dialog">  
            <div class="close" title="Close"></div>
            <div class="content"></div>
            <div class="error"></div>
        </div>
    </div>
</body>
</html>
