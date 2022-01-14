/// <reference path="../scripts/jquery-3.6.0.min.js" />
/// <reference path="activitygriddata.js" />
/// <reference path="fieldkeys.js" />


function Activities() {
    var obj = new Object();
    var fieldKey = new FieldKeys();
    obj.fieldkey = fieldKey.data;
    obj.width = $(window).width();

    obj.Init = function (element, data, id, gridView, type) {
        obj.CreateModifyContent(element, data, id, gridView, type);
    }

    obj.onClickSubmit = function (gridView, type) {
        $('.httgridview').find('p.submit').on('click', function () {
            var id = $(this).attr('id');
            if (id === 'btnCreated') {
                obj.CreateModify(obj.fieldkey, gridView, type);
            }
        });
    }
        

    obj.CreateModify = function (fieldkey, gridView, type) {
        obj.Cmd = $('#btnCreated').html();
        obj.Cmd = obj.Cmd.substr(0, 1);
        var data = {};
        var comment = $('textarea#txtComment');
        if (comment.val() === undefined || comment.val() === '' || comment.val() === null) {
            obj.ShowNotification("Please fill out this field.");
            comment.focus();
            return;
        }

        data[fieldkey.ID] = $('#txtID').val();
        data[fieldkey.Details] = comment.val();
        data[fieldkey.Type] = type;
        data[fieldkey.Action] = "Comment on " + $('#txtName').val();
        data[fieldkey.Cmd] = "C";
       
        $.ajax({
            type: 'POST',
            dataType:"json",
            url: '../Handlers/Handler_CreateActivities.ashx',
            data: data,
            success: function (status) {                
                if (status.Status === 'OK') {
                    obj.ShowNotification("Comment was successfully add.");
                    
                    if (gridView !== undefined && gridView !== '')
                        gridView.LoadData({ "Type": type });

                    //Reset all value 

                    obj.ResetNull();

                }

            },
            error: function () {

            }
        });


    }

    obj.ShowNotification = function (message) {
        var notification = $('.notification');
        notification.html(message);
        notification.show("slide", { direction: "left" }, 100).delay(3000).hide("drop", { direction: "up" }, 500);
    }

    obj.CreateModifyContent = function (element,data, id, gridView, type) {
        var str = "";
        str += "<div class='modify' style='height:280px;'>";
        str += "<p class='notification'></p>";
        str += "<p class='title'>Add Comment</p>";
        str += "<table>";
        str += "<tbody>";
        str += "<tr class='account'>";
        str += "<td>";
        str += "<input type='hidden' id='txtID' value='" + id + "' />";
        var name = "";
        //console.log("Activities: " + id + " data: " + JSON.stringify(data));
        switch (type) {
            case 1:
                name = data.find(x => x.CompanyID == id).CompanyName;
                break;
            case 2:
                idata = data.find(x => x.PrimaryID === id.split("_")[1]);
                if (idata !== undefined) {
                    name = idata.Lastname === undefined ? "" : idata.Lastname;
                    name += idata.Firstname === undefined ? "" : " " + idata.Firstname;
                }                
                break
        }
       
        str += "<input type='hidden' id='txtName' value='" + name + "' />";
        str += "Comment*";
        str += "</td>";
        str += "<td colspan='3'>";
        str += "<textarea id='txtComment' style='height:100px; width:420px;'></textarea>";
        str += "</td>";

        str += "</tr>";
       
        str += "<tr>";
        str += "<td colspan='3'>";
        str += "";//"<p id='btnCancel' class='submit'>Cancel</p>";
        str += "</td>";
        str += "<td colspan='1'>";        
        str += "<p id='btnCreated' class='submit'>SAVE</p>";
        str += "</td>";
        str += "</tr>";
        str += "<tr>";
        str += "<td colspan='4' id='error' class='redText'>";
        str += "</td>";
        str += "</tr>";
        str += "</tbody>";
        str += "</table>";
        str += "</div>";
        $(element).html(str);

        obj.onClickSubmit(gridView, type);       
    }

    obj.ResetNull = function () {

        //$('#txtID').val('');
        //$('#txtCompanyName').val('');
        $('textarea#txtComment').val('');

    }

    return obj;
}