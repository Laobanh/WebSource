
function GridView() {
    var obj = {};
    obj.sortValue = 'asc', obj.sortKey = '',obj.searchType = '';
    obj.data = new Array();
    obj.dataSearch = new Array();
    obj.isSearch = false;    
    //Data 
    obj.Grid = {
        width: 1460,
        url: '',
        searchData: {},
        popup: true,
        data: {},
        columns: [
            {
                value: '',
                field: '',
                title: '',
                width: 0,
                widthPlus: 0,
                sortable: false,
                filterable: false,
                colorChange: true,
                hidden: true,
                template: ""

            }]

    };

    //footer page
    obj.sumRow = 0;
    obj.curPage = 0, obj.rowOfPage = 20, obj.nextPage = 0, obj.lastPage = 0, obj.firstPage = 0, obj.prevPage = 0, obj.coutGroupPage = 0;
    obj.coutPage = 5, obj.limitPage = 5, obj.maxGroupPage = 0;
    obj.isFirst = false, obj.isLast = false;
    obj.startPoint = 0, obj.endPoint = 0;

    obj.OnClickDetails;

    obj.Init = function () {
        
        //Set Header Content Footer width
        obj.setWidthElement("#httGridview");

        //Created Grid Content
        obj.CreateGrid();
        
        //Load data
        obj.LoadData(obj.Grid.searchData);

        //console.log(JSON.stringify(obj.Grid.searchData));
        //Create Header
        obj.CreateHeader('#header');

        //Register onClick CheckAll
        obj.onClikCheckAll();

        //Register onClick Title
        obj.onClickTitle();

        //Register onClick Filter
        obj.onClickFilter();


        //Register onclick search button
        obj.onClickSearchSubmit();
       
    }

    obj.CreateGrid = function () {
        var str = "";
        str += "<div id='search' class='search'>";
        str += "<table>";
        str += "<tr>";
        str += "<td colspan='2'>";
        str += "<input class='keyword' type='text' id='keyword' placeholder='Enter key word' />";
        str += "</td>";
        str += "</tr>";
        str += "<tr>";
        str += "<td>";
        str += "<button id='submit'>Submit</button>";
        str += "</td>";
        str += "<td>";
        str += "<button id='cancel'>Cancel</button>";
        str += "</td>";
        str += "</tr>";
        str += "</table>";
        str += "</div>";
        str += "<div id='header'>";
        str += "</div>";
        str += "<div id='content'>";
        str += "</div>";
        str += "<div id='footer'>";
        str += "<div class='lfooter'>";
        str += "</div>";
        str += "<div class='rfooter'>";
        str += "<table>";
        str += "<tr>";
        str += "<td>";
        str += "</td>";
        str += "<td>";
        str += "<p id='startPoint'></p>";
        str += "</td>";
        str += "<td>";
        str += "-";
        str += "</td>";
        str += "<td>";
        str += "<p id='endPoint'></p>";
        str += "</td>";
        str += "<td><p>of</p></td>";
        str += "<td><p id='sumRow'></p></td>";
        str += "<td><p>items</p></td>";
        str += "</tr>";
        str += "</table>";
        str += "</div>";
        str += "</div>";

        $('#httGridview').html(str);
    }

    //obj.set width
    obj.setWidthElement = function (element) {
        $(element).width(obj.Grid.width);
    }
    //Create Header Table
    obj.CreateHeader = function (element) {
        var str = "<div class='tabHeaderDiv'>";
        str += "<table  class='tab0' style='width:" + (obj.Grid.width) + "px;'> ";
        str += "<tbody>";
        str += "<tr  class='header'>"
        var fields = obj.Grid.columns;
        fields.forEach(function (field) {
            str += "<td style='width:" + field.width + "px;' class='title'>";
            var sortWidth = 0, filterWidth = 0;
            var labelWidth = (field.sortable ? 20 : 0) + (field.filterable ? 16 : 0);            

            //sorting
            if (field.sortable) {
                str += "<span class='sort'  id='s" + field.field + "'></span>";

                // str += "<span class='label' id='ss" + field.field + "' style='width:" + (field.width - labelWidth) + "px;'>" + field.title + "</span>";
            }         

            //Filter
            if (field.filterable)
                str += "<span class='filter' id='f" + field.field + "'></span>";
            str += "<span data-id='" + field.sortable + "' class='label' id='" + field.field + "' style='width:" + (field.width - labelWidth) + "px;'>" + field.title + "</span>";
            
            

            str += "</td>";
        });
       // str+="<td style='width:15px;'></td>"
        str += "</tr>";
        str += "</tbody>";
        str += "</table>";
        str += "</div>";
        $(element).html(str);
    }

    //Create Table conetnt
    obj.CreateContent = function (element, idata) {
        
        var str = "";
        for (var i = obj.startPoint; i < obj.endPoint; i++) {
            var fields = obj.Grid.columns;

            str += "<div class='tabRowDiv' style='width:" + (obj.Grid.width + obj.Grid.widthPlus) + "px;'>";
            str += "<table style='width:" + (obj.Grid.width + obj.Grid.widthPlus) + "px;' class='tab'>";
            //if (i % 2 == 0)
            //    str += "<tr class='first'>";
            //else
                str += "<tr>";
            var stt = new Number(i + 1).toLocaleString();
            //Created table td by array fields
            for (var k = 0; k < fields.length - 1; k++) {     
                //Get value by contruc field value
                var values = fields[k].value.split('|');
                var value = GetObjectValueByKeys(idata[i], values[0]);
                if (values.length === 2) {
                    value = "" + value;                    
                    var value2 = GetObjectValueByKeys(idata[i], values[1]);
                    if (value2 !== undefined && value2 !== "") {
                        value += (value !== undefined && value !== "" ? ", " : "") + value2;
                    }
                        
                    value += "";
                }
                    
                
                var id = GetObjectValueByKeys(idata[i], obj.Grid.id);
                //console.log(value);
                str += "<td style='width:" + (fields[k].width + fields[k].widthPlus -2)  + "px;' ";
                str += " class='" + (fields[k].colorChange ? obj.SetResultValueColor(value) : '') + "'";
                str += " >";
                var navi = fields[k].navigate;
                navi = (navi === undefined ? true : navi);
                //console.log(k + " " + value);
                if (!fields[k].hidden) {
                    if (fields[k].title === 'stt' || value === undefined) {
                        str += stt;
                    } else {
                        if (fields[k].bit) {
                            if (fields[k].bitvalue === '' || fields[k].bitvalue === undefined) {
                                str += (value === 1 ? "<p class='checked' data-id='" + id + "' navi='" + (navi) + "'></p>" : "<p data-id='" + id + "' navi='" + (navi) + "' class='unchecked'></p>");
                            } else {
                                var bitValue = fields[k].bitvalue.split('|');
                                if (bitValue.length === 2)
                                    str += "<p class='" + (value === 1 ? 'greenText' : '') + "'>" + bitValue[value] + "</p>";
                            }
                            
                        } else if (fields[k].datatype === 'Datetime') {
                            str += "<p navi='" + (navi) + "'>";//2021-08-10T15:56:35.679446+07:00
                            str += ReturnDateTime(value, fields[k].dataformat);
                            str += "</p>";
                        }
                        else if (fields[k].isCount) {
                            if (value.length > 0)
                                str += "<p>" + value.length + "</p>";
                        }
                        else if (fields[k].isMethod) {
                            //if (typeof fields[k].method === 'function')
                            str += window[fields[k].method](value);
                        }
                        else {
                            if (id !== undefined && id !== '') {
                               // str += "<p data-id='" + id + "' navi='" + (navi) + "' style='width:" + (fields[k].width + fields[k].widthPlus - 15) + "px;'>";
                                str += "<p data-id='" + id + "' navi='" + (navi) + "'>";
                                str += (value === undefined || value === "0" || value === 0 ? "" : value);
                                str += "</p>";
                            }
                            else {
                                str += (value === undefined || value === "0" || value === 0 ? "<p>&nbsp</p>" : "<p navi='" + (navi) + "'>" + value + "</p>");
                            }
                        }
                    }
                   
                }
                else {
                    var template = fields[k].template;
                    var templates = template.split('#');
                    var reg;
                    for (m = 0; m < templates.length; m++) {
                        if (!(m % 2 === 0)) {
                            value = GetObjectValueByKeys(idata[i], templates[m]);
                            reg = '#' + templates[m] + '#'
                            template = template.replace(reg, value);
                        }
                    }
                    str += template;
                    
                }

                str += "</td>";
            };
            str += "</tr>";
            str += "</table>";
            str += "</div>";
        }
        

        $(element).html(str);

    }

    obj.CleanSort = function(){
        $('.header').find('span').removeClass('asc').removeClass('desc');
    }

    obj.onClikCheckAll = function() {
        $('.header').find('input').on('click', function () {
            if ($(this).is(':checked')) {
                $('#content').find('input').prop('checked', true);
                console.log("checked");
            }
            else {
                $('#content').find('input').prop('checked', false);
                console.log("unchecked");
            }
        });
    }

    obj.onClickTitle = function () {
        $('.label').on('click', function () {

            var id = $(this).attr('id');

            var sortID = '#s' + id;
            var sort = ($(this).attr('data-id') === 'true');
            
            //console.log(sort + ' ' + id);
            //if (id !== 'pdf') {
            if (sort) {
                obj.CleanSort();
                if (obj.sortKey !== id) {
                    $(sortID).addClass('asc');
                    obj.sortValue = 'asc';
                    obj.sortKey = id;
                }
                else {
                    $(sortID).addClass('desc');
                    obj.sortValue = 'desc';
                    obj.sortKey = '';
                }
                console.log('onClickTitle: ID ' + id + ' ' + obj.sortValue + ' startWith: ' + $(this).attr('data-id'));

                obj.SortData(id);

                //Hide search panel
                obj.HideSearch();
            }
        });
    }

    obj.onClickFilter = function() {
        $('.filter').on('click', function () {
            var id = $(this).attr('id');
            var left = $(this).offset().left;
            var top = $(this).offset().top + 24;
            if (obj.searchType !== id) {
                obj.searchType = id;
                $('#keyword').val('')
            }
            obj.ShowSearch(left, top, id);

            // console.log(id + ' left: ' + left + ' yTop: ' + top);

        });
    }

    obj.onClickSearchSubmit = function() {
        $('#search').find('button').on('click', function () {
            var id = $(this).attr('id');
            if (id === 'submit') {
                obj.keyword = $('#keyword').val().trim();
            }
            else {
                obj.keyword = '';
            }
            //Set checkbox All = false
            $('#chkAll').prop('checked', false);

            obj.FilterData();
            //Hide search panel
            obj.HideSearch();

        });
    }

    obj.onClickRow = function (data) {
        $('.tab').find('p').on('click', function () {
            var id = $(this).attr('id');//Get cell ID
            var dataid = $(this).attr('data-id');

            var navigate = $(this).attr("navi");
            navigate = navigate === "true" ? true : false;


            if (id !== undefined && id !== '') {

                var value = id.substr(3);

                id = id.substr(0, 3);
            } else
                id = '0';

            //console.log("Gridview row index: " + $(this).index());

            if (navigate) {
                switch (id) {
                    case 'pdf':
                        obj.ShowPDF(data, value);
                        break;
                    case 'vie':
                        obj.ShowDetails(data, value);
                        break;
                    case 'edi':
                        obj.ShowEdit(data, value);
                        break;
                    case 'del':
                        obj.ShowDelete(data, value);
                        break;
                    default:
                        obj.ShowNavigate(data, dataid);
                        break;
                }
            }
        });
    }

    obj.SortData = function (id) {
        var arraySort = new Array();
        var A, B;
        switch (obj.sortValue) {
            case 'asc':
                if (obj.isSearch)
                    arraySort = obj.dataSearch;
                else
                    arraySort = obj.data;


                if (!$.isNumeric(id)) {
                    arraySort = arraySort.sort(function (a, b) {

                        var columns = obj.Grid.columns;
                        for (i = 0; i < columns.length; i++) {
                            if (id === columns[i].field) {
                                var values = columns[i].value.split('.');
                                if (values.length === 1) {
                                    A = a[values[0]];
                                    B = b[values[0]];
                                }
                                else if (values.length === 2) {
                                    A = a[values[0]][values[1]];
                                    B = b[values[0]][values[1]];
                                }
                            }
                        }                        

                        if (A < B) {
                            return -1;
                        }

                        if (A > B)
                            return 1;

                        if (A == B)
                            return 0;
                    });

                }
                else {                    
                    arraySort = arraySort.sort(function (a, b) {
                        return a.id - b.id;
                    });
                }

                if (obj.isSearch)
                    obj.dataSearch = arraySort;
                else
                    obj.data = arraySort;
                break;
            case 'desc':
                if (obj.isSearch) {
                    arraySort = obj.dataSearch.reverse();
                }
                else {
                    arraySort = obj.data.reverse();
                }
                break;
        }
        obj.data = arraySort;
        //2021-07-08
        obj.Refresh('#content', obj.data);


    }

    obj.FilterData = function () {

        obj.dataSearch = new Array();
        
        if (obj.keyword === null || obj.keyword === '' || obj.keyword === undefined) {
            obj.dataSearch = obj.data;            
        }
        else {
            for (var i = 0; i < obj.data.length; i++) {
                var value = '';
                var fields = obj.Grid.columns;
                for (k = 0; k < fields.length; k++) {
                    if (obj.searchType === 'f' + fields[k].field) {
                        value = GetObjectValueByKeys(obj.data[i], fields[k].value);
                    }
                };


                obj.keyword = obj.keyword.toLowerCase();

                value = value.toString();
                value = value.trim().toLowerCase();
                if (value.search(obj.keyword) > -1) {
                    obj.dataSearch.push(obj.data[i]);
                }

            }           
        }
        if (obj.dataSearch.length > 0) {
            obj.isSearch = true;

            obj.ResetFooterPage(0, obj.dataSearch.length, obj.dataSearch.length);

            obj.Refresh('#content', obj.dataSearch);
        } else {
            obj.isSearch = false;
        }        
    }

    obj.ShowSearch = function (left, top, id) {
        var search = $('#search');
        if (search.is(':hidden'))
            search.show();
        // else
        //     search.hide();

        if (id === 'ftools')
            left = left - search.width();
        else
            left = left - 30;
        search.offset({ top: top + 2, left: left });
        var keywords = [];
        var kq = '';
        $('#content').find('input').each(function () {
            var id = $(this).attr('id');
            if ($(this).is(':checked'))
                keywords.push($('#u' + id.substring(3)).html().trim());

        });
        // console.log(keyword.join());            

    }

    obj.FillData = function (element, data) {        

        //Create Conten Table
        obj.CreateContent(element, data);        

        //Register onClick row
        obj.onClickRow(obj.data);
    }

    obj.DataNotFound = function (element) {
        var str = "";
        str += "<table style='width:" + (obj.Grid.width - 260) + "px;' class='tab'>";
        str += "<tr><td style='text-align:left;font-size:1.1em;color:#f00;padding-top:0px;'>Data not Found.</td></tr>";
        str += "</table>";
        $(element).html(str);
    }

    obj.DataLoading = function (element) {
        var str = "";
        str += "<table style='width:" + obj.Grid.width + "px;' class='tab'>";
        str += "<tr><td style='text-align:center;font-size:1.8em;color:#f00;padding-top:30px;'>";
        str += "<img src='../Images/ic_loading.gif' />";
        str += "</td ></tr > ";
        str += "</table>";
        $(element).html(str);
    }

    obj.SetResultValueColor = function (value) {

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

    obj.Refresh = function (element, idata) {
        $(element).html();
        
        if (idata === undefined)
            idata = obj.data;
        obj.FillData(element, idata);

    }

    obj.LoadData = function (data) {
        obj.DataLoading('#content');       
        if (obj.Grid.data !== undefined) {
            obj.data = obj.Grid.data;            
            obj.sumRow = obj.data.length;

            //console.log(JSON.stringify(obj.data));
            if (obj.sumRow > 0) {

                //If sumrow > rowOD Page show foot
                // if (obj.sumRow > obj.rowOfPage) {

                //Reset  first page point 0
                obj.curPage = 0, obj.startPoint = 0, coutGroupPage = 0;

                obj.ShowFooterPages('.lfooter', obj.sumRow);

                //Fill data conten
                obj.FillData('#content', obj.data);

                //Register onClick Footer Pages
                obj.onClickPages();

                //Footer
                obj.ResetFooterPage(0, obj.sumRow, obj.sumRow);

                // }
            }
            else {
                obj.DataNotFound("#content");

                $('.rfooter').hide();
                $('.lfooter').hide();

            }
        } else {
            $.ajax({
                type: 'POST',
                dataType: "JSON",
                url: obj.Grid.url,
                data: data,
                success: function (status) {
                    //console.log(JSON.stringify(status));
                    if (status.Status === 'error') {
                        if (status.Code === 0)
                            window.location = status.Link;
                    } else {
                        obj.data = status.Data;
                        obj.sumRow = obj.data.length;
                        if (obj.sumRow > 0) {

                            //If sumrow > rowOD Page show foot
                            // if (obj.sumRow > obj.rowOfPage) {

                            //Reset  first page point 0
                            obj.curPage = 0, obj.startPoint = 0, coutGroupPage = 0;

                            obj.ShowFooterPages('.lfooter', obj.sumRow);

                            //Fill data conten
                            obj.FillData('#content', obj.data);

                            //Register onClick Footer Pages
                            obj.onClickPages();

                            //Footer
                            obj.ResetFooterPage(0, obj.sumRow, obj.sumRow);

                            // }
                        }
                        else {
                            obj.DataNotFound("#content");

                            $('.rfooter').hide();
                            $('.lfooter').hide();

                        }
                    }
                },
                error: function () {
                    //alert("Load MRO Report List Failed...");
                }
            });
        }
    }

    obj.HideSearch = function () {
        $('#search').hide();
    }

    //Callback details
    obj.ShowDetails = function (idata, id) {
        obj.ShowHidePopup(obj.Grid.popup);
        if (typeof Details !== undefined && typeof Details === 'function')
            Details(idata, id);
    }

    //Callback open pdf
    obj.ShowPDF = function (data, value) {       
        if (typeof OpenPDF !== undefined && typeof OpenPDF === 'function') {           
            OpenPDF(data, value);
        }
    }

    //callback navigate
    obj.ShowNavigate = function (data, id) {
        if (typeof Navigate !== undefined && typeof Navigate === 'function') {
            Navigate(data, id, obj.Grid.page_navigate);
        }
    }

    //callback Edit
    obj.ShowEdit = function (data, id) {
        if (typeof Edit !== undefined && typeof Edit === 'function') {
            Edit(data, id);
        }
    }

    //callback Delete
    obj.ShowDelete = function (data, id) {
        if (typeof Delete !== undefined && typeof Delete === 'function') {
            Delete(data, id);
        }
    }

    obj.ShowHidePopup = function (isShow) {
        var popup = $('#popup');
        popup.height($(window).height());
        if (!popup.is(':visible')) {
            if (isShow)
                popup.show();
        }

        $('.close').on('click', function () {
            $('#popup').hide();
        });
    }

    //footer page        
    obj.ShowFooterPages = function (element, maxLenght) {
        var str = "";
        //get obj.sumRow
        obj.sumRow = maxLenght;
        //Tong so trang 
        obj.lastPage = parseInt(obj.sumRow / obj.rowOfPage) + (obj.sumRow % obj.coutPage == 0 ? 0 : 1);
        // pages = (obj.sumRow % pages == 0 ? pages : pages + 1);

        //Nhom trang hien thi toi
        obj.maxGroupPage = parseInt(obj.sumRow / (obj.coutPage * obj.rowOfPage));

        if (obj.lastPage < obj.coutPage) {
            obj.limitPage = obj.lastPage;
        }
        else {
            obj.limitPage = obj.coutPage;
        }


        obj.CreatedFooterPage(element, obj.sumRow);

        obj.SetStatusPage();

    }

    obj.onClickPages = function (element) {
        $('.lfooter').find('p').on('click', function () {
            var id = $(this).attr('id');

            //set lai trang thai so trang
            obj.SetStatusPage(id);

            obj.SetPageValues();


            obj.startPoint = obj.curPage * obj.rowOfPage;
            obj.endPoint = obj.startPoint + obj.rowOfPage;
            obj.endPoint = obj.endPoint > obj.sumRow ? obj.sumRow : obj.endPoint;
                        
            obj.FillDataInfoPage(obj.startPoint, obj.endPoint);
            var arrData = [];
            if (obj.isSearch)
                arrData = obj.dataSearch;
            else
                arrData = obj.data;

            obj.Refresh('#content', arrData);

            
        });

    }

    obj.SetStatusPage = function (id) {
        var eNextPage = $('#nextPage');
        var eLastPage = $('#lastPage');
        var ePrevPage = $('#prevPage');
        var eFirstPage = $('#firstPage');
        if (id === undefined || id === '')
            id = '0';
        //console.log('id: ' + id + '/' + id.substr(0, 2) + '/' + id.substr(2));


        if (id === 'lastPage') {
            obj.curPage = obj.lastPage - 1;
        }

        if (id === 'firstPage')
            obj.curPage = 0;

        if (id === 'nextPage') {
            if (obj.curPage < obj.lastPage - 1)
                obj.curPage++;
        }

        if (id === 'prevPage') {
            if (obj.curPage > 0) {
                obj.curPage--;
            }
        }

        if (id.substr(0, 2) === 'ps') {
            var num = obj.coutGroupPage * obj.coutPage + parseInt(id.substr(2));
            obj.curPage = num;
           // console.log('page:' + parseInt(id.substr(2)) + ' curPage: ' + obj.curPage + ' coutGroup:' + obj.coutGroupPage + ' num: ' + num);
        }

        if (obj.curPage === 0) {
            ePrevPage.removeClass('').addClass('disabled').html('');
            eFirstPage.removeClass('').addClass('disabled').html('');
        } else {
            ePrevPage.removeClass('disabled').addClass('page').html('<');
            eFirstPage.removeClass('disabled').addClass('page').html('<<');
        }

        if (obj.curPage === obj.lastPage - 1) {
            eNextPage.removeClass('').addClass('disabled').html('');
            eLastPage.removeClass('').addClass('disabled').html('');
        } else {
            eNextPage.removeClass('disabled').addClass('page').html('>');
            eLastPage.removeClass('disabled').addClass('page').html('>>');;
        }
        
        if (obj.sumRow < obj.rowOfPage) {
            ePrevPage.removeClass('').addClass('disabled').html('');
            eFirstPage.removeClass('').addClass('disabled').html('');
            eNextPage.removeClass('').addClass('disabled').html('');
            eLastPage.removeClass('').addClass('disabled').html('');

        }
        

    }

    obj.SetPageValues = function () {

        //Nhom trang hien thi toi
        obj.coutGroupPage = parseInt(obj.curPage / obj.coutPage);

        if (obj.lastPage < obj.coutPage) {
            obj.limitPage = obj.lastPage;
        }
        else {
            obj.limitPage = obj.coutPage;
        }

        //if (curPage === obj.lastPage - 1 && curPage > obj.coutPage - 1) {
        if (obj.coutGroupPage === obj.maxGroupPage) {
            obj.limitPage = obj.lastPage - obj.coutPage * obj.maxGroupPage;

            //curPage = obj.lastPage;


            for (var k = 0; k < obj.coutPage; k++) {
                if (k < obj.limitPage) {
                    //console.log('k: ' + k + '< ' + obj.limitPage);
                    var elementID = $('#ps' + k)
                    elementID.html(obj.coutGroupPage * obj.coutPage + k + 1);
                    var stt = obj.coutGroupPage * obj.coutPage + k;
                    if (stt === obj.curPage) {
                        elementID.removeClass('page').addClass('selectpage');
                    }
                    else {
                        elementID.removeClass('selectpage').addClass('page');
                    }
                }
                else {
                    $('#ps' + (k)).hide();
                    //console.log('k: ' + k + '> ' + obj.limitPage);
                }
            }
        }
        else {
            for (var i = 0; i < obj.limitPage; i++) {
                var pageID = $('#ps' + i);
                pageID.show();
                pageID.html(obj.coutGroupPage * obj.coutPage + i + 1);
                if (i === (obj.curPage % obj.coutPage))
                    pageID.removeClass('page').addClass('selectpage');
                else
                    pageID.removeClass('selectpage').addClass('page');
            }
        }


    }

    obj.CreatedFooterPage = function (element) {
        var str = '';
        str += "<table class='tabfooter' style='width:600px;'>";
        str += "<tr>";
        str += "<td>";
        str += "<p id='firstPage' class='page'><<</p>";
        str += "<p id='prevPage' class='page'><</p>";
        var point = obj.curPage % obj.coutPage;
        for (var i = 0; i < obj.limitPage; i++) {
            str += "<p id='ps" + i + "' class='" + (point == i ? 'page selectpage' : 'page') + "'>";
            str += (obj.coutPage * obj.coutGroupPage + i + 1);
            str += "</p>";
        }
        str += "<p id='nextPage' class='page'>></p>";
        str += "<p id='lastPage' class='page'>>></p>";
        str += "</td>";
        str += "</tr>";
        str += "</table>";
        $(element).html(str);

        obj.startPoint = obj.curPage * obj.rowOfPage;
        obj.endPoint = obj.startPoint + obj.rowOfPage;
        obj.endPoint = obj.endPoint > obj.sumRow ? obj.sumRow : obj.endPoint;
        
        obj.FillDataInfoPage(obj.startPoint, obj.endPoint);

        //Register onClick Footer Pages
        obj.onClickPages();

    }

    obj.FillDataInfoPage = function (start, end, length) {
        $('#startPoint').html((start + 1).toLocaleString());
        $('#endPoint').html(end.toLocaleString());
        $('#sumRow').html(obj.sumRow.toLocaleString());

        if (end === obj.sumRow)
            obj.isLast = true;
        else
            obj.isLast = false;

        if (end === obj.rowOfPage) {
            obj.isFirst = true;
        }
        else
            obj.isFirst = false

        // console.log('obj.isFirst: ' + obj.isFirst + '/ isLats: ' + obj.isLast);
    }

    obj.ResetFooterPage = function (start, end, length) {
        obj.startPoint = start;
        obj.endPoint = end;
        obj.curPage = 0;
        obj.coutGroupPage = 0;
        obj.sumRow = length;

        obj.ShowFooterPages('.lfooter', length);
    }
        
    return obj;
};