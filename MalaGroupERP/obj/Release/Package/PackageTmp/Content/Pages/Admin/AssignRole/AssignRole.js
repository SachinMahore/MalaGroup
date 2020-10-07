var Module;
var Role;
var SubModel;

$(document).ready(function () {
    fillRole();
    $("#Role").on('change', function (evt, params) {
        var selected = $(this).val();
        if (selected != null) {
            fillModule(selected);
        }
    });
    $("#Module").on('change', function (evt, params) {
        var selected = $(this).val();
        if (selected != null) {
            fillSubModule(selected);
        }
    });
    function FillRoleModuleRightsList(RoleId, ModuleId) {
        $("#divLoader").show();
        $.ajax({
            url: '../AssignRole/RoleModuleRightsList?RoleID=' + RoleId + '&ModuleID=' + ModuleId,
            type: "post",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if ($.trim(response.error) != "") {

                } else {
                    $("#divLoader").hide();
                    $("#tblRights>tbody").empty();
                    $.each(response, function (index, elementValue) {
                        var html = '';
                        html += '<tr id="ans" data-value="' + elementValue.ID + '">';
                        html += '<td class="hidden">' + elementValue.ID + '</td>';
                        html += '<td class="hidden">' + elementValue.ResourceId + '</td>';
                        html += '<td class="hidden">' + elementValue.IsForSave + '</td>';
                        html += '<td class="hidden">' + elementValue.LoopThrough + '</td>';
                        html += '<td>' + elementValue.Resource + '</td>';
                        html += "<td style='text-align:center;'><input id='chkSpecialRight' name='chkSpecialRight' class='chkSpecialRight' type='checkbox'  style='" + (elementValue.SpecialRight == 1 ? "display:block" : "display:none") + "'" + (elementValue.SpecialRight == 1 ? "checked=checked" : '') + "  /></td>";
                        html += "<td><input id='chkFull' name='chkFull' class='chkFull' onclick='checkFull(this\,\"2\",\"" + index + "\",\"" + elementValue.LoopThrough + "\");' type='checkbox'  " + (elementValue.FullRight == 1 ? "checked=checked" : '') + "  /></td>";
                        html += "<td><input id='chkEditRight' name='chkEditRight' class='chkEditRight' onclick='checkEdit(this\,\"3\",\"" + index + "\",\"" + elementValue.LoopThrough + "\");' type='checkbox'  " + (elementValue.EditRight == 1 ? "checked=checked" : '') + "  /></td>";
                        html += "<td><input id='chkAddRight' name='chkAddRight' class='chkAddRight' onclick='checkAdd(this\,\"3\",\"" + index + "\",\"" + elementValue.LoopThrough + "\");' type='checkbox'  " + (elementValue.AddRight == 1 ? "checked=checked" : '') + "  /></td>";
                        html += "<td><input id='chkDeleteRight' name='chkDeleteRight' class='chkDeleteRight' onclick='checkDelete(this\,\"3\",\"" + index + "\",\"" + elementValue.LoopThrough + "\");' type='checkbox'  " + (elementValue.DeleteRight == 1 ? "checked=checked" : '') + "  /></td>";
                        html += "<td><input id='chkDisplayRight' name='chkDisplayRight' class='chkDisplayRight' onclick='checkDisplay(this\,\"3\",\"" + index + "\",\"" + elementValue.LoopThrough + "\");' type='checkbox'  " + (elementValue.DisplayRight == 1 ? "checked=checked" : '') + "  /></td>";
                        html += '</tr>';

                        $("#tblRights>tbody").append(html);
                    });
                }
            }
        });
    }
    

    $("#btnBuild").click(function () {
        
        var roleID = $("#Role").val();
        var moduleID = $("#Module").val();
        var submoduleID = $("#SubModule").val();
        var errMsg = "";
        if (roleID == 0) {
            errMsg = errMsg + "Role is required<br/>";
        }
        if (moduleID == 0) {
            errMsg = errMsg + "Module is required<br/>";
        }

        if (errMsg != "") {
            $("#divLoader").hide();
            $.alert({
                title: 'Alert!',
                content: "Following error need to fixed:<br/>" + errMsg,
                type: 'blue'
            });
            return;
        }

        if (submoduleID != 0)
        {
            moduleID = submoduleID;
        }

        FillRoleModuleRightsList(roleID, moduleID);
        
    });

    $("#btnSave").click(function () {
        $("#divLoader").show();
        var roleID = $("#Role").val();
        var moduleID = $("#Module").val();
        var submoduleID = $("#SubModule").val();
        var errMsg = "";

        if (roleID == "0") {
            errMsg = errMsg + "Role is required<br/>";
        }
        if (moduleID == "0") {
            errMsg = errMsg + "Module is required<br/>";
        }

        if (errMsg != "") {
            $("#divLoader").hide();
            $.alert({
                title: 'Alert!',
                content: "Following error need to fixed:<br/>" + errMsg,
                type: 'red'
            });
            return;
        }
        var tblRights = $('#tblRights').find('tbody').find('tr');
        var roleModules = [];

        for (var i = 0; i < tblRights.length; i++) {
            var id = $(tblRights[i]).find('td:eq(0)').html()
            var resourceID = $(tblRights[i]).find('td:eq(1)').html();
            var isForSave = $(tblRights[i]).find('td:eq(2)').html()
            var specialRight = $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkSpecialRight]').is(":checked");
            var isFull = $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkFull]').is(":checked");
            var EditRight = $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkEditRight]').is(":checked");
            var AddRight = $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkAddRight]').is(":checked");
            var DeleteRight = $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkDeleteRight]').is(":checked");
            var DisplayRight = $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkDisplayRight]').is(":checked");
            var accessLevel = 0;
            if (EditRight == 1) {
                accessLevel = accessLevel + 8;
            }
            if (AddRight == 1) {
                accessLevel = accessLevel + 4;
            }
            if (DeleteRight == 1) {
                accessLevel = accessLevel + 2;
            }
            if (DisplayRight == 1) {
                accessLevel = accessLevel + 1;
            }
            if (specialRight == 1)
            {
                accessLevel = 0;
            }
            if (isForSave == "1") {
                var rightInfo = {
                    RoleID: roleID,
                    ModuleID: moduleID,
                    SubModuleID: submoduleID,
                    ResourceID: resourceID,
                    AccessLevel: accessLevel,
                    HasSpecial: specialRight
                };
                roleModules.push(rightInfo);
            }
        }

        $.ajax({
            url: "../AssignRole/Create?RoleID=" + roleID + "&ModuleID=" + moduleID + "&SubModuleID=" + submoduleID,
            type: "post",
            data: JSON.stringify(roleModules),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            beforeSend: function () {
              
            },
            complete: function () {
               
            },
            success: function (response) {
                $("#divLoader").hide();
                if (response.result == "1") {
                    $.alert({
                        title: 'Alert!',
                        content: "Assign Role SUccessfully.",
                        type: 'blue'
                    });
                    return;
                }
            }
        });

        return false;
    });
});

var fillRole = function () {
    $("#divLoader").show();
    var msg = "";
    $("#Role").empty();
    $.ajax({
        url: '../AssignRole/GetRoles',
        type: "post",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#divLoader").hide();
            if ($.trim(response.error) != "") {
                this.cancelChanges();
            } else {
                $.each(response, function (index, elementValue) {
                   
                    $("#Role").append("<option value='" + elementValue.RoleID + "'>" + elementValue.RoleName + "</option>");
                });
                $("#Role").val(0).trigger('change');

            }
        }
    });
}
function fillModule(RoleId) {
    $("#divLoader").show();
    //var params = { RoleID: RoleId };
    $("#Module").empty();
    
    $.ajax({
        url: '/AssignRole/GetModules?RoleID='+RoleId,
        type: "post",
        //data: JSON.stringify(params),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#divLoader").hide();
            if ($.trim(response.error) != "") {
                
            } else {
                $.each(response, function (index, elementValue) {
                    $("#Module").append("<option value='" + elementValue.ModuleID + "'>" + elementValue.ModuleName + "</option>");
                });
                $("#Module").val(0).trigger('change');
            }
        }
    });
    //$("#Role").chosen({ disable_search: true, width: "100%" });
}
function fillSubModule(ModelId) {
    $("#divLoader").show();
    //var params = { RoleID: RoleId };
    $("#SubModule").empty();

    $.ajax({
        url: '/AssignRole/GetSubModules?ModelID=' + ModelId,
        type: "post",
        //data: JSON.stringify(params),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#divLoader").hide();
            if ($.trim(response.error) != "") {

            } else {

                $.each(response, function (index, elementValue) {
                    $("#SubModule").append("<option value='" + elementValue.ModuleID + "'>" + elementValue.ModuleName + "</option>");
                    $("#SubModule").val(0).trigger('change');
                });

            }
        }
    });
    //$("#Role").chosen({ disable_search: true, width: "100%" });
}
var checkFull = function (cont,cellIndex, rowIndex, loopThrough) {
    var tblRights = $('#tblRights').find('tbody').find('tr');
    if (cellIndex == "2" && rowIndex == "0") {
        $('.chkFull').prop('checked', $(cont).is(':checked'));
        $('.chkEditRight').prop('checked', $(cont).is(':checked'));
        $('.chkAddRight').prop('checked', $(cont).is(':checked'));
        $('.chkDeleteRight').prop('checked', $(cont).is(':checked'));
        $('.chkDisplayRight').prop('checked', $(cont).is(':checked'));
    }
    else {
        var LoopThrough = loopThrough.split('-');
        var LoopStart = LoopThrough[0] - 1;
        var LoopEnd = LoopThrough[1];
        for (var i = LoopStart; i < LoopEnd; i++) {
            var id = $(tblRights[i]).find('td:eq(0)').html()
            $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkFull]').prop('checked', $(cont).is(':checked'));
            $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkEditRight]').prop('checked', $(cont).is(':checked'));
            $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkAddRight]').prop('checked', $(cont).is(':checked'));
            $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkDeleteRight]').prop('checked', $(cont).is(':checked'));
            $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkDisplayRight]').prop('checked', $(cont).is(':checked'));
        }
    }
    validateGrid();
}
var checkEdit = function (cont, cellIndex, rowIndex, loopThrough) {
    var tblRights = $('#tblRights').find('tbody').find('tr');
    if (cellIndex == "3" && rowIndex == "0") {
        if ($(cont).is(':checked') == false) {
            $('.chkFull').prop('checked', $(cont).is(':checked'));
            $('.chkEditRight').prop('checked', $(cont).is(':checked'));
        }
        else {
            $('.chkEditRight').prop('checked', $(cont).is(':checked'));
            $('.chkDisplayRight').prop('checked', $(cont).is(':checked'));

            var id = $(tblRights[rowIndex]).find('td:eq(0)').html();
            var chkEdit = $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkEditRight]').is(':checked');
            var chkAdd = $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkAddRight]').is(':checked');
            var chkDelete = $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkDeleteRight]').is(':checked');
            var chkDisplay = $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkDisplayRight]').is(':checked');

            if (chkEdit==true && chkAdd==true && chkDelete==true && chkDisplay==true) {
                $('.chkFull').prop('checked', $(cont).is(':checked'));
            }
        }
    }
    else {
        var LoopThrough = loopThrough.split('-');
        var LoopStart = LoopThrough[0] - 1;
        var LoopEnd = LoopThrough[1];
        for (var i = LoopStart; i < LoopEnd; i++) {
            var id = $(tblRights[i]).find('td:eq(0)').html()
            if ($(cont).is(':checked') == false) {
                $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkFull]').prop('checked', $(cont).is(':checked'));
                $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkFull]').prop('checked', $(cont).is(':checked'));
            }

            $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkEditRight]').prop('checked', $(cont).is(':checked'));

            if ($(cont).is(':checked')) {
                $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkDisplayRight]').prop('checked', $(cont).is(':checked'));
            }
        }

        for (var i = LoopStart; i < LoopEnd; i++) {
            var id = $(tblRights[i]).find('td:eq(0)').html();
            var chkEdit = $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkEditRight]').is(':checked');
            var chkAdd = $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkAddRight]').is(':checked');
            var chkDelete = $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkDeleteRight]').is(':checked');
            var chkDisplay = $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkDisplayRight]').is(':checked');

            if (chkEdit == true && chkAdd == true && chkDelete == true && chkDisplay == true) {
                $('.chkFull').prop('checked', $(cont).is(':checked'));
            }
        }
    }
    validateGrid();
}
var checkAdd = function (cont, cellIndex, rowIndex, loopThrough) {
    var tblRights = $('#tblRights').find('tbody').find('tr');
    if (cellIndex == "4" && rowIndex == "0") {
        if ($(cont).is(':checked') == false) {
            $('.chkFull').prop('checked', $(cont).is(':checked'));
            $('.chkAddRight').prop('checked', $(cont).is(':checked'));
        }
        else {
            $('.chkAddRight').prop('checked', $(cont).is(':checked'));
            $('.chkDisplayRight').prop('checked', $(cont).is(':checked'));

            var id = $(tblRights[rowIndex]).find('td:eq(0)').html();
            var chkEdit = $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkEditRight]').is(':checked');
            var chkAdd = $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkAddRight]').is(':checked');
            var chkDelete = $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkDeleteRight]').is(':checked');
            var chkDisplay = $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkDisplayRight]').is(':checked');

            if (chkEdit == true && chkAdd == true && chkDelete == true && chkDisplay == true) {
                $('.chkFull').prop('checked', $(cont).is(':checked'));
            }
        }
    }
    else {
        var LoopThrough = loopThrough.split('-');
        var LoopStart = LoopThrough[0] - 1;
        var LoopEnd = LoopThrough[1];

        for (var i = LoopStart; i < LoopEnd; i++) {
            var id = $(tblRights[i]).find('td:eq(0)').html()
            if ($(cont).is(':checked') == false) {
                $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkFull]').prop('checked', $(cont).is(':checked'));
                $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkFull]').prop('checked', $(cont).is(':checked'));
            }

            $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkAddRight]').prop('checked', $(cont).is(':checked'));

            if ($(cont).is(':checked')) {
                $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkDisplayRight]').prop('checked', $(cont).is(':checked'));
            }
        }

        for (var i = LoopStart; i < LoopEnd; i++) {
            var id = $(tblRights[i]).find('td:eq(0)').html();
            var chkEdit = $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkEditRight]').is(':checked');
            var chkAdd = $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkAddRight]').is(':checked');
            var chkDelete = $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkDeleteRight]').is(':checked');
            var chkDisplay = $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkDisplayRight]').is(':checked');

            if (chkEdit == true && chkAdd == true && chkDelete == true && chkDisplay == true) {
                $('.chkFull').prop('checked', $(cont).is(':checked'));
            }
        }
    }
    validateGrid();
}
var checkDelete = function (cont, cellIndex, rowIndex, loopThrough) {
    var tblRights = $('#tblRights').find('tbody').find('tr');
    if (cellIndex == "5" && rowIndex == "0") {
        if ($(cont).is(':checked') == false) {
            $('.chkFull').prop('checked', $(cont).is(':checked'));
            $('.chkDeleteRight').prop('checked', $(cont).is(':checked'));
        }
        else {
            $('.chkDeleteRight').prop('checked', $(cont).is(':checked'));
            $('.chkDisplayRight').prop('checked', $(cont).is(':checked'));

            var id = $(tblRights[rowIndex]).find('td:eq(0)').html();
            var chkEdit = $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkEditRight]').is(':checked');
            var chkAdd = $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkAddRight]').is(':checked');
            var chkDelete = $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkDeleteRight]').is(':checked');
            var chkDisplay = $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkDisplayRight]').is(':checked');

            if (chkEdit == true && chkAdd == true && chkDelete == true && chkDisplay == true) {
                $('.chkFull').prop('checked', $(cont).is(':checked'));
            }
        }
    }
    else {
        var LoopThrough = loopThrough.split('-');
        var LoopStart = LoopThrough[0] - 1;
        var LoopEnd = LoopThrough[1];
        
        for (var i = LoopStart; i < LoopEnd; i++) {
            var id = $(tblRights[i]).find('td:eq(0)').html()
            if ($(cont).is(':checked') == false) {
                $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkFull]').prop('checked', $(cont).is(':checked'));
                $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkFull]').prop('checked', $(cont).is(':checked'));
            }

            $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkDeleteRight]').prop('checked', $(cont).is(':checked'));

            if ($(cont).is(':checked')) {
                $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkDisplayRight]').prop('checked', $(cont).is(':checked'));
            }
        }

        for (var i = LoopStart; i < LoopEnd; i++) {
            var id = $(tblRights[i]).find('td:eq(0)').html();
            var chkEdit = $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkEditRight]').is(':checked');
            var chkAdd = $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkAddRight]').is(':checked');
            var chkDelete = $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkDeleteRight]').is(':checked');
            var chkDisplay = $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkDisplayRight]').is(':checked');

            if (chkEdit == true && chkAdd == true && chkDelete == true && chkDisplay == true) {
                $('.chkFull').prop('checked', $(cont).is(':checked'));
            }
        }
    }
    validateGrid();
}
var checkDisplay = function (cont, cellIndex, rowIndex, loopThrough) {
    var tblRights = $('#tblRights').find('tbody').find('tr');
    if (cellIndex == "6" && rowIndex == "0") {
        if ($(cont).is(':checked') == false) {
            $('.chkFull').prop('checked', $(cont).is(':checked'));
            $('.chkDisplayRight').prop('checked', $(cont).is(':checked'));
        }
        else {
            $('.chkDeleteRight').prop('checked', $(cont).is(':checked'));
            $('.chkDisplayRight').prop('checked', $(cont).is(':checked'));

            var id = $(tblRights[rowIndex]).find('td:eq(0)').html();
            var chkEdit = $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkEditRight]').is(':checked');
            var chkAdd = $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkAddRight]').is(':checked');
            var chkDelete = $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkDeleteRight]').is(':checked');
            var chkDisplay = $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkDisplayRight]').is(':checked');

            if (chkEdit == true && chkAdd == true && chkDelete == true && chkDisplay == true) {
                $('.chkFull').prop('checked', $(cont).is(':checked'));
            }
        }
    }
    else {
        var LoopThrough = loopThrough.split('-');
        var LoopStart = LoopThrough[0] - 1;
        var LoopEnd = LoopThrough[1];
        

        for (var i = LoopStart; i < LoopEnd; i++) {
            var id = $(tblRights[i]).find('td:eq(0)').html()
            if ($(cont).is(':checked') == false) {
                $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkFull]').prop('checked', $(cont).is(':checked'));
                $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkFull]').prop('checked', $(cont).is(':checked'));
            }

            $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkDisplayRight]').prop('checked', $(cont).is(':checked'));

            if ($(cont).is(':checked')) {
                $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkDisplayRight]').prop('checked', $(cont).is(':checked'));
            }
        }

        for (var i = LoopStart; i < LoopEnd; i++) {
            var id = $(tblRights[i]).find('td:eq(0)').html();
            var chkEdit = $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkEditRight]').is(':checked');
            var chkAdd = $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkAddRight]').is(':checked');
            var chkDelete = $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkDeleteRight]').is(':checked');
            var chkDisplay = $("#tblRights tbody").find("tr[data-value=" + id + "]").find('input[class=chkDisplayRight]').is(':checked');
            if (chkEdit == true && chkAdd == true && chkDelete == true && chkDisplay == true) {
                $('.chkFull').prop('checked', $(cont).is(':checked'));
            }
        }
    }
    validateGrid();
}
var validateGrid = function () {
    var tblRights = $('#tblRights').find('tbody').find('tr');
    var LoopValidate = "";

    for (var i = tblRights.length - 1; i >= 0; i--) {
        var resourceID = $(tblRights[i]).find('td:eq(1)').html();
        var LoopThrough = $(tblRights[i]).find('td:eq(3)').html().split('-');;
        var LoopStart = LoopThrough[0] - 1;
        var LoopEnd = LoopThrough[1];
        var IsForSave = $(tblRights[i]).find('td:eq(2)').html();
        var FullAll = 1;
        var EditAll = 1;
        var AddAll = 1;
        var DeleteAll = 1;
        var DisplayAll = 1;
        var uidtop = $(tblRights[i]).find('td:eq(0)').html();
        if (IsForSave == "0") {
            
            for (var j = LoopEnd - 1; j > LoopStart; j--) {
                var uid = $(tblRights[j]).find('td:eq(0)').html();
                var chkFull = $("#tblRights tbody").find("tr[data-value=" + uid + "]").find('input[class=chkFull]').is(':checked');
                if (!chkFull) {
                    FullAll = 0;
                    break;
                }
            }
            for (var j = LoopEnd - 1; j > LoopStart; j--) {
                var uid = $(tblRights[j]).find('td:eq(0)').html();
                var chkEdit = $("#tblRights tbody").find("tr[data-value=" + uid + "]").find('input[class=chkEditRight]').is(':checked');
                if (!chkEdit) {
                    EditAll = 0;
                    break;
                }
            }
            for (var j = LoopEnd - 1; j > LoopStart; j--) {
                var uid = $(tblRights[j]).find('td:eq(0)').html();
                var chkAdd = $("#tblRights tbody").find("tr[data-value=" + uid + "]").find('input[class=chkAddRight]').is(':checked');
                if (!chkAdd) {
                    AddAll = 0;
                    break;
                }
            }
            for (var j = LoopEnd - 1; j > LoopStart; j--) {
                var uid = $(tblRights[j]).find('td:eq(0)').html();
                var chkDelete = $("#tblRights tbody").find("tr[data-value=" + uid + "]").find('input[class=chkDeleteRight]').is(':checked');
                if (!chkDelete) {
                    DeleteAll = 0;
                    break;
                }
            }
            for (var j = LoopEnd - 1; j > LoopStart; j--) {
                var uid = $(tblRights[j]).find('td:eq(0)').html();
                var chkDisplay = $("#tblRights tbody").find("tr[data-value=" + uid + "]").find('input[class=chkDisplayRight]').is(':checked');
                if (!chkDisplay) {
                    DisplayAll = 0;
                    break;
                }
            }
            $("#tblRights tbody").find("tr[data-value=" + uidtop + "]").find('input[class=chkFull]').prop('checked', (FullAll == 1 ? true : false));
            $("#tblRights tbody").find("tr[data-value=" + uidtop + "]").find('input[class=chkEditRight]').prop('checked', (EditAll == 1 ? true : false));
            $("#tblRights tbody").find("tr[data-value=" + uidtop + "]").find('input[class=chkAddRight]').prop('checked', (AddAll == 1 ? true : false));
            $("#tblRights tbody").find("tr[data-value=" + uidtop + "]").find('input[class=chkDeleteRight]').prop('checked', (DeleteAll == 1 ? true : false));
            $("#tblRights tbody").find("tr[data-value=" + uidtop + "]").find('input[class=chkDisplayRight]').prop('checked', (DisplayAll == 1 ? true : false));
        }
    }
}