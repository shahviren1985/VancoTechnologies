function CheckValidation() {
    var PageName = document.getElementById('ctl00_Content_txtPageName').value;
    if (PageName == "" || PageName == 0) {
        alert("Please Enter page name");
        document.getElementById('ctl00_Content_txtPageName').focus();
        return false;

    }
    else {
        return true;
    }
}

function Confirmation() {
    if (confirm("Are you sure, you want to delete the selected page?")) {
        return true;
    }
    else {
        document.getElementById('ctl00_Content_HdnDeleteId').value = "";
        return false;
    }
}

function FillDropDown() {
    FillPageDetails();
}

function FillPageDetails() {    
    if (document.getElementById('ctl00_Content_hfPageContent') == undefined) {        
        return;
    }
    //alert("after if");
    var Details = document.getElementById('ctl00_Content_hfPageContent').value;
    
    document.getElementById('txtText').value = Details;
    document.getElementById('ctl00_Content_hfPageContent').value = "";

}

function FillNewsDetails() {

    //comment by shrikant on 7:49 PM 10/23/2012

    var Details = document.getElementById('ctl00_Content_hfPageContent').value;

    document.getElementById('txtText').innerHTML = Details;
    
    //var Details = document.getElementById('ctl00_Content_hfPageContent').value;

    document.getElementById('ctl00_Content_hfPageContent').value = "";
}

function Encoding() {
    var Data = document.getElementById('txtText').value;
    var EncodeData = escape(Data);
    document.getElementById('ctl00_Content_hfPageContent').value = EncodeData;
//    alert("Alert");
}

function EncodeNews() {
    var encodeData = escape(document.getElementById('txtdescription').value);
    
    try {
        document.getElementById('ctl00_Content_hfNewsContent').value = encodeData;
    }
    catch (e) {
        alert(e)
    }
}

function SetText(element, defaultText) {
    if (element.value == "") {
        element.value = defaultText;
    }
}

function CheckText(element, defaultText) {
    if (element.value == defaultText) {
        element.value = "";
    }
}