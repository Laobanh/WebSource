<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Web_SFH.Donors.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SANTAFE HEALTH CLINIC</title>
    <link href="../css/httGirdView.css?v=2021072801" rel="stylesheet" />  
    <link href="../css/text.css?v=2021071401" rel="stylesheet" />    
    <link href="../CSS/Toolbar.css?v=2021072801" rel="stylesheet" />
    <link href="../CSS/modify.css?v=2021072901" rel="stylesheet" />
    <script src="../Scripts/jquery-3.6.0.min.js"></script>
    <script src="../js/lib.js?v=2021072901"></script>
    <script src="../js/GridView.js?v=20210072801"></script>
    <script src="../js/FieldKeys.js?v=2021072801"></script>
    <script src="../js/Donor.js?v=2021072901"></script>
    <script src="../js/StateCity.js?v=2021071901"></script>
    <script src="../js/ModeCategory.js?v=2021071901"></script>

    <script type="text/javascript">
        var gridView;
        $(document).ready(function () {

            var id = getUrlVars("id").id;//+ getUrlVars("mode").mode;
            //console.log(id);
            
            gridView = new GridView();       

            //onClickSignout
            onClickSignout();

            var d = new Date();

            var searchData = {
                'id': id
            };

            var Grid = {
                url:"../Handlers/Handler_GetDonor.ashx",
                width: 870,
                searchData: searchData,
                popup: false,
                columns: [                    
                    {
                        value:'',
                        field: 'stt',
                        title: '',
                        width: 50,
                        widthPlus:0,
                        sortable: false,
                        filterable: false,
                        hidden: false,
                        bit: false,
                        bitValue:"",
                        template: ""
                        
                    },        
                    {
                        value:'ExcludeFromSelection',
                        field: 'ExcludeFromSelection',
                        title: 'X',
                        width: 39,
                        widthPlus:0,
                        sortable: false,
                        filterable: false,                       
                        bit: true,
                        bitValue:"X"
                    }, 
                    {
                        value:'NotActive',
                        field: 'NoActive',
                        title: 'NO ACTIVE',
                        width: 78,
                        widthPlus:0,
                        sortable: false,
                        filterable: false,
                        bit: true,
                        bitValue: "X"
                        
                    }, 
                    {
                        value:'NotAvilable',
                        field: 'NotAvilable',
                        title: 'NO AVILLABLE',
                        width: 88,
                        widthPlus:0,
                        sortable: false,
                        filterable: false,
                        bit: true,
                        bitValue:"X"                   
                        
                    }, 
                    {
                        value: 'Lastname',
                        field: 'LastName',
                        title: 'LAST',                       
                        width: 91,
                        widthPlus: 0,
                        sortable: true,
                        filterable: true
                    },
                    {
                        value: 'Firstname',
                        field: 'Firstname',
                        title: 'FIRST',                       
                        width: 104,
                        widthPlus: 0,
                        sortable: true,
                        filterable: true
                    },
                    
                    {
                        value: 'PrimaryID',
                        field: 'PrimaryID',
                        title: 'DONOR ID',
                        width: 143,
                        widthPlus: 0,
                        sortable: true,
                        filterable: true
                    },
                    {
                        value: 'Mode',
                        field: 'mode',
                        title: 'MODE',
                        width: 91,
                        widthPlus: 0,
                        sortable: true,
                        filterable: true
                    },
                    {
                        value: 'Category',
                        field: 'Category',
                        title: 'CATEGORY',
                        width: 110,
                        widthPlus: 0,
                        sortable: true,
                        filterable: true
                    },                    
                    {
                        value:'',
                        field: '',
                        title: '',
                        width: 40,
                        widthPlus:0,
                        sortable: false,
                        filterable: false,
                        hidden: true,
                        template: "<p id='vie#PrimaryID#'  class='detail'></p>"
                        
               
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

           // onClickSearch(gridView);

            //InitSearch(gridView); 

            //Get All Checked box
            //GetChecked();

            
            var donor = new Donor();            
           
            donor.Init('#modify', gridView);

            donor.CreateCompanySelect(id, '#selCompany');

            onClickSearch(gridView, donor);
           
        });        

        function Details(data, id) {
            //User
            var donor = new Donor();
            donor.FillData(data, id);
           // user.onClickSubmit(new FieldKeys().data);
        }

        function GetChecked() {
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
                
                console.log(driverIDs + "/" + companyID);
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

        function onClickSearch(gridView , donor) {
            $('#btnSearch').on('click', function () {
                var id = $('#selCompany :selected').val();
                var data = { "id": id };
                gridView.LoadData(data);

                donor.ResetNull();
            });

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
        #toolbar{
            width:1200px;         
        }
        #toolbar .title{
            width:100px;
        }
        .sender {
            width:900px;
            height:30px;
        }
            #toolbar p.senderTitle {
                font-size:0.8em;
                width: 250px;
                height:30px;
                margin-top:3px;
            }
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
            .modify{
                
                height:750px;
                width:750px;
                font-size:0.9em;
            }
            .modify table{
                width:750px;
            }

            .modify p.submit{
                width:180px;
                margin-top:0;
                display:none;
                /*margin-bottom:10px;*/
            }

            .testContent{
                width:500px;
                height:260px;
                /*background-color:#0a5dac;*/
                border:solid 1px #0a5dac;
                border-radius:5px;
                overflow:auto;
            }

                .testContent p{                    
                    font-size:1.2em;
                }

                .testContent p.collection{                 
                    cursor:pointer;
                }
                .testContent p.collection:hover{                    
                    color:#0a5dac;
                    font-weight:bolder;                  
                }
            .work{
                width:200px;
                height:260px;                
                /*background-color:#0a5dac;*/
                border:solid 1px #0a5dac;
                border-radius:5px;
                padding-top:5px;
                overflow:auto;              
            }

                .work p {
                    width: 195px;
                    /*height: 20px;*/
                    /*background-color: #f00;*/
                    padding:10px 0px 5px 5px;
                    font-size: 0.85em;
                }
                .work span {
                    padding-left: 3px;
                    font-weight: bolder;
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
            <div class="toolbar" >                
                <div id="sender" class="sender">
                    <p class="senderTitle">DONOR WORK AT COMPANY</p>  
                    <p class="destination">
                        <select id="selCompany" style="width:90%; overflow:auto;">
                        <option >COMPANY LOADING...</option>
                    </select>  
                    </p>
                                      
                    <button id="btnSearch" class="ok">SEARCH</button>
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
