<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    CodeBehind="frmFindAddCustom.aspx.cs" Inherits="IQCare.Web.frmFindAddCustom"
    EnableEventValidation="False" %>

<%@ Register Src="PatientFinder.ascx" TagName="PatientFinder" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <script language="javascript" type="text/jscript">


        function openWaitingList(path) {
            //  window.location.href = './frmWaitingList.aspx';
            window.open(path, 'popupwindow', 'toolbars=no,location=no,directories=no,dependent=yes,top=150,left=150,maximize=yes,resizable=no,width=800,height=500,scrollbars=yes');
        }
        
    </script>
    <div class="container-fluid">
        <asp:Button runat="server" ID="btnWaitingList" Text="View Waiting List" CssClass="btn btn-primary" Height="30px" Width="16%" style="text-align:left"></asp:Button>
        <span class="glyphicon glyphicon-eye-open" style="margin-left:-3%; vertical-align: middle; color: #fff; margin-top:0.25%;"></span>
        <uc1:PatientFinder ID="FindPatient" runat="server" IncludeEnrollement="True" FilterByServiceLines="False"
            AutoLoadRecords="False" NumberOfRecords="100" CanAddPatient="True" />
        <asp:HiddenField ID="HFormName" runat="server" />
        <asp:HiddenField ID="HPatientID" runat="server" />
        <asp:HiddenField ID="HLocationID" runat="server" />
        <asp:HiddenField ID="HModuleID" runat="server" />
        <asp:UpdateProgress ID="sProgress" runat="server" DisplayAfter="5">
            <ProgressTemplate>
                <div style="width: 100%; height: 100%; position: fixed; top: 0px; left: 0px; vertical-align: middle;">
                    <table style="position: relative; top: 45%; left: 45%; border: solid 1px #808080;
                        background-color: #FFFFC0; width: 150px; height: 24px;" cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="right" valign="middle" style="width: 30px; height: 22px;">
                                <img src="./Images/loading.gif" height="16px" width="16px" alt="" />
                            </td>
                            <td align="left" valign="middle" style="font-weight: bold; color: #808080; width: 100px;
                                height: 22px; padding-left: 5px">
                                Processing....
                            </td>
                        </tr>
                    </table>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
</asp:Content>
