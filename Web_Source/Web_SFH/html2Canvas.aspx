<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="html2Canvas.aspx.cs" Inherits="Web_SFH.html2Canvas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Html2Canvas</title>
    <script src="Scripts/jquery-3.6.0.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/html2canvas/1.3.2/html2canvas.min.js" integrity="sha512-tVYBzEItJit9HXaWTPo8vveXlkK62LbA+wez9IgzjTmFNLMBO1BEYladBw2wnM3YURZSMUyhayPCoLtjGh84NQ==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.3.2/jspdf.min.js"></script>
    <script type="text/javascript">
        var length = 2;
        $(document).ready(function () {
            
            Created(length);
            downloadPdf();
        });
        function downloadPdf() {
            $('.download').click(function () {
                var i = 0;
                var doc = new jsPDF("p", "mm", "a4");
                //var width = doc.internal.pageSize.getWidth();
                //var height = doc.internal.pageSize.getHeight();
                $('.image img').each(function () {
                    doc.setPage(i + 1);
                    var imageData = $(this).attr('src');
                    doc.addImage(imageData, 'JPEG', 10, 10);
                    if (i < length - 1) {
                        doc.addPage();
                        console.log(i);
                    }
                    i++;
                });

                //console.log(images);
                doc.save("sample.pdf");
            });
        }

        function Created(length) {
            for (var i = 0; i < length; i++) {
                var element = '#page' + i;
                html2canvas(document.querySelector(element),{scale:2}).then(canvas => {                    
                    $('.image').append("<img src='" + canvas.toDataURL('image/png') + "' />");

                });
            }

        }

        

        
    </script>
    <style type="text/css">        
       .download{
           width:140px;
           border:solid 1px #4b9807;
           border-radius:5px;
           padding:8px;
       }
        
        .page{            
            font-family:sans-serif;
        }
        .title{            
            font-size: 0.6em;
           color:#000;
           font-weight:bolder;
        }
        .unline{
            border-bottom:solid 1px #000;
            width:50%;            
        }
        .image{
            width:0;
            height:0;
        }
        img{
            width:auto;
            height:auto;            
        }
    </style>
</head>
<body>
    <div class="download">Download PDF</div>
    <div id="main" class="main">
       <div class="page" id="page0">
            <p class="title">Controlled Substance </p>
            <p class="title unline">You've Been Randomly Selected for Testing</p>
        </div>
        <div class="page" id="page1">
            <p class="title">Alcohol Test Notification</p>
            <p class="title unline">You've Been Randomly Selected for Testing</p>
        </div>
    </div>    
    <div class="image"></div>
</body>
</html>
