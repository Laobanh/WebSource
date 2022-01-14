/// <reference path="../scripts/jquery-3.6.0.min.js" />
/// <reference path="modecategory.js" />
/// <reference path="fieldkeys.js" />

function Personal() {

    var obj = new Object();

    obj.fieldkey = new FieldKeys().data;

    obj.stateCities = new StateCity().data;

    obj.modeCategories = ModeCategory.modeCategories;// ModeCategory.modeCategories;

    obj.Init = function (element, id, isCreated) {
        obj.CreateModifyContent(element, id, isCreated);
        if (!isCreated)
            obj.LoadData(id);
    }

    obj.onClickSubmit = function (companyid) {
        $('.modify').find('p.submit').on('click', function () {
            var id = $(this).attr('id');
            if (id === 'btnCreateUser') {
                obj.CreateModify(companyid);
            } else
                obj.ResetNull();
        });
    }

    obj.LoadData = function (ids) {
        id = ids.split('_');
        $.ajax({
            type: 'POST',
            url: '../Handlers/Handler_GetPersonal.ashx',
            data: { "id": id[1] },
            dataType: "JSON",
            success: function (status) {               
                if (status.Status === "error")
                    window.location = status.link;
                else if (status.Status === "OK") {
                    obj.FillData(status.Data, id);
                }

            },
            error: function () {

            }
        });
    }

    obj.FillData = function (data, id) {
        var idata = data;// data.find(x => x.ID == id);        
       // console.log("Personal Filldata: " + id + " data: " + JSON.stringify(data));

        $('#txtCompanyName').html(idata.CompanyName);

        $('#txtID').val(idata.ID);

        $('#txtDonorID').val(idata.Driver.PrimaryID);

        var value = idata.Driver.PrimaryIDExpirationDate;
        value = value === '0' ? '' : value;
        $('#txtExpirationDate').val(value);

        value = idata.PersonalInfo.Person.FirstName;
        value = value === '0' ? '' : value;
        $('#txtFirstName').val(value);

        value = idata.PersonalInfo.Person.LastName;
        value = value === '0' ? '' : value;
        $('#txtLastName').val(value);

        var option = "";
        var mode = (idata.Mode === '0' || idata.Mode === undefined ? 'FMCSA' : idata.Mode);
        //console.log(mode);
        Object.keys(obj.modeCategories).map(function (field) {
            option += "<option id='" + field + "'"
            if (field === mode) {
                option += "selected='selected' ";
            }
            option += ">" + field;
            option += "</option>";

        });
        $("#selMode").html(option);

        var category = idata.Driver.Category;
        //console.log(category);
        option = "";
        obj.modeCategories[mode].map(function (field) {
            option += "<option id='" + field + "'"
            if (field === category) {
                option += "selected='selected' ";
            }
            option += ">" + field;
            option += "</option>";

        });
        $('#selCategory').html(option);


        var title = idata.PersonalInfo.Person.Title;
        option = "";
        obj.fieldkey.PersonTitle.map(function (field) {
            option += "<option id='" + field + "'"
            if (field === title) {
                option += "selected='selected' ";
            }
            option += ">" + field;
            option += "</option>";

        });
        $("#selTitle").html(option);

        

        var Gender = idata.PersonalInfo.Person.Gender;
        option = "";
        obj.fieldkey.PersonGender.map(function (field) {
            option += "<option id='" + field + "'"
            if (field === Gender) {
                option += "selected='selected' ";
            }
            option += ">" + field;
            option += "</option>";

        });
        $("#selGender").html(option);

        

        value = idata.PersonalInfo.Person.DateOfBirth;
        value = value === '0' ? '' : value;
        $('#txtBirthday').val(value);

        value = idata.PersonalInfo.Contact.HomePhone;
        value = value == '0' ? '' : value;
        $('#txtHomePhone').val(value);

        value = idata.PersonalInfo.Contact.WorkPhone;
        value = value === '0' ? '' : value;
        $('#txtWorkPhone').val(value);

        value = idata.PersonalInfo.Contact.MobilePhone;
        value = value === '0' ? '' : value;
        $('#txtMobilePhone').val(value);

        value = idata.PersonalInfo.Contact.Email;
        value = value === '0' ? '' : value;
        $('#txtEmail').val(value);

        value = idata.PersonalInfo.Contact.Website;
        value = value === '0' ? '' : value;
        $('#txtWebsite').val(value);

        value = idata.PersonalInfo.Address.Address;
        value = value === '0' ? '' : value;
        $('#txtAddress').val(value);

        value = idata.PersonalInfo.Address.OfficeLocation;
        value = value === '0' ? '' : value;
        $('#txtOfficeLocation').val(value);

        value = idata.PersonalInfo.Address.Country;
        value = value === '0' ? '' : value;
        $('#txtCountry').val(value);

        value = idata.PersonalInfo.Address.City;
        value = value === '0' ? '' : value;
        $('#txtCity').val(value);

        value = idata.PersonalInfo.Address.State;
        value = value === '0' ? '' : value;
        $('#txtState').val(value);

        value = idata.PersonalInfo.Address.Zip
        value = value === 0 ? '' : value;
        $('#txtZip').val(value);

        $('#btnCreateUser').html('Modify Personal');        

        //console.log(obj.Cmd);        
        $("input[type=tel]").val(function (i, text) {
            text = text.replace(/(\d\d\d)(\d\d\d)(\d\d\d\d)/, "($1)$2-$3");
            return text;
        });
    }

    obj.CreateModify = function (companyid) {

        fieldkey = obj.fieldkey;
        obj.Cmd = $('#btnCreateUser').html();
        obj.Cmd = obj.Cmd.substr(0, 1);
        var data = {};        
        var first = $('#txtFirstName');
        var last = $('#txtLastName');
        var mobilePhone = $('#txtMobilePhone');
        var birthday = $('#txtBirthday');
        var donorID = $('#txtDonorID')
        if (donorID.val() === undefined || donorID.val() === '' || donorID.val() === '0') {       
            donorID.focus();
            $('#error').html('Please enter Donor ID.');
            return;
        } 
        else if (first.val() === undefined || first.val() === '' || first.val() === '0') {
            $('#error').html('Please enter Firstname.');
            first.focus();
            return;
        } else if (last.val() === undefined || last.val() === '' || last.val() === '0') {
            $('#error').html('Please enter Lastname.');
            last.focus();
            return;
        }
        else if (mobilePhone.val() === undefined || mobilePhone.val() === '' || mobilePhone.val() === '0') {
            $('#error').html('Please enter mobile phone.');
            mobilePhone.focus();
            return;
        } else if (birthday.val() === undefined || birthday.val() === '' || birthday.val() === '0') {
            $('#error').html('Please enter date of birth.');
            birthday.focus();
            return;
        }

        data[fieldkey.ID] = (companyid === undefined || companyid === "" ? $('#txtID').val() : companyid);
        data[fieldkey.PrimaryID] = $('#txtDonorID').val();
        data[fieldkey.Mode] = $('#selMode :selected').val();
        data[fieldkey.Category] = $('#selCategory :selected').val();
        data[fieldkey.PrimaryIDExpirationDate] = $('#txtExpirationDate').val();
        data[fieldkey.FirstName] = first.val();
        data[fieldkey.LastName] = last.val();
        data[fieldkey.Title] = $('#selTitle :selected').val();
        data[fieldkey.Gender] = $('#selGender :selected').val();
        data[fieldkey.DateOfBirth] = birthday.val();
        data[fieldkey.HomePhone] = $('#txtHomePhone').val();
        data[fieldkey.WorkPhone] = $('#txtWorkPhone').val();
        data[fieldkey.MobilePhone] = mobilePhone.val();
        data[fieldkey.Email] = $('#txtEmail').val();
        data[fieldkey.Website] = $('#txtWebsite').val();
        data[fieldkey.Address] = $('#txtAddress').val();
        data[fieldkey.OfficeLocation] = $('#txtOfficeLocation').val();
        data[fieldkey.Country] = $('#txtCountry').val();
        data[fieldkey.City] = $('#txtCity').val();
        data[fieldkey.State] = $('#txtState').val();
        data[fieldkey.Zip] = $('#txtZip').val();
        data[fieldkey.Cmd] = obj.Cmd;       

        console.log("Personal OnCreated: " + JSON.stringify(data));
        $.ajax({
            type: 'POST',
            url: '../Handlers/Handler_CreatePersonal.ashx',
            data: data,
            dataType:"JSON",
            success: function (status) {
                //console.log(status);
                if (status.Status === "error")
                    window.location = status.Link;
                else if (status.Status === "OK") {
                    obj.ShowNotification("Personal was successfully " + (obj.Cmd === "C" ? "Add" : "Modify"));
                    if (obj.Cmd === "C")
                        obj.ResetNull();
                }
                

            },
            error: function () {

            }
        });


    }

    obj.CreateModifyContent = function (element, id, isCreated) {
        var str = "";
        str += "<div class='modify'>";
        str += "<p class='notification'></p>";  
        str += "<p class='title'>Personal Details</p>";
        str += "<input type = 'hidden' id ='txtID' />";
        str += "<table>";
        str += "<tbody>";
        str += "<tr>";
        str += "<td colspan='2' class='title'>";
        str += "DRIVER INFO";
        str += "</td>";
        str += "<td colspan='2' id='txtCompanyName'>";
        str += "";
        str += "</td>";
        str += "</tr>";
        str += "<tr>";
        str += "<td>";
        str += "Driver ID";
        str += "</td>";
        str += "<td>";
        str += "<input type='text' id='txtDonorID' />";
        str += "</td>";
        str += "<td>";
        str += "Mode";
        str += "</td>";
        str += "<td>";
        str += "<select id='selMode'>";
        var mode = "FMCSA";
        Object.keys(obj.modeCategories).map(function (value) {
            str += "<option id='" + value + "'"
            if (value === mode) {
                str += "selected='selected' ";
            }
            str += ">" + value;
            str += "</option>";

        });
        str += "</select>";
        str += "</td>";
        str += "</tr>";

        str += "<tr>";
        str += "<td>";
        str += "Category";
        str += "</td>";
        str += "<td>";
        str += "<select id='selCategory'>";
        var category = "CLASS A";
        obj.modeCategories[mode].map(function (value) {
            str += "<option id='" + value + "'"
            if (value === category) {
                str += "selected='selected' ";
            }
            str += ">" + value;
            str += "</option>";

        });
        str += "</select>";
        str += "</td>";
        str += "<td>";
        str += "Expiration Date";
        str += "</td>";
        str += "<td>";
        str +="<input type='text' id='txtExpirationDate'/>"
        str += "</td>";
        str += "</tr>";

        //PErson Contact
        str += "<tr>";
        str += "<td colspan='4' class='title'>PERSON CONTACT</td>";
        str += "</tr>";

        str += "<tr>";
        str += "<td style='width:20%'>";
        str += "First name";
        str += "</td>";
        str += "<td style='width:30%'>";
        str += "<input type='text' id='txtFirstName' />";
        str += "</td>";
        str += "<td style='width:20%'>";
        str += "Last name";
        str += "</td>";
        str += "<td style='width:30%'>";
        str += "<input type='text' id='txtLastName' />";
        str += "</td>";
        str += "</tr>";
        str += "<tr>";
        str += "<td>";
        str += "Title";
        str += "</td>";
        str += "<td>";
        str += "<select id='selTitle'>";

        var title = "Mr";
        obj.fieldkey.PersonTitle.map(function (field) {
            str += "<option id='" + field + "'"
            if (field === title) {
                str += "selected='selected' ";
            }
            str += ">" + field;
            str += "</option>";

        });


        str += "</select>";
        str += "</td>";
        str += "<td>";
        str += "Gender";
        str += "</td>";
        str += "<td>";
        str += "<select id='selGender'>";
        var Gender = "Male";
        obj.fieldkey.PersonGender.map(function (field) {
            str += "<option id='" + field + "'"
            if (field === Gender) {
                str += "selected='selected' ";
            }
            str += ">" + field;
            str += "</option>";

        });
        str += "</select>";
        str += "</td>";
        str += "</tr>";
        str += "<tr>";
        str += "<td>";
        str += "Mobile phone";
        str += "</td>";
        str += "<td>";
        str += "<input type='tel' id='txtMobilePhone'/>";       
        str += "</td>";
        str += "<td>";
        str += "Home phone";
        str += "</td>";
        str += "<td>";
        str += "<input type='tel' id='txtHomePhone' />";
        str += "</td>";
        str += "</tr>";
        str += "<tr>";
        str += "<td>";
        str += "Work phone";
        str += "</td>";
        str += "<td>";
        str += "<input type='tel' id='txtWorkPhone' />";
        str += "</td>";
        str += "<td>";
        str += "Date Of Birth";        
        str += "</td>";
        str += "<td>";
        str += "<input type='text' id='txtBirthday' />";
        str += "</td>";
        str += "</tr>";
        str += "<tr>";
        str += "<td>";
        str += "Email";
        str += "</td>";
        str += "<td>";
        str += "<input type='text' id='txtEmail' />";
        str += "</td>";
        str += "<td>";
        str += "Website";
        str += "</td>";
        str += "<td>";
        str += "<input type='text' id='txtWebsite' />";
        str += "</td>";
        str += "</tr>";
        //Location
        str += "<tr>";
        str += "<td colspan='4' class='title'>Location</td>";
        str += "</tr>";
        str += "<tr>";
        str += "<td>";
        str += "Address";
        str += "</td>";
        str += "<td>";
        str += "<input type='text' id='txtAddress' />";
        str += "</td>";
        str += "<td>";
        str += "State";
        str += "</td>";
        str += "<td>";
        str += "<input type='text' id='txtState' class='stateCity' />";
        str += "</td>";
        str += "</tr>";
        str += "<tr>";
        str += "<td>";
        str += "City";
        str += "</td>";
        str += "<td>";
        str += "<input type='text' id='txtCity' class='stateCity' />";
        str += "</td>";

        str += "<td>";
        str += "Zip";
        str += "</td>";
        str += "<td>";
        str += "<input type='text' id='txtZip' />";
        str += "</td>";      

        str += "</tr>";
        str += "<tr>";
        str += "<td>";
        str += "Country";
        str += "</td>";
        str += "<td>";
        str += "<input type='text' id='txtCountry' value='US' />";
        str += "</td>";
        str += "<td>";
        str += "Office Location";
        str += "</td>";

        str += "<td>";
        str += "<input type='text' id='txtOfficeLocation' /> ";
        str += "</td>";
       
        
        str += "</tr>";
        str += "<tr>";
        str += "<td colspan='2'>";
        str += "<p id='btnCreateUser' class='submit'>Create Personal</p>";
        str += "</td>";
        str += "<td colspan='2'>";
        if (isCreated)
            str += "<p id='btnCancel' class='submit'>Cancel</p>";
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

        obj.onClickSubmit(id);

        //Register event KeyUp State City
        obj.onKeyupInput();

        //Register event Select Mode Change
        obj.SelectModeChange();

        //Register event set toUpperCase
        //obj.SetInputToUpperCase();
    }

    obj.ShowNotification = function (message) {
        var notification = $('.notification');
        notification.html(message);
        notification.show("slide", { direction: "left" }, 100).delay(3000).hide("drop", { direction: "up" }, 500);
    }

    obj.SelectModeChange = function () {
        $('#selMode').on('change', function () {
            var mode = $('#selMode :selected').val();
            //console.log(mode);
            var str = "";
            var category = "CLASS A";
            obj.modeCategories[mode].map(function (value) {
                
                str += "<option id='" + value + "'"
                if (value === category) {
                    str += "selected='selected'";
                }
                str += value;

                str += ">" + value;
                str += "</option>";

            });
            $('#selCategory').html(str);
        });
    }

    obj.ResetNull = function () {
        $('#txtCompanyName').html('');
        $('#txtDonorID').val('');        
        $('#txtID').val('');
        $('#txtFirstName').val('');
        $('#txtLastName').val('');

        var option = "";
        var mode = "FMCSA";       
        Object.keys(obj.modeCategories).map(function (field) {
            option += "<option id='" + field + "'"
            if (field === mode) {
                option += "selected='selected' ";
            }
            option += ">" + field;
            option += "</option>";

        });
        $("#selMode").html(option);

        var category = "CLASS A";
        option = "";
        obj.modeCategories[mode].map(function (field) {
            option += "<option id='" + field + "'"
            if (field === category) {
                option += "selected='selected' ";
            }
            option += ">" + field;
            option += "</option>";

        });
        $('#selCategory').html(option);

        option = "";
        var title = "Mr";
        obj.fieldkey.PersonTitle.map(function (field) {
            option += "<option id='" + field + "'"
            if (field === title) {
                option += "selected='selected' ";
            }
            option += ">" + field;
            option += "</option>";

        });
        $("#selTitle").html(option);

        var Gender = "Male";
        option = '';
        obj.fieldkey.PersonGender.map(function (field) {
            option += "<option id='" + field + "'"
            if (field === Gender) {
                option += "selected='selected' ";
            }
            option += ">" + field;
            option += "</option>";

        });
        $("#selGender").html(option);

        $('#txtBirthday').val('');
        $('#txtHomePhone').val('');
        $('#txtWorkPhone').val('');
        $('#txtMobilePhone').val('');
        $('#txtEmail').val('');
        $('#txtWebsite').val('');
        $('#txtAddress').val('');
        $('#txtOfficeLocation').val('');
        $('#txtCountry').val('US');
        $('#txtCity').val('');
        $('#txtState').val('');
        $('#txtZip').val('');
        $('#error').html('');
        $('#btnCreateUser').html('Create Personal');      

    }

    //Display State city panel    
    obj.onKeyupInput = function () {
        $('.modify').find('input').on('keyup', function () {
            var element = $(this);          
            var id = element.attr('id');
            if (id !== 'txtEmail' && id !== 'txtWebsite') {
                var value = element.val();
                element.val(value.toUpperCase());

                if (id === 'txtState' || id === 'txtCity') {
                   // console.log('StateCity');
                    var elementPanel = $('#panel');
                    var top = element.offset().top
                    var left = element.offset().left;
                    var id = element.attr('id');

                    obj.AutoComplate(element, elementPanel, left, top, id);
                }
            }
           
            if (id === 'txtMobilePhone' || id === 'txtWorkPhone' || id === 'txtHomePhone') {
                var text = $(this).val().replace("/()-/", "");                
                if (text.length > 9)
                    text = text.replace(/(\d\d\d)(\d\d\d)(\d\d\d\d)/, "($1)$2-$3");                
                $(this).val(text);
            }
        });      

    }

    obj.ShowElement = function (element, left, top, id) {
        element.offset({ left: left - 4, top: top + 22 });
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
            //console.log($(this).html())
            var element = $(this);
            var value = element.html();
            $('#' + id).val(value);
            obj.HideElement('#panel');
        });

    }

    obj.AutoComplate = function (elementSelect, elementPanel, left, top, id) {
        var v = elementSelect.val();
        var kq = false;
        if (v.length > 0) {
            v = v.toUpperCase();

            $('#panel').html('');
            var str = "<ul class='cells' >";
            //console.log('AutoCompelte id: ' + id + ' element: ' + v);
            if (id === 'txtState') {
                //set null txtCity
                $('#txtCity').val('');
                //Get States in StateCities
                Object.keys(obj.stateCities).map((value) => {
                    if (value.indexOf(v) != -1) {
                        str += "<li class='li'>";
                        str += value;
                        str += "</li>";
                        kq = true;
                    }

                });

            } else if (id === 'txtCity') {
                var state = $('#txtState');
                if (state.val() === undefined || state.val() === '' || state.val() === null) {
                    state.focus();
                } else {
                    var kq2 = false;
                    //Check state in statecities
                    Object.keys(obj.stateCities).map((value) => {
                        if (value === state.val())
                            kq2 = true;
                    });
                    if (kq2) {
                        obj.stateCities[state.val()].map((value) => {
                            //Get Cities in StateCities
                            if (value.indexOf(v) != -1) {
                                str += "<li class='li'>";
                                str += value;
                                str += "</li>";
                                kq = true;
                            }
                        });
                    }
                }
            }

            str += " </ul>";
            if (kq) {
                obj.ShowElement(elementPanel, left, top, id);
                $('#panel').html(str);
                obj.onCickDatePanel(id);
            }

        }

    }

    obj.CreateCompanySelect = function (name, element) {
        $.ajax({
            type: 'POST',
            url: '../Handlers/Handler_GetCompanies.ashx',
            data: {},
            success: function (msg) {
                var coms = JSON.parse(msg);
                var str = "<option value='0'>SELECT COMPANY</option>";
                if (coms.length > 0) {
                    coms.map(function (com) {
                        str += "<option value='" + com.CompanyID + "'";
                        str += com.CompanyName === name ? "selected='selected'" : "";
                        str += ">" + com.CompanyName;
                        str += "</option>";
                    });
                    $(element).html(str);
                }

            },
            error: function () {

            }
        });
    }
    return obj;
}