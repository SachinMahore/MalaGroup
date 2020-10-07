$(document).ready(function () {
  
    getCallHistoryList(1);
  
    $("#btnQuestion1").removeClass("hidden");
    $("#btnQuestion2").removeClass("hidden");
    $("#lblStepTitle").text("Pin Verification");
    //$("#lblUserName").text("NTSRAdmin");
    //$("#lblUserName2").text("NTSRAdmin");

    clearOrder();
    getPackageList();
    getAddPackageList();
    getVehicleMakeList();
  
    $('#chkDiffBillAddress').on('change', function () {      
        if ($(this).is(':checked')) {           
            $('#divBillAddress').removeClass("hidden");
            $("#lblBAddress").text("Billing Address: " + $("#txtBStreet").val() + ',' + $("#txtBCity").val() + ',' + $("#txtBState").val() + ',' + $("#txtBZip").val());
            $("#lblBAddress8").text("Billing Address: " + $("#txtBStreet").val() + ',' + $("#txtBCity").val() + ',' + $("#txtBState").val() + ',' + $("#txtBZip").val());
            $('#divBillingAdd').removeClass("hidden");
        }
        else {           
            $('#divBillAddress').addClass("hidden");
              $('#divBillingAdd').addClass("hidden");
        }
    });

    $("#ddlVehicleMake").on('change', function (evt, params) {
        var selected = $(this).val();
        if (selected != null) {
            getVehicleTypeList(selected);
        }
    });
    $("#ddlVehicleMake7").on('change', function (evt, params) {
        var selected = $(this).val();
        if (selected != null) {
            getVehicleTypeList7(selected);
        }
    });
    $("#btnQuestion1").on("click", function (event) {
        getQuestionList(1);
        $("#popQuestion1").PopupWindow("open");
        $("#popQuestion2").PopupWindow("close");
    });
    $("#btnQuestion2").on("click", function (event) {
        getQuestionList(2);
        $("#popQuestion2").PopupWindow("open");
        $("#popQuestion1").PopupWindow("close");
    });

    $("#popQuestion1").PopupWindow({
        title: "Questions and Rebuttals 1",
        modal: false,
        autoOpen: false,
        top: 240,
        left: 850,
        height: 400,
        width: 400,
    });
    $("#popQuestion2").PopupWindow({
        title: "Questions and Rebuttals 2",
        modal: false,
        autoOpen: false,
        top: 240,
        left: 850,
        height: 400,
        width: 400,
    });
    $("#ddlLeadLanguageAO").on('change', function (evt, params) {
        var selected = $(this).val();
        if (selected != null) {
            for (var j = 0; j < selected.length; j++) {
                if (selected[j] == 0) {
                    $("#ddlLeadLanguageAO").select2('data', null);
                    $("#ddlLeadLanguageAO").select2('data', { id: '0', text: 'All' });
                    break;
                }
            }
        }
    });

    $("#ddlChargeDay").on('change', function (evt, params) {
        var selected = $(this).val();
        if (selected != null) {
            chargeDateChange(selected);
        }
    });

    $("#ddlPaymentMethod").on('change', function (evt, params) {
        var selected = $(this).val();
        if (selected ==2) {
            $('#divcheck').removeClass("hidden");
            $('#divcc').addClass("hidden");
            $('#lblCardType').addClass("hidden");
            $('#lblCardNumber').addClass("hidden");
            $('#lblCardMonth').addClass("hidden");
            $('#lblCardCode').addClass("hidden");
            $('#lblCheckNumber').removeClass("hidden");
        }
        else  {
            $('#divcheck').addClass("hidden");
            $('#divcc').removeClass("hidden");
            $('#lblCardType').removeClass("hidden");
            $('#lblCardNumber').removeClass("hidden");
            $('#lblCardMonth').removeClass("hidden");
            $('#lblCardCode').removeClass("hidden");
            $('#lblCheckNumber').addClass("hidden");
        }
    });

    //$("#ddlAddDecal").on('change', function (evt, params) {
    //    var selected = $(this).val();
    //    if (selected != 0) {
          
    //        $("#chkIdentityTheft").prop("checked", true);
    //        $.uniform.update("#chkIdentityTheft");
    //    }
       
    //});

    $("#btnVehicleAdd").on("click", function (event) {
        $("#popVehicleDiv").PopupWindow("open");
        clearVehFieldPopUp();
    });
    $("#popVehicleDiv").PopupWindow({
        title: "Additional Vehicle Information",
        modal: true,
        autoOpen: false,
        top: 200,
        left: 180,
        height: 420,
        width: 680,

    });
    $("#txtNoOfVehicle").blur(function () {
        if ($("#txtNoOfVehicle").val() >1) {
            $("#divVehAO").removeClass("hidden");
        } else {
            $("#divVehAO").addClass("hidden");
        }
    });
});

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
                rows += "<label><input  type='checkbox' class='rdo mainpackage' name='radios' onclick='selectPackage(this," + elementValue.PackageID + "," + elementValue.Price + "," + elementValue.NoOfInstallment + ",\"" + elementValue.Package + " ($" + parseFloat(elementValue.Price).toFixed(2) + ")\");' id='chkPackage" + elementValue.PackageID + "' value=" + elementValue.PackageID + " />" + elementValue.Package + " ($" + elementValue.Price + ")</label>";
            });
            $("#tblRadioLists").append(rows);
        }
    });
}
var getAddPackageList = function () {
    $("#tblAdditionalPackage").empty();
    $.ajax({
        url: "/Package/GetAddPackageList",
        type: "post",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            var rows = "";
            $.each(response.model, function (index, elementValue) {
                rows += "<label><input  type='checkbox' class='chkpack addpackage' name='radios' onclick='selectAddPackage(" + elementValue.PackageID + "," + elementValue.Price + ",\"" + elementValue.Package + " ($" + parseFloat(elementValue.Price).toFixed(2) + ")\");' id='chkAddPackage" + elementValue.PackageID + "' value=" + elementValue.PackageID + " />" + elementValue.Package + " ($" + elementValue.Price + ")</label>";
            });
            $("#tblAdditionalPackage").append(rows);
        }
    });
}
var amtArray = [];
function selectPackage(cont, checkedRadio, pprice, installment, packagen) {

    $('.mainpackage').not($(cont)).prop('checked', false);
    $.uniform.update(".mainpackage");
    $('.AOStatus').prop('checked', false);
    $.uniform.update(".AOStatus");

    if (checkedRadio && $(cont).is(":checked")) {
        $("#lblPackage").text(packagen);
        $("#lblPackage8").text(packagen);
        $("#hndPackage").val(checkedRadio);
        $("#hndInstallment").val(installment);
        $("#hndPacakgeAmt").val(pprice);
    }else
    {
        $("#lblPackage").text("");
        $("#lblPackage8").text("");
        $("#hndPackage").val(0);
        $("#hndInstallment").val(0);
        $("#hndPacakgeAmt").val(0);
    }
}
var addPackageArray = [];

function selectAddPackage(checkedRadio, pprice, packagen) {
    addPackageArray = [];
    $('.addpackage').each(function (i, obj) {
        if ($(obj).is(':checked')) {
            var pkid = $(obj).attr("value");
            addPackageArray.push(parseFloat(pkid));
        }
    });
}
function nextTab(step)
{
    var msg = ""; 
    if (step == 1) {
        var pin = $("#txtPin").val();
        if (pin == "") {
            msg += " Please enter Pin.<br />";         
            $('#form-wizard-1').show();
        }
        else
        {
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
            url: '/AgentOrder/Edit',
            type: "post",
            data: JSON.stringify(param),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (response) {
                if (response.leadData.LeadID > 0) {
                    if (response.leadData.StepCompleted < 8) {
                        getCallHistoryList(response.leadData.LeadID,1);
                                              
                        step = response.leadData.StepCompleted;
                        getVehicleMakeList();
                        if (step == 0) {

                            $('#form-wizard-2').show();
                        }
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
                        setLanguage(response.leadData.Language);

                        $("#txtEmail").val(response.leadData.LeadEmail);
                        $("#txtCity").val(response.leadData.City);
                        $("#txtState").val(response.leadData.State);
                        $("#txtZip").val(response.leadData.Zip);
                        $("#txtStreet").val(response.leadData.Street);
                        $("#txtAChargeDate").val(response.leadData.FirstChargeDate);
                        $("#txtBCity").val(response.leadData.BCity);
                        $("#txtBState").val(response.leadData.BState);
                        $("#txtBZip").val(response.leadData.BZip);
                        $("#txtBStreet").val(response.leadData.BStreet);
                        $("#hndAmt").val(response.leadData.TotalAmt);
                        $("#hndPacakgeAmt").val(response.leadData.Price);
                        $("#ddlVehicleMake").val(response.leadData.VehicleMake).trigger('change');
                        $("#ddlVehicleType").val(response.leadData.VehicleType).trigger('change');
                        $("#txtVehicleYear").val(response.leadData.VehicleYear);
                        if (response.leadData.ChargeDay != 0) {
                            $("#ddlChargeDay").val(response.leadData.ChargeDay).trigger('change');
                        }
                        
                        
                        $("#chkPackage" + response.leadData.PackageId).prop("checked", true);
                        $.uniform.update("#chkPackage" + response.leadData.PackageId);

                        $("#hndInstallment").val(response.leadData.NoOfInstallment);
                        if (response.leadData.NoOfInstallment == 3) {
                            $('#divDay').removeClass("hidden");
                            $('#div3inst').removeClass("hidden");
                        }
                        if (response.leadData.NoOfInstallment == 6) {
                            $('#divDay').removeClass("hidden");
                            $('#div3inst').removeClass("hidden");
                            $('#div6inst').removeClass("hidden");
                        }
                       

                        $("#hndPackage").val(response.leadData.PackageId);

                        if (response.leadData.IsDiffBillingAdd != 0) {
                            $("#chkDiffBillAddress").prop("checked", true);
                            $.uniform.update("#chkDiffBillAddress");

                            $('#divBillAddress').removeClass("hidden");

                            $("#lblBAddress").text("Billing Address: " + response.leadData.BStreet + ', ' + response.leadData.BCity + ', ' + response.leadData.BState + ', ' + response.leadData.BZip);
                            $("#lblBAddress8").text("Billing Address: " + response.leadData.BStreet + ', ' + response.leadData.BCity + ', ' + response.leadData.BState + ', ' + response.leadData.BZip);

                            $('#divBillingAdd').removeClass("hidden");
                        }

                        $("#lblCustName").text(response.leadData.FirstName + " " + response.leadData.LastName);
                        $("#lblCustName1").text(response.leadData.LastName);
                        $("#lblCustName71").text(response.leadData.LastName);
                        $("#lblCustName72").text(response.leadData.LastName);
                        $("#lblName").text("Name: " + response.leadData.FirstName +" " +response.leadData.LastName);
                        $("#lblAddress").text("Address: " + response.leadData.Street + ', ' + response.leadData.City + ', ' + response.leadData.State+', ' + response.leadData.Zip);
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
                        addPackageArray = [];
                        if (response.leadData.AdditionalPackage) {
                            setAddPackage(response.leadData.AdditionalPackage);
                        }

                        //$("#lblTotalPackage").text("----- Total Amount    : ($" + response.leadData.TotalAmt + ")");
                        $("#lblEmail").text("Email: " + $("#txtEmail").val());
                        $("#lblEmail8").text("Email: " + $("#txtEmail").val());
                        $("#lblTotalAmt").val("$"+parseFloat(response.leadData.TotalAmt).toFixed(2));

                        $('#div1').removeClass("hidden");
                      
                        //Transaction
                        $("#ddlPaymentMethod").val(response.leadData.PaymentMethod).trigger('change');
                        $("#txtCheckNumber").val(response.leadData.CheckNumber);
                        $("#ddlCardType").val(response.leadData.CardType).trigger('change');
                        if (response.leadData.CardNumber) {
                            var mainStr = response.leadData.CardNumber,
                              vis = mainStr.slice(-4),
                               countNum = '';

                            for (var i = (mainStr.length) - 4; i > 0; i--) {
                                countNum += '*';
                            }
                            $("#txtCardNumber").val(countNum + vis);
                        }
                        // $("#txtCardNumber").val(response.leadData.CardNumber);

                        
                        $("#hndCardNumber").val(response.leadData.CardNumber);

                        $("#ddlMonth").val(response.leadData.CardExpirationMonth).trigger('change');
                        $("#ddlYear").val(response.leadData.CardExpirationYear).trigger('change');
                        $("#txtCardSecurityCode").val(response.leadData.CardSecurityCode);

                        $("#txtNoOfVehicle").val(response.leadData.NoOfVehicle);
                        $("#ddlAddDecal").val(response.leadData.AddDecals).trigger('change');
                        if (response.leadData.AddDecals != 0) {
                            $("#lblAddDecals").removeClass("hidden");
                            $("#editdecals").removeClass("hidden");
                            var decalamt = parseFloat(49) * parseFloat(response.leadData.AddDecals);
                            $("#lblAddDecals").text("Additional Decals  : ($" + parseFloat(decalamt).toFixed(2) + ")");
                            $("#lblAddDecals8").text("Additional Decals  : ($" + parseFloat(decalamt).toFixed(2) + ")");
                        }
                        if (response.leadData.IdentityTheft != 0) {
                            //$("#chkIdentityTheft").prop("checked", true);
                            //$.uniform.update("#chkIdentityTheft");
                            $("#lblIdentityTheft").removeClass("hidden");
                            $("#lblIdentityTheft").text("Identity Theft : ($10.00)");
                            $("#lblIdentityTheft8").text("Identity Theft : ($10.00)");
                        }
                        setChargeDates();
                        getVehicleAOData(response.leadData.OrderID);
                    }
                    else {
                        $('#form-wizard-1').hide();
                        $('#form-wizard-2').show();
                        
                    }
                }
                else
                {
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
        var primaryPhone = $("#txtPhone1").val();        
        if (!primaryPhone) {
            msg += " Please enter Primary Phone No.<br />";
        }
        if (!$('#txtPhone1').val().match('[0-9]{10}')) {
            msg += "Please put 10 digit Primary mobile number.<br />";
           
        }
        var secondryPhone = $("#txtPhone2").val();        
        if (secondryPhone != "") {
            if (!$('#txtPhone2').val().match('[0-9]{10}')) {
                msg += "Please put 10 digit Secondary mobile number.<br />";

            }
        }
        var firstName = $("#txtFirstName").val();
        if (!firstName) {
            msg += " Please enter First Name.<br />";
        }

        var language = $("#ddlLeadLanguageAO").val();
        if (!language) {
            msg += " Please select Langauge <br />";
        }
        var vmake = $("#ddlVehicleMake").val();
        if (vmake==0) {
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
            $("#lblName").text("Name: " + $("#txtFirstName").val() + " " + $("#txtLastName").val());
            $("#lblName8").text("Name: " + $("#txtFirstName").val() + " " + $("#txtLastName").val());

            $("#lblCustName").text($("#txtFirstName").val() + " " + $("#txtLastName").val());
            $("#lblCustName1").text($("#txtLastName").val());
            $("#lblCustName71").text($("#txtLastName").val());
            $("#lblCustName72").text($("#txtLastName").val());

            setVehicle($("#ddlVehicleMake").val(), $("#ddlVehicleType").val());
            $('#div2').removeClass("hidden");
            $('#step3').removeAttr("disabled");
            $("#lblStepTitle").text("Package Selection");
            updateLead(2);
        }
       
    }
    if (step == 3) {
        updateLead(3);
        var selected = $("input[type='checkbox'][class='AOStatus']:checked");
        console.log(selected);
        if (selected.val() == "2" || selected.val() == "3") {
            window.location = '/AgentOrder/AddEdit/';
        }

        $('#form-wizard-1').hide();
        $('#form-wizard-2').hide();
        $('#form-wizard-3').hide();
        $('#form-wizard-4').show();
        $("#lblPhone1").text("Primary Phone: " + $("#txtPhone1").val());
        $("#lblPhone2").text("Secondary Phone: " + $("#txtPhone2").val());
        $('#div2').removeClass("hidden");
        $('#divpackage').removeClass("hidden");
        $('#step3').removeAttr("disabled");
        $('#step4').removeAttr("disabled");
        $("#lblStepTitle").text("Address Information");
       

        $('#divDay').addClass("hidden");
        $('#div3inst').addClass("hidden");
        $('#div6inst').addClass("hidden");

        if ($("#hndInstallment").val() == "3") {
            $('#divDay').removeClass("hidden");
            $('#div3inst').removeClass("hidden");
           
        }
        else if ($("#hndInstallment").val() == "6") {
            $('#divDay').removeClass("hidden");
            $('#div3inst').removeClass("hidden");
            $('#div6inst').removeClass("hidden");
        } else {
            $('#divDay').addClass("hidden");
            $('#div3inst').addClass("hidden");
            $('#div6inst').addClass("hidden");
        }

      

       

    }
    if (step == 4) {
        $("#linkAddressEdit").removeClass("hidden");
        var msg = "";
        var leadEmail = $("#txtEmail").val();
        //if (leadEmail == "") {
        //    msg += " Please enter Lead Email.<br />";
        //}
        if (leadEmail != "") {
            if (!validateEmail(leadEmail)) {
                msg = msg + "Invalid Lead email address.<br/>"
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
            $('#form-wizard-4').hide();
            $('#form-wizard-5').show();
           
            $("#lblPhone1").text("Primary Phone: " + $("#txtPhone1").val());
            $("#lblPhone2").text("Secondary Phone: " + $("#txtPhone2").val());
            $("#lblAddress").text("Address: " + $("#txtStreet").val() + ',' + $("#txtCity").val() + ',' + $("#txtState").val() + ',' + $("#txtZip").val());
            $('#div2').removeClass("hidden");
            $('#divpackage').removeClass("hidden");
            $("#lblEmail").text("Email: " + $("#txtEmail").val());
            $("#lblEmail8").text("Email: " + $("#txtEmail").val());
            $('#divEmail').removeClass("hidden");
            $('#step3').removeAttr("disabled");
            $('#step4').removeAttr("disabled");
            $('#step5').removeAttr("disabled");
            $("#lblStepTitle").text("Mailing Address Information");
            updateLead(4);
        }
       
        
    }
    if (step == 5) {
        $("#linkAddressEdit").removeClass("hidden");
        var msg = "";
       
        var chargeDate = $("#txtAChargeDate").val();
        
        if (chargeDate == "") {
            msg += " Please enter Charge Date.<br />";
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
            $('#form-wizard-4').hide();
            $('#form-wizard-5').hide();
            $('#form-wizard-7').hide();
            $('#form-wizard-6').show();
            $('#form-wizard-8').hide();
            $('#form-wizard-9').hide();
            $('#step3').removeAttr("disabled");
            $('#step4').removeAttr("disabled");
            $('#step5').removeAttr("disabled");
            $('#step6').removeAttr("disabled");
           

            $("#lblTotalAmt").text("$" + parseFloat(totalAmt).toFixed(2));

            $("#lblPhone1").text("Primary Phone: " + $("#txtPhone1").val());
            $("#lblPhone2").text("Secondary Phone: " + $("#txtPhone2").val());
            $("#lblBAddress").text("Billing Address: " + $("#txtBStreet").val() + ',' + $("#txtBCity").val() + ',' + $("#txtBState").val() + ',' + $("#txtBZip").val());

            $('#div2').removeClass("hidden");
            $('#divpackage').removeClass("hidden");
            $("#lblEmail").text("Email: " + $("#txtEmail").val());
            $("#lblEmail8").text("Email: " + $("#txtEmail").val());
            $('#divEmail').removeClass("hidden");
          
            $("#lblStepTitle").text("Transaction Information");
            updateLead(5);
            setChargeDates();
        }
    }
    if (step == 6) {
      
        $("#linkAddressEdit").removeClass("hidden");
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
            var vtype = $("#ddlCardType").val();
            if (vtype == 0) {
                msg += " Please select Card Type <br />";
            }
            var vyear = $("#txtCardSecurityCode").val();
            if (!vyear) {
                msg += " Please Enter Card Security Code <br />";
            }
        } else {
            var checknumber = $("#txtCheckNumber").val();
            if (!checknumber) {
                msg += " Please enter Check No.<br />";
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
            $('#form-wizard-4').hide();
            $('#form-wizard-5').hide();
            $('#form-wizard-6').hide();
            $('#form-wizard-7').show();
            $('#form-wizard-8').hide();
            $('#form-wizard-9').hide();
            $('#step3').removeAttr("disabled");
            $('#step4').removeAttr("disabled");
            $('#step5').removeAttr("disabled");
            $('#step6').removeAttr("disabled");
            $('#step7').removeAttr("disabled");

           
            $("#lblPhone1").text("Primary Phone: " + $("#txtPhone1").val());
            $("#lblPhone2").text("Secondary Phone: " + $("#txtPhone2").val());
            $('#div2').removeClass("hidden");
            $('#divpackage').removeClass("hidden");
            $("#lblEmail").text("Email: " + $("#txtEmail").val());
            $('#divEmail').removeClass("hidden");
            $("#lblStepTitle").text("Additional Vehicle / Decals Information");
            $("#divSurvey").addClass("hidden");
            $("#divClosingScript").addClass("hidden");
            $("#divMessage").addClass("hidden");
            updateLead(6);
            saveTransaction();
           
        }
    }
    if (step == 7) {
        $("#linkAddressEdit").removeClass("hidden");
        $('#form-wizard-1').hide();
        $('#form-wizard-2').hide();
        $('#form-wizard-3').hide();
        $('#form-wizard-4').hide();
        $('#form-wizard-5').hide();
        $('#form-wizard-6').hide();
        $('#form-wizard-7').hide();
        $('#form-wizard-8').show();
        $('#form-wizard-9').hide();
        $('#step3').removeAttr("disabled");
        $('#step4').removeAttr("disabled");
        $('#step5').removeAttr("disabled");
        $('#step6').removeAttr("disabled");
        $('#step7').removeAttr("disabled");

        $("#lblTotalAmtDet").text("1. Total Charge Amount : ($" + parseFloat($("#hndAmt").val()).toFixed(2)+")");

        $("#lblCardType").text($("#ddlCardType").val() == 1 ? "Card Type : Visa" : +$("#ddlCardType").val() == 2 ? "Card Type : Rewards" : +$("#ddlCardType").val() == 3 ? "Card Type : Cash back" : "");

     
        var mainStr = $("#txtCardNumber").val(),
         vis = mainStr.slice(-4),
        countNum = '';

        for (var i = (mainStr.length) - 4; i > 0; i--) {
            countNum += '*';
        }
        $("#lblCardNumber").text("Card Number: " + countNum + vis);

        $("#lblCardMonth").text("Card Expiration Month / Year : " + $("#ddlMonth").val() + " / " + $("#ddlYear").val());
        //$("#lblCardCode").text("Card Security Code : " + $("#txtCardSecurityCode").val());
        $("#lblCheckNumber").text("Check Number : " + $("#txtCheckNumber").val());

        $("#lblPhone1").text("Primary Phone: " + $("#txtPhone1").val());
        $("#lblPhone2").text("Secondary Phone: " + $("#txtPhone2").val());
        $("#lblPhone18").text("Primary Phone: " + $("#txtPhone1").val());
        $("#lblPhone28").text("Secondary Phone: " + $("#txtPhone2").val());

        $('#div2').removeClass("hidden");
        $('#divpackage').removeClass("hidden");
        $("#lblEmail").text("Email: " + $("#txtEmail").val());
        $('#divEmail').removeClass("hidden");
        $("#divSurvey").addClass("hidden");
        $("#divClosingScript").addClass("hidden");
        $("#divMessage").addClass("hidden");
    
        $("#lblStepTitle").text("Transaction Information");
        updateLead(7);
       
       
    }
    if (step == 8) {
        $('#step3').removeAttr("disabled");
        $('#step4').removeAttr("disabled");
        $('#step5').removeAttr("disabled");
        $('#step6').removeAttr("disabled");
        $('#step7').removeAttr("disabled");
        $('#step8').removeAttr("disabled");
        $("#lblStepTitle").text("Pay & Finish");

        $("#linkAddressEdit").removeClass("hidden");
        $('#form-wizard-1').hide();
        $('#form-wizard-2').hide();
        $('#form-wizard-3').hide();
        $('#form-wizard-4').hide();
        $('#form-wizard-5').hide();
        $('#form-wizard-6').hide();
        $('#form-wizard-7').hide();
        $('#form-wizard-8').hide();
        $('#form-wizard-9').hide();

        $('#div2').removeClass("hidden");
        $('#divpackage').removeClass("hidden");
        $("#lblEmail").text("Email: " + $("#txtEmail").val());
        $('#divEmail').removeClass("hidden");

       

    }
   
}
function setVehicle(vmake,vtype)
{
    var param = { VehicleMake: vmake, VehicleType:vtype };
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
        $('#form-wizard-6').hide();
        $('#form-wizard-7').hide();
        $('#form-wizard-8').hide();
    }
    if (step == 3) {
        $("#lblStepTitle").text("Vehicle Information");
        $('#form-wizard-1').hide();
        $('#form-wizard-2').show();
        $('#form-wizard-3').hide();
        $('#form-wizard-4').hide();
        $('#form-wizard-5').hide();
        $('#form-wizard-6').hide();
        $('#form-wizard-7').hide();
        $('#form-wizard-8').hide();
    }
    if (step == 4) {
        $("#lblStepTitle").text("Package Selection");
        $('#form-wizard-1').hide();
        $('#form-wizard-2').hide();
        $('#form-wizard-3').show();
        $('#form-wizard-4').hide();
        $('#form-wizard-5').hide();
        $('#form-wizard-6').hide();
        $('#form-wizard-7').hide();
    }
    if (step == 5) {
        $("#lblStepTitle").text("Address Information");
        $('#form-wizard-1').hide();
        $('#form-wizard-2').hide();
        $('#form-wizard-3').hide();
        $('#form-wizard-4').show();
        $('#form-wizard-5').hide();
        $('#form-wizard-6').hide();
        $('#form-wizard-7').hide();
        $('#form-wizard-8').hide();
    }
    if (step == 6) {
        $("#lblStepTitle").text("Mailing Address Information");
        $('#form-wizard-1').hide();
        $('#form-wizard-2').hide();
        $('#form-wizard-3').hide();
        $('#form-wizard-4').hide();
        $('#form-wizard-5').show();
        $('#form-wizard-6').hide();
        $('#form-wizard-7').hide();
        $('#form-wizard-8').hide();
        setChargeDates();
    }
    if (step == 7) {
        $("#lblStepTitle").text("Transaction Information");
        $('#form-wizard-1').hide();
        $('#form-wizard-2').hide();
        $('#form-wizard-3').hide();
        $('#form-wizard-4').hide();
        $('#form-wizard-5').hide();
        $('#form-wizard-6').show();
        $('#form-wizard-7').hide();
        $('#form-wizard-8').hide();
    }
    if (step == 8) {
        $("#lblStepTitle").text("Additional Vehicle / Decals Information");
        $('#form-wizard-1').hide();
        $('#form-wizard-2').hide();
        $('#form-wizard-3').hide();
        $('#form-wizard-4').hide();
        $('#form-wizard-5').hide();
        $('#form-wizard-6').hide();
        $('#form-wizard-7').show();
        $('#form-wizard-8').hide();
        $('#form-wizard-9').hide();
    }
   
}
function stepTab(step)
{
   
    if (step == 1) {
        $("#lblStepTitle").text("Pin Verification");
        $('#form-wizard-1').show();
        $('#form-wizard-2').hide();
        $('#form-wizard-3').hide();
        $('#form-wizard-4').hide();
        $('#form-wizard-5').hide();
        $('#form-wizard-6').hide();
        $('#form-wizard-7').hide();
        $('#form-wizard-8').hide();
        $('#form-wizard-9').hide();
    }
    if (step == 2) {
        $("#lblStepTitle").text("Vehicle Information");
        $('#form-wizard-1').hide();
        $('#form-wizard-2').show();
        $('#form-wizard-3').hide();

        $('#form-wizard-4').hide();
        $('#form-wizard-5').hide();
        $('#form-wizard-6').hide();
        $('#form-wizard-7').hide();
        $('#form-wizard-8').hide();
        $('#form-wizard-9').hide();
    }
    if (step == 3) {
       
        
        $("#lblStepTitle").text("Package Selection");
        $('#form-wizard-1').hide();
        $('#form-wizard-2').hide();
        $('#form-wizard-3').show();
        $('#form-wizard-4').hide();
        $('#form-wizard-5').hide();
        $('#form-wizard-6').hide();
        $('#form-wizard-7').hide();
        $('#form-wizard-8').hide();
        $('#form-wizard-9').hide();
       
    }
    if (step == 4) {
        $("#lblStepTitle").text("Address Information");
        $('#form-wizard-1').hide();
        $('#form-wizard-2').hide();
        $('#form-wizard-3').hide();
        $('#form-wizard-4').show();
        $('#form-wizard-5').hide();
        $('#form-wizard-6').hide();
        $('#form-wizard-7').hide();
        $('#form-wizard-8').hide();
        $('#form-wizard-9').hide();
    }
    if (step == 5) {
        $("#lblStepTitle").text("Mailing Address Information");
        $('#form-wizard-1').hide();
        $('#form-wizard-2').hide();
        $('#form-wizard-3').hide();
        $('#form-wizard-4').hide();
        $('#form-wizard-5').show();
        $('#form-wizard-6').hide();
        $('#form-wizard-7').hide();
        $('#form-wizard-8').hide();
        $('#form-wizard-9').hide();
        setChargeDates();
    }
    if (step == 6) {
        $("#lblStepTitle").text("Transaction Information");
        $('#form-wizard-1').hide();
        $('#form-wizard-2').hide();
        $('#form-wizard-3').hide();
        $('#form-wizard-4').hide();
        $('#form-wizard-5').hide();
        $('#form-wizard-6').show();
        $('#form-wizard-7').hide();
        $('#form-wizard-8').hide();
        $('#form-wizard-9').hide();
    }
    if (step == 7) {
        $("#lblStepTitle").text("Additional Vehicle / Decals Information");
        $('#form-wizard-1').hide();
        $('#form-wizard-2').hide();
        $('#form-wizard-3').hide();
        $('#form-wizard-4').hide();
        $('#form-wizard-5').hide();
        $('#form-wizard-6').hide();
        $('#form-wizard-7').show();
        $('#form-wizard-8').hide();
        $('#form-wizard-9').hide();
       
    }
    if (step == 8) {
        $("#lblStepTitle").text("Pay & Finish");
        $('#form-wizard-1').hide();
        $('#form-wizard-2').hide();
        $('#form-wizard-3').hide();
        $('#form-wizard-4').hide();
        $('#form-wizard-5').hide();
        $('#form-wizard-6').hide();
        $('#form-wizard-7').hide();
        $('#form-wizard-8').show();
        $('#form-wizard-9').hide();
    }
    if (step == 9) {
        $("#lblStepTitle").text("Transaction Information");
        $('#form-wizard-1').hide();
        $('#form-wizard-2').hide();
        $('#form-wizard-3').hide();
        $('#form-wizard-4').hide();
        $('#form-wizard-5').hide();
        $('#form-wizard-6').hide();
        $('#form-wizard-7').hide();
        $('#form-wizard-8').show();
        $('#form-wizard-9').hide();
    }
}
var setLanguage = function (langauge) {
    $("#ddlLeadLanguageAO").val(langauge.split(",")).trigger("change");
}
var setAddPackage = function (packid) {
    var array = [];
    var addPackageArray = [];
    if (packid.toString().indexOf(',') > -1) {
        array=packid.split(',')
    } else
    {
        array.push(packid);
    }

    $.each(array, function (i) {
        $("#chkAddPackage" + array[i]).prop("checked", true);
        $.uniform.update("#chkAddPackage" + array[i]);
        addPackageArray.push(parseFloat(array[i]));
       
    });
    var param = { AdditionalPackage: packid }
    $.ajax({
        url: "/Package/GetPackageListDet",
        type: "post",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            $("#tblPackageLists").empty();
            $("#tblPackageLists8").empty();
            totalAmt = 0;
            var rows = "";
            $.each(response.model, function (index, elementValue) {
                rows += "<label>" + elementValue.Package + "  ($" + parseFloat(elementValue.Price).toFixed(2) + ")</label>";
                totalAmt += parseFloat(elementValue.Price);
            });
            $("#tblPackageLists").append(rows);
            $("#tblPackageLists8").append(rows);

            totalAmt += parseFloat($("#hndPacakgeAmt").val());
            var addDecal = $("#ddlAddDecal").val();          
            if (addDecal == 0) {
                $("#lblAddDecals").addClass("hidden");
                $("#editdecals").addClass("hidden");
                var idenAmt = $("#uniform-chkIdentityTheft span").hasClass("checked") ? "10" : "0";
                totalAmt += parseFloat(idenAmt);
            } else {
                $("#lblAddDecals").removeClass("hidden");
                $("#editdecals").removeClass("hidden");
                //$("#chkIdentityTheft").prop("checked", true);
                //$.uniform.update("#chkIdentityTheft");

                var decalamt = parseFloat(49) * parseFloat(addDecal);
                $("#lblAddDecals").text("Additional Decals  : ($" + parseFloat(decalamt).toFixed(2) + ")");
                $("#lblAddDecals8").text("Additional Decals  : ($" + parseFloat(decalamt).toFixed(2) + ")");
                totalAmt += parseFloat(decalamt);
            }
            
            $('#chkIdentityTheft').on('change', function () {
                if ($(this).is(':checked')) {
                    $("#lblIdentityTheft").removeClass("hidden");
                    $("#lblIdentityTheft").text("Identity Theft : ($10.00)");
                    $("#lblIdentityTheft8").text("Identity Theft : ($10.00)");

                }
                else {
                    $("#lblIdentityTheft").addClass("hidden");
                }
            });
           
            $("#lblTotalAmtDet").text("1. Total First Charge Amount : ($" + parseFloat(totalAmt).toFixed(2)+")");
            $("#lblTotalAmt").val("$"+parseFloat(totalAmt).toFixed(2));
         
            $("#lblTotalPackage").text("Total First Charge Amount    : ($" + parseFloat(totalAmt).toFixed(2) + ")");
     
        }
    });
   // $("#uniform-chkAddPackage7 span").addClass("checked");
}
var setChargeDates = function () {    
    var aoid = $("#hndAOID").val();
  
    var param = { AOID: aoid };
    $.ajax({
        url: "/AccountPage/GetTransOrderDetails",
        type: "post",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            $("#tblChargeDates").empty();
            $("#tblChargeDates8").empty();
            var rows = "";
            $.each(response, function (index, elementValue) {
                if (elementValue.ChargeNo > 1) {
                    rows += "<label>" + elementValue.ChargeNo + ". Charge Date: " + elementValue.ChargeDate + " Amt: ($" + parseFloat(elementValue.ChargeAmt).toFixed(2) + ")</label>";
                }
               
                if (elementValue.ChargeNo == 2) {
                    $("#txtSecondChargeDate").val(elementValue.ChargeDate);

                } else if (elementValue.ChargeNo == 3) {
                    $("#txtThirdChargeDate").val(elementValue.ChargeDate);
                }
                else if (elementValue.ChargeNo == 4) {
                    $("#txtFourthChargeDate").val(elementValue.ChargeDate);
                }
                else if (elementValue.ChargeNo == 5) {
                    $("#txtFifthChargeDate").val(elementValue.ChargeDate);
                }
                else if (elementValue.ChargeNo == 6) {
                    $("#txtSixthChargeDate").val(elementValue.ChargeDate);
                }
            });
            $("#tblChargeDates").append(rows);
            $("#tblChargeDates8").append(rows);
        }
    });
  
}
var totalAmt = 0;
var updateLead = function (step) {
    var aoID = $("#hndAOID").val();
    var leadID = $("#hndLeadID").val();
    var leadStatus = $("#ddlLeadStatus").val();
    var firstName = $("#txtFirstName").val();
    var lastName = $("#txtLastName").val();
    var primaryPhone = $("#txtPhone1").val();
    var secondaryPhone = $("#txtPhone2").val();
    var leadEmail = $("#txtEmail").val();  
    var language = "";
   
    var street = $("#txtStreet").val();
    var city = $("#txtCity").val();
    var state = $("#txtState").val();
    var zip = $("#txtZip").val();
   
    vehicleMake = $("#ddlVehicleMake").val();
    vehicleType = $("#ddlVehicleType").val();
    vehicleYear = $("#txtVehicleYear").val();
    var isDiffBillingAdd = $("#uniform-chkDiffBillAddress span").hasClass("checked") ? "1" : "0";
    //var vehicleId = $("#uniform-chkDiffBillAddress span").hasClass("checked") ? "1" : "0";

    var chargeDate = $("#txtAChargeDate").val();
    var chargeDay = $("#ddlChargeDay").val();
    var bstreet = $("#txtBStreet").val();
    var bcity = $("#txtBCity").val();
    var bstate = $("#txtBState").val();
    var bzip = $("#txtBZip").val();
    var packageid = $("#hndPackage").val();
    var noofvehicle = $("#txtNoOfVehicle").val();
    var adddecals = $("#ddlAddDecal").val(); 
    var identitytheft = $("#uniform-chkIdentityTheft span").hasClass("checked") ? "1" : "0";
   

    var aostatus = "";
    var selected = $("input[type='checkbox'][class='AOStatus']:checked");
    if (selected.length > 0) {
        aostatus = selected.val();
        
    }

    var languageArray = [];
    languageArray = $('select#ddlLeadLanguageAO').val();
    if (languageArray.length > 0) {
        for (var j = 0; j < languageArray.length; j++) {
            if (language == "") {
                language = languageArray[j];
            } else {
                language += "," + languageArray[j];
            }
        }
    } else {
        language = "0";
    }

 
    var addpackage = "";
    if (addPackageArray.length > 0) {
        for (var j = 0; j < addPackageArray.length; j++) {
            if (addpackage == "") {
                addpackage = addPackageArray[j];
            } else {
                addpackage += "," + addPackageArray[j];
            }
        }
    } else {
        addpackage = "0";
    }

    
    if (step == 3 || 7) {
        setAddPackage(addpackage);
        
    }
  
    var chargedateInfo = [];
    var noi = parseFloat($("#hndInstallment").val());
    
    chargedateInfo.push({ ChargeDate: $("#txtAChargeDate").val(), ChargeNo: 1, ChargeAmt: totalAmt });
    for (var i = 2; i <= noi; i++) {
       
        if (i == 2) {
            var chargeDate = $("#txtSecondChargeDate").val();
           
        } else if (i == 3) {
            var chargeDate = $("#txtThirdChargeDate").val();
        }
        else if (i == 4) {
            var chargeDate = $("#txtFourthChargeDate").val();
        }
        else if (i == 5) {
            var chargeDate = $("#txtFifthChargeDate").val();
        }
        else if (i == 6) {
            var chargeDate = $("#txtSixthChargeDate").val();
        }
                
            var data = {
                ChargeDate: chargeDate,
                ChargeNo: i,
                ChargeAmt: $("#hndPacakgeAmt").val(),
            };
            chargedateInfo.push(data);
           
    }
  
    var vehicleDetailInfo = [];
    for (var i = 1; i < rowcount; i++) {
        var rowEdit = $('#tbl_vehicleLead tr[data-value="' + i + '"]');
        if (rowEdit.length > 0) {
            var vehicleMake = $(rowEdit).find("td:eq(0)").attr("data-makeid");
            var vehicleType = $(rowEdit).find("td:eq(1)").attr("data-typeid");
            var vehicleYear = $(rowEdit).find("td:eq(2)").attr("data-vehicleyear");
            var vinno = $(rowEdit).find("td:eq(3)").attr("data-vinno");
            var licensePlate = $(rowEdit).find("td:eq(4)").attr("data-licenseplate");
            var dealerShip = $(rowEdit).find("td:eq(5)").attr("data-dealership");
            var financeCompany = $(rowEdit).find("td:eq(6)").attr("data-financecompany");
            var data = {
                VehicleMake: vehicleMake,
                VehicleType: vehicleType,
                VehicleYear: vehicleYear,
                VINNO: vinno,
                LicensePlate: licensePlate,
                DealerShip: dealerShip,
                FinanceCompany: financeCompany
            };
            vehicleDetailInfo.push(data);
        }
    }

    var model = {
        StepCompleted: step,
        LeadID: leadID,
        LeadStatus: leadStatus,
        FirstName: firstName,
        LastName: lastName,
        PrimaryPhone: primaryPhone,
        SecondaryPhone: secondaryPhone,
        LeadEmail: leadEmail,
        Language: language,      
        Street: street,
        City: city,
        State: state,
        Zip: zip,
        OrderID:aoID,
        VehicleMake:vehicleMake,
        VehicleType:vehicleType,
        VehicleYear: vehicleYear,
        IsDiffBillingAdd: isDiffBillingAdd,
        BStreet: bstreet,
        BCity: bcity,
        BState: bstate,
        BZip: bzip,
        FirstChargeDate: chargeDate,
        PackageId: packageid,
        AdditionalPackage: addpackage,  
        TotalAmt: totalAmt,
        NoOfVehicle:noofvehicle,
        AddDecals:adddecals,
        IdentityTheft: identitytheft,
        ChargeDay:chargeDay,
        chargeDateList: chargedateInfo,
        AOStatus: aostatus,
        VehicleAOList: vehicleDetailInfo
    }
    $.ajax({
        url: "/AgentOrder/UpdateAgentLead",
        type: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8", // content type sent to server
        dataType: "json", //Expected data format from server
        success: function (response) {
            $("#lblTotalAmt").val("$" + parseFloat(totalAmt).toFixed(2));
            $("#hndAmt").val(totalAmt);
          
        }
    });

    return false;
}
function editName()
{
    $("#divNameEdit").removeClass("hidden");
}
function clearOrder()
{
 $("#hndAOID").val(0);
 $("#hndLeadID").val(0);
 $("#txtPin").val("");
 addPackageArray = [];
 amtArray = [];
 $("#hndPackage").val(0);
 $("#hndInstallment").val(0);
 $("#hndAmt").val(0);
 $("#hndPacakgeAmt").val(0);
}

//vehicle Information Start

var getVehicleLeadData = function (leadID) {
    
    var param = { LeadID: leadID };
    $.ajax({
        url: "/AgentOrder/GetVehicleLeadInfo",
        type: "post",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",

        success: function (response) {
            if (response.vehicleData != "") {
                
            }
        }
    });
}

// Vehicle Information End
// Transaction Save Code Start
var saveTransaction = function () {
    var msg = "";
    var aoid = $("#hndAOID").val();
    var lid = $("#hndLeadID").val();
   
    var cardType = $("#ddlCardType").val();
    var cardNumber = $("#hndCardNumber").val();
   // validateCardNumber(cardNumber);

    var cardExpirationMonth = $("#ddlMonth").val();
    var cardExpirationYear = $("#ddlYear").val();
    var cardSecurityCode = $("#txtCardSecurityCode").val();
    //total amount field is missing add in view 
    var totalAmount = $("#hndAmt").val();
    var checkNumber = $("#txtCheckNumber").val();
    var paymentMethod = $("#ddlPaymentMethod").val();

    var model = {
      
        CardType: cardType,
        CardNumber: cardNumber,       
        CardExpirationMonth: cardExpirationMonth,
        CardExpirationYear: cardExpirationYear,
        CardSecurityCode: cardSecurityCode,
        AOID: aoid,
        LeadID: lid,
        TotalAmount: totalAmount,
        CheckNumber: checkNumber,
        PaymentMethod: paymentMethod,
    }
    var param = { model: model }
    $.ajax({
        url: "/AgentOrder/SaveTransaction",
        method: "post",
        data: model,
        contentType: "application/json; charset=utf-8", // content type sent to server
        dataType: "json", //Expected data format from server
        success: function (response) {
            if ($.trim(response.error) != "") {

            } else {
                  var mainStr = $("#txtCardNumber").val(),
                 vis = mainStr.slice(-4),
                countNum = '';

                  for (var i = (mainStr.length) - 4; i > 0; i--) {
                      countNum += '*';
                  }
                  $("#txtCardNumber").val( countNum + vis);
            }

        }

    });

    return false;
}
var payNow = function () {
    $("#divLoader").show();
    var msg = "";
    var aoid = $("#txtPin").val();
  
    var model = {
        PinNo: aoid,
    }
    var param = { model: model }
    $.ajax({
        url: "/AgentOrder/PayNow",
        method: "post",
        data: model,
        contentType: "application/json; charset=utf-8", // content type sent to server
        dataType: "json", //Expected data format from server
        success: function (response) {
            $("#divLoader").hide();
            if ($.trim(response.error) != "") {

            } else {
                var result = response.accountID.split("|");
                if (result[0] != 0) {
                    $('#form-wizard-8').hide();
                    $('#form-wizard-9').show();
                    $('#step8').removeAttr("disabled");
                    $("#lblStepTitle").text("Transaction Information");

                    $("#divTransactionDetails").removeClass("alert-error");
                    $("#divTransactionDetails").addClass("alert-info");

                    $("#lblTransactionCreated").text("Transaction Date : " + result[1]);
                    $("#lblTransactionStatus").text("Transaction Status : " + result[2]);
                    $("#lblTransactionMessage").text("Transaction Message : " + result[3]);
                    $("#lblTransactionID").text("Transaction ID : " + result[4]);

                    $("#divNewButton").removeClass("hidden");
                    $("#divMessage").removeClass("hidden");
                    $("#divSurvey").removeClass("hidden");
                    $("#divClosingScript").removeClass("hidden");
                    $("#divMessage").addClass("alert-success");

                    $("#divMessage").removeClass("alert-error");
                    $("#divMessage").text("Transaction Done Successfully");
                    $("#finishButton").addClass("hidden");
                    $("#divStepNo").addClass("hidden");
                    $("#hndAccountID").val(result[0]);
                    
                   

                }else
                {
                    $("#backFinish").removeAttr("disabled");
                    $('#form-wizard-8').hide();
                    $('#form-wizard-9').show();
                    $('#step8').removeAttr("disabled");
                    $("#lblStepTitle").text("Transaction Information");

                  
                    $("#txtCardNumber").val("");
                    $("#txtCardNumber").val($("#hndCardNumber").val());

                    $("#divTransactionDetails").removeClass("alert-info");
                    $("#divTransactionDetails").addClass("alert-error");

                    $("#lblTransactionCreated").text("Transaction Date : " + result[1]);
                    $("#lblTransactionStatus").text("Transaction Status : " + result[2]);
                    $("#lblTransactionMessage").text("Transaction Message : " + result[3]);
                    $("#lblTransactionID").text("Transaction ID : " + result[4]);

                    $("#divMessage").removeClass("hidden");
                    $("#divSurvey").addClass("hidden");
                    $("#divClosingScript").addClass("hidden");
                    $("#divMessage").removeClass("alert-success");

                    $("#divMessage").addClass("alert-error");
                    $("#divMessage").text("Transaction Failed.");
                    $("#divNewButton").addClass("hidden");

                    $('#backFinish').removeAttr("disabled");
                }
            }
        }
    });

    return false;
}
var newOrderAcct = function (pager)
{
    if(pager==1)
    {
        window.location = '/AgentOrder/AddEdit/';
    }else
    {
        window.location = '/AccountPage/Edit/' +$("#hndAccountID").val();
    }
}
var goToAccount = function () {
    window.open('/AccountPage/Edit/' + $("#hndAccountID").val(), '_blank')
}
var getVehicleMakeList = function () {
    $("#ddlVehicleMake").empty();
    $("#ddlVehicleMake7").empty();
    $.ajax({
        url: "/Leads/GetVehicleMakeList",
        type: "post",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            $("#ddlVehicleMake").append("<option value='0'>Select Vehicle Make...</option>");
            $("#ddlVehicleMake7").append("<option value='0'>Select Vehicle Make...</option>");
            $.each(response.model, function (index, elementValue) {
                var opt = "<option value='" + elementValue.Value + "'>" + elementValue.Text + "</option>";
                $("#ddlVehicleMake").append(opt);
                $("#ddlVehicleMake7").append(opt);
            });
            $("#ddlVehicleMake").val(0).trigger('change');
            $("#ddlVehicleMake7").val(0).trigger('change');
        }
    });
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
var getVehicleTypeList7 = function (vehicleMake) {
    $("#ddlVehicleType7").empty();
    var param = { VehcileMake: vehicleMake }
    $.ajax({
        url: "/Leads/GetVehicleTypeList",
        type: "post",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            $("#ddlVehicleType7").append("<option value='0'>Select Vehicle Type...</option>");
            $.each(response.model, function (index, elementValue) {
                var opt = "<option value='" + elementValue.Value + "'>" + elementValue.Text + "</option>";
                $("#ddlVehicleType7").append(opt);
            });
            $("#ddlVehicleType7").val(0).trigger('change');
        }
    });
}
function getQuestionList(qtype) {
    var param = { Type: qtype };
    $.ajax({
        url: '/AgentOrder/GetQuestionsList',
        type: "post",
         data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {
                if (qtype == 1) {
                    $("#tblQuestion1>tbody").empty();
                    $.each(response, function (index, elementValue) {
                        var html = '';
                        html += '<tr data-value="' + elementValue.QID + '">';
                        html += '<td><a href="#" >' + elementValue.Question + '</a><br/>' + elementValue.Answer + '</td>';

                        html += '</tr>';
                        $("#tblQuestion1>tbody").append(html);
                    });
                }
                if (qtype == 2) {
                    $("#tblQuestion2>tbody").empty();
                    $.each(response, function (index, elementValue) {
                        var html = '';
                        html += '<tr data-value="' + elementValue.QID + '">';
                        html += '<td><a href="#" >' + elementValue.Question + '</a><br/>' + elementValue.Answer + '</td>';

                        html += '</tr>';
                        $("#tblQuestion2>tbody").append(html);
                    });
                }
            }
        }
    });
}

var chargeDateChange = function (chargeday) {
    var dateAdded = $("#txtAChargeDate").val();
    var resultDate = new Date(dateAdded);
    var noi = parseFloat($("#hndInstallment").val());
    var currentMonth = resultDate.getMonth() + 1;
    var yyyy = resultDate.getFullYear();
    for (var i = 2; i <= noi; i++) {
        var newday = chargeday;
        currentMonth += 1;
        var newMonth = currentMonth;
        if (currentMonth > 12) {
            currentMonth = 1;
            yyyy += 1;
        }
        var nextMonth = new Date(yyyy, currentMonth, 0);
        var nextDay = nextMonth.getDate();
        if (parseFloat(nextDay) < parseFloat(newday)) {
            newday = nextDay;
        }
        var newMonth = nextMonth.getMonth() + 1;
        if (newday < 10) { newday = '0' + newday } if (newMonth < 10) { newMonth = '0' + newMonth }
        var todate = newMonth + '/' + newday + '/' + yyyy;
        if (i == 2) {
            $("#txtSecondChargeDate").val(todate);
        } else if (i == 3) {
            $("#txtThirdChargeDate").val(todate);
        }
        else if (i == 4) {
            $("#txtFourthChargeDate").val(todate);
        }
        else if (i == 5) {
            $("#txtFifthChargeDate").val(todate);
        }
        else if (i == 6) {
            $("#txtSixthChargeDate").val(todate);
        }
    }
}

var rowcount = 1;
var saveVehicleAO = function () {

    var msg = "";
    var vehicleM = $('#ddlVehicleMake7').select2('data');
    var vehicleMakeText = vehicleM.text;
    var vehicleMake = vehicleM.id;
    var vehicleT = $('#ddlVehicleType7').select2('data');
    var vehicleTypeText = vehicleT.text;
    var vehicleType = vehicleT.id;
    var vehicleYear = $("#txtVehicleYear7").val();
    var vinno = $("#txtVinNo").val();
    var licensePlate = $("#txtLicense").val();
    var dealerShip = $("#txtDealer").val();
    var financeCompany = $("#txtFinanceCompany").val();
    if (vehicleMake == 0) {
        msg += " Please Select Vehicle Make.<br />";
    }
   
    if (msg != "") {
        $.alert({
            title: 'Alert!',
            content: msg,
            type: 'red'
        });
        return;
    }

    if (editRowCount > 0) {
        $('#tbl_vehicleLead tr[data-value="' + editRowCount + '"]').remove();
        editRowCount = 0;
    }
    var html = '';
    html += '<tr class="trVehicleMainRow pd-vehicletblText" data-value="' + rowcount + '">';
    html += '<td class="pd-vehicletblText"  data-makeid="' + vehicleMake + '">' + vehicleMakeText + '</td>';
    html += '<td class="pd-vehicletblText"  data-typeid="' + vehicleType + '">' + vehicleTypeText + '</td>';
    html += '<td class="pd-vehicletblText"  data-vehicleyear="' + vehicleYear + '">' + vehicleYear + '</td>';
    html += '<td class="pd-vehicletblText"  data-vinno="' + vinno + '">' + vinno + '</td>';
    html += '<td  class="hidden pd-vehicletblText " data-licenseplate="' + licensePlate + '">' + licensePlate + '</td>';
    html += '<td  class="hidden pd-vehicletblText " data-dealership="' + dealerShip + '">' + dealerShip + '</td>';
    html += '<td  class="hidden pd-vehicletblText hidden" data-financecompany="' + financeCompany + '">' + financeCompany + '</td>';
    html += '<td class="pd-vehicletblText"  class="pd-editdeletetbn"><div><button  onclick="editTableRow(' + rowcount + ')"><i class="icon-pencil" ></i></button><button class="pd-btndelete"  onclick="removeTableRow(' + rowcount + ');"><i class="icon-trash"  ></i></button></div></td>';
    html += '</tr>';
    $("#tbl_vehicleLead>tbody").append(html);
    rowcount += 1;
    $("#popVehicleDiv").PopupWindow("close");
}
var openCallHistory = function ()
{
    $("#popHistory").PopupWindow("open");
}
var editRowCount = 0;
var editTableRow = function (id) {
    editRowCount = id;

    $("#popVehicleDiv").PopupWindow("open");

    var rowEdit = $('#tbl_vehicleLead tr[data-value="' + id + '"]');
    $("#ddlVehicleMake7").val($(rowEdit).find("td:eq(0)").attr("data-makeid")).trigger('change');
    $("#ddlVehicleType7").val($(rowEdit).find("td:eq(1)").attr("data-typeid")).trigger('change');
    $("#txtVehicleYear7").val($(rowEdit).find("td:eq(2)").attr("data-vehicleyear"));
    $('#txtVinNo').val($(rowEdit).find("td:eq(3)").attr("data-vinno"));
    $('#txtLicense').val($(rowEdit).find("td:eq(4)").attr("data-licenseplate"));
    $('#txtDealer').val($(rowEdit).find("td:eq(5)").attr("data-dealership"));
    $('#txtFinanceCompany').val($(rowEdit).find("td:eq(6)").attr("data-financecompany"));
}
var removeTableRow = function (id) {

    $.confirm({
        title: 'Alert!',
        content: '"Are you sure you want to delete the data?"',
        type: 'red',
        buttons: {
            confirm: function () {
                $('#tbl_vehicleLead tr[data-value="' + id + '"]').remove();
            },
            cancel: function () {
            }
        }
    });
}
var getVehicleAOData = function (aoid) {
    var param = { AOID: aoid };
    $.ajax({
        url: "/AgentOrder/GetVehicleAOInfo",
        type: "post",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",

        success: function (response) {
            if (response.vehicleData != "") {
                if (response.vehicleData.length > 0) {
                    $("#divVehAO").removeClass("hidden");
                    rowCount = 0;
                    $.each(response.vehicleData, function (index, elementValue) {
                        elementValue.SrNo
                        var html = '';
                        html += '<tr class="trVehicleMainRow pd-vehicletblText" data-value="' + rowcount + '">';
                        html += '<td class="pd-vehicletblText" data-makeid="' + elementValue.VehicleMake + '">' + elementValue.VehicleMakeText + '</td>';
                        html += '<td class="pd-vehicletblText"  data-typeid="' + elementValue.VehicleType + '">' + elementValue.VehicleTypeText + '</td>';
                        html += '<td class="pd-vehicletblText"  data-vehicleyear="' + elementValue.VehicleYear + '">' + elementValue.VehicleYear + '</td>';
                        html += '<td class="pd-vehicletblText"  data-vinno="' + elementValue.VINNO + '">' + elementValue.VINNO + '</td>';
                        html += '<td class="pd-vehicletblText hidden" data-licenseplate="' + elementValue.LicensePlate + '">' + elementValue.LicensePlate + '</td>';
                        html += '<td class="pd-vehicletblText hidden" data-dealership="' + elementValue.DealerShip + '">' + elementValue.DealerShip + '</td>';
                        html += '<td class="pd-vehicletblText hidden" data-financecompany="' + elementValue.FinanceCompany + '">' + elementValue.FinanceCompany + '</td>';
                        html += '<td class="pd-vehicletblText"  class="pd-editdeletetbn"><div><button onclick="editTableRow(' + rowcount + ')"><i class="icon-pencil" ></i></button><button  onclick="removeTableRow(' + rowcount + ');"><i class="icon-trash" ></i></button></div></td>';
                        html += '</tr>';
                        $("#tbl_vehicleLead>tbody").append(html);
                        rowcount += 1;
                    });
                }
            }
        }
    });
}
var clearVehFieldPopUp = function () {

    $("#ddlVehicleMake7").val(0).trigger('change');
    $("#ddlVehicleType7").val(0).trigger('change');
    $("#txtVehicleYear7").val("");
    $("#txtVinNo").val("");
    $("#txtLicense").val("");
    $("#txtDealer").val("");
    $("#txtFinanceCompany").val("");
}
var setAOStatus=function(cont)
{
    $('.AOStatus').not($(cont)).prop('checked', false);
    $.uniform.update(".AOStatus");
    $('.mainpackage').prop('checked', false);
    $.uniform.update(".mainpackage");
}