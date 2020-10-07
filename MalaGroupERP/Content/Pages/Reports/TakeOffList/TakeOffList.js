
var selectFieldDataSource = [
     { text: "Customer Type", value: "1" },
     { text: "Last Modified Date", value: "2" },
     { text: "Take Off List", value: "3" },
     { text: "Account Status", value: "4" }
]

var showDataSource = [
     { text: "All Account", value: "1" },
     { text: "All Lead", value: "2" },
     { text: "Both(Lead & Account)", value: "3" }
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


var getAccountType=function()
{
    $("#ddlCriteria").empty();
    $.each(showDataSource, function (n, v) {
        $("#ddlCriteria").append("<option value='" + v.value + "'>" + v.text + "</option>");
        $('#ddlCriteria').val(1).trigger('change');
    });
}

var getAccountStatus=function()
{
    $("#ddlCriteriaMul").empty();
    $.each(accountStatusdDataSource, function (n, v) {
        $("#ddlCriteriaMul").append("<option value='" + v.value + "'>" + v.text + "</option>");
        $('#ddlCriteriaMul').val(0).trigger('change');
    });
}
var getSelectFieldList = function () {
    $("#SelectField").empty();
    $.each(selectFieldDataSource, function (n, v) {
        $("#SelectField").append("<option value='" + v.value + "'>" + v.text + "</option>");
        $('#SelectField').val(1).trigger('chosen:updated');
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
    $("#divDDLCriteriaMul").addClass("hidden");
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
  if (selectFieldValue == "2" ) {
        $("#divDateRangeCriteria").removeClass("hidden");
        setSearchDate();
  }
  else if (selectFieldValue == "1") {
      $("#divDDLCriteria").removeClass("hidden");
      getAccountType();
  }
  else if (selectFieldValue == "4") {
      //Drop Down
      $("#divDDLCriteriaMul").removeClass("hidden");
      getAccountStatus();
  }
  else if (selectFieldValue == "3") {
      //CHECK BOX
      $("#divCheckBoxCriteria").removeClass("hidden");
      $("#lblChkCriteria").text("TakeOffList-Yes");
  }
}
var addFilterCriteria = function () {
    var filterText = "";
    var filterValue = "";
    var selectFieldValue = $("#SelectField").val();


     if (selectFieldValue == "2" ) {
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
     }
     else if (selectFieldValue == "4") {
         //DROP DOWN LIST
         var selText = "";
         var value = "";
         $("#ddlCriteriaMul option:selected").each(function () {
             var $this = $(this);
             if ($this.length) {
                 selText += $this.text() + ',';
                 value += $this.val() + ',';
             }
         });
         filterText = selText;
         filterValue = value;
     }
     else if (selectFieldValue == "1") {
         //DROP DOWN LIST
         filterText = $("#ddlCriteria option:selected").text();
         filterValue = $("#ddlCriteria").val();
     }
     else if (selectFieldValue == "3") {
         //CHECK BOX
         filterText = $("#chkCriteria").is(":checked") ? "Yes" : "No";
         filterValue = $("#chkCriteria").is(":checked") ? "1" : "0";
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
    $("#ddlCriteria").val(1).trigger('change');
    $("#ddlCriteriaMul").val(0).trigger('change');
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
    SelectFieldChange(3);
    $("#SelectField").on('change', function (evt, params) {
        SelectFieldChange();
    });
});

var exportDayWise = function () {
    $("#divLoader").show();
    var show = "";
    var lastModiFiedByDate = "";
    var takeOffList = "";
    var accountStatus = "";
   
    if ($("#divFilterCriteria").find("#divFC_1").length) {
        show = $('#lblFT_1').data("value");
    }
    if ($("#divFilterCriteria").find("#divFC_2").length) {
        lastModiFiedByDate = $('#lblFT_2').data("value");
    }
    if ($("#divFilterCriteria").find("#divFC_3").length) {
        takeOffList = $('#lblFT_3').data("value");
    }
    if ($("#divFilterCriteria").find("#divFC_4").length) {
        accountStatus = $('#lblFT_4').data("value");
    }
    var model = {
        Show: show,
        LastModiFiedDate: lastModiFiedByDate,
        TakeOffList: takeOffList,
        AccountStatus: accountStatus

    };
    $.ajax({
        url: '/TakeOffList/GetTakeOffListReport',
        type: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#divLoader").hide();
            if ($.trim(response.error) != "") {
                $.alert({
                    title: 'Msg!',
                    content: response.error,
                    type: 'red'
                });
            } else {
                $.alert({
                    title: 'Alert!',
                    content: response.MSG,
                    type: 'blue'
                });
                //$("#divSearchDay").addClass("hidden");
                //$("#divViewDay").removeClass("hidden");
                //$("#tblTakeOffList>tbody").empty();
                //$("#hndFileName").val(response.model.ExportFileName);
                //$.each(response.model.TakeOFFDataList, function (index, elementValue) {
                //        var html = '';
                //        html += '<tr>';
                //        html += '<td>' + elementValue.PinNumber + '</td>';
                //        html += '<td> ' + elementValue.FirstName + '</td>';
                //        html += '<td> ' + elementValue.LastName + '</td>';
                //        html += '<td> ' + elementValue.Street + '</td>';
                //        html += '<td> ' + elementValue.City + '</td>';
                //        html += '<td> ' + elementValue.State + '</td>';
                //        html += '<td> ' + elementValue.Zip + '</td>';
                //        html += '<td> ' + elementValue.VIN + '</td>';
                //        html += '<td>' + elementValue.LastProduct + '</td>';
                //        html += '<td> ' + elementValue.LastModiFiedDate + '</td>';
                //        html += '</tr>';
                //        $("#tblTakeOffList>tbody").append(html);
                //});
            }
        }
    });
}




var saveToDiskExcelFile = function () {
    var fileName = $("#hndFileName").val();
    var hName = "Take Off List";
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
