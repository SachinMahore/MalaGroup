
$(document).ready(function () {
    
    getCardScheduleDatewise();
    $("#ddlPaymentStatus").on('change', function (evt, params) {
        var selected = $(this).val();
        if (selected != null) {
            if(selected!=1)
            {
                $("#btnChargePendingSchedule").removeAttr("disabled");
            }
            else
            {
                $("#btnChargePendingSchedule").attr("disabled", "disabled");
            }
        }
    });
});

var chargeCardScheduleDatewise = function () {
    var checksd= $("#txtDCChargeDate").val();
    var currentDate = new Date();
    checksd = new Date(checksd);

    if (checksd > currentDate) {
        $.alert({
            title: 'Error!',
            content: "Schedule date is not greater than the current date.",
            type: 'red'
        });
        return;
    }

    var scheduledate = $("#txtDCChargeDate").val().replace(/\//g, "-");
    var model = { ScheduleDate: scheduledate };
    $.confirm({
        title: 'Alert!',
        content: 'Are you sure you want to charge the schedule?',
        type: 'blue',
        buttons: {
            confirm: function () {
                $("#divLoader").show();
                $.ajax({
                    url: "/MGAPI/ChargeScheduleCardsWithDate",
                    type: "post",
                    data: JSON.stringify(model),
                    contentType: "application/json; charset=utf-8", // content type sent to server
                    dataType: "json", //Expected data format from server
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
                                content: 'Charged Sucessfully.',
                                type: 'blue',
                                buttons: {
                                    Ok: function () {
                                        getCardScheduleDatewise();
                                    }
                                }
                            });
                        }
                    }
                });
            },
            cancel: function () {
                return;

            }
        }
    });
}

var getCardScheduleDatewise = function () {
    $("#divLoader").show();

    var model = {
        ChargeDate: $("#txtDCChargeDate").val(),
        PaymentStatus: $("#ddlPaymentStatus").val()
    };
    $.ajax({
        url: '/CardSchedule/GetCardScheduleDatewise',
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
                $("#tblCreditCardSchedule>tbody").empty();
                $("#hndFileName").val(response.Filename)
                $.each(response.Model, function (index, elementValue) {
                    var html = '';
                    html += '<tr  class="">';
                    //html += '<td> <input type="checkbox" class="selectedcard" value="' + elementValue.ID + '" /></td>';
                    html += '<td class="scd-td"> ' + elementValue.OpportunityID + '</td>';
                    html += '<td class="scd-td"> ' + elementValue.PinNumber + '</td>';
                    html += '<td class="scd-td"> ' + elementValue.BillingFirstName + ' ' + elementValue.BillingLastName + '</td>';
                    html += '<td class="scd-td"> ' + elementValue.BillingAddress + '</td>';
                    html += '<td class="scd-td">' + elementValue.BillingCity + ' </td>';
                    html += '<td class="scd-td"> ' + elementValue.BillingState + '</td>';
                    html += '<td class="scd-td"> ' + elementValue.BillingZipPostal + '</td>';
                    html += '<td class="scd-td"> ' + elementValue.PaymentStatus + '</td>';
                    html += '<td class="scd-td">' + elementValue.InvoiceNumber + ' </td>';
                    html += '<td class="scd-td"> ' + elementValue.CardNumber + '</td>';
                    html += '<td class="scd-td"> ' + elementValue.CardExpirationMonth + '/' + elementValue.CardExpirationYear + '</td>';
                    html += '<td class="scd-td">' + parseFloat(elementValue.ChargeAmount).toFixed(2) + ' </td>';
                    //html += '<td class="scd-td">' + elementValue.ChargeDate + ' </td>';
                    html += '</tr>';
                    $("#tblCreditCardSchedule>tbody").append(html);
                });
            }
        }
    });
}

var ExportScheduleReport = function () {
    $("#divLoader").show();

    var model = {
        ChargeDate: $("#txtDCChargeDate").val(),
        PaymentStatus: $("#ddlPaymentStatus").val()
    };
    $.ajax({
        url: '/CardSchedule/ExportScheduleReport',
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
                saveToDiskExcelFile();
            }
        }
    });
}
var saveToDiskExcelFile = function () {
    console.log(baseURL());
    var fileName = $("#hndFileName").val();
    var saveUrl = baseURL() + "/TempFiles/" + fileName;
    //saveUrl = encodeURIComponent(saveUrl);
    var hyperlink = document.createElement('a');
    hyperlink.href = saveUrl;
    hyperlink.target = '_blank';
    hyperlink.download = fileName + ".xlsx";

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