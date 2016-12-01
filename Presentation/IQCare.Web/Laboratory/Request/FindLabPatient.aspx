<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" EnableEventValidation="false"
    AutoEventWireup="True" CodeBehind="FindLabPatient.aspx.cs" Inherits="IQCare.Web.Laboratory.Request.FindLabPatient" %>
<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <IQ:PatientFinder ID="ctrlFindPatient" runat="server" FilterByServiceLines="False" IncludeEnrollement="False" AutoLoadRecords="False" NumberOfRecords="50" CanAddPatient="False" />
    <asp:HiddenField ID="HFormName" runat="server" />
    <asp:HiddenField ID="HPatientID" runat="server" />
    <asp:HiddenField ID="HLocationID" runat="server" />
    <asp:HiddenField ID="HModuleID" runat="server" />

</asp:Content>
