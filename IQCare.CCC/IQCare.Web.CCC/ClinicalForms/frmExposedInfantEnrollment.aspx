<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    EnableEventValidation="false" EnableViewState="true"
    Inherits="IQCare.Web.Clinical.HEIEnrollment" Codebehind="frmExposedInfantEnrollment.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <%-- <form id="frmExposedInfantEnrollment" method="post" runat="server">--%>
    <div class="center" style="padding: 8px;">
        <script language="Javascript" type="text/javascript">

            function isNumberKey(evt) {
                var charCode = (evt.which) ? evt.which : event.keyCode
               // if (charCode > 31 && (charCode < 48 || charCode > 57))
                  //  return false;

                return true;
            }

            function CalEnbleDisble() {

                document.getElementById("ctl00_IQCareContentPlaceHolder_spDeath").style.display = "block";
            }
            function spDeathCon(id) {

                var e = document.getElementById("ctl00_IQCareContentPlaceHolder_DDFinalResult");
                document.getElementById("ctl00_IQCareContentPlaceHolder_TxtDeathDate").value = '';
                var strVal = e.options[e.selectedIndex].value;
                if (strVal == "9") {
                    document.getElementById("ctl00_IQCareContentPlaceHolder_spDeath").style.display = "block";
                }
                else {
                    document.getElementById("ctl00_IQCareContentPlaceHolder_spDeath").style.display = "none";
                }
            }
            function abc() {
                alert("hi");
            }
      
        </script>
        <div id="DivPMTCT" runat="server">
            <div class="border center formbg">
                <table cellspacing="6" cellpadding="0" width="100%" border="0">
                    <tbody>
                        <tr>
                            <td align="center" style="width: 50%" class="border pad5 whitebg">
                                <label id="lblPName" class="required" for="patientname">
                                    *Infant Name:</label>
                                <span id="FName" class="smallerlabel">First: </span>
                                <asp:TextBox ID="TxtFirstName" runat="server" Width="118px" MaxLength="50" OnTextChanged="TxtFirstName_TextChanged"></asp:TextBox>
                                <span id="LName" class="smallerlabel">Last: </span>
                                <asp:TextBox ID="TxtLastName" runat="server" Width="118px" MaxLength="50" OnTextChanged="TxtLastName_TextChanged"></asp:TextBox>
                                <span id="InfantId" class="smallerlabel">
                                    <br />
                                    *Unique ID: </span>
                                <asp:TextBox ID="TxtInfantId" runat="server" Width="118px" MaxLength="10" onkeypress="return isNumberKey(event)"
                                    OnTextChanged="TxtInfantId_TextChanged" Style="text-transform:uppercase"></asp:TextBox>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td align="center" class="border pad5 whitebg" width="50%">
                                <label id="lblDateOfBirth" class="required" for="DateOfBirth">
                                    *Date of Birth:</label>
                                <input id="TxtDateOfBirth" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"
                                    onfocus="javascript:vDateType='3'" maxlength="11" size="11" name="pharmOrderedbyDate"
                                    runat="server" />
                                <%--          <asp:TextBox ID="TxtDateOfBirth" runat="server" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"
                             onfocus="javascript:vDateType='3'" MaxLength="11" Columns="8"></asp:TextBox>--%>
                                <img onclick="w_displayDatePicker('<%= TxtDateOfBirth.ClientID %>');" height="22"
                                    alt="Date Helper" hspace="3" src="../images/cal_icon.gif" width="22" border="0" />
                                <span class="smallerlabel">(DD-MMM-YYYY)</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="width: 50%;white-space:nowrap" class="border pad5 whitebg">
                                <label id="lblInfantFeedingPractice" for="InfantFeedingPractice">
                                    Infant Feeding Practice at 3 Months:</label>
                                <asp:DropDownList ID="DDInfantFeedingPractice" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td align="center" class="border pad5 whitebg" width="50%">
                                <label id="lblCPTStarted" for="CPTStarted">
                                    CPT Started by 2 Months:</label>
                                <asp:CheckBox ID="ChkCPTStarted" runat="server" Checked="false" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="width: 50%; white-space:nowrap" valign="top" class="border pad5 whitebg" >
                                <label id="lblHIVTestType" for="HIVTestType">
                                    HIV Test Type:</label>
                                <asp:DropDownList ID="DDHIVTestType" runat="server">
                                </asp:DropDownList>
                                <label id="lblResult" for="Result">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Result:</label>
                                <asp:DropDownList ID="DDResult" runat="server">
                                </asp:DropDownList>
                                <br />
                            </td>
                            <td align="center" style="width: 50%;white-space:nowrap" class="border pad5 whitebg" >
                                <label id="lblFinalResult" for="FinalResult">
                                    Final Result:</label>
                                <asp:DropDownList ID="DDFinalResult" runat="server" onchange="spDeathCon(this);">
                                </asp:DropDownList>
                                <div id="spDeath" runat="server" style="display: none">
                                    <label id="lblDeathDate" runat="server">
                                        <br />
                                        Death Date:</label>
                                    <%--   <asp:TextBox ID="TxtDeathDate" runat="server" MaxLength="11"  Columns="8" Visible="false"></asp:TextBox>
                            <img id="imgDeath" onclick="w_displayDatePicker('<%=TxtDeathDate.ClientID %>');"
                                height="22" alt="Date Helper" hspace="3" src="../images/cal_icon.gif" width="20"
                                border="0" visible="false" runat="server" />--%>
                                    <asp:TextBox ID="TxtDeathDate" runat="server" MaxLength="11" Columns="8"></asp:TextBox>
                                    <img id="imgDeath" onclick="w_displayDatePicker('<%= TxtDeathDate.ClientID %>');"
                                        height="22" alt="Date Helper" hspace="3" src="../images/cal_icon.gif" width="22"
                                        border="0" />
                                    <span id="spanDeath" class="smallerlabel" runat="server">(DD-MMM-YYYY) </span>
                                </div>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td class="center" colspan="2">
                                <asp:Button ID="btnAdd" Text="Add Infant" runat="server" OnClick="btnAdd_Click1">
                                </asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td class="pad5 formbg border " valign="top" colspan="2">
                                <div id="div-gridview" class="gridview whitebg" >
                                <div class="whitebg" style="height: 300px; overflow: auto">
                                    <asp:GridView ID="grdChildInfo" runat="server" EnableViewState="true" Width="100%"
                                        BorderColor="#666699" AutoGenerateColumns="False" CellSpacing="1" AllowSorting="True"
                                        OnRowDataBound="grdChildInfo_RowDataBound" OnSorting="grdChildInfo_Sorting" OnRowDeleting="grdChildInfo_RowDeleting"
                                        OnSelectedIndexChanging="grdChildInfo_SelectedIndexChanging">
                                        <HeaderStyle CssClass="tableheaderstyle" HorizontalAlign="Center"></HeaderStyle>
                                        <AlternatingRowStyle BackColor="White" BorderColor="Silver" />
                                        <Columns>
                                            <asp:BoundField HeaderText="First Name" DataField="FirstName" ItemStyle-HorizontalAlign="Center"
                                                ReadOnly="true" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Underline="true"
                                                HeaderStyle-ForeColor="Blue">
                                                <HeaderStyle Font-Bold="True" Font-Underline="True" ForeColor="Blue"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Last Name" DataField="LastName" ItemStyle-HorizontalAlign="Center"
                                                ReadOnly="true" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Underline="true"
                                                HeaderStyle-ForeColor="Blue">
                                                <HeaderStyle Font-Bold="True" Font-Underline="True" ForeColor="Blue"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Unique ID" DataField="ExposedInfantId" ItemStyle-HorizontalAlign="Center"
                                                ReadOnly="true" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Underline="true"
                                                HeaderStyle-ForeColor="Blue">
                                                <HeaderStyle Font-Bold="True" Font-Underline="True" ForeColor="Blue"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="DOB" DataField="DOB" ItemStyle-HorizontalAlign="Center"
                                                ReadOnly="true" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Underline="true"
                                                HeaderStyle-ForeColor="Blue" DataFormatString="{0:dd MMM yyyy}" HtmlEncode="false">
                                                <HeaderStyle Font-Bold="True" Font-Underline="True" ForeColor="Blue"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Infant Feeding" DataField="FeedingPractice3mos" ItemStyle-HorizontalAlign="Center"
                                                ReadOnly="true" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Underline="true"
                                                HeaderStyle-ForeColor="Blue">
                                                <HeaderStyle Font-Bold="True" Font-Underline="True" ForeColor="Blue"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="CPT Started" DataField="CTX2mos" ItemStyle-HorizontalAlign="Center"
                                                ReadOnly="true" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Underline="true"
                                                HeaderStyle-ForeColor="Blue">
                                                <HeaderStyle Font-Bold="True" Font-Underline="True" ForeColor="Blue"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="HIV Test Type" DataField="HIVTestType" ItemStyle-HorizontalAlign="Center"
                                                ReadOnly="true" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Underline="true"
                                                HeaderStyle-ForeColor="Blue">
                                                <HeaderStyle Font-Bold="True" Font-Underline="True" ForeColor="Blue"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="HIV Result" DataField="HIVResult" ItemStyle-HorizontalAlign="Center"
                                                ReadOnly="true" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Underline="true"
                                                HeaderStyle-ForeColor="Blue">
                                                <HeaderStyle Font-Bold="True" Font-Underline="True" ForeColor="Blue"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Final Result" DataField="FinalStatus" ItemStyle-HorizontalAlign="Center"
                                                ReadOnly="true" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Underline="true"
                                                HeaderStyle-ForeColor="Blue">
                                                <HeaderStyle Font-Bold="True" Font-Underline="True" ForeColor="Blue"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Death Date" DataField="DeathDate" ItemStyle-HorizontalAlign="Center"
                                                ReadOnly="true" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Underline="true"
                                                HeaderStyle-ForeColor="Blue" DataFormatString="{0:dd MMM yyyy}" HtmlEncode="false">
                                                <HeaderStyle Font-Bold="True" Font-Underline="True" ForeColor="Blue"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:CommandField ButtonType="Link" DeleteText="<img src='../Images/del.gif' alt='Delete this' border='0' />"
                                                ShowDeleteButton="true" />
                                            <asp:BoundField DataField="HivResultId" Visible="false" />
                                            <asp:BoundField DataField="FeedID" Visible="false" />
                                            <asp:BoundField DataField="FinalStatusID" Visible="false" />
                                            <asp:BoundField DataField="HIVTestTypeID" Visible="false" />
                                        </Columns>
                                    </asp:GridView>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="border center formbg">
                <table cellspacing="6" cellpadding="0" width="100%">
                    <tbody>
                        <tr>
                            <asp:TextBox ID="txtSysDate" runat="server" CssClass="textstylehidden"></asp:TextBox>
                            <td class="center">
                                <asp:Button ID="btnsave" Text="Save" runat="server" OnClick="btnsave_Click"></asp:Button>
                                <asp:Button ID="btnCancel" Text="Close" runat="server" OnClick="btnCancel_Click">
                                </asp:Button>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
