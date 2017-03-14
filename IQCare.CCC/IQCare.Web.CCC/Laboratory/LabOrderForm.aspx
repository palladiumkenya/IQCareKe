<%@ Page Language="C#" AutoEventWireup="True" Inherits="IQCare.Web.Laboratory.LabOrderForm"
    CodeBehind="LabOrderForm.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" TagPrefix="act" Namespace="AjaxControlToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<?xml version="1.0" ?>
<html lang="en-US" xml:lang="en-US" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link rel="stylesheet" type="text/css" href="../style/styles.css" />
    <link rel="stylesheet" type="text/css" href="../style/calendar.css" />
    <title id="lblHeader" runat="server"></title>
</head>
<body>
    <script language="javascript" type="text/javascript" src="../incl/menu.js"></script>
    <script language="javascript" type="text/javascript" src="../incl/IQCareScript.js"></script>
    <script language="javascript" type="text/javascript" src="../incl/weeklycalendar.js"></script>
    <script language="javascript" type="text/javascript" src="../incl/highlightLabels.js"></script>
    <script language="javascript" type="text/javascript" src="../incl/dateformat.js"></script>
    <script language="javascript" type="text/javascript" src="../incl/jsDate.js"></script>
    <script language="javascript" type="text/javascript" src="../incl/disableEnter.js"></script>
    <script language="javascript" type="text/javascript">        buildWeeklyCalendar(0);</script>
    <script language="javascript" type="text/javascript">
        function WindowPrint() {
            window.print();
        }
    </script>
    <script type="text/javascript">
        function ace1_itemSelected(source, e) {
            var results = eval('(' + e.get_value() + ')');
            var index = source._selectIndex;
            if (index != -1) {
                //source.get_element().value = removeHTMLTags(source.get_completionList().childNodes[index]._value);
                var hdCustID = $get('<%= hdCustID.ClientID %>');
                hdCustID.value = results.SubTestId;
                var hDataType = $get('<%= hDataType.ClientID %>');
                hDataType.value = results.DataType;
            }
        }


        function onClientPopulated(sender, e) {
            var propertyPeople = sender.get_completionList().childNodes;
            for (var i = 0; i < propertyPeople.length; i++) {
                var div = document.createElement("span");
                var results = eval('(' + propertyPeople[i]._value + ')');
                //div.innerHTML = "<span style=' float:right; font-weight:bold;margin-right: 5px;'> " + results.AvlQty + "</span>";
                //div.innerHTML = results.AvlQty;
                // propertyPeople[i].appendChild(div);

            }

        }
        function removelink(pnlparentid, pnlchdid, labid, rowid) {

            if (confirm('Are you sure you want to Remove this Test?')) {
                document.getElementById(pnlchdid).style.display = 'none';
                if (rowid == '1') {
                    document.getElementById(pnlparentid).style.display = 'none';
                }
                //var result =
                PageMethods.UpdateTable(labid, onSucceed, onRemovalError);

                //                if (result == "0") {
                //                    document.getElementById("hdControlExists").value = "";
                //                }
            }
            return false;

        }
        function onSucceed(result) {
            if (result == "0") {
                document.getElementById("hdControlExists").value = "";
            }
        }
        function onRemovalError(error) { }
        function pageLoad() {

            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndAJAXRequestHandler);
        }

        function EndAJAXRequestHandler(sender, args) {

            if (args.get_error() != undefined && args.get_error().httpStatusCode == '500') {
                var errorMessage = args.get_error().message;
                args.set_errorHandled(true);
                alert("IQCare Application Framework encountered an unrecoverable error:\n" + errorMessage + "\n\nPlease report this error to the support team.");
            }

        }
        // PageMethods.GetActionCancelLinkInfo(OnGetActionCancelInfo, OnGetActionCancelInfoError);


        //**********************************************
        function OnLabSave(result) {
            if (result) {
                if (confirm('Lab tests saved successfully. Do you want to close?')) {
                    window.close();

                } 
            }
            else {
                alert("Unexpected error retrieving the patient data.");
            }
        }
        //**********************************************
        function OnLabSaveError(error) {
            if (error) alert("Error Saving lab request:\n" + error.get_message());
            else alert("Unexpected error saving lab request data.");
        }
        function fnSave() {
            var labcontrol = document.getElementById("hdControlExists").value;
            if (labcontrol == "") {
                alert("Plese select Lab");
                return false;
            }
            var e = document.getElementById("ddlaborderedbyname");
            var strlaborder = e.options[e.selectedIndex].value;
            if (strlaborder == "0") {
                alert('Please select order by');
                return false;
            }
            if (document.getElementById("txtlaborderedbydate").value == "") {
                alert("Order by date can not be blanck");
                return false;
            }
            var orderbydate = document.getElementById("txtlaborderedbydate").value;
            var labtobedone = document.getElementById("txtLabtobeDone").value;
            var appcurrentdate = document.getElementById("hdappcurrentdate").value;
            //var result =
            PageMethods.Save(labtobedone, strlaborder, orderbydate, appcurrentdate, OnLabSave, OnLabSaveError)//.value;
            //			if (result != "false") {
            //				if (confirm('Lab tests saved successfully. Do you want to close?')) {
            //					window.close();
            //				}
            //			}
           
        }
    </script>
    <style type="text/css">
        .autoextender
        {
            font-family: Courier New, Arial, sans-serif;
            font-size: 11px;
            font-weight: 100;
            border: solid 1px #006699;
            line-height: 15px;
            padding: 0px;
            background-color: White;
            margin-left: 0px;
            width: 800px;
        }
        .autoextenderlist
        {
            cursor: pointer;
            color: black;
        }
        .autoextenderhighlight
        {
            color: White;
            background-color: #006699;
            cursor: pointer;
        }
        #divwidth
        {
            width: 800px !important;
        }
        #divwidth div
        {
            width: 800px !important;
        }
    </style>
    <form id="frmLabOrderform" method="post" runat="server">
    <h1 class="margin" style="margin-left: 20px">
        Laboratory Order Form
    </h1>
    <div class="border formbg">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Auto"
            OnAsyncPostBackError="ActionScriptManager_AsyncPostBackError">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel" runat="server">
            <ContentTemplate>
                <table cellspacing="6" cellpadding="0" style="margin-top: 10px" width="100%" border="0">
                    <tbody>
                        <tr id="trPatientInfo" class="border">
                            <td class="form" align="center" colspan="2">
                                <label class="bold">
                                    Patient Name:
                                    <asp:Label ID="lblpatientname" runat="server"></asp:Label></label>
                                <label class="bold">
                                    IQ Number:
                                    <asp:Label ID="lblIQnumber" runat="server"></asp:Label></label>
                            </td>
                        </tr>
                        <tr id="Tr1" class="border" runat="server">
                            <td class="form bold" align="center" colspan="2">
                                <asp:Panel ID="thePnlIdent" runat="server">
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td class="form" align="center" valign="middle" width="50%">
                                <label>
                                    Preclinic Labs:</label><asp:CheckBox ID="preclinicLabs" runat="server" />
                            </td>
                            <td class="form" align="center" valign="middle">
                                <label for="LabtobeDone" class="right35">
                                    Lab to be done on:</label>
                                <asp:TextBox ID="txtLabtobeDone" MaxLength="11" runat="server" OnTextChanged="txtLabtobeDone_TextChanged"></asp:TextBox>
                                <img onclick="w_displayDatePicker('<%= txtLabtobeDone.ClientID %>');" height="22"
                                    alt="Date Helper" hspace="3" src="../images/cal_icon.gif" width="22" border="0" />
                                <span class="smallerlabel">(DD-MMM-YYYY)</span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="form">
                                <div class="border pad5">
                                    <label class="" style="margin-left: 10px">
                                        Select Lab:</label>
                                    <asp:TextBox ID="txtautoTestName" Width="400px" runat="server" AutoPostBack="true"
                                        AutoComplete="off" Font-Names="Courier New" OnTextChanged="txtautoTestName_TextChanged"></asp:TextBox>
                                    <div id="divwidth">
                                    </div>
                                    <act:AutoCompleteExtender ServiceMethod="Searchlab" MinimumPrefixLength="2" CompletionInterval="30"
                                        EnableCaching="false" CompletionSetCount="10" TargetControlID="txtautoTestName"
                                        BehaviorID="AutoCompleteEx" OnClientItemSelected="ace1_itemSelected" ID="AutoCompleteExtender1"
                                        runat="server" OnClientPopulated="onClientPopulated" FirstRowSelected="false"
                                        CompletionListCssClass="autoextender" CompletionListItemCssClass="autoextenderlist"
                                        CompletionListHighlightedItemCssClass="autoextenderhighlight" CompletionListElementID="divwidth">
                                        <Animations>
											  <OnShow>
											  <Sequence>
											  <%-- Make the completion list transparent and then show it --%>
											  <OpacityAction Opacity="0" />
											  <HideAction Visible="true" />

											  <%--Cache the original size of the completion list the first time
												the animation is played and then set it to zero --%>
											  <ScriptAction Script="// Cache the size and setup the initial size
																			var behavior = $find('AutoCompleteEx');
																			if (!behavior._height) {
																				var target = behavior.get_completionList();
																				behavior._height = target.offsetHeight - 2;
																				target.style.height = '0px';
																			}" />
											  <%-- Expand from 0px to the appropriate size while fading in --%>
											  <Parallel Duration=".4">
											  <FadeIn />
											  <Length PropertyKey="height" StartValue="0" 
												EndValueScript="$find('AutoCompleteEx')._height" />
											  </Parallel>
											  </Sequence>
											  </OnShow>
											  <OnHide>
											  <%-- Collapse down to 0px and fade out --%>
											  <Parallel Duration=".4">
											  <FadeOut />
											  <Length PropertyKey="height" StartValueScript="$find('AutoCompleteEx')._height" EndValue="0" />
											  </Parallel>
											  </OnHide>
                                        </Animations>
                                    </act:AutoCompleteExtender>
                                    <asp:HiddenField ID="hdCustID" runat="server" />
                                    <asp:HiddenField ID="hDataType" runat="server" />
                                    <asp:HiddenField ID="hdControlExists" runat="server" />
                                    <asp:HiddenField ID="hdappcurrentdate" runat="server" />
                                </div>
                            </td>
                        </tr>
                        <tr id="Tr2" class="border" runat="server">
                            <td class="form bold" align="left" colspan="2">
                                <div class="border pad5 whitebg" id="pnllabtest" style="overflow: auto" runat="server"
                                    visible="false">
                                    <asp:Panel ID="pnlselectlab" Height="100%" Width="100%" runat="server">
                                    </asp:Panel>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="form" style="width: 50%" align="center">
                                <label class="required">
                                    *Ordered by:</label>
                                <asp:DropDownList ID="ddlaborderedbyname" runat="Server">
                                </asp:DropDownList>
                            </td>
                            <td class="form" style="width: 50%" align="center">
                                <label class="required" for="LabOrderedbyDate">
                                    *Ordered By Date:</label>
                                <asp:TextBox ID="txtlaborderedbydate" MaxLength="12" size="11" runat="server"></asp:TextBox>
                                <img id="appDateimg1" onclick="w_displayDatePicker('<%=txtlaborderedbydate.ClientID%>');"
                                    height="22" alt="Date Helper" hspace="5" src="../images/cal_icon.gif" width="22"
                                    border="0" name="appdateimg">
                                <span class="smallerlabel" id="appdatespan1">(DD-MMM-YYYY)</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="pad5 center" colspan="2">
                                <input type="button" onclick="fnSave();" value="Save" id="btnsave" style="font-size: 12px;
                                    font-weight: 75px" />
                                <asp:Button ID="btnclose" runat="server" Font-Size="12px" Width="75px" Text="Close"
                                    OnClick="btnclose_Click" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="txtautoTestName"></asp:PostBackTrigger>
                <asp:PostBackTrigger ControlID="btnclose"></asp:PostBackTrigger>
            </Triggers>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
