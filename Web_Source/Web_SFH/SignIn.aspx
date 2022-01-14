<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" Inherits="Web_SFH.SignIn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">    
    <title>SANTAFE HEALTH CLINIC</title>
    <link rel="shortcut icon" href="images/ico_90.png" />
    <link href="CSS/icon.css" rel="stylesheet" />
    <link href="CSS/text.css?v=2021082101" rel="stylesheet" />
    <script src="Scripts/jquery-3.6.0.min.js"></script>
    <script type="text/javascript">
        var username = $('#txtUsername');
        var password = $('#txtPassword');
        var error = $('#error');

        $(document).ready(function () {
            username = $('#txtUsername');
            password = $('#txtPassword');
            error = $('#error');
            onClickSignIn();

            $("body").on("keypress", function (e) {
                var code = e.key; // recommended to use e.key, it's normalized across devices and languages
                if (code === "Enter") e.preventDefault();
                if (code === " " || code === "Enter" || code === "," || code === ";") {
                    signIn();
                }

            });
        });
        function onClickSignIn() {
            $('#btnSignin').on('click', function () {

                signIn();
            });
        }

        function signIn() {
            if (checkUserInfo()) {
                $.ajax({
                    type: 'POST',
                    dataType:"JSON",
                    url: 'Handlers/Handler_SignIn.ashx',
                    data: {
                        'username': username.val(),
                        'password': password.val()
                    },
                    success: function (msg) {
                        //console.log(msg);
                        var data = msg;

                        if (data.Status === 'ok') {
                            error.addClass('greenText');
                            window.location = "default.aspx";// data.Link;
                        } else {
                            error.addClass('redText');
                        }

                        error.html(data.Message);

                    },
                    error: function () {
                        error.html("Signin Failed.");
                    }
                });
            }
        }

        function checkUserInfo() {
            var kq = true;       
            if (username.val() === undefined || username.val() === null || username.val() === '') {
                username.focus();
                error.html('Username is not available.');
                error.removeClass('greenText').addClass('redText');
                kq = false;
            }
            else if (password.val() === undefined || password.val() === null || password.val() === '') {
                password.focus();
                error.html('Password is not available.');
                error.removeClass('greenText').addClass('redText');
                kq = false;
            }
            return kq;
        }
    </script>
    <style type="text/css">        
        #container{
            width:600px;
            height:400px;           
            margin-left:30%;
            margin-top:100px;
            background-color:#fff;
            border:solid 1px #e6e6e6;
            border-radius:8px;
            font-size:1em;
        }
        .title{
            
            font-size:1.6em;
            padding-left:5px;
            color:#fff;
            font-weight:bolder;
            background-color:#35792a;
            position:relative;
            margin-bottom:38px;
            padding:1px;
            border:solid 1px #35792a;
            top:0;
            border-top-left-radius:8px;
            border-top-right-radius:8px;
            
        }
        .title p{
            margin:0;
            padding:10px;
        }
        .tab{
            width:500px;
            height:280px;
            margin:auto;
        }        
        .tab input{
            width:90%;
            height:30px;            
            /*padding-left:10px;*/
            font-size:0.95em;
            float:right;
            margin:15px 0 10px 0;
            padding:5px 0 5px 33px;
            border:solid 1px #e6e6e6;
            border-radius:3px;
            /*border:solid 1px #e5e3e3;
            border-radius:3px;*/
        }
        .signin{
            width:168px;
            background-color:#35792a;            
            border:solid 1px #35792a;
            border-radius:5px;
            color:#fff;
            font-size:1.2em;
            padding:10px;
            float:right;
            text-align:center;
        }
        .signin:hover{            
            background-color:#6da331;
            border:solid 1px #6da331;
            color:#fff;
            cursor:pointer;
        }
        #error{
            font-size:0.95em;
            font-style:italic;
            text-align:right;
        }
        .username{
            background:url(../images/ic_user_24_3.png) no-repeat;
            background-position-x:5px; 
            background-position-y:9px;
            background-size:24px 24px;            
        }

        .password{
            background:url(../images/ic_password_24_3.png) no-repeat;
            background-position-x:5px; 
            background-position-y:9px;
            background-size:20px 20px;           
        }
    </style>
    
</head>
<body>    
    
    <div id="container">
        <div class="title">
            <p>Wellcome to Santafe Health Clinic</p> 
        </div>
        <table class="tab">
            
            <tr>
                <td>
                    <input class="username" id="txtUsername" type="text" placeholder="Enter your username" />
                </td>
            </tr>
            <tr>
                <td>
                    <input class="password" id="txtPassword" type="password" placeholder="Enter your password" />
                </td>
            </tr>
            <tr>
                <td>
                    <p id="error">Please enter Your Username, Password.</p>
                </td>
            </tr>
            <tr>
                <td>
                    <p id="btnSignin" class="signin" title="Sign In">Sign in</p>
                </td>
            </tr>
        </table>
    </div>    
</body>
</html>
