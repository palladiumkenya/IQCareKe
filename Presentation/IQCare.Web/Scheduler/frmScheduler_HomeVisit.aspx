<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="IQCare.Web.Scheduler.HomeVisit" Title="Untitled Page" Codebehind="frmScheduler_HomeVisit.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <div class="center" style="padding: 8px;">
     <h3 class="margin" align="left"><asp:Label ID="lblH2" runat="server"></asp:Label></h3>
      <div class="border center formbg">
           <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                   
                    <tr>
                        <td class="form" width="50%">
                            <label class="margin50">
                                CHW/CHV/Nurse Team:</label>
                            <asp:DropDownList ID="ddlCHW" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td class="form" valign="middle">
                            <label id="Vdate" class="required right35">
                                *Home Visit Start Date:</label>
                            <input id="StartDate" maxlength="11" size="10" name="StartDate" runat="server" />
                            <img onclick="w_displayDatePicker('<%=StartDate.ClientID%>');" height="22" alt="Date Helper"
                                hspace="3" src="../images/cal_icon.gif" width="22" border="0" /><span class="smallerlabel">(DD/MM/YYYY)</span>
                        </td>
                    </tr>
                    
                    <tr>
                        <td class="form" width="50%">
                            <label>
                                Alternative CHW/CHV/Nurse Team:</label>
                            <asp:DropDownList ID="ddlAlternateCHW" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td class="form">
                            <label class="margin10">
                                Number of Weeks:</label>
                            <asp:TextBox ID="txtNumofWeeks" MaxLength="2" Width="10%" runat="server" OnTextChanged="txtNumofWeeks_change"
                                AutoPostBack="true"></asp:TextBox>
                            <label class="margin20">
                                Number of Visits per Week:</label>
                            <asp:TextBox ID="txtVisitPerWeek" MaxLength="2" Width="10%" runat="server"></asp:TextBox>
                            <%--<INPUT id="txtVisitPerWeek" maxLength=2 size=5 name=physRR2   runat="server" />--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="form" colspan="2" width="100%">
                            <div id="VisitPerWeekShow" style="display: none">
                                <label class="right20">
                                    Visits by Week: &nbsp;</label>
                                <div id="VisitPerWeekShow1" style="display: none">
                                    <label id="lblVisitsPerWeek1" runat="server">
                                        Week 1:</label>
                                    <select name="VisitsPerWeek" id="VisitsPerWeek1" runat="server">
                                        <option value="0">-Select-</option>
                                        <option value="1">1 day</option>
                                        <option value="2">2 days</option>
                                        <option value="3">3 days</option>
                                        <option value="4">4 days</option>
                                        <option value="5">5 days</option>
                                        <option value="6">6 days</option>
                                        <option value="7">7 days</option>
                                    </select>
                                </div>
                                <%--<DIV id=VisitPerWeekShow2  visible=false  style="width:20%" runat="server">--%>
                                <div id="VisitPerWeekShow2" style="display: none">
                                    <label id="lblVisitsPerWeek2" runat="server" class="margin10">
                                        Week 2:</label>
                                    <select name="VisitsPerWeek" id="VisitsPerWeek2" runat="server">
                                        <option value="0">-Select-</option>
                                        <option value="1">1 day</option>
                                        <option value="2">2 days</option>
                                        <option value="3">3 days</option>
                                        <option value="4">4 days</option>
                                        <option value="5">5 days</option>
                                        <option value="6">6 days</option>
                                        <option value="7">7 days</option>
                                    </select>
                                </div>
                                <div id="VisitPerWeekShow3" style="display: none">
                                    <label id="lblVisitsPerWeek3" runat="server" class="margin10">
                                        Week 3:</label>
                                    <select name="VisitsPerWeek" id="VisitsPerWeek3" runat="server">
                                        <option value="0">-Select-</option>
                                        <option value="1">1 day</option>
                                        <option value="2">2 days</option>
                                        <option value="3">3 days</option>
                                        <option value="4">4 days</option>
                                        <option value="5">5 days</option>
                                        <option value="6">6 days</option>
                                        <option value="7">7 days</option>
                                    </select>
                                </div>
                                <div id="VisitPerWeekShow4" style="display: none">
                                    <label id="lblVisitsPerWeek4" runat="server" class="margin10">
                                        Week 4:</label>
                                    <select name="VisitsPerWeek" id="VisitsPerWeek4" runat="server">
                                        <option value="0">-Select-</option>
                                        <option value="1">1 day</option>
                                        <option value="2">2 days</option>
                                        <option value="3">3 days</option>
                                        <option value="4">4 days</option>
                                        <option value="5">5 days</option>
                                        <option value="6">6 days</option>
                                        <option value="7">7 days</option>
                                    </select>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="form" colspan="2">
                            <asp:Panel ID="pnlCustomList" Visible="false" runat="server" Height="100%" Width="100%"
                                Wrap="true">
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td class="pad5 center" colspan="2">
                            <br>
                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click1" />
                            <asp:Button ID="btnComplete" runat="server" Text="Data Quality Check" OnClick="btnComplete_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Close" OnClick="btnCancel_Click" />
                            <asp:Button ID="theBtn" runat="server" CssClass="textstylehidden" OnClick="theBtn_Click" />
                            <asp:Button ID="theBtn1" runat="server" CssClass="textstylehidden" OnClick="theBtn1_Click" />
                            <br />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
