<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="IQCare.Web.Clinical.ARTCare" Codebehind="frmClinical_ARTCare.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    
    <script language="javascript" type="text/javascript">
        function WindowPrint() {
            window.print(); 
        }

        function GetControl() {
            document.forms[0].submit();
        }
        function ShowValue() {
            document.getElementById('Img1').disabled = true;
            document.getElementById('Img2').disabled = true;
            document.getElementById('Img6').disabled = true;
        }
        function CalEnbleDisble(a, b, c) {
            if (a == 0) {
                document.getElementById('Img1').disabled = true;
            }
            if (b == 0) {
                document.getElementById('Img2').disabled = true;
            }
            if (c == 0) {
                document.getElementById('Img6').disabled = true;
            }
        }
        function setMonthYear() {
            var artTransferDate = document.getElementById("<%=txtanotherRegimendate.ClientID%>").value;
            if (artTransferDate != "") {
                var arrMonthDate = artTransferDate.split('-');
                if (arrMonthDate[1] != "")
                    document.getElementById("<%=txtcohortmnth.ClientID%>").value = arrMonthDate[1];
                else
                    document.getElementById("<%=txtcohortmnth.ClientID%>").value = "";

                if (arrMonthDate[2] != "")
                    document.getElementById("<%=txtcohortyear.ClientID%>").value = arrMonthDate[2];
                else
                    document.getElementById("<%=txtcohortyear.ClientID%>").value = "";
            }
        }   
    </script>
    
    <br />
    <div style="padding-left: 8px; padding-right: 8px;">
        <div class="border center formbg">
            <br />
            <h2 class="forms" align="left">
                Cohort</h2>
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <td class="form center">
                            <label>
                                Cohort Month:</label>
                            <input id="txtcohortmnth" size="10" name="CohortMonth" runat="server" readonly="readonly"/>
                        </td>
                        <td class="form center">
                            <label>
                                Cohort Year:</label>
                            <input id="txtcohortyear" size="10" name="CohortYear" runat="server" readonly="readonly"/>
                        </td>
                    </tr>
                </tbody>
            </table>
            <br/>
        </div>
        <br />
        <div class="border center formbg">
            <br/>
            <h2 class="forms" align="left">
                Transfer in on ART</h2>
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <td class="border whitebg formcenter">

                            <label id="Label2" class="right15">
                                ART Transfer in Date:</label>
                            <input id="txtarttransdate" runat="server" maxlength="11" size="10" name="txtarttransdate" />
                            <img id="img2" onclick="w_displayDatePicker('<%=txtarttransdate.ClientID%>');" height="22"
                                alt="Date Helper" hspace="3" src="../images/cal_icon.gif" width="22" border="0" /><span
                                    class="smallerlabel">(DD-MMM-YYYY)</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <label class="right15">
                                ART Transfer in From:</label>
                            <asp:DropDownList ID="ddlarttransferinfrom" runat="Server" Font-Size="10px">
                            </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <label class="right10">
                                ARVs:</label>
                            <input id="txttransferARVs" size="16" name="transferARVs" readonly="readonly" runat="server"/>
                            <asp:Button ID="btntransferARVs" runat="server" Text="..." OnClick="btnRegimen_Click" />
                        
                        
                        </td>
                    </tr>
                </tbody>
            </table>
            <br/>
        </div>
        <br />
        <div class="border center formbg">
            <br/>
            <h2 class="forms" align="left">
                ART Start at Another Facility</h2>
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <td class="border whitebg formcenter pad5">
                            <label id="lblrARTdate">
                                Start ART 1st Line Regimen Date:</label>
                            <input id="txtanotherRegimendate" runat="server" maxlength="11" size="10" name="txtanotherRegimendate"
                                onmouseout="setMonthYear();" />
                            <img id="imgdate" onclick="w_displayDatePicker('<%=txtanotherRegimendate.ClientID%>');"
                                height="22" alt="Date Helper" hspace="3" src="../images/cal_icon.gif" width="22"
                                border="0" /><span class="smallerlabel">(DD-MMM-YYYY)</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <label class="right10">
                                Regimen:</label>
                            <input id="txtanotherregimen" size="16" name="anotherregimen" readonly="readonly"
                                runat="server"/>
                            <asp:Button ID="btnanotherRegimen" runat="server" Text="..." OnClick="btnTransRegimen_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <label class="right10">
                                Weight :</label>
                            <input id="txtanotherwght" size="6" name="anotherwght" runat="server"/>
                            <label>
                                Kgs</label>
                        </td>
                    </tr>
                    <tr>
                        <td class="border whitebg formcenter pad5">
                            <label>
                                Clinical Stage:</label>
                            <asp:DropDownList ID="lstanotherClinicalStage" runat="server" Style="width: 100px">
                            </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <label class="right15">
                                CD4:</label>
                            <input id="txtanotherCD4" size="6" name="CD4" runat="server"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <label class="right5">
                                CD4 %:</label>
                            <input id="txtanotherCD4percent" size="6" name="CD4percent" runat="server"/>
                            <span id="divanothpreg" runat="server">
                                <label class="right15">
                                    Pregnant:</label>
                                <asp:DropDownList ID="ddlpregnantanother" runat="Server">
                                </asp:DropDownList>
                            </span>
                        </td>
                    </tr>
                </tbody>
            </table>
            <br/>
        </div>
        <br />
        <div class="border center formbg">
            <br/>
            <h2 class="forms" align="left">
                ART Start at This Facility</h2>
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <td class="border whitebg formcenter pad5">
                            <label id="lblthisregi">
                                Start ART 1st Line Regimen Date:</label>
                            <input id="txtthisRegimendate" runat="server" maxlength="11" size="10" name="txtthisRegimendate"
                                readonly="readonly" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <%--<img id="img1" onclick="w_displayDatePicker('<%=txtthisRegimendate.ClientID%>');"
                                height="22" alt="Date Helper" hspace="3" src="../images/cal_icon.gif" width="22"
                                border="0" /><span class="smallerlabel">(DD-MMM-YYYY)</span>--%>
                            <label class="right10">
                                Regimen:</label>
                            <input id="txtthisregimen" size="16" name="thisregimen" readonly="readonly" runat="server"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <label class="right10">
                                Weight :</label>
                            <input id="txtthiswght" size="6" name="CohortYear" runat="server" readonly="readonly"/>
                            <label>
                                Kgs</label>
                        </td>
                    </tr>
                    <tr>
                        <td class="border whitebg formcenter pad5">
                            <label>
                                Clinical Stage:</label>
                            <asp:DropDownList ID="lstthisClinicalStage" runat="server" Style="width: 100px" Enabled="false">
                            </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <label class="right15">
                                CD4:</label>
                            <input id="txtthisCD4" size="6" name="CD4this" runat="server" readonly="readonly"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <label class="right5">
                                CD4 %:</label>
                            <input id="txtthisCD4percent" size="6" name="CD4percentthis" runat="server" readonly="readonly"/>
                            <span id="spnthispreg" runat="server">
                                <label class="right15">
                                    Pregnant:</label>
                                <asp:DropDownList ID="ddlpregthis" runat="Server" Enabled="false">
                                </asp:DropDownList>
                            </span>
                        </td>
                    </tr>
                </tbody>
            </table>
            <br/>
        </div>
        <br />
        <div class="border center formbg">
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <td class="pad5 formbg border">
                            <div class="grid">
                                <div class="rounded">
                                    <div class="top-outer">
                                        <div class="top-inner">
                                            <div class="top">
                                                <h2>
                                                    <center>
                                                        Substitutions and Switches
                                                    </center>
                                                </h2>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="mid-outer">
                                        <div class="mid-inner">
                                            <div class="mid">
                                                <div id="div-gridview" class="gridviewbackup whitebg">
                                                    <asp:GridView ID="grdSubsARVs" runat="server" AutoGenerateColumns="False" AllowSorting="true"
                                                        Width="100%" BorderColor="white" PageIndex="1" BorderWidth="0" GridLines="None"
                                                        CssClass="datatable table-striped table-responsive" CellPadding="0" CellSpacing="0">
                                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                        <RowStyle CssClass="gridrow" />
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Date" DataField="ChangeDate" />
                                                            <asp:BoundField HeaderText="Regimen" DataField="regimentype" />
                                                            <asp:BoundField HeaderText="Line" DataField="RegimenLine" />
                                                            <asp:BoundField HeaderText="Why" DataField="ChangeReason" />
                                                        </Columns>
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
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <br />
        <div class="border center formbg">
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <td class="pad5 formbg border">
                            <div class="grid">
                                <div class="rounded">
                                    <div class="top-outer">
                                        <div class="top-inner">
                                            <div class="top">
                                                <h2>
                                                    <center>
                                                        ART Treatment Interruptions</center>
                                                </h2>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="mid-outer">
                                        <div class="mid-inner">
                                            <div class="mid">
                                                <div id="div-gridview2" class="gridviewbackup whitebg">
                                                    <asp:GridView ID="grdInteruptions" runat="server" AutoGenerateColumns="False" AllowSorting="true"
                                                        Width="100%" BorderColor="white" PageIndex="1" BorderWidth="0" GridLines="None"
                                                        CssClass="datatable table-striped table-responsive" CellPadding="0" CellSpacing="0">
                                                        <%--<HeaderStyle   CssClass ="tableheaderstyle" ForeColor="Blue"  Font-Underline="true"  HorizontalAlign="Left" />--%>
                                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                        <RowStyle CssClass="gridrow" />
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Stop/Lost" DataField="StopLost" />
                                                            <asp:BoundField HeaderText="Date" DataField="ARTEndDate" />
                                                            <asp:BoundField HeaderText="Why" DataField="Reason" />
                                                        </Columns>
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
                        <td class="form pad5 center" colspan="2">
                            <br />
                            <asp:Button ID="btn_save" Text="Save" runat="server" OnClick="btn_save_Click" />
                            <asp:Button ID="DQ_Check" Text="Data quality check" runat="server" OnClick="DQ_Check_Click" />
                            <asp:Button ID="btn_close" Text="Close" runat="server" OnClick="btn_close_Click" />
                            <asp:Button ID="btn_print" Text="Print" runat="server" OnClientClick="WindowPrint()"
                                OnClick="btn_print_Click" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
