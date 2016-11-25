<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    EnableViewState="true" Inherits="IQCare.Web.Admin.FacilitySetup" Title="Untitled Page"
    CodeBehind="frmAdmin_FacilitySetup.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <div style="padding-left: 5px; padding-right: 5px; padding-top: 0px;">
        <h3 class="margin">
            <asp:Label ID="lblForm" runat="server" Text="a"></asp:Label></h3>
        <div class="border center formbg" style="padding: 5px;">
            <table class="pad5 formbg" width="100%" border="0" cellpadding="0" cellspacing="6">
                <tr>
                    <td class="border pad5 whitebg" valign="top" width="100%" colspan="2">
                        <table width="100%">
                            <tr>
                                <td align="right" style="width: 50%">
                                    <label for="lblFacility_name" class="required">
                                        Facility/Satellite Name:</label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtfacilityname" runat="server" MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="border pad5 whitebg" valign="top" width="50%">
                        <table width="100%">
                            <tr>
                                <td align="right" style="width: 50%">
                                    <label for="lblFacility_name">
                                        National Id:</label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtNationalId" runat="server" MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="border pad5 whitebg" valign="top" width="50%">
                        <table width="100%">
                            <tr>
                                <td align="right" style="width: 50%">
                                    <label id="lblCountry" runat="server" class="required" for="lblcountry">
                                        Country number:</label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtcountryno" MaxLength="4" runat="server">695</asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="border pad5 whitebg" valign="top" width="50%">
                        <table width="100%">
                            <tr>
                                <td align="right" style="width: 50%">
                                    <label id="lblPOS" runat="server" class="required" for="lblPOS">
                                        Facility number:</label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtLPTF" MaxLength="5" runat="server">11111</asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="border pad5 whitebg" valign="top" width="50%">
                        <table width="100%">
                            <tr>
                                <td align="right" style="width: 50%">
                                    <label id="lblSatellite" runat="server" class="required" for="lblsatellite">
                                        Satellite number:</label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtSatelliteID" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="border pad5 whitebg" valign="middle" width="50%">
                        <table width="100%">
                            <tr>
                                <td align="right" style="width: 50%">
                                    <label id="lblFaclogo" runat="server" for="lblPOS">
                                        Facility Logo:</label>
                                </td>
                                <td align="left">
                                    <input id="Filelogo" style="width: 210px" type="file" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="border pad5 whitebg" valign="top" width="50%">
                        <table width="100%">
                            <tr>
                                <td align="right" style="width: 50%">
                                    <label id="lblFacAddress" runat="server" class="required" for="lblFacAddress">
                                        Facility Address:</label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtFacAddress" Rows="4" TextMode="MultiLine" Width="200" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="border pad5 whitebg" valign="top" width="50%">
                        <table width="100%">
                            <tr>
                                <td align="right" style="width: 50%">
                                    <label id="lbltele" runat="server" for="lbltele" class="required">
                                        Facility Telephone:</label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtFactele" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="border pad5 whitebg" valign="top" width="50%">
                        <table width="100%">
                            <tr>
                                <td align="right" style="width: 50%">
                                    <label id="lblFacCell" runat="server" for="lblsatellite">
                                        Facility Cell:</label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtFacCell" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="border pad5 whitebg" valign="top" width="50%">
                        <table width="100%">
                            <tr>
                                <td align="right" style="width: 50%">
                                    <label id="lblFacFax" runat="server" for="lblPOS">
                                        Facility Fax:</label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtFacFax" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="border pad5 whitebg" valign="top" width="50%">
                        <table width="100%">
                            <tr>
                                <td align="right" style="width: 50%">
                                    <label id="lblFacEmail" runat="server" for="lblsatellite">
                                        Facility Email:</label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtFacEmail" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="border pad5 whitebg" valign="top" width="50%">
                        <table width="100%">
                            <tr>
                                <td align="right" style="width: 50%">
                                    <label id="lblFacURL" runat="server" for="lblPOS">
                                        Facility URL:</label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtFacURL" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="border pad5 whitebg" valign="top" width="50%">
                        <table width="100%">
                            <tr>
                                <td align="right" style="width: 50%">
                                    <label id="lblpharmfoottext" runat="server" for="lblpharmfoottext">
                                        Pharmacy Footer Text:</label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtpharmfoottext" Rows="6" TextMode="MultiLine" Width="200" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="border pad5 whitebg" colspan="2" valign="top" width="100%">
                        <table width="100%">
                            <tr>
                                <td align="right" style="width: 50%">
                                    <label id="lblprestemp" runat="server" for="lblprestemp" class="required">
                                        Pharmacy Prescription Template:</label>
                                </td>
                                <td align="left">
                                    <input id="Radio1" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);"
                                        checked="true" type="radio" value="0" name="templete" runat="server" />
                                    <label style="vertical-align: text-top">
                                        Simple Template</label>
                                    <input id="Radio2" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);"
                                        type="radio" value="1" name="templete" runat="server" />
                                    <label style="vertical-align: text-top">
                                        Expanded Template</label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="border pad5 whitebg" valign="top" width="50%">
                        <table width="100%">
                            <tr>
                                <td align="right" style="width: 50%">
                                    <label id="Label1" runat="server" for="lblPOS">
                                        Province/State:</label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlprovince" runat="server" Width="188px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="border pad5 whitebg" valign="top" width="50%">
                        <table width="100%">
                            <tr>
                                <td align="right" style="width: 50%">
                                    <label id="Label2" runat="server" for="lblsatellite">
                                        District:</label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddldistrict" runat="server" Width="188px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="border pad5 whitebg" valign="top" width="50%" nowrap="nowrap">
                        <table width="100%">
                            <tr>
                                <td align="right" style="width: 50%">
                                    <label for="lblimage">
                                        Facility login image:</label>
                                </td>
                                <td align="left">
                                    <input id="FileLoad" style="width: 210px" type="file" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="border pad5 whitebg" valign="top" width="50%">
                        <table width="100%">
                            <tr>
                                <td align="right" style="width: 50%">
                                    <label for="lblSatellitenumber" class="required">
                                        Appointment grace period:</label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtGrace" MaxLength="3" runat="server">00</asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="border pad5 whitebg" valign="top" width="50%">
                        <table width="100%">
                            <tr>
                                <td style="width: 40%" align="right">
                                    <label for="lblPEPFAR">
                                        PEPFAR funding start date:</label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtPEPFAR_Fund" MaxLength="11" runat="server"></asp:TextBox>
                                    <img onclick="w_displayDatePicker('<%= txtPEPFAR_Fund.ClientID %>');" height="22"
                                        alt="Date Helper" hspace="3" src="../images/cal_icon.gif" width="22" border="0" /><span
                                            class="smallerlabel">(DD-MMM-YYYY)</span>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="border pad5 whitebg" valign="top" width="50%">
                        <table width="100%">
                            <tr>
                                <td align="right" style="width: 50%">
                                    <asp:XmlDataSource ID="CurrencyDT" runat="server" DataFile="~/XMLFiles/Currency.xml">
                                    </asp:XmlDataSource>
                                    <label class="right required" for="lblcurrency">
                                        Currency:
                                    </label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="cmbCurrency" runat="server" Width="188px" DataSourceID="CurrencyDT"
                                        DataTextField="Name" DataValueField="Id">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="border pad5 whitebg" valign="top" width="50%">
                        <table width="100%">
                            <tr>
                                <td align="right" style="width: 50%">
                                    <label style="width: 100%; text-align: right">
                                        Preferred Location:</label>
                                </td>
                                <td align="left" style="width: 50%">
                                    <input type="checkbox" id="chkPreferred" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="border pad5 whitebg" valign="top" width="50%">
                        <table width="100%">
                            <tr>
                                <td align="right" style="width: 50%">
                                    <label style="width: 100%; text-align: right">
                                        Status:</label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddStatus" runat="server" Width="100px">
                                        <asp:ListItem Value="0">Active</asp:ListItem>
                                        <asp:ListItem Value="1">InActive</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="border pad5 whitebg" valign="top" width="50%">
                        <table width="100%">
                            <tr>
                                <td width="50%" align="right">
                                    <div class="center whitebg" id="paperless" runat="server">
                                        <label class="center">
                                            Paperless clinic:
                                            </label>
                                    </div>
                                </td>
                                <td width="50%" align="left">
                                   <input type="checkbox" id="chkPaperlessclinic" runat="server" />
                                </td>
                            </tr>
                        </table>
                        <table width="100%">
                            <tr>
                                <td width="50%" align="right">
                                    <div class="center whitebg" id="strongpassword" runat="server">
                                        <label class="center">
                                            Strong Password:
                                            </label>
                                    </div>
                                </td>
                                <td width="50%" align="left">
                                    <input type="checkbox" id="chkStrongPwd" runat="server" />
                                </td>
                            </tr>
                        </table>
                        <table width="100%">
                            <tr>
                                <td width="50%" align="right">
                                    <div class="center whitebg" id="divexppwd" runat="server">
                                        <label class="center">
                                            Expire Password:
                                       </label>     
                                    </div>
                                </td>
                                <td width="50%" align="left">
                                    <input type="checkbox" id="chkexpPwd" onclick="toggle('divnoofdays');" runat="server" /><div id="divnoofdays" style="display: none">
                                        <label class="margin5">
                                            Number of Days:</label>
                                        <asp:TextBox ID="txtnoofdays" MaxLength="4" size="7" runat="server" />
                                    </div>
                                </td>
                            </tr>
                        </table>                           
                    </td>
                    <td class="border center whitebg" style="width: 50%">
                        <table width="100%">
                            <tr>
                                <td class="pad35">
                                    <div nowrap='nowrap' class="divBorder border left whitebg" align="left" id="PMTCTdiv"
                                        runat="server" width="1%" style="cursor: pointer; height: 280px; overflow: auto;">
                                        <asp:CheckBoxList ID="cblPMTCT" runat="server" RepeatLayout="Flow" Width="70%">
                                        </asp:CheckBoxList>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="center" colspan="2">
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Reset" />
                        <asp:Button ID="btnExit" runat="server" Text="Close" OnClick="btnExit_Click" />
                        <asp:Button ID="btnOk" Text="OK" CssClass="textstylehidden" runat="server" OnClick="btnOk_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <br />
    </div>
    <br />
</asp:Content>
