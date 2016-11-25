<%@ Page Language="C#" AutoEventWireup="True" MasterPageFile="~/MasterPage/IQCare.master" Inherits="IQCare.Web.Admin.VillageChairperson" Codebehind="frmAdmin_VillageChairperson.aspx.cs" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <br />
    <%-- <form id="regiondistrict" method="post" runat="server" enableviewstate="true" title="">--%>
    <div>
        <script language="javascript" type="text/javascript">
            function fnValidateChairperson() {
                if (document.getElementById('<%=ddlRegion.ClientID %>').value == 0) {
                    alert("Please select Region")
                    return false;
                }
                if (document.getElementById('<%=ddDistric.ClientID %>').value == 0) {
                    alert("Please select District")
                    return false;
                }
                if (document.getElementById('<%=ddWard.ClientID %>').value == 0) {
                    alert("Please select ward")
                    return false;
                }
                if (document.getElementById('<%=ddlVillage.ClientID %>').value == 0) {
                    alert("Please select village")
                    return false;
                }
                if (document.getElementById('<%=ddlVillage.ClientID %>').value != 0) {
                    if (document.getElementById('<%=txtChairPerson.ClientID %>').value == "") {
                        alert("Chairperson Name can't be blank")
                        return false;
                    }
                }
                return true;
            }
        </script>
        <%--<asp:ScriptManager ID="mst" runat="server">
        </asp:ScriptManager>--%>
        <h3>
            Village-Chairperson Mapping</h3>
        <asp:UpdatePanel ID="UpdateMasterLink" runat="server">
            <ContentTemplate>
                <div class="border center formbg" style="padding-bottom: 1%">
                    <table class="center" cellspacing="6" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="border pad5 whitebg" valign="top" width="50%">
                                <label class="right40" for="Region">
                                    Region:</label>&nbsp;<asp:DropDownList ID="ddlRegion" runat="server" AutoPostBack="true"
                                        Width="141px" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                                    </asp:DropDownList>
                            </td>
                            <td class="border pad5 whitebg" valign="top" width="50%">
                                <label class="right" for="Distric">
                                    District:</label>
                                <asp:DropDownList ID="ddDistric" runat="server" AutoPostBack="true" Width="141px"
                                    OnSelectedIndexChanged="ddDistric_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="border pad5 whitebg" valign="top" width="50%">
                                <label class="right40" for="Ward">
                                    Ward:</label>&nbsp;<asp:DropDownList ID="ddWard" runat="server" AutoPostBack="true"
                                        Width="141px" OnSelectedIndexChanged="ddWard_SelectedIndexChanged">
                                    </asp:DropDownList>
                            </td>
                            <td class="border pad5 whitebg" valign="top" width="50%">
                                <label class="right" for="Village">
                                    Village:</label>
                                <asp:DropDownList ID="ddlVillage" AutoPostBack="true" runat="server" Width="141px"
                                    OnSelectedIndexChanged="ddlVillage_SelectedIndexChanged1">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="border pad5 whitebg" align="center" colspan="2" valign="top" width="100%">
                                <label for="ChairPerson">
                                    Chairperson Name:</label>
                                <asp:TextBox ID="txtChairPerson" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="pad5 center" valign="top" colspan="2">
                                <asp:Button ID="btnSave" OnClick="btnSave_Click" OnClientClick="return fnValidateChairperson();"
                                    runat="server" Text="Save" />
                                <asp:Button ID="btnCancel1" runat="server" Text="Cancel" OnClick="btnCancel1_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
