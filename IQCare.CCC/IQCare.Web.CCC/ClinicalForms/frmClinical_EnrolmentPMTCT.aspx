<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="IQCare.Web.Clinical.EnrolmentPMTCT"
    Title="Untitled Page" EnableViewState="true" Codebehind="frmClinical_EnrolmentPMTCT.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <script language="javascript" type="text/javascript">

        //Function to Calculate Age in Year and Months
        function CalculateAge(txtAge, txtmonth, txtDOB, txtRegSysDate) {
            
            var YR1 = document.getElementById(txtDOB).value.toString().substr(7, 4);
            var YR2 = document.getElementById(txtRegSysDate).value.toString().substr(7, 4);

            var mm1 = document.getElementById(txtDOB).value.toString().substr(3, 3);
            var mm2 = document.getElementById(txtRegSysDate).value.toString().substr(3, 3);

            var dd1 = document.getElementById(txtDOB).value.toString().substr(0, 2);
            var dd2 = document.getElementById(txtRegSysDate).value.toString().substr(0, 2);

            var nmm1;
            switch (mm1.toLowerCase()) {
                case "jan": nmm1 = "01";
                    break;
                case "feb": nmm1 = "02";
                    break;
                case "mar": nmm1 = "03";
                    break;
                case "apr": nmm1 = "04";
                    break;
                case "may": nmm1 = "05";
                    break;
                case "jun": nmm1 = "06";
                    break;
                case "jul": nmm1 = "07";
                    break;
                case "aug": nmm1 = "08";
                    break;
                case "sep": nmm1 = "09";
                    break;
                case "oct": nmm1 = "10";
                    break;
                case "nov": nmm1 = "11";
                    break;
                case "dec": nmm1 = "12";
                    break;
            }
            var nmm2;
            switch (mm2.toLowerCase()) {
                case "jan": nmm2 = "01";
                    break;
                case "feb": nmm2 = "02";
                    break;
                case "mar": nmm2 = "03";
                    break;
                case "apr": nmm2 = "04";
                    break;
                case "may": nmm2 = "05";
                    break;
                case "jun": nmm2 = "06";
                    break;
                case "jul": nmm2 = "07";
                    break;
                case "aug": nmm2 = "08";
                    break;
                case "sep": nmm2 = "09";
                    break;
                case "oct": nmm2 = "10";
                    break;
                case "nov": nmm2 = "11";
                    break;
                case "dec": nmm2 = "12";
                    break;
            }
            var dt1 = nmm1 + "/" + dd1 + "/" + YR1;
            var dt2 = nmm2 + "/" + dd2 + "/" + YR2;

            var val1 = dateDiff("d", dt1, dt2, "", "") / 365;
            var val2 = Math.round((dateDiff("d", dt1, dt2, "", "") / 365));
            if (val2 > val1) {
                document.getElementById(txtAge).value = Math.round((dateDiff("d", dt1, dt2, "", "") / 365)) - 1;
                var yr = Math.round(dateDiff("d", dt1, dt2, "", "") / 365) - 1;
                document.getElementById(txtmonth).value = Math.round((dateDiff("d", dt1, dt2, "", "") - (365 * yr)) / 30);
            }
            else {
                document.getElementById(txtAge).value = Math.round((dateDiff("d", dt1, dt2, "", "") / 365));
                var yr = Math.round(dateDiff("d", dt1, dt2, "", "") / 365);
                document.getElementById(txtmonth).value = Math.round((dateDiff("d", dt1, dt2, "", "") - (365 * yr)) / 30);
            }
        }
    </script>
    <script language="javascript" type="text/javascript">
        function WindowPrint() {
            window.print();
        }
    </script>
   
    <div style="padding-top: 1px;">
  <%--      <asp:ScriptManager ID="mst" runat="server">
        </asp:ScriptManager>--%>
        <asp:UpdatePanel ID="UpdateMasterLink" runat="server">
            <ContentTemplate>
                <div id="DivPMTCT" runat="server">
                    <div class="border center formbg">
                        <table cellspacing="6" cellpadding="0" width="100%" border="0">
                            <tbody>
                                <tr>
                                    <td style="width: 550px" class="border pad5 whitebg">
                                        <label id="lblPName" for="patientname">
                                            *Patient Name:</label>
                                        <span id="FName" class="smallerlabel">First: </span>
                                        <asp:TextBox ID="TxtFirstName" runat="server" Width="118px" MaxLength="50"></asp:TextBox>
                                        <span id="MName" class="smallerlabel">Middle: </span>
                                        <asp:TextBox ID="TxtMiddleName" runat="server" Width="118px" MaxLength="50"></asp:TextBox>
                                        <span id="LName" class="smallerlabel">Last: </span>
                                        <asp:TextBox ID="TxtLastName" runat="server" Width="118px" MaxLength="50"></asp:TextBox>
                                    </td>
                                    <td class="border pad5 whitebg" width="35%">
                                        <label id="lblregistrationdate" class="required" for="RegistrationDate">
                                            *Enrollment Date:</label>
                                        <asp:TextBox ID="TxtRegistrationDate" runat="server" Width="25%" MaxLength="11"></asp:TextBox>
                                        <img onclick="w_displayDatePicker('<%= TxtRegistrationDate.ClientID %>');" height="22"
                                            alt="Date Helper" hspace="3" src="../images/cal_icon.gif" width="22" border="0" />
                                        <span class="smallerlabel">(DD-MMM-YYYY)</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 550px" class="border pad5 whitebg" nowrap>
                                        <label id="lblgender" class="required" for="gender">
                                            *Sex:</label>
                                        <asp:DropDownList ID="DDGender" runat="server">
                                            <asp:ListItem Value="" Selected="True">-Select-</asp:ListItem>
                                            <asp:ListItem Value="16">Male</asp:ListItem>
                                            <asp:ListItem Value="17">Female</asp:ListItem>
                                        </asp:DropDownList>
                                        <label id="lblDOB" class="required margin15" for="DOB">
                                            *Date of Birth:</label>
                                        <asp:TextBox ID="TxtDOB" runat="server" Width="70px" MaxLength="11"></asp:TextBox>
                                        <img onclick="w_displayDatePicker('<%=TxtDOB.ClientID %>');" height="22" alt="Date Helper"
                                            hspace="3" src="../images/cal_icon.gif" width="20" border="0" />
                                        <span class="smallerlabel">DD-MMM-YYYY </span>
                                        <input id="RbtnDOBPrecExact" onfocus="up(this)" onclick="down(this)" type="radio"
                                            value="1" name="dobPrecision" runat="server" />
                                        <span class="smalllabel">Exact </span>
                                        <input id="RbtnDOBPrecEstimated" onfocus="up(this)" onclick="down(this)" type="radio"
                                            value="0" name="dobPrecision" runat="server" />
                                        <span class="smalllabel">Estimated</span>
                                        <br />
                                        <br />
                                    </td>
                                    <td class="border pad5 whitebg">
                                        <label class="margin20" for="Age">
                                            Age:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                        <asp:TextBox ID="TxtAgeCurrentYears" runat="server" Width="10%" MaxLength="2" ReadOnly="true"></asp:TextBox>
                                        <span class="smallerlabel">yrs</span>
                                        <asp:TextBox ID="TxtAgeCurrentMonths" runat="server" Width="10%" MaxLength="2" ReadOnly="true"></asp:TextBox>
                                        <span class="smallerlabel">mths</span>
                                        <br />
                                        <br />
                                        <label class="margin20" for="AgeatEnrolment">
                                            Age at Registration:</label>
                                        <asp:TextBox ID="TxtAgeEnrollmentYears" runat="server" Width="10%" MaxLength="2"
                                            ReadOnly="true"></asp:TextBox>
                                        <span class="smallerlabel">yrs</span>
                                        <asp:TextBox ID="TxtAgeEnrollmentMonths" runat="server" Width="10%" MaxLength="2"
                                            ReadOnly="true"></asp:TextBox>
                                        <span class="smallerlabel">mths</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 20px" class="form" align="center" colspan="2">
                                        <label for="MaritalStatus">
                                            Marital Status:</label>
                                        <asp:DropDownList ID="DDMaritalStatus" runat="server" EnableViewState="true">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="border  whitebg" width="50%">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 50%" align="right">
                                                    <label for="PMTCT Transfer In:">
                                                        PMTCT Transfer In:</label>
                                                </td>
                                                <td style="width: 50%">
                                                    <asp:CheckBox ID="chkTransferIn" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="border pad5 whitebg">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 50%" align="right">
                                                    <label for="Referred From:">
                                                        Referred From:</label>
                                                </td>
                                                <td style="width: 50%">
                                                    <asp:DropDownList ID="ddReferredFrom" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                            </tbody>
                        </table>
                    </div>
                    <br />
                    <div class="border center formbg">
                        <br />
                        <h2 class="forms" align="left">
                            PMTCT Identification</h2>
                        <table cellspacing="6" cellpadding="0" width="100%" border="0">
                            <tbody>
                                <tr>
                                    <td class="border pad5 whitebg" style="width: 50%">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 50%" align="right">
                                                    <label>
                                                        ANC Number:</label>
                                                </td>
                                                <td style="width: 50%">
                                                    <asp:TextBox ID="TxtANCNumber" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="border pad5 whitebg" style="width: 50%">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 50%" align="right">
                                                    <label for="PMTCTNumber">
                                                        PMTCT Number:</label>
                                                </td>
                                                <td style="width: 50%">
                                                    <asp:TextBox ID="TxtPMTCTNumber" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="border  whitebg" width="50%">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 50%" align="right">
                                                    <label for="AdmissionNumber">
                                                        Admissions Number:</label>
                                                </td>
                                                <td style="width: 50%">
                                                    <asp:TextBox ID="TxtAdmissionNumber" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="border pad5 whitebg">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 50%" align="right">
                                                    <label for="OutpatientNumber">
                                                        Outpatient Number:</label>
                                                </td>
                                                <td style="width: 50%">
                                                    <asp:TextBox ID="TxtOutPatientNumber" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <br />
                        <h2 class="forms" align="left">
                            Contact Information</h2>
                        <table cellspacing="6" cellpadding="0" width="100%" border="0">
                            <tbody>
                                <tr>
                                    <td class="border pad5 whitebg" width="50%">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 50%" align="right">
                                                    <label for="Address">
                                                        Residence/Address/PO Box:</label>
                                                </td>
                                                <td style="width: 50%">
                                                    <asp:TextBox ID="TxtAddress" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="border pad5 whitebg" width="50%">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 50%" align="right">
                                                    <label for="Village_Town_City">
                                                        Village/Town/City Name:</label>
                                                </td>
                                                <td style="width: 50%">
                                                    <asp:DropDownList ID="ddVillageName" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="border pad5 whitebg" width="50%">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 50%" align="right">
                                                    <label for="Parish_District">
                                                        Parish/District:</label>
                                                </td>
                                                <td style="width: 50%">
                                                    <asp:DropDownList ID="ddDistrict" runat="server" EnableViewState="true">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="border pad5 whitebg">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 50%" align="right">
                                                    <label for="PhoneNumber">
                                                        Phone Number:</label>
                                                </td>
                                                <td style="width: 50%">
                                                    <asp:TextBox ID="TxtPhoneNumber" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form" colspan="2">
                                        <asp:Panel ID="pnlCustomList" runat="server" Visible="false" Height="100%" Width="100%"
                                            Wrap="true">
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <br />
                    <div class="border center formbg">
                        <table cellspacing="6" cellpadding="0" width="100%">
                            <tbody>
                                <tr>
                                    <asp:TextBox ID="txtSysDate" runat="server" CssClass="textstylehidden"></asp:TextBox>
                                    <td align="center">
                                        <asp:Button ID="btnsave" Text="Save" runat="server" OnClick="btnsave_Click"></asp:Button>
                                        <asp:Button ID="btnCancel" Text="Close" runat="server" OnClick="btnCancel_Click">
                                        </asp:Button>
                                        <asp:Button ID="btnPrint" Text="Print" runat="server" OnClientClick="WindowPrint()" />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnsave"></asp:PostBackTrigger>
            </Triggers>
        </asp:UpdatePanel>
        <%--   <script language="javascript" type="text/javascript">
            if (typeof(Sys) !== 'undefined')
            Sys.Application.notifyScriptLoaded();
            var pageManager = Sys.WebForms.PageRequestManager.getInstance();
            var uiId = '';
            pageManager.add_beginRequest(myBeginRequestCallback);
            function myBeginRequestCallback(sender, args)
            {
                   var postbackElem = args.get_postBackElement();
                   uiId = postbackElem.id;
                   postbackElem.disabled = true;
            }
             pageManager.add_endRequest(myEndRequestCallback);
             function myEndRequestCallback(sender, args)
             {
                var status='<%=Request.QueryString["name"]%>';
                if(status!='Edit')
                {
                fnshow();
                }
            }
    </script>--%>
    </div>
</asp:Content>
