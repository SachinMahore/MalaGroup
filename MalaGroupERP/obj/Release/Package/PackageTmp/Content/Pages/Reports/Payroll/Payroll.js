var selectFieldDataSource = [
     { text: "Close Date", value: "1" },
     { text: "Last Modified", value: "3" },
      { text: "Account Status", value: "2" },
     { text: "Product", value: "4" }
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
var getProductList=function(){
    $("#ddlCriteria").empty();
    $.ajax({
        url: "/Payroll/GetProductList",
        type: "post",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            $("#ddlCriteria").append("<option value='0'>ALL</option>");
            $.each(response.model, function (index, elementValue) {
                var opt = "<option value='" + elementValue.Value + "'>" + elementValue.Text + "</option>";
                $("#ddlCriteria").append(opt);
            });
            $("#ddlCriteria").val(0).trigger('change');
        }
    });
}
var fillCriteriaDropDownLists = function (selectedVal) {
    if (selectedVal == "2") {
        $("#ddlCriteria").empty();
        $.each(accountStatusdDataSource, function (n, v) {
            $("#ddlCriteria").append("<option value='" + v.value + "'>" + v.text + "</option>");
            $('#ddlCriteria').val(0).trigger('change');
        });
    } else if (selectedVal == "4") {
        getProductList();
    } 
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
    if (selectFieldValue == "2" || selectFieldValue == "4") {
        fillCriteriaDropDownLists(selectFieldValue);
        $("#divDDLCriteria").removeClass("hidden");
    }
    else if (selectFieldValue == "1" || selectFieldValue == "3") {
        $("#divDateRangeCriteria").removeClass("hidden");
        setSearchDate();
    }
}
var addFilterCriteria = function () {
    var filterText = "";
    var filterValue = "";
    var selectFieldValue = $("#SelectField").val();

   
    if (selectFieldValue == "2" || selectFieldValue == "4" ) {
        //DROP DOWN LIST
        var selText = "";
        var value = "";
        $("#ddlCriteria option:selected").each(function () {
            var $this = $(this);
            if ($this.length) {
                 selText += $this.text()+',';
                 value += $this.val()+',';
            }
        });
        filterText = selText;
        filterValue = value;
    }
    
    else if (selectFieldValue == "1" || selectFieldValue == "3") {
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
    $("#divgraph").addClass("hidden");
    $("#divSearch").removeClass("hidden");
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



var  exportPayRollReport =function(popValue) {
    $("#popPayRollDiv").PopupWindow("close");
    var pop = $("#hndPopID").val();
    var closeDateFrom = $("#dtClosingFrom").val().replace('/', '-').replace('/', '-');
    var closeDateTo = $("#dtClosingTo").val().replace('/', '-').replace('/', '-');
    var lstDateFrom = $("#dtLastClosingFrom").val().replace('/', '-').replace('/', '-');
    var lstDateTo = $("#dtLastClosingTo").val().replace('/', '-').replace('/', '-');
    var acctStaus = $("#ddlAccountStatusLay").val();
    var product = $("#ddlProduct").val();
    window.location = '/Reports/ExportReportPayRoll/?pid=' + pop + "&clDateFrom=" + closeDateFrom + "&clDateto=" + closeDateTo + "&accStatus=" + acctStaus + "&lstDateFrom=" + lstDateFrom + "&lstDateto=" + lstDateTo + "&product=" + product;
}

var exportPayroll = function () {
    $("#divLoader").show();
    var closeDate = "";
    var accountStatus = "";
    var lastModified = "";
    var product = "";

    if ($("#divFilterCriteria").find("#divFC_1").length) {
        closeDate = $('#lblFT_1').data("value");
    }
    if ($("#divFilterCriteria").find("#divFC_2").length) {
        accountStatus = $('#lblFT_2').data("value");
    }
    if ($("#divFilterCriteria").find("#divFC_3").length) {
        lastModified = $('#lblFT_3').data("value");
    }
    if ($("#divFilterCriteria").find("#divFC_4").length) {
        product = $('#lblFT_4').data("value");
    }
    var model = {
        CloseDate: closeDate,
        AccountStatus: accountStatus,
        LastModified: lastModified,
        Product: product,
    };
   
    
    $.ajax({
        url: '/Payroll/GetPayRollViewData',
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
               
                $("#divSearch").addClass("hidden");
                $("#divgraph").removeClass("hidden");
                viewGraph(response.model.payRollGraph);
                var createdByID = "0";
                var count = 0;
                var isChange = false;

                var CreatedCOUNT = "0";
                var ADDSUM = "0";
                var ChargeTotal = "0";
                var DecalCount = "0";
                var VoidCount = "0";
                var GrossCount = "0";
                var NetCount = "0";
                var IDenCount = "0";
                var rowCount = response.model.payRollData.length;
                $("#tblPayRoll>tbody").empty();
                console.log(response.model);
                $("#hndFileName").val(response.model.ExportFileName);
                $.each(response.model.payRollData, function (index, elementValue) {
                    rowCount -= 1;
                    if (createdByID != elementValue.CreatedByID)
                    {
                        
                        if (count == 0)
                        {
                            createdByID   = elementValue.CreatedByID;
                            CreatedCOUNT  = elementValue.CreatedCOUNT;
                            ADDSUM        = elementValue.ADDSUM;
                            ChargeTotal   = elementValue.ChargeTotal;
                            DecalCount    = elementValue.DecalCount;
                            VoidCount     = elementValue.VoidCount;
                            GrossCount    = elementValue.GrossSUM;
                            NetCount      = elementValue.NetSUM;
                            IDenCount     = elementValue.IDenCount;

                        }
                        if (count != 0)
                        {

                            //console.log("count=true");
                            isChange = true;
                        }

                    }
                    count += 1;
                   
                    if (isChange == true)
                    {
                        //console.log("before:"+isChange);
                        isChange = false;
                        //console.log("after:"+isChange);
                        var html = '';
                        html += '<tr>';
                        html += '<td style="background-color:yellow!important;">Sum</td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;">$' + parseFloat(ADDSUM).toFixed(2) + '</td>';
                        html += '<td style="background-color:yellow!important;">$' + parseFloat(ChargeTotal).toFixed(2) + '</td>';
                        html += '<td style="background-color:yellow!important;"> </td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"> </td>';

                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;">' + GrossCount + '</td>';
                        html += '<td style="background-color:yellow!important;">' + NetCount + '</td>';

                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '</tr>';

                        html += '<tr>';
                        html += '<td style="background-color:yellow!important;">COUNT</td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;">' + parseFloat(CreatedCOUNT).toFixed(0) + '</td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"> </td>';
                        html += '<td style="background-color:yellow!important;"></td>';

                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;">' + parseFloat(DecalCount).toFixed(0) + '</td>';
                        html += '<td style="background-color:yellow!important;">' + parseFloat(VoidCount).toFixed(0) + '</td>';
                        html += '<td style="background-color:yellow!important;"> </td>';
                        html += '<td style="background-color:yellow!important;"></td>';

                        html += '<td style="background-color:yellow!important;">' + parseFloat(IDenCount).toFixed(0) + '</td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        
                        html += '</tr>';
                        $("#tblPayRoll>tbody").append(html);

                        createdByID = elementValue.CreatedByID;
                        CreatedCOUNT = elementValue.CreatedCOUNT;
                        ADDSUM = elementValue.ADDSUM;
                        ChargeTotal = elementValue.ChargeTotal;
                        DecalCount = elementValue.DecalCount;
                        VoidCount = elementValue.VoidCount;
                        GrossCount = elementValue.GrossSUM;
                        NetCount = elementValue.NetSUM;
                        IDenCount = elementValue.IDenCount;
                        
                    }

                    //else
                    //{
                        var html = '';
                        html += '<tr>';
                        html += '<td>' + elementValue.CreatedBy + '</td>';
                        html += '<td> ' + elementValue.LastModifiedBy + '</td>';
                        html += '<td> ' + elementValue.AccountName + '</td>';
                        html += '<td> $' + parseFloat(elementValue.TransactionTotal).toFixed(2) + '</td>';
                        html += '<td> $' + parseFloat(elementValue.ChargeAmount).toFixed(2) + '</td>';
                        html += '<td> ' + elementValue.Won + '</td>';
                        html += '<td> ' + elementValue.PaymentStatus + '</td>';
                        html += '<td> ' + elementValue.Stage + '</td>';

                        html += '<td>' + elementValue.VehicleYear + '</a></td>';
                        html += '<td> ' + elementValue.CreatedDate + '</td>';
                        html += '<td> ' + elementValue.IdentityTheftRecovery + '</td>';
                        html += '<td> ' + elementValue.AdditionalDecals + '</td>';
                        html += '<td> ' + elementValue.AdditionalDecalCount + '</td>';
                        html += '<td><span class="icon"> <i class="' + (elementValue.CancelsAndVoids == 1 ? "icon-ok" : "icon-remove") + '"></i> </span></td>';
                        html += '<td><span class="icon"> <i class="' + (elementValue.GrossDeals == 1 ? "icon-ok" : "icon-remove") + '"></i> </span></td>'; 
                        html += '<td><span class="icon"> <i class="' + (elementValue.NetDeals == 0 ? "icon-ok" : "icon-remove") + '"></i> </span></td>'; 

                        html += '<td>' + elementValue.IdentityTheft + '</td>';
                        html += '<td> ' + elementValue.TransType + '</td>';
                        html += '</tr>';
                        $("#tblPayRoll>tbody").append(html);
                    //}

                    if (rowCount == 0)
                    {

                        createdByID = elementValue.CreatedByID;
                        CreatedCOUNT = elementValue.CreatedCOUNT;
                        ADDSUM = elementValue.ADDSUM;
                        ChargeTotal = elementValue.ChargeTotal;
                        DecalCount = elementValue.DecalCount;
                        VoidCount = elementValue.VoidCount;
                        GrossCount = elementValue.GrossSUM;
                        NetCount = elementValue.NetSUM;
                        IDenCount = elementValue.IDenCount;

                        isChange = false;
                        var html = '';
                        html += '<tr>';
                        html += '<td style="background-color:yellow!important;">Sum</td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;">$' + parseFloat(ADDSUM).toFixed(2) + '</td>';
                        html += '<td style="background-color:yellow!important;">$' + parseFloat(ChargeTotal).toFixed(2) + '</td>';
                        html += '<td style="background-color:yellow!important;"> </td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"> </td>';

                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;">' + GrossCount + '</td>';
                        html += '<td style="background-color:yellow!important;">' + NetCount+ '</td>';

                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '</tr>';

                        html += '<tr>';
                        html += '<td style="background-color:yellow!important;">COUNT</td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;">' + CreatedCOUNT + '</td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"> </td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;">' + DecalCount + '</td>';
                        html += '<td style="background-color:yellow!important;">' + VoidCount + '</td>';
                        html += '<td style="background-color:yellow!important;"> </td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;">' + IDenCount + '</td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '</tr>';

                        html += '<tr>';
                        html += '<td style="background-color:yellow!important;">Total Sum</td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;">$' + parseFloat(elementValue.TranTotal).toFixed(2) + '</td>';
                        html += '<td style="background-color:yellow!important;">$' + parseFloat(elementValue.CharTotal).toFixed(2) + '</td>';
                        html += '<td style="background-color:yellow!important;"> </td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"> </td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;">' +elementValue.GRTotal + '</td>';
                        html += '<td style="background-color:yellow!important;">' + elementValue.NETotal + '</td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '</tr>';

                        html += '<tr>';
                        html += '<td style="background-color:yellow!important;">Total Count</td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;">' + elementValue.CReatedTotal + '</td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"> </td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;">' + elementValue.Dectotal + '</td>';
                        html += '<td style="background-color:yellow!important;">' + elementValue.VoiTotal + '</td>';
                        html += '<td style="background-color:yellow!important;"> </td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;">' + elementValue.IDenTotal + '</td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '</tr>';

                        $("#tblPayRoll>tbody").append(html);
                        
                    }
                   
                    
                });
                }
        }
    });             
}

var viewGraph=function(response)
{
   
    //var ctx = document.getElementById("bar-chart").getContext("2d");

    var label = response.Labels.split(',');
    var grossData = response.GrossData.split(',');
    var netData = response.NetData.split(',');
    var cvData = response.CancelVoidData.split(',');
    var addData = response.AddDecalData.split(',');
    var identityData = response.IdentityData.split(',');

    $("#lblGross").text("Gross Total: "+ response.GrossDataTotal);
    $("#lblNet").text("Net Total: " + response.NetDataTotal);
    $("#lblCancel").text("Cancel And Void Total: " + response.CancelVoidDataTotal);
    $("#lblDecal").text("Decal Total: " + response.AddDecalDataTotal);
    $("#lblIdentity").text("Identity Total: " + response.IdentityDataTotal);


    //var grossDataTotal = $("#hndGrossDataTotal").val();
    //var netDataTotal = $("#hndNetDataTotal").val();
    //var cvDataTotal = $("#hndCVDataTotal").val();
    //var addDataTotal = $("#hndADDDataTotal").val();
    //var identityDataTotal = $("#hndIDDataTotal").val();
    var ctx = document.getElementById("bar-chart").getContext("2d");

    var data = {
        labels: label,
        datasets: [
            {
                label: "Gross",
                backgroundColor: "dodgerblue",
                data: grossData
            },
            {
                label: "Net",
                backgroundColor: "green",
                data: netData
            },
            {
                label: "Cancel And Void",
                backgroundColor: "grey",
                data: cvData
            },
            {
                label: "Add Decal",
                backgroundColor: "orange",
                data: addData
            },
            {
                label: "Identity",
                backgroundColor: "yellow",
                data: identityData
            }
        ]
    };
    var myBarChart = new Chart(ctx, {
        type: 'bar',
        data: data,
        options: {
            barValueSpacing: 5,
            scales: {
                yAxes: [{
                    //          gridLines: {
                    //  display: false,
                    //},
                    ticks: {
                        min: 0,
                        stepSize: 5,

                    }
                }]
            }
        }
    });
    Chart.plugins.register({
        afterDatasetsDraw: function (chart, easing) {
            // To only draw at the end of animation, check for easing === 1
            var ctx = chart.ctx;

            chart.data.datasets.forEach(function (dataset, i) {
                var meta = chart.getDatasetMeta(i);
                if (!meta.hidden) {
                    meta.data.forEach(function (element, index) {
                        // Draw the text in black, with the specified font
                        ctx.fillStyle = 'rgb(0, 0, 0)';

                        var fontSize = 16;
                        var fontStyle = 'normal';
                        var fontFamily = 'Helvetica Neue';
                        ctx.font = Chart.helpers.fontString(fontSize, fontStyle, fontFamily);

                        // Just naively convert to string for now
                        var dataString = dataset.data[index].toString();

                        // Make sure alignment settings are correct
                        ctx.textAlign = 'center';
                        ctx.textBaseline = 'middle';

                        var padding = 5;
                        var position = element.tooltipPosition();
                        ctx.fillText(dataString, position.x, position.y - (fontSize / 2) - padding);
                    });
                }
            });
        }
    });
}


var saveToDiskExcelFile = function () {
    var fileName = $("#hndFileName").val();
    var hName="2Payroll";
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

var backToSearch=function()
{
    $("#hndFileName").val('');
    $("#divgraph").addClass("hidden");
    $("#divSearch").removeClass("hidden");
}