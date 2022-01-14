<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pdf.aspx.cs" Inherits="Web_SFH.pdf" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PDF</title>
    <script src="Scripts/jquery-3.6.0.min.js"></script>    

   <script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.3.2/jspdf.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            demoFromHTML();
        });
        function demoFromHTML() {
            var pdf = new jsPDF('p', 'pt', 'a4');
            pdf.setLineWidth(1);
            pdf.line(40, 63, 580, 63);//(x1,y1,x2,y2)
            pdf.line(40, 80, 100, 80);
            source = $('#pdf')[0];
            specialElementHandlers = {
                '#bypassme': function (element, renderer) {
                    return true
                }
            };
            margins = {
                top: 20,
                bottom: 20,
                left: 40,
                width: 522
            };
            pdf.fromHTML(
                source,
                margins.left,
                margins.top, {
                    'width': margins.width,
                    'elementHandlers': specialElementHandlers
                },
                function (dispose) {
                    pdf.save('Test.pdf');
                }, margins);
            
        }
    </script>
    <style type="text/css">
        p{
            font-family:sans-serif;
        }
    </style>
</head>
<body>
    <p id="ignorePDF">don't print this to pdf</p>
    <div id="pdf">
      <div>
          <p style="font-size:11px; font-weight:bolder;">Controlled Substance and / or Alcohol Test Notification</p>
          <p style="font-size:11px; font-weight:bolder; border-bottom:solid 1px #000;">You've Been Randomly Selected for Testing</p>
      </div>
    </div>
</body>
</html>
