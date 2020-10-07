$(document).ready(function () {
    var snippet = [];
    $.ajax({
        url: '/Question/GetSnippetList',
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
                            this.add( snippet[j] , snippet[j]);
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

var getAOScriptData = function (templateID) {
    $("#divLoader").show();
    var aostep = $("#ddlAOSteps").val();
    var param = { step: aostep };
    $.ajax({
        url: "/Question/GetAOScriptData",
        type: "post",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            $("#divLoader").hide();
      
            CKEDITOR.instances['TemplateHTML'].setData(response.html);
        }
    });
}
var saveAOScript= function () {
    $("#divLoader").show();
    var errMsg = "";
    var scriptMessage = CKEDITOR.instances['TemplateHTML'].getData();
    var aostep = $("#ddlAOSteps").val();
   
    if (!scriptMessage) {
        errMsg += "AO Script required.";
    }
    if (aostep=="0") {
        errMsg += "AO Script required.";
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
       
        AOSrcript: scriptMessage,
        Steps: aostep,
    };
    $.ajax({
        url: "/Question/SaveAOScript",
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
                    content: 'Script Updated Successfully',
                    type: 'blue'
                });
              
                CKEDITOR.instances['TemplateHTML'].setData("");
            }
        }
    });
}