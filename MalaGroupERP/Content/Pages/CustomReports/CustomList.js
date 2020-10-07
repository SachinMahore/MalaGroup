$(document).ready(function () {
    $('#ulPaginationReportList').pagination({
        items: 0,
        currentPage: 1,
        displayedPages: 10,
        cssStyle: '',
        useAnchors: true,
        prevText: '&laquo;',
        nextText: '&raquo;',
        onInit: function () {
            buildPaganationReportList();
        },
        onPageClick: function (page, evt) {
            fillReportListTable(page, 1);
        }
    });
    $("#ddlRowPerPage").on('change', function (evt, params) {
        ddlRowPerPageChange();
    });
    $("#ddlRowPerPage").select2({ minimumResultsForSearch: Infinity });
});

var newReport=function()
{
    window.location = '/CustomReport/Index';
}


var buildPaganationReportList = function () {
    $("#divLoader").show();
    var rowDisplay = $("#ddlRowPerPage").val();

    var model = {
        RowDisplay: rowDisplay
    };
    $.ajax({
        url: '/CustomReport/GetCustomReportListRange',
        type: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#divLoader").hide();
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {
                var pnarray = response.PageNumber.split('|');
                $('#ulPaginationReportList').pagination('updateItems', pnarray[0]);
                $('#ulPaginationReportList').pagination('selectPage', 1);
                $("#lblTotalRecords").text("Total Records : " + pnarray[1]);
                fillReportListTable(1, 1);
            }
        }
    });
}
var fillReportListTable = function (PN, SO) {
    $("#divLoader").show();
    var rowDisplay = $("#ddlRowPerPage").val();
   
    var model = {
        RowDisplay: rowDisplay,
        PageNumber: PN,
    };
    $.ajax({
        url: '/CustomReport/GetCustomReportPageList',
        type: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#divLoader").hide();
            if ($.trim(response.error) != "") {
                $.alert({
                    title: 'Alert!',
                    content: response.error,
                    type: 'blue'
                });
            } else {
                $("#tblReportList>tbody").empty();
                $.each(response, function (index, elementValue) {
                    var html = '';
                    var customreportFor = '';
                        customreportFor = "'" + elementValue.CustomReportForText + "'";
                    html += '<tr data-value="' + elementValue.CustomReportID + '">';
                    html += '<td>' + elementValue.CustomReportName + '</td>';
                    html += '<td>' + elementValue.CustomReportForText + '</td>';
                    html += '<td> ' + elementValue.CreatedBy + '</td>';
                    html += '<td> ' + elementValue.CreatedDate + '</td>';
                    //html += '<td> ' + elementValue.ModiFiedBy + '</td>';
                    //html += '<td> ' + elementValue.ModiFiedDate + '</td>';
                    html += '<td style="text-align:center!important; width:200px"><button class="btn btn-success" onclick="exportReport(' + elementValue.CustomReportID + ',' + customreportFor + ')" title="Download"><i class="icon-download-alt"  ></i></td>';
                    html += '</tr>';
                    $("#tblReportList>tbody").append(html);
                });
            }
        }
    });
}

var exportReport = function (reportID, reportFor) {
    $("#divLoader").show();
    var model = {
        ReportID: reportID
    };
        $.ajax({
            url: '/CustomReport/ExportReport',
            type: "post",
            data: JSON.stringify(model),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $("#divLoader").hide();
                if ($.trim(response.error) != "") {
                    $.alert({
                        title: 'Alert!',
                        content: response.error,
                        type: 'red'
                    });
                } else {
                    if (response.FileNam != "") {
                        saveToDiskExcelFile(response.FileName, reportFor);
                    }

                }
            }
        });
}

var saveToDiskExcelFile = function (fileName, reportFor) {
   
    var saveUrl = baseURL() + "/TempFiles/" + fileName;
    var hyperlink = document.createElement('a');
    hyperlink.href = saveUrl;
    hyperlink.target = '_blank';
    hyperlink.download = reportFor + ".xlsx";

    (document.body || document.documentElement).appendChild(hyperlink);
    hyperlink.onclick = function () {
        (document.body || document.documentElement).removeChild(hyperlink);
    };
    var mouseEvent = new MouseEvent('click', {
        view: window,
        bubbles: true,
        cancelable: true
    });
    hyperlink.dispatchEvent(mouseEvent);
    if (!navigator.mozGetUserMedia) {
        window.URL.revokeObjectURL(hyperlink.href);
    }
}
var baseURL = function () {
    if (!window.location.origin) {
        window.location.origin = window.location.protocol + "//"
          + window.location.hostname
          + (window.location.port ? ':' + window.location.port : '');
        return window.location.origin //+ "/Creatives"
    } else {
        return window.location.origin //+ "/Creatives"
    }
}