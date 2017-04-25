<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPatientDetails.ascx.cs" Inherits="IQCare.Web.CCC.UC.ucPatientDetails" %>

        <div id="callout-labels-inline-block" class="col-md-12 col-xs-12 col-sm-10 well well-sm bs-callout bs-callout-primary" style="padding-bottom: 1%;margin-top:1%">
            <div class="col-md-2 col-xs-12 col-sm-12">
                 <div class="col-md-12 col-xs-12 col-sm-12">
                    <div class="col-md-12"><i class="fa fa-user fa-4x pull-left text-info" aria-hidden="true"></i></div>
                    <div class="col-md-12">
                        <small class="pull-left fa-lg"><asp:Label runat="server" CssClass=" label label-info" ID="lblEnrollmentDate">Visit</asp:Label></small>
                    </div>
                </div>
            </div>
            <div class="col-md-10">
                 <div class="col-md-12 form-group">
                      <div class="col-sm-4">
                               <div class="col-md-4 col-xs-12 col-sm-12 pull-left"> <small class="text-success text-muted"><i>Last Name:</i></small></div> 
                               <div class="col-sm-8 col-xs-12 col-sm-12">
                                   <asp:Label runat="server" ClientIDMode="Static" id="lblLastName" name="lblnm" class="text-info pull-left">lname</asp:Label>
                               </div>
                            </div>
                             
                     <div class="col-sm-4">
                               <div class="col-md-5 col-xs-12 col-sm-12 pull-left"> <small class="text-success text-muted"><i>Other Names:</i></small></div>
                                <div class="col-sm-7 col-xs-12 col-sm-12">
                                     <asp:Label runat="server" ClientIDMode="Static" id="lblOtherNames" class="text-info pull-left"> otherNames</asp:Label>
                                </div>
                            </div>
                            
                     <div class="col-sm-4 col-xs-12 col-sm-12">
                               <div class="col-md-4 col-xs-12 col-sm-12"> <small class="text-success  pull-left"><i>Patient Age:</i></small></div> 
                               <div class="col-sm-8 col-xs-12 col-sm-12">
                                   <asp:Label runat="server" ClientIDMode="Static" id="lblAge" class="text-info pull-left"> Age years</asp:Label>
                               </div>
                            </div>    
                 </div>
                 <div class="col-md-12 col-xs-12 col-sm-12 form-group">
                     <div class="col-sm-4 col-xs-12 col-sm-12">
                               <div class="col-md-4 col-xs-12 col-sm-12"> <small class="text-success  pull-left"><i> Gender:</i></small></div>
                               <div class="col-sm-8">
                                    <asp:Label runat="server" ClientIDMode="Static" id="lblGender" class="text-info pull-left"> m/f</asp:Label>
                                </div>
                            </div>
                    
                     <div class="col-md-4">
                               <div class="col-md-6 col-xs-12 col-sm-12"> <small class="text-success pull-left"><i>CCC Registration #.</i></small></div>
                               <div class="col-md-6 col-xs-12 col-sm-12 pull-left">
                                  <asp:Label runat="server" ClientIDMode="Static" id="lblCCCReg" class="text-info pull-left label label-info"> CCC Reg.</asp:Label>
                                </div>
                            </div>
                            
                     <div class="col-md-4">
                               <div class="col-md-5 col-xs-12 col-sm-12 "><small class="text-success pull-left"><i>Patient Status</i></small></div> 
                               <div class="col-sm-6 col-xs-12 col-sm-12 pull-left">
                                    <asp:Label runat="server" ClientIDMode="Static" id="lblStatus" class="text-left text-danger"> Active/Inactive</asp:Label>
                                </div>
                            </div>
                 </div> 
                  
            </div>
        </div>


