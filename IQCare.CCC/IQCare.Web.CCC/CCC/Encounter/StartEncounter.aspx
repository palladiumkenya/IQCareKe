<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Greencard.Master" AutoEventWireup="true" CodeBehind="StartEncounter.aspx.cs" Inherits="IQCare.Web.CCC.Encounter.StartEncounter" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    
    <div class="col-md-12 bs-callout bs-callout-info">
        <div class="col-md-2"></div>
        <div class="col-md-6">
             <div class="col-md-6">
                 <asp:LinkButton runat="server" ID="btnPastEncounter" CssClass="btn btn-warning  fa fa-history fa-3x"> Past Encounter</asp:LinkButton>
             </div>
            <div class="col-md-6">
                 <asp:LinkButton runat="server" ID="btnNewEncounter" CssClass="btn btn-info fa fa-exchange fa fa-3x" OnClientClick='redirect()'> </asp:LinkButton>
            </div>
        </div>
        <div class="col-md-2"></div>
    </div>
    
    <script type="text/javascript">
        $(document).ready(function() {
            
            function redirect() {
                location.href = 'StartEncounter.aspx';
            }
        })
    </script>
</asp:Content>
