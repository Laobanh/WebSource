/// <reference path="../scripts/jquery-3.6.0.min.js" />
/// <reference path="../dist/trumbowyg.js" />

function ScheduleReport() {
    var obj = new Object();

    obj.Companies = undefined;
    obj.data = [];
    obj.Init = (data) => {
        
        var coms = localStorage.getItem("Companies");
        if (coms !== undefined && coms !== "") {
            obj.Companies = JSON.parse(coms);            
        }
        var schedule = localStorage.getItem("schedules");
        if (schedule !== undefined && schedule !== "") {
            schedule = JSON.parse(schedule);
        }
        var ids = data.id.split('_');
        var scheduleID = ids[0];

        schedule = schedule.find(x => x.ID === scheduleID);
        if (data.reportType === "B1" || data.reportType === "B2")
            console.log(data);
        else
            obj.LoadPdf(data, schedule);
            
    }
    obj.Content = (element, data, link, schedule) => {
        var str = "<div class='modify' style='width:100%;'>";//Recipients* 
        str += "<p class='notification'></p>";
        str += "<p class='title'>Schedule Random Report ";
        var value = "";
        //console.log("Content: " + JSON.stringify(data));
        switch (data.reportType) {
            case "N"://1 N
                value = "Notification Letters";
                break;
            case "R"://2 R                        
                value = "Random Selection Summary";
                break;
            case "L": //3 L
                value = "Random Selection List";
                break;
            case "S": //5 S                       
                value = "Notification Slip";
                break;
            case "B": //6 B                      
                value = "Base List Maintained";
                value += parseInt(data.donorType) === 0 ? " For Each Company" : " For Consortium";
                break;
            case "B1": //6 B                      
                value = data.name;
               
                break;
            case "B2": //6 B                      
                value = data.name;
               
                break;
        }
        str += value;
        str += "</p>";
        str += "<table class='tb'>";
        str += "<tbody>";
        //Row 1 download pdf
        str += "<tr>";
        str += "<td colspan='4'>";
        str += "<p id='download' class='button' style='float:right; margin-right:40px;'>Download PDF</p>";
        str += "</td>";
        str += "</tr>";

        //Row 2
        str += "<tr>";

        //Recipients*         
        str += "<td style='width:10%'>";
        str += "Recipients*"
        str += "</td>";
        str += "<td style='width:40%; border:solid 1px #e6e6e6; padding:0; background-color:#6da331; border-radius:3px;'>";
        str += "<p id='selCompany' class='sel'>Selected All Company</p>";       
        str += "</td>";
        //Sunject
        str += "<td style='width:10%;'>";
        str += "Subject*";
        str += "</td>";
        str += "<td style='width:40%; text-align:left;border:solid 1px #e6e6e6; min-heigth:20px;'>";
        str += "<input type='text' id='txtSubject' value='" + data.name + "' style='width:90%; border:0;heigth:20px;'/>";
        str += "</td>";
        str += "</tr>";       
        //Row 3
        str += "<tr>";
        //Message
        str += "<td>";
        str += "Message";
        str += "</td>";
        str += "<td colspan='3' style=';padding:0;border:0;'>";
        str += "<textarea id='txtMessage' style='width:99.3%; min-height:200px;border:solid 1px #e6e6e6' class='reason'>";
        str += "<p>Please see the attached instance eligible donor list.</p>";
        str += "<p>Let me know if you have any questions.</p>";
        str += "<p></p>";
        str += "<p>Sincerely,</p>";
        str += "<p>Vivian Tran</p>";
        str += "</textarea>";
        str += "</td>";
        str += "</tr>";
        //Row 4 
        str += "<tr>";
        str += "<td>";
        str += "&nbsp;";
        str += "</td>";
        str += "<td style='padding:0;'>";
        str += "<input type='checkbox' id='chkContent' />";
        str += "<label for='chkContent'>Use this message to send mail</label>";
        str += "</td>";//
        str += "<td>";
        str += "&nbsp;";
        str += "</td>";
        str += "<td>";
        str += "<input type='number' style='width:40px; text-align:center;' id='txtDays' value='7' min='1' max='30' step='1' />";
        str += "<span> days extension.</span>";
        str += "</td>";
        str += "</tr>";
        //row 5 button send email
        str += "<tr>";
        str += "<td colspan='4'>";
        str += "<p id='send' class='button'  style='float:right; margin-right:40px;'>Send</p>";
        str += "</td>";
        str += "</tr>";
        //Row 4 display pdf file
        str += "<tr style='margin-top: 20px;'>";
        str += "<td colspan='4' >";
        str += "<iframe src='" + link + "' style='border:0; width:100%; height:900pt;'></iframe>";
        str += "</td>";
        str += "</tr>";

        str += "</tbody>";
        str + "</table>";



        str += "</div>";
        $(element).html(str);

        $('#txtMessage').trumbowyg();

        obj.DownloadPDF(link);

        obj.SendMail(data, schedule);

        obj.OnClickSelCompany(data, schedule);

    }

    obj.DataLoading = function (element) {
        var str = "";
        str += "<table style='width:100%;'>";
        str += "<tr><td style='text-align:center;font-size:1.8em;color:#f00;padding-top:30px;'>";
        str += "<img src='../Images/ic_loading.gif' />";
        str += "</td ></tr> ";
        str += "</table>";
        $(element).html(str);
    }

    obj.LoadPdf = (data, schedule) => {
        obj.DataLoading('.modify');
        $.ajax({
            type: 'POST',
            url: '../Handlers/Handler_GetRandomReports.ashx',
            data: data,
            dataType: 'JSON',
            success: function (status) {
                //console.log(JSON.stringify(status));
                if (status.Status === 'OK') {
                    var link = status.Link;
                    obj.Content('.httgridview', data, link, schedule);
                    // console.log(JSON.stringify(status));
                } else if (status.Status === "error" && status.Code === 0) {
                    window.location = status.Link;
                }


            },
            error: function () {

            }
        });

    }

    obj.DownloadPDF = (link) => {
        $('#download').on('click', function () {
            window.open(link, '_bank');
        });
    }

    obj.SendMail = (data,schedule) => {
        $('#send').on('click', function () {
            var msg = "Are you sure to send this email ";
            console.log("SendMail: " + JSON.stringify(data));
            if (data.reportType === "B") {
                if (schedule.Type === 1)
                    msg += parseInt(data.donorType) === 0 ? "according to the driver list of each company" : "to each company of the group, according to the driver list of the whole group";
                else
                    msg += "for Company";
            } else
                msg += schedule.Type === 0 ? "for Company" : "for Consortium";
            
            msg += " ?";
            obj.AlertMessage('.dialog', "Notice", msg, "Yes", "Yes", "NO", "No", data);
        });

    }

    obj.Send = (data) => {       
        data.ListID = obj.data.join();
        data.isUseContent = $('#chkContent').is(':checked') ? 1 : 0;
        data.Content = $('textarea#txtMessage').val();
        data.Days = $('#txtDays').val();
        console.log(JSON.stringify(data));
        $('#popup').show();
        $('.dialog').css({ "background-color":"transparent","border":"0"}).html("<p><img src='../Images/ic_loading.gif' /></p>");
        console.log($('#popup').html());

        $.ajax({
            type: 'POST',
            url: '../Handlers/Handler_SendMail.ashx',
            data: data,
            dataType: 'JSON',
            success: function (status) {
                if (status.Status === 'OK') {
                    var msg = "Email has been sent successfully !!!";
                    obj.AlertMessage('.dialog', "Notice", msg, "", "", "NO", "Close", "");                   
                    $('.dialog').css({ "background-color": "#fff", "border": "solid 1px #e6e6e6" });
                } else if (status.Status === "error" && status.Code === 0) {
                    window.location = status.Link;
                }
            },
            error: function () {

            }
        });
    }

    obj.AlertMessage = (element, error, message, button1, label1, button2, label2, data) => {
        $('#popup').show();
        var str = "<p class='title' style='padding-left:10px;'>";
        str += error;
        str += "</p>";
        str += "<div class='alert' style='padding-left:30px;'>";
        str += message;
        str += "</div>";
        //Button 1
        if (button1 === undefined || button1 === "") 
            str += "";
        else
            str += "<p class='yes button' id='" + button1.toLocaleLowerCase() + "'>" + label1.toLocaleUpperCase() + "</p>";
        if (button2 === undefined || button2 === "")
            str += "";
        else
            str += "<p class='cancel button' id='" + button2.toLocaleLowerCase() + "'>" + label2.toLocaleUpperCase() + "</p>";
        

        $(element).html(str).offset({ top: 10, left: ($(window).width() / 2 - $('.dialog').width() / 2) });
        obj.OnClickPopup(data);
    }

    obj.OnClickPopup = (data) => {
        $('.dialog').find('p').on('click', function () {
            var id = $(this).attr('id');
            id = id.toLocaleLowerCase();
            //console.log("OnClickPopup id: " + id);
            switch (id) {
                case "close":
                case "cancel":
                case "no":
                    $('#popup').hide();
                    $('.dialog').html('');
                    break;
                case "yes":
                case "ok":                    
                    $('#popup').hide();
                    $('.dialog').html('');
                    obj.Send(data);
                    break;
            }           
        });


    }

    obj.OnClickSelCompany = (data, schedule) => {
        //var schedule = localStorage.getItem("schedules");
        //if (schedule !== undefined && schedule !== "") {
        //    schedule = JSON.parse(schedule);
        //}
        var ids = data.id.split('_');
        //var scheduleID = ids[0];

        //schedule = schedule.find(x => x.ID === scheduleID);
        var donors = schedule.Selections[parseInt(ids[1])].DonorSpecimenList.filter(x => x.Selected === true || x.IsAlternate === true);
        //Get Companies
        var companies = obj.Companies;
        var value;
        var sel = $('.sel');
        var offset = sel.offset();
        if (schedule.Type === 0) {
            companies = companies.filter(x => x.CompanyID === parseInt(schedule.CompanyID));
            sel.html("<span>" + companies[0].CompanyName + "&nbsp;" + companies[0].PersonalInfo.Contact.Email + "</sapn>");
            sel.css("background-image", 'none');
            //obj.data.push(parseInt(companies[0].CompanyID));
           
        } else {
            companies = companies.filter(x => x.ConsortiumId === (schedule.ConsortiumID + ""));  

            var coms = [];
            companies.map(function (com) {
                if (donors.find(x => x.CompanyID == com.CompanyID))
                    coms.push(com);
            });
            companies = coms;
            companies.sort(function (a, b) {
                var A = a.CompanyName, B = b.CompanyName;
                if (A < B) {
                    return -1;
                }
                if (A > B) {
                    return 1;
                }
                // names must be equal
                return 0;
            });
            //OnClick SelCompanies
            $('#selCompany').on('click', function () {
                //Show Popup
                $('#popup').show();
                var str = ""; 
                //Created Company list in popup
                str += "<p class='close' title='Close'></p>";
                str += "<ul class='cells' style='width:" + sel.width() + "px'>";  
                //loop
                companies.map(function (com) {
                    str += "<li data='" + com.CompanyID + "'><span><input type='checkbox' class='chk' id='" + com.CompanyID + "'";
                    if (obj.data.length > 0) {
                        if (obj.data.indexOf(com.CompanyID) > -1) {
                            str += " checked='checked'";                          
                        }
                    } else {
                        str += "checked='checked'";
                    }
                       

                    str += "></span>&nbsp;<span>" + com.CompanyName + "</span>";
                    value = com.PersonalInfo.Contact.Email;
                    value = value === "0" ? "" : "&nbsp;&nbsp;<span>" + value + "</span>";
                    str += value;
                    str += "</li>";

                });
                str += "</ul>";               
                //fill company list in dialog element
                $('.dialog').html(str).offset({ top: offset.top, left: offset.left - 5 }).width(sel.width()+30);
               //Register Onclick Close event
                obj.OnClickClose();

                obj.OnClickLi();
            });
        }
        //Set obj.Companies value
        obj.Companies = companies;
        obj.SetIds(data, schedule);
       // console.log("companyIDs: " + JSON.stringify(obj.Companies));
    }

    obj.OnClickLi = () => {
        $('.cells').find('li').on('click', function () {
            var id = $(this).attr('data');
            var chk = $('#' + id);
            if (chk.is(':checked'))
                chk.prop('checked', false);
            else
                chk.prop('checked', true);
        })
    }

    obj.OnClickClose = () => {
        $('.close').on('click', function () {
            //Set data null
            obj.data = [];
            //Get all input checked value
            $('.chk').each(function () {
                var id = $(this).attr('id');
                if ($(this).is(":checked")) {
                    obj.data.push(parseInt(id));                   
                }                    
               
            });
            //console.log("OnClickClose CompanyIDs " + JSON.stringify(obj.data));
            
            if (obj.data.length === obj.Companies.length)
                $('.sel').html('Selected All Company');
            else if (obj.data.length > 0) {
                $('.sel').html('Selected ' + obj.data.length + ' Company.');
            }
            //Hidden popup
            $('#popup').hide();
        });
        
    }

    obj.SetIds = (data,schedule) => {
        //var schedules = JSON.parse(localStorage.getItem("schedules"));
        var ids = data.id.split('_');
        //var schedule = schedules.find(x => x.ID === ids[0]);
        var donors = schedule.Selections[parseInt(ids[1])].DonorSpecimenList.filter(x => x.Selected === true || x.IsAlternate === true);
        //console.log(donors);
        obj.Companies.map(function (com) {
            if (donors.find(x => x.CompanyID == com.CompanyID))
                obj.data.push(com.CompanyID);
        });
    }
    return obj;
}