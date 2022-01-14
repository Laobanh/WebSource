/// <reference path="../scripts/jquery-3.6.0.min.js" />
/// <reference path="fieldkeys.js" />


function Schedules() {
    var obj = new Object();
    obj.fieldKey = new FieldKeys().data;
    obj.Companies = {};


    obj.Init = (data,id) => {
        //configurations.always(function (data) {
        //    obj.CreatedContent('.httgridview', data);
        //});   
        var idata = undefined;
        if (data !== undefined)
            idata = data.find(x => x.ID === id);
        obj.GetConfigurations(idata);


    }

    obj.CreatedContent = (element, configuration, idata) => {
        var str = "";
        var i = 0;
        var isEdit = false;
        if (idata !== undefined)
            isEdit = true;

        str += "<div class='modify' style='width:1100px;'>";

        str += "<table>";
        str += "<tbody>";
        //Top
        str += "<tr>";
        str += "<td style='width:50%;'>";
        str += "<p class='notification'></p>";
        str += "</td>";
        str += "<td style='width:50%;'>";
        str += "</td>";
        str += "</tr>";
        //Middle
        str += "<tr>";

        str += "<td style='width:50%; padding-right:20px;'>";
        //Random selections details
        str += "<p class='title'>Random selections details</p>";
        str += "<table>";
        str += "<tbody>";
        str += "<tr>";
        str += "<td style='width:35%;'>";
        str += "Pull For <span class='redText'>*</span>";
        str += "</td>";
        str += "<td >";
        str += "<select id='selPull'>";
        i = 0;
        configuration.Pull.map(function (r) {
            if (isEdit) {
                if (i == idata.Type)
                    str += "<option value='" + i + "'>" + r + "</option>";
            }
            else
                str += "<option value='" + i + "' " + (i === 0 ? " selected='selected'" : "") + ">" + r + "</option>";
            i++;
        });
        //str += "<option value='0' selected='selected'>Pull For Company</option>";
        //str += "<option value='1'>Pull For Consortium</option>";
        str += "</select>";
        str += "</td>";
        str += "</tr>";

        str += "<tr>";
        str += "<td id='lblPull'>";
        str += "Company";
        str += "</td>";
        str += "<td>";
        str += "<select id='selCompany'>";
        str += "</select>";

        str += "<select id='selConsortium' style='display:none;'>";
        str += "</select>";
        str += "</td>";
        str += "</tr>";

        str += "<tr>";
        str += "<td>";
        str += "Is DOT";
        str += "</td>";
        str += "<td>";
        str += "<select id='selDot'>";
        i = 0;
        configuration.Dot.map(function (dot) {
            if (isEdit) {
                if (i === idata.IsDot)
                    str += "<option value='" + i + "'>" + dot + "</option>";
            }
            else
                str += "<option value='" + i + "'>" + dot + "</option>";
            i++;
        });
        str += "</select>";
        str += "</td>";
        str += "</tr>";
        //Eligible
        str += "<tr id='eligible' style='display:none;'>";
        str += "<td>";
        str += "Eligible";
        str += "</td>";
        str += "<td>";
        str += "<span id='txtDonors'>";
        str += "...";
        str += "</span > ";
        str += "<span> donors</span>";
        str += "</td>";
        str += "</tr > ";

        //White space
        str += "<tr><td colspan='2'>";
        str += "<p style='height:46px; width:100%;'></p>";
        str += "</td></tr>";
        str += "</tbody>";
        str += "</table>";
        //End Random Selections details

        //Schedules details
        str += "<p style='height:40px; width:100%;'></p>";
        str += "<p class='title'>Schedules details</p>";
        str += "<table>";
        str += "<tbody>";
        //End On
        str += "<td style='width:35%;'>";
        str += "End On";
        str += "</td>";
        str += "<td >";
        if (isEdit) {
            str += "<input type='text' id='txtEndOn' value='" + idata.Details.EndOn + "' disabled/>";
        }
        else {
            var d = new Date();
            str += "<input type='text' id='txtEndOn' value='12/31/" + d.getFullYear() + "'/>";
        }

        str += "</td>";
        str += "</tr>";

        str += "<tr>";
        str += "<td>";
        str += "Run time";
        str += "</td>";
        str += "<td >";
        if (isEdit)
            str += "<input id='txtRunTime' value='" + idata.Details.RunTime + "' disabled />";
        else
            str += "<input type='time' id='txtRunTime' value='01:00' min='00:00' max='23:59' required />";
        str += "</td>";
        str += "</tr>";

        str += "<tr>";
        str += "<td>";
        str += "Request";
        str += "</td>";
        str += "<td>";
        str += "<select id='selRepeat'>";
        i = 0;
        configuration.Repeat.map(function (r) {
            if (isEdit) {
                if (i === idata.Details.Repeats)
                    str += "<option value='" + i + "' " + (i === 0 ? "selected='selected'" : '') + ">" + r + "</option>";
            }
            else
                str += "<option value='" + i + "' " + (i === 0 ? "selected='selected'" : '') + ">" + r + "</option>";
            i++;
        });
        str += "</select>";
        str += "</td>";
        str += "</tr>";

        str += "<tr>";
        str += "<td>";
        str += "Number of Times";
        str += "</td>";
        str += "<td >";
        if (isEdit)
            str += "<input value='" + idata.Details.NumberOfTimes + "' disabled/>";
        else
            str += "<input type='number' id='txtNumberOfTimes' value='1' min='1' max='10'/>";
        str += "</td>";
        str += "</tr>";

        str += "<tr>";
        str += "<td>";
        str += "Day Of Week";
        str += "</td>";
        str += "<td>";
        var dayOfWeek = [0, 1, 1, 1, 1, 1, 0];
        if (isEdit) {
            var ws = idata.Details.DayOfWeek.split(',');
            for (var n = 0; n < ws.length; n++) {
                dayOfWeek[n] = parseInt(ws[n]);
            }
        }
        var dayOfWeekText = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
        for (var k = 0; k < dayOfWeek.length; k++) {
            if (k > 0 && k < 7) {
                str += "<input type='checkbox' id='chk" + k + "'" + (dayOfWeek[k] === 1 ? "checked = 'checked'" : "") + (isEdit ? "disabled" : "") + "/>";
                str += "<label for='chk" + k + "'>" + dayOfWeekText[k] + "</label>";
                str += "</br>";
            }
        }

        str += "<input type='checkbox' id='chk0' " + (dayOfWeek[0] === 1 ? "checked='checked'" : "") + (isEdit ? "disabled" : "") + "/>";
        str += "<label for='chk0'>" + dayOfWeekText[0] + "</label>";
       

        //str += "<input type='checkbox' id='chk2' checked='checked'/>";
        //str += "<label for='chk2'>Tuesday</label>";
        //str += "</br>";

        //str += "<input type='checkbox' id='chk3' checked='checked'/>";
        //str += "<label for='chk3'>Wednesday</label>";
        //str += "</br>";

        //str += "<input type='checkbox' id='chk4' checked='checked'/>";
        //str += "<label for='chk4'>Thursday</label>";
        //str += "</br>";

        //str += "<input type='checkbox' id='chk5' checked='checked'/>";
        //str += "<label for='chk5'>Friday</label>";
        //str += "</br>";

        //str += "<input type='checkbox' id='chk6' />";
        //str += "<label for='chk6'>Saturday</label>";
        //str += "</br>";

        //str += "<input type='checkbox' id='chk0' />";
        //str += "<label for='chk0'>Sunday</label>";

        str += "</td>";
        str += "</tr>";

        str += "<tr>";
        str += "<td>";
        str += "Run A Selection Now";
        str += "</td>";
        str += "<td >";
        if (isEdit)
            str += "<input type='checkbox' id='txtRunNow'" + (idata.Details.RunNow === 1 ? "checked = 'checked' disabled" : "") + "/>";
        else
            str += "<input type='checkbox' id='txtRunNow' checked='checked'/>";
        //white space heigth
        str += "<p style='height:53px; width:100%;'></p>";
        str += "</td>";
        str += "</tr>";

        str += "</tbody>";
        str += "</table>";

        //End Schedule details
        str += "</td>";


        //Specimen 1
        str += "<td style='width:50%; padding-right:10px;'>";

        str += "<p class='title'>Specimen 1</p>";
        str += "<table>";
        str += "<tbody>";
        //Specimen type 1
        str += "<tr>";
        str += "<td style='width:35%;'>";
        str += "Type";
        str += "</td>";
        str += "<td >";
        str += "<select id='selSpecimenType1'>";
        i = 0;
        configuration.SpecimenType.map(function (r) {
            if (isEdit) {
                // console.log(idata.Details.SpecimenType1);
                if (r === idata.Specimen1.Type)
                    str += "<option value='" + r + "'>" + r + "</option>";
            }
            else
                str += "<option value='" + r + "' " + (i === 0 ? "selected='selected'" : '') + ">" + r + "</option>";
            i++;
        });
        str += "</select>";
        str += "</td>";
        str += "</tr>";
        //Collection site 1
        str += "<tr>";
        str += "<td>";
        str += "Collection Site";
        str += "</td>";
        str += "<td>";
        if (isEdit)
            str += "<input type='text' id='txtCollectionSite1' value='" + idata.Specimen1.CollectionSite + "' disabled />";
        else
            str += "<input type='text' id='txtCollectionSite1' value='SANTAFE HEALTH CLINIC & LAI CHIRO' />";
        str += "</td>";
        str += "</tr>";
        //NUmber donor 1
        str += "<tr>";
        str += "<td>";
        str += "Num Donors<span class='redText'>*</span>";
        str += "</td>";
        str += "<td>";
        if (isEdit)
            str += "<input id='txtNumberDonor1' value='" + idata.Specimen1.NumberDonor + "' disabled />";
        else
            str += "<input type='number' id='txtNumberDonor1' value='50' min='1' max='100' step='0.01'/>";
        str += "</td>";
        str += "</tr>";
        //Number Donor Type 1
        str += "<tr>";
        str += "<td style='width:35%;'>";
        str += "Num Donor Type";
        str += "</td>";
        str += "<td >";
        str += "<select id='selNumberDonorType1'>";
        i = 0;
        configuration.NumberOfDonorType.map(function (r) {
            if (isEdit) {
                if (i === idata.Specimen1.NumberDonorType)
                    str += "<option>" + r + "</option>";
            }
            else
                str += "<option value='" + i + "' " + (i === 1 ? "selected='selected'" : '') + ">" + r + "</option>";
            i++;
        });
        str += "</select>";
        str += "</td>";
        str += "</tr>";

        //NUmber donor Alternate 1
        str += "<tr>";
        str += "<td>";
        str += "Num Donors Alt";
        str += "</td>";
        str += "<td>";
        if (isEdit)
            str += "<input value='" + (idata.Specimen1.AlternateNumberDonor === '0' ? '' : idata.Specimen1.AlternateNumberDonor) + "' disabled/>";
        else
            str += "<input type='number' id='txtNumberDonorAlt1' min='0' max='100' step='1'/>";
        str += "</td>";
        str += "</tr>";
        //Number Donor Type 1
        str += "<tr>";
        str += "<td style='width:35%;'>";
        str += "Num Donor Alt Type";
        str += "</td>";
        str += "<td >";
        str += "<select id='selNumberDonorAltType1'>";
        i = 0;
        configuration.NumberOfDonorType.map(function (r) {
            if (isEdit) {
                if (i === idata.Specimen1.AlternateNumberDonorType)
                    str += "<option>" + r + "</option>";
            }
            else
                str += "<option value='" + i + "' " + (i === 1 ? "selected='selected'" : '') + ">" + r + "</option>";
            i++;
        });
        //str += "<option value='0' selected='selected'>People from the group</option>";
        //str += "<option value='1'>Percent of people from the group</option>";
        str += "</select>";
        str += "</td>";
        str += "</tr>";
        //MRO 1
        str += "<tr>";
        str += "<td style='width:35%;'>";
        str += "Mro";
        str += "</td>";
        str += "<td >";
        if (isEdit)
            str += "<input value='" + (idata.Specimen1.MRO === '0' ? '' : idata.Specimen1.MRO) + "' disabled />";
        else
            str += "<input type='text' id='txtMro1' />";
        str += "</select>";
        str += "</td>";
        str += "</tr>";
        //LAB 1
        str += "<tr>";
        str += "<td style='width:35%;'>";
        str += "Lab";
        str += "</td>";
        str += "<td >";
        if (isEdit)
            str += "<input value='" + (idata.Specimen1.Lab === '0' ? '' : idata.Specimen1.Lab) + "' disabled/>";
        else
            str += "<input type='text' id='txtLab1' />";
        str += "</td>";
        str += "</tr>";

        str += "</tbody>";
        str += "</table>";
        //End Specimen 1

        str += "<p style='height:20px; width:100%;'></p>";
        //Specimen 2       
        str += "<p class='title'>Specimen 2</p>";
        str += "<table>";
        str += "<tbody>";
        //Specimen type 2
        str += "<tr>";
        str += "<td style='width:35%;'>";
        str += "Type";
        str += "</td>";
        str += "<td >";
        str += "<select id='selSpecimenType2'>";
        i = 0;
        configuration.SpecimenType.map(function (r) {
            if (isEdit) {
                if (r === idata.Specimen2.Type)
                    str += "<option>" + r + "</option>";
            }
            else
                str += "<option value='" + r + "' " + (i === 1 ? "selected='selected'" : '') + ">" + r + "</option>";
            i++;
        });
        str += "</select>";
        str += "</td>";
        str += "</tr>";
        //Collection site 2
        str += "<tr>";
        str += "<td>";
        str += "Collection Site";
        str += "</td>";
        str += "<td>";
        if (isEdit)
            str += "<input value='" + idata.Specimen2.CollectionSite + "' disabled/>";
        else
            str += "<input type='text' id='txtCollectionSite2' value='SANTAFE HEALTH CLINIC & LAI CHIRO' />";
        str += "</td>";
        str += "</tr>";
        //NUmber donor 2
        str += "<tr>";
        str += "<td>";
        str += "Num Donors<span class='redText'>*</span>";
        str += "</td>";
        str += "<td>";
        if (isEdit)
            str += "<input value='" + idata.Specimen2.NumberDonor + "' disabled/>";
        else
            str += "<input type='number' id='txtNumberDonor2' value='20' min='1' max='100' step='1'/>";
        str += "</td>";
        str += "</tr>";
        //Number Donor Type 2
        str += "<tr>";
        str += "<td style='width:35%;'>";
        str += "Num Donor Type";
        str += "</td>";
        str += "<td >";
        str += "<select id='selNumberDonorType2'>";
        i = 0;
        configuration.NumberOfDonorType.map(function (r) {
            if (isEdit) {
                if (i === idata.Specimen2.NumberDonorType)
                    str += "<option>" + r + "</option>";
            }
            else
                str += "<option value='" + i + "' " + (i === 1 ? "selected='selected'" : '') + ">" + r + "</option>";
            i++;
        });
        //str += "<option value='0' >People from the group</option>";
        //str += "<option value='1' selected='selected'>Percent of people from the group</option>";
        str += "</select>";
        str += "</td>";
        str += "</tr>";

        //NUmber donor Alternate 2
        str += "<tr>";
        str += "<td>";
        str += "Num Donors Alt";
        str += "</td>";
        str += "<td>";
        if (isEdit)
            str += "<input value='" + (idata.Specimen2.AlternateNumberDonor === 0 ? '' : idata.Specimen2.AlternateNumberDonor) + "' disabled/>";
        else
            str += "<input type='number' id='txtNumberDonorAlt2'  min='0' max='100' step='1'/>";
        str += "</td>";
        str += "</tr>";
        //Number Donor Type 2
        str += "<tr>";
        str += "<td style='width:35%;'>";
        str += "Num Donor Alt Type";
        str += "</td>";
        str += "<td >";
        str += "<select id='selNumberDonorAltType2'>";
        i = 0;
        configuration.NumberOfDonorType.map(function (r) {
            if (isEdit) {
                if (i === idata.Specimen2.AlternateNumberDonorType)
                    str += "<option>" + r + "</option>";
            }
            else
                str += "<option value='" + i + "' " + (i === 1 ? "selected='selected'" : '') + ">" + r + "</option>";
            i++;
        });
        //str += "<option value='0' selected='selected'>People from the group</option>";
        //str += "<option value='1'>Percent of people from the group</option>";
        str += "</select>";
        str += "</td>";
        str += "</tr>";
        //MRO 2
        str += "<tr>";
        str += "<td style='width:35%;'>";
        str += "Mro";
        str += "</td>";
        str += "<td >";
        if (isEdit)
            str += "<input value='" + (idata.Specimen1.MRO === '0' ? '' : idata.Specimen1.MRO) + "' disabled />";
        else
            str += "<input type='text' id='txtMro2' />";
        str += "</select>";
        str += "</td>";
        str += "</tr>";
        //LAB 2
        str += "<tr>";
        str += "<td style='width:35%;'>";
        str += "Lab";
        str += "</td>";
        str += "<td >";
        if (isEdit)
            str += "<input value='" + (idata.Specimen1.Lab === '0' ? '' : idata.Specimen1.Lab) + "' disabled/>";
        else
            str += "<input type='text' id='txtLab2' />";
        str += "</td>";
        str += "</tr>";

        str += "</tbody>";
        str += "</table>";
        str += "</td>";
        //End Specimen 2
        str += "</tr>";



        //bottom
        //
        str += "<tr>";
        str += "<td colspan='2' >";
        if (!isEdit) {
            str += "<p class='redText'>Note: ";
            str += "If the number of Donor selected is more than the number of Donor in the list, the entire list will be selected</p>";
        }

        str += "</td>";
        str += "</tr>";

        str += "<tr>";
        str += "<td>";
        str += "<p class='notification'></p>";
        str += "</td>";
        str += "<td style='float:right; margin-right:10px;'>";
        if (!isEdit)
            str += "<p class='submit'>Save</p>";
        str += "</td>";
        str += "</tr>";


        str += "</tbody>";
        str += "</table>";
        str += "</div>";

        $(element).html(str);

        obj.Changed();

        obj.OnClickSubmit();

        var companyID = 0, consortiumID = 0;
        if (isEdit) {
            companyID = idata.CompanyID;
            consortiumID = idata.ConsortiumID;
            if (idata.Type === 0) {
                $('#selCompany').show();
                $('#selConsortium').hide();
                $('#lblPull').html('Company');
            } else {
                $('#selCompany').hide();
                $('#selConsortium').show();
                $('#lblPull').html('Consortium');
            }
            $('#eligible').hide();
        } else
            $('#eligible').show();

        obj.GetCompanies(companyID, isEdit);

        obj.GetConsortium(consortiumID, isEdit);

    }
        
    obj.GetData = () => {
        var data = {};
        data[obj.fieldKey.Cmd] = "C";
        //Type
        var value = $('#selPull').select().val();

        data[obj.fieldKey.Type] = value;
        //CompanyID, ConsortiumID
        if (value === "0") {
            var companyID = $('#selCompany').select().val();
            var name = obj.Companies.find(x => x.CompanyID === parseInt(companyID)).CompanyName;
            data[obj.fieldKey.CompanyID] = companyID;
            data[obj.fieldKey.ConsortiumID] = 0;
            data[obj.fieldKey.NewName] = "Random Selections for Company " + name;// $('#selCompany option:selected').text();
        } else {
            data[obj.fieldKey.CompanyID] = 0;
            data[obj.fieldKey.ConsortiumID] = $('#selConsortium').select().val();
            data[obj.fieldKey.NewName] = "Random Selections for Consortium " + $('#selConsortium option:selected').text();
        }
        //IS DOT
        data[obj.fieldKey.IsDot] = $('#selDot').select().val();
        //End On
        data[obj.fieldKey.EndOn] = $('#txtEndOn').val();
        //RunTime
        data[obj.fieldKey.RunTime] = $('#txtRunTime').val();
        //Repeat
        data[obj.fieldKey.Repeat] = $('#selRepeat').select().val();
        //Number OF Times
        data[obj.fieldKey.NumberOfTimes] = $('#txtNumberOfTimes').val();
        //Day Of week
        value = "";
        for (var i = 0; i < 7; i++) {
            value += ($('#chk' + i).is(':checked') ? "1" : "0") + ",";
        }
        data[obj.fieldKey.DayOfWeek] = value.substr(0, value.length - 1);
        //RunNow        
        data[obj.fieldKey.RunNow] = $('#txtRunNow').is(":checked") ? 1 : 0;
        //Specimen 1
        //Specimen Type
        data[obj.fieldKey.SpecimenType1] = $("#selSpecimenType1").select().val();
        //Collection site 1
        data[obj.fieldKey.CollectionSite1] = $('#txtCollectionSite1').val();
        //Number Donors 1
        data[obj.fieldKey.NumberDonor1] = $('#txtNumberDonor1').val();
        //Number Donors Type 1
        data[obj.fieldKey.NumberDonorType1] = $('#selNumberDonorType1').select().val();
        //Number Donors Alternate 1
        data[obj.fieldKey.AlternateNumberDonor1] = $('#txtNumberDonorAlt1').val();
        //Number Donors Type Alternate 1
        data[obj.fieldKey.AlternateNumberDonorType1] = $('#selNumberDonorAltType1').select().val();
        //Mro 1
        data[obj.fieldKey.Mro1] = $('#txtMro1').val();
        //Lab 1
        data[obj.fieldKey.Lab1] = $('#txtLab1').val();

        //Specimen 2
        //Specimen Type
        data[obj.fieldKey.SpecimenType2] = $("#selSpecimenType2").select().val();
        //Collection site 2
        data[obj.fieldKey.CollectionSite2] = $('#txtCollectionSite2').val();
        //Number Donors 2
        data[obj.fieldKey.NumberDonor2] = $('#txtNumberDonor2').val();
        //Number Donors Type 2
        data[obj.fieldKey.NumberDonorType2] = $('#selNumberDonorType2').select().val();
        //Number Donors Alternate 2
        data[obj.fieldKey.AlternateNumberDonor2] = $('#txtNumberDonorAlt2').val();
        //Number Donors Type Alternate 2
        data[obj.fieldKey.AlternateNumberDonorType2] = $('#selNumberDonorAltType2').select().val();
        //Mro 2
        data[obj.fieldKey.Mro2] = $('#txtMro2').val();
        //Lab 2
        data[obj.fieldKey.Lab2] = $('#txtLab2').val();
        //console.log(JSON.stringify(data));
        return data;
    }

    obj.OnClickSubmit = () => {
        $('.httgridview').find('p.submit').on('click', function () {
            var data = obj.GetData();
            $('#popup').show();
            var kq = (parseInt(data.Type) === 0 ? "Company " + $('#selCompany option:selected').text() : "Consortium " + $('#selConsortium option:selected').text());
            var msg = "Are you sure you want to choose this " + kq + " ?";
            obj.AlertMessage("#dialog", "Notification", msg, "yes", "yes", "no", "no");
        });
    }

    obj.AlertMessage = (element, error, message, button1, label1, button2, label2) => {
        console.log(JSON.stringify(obj.GetData()));
        var str = "<p class='title'>";
        str += error;
        str += "</p>";
        str += "<p class='alert'>";
        str += message;
        str += "</p>";
        str += "<p class='yes button' id='" + button1.toLocaleLowerCase() + "'>" + label1.toLocaleUpperCase() + "</p>";
        if (button2 === undefined || button2 === "") {
            str += "<p class='cancel button' id='cancel'>Cancel</p>";
        } else {
            str += "<p class='cancel button' id='" + button2.toLocaleLowerCase() + "'>" + label2.toLocaleUpperCase() + "</p>";
        }
       
        $(element).html(str);
        obj.OnClickPopup();
    }

    obj.OnClickPopup = () => {
        $('.dialog').find('p').on('click', function () {
            var id = $(this).attr('id');
            id = id.toLocaleLowerCase();
            switch (id) {
                case "cancel":
                case "no":
                    $('#popup').hide();
                    $('.dialog').html('');
                    break;
                case "yes":
                    var msg = "Are you sure you want to run this " + obj.GetData().newname + " ?";
                    obj.AlertMessage("#dialog", "Warning !!!", msg, "ok", "ok", "no", "no");
                    break;                
                case "ok":
                    obj.Created();
                    $('#popup').hide();
                    $('.dialog').html('');
                    break;
            }
        });
    }

    obj.Created = () => {
        var data = obj.GetData();
        $.ajax({
            type: 'POST',
            url: '../Handlers/Handler_CreateSchedules.ashx',
            dataType: "json",
            data: data,
            success: function (status) {
                if (status.Status === 'OK') {
                    var notification = $('.notification');
                    notification.html("Successfully " + status.Message);
                    notification.show("slide", { direction: "left" }, 100).delay(3000).hide("drop", { direction: "up" }, 500);
                    //console.log(JSON.stringify(status));
                }
            },
            error: function () {

            }
        });
    }

    obj.Changed = () => {
        $('#selPull').change(function () {
            var id = $(this).select().val();
            var lbl = $('#lblPull');
            var selCompany = $('#selCompany');
            var selConsortium = $('#selConsortium');
            switch (id) {
                case "0":
                    lbl.html('Company');
                    selCompany.show();
                    selConsortium.hide();
                    var com = obj.Companies.find(x => x.CompanyID == selCompany.val());
                    $('#txtDonors').html(com.SumDriver);
                    break
                case "1":
                    lbl.html('Consortium');
                    selCompany.hide();
                    selConsortium.show();
                    obj.SumDonor(selConsortium.find("option:first-child").val());
                    break
                default:
                    lbl.html('Company');
                    selCompany.show();
                    selConsortium.hide();
                    break

            }
        });

        $('#selCompany').change(function () {
            var id = $(this).val();
            var com = obj.Companies.find(x => x.CompanyID == id);
            $('#txtDonors').html(com.SumDriver);
        });

        $('#selConsortium').change(function () {
            var id = $(this).val();

            obj.SumDonor(id);
        });
    }

    obj.SumDonor = (id) => {
        var kq = 0;
        obj.Companies.map(function (c) {
            if (c.ConsortiumId == id) {

                kq += parseInt(c.SumDriver);
            }

        })
        $('#txtDonors').html(kq);
    }

    obj.GetCompanies = (id, isEdit) => {
        $.ajax({
            type: 'POST',
            url: '../Handlers/Handler_GetCompanies.ashx',
            dataType: "json",
            data: {},
            success: function (status) {
                if (status.Status === 'OK') {
                    obj.Companies = status.Data;
                    var selCompany = $('#selCompany');
                    var str = "";
                    //console.log(data);
                    var i = 0;
                    obj.Companies.map((x) => {
                        if (isEdit) {
                            if (id == x.CompanyID) {
                                str += "<option value='" + x.CompanyID + "'>" + x.CompanyName;// + " - " + x.SumDriver;
                                str += "</option>";
                            }
                        } else {
                            str += "<option value='" + x.CompanyID + "'>" + x.CompanyName;// + " - " + x.SumDriver;
                            str += "</option>";
                            if (i === 0)
                                $('#txtDonors').html(x.SumDriver);
                        }

                        i++;
                    });
                    //console.log("SelCompany: " + str + " id/" + id);
                    selCompany.html(str);
                }

            },
            error: function () {

            }
        });
    }

    obj.GetConsortium = (id, isEdit) => {
        $.ajax({
            type: 'POST',
            url: '../Handlers/Handler_GetConsortiums.ashx',
            dataType: "json",
            data: {},
            success: function (status) {
                if (status.Status === 'OK') {
                    var data = status.Data;
                    var selConsortium = $('#selConsortium');
                    var str = "";
                    //str += "<option value='0'>Choose Company</option>";
                    data.map((x) => {
                        if (isEdit) {
                            if (id == x.ID)
                                str += "<option value='" + x.ID + "'>" + x.Name + "</option>";
                        }
                        else
                            str += "<option value='" + x.ID + "'>" + x.Name + "</option>";
                    });
                    //console.log("selConsortium: " + str);
                    selConsortium.html(str);
                }

            },
            error: function () {

            }
        });
    }
    
    obj.GetConfigurations = (idata,id) => {
        $.ajax({
            type: 'POST',
            url: '../Handlers/Handler_GetConfigurations.ashx',
            dataType: "JSON",
            success: function (status) {
                obj.CreatedContent('.httgridview', status, idata);
            },
            error: function () {
                console.log("Load error");
            }
        });
    }

    obj.Test = () => {
        return "Hello";
    }

    obj.Gets = () => {
        $.ajax({
            type: 'POST',
            url: '../Handlers/Handler_GetScheduleList.ashx',
            dataType: "json",
            data: {},
            success: function (status) {               
                if (status.Status === 'OK') {                   
                    localStorage.setItem("schedules", JSON.stringify(status.Data));
                }
            },
            error: function () {

            }
        });
    }
    return obj;
}