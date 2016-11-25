<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" EnableEventValidation="false"
    AutoEventWireup="True"
    Inherits="IQCare.Web.Clinical.PatientClassificationCTC" Codebehind="frmClinical_PatientClassificationCTC.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <script language="javascript" type="text/javascript">
        function Redirect(id) {
            window.location.href = "../ClinicalForms/frmPatient_Home.aspx?PatientId=" + id;
        }
        function WindowPrint() {
            window.print();
        }
    </script>
    <div style="padding-top: 1px;">
        
        <div class="border center formbg">
            <table class="center" cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <td class="border pad5 whitebg" style="width: 50%">
                            <label id="lblclassification" class="required margin20" for="*classification">
                                *Classification:</label>
                            <asp:DropDownList ID="ddlclassification" runat="server" Width="255px">
                            </asp:DropDownList>
                        </td>
                        <td class="border pad5 whitebg">
                            <label id="lbldate" class="required margin15" for="date">
                                *Visit Date:</label>
                            <asp:TextBox ID="txtdate" MaxLength="11" runat="server" Width="70px"></asp:TextBox>
                            <img onclick="w_displayDatePicker('<%= txtdate.ClientID %>');" height="22" alt="Date Helper"
                                hspace="3" src="../images/cal_icon.gif" width="20" border="0">
                            <span class="smallerlabel">DD-MMM-YYYY </span>
                        </td>
                    </tr>
                    <tr>
                        <td class="border pad5 whitebg" colspan="2">
                            <asp:Button ID="btnadd" runat="server" Text="Add" Width="100px" Font-Size="12px"
                                OnClick="btnadd_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="pad5 formbg border" colspan="2">
                            <div class="GridView whitebg" style="position: relative; cursor: pointer">
                                <asp:GridView ID="grdPClassification" GridLines="Both" CellSpacing="1" runat="server"
                                    AutoGenerateColumns="False" Width="100%" AllowSorting="True" BorderColor="#666699"
                                    OnRowDataBound="grdPClassification_RowDataBound" OnSorting="grdPClassification_Sorting"
                                    OnRowDeleting="grdPClassification_RowDeleting">
                                    <HeaderStyle CssClass="tableheaderstyle" HorizontalAlign="Left"></HeaderStyle>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="pad5 center" colspan="2" style="height: 53px">
                            <asp:Button ID="btnsave" Width="80px" Font-Size="12px" runat="server" Text="Save"
                                OnClick="btnSave_Click" />
                            <asp:Button ID="btnClose" runat="server" Width="80px" Font-Size="12px" Text="Close"
                                OnClick="btnClose_Click" />
                            <asp:Button ID="btnPrint" Text="Print" runat="server" OnClientClick="WindowPrint()" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
