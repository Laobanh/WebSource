/// <reference path="../scripts/jquery-3.6.0.min.js" />
/// <reference path="fieldkeys.js" />

function User() {

    var obj = new Object();
    obj.Cmd = "C";
    var fieldkeys = new FieldKeys();

    obj.fieldkey = fieldkeys.data;
    obj.stateCities = {};

    obj.Init = function (element, GridView) {
        obj.CreateModifyContent(element, GridView);
    }

    obj.onClickSubmit = function (GridView) {
        $('.modify').find('p.submit').on('click', function () {
            var id = $(this).attr('id');           
            if (id === 'btnCreateUser') {
                obj.CreateModifyUser(obj.fieldkey, GridView);
            } else           
             obj.ResetNull();           
        });
    }

    obj.FillData = function (data, id) {
        var idata = data.find(x => x.User_Id == id);

        $('#txtUsername').val(idata.Account.Username);
        $('#txtPassword').val();
        $('#txtUserID').val(idata.User_Id);
        $('#txtID').val(idata.ID);
        var value = idata.PersonalInfo.Person.FirstName;
        value = value === '0' ? '' : value;
        $('#txtFirstName').val(value);

        value = idata.PersonalInfo.Person.LastName;
        value = value === '0' ? '' : value;
        $('#txtLastName').val(value);     

        var option = "";
        var Group = idata.Account.Group;
        obj.fieldkey.AccountGroup.map(function (field) {
            option += "<option id='" + field + "'"
            if (field === Group) {
                option += "selected='selected' ";
            }
            option += ">" + field;
            option += "</option>";

        });
        $("#selGroup").html(option);

        
        var title = idata.PersonalInfo.Person.Title;
        option = "";
        obj.fieldkey.PersonTitle.map(function (field) {
            option += "<option id='" + field + "'"
            if (field === title) {
                option += "selected='selected' ";               
            }            
            option += ">"+field;
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

        $('#btnCreateUser').html('Modify User');

        $('#chkPassword').prop('checked', false);
        $('.chk').show();

        //console.log(obj.Cmd);
    }

    obj.CreateModifyUser = function (fieldkey, GridView) {
        obj.Cmd = $('#btnCreateUser').html();
        obj.Cmd = obj.Cmd.substr(0, 1);
        var data = {};       
        var username = $('#txtUsername');
        var password = $('#txtPassword'); 
        var chk = $('#chkPassword').is(':checked');
        if (obj.Cmd === 'C') {
            if (username.val() === undefined || username.val() === '' || username.val() === '0' || username.val() === null) {
                $('#error').html('Please enter Username.');
                username.focus();
                return;
            } else if (password.val() === undefined || password.val() === '' || password.val() === '0' || password.val() === null) {
                $('#error').html('Please enter Password.');
                password.focus();
                return;
            }
        } else {
            if (chk) {
                if (password.val() === undefined || password.val() === '' || password.val() === '0' || password.val() === null) {
                    $('#error').html('Please enter Password.');
                    password.focus();
                    return;
                }
            }           
        }
        var first = $('#txtFirstName');
        var last = $('#txtLastName');
        var mobilePhone = $('#txtMobilePhone');
        if (first.val() === undefined || first.val() === '' || first.val() === '0') {
            $('#error').html('Please enter Firstname.');
            first.focus().addClass('repuired');
            return;
        } else if (last.val() === undefined || last.val() === '' || last.val() === '0') {
            $('#error').html('Please enter Lastname.');
            last.focus().addClass('repuired');
            return;
        }
        else if (mobilePhone.val() === undefined || mobilePhone.val() === '' || mobilePhone.val() === '0') {
            $('#error').html('Please enter mobile phone.');
            mobilePhone.focus().addClass('repuired');
            return;
        }
        data[fieldkey.Username] = username.val();
        data[fieldkey.Password] = password.val();
        data[fieldkey.Group] = $('#selGroup :selected').val();
        data[fieldkey.ID] = $('#txtID').val();
        data[fieldkey.UserID] = $('#txtUserID').val();
        data[fieldkey.FirstName] = first.val();
        data[fieldkey.LastName] = last.val();
        data[fieldkey.Title] = $('#selTitle :selected').val();
        data[fieldkey.Gender] = $('#selGender :selected').val();
        data[fieldkey.DateOfBirth] = $('#txtBirthday').val();
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
        data[fieldkey.isChangePassword] = $('#chkPassword').is(':checked') ? 1 : 0;
        data[fieldkey.Cmd] = obj.Cmd;

       // console.log(JSON.stringify(data));
        $.ajax({
            type: 'POST',
            url: '../Handlers/Handler_CreateUser.ashx',
            data: data,
            success: function (status) {
                console.log(status);
                obj.ResetNull();
                if (GridView !== undefined && GridView !== '')
                    GridView.LoadData();

            },
            error: function () {

            }
        });


    }

    obj.CreateModifyContent = function (element, GridView) {
        var str = "";
        str += "<div><p class='title'>User Information</p></div>";
        str += "<div>";
        str += "<table>";
        str += "<tbody>";
        str += "<tr>";
        str += "<td colspan='4' class='title'>";
        str += "Account info";
        str += "</td>";
        str += "</tr>";
        str += "<tr class='account'>";
        str += "<td>";
        str += "Username";
        str += "</td>";
        str += "<td>";
        str += "<input type='text' id='txtUsername' />";
        str += "</td>";
        str += "<td>";
        str += "Password";
        str += "</td>";
        str += "<td>";
        str += "<input type='password' id='txtPassword' />";
        str += "</td>";
        str += "</tr>";
        str += "<tr class='account'>";
        str += "<td>";
        str += "Group";
        str += "</td>";
        str += "<td>";
        str += "<select id='selGroup'>";
        var Group = "Guest";
        obj.fieldkey.AccountGroup.map(function (field) {
            str += "<option id='" + field + "'"
            if (field === Group) {
                str += "selected='selected' ";
            }
            str += ">" + field;
            str += "</option>";

        });
        str += "</select>";
        str += "</td>";
        str += "<td>";    
        str += "<input type='hidden' id='txtID' />";
        str += "<input type='hidden' id='txtUserID' />";
        str += "</td>";      
        str += "<td>";
        str += "<div class='chk'>";
        str += "<input type='checkbox' id='chkPassword' />";
        str += "<label for='chkPassword'>Need to change Password</label >";
        str += "</div>";
        str += "</td>";
        str += "</tr>";

        //PErson Contact
        str += "<tr>";
        str += "<td colspan='4' class='title'>Person contact info</td>";
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
        str += "Birthday";
        str += "</td>";
        str += "<td>";
        str += "<input type='text' id='txtBirthday' />";
        str += "</td>";
        str += "<td>";
        str += "Home phone";
        str += "</td>";
        str += "<td>";
        str += "<input type='text' id='txtHomePhone' />";
        str += "</td>";
        str += "</tr>";
        str += "<tr>";
        str += "<td>";
        str += "Work phone";
        str += "</td>";
        str += "<td>";
        str += "<input type='text' id='txtWorkPhone' />";
        str += "</td>";
        str += "<td>";
        str += "Mobile phone";
        str += "</td>";
        str += "<td>";
        str += "<input type='text' id='txtMobilePhone' />";
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
        str += "<td colspan='4' class='title'>Location info</td>";
        str += "</tr>";
        str += "<tr>";
        str += "<td>";
        str += "Address";
        str += "</td>";
        str += "<td>";
        str += "<input type='text' id='txtAddress' />";
        str += "</td>";
        str += "<td>";
        str += "Office Location";
        str += "</td>";
        str += "<td>";
        str += "<input type='text' id='txtOfficeLocation' /> ";
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
        str += "<td colspan='2'>";
        str += "<p id='btnCreateUser' class='submit'>Create New User</p>";
        str += "</td>";
        str += "<td colspan='2'>";
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
       
        obj.onClickSubmit(GridView);

        //Register event KeyUp State City
        obj.onKeyupInput();
    }

    obj.ResetNull = function () {
        $('#txtUsername').val('');
        $('#txtPassword').val('');
        $('#txtID').val('');
        $('#txtFirstName').val('');
        $('#txtLastName').val('');      

        var option = "";
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
        $('#txtCountry').val('');
        $('#txtCity').val('');
        $('#txtState').val('');
        $('#txtZip').val('');
        $('#error').html('');
        $('#btnCreateUser').html('Create New User');
        $('#chkPassword').prop('checked', false);
        $('.chk').hide();

    }

    //Display State city panel    
    obj.onKeyupInput = function () {
        $('#modify').find('input').on('keyup', function () {
            var element = $(this);
            var id = element.attr('id');
            if (id !== 'txtEmail' && id !== 'txtWebsite' && id !== 'txtUsername' && id !== 'txtPassword') {
                var value = element.val();
                element.val(value.toUpperCase());

                if (id === 'txtState' || id === 'txtCity') {
                    console.log('StateCity');
                    var elementPanel = $('#date');
                    var top = element.offset().top
                    var left = element.offset().left;
                    var id = element.attr('id');

                    obj.AutoComplate(element, elementPanel, left, top, id);
                }
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
        $('#date').find('.li').on('click', function () {
            console.log($(this).html())
            var element = $(this);
            var value = element.html();           
            $('#' + id).val(value);
            obj.HideElement('#date');
        });

    }

    obj.AutoComplate = function (elementSelect, elementPanel, left, top, id) {
        var v = elementSelect.val();
        var kq = false;
        if (v.length > 0) {
            v = v.toUpperCase();

            $('#date').html('');
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
                $('#date').html(str);
                obj.onCickDatePanel(id);
            }

        }

    }
    return obj;
}