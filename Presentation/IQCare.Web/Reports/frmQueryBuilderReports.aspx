<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="IQCare.Web.Reports.frmQueryBuilderReports"
    Title="Untitled Page" Codebehind="frmQueryBuilderReports.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
   <%-- <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>--%>
    <div style="padding-left: 5px; padding-right: 5px; padding-top: 0px; width: 950">
        <div class="nomargin">
            <h2 class="nomargin">
                QueryBuilder Reports
            </h2>
        </div>
        <div class="border center formbg" style="width: 950">
            <asp:UpdatePanel ID="panelFilter" runat="server" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <table width="100%" border="0">
                        <tbody>
                            <tr>
                                <td style="width: 10%; white-space: nowrap" align="right">
                                    Select Category:
                                </td>
                                <td style="width: 30%" align="left">
                                    <asp:DropDownList ID="ddlCategory" Width="180px" runat="server" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 10%; white-space: nowrap" align="right">
                                    Select Report:
                                </td>
                                <td style="width: 60%" align="left">
                                    <asp:DropDownList ID="ddlReport" Width="100%" runat="server" Height="30px">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Button ID="btnGenerate" runat="server" Text="View" OnClick="btnGenerate_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5" style="height: 5px;">
                                    &nbsp;
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <asp:Panel ID="divError" runat="server" Style="padding: 5px" CssClass="background-color: #FFFFC0; border: solid 1px #C00000"
                        HorizontalAlign="Left" Visible="false">
                        <asp:Label ID="lblError" runat="server" Style="font-weight: bold; color: #800000"
                            Text=""></asp:Label>
                            <asp:HiddenField ID="queryString" runat="server"/>
                            <asp:HiddenField ID="thetableName" runat="server"/>
                    </asp:Panel>
                    <asp:Panel ID="pnlNoData" runat="server" Visible="false">
                        <span style="color: #600000; font-weight: bold;">There is no data at the moment for
                            the selected report </span>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="panelResult" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="mid-outer">
                        <div class="mid-inner">
                            <div class="mid" style="border: 1px solid #666699;">
                                <div class="whitebg" id="div-gridview" style="width: 950; overflow: auto; margin-top: 5px;
                                    margin-bottom: 5px; max-height: 480px; height: 280px">
                                    <asp:GridView ID="gridResult" runat="server" CellPadding="0" CellSpacing="0" EnableModelValidation="True"
                                        BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" Width="100%" UseAccessibleHeader="true"
                                        AllowSorting="false" GridLines="Both" CssClass="datatable" BorderColor="#99CCFF"
                                        EmptyDataText="The report returned no data">
                                        <HeaderStyle CssClass="searchresultfixedheader" Height="20px" HorizontalAlign="Left"
                                            Wrap="False" />
                                        <RowStyle CssClass="gridrow" HorizontalAlign="Left" />
                                    </asp:GridView>                                    
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <asp:Button ID="btnRaisePopup" runat="server" Text="Save" Width="60px" Style="display: none" />
                    <asp:Panel ID="divParameters" runat="server" Style="display: none; width: 420px; border: solid 1px #808080;
                        background-color: #F0F0F0">
                        <asp:Panel ID="divTitle" runat="server" Style="border: solid 1px #808080;
                            margin: 0px 0px 0px 0px; cursor: move; height: 18px">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 18px">
                                <tr>
                                    <td style="width: 5px">
                                        
                                    </td>
                                    <td style="width: 100%;">
                                        Specify the following values for filtering
                                    </td>
                                    <td style="width: 5px">
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <br />
                        <br />
                        <asp:GridView ID="gridParameter" runat="server" EnableModelValidation="True" CssClass="datatable"
                                        AutoGenerateColumns="False" DataKeyNames="ParameterName" Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="ParameterName" HeaderText="Name" ReadOnly="True" />
                                            <asp:BoundField DataField="DataType" HeaderText="Type" ReadOnly="True" />
                                            <asp:TemplateField HeaderText="Value">
                                                <ItemTemplate>
                                                    <asp:TextBox runat="server" ID="paramValue"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                        </Columns>
                                    </asp:GridView>
                        <br />
                        <br />
                        <div style="background-color: #FFFFFF; border-top: solid 1px #808080; width: 100%;
                            text-align: center; padding-top: 5px; padding-bottom: 5px">
                            <asp:Button ID="btnActionOK" runat="server" Text="Continue" Width="80px" />
                            <asp:Button ID="btnActionCancel" runat="server" Text="Cancel" Width="80px" />
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:ModalPopupExtender ID="parameterPopup" runat="server" BehaviorID="programmaticModalPopupBehavior"
                        TargetControlID="btnRaisePopup" PopupControlID="divParameters" BackgroundCssClass="modalBackground"
                        CancelControlID="btnActionCancel" DropShadow="true" PopupDragHandleControlID="divTitle">
                    </ajaxToolkit:ModalPopupExtender>
                    <table width="100%">
                        <tr>
                            <td align="center" colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnExport" runat="server" Text="Export To Excel" OnClick="btnExport_Click" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnGenerate" EventName="Click" />
                    <asp:PostBackTrigger ControlID="btnExport" />
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
    </div>
</asp:Content>
