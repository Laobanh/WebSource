function Menu() {
    var obj = new Object();
    obj.lessHeight = 60;
    obj.lessWidth = 220;
    obj.height = $(window).height();
    obj.width = $(window).width();
    obj.data = [{
            link: 'Dashboard',
            label: 'Dashboard',
            icon: '../images/menu/ic_dashboard_24_1.png',
            icon2: '../images/menu/ic_dashboard_24_4.png',
            nodes: []
        },
        {
            link: 'Groups',
            label: 'Groups',
            icon: '../images/menu/ic_folder_24_1.png',
            icon2: '../images/menu/ic_folder_24_4.png',
            actions: [],
            reports:[],
            nodes: [
                {
                    label: "Companies",
                    link: "Groups_Companies",
                    sublink: ["Details", "Activity", "People"],//, "Locations", "Notification", "Document", "Configuration"
                    actions: ["New Company", "Import Companies"],
                    subactions: [],
                    subreports: ["Eligible Donor List", "MIS", "MIS Test List", "Randoms Summary", "Enrollment Certification", "Good Standing"],
                    people: [{
                        link: ["Details", "Eliminate", "Working", "Speciments", "Activity"],//["Details","Eliminate", "Documents", "Activity", "Training", "Speciments"]
                        actions: ["New Person", "Import People", "Merge Duplicates", "Fix Donor Occupations"],
                        subactions: ["Change Company", "Copy Person"],
                        reports: ["Clinical Summary Report", "Base List Of Company"]
                    }]

                },
                {
                    label: "Consortiums",
                    link: "Groups_Consortiums",
                    sublink: ["Details", "Companies", "People"],
                    actions: ["New Consortium"],
                    subactions: ["Add Subscription", "Invite User"],
                    subreports: [],
                    companies: [{
                        actions: ["Add Company", "Import Companies"],
                        reports: ["Eligible Donor List", "MIS", "MIS Test List", "Companies List"],
                    }],
                    people: [{
                        actions: ["Import People"],
                        reports: ["Base List Of Group"]
                    }]
                }
                //{
                //    label: "Locations",
                //    link: "Groups_Locations",
                //    sublink:[],
                //    actions: ["Import Locations"],
                //    subactions:[],
                //    subreports:[]
                //},
                //{
                //    label: "Acount IDs",
                //    link: "Groups_AcountIDs",
                //    sublink:[],
                //    actions: ["Import Acount IDs"],
                //    subactions:[],
                //    subreports:[]
                //}
            ]
        },

        //{
        //    link: 'Tests',
        //    label: 'Tests',
        //    icon: '../images/menu/ic_test_24_1.png',
        //    icon2: '../images/menu/ic_test_24_4.png',
        //    actions: ["New Specimen", "Result Import", "Order a Test", "Bulk Update Specimen","Bulk Download Specimen"],
        //    reports: [],
        //    nodes: []           
        //},
        {
            link: 'Schedule',
            label: 'Schedule',
            icon: '../images/menu/ic_schedule_24_1.png',
            icon2: '../images/menu/ic_schedule_24_4.png',
            actions: ["New"],
            reports: [],
            nodes: [
                //{
                //    label: "Calander",
                //    link: "Schedule_Calander",
                //    sublink: [],
                //    actions: ["New Event", "New Availability"],
                //    subactions: [],
                //    reports: []

                //},
                {
                    label: "Schedules",
                    link: "Schedule_Schedules",
                    sublink: ["Selections","Details","Summary"],
                    actions: ["Random Schedule"],//, "Follow up"
                   // subactions: ["New Instance","Terminate Schedule"],
                    reports: [],
                    donors: [{
                        link: ["Donors", "Alternates"],
                        actions: [],
                        reports: ["Notification letters", "Random selection summary", "Random Selection list", "Notification slip","Base List Maintained"]
                    }]

                },
                //{
                //    label: "Selections",
                //    link: "Schedule_Selections",
                //    sublink: [],
                //    actions: [],
                //    subactions: [],
                //    reports: ["Selection Donor List"]
                    
                //},
                //{
                //    label: "SMS",
                //    link: "Schedule_SMS",
                //    sublink: [],
                //    actions: [],
                //    subactions: [],
                //    reports: []
                //},
                //{
                //    label: "Email",
                //    link: "Schedule_Email",
                //    sublink: [],
                //    actions: [],
                //    subactions: [],
                //    reports: []
                //}
            ]
            
        },
        //{
        //    link: 'Billing',
        //    label: 'Billing',
        //    icon: '../images/menu/ic_billing_24_1.png',
        //    icon2: '../images/menu/ic_billing_24_4.png',
        //    actions: [],
        //    reports: [],
        //    nodes: [
        //        {
        //            label: "Dashboard",
        //            link: "Billing_Dashboard"
        //        },
        //        {
        //            label: "Invoices",
        //            link: "Billing_Invoices"
        //        },
        //        {
        //            label: "Payments",
        //            link: "Billing_Payments"
        //        },
        //        {
        //            label: "Bills",
        //            link: "Billing_Bills"
        //        },
        //        {
        //            label: "Price Schedule",
        //            link: "Billing_PriceSchedule"
        //        }
        //    ]
        //},
        {
            link: 'Users',
            label: 'Users',
            icon: '../images/menu/ic_folder_user_24_1.png',
            icon2: '../images/menu/ic_folder_user_24_4.png',
            actions: ["New User"],
            reports: [],
            nodes: [
                {
                label: "Details",
                link: "Users_Details",
                sublink: [],
                actions: ["New User"],
                subactions: [],
                subreports: []
                }
            ]
        },
        {
            link: 'Medical',
            label: 'Medical Examination',
            icon: '../images/menu/ic_exam_24_1.png',
            icon2: '../images/menu/ic_exam_24_2.png',
            actions: [],
            reports: [],
            nodes: []
        },
        {
            link: 'Report',
            label: 'Report',
            icon: '../images/menu/ic_report_24_1.png',
            icon2: '../images/menu/ic_report_24_4.png',
            actions: [],
            reports: [],
            nodes: [
                {
                    label: "MroReports",
                    link: "Report_MroReports"
                }
            ]
        },
        {
            link: 'Preferences',
            label: 'Preferences',
            icon: '../images/menu/ic_preferences_24_1.png',
            icon2: '../images/menu/ic_preferences_24_2.png',
            actions: [],
            reports: [],
            nodes: [
                {
                    label: "My Organization",
                    link: "Preferences_MyOrganization"
                },
                {
                    label: "My Account",
                    link: "Preferences_MyAccount"
                },
                {
                    label: "Configuration",
                    link: "Preferences_Configuration"
                }
            ]
        },
        {
            link: 'Signout',
            label: 'Signout',
            icon: '../images/menu/ic_exit_24_1.png',
            icon2: '',
            nodes: []
        }];
   

    obj.Init = function (id) {
        obj.SetSize();
       // $('.toolbar').width(obj.width - 800);
        obj.SetMenu(id)     
       
    }

    obj.SetSize = function () {
        $('.container_content').width(obj.width - 220).height(obj.height - obj.lessHeight);
        $('.menu').height(obj.height - 40)
    }

    obj.SetMenu = function (id) {
        var str = "";
        for (var i = 0; i < obj.data.length; i++) {
            var nodes = obj.data[i].nodes;
            str += "<ul class='root'>";
            str += "<li  id='" + i + "' data-link='" + obj.data[i].link + "'>";
            if (id === obj.data[i].label) {
                //Line
                str += "<p class='root_line select'  id='line" + i + "'></p>"

                //icon
                str += "<p class='root_icon'";

                str += "id = 'icon" + i + "'";

                str += " style='background:url(" + obj.data[i].icon2 + ") center no-repeat;background-size:16px 16px;' ></p> ";
            }
            else {
                //Line
                str += "<p class='root_line'  id='line" + i + "'></p>"

                //icon
                str += "<p class='root_icon'";

                str += "id = 'icon" + i + "'";

                str += " style='background:url(" + obj.data[i].icon + ") center no-repeat;background-size:16px 16px;' ></p> ";
            }

            //label
            str += "<p class='root_label' >" + obj.data[i].label + "</p>";
            //arrow
            str += "<p class='root_arrow' ";


            if (nodes.length > 0) {
                str += " style='background:url(../images/menu/ic_arrow_24_1.png) center no-repeat; background-size:12px 12px;'";
            }

            str += " id='arrow" + i + "'";
            str += "></p > ";
            str += "</li>";
            str += "</ul>";
            if (nodes.length > 0) {
                str += "<div id='nodes" + i + "' class='node_div'>";
                for (var k = 0; k < nodes.length; k++) {
                    var n = i + '-' + k
                    str += "<ul class='node'>";
                    str += "<li id='" + n + "' data-link='" + nodes[k].link + "'>";
                    if (nodes[k].label === id) {
                        str += "<p class='node_line' id='line" + n + "'></p>";
                    } else {
                        str += "<p class='node_line' id='line" + n + "'></p>";
                    }
                    str += "<p class='node_icon'></p>";
                    str += "<p class='node_label' >" + nodes[k].label + "</p>";
                    str += "</li>";
                    str += "</ul>";
                }
                str += "</div>";

            }

        }

        $('.menu').html(str).height($(window).height() - 40);

        obj.MenuChange();

        obj.OnClickMenu();

        
    }


    obj.MenuChange = function () {
        $('.menu').find('li').mouseenter(function () {
            var id = $(this).attr('id');
            if (id != undefined && id !== '')
                $('#line' + id).addClass('hover');

            //console.log('Mouse enter: ' + id + ' data-link: ' + $(this).attr('data-link'));
        }).mouseleave(function () {
            var id = $(this).attr('id');
            if (id != undefined && id !== '')
                $('#line' + id).removeClass('hover');

            // console.log('MOuse leave:' + id+ ' data-link: ' + $(this).attr('data-link'));
        }).click(function () {
            var dataLink = $(this).attr('data-link');

            if (dataLink != undefined && dataLink !== '') {
                if (dataLink === 'Signout')
                    window.location = '../Signout.aspx';
                //else
                //    window.location = '?' + id;
                menuID = dataLink;               
                localStorage.setItem("menu", dataLink);
                //callback main form
                GetMenuID(obj.data);
               // console.log(dataLink);

            }            

            //Set Arrow
            var id = $(this).attr('id');            
           
            var nodes = $('#nodes' + id);       

            obj.SetState(id, nodes.attr('id'));           
           

        });
        
       

    }

    obj.SetState = function (id) {       
        $('.menu p').each(function (index) {
            var className = $(this).attr('class');
            var IDs = id.split('-');
            $('.root_line').removeClass('select');
            $('.node_line').removeClass('select');
            $('.node_div').hide();
           
            if (IDs.length === 1) {    
                var arrow = $('#arrow' + IDs[0]);
                if ($(this).attr('id') === ('icon' + IDs[0])) {                   
                    obj.AddBackgroundImage($(this), false);                   
                } else {                   
                    obj.AddBackgroundImage($(this), true);                   
                }               

                $('#line' + IDs[0]).addClass('select');                

                

            } else if (IDs.length === 2) {
                
                if ($(this).attr('id') === ('icon' + IDs[0])) {
                    obj.AddBackgroundImage($(this), false);                   
                }
                else {
                    obj.AddBackgroundImage($(this), true);                 
                }
                

                $('#line' + id).addClass('select'); 

                //console.log('node: ' + $(this).attr('id'));
            }


            var nodes = $('#nodes' + IDs[0]);
            nodes.show();
           
            
        });

    }

    obj.AddBackgroundImage = function (element, remove) {
        
        var image = element.css('background-image').replace('24_1', '24_4');
        if (remove)
            image = element.css('background-image').replace('24_4', '24_1');
        element.css('background-image', image);
    }

    obj.ChangeArrow = function (element, remove) {

        var image = element.css('background-image').replace('24_1', '24_4');
        if (remove)
            image = element.css('background-image').replace('24_4', '24_1');
        element.css('background-image', image);
    }

    obj.OnClickMenu = function () {
        var content_div = $('.container_content');
        var width = 100;
        $('.menuIcon').on('click', function () {
            var menu = $('.menu');
            if (menu.is(':hidden')) {
                menu.show("slide", { direction: "left" }, 1000);
                width = obj.width - 220;
                //content_div.addClass('move').width(width).attr("margin-left", 20);
                content_div.width(width).css("margin-left", 200);
                $('.logo').width(160);
            }
            else {
                menu.hide("slide", { direction: "left" }, 1000);
                width = obj.width - 40;
                //content_div.removeClass('move').width(width).attr("margin-left", 220);
                content_div.width(width).css("margin-left", 20);
                $('.logo').width(1);
            }         
            
           // $('.toolbar').width(width);

            content_div.height(obj.height - obj.lessHeight);

        });
    }

    obj.ReturnIndex = function () {
        var index = 0;
        var IDs = menuID.split('_');
        if (IDs.length === 1) {
            index = obj.data.findIndex(p => p.link === IDs[0]);
        }
        else if (IDs.length === 2) {
            index = obj.data.findIndex(p => p.link === IDs[0]);
            index = index + "-" + obj.data[index].nodes.findIndex(p => p.link === menuID);
        }
       // console.log('Return index: ' + index + ' menuID: ' + menuID);
        return index;
    }

    return obj;
}