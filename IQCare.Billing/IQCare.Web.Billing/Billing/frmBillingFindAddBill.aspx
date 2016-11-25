<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Module.master" AutoEventWireup="true"
    CodeBehind="frmBillingFindAddBill.aspx.cs" Inherits="IQCare.Web.Billing.FindAddBill"
    EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/MasterPage/Module.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <script src="../Incl/quicksearch.js" type="text/javascript" defer="defer"></script>
    <script type="text/javascript">
        function FilterGrid() {
            $('.search_textbox').each(function (i) {
                $(this).quicksearch("[id*=grdPatienBill] tr:not(:has(th))", {
                    'testQuery': function (query, txt, row) {
                        return $(row).children(":eq(" + i + ")").text().toLowerCase().indexOf(query[0].toLowerCase()) != -1;
                    }
                });
            })
        };
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_pageLoaded(FilterGrid);
    </script>
    <br />
    <div>
        <h2 class="forms" style="text-align: left">
            Patient Billing</h2>
        <div style="text-align: right">
            <asp:LinkButton runat="server" ID="lnkSummaryReport" OnClick="lnkSummaryReport_Click">Cashier&#39;s transactions summary...</asp:LinkButton>
        </div>
        <table cellspacing="6" cellpadding="0" width="100%" border="0">
            <tbody>
                <tr>
                    <td class="form">
                        <table width="100%">
                            <tr>
                                <td align="left">
                                    <label style="padding-left: 10px" id="lblpurpose" runat="server">
                                        Search for:</label>
                                    <asp:RadioButtonList ID="rbtlst_findBill" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
                                        OnSelectedIndexChanged="rbtlst_findBill_SelectedIndexChanged">
                                        <asp:ListItem id="rbt_openbills" Text="Open Bills" Selected="True"></asp:ListItem>
                                        <asp:ListItem id="rbt_patients" Text="Patient"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td align="right" style="display: none">
                                    <asp:CheckBox ID="ckbTodaysBills" runat="server" Text="Today's bills only" AutoPostBack="True"
                                        Checked="True" OnCheckedChanged="ckbTodaysBills_CheckedChanged"></asp:CheckBox>
                                </td>
                            </tr>
                            <asp:Panel ID="divsearch" runat="server">
                                <tr>
                                    <td colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%" align="right">
                                        <label class="" id="Label1" runat="server">
                                            Search Criteria:</label>
                                        <asp:DropDownList ID="ddlSearchCriteria" runat="server" Width="110px" Style="z-index: 2">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 20%" align="left">
                                        <label class="">
                                            Value:</label>
                                        <asp:TextBox ID="txtSearchValue" MaxLength="50" runat="server" Enabled="False"></asp:TextBox>
                                        <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click"></asp:Button>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="center">
                                        <label class="" id="label2" runat="server">
                                            Search Filter</label>
                                        <asp:TextBox ID="txtSearchQuery" runat="server" Enabled="False" Style="width: 99%"
                                            TextMode="MultiLine" Columns="1" Rows="4"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="center">
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                                            CssClass="btn btn-info fa fa-user col-md-3"></asp:Button>
                                        <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" CssClass="btn btn-danger col-md-3">
                                        </asp:Button>
                                    </td>
                                </tr>
                            </asp:Panel>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="pad5 formbg border" colspan="2">
                        <div id="divbtnPriorART" class="whitebg" style="text-align: center">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="pad5 formbg border" colspan="2">
                        <asp:UpdatePanel ID="pendingPanel" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="grid" id="divBills" style="width: 100%;">
                                    <div class="rounded">
                                        <div class="mid-outer">
                                            <div class="mid-inner">
                                                <div class="mid" style="height: 200px; overflow: auto">
                                                    <div id="div-gridview" class="GridView whitebg">
                                                        <asp:GridView ID="grdPatienBill" runat="server" AutoGenerateColumns="False" AllowSorting="true"
                                                            Width="100%" BorderColor="white" PageIndex="1" BorderWidth="1" GridLines="None"
                                                            CssClass="datatable table-striped table-responsive" CellPadding="0" CellSpacing="0"
                                                            OnSelectedIndexChanged="grdPatienBill_SelectedIndexChanged" AutoGenerateSelectButton="False"
                                                            DataKeyNames="PatientID" OnRowDataBound="grdPatienBill_RowDataBound" OnDataBound="grdPatienBill_DataBound">
                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                            <RowStyle CssClass="gridrow" />
                                                            <Columns>
                                                                <asp:BoundField HeaderText="Patient ID" DataField="ID" />
                                                                <asp:BoundField HeaderText="Last Name" DataField="LastName" />
                                                                <asp:BoundField HeaderText="First Name" DataField="FirstName" />
                                                                <asp:BoundField HeaderText="DOB" DataField="DOB" DataFormatString="{0:dd-MMM-yyyy}" />
                                                                <asp:BoundField HeaderText="Bill Date" DataField="BillDate" DataFormatString="{0:dd-MMM-yyyy}" />
                                                                <asp:BoundField HeaderText="Bill Amount" DataField="BillAmount" DataFormatString="{0:N}" />
                                                                <asp:BoundField HeaderText="Outstanding Amount" DataField="OutStandingAmount" DataFormatString="{0:N}" />
                                                            </Columns>
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
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ckbTodaysBills" EventName="CheckedChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <asp:UpdatePanel ID="SummaryPanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Button ID="btnShowSummary" runat="server" Text="" Width="60px" Style="display: none" />
            <asp:Panel ID="divTransactions" runat="server" Style="display: none; width: 680px;
                border: solid 1px #808080; background-color: #F0F0F0; height: 380px; overflow: auto">
                <asp:Panel ID="divItemTitle" runat="server" Style="border: solid 1px #808080; margin: 0px 0px 0px 0px;
                    cursor: move; height: 18px">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 18px">
                        <tr>
                            <td style="width: 5px; height: 19px;">
                            </td>
                            <td style="width: 100%; height: 19px;">
                                <h2 class="forms" style="text-align: left">
                                    <asp:Label ID="labelItemTitle" runat="server">Transactions Summary</asp:Label></h2>
                            </td>
                            <td style="width: 5px; height: 19px;">
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <table cellspacing="6" cellpadding="0" width="680px" border="0">
                    <tbody>
                        <tr>
                            <td class="pad5 formbg border">
                                <div id="divDeposits" class="grid" style="width: 100%; margin-top: 8px;">
                                    <div class="rounded">
                                        <div class="mid-outer">
                                            <div class="mid-inner">
                                                <div class="mid" style="height: 280px; overflow: auto">
                                                    <table cellspacing="6" cellpadding="0" width="100%" border="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <div id="div-depositGridview" class="GridView whitebg">
                                                                        <asp:GridView ID="gridTransaction" runat="server" AllowSorting="False" AutoGenerateColumns="False"
                                                                            BorderColor="White" BorderWidth="1px" CellPadding="0" CssClass="datatable table-striped table-responsive"
                                                                            Enabled="true" EnableModelValidation="True" GridLines="None" HorizontalAlign="Left"
                                                                            ShowFooter="True" ShowHeaderWhenEmpty="True" Width="100%" BorderStyle="Solid">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="PaymentName" HeaderText="Transaction Type" />
                                                                                <asp:BoundField DataField="Total" HeaderText="Total" />
                                                                            </Columns>
                                                                            <HeaderStyle ForeColor="#3399FF" HorizontalAlign="Left" />
                                                                            <RowStyle CssClass="gridrow" />
                                                                        </asp:GridView>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td>
                                                                                <div id="divTTransactions" style="text-align: left; white-space: nowrap; vertical-align: baseline">
                                                                                    <asp:Label ID="lblTotalTransactionsCaption" runat="server" Height="30px" Style="white-space: nowrap;
                                                                                        vertical-align: middle; font-weight: bold">Number of Transactions:</asp:Label>
                                                                                    <asp:Label ID="lblTotalTransactions" runat="server" Height="30px" Style="white-space: nowrap;
                                                                                        vertical-align: middle; font-weight: bold"></asp:Label>&nbsp;&nbsp;&nbsp;
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <div id="divTcollections" style="text-align: left; white-space: nowrap; vertical-align: baseline">
                                                                                    <asp:Label ID="lblTotalCollectionsCaption" runat="server" Height="30px" Style="white-space: nowrap;
                                                                                        vertical-align: middle; font-weight: bold">Total Collections:</asp:Label>
                                                                                    <asp:Label ID="lblTotalCollections" runat="server" Height="30px" Style="white-space: nowrap;
                                                                                        vertical-align: middle; font-weight: bold"></asp:Label>&nbsp;&nbsp;&nbsp;
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <div id="divRefunds" style="text-align: left; white-space: nowrap; vertical-align: baseline">
                                                                                    <asp:Label ID="lblRefundsCaption" runat="server" Height="30px" Style="white-space: nowrap;
                                                                                        vertical-align: middle; font-weight: bold">Total Refunds:</asp:Label>
                                                                                    <asp:Label ID="lblRefunds" runat="server" Height="30px" Style="white-space: nowrap;
                                                                                        vertical-align: middle; font-weight: bold"></asp:Label>&nbsp;&nbsp;&nbsp;
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <div id="divCash" style="text-align: left; white-space: nowrap; vertical-align: baseline">
                                                                                    <asp:Label ID="lblCashCaption" runat="server" Height="30px" Style="white-space: nowrap;
                                                                                        vertical-align: middle; font-weight: bold">Cash in Hand:</asp:Label>
                                                                                    <asp:Label ID="lblCash" runat="server" Height="30px" Style="white-space: nowrap;
                                                                                        vertical-align: middle; font-weight: bold"></asp:Label>&nbsp;&nbsp;&nbsp;
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                    </table>
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
                <div style="background-color: #FFFFFF; border-top: solid 1px #808080; width: 100%;
                    text-align: center; padding-top: 5px; padding-bottom: 5px">
                    <asp:Button ID="buttonCloseDetails" runat="server" Text="Close" Width="80px" Style="border: solid 1px #808080;" />
                </div>
            </asp:Panel>
            <ajaxToolkit:ModalPopupExtender ID="transactionsSummaryPopup" runat="server" BehaviorID="TsPID"
                TargetControlID="btnShowSummary" PopupControlID="divTransactions" BackgroundCssClass="modalBackground"
                CancelControlID="buttonCloseDetails" DropShadow="true" PopupDragHandleControlID="divItemTitle">
            </ajaxToolkit:ModalPopupExtender>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkSummaryReport" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <div style="text-align: center; padding: 10px; white-space: nowrap; border: solid 1px #808080;"
        class="form pad5 center">
        <asp:Button ID="Button2" runat="server" OnClick="btn_close_Click" Text="Close" />
    </div>
    <%-- <asp:UpdateProgress ID="sProgress" runat="server" DisplayAfter="5">
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
</asp:Content>
