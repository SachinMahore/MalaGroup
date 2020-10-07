var selectFieldDataSource = [
     { text: "Close Date", value: "1" },
     { text: "Account Status", value: "2" },
     { text: "Product", value: "3" },
     { text: "Charge Amount Greater Than", value: "4" },
     { text: "Charge Amount Less Than", value: "5" },
     { text: "Charge Amount Not Equal", value: "6" }
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
        url: "/NewAgentReportDailyDeal/GetProductList",
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
    } else if (selectedVal == "3") {
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
    if (selectFieldValue == "2" || selectFieldValue == "3") {
        fillCriteriaDropDownLists(selectFieldValue);
        $("#divDDLCriteria").removeClass("hidden");
    }
    else if (selectFieldValue == "1") {
        $("#divDateRangeCriteria").removeClass("hidden");
        setSearchDate();
    } else if (selectFieldValue == "4" || selectFieldValue == "5" || selectFieldValue == "6") {
        //TEXT BOX
        $("#divTxtCriteria").removeClass("hidden");
    }
}
var addFilterCriteria = function () {
    var filterText = "";
    var filterValue = "";
    var selectFieldValue = $("#SelectField").val();

   
    if (selectFieldValue == "2" || selectFieldValue == "3" ) {
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
    
    else if (selectFieldValue == "1") {
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
    } else if (selectFieldValue == "4" || selectFieldValue == "5" || selectFieldValue == "6") {
        //TEXT BOX
        filterText = $("#Criteria").val();
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


var exportReport = function () {
    $("#divLoader").show();
    var closeDate = "";
    var accountStatus = "";
    var product = "";
    var greaterAmt = "";
    var lessAMT = "";
    var notEqualAMT = "";

    if ($("#divFilterCriteria").find("#divFC_1").length) {
        closeDate = $('#lblFT_1').data("value");
    }
    if ($("#divFilterCriteria").find("#divFC_2").length) {
        accountStatus = $('#lblFT_2').data("value");
    }
    if ($("#divFilterCriteria").find("#divFC_3").length) {
        product = $('#lblFT_3').data("value");
    }
    if ($("#divFilterCriteria").find("#divFC_4").length) {
        greaterAmt = $('#lblFT_2').data("value");
    }
    if ($("#divFilterCriteria").find("#divFC_5").length) {
        lessAMT = $('#lblFT_3').data("value");
    }
    if ($("#divFilterCriteria").find("#divFC_6").length) {
        notEqualAMT = $('#lblFT_4').data("value");
    }
    if (!closeDate) {
        $("#divLoader").hide();
        $.alert({
            title: 'Alert!',
            content: "Please select Close Date",
            type: 'red'
        });
        return;
    }
    var model = {
        CloseDate: closeDate,
        AccountStatus: accountStatus,
        Product: product,
        GreaterAmt: greaterAmt,
        LessAMT: lessAMT,
        NotEqualAMT: notEqualAMT
    };
   
    
    $.ajax({
        url: '/NewAgentReportDailyDeal/ExportReport',
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
                viewGraph(response.model.agentDailyGraph);
                var createdByID = "0";
                var count = 0;
                var isChange = false;

                var CreatedCOUNT = "0";
                var IDTHEFTSUM = "0";
                var AdditionalDECALSUM = "0";
                var TotalDECALSUM = "0";
                var AdditionalDECALPer = "0";
                var IDTHEFTPer = "0";
                var RenewalCount = "0";

                var rowCount = response.model.agentDailyData.length;
                $("#tblReportPreview>tbody").empty();
                $("#hndFileName").val(response.model.ExportFileName);
                $.each(response.model.agentDailyData, function (index, elementValue) {
                    rowCount -= 1;
                    if (createdByID != elementValue.CreadtedByID)
                    {
                        if (count == 0)
                        {
                            createdByID = elementValue.CreadtedByID;
                            IDTHEFTSUM = elementValue.IDTHEFTSUM;
                            AdditionalDECALSUM = elementValue.AdditionalDECALSUM;
                            TotalDECALSUM = elementValue.TotalDECALSUM;
                            AdditionalDECALPer = elementValue.AdditionalDECALPer;
                            IDTHEFTPer = elementValue.IDTHEFTPer;

                            RenewalCount = elementValue.RenewalCount;
                            
                        }
                        if (count != 0)
                        {
                            isChange = true;
                        }

                    }
                    count += 1;

                    if (isChange == true) {
                        isChange = false;
                        var html = '';
                        html += '<tr>';

                        html += '<td style="background-color:yellow!important;">Sub Total</td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';

                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';

                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';

                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';

                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';

                        html += '<td style="background-color:yellow!important;">' + IDTHEFTSUM + '</td>';
                        html += '<td style="background-color:yellow!important;">' + AdditionalDECALSUM + '</td>';
                        html += '<td style="background-color:yellow!important;">' + TotalDECALSUM + '</td>';
                        html += '<td style="background-color:yellow!important;">' + AdditionalDECALPer + '%</td>';
                        html += '<td style="background-color:yellow!important;">' + IDTHEFTPer + '%</td>';
                        html += '<td style="background-color:yellow!important;">' + RenewalCount + '</td>';
                        html += '</tr>';
                        $("#tblReportPreview>tbody").append(html);

                        createdByID = elementValue.CreadtedByID;
                        IDTHEFTSUM = elementValue.IDTHEFTSUM;
                        AdditionalDECALSUM = elementValue.AdditionalDECALSUM;
                        TotalDECALSUM = elementValue.TotalDECALSUM;
                        AdditionalDECALPer = elementValue.AdditionalDECALPer;
                        IDTHEFTPer = elementValue.IDTHEFTPer;

                        RenewalCount = elementValue.RenewalCount;
                        
                    }

                    //else {
                       
                        var html = '';
                        html += '<tr>';

                        html += '<td>' + elementValue.CreatedBy + '</td>';
                        html += '<td> ' + elementValue.PaymentCount + '</td>';
                        html += '<td> ' + elementValue.Product + '</td>';

                        html += '<td>' + elementValue.ListCode + '</td>';
                        html += '<td> ' + elementValue.AccountName + '</td>';
                        html += '<td> ' + elementValue.GatewayDate + '</td>';

                        html += '<td> ' + elementValue.PersonAccountMailingState + '</td>';
                        html += '<td> ' + elementValue.VehicleYear + '</td>';
                        html += '<td>$' +parseFloat(elementValue.ChargeAmount).toFixed(2) + '</td>';

                        html += '<td> ' + elementValue.NumberofDecals + '</td>';
                        html += '<td> ' + elementValue.AdditionalDecalCount + '</td>';
                        html += '<td> ' + elementValue.TotalDecalCount + '</td>';
                        html += '<td><span class="icon"> <i class="' + (elementValue.IdentityTheftRecovery == 1 ? "icon-ok" : "icon-remove") + '"></i> </span></td>';
                        html += '<td><span class="icon"> <i class="' + (elementValue.AdditionalDecals == 1 ? "icon-ok" : "icon-remove") + '"></i> </span></td>';

                        html += '<td>' + elementValue.IdentityTheftRecovery + '</td>';
                        html += '<td>' + elementValue.AdditionalDecals + '</td>';
                        html += '<td></td>';
                        html += '<td></td>';
                        html += '<td></td>';

                        html += '<td><span class="icon"> <i class="' + (elementValue.IsRenewal == 1 ? "icon-ok" : "") + '"></i> </span></td>';
                        html += '</tr>';
                        $("#tblReportPreview>tbody").append(html);
                    //}

                    if (rowCount == 0) {

                        createdByID = elementValue.CreadtedByID;
                        IDTHEFTSUM = elementValue.IDTHEFTSUM;
                        AdditionalDECALSUM = elementValue.AdditionalDECALSUM;
                        TotalDECALSUM = elementValue.TotalDECALSUM;
                        AdditionalDECALPer = elementValue.AdditionalDECALPer;
                        IDTHEFTPer = elementValue.IDTHEFTPer;

                        RenewalCount = elementValue.RenewalCount;
                       

                        isChange = false;
                        var html = '';
                        html += '<tr>';

                        html += '<td style="background-color:yellow!important;">Sub Total</td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';

                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';

                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';

                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';

                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';

                        html += '<td style="background-color:yellow!important;">' + IDTHEFTSUM + '</td>';
                        html += '<td style="background-color:yellow!important;">' + AdditionalDECALSUM + '</td>';
                        html += '<td style="background-color:yellow!important;">' + TotalDECALSUM + '</td>';
                        html += '<td style="background-color:yellow!important;">' + AdditionalDECALPer + '%</td>';
                        html += '<td style="background-color:yellow!important;">' + IDTHEFTPer + '%</td>';
                        html += '<td style="background-color:yellow!important;">' + RenewalCount + '</td>';
                        html += '</tr>';

                        html += '<tr>';
                        html += '<td style="background-color:yellow!important;">Grand Total</td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';

                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"> </td>';

                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"> </td>';
                        html += '<td style="background-color:yellow!important;"></td>';

                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';

                        html += '<td style="background-color:yellow!important;"></td>';
                        html += '<td style="background-color:yellow!important;"></td>';


                        html += '<td style="background-color:yellow!important;">' + elementValue.GRANDIDTHEFTSUM + '</td>';
                        html += '<td style="background-color:yellow!important;">' + elementValue.GRANDAdditionalDECALSUM + '</td>';
                        html += '<td style="background-color:yellow!important;">' + elementValue.GRANDTotalDECALSUM + '</td>';
                        html += '<td style="background-color:yellow!important;">' + elementValue.GRANDAdditionalPER + '%</td>';
                        html += '<td style="background-color:yellow!important;">' + elementValue.GRANDIDTHEFTPer + '%</td>';
                        html += '<td style="background-color:yellow!important;">' + elementValue.RenewalTotal + '</td>';
                        html += '</tr>';

                        $("#tblReportPreview>tbody").append(html);

                    }
                });
            }
        }
    });             
}

var viewGraph=function(response)
{
    var label = response.Labels.split(',');
    var toData = response.TotalDecalCount.split(',');
    var addData = response.AddDecalCount.split(',');
    var thData = response.IdTheftCount.split(',');
    var thRenewal = response.RenewalCount.split(',');
   

    $("#lblTotalDecal").text("Number of Decal: " + response.TotalDecalCountTotal);
    $("#lblAddDecal").text("Total Add Decal Count: " + response.AddDecalCountTotal);
    $("#lblIdTheft").text("Total ID Theft Count: " + response.IdTheftCountTotal);
    $("#lblRenwal").text("Total Renewal: " + response.RenewalTotal);
    
    
   
    var ctx = document.getElementById("bar-chart").getContext("2d");

    var data = {
        labels: label,
        datasets: [
            {
                label: "Number of Decal Total",
                backgroundColor: "turquoise",
                data: toData
            },
            {
                label: "Add Decal Count",
                backgroundColor: "orange",
                data: addData
            },
            {
                label: "ID Theft Count",
                backgroundColor: "royalblue ",
                data: thData
            },
             {
                 label: "Renewal Count",
                 backgroundColor: "green ",
                 data: thRenewal
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
    var hName = "New Agent ReportDaily Deals";
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