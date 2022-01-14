function MROReportDetailts() {

    this.FillDetailReport = function (element, data, value) {        
        var idata = data.find(x => x.PatientID === value); 
        var str = "";
        var v = "";
        str += "<table>";
        str += "<tr>";
        str += "<td class='title' colspan='2'>";
        str += "MRO Report Detailed Information Specimen Number: ";
        str += idata.PatientID;
        str += "</td>";
        str += "</tr>";

        //Company information
        str += "<tr>";
        str += "<td style='width:40%'>"
        str += "<p class='info'>Company Information";
        str += "</p>";
        str += "</td>";
        //Row Test Resaults
        str += "<td rowspan='8' style='width:60%'>";
        v = idata.Result;

        if (v === 'Cancelled') {
            str += "<p>";
            str += "<h1>TEST CANCELLED</h1>";
            str += "<h1 class='redText'>INVALID RESULT</h1>";
            str += "</p> ";
        } else {
            str += "<p class='info'>";
            str += "TEST (S)";
            str += "</p> ";
        }

        //Result
        var results = idata.ResDrugs;
        results.sort((a, b) => (a.Result > b.Result) ? -1 : ((b.Result > a.Result) ? 1 : 0));
        if (results.length > 0) {
            results.forEach(function (result) {
                str += "<p class='value' >";
                v = result.Result;
                switch (v) { }
                str += "<span class='label " + obj.SetResultValueColor(v) + "'>";
                str += v;
                str += "</span>";

                str += "<span class='text'>";
                // str += result.DrugCode + ' / ';
                str += result.DrugName;
                str += "</span>";
                str += "</p>";
            });
        }

        //Notes
        var notes = idata.ResNotes;
        str += "<p class='info'>";
        str += "<span class='redText'>Notes:</span>";
        str += "</p> ";
        notes.map(function (note) {
            str += "<p class='value' >";
            str += "<span class='text'>";
            str += !isNaN(note.Note) ? '' : note.Note;
            str += "</span>";
            str += "</p>";
        });

        str += "<p class=info'>";
        str += "<h2>MRO Name: " + idata.MROName + "</h2>";
        str += "</p>";
        str += "</td>";
        str += "</tr>";

        //Company Content
        str += "<tr>";
        str += "<td style='width:40%'>"
        str += "<p class='value' >";
        str += idata.CompanyName;
        str += "</p>";
        str += "<p class='value' >";
        str += "<span class='label'>Phone:</span>";
        str += "<span class='text'>";
        str += "";
        str += "</span>";
        str += "</p>";
        str += "<p class='value' >";
        str += "<span class='label'>Protocol:</span>";
        str += "<span class='text'>";
        str += "";
        str += "</span>";
        str += "</p>";
        str += "<p class='value' >";
        str += "<span class='label'>LAB:</span>";
        str += "<span class='text'>";
        str += idata.Lab;
        str += "</span>";
        str += "</p>";
        str += "<p class='value' >";
        str += "<span class='label'>Account Number:</span>";
        str += "<span class='text'>";
        str += idata.LabAccount;
        str += "</span>";
        str += "</p>";
        str += "</td>";
        str += "</tr>";

        //Donor Information
        str += "<tr>";
        str += "<td>";
        str += "<p class='info'>Donor Information</p>";
        str += "</td>";
        str += "</tr>";
        //Donor Content
        str += "<tr>";
        str += "<td>";

        str += "<p class='value'>";
        str += "<span class='label'>Name:</span>";
        str += "<span class='text'>";
        str += idata.LastName + ', ' + idata.FirstName;
        str += "</span>";
        str += "</p>";

        str += "<p class='value'>";
        str += "<span class='label'>SSN/ID:</span>";
        str += "<span class='text'>";
        v = idata.SSN;
        str += v === '0' || isNaN(v) || v == undefined ? '' : v;
        str += "</span>";
        str += "</p>";

        str += "<p class='value'>";
        str += "<span class='label'>Spec.#:</span>";
        str += "<span class='text'>";
        str += idata.PatientID;
        str += "</span>";
        str += "</p>";

        str += "<p class='value'>";
        str += "<span class='label'>Accession#:</span>";
        str += "<span class='text'>";
        str += idata.SpecimenNumber;
        str += "</span>";
        str += "</p>";

        str += "<p class='value'>";
        str += "<span class='label'>Alt ID:</span>";
        str += "<span class='text'>";
        str += idata.AlternateID;
        str += "</span>";
        str += "</p>";

        str += "</td>";
        str += "</tr>";

        //Test Infomation
        str += "<tr>";
        str += "<td>";
        str += "<p class='info'>Test Information</p>";
        str += "<p class='value'>";
        str += "</p>";
        str += "</td>";
        str += "</tr>";

        //Test Content
        str += "<tr>";
        str += "<td>";

        str += "<p class='value'>";
        str += "<span class='label'>Test Reason:</span>";
        str += "<span class='text'>";
        str += idata.TestReason;
        str += "</span>";
        str += "</p>";

        str += "<p class='value'>";
        str += "<span class='label'>Mode:</span>";
        str += "<span class='text'>";
        str += idata.TestType;
        str += "</span>";
        str += "</p>";

        str += "<p class='value'>";
        str += "<span class='label'>Date of Collection:</span>";
        str += "<span class='text'>";
        str += idata.CollectionDate;
        str += "</span>";
        str += "</p>";

        str += "<p class='value'>";
        str += "<span class='label'>MRO Verified/Sent:</span>";
        str += "<span class='text'>";
        v = idata.MRODate
        str += v === '0' ? '' : v;
        str += "</span>";
        str += "</p>";

        str += "<p class='value'>";
        str += "<span class='label'>Resault Date:</span>";
        str += "<span class='text'>";
        str += idata.ResultDate;
        str += "</span>";
        str += "</p>";

        str += "<p class='value'>";
        str += "<span class='label'>Spec Type:</span>";
        str += "<span class='text'>";
        str += idata.SpecimenType;
        str += "</span>";
        str += "</p>";

        str += "</td>";
        str += "</tr>";


        //Collection site Information
        str += "<tr>";
        str += "<td>";
        str += "<p class='info'>Collection Information</p>";
        str += "<p class='value'>";
        str += "</p>";
        str += "</td>";
        str += "</tr>";

        //Collection site content
        str += "<tr>";
        str += "<td>";

        str += "<p class='value'>";
        str += "<span class='label'>Name:</span>";
        str += "<span class='text'>";
        str += idata.ResCollection.CollectorName;
        str += "</span>";
        str += "</p>";

        str += "<p class='value'>";
        str += "<span class='label'>Location:</span>";
        str += "<span class='text'>";
        v = idata.ResCollection.Location;
        str += v === '0' ? '' : v;
        str += "</span>";
        str += "</p>";

        str += "<p class='value'>";
        str += "<span class='label'>Phone:</span>";
        str += "<span class='text'>";
        v = idata.ResCollection.CollectionSitePhone;
        str += v === '0' ? '' : v;
        str += "</span>";
        str += "</p>";

        str += "<p class='value'>";
        str += "<span class='label'>Location Code:</span>";
        str += "<span class='text'>";
        v = idata.ResCollection.CollectionLocationCode;
        str += v === '0' ? '' : v;
        str += "</span>";
        str += "</p>";

        str += "</td>";
        str += "</tr>";

        //str += "<tr>";
        //str == "<td>";
        //str += "<embed src='Data/MROReports/SFH/PDF/2000272301.pdf' />";
        //str += "</td>";
        //str += "</tr>";

        str += "</table>";
        $(element).html(str);
    }
}