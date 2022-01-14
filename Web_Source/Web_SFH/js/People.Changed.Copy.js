function PeopleChangedCopy() {
    var obj = new Object();
    obj.fieldKey = new FieldKeys().data;

    obj.Init = (type,data,id) => {
        obj.CreatedContent('.httgridview', type, data, id);
    }

    obj.CreatedContent = (element, type, data, id) => {
        var idata = data.find(x => x.ID == id);
        var str = "";
        var str = "";
        str += "<div class='modify'>";
        str += "<p class='notification'></p>";
        str += "<p class='title'>" + type + "</p>";
        str += "<table>";
        str += "<tbody>";
        str += "<tr>";
        str += "<td colspan='2'>";
        if (type === "Copy Person") {
            str += "Press yes to add " + idata.Firstname + " " + idata.Lastname + " to another company. The new record will have all the same values as this existing person, while the original person record will be preserved.";
        }
        else {
            str += "Press yes to move " + idata.Firstname + " " + idata.Lastname + " from " + idata.CompanyName + " to the following company. Note that this will move their existing test history to the new company, and any access they have (eg Client Admin) will need to be re-enabled.";
        }
        str += "</td>";
        str += "</tr>";
        str += "<tr>";
        str += "<td style='width:20%;'>";
        str += "<input type='hidden' id='txtID' />";
        str += "Company name <span class='redText'>*</span>";
        str += "</td>";
        str += "<td class='search_panel' style='80%'>";
        str += "<input type='text' id='txtCompanyName' style='width:90%;border:0;'/>";
        str += "<div id='company' class='div_panel'>";       
        str += "</div>";
        str += "</td>";
        str += "</tr>";
        str += "<tr>";
        str += "<td>";
        str += "";
        str += "</td>";
        str += "<td>";
        str += "<p class='submit'>YES</p>";
        str += "</td>";
        str += "</tr>";
        str += "<tr>";
        str += "<td colspan='2' id='error' class='redText'>";
        str += "</td>";
        str += "</tr>";
        str += "</tbody";
        str += "</table>";
        str += "</div>";
        $(element).html(str);

        obj.onKeyupInput();

        obj.OnClickSubmit(id,type);

    }

    obj.OnClickSubmit = (id, type) => {
        $('.httgridview').find('p.submit').on('click', function () {
            var idata = JSON.parse(localStorage.getItem("data"));

            var oldID = idata.id;
            var searchElement = $('.div_panel').find('p');
            var newID = searchElement.attr('data-id');
            var name = searchElement.html();
            if (newID === undefined) {
                $('#error').html("Please chosed a company.");
                $('.search_panel').find('input').show().focus();   
                return;
            }
           // console.log("OnClickSubmit => ID: " + newID + " / " + oldID + " Name: " + name + " / " + idata.data.find(x => x.CompanyID == oldID).CompanyName + " People ID: " + id + " Action type:: " + type);
            var data = {};
            data[obj.fieldKey.ID] = id;
            data[obj.fieldKey.OldID] = oldID;
            data[obj.fieldKey.NewID] = newID;
            data[obj.fieldKey.OldName] = idata.data.find(x => x.CompanyID == oldID).CompanyName;
            data[obj.fieldKey.NewName] = name;
            data[obj.fieldKey.Cmd] = type === "Change Company" ? "Changed" : "Copy";
            //console.log("People Changed Copy:" + JSON.stringify(data));
            obj.Submit(data);
        });
    }

    obj.Submit = (data) => {
        $.ajax({
            type: 'POST',
            url: '../Handlers/Handler_ChangedCopyPersonal.ashx',
            dataType: "json",
            data: data,
            success: function (status) {                
                if (status.Status === 'OK') {
                    var notification = $('.notification');
                    notification.html("Successfully " + status.Message);
                    notification.show("slide", { direction: "left" }, 100).delay(3000).hide("drop", { direction: "up" }, 500);
                    
                }

            },
            error: function () {

            }
        });
    }

    //Display State city panel    
    obj.onKeyupInput = function () {
        $('.modify').find('input').on('keyup', function () {
            var element = $(this);
            var id = element.attr('id');
            var value = element.val();
            element.val(value.toUpperCase());

            var elementPanel = $('#panel');
            var top = element.offset().top
            var left = element.offset().left;
           // var id = element.attr('id');
            obj.AutoComplate(element, elementPanel, left, top, id);
        });

    }

    obj.ShowElement = function (element,width, left, top, id) {
        element.offset({ left: left - 4, top: top + 22 });
        element.width(width);
        element.show();
    }

    obj.HideElement = function (element) {
        if (!$(element).is(':hidden')) {
            //$(element).hide();
            $(element).offset({ top: 0, left: 0 }).hide();

        }
    }

    obj.onCickDatePanel = function (id) {
        $('#panel').find('.li').on('click', function () {
            var element = $(this);
           
            var searchPanel = $('.search_panel');
            var str = "<p class='item' data-id='" + element.attr('data-id') + "' style='width:" + (searchPanel.width() - 50) + "px'>";
            str += element.html();
            str += "</p>";
            searchPanel.find('input').hide().val('');
            $('.div_panel').show().html(str).on('click', function () {
                $(this).html('').hide();
                $('.search_panel').find('input').show().focus();
            });
            obj.HideElement('#panel');
        });

    }

    obj.AutoComplate = function (elementSelect, elementPanel, left, top, id) {
        var v = elementSelect.val();
        var data = JSON.parse(localStorage.getItem("data")).data;
        //console.log("AutoComplate Data:" + localStorage.getItem("data"));
        var width = elementSelect.width();
        var kq = false;
        if (v.length > 0) {
            v = v.toUpperCase();
            $('#panel').html('');
            var str = "<ul class='cells' style='width:" + (width - 30) + "px;' >";
            data.map((value) => {
                if (value.CompanyName.indexOf(v) != -1) {
                    str += "<li class='li' data-id='" + value.CompanyID + "'>";
                    str += value.CompanyName;
                    str += "</li>";
                    kq = true;
                    
                }
            });

            str += " </ul>";
           // console.log("AutoComplate: " + str);
            if (kq) {
                obj.ShowElement(elementPanel,width, left, top, id);
                $('#panel').html(str);
                obj.onCickDatePanel(id);
            }

        }

    }


    return obj;
}