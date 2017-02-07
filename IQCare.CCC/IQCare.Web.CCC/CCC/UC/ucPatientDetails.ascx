<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPatientDetails.ascx.cs" Inherits="IQCare.Web.CCC.UC.ucPatientDetails" %>

        <div id="callout-labels-inline-block" class="col-md-12 well well-sm bs-callout bs-callout-primary" style="padding-bottom:1%">
             <div class="col-md-12">
                  <small class="pull-left text-primary">Patient Detail(s)</small>
            </div>
            <div class="col-md-12">
                 <div class="col-md-2">
                      <div class="col-md-12"><i class="fa fa-user fa-5x pull-left" aria-hidden="true"></i></div>
                      <div class="col-md-12 label label-success">
                           <small class="pull-left"><i class="fa fa-exclamation-circle fa-2x" aria-hidden="true">Active Visit</i></small>
                      </div>

                    <div class="col-md-12 form-group">
                       <div class="col-md-12"> <small class="pull-left">Cell Phone</small></div><div class="col-md-6"></div> 
                   </div>

                </div>
                <div class="col-md-10">
                    <div class="col-md-12">
                        <div class="col-md-1" style="border-bottom:1px solid blue;margin-bottom:1%;margin-right:1%"><asp:Label runat="server" ClientIDMode="Static" id="lblLastName" name="lblnm" class="text-info pull-left">lname</asp:Label></div>
                        <div class="col-md-2" style="border-bottom:1px solid blue;margin-bottom:1%;margin-right:1%"><asp:Label runat="server" ClientIDMode="Static" id="lblOtherNames" class="text-info pull-left">otherNames</asp:Label></div>
                        <div class="col-md-2" style="border-bottom:1px solid blue;margin-bottom:1%;margin-right:1%"><asp:Label runat="server" ClientIDMode="Static" id="lblAge" class="text-info pull-left">Age years</asp:Label></div>
                        <div class="col-md-2" style="border-bottom:1px solid blue;margin-bottom:1%;margin-right:1%"><asp:Label runat="server" ClientIDMode="Static" id="lblGender" class="text-info pull-left">m/f</asp:Label></div>
                        <div class="col-md-2" style="border-bottom:1px solid blue;margin-bottom:1%;margin-right:1%"><asp:Label runat="server" ClientIDMode="Static" id="lblCCCReg" class="text-info pull-left">CCC Reg.</asp:Label></div>
                        <div class="col-md-2" style="border-bottom:1px solid blue;margin-bottom:1%;margin-right:1%"><asp:Label runat="server" ClientIDMode="Static" id="lblStatus" class="text-left text-danger">Active/Inactive</asp:Label></div>
                    </div>
                    <div class="col-md-12">
                        <div class="col-md-2"><small class="text-success "><i>last name</i></small> </div>
                        <div class="col-md-2"><small class="text-success"><i>Other names</i></small></div>
                        <div class="col-md-2"><small class="text-success"><i>Age</i></small></div>
                        <div class="col-md-2"><small class="text-success"><i>Gender</i></small></div>
                        <div class="col-md-2"><small class="text-success"><i>CCC Registration #.</i></small></div>
                        <div class="col-md-2"><small class="text-success"><i>Patient Status</i></small></div>
                    </div>
                </div><%-- .col-md-10--%>
            </div><%--.col-md-12--%>
        </div>


