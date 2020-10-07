var totalAmt = 0;
var packageId = 0;
$(document).ready(function () {
    $(":input").attr("autocomplete", "off");
    $("#txtPin").focus();
    validateNumber();

    //(result.card_type != null ? console.log(result.card_type.name) : "");
    //(result.card_type != null ? console.log(result.card_type.type_of_card) : "");
    //(result.valid?console.log(result.valid):"");

    getCallHistoryList(1);

  
    $("#lblStepTitle").text("Pin Verification");

    clearOrder();

    getVehicleMakeList();
    getPackageList();

    $("#txtPin").blur(function () {
        $("#next").focus();
    });

});
var newOrderAcct = function (pager) {
    if (pager == 1) {
        window.location = '/AgentOrder/AddEdit/';
    } else {
        window.location = '/AccountPage/Edit/' + $("#hndAccountID").val();
    }
}

$(document).on('keydown', function (event) {
    if (event.which == 37) {
        window.open('/AccountPage/Index/')
    } else if (event.which == 39) {
        window.open('/Leads/Index/')
    }
});
function clearOrder() {
    $("#hndAOID").val(0);
    $("#hndLeadID").val(0);
    $("#txtPin").val("");

    $("#hndPackage").val(0);

}
var goToAccount = function () {
    window.open('/AccountPage/Edit/' + $("#hndAccountID").val(), '_blank')
}
var getVehicleMakeList = function () {
    $("#ddlVehicleMake").empty();
  
    $.ajax({
        url: "/Leads/GetVehicleMakeList",
        type: "post",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            $("#ddlVehicleMake").append("<option value='0'>Select Vehicle Make...</option>");
          
            $.each(response.model, function (index, elementValue) {
                var opt = "<option value='" + elementValue.Value + "'>" + elementValue.Text + "</option>";
                $("#ddlVehicleMake").append(opt);
              
            });
            $("#ddlVehicleMake").val(0).trigger('change');
           
        }
    });
}

var getPackageList = function () {
    $("#tblRadioLists").empty();
    $.ajax({
        url: "/Package/GetPackageList",
        type: "post",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            var rows = "";
            $.each(response.model, function (index, elementValue) {
                if (elementValue.IsRenewal == "1") {
                    rows += "<label><input  type='checkbox' class='rdo mainpackage' name='radios' onclick='selectPackage(this," + elementValue.PackageID + "," + elementValue.Price + "," + elementValue.NoOfInstallment + ",\"" + elementValue.Package + " ($" + parseFloat(elementValue.Price).toFixed(2) + ")\");' id='chkPackage" + elementValue.PackageID + "' value=" + elementValue.PackageID + " />" + elementValue.Package + " ($" + elementValue.Price + ")</label>";
                }
                });
            $("#tblRadioLists").append(rows);
        }
    });
}
function selectPackage(cont, checkedRadio, pprice, installment, packagen) {
    $('.mainpackage').not($(cont)).prop('checked', false);
    $.uniform.update(".mainpackage");
    if (checkedRadio && $(cont).is(":checked")) {      
        totalAmt = pprice;
        packageId = checkedRadio;
    } else {
        totalAmt = 0;
        packageId = "";
    }
}
var getVehicleTypeList = function (vehicleMake) {
    $("#ddlVehicleType").empty();
    var param = { VehcileMake: vehicleMake }
    $.ajax({
        url: "/Leads/GetVehicleTypeList",
        type: "post",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            $("#ddlVehicleType").append("<option value='0'>Select Vehicle Type...</option>");
            $.each(response.model, function (index, elementValue) {
                var opt = "<option value='" + elementValue.Value + "'>" + elementValue.Text + "</option>";
                $("#ddlVehicleType").append(opt);
            });
            $("#ddlVehicleType").val(0).trigger('change');
        }
    });
}

function nextTab(step) {
    var msg = "";
    if (step == 1) {
     
        var pin = $("#txtPin").val();
        if (pin == "") {
            msg += " Please enter Pin.<br />";
            $('#form-wizard-1').show();
        }
        else {
            $('#form-wizard-1').hide();
        }

        if (msg != "") {
            $.alert({
                title: 'Alert!',
                content: msg,
                type: 'red'
            });
            return;
        }

        var param = { PinNo: pin };
        $.ajax({
            url: '/RenewalOrder/Edit',
            type: "post",
            data: JSON.stringify(param),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (response) {
                if (response.leadData.LeadID > 0) {
                    $('#form-wizard-2').show();
                        getCallHistoryList(response.leadData.LeadID, 1);

                        
                        getVehicleMakeList();
                      
                        $('#step2').removeAttr("disabled");

                        if (response.leadData.AccountID != "0") {

                            $("#hndAccountID").val(response.leadData.AccountID);
                            $('#divAccount').show();
                            $("#lblAccID").text("AccountID: " + response.leadData.AccountID);
                            $("#lblAccDate").text("Created Date: " + response.leadData.TransDate);
                            $("#lblAccBy").text("Created By: " + response.leadData.CreatedBy);
                            $("#lblTransAmt").text("Transaction Amt: $" + response.leadData.TransAmt);
                        }
                        if (response.leadData.CallHistory.length > 1) {
                            $('#divCallHistory').show();
                            $("#lblCallHistory").text("AccountID: " + response.leadData.CallHistory);
                        }
                       

                        $("#hndAOID").val(response.leadData.OrderID);
                        $("#hndLeadID").val(response.leadData.LeadID);
                        $("#txtPhone1").val(response.leadData.PrimaryPhone);
                        $("#txtPhone2").val(response.leadData.SecondaryPhone);
                        $("#txtFirstName").val(response.leadData.FirstName);
                        $("#txtLastName").val(response.leadData.LastName);
                        $("#txtEmail").val(response.leadData.LeadEmail);
                        $("#txtCity").val(response.leadData.City);
                        $("#txtState").val(response.leadData.State);
                        $("#txtZip").val(response.leadData.Zip);
                        $("#txtStreet").val(response.leadData.Street);

                        $("#ddlVehicleMake").val(response.leadData.VehicleMake).trigger('change');
                        $("#ddlVehicleType").val(response.leadData.VehicleType).trigger('change');
                        $("#txtVehicleYear").val(response.leadData.VehicleYear);
                      

                       
                       
                        $("#lblCustName").text(response.leadData.FirstName + " " + response.leadData.LastName);
                        $("#lblCustName1").text(response.leadData.LastName);
                    
                        $("#lblName").text("Name: " + response.leadData.FirstName + " " + response.leadData.LastName);
                        $("#lblpin").text("Pin No.: " + response.leadData.PinNo);
                        $("#lblDealer").text("Dealer: " + response.leadData.Dealership);
                        $("#lblpin8").text("Pin No.: " + response.leadData.PinNo);
                        $("#lblDealer8").text("Dealer: " + response.leadData.Dealership);
                        $("#lblAddress").text("Address: " + response.leadData.Street + ', ' + response.leadData.City + ', ' + response.leadData.State + ', ' + response.leadData.Zip);
                        $("#lblExpDate").text("Expiration Date: " + response.leadData.ExpirationDate);
                        $("#lblListCode").text("List Code: " + response.leadData.ListCode);
                        $("#lblName8").text("Name: " + response.leadData.FirstName + " " + response.leadData.LastName);
                        $("#lblAddress8").text("Address: " + response.leadData.Street + ', ' + response.leadData.City + ', ' + response.leadData.State + ', ' + response.leadData.Zip);
                        $("#lblExpDate8").text("Expiration Date: " + response.leadData.ExpirationDate);
                        $("#lblListCode8").text("List Code: " + response.leadData.ListCode);

                        $("#lblPhone1").text("Primary Phone: " + $("#txtPhone1").val());
                        $("#lblPhone2").text("Secondary Phone: " + $("#txtPhone2").val());
                        $("#lblPhone18").text("Primary Phone: " + $("#txtPhone1").val());
                        $("#lblPhone28").text("Secondary Phone: " + $("#txtPhone2").val());

                        setVehicle(response.leadData.VehicleMake, response.leadData.VehicleType);
                        if (response.leadData.Package) {
                            $("#lblPackage").text(response.leadData.Package + "($" + parseFloat(response.leadData.Price).toFixed(2) + ")");
                            $("#lblPackage8").text(response.leadData.Package + "($" + parseFloat(response.leadData.Price).toFixed(2) + ")");
                        }
                      
                      
                        $('#div1').removeClass("hidden");

                        //Transaction
                        $("#ddlPaymentMethod").val(response.leadData.PaymentMethod).trigger('change');
                        $("#txtCheckNumber").val(response.leadData.CheckNumber);
                        $("#ddlCompType").val(response.leadData.CompType).trigger('change');
                        $("#ddlCardType").val(response.leadData.CardType).trigger('change');
                        if (response.leadData.CardNumber) {
                            var mainStr = response.leadData.CardNumber,
                              vis = mainStr.slice(-4),
                               countNum = '';

                            for (var i = (mainStr.length) - 4; i > 0; i--) {
                                countNum += '*';
                            }

                            $("#txtCardNumber").val(response.leadData.CardNumber);
                            validateNumber();
                            $("#txtCardNumber").val(countNum + vis);
                        }
                        // $("#txtCardNumber").val(response.leadData.CardNumber);

                        $("#hndCardNumber").val(response.leadData.CardNumber);

                        $("#ddlMonth").val(response.leadData.CardExpirationMonth).trigger('change');
                        $("#ddlYear").val(response.leadData.CardExpirationYear).trigger('change');
                        $("#txtCardSecurityCode").val(response.leadData.CardSecurityCode);
                        $("#txtVehicleYear").focus();
                }
                else {
                    $('#form-wizard-1').show();
                    $('#form-wizard-2').hide();
                    $.alert({
                        title: 'Alert!',
                        content: "Pin is not verified for Lead",
                        type: 'red'
                    });
                }

            }
        })

    }
    if (step == 2) {
        var msg = "";
       
        if (packageId == "")
        {
            msg += " Please Mark Renewal Package.<br />";
        }
        var primaryPhone = $("#txtPhone1").val();
        if (!primaryPhone) {
            msg += " Please enter Primary Phone No.<br />";
        }
        if (!$('#txtPhone1').val().match('[0-9]{10}')) {
            msg += "Please put 10 digit Primary mobile number.<br />";

        }
      
       
        var vmake = $("#ddlVehicleMake").val();
        if (vmake == 0) {
            msg += " Please select Vehicle Make <br />";
        }
        var vtype = $("#ddlVehicleType").val();

        var vyear = $("#txtVehicleYear").val();
        if (!vyear) {
            msg += " Please select Vehicle Year <br />";
        }
        if (msg != "") {
            $.alert({
                title: 'Alert!',
                content: msg,
                type: 'red'
            });
            return;
        }
        else {
            $('#form-wizard-1').hide();
            $('#form-wizard-2').hide();
            $('#form-wizard-3').show();
            $("#lblPhone1").text("Primary Phone: " + $("#txtPhone1").val());
            $("#lblPhone2").text("Secondary Phone: " + $("#txtPhone2").val());
           

            setVehicle($("#ddlVehicleMake").val(), $("#ddlVehicleType").val());
            $('#div2').removeClass("hidden");
          
            $('#step3').removeAttr("disabled");
            $("#lblStepTitle").text("Package Selection");
            $("#lblTotalAmt").val("$49");
        }

    }
    if (step == 3) {
      
        var msg = "";

        if ($("#ddlPaymentMethod").val() == 1) {
            var ccnumber = $("#txtCardNumber").val();
            if (ccnumber.toString().indexOf('*') == -1) {
                $("#hndCardNumber").val(ccnumber);
            }

            if (!ccnumber) {
                msg += " Please enter Card No.<br />";
            }

            var cmonth = $("#ddlMonth").val();
            if (cmonth == 0) {
                msg += " Please select Month <br />";
            }
            var cyr = $("#ddlYear").val();
            if (cyr == 0) {
                msg += " Please select Year <br />";
            }
            //var vtype = $("#ddlCardType").val();
            //if (vtype == 0) {
            //    msg += " Please select Card Type <br />";
            //}
            var vyear = $("#txtCardSecurityCode").val();
            if (!vyear) {
                msg += " Please Enter Card Security Code <br />";
            }
        } else if ($("#ddlPaymentMethod").val() == 2) {
            var checknumber = $("#txtCheckNumber").val();
            if (!checknumber) {
                msg += " Please enter Check No.<br />";
            }
        } else if ($("#ddlPaymentMethod").val() == 3) {
            var comptype = $("#ddlCompType").val();
            if (comptype == 0) {
                msg += " Please select COMP Type <br />";
            }
        }

        if (msg != "") {
            $.alert({
                title: 'Alert!',
                content: msg,
                type: 'red'
            });
            return;
        }
        else {
            $('#form-wizard-1').hide();
            $('#form-wizard-2').hide();
            $('#form-wizard-3').hide();
            $('#form-wizard-4').show();
            
          
            $('#step3').removeAttr("disabled");
            $('#step4').removeAttr("disabled");
            $('#step5').removeAttr("disabled");
          


            $("#lblPhone1").text("Primary Phone: " + $("#txtPhone1").val());
           
            $('#div2').removeClass("hidden");
         
            $("#lblEmail").text("Email: " + $("#txtEmail").val());
            $('#divEmail').removeClass("hidden");
            $("#lblStepTitle").text("Additional Vehicle / Decals Information");
            $("#divSurvey").addClass("hidden");
            $("#divClosingScript").addClass("hidden");
            $("#divMessage").addClass("hidden");
            //updateLead(4);
            //saveTransaction();

        }
    }

   

}

function backTab(step) {
    var msg = "";
    if (step == 1) {
        $('#form-wizard-1').show();
    }
    if (step == 2) {
        $("#lblStepTitle").text("Pin Verification");
        $('#form-wizard-1').show();
        $('#form-wizard-2').hide();
        $('#form-wizard-3').hide();
        $('#form-wizard-4').hide();
        $('#form-wizard-5').hide();
      
    }
    if (step == 3) {
        $("#lblStepTitle").text("Vehicle Information");
        $('#form-wizard-1').hide();
        $('#form-wizard-2').show();
        $('#form-wizard-3').hide();
        $('#form-wizard-4').hide();
        $('#form-wizard-5').hide();
      
    }
    if (step == 4) {
        $("#lblStepTitle").text("Package Selection");
        $('#form-wizard-1').hide();
        $('#form-wizard-2').hide();
        $('#form-wizard-3').show();
        $('#form-wizard-4').hide();
        $('#form-wizard-5').hide();
     
    }

    if (step == 5) {
        $("#lblStepTitle").text("Transaction Information");
        $('#form-wizard-1').hide();
        $('#form-wizard-2').hide();
        $('#form-wizard-3').hide();
        $('#form-wizard-4').hide();
        $('#form-wizard-5').hide();
        $('#form-wizard-6').show();
        $('#form-wizard-7').hide();
        $('#form-wizard-8').hide();


        var ccnumber = $("#txtCardNumber").val();
        if (ccnumber.toString().indexOf('*') != -1) {
            $("#txtCardNumber").val("");
            $("#txtCardNumber").val($("#hndCardNumber").val());
            validateNumber();
            var mainStr = $("#hndCardNumber").val(),
                                  vis = mainStr.slice(-4),
                                   countNum = '';
            for (var i = (mainStr.length) - 4; i > 0; i--) {
                countNum += '*';
            }
            $("#txtCardNumber").val(countNum + vis);
        }
    }
   

}

function setVehicle(vmake, vtype) {
    var param = { VehicleMake: vmake, VehicleType: vtype };
    $.ajax({
        url: "/AgentOrder/GetVehicleLeadInfo",
        type: "post",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",

        success: function (response) {
            if (response.vehicleData != "") {
                $("#lblVehicleDet").text("Vehicle Make/Type/Year: " + response.vehicleData.VehicleMakeText + " / " + response.vehicleData.VehicleTypeText + " / " + $("#txtVehicleYear").val());
                $("#lblVehicleDet8").text("Vehicle Make/Type/Year: " + response.vehicleData.VehicleMakeText + " / " + response.vehicleData.VehicleTypeText + " / " + $("#txtVehicleYear").val());

                $("#lblVMake1").text(response.vehicleData.VehicleMakeText);
                $("#lblVMake2").text(response.vehicleData.VehicleMakeText);
                $("#lblVMake3").text(response.vehicleData.VehicleMakeText);
                $("#lblVMake4").text(response.vehicleData.VehicleMakeText);
                $("#lblVMake5").text(response.vehicleData.VehicleMakeText);
                $("#lblVMake6").text(response.vehicleData.VehicleMakeText);
                $("#lblVMake7").text(response.vehicleData.VehicleMakeText);

                $("#lblVMake71").text(response.vehicleData.VehicleMakeText);
                $("#lblVMake72").text(response.vehicleData.VehicleMakeText);
                $("#lblVMake73").text(response.vehicleData.VehicleMakeText);
                $("#lblVYear71").text($("#txtVehicleYear").val());
            }
        }
    });

}
var validateNumber = function () {
    var result = $('#txtCardNumber').validateCreditCard();
    $('#txtCardNumber').removeClass();
    (result.card_type == null ? $('#txtCardNumber').removeClass().addClass("span11 card_number") : $('#txtCardNumber').addClass("span11 card_number " + result.card_type.name));
    (result.valid ? $('#txtCardNumber').addClass().addClass("valid") : $('#txtCardNumber').removeClass("valid"));

   
    var typeOfCard = (result.card_type != null ? result.card_type.type_of_card : "0");
    $("#ddlCardType").val(typeOfCard).trigger('change');
}
var renewalPayNow = function () {
   
    $("#divLoader").show();
    var msg = "";

    var vmake = $("#ddlVehicleMake").val();
    if (vmake == 0) {
        msg += " Please select Vehicle Make <br />";
    }
    var vtype = $("#ddlVehicleType").val();

    var vyear = $("#txtVehicleYear").val();
    if (!vyear) {
        msg += " Please select Vehicle Year <br />";
    }

    var ccnumber = $("#txtCardNumber").val();
    if (ccnumber.toString().indexOf('*') == -1) {
        $("#hndCardNumber").val(ccnumber);
    }

    if (!ccnumber) {
        msg += " Please enter Card No.<br />";
    }

    var cmonth = $("#ddlMonth").val();
    if (cmonth == 0) {
        msg += " Please select Month <br />";
    }
    var cyr = $("#ddlYear").val();
    if (cyr == 0) {
        msg += " Please select Year <br />";
    }
  
    var cccode = $("#txtCardSecurityCode").val();
    if (!cccode) {
        msg += " Please Enter Card Security Code <br />";
    }
  
   
   
    if (msg != "") {
        $("#divLoader").hide();
     
        $.alert({
            title: 'Alert!',
            content: msg,
            type: 'red'
        });
        return;
    }
    var aid = $("#hndAccountID").val();
    var lid = $("#hndLeadID").val();

    var model = {
    
        CardNumber: ccnumber,
        CardExpirationMonth: cmonth,
        CardExpirationYear: cyr,
        CardSecurityCode: cccode,
        AccountID: aid,
        LeadID: lid,
        TotalAmount: totalAmt,
        PackageId: packageId,
        PinNumber: $("#txtPin").val(),
        FirstName: $("#txtFirstName").val(),
        LastName: $("#txtLastName").val(),
        BStreet: $("#txtBillingAddress").val(),
        BCity: $("#txtBillingCity").val(),
        BState: $("#txtBillingState").val(),
        BZip: $("#txtBillingZip").val(),
        EmailID: $("#txtEmail").val(),
        Phone: $("#txtPhone1").val(),
        VehicleMake:vmake,
        VehicleYear: vyear,
        VehicleType:vtype,
    }
    var param = { model: model }
    $.ajax({
        url: "/RenewalOrder/SaveRenewalTrans",
        method: "post",
        data: model,
        contentType: "application/json; charset=utf-8", // content type sent to server
        dataType: "json", //Expected data format from server
        success: function (response) {
            $("#divLoader").hide();
            if ($.trim(response.error) != "") {

            } else {
                var result = response.Msg.split("|");
                if (result[0] != 0) {

                $('#form-wizard-1').hide();
                $('#form-wizard-2').hide();
                $('#form-wizard-3').hide();
                $('#form-wizard-4').show();


                $('#step3').removeAttr("disabled");
                $('#step4').removeAttr("disabled");
                

                $("#lblStepTitle").text("Transaction Information");

                $("#lblTransactionCreated").text("Transaction Date : " + result[0]);
                $("#lblTransactionStatus").text("Transaction Status : " + result[1]);
                $("#lblTransactionMessage").text("Transaction Message : " + result[2]);
                $("#lblTransactionID").text("Transaction ID : " + result[3]);


                } else {
                    $('#form-wizard-1').hide();
                    $('#form-wizard-2').hide();
                    $('#form-wizard-3').hide();
                    $('#form-wizard-4').show();


                    $('#step3').removeAttr("disabled");
                    $('#step4').removeAttr("disabled");


                    $("#lblStepTitle").text("Transaction Information");

                    $("#lblTransactionCreated").text("Transaction Date : " + result[0]);
                    $("#lblTransactionStatus").text("Transaction Status : " + result[1]);
                    $("#lblTransactionMessage").text("Transaction Message : " + result[2]);
                    $("#lblTransactionID").text("Transaction ID : " + result[3]);
                }
                
               
            }
        }
    });
    return false;
}