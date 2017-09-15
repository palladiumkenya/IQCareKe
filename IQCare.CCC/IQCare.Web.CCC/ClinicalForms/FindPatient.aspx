<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    CodeBehind="FindPatient.aspx.cs" Inherits="IQCare.Web.Clinical.FindPatient" EnableEventValidation="False" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <div class="container-fluid">
        <h2 class="forms" align="left">
            Find Patient</h2>
        <IQ:PatientFinder ID="PatientFinder1" runat="server" IncludeEnrollement="False" FilterByServiceLines="True"
            AutoLoadRecords="False" NumberOfRecords="50" CanAddPatient="false" FilterByStatus="true" />
        <asp:HiddenField ID="HFormName" runat="server" />
        <asp:HiddenField ID="HPatientID" runat="server" />
        <asp:HiddenField ID="HLocationID" runat="server" />
        <asp:HiddenField ID="HModuleID" runat="server" />
    </div>
</asp:Content>
