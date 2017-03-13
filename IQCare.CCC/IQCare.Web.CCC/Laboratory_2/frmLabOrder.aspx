<%@ Page Language="C#" AutoEventWireup="True" MasterPageFile="~/MasterPage/IQCare.master"
    Inherits="IQCare.Web.Laboratory.frmLabOrder" Title="Untitled Page" MaintainScrollPositionOnPostback="true"
    EnableViewState="true" CodeBehind="frmLabOrder.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <script language="javascript" type="text/javascript">
        function ClearStoolDesc() {
            if (!(document.getElementById('ctl00_IQCareContentPlaceHolder_rdoStoolList').checked) && !(document.getElementById('ctl00_IQCareContentPlaceHolder_rdoStoolList2').checked)) {
                document.getElementById('ctl00_IQCareContentPlaceHolder_stooldesc').value = "";
                document.getElementById('ctl00_IQCareContentPlaceHolder_stooldesc').disabled = true;
            }
            else {
                document.getElementById('ctl00_IQCareContentPlaceHolder_stooldesc').disabled = false;
            }

        }
        function Validate() {
            var sero = document.getElementById('<%=HIVSerologyConfirm.ClientID%>');
            if (document.getElementById('<%=cblPCRResultsPos.ClientID%>').checked == true) {
                //                document.getElementById('< %=HIVSerologyConfirm.ClientID%>').checked = false
                sero.checked = false;
            }
            else if (document.getElementById('<%=cblPCRResultsNeg.ClientID%>').checked == true) {
                //                document.getElementById('< %=HIVSerologyConfirm.ClientID%>').checked = false
                sero.checked = false;
            }
            else if (document.getElementById('<%=cblSerologyPos.ClientID%>').checked == true) {
                //                document.getElementById('< %=HIVSerologyConfirm.ClientID%>').checked = false;
                sero.checked = false;
            }
            else if (document.getElementById('<%=cblSerologyNeg.ClientID%>').checked == true) {
                // document.getElementById('< % =HIVSerologyConfirm.ClientID %>')
                sero.checked = false;
            }

            //            sero.checked = false;
            //            sero.disabled = false;
            //            document.getElementById("sp101").disabled = false;
            //            }
            //            else if (document.getElementById('< %=cblSerologyNeg.ClientID%>').checked == true) {
            //  document.getElementById('< %=HIVSerologyConfirm.ClientID%>').checked = false;
            //            }
        }

        function WindowPrint() {
            window.print();
        }

        function GetControl() {
            var browserName = navigator.appName;
            if (browserName != "Microsoft Internet Explorer") {

                document.forms[0].submit();
            }
            else {
                document.forms[0].submit();
            }

        }
    </script>
    <div class="center" style="padding: 8px;">
        <%-- <form id="frmLabOrder" method="post" runat="server">--%>
        <%-- <h1 class="margin" id="LabtitleId" runat="server"></h1>--%>
        <span style="margin-left: 835px">
            <asp:LinkButton ID="lnkLabTest" runat="server" Text="Edit Lab Order" OnClick="lnkLabTest_Click"></asp:LinkButton></span>
        <div class="border center formbg">
            <!-- DAL: using tables for form layout. Note that there are classes on labels and td. For custom fields, just use the 2 column layout, if there is an uneven number of cells, set last cell colspan="2" and align="center". Probably should talk through this -->
            <table cellspacing="6" cellpadding="0" width="100%" border="0">
                <tbody>
                    <%--<tr>
                    <td class="form" align="center" valign="top" colspan="2">
                        <label class="patientInfo">
                            Patient Name:</label>
                        <asp:Label ID="lblPatientName" CssClass="patientInfo" runat="server"></asp:Label>
                        <label id="lblpatientenrol" class="patientInfo" runat="server">
                            Patient ID:</label>
                        <asp:Label ID="lblpatientenrol1" CssClass="patientInfo" runat="server"></asp:Label>
                        <label id="lblExisclinicID" class="patientInfo" runat="server">
                            File Reference:</label>
                        <asp:Label ID="lblExisclinicID1" CssClass="patientInfo" runat="server"></asp:Label>
                    </td>
                </tr>--%>
                    <!-- DAL add 20061031 -->
                    <tr>
                        <td class="form" align="center" valign="middle" width="50%">
                            <label>
                                Preclinic Labs:</label><asp:CheckBox ID="preclinicLabs" runat="server" />
                        </td>
                        <td class="form" align="center" valign="middle">
                            <label for="LabtobeDone" class="right35">
                                Lab to be done on:</label>
                            <asp:TextBox ID="txtLabtobeDone" MaxLength="11" runat="server" OnTextChanged="preclinic_changed"></asp:TextBox>
                            <img onclick="w_displayDatePicker('<%= txtLabtobeDone.ClientID %>');" height="22"
                                alt="Date Helper" hspace="3" src="../images/cal_icon.gif" width="22" border="0" />
                            <span class="smallerlabel">(DD-MMM-YYYY)</span>
                        </td>
                    </tr>
                </tbody>
            </table>
            <div id="divLabPeriod" runat="server">
                <table cellspacing="6" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td class="form" align="center" valign="middle">
                            <label class="right35">
                                Lab Number:</label>
                            <asp:TextBox ID="txtLabnumber" runat="server"></asp:TextBox>
                        </td>
                        <td class="form" align="center" valign="middle" width="50%">
                            <label>
                                Period Done:</label>
                            <asp:DropDownList ID="ddLabPeriodDone" runat="Server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <asp:Panel ID="panelOrderedLabs" runat="server" Visible="true">
                        <tr>
                            <td colspan="2" class="form" valign="top">
                                <asp:Repeater ID="rptLabTest" runat="server">
                                    <HeaderTemplate>
                                        <table cellpadding="0" cellspacing="5px" style="width: 100%; margin-bottom: 5px"
                                            width="100%">
                                            <tr>
                                                <td style="text-align: left">
                                                    <h1 class="margin" style="margin-left: 20px">
                                                        List of Lab Test
                                                    </h1>
                                                </td>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr style="border-bottom: solid 1px #A0A0A0;" class="gridviewFaclity whitebg">
                                            <td style="white-space: nowrap; padding-left: 20px; padding-right: 15px; height: 25px;
                                                width: 43%;text-align: left">
                                                <span style="width: 200px; color: blue; font-size: 9pt; font-weight: bold; display: inline-block;">
                                                    <%# DataBinder.Eval(Container.DataItem, "[LabName]")%>
                                                    (
                                                    <%# DataBinder.Eval(Container.DataItem, "[SubTestName]")%>
                                                    ) </span>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                    </asp:Panel>
                </table>
            </div>
            <br />
            <div class="border center formbg">
                <br />
                <h2 class="forms" align="left">
                    ARV Related Labs</h2>
                <br />
                <h3 class="forms" align="left">
                    Immunology</h3>
                <table cellspacing="6" cellpadding="0" width="100%" border="0">
                    <tbody>
                        <!-- labels are rendering as spans. I have removed and just used the label tag. -->
                        <tr>
                            <%-- --%>
                            <td class="form" align="left" width="50%" rowspan="2">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <%-- <div id="div9" runat="server" style="white-space: nowrap">--%>
                                            <span id="sp53" style="white-space: nowrap" disabled="disabled">
                                                <label id="lblsero">
                                                    HIV Rapid Test:</label><%--<asp:RadioButtonList runat="server" ID="HIV_Rapid_Test" TextAlign="Right"
                                                            RepeatDirection="Horizontal" Enabled="false">
                                                            <asp:ListItem Value="Pos">Pos</asp:ListItem>
                                                            <asp:ListItem Value="Neg">Neg</asp:ListItem>
                                                        </asp:RadioButtonList>--%>
                                                <input type="radio" runat="server" id="cblSerologyPos" name="rdocblSerologyPCR" value="1"
                                                    onfocus="up(this)" onclick="down(this); Validate()" disabled="disabled" /><label>Pos</label>
                                                <input type="radio" runat="server" id="cblSerologyNeg" name="rdocblSerologyPCR" value="2"
                                                    onfocus="up(this)" onclick="down(this); Validate()" disabled="disabled" /><label>Neg</label>
                                            </span>
                                            <%--</div>--%>
                                        </td>
                                        <td>
                                            <span id="sp101" disabled="disabled">
                                                <%-- value="101" disabled="disabled"--%>
                                                <%-- <div id="div7" runat="server" align="right">--%>
                                                <asp:CheckBox ID="HIVSerologyConfirm" Value="1" Enabled="true" runat="server" Text="Confirmatory"
                                                    TextAlign="Right" />
                                                <%--  <label>
                                                Confirmatory</label> --%>
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <span id="sp110" value="110" disabled="disabled">
                                                <label id="lblPCR">
                                                    PCR:</label>
                                                <input type="radio" runat="server" id="cblPCRResultsPos" name="rdocblSerologyPCR"
                                                    value="3" onfocus="up(this)" onclick="down(this); Validate()" disabled="disabled" /><label>Pos</label>
                                                <input type="radio" runat="server" id="cblPCRResultsNeg" name="rdocblSerologyPCR"
                                                    value="4" onfocus="up(this)" onclick="down(this); Validate()" disabled="disabled" /><label>Neg</label>
                                            </span>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <%--    </div>--%>
                            <td class="form" align="center" id="td1" disabled="true">
                                <label class="left35" id="lblCD4" style="width: 15%">
                                    CD4:</label><asp:TextBox ID="txtCD4" size="9" MaxLength="9" runat="server" Enabled="false"></asp:TextBox>
                                <asp:Label ID="labelCD4" runat="Server" CssClass="smalllabel"></asp:Label>
                                <asp:TextBox ID="txtCD4perc" runat="server" size="9" MaxLength="9" Enabled="false"></asp:TextBox>
                                <asp:Label ID="lblCD4Perc" runat="server" CssClass="smalllabel"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <!-- PCR Results. -->
                            <div id="div5" runat="server">
                                <td class="form" align="center" style="height: 20%" width="50%" id="td2">
                                    <span id="sp103" value="103" disabled="disabled">
                                        <label class="left35" id="lblRPRVDRL">
                                            RPR/VDRL:</label>
                                        <input type="radio" runat="server" id="cblRPRVDRLPos" name="rdoRPRVDRL" value="1"
                                            onfocus="up(this)" onclick="down(this)" disabled="disabled" /><label>Pos</label>
                                        <input type="radio" runat="server" id="cblRPRVDRLNeg" name="rdoRPRVDRL" value="2"
                                            onfocus="up(this)" onclick="down(this)" disabled="disabled" /><label>Neg</label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </span><span id="sp102" value="102" disabled="disabled">
                                        <label id="lblFTA">
                                            FTA:</label>
                                        <input type="radio" runat="server" id="cblFTAPos" name="rdoFTA" value="1" onfocus="up(this)"
                                            onclick="down(this)" disabled="disabled" /><label>Pos</label>
                                        <input type="radio" runat="server" id="cblFTANeg" name="rdoFTA" value="2" onfocus="up(this)"
                                            onclick="down(this)" disabled="disabled" /><label>Neg</label></span>
                                </td>
                            </div>
                        </tr>
                        <tr>
                            <td class="form" width="55%">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 45%">
                                            <table id="tblVL" disabled="disabled" width="100%">
                                                <tr>
                                                    <td style="width: 45%">
                                                        <label id="lblVLoad">
                                                            Viral Load:</label>
                                                    </td>
                                                    <td style="width: 55%" align="left">
                                                        <asp:TextBox ID="txtViralLoad" MaxLength="9" runat="server" Width="40px" Enabled="false"></asp:TextBox>
                                                        <asp:Label ID="lblViralLoad" runat="server" CssClass="smalllabel" Enabled="false"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 55%" align="right">
                                            <table width="100%">
                                                <tr>
                                                    <td id="tdVLUdtck" disabled="disabled" align="left">
                                                        <input id="ChkViral" onclick="toggle('undetect');" runat="server" type="checkbox"
                                                            value="None" name="presentingComplaintsNone" disabled="disabled" /><strong>Undetectable</strong>
                                                    </td>
                                                    <td>
                                                        <div id="undetect" style="display: none;">
                                                            <asp:Label ID="Label4" runat="server" Text="Limit of detection" CssClass="right"></asp:Label>
                                                            <asp:TextBox ID="txtViralLoadDet" MaxLength="9" size="9" runat="server" Width="50px"></asp:TextBox>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td id="tdSP" disabled="disabled" class="form" width="45%">
                                <label id="lblSPlasma" class="right35">
                                    Store Plasma:</label><asp:CheckBox ID="storePlasma" Enabled="false" value="Yes" runat="server" />
                                <label class="smalllabel">
                                    Stored</label>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <h3 class="forms" align="left">
                    Haematology</h3>
                <table cellspacing="6" cellpadding="0" width="100%" border="0">
                    <tbody>
                        <tr>
                            <td class="form" align="center" width="50%">
                                <label id="lblHCT" disabled="true">
                                    HCT:</label>
                                <asp:TextBox ID="txtHCT" MaxLength="9" size="9" runat="server" Enabled="false"></asp:TextBox>
                                <asp:Label ID="lblHCTPercent" runat="server" CssClass="smalllabel" Enabled="false"></asp:Label>
                                <label class="margin20" id="lblHb" disabled="true">
                                    Hb:</label>
                                <asp:TextBox ID="txtHB" MaxLength="9" size="9" runat="server" Enabled="false"></asp:TextBox>
                                <asp:Label ID="lblHb" runat="server" CssClass="smalllabel" Enabled="false"></asp:Label>
                            </td>
                            <td class="form" align="center">
                                <label id="lblWBC" disabled="true">
                                    WBC:</label><asp:TextBox ID="txtWBC" runat="server" MaxLength="9" Enabled="false"></asp:TextBox>
                                <asp:Label ID="lblWBC" runat="server" CssClass="smalllabel" Enabled="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="form" align="center" valign="top">
                                <table>
                                    <tr>
                                        <td colspan="5" align="center">
                                            <label id="lblDiff" disabled="true">
                                                Diff:</label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <label class="right15" id="lblNeut" disabled="true">
                                                <span class="margin10left">Neut: </span>
                                            </label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="neut1" runat="server" size="2" MaxLength="9" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDiffNeut" runat="server" class="smalllabel" Enabled="false"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="neut2" runat="server" MaxLength="9" size="2" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDiffneut2" runat="server" class="smalllabel" Enabled="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <label class="margin10" id="lblLymph" disabled="true">
                                                Lymph:</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="lymph1" runat="server" size="2" MaxLength="9" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDiffLymph" runat="server" class="smalllabel" Enabled="false"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="lymph2" runat="server" MaxLength="9" size="2" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblLymphPerc" runat="server" class="smalllabel" Enabled="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <label class="right15" id="lblMono" disabled="true">
                                                Mono:</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="mono1" runat="server" size="2" MaxLength="9" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDiffMono" runat="server" class="smalllabel" Enabled="false"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="mono2" runat="server" MaxLength="9" size="2" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDiffMono2" runat="server" class="smalllabel" Enabled="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <label class="right15" id="lblEosin" disabled="true">
                                                Eosin:</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="eosin1" runat="server" size="2" MaxLength="9" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDiffEosin" runat="server" class="smalllabel" Enabled="false"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="eosin2" runat="server" MaxLength="9" size="2" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDiffEosinPerc" runat="server" class="smalllabel" Enabled="false"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <div style="padding-top: 6px;">
                                </div>
                            </td>
                            <td class="form" align="center" valign="top">
                                <label id="lblPlats" disabled="true">
                                    Platelets:
                                </label>
                                <asp:TextBox ID="txtPlatelets" runat="server" MaxLength="9" Enabled="false"></asp:TextBox>
                                <asp:Label ID="lblPlatelets" runat="server" CssClass="smalllabel" Enabled="false"></asp:Label>
                                <br />
                                <br />
                                <label id="lblESR" disabled="true">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ESR:
                                </label>
                                <asp:TextBox ID="TxtESR" runat="server" MaxLength="9" Enabled="false"></asp:TextBox>
                                <asp:Label ID="lblESR2" runat="server" CssClass="smalllabel" Enabled="false" Text="mm/hour"></asp:Label>
                                <br />
                                <br />
                            </td>
                        </tr>
                    </tbody>
                </table>
                <h3 class="forms" align="left">
                    Chemistry</h3>
                <table cellspacing="6" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td class="form" align="center" style="height: 24px">
                            <label id="lblASTSGOT_html" disabled="true">
                                AST/SGOT:
                            </label>
                            <asp:TextBox ID="txtAST" runat="server" MaxLength="9" Enabled="false"></asp:TextBox>
                            <asp:Label ID="lblASTSGOT" runat="server" CssClass="smalllabel" Enabled="false"></asp:Label>
                        </td>
                        <td class="form" align="center" width="50%" style="height: 24px">
                            <label id="lblALTSPGT_html" disabled="true">
                                ALT/SPGT:
                            </label>
                            <asp:TextBox ID="txtALT" runat="server" MaxLength="9" Enabled="false"></asp:TextBox>
                            <asp:Label ID="lblAltSpgt" runat="server" CssClass="smalllabel" Enabled="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="form center" colspan="2">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <label id="lblcreatinine" class="right40" disabled="true">
                                            Creatinine:</label><asp:TextBox ID="txtCreatinine" runat="server" MaxLength="4" Enabled="false"></asp:TextBox>
                                        <asp:Label ID="lblCreatinine" runat="server" CssClass="smalllabel" Enabled="false"></asp:Label>
                                        <!--
                   <asp:DropDownList ID="unitsCreatinine" runat="server" AutoPostBack="True" OnSelectedIndexChanged="unitsCreatinine_SelectedIndexChanged" >
                   <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                   <asp:listitem Value="12" Text="mg/dl"></asp:listitem>
                    <asp:listitem Value="106" Text="mmole/L"></asp:listitem>
                   </asp:DropDownList>
                   -->
                                    </td>
                                    <td>
                                        <label id="lblAmylase" class="margin50" disabled="true">
                                            Amylase:</label>
                                        <asp:TextBox ID="txtAmylase" runat="server" MaxLength="9" Enabled="false"></asp:TextBox>
                                        <asp:Label ID="lblAmylase" runat="server" CssClass="smalllabel" Enabled="false"></asp:Label>
                                    </td>
                                    <td>
                                        <div id="lblpreg" runat="server">
                                            <span id="PregSpan" class="margin50" disabled="disabled">
                                                <label id="lblPregnancy" class="margin50">
                                                    Pregnancy:</label>
                                                <input type="radio" runat="server" id="rdoclPreg" name="clPreg" value="1" onfocus="up(this)"
                                                    onclick="down(this)" disabled="disabled" /><label>Pos</label>
                                                <input type="radio" runat="server" id="rdoclPreg2" name="clPreg" value="2" class="margin20left"
                                                    onfocus="up(this)" onclick="down(this)" disabled="disabled" /><label>Neg</label>
                                            </span>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <h3 class="forms" align="left">
                    Microbiology</h3>
                <table cellspacing="6" cellpadding="0" width="100%" border="0">
                    <tbody>
                        <tr>
                            <td class="form" align="center" width="50%" id="tdMparasite" disabled="disabled"
                                style="display: block">
                                <label id="lblMparasite">
                                    Malarial Parasite:</label>
                                <input type="radio" runat="server" id="rdochkMalaria" name="chkMalaria" value="1"
                                    onfocus="up(this)" onclick="down(this)" disabled="disabled" /><label>Present</label>
                                <input type="radio" runat="server" id="rdochkMalaria2" name="chkMalaria" value="2"
                                    class="margin20left" onfocus="up(this)" onclick="down(this)" disabled="disabled" /><label>Absent</label>
                            </td>
                            <td class="form" align="center" id="tdSCr" disabled="disabled">
                                <label id="lblSCryp">
                                    Serum Crypto. Ag:</label>
                                <input type="radio" runat="server" id="rdorbSerum1" name="rbSerum" value="1" onfocus="up(this)"
                                    onclick="down(this)" disabled="disabled" /><label>Pos</label>
                                <input type="radio" runat="server" id="rdorbSerum2" name="rbSerum" value="2" class="margin20left"
                                    onfocus="up(this)" onclick="down(this)" disabled="disabled" /><label>Neg</label>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <h3 class="forms" align="left">
                    Sputum</h3>
                <table cellspacing="6" cellpadding="0" width="100%" border="0">
                    <tbody>
                        <tr>
                            <td class="form" align="center" width="50%" valign="top">
                                <label id="lblSpAFB" disabled="true">
                                    Sputum AFB &nbsp;#1:</label>
                                <span id="sp1AFB" disabled="disabled">
                                    <input type="radio" id="sputumafb1" name="sputumafb1" value="1" onfocus="up(this)"
                                        onclick="down(this)" runat="server" disabled="disabled" /><label>Pos</label>
                                    <input type="radio" id="sputumafb12" name="sputumafb1" value="2" class="margin20left"
                                        onfocus="up(this)" onclick="down(this)" runat="server" disabled="disabled" /><label>Neg</label></span><br />
                                &nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                                <span id="sp2AFB" disabled="disabled">
                                    <label id="lblSpAFB2">
                                        #2:</label>
                                    <input type="radio" id="sputumafb21" name="sputumafb21" value="1" onfocus="up(this)"
                                        onclick="down(this)" runat="server" disabled="disabled" /><label>Pos</label>
                                    <input type="radio" id="sputumafb22" name="sputumafb21" value="2" class="margin20left"
                                        onfocus="up(this)" onclick="down(this)" runat="server" disabled="disabled" /><label>Neg</label></span><br />
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<span
                                    id="sp3AFB" disabled="disabled"><label id="lblSpAFB3">
                                        #3:
                                    </label>
                                    <input type="radio" id="sputumafb3" name="sputumafb3" value="1" onfocus="up(this)"
                                        onclick="down(this)" runat="server" disabled="disabled" /><label>Pos</label>
                                    <input type="radio" id="sputumafb31" name="sputumafb3" value="2" class="margin20left"
                                        onfocus="up(this)" onclick="down(this)" runat="server" disabled="disabled" /><label>Neg</label></span><br />
                            </td>
                            <td class="form" align="center" width="50%" valign="top" id="tdGStain" disabled="disabled">
                                <label id="lblGstain">
                                    Gram Stain:</label>
                                <div class="center">
                                    <textarea name="gramStain" id="txtgramStain" rows="4" cols="70" disabled="disabled"
                                        runat="server"></textarea></div>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <h3 class="forms" align="left">
                    Urine</h3>
                <table cellspacing="6" cellpadding="0" width="100%" border="0">
                    <tbody>
                        <tr>
                            <td class="form" width="50%" style="height: 219px" align="left" valign="top">
                                <table>
                                    <tr>
                                        <td colspan="3">
                                            <label id="lblurilysis" disabled="true">
                                                Urinalysis:</label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <label id="lblspecGrav" disabled="true">
                                                Spec Grav:</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtUrinalysis1" runat="server" size="4" MaxLength="9" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblUrinalysisSpecGrav" runat="server" Enabled="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <label id="lblGlucose" disabled="true">
                                                Glucose:</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtUrinalysis2" runat="server" size="4" MaxLength="9" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblUrinalysisGlucose" runat="server" Enabled="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <label id="lblKetone" disabled="true">
                                                Ketone:</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtUrinalysis3" runat="server" size="4" MaxLength="9" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblUrinalysisKetone" runat="server" Enabled="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <label id="lblProtein" disabled="true">
                                                Protein:</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtUrinalysis4" runat="server" size="4" MaxLength="9" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblUrinalysisProtein" runat="server" Enabled="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <label id="lblLeuk" disabled="true">
                                                Leuk Est:</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtUrinalysis5" runat="server" size="4" MaxLength="9" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblUrinalysisLeukEst" runat="server" Enabled="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <label id="lblNitrate" disabled="true">
                                                Nitrate:</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtUrinalysis6" runat="server" size="4" MaxLength="9" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblUrinalysisNitrate" runat="server" Enabled="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <label id="lblBlood" disabled="true">
                                                Blood:</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtUrinalysis7" runat="server" size="4" MaxLength="9" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblUrinalysisBlood" runat="server" Enabled="false"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <div style="padding-top: 6px;">
                                </div>
                            </td>
                            <td class="form" width="50%" valign="top" style="height: 219px" align="left" nowrap="nowrap">
                                <label id="lblUrinalysis" disabled="true">
                                    Urinalysis:</label><br />
                                <label class="smalllabel right30" id="lblUrinMBlood" disabled="true">
                                    Urine Microscopic Blood:</label>
                                <asp:DropDownList ID="urineblood" runat="server" Enabled="false">
                                </asp:DropDownList>
                                <label class="smalllabel margin10" id="lblUrMicrWBC" disabled="true">
                                    Urine Microscopic WBC:</label>
                                <asp:DropDownList ID="urineWBC" runat="server" Enabled="false">
                                </asp:DropDownList>
                                <div style="padding-top: 9px;">
                                </div>
                                <label class="smalllabel right30" id="lblUrMicrBac" disabled="true">
                                    Urine Microscopic Bact :
                                </label>
                                <asp:DropDownList ID="urineBact" runat="server" Enabled="false">
                                </asp:DropDownList>
                                <br />
                                <br />
                                <div class="divborder" align="left" id="Div1" style="height: 128px;">
                                    <label class="pad5" id="lblUrinMicCast" disabled="true">
                                        Urine Microscopic Casts:</label>
                                    <br />
                                    <asp:CheckBoxList ID="urineCasts" runat="server" Width="20%" CssClass="divBorder"
                                        RepeatLayout="Flow" Enabled="false">
                                    </asp:CheckBoxList>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="form" width="50%" valign="top" align="left">
                                <label class="right20" id="lblCulture" disabled="true">
                                    Culture/Sensitivity:</label><br />
                                <textarea name="cultureSensitivity" rows="3" cols="70" id="txtcultureSensitivity"
                                    runat="server" disabled="disabled"></textarea><br />
                                <br />
                                <br />
                            </td>
                            <td class="form" valign="top" style="width: 49%" align="left">
                                &nbsp;<label class="right15" id="lblStool" disabled="disabled">Stool O/P:</label>
                                <div id="divstool" disabled="disabled">
                                    <input type="radio" runat="server" id="rdoStoolList" disabled="disabled" name="StoolList"
                                        value="1" onfocus="up(this)" onclick="down(this);ClearStoolDesc();" /><label>Pos</label>
                                    <input type="radio" runat="server" disabled="disabled" id="rdoStoolList2" name="StoolList"
                                        value="2" class="margin20left" onfocus="up(this)" onclick="down(this);ClearStoolDesc();" /><label>Neg<br />
                                        </label>
                                </div>
                                <label class="right20" style="width: 28%" id="lblstoolDesc" disabled="true">
                                    Stool Description:</label>&nbsp;
                                <br />
                                <textarea name="stooldesc" rows="3" cols="70" id="stooldesc" runat="server" disabled="disabled"></textarea><br />
                            </td>
                        </tr>
                    </tbody>
                </table>
                <h3 class="forms" align="left">
                    CSF</h3>
                <table cellspacing="6" cellpadding="0" border="0" width="100%">
                    <tbody>
                        <tr>
                            <td class="form" align="center" width="50%">
                                <table width="100%">
                                    <tr id="trCry" disabled="disabled">
                                        <td align="right" style="width: 50%">
                                            <label id="lblCrypAg">
                                                Cryptococcal Ag:</label>
                                        </td>
                                        <td align="left">
                                            <input type="radio" id="rdocblSerumCrypto1" name="cblSerumCrypto" value="1" onfocus="up(this)"
                                                onclick="down(this)" runat="server" disabled="disabled" /><label>Pos</label>
                                            <input type="radio" id="rdocblSerumCrypto2" name="cblSerumCrypto" value="2" class="margin20left"
                                                onfocus="up(this)" onclick="down(this)" runat="server" disabled="disabled" /><label>Neg</label>
                                        </td>
                                    </tr>
                                    <tr id="trCSFInk" disabled="disabled">
                                        <td align="right" style="width: 50%">
                                            <label id="lblCSFInk">
                                                CSF India Ink:</label>
                                        </td>
                                        <td align="left">
                                            <input type="radio" id="rdocsfIndiaInk1" name="rdocsfIndiaInk1" value="1" onfocus="up(this)"
                                                onclick="down(this)" runat="server" disabled="disabled" /><label>Pos</label>
                                            <input type="radio" id="rdocsfIndiaInk2" name="rdocsfIndiaInk1" value="2" class="margin20left"
                                                onfocus="up(this)" onclick="down(this)" runat="server" disabled="disabled" /><label>Neg</label>
                                        </td>
                                    </tr>
                                    <tr id="trGStain" disabled="disabled">
                                        <td align="right" style="width: 50%">
                                            <label id="lblCSFGram">
                                                CSF Gram Stain:</label>
                                        </td>
                                        <td align="left">
                                            <input type="radio" id="rdocsfgramstain1" name="rdocsfgramstain1" value="1" onfocus="up(this)"
                                                onclick="down(this)" runat="server" disabled="disabled" /><label>Pos</label>
                                            <input type="radio" id="rdocsfgramstain2" name="rdocsfgramstain1" value="2" class="margin20left"
                                                onfocus="up(this)" onclick="down(this)" runat="server" disabled="disabled" /><label>Neg</label>
                                        </td>
                                    </tr>
                                    <tr id="trCulture" disabled="disabled">
                                        <td align="right" style="width: 30%">
                                            <label id="lblCultur">
                                                Culture:</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="csfCulture" runat="server" MaxLength="255" Width="274px" Height="19px"
                                                Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="form" align="center" width="50%">
                                <table width="100%">
                                    <tr>
                                        <td align="right" style="width: 40%">
                                            <label id="lblCellcount" disabled="true">
                                                Cell count: <span class="margin50"></span>RBC's:
                                            </label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtRBCs" runat="server" MaxLength="9" Rows="4" Enabled="false"></asp:TextBox>
                                            <asp:Label ID="lblCellCountRBC" runat="server" CssClass="smalllabel" Enabled="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 40%">
                                            <label id="lblWBChtml" disabled="true">
                                                WBC's:
                                            </label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtWBCs" runat="server" MaxLength="9" Rows="4" Enabled="false"></asp:TextBox>
                                            <asp:Label ID="lblCellCountWBC" runat="server" CssClass="smalllabel" Enabled="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 40%">
                                            <label id="lblNeuthtml" disabled="true">
                                                Neutrophils:
                                            </label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNeutrophils" runat="server" Rows="3" Enabled="false"></asp:TextBox>
                                            <asp:Label ID="lblCellCountNeutrophils" runat="server" CssClass="smalllabel" Enabled="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 40%">
                                            <label id="lblLymp" disabled="true">
                                                Lymphocytes:
                                            </label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtLymphocytes" runat="server" MaxLength="9" Enabled="false"></asp:TextBox>
                                            <asp:Label ID="lblCellCountLymphocytes" runat="server" CssClass="smalllabel" Enabled="false"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="form center" colspan="2">
                                <label id="lblbiochem" disabled="true">
                                    Biochemistry:</label>
                                <label class="margin20" id="lblGlucosehtml" disabled="true">
                                    Glucose:</label>
                                <asp:TextBox ID="txtGlucose" runat="server" MaxLength="9" Rows="9" Enabled="false"></asp:TextBox>
                                <asp:Label ID="lblGlucose" runat="server" CssClass="smalllabel" Enabled="false"></asp:Label>
                                <!--
                        <asp:dropdownlist ID="unitsGlucose" runat="server" >
                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                        <asp:ListItem Value="109" Text="mg/dl"></asp:ListItem>
                        <asp:ListItem Value="110" Text="mmole/L"></asp:ListItem>
                        </asp:dropdownlist>
                        -->
                                <label class="margin50" id="lblProteinhtml" disabled="true">
                                    Protein:</label>
                                <asp:TextBox ID="txtProtein" runat="server" MaxLength="9" Enabled="false"></asp:TextBox>
                                <asp:Label ID="lblProtein" runat="server" CssClass="smalllabel" Enabled="false"></asp:Label>
                                <!--
                            <asp:dropdownlist ID="unitsProtein" runat="server">
                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                            <asp:ListItem Value="111" Text="mg/dl"></asp:ListItem>
                            <asp:ListItem Value="112" Text="mmole/L"></asp:ListItem>
                            </asp:dropdownlist>
                        -->
                            </td>
                        </tr>
                    </tbody>
                </table>
                <h3 class="forms" align="left">
                    Chest X-Ray</h3>
                <table cellspacing="6" cellpadding="0" border="0" width="100%">
                    <tbody>
                        <tr>
                            <td class="form" align="left">
                                <label id="lblCXray" disabled="true">
                                    Chest X-Ray:
                                </label>
                                <div id="divX-Ray" disabled="disabled">
                                    <input type="radio" runat="server" id="rdochestxray1" disabled="disabled" name="listchestxray"
                                        value="1" onfocus="up(this)" onclick="down(this)" /><label>Normal</label>
                                    <input type="radio" runat="server" id="rdochestxray2" disabled="disabled" name="listchestxray"
                                        value="2" class="margin20left" onfocus="up(this)" onclick="down(this)" /><label>Abnormal</label></div>
                                <div class="center" id="divtxtarea" disabled="disabled">
                                    <textarea id="xraytxt" name="xraytxt" disabled="disabled" cols="140" rows="5" runat="server"></textarea>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <br />
                <!-- note: using rendered HTML. The display HTML has been cleaned up as shown below. Will need to be jibed -->
                <!--<a id="A1" class="button" onclick="show('tempOtherLabs');hide('otherLabs')">Other Labs ADDITIONAL LABS</a>  CssClass="button"  BackColor="Green" BorderColor="Green"-->
                <asp:Button ID="OtherLabs" runat="server" OnClick="OtherLabs_Click" Text="Additional Labs"
                    Width="116px" />
                <!-- Button ID="OtherLab" CssClass="marginbuttonPharmacy" Text="Other Labs" OnClick="OtherLabs_Click" />-->
                <br />
                <br />
            </div>
            <br />
            <!-- other labs here. Using static HTML for demo. -->
            <div id="tempOtherLabs">
                <h2 class="forms" align="left">
                    Additional Labs</h2>
                <br />
                <div class="border center formbg">
                    <table cellspacing="6" cellpadding="0" width="100%" border="0">
                        <tbody>
                            <tr>
                                <td class="form" align="left">
                                    <div class="checkboxexport">
                                        <asp:Panel ID="PnlHeading" runat="server" Height="100%" Width="100%" Wrap="false">
                                            <strong>Lab Name &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                &nbsp; &nbsp; &nbsp; Lab Results &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; AR</strong></asp:Panel>
                                        <asp:Panel ID="PnlLab" runat="server" Height="100%" Width="100%" Wrap="true">
                                        </asp:Panel>
                                    </div>
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            <br />
            <div class="border center formbg">
                <br />
                <h2 class="forms" align="left">
                    Approval and Signatures</h2>
                <table cellspacing="6" cellpadding="0" border="0" width="100%">
                    <tbody>
                        <tr>
                            <td class="form" align="center" width="50%" style="height: 49px">
                                <label class="required">
                                    *Ordered by:</label>
                                <asp:DropDownList ID="ddLabOrderedbyName" runat="Server">
                                </asp:DropDownList>
                            </td>
                            <td class="form" align="center" style="height: 49px">
                                <label class="required" for="LabOrderedbyDate">
                                    *Ordered By Date:</label>
                                <asp:TextBox ID="txtLabOrderedbyDate" MaxLength="12" size="11" runat="server"></asp:TextBox>
                                <img id="appDateimg1" onclick="w_displayDatePicker('<%=txtLabOrderedbyDate.ClientID%>');"
                                    height="22" alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="22"
                                    border="0" name="appDateimg">
                                <span class="smallerlabel" id="appDatespan1">(DD-MMM-YYYY)</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="form" align="center">
                                <label id="lblreportedby" runat="server">
                                    Reported by:</label>
                                <asp:DropDownList ID="ddlLabReportedbyName" runat="Server">
                                </asp:DropDownList>
                            </td>
                            <td class="form" align="center">
                                <label id="lblreportedbydate" runat="server" for="labReportedbyDate">
                                    Reported By Date:</label>
                                <asp:TextBox ID="txtlabReportedbyDate" MaxLength="12" size="11" runat="server"></asp:TextBox>
                                <img id="IMG1" onclick="w_displayDatePicker('<%=txtlabReportedbyDate.ClientID%>');"
                                    height="22" alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="22"
                                    border="0" name="appDateimg" />
                                <span class="smallerlabel" id="SPAN1">(DD-MMM-YYYY)</span>
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
                                <asp:Button ID="btnsave" runat="server" Text="Save" OnClick="btnsave_Click" />
                                <asp:Button ID="btncomplete" CssClass="textstylehidden" runat="server" Text="Data Quality Check"
                                    OnClick="btncomplete_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Back" OnClick="btnCancel_Click" />
                                <asp:Button ID="btnok" runat="server" CssClass="textstylehidden" Text="OK" OnClick="btnok_Click" />
                                <asp:Button ID="btnPrint" Text="Print" runat="server" OnClientClick="WindowPrint()" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <br />
        </div>
    </div>
</asp:Content>
