<%@ Page Title="" Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="PatientDuplicateFinder.aspx.cs" Inherits="IQCare.Web.CCC.Patient.PatientDuplicateFinder" %>

<%@ Register Src="~/CCC/UC/ucPatientDuplicateFinder.ascx" TagPrefix="IQ" TagName="ucPatientDuplicateFinder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <IQ:ucPatientDuplicateFinder runat="server" id="ucPatientDuplicateFinder" />
</asp:Content>

