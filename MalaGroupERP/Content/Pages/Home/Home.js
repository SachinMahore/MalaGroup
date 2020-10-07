$(document).ready(function () {
    getExportHistory();
    getTakeOffListReport();
    showGraph();

  
});
var getExportHistory=function()
{
    $("#divLoader").show();
    $.ajax({
        url: '/Home/GetExportHistory',
        type: "post",
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
                $("#tblExportHistory>tbody").empty();
                $.each(response, function (index, elementValue) {
                    var html = '';
                    html += '<tr  class="gradeX" data-value="' + elementValue.ID + '">';
                    html += '<td class="taskDesc"><i class="' + (elementValue.IsExported == "1" ? "icon-ok-sign" : "icon-info-sign") + '"></i> ' + elementValue.FileName + '</td>';
                    html += '<td class="taskStatus">' + elementValue.DateExported + '</td>';
                    html += '<td class="taskStatus">' + elementValue.TotalCount + '</td>';
                    html += '<td class="taskStatus">' + elementValue.DuplicateCount + '</td>';
                    html += '<td class="taskStatus"><span class="' + (elementValue.IsExported == "1" ? "done" : "in-progress") + '">' + elementValue.Status + '</span></td>';
                    html += '<td class="taskOptions"><a href="#" onclick="markViewedExportHistory(' + elementValue.ID + ')" class="tip-top ' + (elementValue.IsExported == "1" ? "" : "hidden") + '" data-original-title="Remove From List"><i class="icon-remove"></i></a></td>';
                    html += '</tr>';
                    $("#tblExportHistory>tbody").append(html);
                });
            }
           // autoRefresh();
        }
    });
}
var markViewedExportHistory=function(id)
{
    $("#divLoader").show();
    var model = {ID:id};

    $.ajax({
        url: '/Home/MarkViewedExportHistory',
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
                $.alert({
                    title: 'Alert!',
                    content: "Data Marked Viewed Successfully...",
                    type: 'blue'
                });
                getExportHistory();
            }
        }
    });
}
var getTakeOffListReport = function () {
    $("#divLoader").show();
    $.ajax({
        url: '/Home/GetTakeOffListReport',
        type: "post",
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
                $("#tblTakeOffList>tbody").empty();
                $.each(response, function (index, elementValue) {
                    var html = '';
                    html += '<tr  class="gradeX" data-value="' + elementValue.ID + '">';
                    html += '<td class="taskDesc"><i class="' + (elementValue.IsExported == "1" ? "icon-ok-sign" : "icon-info-sign") + '"></i> ' + elementValue.FileName + '</td>';
                    html += '<td class="taskStatus">' + elementValue.DateExported + '</td>';
                    html += '<td class="taskStatus"><span class="' + (elementValue.IsExported == "1" ? "done" : "in-progress") + '">' + elementValue.Status + '</span></td>';
                    html += '<td class="taskOptions"><a href="#" onclick="downloadTakeOffListReport(' + elementValue.ID + ')" class="tip-top ' + (elementValue.IsExported == "1" ? "" : "hidden") + '" data-original-title="Down Load Report"><i class="icon-download-alt"></i></a>&nbsp;&nbsp;<a href="#" onclick="deleteTakeOffListReport(' + elementValue.ID + ')" class="tip-top ' + (elementValue.IsExported == "1" ? "" : "hidden") + '" data-original-title="Remove From List"><i class="icon-remove"></i></a></td>';
                    html += '</tr>';
                    $("#tblTakeOffList>tbody").append(html);
                });
            }
           // autoRefresh();
        }
    });
}
var downloadTakeOffListReport = function (id) {
    $("#divLoader").show();
    var model = { ID: id };

    $.ajax({
        url: '/Home/GetTakeOffListReportByReportID',
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
                saveToDiskExcelFile(response);
            }
        }
    });
}
var saveToDiskExcelFile = function (fileName) {
    var hName = "Take Off List";
    var saveUrl = baseURL() + "/TempFiles/" + fileName;
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
var deleteTakeOffListReport = function (id) {
    $("#divLoader").show();
    var model = { ID: id };

    $.ajax({
        url: '/Home/DeleteTakeOffListReport',
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
                $.alert({
                    title: 'Alert!',
                    content: "Deleted Successfully...",
                    type: 'blue'
                });
                getTakeOffListReport();
            }
        }
    });
}
var showGraph = function () {
    $("#divLoader").show();
    
    $.ajax({
        url: '/Home/GetDashGraphData',
        type: "post",
        //data: JSON.stringify(model),
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

                viewGraphOrder(response.model.dashOrderGraph);
                viewGraphSales(response.model.dashSalesGraph);
                viewGraphIdDec(response.model.dashIdThDecGraph);
                viewAOSales(response.model.dashAOGraph);
            }
           // autoRefresh();
        }
    });
}
var viewGraphOrder = function (response) {
   
    //var label = response.Labels.split(',');
    var totalopen = response.TotalOpen.split(',');
    var totalconverted = response.TotalConverted.split(',');
    var totalnotconv = response.TotalNotConverted.split(',');

    $("#lblTotalOpen").html("Total Open: </br>" + response.TotalOpen);
    $("#lblConverted").html("Converted: </br>" + response.TotalConverted);
    $("#lblNotConverted").html("Not Converted: </br>" + response.TotalNotConverted);
    $("#lblTotalPercentage").html("Percentage: </br>" + response.TotalPercentage + "%");

    var ctx = document.getElementById("bar-chart").getContext("2d");

    var data = {
       // labels: label,
        datasets: [
            {
                label: "Total Open",
                backgroundColor: "dodgerblue",
                data: totalopen
            },
            {
                label: "Converted",
                backgroundColor: "green",
                data: totalconverted
            },
            {
                label: "Not Converted",
                backgroundColor: "grey",
                data: totalnotconv
            }
            
        ]
    };
    console.log(data);

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
var viewGraphSales = function (response) {

    //var label = response.Labels.split(',');
    var totalopen = response.TotalAmt.split(',');
    var totalconverted = response.TotalRefund.split(',');
    var totalnotconv = response.TotalVoided.split(',');
    var totaldeclined = response.TotalDeclined.split(',');

    $("#lblTotalAmt").html("Total Amt: </br>" + response.TotalAmt);
    $("#lblTotalRefund").html(" Refund: </br>" + response.TotalRefund);
    $("#lblTotalVoided").html(" Voided: </br>" + response.TotalVoided);
    $("#lblTotalDeclined").html(" Declined: </br>" + response.TotalDeclined);

    var ctx = document.getElementById("bar-chartSales").getContext("2d");

    var data = {
        // labels: label,
        datasets: [
            {
                label: "Total Amt",
                backgroundColor: "dodgerblue",
                data: totalopen
            },
            {
                label: "Total Refund",
                backgroundColor: "green",
                data: totalconverted
            },
            {
                label: "Total Voided",
                backgroundColor: "grey",
                data: totalnotconv
            }
            ,
            {
                label: "Total Declined",
                backgroundColor: "red",
                data: totaldeclined
            }
        ]
    };
    console.log(data);

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
                        stepSize: 1000,

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
var viewAOSales = function (response) {

    var label = response.Labels.split(',');
    var totalopen = response.ATotalOpen.split(',');
    var totalconverted = response.ATotalConverted.split(',');
    var idtft = response.IdTheft.split(',');
    var adddec = response.AddDecals.split(',');
    var renewal = response.Renewal.split(',');

    var ctx = document.getElementById("bar-chartAO").getContext("2d");

    var data = {
        labels: label,
        datasets: [
            {
                label: "Total Open",
                backgroundColor: "dodgerblue",
                data: totalopen
            },
            {
                label: "Converted",
                backgroundColor: "green",
                data: totalconverted
            },
             {
                 label: "Additional Decals",
                 backgroundColor: "grey",
                 data: adddec
             },
            {
                label: "Identity Theft",
                backgroundColor: "red",
                data: idtft
            }
            ,
            {
                label: "Renewal",
                backgroundColor: "yellow",
                data: renewal
            }
        ]
    };
    console.log(data);

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
                        stepSize: 10,

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
var viewGraphIdDec = function (response) {

    //var label = response.Labels.split(',');
    var idtft = response.IdTheft.split(',');
    var adddec = response.AddDecals.split(',');


    $("#lblIdTheft").text("Identity Theft: " + response.IdTheft);
    $("#lblAddDecal").text("Additional Decals: " + response.AddDecals);


    var ctx = document.getElementById("bar-chartIdDec").getContext("2d");

    var data = {
        // labels: label,
        datasets: [
            {
                label: "Additional Decals",
                backgroundColor: "grey",
                data: adddec
            },
            {
                label: "Identity Theft",
                backgroundColor: "red",
                data: idtft
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

function autoRefresh()
{
    setTimeout(function () {
        getExportHistory();
        getTakeOffListReport();
        showGraph();

    }, 60000);
}