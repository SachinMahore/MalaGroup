
var selectFieldDataSource = [
     { text: "Close Date", value: "1" },
     { text: "Identity Theft", value: "2" },
     { text: "Additional Decal", value: "3" },
      { text: "Account Status", value: "4" },
       { text: "Product", value: "5" }
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

var getProductList = function () {
    $("#ddlCriteriaMul").empty();
    
    $.ajax({
        url: "/Payroll/GetProductList",
        type: "post",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            $("#ddlCriteriaMul").append("<option value='0'>ALL</option>");
            $.each(response.model, function (index, elementValue) {
                var opt = "<option value='" + elementValue.Value + "'>" + elementValue.Text + "</option>";
                $("#ddlCriteriaMul").append(opt);
            });
            $("#ddlCriteriaMul").val(0).trigger('change');
        }
    });
}
//var getAccountStaus = function () {
//    $("#ddlCriteriaMul").empty();
//    $.each(accountStatusdDataSource, function (n, v) {
//        $("#ddlCriteriaMul").append("<option value='" + v.value + "'>" + v.text + "</option>");
//        $('#ddlCriteriaMul').val(0).trigger('change');
//    });
//}


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
var fillCriteriaDropDownLists = function (selectedVal) {
    if (selectedVal == "4") {
        $("#ddlCriteriaMul").empty();
        $.each(accountStatusdDataSource, function (n, v) {
            $("#ddlCriteriaMul").append("<option value='" + v.value + "'>" + v.text + "</option>");
            $('#ddlCriteriaMul').val(0).trigger('change');
        });
    } else if (selectedVal == "5") {
        getProductList();
    }
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
    $("#chkCriteria").prop('checked', false);
    //$.uniform.update("#chkCriteria");
    //$('#chkCriteria').removeAttr('checked')
    setSearchDate();
    $("#ddlCriteria").val(1);
    $("#txtRangeFrom").val("");
    $("#txtRangeTo").val("");
  if (selectFieldValue == "1" ) {
        $("#divDateRangeCriteria").removeClass("hidden");
        setSearchDate();
  }
 
  else if (selectFieldValue == "2") {
      //CHECK BOX
      $("#divCheckBoxCriteria").removeClass("hidden");
      $("#lblChkCriteria").text("Identity Theft-True");
  }
  else if (selectFieldValue == "3") {
      //CHECK BOX
      $("#divCheckBoxCriteria").removeClass("hidden");
      $("#lblChkCriteria").text("Add Decal-True");
  //} else if (selectFieldValue == "4") {
  //    //TEXT BOX
     
  //    $("#divDDLCriteria").removeClass("hidden");
  //    getAccountStaus(1);
  } else if (selectFieldValue == "5" || selectFieldValue == "4") {
      //DROP DOWN LIST
      //$("#ddlCriteria").attr("multiple", "multiple");
      $("#divDDLCriteriaMul").removeClass("hidden");
      fillCriteriaDropDownLists(selectFieldValue);
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
     }
     //else if (selectFieldValue == "4") {
     //    //DROP DOWN LIST

     //    filterText = $("#ddlCriteria option:selected").text();
     //    filterValue = $("#ddlCriteria").val();
     //    console.log(filterText);
     //    console.log(filterValue);
     //}
     else if (selectFieldValue == "5" || selectFieldValue == "4") {
         //DROP DOWN LIST multi
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
     else if (selectFieldValue == "2") {
         //CHECK BOX
         filterText = $("#chkCriteria").is(":checked") ? "True" : "False";
         filterValue = $("#chkCriteria").is(":checked") ? "1" : "0";
     }
     else if (selectFieldValue == "3") {
         //CHECK BOX
         filterText = $("#chkCriteria").is(":checked") ? "True" : "False";
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
    $.uniform.update("#chkCriteria");
    setSearchDate();
    $("#ddlCriteria").val(0).trigger('change');
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
    $("#divDateRangeCriteria").removeClass("hidden");
    getSelectFieldList();
    SelectFieldChange(1);
    $("#SelectField").on('change', function (evt, params) {
       SelectFieldChange();
    });
    $("#ddlCriteriaMul").on('change', function (evt, params) {
        var selected = $(this).val();
        if (selected != null) {
            for (var j = 0; j < selected.length; j++) {
                if (selected[j] == 0) {
                    $("#ddlCriteriaMul").select2('data', null);
                    $("#ddlCriteriaMul").select2('data', { id: '0', text: 'All' });
                    break;
                }
            }
        }
    });
});

var exportDayWise = function () {
    $("#divLoader").show();
    var closeDate = "";
    var identityTheft = "";
    var addDecal = "";
    var accountStatus = "";
    var product = "";
   
    if ($("#divFilterCriteria").find("#divFC_1").length) {
        closeDate = $('#lblFT_1').data("value");
    }
    if ($("#divFilterCriteria").find("#divFC_2").length) {
        identityTheft = $('#lblFT_2').data("value");
    }
    if ($("#divFilterCriteria").find("#divFC_3").length) {
        addDecal = $('#lblFT_3').data("value");
    }
    if ($("#divFilterCriteria").find("#divFC_4").length) {
        accountStatus = $('#lblFT_4').data("value");
    }
    if ($("#divFilterCriteria").find("#divFC_5").length) {
        product = $('#lblFT_5').data("value");
    }
    var model = {
        CloseDate: closeDate,
        IdentityTheft: identityTheft,
        AddDecal: addDecal,
        AccountStatus: accountStatus,
        Product: product
    };
    $.ajax({
        url: '/IdentityTheft/GetIdentityTheftReport',
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
                $("#tblIdentityTheft>tbody").empty();
                $("#hndFileName").val(response.model.ExportFileName);
                $.each(response.model.IdentityTheftDataList, function (index, elementValue) {
                    var html = '';
                        html += '<tr>';
                        html += '<td>' + elementValue.CloseDate + '</td>';
                        html += '<td><span class="icon"> <i class="' + (elementValue.IDTheft == 1 ? "icon-ok" : "icon-remove") + '"></i> </span></td>';
                        html += '<td><span class="icon"> <i class="' + (elementValue.ADDDecal == 1 ? "icon-ok" : "icon-remove") + '"></i> </span></td>';
                        html += '<td> ' + elementValue.FirstName + '</td>';
                        html += '<td> ' + elementValue.LastName + '</td>';
                        html += '<td> ' + elementValue.Address + '</td>';
                        html += '<td> ' + elementValue.City + '</td>';
                        html += '<td> ' + elementValue.State + '</td>';
                        html += '<td> ' + elementValue.ZIP + '</td>';
                        html += '<td>' + elementValue.PHONE + '</td>';
                        html += '<td> ' + elementValue.Password + '</td>';
                        html += '<td> ' + elementValue.PINNO + '</td>';
                        html += '<td> ' + elementValue.CreadtedBy + '</td>';
                        html += '<td><span class="icon"> <i class="' + (elementValue.IsRenewal == 1 ? "icon-ok" : "") + '"></i> </span></td>';
                        html += '</tr>';
                        $("#tblIdentityTheft>tbody").append(html);
                });
            }
        }
    });
}




var saveToDiskExcelFile = function () {
    var fileName = $("#hndFileName").val();
    var hName = "Identity Theft";
    var saveUrl = baseURL() + "/TempFiles/" + fileName;
    //saveUrl = encodeURIComponent(saveUrl);
    var hyperlink = document.createElement('a');
    hyperlink.href = saveUrl;
    hyperlink.target = '_blank';
    hyperlink.download = hName + ".xlsx";

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
