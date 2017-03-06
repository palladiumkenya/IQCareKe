<%@ Page Title="" Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="EncounterHistory.aspx.cs" Inherits="IQCare.Web.CCC.Encounter.EncounterHistory" %>

<%@ Register Src="~/CCC/UC/ucPatientDetails.ascx" TagPrefix="IQ" TagName="ucPatientDetails" %>
<%@ Register Src="~/CCC/UC/ucExtruder.ascx" TagPrefix="IQ" TagName="ucExtruder" %>


<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">

    <div class="col-md-12">
        <IQ:ucPatientDetails runat="server" ID="ucPatientDetails" />

        <div class="col-md-12 bs-callout bs-callout-info">
            <asp:TreeView ID="TreeViewEncounterHistory" ForeColor="#000000" runat="server" Width="100%">
            </asp:TreeView>
        </div>
    </div>

    

    <IQ:ucExtruder runat="server" ID="ucExtruder" />
</asp:Content>
