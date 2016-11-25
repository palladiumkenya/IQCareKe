<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True" Inherits="IQCare.Web.Admin.Customfields"
    Title="Untitled Page" MaintainScrollPositionOnPostback="true" Codebehind="frmConfig_Customfields.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">
   
    <div>
        <h1 class="margin" style="padding-left: 10px;">
            Configure Custom Form Fields</h1>
        <script language="javascript" type="text/javascript">
            /**********************************************************
            Function		: SendCodeName	
            Created By		: Amitava Sinha
            Created On		: 01-Feb-2006
            ***********************************************************/
            function SendCodeName() {
                var id = document.getElementById('<%=txtflabel.ClientID%>').value;
                if (id.length <= 0) {
                    alert('Please enter the field name.');

                    if (id.type == "radio") {
                        id.value = 0;
                    }

                    return true;
                }
                else {
                    CallServer(id, "This is context from client");
                    return true;
                }

            }

            function ReceiveServerData(args, context) {
                if (window.ActiveXObject) {
                    //var obj = new ActiveXObject("lt");
                    var obj = new ActiveXObject();
                    obj.loadXML(args);
                    var dsRoot = obj.documentElement;
                    var UserCount = dsRoot.getElementsByTagName('NO');
                    var text;
                    for (var count = 0; count < UserCount.length; count++) {
                        text = UserCount[count].text;

                     
                        showPopup();
                        //                }

                    }
                }
            }



            /**********************************************************
            Author : Amitava Sinha
            Creation Date :5-Jan-2007
            Purpose: Popup window Display 
            ***********************************************************/
            function showPopup() {
                var selectlist = "";
                var intCustomField = '<%=cfieldID%>';
                var lblName = document.getElementById('<%=txtflabel.ClientID%>').value;

                if (lblName == '') {
                    alert("Please enter the field name.")
                    document.getElementById('<%=rbtnselect.ClientID%>').checked = false;
                    document.getElementById('<%=rbtnmulti.ClientID%>').checked = false;
                    return;
                }
                if (intCustomField == 0) {
                    if (document.getElementById('<%=rbtnselect.ClientID%>').checked) {
                        selectlist = document.getElementById('<%=rbtnselect.ClientID%>').value;
                    }
                    if (document.getElementById('<%=rbtnmulti.ClientID%>').checked) {
                        selectlist = document.getElementById('<%=rbtnmulti.ClientID%>').value;
                    }
                    var strqrystring = "&List=" + selectlist
                    //var strqrystr="&CFID="+ intCustomField.toString() 
                    var url = "frmAdmin_ControlListSelector.aspx?Label=" + lblName
                    //url=url+strqrystring+strqrystr;
                    url = url + strqrystring;
                    //"if (this.checked) {convertAllFields('upper')}"
                    window.open(url, 'ControlList', 'toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=no,resize=no,width=700,height=350,scrollbars=yes');
                    //window.focus();
                }
            }

            function RearrangePopup() {


                var url = "frmAdmin_RearrangeList.aspx"

                window.open(url, 'ControlList', 'toolbars=no,location=no,directories=no,top=10,left=30,maximize=no,resize=no,width=700,height=350,scrollbars=yes');

            }


            function CheckNumericValue(e) {
                var key;
                key = e.which ? e.which : e.keyCode;
                if (key >= 48 && key <= 57) {
                    return true;
                }
                else {
                    alert("Please enter number only !");
                    return false;
                }
            }

            function CheckAlphaNumeric(e) {
                var key;
                key = e.which ? e.which : e.keyCode;
                //alert(key);
                if ((key >= 48 && key <= 57) || (key == 63) || (key >= 65 && key <= 90) || (key == 32) || (key >= 97 && key <= 122) || (key == 95)) {
                    return true;
                }
                else {
                    alert("Please enter Alpha Numeric only !");
                    return false;
                }
            }

            function CheckAlphaFirstLetter()
            //  check for valid numeric strings	
            {
                var strValidChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ-/";
                var strChar;
                var strlength;

                var id = document.getElementById('<%=txtflabel.ClientID%>');
                var idvalue = document.getElementById('<%=txtflabel.ClientID%>').value;

                strlength = idvalue.length;
                for (var count = 0; count < strlength; count++) {
                    strChar = idvalue.charAt(0);
                    if (strValidChars.indexOf(strChar) == -1) {
                        id = idvalue.substring(1, id.length);
                        idvalue = id;
                        document.getElementById('<%=txtflabel.ClientID%>').value = idvalue;
                    }
                    else {
                        document.getElementById('<%=txtflabel.ClientID%>').value = idvalue;
                        return;
                    }
                }

            }

            function CheckAlpha(e) {
                var key;
                key = e.which ? e.which : e.keyCode;
                //alert(key);
                if (key != 42) {
                    return true;
                }
                else {
                    return false;
                }
            }
           



        </script>
     
        <div class="center" style="padding: 5px;">
            <div class="border center formbg">
                <br />
                <table width="100%" border="0" cellpadding="0" cellspacing="6">
                    <tr>
                        <td class="border pad5 whitebg" valign="top" width="50%" align="left">
                            <label>
                                Choose Forms (select all that apply):</label><br />
                            <br />
                            <div class="divborder" style="height: 213px" nowrap="nowrap">
                                <asp:CheckBoxList ID="Chkformname" runat="server" CausesValidation="True" RepeatLayout="Flow"
                                    CssClass="check" Height="184px" Width="90%" OnSelectedIndexChanged="Chkformname_SelectedIndexChanged">
                                </asp:CheckBoxList>
                                &nbsp;
                            </div>
                        </td>
                        <td class="border pad5 whitebg" valign="top" width="50%">
                            <table width="100%" border="0" cellpadding="0" cellspacing="6">
                                <tr>
                                    <td class="border pad5 whitebg" valign="top" width="50%">
                                        <label class="margin20">
                                            *Field Label:</label><asp:TextBox ID="txtflabel" runat="server" Width="300px" MaxLength="90"></asp:TextBox><br />
                                        <br />
                                        <label class="margin20" style="vertical-align: top;">
                                            Field Descr:</label><asp:TextBox 
                                            ID="txtfdesc" runat="server" TextMode="MultiLine"
                                                MaxLength="250" CssClass="MultiLineTextBox"></asp:TextBox><br />
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="border pad5 whitebg" valign="top" width="50%" align="left">
                                        <label class="margin20">
                                            Field Type:</label><br />
                                        <div class="margin35" style="margin-top: 5px;" nowrap="nowrap">
                                            <input id="rbtntext" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);toggle('textbox');hide('numeric')"
                                                type="radio" name="radiobtn" runat="server" />
                                            <label>
                                                Text</label>
                                            <div id="textbox" style="display: none">
                                                <label class="margin20">
                                                    Specify type:</label>
                                                <asp:DropDownList ID="customTxtLines" runat="server">
                                                    <asp:ListItem Text="Single Line" Value="1" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Multi-Line" Value="8"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <br />
                                            <input id="rbtnselect" type="radio" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);hide('textbox');hide('numeric');showPopup();"
                                                value="Select List" name="radiobtn" runat="server" />
                                            <input type="hidden" id="hdnselect" name="hdnselect" value="4" />
                                            <label>
                                                Select List</label>
                                            <br />
                                            <input id="rbtndate" type="radio" value="Date" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);hide('textbox');hide('numeric')"
                                                name="radiobtn" runat="server" />
                                            <input type="hidden" id="hdnDate" name="hdnDate" value="5" />
                                            <label>
                                                Date</label>
                                            <br />
                                            <input id="rbtnnumber" type="radio" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);hide('textbox');toggle('numeric')"
                                                value="number" name="radiobtn" runat="server" />
                                            <input type="hidden" id="hdnNumber" name="hdnNumber" value="3" />
                                            <label>
                                                Numeric</label>
                                            <div id="numeric" style="display: none">
                                                <label>
                                                    Specify:</label>
                                                <span class="smalllabel">Min</span><asp:TextBox Width="30" ID="txtmin" runat="server"
                                                    CssClass="margin10" MaxLength="5"></asp:TextBox>
                                                <span class="smalllabel">Max</span><asp:TextBox Width="30" ID="txtmax" runat="server"
                                                    CssClass="margin10" MaxLength="5"></asp:TextBox>
                                                <span class="smalllabel">Units</span><asp:TextBox Width="70px" ID="txtunits" MaxLength="50"
                                                    runat="server" CssClass="margin10"></asp:TextBox>
                                            </div>
                                            <br />
                                            <input id="rbtnyesno" type="radio" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);hide('textbox');hide('numeric')"
                                                value="Yes No" name="radiobtn" runat="server" />
                                            <input type="hidden" id="hdnyesno" name="hdnyesno" value="6" />
                                            <label>
                                                Yes/No</label><br />
                                            <input id="rbtnmulti" type="radio" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);hide('textbox');hide('numeric');showPopup();"
                                                value="Multi Select List" name="radiobtn" runat="server" />
                                            <input type="hidden" id="hdnmultiline" name="hdnmultiline" value="9" />
                                            <label>
                                                Multi-Select Checklist</label>
                                            <br />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="100%" cellpadding="0" cellspacing="3">
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnAdd" runat="server" Text="Add Custom Field" OnClick="btnAdd_Click" />
                            <asp:Button ID="BtnEdit" runat="server" Text="Cancel" OnClick="btnCancel_Click" Width="113px" />
                            <td>
                                <asp:LinkButton ID="btnRearrange" runat="server" Text="Rearrange Fields" OnClientClick="RearrangePopup();" />
                            </td>
                        </td>
                    </tr>
                </table>
                <table width="100%" cellpadding="0" cellspacing="5">
                    <tbody>
                        <tr>
                            <td class="border pad5 formbg">
                                <div class="grid">
                                    <div class="rounded">
                                        <div class="top-outer">
                                            <div class="top-inner">
                                                <div class="top">
                                                    <h2>
                                                        Custom Fields</h2>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="mid-outer">
                                            <div class="mid-inner">
                                                <div class="mid" style="cursor: pointer; height: 280px; overflow: auto;">
                                                    <div id="div-gridview" class="gridview whitebg">
                                                        <asp:GridView ID="grdCustomfields" runat="server" AllowSorting="true" RowStyle-Wrap="true"
                                                            OnRowDataBound="grdCustomfields_RowDataBound" DataKeyNames="CustomFieldID" AutoGenerateColumns="False"
                                                            Width="100%" CssClass="datatable table-striped table-responsive" CellPadding="0" CellSpacing="0" BorderWidth="0"
                                                            GridLines="None" OnRowCommand="grdCustomfields_RowCommand" OnSelectedIndexChanging="grdCustomfields_SelectedIndexChanging"
                                                            OnSorting="grdCustomfields_Sorting" OnSelectedIndexChanged="grdCustomfields_SelectedIndexChanged">
                                                        <HeaderStyle CssClass="searchresultfixedheader" Height="20px"></HeaderStyle>
                                                            <RowStyle CssClass="gridrow" />
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="bottom-outer">
                                            <div class="bottom-inner">
                                                <div class="bottom">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnback_Click" />
                            </td>
                        </tr>
                    </tbody>
                </table>
                <br />
                <br />
            </div>
        </div>
    </div>
</asp:Content>
