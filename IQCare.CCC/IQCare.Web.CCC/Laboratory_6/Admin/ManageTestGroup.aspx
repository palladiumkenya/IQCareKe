<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    CodeBehind="ManageTestGroup.aspx.cs" Inherits="IQCare.Web.Laboratory.Admin.ManageTestGroup" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>--%>
<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<%--<%@ Register Src="../../progressControl.ascx" TagName="progressControl" TagPrefix="uc1" %>--%>
<asp:Content ID="ctMain" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <script language="javascript" type="text/javascript">
        function WindowPrint() {
            window.print();
        }
        function ace1_itemSelected(source, e) {
            var hdCustID = $get('<%= hdCustID.ClientID %>');
            hdCustID.value = e.get_value();

        }
        function onClientPopulated(sender, e) {

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
    <div>
        <h3 class="margin" align="left">
            <asp:Label ID="lblH2" runat="server" Text="Manage Group Tests For: "></asp:Label>
            <asp:Label ID="labelTestName" runat="Server" Text=""></asp:Label>
            <asp:HiddenField ID="hMainLabTestId" runat="Server" Value="-1" />
            <asp:HiddenField ID="hdCustID" runat="server" />
        </h3>
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
            <asp:UpdatePanel ID="divTestComponent" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="border pad5" id="divSelectLab" style='display: <%= svEdit %>'>
                        <table cellspacing="6" cellpadding="0" style="margin-top: 10px" width="100%" border="0">
                            <tbody>
                                <tr>
                                    <td colspan="1" style="text-align: left">
                                        <label class="" style="margin-left: 10px" for="textSelectLab">
                                            Select Lab:</label>
                                        <asp:TextBox ID="textSelectLab" Width="400px" runat="server" AutoPostBack="true"
                                            Height="20px" AutoComplete="off" Font-Names="Courier New" OnTextChanged="LabNameChanged"
                                            Font-Size="Medium"></asp:TextBox>
                                        <div id="divwidth" style="text-align: left">
                                        </div>
                                        <ajaxToolkit:AutoCompleteExtender ServiceMethod="Searchlab" MinimumPrefixLength="2"
                                            CompletionInterval="30" EnableCaching="false" CompletionSetCount="10" TargetControlID="textSelectLab"
                                            BehaviorID="AutoCompleteEx" OnClientItemSelected="ace1_itemSelected" ID="AutoCompleteExtender1"
                                            runat="server" FirstRowSelected="false" CompletionListCssClass="autoextender"
                                            CompletionListItemCssClass="autoextenderlist" CompletionListHighlightedItemCssClass="autoextenderhighlight"
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
                                    </td>
                                    <td>
                                        <div id="divAction" style="white-space: nowrap; border-top: 0px solid #ddd; text-align: left;
                                            display: <%= sDataEntry %>; margin-top: 10px">
                                            <span>&nbsp;&nbsp;&nbsp;<asp:Button ID="btnAddLabRecord" runat="server" Text="Add"
                                                Width="120px" CausesValidation="true" ValidationGroup="df_x" OnClick="AddLabRecord"
                                                ForeColor="Black" />
                                                &nbsp;&nbsp;&nbsp;</span>
                                            <asp:Button ID="buttonCancel" runat="server" Text="Cancel" Width="80px" OnClick="CancelLabEntry"
                                                ForeColor="Black" />
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="textSelectLab" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="btnAddLabRecord" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <table width="100%" border="0" cellpadding="0" cellspacing="6">
                <tbody>
                    <tr>
                        <td class="border pad5 formbg">
                            <asp:UpdatePanel ID="divGridComponent" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="grid">
                                        <div class="rounded">
                                            <div class="mid-outer">
                                                <div class="mid-inner">
                                                    <div class="mid" style="height: 300px; overflow: auto">
                                                        <div id="grd_custom" class="GridView whitebg">
                                                            <asp:GridView ID="gridlabMaster" runat="server" OnRowDataBound="gridlabMaster_RowDataBound"
                                                                AutoGenerateColumns="False" Width="100%" AllowSorting="False" BorderWidth="0px"
                                                                GridLines="None" CssClass="datatable" DataKeyNames="Id" CellPadding="0" OnSorting="grdLab_Sorting"
                                                                OnRowCommand="gridlabMaster_RowCommand">
                                                                <Columns>
                                                                    <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="True" SortExpression="Name">
                                                                        <HeaderStyle Font-Underline="False" />
                                                                        <ItemStyle CssClass="textstyle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="ReferenceId" HeaderText="Reference Name" SortExpression="ReferenceId">
                                                                        <ItemStyle CssClass="textstyle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="DepartmentName" HeaderText="Department" ReadOnly="True"
                                                                        SortExpression="DepartmentName">
                                                                        <ItemStyle CssClass="textstyle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="ParameterCount" HeaderText="# Parameter" SortExpression="ParameterCount" />
                                                                    <asp:BoundField DataField="DeleteFlag" HeaderText="Status" SortExpression="DeleteFlag">
                                                                        <ItemStyle CssClass="textstyle" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField>
                                                                        <ItemStyle CssClass="textstyle" />
                                                                        <ItemTemplate>
                                                                            <div style="white-space: nowrap">
                                                                                <span style='display: none; white-space: nowrap'>
                                                                                    <asp:Button ID="buttonEdit" runat="server" CausesValidation="false" Height="21px"
                                                                                        ForeColor="Blue" CommandName="Modify" Text="Modify" CommandArgument="<%# Container.DataItemIndex %>">
                                                                                    </asp:Button></span> <span style='display: <%= svDelete %>; white-space: nowrap'>
                                                                                        <asp:Button ID="buttonDelete" runat="server" CausesValidation="false" CommandName="RemoveFromGroup"
                                                                                            Text="Remove" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
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
                                                                                                    You are about remove
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "Name")%>. from the group&nbsp;<br />
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
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnAddLabRecord" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td class="pad5 center">
                            <asp:Button ID="btnExit" runat="server" Text="Close" OnClick="ExitPage" CausesValidation="false" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
