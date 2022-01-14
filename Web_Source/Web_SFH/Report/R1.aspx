<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="R1.aspx.cs" Inherits="Web_SFH.Report.R1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>Notification letters</title>
    <script src="../Scripts/jquery-3.6.0.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#content').html(table());
        });

        function table() {
            var str = "";
            str += "<img src='data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAABmJLR0QA/wD/AP+gvaeTAAAAt0lEQVRIie2VMQ7CMBAEh8iiRqn5APkjIvCHvIs2D6CKXdBCESh8RpZJImHHDfJKluVba/euuYXM2MzUdwvcHF7AfemDAs6Akc8xxwCtaH1NcAGOwBO4/di9wx7YSqNtSBrgARwixQEa0dAhUWNH7BPEHXrRqgEqKbp7XMHAaVS+cDYUg2JQDP7JYAzea2p+oLGrtkkQd+t6cAXlkR1wAq6kB043RSpsCmniI3OQJicj00eW0M+CN7xTPUpiZd+cAAAAAElFTkSuQmCC' />";
            str += "<table style='float:left; width:75%;'>";
            str += "<tr>";
            str += "<td style='width:60%;' >";
            str += "<p style='text-align:right;'>";
            str += "&#60;------- Dates -------&#62;";
            str += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p>";
            str += "</td>";
            str += "<td style='width:40%;'>";
            str += "<p>&nbsp;&nbsp;Obsrv</p>";
            str += "</td>";
            str += "</tr>";
            str += "</table>";


            str += "<table style='float:left; width:45%;'>";
            str += "<tr>";
            str += "<th style='width:5%'>&nbsp;</th>";
            str += "<th style='width:25%'>Last Name</th>";
            str += "<th style='width:25%'>First Name</th>";
            str += "<th style='width:5%'  style='text-align:center;'>M.i.</th>";
            str += "<th style='width:15%' style='text-align:center;'>ID No.</th>";
            str += "<th style='width:12%' style='text-align:center;'>Selection</th>";
            str += "<th style='width:13%' style='text-align:center;'>Test</th>";
            str += "</tr>";
            //str += "<tr><td class='col'>CHEN</td><td class='col'>LU</td><td class='colEnd'>F7820991</td></tr>";
            //str += "<tr><td class='col'>DING</td><td class='col'>FU</td><td class='colEnd'>Y3435745</td></tr>";
            //str += "<tr><td class='col2'> KONG</td><td class='col2'>DEYU</td><td class='col2End'>D4257820</td></tr>";
            str += "<tr>";
            str += "<td class='col' style='width:5%' >1</td>";
            str += "<td class='col' style='width:25%'>CHEN</td>";
            str += "<td class='col' style='width:25%'>LU</td>";
            str += "<td class='col' style='width:5%'>&nbsp;</td>";
            str += "<td class='col' style='width:15%'>F7820991</td>";
            str += "<td class='col' style='width:12%'>08-14-2021</td>";
            str += "<td class='colEnd' style='width:13%'>&nbsp;</td>";
            str += "</tr>";

            str += "<tr>";
            str += "<td class='col'>2</td>";
            str += "<td class='col'>DING</td>";
            str += "<td class='col'>FU</td>";
            str += "<td class='col'>&nbsp;</td>";
            str += "<td class='col'>Y3435745</td>";
            str += "<td class='col'>08/14/2021</td>";
            str += "<td class='colEnd'>&nbsp;</td>";
            str += "</tr>";

            str += "<tr>";
            str += "<td class='col2'>3</td>";
            str += "<td class='col2'>KONG</td>";
            str += "<td class='col2'>DEYU</td>";
            str += "<td class='col2'>&nbsp;</td>";
            str += "<td class='col2'>D4257820</td>";
            str += "<td class='col2'>08/14/2021</td>";
            str += "<td class='col2End' style='width:12%'>&nbsp;</td>";
            str += "</tr>";
            str +="</table>";

            str += "<table style='float:left; width:45%;'>";
            str += "<tr>";
            str += "<th style='width:10%' style='text-align:center;'>Rq'd</th>";
            str += "<th style='width:10%' style='text-align:center;'>Alert! </th>";
            str += "<th style='width:25%' style='text-align:center;'>Test Type</th>";
            str += "<th style='width:40%' style='text-align:center;'>Misc.</th>";
            str += "<th style='width:15%' style='text-align:center;'>Expiration</th>";
           
            str += "</tr>";
            str += "</table>";

            return str;
        }


    </script>
    <style type="text/css">
        table{
            width:280mm;            
           border-collapse:collapse;
          
          
        }     
        .row{
            border:solid 1px #000;
        }
        th{
            font-size:10px;
            text-align:left;
            padding:5px 0 3px 5px;
        }
        td{
            font-family:sans-serif;
            font-size:10px;
            /*border:solid 1px #000;*/
            padding:3px 0 3px 5px;
           
           
        }
        .col{border:solid 1px #000; border-bottom:0;border-right:0;}
        .colEnd{border:solid 1px #000;border-bottom:0;}
        .col2{border:solid 1px #000; border-right:0;}
        .col2End{border:solid 1px #000;}
        .colLine{border-bottom:solid 1px #000;}
        .page{width:8.3in;height:11.7in;}
        .signature{font-family:cursive; font-size:14pt; font-style:italic;}

    </style>

</head>
<body>    
    <div id="content">
        <table>
            <tr>
                <td>
                    <iframe src='../Data/Reports/Notificationslip_SF60002_0.pdf' style='border:0; width:100%; height:600px;'></iframe>
                </td>
            </tr>
        </table>
        
    </div>
</body>
</html>
