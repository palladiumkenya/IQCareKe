<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="IQCare.Web.Scheduler.ContactCareTracking"
    Title="Untitled Page" EnableViewState="true" Codebehind="frmScheduler_ContactCareTracking.aspx.cs" %>
 <%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
    <%--<form id="CareTraking" method="post" runat="server">--%>
    <%-- <form id="CareTraking" method="post" runat="server">--%>
    <div class="center" style="padding: 8px;">
        <script language="javascript" type="text/javascript">

            function WindowPrint() {
                window.print();
            }
            function SetValue(theObject, theValue) {
                document.getElementById('ctl00_IQCareContentPlaceHolder_' + theObject).value = theValue; 
                document.forms[0].submit();
            }

            function fnvalidate(checkQty) {
                var hidval = document.getElementById('ctl00_IQCareContentPlaceHolder_hidID').value;
                var hidchkbox = document.getElementById('ctl00_IQCareContentPlaceHolder_hidcheckbox').value;
                var hidsinglechk = document.getElementById('ctl00_IQCareContentPlaceHolder_hidchkbox').value;
                var hidradio = document.getElementById('ctl00_IQCareContentPlaceHolder_hidradio').value;

                var ctrlid = hidval.split('%');
                var chkboxid = hidchkbox.split('%');
                var chksingle = hidsinglechk.split('%');
                var rdo = hidradio.split('%');
                var findradio = "";
                var rdoid = "";


                for (var i = 0; i < ctrlid.length; i++) {
                    var cid = "ctl00_IQCareContentPlaceHolder_" + ctrlid[i];

                    if (document.getElementById(cid) != null) {
                        //                 if(document.getElementById(cid).disabled = false)
                        //                   {

                        if (document.getElementById(cid).value == '') {
                            var id = "ctl00_IQCareContentPlaceHolder_LBL" + cid.substring(cid.indexOf("-"), cid.length);
                            var txt = document.getElementById(id).innerText;
                            document.getElementById(id).style.color = 'blue';
                            alert("Please enter the value for :" + txt);
                            document.getElementById(cid).focus();
                            return false;

                        }
                        if (document.getElementById(cid).value == '0') {
                            var id = "ctl00_IQCareContentPlaceHolder_LBL" + cid.substring(cid.indexOf("-"), cid.length);
                            var txt = document.getElementById(id).innerText;
                            document.getElementById(id).style.color = 'blue';
                            alert("Please select for :" + txt);
                            document.getElementById(cid).focus();
                            return false;
                        }

                        // }   
                    }


                }
                for (var i = 0; i < chksingle.length; i++) {
                    var cid = "ctl00_IQCareContentPlaceHolder_" + chksingle[i];
                    if (document.getElementById(cid) != null) {
                        if (document.getElementById(cid).disabled = false) {
                            if (document.getElementById(cid).checked == false) {
                                var id = "ctl00_IQCareContentPlaceHolder_LBL" + cid.substring(cid.indexOf("-"), cid.length);
                                var txt = document.getElementById(id).innerText;
                                document.getElementById(id).style.color = 'blue';
                                alert("please select for :" + txt);

                                return false;
                            }
                        }
                    }
                }

                for (var i = 0; i < chkboxid.length; i++) {
                    var chkid = "ctl00_IQCareContentPlaceHolder_" + chkboxid[i];
                    if (document.getElementById(chkid) != null) {
                        //                  if(document.getElementById(chkid).disabled = false)
                        //                     {
                        var chkBoxList = document.getElementById(chkid);
                        var find = "";
                        var chkBoxCount = chkBoxList.getElementsByTagName("input");
                        for (var j = 0; j < chkBoxCount.length; j++) {
                            if (chkBoxCount[j].checked) {
                                find = "True";
                            }
                        }
                        if (find == "") {

                            var id = "ctl00_IQCareContentPlaceHolder_LBL" + chkid.substring(chkid.indexOf("-"), chkid.length);
                            var txt = document.getElementById(id).innerText;
                            document.getElementById(id).style.color = 'blue';
                            alert("Please select for :" + txt);
                            document.getElementById(chkid).focus();
                            return false;

                        }

                        //} 

                    }
                }

                for (var i = 0; i < rdo.length; i++) {
                    var cid = "ctl00_IQCareContentPlaceHolder_" + rdo[i];
                    rdoid = cid;
                    if (document.getElementById(cid) != null) {
                        if (document.getElementById(cid).checked) {

                            findradio = "find";
                        }


                    }

                }
                if (document.getElementById(rdoid) != null) {
                    if (document.getElementById(rdoid).disabled = false) {

                        if (findradio == '') {
                            var id = "ctl00_IQCareContentPlaceHolder_LBL" + rdoid.substring(rdoid.indexOf("-"), rdoid.length);
                            var txt = document.getElementById(id).innerText;
                            document.getElementById(id).style.color = 'blue';
                            alert("Please check radio for :" + txt);

                            return false;
                        }
                    }
                }

            }


            function fnvalidateSaveQty(checkQty) {
                var hidval = document.getElementById('ctl00_IQCareContentPlaceHolder_hidID').value;
                var hidchkbox = document.getElementById('ctl00_IQCareContentPlaceHolder_hidcheckbox').value;
                var hidsinglechk = document.getElementById('ctl00_IQCareContentPlaceHolder_hidchkbox').value;
                var hidradio = document.getElementById('ctl00_IQCareContentPlaceHolder_hidradio').value;

                var ctrlid = hidval.split('%');
                var chkboxid = hidchkbox.split('%');
                var chksingle = hidsinglechk.split('%');
                var rdo = hidradio.split('%');
                var findradio = "";
                var rdoid = "";

                var HiddenMsgBuilder = ""

                HiddenMsgBuilder = "Following values are required to complete the data quality check:\n\n";


                for (var i = 0; i < ctrlid.length; i++) {
                    var cid = "ctl00_IQCareContentPlaceHolder_" + ctrlid[i];

                    if (document.getElementById(cid) != null) {
                        //                   if(document.getElementById(cid).disabled = false)
                        //                     {

                        if (document.getElementById(cid).value == '') {
                            var id = "ctl00_IQCareContentPlaceHolder_LBL" + cid.substring(cid.indexOf("-"), cid.length);
                            var txt = document.getElementById(id).innerText;

                            document.getElementById(id).style.color = 'red';
                            HiddenMsgBuilder = HiddenMsgBuilder + txt;
                            HiddenMsgBuilder = HiddenMsgBuilder + '\n';

                        }
                        if (document.getElementById(cid).value == '0') {
                            var id = "ctl00_IQCareContentPlaceHolder_LBL" + cid.substring(cid.indexOf("-"), cid.length);
                            var txt = document.getElementById(id).innerText;
                            document.getElementById(id).style.color = 'red';
                            HiddenMsgBuilder = HiddenMsgBuilder + txt;
                            HiddenMsgBuilder = HiddenMsgBuilder + '\n';
                        }

                        // }

                    }


                }
                for (var i = 0; i < chksingle.length; i++) {
                    var cid = "ctl00_IQCareContentPlaceHolder_" + chksingle[i];
                    if (document.getElementById(cid) != null) {
                        if (document.getElementById(cid).disabled = false) {

                            if (document.getElementById(cid).checked == false) {
                                var id = "ctl00_IQCareContentPlaceHolder_LBL" + cid.substring(cid.indexOf("-"), cid.length);
                                var txt = document.getElementById(id).innerText;
                                document.getElementById(id).style.color = 'red';
                                HiddenMsgBuilder = HiddenMsgBuilder + txt;
                                HiddenMsgBuilder = HiddenMsgBuilder + '\n';
                            }
                        }

                    }
                }

                for (var i = 0; i < chkboxid.length; i++) {
                    var chkid = "ctl00_IQCareContentPlaceHolder_" + chkboxid[i];
                    if (document.getElementById(chkid) != null) {
                        //                      if(document.getElementById(chkid).disabled = false)
                        //                       { 

                        var chkBoxList = document.getElementById(chkid);
                        var find = "";
                        var chkBoxCount = chkBoxList.getElementsByTagName("input");
                        for (var j = 0; j < chkBoxCount.length; j++) {
                            if (chkBoxCount[j].checked) {
                                find = "True";
                            }
                        }
                        if (find == "") {
                            var id = "ctl00_IQCareContentPlaceHolder_LBL" + chkid.substring(chkid.indexOf("-"), chkid.length);
                            var txt = document.getElementById(id).innerText;
                            document.getElementById(id).style.color = 'red';
                            HiddenMsgBuilder = HiddenMsgBuilder + txt;
                            HiddenMsgBuilder = HiddenMsgBuilder + '\n';
                        }
                        // }      
                    }

                }

                for (var i = 0; i < rdo.length; i++) {
                    var cid = "ctl00_IQCareContentPlaceHolder_" + rdo[i];
                    rdoid = cid;
                    if (document.getElementById(cid) != null) {
                        if (document.getElementById(cid).checked) {
                            findradio = "find";
                        }
                    }
                }
                if (document.getElementById(rdoid) != null) {
                    //                 if(document.getElementById(rdoid).disabled = false)
                    //                   {

                    if (findradio == '') {
                        var id = "ctl00_IQCareContentPlaceHolder_LBL" + rdoid.substring(rdoid.indexOf("-"), rdoid.length);
                        var txt = document.getElementById(id).innerText;
                        document.getElementById(id).style.color = 'red';
                        HiddenMsgBuilder = HiddenMsgBuilder + txt;
                        HiddenMsgBuilder = HiddenMsgBuilder + '\n';
                    }
                    // }

                }

                if (checkQty == '0') {

                    return fnvalidateQty(HiddenMsgBuilder);
                }

            }



            function fnvalidateQty() {
                var hidvalQty = document.getElementById('ctl00_IQCareContentPlaceHolder_hidIDQty').value;
                var hidchkboxQty = document.getElementById('ctl00_IQCareContentPlaceHolder_hidcheckboxQty').value;
                var hidsinglechkQty = document.getElementById('ctl00_IQCareContentPlaceHolder_hidchkboxQty').value;
                var hidradioQty = document.getElementById('ctl00_IQCareContentPlaceHolder_hidradioQty').value;

                var ctrlidQty = hidvalQty.split('%');
                var chkboxidQty = hidchkboxQty.split('%');
                var chksingleQty = hidsinglechkQty.split('%');
                var rdoQty = hidradioQty.split('%');
                var findradio = "";
                var rdoid = "";
                var HiddenMsgBuilder1 = ""

                HiddenMsgBuilder1 = "Following values are required to complete the data quality check:\n\n";

                for (var i = 0; i < ctrlidQty.length; i++) {
                    var cid = "ctl00_IQCareContentPlaceHolder_" + ctrlidQty[i];

                    if (document.getElementById(cid) != null) {
                        //               if(document.getElementById(cid).disabled = false)
                        //                   {
                        if (document.getElementById(cid).value == '') {
                            var id = "ctl00_IQCareContentPlaceHolder_LBL" + cid.substring(cid.indexOf("-"), cid.length);
                            var txt = document.getElementById(id).innerText;
                            document.getElementById(id).style.color = 'red';
                            HiddenMsgBuilder1 = HiddenMsgBuilder1 + txt;
                            HiddenMsgBuilder1 = HiddenMsgBuilder1 + '\n';
                        }
                        if (document.getElementById(cid).value == '0') {
                            var id = "ctl00_IQCareContentPlaceHolder_LBL" + cid.substring(cid.indexOf("-"), cid.length);
                            var txt = document.getElementById(id).innerText;
                            document.getElementById(id).style.color = 'red';
                            HiddenMsgBuilder1 = HiddenMsgBuilder1 + txt;
                            HiddenMsgBuilder1 = HiddenMsgBuilder1 + '\n';
                        }
                        // }   

                    }


                }
                for (var i = 0; i < chksingleQty.length; i++) {
                    var cid = "ctl00_IQCareContentPlaceHolder_" + chksingleQty[i];
                    if (document.getElementById(cid) != null) {
                        if (document.getElementById(cid).disabled = false) {
                            if (document.getElementById(cid).checked == false) {
                                var id = "ctl00_IQCareContentPlaceHolder_LBL" + cid.substring(cid.indexOf("-"), cid.length);
                                var txt = document.getElementById(id).innerText;
                                document.getElementById(id).style.color = 'red';
                                HiddenMsgBuilder1 = HiddenMsgBuilder1 + txt;
                                HiddenMsgBuilder1 = HiddenMsgBuilder1 + '\n';
                            }
                        }

                    }
                }

                for (var i = 0; i < chkboxidQty.length; i++) {
                    var chkid = "ctl00_IQCareContentPlaceHolder_" + chkboxidQty[i];
                    if (document.getElementById(chkid) != null) {
                        //                   if(document.getElementById(chkid).disabled = false)
                        //                   {
                        var chkBoxList = document.getElementById(chkid);
                        var find = "";
                        var chkBoxCount = chkBoxList.getElementsByTagName("input");
                        for (var j = 0; j < chkBoxCount.length; j++) {
                            if (chkBoxCount[j].checked) {
                                find = "True";
                            }
                        }
                        if (find == "") {

                            var id = "ctl00_IQCareContentPlaceHolder_LBL" + chkid.substring(chkid.indexOf("-"), chkid.length);

                            var txt = document.getElementById(id).innerText;
                            document.getElementById(id).style.color = 'red';
                            HiddenMsgBuilder1 = HiddenMsgBuilder1 + txt;
                            HiddenMsgBuilder1 = HiddenMsgBuilder1 + '\n';
                        }
                        //}
                    }

                }
                for (var i = 0; i < rdoQty.length; i++) {
                    var cid = "ctl00_IQCareContentPlaceHolder_" + rdoQty[i];
                    rdoid = cid;
                    if (document.getElementById(cid) != null) {
                        if (document.getElementById(cid).checked) {

                            findradio = "find";
                        }

                    }

                }
                if (document.getElementById(rdoid) != null) {
                    //            if(document.getElementById(rdoid).disabled = false)
                    //               {
                    if (findradio == '') {
                        var id = "ctl00_IQCareContentPlaceHolder_LBL" + rdoid.substring(rdoid.indexOf("-"), rdoid.length);
                        var txt = document.getElementById(id).innerText;
                        document.getElementById(id).style.color = 'red';
                        HiddenMsgBuilder1 = HiddenMsgBuilder1 + txt;
                        HiddenMsgBuilder1 = HiddenMsgBuilder1 + '\n';
                    }
                    // }    
                }

                if (HiddenMsgBuilder1 != 'Following values are required to complete the data quality check:\n\n') {
                    alert(HiddenMsgBuilder1);
                    return false;
                }
            }
            function DeathDate_Change() {
                document.getElementById('ctl00_IQCareContentPlaceHolder_txtCareEndDate').value = document.getElementById('ctl00_IQCareContentPlaceHolder_txtDeathDate').value;
            }

        </script>
        <div class="border center formbg">
            <table cellpadding="0" cellspacing="6" width="100%" border="0">
                <tbody>
                    <!-- Display Date of Miss Appoinment and Date of Actual Contact -->
                    <tr>
                        <td class="form center" width="50%">
                            <label for="AppDate">
                                Date of Missed Scheduled Appointment:</label>
                            <input id="txtMissedAppDate" runat="server" maxlength="11" size="10" name="AppDate" />
                            <img onclick="w_displayDatePicker('<%=txtMissedAppDate.ClientID%>');" height="22"
                                alt="Date Helper" hspace="3" src="../images/cal_icon.gif" width="22" border="0" /><span
                                    class="smallerlabel">(DD-MMM-YYYY)</span>
                        </td>
                        <td class="form center" width="50%">
                            <label id="lblcontactdate" for="DateLastContact">
                                Date of Last Actual Contact:</label>
                            <input id="txtDateLastContact" runat="server" maxlength="11" size="10" name="txtDateLastContact" />
                            <img onclick="w_displayDatePicker('<%=txtDateLastContact.ClientID%>');" height="22"
                                alt="Date Helper" hspace="3" src="../images/cal_icon.gif" width="22" border="0" /><span
                                    class="smallerlabel">(DD-MMM-YYYY)</span>
                        </td>
                    </tr>
                    <!-- Display ART Patient Careended and Non_ART Care Ended -->
                    <tr id="Artandnonart" runat="server">
                        <td colspan="2" class="form center" style="width: 100%; height: 33px;">
                            <div class="margin10">
                                <label class="required">
                                    *Patient Exit Reason:</label>
                                <asp:DropDownList ID="cmbPatientExitReason" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbPatientExitReason_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </td>
                    </tr>
                    <tr id="Tr_Deathreason" runat="server">
                        <td class="form" id="td1" visible="true" align="center" style="display: none">
                            <label class="required">
                                *Death Reason:</label>
                            <asp:DropDownList ID="cmbDeathReason" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbDeathReason_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td id="td2" class="form center" valign="top" visible="true" style="display: block">
                            <label for="AppDate1">
                                *Death Date:</label>
                            <input id="txtDeathDate" runat="server" maxlength="11" size="10" name="AppDate1"
                                onblur="DeathDate_Change();" />
                            <img onclick="w_displayDatePicker('<%=txtDeathDate.ClientID%>');" height="22" alt="Date Helper"
                                hspace="3" src="../images/cal_icon.gif" width="22" border="0" />
                            <span class="smallerlabel">(DD-MMM-YYYY)</span>
                        </td>
                    </tr>
                    <asp:Panel ID="PnlConFields" Visible="false" runat="server" Height="100%" Width="100%"
                        Wrap="true">
                    </asp:Panel>
                    <asp:Panel ID="DIVCustomItem" Visible="false" runat="server" Height="100%" Width="100%"
                        Wrap="true">
                    </asp:Panel>
                    <tr>
                        <td class="form" id="tdsignature" visible="true" align="center" style="width: 50%">
                            <label class="required">
                                *Signature:</label>
                            <asp:DropDownList ID="ddinterviewer" runat="server" Visible="true">
                            </asp:DropDownList>
                        </td>
                        <td class="form" id="tdCareEndDate" align="center" valign="top" visible="true" style="width: 50%" style="display: inline">
                            <label id="lblcareendeddate" class="required" visible="true" runat="server">
                                *Date Care Ended:</label>
                            <input id="txtCareEndDate" maxlength="11" size="10" visible="true" name="appDate"
                                runat="server" />
                            <img onclick="w_displayDatePicker('<%=txtCareEndDate.ClientID %>');" height="22"
                                alt="Date Helper" hspace="2" src="../images/cal_icon.gif" width="22" border="0"
                                id="IMG1" />
                            <span class="smallerlabel" id="appDatespan">(DD-MMM-YYYY)</span>
                        </td>
                    </tr>
                    <!-- Display Button -->
                    <tr>
                        <td class="Pad5 center" colspan="2">
                            <br />
                            <input type="submit" value="Save" id="btnsave" name="submit" runat="server" onclick="return fnvalidate('1');"
                                onserverclick="btnsave_ServerClick1" />
                            <%-- <input type="submit" value="Data Quality Check" ID="btnComplete" runat="server" name="DataQualityCheck" 
                            onserverclick="btnComplete_ServerClick1" onclick="return fnvalidateSaveQty('0');" /> --%>
                            <input type="submit" value="Data Quality Check" id="btnComplete" runat="server" name="DataQualityCheck"
                                onserverclick="btnComplete_ServerClick1" onclick="return fnvalidateQty();" />
                            <asp:Button ID="btnCancel" runat="server" Text="Close" OnClick="btnCancel_Click1" />
                            <asp:Button ID="btnOk" Text="OK" CssClass="textstylehidden" runat="server" />
                            <asp:Button ID="theBtnDQ" Text="DQ" CssClass="textstylehidden" runat="server" />
                            <asp:Button ID="btnPrint" Text="Print" runat="server" OnClientClick="WindowPrint()" />
                            <%--<input type="submit" value="Print" id="btnPrint" runat="server" onclick ="rerurn WindowPrint();" />--%>
                            <asp:HiddenField ID="hidID" runat="server" />
                            <asp:HiddenField ID="hidcheckbox" runat="server" />
                            <asp:HiddenField ID="hidradio" runat="server" />
                            <asp:HiddenField ID="hidchkbox" runat="server" />
                            <asp:HiddenField ID="hidIDQty" runat="server" />
                            <asp:HiddenField ID="hidcheckboxQty" runat="server" />
                            <asp:HiddenField ID="hidradioQty" runat="server" />
                            <asp:HiddenField ID="hidchkboxQty" runat="server" />
                            <asp:HiddenField ID="theHitCntrl" runat="server" />
                            <asp:HiddenField ID="HiddenMsgBuilderfield" runat="server" />
                            <br />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
