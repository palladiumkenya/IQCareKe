<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    CodeBehind="FindAdd.aspx.cs" Inherits="IQCare.Web.Patient.FindAdd" EnableEventValidation="False" %>

<%@ Register Src="../PatientFinder.ascx" TagName="PatientFinder" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <script language="javascript" type="text/jscript">


        function openWaitingList(path) {
            //  window.location.href = './frmWaitingList.aspx';
            window.open(path, 'popupwindow', 'toolbars=no,location=no,directories=no,dependent=yes,top=150,left=150,maximize=yes,resizable=no,width=800,height=500,scrollbars=yes');
        }
        
    </script>
    <div class="container-fluid">
        <br />
        <asp:Button runat="server" CssClass="btn btn-info fa fa-list pull-right" ID="btnWaitingList"
            Text="View Waiting List"></asp:Button>
        <uc1:PatientFinder ID="FindPatient" runat="server" IncludeEnrollement="True" FilterByServiceLines="False"
            AutoLoadRecords="False" NumberOfRecords="50" CanAddPatient="True" FilterByStatus="true" />
        <asp:HiddenField ID="HFormName" runat="server" />
        <asp:HiddenField ID="HPatientID" runat="server" />
        <asp:HiddenField ID="HLocationID" runat="server" />
        <asp:HiddenField ID="HModuleID" runat="server" />
    </div>
    <!-- .container-fluid -->
</asp:Content>
