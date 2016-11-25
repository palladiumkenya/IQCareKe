<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="true"
    CodeBehind="Facility.aspx.cs" Inherits="IQCare.Web.Statistics.Facility" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<%@ Register Src="../FacilityStats.ascx" TagName="FacilityStats" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <div class="row">
        <span id="Span1" class="text-capitalize pull-left glyphicon-text-size= fa-2x" runat="server">
            <i class="fa fa-pie-chart fa-2x" aria-hidden="true"></i>Facility Statistics</span>
    </div>
    <!-- .row -->
    <hr />
    <div class="row">
        <div class="col-md-6">
            <label class="control-label  pull-left">
                Facility/Satellite</label>
            <div class="form-group col-md-6">
                <asp:DropDownList ID="ddFacility" CssClass="form-control col-md-6 input-sm" OnSelectedIndexChanged="ddFacility_SelectedIndexChanged"
                    AutoPostBack="true" runat="server">
                </asp:DropDownList>
            </div>
            <div class="col-md-3" style="text-align: left">
                <asp:CheckBox ID="chkpreferred" CssClass="check-box  pull-left" Text=" Preferred"
                    runat="server" AutoPostBack="true" OnCheckedChanged="chkpreferred_CheckedChanged" />
            </div>
        </div>
    </div>
    <!-- .row-->
    <div class="row">
        <div class="col-md-6">
        </div>
    </div>
    <!-- .row -->
    <div class="row">
        <asp:Panel ID="panelHIV" runat="server" Visible="false">
            <div class="col-md-3 col-sm-3">
                <ul class="list-group">
                    <li class="list-group-item disabled"><i class="fa fa-bar-chart" aria-hidden="true"></i>
                        HIV Care Statistics </li>
                    <li class="list-group-item"><span class="badge">
                        <asp:Label ID="lblTotalActivePatients" CssClass="rightalign" runat="server" Text="0"></asp:Label></span>
                        <asp:LinkButton ID="lnkTotalActivePatients" runat="server" OnClick="hlTotalActivePatients_Click"
                            OnClientClick="return false;">Cummulative Ever</asp:LinkButton>
                    </li>
                    <li class="list-group-item"><span class="badge">
                        <asp:Label ID="lblActiveNonARTPatients" CssClass="rightalign" runat="server" Text="0"></asp:Label></span>
                        <asp:LinkButton ID="lnkActiveNonARTPatients" runat="server" OnClick="hlActiveNonARTPatients_Click"
                            OnClientClick="return false;">Active Non-ART Patients </asp:LinkButton>
                    </li>
                    <li class="list-group-item"><span class="badge">
                        <asp:Label ID="lblActiveARTPatients" CssClass="rightalign" runat="server" Text="0"></asp:Label></span>
                        <asp:LinkButton ID="lnkActiveARTPatients" runat="server" OnClick="hlActiveARTPatient_Click"
                            OnClientClick="return false;">Active ART Patients </asp:LinkButton>
                    </li>
                    <li class="list-group-item"><span class="badge"></span>
                        <asp:LinkButton ID="hlLosttoFollowUp" runat="server" OnClick="hlLosttoFollowUp_Click"
                            OnClientClick="return false;">Lost to Follow up Patient list</asp:LinkButton>
                    </li>
                    <li class="list-group-item"><span class="badge"></span>
                        <asp:LinkButton ID="hlartunknown" runat="server" CssClass="<i class='fa fa-list text-muted'></i>"
                            OnClick="hlartunknown_Click" OnClientClick="return false;">Due for Termination List:</asp:LinkButton>
                    </li>
                    <li class="list-group-item"><span class="badge"></span>
                        <asp:LinkButton ID="lnkmore" runat="server" OnClick="more_Click">more...</asp:LinkButton>
                    </li>
                    <%--                <li class="list-group-item">
                    <span class="badge"></span>
                </li>--%>
                    <%-- <li class="list-group-item"><span class="badge"></span></li>--%>
                </ul>
            </div>
            <!-- .col-md-3 col-sm-3-->
        </asp:Panel>
        <asp:Panel ID="panelPMTCT" runat="server" Visible="false">
            <div class="col-md-3 col-sm-3">
                <ul class="list-group">
                    <li class="list-group-item disabled"><i class="fa fa-female" aria-hidden="true"></i>
                        PMTCT</li>
                    <li class="list-group-item"><span class="badge">
                        <asp:Label ID="lblCurrentMotherPMTCT" CssClass="rightalign" runat="server" Text="0"></asp:Label></span>
                        <asp:LinkButton ID="lnkCurrentMotherPMTCT" runat="server" OnClick="hllnkCurrentMotherPMTCT_Click"
                            OnClientClick="return false;">Current  Mothers in PMTCT</asp:LinkButton>
                    </li>
                </ul>
            </div>
            <!-- .col-md-3 col-sm-3-->
            <div class="col-md-3 col-sm-3">
                <ul class="list-group">
                    <li class="list-group-item disabled"><i class="fa fa-medkit" aria-hidden="true"></i>
                        Current Number of Women on ARV Prophylaxis</li>
                    <li class="list-group-item"><span class="badge">
                        <asp:Label ID="lblANC" CssClass="rightalign" runat="server" Text="0"></asp:Label></span>
                        <asp:LinkButton ID="lnkANC" runat="server" OnClick="hllnkANC_Click" OnClientClick="return false;">ANC</asp:LinkButton>
                    </li>
                    <li class="list-group-item"><span class="badge">
                        <asp:Label ID="lblLD" CssClass="rightalign" runat="server" Text="0"></asp:Label></span>
                        <asp:LinkButton ID="lnkLD" runat="server" OnClick="hllnkLD_Click" OnClientClick="return false;">L&amp;D</asp:LinkButton>
                    </li>
                    <li class="list-group-item"><span class="badge">
                        <asp:Label ID="lblPostnatal" CssClass="rightalign" runat="server" Text="0"></asp:Label></span>
                        <asp:LinkButton ID="Postnatal" runat="server" OnClick="hllnkPostnatal_Click" OnClientClick="return false;">Post Natal</asp:LinkButton>
                    </li>
                    <li class="list-group-item"><span class="badge"></span>
                        <asp:LinkButton ID="lnkmore1" runat="server" OnClick="hlmore1_Click">more...</asp:LinkButton>
                    </li>
                </ul>
            </div>
            <!-- .col-md-3 col-sm-3-->
        </asp:Panel>
        <asp:Panel ID="panelHEI" runat="server" Visible="false">
            <div class="col-md-3 col-sm-3">
                <ul class="list-group">
                    <li class="list-group-item disabled"><i class="fa fa-child" aria-hidden="true"></i>Exposed
                        Infants</li>
                    <li class="list-group-item"><span class="badge">
                        <asp:Label ID="lblCurrentTotalExposedInfants" CssClass="rightalign" runat="server"
                            Text="0"></asp:Label></span>
                        <asp:LinkButton ID="lnkCurrentTotalExposedInfants" runat="server" OnClick="hllnkCurrentTotalExposedInfants_Click"
                            OnClientClick="return false;">Current Total Exposed Infants</asp:LinkButton>
                    </li>
                    <li class="list-group-item"><span class="badge">
                        <asp:Label ID="lblCurrentPMTCTInfants" CssClass="rightalign" runat="server" Text="0"></asp:Label></span>
                        <asp:LinkButton ID="lnkCurrentPMTCTInfants" runat="server" OnClick="hllnkCurrentPMTCTInfants_Click"
                            OnClientClick="return false;">Current PMTCT Infants</asp:LinkButton>
                    </li>
                    <li class="list-group-item"><span class="badge">
                        <asp:Label ID="lblCurrentHIVCareInfants" CssClass="rightalign" runat="server" Text="0"></asp:Label></span>
                        <asp:LinkButton ID="lnkCurrentHIVCareInfants" runat="server" OnClick="hllnkCurrentHIVCareInfants_Click"
                            OnClientClick="return false;">Current HIV Care Infants</asp:LinkButton>
                    </li>
                    <li class="list-group-item"><span class="badge"></span>
                        <asp:LinkButton ID="lnkmore2" runat="server" OnClick="hlmore2_Click">more...</asp:LinkButton>
                    </li>
                </ul>
            </div>
            <!-- .col-md-3 col-sm-3-->
        </asp:Panel>
    </div>
    <!-- .row-->
    <div class="row">
        <asp:Panel ID="pnl_FacTexhAreas" runat="server">
            <div class="">
                <uc1:FacilityStats ID="FacilityStats1" runat="server" />
            </div>
        </asp:Panel>
    </div>
    <div class="row">
        <div class="col-md-3">
            <ul class="list-group">
                <li class="list-group-item active"><i class="fa fa-calendar" aria-hidden="true"></i>
                    Appointment</li>
                <li class="list-group-item"><span class="badge"></span><a class="large" id="DirectScheduler"
                    runat="server">Today's Appointments</a> </li>
                <li class="list-group-item"><span class="badge"></span><a class="large" id="MissedScheduler"
                    runat="server">Missed Appointments</a> </li>
            </ul>
        </div>
        <!-- .col-md-3-->
    </div>
</asp:Content>
