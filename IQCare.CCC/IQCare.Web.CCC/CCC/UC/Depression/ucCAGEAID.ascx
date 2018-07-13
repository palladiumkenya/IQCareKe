<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucCAGEAID.ascx.cs" Inherits="IQCare.Web.CCC.UC.Depression.ucCAGEAID" %>
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
					<label class="control-label pull-left"><span class="text-primary">CAGE-AID SCREENING FOR ALCOHOL AND DRUG USE DISORDERS FOR ADULTS</span></label>
				</div>

				<div class="col-md-12 form-group" id="alcoholfrequencyquestions">
					<div class="row">
                        <asp:PlaceHolder ID="PHCageFrequency" runat="server"></asp:PlaceHolder>
					</div>
				</div>
			</div>
		</div>

        <!-- Alcohol screening -->
        <div class="panel panel-info" id="alcoholscreeningpanel">
			<div class="panel-body">
				<div class="col-md-12 form-group">
					<label class="control-label pull-left"><span class="text-primary">In the last three months</span></label>
				</div>

				<div class="col-md-12 form-group">
                    <div class="">
                        <asp:PlaceHolder ID="PHCAGEAlcoholScreening" runat="server"></asp:PlaceHolder>
                    </div>
                    <div class="row depression-results">
                        <div class="col-md-6">
                            <asp:PlaceHolder ID="PHCAGEAIDScore" runat="server"></asp:PlaceHolder>
                        </div>
                        <div class="col-md-6">
                            <asp:PlaceHolder ID="PHRisk" runat="server"></asp:PlaceHolder>
                        </div>
                    </div>
				</div>
			</div>
		</div>

        <!--- smoking --->
        <div class="panel panel-info" id="smokingscreeningpanel">
			<div class="panel-body">
				<div class="col-md-12 form-group">
					<label class="control-label pull-left"><span class="text-primary">During the past 12 months</span></label>
				</div>

				<div class="col-md-12 form-group">
                    <div class="">
                        <asp:PlaceHolder ID="PHCageSmokingScreening" runat="server"></asp:PlaceHolder>
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
                        <asp:PlaceHolder ID="PHCageNotes" runat="server"></asp:PlaceHolder>
                    </div>
				</div>
			</div>
		</div>
	</div>
</div>