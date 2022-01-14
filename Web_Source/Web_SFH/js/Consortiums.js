/// <reference path="fieldkeys.js" />


function Consortiums() {

    var obj = new Object();
    obj.Cmd = "C";

    obj.fieldkey = new FieldKeys().data;

    obj.Init = function (element, data, id, isCreate) {
        obj.CreateModifyContent(element, isCreate);
        if (!isCreate && data !== undefined && data !== '')
            obj.FillData(data, id);
    }

    obj.Add = (element, consortiumID) => {
        var str = "";
        str += "<div class='modify'>";
        str += "<p class='notification'></p>";
        str += "<p class='title'>Add Companies</p>";
        str += "<table>";
        str += "<tbody>";
        str += "<tr>";
        str += "<td style='width:20%;'>";
        str += "<input type='hidden' id='txtID'/>";
        str += "Company name";
        str += "</td>";
        str += "<td class='search_panel' style='80%'>";
        str += "<input type='text' id='txtCompanyName' style='width:90%;border:0;'/>";
        str += "<div id='company' class='div_panel'>";
        str += "</div>";
        str += "</td>";
        str += "</tr>";
        str += "<tr>";
        str += "<td>";
        str += "<p id='btnCreated' class='submit'>Save</p>";
        str += "</td>";
        str += "<td>";
        str += "";
        str += "</td>";
        str += "</tr>";
        str += "<tr>";
        str += "<td colspan='2' id='error' class='redText'>";
        str += "</td>";
        str += "</tr>";
        str += "</tbody>";
        str += "</table>";
        str += "</div>";
        $(element).html(str);

        obj.onClickSubmit(consortiumID);

        //Register on CLick State and City
        obj.onKeyupInput();
    }

    obj.onClickSubmit = function (consortiumID) {
        $('.httgridview p.submit').on('click', function () {
            var id = $(this).attr('id');
            if (consortiumID !== undefined && consortiumID !== "") {
                obj.AddCompany(consortiumID);
            } else {
                if (id === 'btnCreated')
                    obj.CreateModify(obj.fieldkey);
                else if (id === "btnCancel")
                    obj.ResetNull();
            }

        });
    }

    obj.AddCompany = (consortiumID) => {
        var searchElement = $('.div_panel').find('p');
        var companyID = searchElement.attr('data-id');
        var name = searchElement.html();
        if (companyID === undefined) {
            $('#error').html("Please chosed a company.");
            $('.search_panel').find('input').show().focus();
            return;
        }
        var data = {};
        data[obj.fieldkey.Cmd] = "E";
        data[obj.fieldkey.ID] = companyID;
        data[obj.fieldkey.ConsortiumID] = consortiumID;

        $.ajax({
            type: 'POST',
            url: '../Handlers/Handler_CreateCompany.ashx',
            dataType: "json",
            data: data,
            success: function (status) {               
                if (status.Status === 'OK') {
                    var notification = $('.notification');
                    notification.html(name + " was successfully Add.");
                    notification.show("slide", { direction: "left" }, 100).delay(3000).hide("drop", { direction: "up" }, 500);
                    //Reset all value 
                    $('.div_panel').html('').hide();
                    $('.search_panel').find('input').show().focus();     

                }

            },
            error: function () {

            }
        });
    }

    obj.FillData = function (data, id) {
        var idata = data.find(x => x.ID == id);

        $('#txtID').val(idata.ID);
        var value = idata.Name;
        value = value === '0' ? '' : value;
        $('#txtName').val(value);
        $('#btnCreated').html("Modify");
    }

    obj.CreateModify = function (fieldkey) {
        obj.Cmd = $('#btnCreated').html();
        obj.Cmd = obj.Cmd.substr(0, 1);
        var data = {};
        var name = $('#txtName');
        
        if (name.val() === undefined || name.val() === '' || name.val() === '0' || name.val() === null) {
            $('#error').html('Please enter Consortium name.');
            name.focus();
            return;
        }
             

        data[fieldkey.ID] = $('#txtID').val();
        data[fieldkey.NewName] = name.val();
        data[fieldkey.Cmd] = obj.Cmd;
        //console.log(JSON.stringify(data));
        $.ajax({
            type: 'POST',
            url: '../Handlers/Handler_CreateConsortiums.ashx',
            dataType: "json",
            data: data,
            success: function (status) {
                console.log(status);
                if (status.Status === 'OK') {
                    var notification = $('.notification');
                    notification.html(name.val() + " was successfully " + (obj.Cmd === "C" ? "add new" : "update"));
                    notification.show("slide", { direction: "left" }, 100).delay(3000).hide("drop", { direction: "up" }, 500);

                    //Reset all value 
                    if (obj.Cmd === "C")
                        obj.ResetNull();

                }

            },
            error: function () {

            }
        });


    }

    obj.CreateModifyContent = function (element, isCreate) {
        var str = "";
        str += "<div class='modify'>";
        str += "<p class='notification'></p>";
        str += "<p class='title'>Consortium Details</p>";
        str += "<table>";
        str += "<tbody>";
        str += "<tr>";
        str += "<td>";
        str += "<input type='hidden' id='txtID' />";
        str += "Consrtium name";
        str += "</td>";
        str += "<td>";
        str += "<input type='text' id='txtName' style='width:485px;'/>";
        str += "</td>";
        str += "</tr>";  
        str += "<tr>";
        str += "<td>";
        str += "<p id='btnCreated' class='submit'>Created</p>";
        str += "</td>";
        str += "<td>";
        if (isCreate)
            str += "<p id='btnCancel' class='submit'>Cancel</p>";
        else
            str += "<p></p>";
        str += "</td>";
        str += "</tr>";
        str += "<tr>";
        str += "<td colspan='2' id='error' class='redText'>";
        str += "</td>";
        str += "</tr>";
        str += "</tbody>";
        str += "</table>";
        str += "</div>";
        $(element).html(str);

        obj.onClickSubmit();

        //Register on CLick State and City
        //obj.onKeyupInput();
    }

    obj.ResetNull = function () {
        $('#txtID').val('');
        $('#txtName').val('');
    }

    //Display State city panel    
    obj.onKeyupInput = function () {
        $('.modify').find('input').on('keyup', function () {
            var elementPanel = $('#panel');

            var element = $(this);            
            var id = element.attr('id');
            var value = element.val();
            element.val(value.toUpperCase());            
            var top = element.offset().top
            var left = element.offset().left;
           // console.log("OnKeyUpInput: " + top + "/" + left);
            obj.AutoComplate(element, elementPanel, left, top, id);
        });

    }

    obj.ShowElement = function (element, left, top, id) {
       // console.log("ShowElement: " + top + "/" + left);
        element.offset({ left: left - 13, top: top + 22 });
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
        $.ajax({
            type: 'POST',
            dataType: "JSON",
            url: '../Handlers/Handler_GetCompanies.ashx',
            data: {},
            success: function (status) {
                // console.log(JSON.stringify(status));
                if (status.Status === "OK") {
                    var width = elementSelect.width();
                    var kq = false;
                    if (v.length > 0) {
                        v = v.toUpperCase();
                        $('#panel').html('');
                        var str = "<ul class='cells' style='width:" + (width - 30) + "px;' >";                        
                        status.Data.map((value) => {
                           // if (value.ConsortiumId === undefined || value.ConsortiumId === "0") {
                                if (value.CompanyName.indexOf(v) != -1) {
                                    str += "<li class='li' data-id='" + value.CompanyID + "'>";
                                    str += value.CompanyName;
                                    str += "</li>";
                                    kq = true;

                                }
                           // }                           
                        });

                        str += " </ul>";
                       
                        if (kq) {
                            obj.ShowElement(elementPanel, left, top, id);
                            //console.log("AutoComplate: " + top + "/" + left);
                            $('#panel').html(str);
                            obj.onCickDatePanel(id);                           
                        }

                    }
                }
            },
            error: function () {
                //alert("Load MRO Report List Failed...");
            }
        });

    }

    return obj;
}