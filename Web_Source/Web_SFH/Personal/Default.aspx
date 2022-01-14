<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Web_SFH.Personal.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SANTAFE HEALTH CLINIC</title>
    <link href="../css/httGirdView.css?v=2021072901" rel="stylesheet" />  
    <link href="../css/text.css?v=2021072901" rel="stylesheet" />    
    <link href="../CSS/Toolbar.css?v=2021072901" rel="stylesheet" />
    <link href="../CSS/modify.css?v=2021072901" rel="stylesheet" />
    <script src="../Scripts/jquery-3.6.0.min.js"></script>
    <script src="../js/lib.js?v=2021072901"></script>
    <script src="../js/GridView.js?v=2021072901"></script>
    <script src="../js/FieldKeys.js?v=2021072901"></script>
    <script src="../js/Personal.js?v=2021072901"></script>
    <script src="../js/StateCity.js?v=2021072901"></script>
    <script src="../js/ModeCategory.js?v=2021072901"></script>
    <script type="text/javascript">
        var gridView;
        $(document).ready(function () {
            
            gridView = new GridView();       

            //onClickSignout
            onClickSignout();

            var d = new Date();

            var searchDate = {
                'fYear': d.getFullYear(),
                'fMonth': d.getMonth() + 1 ,
                'fDay': 1,
                'tYear': d.getFullYear() + 1,
                'tMonth': d.getMonth(),
                'tDay': d.getDate()
            };

            var Grid = {
                url:'../Handlers/Handler_GetDrivers.ashx',
                width: 875,
                searchDate: searchDate,
                popup: false,
                columns: [
                    {
                        value:'ID',
                        field: 'ID',
                        title: '',
                        width: 37,
                        widthPlus:0,
                        sortable: false,
                        filterable: false,
                        hidden: true,
                        template: "<input type='checkbox' class='checkbox' id='#ID#'  />"
                        
                    },
                    {
                        value:'',
                        field: '',
                        title: '',
                        width: 60,
                        widthPlus:0,
                        sortable: false,
                        filterable: false,
                        hidden: false,
                        template: ""
                        
                    },                    
                    {
                        value: 'PersonalInfo.Person.LastName',
                        field: 'LastName',
                        title: 'LAST',                       
                        width: 109,
                        widthPlus: 0,
                        sortable: true,
                        filterable: true
                    },
                    {
                        value: 'PersonalInfo.Person.FirstName',
                        field: 'Firstname',
                        title: 'FIRST',                       
                        width: 125,
                        widthPlus: 0,
                        sortable: true,
                        filterable: true
                    },
                    
                    {
                        value: 'Driver.PrimaryID',
                        field: 'PrimaryID',
                        title: 'DRIVER ID',
                        width: 171,
                        widthPlus: 0,
                        sortable: true,
                        filterable: true
                    },
                    {
                        value: 'Driver.Mode',
                        field: 'Email',
                        title: 'MODE',
                        width: 156,
                        widthPlus: 0,
                        sortable: true,
                        filterable: true
                    },
                    {
                        value: 'Driver.Category',
                        field: 'Category',
                        title: 'CATEGORY',
                        width: 156,
                        widthPlus: 0,
                        sortable: true,
                        filterable: true
                    },
                    {
                        value:'',
                        field: '',
                        title: '',
                        width: 46,
                        widthPlus:0,
                        sortable: false,
                        filterable: false,
                        hidden: true,
                        template: "<p id='vie#ID#'  class='detail'></p>"
                        
               
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

            gridView.Grid = Grid;

            gridView.Init();            

            InitSearch(gridView); 

            //Get All Checked box
            OnClickOK();

            //personal
            var personal = new Personal();
            personal.stateCities = new StateCity().sateCities;
            personal.modeCategories = new ModeCategory().modeCategories;
            personal.Init('#modify', gridView);
            personal.CreateCompanySelect("", "#selCompany");
            //user.onClickSubmit(obj, obj.data);
           
        });        

        function Details(data, id) {
            //User
            var personal = new Personal();
            personal.FillData(data, id);
           // user.onClickSubmit(new FieldKeys().data);
        }

        function OnClickOK() {
            $('.ok').on('click', function () {
                var driverIDs = [];
                var companyID = $('#selCompany :selected').val();
                if (companyID === '0') {
                    //alert("Please Choose a company name.")
                    AlertMessage("#dialog", "Error message!", "Please Choose a company name.");
                    return;
                }
                $.each($('input:checkbox.checkbox:checked'), function () {
                    var id = $(this).attr('id');
                    driverIDs.push(id);
                    if (companyID !== '0')
                        $('#' + id).prop('checked', false);
                });

                if (driverIDs.length === 0 || driverIDs === null || driverIDs === undefined) {
                    AlertMessage("#dialog", "Error message!", "Please Choose one or more Personal from the list.");
                    return;
                }
                
                var fieldkeys = new FieldKeys();
                var fieldkey = fieldkeys.data;
                var data = {};
                data[fieldkey.ID] = companyID;
                data[fieldkey.ListID] = driverIDs.join(',');
                data[fieldkey.Cmd] = "C";
                console.log(JSON.stringify(data));
                $.ajax({
                    type: 'POST',
                    url: '../Handlers/Handler_CreateDonor.ashx',
                    data: data,
                    success: function (msg) {
                        console.log(msg)

                    },
                    error: function () {

                    }
                });

            });
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
                //console.log('id: ' + element.attr('id') + ' top: ' + top + ' left: ' + left);
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
               // console.log(fDate + ' ' + tDate);
                o.LoadData(datas);
            }
               
        }

        function AlertMessage(element, error, message) {
            var str = "<div class='alert'><p class='title redText'>";
            str += error;
            str += "</p>";
            str += "<p>";
            str += message;
            str += "</p>";
            str += "</div>";            
            $(element).html(str);           
           
            $('#popup').show();
            $('#popup').on('click', function () {
                $('#popup').hide();
            });
        }
    </script>
    <style type="text/css">  
        
        #popup {
            width: 100%;
            height:100%;
            background: rgba(0,0,0,0.8);
            position: absolute;
            top: 0;
            left: 0;
            margin: auto;
            display: none;
           
            z-index: 2;
        }
        #dialog div.alert{           
            padding:20px;
            border:solid 2px #fff;
            border-radius:8px;
            margin-top:10px;
            background: rgba(255,255,255,1);         
        }
        .close{
            width:80px;
            height:80px;           
            background:url('../images/ic_close_48.png') center no-repeat;
            cursor:pointer;
            float:right;
        }
        .dialog{
            width:auto;
            height:auto;
            /*background-color: #f00;*/            
            margin:15% 0 0 40%;   
            position:absolute;
           
            
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
            .search{
                display:none;
            }
       .chk{
           display:none;
       }
       /*Panel State City*/
       #date .cells{
                height:180px;
                width:180px;
            }
            #date .cells li{
                font-size:0.8em;
            }
            #date .cells li:hover {
                color: #fff;
                font-weight: normal;
                border-bottom: dotted 1px #0a5dac;
                background-color: #0a5dac;
                cursor: pointer;
                text-decoration: underline;
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
            <div class="title">
                 Personal list
            </div>  
            <div class="toolbar" > 
                <div id="sender" class="sender">
                    <p class="senderTitle">Work at comapy</p>  
                    <p class="destination">
                        <select id="selCompany" style="width:90%; overflow:auto;">
                        <option >SELECT COMPANY</option>
                    </select>  
                    </p>
                                      
                    <button id="add" class="ok">OK</button>
                </div>
                <div id="date"></div>
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
                            <td class="date" title="Select Motnh"><p id="tMonth"class="dateSearch">1</p></td>
                            <td>day:</td>
                            <td class="date" title="Select Day"><p id="tDay" class="dateSearch">1</p></td>
                            <td>year:</td>
                            <td class="date" title="Select Year"><p id="tYear" class="dateSearch">2021</p></td>
                            <td><p id="Search" class="dateSearch search">Search</p></td>
                            <td><p id="AllDataLoad" class="search">Load All Data</p></td>
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
        
        <div id="modify" class="modify">
           
        </div>
        <div id="popup">
            <p class="close"></p>
            <div class="dialog" id="dialog">    
           
            </div>
        </div>
    </body>
</html>