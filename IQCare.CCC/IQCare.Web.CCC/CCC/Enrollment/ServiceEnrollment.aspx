<%@ Page Title="" Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="ServiceEnrollment.aspx.cs" Inherits="IQCare.Web.CCC.Enrollment.ServiceEnrollment" %>
<%@ Register TagPrefix="uc" TagName="PatientDetails" Src="~/CCC/UC/ucPatientDetails.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <div class="col-md-12">
        <uc:PatientDetails ID="PatientSummary" runat="server" />
    </div>
    
    <div class="col-md-12 bs-callout bs-callout-info">
        <div class="col-md-6">
             <div class="col-md-12 form-group">
                  <div class="col-md-6"><label class="control-label">Service Area</label></div>
                  <div class="col-md-6">
                       <asp:DropDownList runat="server" ID="ServiceAreaId" CssClass="form-control input-sm" ClientIDMode="Static"/>
                  </div>
             </div>
            
            <div class="col-md-12 form-group">
                <div class="col-md-6"><label class="control-label pull-left">Enrollment Date </label></div>
                <div class="col-md-6">
                    
                </div>
            </div>
            
            <div class="col-md-12 form-group">
                <div class="col-md-6"><label class="control-label">Enrollment Status </label></div>
                <div class="col-md-6">
                     <asp:DropDownList runat="server" ID="EnrollmentStatusId" CssClass="form-control input-sm" ClientIDMode="Static"/>
                </div>
            </div>
            
            <div class="col-md-12 form-group">
                 <div class="col-md-4">
                      <div class="col-md-12"><label class="pull-left control-label">Registration type</label></div>
                      <div class="col-md-12">
                           <asp:DropDownList runat="server" CssClass="form-control input-sm" ID="IdentifierTypeId" ClientIDMode="Static"/>
                      </div>
                 </div>
                 <div class="col-md-4">
                     <div class="col-md-12"><label class="pull-left control-label">Registration Number</label></div>
                     <div class="col-md-12">
                         <asp:TextBox runat="server" CssClass="form-control input-sm" ClientIDMode="Static" ID="IdentifierValue" Placeholder="Registration No#..."></asp:TextBox>
                     </div>
                 </div>
                 <div class="col-md-4">
                      <div class="col-md-12">
                          <asp:LinkButton runat="server" CssClass="btn btn-info fa fa-plus-circle"> Add Identifier</asp:LinkButton>
                      </div>
                 </div>
            </div>
            <div class="col-md-12"><hr/></div>
            <div class="col-md-12">
                <div class="col-md-4"></div>
                <div class="col-md-8">
                     <div class="col-md-4">
                          <asp:LinkButton runat="server" ID="btnEnroll" CssClass="btn btn-info fa fa-plus-circle"> Enroll</asp:LinkButton>
                     </div>
                    <div class="col-md-4">
                          <asp:LinkButton runat="server" ID="btnRese" CssClass="btn btn-info fa fa-refresh"> Reset</asp:LinkButton>
                     </div>
                    <div class="col-md-4">
                          <asp:LinkButton runat="server" ID="LinkButton2" CssClass="btn btn-info fa fa-times"> Close</asp:LinkButton>
                     </div>
                </div>
            </div>
        </div>  
        <div class="col-md-6">
             <div class="col-md-12">
                 <div class="list-group">
                      <a class="list-group-item" href="#"><i class="fa fa-home fa-fw" aria-hidden="true"></i>&nbsp; Demo Service</a>
                      <a class="list-group-item" href="#"><i class="fa fa-book fa-fw" aria-hidden="true"></i>&nbsp; Demo Service</a>
                      <a class="list-group-item" href="#"><i class="fa fa-pencil fa-fw" aria-hidden="true"></i>&nbsp; demo service</a>
                      <a class="list-group-item" href="#"><i class="fa fa-cog fa-fw" aria-hidden="true"></i>&nbsp; demo service</a>
                    </div>
             </div>
        </div>     
    </div>

</asp:Content>
