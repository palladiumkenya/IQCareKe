<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="IQCare.Web.Admin.Export" Title="Untitled Page" Codebehind="frmAdmin_Export.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">

    <div>
        <h1 class="margin" style="padding-left: 10px;">
            Export Data</h1>
        <script language="javascript" type="text/javascript">
            function GetControl() {
                document.forms[0].submit();
            }
        </script>
        <div class="center" style="padding: 5px;">
            <div class="border center formbg">
                <table class="bold" width="100%" cellpadding="0" cellspacing="0">
                    <tr class="border pad5 whitebg">
                        <td valign="top" width="20%">
                            <label>
                                Select Field Groups:</label>
                            <asp:Button ID="btnSelectGroup" runat="server" Text="..." OnClick="btnSelectGroup_Click" />
                        </td>
                        <td width="80%" align="left">
                            <asp:ListBox ID="lstAvailable" runat="server" Height="180px" Width="210px"></asp:ListBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="border pad5 whitebg" align="left">
                            <label>
                                De-Identify Data:</label>
                            <asp:CheckBox ID="ChkIdentity" runat="server" Checked="true" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <label>
                                Excel:</label>
                            <asp:RadioButton ID="rdoExcel" runat="server" GroupName="rdoExport" Checked="true" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <!--<label>CSV:</label>
            <asp:RadioButton id="rdoCSV" runat="server" GroupName="rdoExport"/>-->
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:Button ID="btnExport" runat="server" Text="Export" OnClick="btnExport_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnExit_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DataGrid ID="dg1" runat="server">
                            </asp:DataGrid>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
