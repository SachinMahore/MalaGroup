
var selectFieldDataSource = [
     { text: "Close Date", value: "1" },
     { text: "Charge Amount Greater Than", value: "2" },
     { text: "Charge Amount Less Than", value: "3" },
     { text: "Charge Amount Not Equal", value: "4" },
     { text: "Account Status", value: "5" }
]

var accountStatusdDataSource = [
     { text: "All", value: "0" },
     { text: "Active", value: "1" },
     { text: "Suspended", value: "2" },
     { text: "Cancelled - No Refund", value: "3" },
     { text: "Cancelled - Full Refund", value: "4" },
     { text: "Cancelled - Partial Refund", value: "5" },
     { text: "Caspio Account", value: "6" },
    { text: "Check - Pending", value: "7" },
    { text: "Comp - Pending", value: "8" }
    ]

var getSelectFieldList = function () {
    $("#SelectField").empty();
    $.each(selectFieldDataSource, function (n, v) {
        $("#SelectField").append("<option value='" + v.value + "'>" + v.text + "</option>");
        $('#SelectField').val(1).trigger('change');
    });
}
var setSearchDate = function () {
    var todate = new Date();
    todate.setDate(todate.getDate());
    var dd = todate.getDate();
    var mm = todate.getMonth() + 1;
    var yyyy = todate.getFullYear();
    if (dd < 10) { dd = '0' + dd } if (mm < 10) { mm = '0' + mm }
    var todate = mm + '/' + dd + '/' + yyyy;
    $("#dtpFromDate").val(todate);
    $("#dtpToDate").val(todate);
}


var SelectFieldChange = function () {
    var selectFieldValue = $("#SelectField").val();
    $("#divDDLCriteria").addClass("hidden");
    $("#divTxtCriteria").addClass("hidden");
    $("#divCheckBoxCriteria").addClass("hidden");
    $("#divDateRangeCriteria").addClass("hidden");
    $("#divRangeCriteria").addClass("hidden");
    $("#Criteria").val("");
    $("#chkCriteria").prop("checked", false);
    setSearchDate();
    $("#ddlCriteria").val(1);
    $("#txtRangeFrom").val("");
    $("#txtRangeTo").val("");
  if (selectFieldValue == "1" ) {
        $("#divDateRangeCriteria").removeClass("hidden");
        setSearchDate();
  } else if (selectFieldValue == "2" || selectFieldValue == "3" || selectFieldValue == "4" ) {
      //TEXT BOX
      $("#divTxtCriteria").removeClass("hidden");
  }
  else if (selectFieldValue == "5" ) {
      //DropDown MUl
      fillCriteriaDropDownLists(selectFieldValue);
      $("#divDDLCriteria").removeClass("hidden");
    
  }
}

var fillCriteriaDropDownLists = function (selectedVal) {
    if (selectedVal == "5") {
        $("#ddlCriteria").empty();
        $.each(accountStatusdDataSource, function (n, v) {
            $("#ddlCriteria").append("<option value='" + v.value + "'>" + v.text + "</option>");
            $('#ddlCriteria').val(0).trigger('change');
        });
    } 
}
var addFilterCriteria = function () {
    var filterText = "";
    var filterValue = "";
    var selectFieldValue = $("#SelectField").val();


     if (selectFieldValue == "1" ) {
        //DATE PICKER
        if ($("#dtpFromDate").val() + "" == "" && $("#dtpToDate").val() + "" == "") {
            setSearchDate();
        }
        else if ($("#dtpFromDate").val() + "" != "" && $("#dtpToDate").val() + "" == "") {
            $("#dtpToDate").val($("#dtpFromDate").val());
        }
        else if ($("#dtpFromDate").val() + "" == "" && $("#dtpToDate").val() + "" != "") {
            $("#dtpFromDate").val($("#dtpToDate").val());
        }
        filterText = $("#dtpFromDate").val() + "-" + $("#dtpToDate").val();
        filterValue = filterText;
     } else if (selectFieldValue == "2" || selectFieldValue == "3" || selectFieldValue == "4" ) {
         //TEXT BOX
         filterText = $("#Criteria").val();
         filterValue = filterText;
     }
     else if (selectFieldValue == "5") {
         //DROP DOWN LIST
         var selText = "";
         var value = "";
         $("#ddlCriteria option:selected").each(function () {
             var $this = $(this);
             if ($this.length) {
                 selText += $this.text() + ',';
                 value += $this.val() + ',';
             }
         });
         filterText = selText;
         filterValue = value;
     }
    //-----------------------------------------------------//

    var data = $("#SelectField").select2('data');
    if (data) {
        var filterType = data.text;
    }
    var filterCriteria = $("#SelectField").val();

    if ($("#divFilterCriteria").find("#divFC_" + filterCriteria).length) {
        $('#lblFT_' + filterCriteria).html(filterType.toUpperCase() + " : " + filterText.toUpperCase());
        $('#lblFT_' + filterCriteria).data("value", filterValue);
    }
    else {
        $("#divFilterCriteria").append("<div id=divFC_" + filterCriteria + " class='form-group clearfix m-0' style='margin:0'><label id='lblFT_" + filterCriteria + "' class='control-label nopadding' data-value='" + filterValue + "'>" + filterType.toUpperCase() + " : " + filterText.toUpperCase() + "</Label><button id='btnRemove' type='button' class='btn btn-danger pull-right' style='margin-top: -26px!important;'  onclick='removeFilter(" + filterCriteria + ")'><i class='icon icon-trash' style='margin-top:-3px'></i></button></div>");
    }
    $("#Criteria").val("");
    $("#chkCriteria").prop("checked", false);
    setSearchDate();
    $("#ddlCriteria").val(0).trigger('change');
}
var removeFilterAll = function () {
    $("#divFilterCriteria").html("");
}
var removeFilter = function (id) {
    $('#divFC_' + id).remove();
}
$(document).ready(function () {
    $("#divViewDay").addClass("hidden");
    $("#divSearchDay").removeClass("hidden");
    getSelectFieldList();
    SelectFieldChange(1);
    $("#SelectField").on('change', function (evt, params) {
        SelectFieldChange();
    });
    $("#ddlCriteria").on('change', function (evt, params) {
        var selected = $(this).val();
        if (selected != null) {
            for (var j = 0; j < selected.length; j++) {
                if (selected[j] == 0) {
                    $("#ddlCriteria").select2('data', null);
                    $("#ddlCriteria").select2('data', { id: '0', text: 'All' });
                    break;
                }
            }
        }
    });
});

var exportDayWise = function () {
    $("#divLoader").show();
    var date = "";
    var greaterAmt = "";
    var lessAMT = "";
    var notEqualAMT = "";
    var accountStatus = "";
    if ($("#divFilterCriteria").find("#divFC_1").length) {
        date = $('#lblFT_1').data("value");
    }
    if ($("#divFilterCriteria").find("#divFC_2").length) {
        greaterAmt = $('#lblFT_2').data("value");
    }
    if ($("#divFilterCriteria").find("#divFC_3").length) {
        lessAMT = $('#lblFT_3').data("value");
    }
    if ($("#divFilterCriteria").find("#divFC_4").length) {
        notEqualAMT = $('#lblFT_4').data("value");
    }
    if ($("#divFilterCriteria").find("#divFC_5").length) {
        accountStatus = $('#lblFT_5').data("value");
    }
    if (!date) {
        $("#divLoader").hide();
        $.alert({
            title: 'Alert!',
            content: "Please select Close Date",
            type: 'red'
        });
        return;
    }
   
    var model = {
        Date: date,
        GreaterAmt: greaterAmt,
        LessAMT: lessAMT,
        NotEqualAMT: notEqualAMT,
        AccountStatus: accountStatus
    };
    $.ajax({
        url: '/DayWiseReport/GetExportDayWiseData',
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
                $("#divSearchDay").addClass("hidden");
                $("#divViewDay").removeClass("hidden");
                $("#tblDayWiseReport>tbody").empty();
                $("#hndFileName").val(response.model.ExportFileName);
                $.each(response.model.DayWiseDataList, function (index, elementValue) {
                        var html = '';
                        html += '<tr>';
                        html += '<td>' + elementValue.PrimaryPhone + '</td>';
                        html += '<td> ' + elementValue.PasswordId + '</td>';
                        html += '<td> ' + elementValue.PinNumber + '</td>';
                        html += '<td> ' + elementValue.FirstName + '</td>';
                        html += '<td> ' + elementValue.LastName + '</td>';
                        html += '<td> ' + elementValue.MailingStreet + '</td>';
                        html += '<td> ' + elementValue.MailingCity + '</td>';
                        html += '<td> ' + elementValue.MailingState + '</td>';
                        html += '<td>' + elementValue.MailingZip + '</a></td>';
                        html += '<td> ' + elementValue.CreatedBy + '</td>';
                        html += '<td> ' + elementValue.Product + '</td>';
                        html += '<td> ' + elementValue.PaymentFrequency + '</td>';
                        html += '<td> ' + elementValue.AdditionalDecals + '</td>';
                        html += '<td> ' + elementValue.IdentityTheft + '</td>';
                        html += '<td> $' + parseFloat(elementValue.TransactionTotal).toFixed(2) + '</td>';
                        html += '<td> $' + parseFloat(elementValue.ChargeAmount).toFixed(2) + '</td>';
                        html += '<td> ' + elementValue.VIN + '</td>';
                        html += '<td>' + elementValue.VehicleYear + '</td>';
                        html += '<td> ' + elementValue.VehicleMake + '</td>';
                        html += '<td> ' + elementValue.PaymentCount + '</td>';
                        html += '<td> ' + elementValue.ChargeDate + '</td>';
                        html += '<td> ' + elementValue.CloseDate + '</td>';
                        html += '<td> ' + elementValue.Stage + '</td>';
                        html += '<td> ' + elementValue.Probability + '</td>';
                        html += '<td> ' + elementValue.Age + '</td>';
                        html += '<td>' + elementValue.CreatedDate + '</a></td>';
                        html += '<td> ' + elementValue.DecalNumber + '</td>';
                        html += '<td> ' + elementValue.DecalNumber2 + '</td>';
                        html += '<td> ' + elementValue.DecalNumber3 + '</td>';
                        html += '<td> ' + elementValue.DecalNumber4 + '</td>';
                        html += '<td> ' + elementValue.GPSSKU1 + '</td>';
                        html += '<td> ' + elementValue.GPSDN1 + '</td>';
                        html += '<td> ' + elementValue.GPSSKU2 + '</td>';
                        html += '<td>' + elementValue.GPSDN2 + '</td>';
                        html += '<td> ' + elementValue.GPSSKU3 + '</td>';
                        html += '<td>' + elementValue.GPSDN3 + '</td>';
                        html += '<td> ' + elementValue.GPSSKU4 + '</td>';
                        html += '<td> ' + elementValue.GPSDN4 + '</td>';
                        html += '<td> ' + elementValue.Email + '</td>';
                        html += '<td> ' + elementValue.AODID + '</td>';
                        html += '</tr>';
                        $("#tblDayWiseReport>tbody").append(html);
                });
            }
        }
    });
}


var saveToDiskExcelFile = function () {
    var fileName = $("#hndFileName").val();
    var hName = "Day Wise Report";
    var saveUrl = baseURL() + "/TempFiles/" + fileName;
    //saveUrl = encodeURIComponent(saveUrl);
    var hyperlink = document.createElement('a');
    hyperlink.href = saveUrl;
    hyperlink.target = '_blank';
    hyperlink.download = hName + ".csv";

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

var backToSearch = function () {
    $("#hndFileName").val('');
    $("#divViewDay").addClass("hidden");
    $("#divSearchDay").removeClass("hidden");
}
