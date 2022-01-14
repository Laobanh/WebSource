/// <reference path="../scripts/jquery-3.6.0.min.js" />
/// <reference path="fieldkeys.js" />
/// <reference path="statecity.js" />

function Companies() {

    var obj = new Object();
    obj.Cmd = "C";
    var fieldkeys = new FieldKeys();

    obj.fieldkey = fieldkeys.data;

    var stateCity = new StateCity();

    obj.stateCities = stateCity.data;

    obj.Init = function (element, data, id, isCreate) {
        obj.CreateModifyContent(element, isCreate);
        if (!isCreate && data !== undefined && data !== '')
            obj.FillData(data, id);        
    }

    obj.onClickSubmit = function () {
        $('.httgridview').find('p.submit').on('click', function () {
            var id = $(this).attr('id');
            if (id === 'btnCreateUser')
                obj.CreateModify(obj.fieldkey);
            else if (id === "btnCancel")
                obj.ResetNull();
        });
    }

    obj.FillData = function (data, id) {
        var idata = data.find(x => x.CompanyID == id);       

        $('#txtID').val(idata.CompanyID);
        var value = idata.PersonalInfo.Person.FirstName;
        value = value === '0' ? '' : value;
        $('#txtFirstName').val(value);

        value = idata.PersonalInfo.Person.LastName;
        value = value === '0' ? '' : value;
        $('#txtLastName').val(value);

        value = idata.CompanyName;
        value = value === '0' ? '' : value;
        $('#txtCompanyName').val(value);

        value = idata.ConsortiumName;
        value = value === '0' ? '' : value;
        $('#txtConsortiumID').val(value);

        value = idata.ExpirationDate;
        value = value === '0' ? '' : value;
        $('#txtExpirationDate').val(value);

        value = idata.Bill;
        $('#chkBill').prop('checked', (value === 1 ? true : false));

        var option = "";

        var plan = idata.Plan;
        obj.fieldkey.Plans.map(function (field) {
            option += "<option id='" + field + "'"
            if (field === plan) {
                option += "selected='selected' ";
            }
            option += ">" + field;
            option += "</option>";

        });
        $('#selPlan').html(option);

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

        $('#btnCreateUser').html('Modify Company');     

        //console.log(obj.Cmd);        
        $("input[type=tel]").val(function (i, text) {
            text = text.replace(/(\d\d\d)(\d\d\d)(\d\d\d\d)/, "($1)$2-$3");
            return text;
        });
    }

    obj.CreateModify = function (fieldkey) {
        obj.Cmd = $('#btnCreateUser').html();
        obj.Cmd = obj.Cmd.substr(0, 1);
        var data = {};
        var companyName = $('#txtCompanyName');
        if (obj.Cmd === 'C') {
            if (companyName.val() === undefined || companyName.val() === '' || companyName.val() === '0' || companyName.val() === null) {
                $('#error').html('Please enter Company name.');
                companyName.focus();
                return;
            }
        }
        var chk = $('#chkBill').is(':checked') ? 1 : 0;
        var expirationDate = $('#txtExpirationDate');       

        if (chk === 1) {
            if(expirationDate.val() === undefined || expirationDate.val() === '' || expirationDate.val() === '0') {
                $('#error').html('Please enter Expiration date.');
                expirationDate.focus();
                return;
            }
        }

        var first = $('#txtFirstName');
        var last = $('#txtLastName');
        var mobilePhone = $('#txtMobilePhone');
        if (first.val() === undefined || first.val() === '' || first.val() === '0') {
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
        }

        data[fieldkey.ID] = $('#txtID').val();
        data[fieldkey.CompanyName] = companyName.val();
        data[fieldkey.ConsortiumID] = $('#txtConsortiumID').val();
        data[fieldkey.Plan] = $('#selPlan :selected').val();
        data[fieldkey.Bill] = chk;
        data[fieldkey.ExpirationDate] = expirationDate.val();
        data[fieldkey.UserID] = $('#txtUserID').val();
        data[fieldkey.FirstName] = first.val();
        data[fieldkey.LastName] = last.val();
        data[fieldkey.Title] = $('#selTitle :selected').val();
        data[fieldkey.Gender] = $('#selGender :selected').val();
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

        //console.log(JSON.stringify(data));
        $.ajax({
            type: 'POST',
            url: '../Handlers/Handler_CreateCompany.ashx',
            dataType:"json",
            data: data,
            success: function (status) {
               // console.log(status);
                if (status.Status === 'OK') {
                    var notification = $('.notification');
                    notification.html(companyName.val() + " was successfully updated. ");
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
        str += "<p class='title'>Company Details</p>";
        str += "<table class='tb'>";
        str += "<tbody>";
        str += "<tr class='account'>";
        str += "<td>";
        str += "<input type='hidden' id='txtID' />";
        str += "Company name";
        str += "</td>";
        str += "<td colspan='3'>";
        str += "<input type='text' id='txtCompanyName' style='width:485px;'/>";
        str += "</td>";

        str += "</tr>";
        str += "<tr>";
        str += "<td>";
        str += "ConsortiumID";
        str += "</td>";
        str += "<td>";
        str += "<input type='text' id=txtConsortiumID disabled />";
        str += "</td>";
        str += "<td>Plan</td>";
        str += "<td>";
        str += "<select id='selPlan'>";
        var plan = "A";
        obj.fieldkey.Plans.map(function (field) {
            str += "<option id='" + field + "'"
            if (field === plan) {
                str += "selected='selected' ";
            }
            str += ">" + field;
            str += "</option>";

        });
        str += "</select>";
        str += "</td>";

        str += "</tr>";

        //Expiration date
        str += "<tr>";
        str += "<td>";
        str += "</td>";
        str += "<td>";
        str += "<input type='checkbox' id='chkBill' />";
        str += "<label for='chkBill'>Bill</label>";
        str += "</td>";
        str += "<td>";
        str += "Expiration date"
        str += "</td>";
        str += "<td>";
        str += "<input type='text' id='txtExpirationDate' />";
        str += "</td>";
        str += "</tr>";


        //Person Contact
        str += "<tr>";
        str += "<td colspan='4' class='title'>Contact info</td>";
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
        str += "<input type='tel' id='txtMobilePhone' />";
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
        str += "";
        str += "</td>";
        str += "<td>";
        str += "";
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
        str += "<p id='btnCreateUser' class='submit'>Create Company</p>";
        str += "</td>";
        str += "<td colspan='2'>";
        if(isCreate)
            str += "<p id='btnCancel' class='submit'>Cancel</p>";
        else
            str += "<p></p>";
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

        obj.onClickSubmit();

        //Register on CLick State and City
        obj.onKeyupInput();
    }

    obj.ResetNull = function() {

        $('#txtID').val('');
        $('#txtFirstName').val('');
        $('#txtLastName').val('');

        var option = "";

        var plan = "A";
        obj.fieldkey.Plans.map(function (field) {
            option += "<option id='" + field + "'"
            if (field === plan) {
                option += "selected='selected' ";
            }
            option += ">" + field;
            option += "</option>";

        });
        $('#selPlan').html(option);

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

        $('#txtCompanyName').val('').focus();
        $('#txtConsortiumID').val('');
        $('#txtExpirationDate').val('');
        $('#txtHomePhone').val('');
        $('#txtWorkPhone').val('');
        $('#txtMobilePhone').val('');
        $('#txtEmail').val('');
        $('#txtWebsite').val('');
        $('#txtAddress').val('');
        $('#txtOfficeLocation').val('');
        $('#txtCountry').val('');
        $('#txtCity').val('');
        $('#txtState').val('');
        $('#txtZip').val('');
        $('#error').html('');
        $('#btnCreateUser').html('Create Company');
        $('#chkBill').prop('checked', false);
        //$('.chk').hide();

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
                    console.log('StateCity');
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

    obj.ShowElement = function(element, left, top, id) {
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
            if (id === 'txtState') {
                 //Get States in StateCities
                Object.keys(obj.stateCities).map((value) => {
                    if (value.indexOf(v) != -1) {
                        str += "<li class='li'>";
                        str += value;
                        str += "</li>";                       
                        kq = true;
                    }                 
                });
                $('#txtCity').val('');

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

    obj.Gets = () => {        
        $.ajax({
            type: 'POST',
            url: '../Handlers/Handler_GetCompanies.ashx',
            dataType: "json",
            data: {},
            success: function (status) {             
                if (status.Status === 'OK') {
                    localStorage.setItem("Companies", JSON.stringify(status.Data));
                }

            },
            error: function () {

            }
        });
    }
    return obj;
}