
    $(document).ready(function () {

        if ($("#hfEditRight").val() == "0") {
            $("#btnUpdate").addClass("disabled");
        } else {
            $("#btnUpdate").removeClass("disabled");
        }

        if ($("#hfAddRight").val() == "0") {
            $("#btnSave").addClass("disabled");
        } else {
            $("#btnSave").removeClass("disabled");
        }

        if ($("#hfDeleteRight").val() == "0") {
            $("#btnDelete").addClass("disabled");
        } else {
            $("#btnDelete").removeClass("disabled");
        }

        $('.chkRS').on('change', function () {
            $("#hfRS").val($(this).is(':checked'));
        });

        $('.chkModelRole').on('change', function () {
            var IDs = $(this).attr('id').split('_');
            var value = $(this).is(':checked') ? 1 : 0;
            $("#hfRM_" + IDs[1]).val(value);
        });

        $("#btnBackToList").click(function () {
            $("#divLoader").show();
            $("#hfHasUsers").val("0");
            $.get('../RoleManagement/GetRoleList', function (data) {
                $('#divViewList').html(data);
                $("#RoleList").removeClass("in");
                $("#AddEditRole").removeClass("in");
                $("#RoleList").addClass("in");
                $("#RoleList").css("height", "auto");
                $("#AddEditRole").css("height", "0")
                $("#divLoader").hide();
            });
            return false;
        });

        $("#btnCancel").click(function () {
            $("#divLoader").show();
            $("#hfHasUsers").val("0");
            $.get('../RoleManagement/AddRole', function (data) {
                $('#divAddEdit').html(data);
                $("#RoleList").removeClass("in");
                $("#AddEditRole").removeClass("in");
                $("#AddEditRole").addClass("in");
                $("#AddEditRole").css("height", "auto");
                $("#RoleList").css("height", "0")
                $("#divLoader").hide();
            });
            return false;
        });

        $("#btnDelete").click(function () {
            $("#divLoader").show();
            var hasUsers = $("#hfHasUsers").val();
            var roleID = $("#hfRoleID").val();
            shConfirm("Delete", "Are you sure you want to delete role?", function (result) {
                if(result)
                {
                    if (hasUsers == 0) {
                        $.get('../RoleManagement/DeleteRole?RoleID=' + roleID, function (data) {
                            if (data.result == "1") {
                                $("#hfHasUsers").val("0");
                                $.get('../RoleManagement/AddRole', function (data) {
                                    $('#divAddEdit').html(data);
                                    $("#RoleList").removeClass("in");
                                    $("#AddEditRole").removeClass("in");
                                    $("#AddEditRole").addClass("in");
                                    $("#AddEditRole").css("height", "auto");
                                    $("#RoleList").css("height", "0")
                                    $("#divLoader").hide();
                                });
                            } else {
                                $("#divLoader").hide();
                                $.alert({
                                    title: 'Alert!',
                                    content: data.msg,
                                    type: 'red'
                                });
                            }
                        });
                    }
                    else
                    {
                        $("#hfRoleIdToDelete").val(roleID);
                        FillRoleToAssign(roleID);
                        $("#assignRole").data("kendoWindow").open().center();
                    }
                }
            });
            
            return false;
        });

        $("#btnUpdate").click(function () {
            $("#divLoader").show();
            var msg = validateForm();
            if (msg != "") {
                $("#divLoader").hide();
                $.alert({
                    title: 'Alert!',
                    content: "Following error need to fixed :<br/>" + msg,
                    type: 'red'
                });
                return;
            }

            var form = $("#edit_form").serialize();

            $.post("/RoleManagement/Update", form, function (dataResult) {
                $("#divLoader").hide();
                if (dataResult.result == 1) {
                    $.alert({
                        title: 'Alert!',
                        content: dataResult.msg,
                        type: 'blue'
                    });
                }
                else {
                    $.alert({
                        title: 'Alert!',
                        content: dataResult.msg,
                        type: 'red'
                    });
                }
            });
            return false;
        });

        $("#btnSave").click(function () {
            $("#divLoader").show();
            var msg = validateForm();
            if (msg != "") {
                $("#divLoader").hide();
                $.alert({
                    title: 'Alert!',
                    content: "Following error need to fixed :<br/>" + msg,
                    type: 'red'
                });
                
                return;
            }

            var form = $("#add_form").serialize();
            $.post("/RoleManagement/Create", form, function (dataResult) {
                $("#divLoader").hide();
                if (dataResult.result == 1) {
                    var roleID = dataResult.RoleID;
                    $.get('../RoleManagement/EditRole?RoleID=' + roleID, function (data) {
                        $('#divAddEdit').html(data);
                        $.alert({
                            title: 'Alert!',
                            content: dataResult.msg,
                            type: 'blue'
                        });
                    });
                    
                }
                else {
                    $.alert({
                        title: 'Alert!',
                        content: dataResult.msg,
                        type: 'red'
                    });
                }
            });
            return false;
        });

        function validateForm() {
            var msg = "";
            
            if ($("#RoleCode").val()+"" =="") {
                msg = msg + "Role Code is required.<br/>"
            }
            if ($("#RoleDescription").val() + "" == "") {
                msg = msg + "Role Description is required.<br/>"
            }
            return msg;
        }
        return false;
    });
