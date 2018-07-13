<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucGBVScreening.ascx.cs" Inherits="IQCare.Web.CCC.UC.Depression.ucGBVScreening" %>
<style>
    /*.rbList{float: right;}
    .rbList input{margin-left: 5px;}*/
    .cageaidrbList td { text-align: left;}
    .depression-results{padding-top: 10px;padding-bottom: 10px;}
</style>
<div class="col-md-12 form-group">
	<div class="col-md-12" id="gbvscreening">
        <div class="panel panel-info">
			<div class="panel-body">
                <div class="col-md-12 form-group">
					<label class="control-label pull-left"><span class="text-primary">SEXUAL AND GENDER BASE VIOLENCE</span></label>
				</div>
				<div class="col-md-12 form-group">
					<label class="control-label pull-left"><span>Many people do not realize that violnce can lead to all kinds of health problems. Because violence is
                        so common in many women's livs, and because there is help available for women being abused, we now ask all female patients about their experinces
                        with violence.</span></label>
				</div>
				<div class="col-md-12 form-group" id="gbvquestionscontainer">
                    <asp:PlaceHolder ID="PHgbvquestions" runat="server"></asp:PlaceHolder>
				</div>
			</div>
		</div>
    </div>
</div>