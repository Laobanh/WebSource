/// <reference path="../scripts/jquery-3.6.0.min.js" />

function showPupop(isShowHide) {
    //is hidden
    var pupop = $('#pupop');
    pupop.height(height);
    if (isShowHide) {
        pupop.show();
    }
    else {
        pupop.hide();
    }   
}

function imageChange() {
    var _URL = window.URL || window.webkitURL;
    $('#txtUpload').on('change', function () {
        var file, img;
        if ((file = this.files[0])) {
            img = new Image();
            img.onload = function () {
                sendFile(file);
                //Set hide uploader 
                var uploader = $('#lblUploader').hide();

            };
            img.onerror = function () {
                alert("Not a valid file:" + file.type);
            };
            img.src = _URL.createObjectURL(file);
        }
    });
}

function sendFile(file) {
    var formData = new FormData();
    formData.append('file', $('#txtUpload')[0].files[0]);
    formData.append('categoryID', menuID);
    $.ajax({
        type: 'post',
        url: 'Handler/uploader.ashx',
        data: formData,
        success: function (status) {
            if (status != 'error') {
                var my_path = 'images/' + menuID + "/" + status;
                $("#img").css("background-image", "url(" + my_path + ")");
                $('#txtImgLink').val(my_path);
            }
        },
        processData: false,
        contentType: false,
        error: function () {
            alert("Whoops something went wrong!");
        }
    });
}

function showHideUploader() {
    var uploader = $('#lblUploader');
    var imgLink = $('#txtImgLink');
    if (uploader.is(':visible'))
        uploader.hide();
    else {
        uploader.show();

        if (imgLink.val() !== '')
            uploader.removeClass('plus').addClass('edit');
        else
            uploader.removeClass('edit').addClass('plus');
    }
}
function setNullHideInput(elemnet) {
    $(elemnet).find('input').val('');
}

function GetObjectValueByKeys(datas, keys) {
    if (keys !== undefined && isNaN(keys)) {
        var array = keys.split('.');
        switch (array.length) {
            case 1:
                return datas[array[0]];
                break;
            case 2:
                return datas[array[0]][array[1]];
                break
            case 3:
                return datas[array[0]][array[1]][array[2]];
                break;
            case 4:
                return datas[array[0]][array[1]][array[2]][array[3]];
            case 5:
                return datas[array[0]][array[1]][array[2]][array[3]][array[4]];
        }
    }
    return undefined;
}


function SetResultValueColor(value) {
    var className = "";
    if (value != undefined && value != null && value != '0') {
        value = value.toLowerCase();
        switch (value) {
            case 'positive':
                className = 'redText';
                break;
            case 'negative':
                className = 'greenText';
                break;
        }
    }
    return className;
}

function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}

function ReturnDateTime(str, format) {
    var kq = "";
    //2021-08-10T15:56:35.679446+07:00
    var d = new Date(str);
    var MM = d.getMonth();
    var dd = d.getDate();
    var HH = d.getHours();
    var mm = d.getMinutes();
    var ss = d.getSeconds();
    kq = (MM > 8 ? (MM + 1) : "0" + (MM + 1));
    kq += "/" + (dd > 9 ? dd : "0" + dd);
    kq += "/" + d.getFullYear() + " ";
    kq += HH > 9 ? HH : "0" + HH;
    kq += ":" + (mm > 9 ? mm : "0" + mm);
    kq += ":" + (ss > 9 ? ss : "0" + ss);
    if (kq === undefined)
        kq = str;
    
    return kq;
}

function ReturnDate(str) {
    var kq = "";
    //2021-08-10T15:56:35.679446+07:00
    var d = new Date(str);
    var MM = d.getMonth();
    var dd = d.getDate();
    var HH = d.getHours();
    var mm = d.getMinutes();
    var ss = d.getSeconds();
    kq = (MM > 8 ? (MM + 1) : "0" + (MM + 1));
    kq += "/" + (dd > 9 ? dd : "0" + dd);
    kq += "/" + d.getFullYear();
    if (kq === undefined)
        kq = str;

    return kq;
}

function RetDate(date) {
    var d = date.split('/');
    var kq = "";
    var month = 0, day = 0, year = 0;
    if (d.length === 3) {
        month = parseInt(d[0]);
        day = parseInt(d[1]);
        year = parseInt(d[2]);

        kq = (month > 9 ? "" : "0") + month;
        kq += (day > 9 ? "/" : "/0") + day;
        kq += "/" + year;
    }
    return kq;
}

function GridWidth() {
    const gridWidth = $(window).width() - 260;
    return gridWidth > 1200 ? gridWidth : 1200;
}

