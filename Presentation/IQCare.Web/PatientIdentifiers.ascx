<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PatientIdentifiers.ascx.cs"
    Inherits="IQCare.Web.PatientIdentifiers" %>
<asp:Panel ID="IdentifiersPanel" runat="server" Style="width: 100%;">
    <asp:UpdatePanel ID="upIdentifiers" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
           <asp:HiddenField runat="server" ID="ModuleID"  />
    <asp:HiddenField runat="server" ID="PatientID"  />
    <asp:Repeater ID="rptServiceArea" runat="server">
        <HeaderTemplate>
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td class="border center pad5 whitebg" style="width: 20%;">
                    <asp:Label ID="IdentifierLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Identifier") %>'></asp:Label>
                    <asp:HiddenField runat="server" ID="IdentifierType" Value='<%# DataBinder.Eval(Container.DataItem, "IdentifierType") %>' />
                </td>
                <td style="width: 30%">
                    <asp:TextBox ID="IdentifierValueText" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IdentifierValue") %>' Width="180" MaxLength="50"></asp:TextBox>
                    <asp:Label ID="IdentifierValueLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IdentifierValue") %>' Font-Bold="true"></asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <td class="border center pad5 whitebg" style="width: 20%;">
                <asp:Label ID="IdentifierLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Identifier") %>'></asp:Label>
                    <asp:HiddenField runat="server" ID="IdentifierType" Value='<%# DataBinder.Eval(Container.DataItem, "IdentifierType") %>' />
            </td>
            <td style="width: 30%">
                  <asp:TextBox ID="IdentifierValueText" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IdentifierValue") %>' Width="180" MaxLength="50"></asp:TextBox>
                    <asp:Label ID="IdentifierValueLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IdentifierValue") %>' Font-Bold="true"></asp:Label>
            </td>
            <td>
                &nbsp;
            </td>
            </tr>
        </AlternatingItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
        </ContentTemplate>
    </asp:UpdatePanel>
 
</asp:Panel>
