<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Master/Master.master"
    AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Admin_Dashboard" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <script language="javascript">
        function ShowLinkDescription(element, className) {
            var timer = -1;
            var e;
            $(element).find(".LinkDescription").each(function () {
                e = $(this);
                timer = setTimeout(function () {
                    if ($(e).attr("timer") != undefined) {
                        $(e).removeAttr("timer");
                        $(e).show();

                        $(e).attr("class", className + " LinkDescription");
                        //setTimeout(function () { $(e).fadeOut(500); timer = -1; }, 3000);
                    }
                }, 1000);

                $(e).attr("timer", timer);
            });
        }

        function HideLinkDescription(element) {
            $(element).find(".LinkDescription").each(function () {
                $(this).removeAttr("timer");
                $(this).fadeOut(100);
            });
        }
    </script>
    <style type="text/css">
        .ExamStateContainer {
            width: 997px; /*Shrikant*/
            height: 70px;
            border-radius: 5px;
            padding: 1px;
            margin-top: -9px;
            margin-left: -40px;
            background-color: rgb(206, 195, 195);
            float: left;
        }

        .ExamState {
            width: 89px;
            height: 50px;
            float: left;
            margin-right: 2px;
            background-color: rgb(219, 219, 219);
            padding: 10px;
            border-radius: 3px;
            color: Black;
            font-weight: bold;
            text-align: center;
        }

        .Image {
            width: 60px;
            height: 60px;
        }
    </style>
    <!--Added by shrikant for Dashboard page-->
    <div style="float: left; margin-top: 0%; width: 88%; margin-left: 10%; display: block; font-size: 12px;">
        <div class="Box BoxOrange" style="height: 100%; padding-bottom: 8%;">
            <div>
                <div class="BoxList" onmouseover="ShowLinkDescription(this,'BoxOrange');" onmouseout="HideLinkDescription(this)">
                    <div class="image">
                        <img src="static/image/Inventory.png" alt="Add Inventory" class="Image" /><br />
                    </div>
                    <div class="text">
                        <a href="AddInventory.aspx">Add New Inventory</a>
                        <div class="LinkDescription">
                            Add new inventory to system
                        </div>
                    </div>
                </div>
                <div class="BoxList" onmouseover="ShowLinkDescription(this,'BoxOrange');" onmouseout="HideLinkDescription(this)">
                    <div class="image">
                        <img src="static/image/Manage-Inventory.png" alt="Manage Inventory" class="Image" />
                    </div>
                    <div class="text">
                        <a href="MangeInventory.aspx">Manage Inventory</a>
                        <div class="LinkDescription">
                            Edit old inventory in system
                        </div>
                    </div>
                </div>
                <div class="BoxList" onmouseover="ShowLinkDescription(this,'BoxOrange');" onmouseout="HideLinkDescription(this)">
                    <div class="image">
                        <img src="static/image/Current-stock-Report.png" alt="Current stock report" class="Image" />
                    </div>
                    <div class="text">
                        <a href="CurrentStockReport.aspx">Current Stock Report</a>
                        <div class="LinkDescription">
                            Generate excel report for Current stock
                        </div>
                    </div>
                </div>


                <div class="BoxList" onmouseover="ShowLinkDescription(this,'BoxOrange');" onmouseout="HideLinkDescription(this)">
                    <div class="image">
                        <img src="static/image/DepartmentMaster.png" alt="Inventory Issue" class="Image" />
                    </div>
                    <div class="text">
                        <a href="InventoryIssue.aspx">Inventory Issue</a>
                        <div class="LinkDescription">
                            Create Issue of Inventory Item to Teachers
                        </div>
                    </div>
                </div>
                <div class="BoxList" onmouseover="ShowLinkDescription(this,'BoxOrange');" onmouseout="HideLinkDescription(this)">
                    <div class="image">
                        <img src="static/image/DepartmentMaster.png" alt="Issue Report" class="Image" />
                    </div>
                    <div class="text">
                        <a href="IssueReport.aspx">Issue Detail Report</a>
                        <div class="LinkDescription">
                            Generate Issue Details Report
                        </div>
                    </div>
                </div>
                <div class="BoxList" onmouseover="ShowLinkDescription(this,'BoxOrange');" onmouseout="HideLinkDescription(this)">
                    <div class="image">
                        <img src="static/image/Purchase-Order-Report.png" alt="Bill Detail" class="Image" />
                    </div>
                    <div class="text">
                        <a href="InventoryBills.aspx">Bill Detail</a>
                        <div class="LinkDescription">
                            Create Bill Details for Inventory Items
                        </div>
                    </div>
                </div>
                <div class="BoxList" onmouseover="ShowLinkDescription(this,'BoxOrange');" onmouseout="HideLinkDescription(this)">
                    <div class="image">
                        <img src="static/image/Purchase-Order-Report.png" alt="Bill Detail Report" class="Image" />
                    </div>
                    <div class="text">
                        <a href="BillDetailsReport.aspx">Bill Detail Report</a>
                        <div class="LinkDescription">
                            Generate Bill Details Report
                        </div>
                    </div>
                </div>
                <div class="BoxList" onmouseover="ShowLinkDescription(this,'BoxOrange');" onmouseout="HideLinkDescription(this)">
                    <div class="image">
                        <img src="static/image/Manage-Inventory.png" alt="Manage Inventory" class="Image" />
                    </div>
                    <div class="text">
                        <a href="ManageTeachers.aspx">Manage Teacher</a>
                        <div class="LinkDescription">
                            Edit Teachers in Department
                        </div>
                    </div>
                </div>
                <div class="BoxList" onmouseover="ShowLinkDescription(this,'BoxOrange');" onmouseout="HideLinkDescription(this)">
                    <div class="image">
                        <img src="static/image/Current-stock-Report.png" alt="Current stock report" class="Image" />
                    </div>
                    <div class="text">
                        <a href="CurrentStockItemWise.aspx">Item Wise Current Stock Report</a>
                        <div class="LinkDescription">
                            Generate excel report for Item Wise Current stock
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
