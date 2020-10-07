
$(document).ready(function () {
    $("#popUploadScheduleFile").PopupWindow({
        title: "Upload File",
        modal: true,
        autoOpen: false,
        top: 250,
        left: 600,
        height: 250,
        width: 400
    });
    $("#btnImportCardSchedule").on("click", function (event) {
        $("#popUploadScheduleFile").PopupWindow("open");
    });
    getCaredScheduleDetails();
});
var importCardSchedule = function () {
    $("#popUploadScheduleFile").PopupWindow("close");
    $("#divLoader").show();
    $formData = new FormData();
    var $file = document.getElementById('openFileImportList');
    if ($file.files.length > 0) {
        for (var i = 0; i < $file.files.length; i++) {
            $formData.append('file-' + i, $file.files[i]);
        }
    }
    $.ajax({
        url: "/CardSchedule/ImportCardSchedule",
        type: 'POST',
        data: $formData,
        dataType: 'json',
        contentType: false,
        processData: false,
        success: function (response) {
            $("#divLoader").hide();
            if ($.trim(response.error) != "") {
                $.alert({
                    title: 'Alert!',
                    content: response.error,
                    type: 'red'
                });
            } else {
                getCaredScheduleDetails();
                $.alert({
                    title: 'Alert!',
                    content: "Card Data Imported Successfully.",
                    type: 'blue'
                });

            }
        }
    });
}
var getCaredScheduleDetails = function () {
    $("#divLoader").show();

    var model = {
        ChargeDate: $("#txtSCChargeDate").val()
    };
    $.ajax({
        url: '/CardSchedule/GetCaredScheduleDetails',
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
                $.each(response, function (index, elementValue) {
                    var html = '';
                    html += '<tr  class="gradeX">';
                    html += '<td> <input type="checkbox" class="selectedcard" value="' + elementValue.ID + '" /></td>';
                    html += '<td class="sc-td"> ' + elementValue.OpportunityID + '</td>';
                    html += '<td class="sc-td"> ' + elementValue.PinNumber + '</td>';
                    html += '<td class="sc-td"> ' + elementValue.BillingFirstName + ' ' + elementValue.BillingLastName + '</td>';
                    html += '<td class="sc-td"> ' + elementValue.PaymentStatus + '</td>';
                    html += '<td class="sc-td">' + elementValue.InvoiceNumber + ' </td>';
                    html += '<td class="sc-td"> ' + elementValue.CardNumber + '</td>';
                    html += '<td class="sc-td"> ' + elementValue.CardExpirationMonth + '/' + elementValue.CardExpirationYear + '</td>';
                    html += '<td class="sc-td">' + elementValue.ChargeAmount + ' </td>';
                    html += '<td class="sc-td">' + elementValue.ChargeDate + ' </td>';
                    html += '</tr>';
                    $("#tblCreditCardSchedule>tbody").append(html);
                });
            }
        }
    });
}
var chargeCreditCards = function () {
    $("#divLoader").show();
    var cardselected = "";
    $('.selectedcard').each(function (i, obj) {
        if ($(obj).is(':checked')) {
           
            if (!cardselected)
            {
                cardselected = $(obj).attr("value").toString();
            }
            else
            {
                cardselected += "," + $(obj).attr("value").toString();
            }
        }
    });
    if (!cardselected)
    {
        $("#divLoader").hide();
        $.alert({
            title: 'Alert!',
            content: "Please select card(s).",
            type: 'red'
        });
        return;
    }
    var model = {
        CardIDs: cardselected
    };
    $.ajax({
        url: '/CardSchedule/ChargeCreditCards',
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
                    content: response.result + "<br/>Selected Card Charged Successfully",
                    type: 'blue'
                });
                getCaredScheduleDetails();
            }
        }
    });
}