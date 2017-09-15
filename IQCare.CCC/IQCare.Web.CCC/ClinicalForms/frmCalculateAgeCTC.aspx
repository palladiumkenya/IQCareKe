<%@ Page Language="C#" AutoEventWireup="True"
    Inherits="IQCare.Web.Clinical.CalculateAgeCTC" Codebehind="frmCalculateAgeCTC.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title id="lblHeader" runat="server">Calculate Date of Birth</title>
    <link rel="Stylesheet" type="text/css" href="../Style/styles.css" />
</head>
<body>
    <form id="CTC" method="post" runat="server">
    <script language="javascript" type="text/javascript">
        function CheckBlank() {
            if (document.getElementById('<%=txtAge.ClientID %>').value == "") {
                alert("Please Enter Age");
                return false;
            }
        }
        function PostPage() {
            window.opener.document.getElementById('ctl00_IQCareContentPlaceHolder_TxtAgeEnrollmentYears').value = document.getElementById('<%=txtAge.ClientID %>').value;
            window.opener.document.getElementById('ctl00_IQCareContentPlaceHolder_TxtAgeCurrentYears').value = document.getElementById('<%=txtRegAge.ClientID %>').value;
            window.opener.document.getElementById('ctl00_IQCareContentPlaceHolder_TxtDOB').value = document.getElementById('<%=txtDOB.ClientID %>').value;
            window.close();
        }



    </script>
    <div class="border center formbg" style="width: 262px; height: 133px; padding-left: 5px;
        padding-right: 5px; padding-top: 5px; padding-bottom: 5px">
        <table class="left" width="100%" border="0" style="padding-top: 5px;">
            <tbody>
                <tr>
                    <td class="border pad5 whitebg">
                        <label style="margin-left: 49px">
                            Age:</label>
                        <asp:TextBox ID="txtAge" runat="server" Width="100px" />
                    </td>
                </tr>
                <tr>
                    <td class="border pad5 whitebg" style="width: 50%">
                        <label>
                            Date of Birth:</label>
                        <asp:TextBox ID="txtDOB" runat="server" Width="100px" ReadOnly="true" />
                        <asp:HiddenField ID="txtRegAge" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="border pad5 whitebg" colspan="2">
                        <input id="btnCalculate" type="button" value="Calculate DOB" runat="server" onserverclick="btnCalculate_ServerClick" />
                        <input id="btnClose" type="button" value="Close" runat="server" onclick="PostPage()" />
                    </td>
                </tr>
                <tr>
                </tr>
            </tbody>
        </table>
    </div>
    </form>
</body>
</html>
