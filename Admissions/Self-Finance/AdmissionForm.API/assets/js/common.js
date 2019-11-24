function isEmail(email) {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return regex.test(email);
}

function BlockNonNumbers(obj, e, allowDecimal, allowNegative) { 
    var key;
    var isCtrl = false;
    var keychar;
    var reg;
    if (window.event) {
        key = e.keyCode;
        isCtrl = window.event.ctrlKey
    }
    else if (e.which) {
        key = e.which;
        isCtrl = e.ctrlKey;
    }
    if (isNaN(key)) return true;
    keychar = String.fromCharCode(key);
    // check for backspace or delete, or if Ctrl was pressed
    if (key == 8 || isCtrl) {
        return true;
    }
    reg = /\d/;
    var isFirstN = allowNegative ? keychar == '-' && obj.value.indexOf('-') == -1 : false;
    var isFirstD = allowDecimal ? keychar == '.' && obj.value.indexOf('.') == -1 : false;
    return isFirstN || isFirstD || reg.test(keychar);
}

function PreventPasteNonNumbers(obj, allowDecimal) {
    var regex = /^\d+$/;
    if (allowDecimal == true) {
        regex = /^\d+(\.\d{1,2})?$/;
    }
    setTimeout(function () {
        if (!regex.test(obj.value)) {
            obj.value = '';
            return false;
        }
    }, 100);
}

function ExtractNumber(obj, decimalPlaces, allowNegative) {
    var temp = obj.value;
    var reg0Str = '[0-9]*';
    if (decimalPlaces > 0) {
        reg0Str += '\\.?[0-9]{0,' + decimalPlaces + '}';
    } else if (decimalPlaces < 0) {
        reg0Str += '\\.?[0-9]*';
    }
    reg0Str = allowNegative ? '^-?' + reg0Str : '^' + reg0Str;
    reg0Str = reg0Str + '$';
    var reg0 = new RegExp(reg0Str);
    if (reg0.test(temp)) return true
    var reg1Str = '[^0-9' + (decimalPlaces != 0 ? '.' : '') + (allowNegative ? '-' : '') + ']';
    var reg1 = new RegExp(reg1Str, 'g');
    temp = temp.replace(reg1, '');
    if (allowNegative) {
        var hasNegative = temp.length > 0 && temp.charAt(0) == '-';
        var reg2 = /-/g;
        temp = temp.replace(reg2, '');
        if (hasNegative) temp = '-' + temp;
    }

    if (decimalPlaces != 0) {
        var reg3 = /\./g;
        var reg3Array = reg3.exec(temp);
        if (reg3Array != null) {
            var reg3Right = temp.substring(reg3Array.index + reg3Array[0].length);
            reg3Right = reg3Right.replace(reg3, '');
            reg3Right = decimalPlaces > 0 ? reg3Right.substring(0, decimalPlaces) : reg3Right;
            temp = temp.substring(0, reg3Array.index) + '.' + reg3Right;
        }
    }
    obj.value = temp;
}

function EnableDisableSpecialization(id, name) {
    if (id == 'chkDC1') {
        //  $("input.name").removeAttr("disabled");
        for (var i = 1; i < 7; i++) {
            if (id != name + i) {
                $('#' + name + i).attr("disabled", true);
            }
        }
        $('#chkEC1').attr("disabled", true);
        $('#chkFND1').attr("disabled", true);
        $('#chkHTM1').attr("disabled", true);
        $('#chkIDRM1').attr("disabled", true);
        $('#chkMCE1').attr("disabled", true);
        $('#chkTAD1').attr("disabled", true);
    }
    else if (id == 'chkDC1') {
        //  $("input.name").removeAttr("disabled");
        for (var i = 1; i < 7; i++) {
            if (id != name + i) {
                $('#' + name + i).attr("disabled", true);
            }
        }
        $('#chkEC1').attr("disabled", true);
        $('#chkFND1').attr("disabled", true);
        $('#chkHTM1').attr("disabled", true);
        $('#chkIDRM1').attr("disabled", true);
        $('#chkMCE1').attr("disabled", true);
        $('#chkTAD1').attr("disabled", true);
    }
}

function dosum() {    
    var txt1NumberValue = CheckMarksValue($("#Subject1MarksObtained").val());
    var txt2NumberValue = CheckMarksValue($("#Subject2MarksObtained").val());
    var txt3NumberValue = CheckMarksValue($("#Subject3MarksObtained").val());
    var txt4NumberValue = CheckMarksValue($("#Subject4MarksObtained").val());
    var txt5NumberValue = CheckMarksValue($("#Subject5MarksObtained").val());
    var txt6NumberValue = CheckMarksValue($("#Subject6MarksObtained").val());
    var txt7NumberValue = CheckMarksValue($("#Subject7MarksObtained").val());

    var txt1NumberValueTotal = CheckMarksValue($("#Subject1Total").val());
    var txt2NumberValueTotal = CheckMarksValue($("#Subject2Total").val());
    var txt3NumberValueTotal = CheckMarksValue($("#Subject3Total").val());
    var txt4NumberValueTotal = CheckMarksValue($("#Subject4Total").val());
    var txt5NumberValueTotal = CheckMarksValue($("#Subject5Total").val());
    var txt6NumberValueTotal = CheckMarksValue($("#Subject6Total").val());
    var txt7NumberValueTotal = CheckMarksValue($("#Subject7Total").val());

    var result = parseInt(txt1NumberValue) + parseInt(txt2NumberValue) + parseInt(txt3NumberValue) + parseInt(txt4NumberValue) + parseInt(txt5NumberValue) + parseInt(txt6NumberValue) + parseInt(txt7NumberValue);
    var resultTotal = parseInt(txt1NumberValueTotal) + parseInt(txt2NumberValueTotal) + parseInt(txt3NumberValueTotal) + parseInt(txt4NumberValueTotal) + parseInt(txt5NumberValueTotal) + parseInt(txt6NumberValueTotal) + parseInt(txt7NumberValueTotal);

    if (result>0) {
        $("#TotalMarks").val(result);
    } else {
        $("#TotalMarks").val(0);
    }

    if (result>0 && resultTotal > 0) {
            $("#Percentage").val((result * 100 / resultTotal).toFixed(2));        
    } else {
        $("#Percentage").val(0);
    }
}

function CheckMarksValue(value) {
    if (value == undefined || value == "" || value.trim() == "")
        value = 0;
    return value;
}

function getFileSize(fileid) {
    try {
        var fileSize = 0;
        if (navigator.appName.indexOf("Microsoft") != -1) {
            var objFSO = new ActiveXObject("Scripting.FileSystemObject"); var filePath = $("#" + fileid)[0].value;
            var objFile = objFSO.getFile(filePath);
            var fileSize = objFile.size / 1048; //size in kb
            //fileSize = fileSize / 1048576; //size in mb 
        }
        else {
            fileSize = $("#" + fileid)[0].files[0].size / 1048 //size in kb
            //fileSize = fileSize / 1048576; //size in mb 
        }
        return fileSize;
    }
    catch (e) {
        alert("Error is :" + e);
    }
}

function getNameFromPath(strFilepath) {
    var objRE = new RegExp(/([^\/\\]+)$/);
    var strName = objRE.exec(strFilepath);

    if (strName == null) {
        return null;
    }
    else {
        return strName[0];
    }
}

function checkfile(controlName, maxSize) {
    var file = getNameFromPath($("#" + controlName).val());
    if (file != null) {
        var flag = false;
        var extension = file.substr((file.lastIndexOf('.') + 1));
        switch (extension.toLowerCase()) {
            case 'jpg':
            case 'png':
            case 'jpeg':
            case 'bmp':
                flag = true;
                break;
            default:
                flag = false;
        }
        if (flag == false) {
            //errorMessageForPopup("You can upload only jpg, png, gif, bmp extension file");
            $("#" + controlName).val('');
            $("#" + controlName + "Error").show();
            return false;
        }
        else {
            var size = getFileSize(controlName);
            if (size > maxSize) {
                // errorMessageForPopup("You can upload file up to " + maxSize.toString() + " MB");
                $("#" + controlName).val('');
                $("#" + controlName + "Error").show();
                return false;
            }
            else {

            }
        }
    }
    $("#" + controlName + "Error").hide();
    return true;
}

//Form Post Method all Detail like save
function savePageDataPostImageAjax(obj, formName, successCallBack, errorCallBack, fromData) {
    var $form = $('#' + formName);
    //if ($form.valid()) { 
    $.ajax({
        url: encodeURI($(obj).attr("data-post")),
        type: "POST",
        data: fromData,
        processData: false,
        enctype: 'multipart/form-data',
        contentType: false,// not json
        error: function (jqXHR, exception) { 
            if (jqXHR.status == '401') {
                errorCallBack(jqXHR.responseJSON)
            }
            else if (jqXHR.status == 500) {
                errorCallBack('Internal Server Error [500].');
            }
            else if (jqXHR.status == 404) {
                errorCallBack('Requested page not found. [404]');
            }
            else if (jqXHR.status != '200') {
                AjaxCallError(jqXHR.responseJSON)
            }
        },
        success: function (jqxhr) { 
            successCallBack(jqxhr);
        }
    });
}

function showLoading() {
    $(".dialog-background").show();
}

function hideLoading() {
    $(".dialog-background").hide();
}

var isDisplayLoader = false;
$(document).ajaxStart(function () {    
    showLoading();
});

$(document).ajaxComplete(function () {});

$(document).ajaxStop(function () { 
    hideLoading(); 
});