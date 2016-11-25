<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" EnableEventValidation="false"
 AutoEventWireup="True" CodeBehind="FindLabPatient.aspx.cs" Inherits="IQCare.Web.Laboratory.Request.FindLabPatient" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
  <%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<%@ Register Src="~/ProgressControl.ascx" TagName="progressControl" TagPrefix="uc1" %>
<%@ Register src="~/PatientFinder.ascx" tagname="PatientFinder" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">

    <uc1:PatientFinder ID="ctrlFindPatient" runat="server"  FilterByServiceLines="False" IncludeEnrollement="False"  AutoLoadRecords="False" NumberofRecords="50" CanAddPatient="False"/>
    <asp:HiddenField ID="HFormName" runat="server" />
     <asp:HiddenField ID="HPatientID" runat="server" />
      <asp:HiddenField ID="HLocationID" runat="server" />
       <asp:HiddenField ID="HModuleID" runat="server" />
       <uc1:progressControl ID="progressControl1" runat="server" />
</asp:Content>