<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Module.master" AutoEventWireup="true"
    CodeBehind="frmBilling_BillingPanel.aspx.cs" Inherits="IQCare.Web.Billing.frmBilling_BillingPanel"
    MaintainScrollPositionOnPostback="true" %>

<%@ MasterType VirtualPath="~/MasterPage/Module.master" %>
<%@ Register Assembly="AjaxControlToolkit" TagPrefix="Ajaxtoolkit" Namespace="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <script language="javascript" type="text/javascript">

        function WindowPrint() {
            window.print();
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
            window.open(path, 'popupwindow', 'toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=yes,resizable=no,width=950,height=650,scrollbars=yes');
        }
        function openReceiptPage(path) {
            window.open(path, 'ReceiptPage', 'toolbars=no,location=no,directories=no,dependent=yes,top=100,left=30,maximize=no,resize=no,width=1000,height=800,scrollbars=yes');
        }
        function ace1_itemSelected(sender, e) {
            var hdCustID = $get('<%= HItemName.ClientID %>');
            hdCustID.value = e.get_value();
            // alert(hdCustID.value);
        }
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>
    <style>
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
        }
        .AutoExtenderList
        {
            cursor: pointer;
            color: black;
            z-index: 2147483647 !important;
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
    </style>
    <div style="padding-top: 18px;">
        <h2 class="forms" style="text-align: left">
            Quick Panel</h2>
    </div>
    <div class="border center formbg">
        <asp:UpdatePanel ID="upError" runat="server">
            <ContentTemplate>
                <asp:Panel ID="divError" runat="server" Style="padding: 5px; background-color: #FFFFC0;
                    border: solid 1px #C00000; margin-bottom: 10px;" HorizontalAlign="Left" Visible="false">
                    <asp:Label ID="lblError" runat="server" Style="font-weight: bold; color: #800000"
                        Text=""></asp:Label>
                    <asp:DropDownList ID="ddlCostCenter" runat="server" Style="z-index: 2" AutoPostBack="false"
                        Visible="false">
                    </asp:DropDownList>
                </asp:Panel>
            </ContentTemplate>
            <Triggers>
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="upPatient" runat="server">
            <ContentTemplate>
                <asp:Panel ID="panelCalender" runat="server" Visible="true" Style="width: 100%; border-top: solid 1px #C0C0C0;
                    margin-top: 5px">
                    <asp:Calendar ID="calendar1" Width="95%" PrevMonthText="<< Previous Month" NextMonthText="Next Month >>"
                        DayHeaderStyle-Font-Name="Verdana" Height="16px" TodayDayStyle-ForeColor="Black"
                        DayStyle-BorderWidth="1" DayStyle-BorderStyle="Solid" OtherMonthDayStyle-ForeColor="#C0C0C0"
                        SelectedDayStyle-ForeColor="#000000" SelectedDayStyle-BackColor="#faebd7" runat="server"
                        CellSpacing="2" CellPadding="2" BorderColor="SteelBlue" BackColor="white" TitleStyle-Font-Size="12"
                        TitleStyle-Font-Name="Verdana" TitleStyle-Font-Bold="False" SelectionMode="Day"
                        DayStyle-Font-Size="12" DayStyle-Font-Name="Arial" DayStyle-VerticalAlign="Top"
                        DayStyle-HorizontalAlign="Left" DayStyle-Width="15" DayStyle-Height="16" Font-Bold="True"
                        OnSelectionChanged="calendar1_SelectionChanged" OnDayRender="calendar1_DayRender"
                        OnVisibleMonthChanged="calendar1_VisibleMonthChanged">
                        <TodayDayStyle ForeColor="Black"></TodayDayStyle>
                        <DayStyle Font-Size="12pt" Font-Names="verdana" HorizontalAlign="Center" Height="20px"
                            BorderWidth="1px" BorderStyle="Solid" Width="75px" VerticalAlign="Middle"></DayStyle>
                        <DayHeaderStyle Font-Names="Verdana" ForeColor="Desktop"></DayHeaderStyle>
                        <SelectedDayStyle ForeColor="#800000" BackColor="#FFFFC0" Font-Bold="True"></SelectedDayStyle>
                        <TitleStyle Font-Size="12pt" Font-Names="Verdana"></TitleStyle>
                        <OtherMonthDayStyle ForeColor="Silver"></OtherMonthDayStyle>
                    </asp:Calendar>
                    <asp:HiddenField ID="HSelectedDate" runat="server" />
                </asp:Panel>
                <br />
                <br />
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="calendar1" EventName="SelectionChanged" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="divComponent" runat="server">
            <ContentTemplate>
                <div id="divConsumables" class="grid" style="width: 100%;">
                    <div class="rounded">
                        <div class="mid-outer">
                            <div class="mid-inner">
                                <div class="mid" style="height: 200px; overflow: auto">
                                    <div id="div-gridview" class="GridView whitebg">
                                        <asp:HiddenField ID="HItemName" runat="server" />
                                        <asp:HiddenField ID="HItemID" runat="server" />
                                        <asp:HiddenField ID="HItemTypeID" runat="server" />
                                        <asp:HiddenField ID="HItemTypeName" runat="server" />
                                        <asp:GridView ID="gridItems" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                            BorderColor="White" BorderWidth="1px" CellPadding="0" CssClass="datatable table-striped table-responsive"
                                            PageIndex="1" ShowFooter="True" ShowHeaderWhenEmpty="True" Width="100%" GridLines="None"
                                            DataKeyNames="BillItemID,ItemIssuanceID,PatientID,IssuedByID,ItemID,ItemTypeID"
                                            EmptyDataText="No items for the selected date" OnRowCommand="gridItems_RowCommand"
                                            OnRowDataBound="gridItems_RowDataBound" OnRowCancelingEdit="gridItems_RowCancelingEdit"
                                            OnRowDeleting="gridItems_RowDeleting" OnRowEditing="gridItems_RowEditing" OnRowUpdating="gridItems_RowUpdating">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Description">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtEditDescription" runat="server" AutoPostBack="true" OnTextChanged="txtEditDescription_textChanged"
                                                            Text='<%# Bind("ItemName") %>' Width="95%" Font-Names="Courier New"></asp:TextBox>
                                                        <div id="divwidth" runat="server">
                                                        </div>
                                                        <Ajaxtoolkit:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" CompletionInterval="30"
                                                            CompletionListCssClass="AutoExtender" CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                                            CompletionListItemCssClass="AutoExtenderList" CompletionSetCount="10" BehaviorID="AutoCompleteEx"
                                                            EnableCaching="false" FirstRowSelected="false" MinimumPrefixLength="2" OnClientItemSelected="ace1_itemSelected"
                                                            ServiceMethod="SearchItems" TargetControlID="txtEditDescription" CompletionListElementID="divwidth">
                                                        </Ajaxtoolkit:AutoCompleteExtender>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtNewDescription" runat="server" AutoPostBack="true" OnTextChanged="txtNewDescription_textChanged"
                                                            Width="95%" Font-Names="Courier New"></asp:TextBox>
                                                        <div id="divwidthfooter" runat="server" style="text-align: left">
                                                        </div>
                                                        <Ajaxtoolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionInterval="30"
                                                            CompletionListCssClass="AutoExtender" CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                                            CompletionListItemCssClass="AutoExtenderList" CompletionSetCount="10" EnableCaching="false"
                                                            FirstRowSelected="false" MinimumPrefixLength="2" OnClientItemSelected="ace1_itemSelected"
                                                            BehaviorID="AutoCompleteFT" ServiceMethod="SearchItems" TargetControlID="txtNewDescription"
                                                            CompletionListElementID="divwidthfooter">
                                                        </Ajaxtoolkit:AutoCompleteExtender>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="labelItemName" runat="server" Text='<%# Bind("ItemName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="35%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Department" Visible="True" HeaderStyle-HorizontalAlign="Left">
                                                    <EditItemTemplate>
                                                        <div style="white-space: nowrap">
                                                            <%-- <span style='display: <%# ShowEdit(Eval("ItemTypeName")) %>; white-space: nowrap'>--%>
                                                            <asp:DropDownList ID="ddlItemCostCenter" runat="server" Width="99%" Visible="false">
                                                            </asp:DropDownList>
                                                            <%--</span><span style='display: <%# HideEdit(Eval("ItemTypeName")) %>; white-space: nowrap'>--%>
                                                            <asp:Label ID="lblCostCenter" runat="server" Text='<%# Bind("CostCenter") %>' Visible="false"></asp:Label><%--</span>--%>
                                                        </div>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <div style="white-space: nowrap">
                                                            <%--  <span style='display: <%# ShowEdit(Eval("ItemTypeName")) %>; white-space: nowrap'>--%>
                                                            <asp:DropDownList ID="ddlItemCostCenter" runat="server" Width="99%" Visible="false">
                                                            </asp:DropDownList>
                                                            <%--  </span><span style='display: <%# HideEdit(Eval("ItemTypeName")) %>; white-space: nowrap'>--%>
                                                            <asp:Label ID="lblCostCenter" runat="server" Text='<%# Bind("CostCenter") %>' Visible="false"></asp:Label>
                                                            <%--  </span>--%>
                                                        </div>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <div style="white-space: nowrap">
                                                            <asp:Label ID="lblCostCenter" runat="server" Text='<%# Bind("CostCenter") %>'></asp:Label></div>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Qty">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtEditQuantity" runat="server" onkeypress="return isNumber(event)"
                                                            Text='<%# Bind("IssuedQuantity") %>' Width="20px" Wrap="False"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtNewQuantity" runat="server" onkeypress="return isNumber(event)"
                                                            Width="20px" OnTextChanged="txtNewQuantity_TextChanged">1</asp:TextBox>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="labelQuantity" runat="server" Text='<%# Bind("IssuedQuantity") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="5%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Price">
                                                    <ItemTemplate>
                                                        <asp:Label ID="labelPrice" runat="server" Text='<%# Bind("SellingPrice") %>' Width="99%"></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblEditUnitPrice" runat="server" Text='<%# Bind("SellingPrice") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblNewUnitPrice" runat="server" Width="90%">0</asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle Width="8%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount">
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblEditAmount" runat="server" Width="99%"></asp:Label></EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="labelAmount" runat="server" Text='<%# Bind("IssuedAmount") %>' Width="99%"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblNewAmountPrice" runat="server" Width="90%"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle Width="10%" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="IssuedByName" HeaderText="Issued By">
                                                    <ItemStyle Width="10%" Wrap="False" />
                                                </asp:BoundField>
                                                <asp:TemplateField ShowHeader="False">
                                                    <EditItemTemplate>
                                                        <asp:Button ID="buttonUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                                            Text="Update" ForeColor="Blue" />&#160;<asp:Button ID="buttonCancelEdit" runat="server"
                                                                CausesValidation="False" CommandName="Cancel" Text="Cancel" ForeColor="Blue" /></EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Button ID="btnNewAdd" runat="server" CommandName="AddItem" Text="Add" ForeColor="Blue" /></FooterTemplate>
                                                    <ItemTemplate>
                                                        <div style="white-space: nowrap">
                                                            <asp:Button ID="buttonEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                                                Text="Edit" ForeColor="Blue" Visible="false" />&#160;<asp:Button ID="buttonDelete"
                                                                    runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" ForeColor="Blue" /></div>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
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
                        <div class="form pad5 center" style="text-align: center">
                            <br />
                            <asp:Button ID="btnSaveItems" runat="server" Text="Save" OnClick="btnSaveItems_Click" />&nbsp;&nbsp;<asp:Button
                                ID="buttonClose" runat="server" Text="Close" OnClick="buttonClose_Click" />
                        </div>
                        <Ajaxtoolkit:ConfirmButtonExtender ID="cbeWriteOff" runat="server" DisplayModalPopupID="mpeSave"
                            TargetControlID="btnSaveItems">
                        </Ajaxtoolkit:ConfirmButtonExtender>
                        <Ajaxtoolkit:ModalPopupExtender ID="mpeSave" runat="server" PopupControlID="panelSave"
                            TargetControlID="btnSaveItems" OkControlID="SaveYes" CancelControlID="SaveCancel"
                            BackgroundCssClass="modalBackground">
                        </Ajaxtoolkit:ModalPopupExtender>
                        <asp:Panel ID="panelSave" runat="server" Style="display: none; background-color: #FFFFFF;
                            width: 300px; border: 3px solid #0DA9D0;">
                            <div style="background-color: #2FBDF1; height: 30px; color: White; line-height: 30px;
                                text-align: center; font-weight: bold;">
                                Confirmation
                            </div>
                            <div style="min-height: 50px; line-height: 30px; text-align: center; font-weight: bold;
                                white-space: normal">
                                Are you sure you that the patient has received all these items?
                            </div>
                            <div style="padding: 3px;" align="right">
                                <asp:Button ID="SaveYes" runat="server" Text="Yes" ForeColor="DarkGreen" /><asp:Button
                                    ID="SaveCancel" runat="server" Text="No" ForeColor="DarkBlue" Style="margin-left: 10px" /></div>
                        </asp:Panel>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="calendar1" EventName="SelectionChanged" />
                <asp:AsyncPostBackTrigger ControlID="gridItems" EventName="RowCommand" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="sProgress" runat="server" DisplayAfter="5">
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
        </asp:UpdateProgress>
    </div>
</asp:Content>
