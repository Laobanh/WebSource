<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Web_SFH.MROReports.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SANTAFE HEALTH CLINIC</title>
    <link href="../css/httGirdView.css?v=2021072801" rel="stylesheet" />
    <link href="../CSS/Toolbar.css?v=2021072203" rel="stylesheet" />
    <link href="../css/text.css?v=2021072201" rel="stylesheet" />    
    <script src="../Scripts/jquery-3.6.0.min.js"></script>
    <script src="../js/lib.js?v=2021072201"></script>
    <script src="../js/GridView.js?v=2021072802"></script>
    <script src="../js/MROReportDetailts.js?v=2021072901"></script>
    <script type="text/javascript">
        var obj;
        
        $(document).ready(function () {
            obj = new GridView();
           
            //onCLick Download 
            onClickDownload();

            //onClickSignout
            onClickSignout();

            var d = new Date();
            var searchData = {
                'fYear': d.getFullYear(),
                'fMonth': d.getMonth() + 1 ,
                'fDay': 1,
                'tYear': d.getFullYear() ,
                'tMonth': d.getMonth()+ 1,
                'tDay': d.getDate()
            };
            //searchData = JSON.parse(searchData);
            
            var Grid = {
                url:'../Handlers/Handler_GetMROReports.ashx',
                width: 1600,
                searchData: searchData,
                popup: true,
                columns: [
                    {
                        value:'',
                        field: '',
                        title: '',
                        width: 48,
                        widthPlus:0,
                        sortable: false,
                        filterable: false
                    },
                    {
                        value: 'CollectionDate',
                        field: 'CollectionDate',
                        title: 'COLLECTION DATE',                       
                        width: 161,
                        widthPlus: 0,
                        sortable: true,
                        filterable: true
                    },
                    {
                        value: 'MRODate',
                        field: 'MRODate',
                        title: 'MRO DATE',                       
                        width: 118,
                        widthPlus: 0,
                        sortable: true,
                        filterable: true
                    },
                    {
                        value: 'PatientID',
                        field: 'PatientID',
                        title: 'TEST ID',
                        width: 125,
                        widthPlus: 0,
                        sortable: true,
                        filterable: true
                    },
                    {
                        value: 'LastName',
                        field: 'LastName',
                        title: 'LAST',
                        width: 126,
                        widthPlus: 0,
                        sortable: true,
                        filterable: true
                    },
                    {
                        value: 'FirstName',
                        field: 'FirstName',
                        title: 'FIRST',
                        width: 166,
                        widthPlus: 0,
                        sortable: true,
                        filterable: true
                    },
                    {
                        value: 'AlternateID',
                        field: 'AlternateID',
                        title: 'DONOR ID',
                        width: 151,
                        widthPlus: 0,
                        sortable: true,
                        filterable: true
                    },
                    {
                        value: 'CompanyName',
                        field: 'CompanyName',
                        title: 'COMPANY',
                        width: 195,
                        widthPlus: 0,
                        sortable: true,
                        filterable: true
                    },
                    {
                        value: 'Result', //cell value
                        field: 'Result',       //Cell ID
                        title: 'TEST RESULT', //Head cell title
                        width: 139,
                        widthPlus: 0,
                        sortable: true,
                        filterable: true,
                        colorChange: true //Change cell value color
                    },
                    {
                        value: 'TestReason',
                        field: 'TestReason',
                        title: 'TEST REASON',
                        width: 167,
                        widthPlus: 0,
                        sortable: true,
                        filterable: true
                    },
                    {
                        value: 'TestType',
                        field: 'TestType',
                        title: 'MODE',
                        width: 97,
                        widthPlus: 0,
                        sortable: true,
                        filterable: true
                    },
                    {
                        value:'',
                        field: '',
                        title: '',
                        width: 92,
                        widthPlus:0,
                        sortable: false,
                        filterable: false,
                        hidden: true,
                        template: "<p id='pdf#PatientID#' class='pdf'></p><p id='vie#PatientID#'  class='detail'></p>"
                        
               
                    },
                    {
                        field: '',
                        title: '',
                        width: 15,
                        widthPlus: 0,             
                        sortable: false,
                        filterable: false
                    }

                ]
            };   
           

            obj.Grid = Grid;

            obj.Init();

            console.log(obj.Grid);

            InitSearch(obj);
        });

        //ReportDetailts
        function Details(data, id) {
            var details = new MROReportDetailts();
            details.FillDetailReport('#dialog', data, id);
            
        }

        function OpenPDF(data, value) {
            var iData = data.find(x => x.PatientID === value);     
            var link = '../' + iData.ReportBinary.Name;
            window.open(link, '_blank');
        }

         //Signout
        function Signout() {
            window.location = '../signout.aspx';
        }

        function onClickSignout() {
            $('.signout').on('click', function () {
                Signout();
            }); 
        }

         //Download file FTP Server
        var t, i = 0, maxCount = 0, isDownloading = false;
        function DownloadFile() {
             $.ajax({
                type: 'POST',
                url: '../Handlers/Handler_SFTPDownload.ashx',
                data: "",
                success: function (msg) {                        
                    $('#notifi').html(msg);  
                     $('#count').html('');
                    clearInterval(t);
                    
                    i = 0;
                    setTimeout(function () {
                        $('#notifi').html('Click here download MRO Report file.');                        
                    }, 3000);
                },
                error: function () {
                    $('#notifi').html("Download MRO Report file Failed...");
                }
            });
        }
        function Count() {          
            
            t = setInterval(function () {
                i++;

                if (i > maxCount) {                    
                    $('#notifi').html('Please wait, file is loading ');
                    $('#count').html(RetTime(i - maxCount));
                   
                }                
            }, 1000);
            
        }
        function onClickDownload() {
            $('.download').on('click', function () {                
                if (!isDownloading) {
                    Count();
                    this.title = "Cancel download";
                    DownloadFile();
                }
                else {
                    clearInterval(t);
                    $('#count').html('');
                    $('#notifi').html("Click here download MRO Report file.");
                    this.title = "Start download.";
                }
                isDownloading = !isDownloading;
            });
        }
        function RetTime(second) {
            second = parseInt(second, 0);

            var str = "";

            var dd = 0;

            var MM = 0;

            var HH = 0;

            var mm = 0;

            if (second > 59) {
                dd = Math.floor(second / 8640);

                second -= dd * 8640;

                HH = Math.floor(second / 3600);

                second -= HH * 3600;

                mm = Math.floor(second / 60);

                second -= mm * 60;

                str += dd > 0 ? dd + " Day " : "";

                str += HH > 0 ? (HH > 9 ? HH : "0" + HH) + ":" : "";

                str += mm > 0 ? (mm > 9 ? mm : "0" + mm) + ":" : "";

                str += second > 9 ? second : "0" + second;




            }
            else
                str += (second > 0 ? second : '');

            return str;
        }


        //Search
        var firstChar = '', dateId = '', arrayDate = ['Year', 'Month', 'Day'], arrayDateType = ['f', 't'];
        function InitSearch(o) {
            //OnclickDate
            onClickDate(o);

            SetDateDefault();
        }

        function onClickDate(o) {           
            $('.toolbar').find('p.dateSearch').bind('click', function () {

                o.CleanSort();

                firstChar = '';

                var element = $(this);
                var datePanel = $('#date');
                var top = element.offset().top
                var left = element.offset().left;
                dateId = element.attr('id');

                firstChar = dateId.substr(0, 1);
                if (firstChar === 'S') {
                    HideElement(datePanel);                    
                    OnSearchClick(o);
                } else if (firstChar === 'A') {
                    o.LoadData("");
                }
                else {
                    ShowElement(datePanel, left, top);
                }
                console.log('id: ' + element.attr('id') + ' top: ' + top + ' left: ' + left);
            });
            
        }

        function onCickDatePanel() {
            $('#date').find('li').on('click', function () {
                console.log($(this).html())
                var element = $(this);

                var value = parseInt(element.html());

                if (firstChar !== 'S') {
                    var sDate = dateId.substr(1);

                    var year = $('#' + firstChar + 'Year');
                    var month = $('#' + firstChar + 'Month');
                    var day = $('#' + firstChar + 'Day');
                    switch (sDate) {
                        case 'Month':
                            firstChar === 'f' ? fMonth = value : tMonth = value;
                            month.html(value);
                            day.html(1);
                            break;
                        case 'Year':
                            firstChar === 'f' ? fYear = value : tYear = value;
                            year.html(value);
                            day.html(1);
                            break;
                        case "Day":
                            firstChar === 'f' ? fDay = value : tDay = value;
                            day.html(value);
                            break;
                    }

                    HideElement('#date');

                }
                

            });
            
        }

        function ShowElement(element, left, top, ) {

            element.offset({ left: left - 4, top: top - 6 });
            element.show();
            element.html(CreateDatePanel());

            //Register OnClick DatePanel event
            onCickDatePanel();            
        }

        function HideElement(element) {
            if (!$(element).is(':hidden')) {
                //$(element).hide();
                $(element).offset({ top: 0, left: 0 }).hide();
               
            }
        }

        function CreateDatePanel() {
            var k = 0, years = new Date().getFullYear(), months = 12, days = 31;
            
            var i = 0;
            var str = "<ul class='cells'>";
            var firstChar = dateId.substr(0, 1);
            var sDate = dateId.substr(1);

            var year = parseInt($('#' + firstChar + 'Year').html());
            var month = parseInt($('#' + firstChar + 'Month').html());
            switch (sDate) {
                case 'Month':
                    k = 12;
                    i = 0;
                    break;
                case 'Year':
                    k = years;
                    i = years - 5;
                    break;
                case "Day":
                    k = GetDaysOfMonth(year, month);
                    i = 0;
                    break;
            }
            for (i ; i < k; i++) {
                str += "<li>";
                str += i + 1;
                str += "</li>";
            }
            str += "</ul>";
            return str;
        }

        function GetDaysOfMonth(year, month) {
            var days = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
            var leapYear = year % 4 === 0 ? true : false;
            if (leapYear && month === 2)
                return days[month - 1] + 1;
            else
                return days[month - 1];
        }

        function SetDateDefault() {
            var str = ['f', 't'];
            var d = new Date();
            str.forEach(function (s) {
                $('#' + s + 'Year').html(d.getFullYear());
                $('#' + s + 'Month').html(d.getMonth() + 1);
                $('#' + s + 'Day').html((s === 'f' ? 1 : d.getDate()));
            });
        }

        function OnSearchClick(o) {
            var datas = new Object();           
            var s, d;
            for (i = 0; i < arrayDateType.length; i++) {
                s = arrayDateType[i];
                for (k = 0; k < arrayDate.length; k++) {
                    d = arrayDate[k];                    
                    var key = s + d;
                    var v = parseInt($('#' + key).html());
                    datas[key] = v;
                    //console.log('id: ' + s + d + 'value: ' + v);
                }                
            }         

            var fDate = new Date(datas.fYear, datas.fMonth - 1, datas.fDay);
            var tDate = new Date(datas.tYear, datas.tMonth - 1, datas.tDay);
            var curDate = new Date();
            if (fDate > tDate) {
                //alert('Please select from date must be less than date.');
                o.ShowHidePopup(true);
                AlertMessage("#dialog", "Error message!", "Please select from date must be less than date.");
            }
            else if (fDate > curDate || tDate > curDate) {
                //alert('Please select from and to a date that is less than the current date.');
                o.ShowHidePopup(true);
                AlertMessage("#dialog", "Error message!", "Please select from and to a date that is less than the current date.");
            }
            else {
                console.log(datas);
                o.LoadData(datas);
            }
               
        }

        function AlertMessage(element, error, message) {
            var str = "<p class='title redText'>";
            str += error;
            str += "</p>";
            str += "<p class='alert'>";
            str += message;
            str += "</p>";
            str += "</table>";
            $(element).html(str);
        }
    </script>
    <style type="text/css">
        
        
        .download{      
            clear:both;
            margin-top:20px;
            margin-left:5px;
            border:solid 1px #0a5dac;
            border-radius:8px;
            background-color:#fff;
            width:300px;
            height:30px;
            text-align:center;
            padding:10px 0 0 5px;
            position:relative;
            top:10px;
        }
         .download:hover{
            cursor:pointer;           
            text-decoration:underline;
            font-weight:bolder;
            
        }
        .download p{
            float:left;           
            color:#0a5dac;
            
        }
        #popup {
            width: 100%;
            background-color: #c1baba;
            position: absolute;
            top: 0;
            left: 0;
            margin: auto;
            display: none;
            opacity: 1;
            z-index: 0;           
        }
        .close{
            width:80px;
            height:80px;           
            background:url('../images/ic_close_48.png') center no-repeat;
            cursor:pointer;
            float:right;
            margin-right:100px;
            
        }
        .dialog{
            width:800px;
            height:800px;
            /*background-color: #f00;*/           
            top: 0;
            left: 0;
            margin: auto;

        }
        .dialog table{
            width:100%;   
            min-height:600px;
            padding:15px;
            background-color:#fff;
            border:solid 1px #fff;
            border-radius:5px;
            overflow:auto;
            font-size:1em;   
             margin-top:50px;
        }
        .dialog td{
            text-align:left;
            display:table-cell;
            vertical-align:top;        
            width:500px;
        }
        .dialog .title{
            font-size:1.6em;
            font-weight:bolder;           
        }
        .dialog p{                       
            padding:2px;
            vertical-align:top;
            width:100%;
        }
            .dialog p.info {
                padding-top: 15px;
                font-style: italic;
                font-weight: bold;
                text-decoration: underline;
                font-size: 1.2em;
            }
            .dialog p.value {
                padding-left: 10px;
                font-size: 0.9em;
            }

            .dialog span.label{                
                padding-right:15px;
            }
            .dialog span.text{               
                padding-right:15px;                
            }           
            .dialog p.alert{
                width:300px;                
                background-color:#fff;
                border:solid 1px #f00;
                border-radius:8px;
                padding:20px;
                font-size:1.2em;
            }            
    </style>
</head>
    <body>    
        <div id="search">
            <table>            
                <tr>
                    <td colspan="2" >
                        <input class="keyword" type="text" id="keyword" placeholder="Enter key word" />
                    </td>
                </tr>
                <tr>
                    
                    <td>
                        <button id="submit">Submit</button>
                    </td>
                    <td>
                        <button id="cancel">Cancel</button>
                    </td>
                </tr>
            </table>
        </div>
        <div id="toolbar">
            <div>
                <p class="title">
                     MRO Report list
                </p>                
            </div>  
            <div class="toolbar" >
                <div id="date">
                    
                </div>
                <table class="search">
                    <tbody>
                        <tr>                                                      
                            <td style="width:100px;"> From month:</td>
                            <td class="date" title="Select Motnh"><p id="fMonth" class="dateSearch">1</p></td>
                            <td>day:</td>
                            <td class="date" title="Select Day"><p id="fDay" class="dateSearch">1</p></td>
                            <td>year:</td>
                            <td class="date" title="Select Year"><p id="fYear" class="dateSearch">2021</p></td>
                            <td></td>                            
                            <td style="width:100px;">To month:</td>
                            <td class="date" title="Select Motnh"><p id="tMonth" class="dateSearch">1</p></td>
                            <td>day:</td>
                            <td class="date" title="Select Day"><p id="tDay" class="dateSearch">1</p></td>
                            <td>year:</td>
                            <td class="date" title="Select Year"><p id="tYear" class="dateSearch">2021</p></td>
                            <td><p id="Search" class="dateSearch search">Search</p></td>
                            <td><p id="AllDataLoad" class="dateSearch search">Load All Data</p></td>
                        </tr>
                    </tbody>
                </table>
            </div>           
        </div>
        <div id="httGridview" class="httgridview">
            <div id="header">            
            </div>
            <div id="content">
           
            </div>
            <div id="footer">
                <div class="lfooter">
                
                </div>
                 <div class="rfooter">
                     <table>
                         <tr>
                             <td>
                             
                             </td>
                             <td>
                                 <p id="startPoint"></p>
                             </td>
                             <td>
                                 -
                             </td>
                             <td>
                                 <p id="endPoint"></p>
                             </td>
                             <td>
                                 <p>of</p>                                 
                             </td>
                             <td>
                                 <p id="sumRow"></p>
                             </td>
                             <td>
                                 <p>items</p>                                 
                             </td>
                         </tr>
                     </table>
                 </div>
            </div>
        </div>  
        <div class="download" title="Start download">
            <p id="notifi">Click here download MRO Report file.</p><p id="count"></p>        
        </div>
        <div id="popup">
            <p class="close"></p>
            <div class="dialog" id="dialog">    
           
            </div>
        </div>
    </body>
</html>