<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChapterwiseReport.aspx.cs" Title="Chapter wise Students Performance"
    MasterPageFile="~/Master/AdminMasterPage.master" Inherits="Admin_ChapterwiseReport" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <div>
        <div style="margin-top: 5%;">
            <div id="logo" style="float: left;">
                <asp:Image Style="width: 60%;" ID="imgLogo" runat="server" />
            </div>
            <h3>
                Chapter wise Students Performance
            </h3>
        </div>
        <div id="errorSummary" class="ErrorContainer" runat="server" style="text-align: left;
            color: Red; float: left; width: 100%;" visible="false">
        </div>
        <div id="Main" class="Record">
        </div>
    </div>
    <div id="hfContainer" runat="server">
        <asp:HiddenField ID="hfChapter1" runat="server" Value="[]" />
        <asp:HiddenField ID="hfChapter2" runat="server" Value="[]" />
        <asp:HiddenField ID="hfChapter3" runat="server" Value="[]" />
        <asp:HiddenField ID="hfChapter4" runat="server" Value="[]" />
        <asp:HiddenField ID="hfChapter5" runat="server" Value="[]" />
        <asp:HiddenField ID="hfChapter6" runat="server" Value="[]" />
        <asp:HiddenField ID="hfChapter7" runat="server" Value="[]" />
        <asp:HiddenField ID="hfChapter8" runat="server" Value="[]" />
        <asp:HiddenField ID="hfChapter9" runat="server" Value="[]" />
        <asp:HiddenField ID="hfChapter10" runat="server" Value="[]" />
        <asp:HiddenField ID="hfChapter11" runat="server" Value="[]" />
        <asp:HiddenField ID="hfChapter12" runat="server" Value="[]" />
        <asp:HiddenField ID="hfChapter13" runat="server" Value="[]" />
        <asp:HiddenField ID="hfChapter14" runat="server" Value="[]" />
    </div>
    <script type="text/javascript">
        function PopulateChapterwiseUserPercentage() {
            //alert("inside PopulateChapterwiseUserPercentage");
            var pieData = [];

            for (var i = 1; i <= 14; i++) {

                //generateDynamicSection(i);
                pieData = JSON.parse($("#Content_hfChapter" + i).val());
                //console.log("Content_hfChapter" + i + "value");
                //console.log(pieData);
                generateDynamicSection(i);
                // alert("value in hf" + i + " : " + $("#Content_hfChapter" + i).val());
                var myPie = new Chart(document.getElementById("canvas" + i).getContext("2d")).Pie(pieData);
                //generateDynamicSection(i);
            }

            function generateDynamicSection(count) {
                if (count != 0) {
                    var box = $("<div/>");
                    $(box).attr("id", "box" + count);
                    $(box).attr("class", "ChapterPiaContainer");

                    var canvasContainer = $("<div/>");
                    $(canvasContainer).attr("id", "canvasContainer" + count);
                    $(canvasContainer).attr("class", "canvasContainer");

                    var canvas = $("<canvas/>");
                    $(canvas).attr("id", "canvas" + count);
                    $(canvas).attr("height", 150);
                    $(canvas).attr("width", 150);

                    var dataDescriptionContainer = $("<div/>");
                    $(dataDescriptionContainer).attr("id", "dataDescriptionContainer" + count);
                    $(dataDescriptionContainer).attr("class", "DataDescriptionContainer");

                    for (var j = 1; j <= 5; j++) {
                        var values = $("<div/>");
                        $(values).attr("class", "values");

                        var colorCode = $("<div/>");
                        $(colorCode).attr("class", "DataDescription");

                        var Range = $("<div/>");

                        if (j == 1) {
                            $(Range).attr("class", "Range");
                            $(Range).html("< 40 ");
                            //adding number of users who are coming in this criteria
                            $(colorCode).html(pieData[j - 1].value);
                        }
                        else if (j == 2) {
                            $(Range).html("> 40 and < 50");
                            $(Range).attr("class", "Range");
                            $(colorCode).attr("style", "background-color: #FE9A2E");
                            //adding number of users who are coming in this criteria
                            $(colorCode).html(pieData[j - 1].value);
                        }
                        else if (j == 3) {
                            $(Range).html("> 50 and < 60 ");
                            $(Range).attr("class", "Range");
                            $(colorCode).attr("style", "background-color: #5858FA");
                            //adding number of users who are coming in this criteria
                            $(colorCode).html(pieData[j - 1].value);
                        }
                        else if (j == 4) {
                            $(Range).html("> 60 and < 70 ");
                            $(Range).attr("class", "Range");
                            $(colorCode).attr("style", "background-color: #F4FA58");
                            //adding number of users who are coming in this criteria
                            $(colorCode).html(pieData[j - 1].value);
                        }
                        else if (j == 5) {
                            $(Range).html(">70");
                            $(Range).attr("class", "Range");
                            $(colorCode).attr("style", "background-color: #3ADF00");
                            //adding number of users who are coming in this criteria
                            $(colorCode).html(pieData[j - 1].value);
                        }

                        values.append(colorCode);
                        values.append(Range);
                        dataDescriptionContainer.append(values);
                    }

                    // populating all students
                    var totalStudents = 0;
                    for (var k = 0; k < pieData.length; k++) {
                        totalStudents += parseInt(pieData[k].value);
                    }

                    var values = $("<div/>");
                    $(values).attr("class", "values");
                    var colorCode = $("<div/>");
                    $(colorCode).attr("class", "DataDescription");
                    var Range = $("<div/>");
                    $(Range).html("Total Students");
                    $(Range).attr("class", "Range");
                    $(colorCode).attr("style", "background-color: #ccc");
                    $(colorCode).html(totalStudents);

                    values.append(colorCode);
                    values.append(Range);
                    dataDescriptionContainer.append(values);

                    var chapterName = $("<div/>");
                    // $(box).attr("id", "box" + count);
                    $(chapterName).attr("class", "ChapterNameContainer");
                    if (count == 1) {
                        $(chapterName).html("Computer Basics");
                    }
                    else if (count == 2) {
                        $(chapterName).html("Computer Hardware");
                    }
                    else if (count == 3) {
                        $(chapterName).html("Storage Devices");
                    }
                    else if (count == 4) {
                        $(chapterName).html("Operating System");
                    }
                    else if (count == 5) {
                        $(chapterName).html("File management");
                    }
                    else if (count == 6) {
                        $(chapterName).html("MS Paint");
                    }
                    else if (count == 7) {
                        $(chapterName).html("Notepad");
                    }
                    else if (count == 8) {
                        $(chapterName).html("Word");
                    }
                    else if (count == 9) {
                        $(chapterName).html("Excel");
                    }
                    else if (count == 10) {
                        $(chapterName).html("Power point");
                    }
                    else if (count == 11) {
                        $(chapterName).html("Computer Networks");
                    }
                    else if (count == 12) {
                        $(chapterName).html("Security");
                    }
                    else if (count == 13) {
                        $(chapterName).html("Internet");
                    }
                    else if (count == 14) {
                        $(chapterName).html("Social media");
                    }

                    //dataDescriptionContainer.append(values2);
                    canvasContainer.append(canvas);
                    canvasContainer.append(dataDescriptionContainer);


                    box.append(canvasContainer);
                    box.append(chapterName);

                    $("#Main").append(box);
                }
            }
        }
    </script>
</asp:Content>
