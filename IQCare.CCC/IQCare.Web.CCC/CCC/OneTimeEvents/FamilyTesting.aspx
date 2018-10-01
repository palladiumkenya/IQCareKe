<%@ Page Title="" Language="C#" MasterPageFile="~/CCC/Greencard.Master" AutoEventWireup="true" CodeBehind="FamilyTesting.aspx.cs" Inherits="IQCare.Web.CCC.OneTimeEvents.FamilyTesting" %>

<%@ Register TagPrefix="uc" TagName="PatientDetails" Src="~/CCC/UC/ucPatientBrief.ascx" %>
<%@ Register TagPrefix="ucFamily" TagName="FamilyFinder" Src="~/CCC/UC/ucFamilyFinder.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="server">
    <div class="container-fluid">


        <uc:PatientDetails ID="PatientSummary" runat="server" />
        <div class="col-md-12 bs-callout bs-callout-info" id="RegisteredtoClinic">
            <div class="form-group  form-inline">
                <div class="col-sm-3">
                    <label for="input-type" class="custom-control-label">Registered at this Clinic?</label>
                </div>
                  <div class="col-sm-6">
                        <label class="pull-left" style="padding-right: 10px">
                                <input type="radio" class="custom-control-input" name="optRegistered" value="No" id="radiono" >No
                         </label>
                    
                          <label class="pull-left" style="padding-right: 10px">
                                <input type="radio" class="custom-control-input" name="optRegistered"  value="Yes" id="radioyes">Yes
                           </label>
                        
                    </div>
                </div>
           
        </div>
         
        <ucFamily:FamilyFinder ID="FindFamily" runat="server" />
        
         <div class="col-md-12 col-xs-12 col-sm-12 bs-callout bs-callout-info" id="ReturnGrid">
            <div class="col-md-6 pull-right">
                 <button id="btnReturnGrid" class="btn btn-warning btn-lg btn-sm pull-right fa fa-arrow-circle-o-left"  onclick="return false"> Back to Search</button>
             </div>
         </div>
          
        <%--<asp:LinkButton runat="server" ID="btn_open_modal" ClientIDMode="Static" OnClientClick="return false" CssClass=" btn btn-info btn-lg">View Patient Family Members</asp:LinkButton>--%>

        <div class="col-md-12 bs-callout bs-callout-info" id="FamilyTestingDetails">
            <div class="col-md-12" id="FamilyTestingForm" data-parsley-validate="true" data-show-errors="true">
                <div class="col-md-12">
                    <div class="col-md-12">
                        <h3>Person Details</h3><hr />
                    </div>
                    <div class="col-md-6">
                        <div class="col-md-12 form-group">
                            <div class="col-md-6">
                                <label class="required control-label pull-left">First Name</label>
                            </div>
                            <div class="col-md-6">
                                <input id="FirstName" class="form-control input-sm" type="text" runat="server" placeholder="First Name" data-parsley-required="true" />
                            </div>
                        </div>
                        <div class="col-md-12 form-group">
                            <div class="col-md-6">
                                <label class="control-label pull-left">Middle Name</label>
                            </div>
                            <div class="col-md-6">
                                <input id="MiddleName" class="form-control input-sm" type="text" runat="server" placeholder="Middle Name" />
                            </div>
                        </div>
                        <div class="col-md-12 form-group">
                            <div class="col-md-6">
                                <label class="required control-label pull-left">Last Name</label>
                            </div>
                            <div class="col-md-6">
                                <input id="LastName" class="form-control input-sm" type="text" runat="server" placeholder="Last Name" data-parsley-required="true" />
                            </div>
                        </div>
                    </div>

                    <div class="col-md-6">
                        <div class="col-md-12 form-group">
                            <div class="col-md-6">
                                <label class="required control-label pull-left">Relationship</label>
                            </div>
                            <div class="col-md-6">
                                <asp:DropDownList runat="server" ID="Relationship" ClientIDMode="Static" CssClass="form-control input-sm" required="true" onChange="relationShipChanged();" />
                            </div>
                        </div>
                        <div class="col-md-12 form-group">
                            <div class="col-md-6">
                                <label class="required control-label pull-left">Sex</label>
                            </div>
                            <div class="col-md-6">
                                <select runat="server" id="Sex" class="form-control input-sm" ClientIDMode="Static" required="true" onchange="relationShipChanged();"></select>
                            </div>
                        </div>
                        <div class="col-md-12 form-group">
                            <div class="col-md-6">
                                <label class="control-label pull-left">Date of Birth</label>
                            </div>
                            <div class="col-md-6">
                                <div class='input-group date' id='PersonDOBdatepicker'>
                                    <span class="input-group-addon">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </span>
                                    <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="Dob" data-parsley-required="false" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"></asp:TextBox>        
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12 form-group">
                            <div class="col-md-6">
                                <label class="control-label pull-left">Age(Years)</label>
                            </div>
                            <div class="col-md-6">
                                <asp:TextBox ID="personAge" runat="server" ClientIDMode="Static" CssClass="form-control input-sm"  placeholder="0"></asp:TextBox>
                                <asp:HiddenField ID="dobPrecision" runat="server" ClientIDMode="Static" />
                            </div>
                        </div>
                         <div class="col-md-12 form-group">
                            <div class="col-md-6">
                                <label class="control-label pull-left">Age(Months)</label>
                            </div>
                            <div class="col-md-6">
                                <asp:TextBox ID="personMonth" runat="server" ClientIDMode="Static" CssClass="form-control input-sm" placeholder="0" required="true" min="0"></asp:TextBox>
                               
                            </div>
                        </div>
                    </div>
                </div>

<%--
                <div class="col-md-12" id="isRegisteredInClinic">
                    <div class="col-md-6">
                        <div class="col-md-12 form-group">
                            <div class="col-md-6">
                                <label class="required control-label pull-left">Registered at this clinic:</label>
                            </div>
                            <div class="col-md-6">
                                <asp:DropDownList ID="RegisteredInClinic" runat="server" ClientIDMode="Static" CssClass="form-control input-sm" data-parsley-required="true"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    
                    <div class="col-md-6" id="searchButton">
                        <div class="col-md-12 form-group">
                            <div class="col-md-6">
                                <asp:LinkButton ID="btnSearch" runat="server" ClientIDMode="Static" CssClass="btn btn-info btn-lg fa fa-search-minus" OnClientClick="return false;"> Find Patient</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    
                    <div class="col-md-12" id="familyMembersTable">
                        <table class="table" id="familyMembersSearched">
                            <thead class="thead-default">
                            <tr>
                                <th>CCC Number</th>
                                <th>First Name</th>
                                <th>Middle Name</th>
                                <th>Last Name</th>
                                <th>Date Of Birth</th>
                                <th>Sex</th>
                                <th>Enrollment Date</th>
                                <th>Patient Status</th>
                                <th>Action</th>
                            </tr>

                            </thead>

                        </table>
                    </div>
                </div>
                --%>
                
                <div id="hivTestingInfo">
                    <div id="hivoutcome">                 
                        <div class="col-md-12">
                        <div class="col-md-12">
                            <h3>HIV Testing information</h3><hr />

                        </div>

                        <div class="col-md-4">
                        <div class="col-md-12 form-group">
                            <div class="col-md-12">
                                <label class="required control-label pull-left">Baseline HIV Status</label>
                            </div>
                            <div class="col-md-12">
                                <select runat="server" id="BaselineHIVStatus" class="form-control input-sm" clientidmode="Static" onchange="BaselineEnabled();"></select>
                            </div>

                        </div>

                        <div class="col-md-12 form-group">
                            <div class="col-md-12">
                                <label class="control-label pull-left">Baseline HIV Status Date</label>
                            </div>
                            <div class="col-md-12">
                                <div class='input-group date' id='BaselineHIVStatusdatepicker'>
                                    <span class="input-group-addon">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </span>
                                    <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="BaselineHIVStatusDate" data-parsley-required="true" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"></asp:TextBox>        
                                </div>
                            </div>          
                        </div>

                    </div>

                        <div class="col-md-4">
                        <div class="col-md-12 form-group">
                            <div class="col-md-12">
                                <label class="control-label pull-left">HIV Testing Results</label>
                            </div>
                            <div class="col-md-12">
                                <asp:DropDownList runat="server" ID="hivtestingresult" ClientIDMode="Static" CssClass="form-control input-sm" required="true" disabled="true" onChange="CccEnabled();" />
                            </div>
                        </div>
                        <div class="col-md-12 form-group">
                            <div class="col-md-12">
                                <label class="control-label pull-left">HIV Testing Date</label>
                            </div>
                            <div class="col-md-12">
                                <div class='input-group date' id='TestingDate'>
                                    <span class="input-group-addon">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </span>
                                    <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="HIVTestingDate"  disabled="true" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"></asp:TextBox>        
                                </div>
                            </div>
                        </div>
                    </div>

                        <div class="col-md-4">
                        <div class="col-md-12 form-group">
                            <div class="col-md-12">
                                <label class="control-label pull-left">Referred to CCC</label>
                            </div>
                            <div class="col-md-12">
                                <asp:DropDownList ID="CccReferal" runat="server" AutoPostBack="False" CssClass="form-control input-sm" onChange="CccEnabled();" ClientIDMode="Static" data-parsley-required="true">
                                    <asp:ListItem Text="Select" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="False"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        
                        <div class="col-md-12 form-group">
                            <div class="col-md-12">
                                <label class="control-label pull-left">Date Referred to CCC</label>
                            </div>
                            <div class="col-md-12">
                                <div class='input-group date' id='CCCReferaldatepicker'>
                                    <span class="input-group-addon">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </span>
                                    <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="CCCReferalDate" data-parsley-required="true" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"></asp:TextBox>        
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12 form-group">
                            <div class="col-md-12">
                                <label class="control-label pull-left">CCC Number</label>
                            </div>
                            <div class="col-md-12" id="cccnum">
                                <input id="cccNumber" class="form-control input-sm" type="text" data-parsley-trigger="keyup" data-parsley-pattern-message="<%=PatientIdentifier.FailedValidationMessage %>" data-parsley-pattern="<%=PatientIdentifier.ValidatorRegex %>" data-parsley-minlength="<%=PatientIdentifier.MinLength %>" data-parsley-maxlength="<%=PatientIdentifier.MaxLength %>" />
                            </div>
                        </div>
                    </div>

                    </div>
                    </div>



                    <div class="col-md-12">
                        <hr />
                    </div>

                    <div class="col-md-12">
                        <asp:LinkButton runat="server" ID="btnAdd" ClientIDMode="Static" OnClientClick="return false" CssClass="btn btn-info btn-lg fa fa-plus-circle"> Add Member</asp:LinkButton>
                    </div>
                

                    <div class="col-md-12">
                        <hr />
                    </div>

                    <div class="col-md-12">
                        <div class="col-md-12 form-group">
                            <div class="col-md-12 bg-primary"><span class="pull-left"></span>Family Members </div>
                            <table class="table table-hover" id="tblFamilyTesting" clientidmode="Static" runat="server">
                                <thead>
                                    <tr>
                                        <th class="text-primary">#</th>
                                        <th><span class="text-primary" aria-hidden="true">Name</span> </th>
                                        <th><span class="text-primary" aria-hidden="true">Date Of Birth</span></th>
                                        <th><span class="text-primary" aria-hidden="true">Relationship</span> </th>
                                        <th><span class="text-primary" aria-hidden="true">Baseline HIV Status</span> </th>
                                        <th><span class="text-primary" aria-hidden="true">Baseline HIV Status Date</span> </th>
                                        <th><span class="text-primary" aria-hidden="true">HIV Testing Results</span> </th>
                                        <th><span class="text-primary" aria-hidden="true">HIV Testing Results Date</span> </th>
                                        <th><span class="text-primary" aria-hidden="true">CCC Referal</span></th>
                                        <th><span class="text-primary pull-right">Action</span></th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>

                        </div>
                        <div class="col-md-10"></div>
                    </div>
                </div>
            </div>

            <div class="col-md-12" id="buttonSaving">
                <hr />
                <div class="col-md-4"></div>

                <div class="col-md-8">

                    <div class="col-md-4">
                        <asp:LinkButton runat="server" ID="btnSave" CssClass=" btn btn-info btn-lg fa fa-arrow-circle-o-right" ClientIDMode="Static" OnClientClick="return false;"> Save Family Testing</asp:LinkButton>
                    </div>
                    <div class="col-md-4">
                        <asp:LinkButton runat="server" ID="btnReset" CssClass=" btn btn-warning fa fa-refresh btn-lg" ClientIDMode="Static" OnClientClick="return false;"> Reset Family Form</asp:LinkButton>
                    </div>
                    <div class="col-md-4">
                        <asp:LinkButton runat="server" ID="btnClose" CssClass=" btn btn-danger fa fa-times btn-lg" ClientIDMode="Static" > Close Family Form</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-md-12 bs-callout bs-callout-info" id="ViewFamilyTestingDetails">
            <table class="table table-hover" id="tableFamilymembers" clientidmode="Static" runat="server">
                <thead>
                    <tr class="active">
                        <th class="text-primary">#</th>
                        <th><span class="text-primary" aria-hidden="true">Name</span> </th>
                        <th><span class="text-primary" aria-hidden="true">Index</span> </th>
                        <th><span class="text-primary" aria-hidden="true">Date Of Birth</span> </th>
                       <%-- <th><span class="text-primary" aria-hidden="true">Relationship</span> </th>--%>
                        <th><span class="text-primary" aria-hidden="true">Baseline HIV Status</span> </th>
                        <th><span class="text-primary" aria-hidden="true">Baseline HIV Status Date</span> </th>
                        <th><span class="text-primary" aria-hidden="true">HIV Testing Results</span> </th>
                        <th><span class="text-primary" aria-hidden="true">HIV Testing Results Date</span> </th>
                        <th><span class="text-primary" aria-hidden="true">CCC Referal</span></th>
                        <th><span class="text-primary" aria-hidden="true">CCC Referal Date</span></th>
                        <th><span class="text-primary pull-right">Action</span></th>
                        <th><span class="text-primary pull-right">Enroll in Facility</span></th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
            <div class="col-md-12">
                <asp:LinkButton runat="server" ID="FamilyAdd" ClientIDMode="Static" OnClientClick="return false" CssClass=" btn btn-info btn-lg fa fa-plus-circle"> Add Member</asp:LinkButton>
            </div>
        </div>


        <%--Test Follow-up Modal--%>
        <div id="testFollowupModal" class="modal fade" role="dialog" data-parsley-validate="true" data-show-errors="true">
            <div class="modal-dialog" style="width: 90%">
                <div class="modal-content">
                    <div class="modal-header bg-info">
                        <h4 class="modal-title">Family Testing</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-12">

                                <div class="col-md-12">
                                    <h3 class="pull-left">Person Details</h3>
                                </div>
                                <div class="col-md-12">
                                    <hr/>
                                </div>
                                <div class="col-md-6">
                                    <div class="col-md-12 form-group">
                                        <div class="col-md-6">
                                            <label class="control-label pull-left">First Name</label>
                                        </div>
                                        <div class="col-md-6">
                                            <input id="fName" name="fname" class="form-control input-sm" type="text" runat="server" ClientIDMode="Static" placeholder="First Name" required="true" />
                                        </div>
                                    </div>
                                    <div class="col-md-12 form-group">
                                        <div class="col-md-6">
                                            <label class="control-label pull-left">Middle Name</label>
                                        </div>
                                        <div class="col-md-6">
                                            <input id="mName" name="mName" class="form-control input-sm" type="text" runat="server" placeholder="Middle Name" />
                                        </div>
                                    </div>
                                    <div class="col-md-12 form-group">
                                        <div class="col-md-6">
                                            <label class="control-label pull-left">Last Name</label>
                                        </div>
                                        <div class="col-md-6">
                                            <input id="lName" class="form-control input-sm" type="text" runat="server" placeholder="Last Name" required="true" />
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="col-md-12 form-group">
                                        <div class="col-md-6">
                                            <label class="control-label pull-left">Relationship</label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:DropDownList runat="server" ID="relationshipMod" ClientIDMode="Static" CssClass="form-control input-sm" required="true" />
                                        </div>
                                    </div>
                                    <div class="col-md-12 form-group">
                                        <div class="col-md-6">
                                            <label class="control-label pull-left">Sex</label>
                                        </div>
                                        <div class="col-md-6">
                                            <select runat="server" id="sexMod" class="form-control input-sm" required="true" clientidmode="Static"></select>
                                        </div>
                                    </div>
                                    
                                    <div class="col-md-12 form-group">
                                        <div class="col-md-6">
                                            <label class="control-label pull-left">Date of Birth</label>
                                        </div>
                                        <div class="col-md-6">
                                            <div class='input-group date' id='PersonDOBModdatepicker'>
                                                <span class="input-group-addon">
                                                    <span class="glyphicon glyphicon-calendar"></span>
                                                </span>
                                                <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="dobMod" data-parsley-required="true" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"></asp:TextBox>        
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-12 form-group">
                                        <div class="col-md-6">
                                            <label class="control-label pull-left">Age(Years)</label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="TextBox2" runat="server" ClientIDMode="Static" CssClass="form-control input-sm" placeholder="0" required="true" min="0"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            
                            <div class="col-md-12">
                                <div class="col-md-12">
                                    <h3  class="pull-left">Baseline HIV Results</h3>
                                </div>
                            </div>
                            
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="col-md-12 form-group">
                                        <div class="col-md-12">
                                            <label class="control-label pull-left">Baseline HIV Status</label>
                                        </div>
                                        <div class="col-md-12">
                                            <select runat="server" id="bHivStatusMod" class="form-control input-sm" clientidmode="Static"></select>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="col-md-12 form-group">
                                        <div class="col-md-12">
                                            <label class="control-label pull-left">Baseline HIV Status Date</label>
                                        </div>
                                        <div class="col-md-12">
                                            <div class='input-group date' id='BaselineHIVStatusDMod'>
                                                <span class="input-group-addon">
                                                    <span class="glyphicon glyphicon-calendar"></span>
                                                </span>
                                                <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="bHivStatusDateMod" data-parsley-required="true" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"></asp:TextBox>        
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12">

                                <div class="col-md-12">
                                    <h3  class="pull-left">HIV Testing information</h3>
                                </div>
                                <div class="col-md-12">
                                    <hr/>
                                </div>

                                <div class="col-md-4">
                                    <div class="col-md-12 form-group">
                                        <div class="col-md-12">
                                            <label class="control-label pull-left">HIV Testing Results</label>
                                        </div>
                                        <div class="col-md-12">
                                            <asp:DropDownList runat="server" ID="testingStatusMod" ClientIDMode="Static" CssClass="form-control input-sm" required="true" onChange="CccEnabledMod();" />
                                        </div>
                                    </div>
                                    <div class="col-md-12 form-group">
                                        <div class="col-md-12">
                                            <label class="control-label pull-left">HIV Testing Date</label>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="datepicker fuelux form-group" id="TestingDateMod">
                                                <div class="input-group">
                                                    <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="testingStatusDateMod" data-parsley-required="true" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"></asp:TextBox>
                                                    <div class="input-group-btn">
                                                        <button type="button" class="btn btn-default dropdown-toggle input-sm" data-toggle="dropdown" clientidmode="Static" id="btnHIVTestingDate">
                                                            <span class="glyphicon glyphicon-calendar"></span>
                                                            <span class="sr-only">Toggle Calendar</span>
                                                        </button>
                                                        <div class="dropdown-menu dropdown-menu-right datepicker-calendar-wrapper" role="menu">
                                                            <div class="datepicker-calendar">
                                                                <div class="datepicker-calendar-header">
                                                                    <button type="button" class="prev"><span class="glyphicon glyphicon-chevron-left input-sm"></span><span class="sr-only">Previous Month</span></button>
                                                                    <button type="button" class="next"><span class="glyphicon glyphicon-chevron-right input-sm"></span><span class="sr-only">Next Month</span></button>
                                                                    <button type="button" class="title" data-month="11" data-year="2014">
                                                                        <span class="month">
                                                                            <span data-month="0">January</span>
                                                                            <span data-month="1">February</span>
                                                                            <span data-month="2">March</span>
                                                                            <span data-month="3">April</span>
                                                                            <span data-month="4">May</span>
                                                                            <span data-month="5">June</span>
                                                                            <span data-month="6">July</span>
                                                                            <span data-month="7">August</span>
                                                                            <span data-month="8">September</span>
                                                                            <span data-month="9">October</span>
                                                                            <span data-month="10">November</span>
                                                                            <span data-month="11" class="current">December</span>
                                                                        </span><span class="year">2017</span>
                                                                    </button>
                                                                </div>
                                                                <table class="datepicker-calendar-days">
                                                                    <thead>
                                                                        <tr>
                                                                            <th>Su</th>
                                                                            <th>Mo</th>
                                                                            <th>Tu</th>
                                                                            <th>We</th>
                                                                            <th>Th</th>
                                                                            <th>Fr</th>
                                                                            <th>Sa</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody></tbody>
                                                                </table>
                                                                <div class="datepicker-calendar-footer">
                                                                    <button type="button" class="datepicker-today">Today</button>
                                                                </div>
                                                            </div>
                                                            <div class="datepicker-wheels" aria-hidden="true">
                                                                <div class="datepicker-wheels-month">
                                                                    <h2 class="header">Month</h2>
                                                                    <ul>
                                                                        <li data-month="0">
                                                                            <button type="button">Jan</button></li>
                                                                        <li data-month="1">
                                                                            <button type="button">Feb</button></li>
                                                                        <li data-month="2">
                                                                            <button type="button">Mar</button></li>
                                                                        <li data-month="3">
                                                                            <button type="button">Apr</button></li>
                                                                        <li data-month="4">
                                                                            <button type="button">May</button></li>
                                                                        <li data-month="5">
                                                                            <button type="button">Jun</button></li>
                                                                        <li data-month="6">
                                                                            <button type="button">Jul</button></li>
                                                                        <li data-month="7">
                                                                            <button type="button">Aug</button></li>
                                                                        <li data-month="8">
                                                                            <button type="button">Sep</button></li>
                                                                        <li data-month="9">
                                                                            <button type="button">Oct</button></li>
                                                                        <li data-month="10">
                                                                            <button type="button">Nov</button></li>
                                                                        <li data-month="11">
                                                                            <button type="button">Dec</button></li>
                                                                    </ul>
                                                                </div>
                                                                <div class="datepicker-wheels-year">
                                                                    <h2 class="header">Year</h2>
                                                                    <ul></ul>
                                                                </div>
                                                                <div class="datepicker-wheels-footer clearfix">
                                                                    <button type="button" class="btn datepicker-wheels-back"><span class="glyphicon glyphicon-arrow-left"></span><span class="sr-only">Return to Calendar</span></button>
                                                                    <button type="button" class="btn datepicker-wheels-select">Select <span class="sr-only">Month and Year</span></button>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="col-md-12 form-group">
                                        <div class="col-md-12">
                                            <label class="control-label pull-left">CCC Referal</label>
                                        </div>
                                        <div class="col-md-12">
                                            <asp:DropDownList ID="cccReferalMod" runat="server" AutoPostBack="False" CssClass="form-control input-sm" onChange="CccEnabledMod();" ClientIDMode="Static">
                                                <asp:ListItem Text="select" Value=""></asp:ListItem>
                                                <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="False"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    
                                    <div class="col-md-12 form-group">
                                        <div class="col-md-12">
                                            <label class="control-label pull-left">Date Referred to CCC</label>
                                        </div>
                                        <div class="col-md-12">
                                            <div class='input-group date' id='CCCReferalDdatepicker'>
                                                <span class="input-group-addon">
                                                    <span class="glyphicon glyphicon-calendar"></span>
                                                </span>
                                                <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control input-sm" ID="CccReferalModDDate"  data-parsley-required="true" onblur="DateFormat(this,this.value,event,false,'3')" onkeyup="DateFormat(this,this.value,event,false,'3')"></asp:TextBox>        
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                
                                <div class="col-md-4">
                                    <div class="col-md-12 form-group">
                                        <div class="col-md-12">
                                            <label class="control-label pull-left">CCC Number</label>
                                        </div>
                                        <div class="col-md-12" id="cccnumMod">
                                            <input id="cccNumberMod" class="form-control input-sm" type="text" runat="server" data-parsley-trigger="keyup" 
                                                />
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <div class="col-md-12 form-group">
                            <div class="col-md-6">
                                <button type="button" id="btnSaveFamilyTesting" class="btn btn-default" onclientclick="return false;">Save</button>
                                <button type="button" id="btnCancelFamilyTesting" class="btn btn-default" data-dismiss="modal">Cancel</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        var tablefamily = null;
        var cccArrayList = new Array();
        var RelationshipPersonId = 0;
        var RelationshipRelationshipType = 0;
        var CCCNumber = "";
        var HivTestingResult = "";
        var Relationshiptype = "";
        var BaselineResult = "";
        var HivTestingDate = "";
        var ReferredToCare = "";
        var LinkageDate = "";
        var EnrollmentNumber = "";
        var FirstName = "";
        var MiddleName = "";
        var DOB = "";
        var LastName = "";
        var gender = "";
        var BaselineDate = "";
        var DobPrecision = "";
        $(document).ready(function () {
            window.patientAge = <%=PatientAge%>;
            var date = moment("<%=PatientDateOfBirth%>").format('DD-MMM-YYYY');
            window.patientDateOfBirth = date;
            var familyMembers = [];
            $("#<%=CccReferal.ClientID%>").val("False");
            var gender = '<%=Gender%>';

            var todayDate = new Date();
            var todayDatePicker = moment(todayDate).add(2, 'hours');

            //console.log(gender);

            //$('#BaselineHIVStatusD').datepicker({
            //    allowPastDates: true,
            //    date:0,
            //    momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
            //});

            $("#BaselineHIVStatusdatepicker").datetimepicker({
                format: 'DD-MMM-YYYY',
                allowInputToggle: true,
                useCurrent: false,
                maxDate: todayDatePicker
            });


            //$('#TestingDate').datepicker({
            //    allowPastDates: true,
            //    date:0,
            //    momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
            //});

            $('#TestingDate').datetimepicker({
                format: 'DD-MMM-YYYY',
                allowInputToggle: true,
                useCurrent: false,
                maxDate: todayDatePicker
            });

            //$('#DateOfBirth').datepicker({
            //    allowPastDates: true,
            //    momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
            //});

            $('#PersonDOBdatepicker').datetimepicker({
                format: 'DD-MMM-YYYY',
                allowInputToggle: true,
                useCurrent: false,
                maxDate: todayDatePicker
            });

            $('#CCCReferalDdatepicker').datetimepicker({
                format: 'DD-MMM-YYYY',
                allowInputToggle: true,
                useCurrent: false,
                maxDate: todayDatePicker
            });

            $('#CCCReferaldatepicker').datetimepicker({
                format: 'DD-MMM-YYYY',
                allowInputToggle: true,
                useCurrent: false,
                maxDate: todayDatePicker
            });

            //$('#BaselineHIVStatusDMod').datepicker({
            //    allowPastDates: true,
            //    date:0,
            //    momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
            //});

            $('#TestingDateMod').datepicker({
                allowPastDates: true,
                date: 0,
                momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
            });

            $('#DateOfBirthMod').datepicker({
                allowPastDates: true,
                momentConfig: { culture: 'en', format: 'DD-MMM-YYYY' }
            });

            $("#personAge").keyup(function () {
                var personAge = parseInt($("#personAge").val());

                if (personAge <= 0) {
                    $("#Dob").val("");
                    toastr.error("Patient's Age should not be zero", "Person Age");
                    return false;
                } else if (personAge > 120) {
                    $("#Dob").val("");
                    toastr.error("Patient's Age should not be more 120 years", "Person Age");
                    return false;
                }

                if (personAge != null && personAge != "" && (personAge > 0 || personAge <= 120)) {
                    $('#Dob').val(estimateDob(personAge));
                }

                $("#dobPrecision").val("false");
            });

            $("#SearchPeopleFamily").hide()
            $("#FamilyTestingDetails").hide();
            $("#familyMembersTable").hide();
            $("#RegisteredtoClinic").hide();
            $("#ReturnGrid").hide();
            //$("#hivTestingInfo").hide();
            //$("#isRegisteredInClinic").hide();

            $("#searchButton").hide();

            loadFamilyTesting();

            $("#btnReturnGrid").click(function () {
                $("#SearchPeopleFamily").show()
                
                $("#FamilyTestingDetails").hide();
                $("#ReturnGrid").hide();

            });
            $("#RegisteredInClinic").change(function () {
                var registeredInClinic = $("#RegisteredInClinic").find(":selected").text();

                if (registeredInClinic == "Yes") {
                    $("#hivTestingInfo").hide();
                    $("#searchButton").show();
                    $("#familyMembersTable").show();
                    $("#buttonSaving").hide();
                } else if (registeredInClinic == "No") {
                    $("#hivTestingInfo").show();
                    $("#searchButton").hide();
                    $("#familyMembersTable").hide();
                    $("#buttonSaving").show();
                }
            });

            //row selection
            $('#tblFindPatient').on('click', 'tbody tr', function () {
                // window.location.href = $(this).attr('href');
                var patientId = $(this).find('td').eq(0).text();

                var personId = $(this).find('td').eq(9).text();


                // alert("personId:" + patientId + " " + "Patient Status :" + patientStatus);
                GetPatientBaselineandResult(personId, patientId);
                


            });

            function GetDictionaryValue(array, key) {

                var keyValue = key;
                var result;
                jQuery.each(array, function () {
                    if (this.Key == keyValue) {
                        result = this.Value;
                        return false;
                    }
                });
                return result;
            }
       
            function GetPatientBaselineandResult(personId, patientId) {
               

               jQuery.ajax({
                        type: "POST",
                        url: "../WebService/PatientService.asmx/GetPatientBaselineandHivTesting",
                        data: "{'personId':'" + personId + "','patientId':'" + patientId + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {

                            console.log(response);
                            console.log(response);
                            console.log(response.d);
                            var json = jQuery.parseJSON(response.d);

                            if (json == "self") {
                                toastr.error("Cannot select yourself as family member");

                            }
                            else {
                                $("#ViewFamilyTestingDetails").hide();
                                $("#FamilyTestingDetails").show();
                                $("#SearchPeopleFamily").hide();
                                $("#ReturnGrid").show();
                             

                                resetElements();
                                //$("#FamilyTestingDetails").parsley.reset();
                                //$('.parsley-required').hide();

                                RelationshipPersonId = "";
                                RelationshipRelationshipType = "";
                                RelationshipPersonId = GetDictionaryValue(json, "PersonId");
                              
                                CCCNumber = GetDictionaryValue(json, "CCCNumber");
                                 HivTestingResult = GetDictionaryValue(json, "HivTestingResult");
                                 Relationshiptype = GetDictionaryValue(json, "Relationshiptype");
                                 BaselineResult = GetDictionaryValue(json, "BaselineResult");
                                 HivTestingDate = GetDictionaryValue(json, "HivTestingDate");
                                 ReferredToCare = GetDictionaryValue(json, "ReferredToCare");
                                 LinkageDate = GetDictionaryValue(json, "LinkageDate");

                                 EnrollmentNumber = GetDictionaryValue(json, "EnrollmentNumber");
                                 FirstName = GetDictionaryValue(json, "FirstName");
                                 MiddleName = GetDictionaryValue(json, "MiddleName");
                                 DOB = GetDictionaryValue(json, "DOB");
                                 LastName = GetDictionaryValue(json, "LastName");
                                 gender = GetDictionaryValue(json, "gender");
                                 BaselineDate = GetDictionaryValue(json, "BaselineDate");
                                 DobPrecision = GetDictionaryValue(json, "DobPrecision");
                                if (!(FirstName == null || FirstName == "undefined" || FirstName == "")) {
                                    $("#<%=FirstName.ClientID%>").val(FirstName);
                                }
                                if (!(MiddleName == null || MiddleName == 'undefined' || MiddleName == "")) {
                                    $("#<%=MiddleName.ClientID%>").val(MiddleName);

                                }


                                if (!(gender == null || gender == 'undefined' || gender == "" ||gender=="Unkwown")) {
                                    $("#<%=Sex.ClientID%>").val(gender);

                                }

                                if (!(DOB == null || DOB == 'undefined' || DOB == "")) {
                                    $("#<%=Dob.ClientID%>").val(moment(DOB).format('DD-MMM-YYYY'));

                                    var age = getAge(moment(DOB).format('DD-MMM-YYYY'));
                                    $("#personAge").val(age);

                                }

                                if (!(LastName == null || LastName == 'undefined' || LastName == "")) {
                                    $("#<%=LastName.ClientID%>").val(LastName);
                                }



                                if (!(DobPrecision == null || DobPrecision == 'undefined' || DobPrecision == "")) {
                                    $("#<%=dobPrecision.ClientID%>").val(DobPrecision);
                                }
                                if (!(Relationshiptype == null || Relationshiptype == 'undefined' || Relationshiptype == "")) {

                                    $("#<%=Relationship.ClientID%>").val(Relationshiptype);

                                }

                                if (!(BaselineResult == null || BaselineResult == "undefined" || BaselineResult == "")) {

                                    $("#<%=BaselineHIVStatus.ClientID%>").val(BaselineResult);


                                }
                                if (!(BaselineDate == null || BaselineDate == 'undefined' || BaselineDate == "")) {
                                    if (BaselineDate !== 'null') {
                                        $("#<%=BaselineHIVStatusDate.ClientID%>").val(moment(BaselineDate).format('DD-MMM-YYYY'));

                                    } else {
                                        $("#<%=BaselineHIVStatusDate.ClientID%>").val("");
                                    }

                                    $("#BaselineHIVStatusDate").attr('disabled', 'disabled');
                                }

                              if (!(CCCNumber == null || CCCNumber == "undefined" || CCCNumber == "" || CCCNumber == "0")) 
                                    {
                                        $("#hivoutcome").hide();
                                         
                                    }

                                if (!(CCCNumber == null || CCCNumber == "undefined" || CCCNumber == "" || CCCNumber == "0") && ReferredToCare == null) {
                                    ReferredToCare = true;

                                }

                                if (!(HivTestingResult == null || HivTestingResult == 'undefined' || HivTestingResult == "")) {
                                    $("#<%=hivtestingresult.ClientID%>").val(HivTestingResult);
                                    CccEnabled();

                                }

                                if (!(HivTestingDate == null || HivTestingDate == 'undefined' || HivTestingDate == "")) {
                                    $("#<%=HIVTestingDate.ClientID%>").val(moment(HivTestingDate).format('DD-MMM-YYYY'));
                                } else {
                                    $("#<%=HIVTestingDate.ClientID%>").val("");
                                }
                                $("#HIVTestingDate").prop("disabled", true);

                                if (!(ReferredToCare == null || ReferredToCare == "undefined" || ReferredToCare == "")) {
                                    if (ReferredToCare == "True" || ReferredToCare == true) {
                                        $("#<%=CccReferal.ClientID%>").val("True");

                                        CccEnabled();
                                    }
                                    else if (ReferredToCare == "False" || ReferredToCare == false) {
                                        $("#<%=CccReferal.ClientID%>").val("False");

                                        CccEnabled();
                                    }



                                }

                                if (!(LinkageDate == null || LinkageDate == "undefined" || LinkageDate == "")) {
                                    $("#<%=CCCReferalDate.ClientID%>").val(moment(LinkageDate).format('DD-MMM-YYYY'));
                                } else {
                                    $("#<%=CCCReferalDate.ClientID%>").val("");
                                }
                                $("#CCCReferalDate").prop("disabled", true);

                                if (!(CCCNumber == null || CCCNumber == "undefined" || CCCNumber == "" || CCCNumber == "0")) {
                                    $("#<%=cccNumber.ClientID%>").val(CCCNumber);
                                }


                            }
                        },
                        error: function (response) {
                            toastr.error(response.d, "Failed to retrieve family's member baseline and Result");
                        }
               });
            }
        

            $("#btnSearch").click(function () {
                $('#FamilyTestingForm').parsley().destroy();
                $('#FamilyTestingForm').parsley({
                    excluded:
                    "input[type=button], input[type=submit], input[type=reset], input[type=hidden], [disabled], :hidden"
                });

                var firstName = null;
                var middleName = null;
                var lastName = null;
                var sex = null;

                if ($('#FamilyTestingForm').parsley().validate()) {
                    firstName = $("#<%=FirstName.ClientID%>").val();
                    middleName = $("#<%=MiddleName.ClientID%>").val();
                    lastName = $("#<%=LastName.ClientID%>").val();
                    sex = $("#<%=Sex.ClientID%>").find(":selected").text();

                    searchFamilyMembersRegistedInClinic(firstName, middleName, lastName, sex);
                } else {
                    return false;
                }
            });

            $("#btnAdd").click(function (e) {
                $('#FamilyTestingForm').parsley().destroy();
                $('#FamilyTestingForm').parsley({
                    excluded:
                    "input[type=button], input[type=submit], input[type=reset], input[type=hidden], [disabled], :hidden"
                });

                if ($('#FamilyTestingForm').parsley().validate()) {

                    $('#FamilyTestingForm').parsley().destroy();

                    var firstName = escape($("#<%=FirstName.ClientID%>").val());
                    var middleName = escape($("#<%=MiddleName.ClientID%>").val());
                    var lastName = escape($("#<%=LastName.ClientID%>").val());
                    var sex = $("#<%=Sex.ClientID%>").val();
                    var dob = $("#Dob").val();
                    var dobPrecision = $("#dobPrecision").val();
                    var name = $("#<%=FirstName.ClientID%>").val() + ' ' + $("#<%=MiddleName.ClientID%>").val() + ' ' + $("#<%=LastName.ClientID%>").val();
                    var relationshipId = $("#<%=Relationship.ClientID%>").val();
                    var relationship = $("#Relationship :selected").text();
                    var baselineHivStatusId = $("#<%=BaselineHIVStatus.ClientID%>").val();
                    var baselineHivStatus = $("#BaselineHIVStatus :selected").text();
                    var baselineHivStatusDate = $("#<%=BaselineHIVStatusDate.ClientID%>").val();
                    var hivTestingresultId = $("#<%=hivtestingresult.ClientID%>").val();
                    var hivTestingresult = $("#hivtestingresult :selected").text();
                    hivTestingresult = hivTestingresult == "select" ? "" : hivTestingresult;
                    var hivTestingresultDate = $("#<%=HIVTestingDate.ClientID%>").val();
                    var cccreferal = $("#<%=CccReferal.ClientID%>").val();
                    var cccReferalNumber = $("#cccNumber").val();
                    var previousDate = moment().subtract(1, 'days').format('DD-MMM-YYYY');
                    var adult = moment().subtract(10, 'years').format('DD-MMM-YYYY');
                    var cccReferalDate = $("#CCCReferalDate").val();
                    if (dob != "") {
                        var today = new Date();
                        var birthDate = new Date(dob);
                        var age = today.getFullYear() - birthDate.getFullYear();
                    }

                    var today = new Date();
                    var birthDate = new Date(dob);
                   // var totalMonths =  parseInt(moment(today).diff(moment(birthDate), 'months'));
                  
                    var age = today.getFullYear() - birthDate.getFullYear();
                    var cccNumberFound = null;
                    var count = 0;
                    console.log(cccArrayList);
                    if (typeof cccReferalNumber !== "undefined" && cccReferalNumber != null && cccReferalNumber != "") {
                        cccNumberFound = $.inArray("" + cccReferalNumber + "", cccArrayList);


                        if (cccNumberFound > -1) {
                            toastr.error("Error", cccReferalNumber + " CCC Number already exists in the List");
                            return false;
                        }
                        cccArrayList.push("" + cccReferalNumber + "");
                    }
                    //setTimeout(function() { CccNumberExists(cccReferalNumber); }, 200);
                    ////console.log(baselineHivStatusDate);
                    //console.log(hivTestingresultDate);
                    //validations
                    if (baselineHivStatus != "Never Tested" && hivTestingresult == "Never Tested") {
                        toastr.error("Never Tested should not follow baseline(Tested Negative/Tested Positive).");
                        return false;
                    }
                    if (dob != "") {
                        if (moment('' + dob + '').isAfter()) {
                            toastr.error("Date of birth cannot be a future date.");
                            return false;
                        }
                        if (moment('' + dob + '').isAfter(previousDate)) {
                            toastr.error("Date of birth cannot be today.");
                            return false;
                        }
                    }
                    if (moment('' + baselineHivStatusDate + '').isAfter()) {
                        toastr.error("Baseline HIV status date cannot be a future date.");
                        return false;
                    }
                    if (moment('' + hivTestingresultDate + '').isAfter()) {
                        toastr.error("HIV testing result date cannot be a future date.");
                        return false;
                    }
                    console.log(baselineHivStatusDate);

                    if (((baselineHivStatusDate !== "") && !moment(baselineHivStatusDate, 'DD-MMM-YYYY').isValid())) {
                        toastr.error("Baseline HIV status date invalid.");
                        return false;
                    }

                    if (((hivTestingresultDate !== "") && !moment(hivTestingresultDate, 'DD-MMM-YYYY').isValid())) {
                        toastr.error("HIV testing result date invalid.");
                        return false;
                    }
                    if (dob != "") {
                        if (moment('' + baselineHivStatusDate + '').isBefore(dob)) {
                            toastr.error("Baseline HIV status date cannot be before the date of birth.");
                            return false;
                        }
                        if (moment('' + hivTestingresultDate + '').isBefore(baselineHivStatusDate)) {
                            toastr.error("Baseline HIV testing date cannot be after HIV testing result date.");
                            return false;
                        }
                        if (moment('' + hivTestingresultDate + '').isBefore(dob)) {
                            toastr.error("HIV testing result date cannot be before the date of birth.");
                            return false;
                        }
                         if ((moment('' + dob + '').isAfter(adult)) && (($("#Relationship :selected").text() === "Spouse")||($("#Relationship :selected").text() === "Partner")))  {
                    if (moment('' + baselineHivStatusDate + '').isBefore(dob)) {
                        toastr.error("Baseline HIV status date cannot be before the date of birth.");
                        return false;
                    }
                    if (moment('' + hivTestingresultDate + '').isBefore(baselineHivStatusDate)) {
                        toastr.error("Baseline HIV testing date cannot be after HIV testing result date.");
                        return false;
                    }
                    if (moment('' + hivTestingresultDate + '').isBefore(dob)) {
                        toastr.error("HIV testing result date cannot be before the date of birth.");
                        return false;
                    }
                    if (moment('' + baselineHivStatusDate + '').isAfter(hivTestingresultDate)) {
                        toastr.error("Baseline HIV status date cannot be greater than HIV testing result date.");
                        return false;
                    }
                    if ((moment('' + dob + '').isAfter(adult)) && (($("#Relationship :selected").text() === "Spouse") || ($("#Relationship :selected").text() === "Partner"))) {
                        toastr.error("A child cannot have a spouse or partner.");
                        return false;
                    }
                    }
                    if (moment('' + baselineHivStatusDate + '').isAfter(hivTestingresultDate)) {
                        toastr.error("Baseline HIV status date cannot be greater than HIV testing result date.");
                        return false;
                    }
                   
                    var fam = familyMembers.filter(function(el) {
                    var fam = familyMembers.filter(function (el) {
                        return (el.firstName === firstName) &&
                            (el.middleName === middleName) &&
                            (el.lastName === lastName) &&
                            (el.dob === dob) &&
                            (el.relationshipId === relationshipId);
                    });

                    if (cccreferal == "") {
                        cccreferal = false;
                    }
                    if (baselineHivStatusDate != "") {
                        baselineHivStatusDate = moment(baselineHivStatusDate, 'DD-MMM-YYYY').format("DD-MMM-YYYY");
                    }

                    if (fam.length > 0) {
                        toastr.error("Family member already added!");
                        return false;
                    }
                    else {
                        if (familyMembers.length > 0) {
                            count = geth(familyMembers) + 1;
                        }

                        var table = "<tr><td align='left'></td><td align='left'>" +
                            name +
                            "</td><td align='left'>" +
                            dob +
                            "<td align='left'>" +
                            relationship +
                            "</td><td align='left'>" +
                            baselineHivStatus +
                            "</td><td align='left'>" +
                            baselineHivStatusDate +
                            "</td><td align='left'>" +
                            hivTestingresult +
                            "</td><td align='left'>" +
                            hivTestingresultDate +
                            "</td><td align='left'>" +
                            cccreferal +
                            "</td><td align='left' style='display:none;'>" +
                            count +
                            "</td><td align='right'><button type='button' class='btnDelete btn btn-danger fa fa-minus-circle btn-fill' > Remove</button></td></tr>";
                        $("#tblFamilyTesting>tbody:first").append('' + table + '');

                        

                      

                        var testing = {
                            relationshipPersonId: RelationshipPersonId,
                            firstName: firstName,
                            middleName: middleName,
                            lastName: lastName,
                            sex: sex,
                            dob: dob,
                            dobPrecision: dobPrecision,
                            relationshipId: relationshipId,
                            baselineHivStatusId: baselineHivStatusId,
                            baselineHivStatusDate: baselineHivStatusDate,
                            hivTestingresultId: hivTestingresultId,
                            hivTestingresultDate: hivTestingresultDate,
                            cccreferal: cccreferal,
                            cccReferalNumber: cccReferalNumber,
                            cccReferalDate: cccReferalDate,
                            indexCount: count
                        };
                        familyMembers.push(testing);
                        resetElements();
                        e.preventDefault();
                    }
                } else {
                    return false;
                }

            });
            $("#btnClose").click(function () {
                
                window.location.href = '<%=ResolveClientUrl("~/CCC/patient/patientHome.aspx") %>';

            });
            $("#btnReset").click(function () {
                resetElements();
            });

        
            $("#btnSave").click(function () {
                if (familyMembers.length < 1) {
                    toastr.error("error", "Please insert at least One(1) family member");
                    return false;
                }
                
                    addFamilyTesting(familyMembers);
                
            });

           
            
            $("#tblFamilyTesting").on('click', '.btnDelete', function () {
                var indexcount = $(this).closest('tr').find("td").eq(9).html();
                var cccreferalnumber;
                for (var member in familyMembers) {
                    if (familyMembers[member]["indexCount"] == indexcount) {
                        cccreferalnumber = familyMembers[member]["cccReferalNumber"];
                        familyMembers.splice(member, 1);
                        
                        
                    }

                }

                for (var i = 0; i < cccArrayList.length; i++)
                    if (cccArrayList[i] == cccreferalnumber) {
                        cccArrayList.splice(i, 1);
                    }


                console.log(familyMembers);
                $(this).closest('tr').remove();
               
               
           
                
               
                //console.log($(this).closest('tr').find("td").eq(9).html());
            });

            
          
            $('input:radio[name=optRegistered]').change(function () {
                if (this.value == 'No') {
                    $('#FamilyTestingForm').parsley().destroy();
                             resetElements();
                             resetNoOptions();
                    
                    }
                else if (this.value == 'Yes') {
                    $('#FamilyTestingForm').parsley().destroy();
                        $("#SearchPeopleFamily").show()
                        $("#FamilyTestingDetails").hide();
                        $("#ReturnGrid").hide();
                    }
                });
           
         
            $("#FamilyAdd").click(function () {
                $("#ViewFamilyTestingDetails").hide();
                $("#FamilyTestingDetails").hide();
                $("#SearchPeopleFamily").hide();
                $("#RegisteredtoClinic").show();
                resetElements();
            });

            //update family testing
            $("#btnSaveFamilyTesting").click(function () {
                $("#familyMembersTable").show();
                followUpTestFamilyTesting();
            });

            $('#familyMembersSearched').on('click', 'button', function () {
                var data = tablefamily.row($(this).parents('tr')).data();
                //alert(data[0] + "'s salary is: " + data[8]);
                var isPatientLinked = data[10];
                if (isPatientLinked == "False" || isPatientLinked == "false") {
                    addRegisteredPatientFamily(data[8], data[9], data[6], data[0]);
                } else {
                    toastr.success("Patient is already linked to this patient", "Family Testing");
                    return false;
                }
            });

            function loadFamilyTesting() {
                var patientId ="<%=PatientId%>";
                jQuery.support.cors = true;
                $.ajax(
                    {
                        type: "POST",
                        url: "../WebService/PatientService.asmx/GetFamilyTestings",
                        data: "{'patientId':'" + patientId + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        cache: false,
                        success: function (response) {
                            window.itemList = response.d;
                            console.log(response.d);

                            var table = '';
                            itemList.forEach(function (item, i) {
                                var n = i + 1;
                                var name = item.FirstName + " " + item.MiddleName + " " + item.LastName;
                                var baselineDate = item.BaseLineHivStatusDate;
                                if (baselineDate != null) {
                                    baselineDate = moment(item.BaseLineHivStatusDate).format('DD-MMM-YYYY');
                                } else {
                                    baselineDate = "";
                                }
                                //console.log(moment(item.DateOfBirth).format('DD-MMM-YYYY'));
                                var testingDate = item.HivStatusResultDate;
                                if (testingDate != null) {
                                    testingDate = moment(item.HivStatusResultDate).format('DD-MMM-YYYY');
                                } else {
                                    testingDate = "";
                                }

                                var linkageDate = "";
                                if (item.LinkageDate != null)
                                {
                                    linkageDate = moment(item.LinkageDate).format('DD-MMM-YYYY');
                                }

                                var referred = "";
                                var action = "";
                                var enrollment = "";

                                if ((item.BaseLineHivStatus != "Tested Positive" && item.HivStatusResult != "Tested Positive")) {
                                    action = "<button type='button' id= 'btnEditTesting' class='btn btn-link btn-sm pull-right' data-toggle='modal' data-target='#testFollowupModal' onClick='editFamilyTesting(this)'>Follow-up Test</button>";
                                } else if ((item.CccReferal == "True" && item.BaseLineHivStatus == "Tested Positive") || (item.CccReferal == "True" && item.HivStatusResult == "Tested Positive")) {
                                    referred = "Referred";
                                } else if ((item.BaseLineHivStatus == "Tested Positive" && item.CccReferal == "False") || (item.HivStatusResult == "Tested Positive" && item.CccReferal == "False")) {
                                    referred = "Not Referred";
                                    action = "<button type='button' id= 'btnEditTesting' class='btn btn-link btn-sm pull-right' data-toggle='modal' data-target='#testFollowupModal' onClick='editFamilyTesting(this, true)'>Refer to another CCC</button>";
                                    enrollment = "<button type='button' id= 'btnEditTesting' class='btn btn-link btn-sm pull-right' onClick='enrollFamilyTesting(this)'>Enroll to this Facility</button>";
                                }

                                table += '<tr><td style="text-align: left">' + n + '</td><td style="text-align: left">' + name + '</td>' +
                                    '<td style="text-align: left">' + item.Relationship + '</td>' +
                                    '<td style="text-align:left;">' + moment(item.DateOfBirth).format('DD-MMM-YYYY') + '</td>' +
                                    '<td style="text-align: left">' + item.BaseLineHivStatus + '</td>' +
                                    '<td style="text-align: left">' + baselineDate + '</td>' +
                                    '<td style="text-align: left">' + item.HivStatusResult + '</td>' +
                                    '<td style="text-align: left">' + testingDate + '</td>' +
                                    '<td style="text-align: left">' + referred + "</td>" +
                                    '<td style="text-align: left">' + linkageDate + "</td>" +
                                    '<td style="text-align: left">' + action + '</td>' +
                                    '<td align="right">' + enrollment + '</td></tr>';
                            });
                   
                            $('#tableFamilymembers').append(table);

                        },

                        error: function (msg) {
                            alert(msg.responseText);
                        }
                    });
            }

            $('#Dob').change(function() {
                
            });

            $("#PersonDOBdatepicker").on("dp.change", function (e) {
                //Get new date value from the field on change
                var date = new Date(e.date);
                Dobchanged(date);
                var age = getAge(date);
                $("#personAge").val(age);
               
                $("#dobPrecision").val("true");
            });

        });

        function editFamilyTesting(x, referToCcc) {
            window.row = x.parentNode.parentNode.rowIndex;
            var rowIndex = row - 1;
            window.familyTesting = itemList[rowIndex];

            $("#<%=fName.ClientID%>").prop("disabled", true);
            $("#<%=mName.ClientID%>").prop("disabled", true);
            $("#<%=lName.ClientID%>").prop("disabled", true);
            $("#<%=sexMod.ClientID%>").prop("disabled", true);
            $("#<%=relationshipMod.ClientID%>").prop("disabled", true);
            $("#<%=dobMod.ClientID%>").prop("disabled", true);
            $("#<%=TextBox2.ClientID%>").prop("disabled", true);

            $("#<%=fName.ClientID%>").val(familyTesting.FirstName);
            $("#<%=mName.ClientID%>").val(familyTesting.MiddleName);
            $("#<%=lName.ClientID%>").val(familyTesting.LastName);

            var dd = document.getElementById('sexMod');
            for (var i = 0; i < dd.options.length; i++) {
                if (dd.options[i].text === familyTesting.Sex) {
                    dd.selectedIndex = i;
                    break;
                }
            }

            $("#<%=dobMod.ClientID%>").val(moment(familyTesting.DateOfBirth).format('DD-MMM-YYYY'));
            $("#<%=dobPrecision.ClientID%>").val(familyTesting.DobPrecision);
            var dd = document.getElementById('relationshipMod');
            for (var i = 0; i < dd.options.length; i++) {
                if (dd.options[i].text === familyTesting.Relationship) {
                    dd.selectedIndex = i;
                    break;
                }
            }

            $("#bHivStatusMod").prop("disabled", true);
            var dd = document.getElementById('bHivStatusMod');
            for (var i = 0; i < dd.options.length; i++) {
                if (dd.options[i].text === familyTesting.BaseLineHivStatus) {
                    dd.selectedIndex = i;
                    break;
                }
            }
            
            var baselineDate = familyTesting.BaseLineHivStatusDate;
            if (baselineDate != null) {
                $("#<%=bHivStatusDateMod.ClientID%>").val(moment(familyTesting.BaseLineHivStatusDate).format('DD-MMM-YYYY'));

            } else {
                $("#<%=bHivStatusDateMod.ClientID%>").val("");                
            }

            $("#bHivStatusDateMod").attr('disabled', 'disabled');


            //console.log(referToCcc);
            if (referToCcc === true) {
                var dd = document.getElementById('testingStatusMod');
                for (var i = 0; i < dd.options.length; i++) {
                    if (dd.options[i].text === familyTesting.HivStatusResult) {
                        dd.selectedIndex = i;
                        break;
                    }
                }

                $("#testingStatusMod").prop("disabled", true);

                var testingDate = familyTesting.HivStatusResultDate;
                if (testingDate != null) {
                    $("#<%=testingStatusDateMod.ClientID%>").val(moment(familyTesting.HivStatusResultDate).format('DD-MMM-YYYY'));
                } else {
                    $("#<%=testingStatusDateMod.ClientID%>").val("");                
                }
                $("#testingStatusDateMod").prop("disabled", true);

                <%--var dd = document.getElementById('cccReferalMod');
                for (var i = 0; i < dd.options.length; i++) {
                    if (dd.options[i].text === familyTesting.CccReferal) {
                        dd.selectedIndex = i;
                        break;
                    }
                }
                console.log(familyTesting.CccReferal);
                $("#<%=cccNumberMod.ClientID%>").val(familyTesting.CccReferalNumber);--%>

            }
            //$("#BaselineHIVStatusDMod").addClass("noneevents");
            //BaselineEnabledMod();
        }

        function enrollFamilyTesting(member) {
            window.row = member.parentNode.parentNode.rowIndex;
            var rowIndex = row - 1;
            window.familyTestingMember = itemList[rowIndex];

            console.log(familyTestingMember);
            var personId = familyTestingMember.PersonId;

            PageMethods.SetEnrollmentSession(personId);

            setTimeout(function () { window.location.href = '<%=ResolveClientUrl("~/CCC/Enrollment/ServiceEnrollment.aspx") %>'; }, 1000);
        }

        function resetElements(parameters) {
            $(".input-sm").val("");
        }

        function followUpTestFamilyTesting() {
            $('#testFollowupModal').parsley().destroy();
            $('#testFollowupModal').parsley({
                excluded:
                    "input[type=button], input[type=submit], input[type=reset], input[type=hidden], [disabled], :hidden"
            });

            if ($('#testFollowupModal').parsley().validate()) {
                var patientId = <%=PatientId%>;
                var patientMasterVisitId = <%=PatientMasterVisitId%>;
                var hivTestingId = familyTesting.HivTestingId;
                var personId = familyTesting.PersonId;
                var personRelationshipId = familyTesting.PersonRelationshipId;
                var userId = <%=UserId%>;
                var firstName = escape($("#<%=fName.ClientID%>").val());
                var middleName = escape($("#<%=mName.ClientID%>").val());
                var lastName = escape($("#<%=lName.ClientID%>").val());
                var sex = $("#<%=sexMod.ClientID%>").val();
                var dob = $("#<%=dobMod.ClientID%>").val();
                var dobPrecision = $("#dobPrecision").val();
                var relationshipId = $("#<%=relationshipMod.ClientID%>").val();
                var baselineHivStatusId = $("#<%=bHivStatusMod.ClientID%>").val();
                var baselineHivStatusDate = $("#<%=bHivStatusDateMod.ClientID%>").val();
                var hivTestingresultId = $("#<%=testingStatusMod.ClientID%>").val();
                var hivTestingresultText = $("#<%=testingStatusMod.ClientID%>").find(":selected").text();
                var hivTestingresultDate = $("#<%=testingStatusDateMod.ClientID%>").val();
                var cccreferal = $("#<%=cccReferalMod.ClientID%>").val();
                var cccReferalNumber = $("#<%=cccNumberMod.ClientID%>").val();
                var previousDate = moment().subtract(1, 'days').format('DD-MMM-YYYY');
                var adult = moment().subtract(15, 'years').format('DD-MMM-YYYY');
                var cccReferalModDate = $("#CccReferalModDDate").val();
                //console.log(CccReferalModDate);
                ////validations
                //return false;

                if (hivTestingresultText == "Never Tested") {
                    toastr.error("Never Tested should not be a follow up test result");
                    return false;
                }

                if (moment('' + dob + '').isAfter()) {
                    toastr.error("Date of birth cannot be a future date.");
                    return false;
                }
                if (moment('' + dob + '').isAfter(previousDate)) {
                    toastr.error("Date of birth cannot be today.");
                    return false;
                }
                if (moment('' + baselineHivStatusDate + '').isAfter()) {
                    toastr.error("Baseline HIV status date cannot be a future date.");
                    return false;
                }
                if (moment('' + hivTestingresultDate + '').isAfter()) {
                    toastr.error("HIV testing result date cannot be a future date.");
                    return false;
                }
                if (moment('' + baselineHivStatusDate + '').isBefore(dob)) {
                    toastr.error("Baseline HIV status date cannot be before the date of birth.");
                    return false;
                } 
                if (moment('' + hivTestingresultDate + '').isBefore(baselineHivStatusDate)) {
                    toastr.error("Baseline HIV testing date cannot be after HIV testing result date.");
                    return false;
                }
                if (moment('' + hivTestingresultDate + '').isBefore(dob)) {
                    toastr.error("HIV testing result date cannot be before the date of birth.");
                    return false;
                }
                if (moment('' + baselineHivStatusDate + '').isAfter(hivTestingresultDate)) {
                    toastr.error("Baseline HIV status date cannot be greater than HIV testing result date.");
                    return false;
                }
                if ((moment('' + dob + '').isAfter(adult)) && (($("#Relationship :selected").text() === "Spouse")||($("#Relationship :selected").text() === "Partner")))  {
                    toastr.error("A child cannot have a spouse or partner.");
                    return false;
                }
                if (patientAge < 16 && (($("#Relationship :selected").text() === "Spouse"))) {
                    $("#Relationship").val(0);
                    toastr.error("A child cannot have a spouse.");
                    return false;
                }
                if (patientAge < 16 && (($("#Relationship :selected").text() === "Partner"))) {
                    $("#Relationship").val(0);
                    toastr.error("A child cannot have a partner.");
                    return false;
                }
                if (patientAge < 10 && (($("#Relationship :selected").text() === "Child"))) {
                    $("#Relationship").val(0);
                    toastr.error("A child cannot have a child.");
                    return false;
                }
                else {
                    $.ajax({
                        type: "POST",
                        url: "../WebService/PatientService.asmx/UpdatePatientFamilyTesting",
                        data: "{'patientId': '" + patientId + "','patientMasterVisitId': '" + patientMasterVisitId + "','firstName': '" + firstName + "','middleName': '" + middleName + "','lastName': '" + lastName + "','sex': '" + sex + "','dob': '" + dob + "','relationshipId': '" + relationshipId + "','baselineHivStatusId': '" + baselineHivStatusId + "','baselineHivStatusDate': '" + baselineHivStatusDate + "','hivTestingresultId': '" + hivTestingresultId + "','hivTestingresultDate': '" + hivTestingresultDate + "','cccreferal': '" + cccreferal + "','cccReferalNumber': '" + cccReferalNumber + "','userId': '" + userId + "','personRelationshipId': '" + personRelationshipId + "','hivTestingId': '" + hivTestingId + "','personId': '" + personId + "','cccReferalModDate':'" + cccReferalModDate + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            toastr.success(response.d, "Family testing updated successfully");
                            setTimeout(function () { window.location.href = '<%=ResolveClientUrl("~/CCC/OneTimeEvents/FamilyTesting.aspx") %>'; }, 2500);
                        },
                        error: function (response) {
                            toastr.error(response.d, "Family testing not updated");
                        }
                    });
                }
            } 
            else {
                return false;
            }
        }

        function addFamilyTesting(testing) {
            <%--var firstName = testing.firstName;
            var middleName = testing.middleName;
            var lastName = testing.lastName;
            var sex = testing.sex;
            var dob = testing.dob;
            var relationshipId = testing.relationshipId;
            var baselineHivStatusId = testing.baselineHivStatusId;
            var baselineHivStatusDate = testing.baselineHivStatusDate;
            var hivTestingresultId = testing.hivTestingresultId;
            var hivTestingresultDate = testing.hivTestingresultDate;
            var cccreferal = testing.cccreferal;
            var cccReferalNumber = testing.cccReferalNumber;
            var patientId = <%=PatientId%>;
            var patientMasterVisitId = <%=PatientMasterVisitId%>;
            var userId = <%=UserId%>;--%>

            var familyMembers = JSON.stringify(testing);
            //console.log(testing);
            //return false;

            $.ajax({
                type: "POST",
                url: "../WebService/PatientService.asmx/AddPatientFamilyTesting",
                data: "{'familyMembers': '" + familyMembers +"'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    toastr.success(response.d, "Family testing saved successfully");

                    //delay to show success message before redirect
                    setTimeout(function() { window.location.href = '<%=ResolveClientUrl("~/CCC/OneTimeEvents/FamilyTesting.aspx") %>'; }, 2500);
                },
                error: function (response) {
                    toastr.error(response.d, "Family testing not saved");
                }
            });
        }

        function CccEnabled() {
            var testingResult = $("#hivtestingresult :selected").text();

            if ((testingResult === "Tested Negative")) {
                $("#cccNumber").prop('disabled', true);
                $("#<%=CccReferal.ClientID%>").val();
                $("#<%=CccReferal.ClientID%>").prop('disabled', true);
                $("#CCCReferalDate").prop('disabled', true);
                $("#HIVTestingDate").prop('disabled', false);
            } else if (testingResult === "Never Tested") {
                $("#cccNumber").prop('disabled', true);
                $("#<%=CccReferal.ClientID%>").val();
                $("#<%=CccReferal.ClientID%>").prop('disabled', true);
                $("#CCCReferalDate").prop('disabled', true);
                $("#HIVTestingDate").prop('disabled', true);
            } else {
                $("#cccNumber").prop('disabled',true);
                $("#<%=CccReferal.ClientID%>").prop('disabled', false);
                $("#CCCReferalDate").prop('disabled', true);
            }

            if ($("#CccReferal").val() === 'False') {
                $("#cccNumber").prop('disabled', true);
                $("#CCCReferalDate").prop('disabled', true);
            } else if ($("#CccReferal").val() === 'True')
            {
                 $("#cccNumber").prop('disabled',false);
                $("#<%=CccReferal.ClientID%>").prop('disabled', false);
                $("#CCCReferalDate").prop('disabled', false);

                
  
                $("#CCCReferaldatepicker").removeClass('noneevents');
            }
        }
      
        function BaselineEnabled() {
            var baselinehivstatus = $("#BaselineHIVStatus :selected").text();

            if (baselinehivstatus === "Never Tested" || baselinehivstatus === "Unknown") {
                $("#cccNumber").prop('disabled', true);
                $("#<%=CccReferal.ClientID%>").prop('disabled', true);
                $("#<%=BaselineHIVStatusDate.ClientID%>").prop('disabled', true);
                $("#BaselineHIVStatusD").addClass('noneevents');
                if (!(HivTestingResult == null || HivTestingResult == 'undefined' || HivTestingResult == "")) {
                    $("#hivtestingresult").val(HivTestingResult);
                }
                else {
                    $("#hivtestingresult").val("");
                }
                $("#hivtestingresult").prop('disabled', false);

                if (!(HivTestingDate == null || HivTestingDate == 'undefined' || HivTestingDate == "")) {
                    $("#HIVTestingDate").val(moment(HivTestingDate).format('DD-MMM-YYYY'))
                }
                else {
                        $("#HIVTestingDate").val("");
                }
                $("#HIVTestingDate").prop('disabled', false);        
                $("#TestingDate").removeClass('noneevents');

                $("#CCCReferalDate").prop('disabled', true);
                $("#CCCReferaldatepicker").addClass('noneevents');

            } else if (baselinehivstatus === "Tested Positive") {
                if (!(HivTestingResult == null || HivTestingResult == 'undefined' || HivTestingResult == "")) {
                    $("#hivtestingresult").val(HivTestingResult);
                }
                else {
                    $("#hivtestingresult").val("");
                }
                $("#hivtestingresult").prop('disabled', true);
                if (!(HivTestingDate == null || HivTestingDate == 'undefined' || HivTestingDate == "")) {
                    $("#HIVTestingDate").val(moment(HivTestingDate).format('DD-MMM-YYYY'))
                }
                else {
                    $("#HIVTestingDate").val("");
                }
                $("#HIVTestingDate").prop('disabled', true);
                $("#TestingDate").addClass('noneevents');

                $("#CCCReferalDate").prop('disabled', false);
                $("#CCCReferaldatepicker").removeClass('noneevents');

                $("#<%=BaselineHIVStatusDate.ClientID%>").val("");
                $("#<%=BaselineHIVStatusDate.ClientID%>").prop('disabled', false);
                $("#BaselineHIVStatusD").removeClass('noneevents');

                $("#cccNumber").prop('disabled', false);
                $("#<%=CccReferal.ClientID%>").prop('disabled', false);

            } else if (baselinehivstatus === "Tested Negative") {
                if (!(HivTestingResult == null || HivTestingResult == 'undefined' || HivTestingResult == "")) {
                    $("#hivtestingresult").val(HivTestingResult);
                }
                else {
                    $("#hivtestingresult").val("");
                }
                $("#hivtestingresult").prop('disabled', false);
                if (!(HivTestingDate == null || HivTestingDate == 'undefined' || HivTestingDate == "")) {
                    $("#HIVTestingDate").val(moment(HivTestingDate).format('DD-MMM-YYYY'))
                }
                else {
                    $("#HIVTestingDate").val("");
                }
                $("#HIVTestingDate").prop('disabled', false);
                $("#TestingDate").removeClass('noneevents');

                $("#cccNumber").prop('disabled', true);
                $("#<%=CccReferal.ClientID%>").prop('disabled', true);

                $("#CCCReferalDate").prop('disabled', true);
                $("#CCCReferaldatepicker").addClass('noneevents');

                $("#<%=BaselineHIVStatusDate.ClientID%>").prop('disabled', false);
            } else {
                $("#cccNumber").prop('disabled', false);
                $("#<%=CccReferal.ClientID%>").prop('disabled',false);
                $("#BaselineHIVStatusD").removeClass('noneevents');
                $("#<%=BaselineHIVStatusDate.ClientID%>").prop('disabled', false);
                $("#CCCReferalDate").prop('disabled', false);
                $("#CCCReferaldatepicker").removeClass('noneevents');
            }
        }

        <%--function HivEnabled() {
            if ($("#hivtestingresult :selected").text() === "Never Tested") {
              
                $("#<%=HIVTestingDate.ClientID%>").prop('disabled',true);
                $("#TestingDate").addClass('noneevents');
            } else {
              
                $("#<%=HIVTestingDate.ClientID%>").prop('disabled',false);
                $("#TestingDate").removeClass('noneevents');
            }
        }--%>


        function resetNoOptions() {

            $("#ViewFamilyTestingDetails").hide();
            $("#FamilyTestingDetails").show();
            $("#SearchPeopleFamily").hide();
            $("#ReturnGrid").hide();
            $("#hivTestingInfo").show();
            $("#hivoutcome").show();
         

        }
        function CccEnabledMod() {
            var testingStatusMod = $("#testingStatusMod :selected").text();

            if ((testingStatusMod === "Tested Negative") || (testingStatusMod === "Never Tested")) {
                $("#<%=cccNumberMod.ClientID%>").prop('disabled',true);
                $("#<%=cccReferalMod.ClientID%>").val("False");
                $("#<%=cccReferalMod.ClientID%>").prop('disabled', true);
                $("#CccReferalModDDate").prop('disabled', true);
            }
            else if ($("#cccReferalMod").val() === 'False') {
                $("#<%=cccNumberMod.ClientID%>").prop('disabled', true);
                $("#CccReferalModDDate").prop('disabled', true);
            } else {
                $("#<%=cccNumberMod.ClientID%>").prop('disabled',false);
                $("#<%=cccReferalMod.ClientID%>").prop('disabled', false);
                $("#CccReferalModDDate").prop('disabled', false);
            }
        }
      
        function BaselineEnabledMod() {
            var baselinehivstatus = $("#bHivStatusMod :selected").text();
            if (baselinehivstatus === "Never Tested") {
                $("#<%=cccNumberMod.ClientID%>").prop('disabled',true);
                $("#<%=cccReferalMod.ClientID%>").prop('disabled',true);
                $("#<%=bHivStatusDateMod.ClientID%>").prop('disabled',true);
                $("#BaselineHIVStatusDMod").addClass('noneevents');

                //$("#testingStatusMod").val("");
                $("#testingStatusMod").prop('disabled', false);
                //$("#testingStatusDateMod").val("");
                $("#testingStatusDateMod").prop('disabled', false);
                $("#TestingDateMod").removeClass('noneevents');

            } else if (baselinehivstatus == "Tested Positive"){
                //$("#testingStatusMod").val("");
                $("#testingStatusMod").prop('disabled', true);
                //$("#testingStatusDateMod").val("");
                $("#testingStatusDateMod").prop('disabled', true);
                $("#TestingDateMod").addClass('noneevents');

                <%--$("#<%=bHivStatusDateMod.ClientID%>").val("");--%>
                $("#<%=bHivStatusDateMod.ClientID%>").prop('disabled', false);
                $("#BaselineHIVStatusDMod").removeClass('noneevents');


                $("#<%=cccNumberMod.ClientID%>").prop('disabled', false);
                $("#<%=cccReferalMod.ClientID%>").prop('disabled', false);
            } else {
                $("#<%=cccNumberMod.ClientID%>").prop('disabled',false);
                $("#<%=cccReferalMod.ClientID%>").prop('disabled',false);
                $("#BaselineHIVStatusDMod").removeClass('noneevents');
                $("#<%=bHivStatusDateMod.ClientID%>").prop('disabled',false);
            }
        }

        function HivEnabledMod() {
            if ($("#testingStatusMod :selected").text() === "Never Tested") {
              
                $("#<%=HIVTestingDate.ClientID%>").prop('disabled',true);
                $("#TestingDateMod").addClass('noneevents');
            } else {
              
                $("#<%=testingStatusDateMod.ClientID%>").prop('disabled',false);
                $("#TestingDateMod").removeClass('noneevents');
            }
        }

        function Dobchanged(dob) {
            //console.log(dob);
            var adult = moment().subtract(10, 'years').format('DD-MMM-YYYY');
            if ((moment('' + dob + '').isAfter(adult)) && (($("#Relationship :selected").text() === "Spouse")))  {
                toastr.error("A child cannot have a spouse.");
                $("#Dob").val("");
                return false;
            }

            if ((moment('' + dob + '').isAfter(adult)) && (($("#Relationship :selected").text() === "Partner")))  {
                toastr.error("A child cannot have a partner.");
                $("#Dob").val("");
                return false;
            }

            if ((moment(dob).isAfter(patientDateOfBirth)) && (($("#Relationship :selected").text() === "Parent")))  {
                toastr.error("Parent cannot be younger than patient");
                $("#Dob").val("");
                return false;
            }

            if ((moment(dob).isAfter(patientDateOfBirth)) && (($("#Relationship :selected").text() === "Guardian")))  {
                toastr.error("Guardian cannot be younger than patient");
                $("#Dob").val("");
                return false;
            }

            if ((moment(patientDateOfBirth).isAfter(dob)) && (($("#Relationship :selected").text() === "Child")))  {
                toastr.error("Child cannot be older than patient");
                $("#Dob").val("");
                return false;
            }
        }

        function estimateDob(personAge) {
            var currentDate = new Date();
            currentDate.setDate(15);
            currentDate.setMonth(5);
            console.log(currentDate);
            var estDob = moment(currentDate.toISOString());
            var dob = estDob.add((personAge * -1), 'years');
            return moment(dob).format('DD-MMM-YYYY');
        }

        function getAge(dateString) {
            var today = new Date();
            var birthDate = new Date(dateString);
            var age = today.getFullYear() - birthDate.getFullYear();
            var m = today.getMonth() - birthDate.getMonth();
            if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
                age--;
            }

             $("#personMonth").val(m);
            return age;
              
        }

        function relationShipChanged() {
            var gender = '<%=Gender%>';
            var sex = $("#Sex :selected").text();


            if (((patientAge < 18) && (($("#Relationship :selected").text() === "Spouse")) && gender == "Male")) {
                $("#Relationship").val(0);
                toastr.error("A Male patient less than 18 years old should not have a spouse.");
                return false;
            } else if (((patientAge < 15) && (($("#Relationship :selected").text() === "Spouse")) && gender == "Female")) {
                $("#Relationship").val(0);
                toastr.error("A Female patient less than 15 years old should not have a spouse.");
                return false;
            } else if (($("#Relationship :selected").text() === "Spouse") && gender == "Male" && sex == "Male") {
                $("#Sex").val(0);
                toastr.error("A Male patient should not have a male spouse.");
                return false;
            } else if (($("#Relationship :selected").text() === "Spouse") && gender == "Female" && sex == "Female") {
                $("#Sex").val(0);
                toastr.error("A Female patient should not have a Female spouse.");
                return false;
            }

            if (($("#Relationship :selected").text() === "Co-Wife") && gender == "Male") {
                $("#Relationship").val(0);
                toastr.error("A Male patient should not have a Co-Wife.");
                return false;
            } else if (($("#Relationship :selected").text() === "Co-Wife") && (patientAge < 15)) {
                $("#Relationship").val(0);
                toastr.error("A Female patient less than 15 years should not have a Co-Wife.");
                return false;
            }

            if (patientAge < 18 && (($("#Relationship :selected").text() === "Partner"))) {
                $("#Relationship").val(0);
                toastr.error("Partner should be selected for patient above 18 years.");
                return false;
            }

            if (patientAge < 14 && (($("#Relationship :selected").text() === "Child"))) {
                $("#Relationship").val(0);
                toastr.error("A child should not have a child.");
                return false;
            }

            if (patientAge > 18 && (($("#Relationship :selected").text() === "Mother") || ($("#Relationship :selected").text() === "Father"))) {
                $("#Relationship").val(0);
                toastr.error("Father and Mother options should be selected for children under 18 years");
                return false;
            }

        }

        function searchFamilyMembersRegistedInClinic(firstName, middleName, lastName, sex) {
            var arrayReturn = [];

            $.ajax({
                type: "POST",
                url: "../WebService/PatientLookupService.asmx/GetPatientFamilyMembers",
                data: "{'firstName': '" + firstName + "','middleName':'" + middleName + "','lastName':'" + lastName + "','sex':'" + sex + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    //console.log(response.d);
                    var data = JSON.parse(response.d);
                    //window.familyMembers = data;
                    //console.log(data);
                    for (var i = 0, len = data.length; i < len; i++) {
                        arrayReturn.push(
                            [data[i].EnrollmentNumber, data[i].FirstName, data[i].MiddleName, data[i].LastName,
                                moment(data[i].DateOfBirth).format('DD-MMM-YYYY'), data[i].Sex, moment(data[i].EnrollmentDate).format('DD-MMM-YYYY'),
                                data[i].PatientStatus, data[i].Id, data[i].PersonId, data[i].LinkedToPatient]);
                    }
                    
                    inittable(arrayReturn);
                },
                error: function (response) {

                }
            });
        }

        function inittable(data) {
            //console.log(data);
            $("#familyMembersSearched").dataTable().fnDestroy();
            tablefamily = $('#familyMembersSearched').DataTable({
                "aaData": data,
                paging: true,
                searching: true,
                "columnDefs": [{
                    "data": null,
                    "targets": -1,
                    "defaultContent": "<button id='btnAddPatientFamilyMember' Class='btn btn-info btn-lg fa fa-plus-circle'> Add Member</button>"
                }]
            });
        }

        function addRegisteredPatientFamily(id, personId, enrollmentDate, cccNumber) {
            var relationshipType = $("#Relationship").val();
            //console.log(id);
            //console.log(personId);
            //console.log(relationshipType);
            //console.log(enrollmentDate);

            //return false;
            $.ajax({
                type: "POST",
                url: "../WebService/PatientService.asmx/AddPatientAsFamilyMember",
                data: "{'linkedPatientPersonId': '" + personId + "','relationshipTypeId':'" + relationshipType + "','baselineDate':'" + enrollmentDate + "','cccNumber':'" + cccNumber + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    console.log(response.d);

                    toastr.success(response.d, "Family testing saved successfully");
                    setTimeout(function() { window.location.href = '<%=ResolveClientUrl("~/CCC/OneTimeEvents/FamilyTesting.aspx") %>'; }, 500);
                    return false;
                },
                error: function (response) {
                    toastr.error(response.d, "Family Error");
                }
            });
        }

        function CccNumberExists(cccNumber) {
            $.ajax({
                type: "POST",
                url: "../WebService/PatientService.asmx/CccNumberExists",
                data: "{'cccNumber':'" + cccNumber + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(response) {
                    //isCccNumberExists = response.d;
                },
                error: function(response) {
                    toastr.error("Family Testing", response.d);
                }
            });
        }


        function geth(o) {
            var vals = [];
            for (var i in o) {
                vals.push(o[i]["indexCount"]);
            }
            console.log(vals);
            return Math.max.apply(null, vals);
        }

         //Remove empty ReGex validation constraints from relevant textboxes
        function removeEmptyValidationConstraints() {
            $('input[type=text]').each(function () {
                if ($(this).attr("data-parsley-pattern") == "") {
                    $(this).removeAttr("data-parsley-pattern");
                }
                if ($(this).attr("data-parsley-pattern-message") == "") {
                    $(this).removeAttr("data-parsley-pattern-message");
                }
            });
        }

        removeEmptyValidationConstraints();
    </script>

</asp:Content>
