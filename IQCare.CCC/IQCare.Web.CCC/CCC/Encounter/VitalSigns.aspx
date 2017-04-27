<%@ Page Title="" Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="VitalSigns.aspx.cs" Inherits="IQCare.Web.CCC.Encounter.VitalSigns" %>
<%@ Register TagPrefix="uc" TagName="PatientDetails" Src="~/CCC/UC/ucPatientBrief.ascx" %>
<%@ Register TagPrefix="uc" TagName="PatientTriage" Src="~/CCC/UC/ucPatientTriage.ascx" %>
<%@ Register TagPrefix="uc" TagName="FemalVitals" Src="~/CCC/UC/ucFemaleVitals.ascx" %>
<%@ Register TagPrefix="uc" TagName="PatientTriageSummary" Src="~/CCC/UC/ucPatientTriageSummary.ascx" %>
<%@ Register Src="~/CCC/UC/ucExtruder.ascx" TagPrefix="uc" TagName="ucExtruder" %>


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

    <uc:ucExtruder runat="server" ID="ucExtruder" />

    <script type="text/javascript">
    $(document).ready(function () {

        var gender = "<%=Gender%>";
  

        if (gender === 'male') {
            alert("am male");
            $("#divBtnVitals").hide("fast");
        } else if (gender === 'female') {
            alert("am female");
             $("#divBtnVitals").hide("show");
        }
         alert("i dontk know");

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

