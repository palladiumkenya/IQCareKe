<%@ Page Language="C#" EnableEventValidation="false" MasterPageFile="~/MasterPage/IQCare.master"
    AutoEventWireup="True" Inherits="IQCare.Web.Clinical.FamilyInformation" Title="untitled page"
    CodeBehind="frmFamilyInformation.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <script language="javascript" type="text/javascript">
        function WindowPrint() {
            window.print();
        }
        function ShowRelationDt() {
            document.getElementById('divRelationDate').style.display = 'block';

        }
        function Redirect(id) {
            window.location.href = "../ClinicalForms/frmPatient_Home.aspx?PatientId=" + id;
        }
        function fnDeleteMsg() {
            alert('Record Deleted Successfully');
            return false
        }
        function ShowHide(id) {
            var RegthisclinicYes = 'tdregclinic';
            var RegthisclinicNo = 'trhideHIV'
            if (id.value == 1) {
                document.getElementById(RegthisclinicYes).style.display = 'inline';
                document.getElementById(RegthisclinicNo).style.display = 'none';
            }
            else {
                document.getElementById(RegthisclinicNo).style.display = 'inline';
                document.getElementById(RegthisclinicYes).style.display = 'none';
            }
        }
        function ChkAgeMonth() {
            var v1 = document.getElementById('<%=txtAgeMonth.ClientID%>').value;
            if (v1 < 1 || v1 > 11)
                document.getElementById('<%=txtAgeMonth.ClientID%>').value = "";
        }

        function ShowHideRelationDt() {
            var v1 = document.getElementById('<%=ddlrelationtype.ClientID%>').value;
            if (v1 == 3 || v1 == 11)
                document.getElementById('divRelationDate').style.display = 'inline';
            else
                document.getElementById('divRelationDate').style.display = 'none';
        }
        function fnHideShowControl() {
            var val = document.getElementById('<%=regthisclinic.ClientID %>').value;

            if (val == 1) {
                document.getElementById(RegthisclinicYes).style.display = 'inline';
                document.getElementById(RegthisclinicNo).style.display = 'none';
            }
            else {
                document.getElementById(RegthisclinicNo).style.display = 'inline';
                document.getElementById(RegthisclinicYes).style.display = 'none';
            }
        }
        function fnValidate() {
            if (document.getElementById('<%=txtfname.ClientID%>').value == "") {
                alert("First Name cannot be blank");
                document.getElementById('<%=txtfname.ClientID%>').focus();
                return false;
            }
            else if (document.getElementById('<%=txtlname.ClientID%>').value == "") {
                alert("Last Name cannot be blank");
                document.getElementById('<%=txtlname.ClientID%>').focus();
                return false;
            }
            else if (document.getElementById('<%=ddlgender.ClientID%>').value == "0") {
                alert("sex is not selected");
                document.getElementById('<%=ddlgender.ClientID%>').focus();
                return false;
            }
            else if (document.getElementById('<%=txtAgeYear.ClientID%>').value == "") {
                alert("Age(Year) cannot be blank");
                document.getElementById('<%=txtAgeYear.ClientID%>').focus();
                return false;
            }

            else if (document.getElementById('<%=ddlrelationtype.ClientID%>').value != "0") {
                if (document.getElementById('<%=ddlrelationtype.ClientID%>').value == "3" || document.getElementById('<%=ddlrelationtype.ClientID%>').value == "11") {

                }
            }
            else if (document.getElementById('<%=ddlhivstatus.ClientID%>').value == "0") {
                alert("Hiv Status not selected");
                document.getElementById('<%=ddlhivstatus.ClientID%>').focus();
                return false;
            }
            else if (document.getElementById('<%=ddlhivcstatus.ClientID%>').value == "0") {
                alert("Hiv Care Status not selected");
                document.getElementById('<%=ddlhivcstatus.ClientID%>').focus();
                return false;
            }
            return true;
        }
    </script>
    <h1 class="margin" style="padding-left: 10px;">
        Family Information</h1>
    <div class="center" style="padding: 8px;">
        <div id="DivPMTCT" runat="server">
            <div class="border formbg">
                <table width="100%" border="0" style="padding-top: 5px;">
                    <tbody>
                        <tr>
                            <td colspan="2">
                                <div id="divART" runat="server">
                                    <table width="100%">
                                        <tr>
                                            <td class="form" align="center">
                                                <label class="patientInfo">
                                                    Patient Name :
                                                    <asp:Label ID="lblname" runat="server" Text=""></asp:Label></label>
                                                <label class="bold">
                                                    IQ Number:
                                                    <asp:Label ID="lblIQnumber" runat="server"></asp:Label></label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div id="pmtct" runat="server">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <div id="pmtctname" runat="server">
                                                    <table width="100%">
                                                        <tr>
                                                            <td class="form" align="center">
                                                                <label class="bold">
                                                                    Patient Name :
                                                                    <asp:Label ID="lblpatientnamepmtct" runat="server" Text=""></asp:Label></label>
                                                                <label class="bold">
                                                                    IQ Number:
                                                                    <asp:Label ID="lblIQnumberpmtct" runat="server"></asp:Label></label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="form bold" align="center">
                                <asp:Panel ID="thePnlIdent" runat="server">
                                </asp:Panel>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <br />
            <div class="border formbg">
                <table class="center" width="100%" border="0" style="padding-top: 5px;">
                    <tbody>
                        <tr>
                            <td class="border pad5 whitebg" width="50%">
                                <label>
                                    Registered at this clinic:</label>
                                <asp:DropDownList ID="regthisclinic" name="regthisclinic" AutoPostBack="true" runat="server"
                                    OnSelectedIndexChanged="regthisclinic_SelectedIndexChanged">
                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                </asp:DropDownList>
                                &nbsp;&nbsp;
                                <asp:Button ID="btnfind" Text="Find Patient" runat="server" Font-Size="12px" Width="80px"
                                    OnClick="btnFind" />
                            </td>
                            <td class="border pad5 whitebg">
                                <label class="required" for="relativename">
                                    &nbsp;*Relative Name:</label>
                                <span id="FName" class="smallerlabel">First:</span>
                                <asp:TextBox ID="txtfname" MaxLength="50" Width="75px" runat="server"></asp:TextBox>
                                <span id="Span1" class="smallerlabel">Last:</span>
                                <asp:TextBox ID="txtlname" MaxLength="50" Width="75px" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="border pad5 whitebg" align="right" width="50%">
                                <label id="lblgender" class="required" for="gender">
                                    *Sex:</label>
                                <asp:DropDownList ID="ddlgender" runat="server">
                                    <asp:ListItem Selected="True" Value="">-Select-</asp:ListItem>
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem Value="16">Male</asp:ListItem>
                                    <asp:ListItem Value="17">Female</asp:ListItem>
                                </asp:DropDownList>
                                <label class="required" for="Age">
                                    *Age:</label>
                                <asp:TextBox ID="txtAgeYear" MaxLength="2" runat="server" Width="10%"></asp:TextBox>
                                <span class="smallerlabel">yrs</span>
                                <asp:TextBox ID="txtAgeMonth" MaxLength="2" runat="server" Width="10%"></asp:TextBox>
                                <span class="smallerlabel">mths</span>
                            </td>
                            <td class="border pad5 whitebg">
                                <table width="100%" border="0">
                                    <tr>
                                        <td colspan="2">
                                            <table width="100%" border="0">
                                                <tr>
                                                    <td colspan="2" align="center">
                                                        <label>
                                                            Relationship Type:</label>
                                                        <asp:DropDownList ID="ddlrelationtype" onchange="ShowHideRelationDt()" runat="server">
                                                        </asp:DropDownList>
                                                        <div id="divRelationDate" style="display: none;">
                                                            <label id="lblRelationDate">
                                                                <br />
                                                                Birth/Marriage Date:</label>
                                                            <input id="txtRelationDate" onblur="DateFormat(this,this.value,event,false,'3')"
                                                                onkeyup="DateFormat(this,this.value,event,false,'3')" onfocus="javascript:vDateType='3'"
                                                                maxlength="11" size="8" name="RelationDate" runat="server" />
                                                            <img id="appDateimg1" onclick="w_displayDatePicker('<%=txtRelationDate.ClientID%>');"
                                                                height="22" alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="22"
                                                                border="0" name="appDateimg" />
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="trhideHIV">
                            <td colspan="2">
                                <div id="hidstatus" runat="server">
                                    <table width="100%">
                                        <tr>
                                            <td class="border pad5 whitebg" align="center" style="width: 50%">
                                                <label class="required">
                                                    *HIV Status:</label>
                                                <asp:DropDownList ID="ddlhivstatus" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="border pad5 whitebg" align="center">
                                                <label class="required">
                                                    &nbsp;*HIV Care Status:</label>&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:DropDownList ID="ddlhivcstatus" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="border center whitebg" colspan="2" style="height: 36px">
                                <asp:Button ID="btnadd" runat="server" OnClientClick="return fnValidate();" Style="width: 100px;
                                    font-size: 12px" Text="Add Member" OnClick="btnAdd" />
                            </td>
                        </tr>
                        <tr>
                            <td class="border pad5 formbg" colspan="2">
                                <div class="GridView whitebg" style="cursor: pointer;">
                                    <div class="grid">
                                        <div class="rounded">
                                            <div class="top-outer">
                                                <div class="top-inner">
                                                    <div class="top">
                                                        <h2>
                                                            Family Members</h2>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="mid-outer">
                                                <div class="mid-inner">
                                                    <div class="mid" style="height: 300px; overflow: auto">
                                                        <div id="div-gridview" class="GridView whitebg">
                                                            <asp:GridView ID="grdFamily" runat="server" OnRowDataBound="grdFamily_RowDataBound"
                                                                AutoGenerateColumns="False" Width="100%" AllowSorting="True" BorderWidth="0"
                                                                GridLines="None" CssClass="datatable table-striped table-responsive" CellPadding="0"
                                                                CellSpacing="0" OnSelectedIndexChanging="grdFamily_SelectedIndexChanging" OnSorting="grdFamily_Sorting"
                                                                OnRowDeleting="grdFamily_RowDeleting">
                                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                <RowStyle CssClass="gridrow" />
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="bottom-outer">
                                                <div class="bottom-inner">
                                                    <div class="bottom">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="border center whitebg" colspan="2" style="height: 10px">
                                <asp:Button ID="btnSubmit" runat="server" Font-Size="12px" Width="80px" Text="Save"
                                    OnClick="btnSubmit_Click" />
                                &nbsp;
                                <asp:Button ID="btnBack" runat="server" Font-Size="12px" Width="80px" Text="Back"
                                    OnClick="btnBack_Click" />
                                <asp:Button ID="btnPrint" Text="Print" runat="server" OnClientClick="WindowPrint()" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
   
</asp:Content>
