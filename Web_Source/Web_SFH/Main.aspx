<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="Web_SFH.Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SANTAFE HEALTH CLINIC</title>
    <script src="Scripts/jquery-3.6.0.min.js"></script>
    <script src="js/lib.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var menu = [
                {
                    link: '',
                    label: 'Medical Examination',
                    icon: 'images/menu/ic_exam_24_1.png',
                    icon2: 'images/menu/ic_exam_24_2.png',                   
                    nodes: []
                },
                {
                    link: 'Company',
                    label: 'Company',
                    icon: 'images/menu/ic_company_24_1.png',
                    icon2: 'images/menu/ic_company_24_2.png',                   
                    nodes: []
                },
                {
                    link: 'Personal',
                    label: 'Personal',
                    icon: 'images/menu/ic_donor_24_1.png',
                    icon2: 'images/menu/ic_donor_24_2.png',
                    nodes: []
                },
                {
                    link: 'Donor',
                    label: 'Donor',
                    icon: 'images/menu/ic_driver_24_1.png',
                    icon2: 'images/menu/ic_driver_24_2.png',
                    nodes: []
                },
                {
                    link: '',
                    label: 'Report',
                    icon: 'images/menu/ic_report_24_1.png',
                    icon2: 'images/menu/ic_report_24_2.png',
                    nodes: [
                        {
                        label: "MroReports",
                        link:  "MroReports"                   
                        },
                        {
                        label: "Pendding Result",
                        link:  ""                   
                        }
                    ]
                },
                {
                    link: '',
                    label: 'Random',
                    icon: 'images/menu/ic_random_24_1.png',
                    icon2: 'images/menu/ic_report_24_2.png',
                    nodes: []
                },
                {
                    link: 'Users',
                    label: 'Users',
                    icon: 'images/menu/ic_folder_user_24_1.png',
                    icon2: 'images/menu/ic_folder_user_24_2.png',
                    nodes: []
                },
                {
                    link: 'Signout',
                    label: 'Signout',
                    icon: 'images/menu/ic_exit_24_1.png',
                    icon2: '',
                    nodes: []
                }
            ];

            

            var url = getUrlVars()[0];
            var menuID = "Medical Examination";
            var frame = $('#frame');
            frame.height($(window).height() - 50);
            if (url !== undefined && url !== '') {
                menuID = url;
                frame.show();
            } else {               
                frame.hide();
            }
            frame.attr('src', '/' + (url === "Donor" ? "Donor/?id=1001" : url));

            setMenu(menu, menuID);
        });

        function setMenu(menu,id) {
            var str = "";
            for (var i = 0; i < menu.length; i++) {                
                var nodes = menu[i].nodes;
                str += "<ul class='root'>";
                str += "<li  id='" + i + "' data-link='" + menu[i].link + "'>";
                if (id === menu[i].label) {
                    //Line
                    str += "<p class='root_line select'  id='line" + i + "'></p>"

                    //icon
                    str += "<p class='root_icon'";

                    str += " style='background:url(" + menu[i].icon2 + ") center no-repeat;background-size:16px 16px;' ></p> ";
                }
                else {
                    //Line
                    str += "<p class='root_line'  id='line" + i + "'></p>"

                    //icon
                    str += "<p class='root_icon'";

                    str += " style='background:url(" + menu[i].icon + ") center no-repeat;background-size:16px 16px;' ></p> ";
                }                

                //label
                str += "<p class='root_label' >" + menu[i].label + "</p>";
                //arrow
                str += "<p class='root_arrow'";


                if (nodes.length > 0) {
                    str += " style='background:url(images/menu/ic_arrow_24_1.png) center no-repeat; background-size:12px 12px;'";
                }

                str += "></p > ";
                str += "</li>";
                str += "</ul>";
                if (nodes.length > 0) {                    
                    for (var k = 0; k < nodes.length; k++) {
                        var n = i + '-' + k
                        str += "<ul class='node'>";
                        str += "<li id='" + n + "' data-link='" + nodes[k].link + "'>";
                        if (nodes[k].label === id) {
                            str += "<p class='node_line select' id='line" + n + "'></p>";                           
                        } else {
                            str += "<p class='node_line' id='line" + n + "'></p>";                           
                        }
                        str += "<p class='node_icon'></p>";
                        str += "<p class='node_label' >" + nodes[k].label + "</p>";
                        str += "</li>";
                        str += "</ul>";
                    }
                    
                }
                
            }
            
            $('.menu').html(str).height($(window).height() - 40);
            hoverMenu();

            onCLickMenu();
        }

        function setMenuStatu(id) {

        }

        function hoverMenu() {
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
                var id = $(this).attr('data-link');
                if (id != undefined && id !== '') {
                    if (id === 'Signout')
                        window.location = 'Signout.aspx';
                    else
                        window.location = '?' + id;

                }


                console.log('id: ' + $(this).attr('id') + ' / data-link: ' + $(this).attr('data-link'));

            });
        }

        function onCLickMenu() {
            $('.menuIcon').on('click', function () {
                var menu = $('.menu');
                if (menu.is(':hidden'))
                    menu.fadeIn(1000);
                else
                    menu.hide();
            });
        }
    </script>
    <style type="text/css">
        html, body, div{
            margin:0;
            border:0;
            padding:0;
        }
        .banner {
            width: 100%;
            height: 40px;
            margin: 0;
            background-color: #495056;  
            position: absolute;
        }
        .banner p{
            margin:0;
            border:0;
            padding:0;
        }
            .banner p.menuIcon {
                width: 40px;
                height: 40px;
                background:url(images/ic_menu_48.png) center no-repeat;
                background-size: 28px 28px;
                background-color: transparent;                
                float: left;
                position: relative;
            }
            .banner p.logo {
                width: 160px;
                height: 40px;
                background-color: transparent;
                float: left;
                position: relative;
            }
            .banner p.wellcome {
                width: 600px;
                height: 40px;
                float: left;
                position: relative;
                background-color: transparent;
            }
        #frame {
            width: 100%;
            min-height: 600px;
            margin-top: 40px;
            padding-left: 20px;
            border: 0;
            position: relative;
        }
        .menu{
            width:200px;            
            background-color:#495056;
            position:absolute;
            display:none;
            top:0;
            left:0;
            margin-top:40px;
            z-index:1;
            
        }
        .menu ul, li p{
            margin:0;
            border:0;
            padding:0;
            font-family:sans-serif;
            font-size:0.9em;
            list-style:none;
        }
        .root {
            width: 200px;
            height: 40px;
            background-color: #495056;
            position: relative;
            display: block;
        }

        .root p.root_line{
            width:3px;
            height:40px;
            /*background-color:#4cff00;*/
            float:left;
        }


        .root p.root_icon{
            width:35px;
            height:40px;        
            background-color:transparent;
            float:left;
        }
        .root p.root_label{
            width:140px;
            /*height:30px;*/
            /*background-color:#808080;*/
            text-align:left;
            padding-top:12px;
            color:#f1f2f4;
            float:left;
            cursor:pointer;
        }
        .root p.root_arrow{
            width:22px;
            height:40px;
            /*background:url('images/menu/ic_arrow_white_24.png') center no-repeat;
            background-size:12px 12px;*/
            background-color:transparent;           
            float:left;
            cursor:pointer;
            /*transform:rotate(180deg);*/
        }
        .hover, .select{
             background-color:#4cff00;
        }

        .node{
            width:200px;
            min-height:35px;
            background-color:#30312f;
            position:relative;
            /*display:none;*/
        }
        .node p.node_line{
            width:3px;
            height:35px;
            /*background-color:#4cff00;*/
            float:left;
        }
        .node p.node_icon{
            width:37px;
            height:35px;
            background-color:transparent;
            float:left;
        }
        .node p.node_label{
            width:160px;
            /*height:38px;*/           
            text-align:left;
            padding-top:10px;
            color:#f1f2f4;
            float:left;
            cursor:pointer;
        }
        
    </style>
</head>
<body>
    <div class="banner">
        <p class="menuIcon"></p>
        <p class="logo"></p>
        <p class ="wellcome"></p>    
        <div class="menu" >       
        </div>
    </div>
    <iframe id="frame" src="/Company"></iframe>
   
</body>
</html>