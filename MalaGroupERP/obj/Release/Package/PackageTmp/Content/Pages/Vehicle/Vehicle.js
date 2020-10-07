$(document).ready(function () {
    $('#ulPaginationMakeDetail').pagination({
        items: 0,
        currentPage: 1,
        displayedPages: 10,
        cssStyle: '',
        useAnchors: false,
        prevText: '&laquo;',
        nextText: '&raquo;',
        onInit: function () {
            buildPaganationMake();
        },
        onPageClick: function (page, evt) {
            fillMakeTable(page);
        }
    }); 
    $("#ddlRowPerPage").on('change', function (evt, params) {
        ddlRowPerPageChange();
    });
    $('#ulPaginationTypeDetail').pagination({
        items: 0,
        currentPage: 1,
        displayedPages: 10,
        cssStyle: '',
        useAnchors: false,
        prevText: '&laquo;',
        nextText: '&raquo;',
        onInit: function () {
            buildPaganationType();
        },
        onPageClick: function (page, evt) {
            fillTypeTable(page);
        }
    });
    $("#ddlRowPerPageType").on('change', function (evt, params) {
        ddlRowPerPageChangeType();
    });
    clearVType();
    clearVMake();
    //clearVModel();

    //GetVMakeList();
    getVehicleMakeList();
    //GetVTypeList();
    //GetVModelList();
    $("#ddlVehicleMake").on('change', function (evt, params) {
        var selected = $(this).val();
        if (selected != null) {
          
           
        }
    });
    $("#ddlVehicleMake1").on('change', function (evt, params) {
        var selected = $(this).val();
        if (selected != null) {
            getVehicleTypeList(selected);
          
        }
    });

    //$("#ddlVehicleType").on('change', function (evt, params) {
    //    var vehicleMake = $("#ddlVehicleMake1").val();
    //    var selected = $(this).val();
    //    if (selected != null) {
    //        //getVehicleModelList(vehicleMake, selected);
    //        //GetVModelList(vehicleMake, selected);
    //    }
    //});

    $("#myModal").draggable({
        handle: ".modal-header"
    });

});
var ddlRowPerPageChange = function () {
    fillMakeTable(1);
}
var ddlRowPerPageChangeType = function () {
    fillTypeTable(1);
}
var saveUpdateVMake = function () {
    var msg = "";
    var vid = $("#hndVID").val();
    var vMake = $("#txtVehicleMake").val();

    if (vMake == "") {
        msg += " Please enter Vehicle Make.<br />";
    }


    if (msg != "") {
        $.alert({
            title: 'Alert!',
            content: msg,
            type: 'red'
        });
        return;
    }

    var model = {
        VID: vid,
        VehicleMake: vMake,

    }
    var param = { model: model }
    $.ajax({
        url: "/Vehicle/SaveUpdateVMake",
        method: "post",
        data: model,
        contentType: "application/json; charset=utf-8", // content type sent to server
        dataType: "json", //Expected data format from server
        success: function (response) {
            if ($.trim(response.error) != "") {

            } else {
                if (response.id != 0) {
                    $.alert({
                        title: 'Msg!',
                        content: response.MSG,
                        type: 'blue',
                       
                    });
                   
                    getVehicleMakeList();
                    GetVMakeList();
                   
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
var deleteVMake = function () {
    var msg = "";
    var vid = $("#hndVID").val();
    var vMake = $("#txtVehicleMake").val();

    if (vMake == "") {
        msg += " Please enter Vehicle Make.<br />";
    }


    if (msg != "") {
        $.alert({
            title: 'Alert!',
            content: msg,
            type: 'red'
        });
        return;
    }

    var model = {
        VID: vid,
        VehicleMake: vMake,

    }
    var param = { model: model }
    $.confirm({
        title: 'Alert!',
        content: 'Are you sure you want to delete Vehicle Make?',
        type: 'blue',
        buttons: {
            confirm: function () {
                $("#divLoader").show();
                $.ajax({
                    url: "/Vehicle/DeleteVMake",
                    method: "post",
                    data: model,
                    contentType: "application/json; charset=utf-8", // content type sent to server
                    dataType: "json", //Expected data format from server
                    success: function (response) {
                        if ($.trim(response.error) != "") {

                        } else {
                            if (response.id != 0) {
                                $.alert({
                                    title: 'Msg!',
                                    content: response.MSG,
                                    type: 'blue',

                                });

                                getVehicleMakeList();
                                GetVMakeList();

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
            },
            cancel: function () {
                return;

            }
        }
    });


    return false;
}
var saveUpdateVType = function () {
    var msg = "";
    var vtid = $("#hndVTID").val();
    var vMake = $("#ddlVehicleMake").val();
    var vType = $("#txtVehicleType").val();
    if (vMake == 0) {
        msg += " Please Select Vehicle Make.<br />";
    }
    if (vType == "") {
        msg += " Please enter Vehicle Type.<br />";
    }


    if (msg != "") {
        $.alert({
            title: 'Alert!',
            content: msg,
            type: 'red'
        });
        return;
    }

    var model = {
         VTID:vtid,
        VehicleMake: vMake,
        VehicleType: vType,
    }
    var param = { model: model }
    $.ajax({
        url: "/Vehicle/SaveUpdateVType",
        method: "post",
        data: model,
        contentType: "application/json; charset=utf-8", // content type sent to server
        dataType: "json", //Expected data format from server
        success: function (response) {
            if ($.trim(response.error) != "") {

            } else {
                if (response.id != 0) {
                    $.alert({
                        title: 'Msg!',
                        content: response.MSG,
                        type: 'blue',

                    });
                    //GetVTypeList();
                    buildPaganationType();
                  
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
//var saveUpdateVModel = function () {
//    var msg = "";
//    var vmid = $("#hndVMID").val();
//    var vMake = $("#ddlVehicleMake1").val();
//    var vType = $("#ddlVehicleType").val();
//    var vModel = $("#txtVehicleModel").val();
//    if (vMake == 0) {
//        msg += " Please Select Vehicle Make.<br />";
//    }
//    if (vType == 0) {
//        msg += " Please Select Vehicle Type.<br />";
//    }
//    if (vModel == "") {
//        msg += " Please enter Vehicle Model.<br />";
//    }


//    if (msg != "") {
//        $.alert({
//            title: 'Alert!',
//            content: msg,
//            type: 'red'
//        });
//        return;
//    }

//    var model = {
//        VMID: vmid,
//        VehicleMake: vMake,
//        VehicleType: vType,
//        VehicleModal: vModel,
//    }
//    var param = { model: model }
//    $.ajax({
//        url: "/Vehicle/SaveUpdateVModel",
//        method: "post",
//        data: model,
//        contentType: "application/json; charset=utf-8", // content type sent to server
//        dataType: "json", //Expected data format from server
//        success: function (response) {
//            if ($.trim(response.error) != "") {

//            } else {
//                if (response.id != 0) {
//                    $.alert({
//                        title: 'Msg!',
//                        content: response.MSG,
//                        type: 'blue',

//                    });
//                    GetVModelList();
                   
//                }
//                else {
//                    $.alert({
//                        title: 'Alert!',
//                        content: "Error occured while saving data.<br/>Please try later.",
//                        type: 'red'
//                    });
//                }
//            }
//        }
//    });

//    return false;
//}
var getVehicleMakeList = function () {
    $("#ddlVehicleMake").empty();
    $("#ddlVehicleMake1").empty();
    $.ajax({
        url: "/Vehicle/GetVehicleMakeList",
        method: "post",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            $("#ddlVehicleMake").append("<option value='0'>Select Vehicle Make...</option>");
            $("#ddlVehicleMake1").append("<option value='0'>Select Vehicle Make...</option>");
            $.each(response.model, function (index, elementValue) {
                var opt = "<option value='" + elementValue.Value + "'>" + elementValue.Text + "</option>";
                $("#ddlVehicleMake").append(opt);
                $("#ddlVehicleMake1").append(opt);
               
            });
            $("#ddlVehicleMake").val(0).trigger('change');
            $("#ddlVehicleMake1").val(0).trigger('change');
        }
    });
}
var getVehicleTypeList = function (vehicleMake) {
    $("#ddlVehicleType").empty();
    var param = { VehcileMake: vehicleMake }
    $.ajax({
        url: "/Vehicle/GetVehicleTypeList",
        method: "post",
        data: param,
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

//var getVehicleModelList = function (vehicleMake, vehcileType) {
//    $("#ddlVehicleModel").empty();
//    var param = { VehcileMake: vehicleMake, VehicleType: vehcileType }
//    $.ajax({
//        url: "/Vehicle/GetVehicleModelList",
//        method: "post",
//        data: param,
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        async: false,
//        success: function (response) {
//            $("#ddlVehicleModel").append("<option value='0'>Select Vehicle Type...</option>");
//            $.each(response.model, function (index, elementValue) {
//                var opt = "<option value='" + elementValue.Value + "'>" + elementValue.Text + "</option>";
//                $("#ddlVehicleModel").append(opt);
//            });
           
//        }
//    });
//}
var GetVMakeList=function()
{
    $.ajax({
        url: '/Vehicle/GetVehicleMakeList',
        method: "post",
        //data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {
                $("#tblVMake>tbody").empty();
                $.each(response.model, function (index, elementValue) {
                    var html = '';
                    html += '<tr  class="gradeX" data-value="' + elementValue.Value + '">';

                    html += '<td><a href="javascript:void(0);"  onclick="editVMake(' + elementValue.Value + ')">' + elementValue.Text + '</a></td>';

                    html += '</tr>';
                    $("#tblVMake>tbody").append(html);
                });
            }
        }
    });
}
var editVMake = function (vehicleMake) {
    $("#btnVMakeAdd").addClass("hidden");
    $("#btnVMakeUpdate").removeClass("hidden");
    $("#btnVMakeCancel").removeClass("hidden");
    $("#btnVMakeDelete").removeClass("hidden");
    var param = { VID: vehicleMake }
    $.ajax({
        url: '/Vehicle/GetVehicleMakeInfo',
        method: "post",
        data: param,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {
                $("#txtVehicleMake").val(response.VehicleMake);

                $("#hndVID").val(vehicleMake);
            }
        }
    });
  
    //  window.location = '/Vehicle/SaveUpdateVMake/' + id;
}
var cancelVMake = function () {
    $("#btnVMakeAdd").removeClass("hidden");
    $("#btnVMakeUpdate").addClass("hidden");
    $("#btnVMakeCancel").addClass("hidden");
    $("#btnVMakeDelete").addClass("hidden");
    $("#txtVehicleMake").val("");
    clearVMake();
}
var GetVTypeList = function () {
    var vType = $("#txtVehicleTypeSearch").val();

    var param = { VehicleType: vType }
    $.ajax({
        url: '/Vehicle/GetVTypeGrid',
        method: "post",
        data: param,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {
                $("#tblVType>tbody").empty();
                $.each(response.model, function (index, elementValue) {
                    var html = '';
                    html += '<tr  class="gradeX" data-value="' + elementValue.VTID + '">';
                    html += '<td>' + elementValue.VTID + '</td>';
                    html += '<td>'+ elementValue.VehicleMake + '</td>';
                    html += '<td><a href="#"  onclick="editVType(' + elementValue.VTID + ');">' + elementValue.VehicleType + '</a></td>';
                    html += '</tr>';
                    $("#tblVType>tbody").append(html);
                });
            }
        }
    });
}
var editVType = function (vehicleType) {
    $("#btnVTypeAdd").addClass("hidden");
    $("#btnVTypeCancel").removeClass("hidden");
    $("#btnVTypeUpdate").removeClass("hidden");
    var param = { VTID: vehicleType }
    $.ajax({
        url: '/Vehicle/GetVehicleTypeInfo',
        method: "post",
        data: param,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {
                $("#txtVehicleType").val(response.VehicleType);
        
                $("#ddlVehicleMake").val(response.VehicleMake).trigger('change');
                $("#hndVTID").val(vehicleType);
            }
        }
    });
  
    //  window.location = '/Vehicle/SaveUpdateVMake/' + id;
}
var cancelVType = function () {
    $("#btnVTypeAdd").removeClass("hidden");
    $("#btnVTypeCancel").addClass("hidden");
    $("#btnVTypeUpdate").addClass("hidden");
    $("#txtVehicleType").val("");
    clearVType();
}

//var GetVModelList = function () {
//    var vModel = $("#txtVehicleModelSearch").val();
//    var param = { VehicleModal: vModel }
//    $.ajax({
//        url: '/Vehicle/GetVModelGrid',
//        method: "post",
//        data: param,
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (response) {
//            if ($.trim(response.error) != "") {
//                //this.cancelChanges();
//            } else {
//                $("#tblVModel>tbody").empty();
//                $.each(response.model, function (index, elementValue) {
//                    var html = '';
//                    html += '<tr  class="gradeX" data-value="' + elementValue.VMID + '">';
//                    html += '<td>' + elementValue.VMID + '</td>';
//                    html += '<td>' + elementValue.VehicleMake + '</td>';
//                    html += '<td>' + elementValue.VehicleType + '</td>';
//                    html += '<td><a href="#"  onclick="editVModel(' + elementValue.VMID + ');">' + elementValue.VehicleModal + '</a></td>';
//                    html += '</tr>';
//                    $("#tblVModel>tbody").append(html);
//                });
//            }
//        }
//    });
//}
//var editVModel = function (vehicleModel) {
//    $("#btnVModelAdd").addClass("hidden");
//    $("#btnVModelCancel").removeClass("hidden");
//    $("#btnVModelUpdate").removeClass("hidden");
//    var param = { VMID: vehicleModel }
//    $.ajax({
//        url: '/Vehicle/GetVehicleModelInfo',
//        method: "post",
//        data: param,
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (response) {
//            if ($.trim(response.error) != "") {
//                //this.cancelChanges();
//            } else {
//                $("#txtVehicleModel").val(response.VehicleModal);
//                $("#ddlVehicleMake1").val(response.VehicleMake).trigger('change');
//                $("#ddlVehicleType").val(response.VehicleType).trigger('change');
//                $("#hndVMID").val(vehicleModel);
//            }
//        }
//    });
 
//    //  window.location = '/Vehicle/SaveUpdateVMake/' + id;
//}
//var cancelVModel = function () {
//    $("#btnVModelAdd").removeClass("hidden");
//    $("#btnVModelCancel").addClass("hidden");
//    $("#btnVModelUpdate").addClass("hidden");
//    $("#txtVehicleModel").val("");
//    clearVModel();
//}

var clearVMake= function () {
    $("#txtVehicleMake").val("");
    $("#hndVID").val(0);
}
var clearVType = function () {
    $("#txtVehicleType").val("");
    $("#hndVTID").val(0);
}
//var clearVModel = function () {
//    $("#hndVMID").val(0);
//    $("#txtVehicleModel").val("");
//}

var buildPaganationMake = function () {

    var makename = $("#txtMakeNameSearch").val();
    var rowDisplay = $("#ddlRowPerPage").val();

    var model = {
        VehicleMake: makename,
        RowDisplay: rowDisplay
    };
    //alert(JSON.stringify(model));

    $.ajax({
        url: '/Vehicle/GetMakeFilterRangeList',
        type: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {
                var pnarray = response.PageNumber.split('|');
                $('#ulPaginationMakeDetail').pagination('updateItems', pnarray[0]);
                $('#ulPaginationMakeDetail').pagination('selectPage', 1);
                $("#lblTotalRecords").text("Total Records : " + pnarray[1]);
                fillMakeTable(1);
            }
        }
    });
}

var fillMakeTable = function (PN) {
    var makename = $("#txtMakeNameSearch").val();
    var rowDisplay = $("#ddlRowPerPage").val();

    var model = {
        VehicleMake: makename,
        RowDisplay: rowDisplay,
        PageNumber: PN
    };
    $.ajax({
        url: '/Vehicle/GetMakeInfoPageList',
        type: "post",
        data: JSON.stringify(model),
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
                $("#tblVMake>tbody").empty();
                $.each(response, function (index, elementValue) {
                    var html = '';
                    html += '<tr  class="gradeX" data-value="' + elementValue.VID + '">';
                    html += '<td ><a href="javascript:void(0);"  onclick="editVMake(' + elementValue.VID + ')">' + elementValue.VehicleMake + '</a></td>';
                    html += '</tr>';
                    $("#tblVMake>tbody").append(html);
                });
            }
        }
    });
}


var buildPaganationType = function () {

    var typename = $("#txtTypeNameSearch").val();
    var rowDisplay = $("#ddlRowPerPageType").val();

    var model = {
        VehicleType: typename,
        RowDisplay: rowDisplay
    };
    //alert(JSON.stringify(model));

    $.ajax({
        url: '/Vehicle/GetTypeFilterRangeList',
        type: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {
                var pnarrayType = response.PageNumberType.split('|');
                $('#ulPaginationTypeDetail').pagination('updateItems', pnarrayType[0]);
                $('#ulPaginationTypeDetail').pagination('selectPage', 1);
                $("#lblTotalRecordsType").text("Total Records : " + pnarrayType[1]);
                fillTypeTable(1);
            }
        }
    });
}

var fillTypeTable = function (PN) {
    var typename = $("#txtTypeNameSearch").val();
    var rowDisplay = $("#ddlRowPerPageType").val();

    var model = {
        VehicleType: typename,
        RowDisplay: rowDisplay,
        PageNumber: PN
    };
    $.ajax({
        url: '/Vehicle/GetTypeInfoPageList',
        type: "post",
        data: JSON.stringify(model),
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
                $("#tblVType>tbody").empty();
                $.each(response, function (index, elementValue) {
                    var html = '';
                    html += '<tr  class="gradeX" data-value="' + elementValue.VTID + '">';
                    html += '<td >' + elementValue.VTID + '</td>';
                    html += '<td >' + elementValue.VehicleMake + '</td>';
                    html += '<td ><a href="#"  onclick="editVType(' + elementValue.VTID + ');">' + elementValue.VehicleType + '</a></td>';
                    html += '</tr>';
                    $("#tblVType>tbody").append(html);
                });
            }
        }
    });
}