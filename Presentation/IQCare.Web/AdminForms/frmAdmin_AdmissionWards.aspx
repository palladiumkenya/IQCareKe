<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    EnableEventValidation="false" CodeBehind="frmAdmin_AdmissionWards.aspx.cs" Inherits="IQCare.Web.Admin.AdmissionWards" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<%@ Register Assembly="AjaxControlToolkit" TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <h2 class="forms" align="left">
        Patients Wards</h2>
    <div class="rounded">
        <table cellspacing="6" cellpadding="0" width="100%" border="0">
            <tbody>
                <tr>
                    <td align="left" style="padding-left: 10px; padding-right: 15px">
                        <asp:UpdatePanel runat="server" ID="divErrorUp" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:Panel ID="divError" runat="server" Style="padding: 5px" CssClass="background-color: #FFFFC0; border: solid 1px #C00000"
                                    HorizontalAlign="Left" Visible="true">
                                    <asp:Label ID="lblError" runat="server" Style="font-weight: bold; color: #800000"
                                        Text=""></asp:Label>
                                    <asp:HiddenField ID="HSelectedID" runat="server" />
                                    <asp:HiddenField ID="HActionType" runat="server" Value="VIEW" />
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </tbody>
        </table>
        <asp:UpdatePanel runat="server" ID="divComponent" UpdateMode="Conditional">
            <ContentTemplate>
                <table cellspacing="6" cellpadding="0" width="100%" border="0">
                    <tbody>
                        <tr>
                            <td class="pad5 formbg border">
                                <div id="divWardList" class="grid">
                                    <div class="mid-outer">
                                        <div class="mid-inner">
                                            <div class="mid" style="cursor: pointer; height: 280px; max-height: 480px; overflow: auto;
                                                border: 1px solid #666699;">
                                                <div id="div-gridview" class="whitebg">
                                                    <asp:GridView ID="gridWardList" CssClass="datatable table-striped table-responsive" CellPadding="0" runat="server"
                                                        AutoGenerateColumns="False" PageSize="1" BorderWidth="0px" GridLines="None" DataKeyNames="WardID"
                                                        EmptyDataText="No Data to display" Width="100%" ShowHeaderWhenEmpty="True" OnRowCommand="gridWardList_RowCommand"
                                                        OnRowDataBound="gridWardList_RowDataBound">
                                                        <Columns>
                                                            <asp:BoundField DataField="WardName" HeaderText="Ward Name" SortExpression="WardName">
                                                                <ItemStyle CssClass="textstyle" />
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="PatientCategory" HeaderText="Patient Category" SortExpression="PatientCategory">
                                                                <ItemStyle CssClass="textstyle" />
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Capacity" HeaderText="Capacity" SortExpression="Capacity">
                                                                <ItemStyle CssClass="textstyle" />
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Active" HeaderText="Status" SortExpression="Status">
                                                                <ItemStyle CssClass="textstyle" />
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <HeaderStyle CssClass="searchresultfixedheader" Height="20px" HorizontalAlign="Left">
                                                        </HeaderStyle>
                                                        <RowStyle CssClass="gridrow" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="form pad5 center" style="white-space: nowrap; text-align: center">
                                <div id="divAction" style="white-space: nowrap; text-align: center">
                                    <asp:Button ID="buttonAddWard" runat="server" Text="Add Ward" Width="80px" OnClick="AddNewWard" />
                                    <asp:Button ID="buttonCancelAddWard" runat="server" Text="Cancel" Style="display: none" />
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnCloseMain" runat="server" OnClick="btnCloseMain_Click" Text="Close" /></div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                                <br />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="buttonSubmitWard" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnOkAction" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="divDetailsCompnent" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <asp:Button ID="buttonRaiseItemPopup" runat="server" Style="display: none" />
                <asp:Panel ID="divData" runat="server" Style="display: none; width: 680px; border: solid 1px #808080;
                    background-color: #6699FF; z-index: 15000; overflow: auto;">
                    <asp:Panel ID="divTitle" runat="server" Style="border: solid 0px #808080; margin: 0px 0px 0px 0px;
                        cursor: move; height: 18px;">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 18px">
                            <tr>
                                <td valign="middle" style="font-weight: bold; padding: 5px; width: 100%; height: 19px;
                                    border: 0">
                                    <asp:Label ID="lblActionTitle" runat="server" Text="Add, Modify Wards"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <table cellpadding="1" cellspacing="2" border="0" width="680px" style="border: solid 1px #808080;
                        background-color: #CCFFFF; margin-bottom: 10px">
                        <tr>
                            <td colspan="2" align="left">
                                <i>All of the fields in this section are required.</i>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <hr class="forms">
                            </td>
                        </tr>
                        <asp:Panel ID="panelError" runat="server" Style="padding: 5px" CssClass="background-color: #FFFFC0; border: solid 1px #C00000"
                            HorizontalAlign="Left" Visible="true">
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="errorLabel" runat="server" Style="font-weight: bold; color: #800000"
                                        Text=""></asp:Label>
                                </td>
                            </tr>
                        </asp:Panel>
                        <tr>
                            <td align="left">
                                Ward Name:
                            </td>
                            <td align="left">
                                <asp:TextBox ID="textWardName" runat="server" Width="180px" MaxLength="50" AutoComplete="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                Patient Category:
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlPatientCategory" runat="server" Width="180px" AutoPostBack="false">
                                    <asp:ListItem Text="Select..." Value="" />
                                    <asp:ListItem Text="Female" Value="Female" />
                                    <asp:ListItem Text="Male" Value="Male" />
                                    <asp:ListItem Text="Peadiatric" Value="Peadiatric" />
                                    <asp:ListItem Text="All" Value="All" />
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                Bed Capacity:
                            </td>
                            <td align="left">
                                <asp:TextBox ID="textCapacity" runat="server" Width="180px" MaxLength="5" AutoComplete="false"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="fteCapacity" FilterType="Numbers" TargetControlID="textCapacity"
                                    runat="server" />
                                <ajaxToolkit:FilteredTextBoxExtender ID="filTxt" runat="server" TargetControlID="textCapacity"
                                    FilterMode="ValidChars" FilterType="Numbers">
                                </ajaxToolkit:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                Status:
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rblStatus" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1" Selected="True">Active</asp:ListItem>
                                    <asp:ListItem Value="0">InActive</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <hr style="height: 2px; color: #C0C0C0; margin: 1px; padding: 0px" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="white-space: nowrap; padding: 5px; text-align: center; padding-top: 5px;
                                padding-bottom: 5px">
                                <asp:Button ID="buttonSubmitWard" runat="server" Text="Save" Width="80px" OnClick="SaveWard" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="buttonClose" runat="server" Text="Close" Width="80px" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <ajaxToolkit:ModalPopupExtender ID="mpeWardPopup" runat="server" TargetControlID="buttonRaiseItemPopup"
                    PopupControlID="divData" BackgroundCssClass="modalBackground" DropShadow="True"
                    BehaviorID="wardpopup" PopupDragHandleControlID="divTitle" Enabled="True" DynamicServicePath=""
                    CancelControlID="buttonClose">
                </ajaxToolkit:ModalPopupExtender>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="buttonAddWard" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="gridWardList" EventName="RowCommand" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="notificationPanel" runat="server">
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
                                        <asp:Label ID="lblNotice" runat="server">Add Edit Wards</asp:Label></span>
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
                <ajaxToolkit:ModalPopupExtender ID="notifyPopupExtender" runat="server" TargetControlID="btn"
                    PopupControlID="pnNotify" BackgroundCssClass="modalBackground" DropShadow="True"
                    PopupDragHandleControlID="pnPopup_Title" Enabled="True" DynamicServicePath=""
                    CancelControlID="btnOkAction">
                </ajaxToolkit:ModalPopupExtender>
            </ContentTemplate>
            <Triggers>
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <asp:UpdateProgress ID="sProgress" runat="server" DisplayAfter="5">
        <ProgressTemplate>
            <div style="width: 100%; height: 100%; position: fixed; top: 0px; left: 0px; z-index: 300000;
                vertical-align: middle;">
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
</asp:Content>