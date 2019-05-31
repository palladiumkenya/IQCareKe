<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucExtruder.ascx.cs" Inherits="IQCare.Web.CCC.UC.ucExtruder" %>
<%@ Register Src="~/CCC/UC/ucPatientVitalsExtruder.ascx" TagPrefix="IQ" TagName="ucPatientVitalsExtruder" %>
<%@ Register Src="~/CCC/UC/ucPatientLabsExtruder.ascx" TagPrefix="IQ" TagName="ucPatientLabsExtruder" %>
<%@ Register Src="~/CCC/UC/ucPatientWorkPlan.ascx" TagPrefix="IQ" TagName="ucPatientWorkPlan" %>



<script type="text/javascript">

        $(function(){

            $("#extruderLeft").buildMbExtruder({
                position:"left",
                width:400,
                extruderOpacity:.8,
                hidePanelsOnClose:true,
                accordionPanels:true,
                onExtOpen:function(){},
                onExtContentLoad:function(){},
                onExtClose:function(){}
            });

            $("#extruderLeft1").buildMbExtruder({
                position: "left",
                width: 400,
                extruderOpacity: .8,
                hidePanelsOnClose: true,
                accordionPanels: true,
                onExtOpen: function () { },
                onExtContentLoad: function () { },
                onExtClose: function () { }
            });

            $("#extruderLeft2").buildMbExtruder({
                position: "left",
                width: 400,
                extruderOpacity: .8,
                hidePanelsOnClose: true,
                accordionPanels: true,
                onExtOpen: function () { },
                onExtContentLoad: function () { },
                onExtClose: function () { }
            });

        });

    </script>
<style>
    .extruder-content .text{height: 700px !important;}
</style>
<div class="wrapper">
  
    <div style="background-color:white; height:100%">
        <div id="extruderLeft" class="{title:'Patient Vitals'}">
            <IQ:ucPatientVitalsExtruder runat="server" ID="ucPatientVitalsExtruder" />
        </div>

        <div id="extruderLeft1" class="{title:'Lab Results'}">
            <IQ:ucPatientLabsExtruder runat="server" id="ucPatientLabsExtruder" />
        </div>

        <div id="extruderLeft2" class="{title:'Work Plan'}">
            <IQ:ucPatientWorkPlan runat="server" id="ucPatientWorkPlan" />
        </div>
    </div>

</div>