/// <reference path="../scripts/jquery-3.6.0.min.js" />
/// <reference path="modecategory.js" />
/// <reference path="fieldkeys.js" />

function Donor() {

    var obj = new Object();

    var fieldkeys = new FieldKeys();

    obj.fieldkey = fieldkeys.data;

    obj.stateCities = {};

    obj.modeCategories = ModeCategory.modeCategories;

    obj.Init = function (element, data, id, isCreated, GridView) {
        obj.CreateModifyContent(element, GridView);
        if (!isCreated && data !== undefined && data !== '')
            obj.FillData(data, id);
    }

    obj.onClickSubmit = function (GridView) {
        $('.modify').find('p.submit').on('click', function () {
            var id = $(this).attr('id');
            if (id === 'btnCreate') {
                obj.CreateModify(obj.fieldkey, GridView);
            } else
                obj.ResetNull();
        });
    }

    obj.onClickSetToday = function () {
        $('.calendar').on('click', function () {
            var d = new Date();
            var id = $(this).attr('id');
            var str = (d.getMonth() + 1) + "/" + d.getDate() + "/" + d.getFullYear();
            $('#txt' + id).val(str);
        });
    }

    obj.FillData = function (data, id) {
       
        var idata = data.find(x => x.ID == id); 
        
        $('#txtID').val(idata.ID);
        $('#txtPrimaryID').val(idata.PrimaryID);

        $('#txtDonorID').html(idata.PeopleID);

       // $('#txtCompanyName').html("WORK AT " + idata.CompanyName);

        var value = idata.Firstname;
        value = value === '0' ? '' : value;
        $('#txtFirstName').val(value);

        value = idata.Lastname;
        value = value === '0' ? '' : value;
        $('#txtLastName').val(value);

        value = idata.MobilePhone;
        value = value === '0' ? '' : value;
        $('#txtMobilePhone').val(value);

        value = idata.DateOfBirth;
        value = value === '0' ? '' : value;
        $('#txtBirthday').val(value);

        value = idata.DerFirstName;
        value = value === '0' ? '' : value;
        $('#txtDerFirstName').val(value);

        value = idata.Lastname;
        value = value === '0' ? '' : value;
        $('#txtDerLastName').val(value);

        value = idata.DerEmail;
        value = value === '0' ? '' : value;
        $('#txtDerEmail').val(value);

        value = idata.DerMobilePhone;
        value = value === '0' ? '' : value;
        $('#txtDerMobilePhone').val(value);



        value = idata.NotActiveReason;
        value = value === '0' ? '' : value;
        $('textarea#txtNotActiveReason').val(value);

        value = idata.NotAvilableReason;
        value = value === '0' ? '' : value;
        $('textarea#txtNotAvilableReason').val(value);

        value = idata.NotActiveDate;
        value = value === '0' ? '' : value;
        $('#txtNotActiveDate').val(value);

        value = idata.NotAvilableDate;
        value = value === '0' ? '' : value;
        $('#txtNotAvilableDate').val(value);

        var chk = idata.NotActive;
        $('#chkNotActive').prop('checked', (chk === 0 ? false : true));

        chk = idata.NotAvilable;
        $('#chkNotAvilable').prop('checked', (chk === 0 ? false : true));

        $('#btnCreateUser').html('Submit').show();

        $('#error').html('');
        //console.log(obj.Cmd);

        //Set Donor work at companies
        //$('#divDonorWorkAtCompany').html('');       
       // obj.SetDonorWorkAtCompanies(idata.PrimaryID);

        //Set Test Result List
        //$('#divTestResult').html('');
       // obj.SetTestResult(idata.PrimaryID);
    }

    obj.CreateModify = function (fieldkey, GridView) {
        
        var data = {};           
       
        data[fieldkey.ID] = $('#txtID').val();
        data[fieldkey.PrimaryID] = $('#txtPrimaryID').val();
        data[fieldkey.Cmd] = "M";

        var flag = $('#chkNotActive').is(':checked');
        data[fieldkey.NotActive] = flag ? 1 : 0;

        var NotReason = $('textarea#txtNotActiveReason');
        data[fieldkey.NotActiveReason] = NotReason.val();
        var NotDate = $('#txtNotActiveDate');
        data[fieldkey.NotActiveDate] = NotDate.val();

        if (flag) {
            if (NotReason.val() === undefined || NotReason.val() === null || NotReason.val() === '') {
                $('#error').html('Please enter Enter Reason Terminated Not Active');
                NotReason.focus();
                return;
            }

            if (NotDate.val() === undefined || NotDate.val() === null || NotDate.val() === '') {
                $('#error').html('Please enter Termination Date');
                NotDate.focus();
                return;
            }
        }

        flag = $('#chkNotAvilable').is(':checked');
        data[fieldkey.NotAvilable] = flag ? 1 : 0;

        NotReason = $('textarea#txtNotAvilableReason');
        data[fieldkey.NotAvilableReason] = NotReason.val();

        NotDate = $('#txtNotAvilableDate');
        data[fieldkey.NotAvilableDate] = NotDate.val();

        if (flag) {
            if (NotReason.val() === undefined || NotReason.val() === null || NotReason.val() === '') {
                $('#error').html('Please Enter Reason Terminated Not Avilable');
                NotReason.focus();
                return;
            }

            if (NotDate.val() === undefined || NotDate.val() === null || NotDate.val() === '') {
                $('#error').html('Please enter Termination Date');
                NotDate.focus();
                return;
            }
        }

        
        console.log(JSON.stringify(data));
        
        $.ajax({
            type: 'POST',
            url: '../Handlers/Handler_ModifyDonor.ashx',
            data: data,
            dataType:"JSON",
            success: function (status) {
                //console.log(JSON.stringify(status));
                if (status.Status === "OK") {
                    obj.ShowNotification("Donor was successfully Modify.");
                    //console.log(status);
                    obj.ResetNull();

                } else if (status.Status === 'error')
                    window.location = status.Link;
                
            },
            error: function () {

            }
        });       

    }

    obj.CreateModifyContent = function (element, GridView) {
        var str = "";
       // str += "<div><p class='title'>Detailts Information</p></div>";
        str += "<div class='modify'>";
        str += "<p class='notification'></p>";
        str += "<p class='title'>Donor Details</p>";        
        str += "<input type ='hidden' id='txtID' />";  
        str += "<input type ='hidden' id='txtPrimaryID' />";  
        str += "<table>";
        str += "<tbody>";
        str += "<tr>";
        str += "<td colspan='1' class='title'>";     
        str += "DONOR ID";
        str += "</td>";
        str += "<td colspan='3' class='title' id='txtDonorID'>";
        str += "";
        str += "</td>";
        str += "</tr>";

        str += "<tr>";         
        str += "<td style='width:125px'>";
        str += "First name";
        str += "</td>";
        str += "<td style='width:250px'>";
        str += "<input type='text' id='txtFirstName' style='width:200px' disabled />";
        str += "</td>";       
        str += "<td style='width:125px'>";
        str += "Last name";
        str += "</td>";
        str += "<td style='width:250px'>";
        str += "<input type='text' id='txtLastName' style='width:200px' disabled/>";
        str += "</td>";
        str += "</tr>";

        str += "<tr>";
        str += "<td>";
        str += "Mobile Phone";
        str += "</td>";
        str += "<td>";
        str += "<input type='text' id='txtMobilePhone' style='width:200px' disabled />";
        str += "</td>";
        str += "<td>";
        str += "Brithday";
        str += "</td>";
        str += "<td>";
        str += "<input type='text' id='txtBirthday' style='width:200px' disabled/>";
        str += "</td>";
        str += "</tr>";

        str += "<tr>";
        str += "<td colspan='4' class='title'>";
        str += "SUPERIVSOR";
        str += "</td>";        
        str += "</tr>";

        str += "<tr>";
        str += "<td>";
        str += "DER First name";
        str += "</td>";
        str += "<td>";
        str += "<input type='text' id='txtDerFirstName' style='width:200px' disabled />";
        str += "</td>";
        str += "<td>";
        str += "DER Last name";
        str += "</td>";
        str += "<td>";
        str += "<input type='text' id='txtDerLastName' style='width:200px' disabled/>";
        str += "</td>";
        str += "</tr>";



        str += "<tr>";
        str += "<td>";
        str += "DER Phone";
        str += "</td>";
        str += "<td>";
        str += "<input type='text' id='txtDerMobilePhone' style='width:200px' disabled />";
        str += "</td>";
        str += "<td>";
        str += "DER Email";
        str += "</td>";
        str += "<td>";
        str += "<input type='text' id='txtDerEmail' style='width:200px' disabled/>";
        str += "</td>";
        str += "</tr>";

        str += "<tr>";
        str += "<td colspan='2' class='title' style='width:375px;'>";
        str += "Enter Reason Terminated Not Active";
        str += "</td>";       
        str += "<td colspan='2' class='title' style='width:375px;'>";
        str += "Enter Reason Terminated Not Avilable";
        str += "</td>";
        str += "</tr>";

        str += "<tr>";
        str += "<td colspan='2' >";
        str += "<textarea id='txtNotActiveReason' style='height:100px; width:330px;'></textarea>";    
        str += "</td>";
        str += "<td colspan='2'>";
        str += "<textarea id='txtNotAvilableReason' style='height:100px; width:330px;' class='reason'></textarea>";
        str += "</td>";
        str += "</tr>";

        str += "<tr>";
        str += "<td>";
        str += "<input type='checkbox' id='chkNotActive' />";
        str += "<label for='chkNotActive'>NOT ACTIVE</label>"
        str += "</td>";
        str += "<td>";
        str += "<p>Termination Date</p>"
        str += "<input type='text' id='txtNotActiveDate' style='width:100px; float:left;'/>";
        str += "<p class='calendar' id='NotActiveDate'></p>";
        str += "</td>";        
             
        str += "<td>";
        str += "<input type='checkbox' id='chkNotAvilable' />";
        str += "<label for='chkNotAvilable'>NOT AVILABLE</label>"
        str += "</td>";
        str += "<td>";
        str += "<p>Termination Date</p>"
        str += "<input type='text' id='txtNotAvilableDate' style='width:100px; float:left;' class='reason'/>";
        str += "<p class='calendar' id='NotAvilableDate'></p>";
        str += "</td>";
        str += "</tr>";

        str += "<tr>";
        str += "<td colspan='4' class='title'>";
        str += "COLLECTION SITE: SANTAFE HEALTH CLINIC & LAI CHIRO";
        str += "</td>";
        str += "</tr>";

        str += "<tr>";
        str += "<td colspan='3' style='width:500px;'>";
        str += "<label for='selLab'>TEST TYPE:&nbsp;</label>";
        str += "<select id='selTestType'>";
        var title = "Pre-employment";
        obj.fieldkey.TestType.map(function (field) {
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
        str += "<label for='selLab'>LAB:&nbsp;</label>";
        //str += "LAB";
        //str += "</td>";
        //str += "<td>";
        str += "<select id='selLab' style='width:100px;'>";
        var title = "Phamtech";
        obj.fieldkey.LAB.map(function (field) {
            str += "<option id='" + field + "'"
            if (field === title) {
                str += "selected='selected' ";
            }
            str += ">" + field;
            str += "</option>";

        });
        str += "</select>";
        str += "</td>";
        
        str += "</tr>";       

        //str += "<tr>";
        //str += "<td colspan='3' class='title'>";
        //str += "TEST RESULT";
        //str += "</td>";
        //str += "<td class='title'>";       
        //str += "WORK AT COMPANY";
        //str += "</td>";
        //str += "</tr>";

        //str += "<tr>";
        //str += "<td colspan='3'>";
        //str += "<div id='divTestResult' class='testContent'></div>";
        //str += "</td>";
        //str += "<td>";
        //str += "<div id='divDonorWorkAtCompany' class='work'></div>";
        //str += "</td>";
        //str += "</tr>";

        str += "<tr>";
        str += "<td colspan='2'>";
        str += "<p id='btnCreate' class='submit'>Submit</p>";
        str += "</td>";
        str += "<td colspan='2' id='error' class='redText'>";
        str += "</td>";
        str += "</tr>";
        
        str += "</tbody>";
        str += "</table>";
        str += "</div>";
        $(element).html(str);

        obj.onClickSubmit(GridView);

        //Register event KeyUp State City
        //obj.onKeyupInput();

        //Register event Select Mode Change
        //obj.SelectModeChange();

        //Register on click set today
        obj.onClickSetToday();

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
       // $('#txtDonorID').html('DONOR ID');
       // $('#txtID').val('');
       // $('#txtPrimaryID').val('');
       //// $('#txtCompanyName').html('');
       // $('#txtFirstName').val('');
       // $('#txtLastName').val('');
        $('textarea#txtNotActiveReason').val('');

        $('textarea#txtNotAvilableReason').val('');
        $('#txtNotActiveDate').val('');
        $('#txtNotAvilableDate').val('');

        $('#chkNotActive').prop('checked', false);

        $('#chkNotAvilable').prop('checked', false);

        //$('#txtMobilePhone').val('');       
        //$('#txtBirthday').val('');       
        //$('#txtFullname').html("SUPERIVSOR : ");       
        //$('#txtDerEmail').val('');        
        //$('#txtDerMobilePhone').val('');

        $('#btnCreateUser').html('Submit').hide();

        //$('#divDonorWorkAtCompany').html('');

        //$('#divTestResult').html('');

        $('#error').html('');

    }

    //Display State city panel    
    obj.onKeyupInput = function () {
        $('#modify').find('input').on('keyup', function () {
            var element = $(this);
            var id = element.attr('id');
            if (id !== 'txtEmail' && id !== 'txtWebsite') {
                var value = element.val();
                element.val(value.toUpperCase());

                if (id === 'txtState' || id === 'txtCity') {
                    //console.log('StateCity');
                    var elementPanel = $('#date');
                    var top = element.offset().top
                    var left = element.offset().left;
                    var id = element.attr('id');

                    obj.AutoComplate(element, elementPanel, left, top, id);
                }
            }
        });

        $('.modify').find('textarea').on('keyup', function () {
            var element = $(this).attr('id');
            var value = $('textarea#' + element).val();            
            $('textarea#' + element).val(value.toUpperCase());            
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
            //console.log($(this).html())
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

    obj.CreateCompanySelect = function (id, element) {
        $.ajax({
            type: 'POST',
            url: '../Handlers/Handler_GetCompanies.ashx',
            data: {},
            success: function (msg) {
                var coms = JSON.parse(msg);
                var str = "";// "<option value='0'>SELECT COMPANY</option>";
                if (coms.length > 0) {
                    coms.map(function (com) {
                        str += "<option value='" + com.CompanyID + "'";
                        str += com.CompanyID === parseInt(id, 0) ? "selected='selected'" : "";
                        str += ">" + com.CompanyName;
                        str += "&nbsp;&nbsp;" + (com.SumDriver === 0 ? '' : com.SumDriver);
                        str += "</option>";
                    });
                    $(element).html(str);
                }

            },
            error: function () {

            }
        });
    }


    //Get Donor Work At Companies
    obj.SetDonorWorkAtCompanies = function (id) {
        $.ajax({
            type: 'POST',
            url: '../Handlers/Handler_GetDonorWorkAtCompanies.ashx',
            data: { "id": id },
            dataType:"JSON",
            success: function (status) {
                if (status.Status === "error")
                    window.location = status.Link;
                else if (status.Status === "OK")
                    $('#divDonorWorkAtCompany').html(obj.GetDonorWorkAtCompany(status.Data));
            },
            error: function () {

            }
        });
    }

    obj.GetDonorWorkAtCompany = (data) => {
        var str = "";
        var i = 1;
        data.map(function (com) {
            str += "<p>";
            str += "<span>" + i + ":</span><span>" + com.CompanyName + "</span>";
            str += "</p>";
            i++;
        });
        return str;
    }


    //Get Test Result
    obj.SetTestResult = (id) => {
        $.ajax({
            type: 'POST',
            url: '../Handlers/Handler_GetTestResult.ashx',
            data: { "id": id },
            dataType:"JSON",
            success: function (status) {

                $('#divTestResult').html(obj.GetTestResult(status.Data));

                obj.onClickCollectionDate();
            },
            error: function () {

            }
        });
    }

    obj.GetTestResult = (data) => {
        var str = "";
        var i = 1;
        str += "<table style='width:500px;'>";
        str += "<tbody>";
        str += "<tr>";
        str += "<td>";
        str += "<p>";
        str += "";
        str += "</p>";
        str += "</td>";
        str += "<td>";
        str += "<p>";
        str += "CCF";
        str += "</p>";
        str += "</td>";
        str += "<td>";
        str += "<p >";
        str += "COLLECTION DATE";
        str += "</p>";
        str += "</td>";
        str += "<td>";
        str += "<p>";
        str += "RESULT DATE";
        str += "</p>";
        str += "</td>";
        str += "<td>";
        str += "<p>";
        str += "RESULT";
        str += "</p>";
        str += "</td>";
        str += "</tr>";
        str += "<tr>";
        data.map(function (result) {
            str += "<td>";
            str += "<p>";
            str += i;
            str += "</p>";
            str += "</td>";
            str += "<td>";
            str += "<p class='collection' data-link='" + result.ReportBinary.Name + "'>";
            str += result.PatientID;
            str += "</p>";
            str += "</td>";
            str += "<td>";
            str += "<p >";
            str += result.CollectionDate;
            str += "</p>";
            str += "</td>";
            str += "<td>";
            str += "<p>";
            str += result.ResultDate;
            str += "</p>";
            str += "</td>";
            str += "<td>";
            str += "<p class='";
            str += SetResultValueColor(result.Result) + "'";
            str+="> ";
            str += result.Result;
            str += "</p>";
            str += "</td>";
            i++;
        });
        str += "</tr>";
        str += "</tbody>";
        str += "</table>";       
        
        return str;
    }

    obj.onClickCollectionDate = () => {
        $('.testContent').find('p').on('click', function () {
            var link = $(this).attr('data-link');           
            window.open("../" + link, "_bank");
        });
    }

    obj.Gets = () => {
        $.ajax({
            type: 'POST',
            url: '../Handlers/Handler_GetPeople.ashx',
            dataType: "json",
            data: {},
            success: function (status) {
                localStorage.setItem("Donors", JSON.stringify(status.Data));
            },
            error: function () {

            }
        });
    }
    return obj;
}