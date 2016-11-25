<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" EnableEventValidation="false"
    CodeBehind="FindSchedulePatient.aspx.cs" Inherits="IQCare.Web.Scheduler.FindSchedulePatient" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<%@ Register Assembly="AjaxControlToolkit" TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" %>
<%@ Register Src="~/PatientFinder.ascx" TagName="PatientFinder" TagPrefix="pf" %>
<%@ Register Src="~/Scheduler/PatientAppointmentControl.ascx" TagName="Schedule" TagPrefix="app" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <h2 class="forms" align="left">
        Patient Apointment</h2>
    <div class="rounded">
        <asp:UpdatePanel runat="server" ID="divErrorUp" UpdateMode="Always">
            <ContentTemplate>
                <asp:Panel ID="divError" runat="server" Style="padding: 5px" CssClass="background-color: #FFFFC0; border: solid 1px #C00000"
                    HorizontalAlign="Left" Visible="true">
                    <asp:Label ID="lblError" runat="server" Style="font-weight: bold; color: #800000"
                        Text=""></asp:Label>
                    <asp:HiddenField ID="HFormName" runat="server" />                   
                    <asp:HiddenField ID="HModuleID" runat="server" />
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="divPatientComponent" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <pf:PatientFinder ID="FindPatient" runat="server" FilterByServiceLines="False" IncludeEnrollement="False"
                    AutoLoadRecords="False" NumberofRecords="50" CanAddPatient="False" />
                <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none" OnClick="Button1_Click" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="divScheduleComponet" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <app:Schedule ID="SchedulePatient" runat="server" OpenMode="NEW" />
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnOkAction"  />
            </Triggers>
        </asp:UpdatePanel>
        
         <div style="text-align: center; padding: 10px; white-space: nowrap; border: solid 1px #808080;"
            class="form pad5 center">
            <asp:Button ID="btnBack" runat="server" Text="Close" />
        </div>
    </div>
    
</asp:Content>

