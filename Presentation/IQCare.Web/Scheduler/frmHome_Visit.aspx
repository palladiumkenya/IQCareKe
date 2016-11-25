<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    Inherits="IQCare.Web.Scheduler.Home_Visit" Title="Untitled Page" CodeBehind="frmHome_Visit.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <div>
        <%--<H1 class=margin>Home Visit Form</H1>
        --%><div class="border center formbg">
            <!-- DAL: using tables for form layout. Note that there are classes on labels and td. For custom fields, just use the 2 column layout, if there is an uneven number of cells, set last cell colspan="2" and align="center". Probably should talk through this -->
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                  <tr>
                        <td class="form" width="50%">
                            <label class="right50">
                                CHW/CHV/Nurse Team:</label>
                            <input name="CHW" id="txtCHW" runat="server" />
                        </td>
                        <td class="form" valign="middle">
                            <label class="required right35">
                                *Home Visit Start Date:</label>
                            <input id="StartDate" maxlength="10" size="10" name="StartDate" runat="server">
                            <img onclick="w_displayDatePicker('<%=StartDate.ClientID%>');" height="22" alt="Date Helper"
                                hspace="3" src="../images/cal_icon.gif" width="22" border="0" /><span class="smallerlabel">(DD/MM/YYYY)</span>
                        </td>
                    </tr>
                    <tr>
                        <td class="form">
                            <label class="right50">
                                Alternative CHW/CHV/Nurse Team:</label>
                            <input name="guardian2" id="txtguardian2" runat="server" />
                        </td>
                        <td class="form">
                            <label class="margin10">
                                Number of Weeks:</label>
                            <input id="txtphysRR23" maxlength="5" size="5" name="physRR2" runat="server" />
                            <label class="margin20">
                                Number of Visits per Week:</label>
                            <input id="Text1" maxlength="5" size="5" name="physRR2" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="form" colspan="2">
                            <label class="right20">
                                Visits by Week: &nbsp;</label>
                            <label>
                                Week 1:</label>
                            <select name="VisitsPerWeek" id="VisitsPerWeek" runat="server">
                                <option value="">-Select-</option>
                                <option value="1">1 day</option>
                                <option value="2">2 days</option>
                                <option value="3">3 days</option>
                                <option value="4">4 days</option>
                                <option value="5">5 days</option>
                                <option value="6">6 days</option>
                                <option value="7">7 days</option>
                            </select>
                            <label class="margin10">
                                Week 2:</label>
                            <select name="VisitsPerWeek" id="Select1" runat="server">
                                <option value="">-Select-</option>
                                <option value="1">1 day</option>
                                <option value="2">2 days</option>
                                <option value="3">3 days</option>
                                <option value="4">4 days</option>
                                <option value="5">5 days</option>
                                <option value="6">6 days</option>
                                <option value="7">7 days</option>
                            </select>
                            <label class="margin10">
                                Week 3:</label>
                            <select name="VisitsPerWeek" id="Select2">
                                <option value="">-Select-</option>
                                <option value="1">1 day</option>
                                <option value="2">2 days</option>
                                <option value="3">3 days</option>
                                <option value="4">4 days</option>
                                <option value="5">5 days</option>
                                <option value="6">6 days</option>
                                <option value="7">7 days</option>
                            </select>
                            <label class="margin10">
                                Week 4:</label>
                            <select name="VisitsPerWeek" id="Select3">
                                <option value="">-Select-</option>
                                <option value="1">1 day</option>
                                <option value="2">2 days</option>
                                <option value="3">3 days</option>
                                <option value="4">4 days</option>
                                <option value="5">5 days</option>
                                <option value="6">6 days</option>
                                <option value="7">7 days</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td class="pad5 center" colspan="2">
                            <br>
                            <input type="submit" value="Save" name="submit" id="btnsave" runat="server">
                            <input type="submit" value=" Data Quality Check " name="complete" id="btncomplete"
                                runat="server">
                            <br />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
