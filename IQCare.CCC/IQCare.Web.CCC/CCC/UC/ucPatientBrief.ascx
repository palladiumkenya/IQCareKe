<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPatientBrief.ascx.cs" Inherits="IQCare.Web.CCC.UC.ucPatientBrief" %>
			
<div class="ui-user col-md-12 col-xs-12 col-sm-12" style="padding-top: 0%;margin-top: 0%">
					<!-- Image -->
					<div class="col-md-1 col-xs-1 col-sm-1" style="padding-right: 0%">
					    <div class="col-md-12 md-form" id="divIcon"><i class="fa fa-user fa-4x pull-left " aria-hidden="true"></i></div> 
                        <div class="col-md-12">
                             <h6 class="pull-left"><asp:Label runat="server" ID="lblGender" CssClass="text-info pull-left"></asp:Label></h6>
                        </div>
                        <%--<div class="col-md-12 col-xs-12 col-sm-12 form-group" style="padding-top: 0%">
                              <div class="col-md-11 col-xs-11 col-sm-11"><h6 class="pull-left">DOB : </h6>  <h6 class="pull-left"><asp:Label runat="server" ID="lblDOB" CssClass="text-info pull-left"></asp:Label></h6></div>
                          </div>--%>
					  </div>
                    <div class="col-md-7 col-xs-7 col-sm-7" style="padding-left: 1%">
                          <div class="col-md-12 pull-left"><h4 class="pull-left"><asp:Label runat="server" ID="lblPatientNames" CssClass="text-info"> #PatientNames</asp:Label></h4><h4 class="pull-left"><asp:Label runat="server"  ID="lblCCCRegNo"></asp:Label></h4></div> 
                           
                          <div class="col-md-12 col-xs-12 col-sm-12" style="padding-top: 0%">
                              <div class="col-md-11"><h6 class="pull-left">Age : </h6> <h6 class="pull-left"><asp:Label runat="server" ID="lblAge" CssClass="text-info pull-left"></asp:Label></h6></div> 
                          </div>

                          <div class="col-md-12 col-xs-12 col-sm-12" style="padding-top: 0%">
                              <div class="col-md-11 col-xs-11 col-sm-11"><h6 class="pull-left">Marital Status : </h6>  <h6 class="pull-left"><asp:Label runat="server" ID="lblmstatus" CssClass="text-info pull-left"></asp:Label></h6></div>
                          </div>


                      </div>
                    <div class="col-md-4 col-xs-4 col-sm-4 pull-right">
                        
                         <div class="col-md-12 col-xs-12 col-sm-12" style="padding-top: 0%">
                              <div class="col-md-11 col-xs-11 col-sm-11"><h6 class="pull-left">Enrollment Date : </h6>  <h6 class=""><asp:Label runat="server" ID="lblEnrollmentDate" CssClass="text-info"></asp:Label></h6></div>
                          </div>
                          <div class="col-md-12 col-xs-12 col-sm-12" style="padding-top: 0%">
                              <div class="col-md-11 col-xs-11 col-sm-11"><h6 class="pull-left">Patient Type : </h6>  <h6 class=""><asp:Label runat="server" ID="lblPatientType" CssClass="text-info"></asp:Label></h6></div>
                          </div>
                          <div class="col-md-12 col-xs-12 col-sm-12" style="padding-top: 0%">
                              <div class="col-md-11 col-xs-11 col-sm-11"><h6 class="pull-left">Patient Status: </h6>  <h6 class=""><asp:Label runat="server" ID="lblPatientStatus" CssClass="text-info"></asp:Label></h6></div>
                          </div>

                    </div>
					<!-- Name -->
					
				</div>
<div class="col-md-12"><hr/></div>