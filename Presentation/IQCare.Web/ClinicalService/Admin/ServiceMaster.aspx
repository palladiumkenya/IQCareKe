<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    CodeBehind="ServiceMaster.aspx.cs" Inherits="IQCare.Web.ClinicalService.Admin.ServiceMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<%@ Register Src="~/ProgressControl.ascx" TagName="progressControl" TagPrefix="uc1" %>
<asp:Content ID="ctMain" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <script type="text/javascript">
        function ShowModalPopup() {
           // $('#<%=textServiceName.ClientID %>').val("");
           // $('#<%=textDescription.ClientID %>').val("");
           // $("#<%=ddlServiceArea.ClientID %>").get(0).selectedIndex = 0;
            $find("service_bhx").show();
            return false;
        }
        function HideModalPopup() {
            $find("service_bhx").hide();
            return false;
        }
    </script>
    <div>
        <h1 id="H1" runat="server" class="margin" style="padding-left: 10px;">
            Clinical Services
        </h1>
        <div class="center">
            <asp:UpdatePanel runat="server" ID="divErrorUp" UpdateMode="Always">
                <ContentTemplate>
                    <asp:Panel ID="divError" runat="server" Style="padding: 5px" CssClass="background-color: #FFFFC0; border: solid 1px #C00000"
                        HorizontalAlign="Left" Visible="true">
                        <asp:Label ID="lblError" runat="server" Style="font-weight: bold; color: #800000"
                            Text=""></asp:Label>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="divGridComponent" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table width="100%" border="0" cellpadding="0" cellspacing="6">
                        <tbody>
                            <tr>
                                <td class="border pad5 formbg">
                                    <div class="grid">
                                        <div class="rounded">
                                            <div class="mid-outer">
                                                <div class="mid-inner">
                                                    <div class="mid" style="height: 300px; overflow: auto">
                                                        <div id="grd_custom" class="GridView whitebg">
                                                            <asp:GridView ID="gridServiceMaster" runat="server" AutoGenerateColumns="False" Width="100%"
                                                                BorderWidth="0px" GridLines="None" CssClass="datatable table-striped table-responsive" DataKeyNames="Id,ServiceAreaId"
                                                                CellPadding="0" OnRowCommand="gridServiceMaster_RowCommand" OnRowDataBound="gridServiceMaster_RowDataBound">
                                                                <Columns>
                                                                    <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="True" SortExpression="Name">
                                                                        <HeaderStyle Font-Underline="False" />
                                                                        <ItemStyle CssClass="textstyle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description">
                                                                        <ItemStyle CssClass="textstyle" Wrap="False" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="ServiceArea" HeaderText="Service Area" SortExpression="ServiceArea">
                                                                        <ItemStyle CssClass="textstyle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="DeleteFlag" HeaderText="Status" SortExpression="DeleteFlag">
                                                                        <ItemStyle CssClass="textstyle" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField>
                                                                        <ItemStyle CssClass="textstyle" />
                                                                        <ItemTemplate>
                                                                            <div style="white-space: nowrap">
                                                                                 <span style='display: <%= svPerm %>; white-space: nowrap'>
                                                                                        <asp:Button ID="buttonDelete" runat="server" CausesValidation="false" CommandName="DeleteService"
                                                                                            Text="Make Inactive" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                                                                                            ForeColor="Blue"></asp:Button></span>
                                                                                <ajaxToolkit:ConfirmButtonExtender ID="cbeInactive" runat="server" DisplayModalPopupID="mpeInactive"
                                                                                    TargetControlID="buttonDelete">
                                                                                </ajaxToolkit:ConfirmButtonExtender>
                                                                                <ajaxToolkit:ModalPopupExtender ID="mpeInactive" runat="server" PopupControlID="pnlInactivePopup"
                                                                                    TargetControlID="buttonDelete" OkControlID="btnInactiveYes" CancelControlID="btnInactiveNo"
                                                                                    BackgroundCssClass="modalBackground">
                                                                                </ajaxToolkit:ModalPopupExtender>
                                                                                <asp:Panel ID="pnlInactivePopup" runat="server" Style="display: none; background-color: #FFFFFF;
                                                                                    width: 300px; border: 3px solid #0DA9D0;">
                                                                                    <div style="background-color: #2FBDF1; height: 30px; color: White; line-height: 30px;
                                                                                        text-align: center; font-weight: bold;">
                                                                                        Confirmation
                                                                                    </div>
                                                                                    <div style="min-height: 50px; line-height: 30px; text-align: center; font-weight: bold;">
                                                                                        <table border="0" cellpadding="15" cellspacing="0" style="width: 100%; height: 25px">
                                                                                            <tr>
                                                                                                <td style="width: 48px" valign="middle" align="center">
                                                                                                    <img src="../../Images/mb_question.gif" alt="" width="32" height="32" />
                                                                                                </td>
                                                                                                <td style="width: 100%; padding-left: 20px" valign="middle" align="left">
                                                                                                    You are about delete
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "Name")%>. &nbsp;<br />
                                                                                                    Are you sure you want to proceed?
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </div>
                                                                                    <div style="padding: 3px;" align="center">
                                                                                        <asp:Button ID="btnInactiveYes" runat="server" Text=" Yes " ForeColor="DarkGreen" />
                                                                                        <asp:Button ID="btnInactiveNo" runat="server" Text=" No " ForeColor="DarkBlue" Style="margin-left: 10px" /></div>
                                                                                </asp:Panel>
                                                                            </div>
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
                                </td>
                            </tr>
                            <tr>
                                <td class="pad5 center">
                                   <span style='display: <%= svPerm %>; white-space: nowrap'> <asp:Button ID="btnAdd" runat="server" Text="Add" /></span>
                                    <asp:Button ID="btnCancel" runat="server" Text="Close" Visible="false"/>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="buttonSave" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
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
                                            Add Service
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
                                <td style="height: 25px; text-align: left;width:180px">
                                    <label for="textServiceName" class="required" style="white-space:nowrap">
                                        Service Name:</label>
                                </td>
                                <td align="left" style="white-space: nowrap; vertical-align: middle">
                                    <asp:TextBox ID="textServiceName" runat="server" MaxLength="50" Columns="75" ValidationGroup="sf_dx"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftLabname" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,Custom"
                                        TargetControlID="textServiceName" ValidChars="-/\_& ">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* Service Name"
                                        ControlToValidate="textServiceName" Display="Dynamic" ValidationGroup="sf_dx">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr style="display: block">
                                <td style="height: 25px; text-align: left;width:180px">
                                    <label for="textDescription">
                                        Description:</label>
                                </td>
                                <td align="left" style="white-space: nowrap; vertical-align: middle;">
                                    <asp:TextBox ID="textDescription" runat="server" MaxLength="250" Columns="75"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="fteNameRef" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,Custom"
                                        TargetControlID="textDescription" ValidChars="-/\_& ">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr style="display: block">
                                <td style="height: 25px; text-align: left;width:180px">
                                    <label for="ddlServiceArea"> Service Area :</label>
                                </td>
                                <td style="white-space: nowrap; text-align: left" align="left">
                                    <asp:DropDownList ID="ddlServiceArea" runat="server" Width="200px">
                                    </asp:DropDownList>
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
                                        <span style='display: <%= svPerm %>'>&nbsp;&nbsp;&nbsp;<asp:Button ID="buttonSave" runat="server" Text="Save" Width="120px"
                                            OnClick="buttonSave_Click" CausesValidation="true" ValidationGroup="sf_dx" />
                                            &nbsp;&nbsp;&nbsp;</span>
                                        <asp:Button ID="buttonCancel" runat="server" Text="Cancel" Width="80px" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Button ID="buttonRaiseItemPopup" runat="server" Style="display: none" />
                    <ajaxToolkit:ModalPopupExtender ID="ServiceDialog" runat="server" TargetControlID="buttonRaiseItemPopup"
                        BehaviorID="service_bhx" PopupControlID="divLabPopup" BackgroundCssClass="modalBackground"
                        CancelControlID="buttonCancel" DropShadow="True" PopupDragHandleControlID="divTitle"
                        Enabled="True" DynamicServicePath="">
                    </ajaxToolkit:ModalPopupExtender>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="notificationPanel" runat="server">
                <ContentTemplate>
                    <asp:Button ID="btn" runat="server" Style="display: none" />
                    <asp:Panel ID="pnNotify" runat="server" Style="display: none; width: 460px; border: solid 1px #808080;
                        background-color: #E0E0E0; z-index: 15000">
                        <asp:Panel ID="pnPopup_Title" runat="server" Style="border: solid 1px #808080; margin: 0px 0px 0px 0px;
                            cursor: move; height: 18px">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 18px;
                                background-color: #6699FF">
                                <tr>
                                    <td style="width: 5px; height: 19px;">
                                    </td>
                                    <td style="width: 100%; height: 19px;">
                                        <span style="font-weight: bold; color: White">
                                            <asp:Label ID="lblNotice" runat="server"></asp:Label></span>
                                    </td>
                                    <td style="width: 5px; height: 19px;">
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <table border="0" cellpadding="15" cellspacing="0" style="width: 100%;">
                            <tr>
                                <td style="width: 48px" valign="middle" align="center">
                                    <asp:Image ID="imgNotice" runat="server" ImageUrl="~/images/mb_information.gif" Height="32px"
                                        Width="32px" />
                                </td>
                                <td style="width: 100%;" valign="middle" align="center">
                                    <asp:Label ID="lblNoticeInfo" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <div style="background-color: #FFFFFF; border-top: solid 1px #808080; width: 100%;
                            text-align: center; padding-top: 5px; padding-bottom: 5px">
                            <asp:Button ID="btnOkAction" runat="server" Text="Close" Width="120px" Style="border: solid 1px #808080;" />&nbsp;&nbsp;&nbsp;<asp:Button
                                ID="btnExit" runat="server" Text="Close" Width="80px" Style="border: solid 1px #808080;
                                display: none" /></div>
                    </asp:Panel>
                    <ajaxToolkit:ModalPopupExtender ID="notifyPopupExtender" runat="server" TargetControlID="btn"
                        PopupControlID="pnNotify" BackgroundCssClass="modalBackground" DropShadow="True"
                        PopupDragHandleControlID="pnPopup_Title" Enabled="True" DynamicServicePath=""
                        OkControlID="btnOkAction">
                    </ajaxToolkit:ModalPopupExtender>
                </ContentTemplate>
            </asp:UpdatePanel>
            <uc1:progressControl ID="progressControl1" runat="server" />
        </div>
    </div>
</asp:Content>
