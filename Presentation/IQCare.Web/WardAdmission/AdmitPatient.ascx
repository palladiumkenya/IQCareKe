<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="AdmitPatient.ascx.cs"
    Inherits="IQCare.Web.WardAdmission.AdmitPatient" %>
<%@ Register Assembly="AjaxControlToolkit" TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" %>
<asp:Panel ID="panelPopup" runat="server" Style="display: none; width: 680px; border: solid 1px #808080;
    background-color: #6699FF" Width="680px" DefaultButton="buttonAdmit">
    <asp:Panel ID="divTitle" runat="server" Style="border: solid 1px #808080; margin: 0px 0px 0px 0px;
        cursor: move; height: 18px">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 18px">
            <tr>
                <td style="width: 5px; height: 19px;">
                </td>
                <td style="width: 100%; height: 19px;">
                    <h2 class="forms" align="left">
                        Admission Form
                    </h2>
                </td>
                <td style="width: 5px; height: 19px;">
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table cellpadding="1" cellspacing="2" border="0" width="680px" style="border: solid 0px #808080;
        background-color: #CCFFFF; margin-bottom: 10px" class="border left whitebg">
        <tr>
            <td colspan="2">
                <table style="width: 100%">
                    <tbody>
                        <tr>
                            <td style="width: 170px; font-weight: bold">
                                Patient Name:
                            </td>
                            <td style="width: 170">
                                <asp:Label ID="lblname" runat="server"></asp:Label>
                            </td>
                            <td style="width: 380; white-space: nowrap" colspan="2">
                                <b>Age:</b>&nbsp;&nbsp;&nbsp;<asp:Label ID="lblAge" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                <b>Sex:</b> &nbsp;&nbsp;&nbsp;<asp:Label ID="lblSex" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold">
                                Patient Facility ID:
                            </td>
                            <td>
                                <asp:Label ID="lblFacilityID" runat="server"></asp:Label>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
        <tr style="display: <% = sVid %>">
            <td colspan="2" align="left">
                <span style="color: #FF0000">*</span> - <i>Indicates a required field</i>
            </td>
        </tr>
        <asp:Panel ID="panelError" runat="server" Style="padding: 5px" CssClass="background-color: #FFFFC0; border: solid 1px #C00000"
            HorizontalAlign="Left" Visible="true">
            <tr>
                <td colspan="2">
                    <asp:Label ID="errorLabel" runat="server" Style="font-weight: bold; color: #800000"
                        Text=""></asp:Label>
                    <asp:HiddenField ID="HPatientID" runat="server" />
                    <asp:HiddenField ID="HAdmissionID" runat="server" Value="-1" />
                    <asp:HiddenField ID="HMode" runat="server" Value="New" />
                    <asp:HiddenField ID="HLocationID" runat="server" />
                    <asp:HiddenField ID="HUserID" runat="server" />
                    <asp:HiddenField ID="HPatientGender" runat="server" />
                    <asp:HiddenField ID="HPatientAge" runat="server" />
                </td>
            </tr>
        </asp:Panel>
        <tr>
            <td colspan="2">
                <hr class="forms" />
            </td>
        </tr>
        <tr>
            <td align="left">
                <span style="color: #FF0000; display: <% = sNew %>">*</span> Select Ward:
            </td>
            <td align="left" style="white-space: nowrap; vertical-align: text-top" nowrap="nowrap"
                valign="top">
                <span style="display: <% = sNew %>">
                    <asp:UpdatePanel ID="divWardPanel" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlPatientWard" runat="server" Width="235px" AutoPostBack="true"
                                OnSelectedIndexChanged="SelectedWardChanged">
                            </asp:DropDownList>
                            <asp:Label runat="server" ID="labelAvailablity" Font-Bold="true" ForeColor="Red"
                                Style="text-align: right; margin-bottom: 10px;" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </span>
                <asp:Label ID="labelWard" runat="server" Visible="false" Font-Bold="true" />
            </td>
        </tr>
        <tr>
            <td align="left" height="25px;">
                <span style="color: #FF0000; display: <% = sVid %>">*</span> Referred From:
            </td>
            <td align="left" height="18px;">
                <span style="color: #FF0000; display: <% = sVid %>">
                    <asp:DropDownList ID="ddlReferral" runat="server" Width="235px" AutoPostBack="false" AppendDataBoundItems="true">
                    </asp:DropDownList>
                </span>
                <asp:Label ID="labelReferred" runat="server" Visible="false" Font-Bold="true" />
            </td>
        </tr>
        <tr id="trOtherSource" style="display: none">
            <td align="left" style="white-space: nowrap">
                &nbsp;&nbsp;Other Source:
            </td>
            <td align="left">
                <asp:TextBox ID="textReferral" runat="server" MaxLength="50" Width="230px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left" style="white-space: nowrap">
                <span style="color: #FF0000; display: <% = sVid %>">*</span> Bed Number:
            </td>
            <td align="left">
                <span style="color: #FF0000; display: <% = sVid %>">
                    <asp:TextBox ID="textBedNumber" runat="server" MaxLength="10" Width="230px"></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                        FilterType="Numbers, UppercaseLetters, LowercaseLetters,Custom" TargetControlID="textBedNumber"
                        ValidChars="-/\">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </span>
                <asp:Label ID="labelBedNumber" runat="server" Visible="false" Font-Bold="true" />
            </td>
        </tr>
       <%-- <tr style="display:none">
            <td align="left" style="white-space: nowrap">
                <span style="color: #FF0000; display: <% = sVid %>">*</span> Admission Number:
            </td>
            <td align="left" style="white-space: nowrap; vertical-align: middle">
                <span style="color: #FF0000; display: <% = sVid %>">
                    <asp:TextBox ID="textAdmissionNumber" runat="server" MaxLength="10" Width="230px"></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FTEID" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,Custom"
                        TargetControlID="textAdmissionNumber" ValidChars="-/\">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </span><span style="display: <% = sNew %>">
                    <asp:CheckBox ID="chkAutoCode" runat="server" Text="Automatic (Recommended)" Visible="true"
                        AutoPostBack="false" /></span>
                <asp:Label ID="labelAdmissionNumber" runat="server" Visible="false" Font-Bold="true" />
            </td>
        </tr>--%>
        <tr>
            <td align="left">
                <span style="color: #FF0000; display: <% = sVid %>">*</span> Admission Date:
            </td>
            <td align="left" style="white-space: nowrap; vertical-align: middle">
                <span style="color: #FF0000; display: <% = sVid %>">
                    <asp:TextBox ID="textAdmissionDate" runat="server" Width="230px" MaxLength="12" AutoComplete="false" ></asp:TextBox>
                    <asp:ImageButton runat="Server" ID="Image1" Height="22" Style="hspace: 3; width: 22;
                        height: 22" ImageUrl="~/Images/cal_icon.gif" AlternateText="Click to show calendar" />
                    <ajaxToolkit:CalendarExtender ID="calendarButtonExtender" runat="server" TargetControlID="textAdmissionDate"
                        PopupButtonID="Image1" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="disable_future_dates"/>
                </span>
                <asp:Label ID="labelAdmissionDate" runat="server" Visible="false" Font-Bold="true" />
            </td>
        </tr>
        <tr>
            <td align="left">
               <span style="display: <% = sVid %>">&nbsp;&nbsp;</span>Expected Discharge:
            </td>
            <td align="left" style="white-space: nowrap; vertical-align: middle">
                <span style="color: #FF0000; display: <% = sVid %>">
                    <asp:TextBox ID="textExpectedDOD" runat="server" Width="230px" MaxLength="12" AutoComplete="false"></asp:TextBox>
                    <asp:ImageButton runat="Server" ID="ImageButton1" Height="22" Style="width: 22; height: 22"
                        ImageUrl="~/Images/cal_icon.gif" AlternateText="Click to show calendar" />
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="textExpectedDOD"
                        PopupButtonID="ImageButton1" Format="dd-MMM-yyyy" />
                </span>
                <asp:Label ID="labelExpectedDOD" runat="server" Visible="false" Font-Bold="true" />
            </td>
        </tr>
        <tr style="display: <% = sEdit %>">
            <td align="left">
                <span style="display: <% = sVid %>">&nbsp;&nbsp;</span>Admitted By:
            </td>
            <td align="left" style="white-space: nowrap; vertical-align: middle">
                <asp:Label ID="labelAdmittedBy" runat="server" Visible="true" Font-Bold="true" />
            </td>
        </tr>
        <tr style="display: <% = inversNew %>">
            <td align="left">
                <span style="display: <% = sVid %>">&nbsp;&nbsp;</span>Discharge
            </td>
            <td align="left" style="white-space: nowrap; vertical-align: middle">
                <asp:Label ID="labelDischarge" runat="server" Visible="true" Font-Bold="true"  />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr class="forms" />
            </td>
        </tr>
        <tr>
            <td class="form pad5 center" style="white-space: nowrap; text-align: center; border: 0"
                colspan="2">
                <div id="divAction" style="white-space: nowrap; border: 0; text-align: center;">
                   <span style="display: <% = sVid %>"> &nbsp;&nbsp;&nbsp;<asp:Button ID="buttonAdmit" runat="server" Text="Save Admission"
                        Width="120px" OnClick="buttonAdmit_Click"  />
                    &nbsp;&nbsp;&nbsp;</span>
                    <asp:Button ID="buttonCancelAddWard" runat="server" Text="Cancel" Width="80px" />
                </div>
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Button ID="buttonRaiseItemPopup" runat="server" Style="display: none" />
<ajaxToolkit:ModalPopupExtender ID="AdmissionDialog" runat="server" TargetControlID="buttonRaiseItemPopup"
    PopupControlID="panelPopup" BackgroundCssClass="modalBackground" DropShadow="True"
    BehaviorID="admisionx572" PopupDragHandleControlID="divTitle" Enabled="True"
    CancelControlID="buttonCancelAddWard" DynamicServicePath="">
</ajaxToolkit:ModalPopupExtender>
