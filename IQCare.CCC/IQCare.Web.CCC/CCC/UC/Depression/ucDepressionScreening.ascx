<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucDepressionScreening.ascx.cs" Inherits="IQCare.Web.CCC.UC.Depression.ucDepressionScreening" %>
<style>
    .rbList{float: right;}
    .rbList input{margin-left: 5px;}
    .depression-results{padding-top: 10px;padding-bottom10px;}
</style>
<div class="col-md-12 form-group">
	<div class="col-md-12">
		<div class="panel panel-info">
			<div class="panel-body">
				<div class="col-md-12 form-group">
					<label class="control-label pull-left"><span class="text-primary">During the last two weeks have you ever been bothered by:</span></label>
				</div>

				<div class="col-md-12 form-group">
					<div class="row">
                        <div class="col-md-6">
                            <div class="row">
                                <div class="col-md-8 text-left">
							        <label>Feeling down, depressed or hopeless?</label>
						        </div>
						        <div class="col-md-4">
							        <asp:RadioButtonList ClientIDMode="Static" ID="rbBotheredbyHopeless" RepeatColumns="2" RepeatDirection="Horizontal" runat="server" CssClass="rbList" CellPadding="20" CellSpacing="2"> 
                                    </asp:RadioButtonList>
						        </div>
                            </div>
                        </div>
						<div class="col-md-6">
                            <div class="row">
                                <div class="col-md-8 text-left">
							        <label>Little interest or no pleasure in doing things?</label>
						        </div>
						        <div class="col-md-4">
							        <asp:RadioButtonList ClientIDMode="Static" ID="rbBotheredByLittleInterest" RepeatColumns="2" RepeatDirection="Horizontal" runat="server" CssClass="rbList" CellPadding="20" CellSpacing="2"> 
                                    </asp:RadioButtonList>
						        </div>
                            </div>
                        </div>
					</div>
				</div>
			</div>
		</div>

        <!-- PHQ 9 -->
        <div class="panel panel-info">
			<div class="panel-body">
                <div class="col-md-12 form-group">
					<label class="control-label pull-left"><span>PATIENT HAELTH QUESTIONNAIRE (PHQ - 9)</span></label>
				</div>
				<div class="col-md-12 form-group">
					<label class="control-label pull-left"><span class="text-primary">On the last two weeks, how often have you been bothered by any of the following problems?</span></label>
				</div>

				<div class="col-md-12 form-group">
                    <div class="">
                        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="depression-measurements">
                                Depression Severity: <span class="label label-success">0-4 None</span>
                                <span class="label label-primary">5-9 Mild</span> <span class="label label-info">10-14 Moderate</span> <span class="label label-warning">15-19 Moderately Severe</span> <span class="label label-danger">20 - 30 Severe</span>
                            </div>
                        </div>
                    </div>
                    <div class="row depression-results">
                        <div class="col-md-6">
                            <div class="input-group">
                                <span class="input-group-addon">Total</span>                      
                                <asp:TextBox runat="server" ID="HeartRate" ClientIDMode="Static" CssClass="form-control input-sm" placeholder=".." data-parsley-trigger="keyup" data-parsley-type="number" Min="0" Max="200" data-parsley-range="[0, 200]" disabled="disabled"></asp:TextBox>
                                <span class="input-group-addon">/ 30</span>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="input-group">
                                <span class="input-group-addon"><small class="text-danger"></small>Depression Severity</span> 
                                <asp:TextBox runat="server" ID="TextBox1" ClientIDMode="Static" CssClass="form-control input-sm" placeholder="" Type="Number" Min="1" Max="40" data-parsley-range="[1, 40]" data-parsley-range-message="Age foe ZScore is out of range" disabled="disabled"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row depression-results">
                        <div class="col-md-12">
                            <div class="input-group">
                                <span class="input-group-addon"><small class="text-danger"></small>Recommended Management</span> 
                                <asp:TextBox runat="server" ID="AgefoZ" ClientIDMode="Static" CssClass="form-control input-sm" placeholder="" Type="Number" Min="1" Max="40" data-parsley-range="[1, 40]" data-parsley-range-message="Age foe ZScore is out of range" disabled="disabled"></asp:TextBox>
                            </div>
                        </div>
                    </div>
				</div>
			</div>
		</div>
	</div>
</div>
<script type="text/javascript">
    $("#myWizard").on("actionclicked.fu.wizard", function (evt, data) {
        var currentStep = data.step;
        if (currentStep == 1) {
            var depressionId = <%=depressionId%>;
            if (depressionId > 0) {
                updateDepressionScreening(tannersId);
            }
            else {
                AddDepressionScreening();
            }
        }
    });
</script>