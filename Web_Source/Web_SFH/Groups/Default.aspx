<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Web_SFH.Groups.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head><title>
	SANTAFE HEALTH CLINIC
</title>
    <link href="../CSS/httGridView.css?v=2021080801" rel="stylesheet" />
    <link href="../CSS/text.css?v=2021072203" rel="stylesheet" />
    <link href="../CSS/Toolbar.css?v=2021072203" rel="stylesheet" />
    <link href="../CSS/modify.css?v=2021080801" rel="stylesheet" />
    <link href="../CSS/container_top.css?v=2021080210" rel="stylesheet" />
    <link href="../CSS/Toolbar.css" rel="stylesheet" />
    <script src="../Scripts/jquery-3.6.0.min.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>
    <script src="../js/menu.js?v=2021080502"></script>
    <script src="../js/lib.js?v=2021072203"></script>
    <script src="../js/GridView.js?v=20210080802"></script>
    <script src="../js/FieldKeys.js?v=2021072701"></script>   
    
    <script src="../js/StateCity.js"></script>

    <script src="../js/Companies.js?v=2021072701"></script>
    <script src="../js/CompanyData.js?v=2021080201"></script>

    <script type="text/javascript">        
        var menuID = "Dashboard";
        var menu = new Menu();

        var obj = new Object();

        var gridView, gridData;
        $(document).ready(function () {
           
            menu.Init(menuID);

            GetMenuID();
        });   

        function GetMenuID() {
            var data = menu.data;
            var menuIDs = menuID.split('_');
            var gridView = new GridView();

            if (menuIDs.length === 2) {
                switch (menuIDs[1]) {
                    case 'Dashboard':
                        break;
                    case 'Companies':
                        obj = new Companies();
                        var comData = new CompanyData();
                        gridData = comData.Grid;
                        break;
                    default:
                        gridData = undefined;
                        $('#httGridview').html('');
                       // console.log('default');
                        break;
                }
                if (gridData !== undefined && gridData !== '') {
                    gridView.Grid = gridData;
                    gridView.Init();
                }


            }

            // console.log(JSON.stringify(data));
            $('.wellcome').html(menuID.replace("_", " > "));
            //$('.httgridview').html("<p>" + menuID + "</p>");
            //Set navigate 
            SetToolbar(data, menuIDs);
        }
        //callback Girdview
        function Details(data, id) {            
            var com = new Companies();
            com.Init('#httGridview');    
        }

        function Navigate(data, id) {
            var menuIDs = menuID.split('_');
            var name = "";
            if (menuIDs.length === 2) {
                switch (menuIDs[1]) {
                    case "Companies":
                        var idata = data.find(x => x.CompanyID == id);
                        name = idata.CompanyName;
                        var nodes = menu.data.find(x => x.link === menuIDs[0]).nodes;
                        if (nodes !== undefined) {
                            var subLinks = nodes.find(x => x.link === menuID).sublink;
                            var subActions = nodes.find(x => x.link === menuID).subactions;
                            var subReports = nodes.find(x => x.link === menuID).subreports;
                            SetSubToolbar(subLinks, subActions, subReports);
                        }

                        var company = new Companies();
                        company.Init($('.httgridview'), data, id, false);
                        break;
                }

            }
            SetMenuPosition(name);
        }

        function Delete(data, id) {

        }

        function Edit(data, id) {
            var menuIDs = menuID.split('_');
            var name = "";
            if (menuIDs.length === 2) {
                switch (menuIDs[1]) {
                    case "Companies":
                        var idata = data.find(x => x.CompanyID == id);
                        name = idata.CompanyName;
                        var nodes = menu.data.find(x => x.link === menuIDs[0]).nodes;
                        if (nodes !== undefined) {
                            var subLinks = nodes.find(x => x.link === menuID).sublink;
                            var subActions = nodes.find(x => x.link === menuID).subactions;
                            var subReports = nodes.find(x => x.link === menuID).subreports;
                            SetSubToolbar(subLinks, subActions, subReports);
                        }
                        break;
                }

            }
             SetMenuPosition(name);

        }

        //Root toolbar
        function SetToolbar() {
            var str = "";
            var menuIDs = menuID.split('_');
            //console.log(menuIDs.length + "/" + menuIDs[0]);
            var idata = menu.data.find(x => x.link === menuIDs[0]);
            var actions, reports;
            if (idata !== undefined) {
                idata.nodes.map(function (m) {
                    if (menuIDs.length === 2) {
                        if (menuID === m.link) {
                            str += "<p class='p selected' data-link='" + m.link + "' id='" + m.link + "'>";
                            str += m.label + "</p>";
                            actions = m.actions;
                            reports = m.reports;
                        }
                        else {
                            str += "<p class='p' data-link='" + m.link + "' id='" + m.link + "'>" + m.label + "</p>";
                        }

                    } else
                        str += "<p class='p' data-link='" + m.link + "' id='" + m.link + "'>" + m.label + "</p>";
                });
                if (idata.nodes.length === 0)
                    str += "<p class='p selected'>" + menuID + "</p>";

                //Add Actions
                var actionsDiv = $('.toolbar2');
                var s = "";

                if (reports !== undefined && reports.length > 0) {
                    s += "<p class='actions' id='reports'>Reports";
                    s += "</p>";
                    s += "<ul class='selReports'>";                    
                    reports.map((report) => {
                        s += "<li data-link='" + report + "'>" + report + "</li>";
                    });
                    s += "</ul>";
                    
                }

                if (actions !== undefined) {
                    Object.values(actions).map((action) => {
                        s += "<p class='addNew' id='" + action + "'>" + action + "</p>";
                    });
                }

                actionsDiv.html('').html(s);

                $('.toolbar').html(str);

                //Register Onclick toolbar
                OnClickToolbar();

                OnClickActions();

                OnClickDropDownList();
            } 
           

        }

        function OnClickToolbar() {
            $('.toolbar p').on("click", function () {
                var link = $(this).attr("data-link");
                var actionsDiv = $('.toolbar2');
                actionsDiv.html('');
                if (link !== undefined) {
                    $('.p').removeClass('selected');

                    $('#' + link).addClass('selected');

                    menuID = link;

                    var postion = menu.ReturnIndex();

                    menu.SetState(postion);

                    $('.wellcome').html(menuID.replace("_", " > "));

                    $('#httGridview').html("<p>" + link + "</p> ");
                    
                    var str = "";

                    var actions = GetActions();

                    //console.log('actions: ' + actions.actions + '/ reports: ' + actions.reports);
                    if (actions.actions !== undefined) {
                        Object.values(actions.actions).map((action) => {
                            str += "<p class='addNew' id='" + action + "'>" + action + "</p>";
                        });

                        actionsDiv.append(str);
                    }

                    if (actions.reports !== undefined && actions.reports.length > 0) {
                        str = "<p class='actions' id='reports'>Reports</p>";
                        actionsDiv.append(str);
                    }                   
                    //Register Onclick event
                    OnClickActions();

                    //call GetMenu
                    GetMenuID();
                }
                
            });

            
        }

        function OnClickActions() {
            $('.toolbar2').find('p').on("click", function () {
                var id = $(this).attr("id");
                var position = $(this).offset();
                if (id !== undefined) {
                    if (id === 'reports') {
                        ShowDropDownListReport(position.top, position.left, position.right);
                    }
                }
                else
                    $('#httGridview').html("<p>undefined</p> ");
            });
           
        }

        function GetActions() {
            var kq = new Object();
            var menuIDs = menuID.split('_');
            //console.log(menuIDs.length + "/" + menuIDs[0]);
            var idata = menu.data.find(x => x.link === menuIDs[0]);
            var actions, reports;
            if (idata !== undefined) {
                idata.nodes.map(function (m) {
                    if (menuIDs.length === 2) {
                        if (menuID === m.link) {
                            actions = m.actions;
                            reports = m.reports;
                            kq["actions"] = actions;
                            kq["reports"] = reports;
                        }
                    }
                });
            }
            //console.log(JSON.stringify(kq));
            return kq;
        }

        //SubToolbar
        function SetSubToolbar(subLinks, subActions, subReports) {
            var str = "";            

            if (subLinks !== undefined && subLinks.length > 0) {
                var i = 0;
                subLinks.map(function (sub) {
                    str += "<p class='p " + (i === 0 ? ' selected' : '') + "' data-link='" + sub + "' id='" + sub + "'>" + sub + "</p>";
                    i++;
                });

                //Add Actions
                var actionsDiv = $('.toolbar2');
                var s = "";
                i = 0;
                if (subReports !== undefined && subReports.length > 0) {
                    s += "<p class='actions' id='reports'>Reports";
                    s += "</p>";
                    s += "<ul class='selReports'>";                    
                    subReports.map((report) => {
                        s += "<li data-link='" + report + "'>" + report + "</li>";
                    });
                    s += "</ul>";                    
                }
                   

                if (subActions !== undefined && subActions.length > 0) {
                    subActions.map((action) => {
                        s += "<p class='addNew' id='" + action + "'>" + action + "</p>";
                    });
                }

                actionsDiv.html('').html(s);

                $('.toolbar').html(str);



                //Register onclick event
                OnClickSubToolbar();

                OnClickActions();

                OnClickDropDownList();                
            }

        }

        function OnClickSubToolbar() {
            $('.toolbar p').on("click", function () {
                var link = $(this).attr("data-link");
                var actionsDiv = $('.toolbar2');
                actionsDiv.html('');
                
                if (link !== undefined) {
                    $('.p').removeClass('selected');

                    $('#' + link).addClass('selected');                    

                    var menuIDs = menuID.split('_');
                    menuID = menuIDs[0] + "_" + menuIDs[1] + "_" + link;

                    SetMenuPosition();
                    var str = "";                   

                    //console.log('actions: ' + actions.actions + '/ reports: ' + actions.reports);
                    //if (actions.actions !== undefined) {
                    //    Object.values(actions.actions).map((action) => {
                    //        str += "<p class='addNew' id='" + action + "'>" + action + "</p>";
                    //    });

                    //    actionsDiv.append(str);
                    //}

                    //if (actions.reports !== undefined && actions.reports.length > 0) {
                    //    str = "<p class='actions' id='reports'>Reports</p>";
                    //    actionsDiv.append(str);
                    //}                   
                    //Register Onclick event
                    //OnClickActions();

                    //call GetMenu
                   // GetMenuID();
                }
                
            });

            
        }

        function SetMenuPosition(s) {
            var str = menuID.replaceAll("_", " > ");
            str += s !== undefined ? ": " + s : "";
            $('.wellcome').html(str);
        }

        function ShowDropDownListReport(top, left,right) {
            $('.selReports').show().offset({ top: top, left: left - 70 });
        }

        function OnClickDropDownList() {
            $('.toolbar2').find('li').on('click', function () {
                var id = $(this).attr('data-link');

                console.log('report: ' + id);

                $('.selReports').hide();
            });
        }

       
    </script>
    <style type="text/css">
        html, body {
            width: 100%;
            margin: 0;
            border: 0;
            padding: 0;
            background-color: #03305b;  
            font-family:sans-serif;
        }        
        .container_content {
            width: 100%;
            min-height: 600px;           
            margin-left: 20px;
            border: 0;
            padding: 0;
            background-color:#fff;
            border:solid 1px #03305b;
            border-radius:8px;
            overflow:auto;
        }

        .toolbar{     
            position:relative;           
            font-size:0.85em;                
            margin:auto;
            float:left;
            width:50%;
        }
        .toolbar p{
            float:left;
            padding:5px 0 5px 10px;
            
        }

        .toolbar p:hover{
            color:#03305b;
            cursor:pointer;
        }

        .toolbar p.selected{          
           color:#0665db;
           text-decoration:underline;
        }

        .toolbar2 {
            width: 50%;
            position: relative;
            float: left;
        }

            .toolbar2 p {
                cursor: pointer;
            }

            .toolbar2 p.addNew {
                min-width: 140px;
                /*background:url(../images/ic_drop_down_24.png) no-repeat;*/
                background-color: #03305b;
                background-position: right;
                border: solid 1px #03305b;
                border-radius: 4px;
                float: right;
                color: #fff;
                text-decoration: none;
                text-align: center;
                margin: 5px 10px 0 0;
                padding: 5px;               
            }

            .toolbar2 p.addNew:hover {               
                background-color:#0665db;
                
            }

            .toolbar2 p.actions {
                min-width: 140px;
                background: url(../images/ic_drop_down_24_1.png) no-repeat;
                background-size: 14px 14px;
                background-position-x: 120px;
                background-position-y: 5px;
                background-color: #03305b;
                border: solid 1px #03305b;
                border-radius: 4px;
                float: right;
                color: #fff;
                text-decoration: none;
                margin: 5px 10px 0 0;   
                padding:5px 0 5px 10px;
            }

            .toolbar2 p.actions:hover {
                background-color:#0665db;
                color:#fff;
            }

            .selReports{
                display:none;  
                width:180px;
                position:absolute;
                min-height:80px; 
                background-color: #0665db;
                border:solid 1px #03305b;
                border-radius:5px;
                z-index:2;
            }
            .selReports ul{
                height:30px;                
            }
                .selReports li {
                    list-style: none;
                    color: #fff;
                    margin:5px;
                    padding: 5px 0 5px 0;
                    background-color: #0665db;
                    font-size:0.9em;
                }
                .selReports li:hover{
                    text-decoration:underline;
                    cursor:pointer;
                }

        .move{
            margin-left:200px;       
        }
    </style>
</head>
<body>
    <div class="container_top">
        <p class="menuIcon"></p>
        <p class="logo"></p>
        <p class ="wellcome"></p>          
        <div class="menu" >       
        </div>
    </div>
   
    <div class="container_content">
        <div class="toolbar">

        </div>
        <div class="toolbar2"></div>
        <div id="httGridview" class="httgridview">
            
        </div>
    </div>
    <div id="popup">
        <p class="close"></p>
        <div class="dialog" id="dialog">    
           
        </div>
    </div>
</body>
</html>