<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    CodeBehind="ServiceRecordEntry.aspx.cs" Inherits="IQCare.Web.ClinicalService.ServiceRecordEntry" %>

<%@ Register Assembly="AjaxControlToolkit" TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" %>
<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<%@ Register Src="~/ProgressControl.ascx" TagName="progressControl" TagPrefix="uc1" %>
<asp:Content ID="ctMain" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <asp:HiddenField ID="HPatientId" runat="server" Value="-1" />
    <asp:HiddenField ID="HServiceOrderId" runat="server" Value="-1" />
    <asp:HiddenField ID="hdSelectedModule" runat="server" Value="-1" />
    <asp:HiddenField ID="hdSelectedModuleName" runat="server" Value="" />
    <asp:HiddenField ID="HUserId" runat="server" />
    <script language="javascript" type="text/javascript">
        function WindowPrint() {
            window.print();
        }
        function ace1_itemSelected(source, e) {
            var hdCustID = $get('<%= hdCustID.ClientID %>');
            hdCustID.value = e.get_value();

        }

        function pageLoad() {

            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndAJAXRequestHandler);
        }

        function EndAJAXRequestHandler(sender, args) {

            if (args.get_error() != undefined && args.get_error().httpStatusCode == '500') {
                var errorMessage = args.get_error().message;
                args.set_errorHandled(true);
                alert("IQCare Application Framework encountered an unrecoverable error:\n" + errorMessage + "\n\nPlease report this error to the support team.");
            }

        }

        $(function () {
            var mySelect = $('#<%=ddlDepartment.ClientID %>');
            mySelect.change(function () {
                var val = $(this).val();
                var text = $('#<%=ddlDepartment.ClientID %> option:selected').text();
                var dtype = parseInt(val)
                if (dtype > 0) {
                    $("#divSelectService").css('display', '');
                    $("#divSelectService").show();
                    $('#<%=hdSelectedModule.ClientID %>').val(val);
                    $('#<%=hdSelectedModuleName.ClientID %>').val(text);

                    $find('AutoCompleteEx').set_contextKey(val);

                } else {
                    $("#divSelectService").hide();
                    $("#divSelectService").css('display', 'none');
                    $('#<%=hdSelectedModule.ClientID %>').val("-1");
                }
            });

        }
        );


    </script>
    <style type="text/css">
        .autoextender
        {
            font-family: Courier New, Arial, sans-serif;
            font-size: 15px;
            font-weight: 100;
            border: solid 1px #006699;
            line-height: 20px;
            padding: 0px;
            background-color: White;
            margin-left: 0px;
            width: 400px;
        }
        .autoextenderlist
        {
            cursor: pointer;
            color: black;
        }
        .autoextenderhighlight
        {
            color: White;
            background-color: #006699;
            cursor: pointer;
        }
        #divwidth
        {
            width: 400px !important;
        }
        #divwidth div
        {
            width: 400px !important;
        }
    </style>
    <div style="padding: 8px;">
        <div class="border">
            <asp:UpdatePanel ID="divErrorComponent" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="divError" runat="server" Style="padding: 5px; background-color: #FFFFC0;
                        border: solid 1px #C00000; margin-bottom: 10px;" HorizontalAlign="Left" Visible="false">
                        <asp:Label ID="lblError" runat="server" Style="font-weight: bold; color: #800000"
                            Text=""></asp:Label>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="divTestComponent" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="border pad5">
                        <table cellspacing="6" cellpadding="0" width="100%" border="0">
                            <tbody>
                                <tr>
                                    <td colspan="2">
                                        <div class="pad5" style="white-space: nowrap">
                                            <label id="labeldepartm" class="required right35" for="ddlDepartment" style="margin-left: 10px;
                                                width: 100px">
                                                *Department:</label>
                                            <span style="display: <%= sDepartmentEntry %>">
                                                <asp:DropDownList ID="ddlDepartment" runat="Server" Width="250px" Height="20px">
                                                </asp:DropDownList>
                                            </span><span style="display: <%= hDepartmentEntry %>">
                                                <asp:Label ID="labelSelectedDepartment" runat="server"></asp:Label></span> &nbsp;&nbsp;<b><i>NB:
                                                    You can only request services from one department at a time. </i></b>
                                    </td>
                                </tr>
                                <tr id="divSelectService" style="display: <%= sServiceEntry %>">
                                    <td colspan="2">
                                        <div class="pad5">
                                            <label class="required right35" for="textSelectService" style="margin-left: 10px;
                                                width: 100px">
                                                Select Service:</label>
                                            <asp:TextBox ID="textSelectService" Width="63%" runat="server" AutoPostBack="true"
                                                AutoComplete="off" Font-Names="Courier New" Font-Size="Medium" OnTextChanged="ServiceNameChanged"
                                                Height="20px"></asp:TextBox>
                                            <div id="divwidth" style="width: 63%">
                                            </div>
                                            <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchService" MinimumPrefixLength="2"
                                                CompletionInterval="30" EnableCaching="false" CompletionSetCount="10" TargetControlID="textSelectService"
                                                BehaviorID="AutoCompleteEx" OnClientItemSelected="ace1_itemSelected" ID="AutoCompleteExtender1"
                                                runat="server" FirstRowSelected="false" CompletionListCssClass="autoextender"
                                                UseContextKey="true" CompletionListItemCssClass="autoextenderlist" CompletionListHighlightedItemCssClass="autoextenderhighlight"
                                                CompletionListElementID="divwidth">
                                                <Animations>
								                <OnShow>
								                    <Sequence>
								                        <OpacityAction Opacity="0" />
								                        <HideAction Visible="true" />
								                        <ScriptAction Script="var behavior = $find('AutoCompleteEx');if (!behavior._height) {var target = behavior.get_completionList();behavior._height = target.offsetHeight - 2;target.style.height = '0px';}" />
								                        <Parallel Duration=".4">
								                            <FadeIn />
								                            <Length PropertyKey="height" StartValue="0" EndValueScript="$find('AutoCompleteEx')._height" />
								                        </Parallel>
								                    </Sequence>
								                </OnShow>
								                <OnHide>
								                    <Parallel Duration=".4">
								                        <FadeOut />
								                        <Length PropertyKey="height" StartValueScript="$find('AutoCompleteEx')._height" EndValue="0" />
								                    </Parallel>
								                </OnHide>
                                                </Animations>
                                            </ajaxToolkit:AutoCompleteExtender>
                                            <asp:HiddenField ID="hdCustID" runat="server" />
                                            <asp:HiddenField ID="hServiceId" runat="server" Value="-1" />
                                            <asp:HiddenField ID="hServiceName" runat="server" Value="" />
                                            <asp:HiddenField ID="hModuleServiceId" runat="server" Value="-1" />
                                            <asp:HiddenField ID="hModuleServiceName" runat="server" Value="" />
                                            <asp:HiddenField ID="hdappcurrentdate" runat="server" />
                                        </div>
                                    </td>
                                </tr>
                                <tr style="display: <%= sDataEntry %>">
                                    <td colspan="2">
                                        <div class="pad5">
                                            <label class="right35" style="margin-left: 10px; width: 100px" for="txtRequestNote">
                                                Description:</label>
                                            <asp:TextBox ID="txtRequestNote" runat="server" MaxLength="255" Width="63%" Height="20px"></asp:TextBox></div>
                                    </td>
                                </tr>
                                <tr style="display: <%= sDataEntry %>">
                                    <td colspan="2">
                                        <div class="pad5">
                                            <label class="right35" style="margin-left: 10px; width: 100px" for="textQuantity">
                                                Quantity:</label>
                                            <asp:TextBox ID="textQuantity" runat="server" MaxLength="2" Width="80px" Height="20px"
                                                Text="1" ValidationGroup="df_x"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* Quantity"
                                                ControlToValidate="textQuantity" Display="Dynamic" ValidationGroup="df_x">
                                            </asp:RequiredFieldValidator>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="fteLimit" runat="server" Enabled="false"
                                                FilterType="Numbers" TargetControlID="textQuantity">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                        </div>
                                    </td>
                                </tr>
                                <tr style="display: <%= sPaper %>">
                                    <td colspan="2">
                                        <div class="pad5">
                                            <label class="right35" style="margin-left: 10px; width: 100px" for="txtResultNote">
                                                Results:</label>
                                            <asp:TextBox ID="txtResultNote" runat="server" MaxLength="255" Width="63%" Height="20px"></asp:TextBox></div>
                                    </td>
                                </tr>
                                <tr style="display: <%= sDataEntry %>">
                                    <td colspan="2" style="width: 100%">
                                        <div id="divAction" class="pad5" style="white-space: nowrap; border-top: 0px solid #ddd;
                                            text-align: center; margin-top: 10px; width: 100%">
                                            <span>&nbsp;&nbsp;&nbsp;<asp:Button ID="btnAddRecord" runat="server" Text=" Add "
                                                Width="120px" CausesValidation="true" ValidationGroup="df_x" ForeColor="Black"
                                                OnClick="btnAddRecord_Click" />
                                                &nbsp;&nbsp;&nbsp;</span>
                                            <asp:Button ID="buttonCancel" runat="server" Text="Cancel" Width="80px" OnClick="CancelEntry"
                                                ForeColor="Black" />
                                            <asp:Button ID="button1" runat="server" Text="Button1" Width="80px" ForeColor="Black"
                                                Style="display: none" OnClick="button1_Click" />
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="gridServiceRequested" EventName="RowCommand" />
                    <asp:AsyncPostBackTrigger ControlID="button1" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="divGridComponent" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <div class="whitebg border pad5" style="overflow: auto;">
                        <div class="grid">
                            <div class="rounded">
                                <div class="mid-outer">
                                    <div class="mid-inner">
                                        <div class="mid" style="height: 200px; overflow: auto">
                                            <div id="grd_custom" class="GridView whitebg">
                                                <asp:GridView ID="gridServiceRequested" runat="server" AutoGenerateColumns="False"
                                                    Width="100%" AllowSorting="False" BorderWidth="0px" GridLines="None" CssClass="datatable table-striped table-responsive"
                                                    DataKeyNames="ServiceId" OnRowCommand="gridServiceRequested_RowCommand" OnRowDataBound="gridServiceRequested_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="ServiceName" HeaderText="Service Name" ReadOnly="True"
                                                            SortExpression="ServiceName">
                                                            <HeaderStyle Font-Underline="False" />
                                                            <ItemStyle CssClass="textstyle" />
                                                        </asp:BoundField>
                                                         <asp:BoundField DataField="Quantity" HeaderText="Quantity (rounds)" SortExpression="Quantity">
                                                            <ItemStyle CssClass="textstyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="RequestNotes" HeaderText="Description" SortExpression="RequestNotes">
                                                            <ItemStyle CssClass="textstyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ResultNotes" HeaderText="Result" SortExpression="ResultNotes">
                                                            <ItemStyle CssClass="textstyle" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField>
                                                            <ItemStyle CssClass="textstyle" />
                                                            <ItemTemplate>
                                                                <div style="white-space: nowrap; margin-left: 10px">
                                                                    <span style='display: <%= sEdit %>; white-space: nowrap'>
                                                                        <asp:Button ID="buttonRemove" runat="server" CausesValidation="false" CommandName="Remove"
                                                                            Text="Remove" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ServiceId") %>'
                                                                            ForeColor="Blue"></asp:Button></span></div>
                                                                <ajaxToolkit:ConfirmButtonExtender ID="cbeRemove" runat="server" DisplayModalPopupID="mpeRemove"
                                                                    TargetControlID="buttonRemove">
                                                                </ajaxToolkit:ConfirmButtonExtender>
                                                                <ajaxToolkit:ModalPopupExtender ID="mpeRemove" runat="server" PopupControlID="pnlPopup"
                                                                    TargetControlID="buttonRemove" OkControlID="btnYes" CancelControlID="btnNo" BackgroundCssClass="modalBackground">
                                                                </ajaxToolkit:ModalPopupExtender>
                                                                <asp:Panel ID="pnlPopup" runat="server" Style="display: none; background-color: #FFFFFF;
                                                                    width: 300px; border: 3px solid #0DA9D0;">
                                                                    <div style="background-color: #2FBDF1; height: 30px; color: White; line-height: 30px;
                                                                        text-align: center; font-weight: bold;">
                                                                        Confirmation
                                                                    </div>
                                                                    <div style="min-height: 50px; line-height: 30px; text-align: center; font-weight: bold;">
                                                                        Are you sure you want to remove this service?
                                                                    </div>
                                                                    <div style="padding: 3px;" align="right">
                                                                        <asp:Button ID="btnYes" runat="server" Text="Yes" ForeColor="DarkGreen" /><asp:Button
                                                                            ID="btnNo" runat="server" Text="No" ForeColor="DarkBlue" Style="margin-left: 10px" /></div>
                                                                </asp:Panel>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                    <RowStyle CssClass="gridrow" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnAddRecord" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="buttonCancel" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="divRequestComponent" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table cellspacing="6" cellpadding="0" style="margin-top: 5px" width="100%" border="0"
                        class="form">
                        <tbody>
                            <%-- <tr>
                                <td colspan="2">
                                    <hr />
                                </td>
                            </tr>--%>
                            <tr>
                                <td class="pad18" align="left" colspan="2">
                                    <label class="right35" for="txtClinicalNotes">
                                        Clinical Notes:</label>
                                    <br />
                                    <asp:TextBox ID="txtClinicalNotes" runat="server" TextMode="MultiLine" Rows="3" MaxLength="255"
                                        Width="90%" Text=''></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="pad18" style="width: 50%">
                                    <label class="required" for="ddlorderedbyname">
                                        *Ordered by:</label>
                                    <asp:DropDownList ID="ddlorderedbyname" runat="Server" Width="180px">
                                    </asp:DropDownList>
                                </td>
                                <td class="pad18" style="width: 50%">
                                    <label class="required" for="txtorderedbydate">
                                        *Ordered By Date:</label>
                                    <asp:TextBox ID="txtorderedbydate" MaxLength="12" runat="server"></asp:TextBox>
                                    <asp:ImageButton runat="Server" ID="Image1" Height="22" Style="width: 22; height: 22"
                                        ImageUrl="~/Images/cal_icon.gif" AlternateText="Click to show calendar" />
                                    <ajaxToolkit:CalendarExtender ID="cbeOrderDate" runat="server" TargetControlID="txtorderedbydate"
                                        PopupButtonID="Image1" Format="dd-MMM-yyyy" />
                                    <span class="smallerlabel" id="SPAN2">(DD-MMM-YYYY)</span>
                                </td>
                            </tr>
                            <tr style="display: <%= sPaper %>">
                                <td class="pad18" style="width: 50%">
                                    <label id="lblreportedby" runat="server" class="required" for="ddlReportedbyName">
                                        *Reported by:</label>
                                    <asp:DropDownList ID="ddlReportedbyName" runat="Server" Width="180px">
                                    </asp:DropDownList>
                                </td>
                                <td class="pad18" style="width: 50%">
                                    <label id="lblreportedbydate" runat="server" for="txtReportedbyDate" class="required">
                                        *Reported By Date:</label>
                                    <asp:TextBox ID="txtReportedbyDate" MaxLength="12" runat="server"></asp:TextBox>
                                    <asp:ImageButton runat="Server" ID="ImageButton1" Height="22" Style="width: 22; height: 22"
                                        ImageUrl="~/Images/cal_icon.gif" AlternateText="Click to show calendar" />
                                    <ajaxToolkit:CalendarExtender ID="cbeReportDate" runat="server" TargetControlID="txtReportedbyDate"
                                        PopupButtonID="ImageButton1" Format="dd-MMM-yyyy" />
                                    <span class="smallerlabel" id="SPAN1">(DD-MMM-YYYY)</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="pad18 center form" colspan="2">
                                    <asp:Button ID="buttonSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                                    <asp:Button ID="btnclose" runat="server" Font-Size="12px" Width="75px" Text="Close"
                                        OnClick="ExitPage" />
                                </td>
                            </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="textSelectService" EventName="TextChanged" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="divNotifyComponent" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="pnNotify" runat="server" Style="display: none; width: 460px; border: solid 1px #808080;
                        background-color: #E0E0E0; z-index: 15000">
                        <asp:Panel ID="pnPopup_Title" runat="server" Style="border: solid 1px #808080; margin: 0px 0px 0px 0px;
                            cursor: move; height: 18px">
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
                            <asp:Button ID="btnOkAction" runat="server" Text="OK" Width="80px" Style="border: solid 1px #808080;"
                                OnClick="btnOkAction_Click" CausesValidation="false" />
                        </div>
                    </asp:Panel>
                    <asp:Button ID="btn" runat="server" Style="display: none" />
                    <ajaxToolkit:ModalPopupExtender ID="notifyPopupExtender" runat="server" TargetControlID="btn"
                        PopupControlID="pnNotify" BackgroundCssClass="modalBackground" DropShadow="True"
                        BehaviorID="laborder_bhx" PopupDragHandleControlID="pnPopup_Title" Enabled="True"
                        DynamicServicePath="">
                    </ajaxToolkit:ModalPopupExtender>
                </ContentTemplate>
                <Triggers>
                </Triggers>
            </asp:UpdatePanel>
            <uc1:progressControl ID="progressControl1" runat="server" />
        </div>
    </div>
</asp:Content>
