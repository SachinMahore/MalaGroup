var selectFieldDataSource = [
     { text: "Last ModiFied Date", value: "1" },
     { text: "Last ModiFied By ", value: "2" },
     { text: "State", value: "3" },
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


var getSelectFieldList = function () {
    $("#SelectField").empty();
    $.each(selectFieldDataSource, function (n, v) {
        $("#SelectField").append("<option value='" + v.value + "'>" + v.text + "</option>");
        $('#SelectField').val(1).trigger('chosen:updated');
    });
}

var fillCriteriaDropDownLists = function (selectedVal) {
    if (selectedVal == "4") {
        $("#ddlCriteria").empty();
        $.each(accountStatusdDataSource, function (n, v) {
            $("#ddlCriteria").append("<option value='" + v.value + "'>" + v.text + "</option>");
            $('#ddlCriteria').val(0).trigger('change');
        });
    } else if (selectedVal == "5") {
        getProductList();
    } else if (selectedVal == "2") {
        getLastModified();
    }
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
var getLastModified = function () {
    $("#ddlCriteria").empty();
    $.ajax({
        url: "/AgentClosing/GetLastModified",
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

var getState = function () {
    $("#ddlCriteria").empty();
    $.ajax({
        url: "/AgentClosing/GetState",
        type: "post",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            getLastModified();
        }
    });
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
    if (selectFieldValue == "2" || selectFieldValue == "4" || selectFieldValue == "5") {
        fillCriteriaDropDownLists(selectFieldValue);
        $("#divDDLCriteria").removeClass("hidden");
    }
    else if (selectFieldValue == "1") {
        $("#divDateRangeCriteria").removeClass("hidden");
        setSearchDate();
    } else if (selectFieldValue == "3") {
        //TEXT BOX
        $("#divTxtCriteria").removeClass("hidden");
    }
}
var addFilterCriteria = function () {
    var filterText = "";
    var filterValue = "";
    var selectFieldValue = $("#SelectField").val();

   
    if (selectFieldValue == "2" || selectFieldValue == "4" || selectFieldValue == "5") {
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
    } else if (selectFieldValue == "3") {
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
    var modiDate = "";
    var madifiedBy = "";
    var state = "";
    var accountStatus = "";
    var product = "";
   

    if ($("#divFilterCriteria").find("#divFC_1").length) {
        modiDate = $('#lblFT_1').data("value");
    }
    if ($("#divFilterCriteria").find("#divFC_2").length) {
        madifiedBy = $('#lblFT_2').data("value");
    }
    if ($("#divFilterCriteria").find("#divFC_3").length) {
        state = $('#lblFT_3').data("value");
    }
    if ($("#divFilterCriteria").find("#divFC_3").length) {
        accountStatus = $('#lblFT_3').data("value");
    }
    if ($("#divFilterCriteria").find("#divFC_3").length) {
        product = $('#lblFT_3').data("value");
    }
    
    var model = {
        LastModiFiedDate: modiDate,
        LastModiFiedBy: madifiedBy,
        State: state,
        AccountStatus: accountStatus,
        Product: product
    };
   
    
    $.ajax({
        url: '/AgentClosing/ExportReport',
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
                console.log(response.model);
                $("#divSearch").addClass("hidden");
                $("#divgraph").removeClass("hidden");
                viewGraph(response.model.agentClosingGraph);
                var lastModiFiedByID = "0";
                var count = 0;
                var isChange = false;


                var FileOpen  = "0";
                var Deal = "0";
                var ClosingPer = "0";

                var rowCount = response.model.agentClosingData.length;
                $("#tblReportPreview>tbody").empty();
                $("#hndFileName").val(response.model.ExportFileName);
                $.each(response.model.agentClosingData, function (index, elementValue) {
               
                    rowCount -= 1;
                    
                    
                    if (lastModiFiedByID != elementValue.LastModifiedByID)
                    {
                       
                        if (count == 0)
                        {
                            lastModiFiedByID = elementValue.LastModifiedByID;
                            FileOpen = elementValue.FileOpen;
                            Deal = elementValue.Deal;
                            ClosingPer = elementValue.ClosingPer;
                           
                        }
                        if (count != 0)
                        {
                            isChange = true;
                        }

                    }
                    count += 1;
                    
                    if (isChange == true) {
                        if (rowCount==0)
                        {
                            isChange = true
                        } else
                        {
                            isChange = false;
                        }
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

                        html += '<td style="background-color:yellow!important;">' + FileOpen + '</td>';
                        html += '<td style="background-color:yellow!important;">' + Deal + '</td>';
                        html += '<td style="background-color:yellow!important;">' + ClosingPer + '%</td>';

                        html += '</tr>';
                        $("#tblReportPreview>tbody").append(html);

                        lastModiFiedByID = elementValue.LastModifiedByID;
                        FileOpen = elementValue.FileOpen;
                        Deal = elementValue.Deal;
                        ClosingPer = elementValue.ClosingPer;
                    }

                    //else {
                       
                        var html = '';
                        html += '<tr>';

                        html += '<td>' + elementValue.LastModifiedBy + '</td>';
                        html += '<td> ' + elementValue.ListCode + '</td>';
                        html += '<td> ' + elementValue.FirstName + '</td>';

                        html += '<td>' + elementValue.LastName + '</td>';
                        html += '<td> ' + elementValue.VehicleYear + '</td>';
                        html += '<td> ' + elementValue.PinNumber + '</td>';

                        html += '<td> ' + elementValue.State + '</td>';
                        html += '<td> ' + elementValue.Stage + '</td>';
                        html += '<td>' + elementValue.LeadStatus + '</td>';

                        html += '<td> ' + elementValue.ConvertedDate + '</td>';
                        html += '<td> ' + elementValue.OpportunityAccount + '</td>';
                        html += '<td> $' +parseFloat(elementValue.OpportunityAmount).toFixed(2) + '</td>';

                        html += '<td> ' + elementValue.OpptCloseDate + '</td>';

                        html += '<td></td>';
                        html += '<td></td>';
                        html += '<td></td>';

                        html += '</tr>';
                        $("#tblReportPreview>tbody").append(html);
                    //}
                    
                    if (rowCount == 0) {

                        lastModiFiedByID = elementValue.LastModifiedByID;
                        FileOpen = elementValue.FileOpen;
                        Deal = elementValue.Deal;
                        ClosingPer = elementValue.ClosingPer;
                        var html = '';
                        if(isChange==true)
                        {
                            html += '<tr>';

                            html += '<td>' + elementValue.LastModifiedBy + '</td>';
                            html += '<td> ' + elementValue.ListCode + '</td>';
                            html += '<td> ' + elementValue.FirstName + '</td>';

                            html += '<td>' + elementValue.LastName + '</td>';
                            html += '<td> ' + elementValue.VehicleYear + '</td>';
                            html += '<td> ' + elementValue.PinNumber + '</td>';

                            html += '<td> ' + elementValue.State + '</td>';
                            html += '<td> ' + elementValue.Stage + '</td>';
                            html += '<td>' + elementValue.LeadStatus + '</td>';

                            html += '<td> ' + elementValue.ConvertedDate + '</td>';
                            html += '<td> ' + elementValue.OpportunityAccount + '</td>';
                            html += '<td> $' + elementValue.OpportunityAmount + '</td>';

                            html += '<td> ' + elementValue.OpptCloseDate + '</td>';

                            html += '<td></td>';
                            html += '<td></td>';
                            html += '<td></td>';

                            html += '</tr>';
                        }
                        isChange = false;


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

                        html += '<td style="background-color:yellow!important;">' + FileOpen + '</td>';
                        html += '<td style="background-color:yellow!important;">' + Deal + '</td>';
                        html += '<td style="background-color:yellow!important;">' + ClosingPer + '%</td>';

                        html += '</tr>';

                        html+='<tr>'

                        html += '<td style="background-color:yellow!important;">Grand Total</td>';
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

                        html += '<td style="background-color:yellow!important;">' + elementValue.FileOpenTotal + '</td>';
                        html += '<td style="background-color:yellow!important;">' + elementValue.DealTotal + '</td>';
                        html += '<td style="background-color:yellow!important;">' + elementValue.ClosingPerTotal + '%</td>';
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
    var FOpenCount = response.FOpenCount.split(',');
   
   

    $("#lblFileOpen").text("File Opened: " + response.FOpenTotal);
    $("#lblDeal").text("Deals: " + response.DSUMTOtal);
    $("#lblClose").text("Closing %: " + response.CPER);
    
   
    var ctx = document.getElementById("bar-chart").getContext("2d");

    var data = {
        labels: label,
        datasets: [
            {
                label: "File Opened",
                backgroundColor: "royalblue ",
                data: FOpenCount
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
                        stepSize: 50,

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
    var hName = "AGENT CLOSING";
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