<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="IQCare.Web.Clinical.FollowUpEducationCTC"
    EnableEventValidation="false" Codebehind="frmFollowUpEducationCTC.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <script language="javascript" type="text/javascript">
        function ShowHide(id) {
            var ShowOtherConType = 'tdotherctopic';
            if (id.value == 2) {
                document.getElementById(ShowOtherConType).style.display = 'inline';
            }
            else {
                document.getElementById(ShowOtherConType).style.display = 'none';
            }
        } 
        function WindowPrint() {
            window.print();
        }
        function Redirect(id) {
            window.location.href = "../ClinicalForms/frmPatient_Home.aspx?PatientId=" + id;
        }
    </script>
    <div class="center" style="padding: 8px;">
        <%-- <form id="frmfollowupeducation" method="post" runat="server">    --%>
        <div class="border center formbg">
            <table class="center" cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <td class="border pad5 whitebg" style="width: 50%">
                            <label id="lblcouncellingtype" class="required margin20" for="counsellingtype">
                                *Counselling Type:</label>
                            <asp:DropDownList ID="ddlcouncellingtype" runat="server" Width="255px" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlcouncellingtype_SelectedIndexChanged">
                            </asp:DropDownList></td>
                            <td class="border pad5 whitebg">
                                <label id="lblcouncellingtopic" class="required left" for="counsellingtopic">
                                    *Counselling Topic:</label>
                                <asp:DropDownList ID="ddlcouncellingtopic" runat="server" AutoPostBack="true" Width="255px"
                                    OnSelectedIndexChanged="ddlcouncellingtopic_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        
                    </tr>
                    <tr>
                        <td id="tdotherctopic" runat="server" visible="false" class="border center whitebg"
                            colspan="2">
                            <label class="left">
                                Other Counselling Topic:</label>
                            <asp:TextBox ID="txtotherctopic" runat="server" Width="500px" MaxLength="250"></asp:TextBox><a></a>
                        </td>
                    </tr>
                    <tr>
                        <td class="border pad5 whitebg">
                            <label id="lblvisitdate" class="required margin15" for="visitdate">
                                *Visit Date:</label>
                            <asp:TextBox ID="txtvisitdate" MaxLength="11" runat="server" Width="70px"></asp:TextBox>
                            <img onclick="w_displayDatePicker('<%= txtvisitdate.ClientID %>');" height="22" alt="Date Helper"
                                hspace="3" src="../images/cal_icon.gif" width="20" border="0"/>
                            <span class="smallerlabel">DD-MMM-YYYY </span>
                        </td>
                        <td class="border pad5 whitebg">
                            <label id="lblcomments" class="margin20" for="comments">
                                Comments:</label>
                            <asp:TextBox ID="txtcomments" runat="server" Width="255px" TextMode="MultiLine" MaxLength= "250"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="border pad5 whitebg" colspan="2">
                            <asp:Button ID="btnaddtopic" runat="server" Text="Add Topic" Width="100px" Font-Size="12px"
                                OnClick="btnadd_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="pad5 formbg border " colspan="2">
                            <div class="gridview whitebg">
                            <div class="whitebg" style="height: 300px; overflow: auto">
                                <asp:GridView ID="grdFollowupEducation" GridLines="Both" CellSpacing="1" runat="server"
                                    OnRowDataBound="grdFollowupEducation_RowDataBound" AutoGenerateColumns="False"
                                    Width="100%" AllowSorting="True" BorderColor="#666699" OnSelectedIndexChanging="grdFollowupEducation_SelectedIndexChanging"
                                    OnSorting="grdFollowupEducation_Sorting" OnRowDeleting="grdFollowupEducation_RowDeleting">
                                    <HeaderStyle CssClass="tableheaderstyle" HorizontalAlign="Left"></HeaderStyle>
                                </asp:GridView>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="pad5 center" colspan="2" style="height: 53px">
                            <asp:Button ID="btnsave" Width="80px" Font-Size="12px" runat="server" Text="Save"
                                OnClick="btnsave_Click" />
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
