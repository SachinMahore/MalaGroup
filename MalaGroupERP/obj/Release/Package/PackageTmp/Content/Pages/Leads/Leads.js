var search = function () {
    window.location = '/Leads/Index';
}
var saveUpdateLead = function () {
    $("#divLoader").show();
    var msg = "";
    var leadID = $("#hndLeadID").val();
    var leadStatus = $("#ddlLeadStatus").val();
    var leadOwner = $("#hndOwnerID").val();
    var name = $("#txtName").val();
    var primaryPhone = $("#txtPhone1").val();
    var secondaryPhone = $("#txtPhone2").val();
    var leadEmail = $("#txtLeadEmail").val();
    var listCode = $("#txtListCode").val();
    var expirationDate = $("#dtExpiryDate").val();
    var warranty = $("#txtWarranty").val();
    var language = "";
    var listCode2 = $("#txtListCode2").val();
    var street = $("#txtStreet").val();
    var city = $("#txtCity").val();
    var state = $("#txtState").val();
    var zip = $("#txtZip").val();
    var country = $("#txtCountry").val();
    var password = $("#txtPasswordId").val();
    var pinNo = $("#txtPin").val();
    var takeOffList = $("#chkTakeOffList").is(":checked") ? "1" : "0";
    var languageArray = [];
    languageArray = $('select#ddlLeadLanguage').val();
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
    //if (name == "") {
    //    msg += " Please enter  Name.<br />";
    //}


    //if (primaryPhone == "") {
    //    msg += " Please enter Primary Phone No.<br />";
    //}

    //if (leadEmail == "") {
    //    msg += " Please enter Lead Email.<br />";
    //}
    //if (leadEmail != "") {
    //    if (!validateEmail(leadEmail)) {
    //        msg = msg + "Invalid Lead email address.<br/>"
    //    }
    //}

    if (leadStatus == 0) {
        msg += " Please select Lead Status.<br />";
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

    var model = {
        LeadID: leadID,
        LeadEmail: leadEmail,
        LeadStatus: leadStatus,
        LeadOwner: leadOwner,
        Name: name,
        PrimaryPhone: primaryPhone,
        SecondaryPhone: secondaryPhone,
        LeadEmail: leadEmail,
        ListCode: listCode,
        ExpirationDate: expirationDate,
        Warranty: warranty,
        Language: language,
        ListCode2: listCode2,
        Street: street,
        City: city,
        State: state,
        Zip: zip,
        Country: country,
        Password: password,
        PinNo: pinNo,
        VehicleLeadList: vehicleDetailInfo,
        TakeOffList: takeOffList
    }
    $.ajax({
        url: "/Leads/SaveUpdateLead",
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
                if (response.LeadID != 0) {
                    if ($("#hndLeadID").val() == "0") {
                        $.alert({
                            title: 'Msg!',
                            content: 'Data Saved Successfully.',
                            type: 'blue',
                            buttons: {
                                Ok: function () {
                                    window.location = '/Leads/Edit/' + response.LeadID;
                                }
                            }
                        });
                    }
                    else {
                        $.alert({
                            title: 'Alert!',
                            content: "Data Updated Successfully.",
                            type: 'blue'
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
var deleteLeadInfo = function () {
    var leadID = $("#hndLeadID").val();
    var model = { LeadID: leadID };
    $.confirm({
        title: 'Alert!',
        content: 'Are you sure you want to delete the data?',
        type: 'blue',
        buttons: {
            confirm: function () {
                $.ajax({
                    url: "/Leads/DeleteLeadData",
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
                                        window.location = '/Leads/AddEdit';
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
var rowcount = 1;
var saveVehicleLead = function () {

    var msg = "";
    var vehicleM = $('#ddlVehicleMake').select2('data');
    var vehicleMakeText = vehicleM.text;
    var vehicleMake = vehicleM.id;
    var vehicleT = $('#ddlVehicleType').select2('data');
    var vehicleTypeText = vehicleT.text;
    var vehicleType = vehicleT.id;
    var vehicleYear = $("#txtVehicleYear").val();
    var vinno = $("#txtVinNo").val();
    var licensePlate = $("#txtLicense").val();
    var dealerShip = $("#txtDealer").val();
    var financeCompany = $("#txtFinanceCompany").val();
    if (vehicleMake == 0) {
        msg += " Please Select Vehicle Make.<br />";
    }
    
    //if (vehicleYear == 0) {
    //    msg += " Please  Enter Vehicle Year.<br />";
    //}
    //if (vinno == 0) {
    //    msg += " Please Enter VIN No.<br />";
    //}
    //if (licensePlate == 0) {
    //    msg += " Please Enter License Plate No.<br />";
    //}
    //if (dealerShip == 0) {
    //    msg += " Please Enter Dealership Name.<br />";
    //}
    //if (financeCompany == 0) {
    //    msg += " Please Enter Finance Company Name.<br />";
    //}
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
var setLanguage = function (langauge) {
    $("#ddlLeadLanguage").val(langauge.split(","));
}
var getVehicleMakeList = function () {
    $("#ddlVehicleMake").empty();
    $("#ddlVehicleMakeA").empty();
    $.ajax({
        url: "/Leads/GetVehicleMakeList",
        type: "post",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            $("#ddlVehicleMake").append("<option value='0'>Select Vehicle Make...</option>");
            $("#ddlVehicleMakeA").append("<option value='0'>Select Vehicle Make...</option>");
            $.each(response.model, function (index, elementValue) {
                var opt = "<option value='" + elementValue.Value + "'>" + elementValue.Text + "</option>";
                $("#ddlVehicleMake").append(opt);
                $("#ddlVehicleMakeA").append(opt);
            });
            $("#ddlVehicleMake").val(0).trigger('change');
            $("#ddlVehicleMakeA").val(0).trigger('change');
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

var editRowCount = 0;
var editTableRow = function (id) {
    editRowCount = id;

    $("#popVehicleDiv").PopupWindow("open");

    var rowEdit = $('#tbl_vehicleLead tr[data-value="' + id + '"]');
    $("#ddlVehicleMake").val($(rowEdit).find("td:eq(0)").attr("data-makeid")).trigger('change');
    $("#ddlVehicleType").val($(rowEdit).find("td:eq(1)").attr("data-typeid")).trigger('change');
    $("#txtVehicleYear").val($(rowEdit).find("td:eq(2)").attr("data-vehicleyear"));
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
var clearFieldPopUp = function () {

    $("#ddlVehicleMake").val(0).trigger('change');
    $("#ddlVehicleType").val(0).trigger('change');
    $("#txtVehicleYear").val("");
    $("#txtVinNo").val("");
    $("#txtLicense").val("");
    $("#txtDealer").val("");
    $("#txtFinanceCompany").val("");
}
var clearLeadPageFields = function () {
    $("#hndLeadID").val(0);
    $("#txtCompanyName").val("");
    $("#txtPhone").val("");
    $("#txtEmail").val("");
    $("#ddlLeadStatus").val();
    $("#ddlLeadStatus").val(0).trigger('change');
    $("#txtLeadOwner").val("");
    $("#txtName").val("");
    $("#txtPhone1").val("");
    $("#txtPhone2").val("");
    $("#txtLeadEmail").val("");
    $("#txtListCode").val("");
    $("#dtExpiryDate").val("");
    $("#txtWarranty").val("");
    $("#txtListCode2").val("");
    $("#txtAddress").val("");
    $("#txtPasswordId").val("");
    $("#txtPin").val("");
    $("#ddlLeadLanguage").val(0).trigger('change');
}

var getVehicleLeadData = function (leadID) {
    var param = { LeadID: leadID };
    $.ajax({
        url: "/Leads/GetVehicleLeadInfo",
        type: "post",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",

        success: function (response) {
            if (response.vehicleData != "") {
                if (response.vehicleData.length > 0) {
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
$(document).ready(function () {

    $("#popOwnerDiv").PopupWindow({
        title: "Add Owner",
        modal: true,
        autoOpen: false,
        top: 200,
        left: 380,
        height: 400,
        width: 450,

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
        $("#popOwnerDiv").PopupWindow("open");
    });
    $("#popUploadFile").PopupWindow({
        title: "Upload File",
        modal: true,
        autoOpen: false,
        top: 250,
        left: 600,
        height: 250,
        width: 400
    });
    $("#btnImport").on("click", function (event) {
        $("#popUploadFile").PopupWindow("open");
    });
    getOwnerList();
    getVehicleMakeList();
    getPackageList();
    getAddPackageList();

    $("#popVehicleDiv").PopupWindow({
        title: "Vehicle Information",
        modal: true,
        autoOpen: false,
        top: 220,
        left: 180,
        height: 400,
        width: 650,

    });
    $("#popEmailTemplateDiv").PopupWindow({
        title: "Email Templates",
        modal: true,
        autoOpen: false,
        top: 180,
        left: 180,
        height: 400,
        width: 650,

    });
    $("#popAccountDiv").PopupWindow({
        title: "Convert To Account - Online",
        modal: true,
        autoOpen: false,
        top: 220,
        left: 80,
        height: 500,
        width: 850,

    });

    $("#popAOAccountDiv").PopupWindow({
        title: "Convert To Account - Check / Comp",
        modal: true,
        autoOpen: false,
        top: 220,
        left: 280,
        height: 300,
        width: 450,

    });
    $("#btnAddChat").on("click", function (event) {
        $("#hndCID").val(0);
        $("#txtChatTitle").val("");
        $("#txtChatBody").val("");
        $("#popChatDet").PopupWindow("open");
        $("#btnSavechat").removeClass("hidden");
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
    $("#popAdvSearch").PopupWindow({
        title: "Advance Lead Search",
        modal: true,
        autoOpen: false,
        top: 150,
        left: 250,
        height: 400,
        width: 650,

    });


    $("#btnSelectTemplate").on("click", function (event) {
        getEmailTemplates();
    });
    $("#btnVehicleAdd").on("click", function (event) {
        $("#popVehicleDiv").PopupWindow("open");
        clearFieldPopUp();
    });
    $("#ddlVehicleMake").on('change', function (evt, params) {
        var selected = $(this).val();
        if (selected != null) {
            getVehicleTypeList(selected);
        }
    });

    $("#ddlLeadLanguage").on('change', function (evt, params) {
        var selected = $(this).val();
        if (selected != null) {
            for (var j = 0; j < selected.length; j++) {
                if (selected[j] == 0) {
                    $("#ddlLeadLanguage").select2('data', null);
                    $("#ddlLeadLanguage").select2('data', { id: '0', text: 'All' });
                    break;
                }
            }
        }
    });

    $("#openFileImportList").change(function () {
        var sfilename = $("#openFileImportList")[0].files[0].name;
        var filesize = $("#openFileImportList")[0].files[0].size;
        var fileType = $("#openFileImportList").val();

        var $file = document.getElementById('openFileImportList');
        var ext = sfilename.substr(sfilename.lastIndexOf(".") + 1);
        if (ext.toLowerCase() != "csv" && ext.toLowerCase() != "xlsx") {
            $.alert({
                title: 'Alert!',
                content: 'File type should be .cvs or .xlsx',
                type: 'red'
            });
            $("#openFileImportList").val('');
            return;
        }
    });
    //
    $('#ulPaginationLeadDetail').pagination({
        items: 0,
        currentPage: 1,
        displayedPages: 10,
        cssStyle: '',
        useAnchors: false,
        prevText: '&laquo;',
        nextText: '&raquo;',
        onInit: function () {
            buildPaganationLead();
        },
        onPageClick: function (page, evt) {
            fillLeadTable(page, 1);
        }
    });
    $("#ddlRowPerPage").on('change', function (evt, params) {
        ddlRowPerPageChange();
    });

    $('#ulPaginationLeadDetailAdv').pagination({
        items: 0,
        currentPage: 1,
        displayedPages: 10,
        cssStyle: '',
        useAnchors: true,
        prevText: '&laquo;',
        nextText: '&raquo;',
        onInit: function () {
            buildPaganationLead();
        },
        onPageClick: function (page, evt) {
            fillLeadTable(page, 2);
        }
    });
    $("#ddlRowPerPageAdv").on('change', function (evt, params) {
        ddlRowPerPageChangeAdv();
    });
    $("#ddlRowPerPageAdv").select2({ minimumResultsForSearch: Infinity });


});
var ddlRowPerPageChange = function () {
    buildPaganationLead();
}
var ddlRowPerPageChangeAdv = function () {
    buildAdvPaganationLead();
}
//// Index Page
var newLead = function () {
    window.location = '/Leads/AddEdit';
}

var editLead = function (id) {
    window.location = '/Leads/Edit/' + id;
}

var getOwnerList = function () {
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
                html += '<tr  class="gradeX " data-value="' + elementValue.UserID + '">';
                html += '<td class="pd-vehicletblText" >' + elementValue.UserID + '</td>';
                html += '<td class="pd-vehicletblText"><a href="#"  onclick="addOwner(' + elementValue.UserID + ',\'' + elementValue.UserName + '\');">' + elementValue.UserName + '</a></td>';
                html += '<td class="pd-vehicletblText">' + elementValue.FirstName + '</td>';
                html += '<td class="pd-vehicletblText">' + elementValue.LastName + '</td>';
                html += '</tr>';
                $("#tblOwner>tbody").append(html);
            });
        }
    });
}
var addOwner = function (userId, userName) {
    $("#hndOwnerID").val(userId);
    $("#txtLeadOwner").val(userName);
    $("#popOwnerDiv").PopupWindow("close");
}


var exportToLead = function () {
    $("#popUploadFile").PopupWindow("close");
    $("#divLoader").show();
    $formData = new FormData();
    var $file = document.getElementById('openFileImportList');
    if ($file.files.length > 0) {
        for (var i = 0; i < $file.files.length; i++) {
            $formData.append('file-' + i, $file.files[i]);
        }
    }
    $.ajax({
        url: "/Leads/ExportToLead",
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
                buildPaganationLead();
                $.alert({
                    title: 'Alert!',
                    content: response.Msg,
                    type: 'blue'
                });

            }
        }
    });
}
var getEmailTemplates = function () {
    $("#divLoader").show();
    $("#tblEmailTemplate>tbody").empty();
    $.ajax({
        url: '/Leads/GetEmailTemplates',
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
    var param = { TemplateID: templateID, LeadID: $("#hndLeadID").val() };
    $.ajax({
        url: "/Leads/GetEmailData",
        type: "post",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            $("#divLoader").hide();
            $("#txtLeadSubject").val(response.subject);
            $("#hndAttachFile").val(response.attachmentFile);
            CKEDITOR.instances['TemplateHTML'].setData(response.html);
        }
    });
}
var buildPaganationLead = function () {
    $("#s2id_ddlRowPerPage").removeClass("hidden");
    $("#ulPaginationLeadDetail").removeClass("hidden");
    $("#s2id_ddlRowPerPageAdv").addClass("hidden");
    $("#ulPaginationLeadDetailAdv").addClass("hidden");
    $("#ulPaginationLeadDetail").addClass("simple-pagination");

    $("#divLoader").show();
    var name = $("#txtName").val();
    var rowDisplay = $("#ddlRowPerPage").val();

    var model = {
        Name: name,
        RowDisplay: rowDisplay
    };
    //alert(JSON.stringify(model));

    $.ajax({
        url: '/Leads/GetLeadsFilterRangeList',
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
                $('#ulPaginationLeadDetail').pagination('updateItems', pnarray[0]);
                $('#ulPaginationLeadDetail').pagination('selectPage', 1);
                $("#lblTotalRecords").text("Total Records : " + pnarray[1]);
                fillLeadTable(1, 1);
            }
        }
    });
}

var fillLeadTable = function (PN, SO) {
    $("#divLoader").show();
    var cToDate = $("#dtCTotDate").val();
    var cFromDate = $("#dtCFromDate").val();
    var accStatus = $("#ddlLeadStatus").val();
    var takeOffList = $("#chkTakeOffList").is(":checked") ? "1" : "0";
    if (SO == 1) {
        var rowDisplay = $("#ddlRowPerPage").val();
        var name = $("#txtName").val();
    }
    else if (SO == 2) {
        var rowDisplay = $("#ddlRowPerPageAdv").val();
        var name = $("#txtAccName").val();
    }
    var model = {
        Name: name,
        RowDisplay: rowDisplay,
        PageNumber: PN,
        SearchOption: SO,
        CreatedDate: cFromDate,
        LastModifiedDate: cToDate,
        LeadStatus: accStatus,
        TakeOffList: takeOffList,
    };
    $.ajax({
        url: '/Leads/GetLeadInfoPageList',
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
                $("#tblLeads>tbody").empty();
                $.each(response, function (index, elementValue) {
                    var html = '';
                    html += '<tr  class="gradeX pd-vehicletblText" data-value="' + elementValue.LeadID + '">';
                    html += '<td class="pd-vehicletblText"><a href="#"  onclick="editLead(' + elementValue.LeadID + ');">' + elementValue.Name + '</a></td>';
                    html += '<td class="pd-vehicletblText"><a href="#"  onclick="editLead(' + elementValue.LeadID + ');"> ' + elementValue.PinNo + '</td>';
                    html += '<td class="pd-vehicletblText"> ' + elementValue.State + '</td>';
                    html += '<td class="pd-vehicletblText"> ' + elementValue.LastModifiedDate + '</td>';
                    html += '<td class="pd-vehicletblText"> ' + elementValue.LastModifiedById + '</td>';
                    html += '<td class="pd-vehicletblText"> ' + elementValue.ExpirationDate + '</td>';
                    html += '<td class="pd-vehicletblText">' + elementValue.ListCode + ' </td>';
                    html += '<td class="pd-vehicletblText">' + elementValue.CreatedById + ' </td>';
                    html += '</tr>';
                    $("#tblLeads>tbody").append(html);
                });
            }
        }
    });
}
//var sendEmail = function () {
//    $("#divLoader").show();
//    var fromEmail = $("#ddlEmailFrom").val();
//    var toEmail = $("#txtToLeadEmail").val();
//    var ccEmail = $("#txtCCLeadEmail").val();
//    var bccEmail = $("#txtBCCLeadEmail").val();
//    var emailSubject = $("#txtLeadSubject").val();
//    var emailMessage = CKEDITOR.instances['TemplateHTML'].getData();
//    var leadID = $("#hndLeadID").val();
//    var attachFile = $("#hndAttachFile").val();
//    var errMsg = "";

//    if (!toEmail)
//    {
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
//        LeadID: leadID,
//        AttachFile: attachFile
//    };
//    $.ajax({
//        url: '/Leads/SendEmail',
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
//                    content: 'Email Send Successfully',
//                    type: 'blue'
//                });
//                $("#txtCCLeadEmail").val("");
//               $("#txtBCCLeadEmail").val("");
//                $("#txtLeadSubject").val("");
//                $("#hndAttachFile").val("");
//                CKEDITOR.instances['TemplateHTML'].setData("");
//            }
//        }
//    });
//}
var sendEmailAttach = function () {
    $("#divLoader").show();
    var fromEmail = $("#ddlEmailFrom").val();
    var toEmail = $("#txtToLeadEmail").val();
    var ccEmail = $("#txtCCLeadEmail").val();
    var bccEmail = $("#txtBCCLeadEmail").val();
    var emailSubject = $("#txtLeadSubject").val();
    var emailMessage = CKEDITOR.instances['TemplateHTML'].getData();
    var leadID = $("#hndLeadID").val();
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
    $formData.append('AGID', leadID);
    $formData.append('PageID', 1);

    var $file = document.getElementById('emailattUpload');
    if ($file.files.length > 0) {
        for (var i = 0; i < $file.files.length; i++) {
            $formData.append('file-' + i, $file.files[i]);
        }
    }

    $.ajax({
        url: "/Leads/SendEmailAttach",
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

    var leadid = $("#hndLeadID").val();
    $formData = new FormData();
    $formData.append('LeadID', leadid);
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
        url: "/Leads/SaveToFolder",
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
    var leadID = $("#hndLeadID").val();
    var param = { LeadID: leadID };
    $.ajax({
        url: '/Leads/GetAttachedLeadFiles',
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
                $("#tblLeadFiles>tbody").empty();
                $.each(response.returnREData, function (index, elementValue) {
                    var html = '';
                    html += '<tr  data-value="' + elementValue.ID + '">';
                    html += '<td >' + elementValue.OriginalFileName + '</td>';
                    html += '<td >' + elementValue.CreatedBy + '</td>';
                    html += '<td> ' + elementValue.CreatedDate + '</td>';
                    html += '<td><div><button  onclick="deleteFiles(' + elementValue.ID + ');"><i class="icon-trash" ></i></button></div></td>';
                    html += '<td><a href="/FileAttachments/' + elementValue.SystemFileName + '" class="btn" id="linkFile" >  <i class="icon-download"></i></a></td>';
                    html += '</tr>';
                    $("#tblLeadFiles>tbody").append(html);
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
    $.ajax({
        url: "/Leads/DeleteFiles",
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
    $('#tblLeadFiles tr[data-value="' + id + '"]').remove();
}


var dateTimeDataSource = [
     { text: "12.00 AM", value: "1" },
     { text: "1.00 AM", value: "2" },
     { text: "2.00 AM", value: "3" },
     { text: "3.00 Am", value: "4" },
     { text: "4.00 AM", value: "5" },
     { text: "5.00 AM", value: "6" },
     { text: "6.00 AM", value: "7" },
     { text: "7.00 AM", value: "8" },
     { text: "8.00 AM", value: "9" },
     { text: "9.00 AM", value: "10" },
     { text: "10.00 AM", value: "11" },
     { text: "11.00 AM", value: "12" },
     { text: "12.00 PM", value: "13" },
     { text: "1.00 PM", value: "14" },
     { text: "2.00 PM", value: "15" },
     { text: "3.00 PM", value: "16" },
     { text: "4.00 PM", value: "17" },
     { text: "5.00 PM", value: "18" },
     { text: "6.00 PM", value: "19" },
     { text: "7.00 PM", value: "20" },
     { text: "8.00 PM", value: "21" },
     { text: "9.00 PM", value: "22" },
     { text: "10.00 PM", value: "23" },
     { text: "11.00 PM", value: "24" }
];
var startDate_DDL = function () {
    $("#ddlStartTime").empty();
    $.each(dateTimeDataSource, function (n, v) {
        $("#ddlStartTime").append("<option value='" + v.value + "'>" + v.text + "</option>");
        $('#ddlStartTime').trigger('chosen:updated');
    });

}
var endDate_DDL = function () {
    $("#ddlENDTime").empty();
    var endTime = "";
    $.each(dateTimeDataSource, function (n, v) {
        $("#ddlENDTime").append("<option value='" + v.value + "'>" + v.text + "</option>");
        $('#ddlENDTime').trigger('chosen:updated');
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
var totalAmt = 0;
var packageId = 0;
var adddecal = 0;
var idtheft = 0;
function selectPackage(cont, checkedRadio, pprice, installment, packagen) {

    $('.mainpackage').not($(cont)).prop('checked', false);
    $.uniform.update(".mainpackage");


    if (checkedRadio && $(cont).is(":checked")) {
        totalAmt = parseFloat(pprice);
        packageId = checkedRadio;
    } else {

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

function convertTOAccount() {
    totalAmt = 0;
    packageId = 0;
    adddecal = 0;
    idtheft = 0;
    addPackageArray = [];
    var leadStatus = $("#ddlLeadStatus").val();
    if (leadStatus == 7 || leadStatus == 8) {
        var pinNo = $("#txtPin").val();
        var param = { PinNo: pinNo };
        $.ajax({
            url: '/AgentOrder/CheckLeadOrder',
            type: "post",
            data: JSON.stringify(param),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (response) {
                if (response.leadData.LeadID > 0) {
                    $("#popAOAccountDiv").PopupWindow("open");
                    $("#lblAOIDl").text(response.leadData.OrderID);
                    $("#lblPackagel").text("Package: " + response.leadData.PackageName);
                    $("#lblChargeDatel").text("Charge Date: " + response.leadData.ChargeDate);
                    $("#lblVehicleDetl").text("Vehicle Make/Year : " + response.leadData.VehicleText + "/" + response.leadData.VehicleYear);
                    $("#lblTotalAmtDet").text("Total Amount: " + response.leadData.TotalAmt);
                    if (response.leadData.CardCheckNumber) {
                        $("#lblCheckNumber").text("Check No.: " + response.leadData.CardCheckNumber);
                    }
                    $("#lblCompType").text(response.leadData.CompType);


                }
            }
        });


    }
    else if (leadStatus == 2 || leadStatus == 3 || leadStatus == 4) {
        $("#popAccountDiv").PopupWindow("open");
    }
    else {
        $.alert({
            title: 'Alert!',
            content: "Account Already Active",
            type: 'red'
        });
        return;
    }
}
function saveLeadToAccount(accType) {
    $("#divLoader").show();
    var msg = "";
    var leadID = $("#hndLeadID").val();
    var name = $("#txtName").val();
    var primaryPhone = $("#txtPhone1A").val();
    var leademail = $("#txtEmailA").val();
    var street = $("#txtStreet").val();
    var city = $("#txtCity").val();
    var state = $("#txtState").val();
    var zip = $("#txtZip").val();
    var country = $("#txtCountry").val();
    var password = $("#txtPasswordId").val();
    var pinNo = $("#txtPin").val();
    var cardType = $("#ddlCardType").val();
    var cardNumber = $("#txtCardNumber").val();
    // var cardExpirationMonth = $("#ddlMonth").val();
    //var cardExpirationYear = $("#ddlYear").val();
    //var cardSecurityCode = $("#txtCardSecurityCode").val();
    var vehicleMake = $("#ddlVehicleMakeA").val();
    var vyear = $("#txtVehicleYearA").val();
    var chargeDate = $("#txtAChargeDate").val();
    var packageid = packageId;
    var transid = $("#txtTransactionID").val();
    var authcode = $("#txtAuthCode").val();
    var aoid = $("#lblAOIDl").text();
    var accountStatus = $("#ddlAccStatus").val();

    var addpackage = "";
    if (addPackageArray.length > 0) {
        for (var j = 0; j < addPackageArray.length; j++) {
            if (addpackage == "") {
                addpackage = addPackageArray[j];
            } else {
                addpackage += "," + addPackageArray[j];

            }
            if (addPackageArray[j] == 16) {
                adddecal = 1;
                totalAmt += parseFloat(49);
            }
            if (addPackageArray[j] == 17) {
                adddecal = 2;
                totalAmt += parseFloat(98);
            }
            if (addPackageArray[j] == 18) {
                adddecal = 3;
                totalAmt += parseFloat(147);
            }
            if (addPackageArray[j] == 19) {
                totalAmt += parseFloat(10);
                idtheft = 1;
            }
        }
    } else {
        addpackage = "0";
    }
    //console.log(parseFloat(totalAmt).toFixed(2));
    //console.log(idtheft);
    //console.log(adddecal);
    if (accType == 1) {
        if (vehicleMake == 0) {
            msg += " Please select Vehicle Make <br />";
        }

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

        //var cmonth = $("#ddlMonth").val();
        //if (cmonth == 0) {
        //    msg += " Please select Month <br />";
        //}
        //var cyr = $("#ddlYear").val();
        //if (cyr == 0) {
        //    msg += " Please select Year <br />";
        //}

        //var ccode = $("#txtCardSecurityCode").val();
        //if (!ccode) {
        //    msg += " Please Enter Card Security Code <br />";
        //}
        if (msg != "") {
            $.alert({
                title: 'Alert!',
                content: msg,
                type: 'red'
            });
            return;
        }
    }
    var model = {
        LeadID: leadID,
        OrderID: aoid,
        AccountName: name,
        Phone: primaryPhone,
        Street: street,
        City: city,
        State: state,
        Zip: zip,
        Country: country,
        Password: password,
        PinNo: pinNo,
        VehicleMake: vehicleMake,
        //VehicleType: vehicleType,
        VehicleYear: vyear,

        FirstChargeDate: chargeDate,
        PackageId: packageid,
        AdditionalPackage: addpackage,
        TotalAmt: parseFloat(totalAmt).toFixed(2),
        AddDecals: adddecal,
        IdentityTheft: idtheft,
        CardType: cardType,
        CardCheckNumber: cardNumber,
        //CardExpirationMonth: cardExpirationMonth,
        //CardExpirationYear: cardExpirationYear,
        //CardSecurityCode: cardSecurityCode,
        TransactionID: transid,
        AuthCode: authcode,
        LeadEmail: leademail,
        AccType: accType,
        AccountStatus: accountStatus,
    }
    $.ajax({
        url: "/AgentOrder/SaveLeadToAccount",
        type: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8", // content type sent to server
        dataType: "json", //Expected data format from server
        success: function (response) {

            window.location = '/AccountPage/Edit/' + response.LeadID;
            $("#divLoader").hide();
        }
    });

    return false;
}
var validateNumber = function () {
    var result = $('#txtCardNumber').validateCreditCard();
    $('#txtCardNumber').removeClass();
    (result.card_type == null ? $('#txtCardNumber').removeClass().addClass("span11 card_number") : $('#txtCardNumber').addClass("span11 card_number " + result.card_type.name));
    (result.valid ? $('#txtCardNumber').addClass().addClass("valid") : $('#txtCardNumber').removeClass("valid"));

    console.log((result.card_type == null ? "" : result.card_type.name));

    var typeOfCard = (result.card_type != null ? result.card_type.type_of_card : "0");
    //$("#ddlCardType").val(typeOfCard).trigger('change');
}

function getChatist() {
    var leadID = $("#hndLeadID").val();

    var param = { AccountID: leadID, PageId: 2 };
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
                $("#btnSavechat").addClass("hidden");
            }
        }
    });
}
var saveUpdateChat = function () {
    $("#divLoader").show();
    var msg = "";
    var leadID = $("#hndLeadID").val();
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
    $formData.append('LeadID', leadID);

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
                        content: "Chat added successfully.",
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
    var accountID = $("#hndLeadID").val();

    var param = { AccountID: accountID, PageID: 1 };
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
    $("#btnSendEmail").addClass("hidden");
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
                $("#txtToLeadEmail").val(response.ToEmails);
                CKEDITOR.instances['TemplateHTML'].setData(response.EmailHTMLBody);
                $("#txtCCLeadEmail").val(response.CCEmails);
                $("#txtBCCLeadEmail").val(response.BCCEmails);
                //$("#chatterUploadDiv").addClass("hidden");
                //$("#linkChatFile").removeClass("hidden");
                $("#txtLeadSubject").val(response.EmailSubject);
                $("#linkEmailFile").removeClass("hidden");
                $("#emailUploadDiv").addClass("hidden");
                $("#linkEmailFile").text(response.OriginalFileName);
                $("#linkEmailFile").attr("href", baseURL() + "/AttachmentFiles/" + response.AttachedFileName);
            }
        }
    });
}
function sendNewEmail() {
    $("#btnSendEmail").removeClass("hidden");

    $("#txtCCLeadEmail").val("");
    $("#txtBCCLeadEmail").val("");
    $("#txtLeadSubject").val("");
    $("#hndAttachFile").val("");
    CKEDITOR.instances['TemplateHTML'].setData("");
    $("#linkEmailFile").addClass("hidden");
    $("#emailUploadDiv").removeClass("hidden");
    $("#linkEmailFile").text("");
}
function advSearch() {
    $("#popAdvSearch").PopupWindow("open");
}
var buildAdvPaganationLead = function () {
    $("#s2id_ddlRowPerPage").addClass("hidden");
    $("#ulPaginationLeadDetail").addClass("hidden");
    $("#s2id_ddlRowPerPageAdv").removeClass("hidden");
    $("#ulPaginationLeadDetailAdv").removeClass("hidden");
    $("#ulPaginationLeadDetail").removeClass("simple-pagination");


    $("#divLoader").show();
    var accountName = $("#txtAccName").val();
    var rowDisplay = $("#ddlRowPerPageAdv").val();
    var cToDate = $("#dtCTotDate").val();
    var cFromDate = $("#dtCFromDate").val();
    var accStatus = $("#ddlLeadStatus").val();
    var takeOffList = $("#chkTakeOffList").is(":checked") ? "1" : "0";

    var model = {
        Name: accountName,
        RowDisplay: rowDisplay,
        CreatedDate: cFromDate,
        LastModifiedDate: cToDate,
        LeadStatus: accStatus,
        TakeOffList: takeOffList,
    };
    //alert(JSON.stringify(model));

    $.ajax({
        url: '/Leads/GetLeadAdvSearch',
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
                $('#ulPaginationLeadDetailAdv').pagination('updateItems', pnarray[0]);
                $('#ulPaginationLeadDetailAdv').pagination('selectPage', 1);
                $("#lblTotalRecords").text("Total Records : " + pnarray[1]);
                $("#popAdvSearch").PopupWindow("close");
                fillLeadTable(1, 2);
            }
        }
    });
}
var getHistoryList = function () {
    var accountID = 0;
    var leadID = $("#hndLeadID").val();
    var param = { AccountID: accountID, LeadID: leadID };
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