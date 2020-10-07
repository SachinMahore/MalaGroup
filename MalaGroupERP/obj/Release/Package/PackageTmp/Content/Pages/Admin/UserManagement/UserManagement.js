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
var timeZoneDS = [
    { text: "Eastern Standard Time", value: "EST" },
    { text: "Central Standard Time", value: "CST" },
    { text: "Mountain Standard Time", value: "MST" },
      { text: "Pacific Standard Time", value: "PST" },
      { text: "Alaskan Standard Time", value: "AKST" },
      { text: "Hawaiian Standard Time", value: "HST" },
    { text: "Tonga Standard Time", value: "TOT" },
    { text: "New Zealand Standard Time", value: "NZST" },
{ text: "E. Australia Standard Time", value: "EAST" },
{ text: "AUS Central Standard Time", value: "ACST" },
{ text: "Tokyo Standard Time", value: "JST" },
{ text: "Singapore Standard Time", value: "SGT" },
{ text: "North Asia Standard Time", value: "NAST" },
{ text: "Myanmar Standard Time", value: "MST" },
{ text: "Central Asia Standard Time", value: "CAST" },
{ text: "Nepal Standard Time", value: "NST" },
{ text: "India Standard Time", value: "IST" },
{ text: "Pakistan Standard Time", value: "PKT" },
{ text: "Afghanistan Standard Time", value: "AfT" },
{ text: "Arabian Standard Time", value: "SAST" },
{ text: "Iran Standard Time", value: "IRST" },
{ text: "Russian Standard Time", value: "MSK" },
{ text: "Jordan Standard Time", value: "EET" },
{ text: "Central Europe Standard Time", value: "CET" },
{ text: "Azores Standard Time", value: "AZOST" },

{ text: "Mid-Atlantic Standard Time", value: "AST" },
{ text: "Greenland Standard Time", value: "3" },
{ text: "Newfoundland Standard Time", value: "NST" },
{ text: "Atlantic Standard Time", value: "AST" },
{ text: "Venezuela Standard Time", value: "VET" },
{ text: "Canada Central Standard Time", value: "2" },
{ text: "Samoa Standard Time", value: "SST" },
{ text: "Dateline Standard Time", value: "4" }
];
var startDate_DDL = function () {
    $("#ddlStartDay").empty();
    $.each(dateTimeDataSource, function (n, v) {
        $("#ddlStartDay").append("<option value='" + v.value + "'>" + v.text + "</option>");
        $('#ddlStartDay').trigger('chosen:updated');
    });
    
}
var endDate_DDL = function () {
    $("#ddlEndDay").empty();
    var endTime = "";
    $.each(dateTimeDataSource, function (n, v) {
        $("#ddlEndDay").append("<option value='" + v.value + "'>" + v.text + "</option>");
        $('#ddlEndDay').trigger('chosen:updated');
    });
}
var timezone_DDL = function () {
    $("#ddlTimeZone").empty();
    $.each(timeZoneDS, function (n, v) {
        $("#ddlTimeZone").append("<option value='" + v.value + "'>" + v.text + "</option>");
        $('#ddlTimeZone').trigger('chosen:updated');
    });

}

$(document).ready(function () {
    startDate_DDL();
    endDate_DDL();
    timezone_DDL();
    $('#ulPaginationUserDetail').pagination({
        items: 0,
        currentPage: 1,
        displayedPages: 10,
        cssStyle: '',
        useAnchors: true,
        prevText: '&laquo;',
        nextText: '&raquo;',
        onInit: function () {
            buildPaganationUser();
        },
        onPageClick: function (page, evt) {
            fillUserTable(page);
        }
    });
    $("#ddlRowPerPage").on('change', function (evt, params) {
        ddlRowPerPageChange();
    });
});
var ddlRowPerPageChange = function () {
    buildPaganationUser();
}
var newUser = function () {
    window.location = '/UserManagement/AddEdit';
}
var saveUpdateUser = function () {
    $("#divLoader").show();
    var msg = "";
    var userID = $("#hndUserID").val(); 
    var firstName = $("#txtFirstName").val();
    var lastName = $("#txtLastName").val();
    var email =  $("#txtEmail").val();
    var username = $("#txtUserName").val();
    var company = $("#txtCompany").val();
    var department = $("#txtDepartment").val();
    var division = $("#txtDivision").val();
    var userlicense = $("#txtUserLicense").val();
    var password =  $("#txtPassword").val();
    var  cellPhone =  $("#txtPhone").val();
    var  extension =  $("#txtExtention").val();
    var workPhone =  $("#txtFax").val();
    var address1 = $("#txtStreet").val();
    var city = $("#txtCity").val();
    var  stateID =  $("#txtState").val();
    var  zipCode =  $("#txtZip").val();
    var country = $("#txtCountry").val();
    var isActive = ($("#chkActive").is(":checked") ? 1 : 0);
    //var isSuperUser = $("#hndAccountID").val();
    var employeeID = $("#txtEmployerNumber").val();
    var addToGroup =  $("#hndAccountID").val();
    var userType =  $("#ddlProfile").val();   
    var  federationID = $("#txtFederationID").val();
    var  timezone =  $("#ddlTimeZone").val();
    var  language =  $("#ddlLanguage").val();
    var  locale =  $("#ddlLocale").val();
    var startday = $("#ddlStartDay").val();
    var endday = $("#ddlEndDay").val();
    var smtpusername = $("#txtSMTPUserName").val();
    var smtppassword = $("#txtSMTPPassword").val();

     if (firstName == "") {
        msg += " Please enter First Name.<br />";
     }
     if (username == "") {
         msg += " Please enter User Name.<br />";
     }
     if (email == "") {
        msg += " Please enter Email.<br />";
    }
     if (cellPhone == "") {
        msg += " Please enter Phone No.<br />";
    }
    if (password == "") {
        msg += " Please enter Password.<br />";
    }
    if (userType == 0) {
        msg += " Please select User Profile.<br />";
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
    var tblUserRoles = $('#tblUserRoles').find('tbody').find('tr');
    var roles = [];

    for (var i = 0; i < tblUserRoles.length; i++) {
        var id = $(tblUserRoles[i]).find('td:eq(0)').html()
        var roleID = $(tblUserRoles[i]).find('td:eq(1)').html();
        var roleName = $(tblUserRoles[i]).find('td:eq(2)').html()
        var isAssign = $(tblUserRoles[i]).find('input[class=chkUserRole]').is(":checked");

        var roleInfo = {
            ID: id,
            RoleID: roleID,
            RoleName: roleName,
            IsAssign: (isAssign==true?1:0)
        };
        roles.push(roleInfo);

    }
    var model = {
        UserID :userID,    
        FirstName : firstName,
        LastName : lastName,
        Email : email,
        Username: username,
        Company: company,
        Division: division,
        Department: department,
        Password : password,
        Address1 : address1,
        Country: country,
        City : city,
        StateID :stateID,
        ZipCode : zipCode,
        CellPhone : cellPhone,
        Extension :extension,
        WorkPhone : workPhone,
        IsActive : isActive,
        EmployeeID : employeeID,
        AddToGroup : 0,
        UserType: userType,
        FederationID: federationID,
        Timezone: timezone,
        Language: language,
        Locale: locale,
        StartDay: startday,
        EndDay: endday,
        UserRole: roles,
        SMTPUserName: smtpusername,
        SMTPPassword: smtppassword,
    }
    var param = { model: model }
  
    $.ajax({
        url: "/UserManagement/AddUser",
        type: "post",
        data:JSON.stringify(model),
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
                //if (response.UserID != 0) {
                    if ($("#hndUserID").val() == "0") {
                        $.alert({
                            title: 'Msg!',
                            content: 'Data Saved Successfully.',
                            type: 'blue',
                            buttons: {
                                Ok: function () {
                                    window.location = '/UserManagement/Edit/' + response.UserID;
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
                //}
                //else {
                //    $.alert({
                //        title: 'Alert!',
                //        content: "Error occured while saving data.<br/>Please try later.",
                //        type: 'red'
                //    });
                //}
            }
        }
    });

    return false;
}
//var getUserList = function () {
//    $.ajax({
//        url: '/UserManagement/GetUserList',
//        method: "post",
//        //data: JSON.stringify(param),
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (response) {
//            if ($.trim(response.error) != "") {
//                //this.cancelChanges();
//            } else {
//                $("#tblUser>tbody").empty();
//                $.each(response, function (index, elementValue) {
//                    var html = '';
//                    html += '<tr  class="gradeX even" data-value="' + elementValue.UserID + '">';
//                    html += '<td   class="pd-vehicletblText"><a href="#"  onclick="editAccount(' + elementValue.UserID + ');">' + elementValue.UserName + '</a></td>';
//                    html += '<td class="pd-vehicletblText"> ' + elementValue.FirstName + ' ' + elementValue.LastName + '</td>';
//                    html += '<td class="pd-vehicletblText"> ' + elementValue.Email + '</td>';
//                    html += '<td class="pd-vehicletblText"> ' + elementValue.CellPhone + '</td>';
//                    html += '</tr>';
//                    $("#tblUser>tbody").append(html);
//                });
//            }
//        }
//    });
//}
var editAccount = function (id) {
    window.location = '/UserManagement/Edit/' + id;
}


var buildPaganationUser = function () {
    var name = $("#txtName").val();
    var rowDisplay = $("#ddlRowPerPage").val();
    var model = {
        Name: name,
        RowDisplay: rowDisplay
    };
    $.ajax({
        url: '/UserManagement/GetUserFilterRangeList',
        type: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {
                var pnarray = response.PageNumber.split('|');
                $('#ulPaginationUserDetail').pagination('updateItems', pnarray[0]);
                $('#ulPaginationUserDetail').pagination('selectPage', 1);
                $("#lblTotalRecords").text("Total Records : " + pnarray[1]);
                fillUserTable(1);
            }
        }
    });
}

var fillUserTable = function (PN) {
    $("#divLoader").show();
    var name = $("#txtName").val();
    var rowDisplay = $("#ddlRowPerPage").val();

    var model = {
        Name: name,
        RowDisplay: rowDisplay,
        PageNumber: PN
    };
    $.ajax({
        url: '/UserManagement/GetUserList',
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
                $("#tblUser>tbody").empty();
                $.each(response, function (index, elementValue) {
                    var html = '';
                    html += '<tr  class="gradeX even" data-value="' + elementValue.UserID + '">';
                    html += '<td   class="pd-vehicletblText"><a href="#"  onclick="editAccount(' + elementValue.UserID + ');">' + elementValue.UserName + '</a></td>';
                    html += '<td class="pd-vehicletblText"> ' + elementValue.Name + ' </td>';
                    html += '<td class="pd-vehicletblText"> ' + elementValue.Email + '</td>';
                    html += '<td class="pd-vehicletblText"> ' + elementValue.CellPhone + '</td>';
                    html += '</tr>';
                    $("#tblUser>tbody").append(html);
                });
            }
        }
    });
}