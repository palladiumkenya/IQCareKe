<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Module.Master" AutoEventWireup="true"
    CodeBehind="frmBilling_ClientBill.aspx.cs" Inherits="IQCare.Web.Billing.ClientBill"
    MaintainScrollPositionOnPostback="true"  EnableEventValidation="false"%>

<%@ MasterType VirtualPath="~/MasterPage/Module.master" %>
<%@ Register Assembly="AjaxControlToolkit" TagPrefix="act" Namespace="AjaxControlToolkit" %>
<%@ Register Src="TransactionReversal.ascx" TagName="TransactionReversal" TagPrefix="uc1" %>
<%@ Register Src="PatientDeposits.ascx" TagName="PatientDeposit" TagPrefix="pd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <script language="javascript" type="text/javascript">

        function WindowPrint() {
            window.print();
        }
        function printInterimBill() {
            window.open("./frmCustomReportPrint.aspx");
            // div.innerHTML = '<iframe src="./frmCustomReportPrint.aspx" onload="this.contentWindow.print();"></iframe>';
        }
        function WindowPrintAll() {
            window.print();
        }
        function PrintGrid(strid) {

            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'letf=0,top=0,width=400,height=400,toolbar=0,scrollbars=0,status=0');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

        }


        function openReportPage(path) {
            window.open(path, 'popupwindow', 'toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=yes,resizable=yes,width=950,height=650,scrollbars=yes');
        }
        function openReceiptPage(path) {
            window.open(path, 'ReceiptPage', 'toolbars=no,location=no,directories=no,dependent=yes,top=100,left=30,maximize=no,resize=yes,width=1000,height=800,scrollbars=yes');
        }
        function ace1_itemSelected(sender, e) {
            var hdCustID = $get('<%= hdCustID.ClientID %>');
            hdCustID.value = e.get_value();
            //alert(hdCustID.value);
        }

        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
        var allCheckBoxSelector = '#<%=grdUnBilledItems.ClientID%> input[id*="chkBxHeader"]:checkbox';
        var checkBoxSelector = '#<%=grdUnBilledItems.ClientID%> input[id*="chkBxItem"]:checkbox:not(:disabled)';
        function ToggleCheckUncheckAllOptionAsNeeded() {
            var totalCheckboxes = $(checkBoxSelector),
                checkedCheckboxes = totalCheckboxes.filter(":checked");
            noCheckboxesAreChecked = (checkedCheckboxes.length === 0),
                allCheckboxesAreChecked = (totalCheckboxes.length === checkedCheckboxes.length);

            $(allCheckBoxSelector).prop('checked', allCheckboxesAreChecked);
            if (noCheckboxesAreChecked) {
                // $("#divRequestAction").css("display", "none");
              //  $("#<%= buttonGenerateBill.ClientID %>").prop("disabled", true);

            }
            else {
               // $("#<%= buttonGenerateBill.ClientID %>").prop("disabled", false);
                //attr("disabled", "false");// css("display", "block");
            }

        }
        $(document).ready(function () {
            $(allCheckBoxSelector).on("click", "", function () {
                $(checkBoxSelector).prop('checked', $(this).is(':checked'));
                ToggleCheckUncheckAllOptionAsNeeded();

            });

            $(checkBoxSelector).on('click', "", ToggleCheckUncheckAllOptionAsNeeded);
            ToggleCheckUncheckAllOptionAsNeeded();

        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_pageLoaded(ToggleCheckUncheckAllOptionAsNeeded);

    </script>
    <style type="text/css">
        .AutoExtender
        {
            font-family: Courier New, Arial, sans-serif;
            font-size: 11px;
            font-weight: 100;
            border: solid 1px #006699;
            line-height: 15px;
            padding: 0px;
            background-color: White;
            margin-left: 0px;
            width: 800px;
            text-align: left;
        }
        .AutoExtenderList
        {
            cursor: pointer;
            color: black;
            z-index: 2147483647 !important;
            text-align: left;
        }
        .AutoExtenderHighlight
        {
            color: White;
            background-color: #006699;
            cursor: pointer;
        }
        #divwidth
        {
            width: 800px !important;
        }
        #divwidth div
        {
            width: 800px !important;
        }
        #divwidthFooter
        {
            width: 800px !important;
        }
        #divwidthFooter div
        {
            width: 800px !important;
        }
        .datatable
        {
            margin-bottom: 161px;
        }
    </style>
    <div class="container-fluid">
        <h2 class="forms" align="left">
            Patient Bill</h2>
    </div>
    <%--<table cellspacing="6" cellpadding="0" width="100%" border="0">
        <tbody>
            <tr>
                <td class="form">
                    <div class="grid" style="width: 100%;">
                        <div class="rounded">
                            <div class="mid-outer">
                                <div class="mid-inner">
                                    <div class="mid">
                                        <label>
                                            Patient Name:
                                            <asp:Label ID="lblname" runat="server"></asp:Label>
                                        </label>
                                        &nbsp;&nbsp;
                                        <label>
                                            Sex:
                                            <asp:Label ID="lblsex" runat="server"></asp:Label>
                                        </label>
                                        &nbsp;&nbsp;
                                        <label>
                                            DOB:
                                            <asp:Label ID="lbldob" runat="server"></asp:Label>
                                        </label>
                                        &nbsp;&nbsp;<label>
                                            Facility ID:
                                            <asp:Label ID="lblFacilityID" runat="server"></asp:Label>
                                        </label>
                                        &nbsp;&nbsp;
                                        <label>
                                            IQCare Reference Number:
                                            <asp:Label ID="lblIQno" runat="server"></asp:Label>
                                        </label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
        </tbody>
    </table>--%>
    <div class="container-fluid">
        <asp:UpdatePanel ID="upError" runat="server">
            <ContentTemplate>
                <asp:Panel ID="divError" runat="server" Style="padding: 5px; background-color: #FFFFC0;
                    border: solid 1px #C00000; margin-bottom: 10px;" HorizontalAlign="Left" Visible="false">
                    <asp:Label ID="lblError" runat="server" Style="font-weight: bold; color: #800000"
                        Text=""></asp:Label>
                </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="TabContainer1" />
            </Triggers>
        </asp:UpdatePanel>
        <div align="right">
            <asp:LinkButton ID="btnPatientInterimBill" runat="server" Text="Current Oustanding Bill..."
                OnClick="btnPatientInterimBill_Click"></asp:LinkButton>
        </div>
        <act:TabContainer ID="TabContainer1" runat="server" Width="100%" ActiveTabIndex="0"
            OnDemand="true" AutoPostBack="true" OnActiveTabChanged="TabContainer1_ActiveTabChanged">
            <act:TabPanel ID="tabCurrentBill" runat="server" HeaderText="Unbilled Items">
                <ContentTemplate>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                                <tbody>
                                    <tr>
                                        <td class="pad5 formbg border">
                                            <div id="divBills" class="grid" style="width: 100%;">
                                                <div class="rounded">
                                                    <div class="mid-outer">
                                                        <div class="mid-inner">
                                                            <div class="mid" style="height: 200px; overflow: auto">
                                                                <div id="div-gridview" class="GridView whitebg">
                                                                    <asp:DropDownList ID="ddlCostCenter" runat="server" Style="z-index: 2" AutoPostBack="false"
                                                                        Visible="false">
                                                                    </asp:DropDownList>
                                                                    <asp:GridView ID="grdUnBilledItems" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                                        BorderColor="White" BorderWidth="1px" CellPadding="0" CssClass="datatable table-striped table-responsive"
                                                                        DataKeyNames="billItemID,PaymentType,ItemType,ItemID" GridLines="None" OnRowCancelingEdit="grdUnBilledItems_RowCancelingEdit"
                                                                        OnRowCommand="grdUnBilledItems_RowCommand" OnRowDataBound="grdUnBilledItems_RowDataBound"
                                                                        OnRowDeleting="grdUnBilledItems_RowDeleting" OnRowEditing="grdUnBilledItems_RowEditing"
                                                                        OnRowUpdating="grdUnBilledItems_RowUpdating" PageIndex="1" ShowFooter="True"
                                                                        ShowHeaderWhenEmpty="True" Width="100%">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <HeaderTemplate>
                                                                                    <asp:CheckBox ID="chkBxHeader" runat="server" AutoPostBack="false" /></HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkBxItem" runat="server" AutoPostBack="false"></asp:CheckBox></ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="5px" Wrap="False" />
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="5px" Wrap="False" />
                                                                                <ItemStyle Width="20px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Left">
                                                                                <EditItemTemplate>
                                                                                    <asp:Label ID="lblEditDate" runat="server" Font-Bold="True" Text='<%# Bind("BillItemDate","{0:dd-MMM-yyyy}") %>'
                                                                                        Width="99%"></asp:Label></EditItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="lblNewDate" runat="server" Width="99%"></asp:Label></FooterTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("BillItemDate","{0:dd-MMM-yyyy}") %>'></asp:Label></ItemTemplate>
                                                                                <ItemStyle Width="10%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Description" HeaderStyle-HorizontalAlign="Left">
                                                                                <EditItemTemplate>
                                                                                    <asp:TextBox ID="txtEditDescription" runat="server" AutoPostBack="true" OnTextChanged="txtEditDescription_textChanged"
                                                                                        Text='<%# Bind("ItemName") %>' Width="95%" Font-Names="Courier New"></asp:TextBox>
                                                                                    <div id="divwidth" runat="server" style="z-index:6000">
                                                                                    </div>
                                                                                    <act:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" CompletionInterval="30"
                                                                                        CompletionListCssClass="AutoExtender" CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                                                                        CompletionListItemCssClass="AutoExtenderList" CompletionSetCount="10" BehaviorID="AutoCompleteEx"
                                                                                        EnableCaching="false" FirstRowSelected="false" MinimumPrefixLength="2" OnClientItemSelected="ace1_itemSelected"
                                                                                        ServiceMethod="SearchItems" TargetControlID="txtEditDescription" CompletionListElementID="divwidth">
                                                                                    </act:AutoCompleteExtender>
                                                                                </EditItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:TextBox ID="txtNewDescription" runat="server" AutoPostBack="true" OnTextChanged="txtNewDescription_textChanged"
                                                                                        Width="95%" Font-Names="Courier New"></asp:TextBox>
                                                                                    <div id="divwidthfooter" runat="server" style="z-index:6000" />
                                                                                    <act:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionInterval="30"
                                                                                        CompletionListCssClass="AutoExtender" CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                                                                        CompletionListItemCssClass="AutoExtenderList" CompletionSetCount="10" BehaviorID="AutoCompleteExFooter"
                                                                                        EnableCaching="false" FirstRowSelected="false" MinimumPrefixLength="2" OnClientItemSelected="ace1_itemSelected"
                                                                                        ServiceMethod="SearchItems" TargetControlID="txtNewDescription" CompletionListElementID="divwidthfooter">
                                                                                    </act:AutoCompleteExtender>
                                                                                </FooterTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("ItemName") %>'></asp:Label></ItemTemplate>
                                                                                <ItemStyle Width="35%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Department" Visible="True" HeaderStyle-HorizontalAlign="Left">
                                                                                <EditItemTemplate>
                                                                                    <div style="white-space: nowrap">
                                                                                        <asp:DropDownList ID="ddlItemCostCenter" runat="server" Width="99%" Visible="false">
                                                                                        </asp:DropDownList>
                                                                                        <asp:Label ID="lblCostCenter" runat="server" Text='<%# Bind("CostCenterName") %>'
                                                                                            Visible="false"></asp:Label>
                                                                                    </div>
                                                                                </EditItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <div style="white-space: nowrap">
                                                                                        <asp:DropDownList ID="ddlItemCostCenter" runat="server" Width="99%" Visible="false">
                                                                                        </asp:DropDownList>
                                                                                        <asp:Label ID="lblCostCenter" runat="server" Text='<%# Bind("CostCenterName") %>'
                                                                                            Visible="false"></asp:Label>
                                                                                    </div>
                                                                                </FooterTemplate>
                                                                                <ItemTemplate>
                                                                                    <div style="white-space: nowrap">
                                                                                        <asp:Label ID="lblCostCenter" runat="server" Text='<%# Bind("CostCenterName") %>'></asp:Label></div>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="10%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Qty" HeaderStyle-HorizontalAlign="Left">
                                                                                <EditItemTemplate>
                                                                                    <asp:TextBox ID="txtEditQuantity" runat="server" onkeypress="return isNumber(event)"
                                                                                        Text='<%# Bind("Quantity") %>' Width="20px" Wrap="False"></asp:TextBox></EditItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:TextBox ID="txtNewQuantity" runat="server" onkeypress="return isNumber(event)"
                                                                                        Width="20px" Text="1"></asp:TextBox></FooterTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("Quantity") %>'></asp:Label></ItemTemplate>
                                                                                <ItemStyle Width="5%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Price" HeaderStyle-HorizontalAlign="Left">
                                                                                <EditItemTemplate>
                                                                                    <asp:Label ID="lblEditUnitPrice" runat="server" Text='<%# Bind("sellingPrice") %>'></asp:Label></EditItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="lblNewUnitPrice" runat="server" Width="90%">0</asp:Label></FooterTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("sellingPrice") %>' Width="99%"></asp:Label></ItemTemplate>
                                                                                <ItemStyle Width="8%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Left">
                                                                                <EditItemTemplate>
                                                                                    <asp:Label ID="lblEditAmount" runat="server" Width="99%"></asp:Label></EditItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="lblNewAmountPrice" runat="server" Width="90%"></asp:Label></FooterTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("Amount") %>' Width="99%"></asp:Label></ItemTemplate>
                                                                                <ItemStyle Width="10%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Received?" Visible="True">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblServiceStatus" runat="server" Width="99%"></asp:Label></ItemTemplate>
                                                                                <ItemStyle Width="5%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField ShowHeader="False">
                                                                                <EditItemTemplate>
                                                                                    <asp:Button ID="buttonUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                                                                        Text="Update" ForeColor="Blue"></asp:Button>&#160;<asp:Button ID="buttonCancelEdit"
                                                                                            runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" ForeColor="Blue">
                                                                                        </asp:Button></EditItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Button ID="btnNewAdd" runat="server" CommandName="AddItem" ForeColor="Blue"
                                                                                        Text="Add" /></FooterTemplate>
                                                                                <ItemTemplate>
                                                                                    <div style="white-space: nowrap">
                                                                                        <asp:Button ID="buttonEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                                                                            Text="Edit" ForeColor="Blue"></asp:Button>&nbsp;&nbsp;&nbsp;<asp:Button ID="buttonDelete"
                                                                                                runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" ForeColor="Blue">
                                                                                            </asp:Button></div>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </asp:TemplateField>
                                                                            <%--<asp:TemplateField ShowHeader="False">
                                                                                <ItemTemplate>
                                                                                    </ItemTemplate>
                                                                            </asp:TemplateField>--%>
                                                                        </Columns>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <RowStyle CssClass="gridrow" />
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                            <asp:HiddenField ID="hdCustID" runat="server" />
                                                            <asp:HiddenField ID="HItemTypeID" runat="server" />
                                                            <asp:HiddenField ID="HItemTypeName" runat="server" />
                                                            <asp:HiddenField ID="HItemID" runat="server" />
                                                            <asp:HiddenField ID="hConsumableItemTypeID" runat="server" Value="-1" />
                                                            <br />
                                                            <%--  <act:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="buttonGenerateBill"
                                                                ConfirmText="Are you sure you want to generate the bill for the checked items?"
                                                                Enabled="True" /> --%>
                                                        </div>
                                                    </div>
                                                    <div class="bottom-outer">
                                                        <div class="bottom-inner">
                                                            <div class="bottom">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr align="right" style="">
                                        <td>
                                            <asp:Label ID="lbl_total" runat="server" Text="Total:" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form pad5 center">
                                            <br />
                                            <div class="col-md-2">
                                            </div>
                                            <div class="form-group col-md-8">
                                                <asp:Button ID="btn_saveBill" runat="server" OnClick="btn_saveBill_Click" Text="Save"
                                                    type="button" CssClass="btn btn-default btn-md col-md-3" Style="margin-right:5px; font-weight:bold"/><asp:Button ID="buttonGenerateBill"
                                                        runat="server" type="button" Text="Generate Bill" OnClick="GenerateBill_Click"   Style="margin-right:5px; font-weight:bold"
                                                        OnPreRender="buttonGenerateBill_PreRender" CssClass="btn btn-default btn-md col-md-3" /><asp:Button
                                                            ID="btn_print1" runat="server" OnClick="btn_print_Click" type="button" OnClientClick="PrintGrid('div-gridview')"
                                                            Text="Print" CssClass="btn btn-default btn-md col-md-3" Style="margin-right:5px; font-weight:bold" />
                                            </div>
                                            <div class="col-md-2">
                                            </div>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="TabContainer1" EventName="ActiveTabChanged" />
                            <asp:AsyncPostBackTrigger ControlID="btnOkAction" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </act:TabPanel>
            <act:TabPanel ID="tabPendingBill" runat="server" HeaderText="Open Bills">
                <ContentTemplate>
                    <asp:UpdatePanel ID="upPendingBills" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                                <tbody>
                                    <tr>
                                        <td class="pad5 formbg border">
                                            <asp:HiddenField ID="pendingBillsOpenITem" runat="server" />
                                            <div id="divPendingBills" class="grid" style="width: 100%;">
                                                <div class="rounded">
                                                    <div class="mid-outer">
                                                        <div class="mid-inner">
                                                            <div class="mid" style="height: 280px; overflow: auto">
                                                                <div id="div-pendingBillsGridview" class="GridView whitebg">
                                                                    <asp:GridView ID="gridPendingBills" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                                        BorderColor="White" BorderWidth="1px" CellPadding="0" GridLines="None" CssClass="datatable table-striped table-responsive"
                                                                        ShowHeaderWhenEmpty="True" Width="100%" DataKeyNames="billID,PatientID,HasTransaction"
                                                                        BorderStyle="Solid" OnRowCommand="gridPendingBills_RowCommand" OnRowDataBound="gridPendingBills_RowDataBound"
                                                                        OnRowCreated="gridPendingBills_RowCreated">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="ExpandGridButton" runat="server" CommandName="Expand" ImageUrl="~/Images/plus.png"
                                                                                        CommandArgument="<%# Container.DataItemIndex %>" /></ItemTemplate>
                                                                                <ItemStyle Width="20px" />
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="BillNumber" HeaderText="Invoice #" />
                                                                            <asp:BoundField DataField="BillDate" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="Invoice Date" />
                                                                            <asp:BoundField DataField="BillAmount" HeaderText="Amount" />
                                                                            <asp:BoundField DataField="UnpaidAmount" HeaderText="Amount Oustanding" />
                                                                            <asp:BoundField DataField="CreatedBy" HeaderText="Invoiced By" />
                                                                            <asp:TemplateField HeaderText="Status">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="labelBillStatus" runat="server" Text='<%# Bind("BillStatus") %>'></asp:Label></ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField InsertVisible="False" ShowHeader="False">
                                                                                <ItemTemplate>
                                                                                    <div style="white-space: nowrap">
                                                                                        <span style='display: <%# ShowPay(Eval("BillStatus"),Eval("HasTransaction")) %>;
                                                                                            white-space: nowrap'>
                                                                                            <asp:Button ID="buttonPayRedirect" runat="server" CausesValidation="false" CommandName="PayBill"
                                                                                                Text="Pay Bill" CommandArgument="<%# Container.DataItemIndex %>" Visible="false"
                                                                                                ForeColor="Blue" />
                                                                                            <asp:HiddenField runat="server" ID="HPayMode" />
                                                                                        </span><span style='display: <%# ShowCancel(Eval("BillStatus"),Eval("HasTransaction")) %>;
                                                                                            white-space: nowrap'>
                                                                                            <asp:Button ID="buttonCancel" runat="server" CausesValidation="false" CommandName="CancelBill"
                                                                                                Text="Cancel Bill" CommandArgument="<%# Container.DataItemIndex %>" ForeColor="Blue">
                                                                                            </asp:Button></span>
                                                                                        <!-- Cancel Bill -->
                                                                                        <act:ConfirmButtonExtender ID="cbeBillCancel" runat="server" DisplayModalPopupID="mpeBillCancel"
                                                                                            TargetControlID="buttonCancel">
                                                                                        </act:ConfirmButtonExtender>
                                                                                        <act:ModalPopupExtender ID="mpeBillCancel" runat="server" PopupControlID="pnlPopup"
                                                                                            TargetControlID="buttonCancel" OkControlID="btnYes" CancelControlID="btnNo" BackgroundCssClass="modalBackground">
                                                                                        </act:ModalPopupExtender>
                                                                                        <asp:Panel ID="pnlPopup" runat="server" Style="display: none; background-color: #FFFFFF;
                                                                                            width: 300px; border: 3px solid #0DA9D0;">
                                                                                            <div style="background-color: #2FBDF1; height: 30px; color: White; line-height: 30px;
                                                                                                text-align: center; font-weight: bold;">
                                                                                                Confirmation
                                                                                            </div>
                                                                                            <div style="min-height: 50px; line-height: 30px; text-align: center; font-weight: bold;">
                                                                                                This action cannot be reversed.<br />
                                                                                                Are you sure you want to Cancel this bill?
                                                                                            </div>
                                                                                            <div style="padding: 3px;" style="text-align: right">
                                                                                                <asp:Button ID="btnYes" runat="server" Text="Yes" ForeColor="DarkGreen" /><asp:Button
                                                                                                    ID="btnNo" runat="server" Text="No" ForeColor="DarkBlue" Style="margin-left: 10px" /></div>
                                                                                        </asp:Panel>
                                                                                        <span style='display: <%# ShowWriteOff(Eval("BillStatus"),Eval("HasTransaction")) %>;
                                                                                            white-space: nowrap'>
                                                                                            <asp:Button ID="buttonWriteOff" runat="server" CausesValidation="false" CommandName="WriteOffBill"
                                                                                                Text="Write Off Bill" CommandArgument="<%# Container.DataItemIndex %>" ForeColor="Blue">
                                                                                            </asp:Button></span>
                                                                                        <!-- Write off Bill -->
                                                                                        <act:ConfirmButtonExtender ID="cbeWriteOff" runat="server" DisplayModalPopupID="mpeWriteOff"
                                                                                            TargetControlID="buttonWriteOff">
                                                                                        </act:ConfirmButtonExtender>
                                                                                        <act:ModalPopupExtender ID="mpeWriteOff" runat="server" PopupControlID="panelWriteOff"
                                                                                            TargetControlID="buttonWriteOff" OkControlID="WriteOffYes" CancelControlID="WriteOffCancel"
                                                                                            BackgroundCssClass="modalBackground">
                                                                                        </act:ModalPopupExtender>
                                                                                        <asp:Panel ID="panelWriteOff" runat="server" Style="display: none; background-color: #FFFFFF;
                                                                                            width: 300px; border: 3px solid #0DA9D0;">
                                                                                            <div style="background-color: #2FBDF1; height: 30px; color: White; line-height: 30px;
                                                                                                text-align: center; font-weight: bold;">
                                                                                                Confirmation
                                                                                            </div>
                                                                                            <div style="min-height: 50px; line-height: 30px; text-align: center; font-weight: bold;
                                                                                                white-space: normal">
                                                                                                Are you sure you want to Write Off?<br />
                                                                                                The outstanding amount is
                                                                                                <%# Eval("UnpaidAmount") %>
                                                                                            </div>
                                                                                            <div style="padding: 3px;" style="text-align: right">
                                                                                                <asp:Button ID="WriteOffYes" runat="server" Text="Yes" ForeColor="DarkGreen" /><asp:Button
                                                                                                    ID="WriteOffCancel" runat="server" Text="No" ForeColor="DarkBlue" Style="margin-left: 10px" /></div>
                                                                                        </asp:Panel>
                                                                                        <asp:Button ID="buttonInvoice" runat="server" CausesValidation="false" CommandName="PrintInvoice"
                                                                                            Text="Print Invoice" CommandArgument="<%# Container.DataItemIndex %>" ForeColor="Blue">
                                                                                        </asp:Button>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:HiddenField ID="HdnTransaction" runat="server" Value='<%# Bind("HasTransaction") %>' />
                                                                                    </td></tr>
                                                                                    <tr>
                                                                                        <td colspan="100%">
                                                                                            <asp:Panel ID="ContainerDiv" runat="server" Style="display: none; position: relative;
                                                                                                left: 5px;">
                                                                                                <asp:GridView ID="gridBillItemList" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                                                                    BorderColor="White" BorderWidth="1px" CellPadding="0" CssClass="datatable table-striped table-responsive"
                                                                                                    DataKeyNames="billItemID,BillID,PatientID" Enabled="true" EnableModelValidation="True"
                                                                                                    GridLines="None" HorizontalAlign="Left" ShowFooter="True" ShowHeaderWhenEmpty="True"
                                                                                                    Width="100%">
                                                                                                    <Columns>
                                                                                                        <asp:BoundField DataField="BillItemDate" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="Date" />
                                                                                                        <asp:BoundField DataField="ItemName" HeaderText="Item Description" />
                                                                                                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                                                                                        <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                                                                                        <asp:TemplateField InsertVisible="False" ShowHeader="False" Visible="false">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Button ID="buttonRemove" runat="server" CausesValidation="false" CommandName="RemoveItem"
                                                                                                                    Text="Remove" CommandArgument="<%# Container.DataItemIndex %>" ForeColor="Blue">
                                                                                                                </asp:Button></ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                    </Columns>
                                                                                                    <HeaderStyle ForeColor="#3399FF" HorizontalAlign="Left" />
                                                                                                    <RowStyle CssClass="gridrow" />
                                                                                                </asp:GridView>
                                                                                            </asp:Panel>
                                                                                        </td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <RowStyle CssClass="gridrow" />
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="bottom-outer">
                                                        <div class="bottom-inner">
                                                            <div class="bottom">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--<tr>
                                        <td class="form pad5 center">
                                            <br />
                                            <asp:Button ID="Button1" runat="server" OnClick="btn_close_Click" Text="Close" />
                                        </td>
                                    </tr>--%>
                                </tbody>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="TabContainer1" EventName="ActiveTabChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </act:TabPanel>
            <act:TabPanel ID="tabPaidBills" runat="server" HeaderText="Closed Bills">
                <ContentTemplate>
                    <asp:UpdatePanel ID="upClearedBills" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                                <tbody>
                                    <tr>
                                        <td class="pad5 formbg border">
                                            <asp:HiddenField ID="OpenedDivsHiddenField" runat="server" />
                                            <div id="paidBills" class="grid" style="width: 100%;">
                                                <div class="rounded">
                                                    <div class="mid-outer">
                                                        <div class="mid-inner">
                                                            <div class="mid" style="height: 280px; overflow: auto">
                                                                <div id="div-receiptgridview" class="GridView whitebg">
                                                                    <asp:GridView ID="gridClosedBills" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                                        BorderColor="White" BorderWidth="1px" CellPadding="0" GridLines="None" CssClass="datatable table-striped table-responsive"
                                                                        ShowHeaderWhenEmpty="True" Width="100%" DataKeyNames="billID,PatientID" OnRowCommand="gridClosedBills_RowCommand"
                                                                        OnRowDataBound="gridClosedBills_RowDataBound" BorderStyle="Solid" OnRowCreated="gridClosedBills_RowCreated">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="ExpandGridButton" runat="server" CommandName="Expand" ImageUrl="~/Images/plus.png"
                                                                                        CommandArgument="<%# Container.DataItemIndex %>" /></ItemTemplate>
                                                                                <ItemStyle Width="20px" />
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="BillNumber" HeaderText="Invoice #" />
                                                                            <asp:BoundField DataField="BillDate" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="Invoice Date" />
                                                                            <asp:BoundField DataField="BillAmount" HeaderText="Full Amount" />
                                                                            <asp:BoundField DataField="SettledAmount" HeaderText="PaidUp Amount" />
                                                                            <asp:BoundField DataField="CreatedBy" HeaderText="Invoiced By" />
                                                                            <asp:TemplateField HeaderText="Status">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="labelBillStatus" runat="server" Text='<%# Bind("BillStatus") %>'></asp:Label></ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField InsertVisible="False" ShowHeader="False">
                                                                                <ItemTemplate>
                                                                                    <asp:Button ID="invoicePrint" runat="server" CausesValidation="false" CommandName="PrintInvoice"
                                                                                        Text="Print Invoice" CommandArgument="<%# Container.DataItemIndex %>" ForeColor="Blue" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    </td></tr><tr>
                                                                                        <td colspan="100%">
                                                                                            <asp:Panel ID="ContainerDiv" runat="server" Style="display: none; position: relative;
                                                                                                left: 5px;">
                                                                                                <asp:GridView ID="gridBillTransaction" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                                                                    BorderColor="White" BorderWidth="1px" CellPadding="0" CssClass="datatable table-striped table-responsive"
                                                                                                    DataKeyNames="TransactionID,PatientID" Enabled="true" EnableModelValidation="True"
                                                                                                    GridLines="None" HorizontalAlign="Left" ShowFooter="True" ShowHeaderWhenEmpty="True"
                                                                                                    Width="100%" Caption="Payment Transactions">
                                                                                                    <Columns>
                                                                                                        <asp:BoundField DataField="ReceiptNumber" HeaderText="Reference #" />
                                                                                                        <asp:BoundField DataField="TransactionDate" DataFormatString="{0:dd-MMM-yyyy hh:mm:ss}"
                                                                                                            HeaderText="Transaction Date" />
                                                                                                        <asp:BoundField DataField="TotalAmount" HeaderText="Amount" />
                                                                                                        <asp:BoundField DataField="TransactionTypeName" HeaderText="Transaction Type" />
                                                                                                        <asp:TemplateField HeaderText="Transaction Status">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="labelTransactionStatus" runat="server" Text='<%# Bind("TransactionStatus") %>'></asp:Label></ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField InsertVisible="False" ShowHeader="False">
                                                                                                            <ItemTemplate>
                                                                                                                <div style='text-align: center; padding: 10px; white-space: nowrap; display: <%# IsReversible(Eval("TransactionStatus"),Eval("Reversible")) %>'>
                                                                                                                    <asp:Button ID="buttonReverse" runat="server" CausesValidation="false" CommandName="Reverse"
                                                                                                                        Text="Reverse" ToolTip="Request for reversal" CommandArgument="<%# Container.DataItemIndex %>"
                                                                                                                        ForeColor="Blue" />
                                                                                                                    <asp:Button ID="receiptPrint" runat="server" CausesValidation="false" CommandName="PrintReceipt"
                                                                                                                        Text="Print Receipt" CommandArgument="<%# Container.DataItemIndex %>" ForeColor="Blue" />
                                                                                                                </div>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                    </Columns>
                                                                                                    <HeaderStyle ForeColor="#3399FF" HorizontalAlign="Left" />
                                                                                                    <RowStyle CssClass="gridrow" />
                                                                                                </asp:GridView>
                                                                                            </asp:Panel>
                                                                                        </td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <RowStyle CssClass="gridrow" />
                                                                    </asp:GridView>
                                                                </div>
                                                                <asp:Button ID="btnRaiseReversal" runat="server" Style="display: none" /><act:ModalPopupExtender
                                                                    ID="mpeReverse" runat="server" PopupControlID="panelReversalPopup" TargetControlID="btnRaiseReversal"
                                                                    CancelControlID="buttonCancelReversal" BackgroundCssClass="modalBackground" PopupDragHandleControlID="divTitle">
                                                                </act:ModalPopupExtender>
                                                                <asp:Panel ID="panelReversalPopup" runat="server" Style="display: none; border: solid 1px #808080;
                                                                    background-color: #6699FF; width: 500px;">
                                                                    <asp:Panel ID="divTitle" runat="server" Style="border: solid 1px #808080; margin: 0px 0px 0px 0px;
                                                                        cursor: move; height: 18px; width: 500px;">
                                                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 300px; height: 18px">
                                                                            <tr>
                                                                                <td style="width: 5px; height: 19px;">
                                                                                </td>
                                                                                <td style="width: 100%; height: 19px;">
                                                                                    <span style="font-weight: bold;">
                                                                                        <asp:Label ID="labelParamTitle" runat="server">Request For Reversal </asp:Label><asp:Label
                                                                                            ID="labelReceipt" runat="server" /></span>
                                                                                </td>
                                                                                <td style="width: 5px; height: 19px;">
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                    <%-- <div style="background-color: #2FBDF1; height: 30px; color: White; line-height: 30px;
                                                                        text-align: center; font-weight: bold;">
                                                                        Request For Reversal
                                                                        </div>--%>
                                                                    <div style="min-height: 50px; line-height: 30px; text-align: center; font-weight: bold;">
                                                                        <table cellpadding="15" cellspacing="0" style="width: 500px; height: 18px; background-color: #CCFFFF">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td colspan="2" align="left">
                                                                                        <i>All of the fields in this section are required.</i>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td valign="middle" align="left" style="white-space: nowrap">
                                                                                        Reason for reversal?
                                                                                        <%--<br />--%>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox runat="server" ID="textReason" Width="286px" TextMode="MultiLine" /><asp:HiddenField
                                                                                            ID="HTransactionID" runat="server" />
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                    <div style="padding: 6px;" style="text-align: center">
                                                                        <asp:Button ID="buttonRequestReversal" runat="server" Text="Send Request" ForeColor="DarkGreen"
                                                                            OnClick="RequestReversal" />
                                                                        <asp:Button ID="buttonCancelReversal" runat="server" Text="Cancel" ForeColor="DarkBlue" /></div>
                                                                </asp:Panel>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="bottom-outer">
                                                        <div class="bottom-inner">
                                                            <div class="bottom">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="TabContainer1" EventName="ActiveTabChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </act:TabPanel>
            <act:TabPanel ID="tabReversals" runat="server" HeaderText="Reversals">
                <ContentTemplate>
                    <asp:UpdatePanel ID="divReversalComponent" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <uc1:TransactionReversal ID="ReverseTransaction" runat="server" PrintReceiptJSMethod="openReceiptPage"
                                PrintReceiptURL="./frmBilling_Reciept.aspx" CanRefund="True" IsApprovalMode="False" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="TabContainer1" EventName="ActiveTabChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </act:TabPanel>
            <act:TabPanel ID="tabDeposits" runat="server" HeaderText="Cash Deposits">
                <ContentTemplate>
                    <asp:UpdatePanel ID="divDepositComponent" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <pd:PatientDeposit ID="PDControl" runat="server" CanRefund="True" PrintReceiptJSMethod="openReceiptPage"
                                PrintReceiptURL="./frmBilling_Reciept.aspx" />
                            <asp:Button ID="buttonHidden" runat="server" Width="80px" Style="border: solid 1px #808080;
                                display: none" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="TabContainer1" EventName="ActiveTabChanged" />
                            <asp:AsyncPostBackTrigger ControlID="buttonHidden" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="PDControl" EventName="ErrorOccurred" />
                        </Triggers>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </act:TabPanel>
        </act:TabContainer>
        <%--<asp:UpdatePanel ID="notificationPanel" runat="server">
            <ContentTemplate>
                <asp:Panel ID="panelNotify" runat="server" Style="display: none; width: 460px; border: solid 1px #808080;
                    background-color: #E0E0E0; z-index: 15000">
                    <asp:Panel ID="panelPopup_Title" runat="server" Style="border: solid 1px #808080;
                        margin: 0px 0px 0px 0px; cursor: move; height: 18px">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 18px">
                            <tr>
                                <td style="width: 5px; height: 19px;">
                                </td>
                                <td style="width: 100%; height: 19px;">
                                    <span style="font-weight: bold; color: White">
                                        <asp:Label ID="lblNotice" runat="server">Add Editing Item</asp:Label></span>
                                </td>
                                <td style="width: 5px; height: 19px;">
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <table border="0" cellpadding="15" cellspacing="0" style="width: 100%;">
                        <tr>
                            <td style="width: 48px" valign="middle" align="center">
                                <asp:Image ID="imgNotice" runat="server" ImageUrl="~/Images/mb_information.gif" Height="32px"
                                    Width="32px" />
                            </td>
                            <td style="width: 100%;" valign="middle" align="center">
                                <asp:Label ID="lblNoticeInfo" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <div style="background-color: #FFFFFF; border-top: solid 1px #808080; width: 100%;
                        text-align: center; padding-top: 5px; padding-bottom: 5px">
                        <asp:Button ID="btnOkAction" runat="server" Text="OK" Width="80px" Style="border: solid 1px #808080;" />
                    </div>
                </asp:Panel>
                <asp:Button ID="btn" runat="server" Style="display: none" />
                <act:ModalPopupExtender ID="notifyPopupExtender" runat="server" TargetControlID="btn"
                    PopupControlID="panelNotify" BackgroundCssClass="modalBackground" DropShadow="True"
                    PopupDragHandleControlID="panelPopup_Title" Enabled="True" DynamicServicePath="">
                </act:ModalPopupExtender>
            </ContentTemplate>
            <Triggers>
            </Triggers>
        </asp:UpdatePanel>--%>
        <div style="text-align: center; padding: 10px; white-space: nowrap; border: solid 1px #808080;"
            class="form pad5 center">
            <asp:Button ID="Button2" runat="server" OnClick="btn_close_Click" Text="Close" />
        </div>
        <%--<asp:UpdateProgress ID="sProgress" runat="server" DisplayAfter="5">
            <ProgressTemplate>
                <div style="width: 100%; height: 100%; position: fixed; top: 0px; left: 0px; vertical-align: middle;">
                    <table style="position: relative; top: 45%; left: 45%; border: solid 1px #808080;
                        background-color: #FFFFC0; width: 110px; height: 24px;" cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="right" valign="middle" style="width: 30px; height: 22px;">
                                <img src="../Images/loading.gif" height="16px" width="16px" alt="" />
                            </td>
                            <td align="left" valign="middle" style="font-weight: bold; color: #808080; width: 80px;
                                height: 22px; padding-left: 5px">
                                Processing....
                            </td>
                        </tr>
                    </table>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>--%>
    </div>
</asp:Content>
