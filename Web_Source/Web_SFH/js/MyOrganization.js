/// <reference path="../dist/trumbowyg.js" />
/// <reference path="fieldkeys.js" />

function MyOrganization() {
    var obj = new Object();
    obj.key = new FieldKeys().data;
    obj.Init = () => {
        obj.LoadData();
    }

    obj.Content = (element) => {
        var str = "<div class='modify' style='margin-top:20px;'>";
        str += "<p class='title'>"; 
        str += "Company Infomations";
        str += "</p>";
        str += "<table style='width:100%;'>";
        str += "<tbody>";
        //Row 1
        str += "<tr>";
        str += "<td style='20%'>";
        str += "Name";
        str += "</td>";
        str += "<td colspan='3'>";
        str += "<input type='text' id='txtCompanyName' />";
        str += "</td>";        
        str += "</tr>";
        //Row 2
        str += "<tr>";
        str += "<td style='20%'>";
        str += "First Name";
        str += "</td>";
        str += "<td style='30%'>";
        str += "<input type='text' id='txtFirstName' />";
        str += "</td>";
        str += "<td style='20%'>";
        str += "Last Name";
        str += "</td>";
        str += "<td style='20%'>";
        str += "<input type='text' id='txtLastName' />";
        str += "</td>";
        str += "</tr>";
        //Row 3
        str += "<tr>";
        str += "<td>";
        str += "Phone";
        str += "</td>";
        str += "<td>";
        str += "<input type='text' id='txtPhone' />";
        str += "</td>";
        str += "<td>";
        str += "Fax";
        str += "</td>";
        str += "<td>";
        str += "<input type='text' id='txtFax' />";
        str += "</td>";
        str += "</tr>";
        //Row 4
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
        //Row 5
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
        str += "<input type='text' id='txtState' />";
        str += "</td>";
        str += "</tr>";
        //Row 6
        str += "<tr>";
        str += "<td>";
        str += "City";
        str += "</td>";
        str += "<td>";
        str += "<input type='text' id='txtCity' />";
        str += "</td>";
        str += "<td>";
        str += "Zip";
        str += "</td>";
        str += "<td>";
        str += "<input type='text' id='txtZip' />";
        str += "</td>";
        str += "</tr>";

        str += "</tbody>";
        str += "</table>";
        //Space
        str += "<p style='margin-top:20px;'></p>";
        str += "<p class='title'>";
        str += "Email Settings";
        str += "</p>";
        str += "<table style='width:100%'>";
        str += "<tbody>";
        //Row 1
        str += "<tr>";
        str += "<td style='20%'>";
        str += "Server";
        str += "</td>";
        str += "<td style='30%'>";
        str += "<input type='text' id='txtServer'  value='smtp.gmail.com'/>";
        str += "</td>";
        str += "<td style='20%'>";
        str += "Port";
        str += "</td>";
        str += "<td style='30%'>";
        str += "<input type='text' id='txtPort' value='587' />";
        str += "</td>";
        str += "</tr>";
        //Row 2
        str += "<tr>";
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
        //Row 3
        str += "<tr>";
        str += "<td>";
        str += "From";
        str += "</td>";
        str += "<td>";
        str += "<input type='text' id='txtFrom' />";
        str += "</td>";
        str += "<td>";
        str += "From Name";
        str += "</td>";
        str += "<td>";
        str += "<input type='text' id='txtFromName' />";
        str += "</td>";
        str += "</tr>";
        //Row 4
        str += "<tr>";
        str += "<td>";
        str += "CC";
        str += "</td>";
        str += "<td>";
        str += "<input type='text' id='txtCC' />";
        str += "</td>";
        str += "<td>";
        str += "CC Name";
        str += "</td>";
        str += "<td>";
        str += "<input type='text' id='txtCCName' />";
        str += "</td>";
        str += "</tr>";
        //Row 5
        str += "<tr>";
        str += "<td>";
        str += "BCC";
        str += "</td>";
        str += "<td>";
        str += "<input type='text' id='txtBcc' />";
        str += "</td>";
        str += "<td>";
        str += "BCC Name";
        str += "</td>";
        str += "<td>";
        str += "<input type='text' id='txtBccName' />";
        str += "</td>";
        str += "</tr>";

        str += "</tbody>";
        str += "</table>";

        //Space
        str += "<p style='margin-top:20px;'></p>";
        str += "<p class='title'>";
        str += "Email Notice";
        str += "</p>";
        str + "<table style='width:100%;'>";
        str += "<tbody>";
        //Row 1
        str += "<tr>";
        str += "<td>";
        str += "<textarea id='editorNotice' style='height:200px; border:solid 1px #e6e6e6;'></textarea>";
        str += "</td>";
        str += "</tr>";

        str += "</tbody>";
        str + "</table>";

        //Space
        str += "<p style='margin-top:20px;'></p>";
        str += "<p class='title'>";
        str += "Email Contents";
        str += "</p>";
        str + "<table style='width:100%;'>";
        str += "<tbody>";   
        //Row 1
        str += "<tr>";
        str += "<td>";
        str += "<textarea id='editor' style='min-height:600px; border:solid 1px #e6e6e6;'></textarea>";
        str += "</td>";
        str += "</tr>";     

        str += "</tbody>";
        str + "</table>";

        //Space
        str += "<p style='margin-top:20px;'></p>";
        str + "<table style='width:100%'>";
        str += "<tbody>";
        str += "<tr>";
        str += "<td>";
        str += "<p style='float:right; margin-right:20px;' class='submit'>Save</p>";
        str += "</td>";
        str += "</tr>";
        str += "</tbody>";
        str + "</table>";

        //Space
        str += "<p style='margin-bottom:60px; height:60px;'></p>";

        str += "</div>";
        $(element).html(str);

        obj.OnClickSubmit();

        $('#editor').trumbowyg({            
        });
        $('#editorNotice').trumbowyg({
        });
    }

    obj.OnClickSubmit = () => {
        $('.modify').find('.submit').on('click', function () {
            obj.Loading();
            obj.Created();
        });
    }

    obj.Loading = () => {
        $('#popup').show();
        $('.dialog').css({ "background-color": "transparent", "border": "0" }).html("<p><img src='../Images/ic_loading.gif' /></p>");
    }

    obj.UnLoading = () => {
        $('#popup').hide();
        $('.dialog').css({ "background-color": "#fff", "border": "solid 1px #e6e6e6" });
    }

    obj.Created = () => {
        var data = {};
        var key = obj.key;
        data[key.CompanyName] = $('#txtCompanyName').val();
        data[key.FirstName] = $('#txtFirstName').val();
        data[key.LastName] = $('#txtLastName').val();
        data[key.MobilePhone] = $('#txtPhone').val();
        data[key.WorkPhone] = $('#txtFax').val();
        data[key.Email] = $('#txtEmail').val();
        data[key.Website] = $('#txtWebsite').val();
        data[key.Address] = $('#txtAddress').val();
        data[key.City] = $('#txtCity').val();
        data[key.State] = $('#txtState').val();
        data[key.Zip] = $('#txtZip').val();
        data[key.Server] = $('#txtServer').val();
        data[key.Port] = $('#txtPort').val();
        data[key.Username] = $('#txtUsername').val();
        data[key.Password] = $('#txtPassword').val();
        data[key.From] = $('#txtFrom').val();
        data[key.FromName] = $('#txtFromName').val();
        data[key.Cc] = $('#txtCC').val();
        data[key.CcName] = $('#txtCCName').val();
        data[key.Bcc] = $('#txtBcc').val();
        data[key.BccName] = $('#txtBccName').val();
        data[key.Notice] = $('textarea#editorNotice').val();
        data[key.Content] = $('textarea#editor').val();

        //console.log(JSON.stringify(data));
        $.ajax({
            type: 'POST',
            url: '../Handlers/Handler_CreatedPreferences.ashx',
            dataType: "json",
            data: data,
            success: function (status) {                
                if (status.Status === 'OK') {
                    obj.UnLoading();
                }
                console.log(JSON.stringify(status));
            },
            error: function () {

            }
        });

    }

    obj.Fill = (data) => {
        var value = data.CompanyName;
        value = value === '0' ? "" : value;
        $('#txtCompanyName').val(value);

        value = data.FirstName;
        value = value === '0' ? "" : value;
        $('#txtFirstName').val(value);

        value = data.LastName;
        value = value === '0' ? "" : value;
        $('#txtLastName').val(value);

        value = data.MobilePhone;
        value = value === '0' ? "" : value;
        $('#txtPhone').val(value);

        value = data.WorkPhone;
        value = value === '0' ? "" : value;
        $('#txtFax').val(value);

        value = data.Email;
        value = value === '0' ? "" : value;
        $('#txtEmail').val(value);

        value = data.Website;
        value = value === '0' ? "" : value;
        $('#txtWebsite').val(value);

        value = data.Address;
        value = value === '0' ? "" : value;
        $('#txtAddress').val(value);

        value = data.City;
        value = value === '0' ? "" : value;
        $('#txtCity').val(value);

        value = data.State;
        value = value === '0' ? "" : value;
        $('#txtState').val(value);

        value = data.Zip;
        value = value === '0' ? "" : value;
        $('#txtZip').val(value);

        value = data.Server;
        value = value === '0' ? "" : value;
        $('#txtServer').val(value);

        value = data.Port;
        value = value === '0' ? "" : value;
        $('#txtPort').val(value);

        value = data.Username;
        value = value === '0' ? "" : value;
        $('#txtUsername').val(value);

        value = data.Password;
        value = value === '0' ? "" : value;
        $('#txtPassword').val(value);

        value = data.From;
        value = value === '0' ? "" : value;
        $('#txtFrom').val(value);

        value = data.FromName;
        value = value === '0' ? "" : value;
        $('#txtFromName').val(value);

        value = data.Cc;
        value = value === '0' ? "" : value;
        $('#txtCC').val(value);

        value = data.CcName;
        value = value === '0' ? "" : value;
        $('#txtCCName').val(value);

        value = data.Bcc;
        value = value === '0' ? "" : value;
        $('#txtBcc').val(value);

        value = data.BccName;
        value = value === '0' ? "" : value;
        $('#txtBccName').val(value);

        value = data.Content;
        value = value === '0' ? "" : value;       

        $('#editor').trumbowyg('html', value); 

        value = data.Notice;
        value = value === '0' ? "" : value;
        $('#editorNotice').trumbowyg('html', value); 
    }
    obj.LoadData = () => {
        $.ajax({
            type: 'POST',
            url: '../Handlers/Handler_GetPreferences.ashx',
            dataType: "json",
            data: {},
            success: function (status) {
                console.log(status);
                if (status.Status === 'OK') {                                     
                    obj.Content('.httgridview');//, 
                    obj.Fill(status.Data.MyOrganization);
                } else if (status.Status === 'error' && status.Code === 0)
                    window.location = status.Link;

            },
            error: function () {

            }
        });
    }
    return obj;
}