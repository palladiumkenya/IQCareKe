<%@ Page Title="" Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="VitalSigns.aspx.cs" Inherits="IQCare.Web.CCC.Encounter.VitalSigns" %>
<%@ Register TagPrefix="uc" TagName="PatientDetails" Src="~/CCC/UC/ucPatientBrief.ascx" %>
<%@ Register TagPrefix="uc" TagName="PatientTriage" Src="~/CCC/UC/ucPatientTriage.ascx" %>
<%@ Register TagPrefix="uc" TagName="FemalVitals" Src="~/CCC/UC/ucFemaleVitals.ascx" %>
<%@ Register TagPrefix="uc" TagName="PatientTriageSummary" Src="~/CCC/UC/ucPatientTriageSummary.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    
    <div class="col-md-12 col-xs-12 col-sm-12">
         <uc:PatientDetails ID="PatientSummary" runat="server" />
         <uc:PatientTriageSummary ID="ptnVitalSummary" runat="server" />
         
        
    </div>
    <div class="col-md-12 col-xs-12 col-sm-12" id="PatientVitals">
        <uc:PatientTriage ID="ptnVitalSigns" runat="server" />
    </div>

    <div class="col-md-12 col-xs-12" id="femaleVitals">
        <uc:FemalVitals ID="ptnFemaleVitals" runat="server" />
    </div>

    <script type="text/javascript">
    $(document).ready(function () {

        var Gender = "<%=Gender%>";

        if (Gender === 'male') { $("#btnFemalVitals").prop("disabled",true); }
        else {
            $("#btnFemalVitals").prop("disabled",false);
        }

        $("#femaleVitals").hide("fast");

        
         $("#btnFemalVitals").click(function () {

             $("#PatientVitals").hide("fast", function () { $("#femaleVitals").show("fast");})
         });

         $("#btnLoadTriage").click(function () {
              $("#femaleVitals").hide("fast", function () { $("#PatientVitals").show("fast");});
         });


        
    });
    </script>

</asp:Content>

