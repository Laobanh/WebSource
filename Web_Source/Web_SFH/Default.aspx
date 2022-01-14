<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Web_SFH.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>SANTAFE HEALTH CLINIC</title>
    <link rel="shortcut icon" href="images/ico_90.png" />
    <link href="CSS/httGridView.css?v=2021092501" rel="stylesheet" />
    <link href="CSS/text.css?v=2021092501" rel="stylesheet" />
    <link href="CSS/Toolbar.css?v=2021092501" rel="stylesheet" />
    <link href="CSS/modify.css?v=2021092501" rel="stylesheet" />
    <link href="CSS/container_top.css?v=2021092501" rel="stylesheet" />
    <link href="CSS/Toolbar.css" rel="stylesheet" />
    <link href="CSS/popup.css?v=2021092501" rel="stylesheet" />
    <script src="Scripts/jquery-3.6.0.min.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>
    <script src="js/menu.js?v=2021092501"></script>
    <script src="js/lib.js?v=2021092501"></script>
    <%--GridView--%>
    <script src="js/GridView.js?v=2021092501"></script>

    <script src="js/FieldKeys.js?v=2021092501"></script> 
    <script src="js/StateCity.js?v=2021092501"></script>
    <script src="js/ModeCategory.js?v=2021092501"></script>

    <%-- Upload File --%>
    <script src="js/Imports.js?v=2021092501"></script>
   
    <%-- Companies --%>
    <script src="js/Companies.js?v=2021092501"></script>
    <script src="js/CompanyGridData.js?v=2021092501"></script>
    <%-- Consortiums --%>
    <script src="js/Consortiums.js?v=2021092501"></script>
    <script src="js/ConsortiumsGridData.js?v=2021092501"></script>

    <%-- Activity --%>
    <script src="js/Activities.js?v=2021092501"></script>
    <script src="js/ActivityGridData.js?v=2021092501"></script>
    <%-- Donor --%>
    <script src="js/Donor.js?v=2021092501"></script>
    <script src="js/DonorGridData.js?v=2021092501"></script>
    <script src="js/Personal.js?v=2021092501"></script>

    <%-- Donor Working At Companies --%>
    <script src="js/WorkingAtGridData.js?v=2021092501"></script>

    <%-- Test Results --%>
    <script src="js/SpecimentGridData.js"></script>

    <%-- Changed copy person Company --%>
    <script src="js/People.Changed.Copy.js?v=2021092501"></script>

    <%-- Schedules --%>
    <script src="js/Schedules.js?v=2021092501"></script>
    <script src="js/ScheduleGridData.js?v=2021092501"></script>
    <script src="js/SelectionGridData.js?v=2021092501"></script>
    <script src="js/SelectionDonorGridData.js?v=2021092501"></script>
    <script src="js/ScheduleReport.js?v=2021092501"></script>

    <%-- My Organizations --%>
    <script src="js/MyOrganization.js"></script>
    <script src="dist/trumbowyg.js"></script>
    <link href="dist/ui/trumbowyg.css?v=2021092501" rel="stylesheet" />
    <script type="text/javascript">  
        var obj = new Object();

        var menuID = "Dashboard";
        var menu = new Menu();   

        var company = new Companies();

        var schedules = new Schedules();

        var donor = new Donor();
        
        //Ret_DonorMobilePhone("1001_CAB9254318");
        //console.log(localStorage.getItem("Donors"));

        var gridView = new GridView();

        var gridData;

        //Random Reports
        var reports = undefined;
        var donorSelectType = 2;

         

        $(document).ready(function () {

            menu.Init(menuID);

            GetMenuID();
            //Hide panel
            $(window).click(function () {
                if ($('#panel').is(":hidden"))
                    $('#panel').hide().html('');
            });

            //Get Companies
            company.Gets();

           //Get schedules
            schedules.Gets();            

            //Get all donors
            donor.Gets();
        });   

        function GetMenuID() {
            var data = menu.data;
            var menuIDs = menuID.split('_');
            var gridView = new GridView();
            //Hidden subtoolbar
            $('.subtoolbar').hide();
            $('#panel').hide();
            if (menuIDs.length === 2) {
                switch (menuIDs[1]) {
                    case 'Dashboard':
                        break;
                    case 'Companies':
                        obj = new Companies();
                        //var comData = new CompanyGridData();
                        gridData = CompanyGridData.Grid;// comData.Grid;                       
                        gridView.Grid = gridData;      
                        gridView.Grid.searchData = { NewName: "" };
                        gridView.Init();

                         $('.httgridview').prepend("<div class='list_title'>Companies Base List</div>");
                        break;
                    case "Consortiums":
                        gridData = new ConsortiumGridData().Grid;
                        gridView.Grid = gridData;
                        gridView.Init();

                         $('.httgridview').prepend("<div class='list_title'>Consortiums Base List</div>");
                        break;
                    case "Schedules":
                        gridData = new ScheduleGridData().Grid;
                        gridView.Grid = gridData;
                        gridView.Init();
                        break;
                    case "MyOrganization":
                        var myOrg = new MyOrganization();
                        myOrg.Init();
                        break;
                    default:
                        gridData = undefined;
                        $('#httGridview').html(menuID);
                        break;
                }
                if (gridData !== undefined && gridData !== '') {
                    gridView.Grid = gridData;
                    // gridView.Init();
                }


            } else {
                switch (menuIDs[0]) {
                    case "Dashboard":
                        $('#httGridview').html(menuIDs[0]);
                        break;
                    default:
                        $('#httGridview').html('');
                        break;
                }
            }

            // console.log(JSON.stringify(data));
            //$('.wellcome').html(menuID.replace("_", " > "));
            //$('.httgridview').html("<p>" + menuID + "</p>");
            //Set navigate 
            SetToolbar();
        }

        //callback by GridView
        function OpenPDF(data, value) {
            var iData = data.find(x => x.PatientID === value);     
            var link = '../' + iData.ReportBinary.Name;
            window.open(link, '_blank');
        }

        //callback by GridView
        function Details(data, id) {            
            var com = new Companies();
            com.Init('#httGridview');    
        }

        //callback by GridView
        function Navigate(data, id, pageNavigate) {
            //console.log("Schedule => " + menuID + "/" + pageNavigate + "/" + id);
            //console.log("Naviagte: ID: " + id + " data: " + JSON.stringify(data));
            var menuIDs = localStorage.getItem("menu").split('_');
            //console.log(menuIDs);
            var name = "";
            var subLink, subActions, subReports;
            if (menuIDs.length === 2) {
                switch (menuIDs[1]) {
                    case "Companies":
                        var idata = data.find(x => x.CompanyID == id);
                        name = idata.CompanyName;
                        company.Init($('.httgridview'), data, id, false);
                        localStorage.setItem("data", JSON.stringify({ "data": data, "id": id }));
                        break;
                    case "Consortiums":
                        var idata = data.find(x => x.ID === id);
                        name = idata.Name;
                        var consortium = new Consortiums();
                        consortium.Init(".httgridview", data, id, false);
                        break;

                    case "Schedules":
                        if (pageNavigate === undefined) {
                            var idata = data.find(x => x.ID === id);
                            //console.log(JSON.stringify(idata.Selections));
                            var selctionGrid = new SelectionGridData(idata.Selections);
                            gridData = selctionGrid.Grid;
                            gridView.Grid = gridData;
                            gridView.Init();
                        } else {
                            subLink = menu.data.find(x => x.link === 'Schedule').nodes.find(x => x.link === "Schedule_Schedules").donors[0].link;
                            subActions = menu.data.find(x => x.link === 'Schedule').nodes.find(x => x.link === "Schedule_Schedules").donors[0].actions;
                            subReports = menu.data.find(x => x.link === 'Schedule').nodes.find(x => x.link === "Schedule_Schedules").donors[0].reports;

                            SetSubToolbar(subLink, data, id, menuID);
                            SetSubActions(subActions, subReports, data, id);
                            gridData = new SelectionDonorGridData().Grid;
                            gridView.Grid = gridData;
                            var specimen = data.find(x => x.ID === id).DonorSpecimenList;
                            //specimen = specimen.find(x => x.Selected == true);
                            var o = [];
                            for (var i = 0; i < specimen.length; i++) {
                                if (specimen[i].Selected)
                                    o.push(specimen[i]);
                            }
                            o.sort(function (a, b) {
                                var A = a.CompanyName;
                                var B = b.CompanyName;
                                if (A > B)
                                    return 1;
                                if (A < B)
                                    return -1;
                                if (A === B)
                                    return 0;

                            });
                            gridView.Grid.data = o;
                            gridView.Init();

                        }
                        break;
                }



                //Created sub tool
                if (pageNavigate === undefined) {
                    var nodes = menu.data.find(x => x.link === menuIDs[0]).nodes;
                    //console.log(JSON.stringify(nodes));
                    if (nodes !== undefined) {
                        subLinks = nodes.find(x => x.link === menuID).sublink;
                        subActions = nodes.find(x => x.link === menuID).subactions;
                        subReports = nodes.find(x => x.link === menuID).subreports;
                        SetSubToolbar(subLinks, data, id, menuID);
                        SetSubActions(subActions, subReports, data, id);
                    }
                    SetMenuPosition(name, menuID);
                }

            }
            else if (menuIDs.length === 3) {
                switch (menuIDs[2]) {
                    case "People":
                        $('.subtoolbar').show();
                        subLink = menu.data.find(x => x.link === 'Groups').nodes.find(x => x.link === "Groups_Companies").people[0].link;
                        subActions = menu.data.find(x => x.link === 'Groups').nodes.find(x => x.link === "Groups_Companies").people[0].subactions;
                        subReports = menu.data.find(x => x.link === 'Groups').nodes.find(x => x.link === "Groups_Companies").people[0].reports;

                        SetSubToolbarChild(subLink, data, id, menuID);
                        SetSubActions(subActions, subReports, data, id);

                        var personal = new Personal()
                        personal.Init(".httgridview", id, false);
                        break;
                    case "Companies":
                        var idata = data.find(x => x.CompanyID == id);
                        name = idata.CompanyName;
                        company.Init($('.httgridview'), data, id, false);
                        break;
                    case "Selections":
                        subLink = menu.data.find(x => x.link === 'Schedule').nodes.find(x => x.link === "Schedule_Schedules").donors[0].link;
                        subActions = menu.data.find(x => x.link === 'Schedule').nodes.find(x => x.link === "Schedule_Schedules").donors[0].actions;
                        subReports = menu.data.find(x => x.link === 'Schedule').nodes.find(x => x.link === "Schedule_Schedules").donors[0].reports;

                        SetSubToolbar(subLink, data, id, menuID);
                        SetSubActions(subActions, subReports, data, id);
                        gridData = new SelectionDonorGridData().Grid;
                        gridView.Grid = gridData;
                        var specimen = data.find(x => x.ID === id).DonorSpecimenList;
                        //specimen = specimen.find(x => x.Selected == true);
                        var o = [];
                        for (var i = 0; i < specimen.length; i++) {
                            if (specimen[i].Selected)
                                o.push(specimen[i]);
                        }
                        o.sort(function (a, b) {
                            var A = a.CompanyName;
                            var B = b.CompanyName;
                            if (A > B)
                                return 1;
                            if (A < B)
                                return -1;
                            if (A === B)
                                return 0;

                        });
                        gridView.Grid.data = o;
                        gridView.Init();
                        break;
                    default:
                        console.log("ID: " + id + "MenuID: " + menuID);
                        break;
                }
            }


        }

        //callback by GridView
        function Delete(data, id) {

        }

        //callback by GridView
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
                            SetSubToolbar(subLinks, data, id);
                            SetSubActions(subActions, subReports, data, id);
                        }

                        var company = new Companies();
                        company.Init($('.httgridview'), data, id, false);
                        break;
                }

            }
            SetMenuPosition(name);
        }

        //callback by Gridview
        function Test(data) {
            var sp1 = "", sp2 ="";
            var kq = "";           
            var specimenName1 = "", name1, name2;
            var specimenName2 = "";
            for (var i = 0; i < data.length; i++) {
                var seletion = data[i].Selected;
                specimenName1 = data[i].Specimen1;
                specimenName2 = data[i].Specimen2;
                if (seletion) {
                    if (specimenName1 && specimenName1 !== undefined && specimenName1 !== "") {
                        sp1 += specimenName1 + ",";
                        name1 = specimenName1;
                    }

                    if (specimenName2 && specimenName2 !== undefined && specimenName2 !== "") {
                        sp2 += specimenName2 + ",";
                        name2 = specimenName2;
                    }                    
                }

            }            
           
            sp1 = sp1.split(',').length;            
            sp2 = sp2.split(',').length;

            kq = "<p>" + (sp1 > 1 ? (sp1 - 1) + " " + name1 : '');
            if (sp1 > 1 && sp2 > 1)
                kq += ",";
            kq += (sp2 > 1  ? " " + (sp2 - 1) + ' ' + name2 : '');
            kq+= "</p>";
            
            return kq;
        }

        //Callback by Gridview
        function Specimen(data, id) {
            return "Urine";
        }

        function RetDonorMobilePhone(id) {
            var ids = id.split('_');
            var id = ids.length === 2 ? ids[1] : 0;
            var donors = localStorage.getItem("Donors");
            donors = JSON.parse(donors).find(x => x.ID === id);           
            return donors.PersonalInfo.Contact.MobilePhone
           
        }

        function RetDonorBirthday(id) {
            var ids = id.split('_');
            var id = ids.length === 2 ? ids[1] : 0;
            var donors = localStorage.getItem("Donors");
            donors = JSON.parse(donors).find(x => x.ID === id);
            return RetDate(donors.PersonalInfo.Person.DateOfBirth);
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
                    s += "<ul class='dropdownlist' id='report'>";                    
                    reports.map((report) => {
                        s += "<li data-link='" + report + "'>" + report + "</li>";
                    });
                    s += "</ul>";
                    
                }               

                if (actions !== undefined && actions.length > 0) {
                    s += "<p class='actions' id='actions'>Actions</p>";
                    s += "<ul class='dropdownlist' id='action'>";
                    actions.map((action) => {
                        s += "<li data-link='" + action + "'>" + action + "</li>";
                    });
                    s += "</ul>";
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

                    localStorage.setItem("menu", link);

                    console.log("OnClickToolBar: " + link);

                    var postion = menu.ReturnIndex();

                    menu.SetState(postion);

                    //$('.wellcome').html(menuID.replace("_", " > "));

                    $('#httGridview').html("<p>" + link + "</p> ");
                    
                    var str = "";                       
                    
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
                    switch (id.toLocaleLowerCase()) {
                        case 'reports':
                            ShowDropDownList("#report", position.top, position.left, position.right);
                            break;
                        case 'actions':
                            ShowDropDownList("#action", position.top, position.left, position.right);
                            break;

                    }                   
                    //$('#httGridview').html("<p>" + id + "</p>");               
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

        function returnMenuID(link) {
            var menuIDs = localStorage.getItem("menu").split('_');
            var kq = "";
            switch (menuIDs.length) {
                case 1:
                    kq = menuIDs[0] + "_" + link;
                    break;
                case 2:
                    kq = menuIDs[0] + "_" + menuIDs[1] + "_" + link;
                    break;
                case 3:
                    kq = menuIDs[0] + "_" + menuIDs[1] + "_" + menuIDs[2] + "_" + link;
                    break;
            }

            return kq;                      
        }
        //SubToolbar
        function SetSubToolbar(subLinks, data, id, menuID) {
            var str = "";            

            if (subLinks !== undefined && subLinks.length > 0) {
                var i = 0;
                subLinks.map(function (sub) {
                    str += "<p class='p " + (i === 0 ? ' selected' : '') + "' data-link='" + sub + "' id='" + sub + "'>" + sub + "</p>";
                    i++;
                });                

                $('.toolbar').html(str);

                //Register onclick event
                OnClickSubToolbar(data, id, menuID);                             
            }

        }

        function SetSubToolbarChild(subLinks, data, id, menuID) {
            var str = "";            

            if (subLinks !== undefined && subLinks.length > 0) {
                var i = 0;
                subLinks.map(function (sub) {
                    str += "<p class='p " + (i === 0 ? ' selected' : '') + "' data-link='" + sub + "' id='sub" + sub + "'>" + sub + "</p>";
                    i++;
                });

                

                $('.subtoolbar').html(str);

                //Register onclick event
                OnClickSubToolbarChild(data, id, menuID);

                //OnClickActions(data, id);

               // OnClickDropDownList(data, id);                
            }

        }

        function SetSubActions(subActions, subReports, data, id, consortiumname) {

            //Add Actions and reports
            var actionsDiv = $('.toolbar2');
            var s = "";
            i = 0;
            if (subReports !== undefined && subReports.length > 0) {
                s += "<p class='actions' id='reports'>Reports";
                s += "</p>";
                s += "<ul class='dropdownlist' id='report'>";
                subReports.map((report) => {
                    s += "<li data-link='" + report + "'>" + report + "</li>";
                });
                s += "</ul>";
            }


            if (subActions !== undefined && subActions.length > 0) {
                s += "<p class='actions' id='actions'>Actions</p>";
                s += "<ul class='dropdownlist' id='action'>";
                subActions.map((action) => {
                    s += "<li data-link='" + action + "'>" + action + "</li>";
                });
                s += "</ul>";
            }

            actionsDiv.html('').html(s);

            //Register onclick event
           // OnClickSubToolbar(data, id);

            OnClickActions(data, id);;

            OnClickDropDownList(data, id, consortiumname);
        }

        function OnClickSubToolbar(data, id, menuID) {
            $('.toolbar p').on("click", function () {
                var link = $(this).attr("data-link");
               // var actionsDiv = $('.toolbar2');
                // actionsDiv.html('');
                $('.subtoolbar').hide();
                $('#panel').hide();
                if (link !== undefined) {
                    $('.p').removeClass('selected');

                    $('#' + link).addClass('selected');                    

                    var menuIDs = menuID.split('_');
                    if (menuIDs.length < 4) {
                        menuID = menuIDs[0] + "_" + menuIDs[1] + "_" + link;
                        localStorage.setItem("menu", menuID);                       
                    } else {
                        menuID = menuIDs[0] + "_" + menuIDs[1] + "_" + menuIDs[2] + "_" + link;

                        localStorage.setItem("menu", menuID);
                    }               

                    var subActions, subReports, consortiumName = "";
                    //Root Group
                    if (menuIDs[0] === "Groups") {
                        //Node = "Companies
                        if (menuIDs[1] === "Companies") {
                            //Sub link
                            switch (link) {
                                case "Details":
                                    var company = new Companies();
                                    company.Init($('.httgridview'), data, id, false);
                                    subActions = menu.data.find(x => x.link === 'Groups').nodes.find(x => x.link === "Groups_Companies").subactions;
                                    subReports = menu.data.find(x => x.link === 'Groups').nodes.find(x => x.link === "Groups_Companies").subreports;
                                    break;
                                case "Activity":
                                    gridData = ActivityGridData.Grid;

                                    gridView.Grid = ActivityGridData.Grid;

                                    gridView.Grid.searchData = { "Type": 1 };

                                    gridView.Init();

                                    $('.httgridview').prepend("<div class='comment'></div>");
                                    var activities = new Activities();
                                    activities.Init('.comment', data, id, gridView, 1);
                                    break;
                                case "People":
                                    subActions = menu.data.find(x => x.link === 'Groups').nodes.find(x => x.link === "Groups_Companies").people[0].actions;
                                    subReports = menu.data.find(x => x.link === 'Groups').nodes.find(x => x.link === "Groups_Companies").people[0].reports;
                                    // DonorGridData.searchData = { id: id, NewID: "" };
                                    gridView.Grid = DonorGridData.Grid;

                                    gridView.Grid.searchData = { id: id, NewID: "" };
                                    gridView.Init();

                                    $('.httgridview').prepend("<div class='list_title'>Donor Base List </div>");
                                    break;
                                default:
                                    $('.httgridview').html('').html(menuID);
                                    break;

                            }

                        }
                        else if (menuIDs[1] === "Consortiums") {
                            switch (link) {
                                case "Details":
                                    var consortium = new Consortiums();
                                    consortium.Init('.httgridview', data, id, false);

                                    subActions = menu.data.find(x => x.link === 'Groups').nodes.find(x => x.link === "Groups_Consortiums").subactions;

                                    subReports = menu.data.find(x => x.link === 'Groups').nodes.find(x => x.link === "Groups_Consortiums").subreports;
                                    break;
                                case "Companies":
                                    consortiumName = data.find(x => x.ID === id).Name;
                                    gridData = CompanyGridData.Grid;// comData.Grid;                       
                                    gridView.Grid = gridData;
                                    gridView.Grid.searchData = { id: id };
                                    gridView.Init();
                                    $('.httgridview').prepend("<div class='list_title'>Companies Base List</div>");

                                    subActions = menu.data.find(x => x.link === 'Groups').nodes.find(x => x.link === "Groups_Consortiums").companies[0].actions;

                                    subReports = menu.data.find(x => x.link === 'Groups').nodes.find(x => x.link === "Groups_Consortiums").companies[0].reports;
                                    //console.log(subActions + "/" + subReports);

                                    break;
                                case "People":
                                    consortiumName = data.find(x => x.ID === id).Name;
                                    subActions = menu.data.find(x => x.link === 'Groups').nodes.find(x => x.link === "Groups_Consortiums").people[0].actions;
                                    subReports = menu.data.find(x => x.link === 'Groups').nodes.find(x => x.link === "Groups_Consortiums").people[0].reports;
                                    //console.log(subActions + "/" + subReports);
                                    gridView.Grid = DonorGridData.Grid;
                                    gridView.Grid.searchData = { NewID: id };
                                    gridView.Init();
                                    break;
                                default:
                                    $('.httgridview').html('').html(menuID);
                                    break;
                            }

                            console.log("Consortium: " + menuID);
                        }

                        //if (subActions !== undefined || subReports !== undefined)
                        SetSubActions(subActions, subReports, data, id, consortiumName);
                        //   else
                        //       $('.toolbar2').html('');

                    }
                    else if (menuIDs[0] === "Schedule") {
                        if (menuIDs[1] === "Schedules") {
                            switch (link) {
                                case "Selections":
                                    var idata = data.find(x => x.ID === id);
                                    //console.log(JSON.stringify(idata.Selections));
                                    var selctionGrid = new SelectionGridData(idata.Selections);
                                    gridData = selctionGrid.Grid;
                                    gridView.Grid = gridData;
                                    gridView.Init();

                                    break;
                                case "Details":
                                    var schedule = new Schedules();
                                    schedule.Init(data, id);
                                    break;
                                case "Summary":
                                    $('.httgridview').html('Summary');
                                    break;
                                case "Donors":
                                    gridData = new SelectionDonorGridData().Grid;
                                    gridView.Grid = gridData;
                                    var specimen = data.find(x => x.ID === id).DonorSpecimenList;
                                    //specimen = specimen.find(x => x.Selected == true);
                                    var o = [];
                                    for (var i = 0; i < specimen.length; i++) {
                                        if (specimen[i].Selected)
                                            o.push(specimen[i]);
                                    }
                                    o.sort(function (a, b) {
                                        var A = a.CompanyName;
                                        var B = b.CompanyName;
                                        if (A > B)
                                            return 1;
                                        if (A < B)
                                            return -1;
                                        if (A === B)
                                            return 0;

                                    });
                                    gridView.Grid.data = o;
                                    gridView.Init();
                                    break;
                                case "Alternates":
                                    gridData = new SelectionDonorGridData().Grid;
                                    gridView.Grid = gridData;
                                    var specimen = data.find(x => x.ID === id).DonorSpecimenList;
                                    //specimen = specimen.find(x => x.Selected == true);
                                    var o = [];
                                    for (var i = 0; i < specimen.length; i++) {
                                        if (specimen[i].IsAlternate)
                                            o.push(specimen[i]);
                                    }
                                    o.sort(function (a, b) {
                                        var A = a.CompanyName;
                                        var B = b.CompanyName;
                                        if (A > B)
                                            return 1;
                                        if (A < B)
                                            return -1;
                                        if (A === B)
                                            return 0;

                                    });
                                    gridView.Grid.data = o;
                                    gridView.Init();
                                    break;
                                default:
                                    $('.httgridview').html(menuIDs[0] + "/" + menuID);
                                    break;
                            }
                        }
                    }


                }
                
            });

            
        }

        function OnClickSubToolbarChild(data, id, menuID) {
            $('.subtoolbar p').on("click", function () {
                var link = $(this).attr("data-link");
                $('#panel').hide();
                var rootData = JSON.parse(localStorage.getItem("data"));
                //console.log("localStorage: " + JSON.stringify(rootData));
               // console.log(link);
                if (link !== undefined) {
                    $('.subtoolbar .p').removeClass('selected');
                    $('#sub' + link).addClass('selected');

                    switch (link) {
                        case "Details":
                            var personal = new Personal()
                            personal.Init(".httgridview", id, false);
                            //console.log(link + " " + id);
                            //$('.httgridview').prepend("<div class='list_title'>Personal Base List</div>");
                            break;
                        case "Eliminate":
                            var idata = data.find(x => x.ID == id);
                            //name = idata.Lastname + " " + idata.Firstname;
                            donor.Init(".httgridview", data, id, false, "");                           
                            //$('.httgridview').prepend("<div class='list_title'>Donor Base List</div>");
                            break;
                        case "Activity":
                            gridData = ActivityGridData.Grid;
                            gridView.Grid = ActivityGridData.Grid;
                            gridView.Grid.searchData = { "Type": 2 };
                            gridView.Init();

                            $('.httgridview').prepend("<div class='comment'></div>");
                            var activities = new Activities();
                            activities.Init('.comment', data, id, gridView, 2);
                            break;
                        case "Working":
                            gridView.Grid = WorkingAtGridData.Grid;
                            gridView.Grid.searchData = { "id": id };
                            gridView.Init();
                            $('.httgridview').prepend("<div class='list_title'>Donor Working At Companies List</div>"); 

                            console.log(link + " " + id);
                            break;
                        case "Speciments":
                            gridView.Grid = SpecimentGridData.Grid;
                            gridView.Grid.searchData = { "id": id };
                            gridView.Init();
                            $('.httgridview').prepend("<div class='list_title'>Speciments Base List</div>");                           
                            break;
                        default:
                            $('.httgridview').html("Groups_Companies_People_" + link);
                            break;
                    }

                   // console.log("ID: " + id + " " + JSON.stringify(data));
                }
                
            });

            
        }

        function SetMenuPosition(s, menu) {
            var idata = {};            
            var str = menu.replaceAll("_", " > ");
            str += s !== undefined ? ": " + s : "";
            //$('.wellcome').html(str);
        }

        //Show Sub Actions toolbar2
        function ShowDropDownList(element, top, left, right) {
            $(element).show().offset({ top: top, left: left - 130 }).mouseleave(function () {
                $(this).hide();
            });
        }

        //OnClick Sub Actions toolbar2
        function OnClickDropDownList(data, id, consortiumName) {
            $('.toolbar2').find('li').on('click', function () {
                reports = undefined;
                $('.subtoolbar').hide();
                var dataLink = $(this).attr('data-link');
                var rootData = JSON.parse(localStorage.getItem("data"));
                switch (dataLink.toLocaleLowerCase()) {
                    case "new company":
                        company.Init($('.httgridview'), '', '', true);
                        break;
                    case "new person":
                        var personal = new Personal();
                        personal.Init('.httgridview', id, true);
                        break;
                    case "copy person":
                        var ccp = new PeopleChangedCopy();
                        ccp.Init(dataLink, data, id);
                        break;
                    case "change company":
                        var ccp = new PeopleChangedCopy();
                        ccp.Init(dataLink, data, id);
                        break;
                    case "import people":
                    case "import companies":
                        var imp = new Imports();
                        imp.Init(dataLink, id, consortiumName);
                        break;
                    //Consrtium
                    case "new consortium":
                        var consortium = new Consortiums();
                        consortium.Init(".httgridview", "", "", true);
                        break;
                    case "add company":
                        var consortium = new Consortiums();
                        consortium.Add('.httgridview', id);
                        break;
                    case "random schedule":
                        var schedule = new Schedules();
                        schedule.Init();
                        break;
                    //Report Random
                    case "notification letters"://1 N
                        reports = { report: true, donorType: 2, reportType: 'N', id: id };
                        //$('.httgridview').html("<b>Menu position: " + menuID + "_" + dataLink + " ID: " + id + "</b><h3> Notification letters.</h3>");
                        break;
                    case "random selection summary"://2 R                        
                        reports = { report: true, donorType: 2, reportType: 'R', id: id };
                        break;
                    case "random selection list": //3 L                       
                        reports = { report: true, donorType: 2, reportType: 'L', id: id };
                        break;
                    case "notification slip": //5 S                       
                        reports = { report: true, donorType: 2, reportType: 'S', id: id };
                        break;                   
                    case "base list maintained": //6 B                         
                        reports = { report: true, donorType: 0, reportType: 'B', id: id };
                        break;                    
                    case "base list of company": //6 B                         
                        reports = { report: true, donorType: 0, reportType: 'B1', id: id, name: "Base List Of Company" };
                        break;
                     case "base list of group"://6 B
                        reports = { report: true, donorType: 0, reportType: 'B2', id: id, name: "Base List Of Group" };
                        break;
                    default:
                        $('.httgridview').html("<b>Menu position: " + menuID + "_" + dataLink + " ID: " + id + "</b><h3> Coming Soon.</h3>");
                        break;
                }
                $('.dropdownlist').hide();

                //console.log("OnClickDorpdownlist :" + JSON.stringify(data));
                if (reports !== undefined && reports.report) {
                    switch (reports.reportType) {
                        case "B1": 
                            break
                        case "B2":
                            break;
                        default:

                            var ids = id.split('_');

                            var schedule = localStorage.getItem("schedules");
                            if (schedule !== undefined && schedule !== "") {
                                schedule = JSON.parse(schedule);                            }
                            var scheduleID = ids[0];
                            schedule = schedule.find(x => x.ID === scheduleID);
                            var selections = data.find(x => x.ID === id);
                             reports.name = selections.Name + " on " + ReturnDate(selections.RunOn);
                            break;
                    }
                   
                    if (reports.reportType === 'L') {
                        var msg = $('#donorSelectType').html();
                        AlertMessage("#dialog", "Notification", msg, "", "", "ok", "ok", reports);
                    }
                    else if (reports.reportType === "B" && schedule.Type === 1) {
                        var msg = $('.donorSelectType').html();
                        AlertMessage("#dialog", "Notification", msg, "", "", "OK", "ok", reports);
                    }
                    else {
                        var scheduleReport = new ScheduleReport();
                        scheduleReport.DataLoading('.httgridview');
                        scheduleReport.Init(reports);
                    }
               

                }


                //console.log("OnClickDropDownList Root ID: " + rootData.id);
            });
        }        

        function AlertMessage(element, error, message, button1, label1, button2, label2, data) {
            $('#popup').show();
            var str = "<p class='title'>";
            str += error;
            str += "</p>";
            str += "<div class='alert'>";
            str += message;
            str += "</div>";
            //Button 1
            if (button1 === undefined || button1 === "")
                str += "";
            else
                str += "<p class='yes button' id='" + button1.toLocaleLowerCase() + "'>" + label1.toLocaleUpperCase() + "</p>";
            if (button2 === undefined || button2 === "")
                str += "";
            else
                str += "<p class='cancel button' id='" + button2.toLocaleLowerCase() + "'>" + label2.toLocaleUpperCase() + "</p>";

            $(element).html(str);
            OnClickPopup(data);
        }

        function OnClickPopup(data) {
            $('.dialog').find('p').on('click', function () {
                var id = $(this).attr('id');
                id = id.toLocaleLowerCase();
                switch (id) {
                    case "cancel":
                    case "no":
                        $('#popup').hide();
                        $('.dialog').html('');
                        break;
                    case "yes":
                    case "ok":
                        if (data !== undefined && data.report) {
                            donorSelectType = $('input[name=chkDonorSelectType]:checked').val();
                            reports.donorType = donorSelectType;
                            var scheduleReport = new ScheduleReport();
                            scheduleReport.DataLoading('.httgridview');
                            scheduleReport.Init(reports);
                            //console.log("OnClickPopup: " + JSON.stringify(data));
                        }
                        
                        $('#popup').hide();
                        $('.dialog').html('');
                        break;
                }

                //if (reports !== undefined && reports.report)
                //    console.log("On Click Popup: " + JSON.stringify(reports));
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
            width: 90%;
            min-height: 600px;           
            margin-left: 200px;
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
            width:70%;
        }
        .toolbar p{
            float:left;
            margin:8px 0 5px 10px;
            
        }

        .toolbar p:hover{
            color:#03305b;
            cursor:pointer;
        }

        .toolbar p.selected{          
           color:#0665db;
           /*text-decoration:underline;*/
           border-bottom:solid 2px #1fb141;
           font-style:italic;
        }

        .toolbar2 {
            width: 29%;
            position: relative;
            float: left;
            font-size:0.9em;
        }

            .toolbar2 p {
                cursor: pointer;
            }

            .toolbar2 p.addNew {
                min-width: 80px;
                /*background:url(../images/ic_drop_down_24.png) no-repeat;*/
                background-color: #6da331;
                background-position: right;
                border: solid 1px #6da331;
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
                    min-width: 80px;
                    background: url(../images/ic_drop_down_24_1.png) no-repeat;
                    background-size: 14px 14px;
                    background-position: 92% 60%;
                    /*background-position-y: 5px;*/
                    background-color: #6da331;
                    border: solid 1px #6da331;
                    border-radius: 4px;
                    float: right;
                    color: #fff;
                    text-decoration: none;
                    margin: 8px 8px 0 0;
                    padding: 5px 0 5px 10px;
                }

            .toolbar2 p.actions:hover {
                background-color:#6da331;
                color:#fff;
            }

            .dropdownlist{
                display:none;  
                min-width:180px;
                position:absolute;
                min-height:80px; 
                background-color: #6da331;
                border:solid 1px #e6e6e6;
                border-radius:5px;
                z-index:2;
                margin-right:10px;
            }
            .dropdownlist ul{                
                height:30px; 
                padding:5px;
                background-color:#03305b;
                
            }
                .dropdownlist li {
                    list-style: none;
                    color: #fff;
                    /*margin:5px;*/
                    padding: 5px 0 5px 0;
                    background-color: #6da331;
                    font-size:0.9em;                  
                   
                }
                .dropdownlist li:hover{
                    text-decoration:underline;
                    cursor:pointer;
                }
        .subtoolbar {
            position: relative;
            font-size: 0.85em;
            margin-left: 20px;
            float: left;
            width: 70%;
            /*background-color:#0665db;*/
            display: none;
        }
            .subtoolbar p {
                float: left;
                margin: 8px 0 5px 10px;
            }

            .subtoolbar p:hover {
                color: #03305b;
                cursor: pointer;
            }

            .subtoolbar p.selected {
                color: #0665db;
                /*text-decoration:underline;*/
                border-bottom: solid 2px #1fb141;
                font-style: italic;
            }
        .tab p{
            margin:0;
            padding:5px 0 5px 0;
        }

        .list_title {
            margin-bottom: 8px;
            font-size: 0.9em;
            position: relative;
        }

        td.search_panel {
            border: solid 1px #e6e6e6;
            border-radius:3px;           
        }

         td.search_panel input:focus{
             outline:none;
         }          

          td.search_panel p.item{
              border:solid 1px #0665db;
              border-radius:3px;
              background:url('../images/ic_close_48.png') no-repeat;
              background-size:18px 18px;
              background-position:99% 50%;
              margin:5px;
              padding:5px; 
          }

          td.search_panel p.upload{
              width:300px;
              height:28px;
              float:left;
              border:solid 1px #e6e6e6;
              border-top-left-radius:5px;
              border-bottom-left-radius:5px;
              padding-top:2px;

              
          }

          td.search_panel p.upload input{
              width:100%;              
              border:0;             
          }


            td.search_panel p.browser {
                width: 100px;
                height: 24px;
                background-color: #6da331;
                border-top-right-radius: 5px;
                border-bottom-right-radius:5px;
                color: #fff;
                float: left;
                text-align: center;
                padding-top:8px;
            }
            td.search_panel p.browser:hover {
                cursor:pointer;
            }        

        .move {
            margin-left: 220px;
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
        <div class="subtoolbar">

        </div>
        <div id="httGridview" class="httgridview">
            
        </div>
    </div>
    <div id="popup">        
        <div class="dialog" id="dialog">    
           
        </div>
    </div>
    <div id="panel" class="panel"></div>
    <p id="donorSelectType" style="display:none">
        <label>&nbsp;&nbsp;Please choose a type report</label><br />
        &nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" name="chkDonorSelectType" id="radio2" value="2" checked="checked" />
        <label for="radio2">Both</label>
        <input type="radio" name="chkDonorSelectType" id="radio1" value="1" />
        <label for="radio1">Selection</label>
        <input type="radio" name="chkDonorSelectType" id="radio0" value="0" />
        <label for="radio0">Alternate</label>        
    </p>
    <p class="donorSelectType" style="display:none" id="BaseList">
        <label>&nbsp;&nbsp;Please choose a type report</label><br />
        &nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" name="chkDonorSelectType" id="radio3" value="0" />
        <label for="radio3">for each Company</label>
        <input type="radio" name="chkDonorSelectType" id="radio4" value="1"  checked="checked" />
        <label for="radio4">for Consortium</label>              
    </p>
</body>
</html>