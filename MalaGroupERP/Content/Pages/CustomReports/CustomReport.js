$(document).ready(function () {
    $("#headText").text("Report Info"); 
    $("#ddlFilterField").on('change', function (evt, params) {
        var selected = $(this).val();
        var lblText = $(this).find("option:selected").text();
        if (selected != null) {
            getFilterFieldChange(selected, lblText);
        }
    });
    $("#ddlFieldCondition").on('change', function (evt, params) {
        var selected = $(this).val();
        if (selected != null) {
            getFieldConditionChange(selected);
        }
    });

    $("#popFiltervalue").PopupWindow({
        title: "Details Of filter",
        modal: true,
        autoOpen: false,
        top: 220,
        left: 180,
        height: 400,
        width: 650,

    });

    $("#ddlFilterValue").on('change', function (evt, params) {
        var selected = $(this).val();
        if (selected != null) {
            for (var j = 0; j < selected.length; j++) {
                if (selected[j] == 0 ) {
                    $("#ddlFilterValue").select2('data', null);
                    $("#ddlFilterValue").select2('data', { id: '0', text: 'All' });
                    break;
                }
            }
        }
    });
    $('.chkFull').change(function () {
        $('#tblStep2>tbody tr td input[type="checkbox"]').prop('checked', $(this).prop('checked'));
    });
});
var step1Model = [];
var step2Model = [];
var step3Model = [];
function nextTab(step) {
   
    var msg = "";
    if (step == 1) {
        step1Model = [];
        var reportName = $("#txtReportName").val();
        var reportFor = $("#ddlReportFor").val();
        var reportForText = $("#ddlReportFor option:selected").text();
        var isPublic = $("#chkIsPublic").is(":checked") ? "1" : "0";
        var isPublicText = $("#chkIsPublic").is(":checked") ? "Yes" : "No";
        var isSaved = $("#chkSaveReport").is(":checked") ? "1" : "0";
        var isSavedText = $("#chkSaveReport").is(":checked") ? "Yes" : "0";
        if (reportName == "") {
            msg += " Please enter Report Name.<br />";
            $('#form-wizard-1').show();
        }
        else if (reportFor == "0") {
            msg += " Please select  Report For.<br />";
            $('#form-wizard-1').show();
        } else {
            $('#form-wizard-1').hide();
        }

        if (msg != "") {
            $.alert({
                title: 'Alert!',
                content: msg,
                type: 'red'
            });
            return;
        }

        //var model = { CustomReportName: reportName, CustomReportFor: reportFor, IsPublic: isPublic, IsSaved: isSaved };

        //step1Model.push(model);
        $("#hndReportFor").val(reportFor);
        getCustomReportList(reportFor);
        $('#form-wizard-1').hide();
        $("#headText").text("Select Report Fields");
        $('#form-wizard-2').show();
        //$.ajax({
        //    url: '/CustomReport/Step1',
        //    type: "post",
        //    data: JSON.stringify(model),
        //    contentType: "application/json; charset=utf-8",
        //    dataType: "json",
        //    async: false,
        //    success: function (response) {
        //        if (response.ID > 0) {
        //            $("#divStep1").removeClass("hidden");
        //            $("#hndCusReporID").val(response.ID);
        //            $("#hndTableID").val(reportFor)
        //            $("#lblReportName").text("Report Name: " + model.CustomReportName);
        //            $("#lblReportFor").text("Report For: " + reportForText);
        //            $("#lblPublic").text("Ispublic: " + isPublicText);
        //            $("#lblSave").text("IsSaved:" + isSaved);
        //            getCustomReportList(reportFor);
        //            $('#form-wizard-1').hide();
        //            $('#form-wizard-2').show();
        //        } else {
        //            $('#form-wizard-1').show();
        //            $('#form-wizard-2').hide();
        //        }
        //    }
        //})

    }
    if (step == 2) {
        step2Model = [];
        var msg = "";
        var ids = "";
        //var cusReportID = $("#hndCusReporID").val();
        $('.chkCUID').each(function (index, obj) {
            if (obj.checked == true) {
                var id = $(obj).attr("data-value");
                    ids += (ids == "" ? id : "," + id);
            }
        });

        if (ids == "") {
            $.alert({
                title: 'Alert!',
                content: "Please select atleast one Field.",
                type: 'red'
            });
        }
        else {

            $("#hndCIDs").val(ids);
            //var param = { CIDs: ids };
            //step2Model.push(param);
            var reportFor = $("#hndReportFor").val();
            getDmFilterField(reportFor);
            getVarcharDrop();
            $("#divStep2").removeClass("hidden");
            $('#form-wizard-1').hide();
            $('#form-wizard-2').hide();
            $("#headText").text("Select Report Filters");
            $('#form-wizard-3').show();


            //$.ajax({
            //    url: "/CustomReport/Step2",
            //    type: "post",
            //    data: JSON.stringify(param),
            //    contentType: "application/json; charset=utf-8",
            //    dataType: "json",
            //    async: false,
            //    success: function (response) {
            //        if ($.trim(response.error) != "") {
            //            $.alert({
            //                title: 'Alert!',
            //                content: response.error,
            //                type: 'red'
            //            });
            //        } else {
            //            if (response.CID > 0) {
            //                $("#hndCusFieldReporID").val(response.CID);
            //                getReportFieldList(response.CID, 1);
            //                getDmFilterField(response.CID);
            //                getVarcharDrop();

            //                $("#divStep2").removeClass("hidden");
            //                $('#form-wizard-1').hide();
            //                $('#form-wizard-2').hide();
            //                $('#form-wizard-3').show();
            //            } else {
            //                $('#form-wizard-1').show();
            //                $('#form-wizard-2').hide();

            //                $('#form-wizard-1').hide();
            //                $('#form-wizard-2').show();
            //                $('#form-wizard-3').hide();
            //            }
            //        }
            //    }
            //});
        }
    }
    if (step == 3) {
        step3Model = [];
       
        for (var i = 1; i < rowCount; i++) {
            var rowEdit = $('#tblFiltervalue tr[data-value="' + i + '"]');
            if (rowEdit.length > 0) {
                var dataField = $(rowEdit).find("td:eq(0)").attr("data-Fieldval");
                var dataFilCon = $(rowEdit).find("td:eq(1)").attr("data-FilCond");

                var dataFilter = $(rowEdit).find("td:eq(2)").attr("data-FilterVal");
                var Dval = dataField.split(',');
                var columnName = Dval[0];
                var dataType = Dval[1];
               
                    var id = $("#hndCusReporID").val();
                    if (dataType == "1" && dataFilCon == "1") {
                        var dataTxt = columnName + " LIKE '" + dataFilter.toUpperCase() + "%'";
                    } else if (dataType == "1" && dataFilCon == "2") {
                        var dataTxt = columnName + " LIKE '%" + dataFilter.toUpperCase() + "'";
                    } else if (dataType == "1" && dataFilCon == "3") {
                        var dataTxt = columnName + " LIKE '%" + dataFilter.toUpperCase() + "%'";
                    } else if (dataType == "1" && dataFilCon == "4") {
                        var dataTxt = columnName + "='" + dataFilter.toUpperCase() + "'";
                    }
                    else if (dataType == "2") {
                        if (dataFilter == "0") {
                            var dataTxt = columnName + " " + dataFilCon;
                        } else {
                            var dataTxt = columnName + " " + dataFilCon + "  (" + dataFilter.toUpperCase() + ")";
                        }
                    }
                    else if (dataType == "4") {
                        if (columnName == "dbo.getDecalCountCustomReport(A.ID)") {
                            if (dataFilter>0)
                            {
                              var  dataTxt = "dbo.getDecalCountCustomReport(A.ID) >0";
                            }else
                            {
                                var dataTxt = "dbo.getDecalCountCustomReport(A.ID) =0";
                            }
                            
                        } else if (columnName == "dbo.getIdentityTheftCountCustomReport(A.ID)") {
                            if (dataFilter > 0) {
                                var dataTxt = "dbo.getIdentityTheftCountCustomReport(A.ID) >0";
                            } else {
                                var dataTxt = "dbo.getIdentityTheftCountCustomReport](A.ID) =0";
                            }
                           
                        } else if (columnName == "dbo.getIdentityTheftCountCustomReportLead(L.LeadID)") {
                            if (dataFilter > 0) {
                                var dataTxt = "dbo.getIdentityTheftCountCustomReportLead(L.LeadID) >0";
                            } else {
                                var dataTxt = "dbo.getIdentityTheftCountCustomReportLead(L.LeadID) =0";
                            }

                        } else if (columnName == "dbo.getDecalCountCustomReportLead(L.LeadID)") {
                            if (dataFilter > 0) {
                                var dataTxt = "dbo.getDecalCountCustomReportLead(L.LeadID) >0";
                            } else {
                                var dataTxt = "dbo.getDecalCountCustomReportLead(L.LeadID) =0";
                            }

                        } else
                        {
                            var dataTxt = columnName + " = " + dataFilter.toUpperCase() + "";
                        }
                        console.log(dataTxt);
                    } else if (dataType == "5" && dataFilCon == "1") {
                        var dataTxt = columnName + " = '" + dataFilter.toUpperCase() + "'";
                    } else if (dataType == "5" && dataFilCon == "2") {
                        var dataTxt = columnName + " > '" + dataFilter.toUpperCase() + "'";
                    } else if (dataType == "5" && dataFilCon == "3") {
                        var dataTxt = columnName + " < '" + dataFilter.toUpperCase() + "'";
                    } else if (dataType == "5" && dataFilCon == "4") {
                        var dataTxt = columnName + " >= '" + dataFilter.toUpperCase() + "'";
                    } else if (dataType == "5" && dataFilCon == "5") {
                        var dataTxt = columnName + " <= '" + dataFilter.toUpperCase() + "'";
                    } else if (dataType == "5" && dataFilCon == "6") {
                        var dateVal = dataFilter.split('-');
                        var from = dateVal[0];
                        var to = dateVal[1];
                        var dataTxt = columnName + " >= '" + from + "'" + " AND  " + columnName + " <= '" + to + "'";
                    }
                    else {
                        var dataTxt = "";
                    }
                    var data = {
                        DataText: dataTxt,
                        ReportID: id
                    };
                    step3Model.push(data);
            }
        }
        $('#form-wizard-1').hide();
        $('#form-wizard-2').hide();
        $('#form-wizard-3').hide();
        $("#headText").text("Save Report/View Report");
        $('#form-wizard-4').show();

        //var model = { CustomFilterList: customFilterData }
        //$.ajax({
        //    url: '/CustomReport/SaveCusReFilterTxt',
        //    type: "post",
        //    data: JSON.stringify(model),
        //    contentType: "application/json; charset=utf-8",
        //    dataType: "json",
        //    async: false,
        //    success: function (response) {
        //        $("#divLoader").hide();
        //        if (response.Msg != "") {
        //            //$('#form-wizard-1').hide();
        //            //$('#form-wizard-2').hide();
        //            //$('#form-wizard-3').hide();
        //            //$('#form-wizard-4').show();
        //            exportReport();
        //        }
        //    }
        //})
    }
}
var rowIndex = 0;
var getCustomReportList=function(tableID)
{
    var param = { tableID: parseInt(tableID) }
    $.ajax({
        url: '/CustomReport/GetCustomReportList',
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
                if(response!="")
                {
                    $("#tblStep2>tbody").empty();
                    $.each(response, function (index, elementValue) {
                        var html = '';
                        html += '<tr   data-value="' + elementValue.CusReportFeildListID + '">';
                        html += '<td class=""><input type="checkbox" name="radios" id="chkID_' + elementValue.CusReportFeildListID + '" class="chkCUID" data-value="' + elementValue.CusReportFeildListID + '" /></td>';
                        //html += '<td> <input  type="checkbox" class="chkCUID" id="chkID_' + elementValue.CusReportFeildListID + '" name="radios" data-value="' + elementValue.CusReportFeildListID + '" </></td>';
                        html += '<td> ' + elementValue.DisplayName + '</td>';
                        html += '</tr>';
                        $("#tblStep2>tbody").append(html);
                        rowIndex += 1;
                    });
                }
            }
        }
    });
}
var getDmFilterField = function (cusID) {
    var param={CusID:cusID}
    $("#ddlFilterField").empty();
    $.ajax({
        url: "/CustomReport/getDmFilterField",
        type: "post",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            $("#ddlFilterField").append("<option value='0'>Select Filter Field...</option>");
            $.each(response.model, function (index, elementValue) {
                var opt = "<option value='" + elementValue.Value + "'>" + elementValue.Text + "</option>";
                $("#ddlFilterField").append(opt);
            });
            $("#ddlFilterField").val(0).trigger('change');
        }
    });
}
var varCharDataSource = [
     { text: "Start With", value: "1" },
     { text: "End With", value: "2" },
     { text: "Contains", value: "3" },
     { text: "Equal to", value: "4" }
]
var varDateSource = [
     { text: "Equal To", value: "1" },
     { text: "Greater Then", value: "2" },
     { text: "Less Then", value: "3" },
     { text: "Greater Then Equal To", value: "4" },
     { text: "Less Then Equal To", value: "5" },
     { text: "Between", value: "6" },
]
var getVarcharDrop = function () {
    
    $("#ddlFieldCondition").empty();
    $("#ddlFieldCondition").append("<option value='0'>Select Filter Value...</option>");
    $.each(varCharDataSource, function (n, v) {
        $("#ddlFieldCondition").append("<option value='" + v.value + "'>" + v.text + "</option>");
        $('#ddlFieldCondition').val(0).trigger('change');
    });
}
var getDateFilter = function () {
    $("#ddlFieldCondition").empty();
    $("#ddlFieldCondition").append("<option value='0'>Select Filter Value...</option>");
    $.each(varDateSource, function (n, v) {
        $("#ddlFieldCondition").append("<option value='" + v.value + "'>" + v.text + "</option>");
        $('#ddlFieldCondition').val(0).trigger('change');
    });
    $("#ddlFieldCondition").val(0).trigger('change');
}
var compTypeSource = [
     { text: "ALL", value: "0" },
     { text: "COMP - MNGR", value: "1" },
     { text: "COMP - HARDSHIP", value: "2" },
     { text: "Comp - USA", value: "3" },
     { text: "COMP - LAW/GOV/1ST REP", value: "4" }
]
var accountStatusSource = [
     { text: "All", value: "0" },
     { text: "Active", value: "1" },
     { text: "Suspended", value: "2" },
     { text: "Cancelled - No Refund", value: "3" },
     { text: "Cancelled - Full Refund", value: "4" },
     { text: "Cancelled - Partial Refund", value: "5" },
     { text: "Caspio Account", value: "6" },
     { text: "Check - Pending", value: "7" },
     { text: "Comp - Pending", value: "8" },
     { text: "Online", value: "9" }

]
var languageSource = [
     { text: "All", value: "0" },
     { text: "English", value: "1" },
     { text: "Spanish", value: "2" },
     { text: "French", value: "3" },
     { text: "Dutch", value: "4" }
]

var leadStatusDataSource = [
     { text: "All", value: "0" },
     { text: "Active", value: "1" },
     { text: "New", value: "2" },
     { text: "Not Active", value: "3" },
     { text: "In Progress", value: "4" },
     { text: "Check - Pending", value: "7" },
     { text: "Comp - Pending", value: "8" }
]
var getComType = function () {
    $("#ddlFilterValue").empty();
    $.each(compTypeSource, function (n, v) {
        $("#ddlFilterValue").append("<option value='" + v.value + "'>" + v.text + "</option>");
        $('#ddlFilterValue').val(0).trigger('change');
    });
    $("#ddlFilterValue").val(0).trigger('change');
}
var getAccountStatus = function () {
    $("#ddlFilterValue").empty();
    $.each(accountStatusSource, function (n, v) {
        $("#ddlFilterValue").append("<option value='" + v.value + "'>" + v.text + "</option>");
        $('#ddlFilterValue').val(0).trigger('change');
    });
    $("#ddlFilterValue").val(0).trigger('change');
}
var getLangauge = function () {
    $("#ddlFilterValue").empty();
    $.each(languageSource, function (n, v) {
        $("#ddlFilterValue").append("<option value='" + v.value + "'>" + v.text + "</option>");
        $('#ddlFilterValue').val(0).trigger('change');
    });
    $("#ddlFilterValue").val(0).trigger('change');
}
var getCreatedBy = function () {
    $("#ddlFilterValue").empty();
    $.ajax({
        url: "/CustomReport/GetCreatedBy",
        type: "post",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            $("#ddlFilterValue").append("<option value='0'>ALL</option>");
            $.each(response.model, function (index, elementValue) {
                var opt = "<option value='" + elementValue.Value + "'>" + elementValue.Text + "</option>";
                $("#ddlFilterValue").append(opt);
            });
            $("#ddlFilterValue").val(0).trigger('change');
        }
    });
}
var getlastModified=function()
{
    $("#ddlFilterValue").empty();
    $.ajax({
        url: "/CustomReport/GetCreatedBy",
        type: "post",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            $("#ddlFilterValue").append("<option value='0'>ALL</option>");
            $.each(response.model, function (index, elementValue) {
                var opt = "<option value='" + elementValue.Value + "'>" + elementValue.Text + "</option>";
                $("#ddlFilterValue").append(opt);
            });
            $("#ddlFilterValue").val(0).trigger('change');
        }
    });
}
var getAccountOwner=function()
{
    $("#ddlFilterValue").empty();
    $.ajax({
        url: "/CustomReport/GetCreatedBy",
        type: "post",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            $("#ddlFilterValue").append("<option value='0'>ALL</option>");
            $.each(response.model, function (index, elementValue) {
                var opt = "<option value='" + elementValue.Value + "'>" + elementValue.Text + "</option>";
                $("#ddlFilterValue").append(opt);
            });
            $("#ddlFilterValue").val(0).trigger('change');
        }
    });
}
var getLeadStatus = function () {
    $("#ddlFilterValue").empty();
    $.each(leadStatusDataSource, function (n, v) {
        $("#ddlFilterValue").append("<option value='" + v.value + "'>" + v.text + "</option>");
        $('#ddlFilterValue').val(0).trigger('change');
    });
    $("#ddlFilterValue").val(0).trigger('change');
}

var getVehicleMake=function()
{
    $("#ddlFilterValue").empty();
    $.ajax({
        url: "/CustomReport/GetVehicleMakeList",
        type: "post",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            $("#ddlFilterValue").append("<option value='0'>ALL</option>");
            $.each(response.model, function (index, elementValue) {
                var opt = "<option value='" + elementValue.Value + "'>" + elementValue.Text + "</option>";
                $("#ddlFilterValue").append(opt);
            });
            $("#ddlFilterValue").val(0).trigger('change');
        }
    });
}
var getFilterFieldChange = function (val, lblText) {
    clearOnChangeFilterField();
    var lbText = "";
    lbText = lblText;
    var Dval = val.split(',');
    var columnName = Dval[0];
    var dataType = Dval[1];
    if (dataType == "1") {
        getVarcharDrop();
        $("#divText").removeClass("hidden");
        $("#divMulSelect").addClass("hidden");
        $("#divCon").removeClass("hidden");
        $("#divcheck").addClass("hidden");
        $("#divDate").addClass("hidden");
        $("#divDateFromTo").addClass("hidden");
    } else if (dataType == "2") {
        if (columnName == "A.Type") {
            getComType();
        } else if (columnName == "A.CreatedById" || columnName == "L.CreatedById") {
            getCreatedBy();
        } else if (columnName == "A.LastModifiedById" || columnName == "L.LastModifiedById") {
            getlastModified();
        } else if (columnName == "A.AccountStatus") {
            getAccountStatus();
        } else if (columnName == "A.AccountOwner" || columnName == "L.LeadOwner") {
            getAccountOwner();
        } else if (columnName == "LI.Language" || columnName == "L.Language") {
            getLangauge();
        } else if (columnName == "L.LeadStatus") {
            getLeadStatus();
        } else if (columnName == "VL.VehicleMake") {
            getVehicleMake();
        }
        $("#divCon").addClass("hidden");
        $("#divcheck").addClass("hidden");
        $("#divMulSelect").removeClass("hidden");
        $("#divText").addClass("hidden");
        $("#divDate").addClass("hidden");
        $("#divDateFromTo").addClass("hidden");
    } else if (dataType == "3") {

    } else if (dataType == "4") {
        $("#divcheck").removeClass("hidden");
        $("#lblChkFilterValue").text(lbText);
        $("#divCon").addClass("hidden");
        $("#divText").addClass("hidden");
        $("#divMulSelect").addClass("hidden");
        $("#divDateFromTo").addClass("hidden");
        $("#divDate").addClass("hidden");
        
    } else if (dataType == "5") {
        getDateFilter();
        $("#lblChkFilterValue").text(lbText);
        $("#divcheck").addClass("hidden");
        $("#divCon").removeClass("hidden");
        $("#divText").addClass("hidden");
        $("#divMulSelect").addClass("hidden");
    }
}
var getFieldConditionChange=function(data)
{
    clearOnChangeFilterCondition();
    var filedValue = $("#ddlFilterField").val();
    var Dval = filedValue.split(',');
    var columnName = Dval[0];
    var dataType = Dval[1];
   
    if (dataType == "5" && data!="6") {
        $("#divDate").removeClass("hidden");
        $("#divDateFromTo").addClass("hidden");
    } else if (dataType == "5" && data == "6")
    {
        $("#divDate").addClass("hidden");
        $("#divDateFromTo").removeClass("hidden");
    }
}
var rowCount = 1
var saveCusReFilterTxt = function ()
{
    var filterFieldText = $("#ddlFilterField option:selected").text();
    var filterConditionText = $("#ddlFieldCondition option:selected").text();
    var filterValueText = $("#txtFilterValue").val();

    var filterFieldVal = $("#ddlFilterField").val();
  
    var filterConditionVal = $("#ddlFieldCondition").val();
    var html = '';
    html += '<tr class="trfilterValMainRow pd-vehicletblText" data-value="' + rowCount + '">';
    html += '<td data-Fieldval="' + filterFieldVal + '">' + filterFieldText + '</td>';
    html += '<td data-FilCond="' + filterConditionVal + '">' + filterConditionText + '</td>';
    html += '<td data-FilterVal="' + filterValueText + '">' + filterValueText.toUpperCase() + '</td>';
    html += '<td   class="pd-editdeletetbn"><div><button class="pd-btndelete"  onclick="removeTableRow(' + rowCount + ');"><i class="icon-trash"  ></i></button></div></td>';
    html += '</tr>';
    $("#tblFiltervalue>tbody").append(html);
    rowCount += 1;
}
var saveCusReFilterDDL = function () {
    var filterFieldText = $("#ddlFilterField option:selected").text();
    var filterValueText = $("#ddlFilterValue option:selected").val();
    var selText = "";
    var value = "";
    $("#ddlFilterValue option:selected").each(function () {
        var $this = $(this);
        if ($this.length) {
            selText += $this.text() + ",";
            value += ""+$this.val() + ",";
        }
    });
 
    filterText = selText;
    filterValue = value.slice(0, -1);
    var filterConditionVal = "";


    var filterFieldVal = $("#ddlFilterField").val();
    var filterValueVal = $("#ddlFilterValue").val();

    if (filterValueVal == "0")
    {
        filterConditionVal = " IS NOT NULL  "
      
    } else {
        filterConditionVal = " IN "
    }
   
    var html = '';
    html += '<tr class="trfilterValMainRow pd-vehicletblText" data-value="' + rowCount + '">';
    html += '<td data-Fieldval="' + filterFieldVal + '">' + filterFieldText + '</td>';
    html += '<td data-FilCond="' + filterConditionVal + '">IN</td>';
    html += '<td data-FilterVal="' + filterValue + '">' + filterText.toUpperCase() + '</td>';
    html += '<td   class="pd-editdeletetbn"><div><button class="pd-btndelete"  onclick="removeTableRow(' + rowCount + ');"><i class="icon-trash"  ></i></button></div></td>';
    html += '</tr>';
    $("#tblFiltervalue>tbody").append(html);
    rowCount += 1;
}
var saveCusReFilterChk = function () {
    var filterFieldText = $("#ddlFilterField option:selected").text();

    var filterFieldVal = $("#ddlFilterField").val();
    var filterValueVal = $("#chkFilterValue").is(":checked") ? "1" : "0";
    var filterValueText = $("#chkFilterValue").is(":checked") ? "Yes" : "No";
  
    var html = '';
    html += '<tr class="trfilterValMainRow pd-vehicletblText" data-value="' + rowCount + '">';
    html += '<td data-Fieldval="' + filterFieldVal + '">' + filterFieldText + '</td>';
    html += '<td data-FilCond="">=</td>';
    html += '<td data-FilterVal="' + filterValueVal + '">' + filterValueText.toUpperCase() + '</td>';
    html += '<td   class="pd-editdeletetbn"><div><button class="pd-btndelete"  onclick="removeTableRow(' + rowCount + ');"><i class="icon-trash"  ></i></button></div></td>';
    html += '</tr>';
    $("#tblFiltervalue>tbody").append(html);
    rowCount += 1;
   
}
var saveCusReFilterDate1 = function () {
    var filterFieldText = $("#ddlFilterField option:selected").text();
    var filterConditionText = $("#ddlFieldCondition option:selected").text();
    var filterValueText = $("#dtDate").val();

    var filterFieldVal = $("#ddlFilterField").val();

    var filterConditionVal = $("#ddlFieldCondition").val();
    var html = '';
    html += '<tr class="trfilterValMainRow pd-vehicletblText" data-value="' + rowCount + '">';
    html += '<td data-Fieldval="' + filterFieldVal + '">' + filterFieldText + '</td>';
    html += '<td data-FilCond="' + filterConditionVal + '">' + filterConditionText + '</td>';
    html += '<td data-FilterVal="' + filterValueText + '">' + filterValueText.toUpperCase() + '</td>';
    html += '<td   class="pd-editdeletetbn"><div><button class="pd-btndelete"  onclick="removeTableRow(' + rowCount + ');"><i class="icon-trash"  ></i></button></div></td>';
    html += '</tr>';
    $("#tblFiltervalue>tbody").append(html);
    rowCount += 1;


}
var saveCusReFilterDate2 = function () {
    var filterFieldText = $("#ddlFilterField option:selected").text();
    var filterConditionText = $("#ddlFieldCondition option:selected").text();
   

    if ($("#dtFromDate").val() + "" == "" && $("#dtTotDate").val() + "" == "") {
        setSearchDate();
    }
    else if ($("#dtFromDate").val() + "" != "" && $("#dtTotDate").val() + "" == "") {
        $("#dtpToDate").val($("#dtTotDate").val());
    }
    else if ($("#dtFromDate").val() + "" == "" && $("#dtTotDate").val() + "" != "") {
        $("#dtFromDate").val($("#dtTotDate").val());
    }
    var filterValueText = $("#dtFromDate").val() + "-" + $("#dtTotDate").val();
    var filterValueVal = $("#dtFromDate").val() + "-" + $("#dtTotDate").val()+" 23:59:59";

    var filterFieldVal = $("#ddlFilterField").val();

    var filterConditionVal = $("#ddlFieldCondition").val();
    var html = '';
    html += '<tr class="trfilterValMainRow pd-vehicletblText" data-value="' + rowCount + '">';
    html += '<td data-Fieldval="' + filterFieldVal + '">' + filterFieldText + '</td>';
    html += '<td data-FilCond="' + filterConditionVal + '">' + filterConditionText + '</td>';
    html += '<td data-FilterVal="' + filterValueVal + '">' + filterValueText.toUpperCase() + '</td>';
    html += '<td   class="pd-editdeletetbn"><div><button class="pd-btndelete"  onclick="removeTableRow(' + rowCount + ');"><i class="icon-trash"  ></i></button></div></td>';
    html += '</tr>';
    $("#tblFiltervalue>tbody").append(html);
    rowCount += 1;


}
var setSearchDate = function () {
    var todate = new Date();
    todate.setDate(todate.getDate());
    var dd = todate.getDate();
    var mm = todate.getMonth() + 1;
    var yyyy = todate.getFullYear();
    if (dd < 10) { dd = '0' + dd } if (mm < 10) { mm = '0' + mm }
    var todate = mm + '/' + dd + '/' + yyyy;
    $("#dtFromDate").val(todate);
    $("#dtTotDate").val(todate);
}
var viewCon=function()
{
    $("#popFiltervalue").PopupWindow("open");
}
var removeTableRow = function (id) {
    var model = { ID: id };
    $.confirm({
        title: 'Alert!',
        content: 'Are you sure you want to delete the data?',
        type: 'red',
        buttons: {
            confirm: function () {
                            $('#tblFiltervalue tr[data-value="' + id + '"]').remove();
            },
            cancel: function () {
            }
        }
    });
}
var clearOnChangeFilterField=function()
{
    $("#ddlFieldCondition").val(0).trigger('change');;
    $("#txtFilterValue").val("");
    $("#ddlFilterValue").val(0).trigger('change');
    $("#txtFilterValue").val("");
    $("#chkFilterValue").prop("checked", false);
    $.uniform.update("#chkFilterValue");
 
}
var clearOnChangeFilterCondition  = function () {
    $("#txtFilterValue").val("");
    $("#ddlFilterValue").val(0).trigger('change');
    $("#chkFilterValue").prop("checked", false);
    $.uniform.update("#chkFilterValue");
}
var saveToDiskExcelFile = function (fileName, title) {
    var fName = fileName;
    var saveUrl = baseURL() + "/TempFiles/" + fName;
    //saveUrl = encodeURIComponent(saveUrl);
    var hyperlink = document.createElement('a');
    hyperlink.href = saveUrl;
    hyperlink.target = '_blank';
    hyperlink.download = title + ".xlsx";

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
function backTab(step) {
    var msg = "";
    
    if (step == 2) {
        $('#form-wizard-1').show();
        $('#form-wizard-2').hide();
        $('#form-wizard-3').hide();
    }
    if (step == 3) {
        $('#form-wizard-1').hide();
        $('#form-wizard-2').show();
        $('#form-wizard-3').hide();
        $('#tblFiltervalue>tbody').empty();
        rowcount = 0;
    }
    if (step == 4) {
        $('#form-wizard-1').hide();
        $('#form-wizard-2').hide();
        $('#form-wizard-3').show();
        $('#form-wizard-4').hide();
    }
}
var lastStep=function(val)
{
    $("#divLoader").show();
    var reportName = $("#txtReportName").val();
    var reportFor = $("#ddlReportFor").val();
    var isPublic = $("#chkIsPublic").is(":checked") ? "1" : "0";

    var cids = $("#hndCIDs").val();
    var isSaved = val;
    //var reportForText="'"+$("#ddlReportFor option:selected").text()+"'";
    var reportForText = $("#ddlReportFor option:selected").text();
    console.log(step3Model);
    var model = {

        CustomReportName: reportName,
        CustomReportFor: reportFor,
        IsPublic: isPublic,
        CIDs: cids,
        CustomFilterList: step3Model,
        IsSaved: isSaved,
        //IdentityTheft: identityTheft,
        //AddDecal: addDecal
    };

    $.ajax({
        url: '/CustomReport/LastStep',
        type: "post",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            $("#divLoader").hide();
            if (response != "") {
                $("#btnback").addClass("hidden");
                $("#btnFinished").removeClass("hidden");
                saveToDiskExcelFile(response, reportForText);
            }
        }
    })

}
var newReport=function()
{
    window.location = "/CustomReport/Index";
}
var gotoCustom=function()
{
    window.location = "/CustomReport/ReportList";
}
