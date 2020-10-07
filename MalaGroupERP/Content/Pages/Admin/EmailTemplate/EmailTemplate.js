$(document).ready(function () {
    getEmailTemplatesList();
    var snippet = [];
    $.ajax({
        url: '/EmailTemplate/GetSnippetList',
        type: "post",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $.each(response, function (index, elementValue) {
                snippet.push(elementValue.Value);
               
            });

        }
    });

    CKEDITOR.replace('TemplateHTML', {
        fullPage: true,
        allowedContent: true,
        autoGrow_onStartup: true,
        enterMode: CKEDITOR.ENTER_BR,
        height: 500,
        //toolbarGroups: [
        //    { name: 'mode' },
        //    { name: 'basicstyles' }
        //],
       
        on: {
            pluginsLoaded: function () {
                var editor = this,
                    config = editor.config;

                editor.ui.addRichCombo('my-combo', {
                    label: 'Snippet',
                    title: 'Snippet List',
                    toolbar: 'basicstyles,0',

                    panel: {
                        css: [CKEDITOR.skin.getPath('editor')].concat(config.contentsCss),
                        multiSelect: false,
                        attributes: { 'aria-label': 'Snippet List' }
                    },

                    init: function () {
                        this.startGroup('Add Snippet');

                        for (var j = 0; j < snippet.length; j++) {
                            this.add('{'+snippet[j]+'}', snippet[j]);
                        }
                      

                    },

                    onClick: function (value) {
                        editor.focus();
                        editor.fire('saveSnapshot');
                        editor.insertHtml(value);
                        editor.fire('saveSnapshot');
                    }
                });
            }
      
        }
    });

});
var getEmailTemplatesList = function () {
    $("#divLoader").show();
    $("#tblEmailTemplate>tbody").empty();
    $.ajax({
        url: '/EmailTemplate/GetEmailTemplates',
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
                    html += '<td class="text-left"><a href="#"  onclick="getEmailData(' + elementValue.TemplateID + ');">' + elementValue.TemplateName + '</a></td>';
                    html += '<td class="text-left">' + (elementValue.TemplateFor==1? "Lead" : "Account") + '</td>';
                    html += '</tr>';
                    $("#tblEmailTemplate>tbody").append(html);
                });
              
            }
        }
    });

}
var getEmailData = function (templateID) {
    $("#divLoader").show();
    $("#hndTemplateID").val(templateID);
    var param = { TemplateID: templateID, AccountID: 1 };
    $.ajax({
        url: "/EmailTemplate/GetEmailTemplateData",
        type: "post",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            $("#divLoader").hide();
            $("#txtEmailSubject").val(response.subject);
            $("#txtTempName").val(response.name);
            $("#ddlTempFor").val(response.templateFor).trigger("change");
            CKEDITOR.instances['TemplateHTML'].setData(response.html);
        }
    });
}
var saveEmailTemplate = function () {
    $("#divLoader").show();
    var errMsg = "";
    var tempid = $("#hndTemplateID").val();
    var emailTemp = CKEDITOR.instances['TemplateHTML'].getData();
    var emailSub = $("#txtEmailSubject").val();
    var tempname = $("#txtTempName").val();
   var tempfor = $("#ddlTempFor").val();

    if (!emailTemp) {
        errMsg += "Email Template required.";
    }
    if (!emailSub) {
        errMsg += "Email Template Subject required.";
    }
    if (!tempname) {
        errMsg += "Template Name required.";
    }
    if (errMsg != "") {
        $("#divLoader").hide();
        $.alert({
            title: 'Alert!',
            content: errMsg,
            type: 'red'
        });
        return;
    }
    var model = {
        TemplateId:tempid,
        EmailData: emailTemp,
        EmailSubject: emailSub,
        TemplateName: tempname,
        TemplateFor: tempfor,
    };
    $.ajax({
        url: "/EmailTemplate/SaveEmailTemplate",
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
                    type: 'red'
                });
            } else {
                $.alert({
                    title: 'Alert!',
                    content: 'Email Template Updated Successfully',
                    type: 'blue'
                });
                getEmailTemplatesList();
                CKEDITOR.instances['TemplateHTML'].setData("");
                $("#hndTemplateID").val("0");
                $("#txtEmailSubject").val("");
                $("#txtTempName").val("");
            }
        }
    });
}

function newEmailTemplate()
{
    CKEDITOR.instances['TemplateHTML'].setData("");
    $("#hndTemplateID").val("0");
    $("#txtEmailSubject").val("");
    $("#txtTempName").val("");
}