<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Module.master" AutoEventWireup="true"
    CodeBehind="BillKnockOff.aspx.cs" Inherits="IQCare.Web.Billing.BillKnockOff" %>

<%@ MasterType VirtualPath="~/MasterPage/Module.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="ctMain" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <%--<script language="javascript" type="text/javascript">
        var allCheckBoxSelector = '#<%=gridKO.ClientID%> input[id*="chkBxHeader"]:checkbox';
        var checkBoxSelector = '#<%=gridKO.ClientID%> input[id*="chkBxItem"]:checkbox:not(:disabled)';
        function ToggleCheckUncheckAllOptionAsNeeded() {
            var totalCheckboxes = $(checkBoxSelector),
            checkedCheckboxes = totalCheckboxes.filter(":checked");
           
            noCheckboxesAreChecked = (checkedCheckboxes.length === 0),
            allCheckboxesAreChecked = (totalCheckboxes.length === checkedCheckboxes.length);
           
            $(allCheckBoxSelector).attr('checked', allCheckboxesAreChecked);
            if (noCheckboxesAreChecked) {               
                $("#<%= btnKnockOff.ClientID %>").prop("disabled", "disabled");   
            }
            else {
                $("#<%= btnKnockOff.ClientID %>").removeProp("disabled");                    
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
    </script>--%>
    <style type="text/css">
        .ajax__calendar_container
        {
            z-index: 8000;
            width: 180px;
        }
        .RadComboBox .rcbInput
        {
            margin: 0;
        }
    </style>
    <div class="container-fluid">
        <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" Width="100%" ActiveTabIndex="1"
            ondemand="true" AutoPostBack="true" OnActiveTabChanged="TabContainer1_ActiveTabChanged">
            <ajaxToolkit:TabPanel ID="tabPay" runat="server" HeaderText="Capture Debt Payment">
                <ContentTemplate>
                    <asp:UpdatePanel ID="upDB" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-12 label label-primary">
                                    <div class="col-md-3 pull-left" style="text-align: left">
                                        <span class="bold h5">DEBT PAYMENT DETAILS</span>
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="margin-top: 10px">
                                <div class="col-md-6" id="divForm" data-parsley-validate="off">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label class="control-label pull-left" for="txtuname">
                                                Voucher Date:</label></div>
                                        <div class="form-group  col-md-3" style="white-space: nowrap; position: relative">
                                            <asp:TextBox ID="txtDate" Name="txtDate" CssClass="form-control input-sm col-md-3"
                                                runat="server" placeholder="date of payment" required="true" AutoComplete="Off"
                                                data-parsley-group="PV"></asp:TextBox>
                                            <asp:ImageButton runat="Server" ID="Image1" Height="22" Style="width: 22; height: 22;
                                                z-index: auto; padding-left: 5px" ImageUrl="~/Images/cal_icon.gif" ImageAlign=" Bottom"
                                                AlternateText="Click to show calendar" />
                                            <ajaxToolkit:CalendarExtender ID="calendarButtonExtender" runat="server" TargetControlID="txtDate"
                                                PopupButtonID="Image1" EnabledOnClient="True" Format="dd-MMM-yyyy" />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDate"
                                                ErrorMessage="*" Display="None" ValidationExpression="^(0?[1-9]|[12][0-9]|3[01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$"></asp:RegularExpressionValidator><br />
                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtDate"
                                                Enabled="True" UserDateFormat="DayMonthYear" CultureDateFormat="dd-MMM-yyyy"
                                                ClearMaskOnLostFocus="False" CultureName="en-GB" Mask="99-LLL-9999">
                                            </ajaxToolkit:MaskedEditExtender>
                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label class="control-label pull-left" for="txtAmount">
                                                Amount:</label>
                                        </div>
                                        <div class="form-group col-md-3">
                                            <asp:TextBox ID="txtAmount" CssClass="form-control input-sm col-md-3" Name="txtAmount"
                                                placeholder="amount of payment" runat="server" AutoComplete="Off" MaxLength="13"
                                                required="true" data-parsley-type="digits" data-parsley-group="PV"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="fteAmountToPay" runat="server" TargetControlID="txtAmount"
                                                FilterType="Numbers, Custom" ValidChars="." />
                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label class="control-label pull-left" for="ddlVoucherType">
                                                Voucher Type:</label>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <asp:DropDownList required="true" CssClass="form-control" ID="ddlVoucherType" Name="ddlVoucherType"
                                                runat="server" AutoPostBack="false" data-parsley-group="PV">
                                                <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                                <asp:ListItem Value="Cheque" Text="Cheque"></asp:ListItem>
                                                <asp:ListItem Value="Direct Deposit">Direct Deposit</asp:ListItem>
                                                <asp:ListItem Value="EFT">EFT</asp:ListItem>
                                                <asp:ListItem Value="RTGS">RTGS</asp:ListItem>
                                                <asp:ListItem Value="Mobile Money">Mobile Money</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label class="control-label pull-left" for="txtReference">
                                                Reference Number:</label>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <asp:TextBox ID="txtReference" required="true" CssClass="form-control input-sm col-md-3"
                                                Name="txtReference" placeholder="E.g Cheque Number, Transaction Number" runat="server"
                                                AutoComplete="Off" MaxLength="50" data-parsley-group="PV"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                TargetControlID="txtAmount" FilterType="Numbers, Custom" ValidChars=".-/\_)(" />
                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label class="control-label pull-left" for="txtDetails">
                                                Other Details:</label>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <asp:TextBox ID="txtDetails" CssClass="form-control" Name="txtDetails" placeholder="E.g Other details worth noting"
                                                runat="server" AutoComplete="Off"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                        </div>
                                        <div class="form-group col-md-8">
                                            <asp:Button CssClass="btn btn-info fa fa-user col-md-4" ID="btnSave" runat="server"
                                                Text=" Save" Style="margin-right: 5px" OnClick="SaveVoucher"  UseSubmitBehavior="false" type="button" CausesValidation="false"  />
                                            <asp:Button ID="btnCancel" CssClass="btn btn-danger col-md-4" runat="server" OnClick="CancelSaveVoucher"
                                                Text=" Cancel" CausesValidation="false"  UseSubmitBehavior="false" />
                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="TabContainer1" EventName="ActiveTabChanged" />
                            <asp:AsyncPostBackTrigger ControlID="btnSave" />
                            <asp:AsyncPostBackTrigger ControlID="btnCancel" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="tabKO" runat="server" HeaderText="Knock Off">
                <ContentTemplate>
                    <asp:UpdatePanel ID="upKO" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-12 label label-primary">
                                    <div class="col-md-3 pull-left" style="text-align: left">
                                        <span class="bold h5">KNOCK OFF</span>
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="margin-top: 10px">
                                <div class="col-md-8" id="divKO">
                                    <div class="row">
                                        <div class="col-md-8">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <label class="control-label pull-left" for="ddlKOVoucher">
                                                        Select Voucher:</label></div>
                                                <div class="form-group col-md-6">
                                                    <asp:DropDownList CssClass="form-control input-sm col-md-6" ID="ddlKOVoucher" Name="ddlKOVoucher"
                                                        required="true" runat="server" data-parsley-group="KO" AppendDataBoundItems="false">
                                                    </asp:DropDownList>
                                                    <asp:HiddenField ID="hdVoucher" Value="0" runat="server" />
                                                </div>
                                                <div class="col-md-2">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <label class="control-label pull-left" for="txtFrom">
                                                        Date Range:
                                                    </label>
                                                </div>
                                                <div class="form-group  col-md-3" style="white-space: nowrap; position: relative">
                                                    <asp:TextBox ID="txtKOFrom" Name="txtFrom" CssClass="form-control input-sm col-md-4"
                                                        runat="server" required="true" placeholder="Start Date (From)" AutoComplete="Off"
                                                        data-parsley-group="KO"></asp:TextBox>
                                                    <asp:ImageButton runat="Server" ID="imFrom" Height="22" Style="width: 22; height: 22;
                                                        z-index: auto; padding-left: 5px" ImageUrl="~/Images/cal_icon.gif" ImageAlign=" Bottom"
                                                        AlternateText="Click to show calendar" />
                                                    <ajaxToolkit:CalendarExtender ID="calFrom" runat="server" TargetControlID="txtKOFrom"
                                                        PopupButtonID="imFrom" EnabledOnClient="True" Format="dd-MMM-yyyy" />
                                                </div>
                                                <div class="form-group  col-md-3" style="white-space: nowrap; position: relative">
                                                    <asp:TextBox ID="txtKOTo" Name="txtTo" CssClass="form-control input-sm col-md-4"
                                                        runat="server" required="true" placeholder="End Date (To)" AutoComplete="Off"
                                                        data-parsley-group="KO"></asp:TextBox>
                                                    <asp:ImageButton runat="Server" ID="imTo" Height="22" Style="width: 22; height: 22;
                                                        z-index: auto; padding-left: 5px" ImageUrl="~/Images/cal_icon.gif" ImageAlign=" Bottom"
                                                        AlternateText="Click to show calendar" />
                                                    <ajaxToolkit:CalendarExtender ID="calTo" runat="server" TargetControlID="txtKOTo"
                                                        PopupButtonID="imTo" EnabledOnClient="True" Format="dd-MMM-yyyy" />
                                                </div>
                                                <div class="col-md-2">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <label class="control-label pull-left" for="ddlPTKO">
                                                        Select Payment Type:</label></div>
                                                <div class="form-group col-md-6">
                                                    <asp:DropDownList CssClass="form-control" ID="ddlPTKO" Name="ddlPTKO" required="true"
                                                        data-parsley-group="KO" runat="server" AppendDataBoundItems="false">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group col-md-4" style="text-align: left; vertical-align: middle;
                                            margin-top: 10px">
                                            <asp:Button CssClass="btn btn-primary btn-lg col-md-4" ID="btnKOView" type="button"
                                                runat="server" Text=" Find  " Style="margin-right: 5px; vertical-align: middle"
                                                OnClick="FindKnockOff" UseSubmitBehavior="false" CausesValidation="false" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4" style="text-align: left">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 label label-primary pull-left" style="margin-left: 10px; margin-right: 10px;
                                    text-align: left">
                                    <span class="bold h5">Knock Transactions</span>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div id="divBills" class="grid" style="width: 100%;">
                                        <div class="rounded">
                                            <div class="mid-outer">
                                                <div class="mid-inner">
                                                    <div class="mid" style="height: 200px; overflow: auto">
                                                        <div id="div-gridview" class="GridView whitebg">
                                                            <asp:GridView ID="gridKO" runat="server" PageSize="1" CssClass="datatable table-striped table-responsive"
                                                                AutoGenerateColumns="False" CellPadding="0" BorderWidth="0px" GridLines="None"
                                                                EmptyDataText="No Credit Transctions to knock off against the selected voucher"
                                                                OnRowCommand="gridKO_RowCommand" OnRowDataBound="gridKO_RowDataBound" DataKeyNames="TransactionId,PaymentTypeId"
                                                                ShowFooter="True" ShowHeaderWhenEmpty="True">
                                                                <EmptyDataTemplate>
                                                                    No Credit Transctions to knock off against the selected voucher</EmptyDataTemplate>
                                                                <HeaderStyle CssClass="searchresultfixedheader" Height="20px" VerticalAlign="Middle"
                                                                    HorizontalAlign="Left"></HeaderStyle>
                                                                <RowStyle CssClass="gridrow" Height="20px" VerticalAlign="Middle" />
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            <asp:CheckBox ID="chkBxHeader" runat="server" AutoPostBack="false" /></HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkBxItem" runat="server" AutoPostBack="false"></asp:CheckBox></ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="25px" Wrap="False" />
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="25px" Wrap="False" />
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="patientname" HeaderText="Patient Name" />
                                                                    <asp:BoundField DataField="PatientFacilityId" HeaderText="Facility ID" />
                                                                    <asp:BoundField DataField="TransactionDate" HeaderText="Tran Date" DataFormatString="{0:dd-MMM-yyyy hh:mm:ss}" />
                                                                    <asp:BoundField DataField="ReceiptNumber" HeaderText="Receipt #" />
                                                                    <asp:TemplateField  HeaderText="Total Amount">
                                                                        <ItemTemplate><asp:Label runat="server" ID="labeltranTotal" Text='<%# Bind("Amount") %>'></asp:Label></ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="PendingAmount" HeaderText="Pending Amt" />
                                                                    <asp:TemplateField HeaderText="Amt to Knock Off">
                                                                        <ItemStyle Width="35%" Wrap="false" />
                                                                        <ItemTemplate>
                                                                            <div class="col-md-3" style="text-align: left; width: 80px">
                                                                                <asp:TextBox ID="txtKOAmt" runat="server" onkeypress="return isNumber(event)" MaxLength="9"
                                                                                    Wrap="False" CssClass="input-sm" Width="80px" ValidationGroup="KOGrid"></asp:TextBox>
                                                                                <div />
                                                                                <div class="col-mod-9" style="white-space: nowrap; text-align: left">
                                                                                    <asp:RangeValidator ID="rgKOAmount" runat="server" ControlToValidate="txtKOAmt" Type="Double"
                                                                                        MinimumValue="0" MaximumValue="100" ErrorMessage="The value should be between 0 and  the outstanding amount"
                                                                                        Display="Dynamic" Enabled="True" CssClass="control-label pull-left"  SetFocusOnError="true" ValidationGroup="KOGrid" />
                                                                                </div>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
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
                                        </div>
                                        <div class="row" style="display:none">
                                            <div class="col-md-6">
                                            </div>
                                            <div class="form-group col-md-6">
                                                <label class="control-label pull-left" for="txtTo">
                                                    <asp:Label ID="labeltotal" runat="server" Text="Total:0.00" Font-Bold="True"></asp:Label>
                                                </label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group col-md-6">
                                            </div>
                                            <div class="form-group col-md-6" style="text-align: left">
                                                <asp:Button CssClass="btn btn-info fa fa-user col-md-6" ID="btnKnockOff" runat="server"  CausesValidation="true" UseSubmitBehavior="false"
                                                    Text=" Knock Off Selected Transactions" Style="margin-right: 5px" OnClick="KnockOffTransaction" ValidationGroup="KOGrid" />
                                            </div>
                                        </div>
                                    </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="TabContainer1" EventName="ActiveTabChanged" />
                            <asp:AsyncPostBackTrigger ControlID="btnKOView" />
                            <asp:AsyncPostBackTrigger ControlID="btnKnockOff" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="tabHIST" runat="server" HeaderText="History">
                <ContentTemplate>
                    <asp:UpdatePanel ID="upHIST" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-12 label label-primary">
                                    <div class="col-md-3 pull-left" style="text-align: left">
                                        <span class="bold h5">Knock History</span>
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="margin-top: 10px">
                                <div class="col-md-12" data-parsley-validate="off" id="divHist">
                                    <div class="row">
                                        <div class="col-md-8">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <label class="control-label pull-left" for="txtHistFrom">
                                                        Date Range:</label>
                                                </div>
                                                <div class="form-group  col-md-3" style="white-space: nowrap; position: relative">
                                                    <asp:TextBox ID="txtHistFrom" Name="txtHistFrom" CssClass="form-control input-sm col-md-4"
                                                        runat="server" required="true" placeholder="Start Date (From)" AutoComplete="Off"></asp:TextBox>
                                                    <asp:ImageButton runat="Server" ID="imHistFrom" Height="22" Style="width: 22; height: 22;
                                                        z-index: auto; padding-left: 5px" ImageUrl="~/Images/cal_icon.gif" ImageAlign=" Bottom"
                                                        AlternateText="Click to show calendar" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtHistFrom"
                                                        PopupButtonID="imHistFrom" EnabledOnClient="True" Format="dd-MMM-yyyy" />
                                                </div>
                                                <div class="form-group  col-md-3" style="white-space: nowrap; position: relative">
                                                    <asp:TextBox ID="txtHistTo" Name="txtHistTo" CssClass="form-control input-sm col-md-4"
                                                        runat="server" required="true" placeholder="End Date (To)" AutoComplete="Off"></asp:TextBox>
                                                    <asp:ImageButton runat="Server" ID="ImageButton2" Height="22" Style="width: 22; height: 22;
                                                        z-index: auto; padding-left: 5px" ImageUrl="~/Images/cal_icon.gif" ImageAlign=" Bottom"
                                                        AlternateText="Click to show calendar" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtHistTo"
                                                        PopupButtonID="ImageButton2" EnabledOnClient="True" Format="dd-MMM-yyyy" />
                                                </div>
                                                <div class="col-md-2">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <label class="control-label pull-left" for="ddlHistVTKO">
                                                        Voucher Type:</label></div>
                                                <div class="form-group col-md-6">
                                                    <asp:DropDownList CssClass="form-control" ID="ddlHistVTKO" Name="ddlHistVTKO" required="true"
                                                        runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group col-md-4" style="text-align: left">
                                            <asp:Button CssClass="btn btn-primary btn-lg col-md-4" ID="btnHistView" type="button"
                                                runat="server" Text=" View  " Style="margin-right: 5px; vertical-align: middle"
                                                OnClick="FindHistory" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 label label-primary pull-left" style="margin-left: 10px; margin-right: 10px;
                                    text-align: left">
                                    <span class="bold h5">Vouchers</span>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:GridView ID="gridHistory" runat="server" Width="100%" PageSize="1" CssClass="datatable table-striped table-responsive"
                                        AutoGenerateColumns="False" CellPadding="0" BorderWidth="0px" GridLines="None"
                                        OnRowCommand="gridHistory_RowCommand" OnRowDataBound="gridHistory_RowDataBound">
                                        <HeaderStyle CssClass="searchresultfixedheader" Height="20px" VerticalAlign="Middle"
                                            HorizontalAlign="Left"></HeaderStyle>
                                        <RowStyle Height="30" CssClass="gridrow" />
                                        <Columns>
                                            <asp:BoundField DataField="VoucherNnumber" HeaderText="Voucher #" />
                                            <asp:BoundField DataField="VoucherType" HeaderText="Voucher Type" />
                                            <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                            <asp:BoundField DataField="KnockedOffAmt" HeaderText="Knocked Out Amt" />
                                            <asp:TemplateField ShowHeader="false">
                                                <ItemTemplate>
                                                    <asp:Button ID="buttonDetails" runat="server" CausesValidation="false" CommandName="KODetail"
                                                        Text="Details" CssClass="btn btn-primary" CommandArgument="<%# Container.DataItemIndex %>"
                                                        Visible="false" ForeColor="Blue" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </ContentTemplate>
                         <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="TabContainer1" EventName="ActiveTabChanged" />                              
                        </Triggers>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
        </ajaxToolkit:TabContainer>
    </div>
</asp:Content>
