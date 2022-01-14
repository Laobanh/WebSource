<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScheduleRandom.aspx.cs" Inherits="Web_SFH.ScheduleRandom" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Schedules Random</title>
    <link rel="shortcut icon" href="images/ico_90.png" />
    <link href="CSS/httGridView.css?v=2021082501" rel="stylesheet" />
    <link href="CSS/text.css?v=2021081101" rel="stylesheet" />
    <link href="CSS/Toolbar.css?v=2021072203" rel="stylesheet" />
    <link href="CSS/modify.css?v=2021090501" rel="stylesheet" />
    <link href="CSS/container_top.css?v=2021081401" rel="stylesheet" />
    <link href="CSS/Toolbar.css" rel="stylesheet" />
    <script src="Scripts/jquery-3.6.0.min.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>
    <script src="js/menu.js?v=2021082601"></script>
    <script src="js/lib.js?v=2021081501"></script>
    <%--GridView--%>
    <script src="js/GridView.js?v=2021082601"></script>

    <script src="js/FieldKeys.js?v=2021090402"></script> 
    <script src="js/StateCity.js?v=2021080901"></script>
    <script src="js/ModeCategory.js?v=2021081301"></script>
    <script src="js/Schedules.js?v=2021090505"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var schedule = new Schedules();
            schedule.Init();
        });

    </script>
    
</head>
<body>    
    <div class="httgridview">

    </div>   
</body>
</html>
