<%@ Page Title="" Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="PatientHome.aspx.cs" Inherits="IQCare.Web.CCC.Patient.PatientHome" %>

<%@ Register Src="~/CCC/UC/ucPatientDetails.ascx" TagPrefix="IQ" TagName="ucPatientDetails" %>
<%@ Register Src="~/CCC/UC/ucExtruder.ascx" TagPrefix="IQ" TagName="ucExtruder" %>


<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <div class="row">
        <div class="col-md-12">
            <IQ:ucPatientDetails runat="server" ID="ucPatientDetails" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-3">
            <div class="bs-component">
                <div class="alert alert-dismissible alert-danger">
                    <button type="button" class="close" data-dismiss="alert">
                        &times;</button>
                    <strong>Last VL Results : </strong><span class="badge">
                        <asp:Label ID="lblviralload" runat="server"></asp:Label></span>
                </div>
                <!-- .alert -->
            </div>
            <!-- .bs-component-->
        </div>
        <!-- .col-lg-3 -->
        <div class="col-md-3">
            <div class="bs-component">
                <div class="alert alert-dismissible alert-warning">
                    <button type="button" class="close" data-dismiss="alert">
                        &times;</button>
                    <strong>VL Due Date : </strong><span class="badge">
                        <asp:Label ID="lblVLDueDate" runat="server"></asp:Label></span>
                </div>
                <!-- .alert -->
            </div>
            <!-- .bs-component-->
        </div>
        <!-- .col-lg-3 -->
        <div class="col-md-3">
            <div class="bs-component">
                <div class="alert alert-dismissible alert-success">
                    <button type="button" class="close" data-dismiss="alert">
                        &times;</button>
                    <strong>Current Regimen </strong><span class="badge">
                        <asp:Label ID="lblCurARVRegimen" runat="server"></asp:Label></span>
                </div>
                <!-- .alert -->
            </div>
            <!-- .bs-component-->
        </div>
        <!-- .col-lg-3 -->
        <div class="col-md-3">
            <div class="bs-component">
                <div class="alert alert-dismissible alert-danger">
                    <button type="button" class="close" data-dismiss="alert">
                        &times;</button>
                    <strong>Most Recent CD4 : </strong><span class="badge">
                        <asp:Label ID="lblrecentCD4ValueDate" runat="server"></asp:Label></span>
                </div>
                <!-- .alert -->
            </div>
            <!-- .bs-component-->
        </div>
        <!-- .col-lg-3 -->
    </div>
    
    <div class="col-md-12 col-xs-12 col-sm-12">
        
        
<!-- Nav tabs -->
<ul class="nav nav-tabs md-pills pills-ins" role="tablist">
    <li class="nav-item">
        <a class="nav-link active" data-toggle="tab" href="#panel11" role="tab"><i class="fa fa-user"></i> Patient Baseline Summary </a>
    </li>
    <li class="nav-item">
        <a class="nav-link" data-toggle="tab" href="#panel12" role="tab"><i class="fa fa-heart"></i> Follow</a>
    </li>
    <li class="nav-item">
        <a class="nav-link" data-toggle="tab" href="#panel13" role="tab"><i class="fa fa-envelope"></i> Contact</a>
    </li>
</ul>

<!-- Tab panels -->
<div class="tab-content">

    <!--Panel 1-->
    <div class="tab-pane fade in show active" id="panel11" role="tabpanel">
        <br>

        <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Nihil odit magnam minima, soluta doloribus reiciendis molestiae placeat unde eos molestias. Quisquam aperiam, pariatur. Tempora, placeat ratione porro voluptate odit minima.</p>

    </div>
    <!--/.Panel 1-->

    <!--Panel 2-->
    <div class="tab-pane fade" id="panel12" role="tabpanel">
        <br>

        <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Nihil odit magnam minima, soluta doloribus reiciendis molestiae placeat unde eos molestias. Quisquam aperiam, pariatur. Tempora, placeat ratione porro voluptate odit minima.</p>
        <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Nihil odit magnam minima, soluta doloribus reiciendis molestiae placeat unde eos molestias. Quisquam aperiam, pariatur. Tempora, placeat ratione porro voluptate odit minima.</p>

    </div>
    <!--/.Panel 2-->

    <!--Panel 3-->
    <div class="tab-pane fade" id="panel13" role="tabpanel">
        <br>

        <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Nihil odit magnam minima, soluta doloribus reiciendis molestiae placeat unde eos molestias. Quisquam aperiam, pariatur. Tempora, placeat ratione porro voluptate odit minima.</p>

    </div>
    <!--/.Panel 3-->

    <!--Panel 4-->
    <div class="tab-pane fade" id="panel14" role="tabpanel">
        <br>

        <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Nihil odit magnam minima, soluta doloribus reiciendis molestiae placeat unde eos molestias. Quisquam aperiam, pariatur. Tempora, placeat ratione porro voluptate odit minima.</p>
        <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Nihil odit magnam minima, soluta doloribus reiciendis molestiae placeat unde eos molestias. Quisquam aperiam, pariatur. Tempora, placeat ratione porro voluptate odit minima.</p>

    </div>
    <!--/.Panel 4-->

</div>

    </div>

    <IQ:ucExtruder runat="server" ID="ucExtruder" />
</asp:Content>
