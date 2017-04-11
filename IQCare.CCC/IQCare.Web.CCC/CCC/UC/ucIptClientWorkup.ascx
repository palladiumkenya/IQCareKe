<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucIptClientWorkup.ascx.cs" Inherits="IQCare.Web.CCC.UC.ucIptClientWorkup" %>

<div class="col-md-12 form-group">
    <div class="panel panel-info">
        <div class="panel-body">
            <div class="col-md-12">
                <div class="col-md-12 form-group">
                    <label class="control-label pull-left">Isoniazid Preventive Therapy Client Work Up</label>
                </div>

                <div class="col-md-12 form-group">
                    <div class="col-md-12 form-group">
                        <label class="control-label pull-left text-primary">Ask for the following</label>
                    </div>
                    <div class="col-md-6">
                        <div class="col-md-12">
                            <label class="control-label pull-left">Yellow Coloured Urine</label>
                        </div>
                        <div class="col-md-12">
                            <asp:DropDownList runat="server" AutoPostBack="False" CssClass="form-control input-sm" ID="urineColour" ClientIDMode="Static">
                                <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                                <asp:ListItem Text="No" Value="False" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="col-md-12">
                            <label class="control-label pull-left">Numbness/Burning sensetion in the hands/feet</label>
                        </div>
                        <div class="col-md-12">
                            <asp:DropDownList runat="server" AutoPostBack="False" CssClass="form-control input-sm" ID="numbness" ClientIDMode="Static">
                                <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                                <asp:ListItem Text="No" Value="False" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-md-12 form-group">
                    <div class="col-md-12 form-group">
                        <label class="control-label pull-left text-primary">Examine for the following</label>
                    </div>
                    <div class="col-md-4">
                        <div class="col-md-12">
                            <label class="control-label pull-left">Yellowness of eyes</label>
                        </div>
                        <div class="col-md-12">
                            <asp:DropDownList runat="server" AutoPostBack="False" CssClass="form-control input-sm" ID="yellowEyes" ClientIDMode="Static">
                                <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                                <asp:ListItem Text="No" Value="False" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="col-md-12">
                            <label class="control-label pull-left">Tenderness in the upper right quadrant of the abdomen</label>
                        </div>
                        <div class="col-md-12">
                            <asp:DropDownList runat="server" AutoPostBack="False" CssClass="form-control input-sm" ID="abdominalTenderness" ClientIDMode="Static">
                                <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                                <asp:ListItem Text="No" Value="False" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="col-md-12">
                            <label class="control-label pull-left">Liver function test results(if available)</label>
                        </div>
                        <div class="col-md-12">
                            <asp:TextBox runat="server" CssClass="form-control input-sm" ID="liverTest" ClientIDMode="Static" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
</div>
</div>

<script type="text/javascript">
    $(document).ready(function () {

    });

</script>
