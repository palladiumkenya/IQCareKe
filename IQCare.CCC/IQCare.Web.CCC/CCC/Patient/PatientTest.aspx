<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Greencard.Master" AutoEventWireup="true" CodeBehind="PatientTest.aspx.cs" Inherits="IQCare.Web.CCC.Patient.PatientTest" %>

<%@ Register TagPrefix="uc" TagName="PatientTriage" Src="~/CCC/UC/ucPatientTriage.ascx" %>
<%@ Register TagPrefix="uc" TagName="PatientDetails" Src="~/CCC/UC/ucPatientDetails.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    
    <div class="col-md-12">
        <uc:PatientTriage runat="server"/>
    </div>

</asp:Content>
