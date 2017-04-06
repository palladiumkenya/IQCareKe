<%@ Page Title="" Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="VitalSigns.aspx.cs" Inherits="IQCare.Web.CCC.Encounter.VitalSigns" %>
<%@ Register TagPrefix="uc" TagName="PatientDetails" Src="~/CCC/UC/ucPatientBrief.ascx" %>
<%@ Register TagPrefix="uc" TagName="PatientTriage" Src="~/CCC/UC/ucPatientTriage.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    
    <div class="col-md-12">
         <uc:PatientDetails ID="PatientSummary" runat="server" />
         <uc:PatientTriage ID="ptnVitalSigns" runat="server" />
    </div>
</asp:Content>
