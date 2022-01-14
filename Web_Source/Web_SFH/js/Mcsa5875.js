function MCSA5875(){
    var obj = new Object();
    obj.CreatedTablePage2 = function (data,element) {
        var str = "";
        var value = "";
        //var button = data.data.buttons2;
        str += "<tr>";
        str += "<th style='width:415px; text-align:left;'>";
        value = data.data.doYouHave;
        str += value + ":";
        str += "</th>";
        str += "<th style='width:30px; text-align:center;'>";
        str += data.data.buttons2[0]
        str += "</th>";
        str += "<th style='width:30px;text-align:center;'>";
        str += data.data.buttons2[1]
        str += "</th>";
        str += "<th style='width: 30px; text - align: center; '>";
        str += data.data.buttons2[2]
        str += "</th>";
        str += "<th style='width:395px;'>";
        str += "&nbsp;";
        str += "</th>";
        str += "<th style='width:30px;'>";
        str += data.data.buttons2[0]
        str += "</th>";
        str += "<th style='width:30px;'>";
        str += data.data.buttons2[1]
        str += "</th>";
        str += "<th style='width:30px;'>";
        str += data.data.buttons2[2]
        str += "</th>";
        str += "</tr>";
        var i = 0;
        var d = data.data.page2;
        for (var i = 1; i < 16; i++) {
            var m =(d[i + ""]);
            var k = i + 15;
            var n = ( d[k + ""]);
            if (i === 13) {
                m = d[i + "A"];
              
                str += "<tr>";
                str += "<td style='text-align:left;'>";
                str += "<span>" + i + ".</span>";
                str += "<span>" + m + " </span>";
                str += "<span style='font-size:8pt;'></span>";
                str += "</td>";
                str += "<td style='text-align:center;'>";
                str += "<input type='radio' name='r"+i+"' value='1' />";
                str += "</td>";
                str += "<td style='text-align:center;'>";
                str += "<input type='radio' name='r" + i +"' value='2' />";
                str += "</td>";
                str += "<td style='text-align:center;'>";
                str += "<input type='radio' name='r" + i +"' value='3' />";
                str += "</td>";
                str += "<td>";
                str += "<span>" + (i + 15) + ".</span>";
                str += "<span>" + n + "</span>";
                str += "<span style='font-size:8pt;'></span>";
                str += "</td>";
                str += "<td style='text-align:center;'>";
                str += "<input type='radio' name='r" + (i + 15) + "' value='1' />";
                str += "</td>";
                str += "<td style='text-align:center;'>";
                str += "<input type='radio' name='r" + (i + 15) + "' value='2' />";
                str += "</td>";
                str += "<td style='text-align:center;'>";
                str += "<input type='radio' name='r" + (i + 15) + "' value='3' />";
                str += "</td>";
                str += "</tr>";

                //13B
                str += "<tr>";
                str += "<td style='text-align:left;'>";
                str += "<span>&nbsp;</span>";
                str += "<span>&nbsp;&nbsp;&nbsp;&nbsp;" + d[i + "B"] + " </span>";
                str += "<span style='font-size:8pt;'></span>";
                str += "</td>";
                str += "<td style='text-align:center;'>";
                str += "<input type='radio' name='r" + i + "B' value='1' />";
                str += "</td>";
                str += "<td style='text-align:center;'>";
                str += "<input type='radio' name='r" + i + "B' value='2' />";
                str += "</td>";
                str += "<td style='text-align:center;'>";
                str += "<input type='radio' name='r" + i + "B' value='3' />";
                str += "</td>";
                str += "<td colspan='5'>";
                str += "&nbsp;";
                str += "</td>";
                str += "</tr>";
            } else {
                str += "<tr>";
                str += "<td style='text-align:left;'>";
                str += "<span>" + i + ".</span>";
                str += "<span>" + m + " </span>";
                str += "<span style='font-size:8pt;'></span>";
                str += "</td>";
                str += "<td style='text-align:center;'>";
                str += "<input type='radio' name='r" + i + "' value='1' />";
                str += "</td>";
                str += "<td style='text-align:center;'>";
                str += "<input type='radio' name='r" + i + "' value='2' />";
                str += "</td>";
                str += "<td style='text-align:center;'>";
                str += "<input type='radio' name='r" + i + "' value='3' />";
                str += "</td>";
                str += "<td>";
                str += "<span>" + (i + 15) + ".</span>";
                str += "<span>" + n + "</span>";
                str += "<span style='font-size:8pt;'></span>";
                str += "</td>";
                str += "<td style='text-align:center;'>";
                str += "<input type='radio' name='r" + (i + 15) + "' value='1' />";
                str += "</td>";
                str += "<td style='text-align:center;'>";
                str += "<input type='radio' name='r" + (i + 15) + "' value='2' />";
                str += "</td>";
                str += "<td style='text-align:center;'>";
                str += "<input type='radio' name='r" + (i + 15) + "' value='3' />";
                str += "</td>";
                str += "</tr>";
            }
        }        
        $(element).html(str);
    }
    obj.Fill = (data,lang) => {
        var d = data.data;
        var s = ":";
        if (lang === "en") {            
            $('.takingMedicationsSub').show();
            $('#txtLastName').width(230);
            $('#txtAddress').width(287);
            $('#txtLicense').width(300);
        } else {
            s += "&nbsp;&nbsp;";
            $('.takingMedicationsSub').hide();
            $('#txtLastName').width(310);
            $('#txtAddress').width(383);
            $('#txtLicense').width(430);
        }
        //page 1
        var value = d.nameLast + s;
        $('.lastName').html(value);

        value = d.nameFirst + s;
        $('.firstName').html(value);

        value = d.nameInitial + s;
        $('.middleName').html(value);

        value = d.DOB + s;
        $('.dob').html(value);

        value = d.DOB2 + s;
        $('.dob2').html(value);

        value = d.age + s;
        $('.age').html(value);

        value = d.address + s;
        $('.address').html(value);

        value = d.city + s;
        $('.city').html(value);

        value = d.state + s;
        $('.state').html(value);

        value = d.zip + s;
        $('.zip').html(value);

        value = d.license + s;
        $('.license').html(value);

        value = d.licenseState + s;
        $('.licenseState').html(value);

        value = d.phone + s;
        $('.phone').html(value);

        value = d.gender + s;
        $('.gender').html(value);

        value = d.genderButtons[0];
        $('#M').html(value);

        value = d.genderButtons[1];
        $('#F').html(value);

        value = d.email + s;
        $('.email').html(value);

        value = d.page1.cdl + s;
        $('.cdl').html(value);

        value = d.buttons2[0];
        $('.yes').html(value);

        value = d.buttons2[1];
        $('.no').html(value);

        value = d.buttons2[2];
        $('.noSure').html(value);

        value = d.page1.verify + s;
        $('.verify').html(value);

        value = d.page1.certDeny;
        $('.certDeny').html(value);

        value = d.examDate + s;
        $('.examDate').html(value);

        value = d.page1.surgery;
        $('.surgery').html(value);

        value = d.page1.takingMedications;
        $('.takingMedications').html(value);

        value = d.otherHealthCondition + s;
        $('.ohc').html(value);

        value = d.answer;
        $('.answer').html(value);

        value = d.driverSignature + s;
        $('.driverSignature').html(value);

        value = d.date + s;
        $('.date').html(value);

        value = d.driverHistory + s;
        $('.driverHistory').html(value);
    }
    obj.GetLang = function (lang, element) {
        lang = lang === undefined || lang === "" ? "en" : lang;
        var url = "../data/5875_" + lang + ".json?v=21102003";
        $.getJSON(url, function (data) {
            var i = 0;
            //for (var k in data.data.page2) {
            //    if (i % 2 === 0)
            //        console.log(i + ". " + data.data.page2[k]);
            //    i++;
            //}
            //var d = data.data.page2;
            //for (var i = 1; i < 17; i++) {
            //    console.log(i + ". " + d[i + ""]);
            //    var k = i + 15;
            //    console.log(k + ". " + d[k + ""]);
            //}
            obj.CreatedTablePage2(data, element);
            obj.Fill(data, lang);
            //console.log(data.data.buttons2[0]);
        });

    }
    return obj;
}