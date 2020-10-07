var fixTransaction = function () {
    var checkcd = $("#txtChargeDate").val();
    var approved = $("#chkApproved").is(":checked") ? "1" : "0";
    var decline = $("#chkDecline").is(":checked") ? "1" : "0";
    var currentDate = new Date();
    checkcd = new Date(checkcd);
    if (checkcd > currentDate) {
        $.alert({
            title: 'Error!',
            content: "Charge date is not greater than the current date.",
            type: 'red'
        });
        return;
    }

    var chargedate = $("#txtChargeDate").val();


    var model = { ChargeDate: chargedate, SendApproved: approved, SendDecline: decline };
    $.confirm({
        title: 'Alert!',
        content: 'Are you sure you want to fix the authorize transaction?',
        type: 'blue',
        buttons: {
            confirm: function () {
                $("#divLoader").show();
                $.ajax({
                    url: "/FixAuthorizeTransaction/FixTransaction",
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
                                content: 'Data Fixed Sucessfully.',
                                type: 'blue'
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
