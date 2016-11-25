<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="IQCare.Web.Reports.frmReportCDC" Title="Untitled Page" Codebehind="frmReportCDC.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <script language="javascript" type="text/javascript">
        /**********************************************************
        Function		: Validation	
        Created By		: Amitava Sinha
        Created On		: 13-Feb-2006
        ***********************************************************/
        function CheckDate(currentyear) {

            var stryear = document.getElementById('<%=txtYear.ClientID%>').value;
            if (stryear != '') {
                if (stryear > currentyear) {
                    alert("Selected year should be less then or equal to Current year. Reenter..");
                    stryear.value = "";
                    stryear.focus();

                }
            }
        }

        function Validate() {
            var strStartDate = document.getElementById('<%=txtStartDate.ClientID%>').value;
            var strEndDate = document.getElementById('<%=txtEndDate.ClientID%>').value;
            var stryear = document.getElementById('<%=txtYear.ClientID%>').value;
            var objddl = document.getElementById('<%=ddQuarter.ClientID%>');
            var blnslect;
            blndate = true;
            blnyear = true;

            if (strStartDate.length <= 0) {
                blndate = false;
            }
            if (strEndDate.length <= 0) {
                blndate = false;
            }
            if (stryear.length <= 0) {
                blnyear = false;
            }
            if (document.getElementById('<%=rdoDate.ClientID%>').checked == true) {
                if (!blndate) {
                    alert('Please enter date');
                    return false;
                }

                if (strStartDate > strEndDate) {
                    alert('Start Date cannot be greater than End Date');
                    return false;

                }

            }


            //        else if (stryear.length >0)
            else if (document.getElementById('<%=rdoQuarter.ClientID%>').checked == true) {
                if (objddl.selectedIndex == 0) {
                    alert("Select Quarter !");
                    return false;
                }

                if (!blnyear) {

                    alert('Please enter Year');
                    return false;
                }

            }

            else if ((document.getElementById('<%=rdoQuarter.ClientID%>').checked == false) && (document.getElementById('<%=rdoQuarter.ClientID%>').checked == false)) {
                alert('Please Select any Date or Quarter');
                return false;


            }


        }

        function btnEnabledDisabled() {
            if (document.getElementById('<%=rdoDate.ClientID%>').checked == true) {
                document.getElementById('dvDate').disabled = false;
                var objDate = document.getElementById('<%=txtStartDate.ClientID%>');
                objDate.readOnly = false;
                var objDate1 = document.getElementById('<%=txtEndDate.ClientID%>');
                objDate1.readOnly = false;
                document.getElementById('<%=txtYear.ClientID%>').value = '';
                document.getElementById('imgstart').style.visibility = 'visible';
                document.getElementById('imgend').style.visibility = 'visible';

            }
            if (document.getElementById('<%=rdoDate.ClientID%>').checked == false) {
                document.getElementById('dvDate').disabled = true;
                var objDate = document.getElementById('<%=txtStartDate.ClientID%>');
                objDate.readOnly = true;
                var objDate1 = document.getElementById('<%=txtEndDate.ClientID%>');
                objDate1.readOnly = true;
                document.getElementById('imgstart').style.visibility = 'hidden';
                document.getElementById('imgend').style.visibility = 'hidden';
            }
            if (document.getElementById('<%=rdoQuarter.ClientID%>').checked == true) {
                document.getElementById('dvQuarter').disabled = false;
                var objddl = document.getElementById('<%=ddQuarter.ClientID%>');
                objddl.disabled = false;
                var objqtr = document.getElementById('<%=txtYear.ClientID%>');
                objqtr.readOnly = false;
                document.getElementById('<%=txtStartDate.ClientID%>').value = '';
                document.getElementById('<%=txtEndDate.ClientID%>').value = '';
            }
            if (document.getElementById('<%=rdoQuarter.ClientID%>').checked == false) {
                document.getElementById('dvQuarter').disabled = true;
                var objddl = document.getElementById('<%=ddQuarter.ClientID%>');
                objddl.disabled = true;
                var objqtr = document.getElementById('<%=txtYear.ClientID%>');
                objqtr.readOnly = true;
            }
        }


        function GetControl() {
            document.forms[0].submit();
        }
    
 
    </script>
    <div>
        <%--<form runat="server" id="frmCDCReport" >--%>
        <h1 class="nomargin">
            <asp:Label ID="lblHeadertext" runat="server" Font-Bold="True" Font-Size="Medium"
                Text="Label"></asp:Label>
        </h1>
        <div class="border center formbg">
            <table class="center" width="100%" border="0" cellpadding="0" cellspacing="6">
                <tr>
                    <td class="border center whitebg" style="width: 562px">
                        <label class="margin10left">
                            Select Date
                        </label>
                        <input type="radio" id="rdoDate" name="rdoOption" value="1" onclick="btnEnabledDisabled(this)"
                            runat="server" />
                    </td>
                    <td class="border center whitebg" width="40%">
                        <label class="margin10left">
                            Select Quarter</label>
                        <input type="radio" id="rdoQuarter" name="rdoOption" value="1" onclick="btnEnabledDisabled(this)"
                            runat="server" />
                    </td>
                    <tr>
                        <!-- Modified 13th june 2007(1) -->
                        <td class="border center whitebg" style="width: 562px; height: 46px;">
                            <div id="dvDate" disabled>
                                <label>
                                    From
                                </label>
                                <input id="txtStartDate" maxlength="11" size="11" name="txtStartDate" runat="server"
                                    readonly="true">
                                <img id="imgstart" style="visibility: hidden" src="../Images/cal_icon.gif" height="22"
                                    alt="Date Helper" hspace="3" border="0" onclick="w_displayDatePicker('<%= txtStartDate.ClientID%>');" />
                                <span class="smallerlabel">(DD-MMM-YYYY)</span>
                                <label>
                                    To</label>
                                <input id="txtEndDate" maxlength="11" size="11" name="txtEndDate" runat="server"
                                    readonly="true">
                                <img id="imgend" style="visibility: hidden" src="../Images/cal_icon.gif" height="22"
                                    alt="Date Helper" hspace="3" border="0" onclick="w_displayDatePicker('<%= txtEndDate.ClientID%>');" />
                                <span class="smallerlabel">(DD-MMM-YYYY)</span>
                            </div>
                            <!-- Modified 13th june 2007(1) -->
                        </td>
                        <td class="border center whitebg" style="height: 46px">
                            <div id="dvQuarter" disabled>
                                <label>
                                    Quarter
                                </label>
                                <asp:DropDownList ID="ddQuarter" runat="server" Width="100px" Enabled="false">
                                    <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                                    <asp:ListItem Text="Quarter 1" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Quarter 2" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Quarter 3" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Quarter 4" Value="4"></asp:ListItem>
                                </asp:DropDownList>
                                &nbsp;&nbsp;&nbsp;&nbsp;<label>Year</label>
                                <asp:TextBox ID="txtYear" MaxLength="4" Width="75px" runat="server"></asp:TextBox>
                            </div>
                        </td>
                    </tr>
                    <tr id="tdallLocation" runat="server">
                        <td colspan="6" class="border whitebg" align="center">
                            <asp:CheckBox ID="chkAllLocations" runat="server" Text="All Locations" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" class="pad5 center">
                            <asp:Label ID="lblWarning" runat="server" Width="100%" CssClass="textjustify" Text="*Warning: Track 1.0 Report can take some time to display which depends on the speed of your computer. Please be patient and allow 2-10 minutes"
                                ForeColor="Red" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" class="pad5 center">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
            </table>
        </div>
        <!-- Modified 13th june 2007(2) -->
        <br />
        <br />
        <br />
        <br />
        <br />
        <!-- Modified 13th june 2007(2) -->
    </div>
</asp:Content>
