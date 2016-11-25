<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    CodeBehind="EnterResultPage.aspx.cs" Inherits="IQCare.Web.Laboratory.EnterResultPage" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%--<%@ Register Assembly="AjaxControlToolkit" TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" %>--%>
<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<%--<%@ Register Src="~/ProgressControl.ascx" TagName="progressControl" TagPrefix="uc1" %>--%>
<asp:Content ID="ctMain" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <script type="text/javascript">
        function ShowModalPopup() {
            $find("laborder_bhx").show();
            return false;
        }

        function HideModalPopup() {
            $find("laborder_bhx").hide();
            return false;
        }
        function unitSelectedChanged(combo, eventArgs) {
            var item = eventArgs.get_item();
            var detection_limit = (item.get_attributes().getAttribute("detection_limit"));
            var min_value = (item.get_attributes().getAttribute("min"));
            var max_value = (item.get_attributes().getAttribute("max"));
            var min_normal = (item.get_attributes().getAttribute("min_normal"));
            var max_normal = (item.get_attributes().getAttribute("max_normal"));
        }
    </script>
    <div style="padding: 8px;">
        <div class="border">
            <asp:HiddenField ID="HPatientId" runat="server" Value="-1" />
            <asp:HiddenField ID="HLabOrderId" runat="server" Value="-1" />
            <asp:HiddenField ID="HUserId" runat="server" />
            <asp:HiddenField ID="hLabTestId" runat="server" Value="-1" />
            <asp:HiddenField ID="HLabOrderTestId" runat="server" Value="-1" />
            <asp:HiddenField ID="hdappcurrentdate" runat="server" />
            <h3 id="H1" class="margin" style="padding-left: 10px;">
                <asp:Label runat="server" ID="labelOrderNumber"></asp:Label>
                <asp:Label runat="server" ID="labelTestOrderStatus"></asp:Label>
            </h3>
            <asp:UpdatePanel ID="divErrorComponent" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="divError" runat="server" Style="padding: 5px; background-color: #FFFFC0;
                        border: solid 1px #C00000; margin-bottom: 10px;" HorizontalAlign="Left" Visible="false">
                        <asp:Label ID="lblError" runat="server" Style="font-weight: bold; color: #800000"
                            Text=""></asp:Label>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <table width="100%" class="form">
                <tr>
                    <td align="left" valign="top" colspan="2">
                        <label class="right35 bold" for="labelTestNotes">
                            Request Notes:</label>
                        <asp:Label ID="labelTestNotes" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:UpdatePanel ID="divResultComponent" runat="server" UpdateMode="Conditional"
                ChildrenAsTriggers="true">
                <ContentTemplate>
                    <asp:Repeater ID="repeaterResult" runat="server" OnItemDataBound="repeaterResult_ItemDataBound">
                        <HeaderTemplate>
                            <table style="width: 100%;">
                                <tr>
                                    <td class="pad18 bold" style="width: 20%;">
                                        Parameter Name
                                    </td>
                                    <td class="pad18 bold" style="width: 30%; text-align: center">
                                        Result
                                    </td>
                                    <td class="pad18 bold" style="width: 10%; text-align: center">
                                        Undetectable
                                    </td>
                                    <td class="pad18 bold" style="width: 20%; text-align: center">
                                        Detection Limit
                                    </td>
                                    <td class="pad18 bold" style="width: 20%; text-align: center">
                                        Unit
                                    </td>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="pad18" align="left" style="vertical-align: middle">
                                    <asp:Label ID="labelParameterName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ParameterName")%>'></asp:Label>
                                    <asp:HiddenField ID="hParameterId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "ParameterId") %>' />
                                    <asp:HiddenField ID="hResultTestId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "LabTestId") %>' />
                                    <asp:HiddenField ID="hTestOrderId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "LabOrderTestId") %>' />
                                    <asp:HiddenField ID="HResultDataType" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "ResultDataType") %>' />
                                    <asp:HiddenField ID="HResultId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Id") %>' />
                                </td>
                                <td class="pad18" align="left" style="vertical-align: middle; width: 30%" colspan='<%#ColCount(Eval("ResultDataType"))%>'>
                                    <div id="divNumeric"  style=' text-align: center;white-space: nowrap; width: 100%; display: <%#ShowNumDiv(Eval("ResultDataType"))%>'>
                                        <%--<span style="width: 40%;">--%>
                                        <asp:TextBox ID="textResultValue" runat="server" MaxLength="10" Width="80px" Text='<%# DataBinder.Eval(Container.DataItem, "ResultValue")%>'></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="fteValue" runat="server" Enabled="true"
                                            FilterType="Numbers,Custom" TargetControlID="textResultValue" ValidChars=".">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        &nbsp;&nbsp;&nbsp;<%--</span> --%>
                                        <%--  <span style="width: 20%;">
                                                </span>--%>
                                        <%-- <span style="width: 20%;">
                                           </span>--%>
                                        <%--<span style="width: 20%;">                                            
                                            </span>--%>
                                    </div>
                                    <div id="divText" style='white-space: nowrap; width: 100%; display: <%# ShowTextDiv(Eval("ResultDataType")) %>'>
                                        <asp:TextBox ID="textResultText" runat="server" Rows="3" MaxLength="255" Text='<%# DataBinder.Eval(Container.DataItem, "ResultText")%>'
                                            Width="90%"></asp:TextBox>
                                    </div>
                                    <div id="divSelect" style='white-space: nowrap; width: 100%; display: <%# ShowSelectDiv(Eval("ResultDataType")) %>'>
                                        <asp:DropDownList ID="ddlResultList" runat="Server" Width="180px">
                                        </asp:DropDownList>
                                    </div>
                                </td>
                                <td style='text-align: center; white-space: nowrap; width: 10%; display: <%#ShowNumDiv(Eval("ResultDataType"))%>'>
                                    <asp:CheckBox ID="checkUndetectable" runat="server" Text="" TextAlign="Left" Checked='<%# DataBinder.Eval(Container.DataItem, "Undetectable")%>' />
                                </td>
                                <td style='text-align: center; white-space: nowrap; width: 20%; display: <%#ShowNumDiv(Eval("ResultDataType"))%>'>
                                    <%-- <span id="divNumericLimit" style='white-space: nowrap; width: 100%; display: <%#ShowNumDiv(Eval("ResultDataType"))%>'>--%>
                                    <asp:TextBox ID="textDetectionLimit" runat="server" MaxLength="6" Width="80px" Text='<%# DataBinder.Eval(Container.DataItem, "DetectionLimit")%>'></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="fteLimit" runat="server" Enabled="true"
                                        FilterType="Numbers,Custom" TargetControlID="textDetectionLimit" ValidChars=".">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                    <%-- &nbsp;&nbsp;&nbsp; </span>--%>
                                </td>
                                <td style='text-align: center; white-space: nowrap; width: 20%; display: <%#ShowNumDiv(Eval("ResultDataType"))%>'>
                                    <%--  <span id="divNumericResult" style='white-space: nowrap; width: 100%; display: <%#ShowNumDiv(Eval("ResultDataType"))%>'>--%>
                                 <%--   <telerik:RadComboBox ID="ddlResultUnit" runat="server" Width="180px" AutoPostBack="false"
                                        MarkFirstMatch="false" HighlightTemplatedItems="true" AppendDataBoundItems="true">
                                        <CollapseAnimation Duration="200" Type="OutQuint" />
                                        <HeaderTemplate>
                                        </HeaderTemplate>
                                    </telerik:RadComboBox>--%>
                                    <asp:DropDownList ID="ddlResultUnit" runat="server" Width="180px" AutoPostBack="false" AppendDataBoundItems="false"></asp:DropDownList>
                                    <%-- </span>--%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            <tr>
                                <td>
                                </td>
                            </tr>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="divRequestComponent" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table cellspacing="6" cellpadding="0" style="margin-top: 10px" width="100%" border="0"
                        class="form">
                        <tbody>
                            <tr>
                                <td class="pad18" align="left" colspan="2">
                                    <label class="right35" for="txtResultNotes">
                                        Result Commemts:</label>
                                    <asp:TextBox ID="txtResultNotes" runat="server" TextMode="MultiLine" Rows="3" MaxLength="255"
                                        Width="90%" Text=''></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="">
                                <td class="pad18" style="width: 50%">
                                    <label id="lblreportedby" runat="server" class="required" for="ddlLabReportedbyName">
                                        Reported by:</label>
                                    <asp:DropDownList ID="ddlLabReportedbyName" runat="Server" Width="180px">
                                    </asp:DropDownList>
                                </td>
                                <td class="pad18" style="width: 50%">
                                    <label id="lblreportedbydate" runat="server" for="labReportedbyDate" class="required">
                                        Reported By Date:</label>
                                    <asp:TextBox ID="txtlabReportedbyDate" MaxLength="12" runat="server"></asp:TextBox>
                                    <asp:ImageButton runat="Server" ID="ImageButton1" Height="22" Style="hspace: 3; width: 22;
                                        height: 22" ImageUrl="~/Images/cal_icon.gif" AlternateText="Click to show calendar" />
                                    <ajaxToolkit:CalendarExtender ID="cbeReportDate" runat="server" TargetControlID="txtlabReportedbyDate"
                                        PopupButtonID="ImageButton1" Format="dd-MMM-yyyy" />
                                    <span class="smallerlabel" id="SPAN1">(DD-MMM-YYYY)</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="pad18 center form" colspan="2">
                                    <asp:Button ID="buttonSave" runat="server" Text="Save" OnClick="SaveResults" />
                                    <asp:Button ID="btnclose" runat="server" Font-Size="12px" Width="75px" Text="Close"
                                        OnClick="ExitPage" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <%--<asp:UpdatePanel ID="divNotifyComponent" runat="server">
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
            </asp:UpdatePanel>--%>
<%--            <uc1:progressControl ID="progressControl1" runat="server" />--%>
        </div>
    </div>
</asp:Content>
