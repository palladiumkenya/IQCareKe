<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    Inherits="IQCare.Web.Admin.ChangePassword" Title="Untitled Page" CodeBehind="frmAdmin_ChangePassword.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <%--<form id="adduser" method="post" runat="server">--%>
    <div id="unique_id" style="display: none;">
        The Password must meet the following Requirements: Minimum of 6 character length.
        Atleast one upper case letter. Atleast one numeric character. Atleast one non alpha
        character. No more than 3 consecutive characters e.g '1234' or 'abcd'. You may not
        use the word 'password',firstname,lastname,username.
    </div>
    <script language="javascript" type="text/javascript">
        function validstrngpwd(txtnewpwd, badword) {
            //var txtnewpwdid = badword.value;
            if (!validateStrongPassword('ctl00_IQCareContentPlaceHolder_txtNewPassword', { length: [6, Infinity], lower: 1, upper: 1, numeric: 1, alpha: 1, special: 1, badWords: ['password', badword], badSequenceLength: 4 })) {
                //alert(badword.toString());
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <div>
        <h3 class="margin" align="left">
            <asp:Label ID="lblh2" runat="server" Text="Change Password"></asp:Label></h3>
        <div class="center" style="padding: 5px;">
            <div class="border center formbg">
                <br>
                <table cellspacing="6" cellpadding="0" width="100%" border="0">
                    <tbody>
                        <tr>
                            <td class="border pad5 whitebg" width="50%">
                                <label class="right" for="UserName">
                                    User Name:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </label>
                                &nbsp;<asp:TextBox ID="txtUserName" runat="server" MaxLength="30"></asp:TextBox>
                            </td>
                            <td class="border pad5 whitebg" width="50%">
                                <label class="right" for="existingpwd">
                                    Existing Password:</label>
                                <asp:TextBox ID="txtExisPassword" runat="server" TextMode="Password" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="border pad5 whitebg" width="50%">
                                <label class="right" for="passverification">
                                    New Password:</label>
                                <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" MaxLength="30"></asp:TextBox>
                            </td>
                            <td class="border pad5 whitebg" width="50%">
                                <label class="right" for="password">
                                    Confirm Password:</label>
                                <asp:TextBox ID="txtConfirmpassword" runat="server" TextMode="Password" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="pad5 center" colspan="2">
                                <asp:Button ID="btnSave" runat="server" Text="Update" OnClick="btnSave_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Reset" />
                                <input id="btnExit" type="button" value="Close" runat="server" onserverclick="btnExit_ServerClick" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>