<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPatientDetails.ascx.cs" Inherits="IQCare.Web.CCC.UC.ucTriage" %>

        <div id="callout-labels-inline-block" class="col-md-12 well well-sm bs-callout bs-callout-primary" style="padding-bottom:1%">
             <div class="col-md-12">
                  <small class="pull-left text-primary">Patient Detail(s)</small>
            </div>
            <div class="col-md-12">
                 <div class="col-md-2">
                      <div class="col-md-12"><i class="fa fa-user fa-5x pull-left" aria-hidden="true"></i></div>
                      <div class="col-md-12 label label-success">
                           <small class="pull-left"><i class="fa fa-exclamation-circle fa-2x" aria-hidden="true">Active Visit - Date # Time-#</i></small>
                      </div>

                    <div class="col-md-12 form-group">
                       <div class="col-md-6"> <small class="pull-left">Cell Phone</small></div><div class="col-md-6"></div> 
                   </div>

                </div>
                <div class="col-md-10">
                    <div class="col-md-12">
                        <div class="col-md-2" style="border-bottom:1px solid blue;margin-bottom:1%;margin-right:1%"><label id="lblLastName" class="text-info pull-left">lname</label></div>
                        <div class="col-md-2" style="border-bottom:1px solid blue;margin-bottom:1%;margin-right:1%"><label id="lblOtherNames" class="text-info pull-left">otherNames</label></div>
                        <div class="col-md-2" style="border-bottom:1px solid blue;margin-bottom:1%;margin-right:1%"><label id="lblAge" class="text-info pull-left">Age years</label></div>
                        <div class="col-md-1" style="border-bottom:1px solid blue;margin-bottom:1%;margin-right:1%"><label id="lblGender" class="text-info pull-left">m/f</label></div>
                        <div class="col-md-2" style="border-bottom:1px solid blue;margin-bottom:1%;margin-right:1%"><label id="lblpopulation" class="text-info pull-left">pop</label></div>
                        <div class="col-md-2" style="border-bottom:1px solid blue;margin-bottom:1%;margin-right:1%"><label id="lblStatus" class="text-left text-danger">Active/Inactive</label></div>
                    </div>
                    <div class="col-md-12">
                        <div class="col-md-2"><small class="text-success "><i>last name</i></small> </div>
                        <div class="col-md-2"><small class="text-success"><i>Other names</i></small></div>
                        <div class="col-md-2"><small class="text-success"><i>Age</i></small></div>
                        <div class="col-md-2"><small class="text-success"><i>Gender</i></small></div>
                        <div class="col-md-2"><small class="text-success"><i>population type</i></small></div>
                        <div class="col-md-2"><small class="text-success"><i>Patient Status</i></small></div>
                    </div>
                </div><%-- .col-md-10--%>
            </div><%--.col-md-12--%>
        </div>


