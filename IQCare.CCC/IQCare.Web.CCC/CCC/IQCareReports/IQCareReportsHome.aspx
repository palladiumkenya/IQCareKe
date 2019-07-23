<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CCC/Greencard.Master" CodeBehind="IQCareReportsHome.aspx.cs" Inherits="IQCare.Web.CCC.IQCareReports.IQCareReportsHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder"  runat="server">
    <style>
        a{display: block;}
        .reportlink{text-align: left;}
    </style>
    <div class="reportlink">
        <a href="ReportsLineList.aspx?report=txcurr">TX_CURR</a>
        <a href="ReportsLineList.aspx?report=txnew">TX_New</a>
        <a href="ReportsLineList.aspx?report=ltfu">LTFU</a>
        <a href="ReportsLineList.aspx?report=rtc">Re-initiated To Care (RTC)</a>
        <a href="ReportsLineList.aspx?report=defaulters">Defaulters</a>
        <a href="ReportsLineList.aspx?report=appointments">Appointments</a>
        <a href="../../IQCare/Scheduler/frmScheduler_AppointmentMain.aspx?name=Add&AppointmentStatus=Missed">Missed Appointments</a>
        <a href="ReportsLineList.aspx?report=highvls">High VLs</a>
        <a href="ReportsLineList.aspx?report=stableclients">Stable Clients</a>
        <a href="ReportsLineList.aspx?report=unstableclients">Unstable Clients</a>
        <a href="ReportsLineList.aspx?report=htsclients">HTS Clients</a>
        <a href="ReportsLineList.aspx?report=htspositives">HTS Positives</a>
        <a href="ReportsLineList.aspx?report=documentedltfu">Documented Lost To Follow Up</a>
        <a href="ReportsLineList.aspx?report=undocumentedltfu">Undocumented Lost To Follow Up</a>
        <a href="ReportsLineList.aspx?report=labresults">Lab Results</a>
        <a href="ReportsLineList.aspx?report=laborders">Lab Orders</a>
        <a href="ReportsLineList.aspx?report=suppressed">Suppressed</a>
        <a href="ReportsLineList.aspx?report=unsuppressed">Unsuppressed</a>
        <a href="ReportsLineList.aspx?report=tbipt">TB IPT</a>
        <a href="ReportsLineList.aspx?report=dc">Differentiated Care</a>
        <a href="ReportsLineList.aspx?report=transferouts">Transfer Outs</a>
    </div>
</asp:Content>
