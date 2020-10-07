var search = function () {
    window.location = '/AccountPage/Index';
}
var lastmodiDate = "";
$(document).ready(function () {
    $("#txtLastModifiedDate").change(function () {
      
       lastmodiDate = $("#txtLastModifiedDate").val();
    });
    getPackageList();
    $("#popEmailTemplateDiv").PopupWindow({
        title: "Email Templates",
        modal: true,
        autoOpen: false,
        top: 220,
        left: 180,
        height: 400,
        width: 650,

    });
    $("#popPaymentConsole").PopupWindow({
        title: "Payment Console",
        modal: false,
        autoOpen: false,
        top: 170,
        left: 180,
        height: 600,
        width: 795
    });

    $("#popRefundOff").PopupWindow({
        title: "Refund Transaction",
        modal: true,
        autoOpen: false,
        top: 200,
        left: 350,
        height: 350,
        width: 750,

    });
    $("#popVoid").PopupWindow({
        title: "Void Transaction",
        modal: true,
        autoOpen: false,
        top: 200,
        left: 450,
        height: 350,
        width: 650,

    });
    $("#popCharge").PopupWindow({
        title: "Charge Transaction",
        modal: true,
        autoOpen: false,
        top: 200,
        left: 450,
        height: 350,
        width: 650,

    });
    $("#popAddTrans").PopupWindow({
        title: "Add Transaction",
        modal: false,
        autoOpen: false,
        top: 200,
        left: 100,
        height: 450,
        width: 320,

    });
    $("#btnAddTrans").on("click", function (event) {
        $("#hndAOID").val(0)
        $("#divAddTransPackage").removeClass("hidden");
        $("#popAddTrans").PopupWindow("open");

    });
    $("#btnAddTransAO").on("click", function (event) {
        $("#divAddTransPackage").addClass("hidden");
        $("#popAddTrans").PopupWindow("open");

    });
    $("#PaymentConsole").on("click", function (event) {
        //$("#txtPCCardNumber").val("");
        //$("#ddlPCMonth").val(0).trigger('change');
        //$("#ddlPCYear").val(0).trigger('change');
        //$("#txtPCChargeAmount").val("");
        //$("#txtPCCardCode").val("");
        //$("#txtPCCardName").val("");
        $("#divPack").removeClass("hidden");
        getVehicleMakeList();
        getPCCheckInfo($("#hndAccountID").val());
        $("#popPaymentConsole").PopupWindow("open");


    });
    $("#txtEmployee").keypress(function (e) {
        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
            $("#errEMP").html("Digits Only").show().fadeOut("slow");
            return false;
        }
    });
    $("#txtParentAccount").keypress(function (e) {
        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
            $("#errmsg").html("Digits Only").show().fadeOut("slow");
            return false;
        }
    });
    $("#btnSelectTemplate").on("click", function (event) {
        getEmailTemplates();
    });
    $("#popOwnerDiv").PopupWindow({
        title: "Select User",
        modal: true,
        autoOpen: false,
        top: 200,
        left: 380,
        height: 400,
        width: 750,

    });
    
    $("#popFileDiv").PopupWindow({
        title: "Attached File(s)",
        modal: true,
        autoOpen: false,
        top: 200,
        left: 180,
        height: 400,
        width: 750,

    });
    $("#btnAddOwnerPop").on("click", function (event) {
        getOwnerList(1);
      
        $("#popOwnerDiv").PopupWindow("open");

    });
    $("#btnAddCreatedByPop").on("click", function (event) {
        getOwnerList(2);
        $("#popOwnerDiv").PopupWindow("open");

    });
    $("#btnAddLastModifiedByPop").on("click", function (event) {
        getOwnerList(3);
        $("#popOwnerDiv").PopupWindow("open");

    });
    $("#popExportDiv").PopupWindow({
        title: "Export Day Wise Details",
        modal: true,
        autoOpen: false,
        top: 200,
        left: 380,
        height: 400,
        width: 450,

    });
    $("#popAdvSearch").PopupWindow({
        title: "Advance Account Search",
        modal: true,
        autoOpen: false,
        top: 150,
        left: 250,
        height: 350,
        width: 600,

    });

    $("#btnExportDaily").on("click", function (event) {
        $("#popExportDiv").PopupWindow("open");
    });
    
    $("#ddlOwner").on('change', function (evt, params) {
        var selected = $(this).val();
        if (selected != null) {


        }
    });

    $("#ddlAccountStatus").on('change', function (evt, params) {
        var selected = $(this).val();
        if (selected == 1 || selected == 2 || selected == 6 || selected == 7 || selected == 8 || selected == 9) {

            $("#txtCancellationDate").val("");
        }
        else {
            var d = new Date(),
            date = ((d.getMonth() + 1) + '/' + (d.getDate()) + '/' + d.getFullYear());
            $('#txtCancellationDate').val(date);

        }
    });


    $('#ulPaginationAccountDetail').pagination({
        items: 0,
        currentPage: 1,
        displayedPages: 10,
        cssStyle: '',
        useAnchors: true,
        prevText: '&laquo;',
        nextText: '&raquo;',
        onInit: function () {
            buildPaganationAccount();
        },
        onPageClick: function (page, evt) {
            fillAccountTable(page, 1);
        }
    });
    $("#ddlRowPerPage").on('change', function (evt, params) {
        ddlRowPerPageChange();
    });
    $("#ddlRowPerPage").select2({ minimumResultsForSearch: Infinity });

    $('#ulPaginationAccountDetailAdv').pagination({
        items: 0,
        currentPage: 1,
        displayedPages: 10,
        cssStyle: '',
        useAnchors: true,
        prevText: '&laquo;',
        nextText: '&raquo;',
        onInit: function () {
            buildPaganationAccount();
        },
        onPageClick: function (page, evt) {
            fillAccountTable(page, 2);
        }
    });
    $("#ddlRowPerPageAdv").on('change', function (evt, params) {
        ddlRowPerPageChangeAdv();
    });
    $("#ddlRowPerPageAdv").select2({ minimumResultsForSearch: Infinity });

    $("#popTransDiv").PopupWindow({
        title: "Order Transaction Details",
        modal: false,
        autoOpen: false,
        top: 200,
        left: 450,
        height: 350,
        width: 750,

    });

    $("#popDecalsDiv").PopupWindow({
        title: "Decals Info",
        modal: true,
        autoOpen: false,
        top: 200,
        left: 110,
        height: 350,
        width: 1050,

    });
    $("#popCardDiv").PopupWindow({
        title: "Card/Check Info",
        modal: false,
        autoOpen: false,
        top: 200,
        left: 410,
        height: 280,
        width: 450,

    });

    //CKEDITOR.replace('txtChatBody', {
    //    fullPage: false,
    //    allowedContent: true,
    //    autoGrow_onStartup: true,
    //    enterMode: CKEDITOR.ENTER_BR,
    //    height: 100,

    //});
    $("#ddlPackages").on('change', function (evt, params) {
        var selected = $(this).val();
        if (selected != null) {
            var packid = selected.split('|');
            $("#txtPCChargeAmount").val(packid[1]);
            $("#hndPackId").val(packid[0]);
        }
    });
    $("#ddlPackagesT").on('change', function (evt, params) {
        var selected = $(this).val();
        if (selected != null) {
            var packid = selected.split('|');
            $("#txtChargeAmt").val(packid[1]);
            $("#hndPackId").val(packid[0]);
        }
    });
    $("#btnAddChat").on("click", function (event) {
        $("#hndCID").val(0);
        $("#txtChatTitle").val("");
        $("#txtChatBody").val("");
        $("#popChatDet").PopupWindow("open");
        $("#btnDeletechat").addClass("hidden");
        $("#chatterUploadDiv").removeClass("hidden");
        $("#linkChatFile").addClass("hidden");
    });
    $("#popChatDet").PopupWindow({
        title: "Add Chat",
        modal: false,
        autoOpen: false,
        top: 220,
        left: 400,
        height: 380,

    });

    $("#popEmaillHistoryDiv").PopupWindow({
        title: "Email History",
        modal: false,
        autoOpen: false,
        top: 220,
        left: 400,
        height: 380,

    });


    $("#popImportFile").PopupWindow({
        title: "Import File to Account",
        modal: false,
        autoOpen: false,
        top: 220,
        left: 300,
        height: 380,

    });

    $("#btnImport").on("click", function (event) {
        $("#popImportFile").PopupWindow("open");
    });

    $("#popAddDecalAcc").PopupWindow({
        title: "Add Decals",
        modal: false,
        autoOpen: false,
        top: 220,
        left: 300,
        height: 380,

    });
    $("#btnVehicleAdd").on("click", function (event) {
        if ($("#hndDecalRow").val() < 4) {
            getVehicleMakeList();
            clearDecal();
            $("#popAddDecalAcc").PopupWindow("open");
        }
        else {
            $.alert({
                title: 'Alert!',
                content: "Max Decal Defined...!",
                type: 'red'
            });
        }
    });

    $("#ddlLeadLanguageAcc").on('change', function (evt, params) {
        var selected = $(this).val();
        if (selected != null) {
            for (var j = 0; j < selected.length; j++) {
                if (selected[j] == 0) {
                    $("#ddlLeadLanguageAcc").select2('data', null);
                    $("#ddlLeadLanguageAcc").select2('data', { id: '0', text: 'All' });
                    break;
                }
            }
        }
    });

    //$('#chkDefaultCard').on('change', function () {
    //    if ($(this).is(':checked')) {
    //        $("#txtPCCardNumber").removeAttr("disabled");
    //        $("#txtPCCardCode").removeAttr("disabled");
    //    }
    //    else {

    //        $("#txtPCCardNumber").attr("disabled", "disabled");
    //        $("#txtPCCardCode").attr("disabled", "disabled");

    //    }
    //});
});
var ddlRowPerPageChange = function () {
    buildPaganationAccount();
}
var ddlRowPerPageChangeAdv = function () {
    buildAdvPaganationAccount();
}

var saveUpdateAccount = function () {
    $("#divLoader").show();
    var msg = "";
    var accountID = $("#hndAccountID").val();
    var accountName = $("#txtAccountName").val();
    var website = $("#txtWebsite").val();
    var phone = $("#txtPhone").val();
    var accountType = $("#ddlCompType").val();
    var parentAccount = $("#txtParentAccount").val();
    var employee = $("#txtEmployee").val();
    var accountOwner = $("#hndOwnerID").val();
    var oldaccountOwner = $("#hndOldOwnerID").val();
    var description = $("#txtDescription").val();
    var industry = $("#ddlIndustry").val();
    var billingAddress = $("#txtBillingAddress").val();
    var billingCity = $("#txtBillingCity").val();
    var billingState = $("#txtBillingState").val();
    var billingZip = $("#txtBillingZip").val();
    var billingCountry = $("#txtBillingCountry").val();
    var shippingAddress = $("#txtShippingAddress").val();
    var shippingCity = $("#txtShippingCity").val();
    var shippingState = $("#txtShippingState").val();
    var shippingZip = $("#txtShippingZip").val();
    var shippingCountry = $("#txtShippingCountry").val();
    var pinno = $("#txtPin").val();
    var password = $("#txtPasswordId").val();
    var accountStatus = $("#ddlAccountStatus").val();
    var firstname = $("#txtFirstName").val();
    var lastname = $("#txtLastName").val();
    var takeOffList = $("#chkTakeOffList").is(":checked") ? "1" : "0";
    var closeDate = $("#txtSalesDate").val();
    var createdbyid = $("#hndCreatedByID").val();
    var lastmodbyid = $("#hndLastModifiedByID").val();
    var createdDate = $("#txtCreatedDate").val();
  
    var listcode = $("#txtListCode").val();
    var listcode2 = $("#txtListCode2").val();
    var cdate = $("#txtCancellationDate").val();
    var rdate = $("#txtRenewalDate").val();

    var phone2 = $("#txtPhone2").val();
    var email = $("#txtAccEmail").val();
    //var listcode = $("#ddlAccountStatus").val();
    //var listcode2 = $("#txtFirstName").val();
    var warantee = $("#txtWarranty").val();
    //var caneceldate = $("#txtLastName").val();

    if (accountName == "") {
        msg += " Please enter Company Name.<br />";
    }
    if (website == "") {
        msg += " Please enter website.<br />";
    }

    if (phone == "") {
        msg += " Please enter Phone No.<br />";
    }
    var language = "";

    if (!$("#ddlLeadLanguageAcc").val()) {
        msg += " Please select Langauge <br />";
    }

    var languageArray = [];
    languageArray = $('select#ddlLeadLanguageAcc').val();
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
    //if (description == "") {
    //    msg += " Please enter description.<br />";
    //}
    //if (accountType == 0) {
    //    msg += " Please select Account Type.<br />";
    //}
    if (msg != "") {
        $("#divLoader").hide();
        $.alert({
            title: 'Alert!',
            content: msg,
            type: 'red'
        });
        return;
    }

    var model = {
        AccountID: accountID,
        AccountName: accountName,
        Phone: phone,
        Website: website,
        Type: accountType,
        AccountOwner: accountOwner,
        OldAccountOwner: oldaccountOwner,
        ParentAccount: parentAccount,
        Industry: industry,
        Employee: employee,
        Description: description,
        BillingAddress: billingAddress,
        BillingCity: billingCity,
        BillingState: billingState,
        BillingZip: billingZip,
        BillingCountry: billingCountry,
        ShippingAddress: shippingAddress,
        ShippingCity: shippingCity,
        ShippingState: shippingState,
        ShippingZip: shippingZip,
        ShippingCountry: shippingCountry,
        PinNo: pinno,
        Password: password,
        AccountStatus: accountStatus,
        FirstName: firstname,
        LastName: lastname,
        Language: language,
        TakeOffList: takeOffList,
        SecondaryPhone: phone2,
        Warranty: warantee,
        AccountEmail: email,
        SalesDate: closeDate,
        CreatedById:createdbyid,
        LastModifiedById: lastmodbyid,
        CreatedDate:createdDate,
        LastModifiedDate: lastmodiDate,
        ListCode :listcode,
        ListCode2: listcode2,
        AccountCancellationDate: cdate,
        RenewalDate:rdate,
    };


    $.ajax({
        url: "/AccountPage/SaveUpdateAccounts",
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
                if (response.AccountID != 0) {
                    if ($("#hndAccountID").val() == "0") {
                        $.alert({
                            title: 'Msg!',
                            content: 'Data Saved Successfully.',
                            type: 'blue',
                            buttons: {
                                Ok: function () {
                                    window.location = '/AccountPage/Edit/' + response.AccountID;
                                }
                            }
                        });
                    }
                    else {
                        $("#txtAccountName").val(firstname + ' ' + lastname);
                        $.alert({
                            title: 'Msg!',
                            content: 'Data Saved Successfully.',
                            type: 'blue',
                            buttons: {
                                Ok: function () {
                                    window.location = '/AccountPage/Edit/' + response.AccountID;
                                }
                            }
                        });
                        getHistoryList();
                    }
                }
                else {
                    $.alert({
                        title: 'Alert!',
                        content: "Error occured while saving data.<br/>Please try later.",
                        type: 'red'
                    });
                }
            }
        }
    });

    return false;
}

var deleteAccount = function () {
    var accountID = $("#hndAccountID").val();
    var model = { AccountID: accountID };
    $.confirm({
        title: 'Alert!',
        content: 'Are you sure you want to delete the data?',
        type: 'blue',
        buttons: {
            confirm: function () {
                $.ajax({
                    url: "/AccountPage/DeleteAccountData",
                    type: "post",
                    data: JSON.stringify(model),
                    contentType: "application/json; charset=utf-8", // content type sent to server
                    dataType: "json", //Expected data format from server
                    success: function (response) {
                        if ($.trim(response.error) != "") {
                            $.alert({
                                title: 'Alert!',
                                content: response.error,
                                type: 'red'
                            });
                        } else {
                            $.alert({
                                title: 'Msg!',
                                content: 'Data Deleted Successfully.',
                                type: 'blue',
                                buttons: {
                                    Ok: function () {
                                        window.location = '/AccountPage/AddEdit';
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


function getOwnerList(ownerType) {
   
    var username = $("#txtSearchOwnerName").val();
    var param = { UserName: username }
    $.ajax({
        url: "/UserManagement/GetOwnerList",
        type: "post",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            $("#tblOwner>tbody").empty();
            $.each(response.model, function (index, elementValue) {
                var html = '';
                html += '<tr  class="gradeX" data-value="' + elementValue.UserID + '">';

                html += '<td class="pd-vehicletblText">' + elementValue.UserID + '</td>';
                html += '<td class="pd-vehicletblText"><a href="#"  onclick="addOwner('+ownerType+',' + elementValue.UserID + ',\'' + elementValue.FirstName + " " + elementValue.LastName + '\');">' + elementValue.UserName + '</a></td>';
                html += '<td class="pd-vehicletblText">' + elementValue.FirstName + '</td>';
                html += '<td class="pd-vehicletblText">' + elementValue.LastName + '</td>';

                html += '</tr>';
                $("#tblOwner>tbody").append(html);
            });
        }
    });
}
var addOwner = function (ownerType,userId, userName) {
    if (ownerType == 1) {
        $("#hndOwnerID").val(userId);
        $("#txtLeadOwner").val(userName);
        $("#popOwnerDiv").PopupWindow("close");
    }
    else if (ownerType == 2) {
        $("#hndCreatedByID").val(userId);
        $("#txtCreatedBy").val(userName);
        $("#popOwnerDiv").PopupWindow("close");
    }
    else if (ownerType == 3) {
        $("#hndLastModifiedByID").val(userId);
        $("#txtLastModifiedBy").val(userName);
        $("#popOwnerDiv").PopupWindow("close");
    }
}

var clearAccountField = function () {
    $("#hndAccountID").val(0);
    $("#txtAccountName").val("");
    $("#txtWebsite").val("");
    $("#txtPhone").val("");
    $("#ddlCompType").val(0).trigger('change');
    $("#txtParentAccount").val("");
    $("#txtEmployee").val("");
    $("#txtLeadOwner").val("");
    $("#txtDescription").val("");
    $("#ddlIndustry").val(0).trigger('change');
    $("#txtBillingAddress").val("");
    $("#txtBillingCity").val("");
    $("#txtBillingState").val("");
    $("#txtBillingZip").val("");
    $("#txtBillingCountry").val("");
    $("#txtShippingAddress").val("");
    $("#txtShippingCity").val("");
    $("#txtShippingState").val("");
    $("#txtShippingZip").val("");
    $("#txtShippingCountry").val("");
    $("#hndPNAOID").val(0)
    $("#hndPackId").val(0);
   // $("#hndOwnerType").val(0);
}

var getTransactionHistory = function (accountID) {
    //var accountID = $("#hndAccountID").val();
    var param = { AccountID: accountID }

    $.ajax({
        url: '/AccountPage/GetTransactionHistoryDeatil',
        type: "post",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                $.alert({
                    title: 'Alert!',
                    content: response.error,
                    type: 'blue'
                });
            } else {
                $("#tblTransactionDetails>tbody").empty();
                $.each(response, function (index, elementValue) {
                    if (elementValue.PaymentMethod == 1) {
                        var html = '';
                        html += '<tr class="' + elementValue.StatusText.toLowerCase() + ' " >';
                        html += '<td><label class="control-label">Payment Method : ' + elementValue.PaymentType + ' </label><label class="control-label">Payment Type : ' + elementValue.TransType + '</label><label class="control-label">Card Type : ' + elementValue.CardType + '</label><label class="control-label">TransactionID : ' + elementValue.TransactionID + ' </label><label class="control-label">AuthCode : ' + elementValue.AuthCode + ' </label> <label class="control-label">Charged Amount : $' + parseFloat(elementValue.ChargeAmt).toFixed(2) + '</label> <label class="control-label">Charged Date : ' + elementValue.ChargeDate + '</label></td>';
                        html += '</tr>';
                        $("#tblTransactionDetails>tbody").append(html);
                    }
                    else {
                        var html = '';
                        html += '<tr >';
                        html += '<td><label class="control-label">Payment Method : ' + elementValue.PaymentType + ' </label><label class="control-label">Check Number : ' + elementValue.CardCheckNumber + ' </label> <label class="control-label">Charged Amount : $' + parseFloat(elementValue.ChargeAmt).toFixed(2) + '</label> <label class="control-label">Charged Date : ' + elementValue.ChargeDate + '</label></td>';
                        html += '</tr>';
                        $("#tblTransactionDetails>tbody").append(html);
                    }
                });
            }
        }
    });
}
var getTransactionDetails = function (accountID) {
    //var accountID = $("#hndAccountID").val();
    var param = { AccountID: accountID }

    $.ajax({
        url: '/AccountPage/GetTransactionDetails',
        type: "post",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                $.alert({
                    title: 'Alert!',
                    content: response.error,
                    type: 'blue'
                });
            } else {
                //if (response != "")
                //{
                //    $("#btnAddTrans").addClass("hidden");
                //} else
                //{
                //    $("#btnAddTrans").removeClass("hidden");
                //}
                $("#tblDetailTransaction>tbody").empty();
                $.each(response, function (index, elementValue) {
                    var html = '';
                    html += '<tr data-value="' + elementValue.AOID + '">';
                    html += '<td >' + elementValue.AOID + '</td>';
                    html += '<td>$' + parseFloat(elementValue.TotalAmount).toFixed(2) + '</td>';
                    html += '<td>$' + parseFloat(elementValue.PaidAmount).toFixed(2) + '</td>';
                    //html += '<td>$' + parseFloat(elementValue.BalanceAmount).toFixed(2) + '</td>';
                    html += '<td>$<input type="text" id="txtFullAmt_' + elementValue.AOID + '" value="' + parseFloat(elementValue.BalanceAmount).toFixed(2) + '" style="width:50px" disabled /></td>';
                    html += '<td class="hidden">$<input type="text" id="txtBalAmt_' + elementValue.AOID + '" value="' + parseFloat(elementValue.BalanceAmount).toFixed(2) + '" class="hidden" /></td>';
                    html += '<td> ' + elementValue.ChargeDate + '</td>';
                    html += '<td> ' + elementValue.CardType + '</td>';
                    html += '<td> ' + elementValue.UserName + '</td>';
                    html += '<td style="text-align:center!important; width:200px"><input type="button" class="ibtnUpdate btn btn-md btn-primary " value="View"  id="ibtnUpdate_' + elementValue.AOID + '"  onclick="viewTransOrderDetail(' + elementValue.AOID + ',' + elementValue.RecurringRefund + ',' + elementValue.TransType + ')"> <input type="button" class="ibtnFullPay btn btn-md btn-info " value="Full Pay"  id="ibtnFullPay_' + elementValue.AOID + '"  onclick="fullPay(' + elementValue.AOID + ')"><input type="button" class="ibtnFullPay btn btn-md btn-danger hidden" id="ibtnFullPayNow_' + elementValue.AOID + '" value="Pay Now"  onclick="fullPayNow(' + elementValue.AOID + ')"> <input type="button" class="ibtnCancelPay btn btn-md btn-success hidden" id="ibtnCancelPayNow_' + elementValue.AOID + '" value="Cancel"  onclick="cancelPayNow(' + elementValue.AOID + ')"></td>';

                    html += '</tr>';
                    $("#tblDetailTransaction>tbody").append(html);
                });
            }
        }
    });
}

var fullPay = function (aoid) {
    if ($("#txtFullAmt_" + aoid).val() != 0) {
        $("#ibtnFullPay_" + aoid).addClass("hidden");
        $("#ibtnUpdate_" + aoid).addClass("hidden");
        $("#ibtnCancelPayNow_" + aoid).removeClass("hidden");
        $("#ibtnFullPayNow_" + aoid).removeClass("hidden");
        $("#txtFullAmt_" + aoid).removeAttr("disabled");

        $("#btnSetEditMode").addClass("hidden");
        $("#btnreenable").addClass("hidden");
    }
}
var fullPayNow = function (aoid) {
    var msg = "";
    if (parseFloat($("#txtFullAmt_" + aoid).val()) <= parseFloat($("#txtBalAmt_" + aoid).val())) {
        $("#txtPCChargeAmount").val(parseFloat($("#txtFullAmt_" + aoid).val()).toFixed(2));
        $("#hndPNAOID").val(aoid);
        $("#divPack").addClass("hidden");
        getPCCheckInfo($("#hndAccountID").val());
        $("#popPaymentConsole").PopupWindow("open");
    }
    else {
        msg += "Full Amount Greater than Balance Amount";
    }
    if (msg != "") {
        $.alert({
            title: 'Alert!',
            content: msg,
            type: 'red'
        });
        return;
    }
}
var cancelPayNow = function (aoid) {
    $("#ibtnFullPay_" + aoid).removeClass("hidden");
    $("#ibtnUpdate_" + aoid).removeClass("hidden");
    $("#ibtnCancelPayNow_" + aoid).addClass("hidden");
    $("#ibtnFullPayNow_" + aoid).addClass("hidden");
    $("#hndPNAOID").val(0);
    $("#txtFullAmt_" + aoid).val(parseFloat($("#txtBalAmt_" + aoid).val()).toFixed(2));
    $("#txtFullAmt_" + aoid).attr("disabled", "disabled");
}
var viewTransOrderDetail = function (aoid, recurringRefund, transType) {

    $("#hndAOID").val(aoid);
    if ($("#txtFullAmt_" + aoid).val() == 0) {

        $("#btnSetEditMode").addClass("hidden");
        if (parseInt(recurringRefund) > 0 && (transType == 1)) {
            $("#btnreenable").removeClass("hidden");
        }
    }
    $("#popTransDiv").PopupWindow("open");
    var param = { AOID: aoid }
    $.ajax({
        url: '/AccountPage/GetTransOrderDetails',
        type: "post",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                $.alert({
                    title: 'Alert!',
                    content: response.error,
                    type: 'blue'
                });
            } else {
                $("#tbl_TransOrderDetail>tbody").empty();
                var srno = 1;
                $.each(response, function (index, elementValue) {
                    var html = '';
                    html += '<tr class="' + (elementValue.IsTransDone != "1" ? "can-save" : "") + '" data-value="' + elementValue.CDID + '">';
                    html += '<td>' + srno + '</td>';
                    html += '<td><div data-date="12-02-2018" class="input-append date datepicker"><input type="text" id="dtpChargeDate_' + elementValue.CDID + '" data-date-format="mm/dd/yyyy" class="datepicker span2 ' + (elementValue.IsTransDone != "1" ? "can-change" : "") + '" value="' + elementValue.ChargeDate + '" disabled/><span class="add-on"><i class="icon-th"></i></span></div></td>';
                    html += '<td>$<input type="text" class="span1 ' + (elementValue.IsTransDone != "1" ? "can-change" : "") + '" id="txtChargeAmt_' + elementValue.CDID + '" value="' + parseFloat(elementValue.ChargeAmt).toFixed(2) + '" disabled /></td>';
                    html += '<td> ' + elementValue.TransactionID + '</td>';
                    html += '<td><span id="spStatus_' + elementValue.CDID + '">' + elementValue.Status + '</span></td>';
                    if (elementValue.IsTransDone != "1") {
                        html += '<td><button id="btnSchedule_' + elementValue.CDID + '" class="btn btn-success ' + (elementValue.TransType != '5' ? '' : 'hidden') + '" onclick="setScheduleStatus(5,' + elementValue.CDID + ')" title="Scheduled"><i class="icon-play"></i></button><button id="btnPause_' + elementValue.CDID + '" class="btn btn-danger ' + (elementValue.TransType == '5' ? '' : 'hidden') + '" onclick="setScheduleStatus(0,' + elementValue.CDID + ')" title="Paused"><i class="icon-pause"  ></i></button></td>';
                    } else {
                        html += '<td></td>';
                    }
                    html += '</tr>';
                    srno++;
                    $("#tbl_TransOrderDetail>tbody").append(html);
                });
            }
        }
    });
}
var setControlsInEditMode = function () {
    $("#btnUpdateDateAmount").removeClass("hidden");
    $("#btnCancelUpdateDateAmount").removeClass("hidden");
    $("#btnSetEditMode").addClass("hidden");
    $(".can-change").removeAttr("disabled");
}
var unsetControlsInEditMode = function () {
    $(".can-change").attr("disabled", "disabled");
    $("#btnUpdateDateAmount").addClass("hidden");
    $("#btnCancelUpdateDateAmount").addClass("hidden");
    $("#btnSetEditMode").removeClass("hidden");
}
var updateDateAmountTransaction = function () {
    var model = [];
    $("#tbl_TransOrderDetail tr.can-save").each(function (ix, element) {
        var cid = $(element).attr("data-value");
        var chargeDate = $("#dtpChargeDate_" + cid).val();
        var chargeAmount = $("#txtChargeAmt_" + cid).val();
        var value = { CDID: cid, ChargeDate: chargeDate, ChargeAmt: chargeAmount }
        model.push(value);
    });

    if (model.length == 0) {
        $(".can-change").attr("disabled", "disabled");
        $("#btnUpdateDateAmount").addClass("hidden");
        $("#btnCancelUpdateDateAmount").addClass("hidden");
        $("#btnSetEditMode").removeClass("hidden");
        $.alert({
            title: 'Alert!',
            content: "No Schedules To Update.",
            type: 'red'
        });
        return;

    }

    $.ajax({
        url: '/AccountPage/UpdateDateAmountTransaction',
        type: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                $.alert({
                    title: 'Alert!',
                    content: response.error,
                    type: 'red'
                });
            } else {
                $(".can-change").attr("disabled", "disabled");
                $("#btnUpdateDateAmount").addClass("hidden");
                $("#btnCancelUpdateDateAmount").addClass("hidden");
                $("#btnSetEditMode").removeClass("hidden");
                $.alert({
                    title: 'Alert!',
                    content: "Data Updated Successfully.",
                    type: 'blue'
                });
            }
        }
    });
}

var setScheduleStatus = function (transType, cdID) {
    var param = { CDID: cdID, TransType: transType };
    $.ajax({
        url: '/AccountPage/SetScheduleStatus',
        type: "post",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                $.alert({
                    title: 'Alert!',
                    content: response.error,
                    type: 'red'
                });
            } else {
                $("#btnSchedule_" + cdID).removeClass("hidden");
                $("#btnPause_" + cdID).removeClass("hidden");
                $("#spStatus_" + cdID).text("");
                if (transType == "5") {
                    $("#btnSchedule_" + cdID).addClass("hidden");
                    $("#spStatus_" + cdID).text("Paused");
                }
                else {
                    $("#btnPause_" + cdID).addClass("hidden");
                }
                $.alert({
                    title: 'Alert!',
                    content: "Schedule " + (transType == "5" ? "Paused" : "Started") + " Successfully.",
                    type: 'blue'
                });
            }
        }
    });

}

var getTotalOrderDecal = function (aoid) {
    var param = { AOID: aoid }
    $.ajax({
        url: '/AccountPage/GetTotalOrderDecal',
        type: "post",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                $.alert({
                    title: 'Alert!',
                    content: response.error,
                    type: 'blue'
                });
            } else {
                $("#tblDecalDetail>tbody").empty();
                $.each(response, function (index, elementValue) {
                    var html = '';
                    html += '<tr data-value="' + elementValue.AOID + '">';
                    html += '<td >' + elementValue.AOID + '</td>';
                    html += '<td>' + elementValue.VehicleMakeText + ' </td>';
                    html += '<td>' + elementValue.VehicleTypeText + ' </td>';
                    html += '<td>' + elementValue.VehicleYear + ' </td>';
                    html += '<td> <input type="checkbox" id="chkAddTheft" name="radios" onclick="updateAddTheft(' + elementValue.AOID + ')"/></td>';
                    html += '<td> <input type="checkbox" id="chkDecals" name="radios"  onclick="updateAddDecal(' + elementValue.AOID + ')"/></td>';
                    html += '<td>' + elementValue.NoOfVehicle + ' </td>';
                    html += '<td>' + elementValue.NoOfDecals + '</td>';
                    html += '<td><div><button onclick="editDecal(' + elementValue.AOID + ',' + elementValue.NumberOfRows + ')"><i class="icon-pencil" ></i></button></div></td>';
                    html += '</tr>';
                    $("#tblDecalDetail>tbody").append(html);
                    if (elementValue.IdentityTheft > 0) {
                        $("#chkAddTheft").prop("checked", true);
                    }
                    if (elementValue.AddDecals > 0) {
                        $("#chkDecals").prop("checked", true);
                    }

                    //$("#txtVinNo").val(elementValue.VINNO);
                    $("#txtLicense").val(elementValue.LicensePlate);
                    $("#txtDealer").val(elementValue.DealerShip);
                    $("#txtFinanceCompany").val(elementValue.FinanceCompany);

                });
            }
        }
    });
}

var editDecal = function (aoid, decimalCount) {
    var aoID = aoid;
    $("#hndAOID").val(aoid);
    $("#hndDecalRow").val(decimalCount);
    $("#popDecalsDiv").PopupWindow("open");
    $("#popDecalsDiv").PopupWindow("maximize")
    $("#tblFillDecalDetail>tbody").empty();
    for (var j = 1; j <= parseInt(decimalCount) ; j++) {
        var htmlBody = "";
        htmlBody += '<tr id="vehicleInfo_' + j + '" class="trMediMainRow" data-value="' + j + '">';
        htmlBody += '<td class="span3">' + j + '</td>'
        htmlBody += '<td class="span3"><select id="ddlVehicleMakeUpAcc_' + j + '"></select></td>';
        htmlBody += '<td class="span3"><input type="text" class="span2" id="txtVehicleYearUp_' + j + '" name="txtDecal"   maxlength="200" ></td></td>';
        htmlBody += '<td class="span3"><input type="text" class="span2" id="txtVinnoUp_' + j + '" name="txtDecal"   maxlength="200" ></td></td>';
        htmlBody += '<td class="span3"><input type="text" class="span2" id="txtLicenseUp_' + j + '" name="txtDecal"   maxlength="200" ></td></td>';
        htmlBody += '<td class="span3"><input type="text" class="span2" id="txtDecal_' + j + '" name="txtDecal"   maxlength="200" ></td></td>';
        htmlBody += '<td class="span3"><input type="text" class="span2" id="txtGPSSKU_' + j + '" name="txtGPSSKU"   maxlength="200" ></td>';
        htmlBody += '<td class="span3"><input type="text" class="span2" id="txtGPSDN_' + j + '" name="txtGPSDN"  maxlength="200" ></td>';
        htmlBody += '<td class="span3"><input  class="span1" type="button" class="ibtnUpdate btn btn-md btn-primary " value="Save"  onclick="updateDecalTableRow(' + aoID + ',' + j + ')"><input  class="span1" type="button" class="ibtnDelete btn btn-md btn-primary " value="Delete"  onclick="deleteDecalTableRow(' + aoID + ',' + j + ')"></td>';
        htmlBody += '</tr>';
        $("#tblFillDecalDetail>tbody").append(htmlBody);
        getVehicleMakeUp(j);
    }
    var param = { AOID: aoid };
    if (decimalCount > 0) {
        $.ajax({
            url: '/AccountPage/GetDecalInfoData',
            type: "post",
            data: JSON.stringify(param),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if ($.trim(response.error) != "") {
                    $.alert({
                        title: 'Alert!',
                        content: response.error,
                        type: 'red'
                    });

                } else {
                    if (decimalCount == 4) {
                        $("#txtDecal_1").val(response.returnREData.Decal1);
                        $("#txtGPSSKU_1").val(response.returnREData.GPSSKN1);
                        $("#txtGPSDN_1").val(response.returnREData.GPSDN1);
                        getVehicleMakeUp(1);
                        $("#ddlVehicleMakeUpAcc_1").val(response.returnREData.VehicleMake1).trigger('change');
                        $("#txtVehicleYearUp_1").val(response.returnREData.VehicleYear1);
                        $("#txtVinnoUp_1").val(response.returnREData.VINNNO1);
                        $("#txtLicenseUp_1").val(response.returnREData.LicensePlate1);


                        $("#txtDecal_2").val(response.returnREData.Decal2);
                        $("#txtGPSSKU_2").val(response.returnREData.GPSSKN2);
                        $("#txtGPSDN_2").val(response.returnREData.GPSDN2);
                        getVehicleMakeUp(2);
                        $("#ddlVehicleMakeUpAcc_2").val(response.returnREData.VehicleMake2).trigger('change');
                        $("#txtVehicleYearUp_2").val(response.returnREData.VehicleYear2);
                        $("#txtVinnoUp_2").val(response.returnREData.VINNNO2);
                        $("#txtLicenseUp_2").val(response.returnREData.LicensePlate2);


                        $("#txtDecal_3").val(response.returnREData.Decal3);
                        $("#txtGPSSKU_3").val(response.returnREData.GPSSKN3);
                        $("#txtGPSDN_3").val(response.returnREData.GPSDN3);
                        getVehicleMakeUp(3);
                        $("#ddlVehicleMakeUpAcc_3").val(response.returnREData.VehicleMake3).trigger('change');
                        $("#txtVehicleYearUp_3").val(response.returnREData.VehicleYear3);
                        $("#txtVinnoUp_3").val(response.returnREData.VINNNO3);
                        $("#txtLicenseUp_3").val(response.returnREData.LicensePlate3);

                        $("#txtDecal_4").val(response.returnREData.Decal4);
                        $("#txtGPSSKU_4").val(response.returnREData.GPSSKN4);
                        $("#txtGPSDN_4").val(response.returnREData.GPSDN4);
                        getVehicleMakeUp(4);
                        $("#ddlVehicleMakeUpAcc_4").val(response.returnREData.VehicleMake4).trigger('change');
                        $("#txtVehicleYearUp_4").val(response.returnREData.VehicleYear4);
                        $("#txtVinnoUp_4").val(response.returnREData.VINNNO4);
                        $("#txtLicenseUp_4").val(response.returnREData.LicensePlate4);
                    } else if (decimalCount == 3) {
                        $("#txtDecal_1").val(response.returnREData.Decal1);
                        $("#txtGPSSKU_1").val(response.returnREData.GPSSKN1);
                        $("#txtGPSDN_1").val(response.returnREData.GPSDN1);
                        getVehicleMakeUp(1);
                        $("#ddlVehicleMakeUpAcc_1").val(response.returnREData.VehicleMake1).trigger('change');
                        $("#txtVehicleYearUp_1").val(response.returnREData.VehicleYear1);
                        $("#txtVinnoUp_1").val(response.returnREData.VINNNO1);
                        $("#txtLicenseUp_1").val(response.returnREData.LicensePlate1);


                        $("#txtDecal_2").val(response.returnREData.Decal2);
                        $("#txtGPSSKU_2").val(response.returnREData.GPSSKN2);
                        $("#txtGPSDN_2").val(response.returnREData.GPSDN2);
                        getVehicleMakeUp(2);
                        $("#ddlVehicleMakeUpAcc_2").val(response.returnREData.VehicleMake2).trigger('change');
                        $("#txtVehicleYearUp_2").val(response.returnREData.VehicleYear2);
                        $("#txtVinnoUp_2").val(response.returnREData.VINNNO2);
                        $("#txtLicenseUp_2").val(response.returnREData.LicensePlate2);


                        $("#txtDecal_3").val(response.returnREData.Decal3);
                        $("#txtGPSSKU_3").val(response.returnREData.GPSSKN3);
                        $("#txtGPSDN_3").val(response.returnREData.GPSDN3);
                        getVehicleMakeUp(3);
                        $("#ddlVehicleMakeUpAcc_3").val(response.returnREData.VehicleMake3).trigger('change');
                        $("#txtVehicleYearUp_3").val(response.returnREData.VehicleYear3);
                        $("#txtVinnoUp_3").val(response.returnREData.VINNNO3);
                        $("#txtLicenseUp_3").val(response.returnREData.LicensePlate3);

                    } else if (decimalCount == 2) {
                        $("#txtDecal_1").val(response.returnREData.Decal1);
                        $("#txtGPSSKU_1").val(response.returnREData.GPSSKN1);
                        $("#txtGPSDN_1").val(response.returnREData.GPSDN1);
                        getVehicleMakeUp(1);
                        $("#ddlVehicleMakeUpAcc_1").val(response.returnREData.VehicleMake1).trigger('change');
                        $("#txtVehicleYearUp_1").val(response.returnREData.VehicleYear1);
                        $("#txtVinnoUp_1").val(response.returnREData.VINNNO1);
                        $("#txtLicenseUp_1").val(response.returnREData.LicensePlate1);


                        $("#txtDecal_2").val(response.returnREData.Decal2);
                        $("#txtGPSSKU_2").val(response.returnREData.GPSSKN2);
                        $("#txtGPSDN_2").val(response.returnREData.GPSDN2);
                        getVehicleMakeUp(2);
                        $("#ddlVehicleMakeUpAcc_2").val(response.returnREData.VehicleMake2).trigger('change');
                        $("#txtVehicleYearUp_2").val(response.returnREData.VehicleYear2);
                        $("#txtVinnoUp_2").val(response.returnREData.VINNNO2);
                        $("#txtLicenseUp_2").val(response.returnREData.LicensePlate2);
                    } else if (decimalCount == 1) {
                        $("#txtDecal_1").val(response.returnREData.Decal1);
                        $("#txtGPSSKU_1").val(response.returnREData.GPSSKN1);
                        $("#txtGPSDN_1").val(response.returnREData.GPSDN1);
                        getVehicleMakeUp(1);
                        $("#ddlVehicleMakeUpAcc_1").val(response.returnREData.VehicleMake1).trigger('change');
                        $("#txtVehicleYearUp_1").val(response.returnREData.VehicleYear1);
                        $("#txtVinnoUp_1").val(response.returnREData.VINNNO1);
                        $("#txtLicenseUp_1").val(response.returnREData.LicensePlate1);
                    }
                }
            }
        });
    }
}
var getVehicleMakeUp = function (rowNum) {
    //$("#ddlSattus_" + rowNum).kendoDropDownList({
    //    dataTextField: "text",
    //    dataValueField: "value",
    //    dataSource: typeDataSourceStatusNew,
    //    dataType: "JSON"
    //});
    $("#ddlVehicleMakeUpAcc_" + rowNum).empty();
    $.ajax({
        url: "/Leads/GetVehicleMakeList",
        type: "post",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            $("#ddlVehicleMakeUpAcc_" + rowNum).append("<option value='0'>Select Vehicle Make...</option>");
            $.each(response.model, function (index, elementValue) {
                var opt = "<option value='" + elementValue.Value + "'>" + elementValue.Text + "</option>";
                $("#ddlVehicleMakeUpAcc_" + rowNum).append(opt);
            });
            $("#ddlVehicleMakeUpAcc_" + rowNum).val(0).trigger('change');
        }
    });
}

var getVehicleMakeList = function () {
    $("#ddlVehicleMakeDecAcc").empty();
    $("#ddlVehicleMakeA").empty();
    $.ajax({
        url: "/Leads/GetVehicleMakeList",
        type: "post",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            $("#ddlVehicleMakeDecAcc").append("<option value='0'>Select Vehicle Make...</option>");
            $("#ddlVehicleMakeA").append("<option value='0'>Select Vehicle Make...</option>");
            $.each(response.model, function (index, elementValue) {
                var opt = "<option value='" + elementValue.Value + "'>" + elementValue.Text + "</option>";
                $("#ddlVehicleMakeDecAcc").append(opt);
                $("#ddlVehicleMakeA").append(opt);
            });
            $("#ddlVehicleMakeDecAcc").val(0).trigger('change');
            $("#ddlVehicleMakeA").val(0).trigger('change');
        }
    });
}
var updateDecalTableRow = function (aoid, rowcount) {
    var decalDetaildata = [];
    var srno = rowcount;
    var aoid = aoid;
    var decal = $("#txtDecal_" + rowcount).val();
    var gpsskn = $("#txtGPSSKU_" + rowcount).val();
    var gpsdn = $("#txtGPSDN_" + rowcount).val();

    var vehicleMake = $("#ddlVehicleMakeUpAcc_" + rowcount).val();
    var vehicleYear = $("#txtVehicleYearUp_" + rowcount).val();

    var vinno = $("#txtVinnoUp_" + rowcount).val();
    var licensePlate = $("#txtLicenseUp_" + rowcount).val();
    var model = {
        SRNO: srno,
        AOID: aoid,
        Decal: decal,
        GPSSKN: gpsskn,
        GPSDN: gpsdn,
        VehicleMake: vehicleMake,
        VehicleYear: vehicleYear,
        VINNO: vinno,
        LicensePlate: licensePlate
    }
    $.ajax({
        url: "/AccountPage/UpdateDecal",
        type: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8", // content type sent to server
        dataType: "json", //Expected data format from server
        success: function (response) {
            if ($.trim(response.error) != "") {
                $.alert({
                    title: 'Alert!',
                    content: response.error,
                    type: 'red'
                });
            } else {
                $.alert({
                    title: 'Alert!',
                    content: "Data Updated Successfully.",
                    type: 'blue'
                });
                getHistoryList();

            }
        }
    });

}
var deleteDecalTableRow = function (aoid, rowcount) {
    var decalDetaildata = [];
    var srno = rowcount;
    var aoid = aoid;
    var decal = $("#txtDecal_" + rowcount).val();
    var gpsskn = $("#txtGPSSKU_" + rowcount).val();
    var gpsdn = $("#txtGPSDN_" + rowcount).val();

    var vehicleMake = $("#ddlVehicleMakeUpAcc_" + rowcount).val();
    var vehicleYear = $("#txtVehicleYearUp_" + rowcount).val();

    var vinno = $("#txtVinnoUp_" + rowcount).val();
    var licensePlate = $("#txtLicenseUp_" + rowcount).val();
    var model = {
        SRNO: srno,
        AOID: aoid,
        Decal: decal,
        GPSSKN: gpsskn,
        GPSDN: gpsdn,
        VehicleMake: vehicleMake,
        VehicleYear: vehicleYear,
        VINNO: vinno,
        LicensePlate: licensePlate
    }
    $.ajax({
        url: "/AccountPage/DeleteDecal",
        type: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8", // content type sent to server
        dataType: "json", //Expected data format from server
        success: function (response) {
            if ($.trim(response.error) != "") {
                $.alert({
                    title: 'Alert!',
                    content: response.error,
                    type: 'red'
                });
            } else {
                $.alert({
                    title: 'Alert!',
                    content: "Data Updated Successfully.",
                    type: 'blue'
                });
                getHistoryList();

            }
        }
    });

}

var saveDecalAcc = function () {
    var decalCount = $("#hndDecalRow").val();
    var aoid = $("#hndAOID").val();
    var gpsskn = $("#txtGPSSKUDecAcc").val();
    var gpsdn = $("#txtGPSDNDecAcc").val();
    var decal = $("#txtDecalNumberDecAcc").val();
    var vehicleMake = $("#ddlVehicleMakeDecAcc").val();
    var vehicleYear = $("#txtVehicleYearDecAcc").val();
    var vinno = $("#txtVINNODecAcc").val();
    var licensePlate = $("#txtLicensePlateDecAcc").val();

    var model = {
        Decal: decal,
        GPSSKN: gpsskn,
        GPSDN: gpsdn,
        VehicleMake: vehicleMake,
        VehicleYear: vehicleYear,
        VINNO: vinno,
        LicensePlate: licensePlate,
        AOID: aoid,
        DecalCount: decalCount
    }
    $.ajax({
        url: "/AccountPage/SaveDecal",
        type: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8", // content type sent to server
        dataType: "json", //Expected data format from server
        success: function (response) {
            if ($.trim(response.error) != "") {
                $.alert({
                    title: 'Alert!',
                    content: response.error,
                    type: 'red'
                });
            } else {
                editDecal(response.AOID, parseInt(decalCount) + 1);
                getTotalOrderDecal($("#hndAccountID").val());
                $.alert({
                    title: 'Alert!',
                    content: "Data Saved Successfully.",
                    type: 'blue'
                });
            }
        }
    });

}

var updateVehicleInfo = function () {

    var aoid = $("#hndAOID").val();
    //var vinno = $("#txtVinNo").val();
    var licensePlate = $("#txtLicense").val();
    var dealerShip = $("#txtDealer").val();
    var financeCompany = $("#txtFinanceCompany").val();

    var model = {

        AOID: aoid,
        //VINNo: vinno,
        DealerShip: dealerShip,
        FinanceCompany: financeCompany,
        LicensePlate: licensePlate,
    }
    $.ajax({
        url: "/AccountPage/UpdateVehicleInfo",
        type: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8", // content type sent to server
        dataType: "json", //Expected data format from server
        success: function (response) {
            if ($.trim(response.error) != "") {
                $.alert({
                    title: 'Alert!',
                    content: response.error,
                    type: 'red'
                });
            } else {
                $.alert({
                    title: 'Alert!',
                    content: "Vehicle Info Updated Successfully.",
                    type: 'blue'
                });
            }
        }
    });

}
var getCardCheckInfo = function (accountID) {
    var param = { AccountID: accountID };
    $.ajax({
        url: '/AccountPage/GetCardCheckInfo',
        type: "post",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                $.alert({
                    title: 'Alert!',
                    content: response.error,
                    type: 'blue'
                });
            } else {
                $("#tblCardCheck>tbody").empty();

                $.each(response, function (index, elementValue) {
                    var html = '';
                    html += '<tr data-value="' + elementValue.CCID + '">';
                    html += '<td >' + elementValue.PaymentMethod + '</td>';
                    html += '<td>' + elementValue.CardType + '</td>';
                    html += '<td> ' + elementValue.CardNumber + '</td>';
                    validateNumber();
                    html += '<td> ' + elementValue.CardExpirationMonth + '/' + elementValue.CardExpirationYear + '</td>';

                    html += '<td> ' + elementValue.CheckNumber + '</td>';
                    html += '<td style=text-align:center!important;"><button  onclick="showCardInfo(' + elementValue.IsDefault + ',\'' + elementValue.CardNumberOriginal + '\',\'' + elementValue.CardSecurityCode + '\',\'' + elementValue.CardExpirationMonth + '\',' + elementValue.CardExpirationYear + ')"><i class="icon-pencil" ></i></button></td>';
                    html += '</tr>';
                    $("#tblCardCheck>tbody").append(html);

                });
            }
        }
    });
}
var getPCCheckInfo = function (accountID) {
    var param = { AccountID: accountID };
    $.ajax({
        url: '/AccountPage/GetCardCheckInfo',
        type: "post",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                $.alert({
                    title: 'Alert!',
                    content: response.error,
                    type: 'blue'
                });
            } else {

                $("#tblPCCardDetail>tbody").empty();
                $.each(response, function (index, elementValue) {
                    var html = '';
                    html += '<tr data-value="' + elementValue.CCID + '">';
                    if (elementValue.IsDefault == 1) {
                        html += '<td style="text-align:center!important;"><input  type="radio" class="rdo mainCard" name="radios" onclick="selectCC(' + elementValue.CardNumberOriginal + ',\'' + elementValue.CardSecurityCode + '\',\'' + elementValue.CardExpirationMonth + '\',' + elementValue.CardExpirationYear + ')" checked></td>';
                        $("#hndccnum").val(elementValue.CardNumberOriginal);
                        $("#hndcccode").val(elementValue.CardSecurityCode);
                        $("#hndccmm").val(elementValue.CardExpirationMonth);
                        $("#hndccyy").val(elementValue.CardExpirationYear);
                    } else {
                        html += '<td style="text-align:center!important;"><input  type="radio" class="rdo mainCard" name="radios" onclick="selectCC(' + elementValue.CardNumberOriginal + ',\'' + elementValue.CardSecurityCode + ',\'' + elementValue.CardExpirationMonth + '\',' + elementValue.CardExpirationYear + ')"></td>';
                    }

                    html += '<td >' + elementValue.PaymentMethod + '</td>';
                    //html += '<td>' + elementValue.CardType + '</td>';
                    html += '<td> ' + elementValue.CardNumber + '</td>';
                    html += '<td> ' + elementValue.CardSecurityCode + '</td>';
                    html += '<td> ' + elementValue.CardExpirationMonth + '/' + elementValue.CardExpirationYear + '</td>';

                    html += '</tr>';

                    $("#tblPCCardDetail>tbody").append(html);
                });
            }
        }
    });
}
function selectCC(ccno, cccode, ccmm, ccyy) {
   
    $("#hndccnum").val(ccno);
    $("#hndcccode").val(cccode);
    $("#hndccmm").val(ccmm);
    $("#hndccyy").val(ccyy);
}
var newCCard = function () {
    // $("#divDefCC").removeClass("hidden");
    $("#txtPCCardNumber").val("");
    validateNumber();
    $("#txtPCCardCode").val("");
    $("#ddlPCMonth").val(0).trigger('change');
    $("#ddlPCYear").val(0).trigger('change');
    $("#hndccnum").val("");
    $("#hndcccode").val("");
    $("#hndccmm").val(0);
    $("#hndccyy").val(0);
    $("#popCardDiv").PopupWindow("open");

}
var showCardInfo = function (isdef, ccno, cccode, ccmm, ccyy) {

    // $("#divDefCC").addClass("hidden");
    if (isdef == 1) {
        $("#chkDefaultCard").prop("checked", true);
        $.uniform.update("#chkDefaultCard");
    } else {
        $("#chkDefaultCard").prop("checked", false);
        $.uniform.update("#chkDefaultCard");
    }
    $("#txtPCCardNumber").val(ccno);
    validateNumber();
    var mainStr = $("#txtPCCardNumber").val(),
              vis = mainStr.slice(-4),
                   countNum = '';
    validateNumber();

    for (var i = (mainStr.length) - 4; i > 0; i--) {
        countNum += '*';
    }
    $("#txtPCCardNumber").val(countNum + vis);
    $("#txtPCCardCode").val(cccode);
    $("#ddlPCMonth").val(ccmm).trigger('change');
    $("#ddlPCYear").val(ccyy).trigger('change');
    $("#hndccnum").val(ccno);
    $("#hndcccode").val(cccode);
    $("#hndccmm").val(ccmm);
    $("#hndccyy").val(ccyy);
    $("#popCardDiv").PopupWindow("open");

}

var editCardInfo = function () {
    $("#divLoader").show();
    var msg = "";
    var ccnumber = $("#txtPCCardNumber").val();
    if (!ccnumber) {
        msg += " Please enter Card No.<br />";
    }

    var cmonth = $("#ddlPCMonth").val();
    if (cmonth == 0) {
        msg += " Please select Month <br />";
    }
    var cyr = $("#ddlPCYear").val();
    if (cyr == 0) {
        msg += " Please select Year <br />";
    }

    var cccode = $("#txtPCCardCode").val();
    if (!cccode) {
        msg += " Please Enter Card Security Code <br />";
    }
    var leadid = $("#hndLeadID").val();
    var isDefaultCC = $("#uniform-chkDefaultCard span").hasClass("checked") ? "1" : "0";
    var oldccno = $("#hndccnum").val();
    var oldccscode = $("#hndcccode").val();
    var oldccmonth = $("#hndccmm").val();
    var oldccyear = $("#hndccyy").val();

    var cardType = $("#ddlCardType").val();
 

    if (msg != "") {
        $("#divLoader").hide();
        $("#popPaymentConsole").PopupWindow("unminimize");

        $.alert({
            title: 'Alert!',
            content: msg,
            type: 'red'
        });
        return;
    }


    var param = { isDefault: isDefaultCC, lid: leadid, CCNUM: ccnumber, CCCODE: cccode, CCMM: cmonth, CCYY: cyr, OldCardNumber: oldccno, OldCardSecCode: oldccscode, OldCardMonth: oldccmonth, OldCardYear: oldccyear }
    $.ajax({
        url: '/AccountPage/EditCardCheckDetail',
        type: "post",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#divLoader").hide();
            $("#popCardDiv").PopupWindow("close");

            getCardCheckInfo($("#hndAccountID").val());
            getPCCheckInfo($("#hndAccountID").val());
            getHistoryList();
            $.alert({
                title: 'Alert!',
                content: response.MSG,
                type: 'blue'
            });

        }
    });
}
var newAccount = function () {
    window.location = '/AccountPage/AddEdit';
}
var editAccount = function (id) {
    window.location = '/AccountPage/Edit/' + id;
}
var buildPaganationAccount = function () {
    $("#s2id_ddlRowPerPage").removeClass("hidden");
    $("#ulPaginationAccountDetail").removeClass("hidden");
    $("#s2id_ddlRowPerPageAdv").addClass("hidden");
    $("#ulPaginationAccountDetailAdv").addClass("hidden");
    $("#ulPaginationAccountDetail").addClass("simple-pagination");

    $("#divLoader").show();
    var accountName = $("#txtAccountName").val();
    var rowDisplay = $("#ddlRowPerPage").val();

    var model = {
        AccountName: accountName,
        RowDisplay: rowDisplay
    };
    //alert(JSON.stringify(model));

    $.ajax({
        url: '/AccountPage/GetAccountFilterRangeList',
        type: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#divLoader").hide();
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {
                var pnarray = response.PageNumber.split('|');
                $('#ulPaginationAccountDetail').pagination('updateItems', pnarray[0]);
                $('#ulPaginationAccountDetail').pagination('selectPage', 1);
                $("#lblTotalRecords").text("Total Records : " + pnarray[1]);
                fillAccountTable(1, 1);
            }
        }
    });
}
var fillAccountTable = function (PN, SO) {
    $("#divLoader").show();

    var cToDate = $("#dtCTotDate").val();
    var cFromDate = $("#dtCFromDate").val();
    var accStatus = $("#ddlAccountStatus").val();
    var cresaledt = $("#ddlCreatedSale").val();
    if (SO == 1) {
        var rowDisplay = $("#ddlRowPerPage").val();
        var accountName = $("#txtAccountName").val();
    }
    else if (SO == 2) {
        var rowDisplay = $("#ddlRowPerPageAdv").val();
        var accountName = $("#txtAccName").val();
    }
    var model = {
        AccountName: accountName,
        RowDisplay: rowDisplay,
        PageNumber: PN,
        SearchOption: SO,
        CreatedDate: cFromDate,
        LastModifiedDate: cToDate,
        AccountStatus: accStatus,
        CreatedSale: cresaledt,
    };
    $.ajax({
        url: '/AccountPage/GetAccountsList',
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
                    type: 'blue'
                });
            } else {
                $("#tblAccounts>tbody").empty();
                $.each(response, function (index, elementValue) {
                    var html = '';
                    html += '<tr  class="gradeX" data-value="' + elementValue.AccountID + '">';
                    html += '<td  class="sorting_1 pd-vehicletblText"><a href="#"  onclick="editAccount(' + elementValue.AccountID + ');">' + elementValue.AccountName + '</a></td>';
                    html += '<td class="pd-vehicletblText"><a href="#"  onclick="editAccount(' + elementValue.AccountID + ');"> ' + elementValue.PinNo + '</td>';
                    html += '<td class="pd-vehicletblText"> ' + elementValue.AccountStatusText + '</td>';
                    html += '<td class="pd-vehicletblText"> ' + elementValue.Package + '</td>';
                    html += '<td class="pd-vehicletblText"> ' + parseFloat(elementValue.TotalCost).toFixed(2) + '</td>';
                    html += '<td class="pd-vehicletblText"> ' + elementValue.FirstName + '</td>';
                    html += '<td class="pd-vehicletblText"> ' + elementValue.CreatedDateText + '</td>';
                    html += '<td class="pd-vehicletblText"> ' + elementValue.LastModifiedDateText + '</td>';
                    html += '</tr>';
                    $("#tblAccounts>tbody").append(html);
                });
            }
        }
    });
}
var importToAccount = function () {
    $("#popImportFile").PopupWindow("close");
    $("#divLoader").show();
    $formData = new FormData();
    var $file = document.getElementById('openFileAccountList');
    if ($file.files.length > 0) {
        for (var i = 0; i < $file.files.length; i++) {
            $formData.append('file-' + i, $file.files[i]);
        }
    }
    $.ajax({
        url: "/AccountPage/ImportToAccount",
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
                buildPaganationAccount();
                $.alert({
                    title: 'Alert!',
                    content: response.Msg,
                    type: 'blue'
                });

            }
        }
    });
}
var exportDaily = function () {
    $("#popExportDiv").PopupWindow("close");
    $("#divLoader").show();
    var exportDate = $("#dtExportDate").val();
    var param = { ExportDate: exportDate }
    $.ajax({
        url: "/AccountPage/ExportDailyMail",
        type: "post",
        data: JSON.stringify(param),
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
                saveToDiskExcelFile(response.FileName);
            }
        }
    });
}
var saveToDiskExcelFile = function (fileName) {

    var saveUrl = "/TempFiles/" + fileName;

    var hyperlink = document.createElement('a');
    hyperlink.href = saveUrl;
    hyperlink.target = '_blank';
    hyperlink.download = 'Day Wise Report.csv';

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
//var sendEmail = function () {
//    $("#divLoader").show();
//    var fromEmail = $("#ddlEmailFrom").val();
//    var toEmail = $("#txtToAccountEmail").val();
//    var ccEmail = $("#txtCCAccountEmail").val();
//    var bccEmail = $("#txtBCCAccountEmail").val();
//    var emailSubject = $("#txtAccountSubject").val();
//    var emailMessage = CKEDITOR.instances['TemplateHTML'].getData();
//    var accountID = $("#hndAccountID").val();
//    var attachFile = $("#hndAttachFile").val();
//    var errMsg = "";

//    if (!toEmail) {
//        errMsg += "To Email is required.";
//    }
//    if (!emailSubject) {
//        errMsg += "Subject is required.";
//    }
//    if (!emailMessage) {
//        errMsg += "Message Body is required.";
//    }
//    var model = {
//        FromEmail: fromEmail,
//        ToEmail: toEmail,
//        CCEmail: ccEmail,
//        BCCEmail: bccEmail,
//        EmailSubject: emailSubject,
//        EmailMessage: emailMessage,
//        LeadID: accountID,
//        AttachFile: attachFile
//    };
//    $.ajax({
//        url: '/AccountPage/SendEmail',
//        type: "post",
//        data: JSON.stringify(model),
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (response) {
//            $("#divLoader").hide();
//            if ($.trim(response.error) != "") {
//                $.alert({
//                    title: 'Alert!',
//                    content: response.error,
//                    type: 'red'
//                });
//            } else {
//                $.alert({
//                    title: 'Alert!',
//                    content: 'Email Sent Successfully',
//                    type: 'blue'
//                });
//                $("#txtCCLeadEmail").val("");
//                $("#txtBCCLeadEmail").val("");
//                $("#txtAccountSubject").val("");
//                $("#hndAttachFile").val("");
//                CKEDITOR.instances['TemplateHTML'].setData("");
//            }
//        }
//    });
//}
var sendEmailAttach = function () {
    $("#divLoader").show();
    var fromEmail = $("#ddlEmailFrom").val();
    var toEmail = $("#txtToAccountEmail").val();
    var ccEmail = $("#txtCCAccountEmail").val();
    var bccEmail = $("#txtBCCAccountEmail").val();
    var emailSubject = $("#txtAccountSubject").val();
    var emailMessage = CKEDITOR.instances['TemplateHTML'].getData();
    var accountID = $("#hndAccountID").val();
    var attachFile = $("#hndAttachFile").val();
    var errMsg = "";

    if (!toEmail) {
        errMsg += "To Email is required.";
    }
    if (!emailSubject) {
        errMsg += "Subject is required.";
    }
    if (!emailMessage) {
        errMsg += "Message Body is required.";
    }


    $formData = new FormData();

    $formData.append('FromEmail', fromEmail);
    $formData.append('ToEmails', toEmail);
    $formData.append('CCEmails', ccEmail);
    $formData.append('BCCEmails', bccEmail);
    $formData.append('EmailSubject', emailSubject);
    $formData.append('EmailHTMLBody', emailMessage);
    $formData.append('AGID', accountID);
    $formData.append('PageID', 2);

    var $file = document.getElementById('emailattUpload');
    if ($file.files.length > 0) {
        for (var i = 0; i < $file.files.length; i++) {
            $formData.append('file-' + i, $file.files[i]);
        }
    }

    $.ajax({
        url: "/AccountPage/SendEmailAttach",
        type: 'POST',
        data: $formData,
        dataType: 'json',
        contentType: false,
        processData: false,
        success: function (response) {
            $("#divLoader").hide();
            if ($.trim(response.error) != "") {

            } else {
                if (response.id != 0) {
                    $.alert({
                        title: 'Alert!',
                        content: response.Msg,
                        type: 'blue',

                    });

                }
                else {
                    $.alert({
                        title: 'Error!',
                        content: "Error occured while saving data.<br/>Please try later.",
                        type: 'red'
                    });
                }
            }

        }

    });

    return false;
}
var saveToFolder = function () {
    $("#divLoader").show();
    var accountID = $("#hndAccountID").val();
    $formData = new FormData();
    $formData.append('AccountID', accountID);
    var $file = document.getElementById('openFileSaveList');
    if ($file.files.length > 0) {
        for (var i = 0; i < $file.files.length; i++) {
            $formData.append('file-' + i, $file.files[i]);
        }
    }
    else {
        $("#divLoader").hide();
        $.alert({
            title: 'Alert!',
            content: "Please select file...!",
            type: 'red'
        });
        return;
    }
    $.ajax({
        url: "/AccountPage/SaveToFolder",
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
                $("#fileCount").text(response.Msg);
                $.alert({
                    title: 'Alert!',
                    content: "File Uploaded Successfully.",
                    type: 'blue'
                });

            }
        }
    });
}
var viewFiles = function () {
    $("#divLoader").show();
    $("#popFileDiv").PopupWindow("open");
    var accountID = $("#hndAccountID").val();
    var param = { AccountID: accountID };
    $.ajax({
        url: '/AccountPage/GetAttachedLeadFiles',
        type: "post",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#divLoader").hide();
            if ($.trim(response.error) != "") {
                $.alert({
                    title: 'Alert!',
                    content: response.error,
                    type: 'blue'
                });
            } else {
                $("#tblAccountFiles>tbody").empty();
                $.each(response.returnREData, function (index, elementValue) {
                    var html = '';
                    html += '<tr  data-value="' + elementValue.ID + '">';
                    html += '<td >' + elementValue.OriginalFileName + '</td>';
                    html += '<td >' + elementValue.CreatedBy + '</td>';
                    html += '<td> ' + elementValue.CreatedDate + '</td>';
                    html += '<td><div><button  onclick="deleteFiles(' + elementValue.ID + ');"><i class="icon-trash" ></i></button></div></td>';
                    //html += '<td>'+$("#linkFile").attr("href", baseURL() + "/FileAttachments/" + elementValue.OriginalFileName)+'</td>';
                    html += '<td><a href="/FileAttachments/' + elementValue.SystemFileName + '" class="btn" id="linkFile" >  <i class="icon-download"></i></a></td>';
                    html += '</tr>';
                    $("#tblAccountFiles>tbody").append(html);
                });
            }
        }
    });
}
var deleteFiles = function (id) {
    $("#divLoader").show();
    $.confirm({
        title: 'Alert!',
        content: 'Are you sure you want to delete the data?',
        type: 'red',
        buttons: {
            confirm: function () {
                deleteFileData(id);
            },
            cancel: function () {
                $("#divLoader").hide();
            }
        }
    });
}
var deleteFileData = function (id) {
    $("#divLoader").show();
    var param = { ID: id };
    console.log(JSON.stringify(param));
    $.ajax({
        url: "/AccountPage/DeleteFiles",
        type: "post",
        data: JSON.stringify(param),
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
                $("#fileCount").text(response.Msg);
                $.alert({
                    title: 'Alert!',
                    content: "File Deleted Successfully..!",
                    type: 'red'
                });
            }
        }
    });
    $('#tblAccountFiles tr[data-value="' + id + '"]').remove();
}

var getEmailTemplates = function () {
    $("#divLoader").show();
    $("#tblEmailTemplate>tbody").empty();
    $.ajax({
        url: '/AccountPage/GetEmailTemplates',
        type: "post",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#divLoader").hide();
            if ($.trim(response.error) != "") {
                $.alert({
                    title: 'Alert!',
                    content: response.error,
                    type: 'blue'
                });
            } else {
                $.each(response, function (index, elementValue) {
                    var html = '';
                    html += '<tr>';
                    html += '<td class="text-left"><a href="#"  onclick="getEmailData(' + elementValue.Value + ');">' + elementValue.Text + '</a></td>';
                    html += '</tr>';
                    $("#tblEmailTemplate>tbody").append(html);
                });
                $("#popEmailTemplateDiv").PopupWindow("open");
            }
        }
    });

}
var getEmailData = function (templateID) {
    $("#divLoader").show();
    $("#popEmailTemplateDiv").PopupWindow("close");
    var param = { TemplateID: templateID, AccountID: $("#hndAccountID").val() };
    $.ajax({
        url: "/AccountPage/GetEmailData",
        type: "post",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            $("#divLoader").hide();
            $("#txtAccountSubject").val(response.subject);
            //Email Ed Requests Customer Service
            if (templateID == 15) {

                $("#txtToAccountEmail").val("e.obst@nationaltheftsearchandrecovery.org");
            } else {
                $("#txtToAccountEmail").val($("#txtAccEmail").val());
            }
            $("#hndAttachFile").val(response.attachmentFile);
            CKEDITOR.instances['TemplateHTML'].setData(response.html);
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
                    //  rowCount = 0;
                    $.each(response.vehicleData, function (index, elementValue) {
                        elementValue.SrNo
                        var html = '';
                        html += '<tr class="trVehicleMainRow pd-vehicletblText" data-value="' + elementValue.SrNo + '">';
                        html += '<td class="pd-vehicletblText" data-makeid="' + elementValue.VehicleMake + '">' + elementValue.VehicleMakeText + '</td>';
                        html += '<td class="pd-vehicletblText"  data-typeid="' + elementValue.VehicleType + '">' + elementValue.VehicleTypeText + '</td>';
                        html += '<td class="pd-vehicletblText"  data-vehicleyear="' + elementValue.VehicleYear + '">' + elementValue.VehicleYear + '</td>';
                        html += '<td class="pd-vehicletblText"  data-vinno="' + elementValue.VINNO + '">' + elementValue.VINNO + '</td>';
                        html += '<td class="pd-vehicletblText hidden" data-licenseplate="' + elementValue.LicensePlate + '">' + elementValue.LicensePlate + '</td>';
                        html += '<td class="pd-vehicletblText hidden" data-dealership="' + elementValue.DealerShip + '">' + elementValue.DealerShip + '</td>';
                        html += '<td class="pd-vehicletblText hidden" data-financecompany="' + elementValue.FinanceCompany + '">' + elementValue.FinanceCompany + '</td>';
                        //html += '<td class="pd-vehicletblText"  class="pd-editdeletetbn"><div><button onclick="editTableRow(' + rowcount + ')"><i class="icon-pencil" ></i></button><button  onclick="removeTableRow(' + rowcount + ');"><i class="icon-trash" ></i></button></div></td>';
                        html += '</tr>';
                        $("#tbl_vehicleLead>tbody").append(html);
                        // rowcount += 1;
                    });
                }
            }
        }
    });
}

var ChargePCCard = function (aoid) {

    $("#popPaymentConsole").PopupWindow("minimize");
    $("#divLoader").show();
    var msg = "";
    var ccnumber = $("#hndccnum").val();
    //if (!ccnumber) {
    //    msg += " Please enter Card No.<br />";
    //}

    var cmonth = $("#hndccmm").val();
    //if (cmonth == 0) {
    //    msg += " Please select Month <br />";
    //}
    var cyr = $("#hndccyy").val();
    //if (cyr == 0) {
    //    msg += " Please select Year <br />";
    //}

    var cccode = $("#hndcccode").val();
    //if (!cccode) {
    //    msg += " Please Enter Card Security Code <br />";
    //}

    var chargeamt = $("#txtPCChargeAmount").val();
    if (!chargeamt) {
        msg += " Please Enter Amount <br />";
    }
    if (msg != "") {
        $("#divLoader").hide();
        $("#popPaymentConsole").PopupWindow("unminimize");
        $.alert({
            title: 'Alert!',
            content: msg,
            type: 'red'
        });
        return;
    }


    var aid = $("#hndAccountID").val();
    var lid = $("#hndLeadID").val();
    var vehicleMake = $("#ddlVehicleMakeA").val();
    var vyear = $("#txtVehicleYearA").val();
    var isDefaultCC = $("#uniform-chkDefaultCard span").hasClass("checked") ? "1" : "0";

    var model = {
        CardDetail: $("#txtPCCardName").val(),
        CardNumber: ccnumber,
        CardExpirationMonth: cmonth,
        CardExpirationYear: cyr,
        CardSecurityCode: cccode,
        AccountID: aid,
        LeadID: lid,
        TotalAmount: chargeamt,
        FirstName: $("#txtFirstName").val(),
        LastName: $("#txtLastName").val(),
        BStreet: $("#txtBillingAddress").val(),
        BCity: $("#txtBillingCity").val(),
        BState: $("#txtBillingState").val(),
        BZip: $("#txtBillingZip").val(),
        Phone: $("#txtCCPhone").val(),
        PinNumber: $("#hndPin").val(),
        AOID: $("#hndPNAOID").val(),
        PackageId: $("#hndPackId").val(),
        VehicleMake: vehicleMake,
        //VehicleType: vehicleType,
        VehicleYear: vyear,
        DefaultCreditCard: isDefaultCC,
        EmailID: $("#txtAccEmail").val(),
        SaleDate: $("#txtSalesDate").val(),
    }
    var param = { model: model }
    $.ajax({
        url: "/AccountPage/SavePaymentConsoleTrans",
        method: "post",
        data: model,
        contentType: "application/json; charset=utf-8", // content type sent to server
        dataType: "json", //Expected data format from server
        success: function (response) {
            $("#divLoader").hide();
            if ($.trim(response.error) != "") {

            } else {
                $("#popPaymentConsole").PopupWindow("close");

                $.alert({
                    title: 'Alert!',
                    content: response.Msg,
                    type: 'blue',
                    buttons: {
                        Ok: function () {
                            window.location = '/AccountPage/Edit/' + aid;
                        }
                    }
                });
                getTransactionHistory($("#hndAccountID").val());
                getTransactionDetails($("#hndAccountID").val());
            }
        }
    });
    return false;
}

var viewRefundOff = function () {
    $("#popRefundOff").PopupWindow("open");
    var param = { AccountID: $("#hndAccountID").val(), RVCType: '1' };
    $.ajax({
        url: '/AccountPage/GetRefundVoidInfo',
        type: "post",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                $.alert({
                    title: 'Alert!',
                    content: response.error,
                    type: 'blue'
                });
            } else {
                $("#tbl_RefundOff>tbody").empty();
                $.each(response, function (index, elementValue) {
                    var html = '';
                    html += '<tr  class="gradeX">';
                    html += '<td> <input id="chkRefundCheck" type="checkbox" name="radios" class="rdCharge" value="' + elementValue.CDID + '" onclick="changeRefund(' + elementValue.CDID + ')"/></td>';
                    html += '<td >' + (index + 1).toString() + '</td>';
                    html += '<td>' + elementValue.ChargeDate + '</td>';
                    html += '<td>' + elementValue.PackageDetails + '</td>';
                    //html += '<td>$' + parseFloat(elementValue.ChargeAmt).toFixed(2) + '</td>';
                    html += '<td>$<input type="text" id="txtRefundAmt_' + elementValue.CDID + '" value="' + parseFloat(elementValue.ChargeAmt).toFixed(2) + '" style="width:40px" disabled /></td>';
                    html += '<td> ' + elementValue.TransactionID + '</td>';
                    html += '<td> ' + elementValue.Status + '</td>';
                    html += '</tr>';
                    $("#tbl_RefundOff>tbody").append(html);
                });
            }

        }
    });
}
function changeRefund(cid) {
    if ($('.rdCharge').is(':checked')) {
        // var valccid = $('#chkRefundCheck').attr("value");
        $("#txtRefundAmt_" + cid).removeAttr("disabled");

    }
    else {
        $("#txtRefundAmt_" + cid).attr('disabled', 'disabled');
    }
}
function updateAddDecal(aoid) {
    if ($('#chkDecals').is(':checked')) {
        updateAddDecalID(aoid, 2, 1);
    }
    else {
        updateAddDecalID(aoid, 2, 0);
    }
}
function updateAddTheft(aoid) {
    if ($('#chkAddTheft').is(':checked')) {
        updateAddDecalID(aoid, 1, 1);
    }
    else {
        updateAddDecalID(aoid, 1, 0);
    }
   
}
function updateAddDecalID(aoid, dt,dtvalue) {
    var model = { AOID: aoid, DecTheft: dt, DecTheftValue: dtvalue };
    $.ajax({
        url: '/AccountPage/UpdateDecalTheft/',
        type: "post",
        contentType: "application/json utf-8",
        data: JSON.stringify(model),
        dataType: "JSON",
        success: function (response) {
            $.alert({
                title: 'Alert!',
                content: response.Msg,
                type: 'blue'
            });
            getTotalOrderDecal($("#hndAccountID").val());
        }
    });
}

var viewVoidOpp = function () {
    $("#popVoid").PopupWindow("open");
    var param = { AccountID: $("#hndAccountID").val(), RVCType: '1' };
    $.ajax({
        url: '/AccountPage/GetRefundVoidInfo',
        type: "post",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                $.alert({
                    title: 'Alert!',
                    content: response.error,
                    type: 'blue'
                });
            } else {
                $("#tbl_Void>tbody").empty();
                $.each(response, function (index, elementValue) {
                    var html = '';
                    html += '<tr  class="gradeX">';
                    html += '<td> <input type="checkbox" name="radios" class="rdCharge" value="' + elementValue.CDID + '" /></td>';
                    html += '<td >' + (index + 1).toString() + '</td>';
                    html += '<td>' + elementValue.ChargeDate + '</td>';
                    html += '<td>' + elementValue.PackageDetails + '</td>';
                    html += '<td>$' + parseFloat(elementValue.ChargeAmt).toFixed(2) + '</td>';
                    html += '<td> ' + elementValue.TransactionID + '</td>';
                    html += '<td> ' + elementValue.Status + '</td>';
                    html += '</tr>';
                    $("#tbl_Void>tbody").append(html);
                });
            }
        }
    });
}

var viewChrageOpp = function () {

    $("#popCharge").PopupWindow("open");
    var param = { AccountID: $("#hndAccountID").val(), RVCType: '2' };
    $.ajax({
        url: '/AccountPage/GetRefundVoidInfo',
        type: "post",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                $.alert({
                    title: 'Alert!',
                    content: response.error,
                    type: 'blue'
                });
            } else {
                $("#tbl_charge>tbody").empty();
                $.each(response, function (index, elementValue) {
                    var html = '';
                    html += '<tr  class="gradeX">';
                    html += '<td> <input type="checkbox" name="radios" class="rdCharge"  onclick="selectMultCharge(' + elementValue.ChargeAmt + ')" value="' + elementValue.CDID + '" /></td>';
                    html += '<td >' + (index + 1).toString() + '</td>';
                    html += '<td>' + elementValue.ChargeDate + '</td>';
                    html += '<td>$' + parseFloat(elementValue.ChargeAmt).toFixed(2) + '</td>';
                    html += '<td> ' + elementValue.TransactionID + '</td>';
                    html += '<td> ' + elementValue.Status + '</td>';
                    html += '</tr>';
                    $("#tbl_charge>tbody").append(html);
                });

            }
        }
    });
}
var totalcharge = 0.00;
function selectMultCharge(pprice) {

    $('.addpackage').each(function (i, obj) {
        if ($(obj).is(':checked')) {
            var selcharge = $(obj).attr("value");
            totalcharge += parseFloat(pprice).toFixed(2);
            console.log(totalcharge);
        }
    });
}
var ChargeOppTrans = function (rvctype) {

    $("#popCharge").PopupWindow("minimize");
    $("#popRefundOff").PopupWindow("minimize");
    $("#popVoid").PopupWindow("minimize");
    $("#divLoader").show();

    var model = []

    var ccid = "";
    var chargeamt = "";

    $('.rdCharge').each(function (i, obj) {
        if ($(obj).is(':checked')) {
            var valccid = $(obj).attr("value");

            var refmat = 0;
            if (rvctype == 2) {
                refmat = $("#txtRefundAmt_" + valccid).val();
            } else {
                refmat = 0;
            }
            
            if (!ccid) {
                ccid = valccid;
                chargeamt = refmat;
            }
            else {
                ccid += "," + valccid;
                chargeamt += "," + refmat;
            }
            if (rvctype == 4) {
                chargeamt = 0;
            }
        }
    });

    var msg = "";

    if (!ccid) {
        msg += " Please Select Transaction.<br />";
    }

    if (msg != "") {
        $("#divLoader").hide();
        $("#popCharge").PopupWindow("unminimize");
        $("#popRefundOff").PopupWindow("unminimize");
        $("#popVoid").PopupWindow("unminimize");
        $.alert({
            title: 'Alert!',
            content: msg,
            type: 'red'
        });
        return;
    }

    var model = {
        CDID: ccid,
        RVCType: rvctype,
        PinNumber: $("#hndPin").val(),
        RefundAmt: chargeamt,
    }
    var param = { model: model }
    $.ajax({
        url: "/AccountPage/SaveRefundVoidInfo",
        method: "post",
        data: model,
        contentType: "application/json; charset=utf-8", // content type sent to server
        dataType: "json", //Expected data format from server
        success: function (response) {
            $("#popCharge").PopupWindow("close");
            $("#popRefundOff").PopupWindow("close");
            $("#popVoid").PopupWindow("close");
            $("#divLoader").hide();
            if ($.trim(response.error) != "") {

            } else {
                $("#popCharge").PopupWindow("close");
                $.alert({
                    title: 'Alert!',
                    content: response.Msg,
                    type: 'blue'
                });
                window.location = '/AccountPage/Edit/' + $("#hndAccountID").val();
                getTransactionHistory($("#hndAccountID").val());
                getTransactionDetails($("#hndAccountID").val());

            }
        }
    });
    return false;
}

var ChargeMultiTrans = function () {
    $("#popCharge").PopupWindow("minimize");
    $("#divLoader").show();
    var model = []

    var ccid = "";
    var chargeamt = "";

    $('.rdCharge').each(function (i, obj) {
        if ($(obj).is(':checked')) {
            var valccid = $(obj).attr("value");

            if (!ccid) {
                ccid = valccid;
                // chargeamt = refmat;
            }
            else {
                ccid += "," + valccid;
                //chargeamt += "," + refmat;
            }
        }
    });

    var msg = "";

    if (!ccid) {
        msg += " Please Select Transaction.<br />";
    }

    if (msg != "") {
        $("#divLoader").hide();
        $("#popCharge").PopupWindow("unminimize");
        $.alert({
            title: 'Alert!',
            content: msg,
            type: 'red'
        });
        return;
    }

    var model = {
        CDID: ccid,
        RVCType: 1,
        PinNumber: $("#hndPin").val(),
        RefundAmt: 0,
    }
    var param = { model: model }
    $.ajax({
        url: "/AccountPage/SaveChargeInfo",
        method: "post",
        data: model,
        contentType: "application/json; charset=utf-8", // content type sent to server
        dataType: "json", //Expected data format from server
        success: function (response) {
            $("#popCharge").PopupWindow("close");
            $("#divLoader").hide();
            if ($.trim(response.error) != "") {

            } else {
                $("#popCharge").PopupWindow("close");
                $.alert({
                    title: 'Alert!',
                    content: response.Msg,
                    type: 'blue'
                });
                window.location = '/AccountPage/Edit/' + $("#hndAccountID").val();
                getTransactionHistory($("#hndAccountID").val());
                getTransactionDetails($("#hndAccountID").val());
            }
        }
    });
    return false;
}
var ReenableRefund = function () {
    var aoid = $("#hndAOID").val();

    $("#divLoader").show();

    var model = {
        AOID: aoid,
    }

    $.ajax({
        url: "/AccountPage/ReenableRefund",
        method: "post",
        data: model,
        contentType: "application/json; charset=utf-8", // content type sent to server
        dataType: "json", //Expected data format from server
        success: function (response) {
            $("#popCharge").PopupWindow("close");
            $("#divLoader").hide();
            if ($.trim(response.error) != "") {

            } else {
                $("#popCharge").PopupWindow("close");
                $.alert({
                    title: 'Alert!',
                    content: response.Msg,
                    type: 'blue'
                });
                $("#btnreenable").addClass("hidden");
                $("#btnSetEditMode").removeClass("hidden");
                viewTransOrderDetail($("#hndAOID").val());
                getTransactionHistory($("#hndAccountID").val());
                getTransactionDetails($("#hndAccountID").val());

            }
        }
    });
    return false;
}
function getChatist() {
    var accountID = $("#hndAccountID").val();

    var param = { AccountID: accountID, PageId: 1 };
    $.ajax({
        url: '/Chatter/GetChats',
        type: "post",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {
                //$("#tblChat>tbody").empty();
                //$.each(response, function (index, elementValue) {
                //    var html = '';
                //    html += '<tr data-value="' + elementValue.CID + '">';
                //    html += '<td><span style="color:#ff6666;">' + elementValue.UserName + '</span> - ' + elementValue.CreatedDate + '</td>';
                //    html += '<tr><td><a href="javascript:void(0);"  onclick="getChat(' + elementValue.CID + ');">' + elementValue.Body + '</a><br/> Seen By: (' + elementValue.ViewedCount + ')' + elementValue.ViewedBy + '</td></tr>';
                //    html += '</tr>';
                //    $("#tblChat>tbody").append(html);
                //});

                $("#tblChat").empty();
                $.each(response, function (index, elementValue) {
                    var html = '';
                    html += '<li data-value="' + elementValue.CID + '">';
                    html += '<div class="article-post"><span class="user-info">' + elementValue.UserName + ' - ' + elementValue.CreatedDate + '</span></div>';
                    html += '<p><a href="javascript:void(0);"  onclick="getChat(' + elementValue.CID + ');">' + elementValue.Body + '</a></p>';
                    html += '<div class="article-post"><span class="user-info">Seen By: (' + elementValue.ViewedCount + ')' + elementValue.ViewedBy + '</span></div>';
                    html += '</li>';
                    $("#tblChat").append(html);
                });
            }
        }
    });
}
function getChat(id) {
    $("#hndCID").val(id);

    var param = { ID: id }
    $.ajax({
        url: '/Chatter/GetChatDet',
        type: "post",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {

                $("#txtChatTitle").val(response.Title);
                $("#txtChatBody").val(response.Body);
                // CKEDITOR.instances['txtChatBody'].setData(response.Body);
                $("#txtChatLink").val(response.LinkUrl);
                $("#ddlChatType").val(response.Type);
                $("#chatterUploadDiv").addClass("hidden");
                $("#linkChatFile").removeClass("hidden");
                $("#linkChatFile").text(response.OriginalFileName);
                $("#linkChatFile").attr("href", baseURL() + "/FileAttachments/" + response.SystemFileName);
                $("#popChatDet").PopupWindow("open");
                $("#btnDeletechat").removeClass("hidden");
            }
        }
    });
}
var setLanguageAccount = function (langauge) {
    $("#ddlLeadLanguageAcc").val(langauge.split(","));
}
var saveUpdateChat = function () {
    $("#divLoader").show();
    var msg = "";
    var accountID = $("#hndAccountID").val();
    var cid = $("#hndCID").val();
    var title = $("#txtChatTitle").val();
    var notes = $("#txtChatBody").val();
    // var notes = CKEDITOR.instances['txtChatBody'].getData();
    var linkurl = $("#txtChatLink").val();
    var ctype = $("#ddlChatType").val();

    if (notes == "") {
        msg += " Please enter Chat Details .<br />";
    }
    //if (notes == "") {
    //    msg += " Please enter Notes Details .<br />";
    //}

    if (msg != "") {
        $.alert({
            title: 'Alert!',
            content: msg,
            type: 'red'
        });
        return;
    }

    $formData = new FormData();

    $formData.append('CID', cid);
    $formData.append('Title', title);
    $formData.append('Body', notes);
    $formData.append('Type', ctype);
    $formData.append('LinkUrl', linkurl);
    $formData.append('AccountID', accountID);

    var $file = document.getElementById('chatterUpload');
    if ($file.files.length > 0) {
        for (var i = 0; i < $file.files.length; i++) {
            $formData.append('file-' + i, $file.files[i]);
        }
    }

    //var model = {
    //    CID: cid,
    //    Title: title,
    //    Body: notes,
    //    Type:ctype,
    //    LinkUrl: linkurl,
    //    AccountId: accountID,
    //}
    // var param = { model: model }
    $.ajax({
        url: "/Chatter/SaveUpdateChat",
        type: 'POST',
        data: $formData,
        dataType: 'json',
        contentType: false,
        processData: false,
        success: function (response) {
            $("#divLoader").hide();
            if ($.trim(response.error) != "") {

            } else {
                if (response.id != 0) {
                    $.alert({
                        title: 'Alert!',
                        content: response.Msg,
                        type: 'blue',

                    });
                    getChatist();
                    $("#popChatDet").PopupWindow("close");
                }
                else {
                    $.alert({
                        title: 'Error!',
                        content: "Error occured while saving data.<br/>Please try later.",
                        type: 'red'
                    });
                }
            }

        }

    });

    return false;
}
var deleteChat = function () {
    $("#divLoader").show();

    var cid = $("#hndCID").val();

    var param = { CID: cid }
    $.ajax({

        url: "/Chatter/DeleteChat",
        type: "post",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#divLoader").hide();
            $.alert({
                title: 'Alert!',
                content: response.MSG,
                type: 'blue',

            });
            getChatist();
            $("#popChatDet").PopupWindow("close");


        }

    });

    return false;
}
var clearDecal = function () {
    $("#txtGPSSKUDecAcc").val("");
    $("#txtGPSDNDecAcc").val("");
    $("#txtDecalNumberDecAcc").val("");
    $("#ddlVehicleMakeDecAcc").val(0).trigger('change');
    $("#txtVehicleYearDecAcc").val("");
    $("#txtVINNODecAcc").val("");
    $("#txtLicensePlateDecAcc").val("");
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
function getEmailHistory() {
    var accountID = $("#hndAccountID").val();

    var param = { AccountID: accountID, PageID: 2 };
    $.ajax({
        url: '/AccountPage/GetEmailHistory',
        type: "post",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {
                $("#popEmaillHistoryDiv").PopupWindow("open");
                $("#tblEmailHistory>tbody").empty();
                $.each(response, function (index, elementValue) {
                    var html = '';
                    html += '<tr data-value="' + elementValue.ID + '">';
                    html += '<td>' + elementValue.CreatedDate + '</td>';
                    html += '<td><a href="javascript:void(0);"  onclick="getEmail(' + elementValue.ID + ');">' + elementValue.EmailSubject + '</a></td>';
                    html += '</tr>';
                    $("#tblEmailHistory>tbody").append(html);
                });

            }
        }
    });
}
function getEmail(id) {
    $("#popEmaillHistoryDiv").PopupWindow("close");
   // $("#btnSendEmail").addClass("hidden");
    $("#hndCID").val(id);

    var param = { ID: id }
    $.ajax({
        url: '/AccountPage/GetEmailDet',
        type: "post",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {

                $("#ddlEmailFrom").val(response.FromEmail);
                $("#txtToAccountEmail").val(response.ToEmails);
                CKEDITOR.instances['TemplateHTML'].setData(response.EmailHTMLBody);
                $("#txtCCAccountEmail").val(response.CCEmails);
                $("#txtBCCAccountEmail").val(response.BCCEmails);
                //$("#chatterUploadDiv").addClass("hidden");
                //$("#linkChatFile").removeClass("hidden");
                $("#txtAccountSubject").val(response.EmailSubject);
                $("#linkEmailFile").removeClass("hidden");
                $("#emailUploadDiv").addClass("hidden");
                $("#linkEmailFile").text(response.OriginalFileName);
                $("#linkEmailFile").attr("href", baseURL() + "/AttachmentFiles/" + response.AttachedFileName);
            }
        }
    });
}
function sendNewEmail() {
   // $("#btnSendEmail").removeClass("hidden");

    $("#txtCCAccountEmail").val("");
    $("#txtBCCAccountEmail").val("");
    $("#txtAccountSubject").val("");
    $("#hndAttachFile").val("");
    CKEDITOR.instances['TemplateHTML'].setData("");
    $("#linkEmailFile").addClass("hidden");
    $("#emailUploadDiv").removeClass("hidden");
    $("#linkEmailFile").text("");
}

function advSearch() {
    $("#popAdvSearch").PopupWindow("open");
}
var buildAdvPaganationAccount = function () {
    $("#s2id_ddlRowPerPage").addClass("hidden");
    $("#ulPaginationAccountDetail").addClass("hidden");
    $("#s2id_ddlRowPerPageAdv").removeClass("hidden");
    $("#ulPaginationAccountDetailAdv").removeClass("hidden");
    $("#ulPaginationAccountDetail").removeClass("simple-pagination");


    $("#divLoader").show();
    var accountName = $("#txtAccName").val();
    var rowDisplay = $("#ddlRowPerPageAdv").val();
    var cToDate = $("#dtCTotDate").val();
    var cFromDate = $("#dtCFromDate").val();
    var accStatus = $("#ddlAccountStatus").val();
    var cresaledt = $("#ddlCreatedSale").val();

    var model = {
        AccountName: accountName,
        RowDisplay: rowDisplay,
        CreatedDate: cFromDate,
        LastModifiedDate: cToDate,
        AccountStatus: accStatus,
        CreatedSale: cresaledt,
    };
    //alert(JSON.stringify(model));

    $.ajax({
        url: '/AccountPage/GetAccountAdvSearch',
        type: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#divLoader").hide();
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {
                var pnarray = response.PageNumber.split('|');
                $('#ulPaginationAccountDetailAdv').pagination('updateItems', pnarray[0]);
                $('#ulPaginationAccountDetailAdv').pagination('selectPage', 1);
                $("#lblTotalRecords").text("Total Records : " + pnarray[1]);
                $("#popAdvSearch").PopupWindow("close");
                fillAccountTable(1, 2);
            }
        }
    });
}

var getPackageList = function () {
    $("#ddlPackages").empty();
    $("#ddlPackagesT").empty();
    var packageName = "";
    var rowDisplay = 25;
    var PN = 1;
    var model = {
        Package: packageName,
        RowDisplay: rowDisplay,
        PageNumber: PN
    };
    $.ajax({
        url: '/Package/GetAllPackageList',
        type: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#ddlPackages").append("<option value='0'>Select Package...</option>");
            $("#ddlPackagesT").append("<option value='0'>Select Package...</option>");
            $.each(response.model, function (index, elementValue) {
                var packid = elementValue.PackageID + "|" + elementValue.Price;
                var opt = "<option value='" + packid + "'>" + elementValue.Package + "</option>";
                $("#ddlPackages").append(opt);
                $("#ddlPackagesT").append(opt);
            });
            $("#ddlPackages").val(0).trigger('change');
            $("#ddlPackagesT").val(0).trigger('change');
        }
    });
}
var getHistoryList = function () {
    //var accountID = $("#hndAccountID").val();
    var accountID = $("#hndAccountID").val();
    var leadID = $("#hndLeadID").val();
    var param = { AccountID: accountID, LeadID: leadID};
    $.ajax({
        url: '/AccountPage/HistoryDetail',
        type: "post",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            if ($.trim(response.error) != "") {
            } else {
                $("#tblHistoryDetail").empty();
                $.each(response, function (index, elementValue) {
                    var html = '';
                    html += '<li>';
                    html += '<div class="article-post"><span class="user-info">' + elementValue.UserName + ' - ' + elementValue.UpdateDate + '</span></div>';
                    html += '<p>' + elementValue.UpDateDetail + '</p>';
                    html += '</li>';
                    $("#tblHistoryDetail").append(html);
                });
            }
        }
    });
}
var validateNumber = function () {
    var result = $('#txtPCCardNumber').validateCreditCard();
    $('#txtPCCardNumber').removeClass();
    (result.card_type == null ? $('#txtPCCardNumber').removeClass().addClass("span4 card_number") : $('#txtPCCardNumber').addClass("span4 card_number " + result.card_type.name));
    (result.valid ? $('#txtPCCardNumber').addClass().addClass("valid") : $('#txtPCCardNumber').removeClass("valid"));

    var typeOfCard = (result.card_type != null ? result.card_type.type_of_card : "0");
    console.log(result.card_type);
    $("#ddlCardType").val(typeOfCard).trigger('change');
}

function saveNewTrans() {
    var accid = $("#hndAccountID").val();
    var aoid = $("#hndAOID").val();
    var chargedate = $("#txtAChargeDate").val();
    var chargeamt = $("#txtChargeAmt").val();
    var status = $("#ddlStatus").val();
    var cardCheckNumber = $("#txtCardNumber").val();
    var authCode = $("#txtAuthCode").val();
    var transactionID = $("#txtTransactionID").val();
    var transType = $("#ddlTransType").val();
    var leadid = $("#hndLeadID").val();
    var pinno = $("#txtPin").val();
    // var statusText = $("#txtStatusText").val();
    // var transactionID = $("#txtChargeNo").val();
    var model = {
        AOID: aoid,
        ChargeDate: chargedate,
        Status: status,
        ChargeAmt: chargeamt,
        CardCheckNumber: cardCheckNumber,
        AuthCode: authCode,
        TransactionID: transactionID,
        TransType: transType,
        AccountID: accid,
        PackageId: $("#hndPackId").val(),
        LeadID:leadid ,
        PinNo: pinno,
    }
    $.ajax({
        url: "/AccountPage/SaveNewTrans",
        type: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8", // content type sent to server
        dataType: "json", //Expected data format from server
        success: function (response) {
            if ($.trim(response.error) != "") {
                $.alert({
                    title: 'Alert!',
                    content: response.error,
                    type: 'red'
                });
            } else {
                viewTransOrderDetail($("#hndAOID").val());
                getTransactionDetails($("#hndAccountID").val());
                getTransactionHistory($("#hndAccountID").val());
                $.alert({
                    title: 'Alert!',
                    content: "Transaction Added Successfully.",
                    type: 'blue'
                });
            }
        }
    });

}