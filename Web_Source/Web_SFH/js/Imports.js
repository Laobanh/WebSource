function Imports() {
    var obj = new Object();
    obj.fieldKey = new FieldKeys().data;

    obj.Init = (type, id, consortiumName) => {
        obj.CreatedContent('.httgridview', type, id, consortiumName);
    }

    obj.CreatedContent = (element, type, id, consortiumName) => {       
        var str = "";
        var str = "";
        str += "<div class='modify'>";
        str += "<p class='notification'></p>";

        str += "<div style='height:20px; padding:10px;'>";
        str += "<span>";
        str += "Download: ";
        str += "</span>";       
        str += "<span class='link' id='btnLoadSampleFile' title='Click to download'>Exist csv file.</span>";
        str += "</div>";

        str += "<p class='title'>" + type + "</p>";        
        str += "<table>";
        str += "<tbody>";        
        str += "<tr>";
        str += "<td style='width:23%; text-align:right'>";
        str += "Csv File<span class='redText'>*</span>";
        str += "</br>A properly formatted csv file.";
        str += "</td>";
        str += "<td class='search_panel' style='border:0;'>";            
        str += "<input type='file' id='uploadFile' style='display:none;'/>";
        str += "<p class='upload'><input type='text' id='txtFileName' style='display:none;'/></p><p class='browser' id='btnBrowser'>Browser</p>";       
        str += "</td>";
        str += "</tr>";        
        str += "<tr>";
        str += "<td>";
        str += "";
        str += "</td>";
        str += "<td>";
        str += "<p class='submit'>Upload</p>";
        str += "</td>";
        str += "</tr>";
        str += "</tbody>";
        str += "</table>";
        str += "</div>";
        $(element).html(str);


        obj.OnClickBrowser();

        obj.Changed();

        obj.OnClickSubmit(type, id);

        obj.OnCLickDownloadSample(type, id, consortiumName);

    }

    obj.OnClickSubmit = (type, id) => {
        $('.httgridview').find('p.submit').on('click', function () {
            var data = new FormData();
            var file = $('#uploadFile')[0].files[0];
            data.append('file', file);
            data.append('type', type);
            data.append("id", id);
            $.ajax({
                type: 'POST',
                url: '../Handlers/Handler_Imports.ashx',
                dataType: "json",
                data: data,
                success: function (status) {
                    if (status.Status === 'OK') {
                        var notification = $('.notification');
                        notification.html("Successfully " + status.Message);
                        notification.show("slide", { direction: "left" }, 100).delay(3000).hide("drop", { direction: "up" }, 500);

                    }

                },
                processData: false,
                contentType: false,
                error: function () {

                }
            });
        });
    }
       

    obj.OnClickBrowser = () => {
        $('#btnBrowser').on('click', function () {
            $('#uploadFile').click();
        })
    }

    obj.Changed = () => {
        $('#uploadFile').change(function () {
            var text = $(this).val().replace(/C:\\fakepath\\/ig, '');
            $('#txtFileName').val(text).show();
        });
    }

    obj.OnCLickDownloadSample = (type, id, consortiumName) => {
        $('#btnLoadSampleFile').on('click', function () {
            $.ajax({
                type: 'POST',
                url: '../Handlers/Handler_GetImports.ashx',
                dataType: "json",
                data: {
                    type: type,
                    id: id,
                    NewName: consortiumName
                },
                success: function (status) {
                    if (status.Status === 'OK') {
                        window.open(status.Link, "_blank");
                    }

                },
                error: function () {

                }
            });
        });
    }
    return obj;
}