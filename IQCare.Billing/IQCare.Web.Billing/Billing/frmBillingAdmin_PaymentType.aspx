<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Module.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmBillingAdmin_PaymentType.aspx.cs"
    Inherits="IQCare.Web.Billing.ManagePaymentType" %>

<%@ MasterType VirtualPath="~/MasterPage/Module.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
   <hr />
    <div>
        <h1 class="margin" style="padding-left: 10px;">
            <asp:Label ID="lblHeader" CssClass="control-label pull-left" runat="server" Text=""></asp:Label></h1>
        <div class="center" style="padding: 5px;">
            <div class="border center">
                <table width="100%" border="0" cellpadding="0" cellspacing="6">
                    <tbody>
                        <asp:Panel ID="divError" runat="server" Style="padding: 5px" CssClass="background-color: #FFFFC0; border: solid 1px #C00000"
                            HorizontalAlign="Left" Visible="true">
                            <tr>
                                <td>
                                    <asp:Label ID="lblError" runat="server" Style="font-weight: bold; color: #800000"
                                        Text=""></asp:Label>
                                </td>
                            </tr>
                        </asp:Panel>
                        <tr>
                            <td class="pad5 formbg border" style="vertical-align: top">
                                <div class="grid">
                                    <div class="rounded">
                                        <asp:UpdatePanel runat="server" ID="updatePanelMasterItem">
                                            <ContentTemplate>
                                                <div class="mid-outer">
                                                    <div class="mid-inner">
                                                        <div class="mid">
                                                            <div id="grd_payment" class="GridView whitebg" style="cursor: pointer; height: 280px;
                                                                overflow: auto">
                                                                <asp:GridView ID="gridPaymentType" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                                                                    Width="100%" PageIndex="1" BorderWidth="0px" GridLines="None" CssClass="datatable table-striped table-responsive"
                                                                    CellPadding="0" OnRowDataBound="gridPaymentType_RowDataBound" OnSelectedIndexChanging="gridPaymentType_SelectedIndexChanging"
                                                                    OnSorting="gridPaymentType_Sorting" DataKeyNames="ID,Name">
                                                                    <Columns>                                                                      
                                                                        <asp:TemplateField HeaderText="Payment Type" SortExpression="Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="labelPayMethod" runat="server" Text='<%# Bind("Name") %>' ToolTip='<%# Bind("MethodDescription") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass="textstyle" />
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField  HeaderText="Description" SortExpression="MethodDescription">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="labelDescription" runat="server" Text='<%# Bind("MethodDescription") %>' ToolTip='<%# Bind("MethodDescription") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass="textstyle" />
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Payment Plugin" SortExpression="PluginName" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="labelHandler" runat="server" Text='<%# Bind("ControlName") %>' ToolTip='<%# Bind("MethodDescription") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass="textstyle" />
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                         
                                                                        <asp:TemplateField HeaderText="Status" SortExpression="Active">
                                                                            <ItemTemplate>
                                                                               <asp:Label ID="labelStatus" runat="server" Text='<%# (Boolean.Parse(Eval("Active").ToString())) ? "Active" : "InActive" %>' />
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass="textstyle" />
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                    <RowStyle CssClass="gridrow" />
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="buttonSubmit" EventName="Click" />
                                                <asp:AsyncPostBackTrigger ControlID="btnOkAction" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
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
                        <tr>
                            <td class="pad5 center">
                                <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Close" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <asp:UpdatePanel ID="ItemPanel" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <asp:Button ID="btnShowItems" runat="server" Text="" Width="60px" Style="display: none" />
                    <asp:Panel ID="divItems" runat="server" Style="display: none; width:680px" DefaultButton="buttonSubmit">
                        <asp:Panel ID="divItemTitle" runat="server" Style="border:margin: 0px 0px 0px 0px;cursor: move; height: 18px">
                            
                            <div class="panel panel-primary">

                                <div class="panel-heading"><span class="control-label pull-left">Payment Type Details</span><br /></div>

                                <div class="panel-body">
                                    <div class="row">
                                       <div class="col-md-12"><small class="text-danger">* All of the fields in this section are required.</small></div>  
                                       <hr />
                                      </div>

                                      <asp:Panel ID="panelError" runat="server" Style="padding: 5px" CssClass="alert alert-danger" HorizontalAlign="Left" Visible="false">
                                        <div class="col-md-12">
                                            <asp:Label ID="errorLabel" runat="server" Style="font-weight: bold; color: #800000" Text=""></asp:Label>
                                        </div>
                                     </asp:Panel>

                                     <div class="col-md-12">
                                             <asp:Label ID="labelItemMainType" runat="server" Text=""></asp:Label>
                                     </div>

                                     <div class="row" style="padding-bottom:5px">
                                        <div class="form-group">
                                            <div class="col-md-3"><label class="control-label pull-left">Name :</label></div>
                                            <div class="col-md-9">
                                                 <asp:TextBox ID="textPaymentTypeName" runat="server" CssClass="form-control input-sm" AutoComplete="off" MaxLength="100"></asp:TextBox>
                                                 <asp:HiddenField ID="prevPaymentName" runat="server" />
                                                 <asp:HiddenField ID="currentID" runat="server" Value="-1" />
                                                 <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                 FilterType="Numbers, UppercaseLetters, LowercaseLetters,Custom" TargetControlID="textPaymentTypeName" ValidChars="&-, " />
                                            </div>
                                        </div>
                                     </div><!-- .row -->

                                     <div class="row" style=" display:none" style="padding-bottom:5px">
                                        <div class="form-group">
                                            <div class="col-md-3"><label class="control-label pull-left">Short Code :</label></div>
                                            <div class="col-md-9">
                                                 <asp:TextBox ID="textPaymentTypeCode" runat="server" CssClass="form-control input-sm" AutoComplete="false"></asp:TextBox>
                                                 <asp:HiddenField ID="prevTypeCode" runat="server" />
                                            </div>
                                        </div>
                                     </div>

                                     <div class="row" style="padding-bottom:5px">
                                        <div class="form-group">
                                            <div class="col-md-3"><label class="control-label pull-left">Description :</label></div>
                                            <div class="col-md-9">
                                                 <asp:TextBox ID="textDescription" CssClass="form-control input-sm" runat="server" AutoComplete="false"
                                                TextMode="MultiLine" Rows="2" MaxLength="255"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                FilterType="Numbers, UppercaseLetters, LowercaseLetters,Custom" TargetControlID="textDescription"
                                                ValidChars="&-, " />
                                            </div>
                                        </div>
                                     </div>

                                     <div class="row" style="padding-bottom:5px;display:none">
                                        <div class="form-group">
                                            <div class="col-md-3"><label class="control-label pull-left">Priority :</label></div>
                                            <div class="col-md-9">
                                                 <asp:TextBox ID="txtSeqNo" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                    TargetControlID="txtSeqNo" FilterType="Numbers" FilterMode="ValidChars" />
                                            </div>
                                        </div>
                                     </div>

                                     <div class="row" style="padding-bottom:5px;display:none">
                                        <div class="form-group">
                                            <div class="col-md-3"><label class="control-label pull-left">Handler Name :</label></div>
                                            <div class="col-md-9">
                                                 <asp:HiddenField ID="prevPluginName" runat="server" />
                                            </div>
                                        </div>
                                     </div>

                                    <div class="row" style="padding-bottom:5px">
                                        <div class="form-group">
                                            <div class="col-md-3"><label class="control-label pull-left">Status :</label></div>
                                            <div class="col-md-9">
                                                   <asp:RadioButtonList ID="rblStatus" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1" Selected="True"> Active</asp:ListItem>
                                                        <asp:ListItem Value="0"> InActive</asp:ListItem>
                                                    </asp:RadioButtonList>
                                            </div>
                                        </div>
                                     </div>
                                     <div class="col-md-12"><hr /></div>
                                     <div class="row">
                                          <asp:Button ID="buttonSubmit" CssClass="btn btn-info" runat="server" Text="Save" Width="120px" OnClick="buttonSubmit_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                           <asp:Button ID="buttonClose"  CssClass="btn btn-danger" runat="server" Text="Close" Width="120px" />
                                     </div>

                                    


                                
                                </div>

                            </div><!-- .panel -->


                            
<%--                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 18px">
                                <tr>
                                    <td style="width: 5px; height: 19px;">
                                    </td>
                                    <td style="width: 100%; height: 19px;">
                                        <span style="font-weight: bold;">
                                            <asp:Label ID="labelItemTitle" runat="server">Payment Type Details</asp:Label></span>
                                    </td>
                                    <td style="width: 5px; height: 19px;">
                                    </td>
                                </tr>
                            </table>--%>
                        </asp:Panel>
                       <%-- <table cellpadding="1" cellspacing="1" border="0" width="680px" style="border: solid 1px #808080;--%>
<%--                            background-color: #CCFFFF; margin-bottom: 10px">
                            <tr>
                                <td colspan="2" align="left">
                                    <i>All of the fields in this section are required.</i>
                                </td>
                            </tr>--%>
<%--                            <asp:Panel ID="panelError" runat="server" Style="padding: 5px" CssClass="background-color: #FFFFC0; border: solid 1px #C00000"
                                HorizontalAlign="Left" Visible="true">
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="errorLabel" runat="server" Style="font-weight: bold; color: #800000"
                                            Text=""></asp:Label>
                                    </td>
                                </tr>
                            </asp:Panel>--%>
<%--                            <tr>
                                <td colspan="2">
                                    <hr class="forms">
                                </td>
                            </tr>--%>
<%--                            <tr>
                                <td align="left">
                                </td>
                                <td valign="top" colspan="1" style="font-weight: bold; padding: 3px" align="left">
                                    <asp:Label ID="labelItemMainType" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>--%>
<%--                            <tr>
                                <td align="left" style="font-weight: bold;">
                                    Name:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="textPaymentTypeName" runat="server" Width="200px" AutoComplete="off"
                                        MaxLength="100"></asp:TextBox>
                                    <asp:HiddenField ID="prevPaymentName" runat="server" />
                                    <asp:HiddenField ID="currentID" runat="server" Value="-1" />
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                        FilterType="Numbers, UppercaseLetters, LowercaseLetters,Custom" TargetControlID="textPaymentTypeName"
                                        ValidChars="&-, " />
                                </td>
                            </tr>--%>
                           
<%--                            <tr style="display: none">
                                <td align="left" style="font-weight: bold;">
                                    Short Code:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="textPaymentTypeCode" runat="server" Width="180px" AutoComplete="false"></asp:TextBox>
                                    <asp:HiddenField ID="prevTypeCode" runat="server" />
                                </td>
                            </tr>--%>
<%--                            <tr>
                                <td align="left" style="font-weight: bold;">
                                    Description:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="textDescription" runat="server" Width="200px" AutoComplete="false"
                                        TextMode="MultiLine" Rows="2" MaxLength="255"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                        FilterType="Numbers, UppercaseLetters, LowercaseLetters,Custom" TargetControlID="textDescription"
                                        ValidChars="&-, " />
                                </td>
                            </tr>--%>
<%--                            <tr style="display:none">
                                <td align="left" style="font-weight: bold;">
                                    Priority :
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtSeqNo" Width="40px" runat="server"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                        TargetControlID="txtSeqNo" FilterType="Numbers" FilterMode="ValidChars" />
                                </td>
                            </tr>--%>
<%--                            <tr style="display:none">
                                <td align="left" style="font-weight: bold;">
                                    Handler Name :
                                </td>
                                <td align="left">
                                    <%--<asp:DropDownList ID="ddlHandler" runat="server" Width="200px">
                                    </asp:DropDownList>--%>
                                   <%-- <asp:HiddenField ID="prevPluginName" runat="server" />--%>
                           <%--     </td>
                            </tr>--%>
<%--                            <tr>
                                <td align="left" style="font-weight: bold;">
                                    Status:
                                </td>
                                <td align="left">
                                    <asp:RadioButtonList ID="rblStatus" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="1" Selected="True">Active</asp:ListItem>
                                        <asp:ListItem Value="0">InActive</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>--%>
<%--                            <tr>
                                <td colspan="2">
                                    <hr style="height: 2px; color: #C0C0C0; margin: 1px; padding: 0px" />
                                </td>
                            </tr>--%>
<%--                            <tr>
                                <td colspan="2" style="white-space: nowrap; padding: 5px; text-align: center; padding-top: 5px;
                                    padding-bottom: 5px">
                                    <asp:Button ID="buttonSubmit" CssClass="btn btn-info" runat="server" Text="Save" Width="120px" OnClick="buttonSubmit_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="buttonClose"  CssClass="btn btn-danger" runat="server" Text="Close" Width="120px" />
                                </td>
                            </tr>
                        </table>--%>
                    </asp:Panel>
                    <ajaxToolkit:ModalPopupExtender ID="paymentTypePopup" runat="server" BehaviorID="ptpBehavior"
                        TargetControlID="btnShowItems" PopupControlID="divItems" BackgroundCssClass="modalBackground"
                        CancelControlID="buttonClose" DropShadow="true" PopupDragHandleControlID="divItemTitle">
                    </ajaxToolkit:ModalPopupExtender>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="gridPaymentType" EventName="RowCommand" />
                    <asp:AsyncPostBackTrigger ControlID="gridPaymentType" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
           
        </div>
      
    </div>
</asp:Content>
