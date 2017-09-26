var hostAddress = document.location.host;

// Bootstrap Master Table
var BootstrapTable = function () {
    var dataTable = "";

    function selectData(para) {
        $(para.tableId).bootstrapTable('showLoading');
        $.ajax({
            type: "GET",
            url: para.serviceName,
            data: para.para,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (response) {
                dataTable = response;
                $(para.tableId).bootstrapTable('load', dataTable);
                dataTable = '';
                $(para.tableId).bootstrapTable('hideLoading');
                return true;
            },
            error: function () {
                return false;
            }
        });
    }

    return {
        GetDataTable: function (para) {
            $(para.tableId).bootstrapTable({
                data: dataTable,
                pageSize: para.pageSize,
                pageList: [10, 25, 50, 100],
                height: para.height
            });
            selectData(para);
        },
        RefreshData: function (para) {
            selectData(para);
        }
    }
    console.log("-----common--------" + dataTable);
}();
// Format Column Index 
//-----cell style

function confirmDelete() {
    return confirm('Do you really want to delete that row?');
}
/*======================Show notification========================*/
function showNotification(feeownTitle, feeownMess, feeownStyle) {
    //STYLE:   smokey, gray, osx, simple    
    //OPTION: autoHide_bool, autoHideDelay_int_ms, classes_array, prepend_bool
    var title = feeownTitle;
    var message = feeownMess;
    var opts = {};
    opts.classes = [feeownStyle]; //style
    opts.prepend = false;
    opts.autoHide = true;
    var container = '#freeow-tr';
    opts.classes.push("slide");
    opts.hideStyle = {
        opacity: 0,
        left: "400px"
    };
    opts.showStyle = {
        opacity: 1.5,
        left: 0
    };
    $(container).freeow(title, message, opts);
}
//Display Notification
function DisplayNotification() {
    var msg = $('#notiMsg').html();
    var title = $('#notiTitle').html();
    var style = $('#notiStyle').html();
    if (msg !== '') {
        showNotification(title, msg, style);
    }
}
// encode(decode) html text into html entity
var decodeHtmlEntity = function (str) {
    return str.replace(/&#(\d+);/g, function (match, dec) {
        return String.fromCharCode(dec);
    });
};

var encodeHtmlEntity = function (str) {
    var buf = [];
    for (var i = str.length - 1; i >= 0; i--) {
        buf.unshift(['&#', str[i].charCodeAt(), ';'].join(''));
    }
    return buf.join('');
};


function formatDateyyyyMMdd(date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;

    return [year, month, day].join('');
}

function loading() {
    var src = 'http://' + hostAddress + '/Content/img/loading_en.gif';
    var over = "<div id='overlay'><img id='loading' src='" + src + "'></div>";
    $(over).appendTo("body");
    // console.log("có vào loading");
}
function removeloading() {
    $('#overlay').remove();
    // console.log("remove loading");
}

//check empty, conflict
function fnCheckEmpty($element, mess) {
    if ($element.val() === "") {
        showNotification("Hyosung Portal", mess, "gray error");
        $element.focus();
        return false;
    }
    return true;
}

function showErrorMessage(mess) {
    if (mess !== "")
        showNotification("Hyosung Portal", mess, "gray error");
}

//validate file extention
function validateFileExtention(sender, type) {

    if (typeof type === "undefined" || type === null) { 
        type = "doc";
    }
    var ext = $(sender).val().split(".").pop().toLowerCase();
    var doc = ["pdf", "doc", "docx", "xls", "xlsx", "ppt", "pptx"];
    var img = ["jpg", "jpeg", "png", "bmp", "gif"];
    var valid = [];
    switch (type) {
        case "doc":
            valid = doc;
            break;
        case "img":
            valid = img;
            break;
        default:
            valid = doc;
    }
    if (valid.lastIndexOf(ext) === -1) {
        showNotification("Hyosung Portal", "Just allow upload file with extention in (" + valid.toString() + ")", "gray error");
        $(sender).val("");
    }
}


//begin -------format collumn
var rowIndex = function (row, column, value, defaultHtml, columnSettings, rowData) {
    return '<div style="text-align: center; margin-top: 5px;">' + (row + 1).toString() + '</div>';
}

var deleteColumn = function (row, column, value, defaultHtml, columnSettings, rowData) {
    return "<a href='#' onclick='deleteFile(&#39;" + JSON.stringify(rowData) + "&#39;);' style='padding:18px'><i class='fa fa-trash' style='padding-top:4px; color:red'></i></a>";
}

var colRight = function (value) {
    return '<div style="text-align: right; margin-top: 5px;">' + value + '</div>';
}

var colLeft = function (value) {
    return '<div style="text-align: left; margin-top: 5px;">' + value + '</div>';
}


var colHeaderLeft = function (value) {
    return '<div style="text-align: left; margin-top: 5px; margin-left: 5px; font-weight:bold">' + value + '</div>';
}

var colHeaderCenter = function (value) {
    return '<div style="text-align: center; margin-top: 5px; font-weight:bold">' + value + '</div>';
}

var colHeaderRight = function (value) {
    return '<div style="text-align: right; margin-top: 5px; margin-right: 5px;font-weight:bold">' + value + '</div>';
}

var downloadFile = function (row, column, value, defaultHtml, columnSettings, rowData) {
    return value ? "<a href='" + rowData.FILEPATH + "' style='padding:10px' ><i class='fa fa-paperclip' aria-hidden='true' style='padding-top:4px; color:#606060'></i></a>"
        : "";
}

var attachFile = function (row, column, value, defaultHtml, columnSettings, rowData) {
    return value ? "<a href='#' onclick='showModalAttachment(&#39;" + rowData.Id + "&#39;);'style='padding:10px' >" + "<i class='fa fa-paperclip' aria-hidden='true' style='padding-top:4px; color:#606060'></i></a>"
        : "";
}

var rowStatus = function (row, column, value, defaultHtml, columnSettings, rowData) {
    return '<div style="text-align: center; margin-top: 5px;">' + (value ? "Enable" : "Disable") + '</div>';// value ? "Enable" : "Disable";
}
//end -------format collumn

//02-12-2014
function Str2Dt(strDateTime) {
    return (strDateTime == "" ? new Date() : new Date(strDateTime.split('/')[2], strDateTime.split('/')[1] - 1, strDateTime.split('/')[0]));
}
//dd/MM/yyyy
function Dt2Str(dt) {
    return convert2Degit(dt.getDate().toString()) + "/" + convert2Degit((dt.getMonth() + 1).toString()) + "/" + dt.getFullYear();
}
function convert2Degit(ori) {
    return ori.length == 1 ? ("0") + ori : ori;
}

function createCookie(name, value, days) {
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        var expires = "; expires=" + date.toGMTString();
    }
    else var expires = "";
    document.cookie = name + "=" + value + expires + "; path=/";
}

function readCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0)
            return c.substring(nameEQ.length, c.length);
    }
    if (name == "H_UserID")
        window.parent.location.href = '../Login.aspx';
    return "";
}

function copyTextToClipboard(text) {
    var textArea = document.createElement("textarea");

    //
    // *** This styling is an extra step which is likely not required. ***
    //
    // Why is it here? To ensure:
    // 1. the element is able to have focus and selection.
    // 2. if element was to flash render it has minimal visual impact.
    // 3. less flakyness with selection and copying which **might** occur if
    //    the textarea element is not visible.
    //
    // The likelihood is the element won't even render, not even a flash,
    // so some of these are just precautions. However in IE the element
    // is visible whilst the popup box asking the user for permission for
    // the web page to copy to the clipboard.
    //

    // Place in top-left corner of screen regardless of scroll position.
    textArea.style.position = 'fixed';
    textArea.style.top = 0;
    textArea.style.left = 0;

    // Ensure it has a small width and height. Setting to 1px / 1em
    // doesn't work as this gives a negative w/h on some browsers.
    textArea.style.width = '2em';
    textArea.style.height = '2em';

    // We don't need padding, reducing the size if it does flash render.
    textArea.style.padding = 0;

    // Clean up any borders.
    textArea.style.border = 'none';
    textArea.style.outline = 'none';
    textArea.style.boxShadow = 'none';

    // Avoid flash of white box if rendered for any reason.
    textArea.style.background = 'transparent';


    textArea.value = text;

    document.body.appendChild(textArea);

    textArea.select();

    try {
        var successful = document.execCommand('copy');
        var msg = successful ? 'successful' : 'unsuccessful';
        console.log('Copying text command was ' + msg);
    } catch (err) {
        console.log('Oops, unable to copy');
    }

    document.body.removeChild(textArea);
}

function removeTags(string){
  return string.replace(/<[^>]*>/g, ' ')
               .replace(/\s{2,}/g, ' ')
               .trim();
}