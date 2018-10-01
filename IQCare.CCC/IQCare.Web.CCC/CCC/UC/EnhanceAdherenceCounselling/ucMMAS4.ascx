<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucMMAS4.ascx.cs" Inherits="IQCare.Web.CCC.UC.EnhanceAdherenceCounselling.ucMMAS4" %>
<style>
    .mmrbList{float: right;}
    .mmas4-results{margin-bottom: 30px;}
</style>
<div class="col-md-12 form-group">
	<div class="col-md-12">
		<div class="panel panel-info">
			<div class="panel-body">
				<div class="col-md-12 form-group">
					<label class="control-label pull-left"><span class="text-primary">Morisky Medication Adherence Scale (MMAS - 4)</span></label>
                    <asp:TextBox runat="server" CssClass="sessionnumberTb"/>
				</div>

				<div class="col-md-12 form-group" id="mmasquestionscontainer">
                    <div class="mmas4container">
                        <asp:PlaceHolder ID="PHMMAS4" runat="server"></asp:PlaceHolder>
                        <div class="row mmas4-results">
                            <div class="col-md-6">
                                <asp:PlaceHolder ID="PHMMAS4Scores" runat="server"></asp:PlaceHolder>
                            </div>
                            <div class="col-md-6">
                                <asp:PlaceHolder ID="PHMMAS4Rating" runat="server"></asp:PlaceHolder>
                            </div>
                        </div>
                    </div>
                    <div class="mmas4container" style="display: none">
                        <asp:PlaceHolder ID="PHMMAS8" runat="server"></asp:PlaceHolder>
                        <div class="row mmas8-results">
                            <div class="col-md-6">
                                <asp:PlaceHolder ID="PHMMAS8Scores" runat="server"></asp:PlaceHolder>
                            </div>
                            <div class="col-md-6">
                                <asp:PlaceHolder ID="PHMMAS8Rating" runat="server"></asp:PlaceHolder>
                            </div>
                        </div>
                    </div>
				</div>
			</div>
		</div>
	</div>
</div>
<script type="text/javascript">
    var parentid = "";
    $("input:radio").change(function (evt, data) {
        parentid = $(this).closest('div.eahpanel').attr('id');
        alert(parentid);
    });
    //jQuery(function (evt, data) {
    //    var currentStep = data.step;
    //    alert(currentStep);
    //    $("#sessionnumberTb").val(currentStep);
    //});
    $(document).ready(function (evt, data) {
        parentid = $('.sessionnumberTb').closest('div.eahpanel').attr('id');
        alert(parentid);
    });
</script>