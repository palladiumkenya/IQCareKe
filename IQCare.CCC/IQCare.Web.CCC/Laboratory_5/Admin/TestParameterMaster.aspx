<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    CodeBehind="TestParameterMaster.aspx.cs" Inherits="IQCare.Web.Laboratory.Admin.TestParameterMaster" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>--%>
<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<%--<%@ Register Src="../../progressControl.ascx" TagName="progressControl" TagPrefix="uc1" %>--%>
<asp:Content ID="ctMain" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
  <script src="../../Incl/quicksearch.js" type="text/javascript" defer="defer"></script>
    <script type="text/javascript">
//        function FilterGrid() {
//            $('.search_textbox').each(function (i) {
//                $(this).quicksearch("[id*=gridParamMaster] tr:not(:has(th))", {
//                    'testQuery': function (query, txt, row) {
//                        return $(row).children(":eq(" + i + ")").text().toLowerCase().indexOf(query[0].toLowerCase()) != -1;
//                    }
//                });
//            })
//        };
//        var prm = Sys.WebForms.PageRequestManager.getInstance();
//        prm.add_pageLoaded(FilterGrid);
    </script>
    <div>

        <div class="row ">
        <i class="fa fa-cogs fa-3x pull-left" aria-hidden="true"></i><span class="text-capitalize pull-left glyphicon-text-size= fa-2x">
           Tests Parameters for &nbsp;<asp:Label ID="labelTestName" runat="Server" Text=""></asp:Label></span>
            <asp:HiddenField ID="hLabTestId" runat="Server" Value="-1" /></span>
    </div>
    <hr />

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
                                                    <asp:UpdatePanel ID="divGridComponent" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:GridView ID="gridParamMaster" runat="server" AutoGenerateColumns="False" Width="100%"
                                                                AllowSorting="True" BorderWidth="0px" GridLines="None" 
                                                                CssClass="datatable" DataKeyNames="Id"
                                                                CellPadding="0"  onrowcommand="gridParamMaster_RowCommand" 
                                                                onrowdatabound="gridParamMaster_RowDataBound" 
                                                                ondatabound="gridParamMaster_DataBound">
                                                                <Columns>
                                                                    <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="True" SortExpression="Name">
                                                                        <HeaderStyle Font-Underline="False" />
                                                                        <ItemStyle CssClass="textstyle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="ReferenceId" HeaderText="Reference Name" SortExpression="ReferenceId"
                                                                        Visible="False">
                                                                        <ItemStyle CssClass="textstyle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="DataType" HeaderText="Data Type" ReadOnly="True" SortExpression="DataType">
                                                                        <ItemStyle CssClass="textstyle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Rank" HeaderText="Rank/Position" ReadOnly="True" SortExpression="Rank" DataFormatString="{0:N}">
                                                                        <ItemStyle CssClass="textstyle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="LoincCode" HeaderText="Loinc Code" SortExpression="LoincCode" />
                                                                    <asp:BoundField DataField="DeleteFlag" HeaderText="Status" SortExpression="DeleteFlag">
                                                                        <ItemStyle CssClass="textstyle" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField>
                                                                        <ItemStyle CssClass="textstyle" />
                                                                        <ItemTemplate>
                                                                            <div style="white-space: nowrap">                                                                               
                                                                                    <asp:Button ID="buttonEdit" runat="server" CausesValidation="false" CssClass="btn btn-default" CommandName="Modify" Text="View | Modify" CommandArgument="<%# Container.DataItemIndex %>">
                                                                                    </asp:Button>
                                                                                    <span style='display: <%= svDelete %>; white-space: nowrap'>
                                                                                <asp:Button ID="buttonDelete" runat="server" CausesValidation="false" CssClass="btn btn-danger" CommandName="DeleteParam"
                                                                                    Text="Make Inactive" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' >
                                                                                </asp:Button></span>
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
                                                                                        <table border="0" cellpadding="15" cellspacing="0" style="width: 100%; height: 18px">
                                                                                            <tr>
                                                                                                <td style="width: 48px" valign="middle" align="center">
                                                                                                    <img src="../../Images/mb_question.gif" alt="" width="32" height="32" />
                                                                                                </td>
                                                                                                <td style="width: 100%; padding-left: 20px" valign="middle" align="left">
                                                                                                    You are about delete
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "Name")%>. &nbsp;<br /> Are you sure you want to proceed?
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </div>
                                                                                    <div style="padding: 3px;" align="center">
                                                                                        <asp:Button ID="btnInactiveYes" runat="server" Text=" Yes " ForeColor="DarkGreen" />
                                                                                        <asp:Button ID="btnInactiveNo" runat="server" Text="Cancel" ForeColor="DarkBlue"
                                                                                            Style="margin-left: 10px" /></div>
                                                                                </asp:Panel>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                <RowStyle CssClass="gridrow" />
                                                            </asp:GridView>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
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
                            <asp:Button ID="btnAdd" CssClass="btn btn-primary" runat="server" Text="Add Parameter" />
                            <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Close Window " />
                        </td>
                    </tr>
                </tbody>
            </table>
          
        </div>
    </div>
</asp:Content>
