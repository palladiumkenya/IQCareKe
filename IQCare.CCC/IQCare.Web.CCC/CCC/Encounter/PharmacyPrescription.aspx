<%@ Page Title="" Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="PharmacyPrescription.aspx.cs" Inherits="IQCare.Web.CCC.Encounter.PharmacyPrescription" %>

<%@ Register Src="~/CCC/UC/ucPharmacyPrescription.ascx" TagPrefix="IQ" TagName="ucPharmacyPrescription" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <IQ:ucPharmacyPrescription runat="server" ID="ucPharmacyPrescription" />
</asp:Content>
