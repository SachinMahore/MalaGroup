$(document).ready(function () {
    getMainSearchList();

});

function getMainSearchList(searchtext) {
    var param = { term: searchtext };
    $.ajax({
        url: '/Search/GetMainSearchList',
        type: "post",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if ($.trim(response.error) != "") {
                //this.cancelChanges();
            } else {
                console.log(response);
                $("#tblLeadSearch>tbody").empty();
                $.each(response, function (index, elementValue) {
                    var html = '';
                    html += '<tr data-value="' + elementValue.DValue + '">';
                    html += '<td><a href="#">' + elementValue.DText + '</td>';

                    html += '</tr>';
                    $("#tblLeadSearch>tbody").append(html);
                });
            }
        }
    });
}