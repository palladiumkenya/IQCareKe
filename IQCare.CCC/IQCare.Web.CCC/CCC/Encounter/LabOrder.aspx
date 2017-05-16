<%@ Page Title="" Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="LabOrder.aspx.cs" Inherits="IQCare.Web.CCC.Encounter.LabOrder" %>

<%@ Register Src="~/CCC/UC/ucPatientBrief.ascx" TagPrefix="IQ" TagName="ucPatientBrief" %>
<%@ Register Src="~/CCC/UC/ucPatientLabs.ascx" TagPrefix="IQ" TagName="ucPatientLabs" %>
<%@ Register Src="~/CCC/UC/ucExtruder.ascx" TagPrefix="IQ" TagName="ucExtruder" %>



<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <div>
        <IQ:ucPatientBrief runat="server" ID="ucPatientBrief" />
    </div>

    <div>
        <IQ:ucPatientLabs runat="server" ID="ucPatientLabs" />
    </div>

    <IQ:ucExtruder runat="server" ID="ucExtruder" />

</asp:Content>
