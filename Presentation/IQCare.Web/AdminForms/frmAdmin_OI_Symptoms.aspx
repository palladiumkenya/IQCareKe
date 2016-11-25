<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="IQCare.Web.Admin.OISymptoms" Codebehind="frmAdmin_OI_Symptoms.aspx.cs" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <%--<form id="addeditDisease" method="post" runat="server">--%>
    <div>
      <%--  <asp:ScriptManager ID="mst" runat="server">
        </asp:ScriptManager>--%>
        <asp:UpdatePanel ID="UpdateOIAIDSIllness" runat="server">
            <ContentTemplate>
                <h3 class="margin" align="left">
                    <asp:Label ID="lblHeader" runat="server" Text=""></asp:Label></h3>
                <div class="border center formbg">
                    <br>
                    <table cellspacing="6" cellpadding="0" width="100%" border="0">
                        <tbody>
                            <tr>
                                <td class="border center pad5 whitebg" width="50%" nowrap="nowrap">
                                    <asp:Label ID="lblName" runat="server" Text="" Font-Bold="true"></asp:Label>
                                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                                </td>
                                <td class="border center pad5 whitebg" width="50%">
                                    <asp:Label ID="lblPriority" runat="server" Text="Priority :" Font-Bold="true"></asp:Label>
                                    <asp:TextBox ID="txtSeqNo" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="border pad5 whitebg" width="100%" nowrap="nowrap" colspan="2">
                                    <div class="treeview">
                                        <asp:TreeView ID="TVICD10" runat="server" NodeWrap="True" PopulateNodesFromClient="true"
                                            EnableClientScript="true" OnSelectedNodeChanged="TVICD10_SelectedNodeChanged">
                                        </asp:TreeView>
                                    </div>
                                </td>
                                <td class="border pad5 whitebg" width="10%" nowrap="nowrap" style="display: none">
                                    <div class="treeview">
                                        <asp:CheckBoxList ID="cblModuleName" RepeatLayout="Flow" runat="server">
                                        </asp:CheckBoxList>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td id="tdStatus" runat="server" class="border center pad5 whitebg" width="50%">
                                    <asp:Label ID="lblStatus" runat="server" Text="Status :" Font-Bold="true"></asp:Label>
                                    <asp:DropDownList ID="ddStatus" runat="server">
                                        <asp:ListItem Value="0">Active</asp:ListItem>
                                        <asp:ListItem Value="1">Inactive</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td id="td1" runat="server" class="border center pad5 whitebg" width="50%">
                                    <asp:Label ID="lblType" runat="server" Text="Form List Type :" Font-Bold="true"></asp:Label>
                                    <asp:DropDownList ID="ddType" runat="server">
                                        <asp:ListItem Value="1">Disease</asp:ListItem>
                                        <asp:ListItem Value="0">Symptom</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <table width="100%">
                        <tbody>
                            <tr>
                                <td align="center">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Reset" Visible="false" />
                                    <asp:Button ID="btnExit" runat="server" Text="Close" OnClick="btnExit_Click" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <br />
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSave"></asp:PostBackTrigger>
                <asp:PostBackTrigger ControlID="btnCancel"></asp:PostBackTrigger>
                <asp:PostBackTrigger ControlID="btnExit"></asp:PostBackTrigger>
                <asp:PostBackTrigger ControlID="TVICD10"></asp:PostBackTrigger>
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
