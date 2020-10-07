$(document).ready(function () {
    fillRole();
    $("#Role").on('change', function (evt, params) {
        var selected = $(this).val();
        alert(selected);
        if (selected != null) {
            fillModule(selected);
        }
    });
    var Module;
    var Role;
    var SubModel;

    if ($("#hfEditRight").val() == "1" || $("#hfAddRight").val() == "1") {
        $("#btnSave").removeClass("disabled");
    } else {
        $("#btnSave").addClass("disabled");
    }
   
    function RoleChange() {
        $("#divGridAndButton").hide();
        FillModules(Role.val());
    }

    //FillModules(0);

    //function FillModules(RoleId) {
    //    var params = { RoleID: RoleId };
    //    var modulesDataSource = new kendo.data.DataSource({
    //        error: function (e) {
    //            //showError("",e.errors);
    //            this.cancelChanges();
    //        },
    //        transport: {
    //            read: {
    //                url: '../AssignRole/GetModules',
    //                data:params
    //            }
    //        }
    //    });

    //    Module = $("#Module").kendoDropDownList({
    //        dataTextField: "ModuleName",
    //        dataValueField: "ModuleID",
    //        change: ModuleChange,
    //        dataSource: modulesDataSource,
    //        dataType: "JSON"
    //    });
    //    $("#Module").data("kendoDropDownList").value("0");
    //    FillSubModules(0);
    //    $("#SubModule").data("kendoDropDownList").value("0");
    //}

    //FillSubModules(0);

    //function FillSubModules(ModelId) {
    //    var params = { ModelID: ModelId };
    //    var submodulesDataSource = new kendo.data.DataSource({
    //        error: function (e) {
    //            //showError("",e.errors);
    //            this.cancelChanges();
    //        },
    //        transport: {
    //            read: {
    //                url: '../AssignRole/GetSubModules',
    //                data: params
    //            }
    //        }
    //    });

    //    SubModule = $("#SubModule").kendoDropDownList({
    //        dataTextField: "ModuleName",
    //        dataValueField: "ModuleID",
    //        change: SubModuleChange,
    //        dataSource: submodulesDataSource,
    //        dataType: "JSON"
    //    });
    //    $("#SubModule").data("kendoDropDownList").value("0");
    //}

    //function FillRoleModuleRightsList(RoleId, ModuleId) {
    //    var params = { RoleID: RoleId, ModuleID: ModuleId };
    //    var dataSource = new kendo.data.DataSource({
    //        error: function (e) {
    //            //showError("",e.errors);
    //            this.cancelChanges();
    //        },
    //        transport: {
    //            read: {
    //                url: '../AssignRole/RoleModuleRightsList',
    //                data: params
    //            }
    //        },
    //        schema: {
    //            errors: "error",
    //            model: {
    //                id: "ID",
    //                fields: {
    //                    ID: { editable: false },
    //                    ResourceId: { type: "int" },
    //                    IsForSave: { type: "int" },
    //                    LoopThrough: { type: "string" },
    //                    SpecialRight: { type: "int" },
    //                    FullRight: { type: "int" },
    //                    EditRight: { type: "int" },
    //                    AddRight: { type: "int" },
    //                    DeleteRight: { type: "int" },
    //                    DisplayRight: { type: "int" }
    //                }
    //            }
    //        }
    //    });

    //    $("#rightsGrid").kendoGrid({
    //        dataSource: dataSource,
    //        pageable: false,
    //        height: 500,
    //        columns: [
    //            { field: "Resource", title: "Resource", encoded: false, width: "40%" },
    //            { field: "SpecialRight", title: "Special", template: "<input  type='checkbox' name='chkspecial' id='chkspecial' class='chkspecial' #= SpecialRight==1 ? checked='checked' : '' # style='  #= IsSpecialRight==1 ? 'display:block' : 'display:none' #'  />", width: "10%" },
    //            { field: "FullRight", title: "Full", template: "<input  type='checkbox' name='chkfull' id='chkfull' class='chkfull' #= FullRight==1 ? checked='checked' : '' # style='  #= IsSpecialRight==0 ? 'display:block' : 'display:none' #'/>", width: "10%" },
    //            { field: "EditRight", title: "Edit", template: "<input  type='checkbox' name='chkedit' id='chkedit' class='chkedit' #= EditRight==1 ? checked='checked' : '' # style='  #= IsSpecialRight==0 ? 'display:block' : 'display:none' #'/>", width: "10%" },
    //            { field: "AddRight", title: "Add", template: "<input  type='checkbox' name='chkadd' id='chkadd' class='chkadd' #= AddRight==1 ? checked='checked' : '' # style='  #= IsSpecialRight==0 ? 'display:block' : 'display:none' #'/>", width: "10%" },
    //            { field: "DeleteRight", title: "Delete", template: "<input  type='checkbox' name='chkdelete' id='chkdelete' class='chkdelete' #= DeleteRight==1 ? checked='checked' : '' # style='  #= IsSpecialRight==0 ? 'display:block' : 'display:none' #'/>", width: "10%" },
    //            { field: "DisplayRight", title: "Display", template: "<input  type='checkbox' name='chkdisplay' id='chkdisplay' class='chkdisplay' #= DisplayRight==1 ? checked='checked' : '' # style='  #= IsSpecialRight==0 ? 'display:block' : 'display:none' #'/>", width: "10%" }
    //        ]
    //    });
    //    $("#divGridAndButton").show();
    //    $("#rightsGrid").data("kendoGrid").refresh();
    //    validateGrid();
    //}

    //$('#rightsGrid').on("click", ".chkfull", function (e) {
    //    var grid = $("#rightsGrid").data("kendoGrid");
    //    var dataView = grid.dataSource.view();
    //    var rowIndex = $(this).parent().parent().index();
    //    var cellIndex = $(this).parent().index();
    //    if (cellIndex == 2 && rowIndex == 0) {
    //        $('.chkfull').prop('checked', this.checked);
    //        $('.chkedit').prop('checked', this.checked);
    //        $('.chkadd').prop('checked', this.checked);
    //        $('.chkdelete').prop('checked', this.checked);
    //        $('.chkdisplay').prop('checked', this.checked);
    //    }
    //    else {
    //        var LoopThrough = dataView[rowIndex].LoopThrough.split('-');
    //        var LoopStart = LoopThrough[0] - 1;
    //        var LoopEnd = LoopThrough[1];
    //        for (var i = LoopStart; i < LoopEnd; i++) {
    //            var uid = dataView[i].uid;
    //            $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkfull]').prop('checked', this.checked);
    //            $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkedit]').prop('checked', this.checked);
    //            $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkadd]').prop('checked', this.checked);
    //            $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkdelete]').prop('checked', this.checked);
    //            $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkdisplay]').prop('checked', this.checked);
    //        }
    //    }
    //    validateGrid();
    //});

    //$('#rightsGrid').on("click", ".chkedit", function (e) {
    //    var grid = $("#rightsGrid").data("kendoGrid");
    //    var dataView = grid.dataSource.view();
    //    var rowIndex = $(this).parent().parent().index();
    //    var cellIndex = $(this).parent().index();
    //    if (cellIndex == 3 && rowIndex == 0) {
    //        if (this.checked == false) {
    //            $('.chkfull').prop('checked', this.checked);
    //            $('.chkedit').prop('checked', this.checked);
    //        }
    //        else {
    //            $('.chkedit').prop('checked', this.checked);
    //            var uid = dataView[rowIndex].uid;
    //            $('.chkdisplay').prop('checked', this.checked);
    //            var chkEdit = $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkedit]:checked').val();
    //            var chkAdd = $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkadd]:checked').val();
    //            var chkDelete = $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkdelete]:checked').val();
    //            var chkDisplay = $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkdisplay]:checked').val();
    //            if (chkEdit + "" == "on" && chkAdd + "" == "on" && chkDelete + "" == "on" && chkDisplay + "" == "on") {
    //                $('.chkfull').prop('checked', this.checked);
    //            }
    //        }
    //    }
    //    else {
    //        var LoopThrough = dataView[rowIndex].LoopThrough.split('-');
    //        var LoopStart = LoopThrough[0] - 1;
    //        var LoopEnd = LoopThrough[1];
    //        var AllChecked = 0;
    //        for (var i = LoopStart; i < LoopEnd; i++) {
    //            var uid = dataView[i].uid;
    //            if (this.checked == false) {
    //                $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkfull]').prop('checked', this.checked);
    //            }
    //            $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkedit]').prop('checked', this.checked);
    //            if (this.checked == true) {
    //                $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkdisplay]').prop('checked', this.checked);
    //            }
    //        }
    //        for (var i = LoopStart; i < LoopEnd; i++) {
    //            var uid = dataView[i].uid;
    //            var chkEdit = $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkedit]:checked').val();
    //            var chkAdd = $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkadd]:checked').val();
    //            var chkDelete = $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkdelete]:checked').val();
    //            var chkDisplay = $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkdisplay]:checked').val();
    //            if (chkEdit + "" == "on" && chkAdd + "" == "on" && chkDelete + "" == "on" && chkDisplay + "" == "on") {
    //                $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkfull]').prop('checked', this.checked);
    //            }
    //        }
    //    }
    //    validateGrid();
    //});

    //$('#rightsGrid').on("click", ".chkadd", function (e) {
    //    var grid = $("#rightsGrid").data("kendoGrid");
    //    var dataView = grid.dataSource.view();
    //    var rowIndex = $(this).parent().parent().index();
    //    var cellIndex = $(this).parent().index();
    //    if (cellIndex == 4 && rowIndex == 0) {
    //        if (this.checked == false) {
    //            $('.chkfull').prop('checked', this.checked);
    //            $('.chkadd').prop('checked', this.checked);
    //        }
    //        else {
    //            $('.chkadd').prop('checked', this.checked);
    //            var uid = dataView[rowIndex].uid;
    //            $('.chkdisplay').prop('checked', this.checked);
    //            var chkEdit = $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkedit]:checked').val();
    //            var chkAdd = $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkadd]:checked').val();
    //            var chkDelete = $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkdelete]:checked').val();
    //            var chkDisplay = $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkdisplay]:checked').val();
    //            if (chkEdit + "" == "on" && chkAdd + "" == "on" && chkDelete + "" == "on" && chkDisplay + "" == "on") {
    //                $('.chkfull').prop('checked', this.checked);
    //            }
    //        }
    //    }
    //    else {
    //        var LoopThrough = dataView[rowIndex].LoopThrough.split('-');
    //        var LoopStart = LoopThrough[0] - 1;
    //        var LoopEnd = LoopThrough[1];
    //        for (var i = LoopStart; i < LoopEnd; i++) {
    //            var uid = dataView[i].uid;
    //            if (this.checked == false) {
    //                $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkfull]').prop('checked', this.checked);
    //            }
    //            $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkadd]').prop('checked', this.checked);
    //            if (this.checked == true) {
    //                $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkdisplay]').prop('checked', this.checked);
    //            }
    //        }
    //        for (var i = LoopStart; i < LoopEnd; i++) {
    //            var uid = dataView[i].uid;
    //            var chkEdit = $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkedit]:checked').val();
    //            var chkAdd = $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkadd]:checked').val();
    //            var chkDelete = $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkdelete]:checked').val();
    //            var chkDisplay = $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkdisplay]:checked').val();
    //            if (chkEdit + "" == "on" && chkAdd + "" == "on" && chkDelete + "" == "on" && chkDisplay + "" == "on") {
    //                $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkfull]').prop('checked', this.checked);
    //            }
    //        }
    //    }
    //    validateGrid();
    //});

    //$('#rightsGrid').on("click", ".chkdelete", function (e) {
    //    var grid = $("#rightsGrid").data("kendoGrid");
    //    var dataView = grid.dataSource.view();
    //    var rowIndex = $(this).parent().parent().index();
    //    var cellIndex = $(this).parent().index();
    //    if (cellIndex == 5 && rowIndex == 0) {
    //        if (this.checked == false) {
    //            $('.chkfull').prop('checked', this.checked);
    //            $('.chkdelete').prop('checked', this.checked);
    //        }
    //        else {
    //            $('.chkdelete').prop('checked', this.checked);
    //            var uid = dataView[rowIndex].uid;
    //            $('.chkdisplay').prop('checked', this.checked);
    //            var chkEdit = $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkedit]:checked').val();
    //            var chkAdd = $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkadd]:checked').val();
    //            var chkDelete = $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkdelete]:checked').val();
    //            var chkDisplay = $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkdisplay]:checked').val();
    //            if (chkEdit + "" == "on" && chkAdd + "" == "on" && chkDelete + "" == "on" && chkDisplay + "" == "on") {
    //                $('.chkfull').prop('checked', this.checked);
    //            }
    //        }
    //    }
    //    else {
    //        var LoopThrough = dataView[rowIndex].LoopThrough.split('-');
    //        var LoopStart = LoopThrough[0] - 1;
    //        var LoopEnd = LoopThrough[1];
    //        for (var i = LoopStart; i < LoopEnd; i++) {
    //            var uid = dataView[i].uid;
    //            if (this.checked == false) {
    //                $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkfull]').prop('checked', this.checked);
    //            }
    //            $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkdelete]').prop('checked', this.checked);
    //            if (this.checked == true) {
    //                $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkdisplay]').prop('checked', this.checked);
    //            }
    //        }
    //        for (var i = LoopStart; i < LoopEnd; i++) {
    //            var uid = dataView[i].uid;
    //            var chkEdit = $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkedit]:checked').val();
    //            var chkAdd = $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkadd]:checked').val();
    //            var chkDelete = $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkdelete]:checked').val();
    //            var chkDisplay = $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkdisplay]:checked').val();
    //            if (chkEdit + "" == "on" && chkAdd + "" == "on" && chkDelete + "" == "on" && chkDisplay + "" == "on") {
    //                $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkfull]').prop('checked', this.checked);
    //            }
    //        }
    //    }
    //    validateGrid();
    //});

    //$('#rightsGrid').on("click", ".chkdisplay", function (e) {
    //    var grid = $("#rightsGrid").data("kendoGrid");
    //    var dataView = grid.dataSource.view();
    //    var rowIndex = $(this).parent().parent().index();
    //    var cellIndex = $(this).parent().index();
    //    if (cellIndex == 6 && rowIndex == 0) {
    //        if (this.checked == false) {
    //            $('.chkfull').prop('checked', this.checked);
    //            $('.chkdisplay').prop('checked', this.checked);
    //        }
    //        else {
    //            var uid = dataView[rowIndex].uid;
    //            $('.chkdisplay').prop('checked', this.checked);
    //            var chkEdit = $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkedit]:checked').val();
    //            var chkAdd = $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkadd]:checked').val();
    //            var chkDelete = $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkdelete]:checked').val();
    //            var chkDisplay = $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkdisplay]:checked').val();
    //            if (chkEdit + "" == "on" && chkAdd + "" == "on" && chkDelete + "" == "on" && chkDisplay + "" == "on") {
    //                $('.chkfull').prop('checked', this.checked);
    //            }
    //        }
    //    }
    //    else {
    //        var LoopThrough = dataView[rowIndex].LoopThrough.split('-');
    //        var LoopStart = LoopThrough[0] - 1;
    //        var LoopEnd = LoopThrough[1];
    //        for (var i = LoopStart; i < LoopEnd; i++) {
    //            var uid = dataView[i].uid;
    //            if (this.checked == false) {
    //                $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkfull]').prop('checked', this.checked);
    //            }
    //            $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkdisplay]').prop('checked', this.checked);
    //            if (this.checked == true) {
    //                $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkdisplay]').prop('checked', this.checked);
    //            }
    //        }
    //        for (var i = LoopStart; i < LoopEnd; i++) {
    //            var uid = dataView[i].uid;
    //            var chkEdit = $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkedit]:checked').val();
    //            var chkAdd = $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkadd]:checked').val();
    //            var chkDelete = $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkdelete]:checked').val();
    //            var chkDisplay = $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkdisplay]:checked').val();
    //            if (chkEdit + "" == "on" && chkAdd + "" == "on" && chkDelete + "" == "on" && chkDisplay + "" == "on") {
    //                $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkfull]').prop('checked', this.checked);
    //            }
    //        }

    //    }
    //    validateGrid();
    //});

    //function validateGrid() {
    //    var grid = $("#rightsGrid").data("kendoGrid");
    //    var dataView = grid.dataSource.view();
    //    var LoopValidate = "";

    //    for (var i = dataView.length - 1; i >= 0; i--) {
    //        var grid = $("#rightsGrid").data("kendoGrid");
    //        var dataView = grid.dataSource.view();
    //        var LoopThrough = dataView[i].LoopThrough.split('-');
    //        var LoopStart = LoopThrough[0] - 1;
    //        var LoopEnd = LoopThrough[1];
    //        var IsForSave = dataView[i].IsForSave;
    //        var FullAll = 1;
    //        var EditAll = 1;
    //        var AddAll = 1;
    //        var DeleteAll = 1;
    //        var DisplayAll = 1;
    //        var uidtop = dataView[i].uid;
    //        if (IsForSave == 0) {
    //            for (var j = LoopEnd - 1; j > LoopStart; j--) {
    //                var uid = dataView[j].uid;
    //                var chkFull = $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkfull]:checked').val();
    //                if (chkFull + "" != "on") {
    //                    FullAll = 0;
    //                    break;
    //                }
    //            }
    //            for (var j = LoopEnd - 1; j > LoopStart; j--) {
    //                var uid = dataView[j].uid;
    //                var chkEdit = $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkedit]:checked').val();
    //                if (chkEdit + "" != "on") {
    //                    EditAll = 0;
    //                    break;
    //                }
    //            }
    //            for (var j = LoopEnd - 1; j > LoopStart; j--) {
    //                var uid = dataView[j].uid;
    //                var chkAdd = $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkadd]:checked').val();
    //                if (chkAdd + "" != "on") {
    //                    AddAll = 0;
    //                    break;
    //                }
    //            }
    //            for (var j = LoopEnd - 1; j > LoopStart; j--) {
    //                var uid = dataView[j].uid;
    //                var chkDelete = $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkdelete]:checked').val();
    //                if (chkDelete + "" != "on") {
    //                    DeleteAll = 0;
    //                    break;
    //                }
    //            }
    //            for (var j = LoopEnd - 1; j > LoopStart; j--) {
    //                var uid = dataView[j].uid;
    //                var chkDisplay = $("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkdisplay]:checked').val();
    //                if (chkDisplay + "" != "on") {
    //                    DisplayAll = 0;
    //                    break;
    //                }
    //            }

    //            $("#rightsGrid tbody").find("tr[data-uid=" + uidtop + "]").find('input[id=chkfull]').prop('checked', (FullAll == 1 ? true : false));
    //            $("#rightsGrid tbody").find("tr[data-uid=" + uidtop + "]").find('input[id=chkedit]').prop('checked', (EditAll == 1 ? true : false));
    //            $("#rightsGrid tbody").find("tr[data-uid=" + uidtop + "]").find('input[id=chkadd]').prop('checked', (AddAll == 1 ? true : false));
    //            $("#rightsGrid tbody").find("tr[data-uid=" + uidtop + "]").find('input[id=chkdelete]').prop('checked', (DeleteAll == 1 ? true : false));
    //            $("#rightsGrid tbody").find("tr[data-uid=" + uidtop + "]").find('input[id=chkdisplay]').prop('checked', (DisplayAll == 1 ? true : false));

    //        }
    //    }
    //}

    //$("#btnBuild").click(function(){
    //    var roleID = Role.val();
    //    var moduleID = Module.val();
    //    var submoduleID = SubModule.val();
    //    var errMsg = "";
    //    if (roleID == 0) {
    //        errMsg = errMsg + "Role is required<br/>";
    //    }
    //    if (moduleID == 0) {
    //        errMsg = errMsg + "Module is required<br/>";
    //    }

    //    if (errMsg != "") {
    //        showError("","Following error need to fixed:<br/>" + errMsg);
    //        return;
    //    }

    //    if (submoduleID != 0)
    //    {
    //        moduleID = submoduleID;
    //    }

    //    FillRoleModuleRightsList(roleID, moduleID);
        
    //});

    //$("#btnSave").click(function () {
    //    var roleID = Role.val();
    //    var moduleID = Module.val();
    //    var submoduleID = SubModule.val();
    //    var errMsg = "";

    //    if (roleID == "0") {
    //        errMsg = errMsg + "Role is required<br/>";
    //    }
    //    if (moduleID == "0") {
    //        errMsg = errMsg + "Module is required<br/>";
    //    }

    //    if (errMsg != "") {
    //        showError("","Following error need to fixed:<br/>" + errMsg);
    //        return;
    //    }

    //    var grid = $("#rightsGrid").data("kendoGrid");
    //    var dataView = grid.dataSource.view();

    //    var roleModules = [];

    //    for (var i = 0; i < dataView.length; i++) {
    //        var uid = dataView[i].uid;
    //        var IsForSave = dataView[i].IsForSave;
    //        var resourceID = dataView[i].ResourceId;
    //        var specialRight = ($("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkspecial]:checked').val() + "" == "on" ? 1 : 0);
    //        var EditRight = ($("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkedit]:checked').val() + "" == "on" ? 1 : 0);
    //        var AddRight = ($("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkadd]:checked').val() + "" == "on" ? 1 : 0);
    //        var DeleteRight = ($("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkdelete]:checked').val() + "" == "on" ? 1 : 0);
    //        var DisplayRight = ($("#rightsGrid tbody").find("tr[data-uid=" + uid + "]").find('input[id=chkdisplay]:checked').val() + "" == "on" ? 1 : 0);
    //        var accessLevel = 0;


    //        if (EditRight == 1) {
    //            accessLevel = accessLevel + 8;
    //        }
    //        if (AddRight == 1) {
    //            accessLevel = accessLevel + 4;
    //        }
    //        if (DeleteRight == 1) {
    //            accessLevel = accessLevel + 2;
    //        }
    //        if (DisplayRight == 1) {
    //            accessLevel = accessLevel + 1;
    //        }

    //        if (specialRight == 1)
    //        {
    //            accessLevel = 0;
    //        }
    //        if (IsForSave == 1) {
    //            var rightInfo = {
    //                RoleID: roleID,
    //                ModuleID: moduleID,
    //                SubModuleID: submoduleID,
    //                ResourceID: resourceID,
    //                AccessLevel: accessLevel,
    //                HasSpecial: specialRight
    //            };
    //            roleModules.push(rightInfo);
    //        }
    //    }

    //    $.ajax({
    //        url: "../AssignRole/Create?RoleID=" + roleID + "&ModuleID=" + moduleID + "&SubModuleID=" + submoduleID,
    //        method: "post",
    //        data: JSON.stringify(roleModules),
    //        contentType: "application/json; charset=utf-8",
    //        dataType: "json",
    //        async: false,
    //        beforeSend: function () {
    //            kendo.ui.progress($("#loading"), true);
    //        },
    //        complete: function () {
    //            kendo.ui.progress($("#loading"), false);
    //        },
    //        success: function (response) {
    //            if (response.result == "1") {
    //                showError("Message",response.msg);
    //            }
    //        }
    //    });

    //    return false;
    //});

    //function ModuleChange() {
    //    $("#divGridAndButton").hide();
    //    FillSubModules(Module.val());
    //}

    //function SubModuleChange()
    //{
    //    $("#divGridAndButton").hide();
    //}
    //SubModuleChange()
});

var fillRole = function () {
   
    $("#Role").empty();
    //$("#Role").chosen("destroy");
    $.ajax({
        url: '../AssignRole/GetRoles',
        method: "post",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                this.cancelChanges();
            } else {

                $.each(response, function (index, elementValue) {
                    $("#Role").append("<option value='" + elementValue.RoleID + "'>" + elementValue.RoleName + "</option>");
                    //$('#Role').trigger('chosen:updated');
                });
               
            }
        }
    });
    //$("#Role").chosen({ disable_search: true, width: "100%" });
   
}
var fillModule = function (RoleId) {
    var param = { RoleID: RoleId };
    $("#Module").empty();
    //$("#Role").chosen("destroy");
    $.ajax({
        url: '../AssignRole/GetModules',
        method: "post",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                this.cancelChanges();
            } else {

                $.each(response, function (index, elementValue) {
                    $("#Module").append("<option value='" + elementValue.ModuleID + "'>" + elementValue.ModuleName + "</option>");
                    //$('#Role').trigger('chosen:updated');
                });

            }
        }
    });
    //$("#Role").chosen({ disable_search: true, width: "100%" });
}