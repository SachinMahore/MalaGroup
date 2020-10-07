$(document).ready(function () {
    $.get('../RoleManagement/GetRoleList', function (data) {
        $('#divViewList').html(data);
    });

    $.get('../RoleManagement/AddRole', function (data) {
        $('#divAddEdit').html(data);
    });
});