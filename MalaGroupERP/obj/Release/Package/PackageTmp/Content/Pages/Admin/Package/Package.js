var saveUpdatePackage = function () {
    $("#divLoader").show();
    var msg = "";
    var packageID = $("#hndPackageID").val();
    var packageName = $("#txtPackage").val();
    var price = $("#txtPrice").val();
    var expiryDate = $("#txtExpiryDate").val();
    var noOfInstallment = $("#txtInstallment").val();
    var inPackageAdditional = $("#chkAdditional").is(":checked") ? "1" : "0";

    var decal = $("#chkDecal").is(":checked") ? "1" : "0";
    var identityTheft = $("#chkIdentity").is(":checked") ? "1" : "0";
    var decalNumber = $("#ddlDecalNumber").val();
    var isrenewal = $("#chkRenewal").is(":checked") ? "1" : "0";

    if (packageName == "") {
        msg += " Please enter Package.<br />";
    }
    if (price == "") {
        msg += " Please enter Price.<br />";
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
        PackageID: packageID,
        Package: packageName,
        Price: price,
        ExpiryDate: expiryDate,
        NoOfInstallment: noOfInstallment,
        InPackageAdditional: inPackageAdditional,
        Decal:decal,
        IdentityTheft:identityTheft,
        DecalNumber: decalNumber,
        IsRenewal: isrenewal,
    }
    $.ajax({
        url: "/Package/SaveUpdatePackage",
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
                if (response.PackageID != 0) {
                    if ($("#hndPackageID").val() == "0") {
                        $.alert({
                            title: 'Msg!',
                            content: 'Data Saved Successfully.',
                            type: 'blue',
                            buttons: {
                                Ok: function () {
                                    window.location = '/Package/Edit/' + response.PackageID;
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
var deletePackage = function () {
    var packageID = $("#hndPackageID").val();
    var model = { PackageID: packageID };
    $.confirm({
        title: 'Alert!',
        content: 'Are you sure you want to delete the data?',
        type: 'blue',
        buttons: {
            confirm: function () {
                $.ajax({
                    url: "/Package/DeletePackageData",
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
                                        window.location = '/Package/AddEdit';
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

var clearPackageField = function () {
    $("#hndAccountID").val(0);
    $("#txtAccountName").val();
    $("#txtWebsite").val();
}

var searchPackage=function()
{
    window.location = '/Package/Index/';
}

//Index Page

$(document).ready(function () {

    $('#chkDecal').on('change', function () {
        if ($(this).is(':checked')) {
            $('#divDecal').removeClass("hidden");
        }
        else {
            $('#divDecal').addClass("hidden");
           
        }
    });

    $('#ulPaginationPackageDetail').pagination({
        items: 0,
        currentPage: 1,
        displayedPages: 10,
        cssStyle: '',
        useAnchors: true,
        prevText: '&laquo;',
        nextText: '&raquo;',
        onInit: function () {
            buildPaganationPackage();
        },
        onPageClick: function (page, evt) {
            fillPackageTable(page);
        }
    });
    $("#ddlRowPerPage").on('change', function (evt, params) {
        ddlRowPerPageChange();
    });
});
var ddlRowPerPageChange = function () {
    buildPaganationPackage();
}

//var getPackageList = function () {
//    $.ajax({
//        url: '/Package/GetFullPackageList',
//        type: "post",
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (response) {
//            if ($.trim(response.error) != "") {
                
//            } else {
//                $("#tblPackage>tbody").empty();
//                $.each(response.model, function (index, elementValue) {
//                    var html = '';
//                    html += '<tr  class="gradeX" data-value="' + elementValue.PackageID + '">';
//                    html += '<td class="pd-vehicletblText"> <input type="checkbox"  /></td>';
//                    html += '<td  class="sorting_1 pd-vehicletblText"><a href="#"  onclick="editPackage(' + elementValue.PackageID + ');">' + elementValue.Package + '</a></td>';
//                    html += '<td class="pd-vehicletblText"> ' + elementValue.Price + '</td>';
//                    html += '<td class="pd-vehicletblText"> ' + elementValue.Additional + '</td>';
//                    html += '<td class="pd-vehicletblText"> ' + elementValue.ExpiryDate + '</td>';
//                    html += '</tr>';
//                    $("#tblPackage>tbody").append(html);
//                });
//            }
//        }
//    });
//}
var newPackage = function () {
    window.location = '/Package/AddEdit';
}
var editPackage = function (id) {
    window.location = '/Package/Edit/' + id;
}


var buildPaganationPackage = function () {

    var packageName = $("#txtPackageName").val();
    var rowDisplay = $("#ddlRowPerPage").val();

    var model = {
        Package: packageName,
        RowDisplay: rowDisplay
    };
    //alert(JSON.stringify(model));

    $.ajax({
        url: '/Package/GetPackageFilterRangeList',
        type: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {
                var pnarray = response.PageNumber.split('|');
                $('#ulPaginationPackageDetail').pagination('updateItems', pnarray[0]);
                $('#ulPaginationPackageDetail').pagination('selectPage', 1);
                $("#lblTotalRecords").text("Total Records : " + pnarray[1]);
                fillPackageTable(1);
            }
        }
    });
}

var fillPackageTable = function (PN) {
    $("#divLoader").show();
    var packageName = $("#txtPackageName").val();
    var rowDisplay = $("#ddlRowPerPage").val();

    var model = {
        Package: packageName,
        RowDisplay: rowDisplay,
        PageNumber: PN
    };
    $.ajax({
        url: '/Package/GetFullPackageList',
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
                $("#tblPackage>tbody").empty();
                $.each(response, function (index, elementValue) {
                    var html = '';
                    html += '<tr  class="gradeX" data-value="' + elementValue.PackageID + '">';
                    html += '<td  class="sorting_1 pd-vehicletblText"><a href="#"  onclick="editPackage(' + elementValue.PackageID + ');">' + elementValue.Package + '</a></td>';
                    html += '<td class="pd-vehicletblText"> ' + elementValue.Price + '</td>';
                    html += '<td class="pd-vehicletblText"> ' + elementValue.Additional + '</td>';
                    html += '<td class="pd-vehicletblText"> ' + elementValue.ExpiryDate + '</td>';
                    html += '</tr>';
                    $("#tblPackage>tbody").append(html);
                });
            }
        }
    });
}
