<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucCRAFFT.ascx.cs" Inherits="IQCare.Web.CCC.UC.Depression.ucCRAFFT" %>
<style>
    /*.rbList{float: right;}
    .rbList input{margin-left: 5px;}*/
    .cageaidrbList td { text-align: left;}
    .depression-results{padding-top: 10px;padding-bottom: 10px;}
</style>
<div class="col-md-12 form-group">
	<div class="col-md-12" id="depressionscreening">
		<div class="panel panel-info">
			<div class="panel-body">
				<div class="col-md-12 form-group">
					<label class="control-label pull-left"><span>CRAFFT Alcohol and Drug Abuse Screening in Adolescents</span></label>
				</div>
                <div class="col-md-12 form-group">
					<label class="control-label pull-left"><span class="text-primary">During the past 12 months, did you</span></label>
				</div>
				<div class="col-md-12 form-group" id="alcoholfrequencyquestions">
                    <asp:PlaceHolder ID="PHCRAFFTFrequency" runat="server"></asp:PlaceHolder>
				</div>
			</div>
		</div>

        <!-- Alcohol screening -->
        <div class="panel panel-info" id="alcoholscreeningpanel">
			<div class="panel-body">
				<div class="col-md-12 form-group">
                    <div class="">
                        <asp:PlaceHolder ID="PHCRAFFTAlcoholScreening" runat="server"></asp:PlaceHolder>
                    </div>
                    <div class="row depression-results">
                        <div class="col-md-6">
                            <asp:PlaceHolder ID="PHCRAFFTScore" runat="server"></asp:PlaceHolder>
                        </div>
                        <div class="col-md-6">
                            <asp:PlaceHolder ID="PHCrafftRisk" runat="server"></asp:PlaceHolder>
                        </div>
                    </div>
				</div>
			</div>
		</div>

        <!--- notes --->
        <div class="panel panel-info" id="notespanel">
			<div class="panel-body">
				<div class="col-md-12 form-group">
					<label class="control-label pull-left"><span class="text-primary">Notes</span></label>
				</div>

				<div class="col-md-12 form-group">
                    <div class="">
                        <asp:PlaceHolder ID="PHCRAFFTNotes" runat="server"></asp:PlaceHolder>
                    </div>
				</div>
			</div>
		</div>
	</div>
</div>