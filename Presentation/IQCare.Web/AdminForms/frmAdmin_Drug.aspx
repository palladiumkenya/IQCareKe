<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    Inherits="IQCare.Web.Admin.ManageDrug" Title="Untitled Page" CodeBehind="frmAdmin_Drug.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <div>
        <h3 class="margin" align="left" style="padding-left: 10px;">
            <asp:Label ID="lblH2" runat="server"></asp:Label></h3>
        <script language="javascript" type="text/javascript">
            function GetControl() {

                document.forms[0].submit();
            }
            function EnabDisabButton(QueryString) {
                var btnadddose = document.getElementById("<%=btnAddDose.ClientID%>");
                var btnaddfrequency = document.getElementById("<%=btnAddFrequency.ClientID%>");
                if (document.getElementById("<%=txtDrugName.ClientID%>").value != "") {
                    if (btnadddose != null && btnaddfrequency != null) {
                        btnadddose.disabled = true;
                        btnaddfrequency.disabled = true;
                    }

                }
                else {
                    var count;

                    count = document.getElementById("<%=lstGeneric.ClientID%>").options.length;
                    if (count < 2) {
                        if (QueryString == "Edit") {                            
                            if (btnadddose != null && btnaddfrequency != null) {
                                btnadddose.disabled = true;
                                btnaddfrequency.disabled = true;
                            }
                            
                        }
                       
                    }
                    else {
                        if (btnadddose != null && btnaddfrequency != null) {
                            btnadddose.disabled = true;
                            btnaddfrequency.disabled = true;
                        }
                        
                    }
                }
            }
        </script>
        <div class="center" style="padding: 15px;">
            <div class="border center formbg">
                <table cellspacing="6" cellpadding="0" width="100%" border="0">
                    <tbody>
                        <tr>
                            <td class="form" style="width: 50%; height: 141px" align="left">
                                <table width="100%" cellpadding="5px" cellspacing="5px">
                                    <tr>
                                        <td style="width: 35%;" align="right">
                                            <label class="right30">
                                                Drug type :</label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddDrugTypeID" runat="server" OnSelectedIndexChanged="ddDrugType_SelectedIndexChanged"
                                                AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <label class="right30">
                                                Trade name :</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDrugName" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <label class="right30">
                                                Drug abbreviation :</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDrugAbbre" ReadOnly="true" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="border center pad5 whitebg" style="width: 50%; height: 141px" align="left">
                                <label class="right30">
                                    Generic name :</label>
                                <asp:ListBox ID="lstGeneric" runat="server" Height="100px" Width="300px"></asp:ListBox>
                                <br />
                                <asp:Button ID="btnAddGeneric" runat="server" Text="Add Generic Drug" OnClick="btnAddGeneric_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td class="border center pad5 whitebg" style="width: 50%" align="left">
                                <div id="arvShow" runat="server">
                                    <label class="Center">
                                        Available Strengths
                                    </label>
                                    <br />
                                    <asp:ListBox ID="lstStrength" runat="server" Height="100px" Width="300px"></asp:ListBox>
                                    <br />
                                    <asp:Button ID="btnAddDose" runat="server" Text="Add Strengths" OnClick="btnAddDose_Click" />
                                </div>
                            </td>
                            <td id="FrequencyID" class="border center pad5 whitebg" style="width: 50%" runat="server"
                                align="left">
                                <div id="arvShow1" runat="server">
                                    <label class="center">
                                        Available Frequency</label><br />
                                    <asp:ListBox ID="lstFrequency" runat="server" Height="100px" Width="300px"></asp:ListBox>
                                    <br />
                                    <asp:Button ID="btnAddFrequency" runat="server" Text="Add Frequency" Enabled="false"
                                        OnClick="btnAddFrequency_Click" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="border center pad5 whitebg" style="width: 50%" align="left">
                                <div id="vaccshow" runat="server">
                                    <label class="Center">
                                        Available Schedule Times
                                    </label>
                                    <br />
                                    <asp:ListBox ID="lstshedule" runat="server" Height="100px" Width="300px"></asp:ListBox>
                                    <br />
                                    <asp:Button ID="btnAddshedule" runat="server" Text="Add Times" OnClick="btnAddshedule_Click" />
                                </div>
                            </td>
                            <td id="Td1" class="border center pad5 whitebg" style="width: 50%" align="left">
                            </td>
                        </tr>
                        <tr>
                            <td class="border center pad5 whitebg" width="100%" colspan="2" align="left">
                                <div id="divStatus" runat="server" style="display: inline">
                                    <label class="margin">
                                        Status :</label>
                                    <asp:DropDownList ID="ddStatus" runat="server">
                                        <asp:ListItem Value="0">Active</asp:ListItem>
                                        <asp:ListItem Value="1">InActive</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="pad5 center" align="center" colspan="6">
                                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Close" OnClick="btnCancel_Click" />
                                <asp:Button ID="btn" runat="server" CssClass="textstylehidden" Text="OK" OnClick="btn_Click" />
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table>
                    <tbody>
                    </tbody>
                </table>
                <br />
                <!-- Rupesh 20-Jun-07 -->
                <table>
                    <tr>
                        <td width="100%" colspan="2">
                            <div id="nonARVShow1" runat="server">
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
