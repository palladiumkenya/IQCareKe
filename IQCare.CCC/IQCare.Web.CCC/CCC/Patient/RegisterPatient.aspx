<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.Greencard.master" AutoEventWireup="true" CodeBehind="RegisterPatient.aspx.cs" Inherits="IQCare.Web.CCC.Patient.PatientRegistration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    
   <div class="col-md-12">
      

    <div class="panel panel-default">
        <div class="panel-body">
            <div class="col-md-12"><label class="control-label"><i class="fa fa-user fa-3x" aria-hidden="true"></i> New Patient Registration</label></div> 
            <div class="col-md-12"><hr /></div>
        </div><%-- .panel-body--%>
    </div><%-- .panel--%>


       
       
   </div><%-- .col-md-12--%>
     
   <script type="text/javascript">
        $(document).ready(function () {
             
            $('#myWizard').wizard();
        })
        ;
    </script>
</asp:Content>
