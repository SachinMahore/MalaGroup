$(document).ready(function () {
      $.ajax({
        url: '/RoleManagement/GetList',
        type: "post",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
            } else {
                $("#rolelist>tbody").empty();
                $.each(response, function (index, elementValue) {
                    var html = '';
                    html += '<tr data-value="' + elementValue.RoleId + '">';
                    html += '<td>' + elementValue.RoleCode + '</td>';
                    html += '<td>' + elementValue.RoleDescription + '</td>';
                    html += '<td>' + elementValue.RoleStatusText + '</td>';
                    html += '</tr>';
                    $("#rolelist>tbody").append(html);
                });
            }
        }
    });
     
      $('#rolelist tbody').on('click', 'tr', function () {
          $('#rolelist tr').removeClass("pds-selected-row");
          $(this).addClass("pds-selected-row")
      });
      $('#rolelist tbody').on('dblclick', 'tr', function () {
          goToRole();
      });
});

var goToRole = function () {
    $("#divLoader").show();
    var row = $('#rolelist tbody tr.pds-selected-row').closest('tr');
    var RoleID = $(row).attr("data-value");
    if (RoleID != null) {
        $.get('../RoleManagement/EditRole?RoleID=' + RoleID, function (data) {
            $('#divAddEdit').html(data);
            $("#RoleList").removeClass("in");
            $("#AddEditRole").removeClass("in");
            $("#AddEditRole").addClass("in");
            $("#RoleList").css("height", "0");
            $("#AddEditRole").css("height", "auto");
            $("#divLoader").hide();
        });

    } else {
        $("#divLoader").hide();
        $.alert({
            title: 'Alert!',
            content: "Please select a role!",
            type: 'red'
        });
    }
}
