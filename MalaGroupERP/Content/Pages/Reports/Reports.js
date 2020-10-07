
//$(document).ready(function () {
   
   
//});


var saveToDiskExcelFile = function (fileName, hName) {
    console.log(baseURL());
    var saveUrl = baseURL() + "/TempFiles/" + fileName;
    //saveUrl = encodeURIComponent(saveUrl);
    var hyperlink = document.createElement('a');
    hyperlink.href = saveUrl;
    hyperlink.target = '_blank';
    hyperlink.download = hName+".csv";

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

