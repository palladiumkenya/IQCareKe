<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPatientTriage.ascx.cs" Inherits="IQCare.Web.CCC.UC.ucPatientTriage" %>

<div class="col-md-12">
    
    <div class="panel panel-info">

    <div class="panel-body">
        
        <div class="col-md-12"><small class="muted pull-left"><strong><i class="fa fa-heartbeat fa-2x" aria-hidden="true"></i> PATIENT TRIAGE </strong></small></div>                                                  
        <div class="col-md-12"><hr /></div>
        <div class="col-md-12">
             <div class="col-md-6">
                  <div class="col-md-12"> <label class="control-label text-primary pull-left text-muted">Anthropometric Measurement</label></div>
                  <div class="col-md-12"><br/></div>

                  <div class="col-md-12 form-group">
                      <div class="col-md-6"><label class="control-label pull-left"><small class="text-danger">*</small> Height </label></div> 
                      <div class="col-md-6">
                          <asp:TextBox runat="server" ID="Heights" ClientIDMode="Static" CssClass="form-control input-sm" placeholder="cms.."></asp:TextBox>
                      </div>
                  </div>
              
                 <div class="col-md-12 form-group">
                      <div class="col-md-6"><label class="control-label pull-left"><small class="text-danger">*</small> Weight </label></div> 
                      <div class="col-md-6">
                          <asp:TextBox runat="server" ID="weights" ClientIDMode="Static" CssClass="form-control input-sm" placeholder="kgs.."></asp:TextBox>
                      </div>
                  </div>
             
                  <div class="col-md-12 form-group">
                      <div class="col-md-6"><label class="control-label pull-left"><small class="text-danger">*</small> Body Mass Index(BMI) </label></div> 
                      <div class="col-md-6">
                          <asp:Label runat="server" CssClass="control-label text-warning pull-left">BMI</asp:Label>
                      </div>
                  </div>
             
                  <div class="col-md-12 form-group">
                      <div class="col-md-6"><label class="control-label pull-left"><small class="text-danger"></small>Head Circumference (cm) </label></div> 
                      <div class="col-md-6">
                          <asp:TextBox runat="server" ID="circumference" ClientIDMode="Static" CssClass="form-control input-sm" placeholder="cms.."></asp:TextBox>
                      </div>
                  </div>
             
                  <div class="col-md-12 form-group">
                      <div class="col-md-6"><label class="control-label pull-left"><small class="text-danger"></small> MUAC (cms) </label></div> 
                      <div class="col-md-6">
                          <asp:TextBox runat="server" ID="muacs" ClientIDMode="Static" CssClass="form-control input-sm" placeholder="cms.."></asp:TextBox>
                      </div>
                  </div>


             </div>

             <div class="col-md-6">
                <div class="col-md-12"> <label class="control-label text-primary pull-left text-muted">Vital Signs</label></div>
                <div class="col-md-12"><br/></div>
            
                <div class="col-md-12 form-group">
                      <div class="col-md-6 clearfix"><label class="control-label pull-left"><small class="text-danger"></small> Blood Pressure </label></div> 
                      <div class="col-md-2">
                          <asp:TextBox runat="server" ID="distolic" ClientIDMode="Static" CssClass="form-control input-sm" placeholder="kgs.."></asp:TextBox>
                      </div>
                    <div class="col-md-2 clearfix"><label class="control-label">/</label></div>
                    <div class="col-md-2 clearfix">
                          <asp:TextBox runat="server" ID="systolic" ClientIDMode="Static" CssClass="form-control input-sm" placeholder="kgs.."></asp:TextBox>
                      </div>
                </div>
            
                <div class="col-md-12 form-group">
                     <div class="col-md-6"><label class="control-label pull-left"><small class="text-danger"></small> Temperature </label></div> 
                     <div class="col-md-6">
                          <asp:TextBox runat="server" ID="TextBox2" ClientIDMode="Static" CssClass="form-control input-sm" placeholder=".."></asp:TextBox>
                     </div>
                </div>
            
               <div class="col-md-12 form-group">
                      <div class="col-md-6"><label class="control-label pull-left"><small class="text-danger"></small> Respiratory Rate </label></div> 
                      <div class="col-md-6">
                          <asp:TextBox runat="server" ID="TextBox3" ClientIDMode="Static" CssClass="form-control input-sm" placeholder=".."></asp:TextBox>
                      </div>
                  </div>
            
              <div class="col-md-12 form-group">
                      <div class="col-md-6"><label class="control-label pull-left"><small class="text-danger"></small> Blood Oxygen Saturation </label></div> 
                      <div class="col-md-6">
                          <asp:TextBox runat="server" ID="bosaturation" ClientIDMode="Static" CssClass="form-control input-sm" placeholder="%.."></asp:TextBox>
                      </div>
                  </div>




            </div>
    
        </div><%-- .col-md-11--%>
        
        <div class="col-md-12"><hr/></div>
        <div class="col-md-12">
             <div class="col-md-8"></div>
            <div class="col-md-4">
                <div class="col-md-4"><asp:LinkButton runat="server" ID="btnSaveTriage" CssClass="btn btn-info fa fa-plus-circle btn-lg"> Save Triage </asp:LinkButton></div>
                <div class="col-md-4"><asp:LinkButton runat="server" ID="btnReset" CssClass="btn btn-warning  fa fa-refresh btn-lg "> Reset Entry  </asp:LinkButton></div>
                <div class="col-md-4"><asp:LinkButton runat="server" ID="btnCancel" CssClass="btn btn-danger fa fa-times btn-lg"> Close Triage </asp:LinkButton></div>
            </div>
        </div>
    </div>

</div>

</div>