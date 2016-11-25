<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    CodeBehind="ServiceSubmitResult.aspx.cs" Inherits="IQCare.Web.ClinicalService.ServiceSubmitResult" %>

<%@ Register Assembly="AjaxControlToolkit" TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" %>
<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<%@ Register Src="~/ProgressControl.ascx" TagName="progressControl" TagPrefix="uc1" %>
<asp:Content ID="ctMain" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <style>
        div.ajax__calendar_container, div.ajax__calendar_body
        {
            width: 225px;
        }
        
        .ajax__calendar_days td
        {
            padding: 2px 4px;
        }
    </style>
    <script type="text/javascript">
        function ShowModalPopup(serviceorderid, serviceName) {
            $('#<%=textResult.ClientID %>').val("");
            $('#<%=hdServiceTestId.ClientID %>').val(serviceorderid);
            $('#<%=labelServiceName.ClientID %>').text("Results for " + serviceName);
            $('#<%=textResult.ClientID %>').val("");
            var currDate = $('#<%=hdappcurrentdate.ClientID %>').val()
            $('#<%=txtReportedbyDate.ClientID %>').val(currDate);
            $("#<%=ddlReportedbyName.ClientID %>").get(0).selectedIndex = 0;
            if (parseInt(serviceorderid) > 0) {
                $find("serviceresult_bhx").show();
            }
            return false;
        }
        function HideModalPopup() {
            $find("serviceresult_bhx").hide();
            return false;
        }
        function disable_future_dates(sender, args) {
            var senderDate = new Date(sender._selectedDate);
            senderDate.setHours(0, 0, 0, 0);
            var nowDate = new Date(); nowDate.setHours(0, 0, 0, 0);
            if (senderDate > nowDate) {
                alert('You cannot select a day after today');
                sender._selectedDate = new Date(); sender._textbox.set_Value(sender._selectedDate.format(sender._format));
            }
        }
    </script>
    <div style="padding: 8px;">
        <div class="border">
            <asp:HiddenField ID="hdServiceOrderId" runat="server" Value="-1" />
            <asp:HiddenField ID="hdServiceTestId" runat="server" Value="-1" />
            <asp:HiddenField ID="hdPatientId" runat="server" Value="-1" />
            <asp:HiddenField ID="hdappcurrentdate" runat="server" />
            <asp:UpdatePanel ID="divErrorComponent" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="divError" runat="server" Style="padding: 5px; background-color: #FFFFC0;
                        border: solid 1px #C00000; margin-bottom: 10px;" HorizontalAlign="Left" Visible="false">
                        <asp:Label ID="lblError" runat="server" Style="font-weight: bold; color: #800000"
                            Text=""></asp:Label>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <h3 id="H1" class="margin" style="padding-left: 10px;">
                <asp:Label runat="server" ID="labelOrderNumber"></asp:Label>
            </h3>
            <table width="100%" class="form">
                <tr style="display: none">
                    <td class="pad18" style="width: 20%; white-space: nowrap">
                        <label class="right35 bold" for="labelOrderedbyname">
                            Ordered by:</label>
                        <asp:Label ID="labelOrderedbyname" runat="Server">
                        </asp:Label>
                    </td>
                    <td class="pad18 bold" style="width: 20%">
                        <label class="right35" for="txtlaborderedbydate">
                            Order Date:</label>
                        <asp:Label ID="labellaborderedbydate" MaxLength="12" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="pad18" colspan="2" valign="middle">
                        <label class="right35 bold" for="labelClinicalNotes">
                            Clinical Notes:</label>
                        <asp:Label ID="labelClinicalNotes" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:UpdatePanel ID="divResultComponent" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="whitebg border pad5" style="overflow: auto; min-height: 300px">
                        <div class="grid">
                            <div class="rounded">
                                <div class="mid-outer">
                                    <div class="mid-inner">
                                        <div class="mid" style="height: 280px; overflow: auto">
                                            <div id="grd_custom" class="GridView whitebg">
                                                <asp:GridView ID="gridServiceRequested" runat="server" AutoGenerateColumns="False"
                                                    Width="100%" BorderWidth="0px" GridLines="None" CssClass="datatable" DataKeyNames="ServiceId,Id"
                                                    OnRowCommand="gridServiceRequested_RowCommand" OnRowDataBound="gridServiceRequested_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="ServiceName" HeaderText="Service Name" ReadOnly="True"
                                                            SortExpression="ServiceName">
                                                            <HeaderStyle Font-Underline="False" />
                                                            <ItemStyle CssClass="textstyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Quantity" HeaderText="Quantity (rounds)" SortExpression="Quantity">
                                                            <ItemStyle CssClass="textstyle" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Description">
                                                            <ItemStyle CssClass="textstyle" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="labelRequestNotes" runat="server" Font-Bold="false" Visible="true">
                                                                </asp:Label>
                                                                <span style='display: <%# ShowInfoImage(Eval("RequestNotes")) %>'>
                                                                    <asp:LinkButton ID="LinkButton1" runat="server" Text="...." Height="32px" Width="32px"
                                                                        ForeColor="Black" /></span>
                                                                <ajaxToolkit:ModalPopupExtender ID="mpeViewTestNotes" runat="server" PopupControlID="pnlPopupTestNotes"
                                                                    TargetControlID="LinkButton1" CancelControlID="btnTestNoteClose" BackgroundCssClass="modalBackground">
                                                                </ajaxToolkit:ModalPopupExtender>
                                                                <asp:Panel ID="pnlPopupTestNotes" runat="server" Style="display: none; background-color: #FFFFFF;
                                                                    width: 300px; border: 3px solid #0DA9D0;">
                                                                    <div style="background-color: #2FBDF1; height: 30px; color: White; line-height: 30px;
                                                                        text-align: center; font-weight: bold;">
                                                                        <br />
                                                                    </div>
                                                                    <div style="min-height: 50px; line-height: 30px; text-align: center; font-weight: bold;">
                                                                        <%# DataBinder.Eval(Container.DataItem, "RequestNotes")%>
                                                                        &nbsp; &nbsp;
                                                                    </div>
                                                                    <div style="padding: 3px; text-align: center">
                                                                        <asp:Button ID="btnTestNoteClose" runat="server" Text="Close" Style="margin-left: 10px" /></div>
                                                                </asp:Panel>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Result">
                                                            <ItemStyle CssClass="textstyle" />
                                                            <ItemTemplate>
                                                                <div style="white-space: nowrap">
                                                                    <span style='display: <%# EnterResult(Eval("ServiceStatus")) %>; white-space: nowrap'>
                                                                        <asp:Label ID="labelStatus" runat="server" Font-Bold="false" Visible="true"><%# DataBinder.Eval(Container.DataItem, "ServiceStatus")%></asp:Label>&nbsp;
                                                                        <span style="padding: 3px; text-align: right">
                                                                            <asp:Button ID="buttonResult" runat="server" CausesValidation="false" Text="Enter Result"
                                                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' ForeColor="Blue">
                                                                            </asp:Button></span> </span><span style='display: <%# ShowResult(Eval("ServiceStatus")) %>'>
                                                                                <asp:Label ID="labelResultNotes" runat="server"><%# DataBinder.Eval(Container.DataItem, "ResultNotes")%></asp:Label>
                                                                            </span>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Result By">
                                                            <ItemStyle CssClass="textstyle" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="labelReportedbyName" runat="Server"></asp:Label></ItemTemplate>
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
                    <asp:AsyncPostBackTrigger ControlID="buttonSave" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnOkAction" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <div class="pad18 center  form" style="text-align: center; margin-top: 5px; margin: 5px;">
                <asp:Button ID="buttonPrint" runat="server" Text="Print Result" ForeColor="Black"
                    OnClientClick="javascript:alert('Feature not implemented');return false;" Style='display: none' />
                <asp:Button ID="btnExitPage" runat="server" Font-Size="12px" Width="75px" Text="Close"
                    ForeColor="Black" />
            </div>
            <asp:UpdatePanel ID="divServiceComponent" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="divLabPopup" runat="server" Style="display: none; width: 680px; border: solid 1px #808080;
                        background-color: #6699FF" Width="680px" DefaultButton="buttonSave">
                        <asp:Panel ID="divTitle" runat="server" Style="border: solid 1px #808080; margin: 0px 0px 0px 0px;
                            cursor: move; height: 18px">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 18px">
                                <tr>
                                    <td style="width: 5px; height: 19px;">
                                    </td>
                                    <td style="width: 100%; height: 19px;">
                                        <h2 class="forms" align="left">
                                            <asp:Label ID="labelServiceName" runat="server">Results</asp:Label>
                                        </h2>
                                    </td>
                                    <td style="width: 5px; height: 19px;">
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <table cellpadding="1" cellspacing="2" border="0" width="680px" style="border: solid 0px #808080;
                            background-color: #CCFFFF; margin-bottom: 10px" class="border left whitebg">
                            <tr>
                                <td colspan="2">
                                    <hr class="forms" />
                                </td>
                            </tr>
                            <tr style="display: block">
                                <td style="height: 25px; text-align: left; width: 180px">
                                    <label for="textResult" class="required" style="white-space: nowrap">
                                        Results Notes:</label>
                                </td>
                                <td align="left" style="white-space: nowrap; vertical-align: middle">
                                    <asp:TextBox ID="textResult" runat="server" TextMode="MultiLine" Wrap="true" Columns="75"
                                        MaxLength="400" Rows="3" />
                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftResultText" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,Custom"
                                        TargetControlID="textResult" ValidChars="-/\_& ">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* Result"
                                        ControlToValidate="textResult" Display="Dynamic" ValidationGroup="sf_dx">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr style="display: block">
                                <td style="height: 25px; text-align: left; width: 180px">
                                    <label id="lblreportedby" runat="server" width="100px" class="required" for="ddlReportedbyName">
                                        *Reported by:</label>
                                </td>
                                <td align="left" style="white-space: nowrap; vertical-align: middle;">
                                    <asp:DropDownList ID="ddlReportedbyName" runat="Server" Width="280px" Height="20px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr style="display: block">
                                <td style="height: 25px; text-align: left; width: 180px">
                                    <label id="lblreportedbydate" for="txtReportedbyDate" class="required right35">
                                        *Reported By Date:</label>
                                </td>
                                <td style="white-space: nowrap; text-align: left" align="left">
                                    <asp:TextBox ID="txtReportedbyDate" MaxLength="12" runat="server" Width="200px" Height="20px"
                                        ValidationGroup="sf_dx"></asp:TextBox>
                                    <asp:ImageButton runat="Server" ID="ImageButton1" Height="22" Style="width: 22; height: 22"
                                        ImageUrl="~/Images/cal_icon.gif" AlternateText="Click to show calendar" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtReportedbyDate"
                                        ErrorMessage="*" Display="None" ValidationExpression="^(0?[1-9]|[12][0-9]|3[01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$"></asp:RegularExpressionValidator><br />
                                    <ajaxToolkit:CalendarExtender ID="cbeReportDate" runat="server" TargetControlID="txtReportedbyDate"
                                        PopupButtonID="ImageButton1" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="disable_future_dates" />
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtReportedbyDate"
                                        Enabled="True" UserDateFormat="DayMonthYear" CultureDateFormat="dd-MMM-yyyy"
                                        ClearMaskOnLostFocus="False" CultureName="en-GB" Mask="99-LLL-9999">
                                    </ajaxToolkit:MaskedEditExtender>
                                    <span class="smallerlabel" id="SPAN1">(DD-MMM-YYYY)</span>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <hr class="forms" />
                                </td>
                            </tr>
                            <tr>
                                <td class="form pad5 center" style="white-space: nowrap; text-align: center; border: 0"
                                    colspan="2">
                                    <div id="divAction" style="white-space: nowrap; border: 0; text-align: center;">
                                        &nbsp;&nbsp;&nbsp;<asp:Button ID="buttonSave" runat="server" Text="Save" Width="120px"
                                            OnClick="buttonSave_Click" CausesValidation="true" ValidationGroup="sf_dx" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="buttonCancel" runat="server" Text="Cancel" Width="80px" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Button ID="buttonRaiseItemPopup" runat="server" Style="display: none" />
                    <ajaxToolkit:ModalPopupExtender ID="ServiceDialog" runat="server" TargetControlID="buttonRaiseItemPopup"
                        BehaviorID="serviceresult_bhx" PopupControlID="divLabPopup" BackgroundCssClass="modalBackground"
                        CancelControlID="buttonCancel" DropShadow="True" PopupDragHandleControlID="divTitle"
                        Enabled="True" DynamicServicePath="">
                    </ajaxToolkit:ModalPopupExtender>
                </ContentTemplate>
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
                        BehaviorID="notify_bhs" PopupDragHandleControlID="pnPopup_Title" Enabled="True"
                        DynamicServicePath="">
                    </ajaxToolkit:ModalPopupExtender>
                </ContentTemplate>
                <Triggers>
                </Triggers>
            </asp:UpdatePanel>
            <uc1:progressControl ID="progressControl1" runat="server" /></div>
    </div>
</asp:Content>
