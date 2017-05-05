<%@ Page Title="" Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="PharmacyPrescription.aspx.cs" Inherits="IQCare.Web.CCC.Encounter.PharmacyPrescription" EnableEventValidation="true" %>

<%@ Register Src="~/CCC/UC/ucPharmacyPrescription.ascx" TagPrefix="IQ" TagName="ucPharmacyPrescription" %>
<%@ Register Src="~/CCC/UC/ucPatientBrief.ascx" TagPrefix="IQ" TagName="ucPatientBrief" %>
<%@ Register Src="~/CCC/UC/ucExtruder.ascx" TagPrefix="IQ" TagName="ucExtruder" %>




<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <div>
        <IQ:ucPatientBrief runat="server" ID="ucPatientBrief" />
    </div>

    
    <IQ:ucPharmacyPrescription runat="server" ID="ucPharmacyPrescription" />

    <IQ:ucExtruder runat="server" ID="ucExtruder" />
</asp:Content>
