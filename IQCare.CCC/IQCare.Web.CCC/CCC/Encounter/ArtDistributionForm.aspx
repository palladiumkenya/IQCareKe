<%@ Page Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="ArtDistributionForm.aspx.cs" Inherits="IQCare.Web.CCC.Encounter.ArtDistributionForm" %>

<%@ Register TagPrefix="uc" TagName="PatientDetails" Src="~/CCC/UC/ucPatientBrief.ascx" %>
<%@ Register TagPrefix="uc" TagName="PharmacyControl" Src="~/CCC/UC/ucPharmacyPrescription.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <div class="col-md-12">
        <uc:PatientDetails ID="PatientSummary" runat="server" />
    </div>

    <div class="col-md-12">
        <div id="callout-labels-inline-block" class="col-md-12  bs-callout bs-callout-primary" style="padding-bottom: 1%">
            <div class="col-md-12" id="AppointmentForm" data-parsley-validate="true" data-show-errors="true">
                <div class="col-md-12 form-group">
                    <div class="col-md-12">
                        <div class="col-md-6">
                            <div class="form-group">
                                <div class="col-md-4">
                                    <label for="ArtRefill" class="control-label pull-left">ART Refill Model</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox runat="server" ID="ArtRefill" CssClass="form-control input-sm" ClientIDMode="Static" required="true"/>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <div class="col-md-12">
                                    <button type="button" class="btn btn-info btn-lg fa fa-plus-circle" id="btnAddPharmacy" data-toggle="modal" data-target="#PharmacyModal"> Pharmacy </button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <hr />
                    </div>
                    <div class="col-md-12">
                        <div class="col-md-6">
                            <div class="form-group">
                                <div class="col-md-8">
                                    <label for="missedDoses" class="control-label pull-left">Any missed doses of ARVs since last clinic visit:</label>
                                </div>
                                <div class="col-md-4">
                                    <label class="pull-left" style="padding-right: 10px">
                                        <input id="mYes" type="radio" name="missedArvDoses" value="1" clientidmode="Static" runat="server" />Yes
                                    </label>
                                    <label class="pull-left" style="padding-right: 10px">
                                        <input id="mNo" type="radio" name="missedArvDoses" value="0" clientidmode="Static" runat="server" data-parsley-required="true" />No
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <div class="col-md-8">
                                    <label for="missedDosesCount" class="control-label pull-left">If Yes, how many missed doses:</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox runat="server" ID="missedDosesCount" CssClass="form-control input-sm" ClientIDMode="Static" Type="Number" Min="1"/>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-12">
                    <hr />
                </div>
            <div class="col-md-12 form-group">
                <div class="col-md-12 form-group">
                    <div class="col-md-12 form-group">
                        <label class="control-label pull-left text-primary">Any current/worsening symptoms:</label>
                    </div>
                    <div class="col-md-3">
                        <div class="col-md-12">
                            <label class="control-label pull-left">Fatigue</label>
                        </div>
                        <div class="col-md-12">
                            <asp:DropDownList runat="server" AutoPostBack="False" CssClass="form-control input-sm" ID="fatigue" ClientIDMode="Static">
                                <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                                <asp:ListItem Text="No" Value="False" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="col-md-12">
                            <label class="control-label pull-left">Fever</label>
                        </div>
                        <div class="col-md-12">
                            <asp:DropDownList runat="server" AutoPostBack="False" CssClass="form-control input-sm" ID="fever" ClientIDMode="Static">
                                <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                                <asp:ListItem Text="No" Value="False" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="col-md-12">
                            <label class="control-label pull-left">Nauesa/Vomitting</label>
                        </div>
                        <div class="col-md-12">
                            <asp:DropDownList runat="server" AutoPostBack="False" CssClass="form-control input-sm" ID="nausea" ClientIDMode="Static">
                                <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                                <asp:ListItem Text="No" Value="False" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="col-md-12">
                            <label class="control-label pull-left">Diarrhea</label>
                        </div>
                        <div class="col-md-12">
                            <asp:DropDownList runat="server" AutoPostBack="False" CssClass="form-control input-sm" ID="diarrhea" ClientIDMode="Static">
                                <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                                <asp:ListItem Text="No" Value="False" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="col-md-12">
                            <label class="control-label pull-left">Cough</label>
                        </div>
                        <div class="col-md-12">
                            <asp:DropDownList runat="server" AutoPostBack="False" CssClass="form-control input-sm" ID="cough" ClientIDMode="Static">
                                <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                                <asp:ListItem Text="No" Value="False" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="col-md-12">
                            <label class="control-label pull-left">Rash</label>
                        </div>
                        <div class="col-md-12">
                            <asp:DropDownList runat="server" AutoPostBack="False" CssClass="form-control input-sm" ID="rash" ClientIDMode="Static">
                                <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                                <asp:ListItem Text="No" Value="False" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="col-md-12">
                            <label class="control-label pull-left">Genital sore/ Discharge</label>
                        </div>
                        <div class="col-md-12">
                            <asp:DropDownList runat="server" AutoPostBack="False" CssClass="form-control input-sm" ID="genitalSore" ClientIDMode="Static">
                                <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                                <asp:ListItem Text="No" Value="False" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="col-md-12">
                            <label class="control-label pull-left">Other</label>
                        </div>
                        <div class="col-md-12">
                            <asp:TextBox runat="server" ID="otherSymptom" CssClass="form-control input-sm" ClientIDMode="Static"/>
                        </div>
                    </div>
                </div>
            </div>
                <div class="col-md-12">
                    <hr />
                </div>
                <div class="col-md-12">
                    <div class="col-md-12">
                    <div class="col-md-8">
                        <div class="form-group">
                            <div class="col-md-8">
                                <label for="missedDoses" class="control-label pull-left">Any new medication prescribed outside the HIV clinic:</label>
                            </div>
                            <div class="col-md-4">
                                <label class="pull-left" style="padding-right: 10px">
                                    <input id="medYes" type="radio" name="newMedication" value="1" clientidmode="Static" runat="server" />Yes
                                </label>
                                <label class="pull-left" style="padding-right: 10px">
                                    <input id="medNo" type="radio" name="newMedication" value="0" clientidmode="Static" runat="server" data-parsley-required="true" />No
                                </label>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <div class="col-md-5">
                                <label for="missedDosesCount" class="control-label pull-left">If Yes, specify:</label>
                            </div>
                            <div class="col-md-7">
                                <asp:TextBox runat="server" ID="TextBox1" CssClass="form-control input-sm" ClientIDMode="Static" Type="Number" Min="1"/>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-8">
                        <div class="form-group">
                            <div class="col-md-8">
                                <label for="missedDoses" class="control-label pull-left">Family Planning:</label>
                            </div>
                            <div class="col-md-4">
                                <label class="pull-left" style="padding-right: 10px">
                                    <input id="fpYes" type="radio" name="familyPlanning" value="1" clientidmode="Static" runat="server" />Yes
                                </label>
                                <label class="pull-left" style="padding-right: 10px">
                                    <input id="fpNo" type="radio" name="familyPlanning" value="0" clientidmode="Static" runat="server" data-parsley-required="true" />No
                                </label>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <div class="col-md-5">
                                <label for="missedDosesCount" class="control-label pull-left">If Yes, specify:</label>
                            </div>
                            <div class="col-md-7">
                                <asp:TextBox runat="server" ID="fpmethod" CssClass="form-control input-sm" ClientIDMode="Static" Type="Number" Min="1"/>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-8">
                        <div class="form-group">
                            <div class="col-md-6">
                                <label for="missedDoses" class="control-label pull-left">Pregnancy Status:</label>
                            </div>
                            <div class="col-md-6">
                                <asp:DropDownList runat="server" AutoPostBack="False" CssClass="form-control input-sm" ID="pregnancyStatus" ClientIDMode="Static">
                                    <asp:ListItem Text="Pregnant" Value="True"></asp:ListItem>
                                    <asp:ListItem Text="NotPregnant" Value="False" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                    </div>
                    <div class="col-md-8">
                        <div class="form-group">
                            <div class="col-md-6">
                                <label for="missedDoses" class="control-label pull-left">Refered to clinic</label>
                            </div>
                            <div class="col-md-4">
                                <label class="pull-left" style="padding-right: 10px">
                                    <input id="refYes" type="radio" name="referredToClinic" value="1" clientidmode="Static" runat="server" />Yes
                                </label>
                                <label class="pull-left" style="padding-right: 10px">
                                    <input id="refNo" type="radio" name="referredToClinic" value="0" clientidmode="Static" runat="server" data-parsley-required="true" />No
                                </label>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <div class="col-md-5">
                                <label for="missedDosesCount" class="control-label pull-left">If Yes, AppointmentDate:</label>
                            </div>
                            <div class="col-md-7">
                                <asp:TextBox runat="server" ID="TextBox3" CssClass="form-control input-sm" ClientIDMode="Static" Type="Number" Min="1"/>
                            </div>
                        </div>
                    </div>
                        <</div>
    </div>
    </div>
            <div class="col-md-12">
                <hr />
            </div>
                <div class="col-md-12">
                    <div class="col-md-3"></div>
                    <div class="col-md-9">
                        <div class="col-md-3">
                            <asp:LinkButton runat="server" ID="btnSave" CssClass="btn btn-info fa fa-plus-circle btn-lg" ClientIDMode="Static" OnClientClick="return false;"> Save </asp:LinkButton>
                        </div>
                        <div class="col-md-3">
                            <asp:LinkButton runat="server" ID="btnReset" CssClass="btn btn-warning  fa fa-refresh btn-lg " ClientIDMode="Static" OnClientClick="return false;"> Reset Form  </asp:LinkButton>
                        </div>
                        <div class="col-md-3">
                            <asp:LinkButton runat="server" ID="btnCancel" CssClass="btn btn-danger fa fa-times btn-lg" ClientIDMode="Static" OnClientClick="return false;"> Close </asp:LinkButton>
                        </div>

                    </div>
                </div>
                <div class="col-md-12">
                    <hr />
                </div>
            </div>
        </div>
    <div class="modal"  id="PharmacyModal" tabindex="-1" role="dialog" aria-labelledby="PharmacyLabel" aria-hidden="true" clientidmode="Static">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content" style="width: 100%">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalLabel">Pharmacy</h4>
                </div>
                <div class="modal-body" style="width: 100%">
                    <div class="row">
                        <uc:PharmacyControl ID="pharmacyDetails" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>
<script type="text/javascript">

    $(document).ready(function () {
    });

</script>

</asp:Content>
