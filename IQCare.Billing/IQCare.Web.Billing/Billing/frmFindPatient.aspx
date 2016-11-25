<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Module.master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="frmFindPatient.aspx.cs" 
Inherits="IQCare.Web.Billing.FindPatient" %>
 <%@ MasterType VirtualPath="~/MasterPage/Module.master" %>
<%@ Register src="../PatientFinder.ascx" tagname="PatientFinder" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">

    <uc1:PatientFinder ID="ctrlFindPatient" runat="server"  FilterByServiceLines="False" IncludeEnrollement="False"  AutoLoadRecords="False" NumberofRecords="50" CanAddPatient="False"/>
    <asp:HiddenField ID="HFormName" runat="server" />
     <asp:HiddenField ID="HPatientID" runat="server" />
      <asp:HiddenField ID="HLocationID" runat="server" />
       <asp:HiddenField ID="HModuleID" runat="server" />
       <%--<asp:UpdateProgress ID="sProgress" runat="server" DisplayAfter="5">
                <ProgressTemplate>
                    <div style="width: 100%; height: 100%; position: fixed; top: 0px; left: 0px; vertical-align: middle;">
                        <table style="position: relative; top: 45%; left: 45%; border: solid 1px #808080;
                            background-color: #FFFFC0; width: 110px; height: 24px;" cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="right" valign="middle" style="width: 30px; height: 22px;">
                                    <img src="../Images/loading.gif" height="16px" width="16px" alt="" />
                                </td>
                                <td align="left" valign="middle" style="font-weight: bold; color: #808080; width: 80px;
                                    height: 22px; padding-left: 5px">
                                    Processing....
                                </td>
                            </tr>
                        </table>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>--%>
</asp:Content>
