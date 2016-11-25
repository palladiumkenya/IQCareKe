<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master"
    Inherits="IQCare.Web.Reports.frmReportCustomNew" MaintainScrollPositionOnPostback="true" EnableViewState="true" AutoEventWireup="True" EnableEventValidation="true"
    Title="Untitled Page" Codebehind="frmReportCustomNew.aspx.cs" %>

<%@ MasterType VirtualPath="~/MasterPage/IQCare.master" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">

 <script language="javascript" type="text/javascript">
  function GetRefresh()
  { 
     document.forms[0].submit();  
  }
  function fnChangeValue(id,vid)
  {
      var CtrlID = 'ctl00_IQCareContentPlaceHolder_' + id;
      var CtrlvID = 'ctl00_IQCareContentPlaceHolder_' + vid;
    if(document.getElementById(CtrlID).value=='IS NULL' || document.getElementById(CtrlID).value=='IS NOT NULL')
    {
        alert("Please change where conditon");
        document.getElementById(CtrlvID).value='Select';
        return false;
    }
     return true;
  }
  function fnChangeCondition(id,vid)
  {
      var CtrlID = 'ctl00_IQCareContentPlaceHolder_' + id;
      var CtrlvID = 'ctl00_IQCareContentPlaceHolder_' + vid;
    if(document.getElementById(CtrlID).value=='IS NULL' || document.getElementById(CtrlID).value=='IS NOT NULL')
    {
       
        document.getElementById(CtrlvID).value='';
        return false;
    }
     return true;
  }
  function fnChangeText(id,vid)
  {
      var CtrlID = 'ctl00_IQCareContentPlaceHolder_' + id;
      var CtrlvID = 'ctl00_IQCareContentPlaceHolder_' + vid;
    if(document.getElementById(CtrlID).value=='IS NULL' || document.getElementById(CtrlID).value=='IS NOT NULL')
    {
        alert("Please change where conditon");
        document.getElementById(CtrlvID).value='';
        return false;
    }
     return true;
  }
  function fnChangeDateValue(id,vid)
  {

      var CtrlID = 'ctl00_IQCareContentPlaceHolder_' + id;
      var CtrlvID = 'ctl00_IQCareContentPlaceHolder_' + vid;
    if(document.getElementById(CtrlID).value=='IS NULL' || document.getElementById(CtrlID).value=='IS NOT NULL')
    {
        alert("Please change where conditon");
        document.getElementById(CtrlvID).value='';
        return false;
    }
     return true;
  }
  function fnValidate()
  {
       
       
       for(j=1;j<10;j++)
       {
           var controlCount = document.forms[0].elements.length;
           var cntrl="";
           var ctrlValue="";
           
           
           for(k=2;k<6;k++)
           {
               cntrl = trim('ctl00_IQCareContentPlaceHolder_pnlSub' + j + 'Con' + k + 'Optr');
               ctrlValue = trim('ctl00_IQCareContentPlaceHolder_pnlSub' + j + 'Con' + k + 'txtValue');
               ctrlDate = trim('ctl00_IQCareContentPlaceHolder_pnlSub' + j + 'Con' + k + 'DateValue')
               ctrldd = trim('ctl00_IQCareContentPlaceHolder_pnlSub' + j + 'Con' + k + 'ddlValue')
               for(i = 0; i < controlCount; i++)
               {
                     
                        
                        var elm = document.forms[0].elements[i];
                        var ctrlID=elm.id;
                            
                            if(ctrlID==cntrl)
                            {
                                if(document.getElementById(cntrl).value!='Select' && document.getElementById(cntrl).value!='IS NULL' && document.getElementById(cntrl).value!='IS NOT NULL')
                                {
                                        if(document.getElementById(ctrlValue)!=null)
                                        {
                                            if(document.getElementById(ctrlValue).value=="")
                                            {
                                             alert('Pleas enter value in where condition');
                                             return false;
                                            }
                                        }
                                        if(document.getElementById(ctrlDate)!=null)
                                        {
                                            if(document.getElementById(ctrlDate).value=="")
                                            {
                                             alert('Pleas enter Date in where condition');
                                             return false;
                                            }
                                        }
                                        if(document.getElementById(ctrldd)!=null)
                                        {
                                            
                                            if(document.getElementById(ctrldd).value=="")
                                            {
                                             alert('Pleas  select a value in where condition');
                                             return false;
                                            }
                                        }
                                    
                                    
                                }
                            }
                
               }
           }
       }
      
       return true;
  }
    function trim(str)
    {    
        while (str.substring(0, 1) == ' ') 
        {        
        str = str.substring(1, str.length);    
        }    
        while (str.substring(str.length - 1, str.length) == ' ') 
        {        
        str = str.substring(0, str.length - 1);    
        }    
        return str;
    } 
    </script>
     <div <%--style="padding-top:140px;"--%>>
    <%--<form runat="server" id="frmNewCustomReport" onload ="Page_Load">--%>
    <h1 class="nomargin">
        New Custom Report</h1>
<%--    <asp:ScriptManager ID="scm_customreport" runat="server" OnAsyncPostBackError="scm_customreport_AsyncPostBackError">
    </asp:ScriptManager>--%>
    <div id="divshow" class="border center formbg">
        <asp:UpdatePanel ID="up_customreport"   runat="server">
            <ContentTemplate>
                <table class="center" cellspacing="6" cellpadding="0" width="915" border="0">
                    <tbody>
                        <tr>
                            <td align="left" width="100%">
                                <asp:RadioButton ID="rdoDynamicQuery" runat="server" Text="Dynamic Query" OnCheckedChanged="rdoDynamicQuery_CheckedChanged"
                                    GroupName="QueryType" ToolTip="Select option" AutoPostBack="true"></asp:RadioButton>
                                <asp:RadioButton ID="rdoTSQL" runat="server" Text="Type SQL" OnCheckedChanged="rdoTSQL_CheckedChanged"
                                    GroupName="QueryType" ToolTip="Make your own query" AutoPostBack="true"></asp:RadioButton>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 905px; height: 33px" align="left">
                                <label style="width: 200px">
                                    Step 1: Give your report an identity</label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 905px" class="border pad5 whitebg" align="center">
                                <table width="100%">
                                    <tbody>
                                        <tr>
                                            <td style="width: 30%" align="right">
                                                <label style="width: 80%">
                                                    *<span style="color: #6666cc"> Report Title </span>:
                                                </label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtTitle" runat="server" Width="400px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <label style="width: 80px" forecolor="Blue">
                                                    Description :
                                                </label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtDescription" CssClass="textarea" runat="server" Width="400px" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <label style="width: 80px" forecolor="Blue">
                                                    * <span style="color: #6666cc">Category </span>:</label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddCategory" runat="server" Width="200px">
                                                </asp:DropDownList>
                                                &nbsp;&nbsp;&nbsp;
                                                <input id="btnNewCategory" type="button" value="New Category" runat="server" />
                                                &nbsp;&nbsp;
                                                <input style="visibility: hidden" id="txtNewCategory" type="text" runat="server" />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 905px" class="border pad5 whitebg">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 905px; height: 18px" align="left">
                                <label style="width: 600px">
                                    Step 2: Select your columns and column action</label>
                            </td>
                        </tr>
                        <tr>
                            <td class="form" align="left">
                                <asp:Panel ID="pnlCustomRpt" runat="server" Width="900px" BackColor="white">
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <%--<asp:UpdateProgress ID="updateProgress" runat="server" AssociatedUpdatePanelID="up_customreport">
                                        <ProgressTemplate>
                                            Processing....<asp:Image runat="server" ID="imggif" ImageUrl="~/Images/loading.gif"
                                                ImageAlign="AbsMiddle" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>--%>
                                <asp:Button ID="btnAddField" OnClick="btnAddField_Click" runat="server" Text="Add Field">
                                </asp:Button>
                                <asp:Button ID="btnRemoveField" OnClick="btnRemoveField_Click" runat="server" Text="Remove Field">
                                </asp:Button>
                                <input style="visibility: hidden; width: 12px" id="hdnFld" type="text" name="hdnFld"
                                    runat="server" />
                                <asp:HiddenField ID="hidMessage" runat="server"></asp:HiddenField>
                                <asp:HiddenField ID="hidOrgMsg" runat="server"></asp:HiddenField>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 905px" class="border pad5 whitebg">
                                <table width="100%">
                                    <tbody>
                                        <tr>
                                            <td colspan="2">
                                                <label style="font-weight: bold; font-size: small">
                                                    Advanced Query Options:
                                                </label>
                                                <hr />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <label>
                                                    Enter SQL Statement :
                                                </label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtSQLStatement" runat="server" Width="700px" TextMode="MultiLine"
                                                    Height="150px" CssClass="textjustify textarea"></asp:TextBox>
                                                <asp:HiddenField ID="hidReportid" runat="server"></asp:HiddenField>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 905px" class="border pad5 whitebg">
                                      <asp:Button ID="btnSaveReport" OnClientClick="return fnValidate();" OnClick="btnSaveReport_Click"
                                    runat="server" Text="Save and Run Report"></asp:Button>
                                <asp:Button ID="Button2" runat="server" Text="Export Report Parameters" onclick="Button2_Click" />
                                <asp:Button ID="btnClose" OnClick="btnClose_Click" runat="server" Text="Close" Width="60px">
                                </asp:Button>
                                
                            </td>
                        </tr>
                    </tbody>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSaveReport" />
                <asp:PostBackTrigger ControlID="btnRemoveField" />
                <asp:PostBackTrigger ControlID="btnClose" />
                <asp:PostBackTrigger ControlID="Button2" />
            </Triggers>
        </asp:UpdatePanel>

        <script language="javascript" type="text/javascript">
            if (typeof(Sys) !== 'undefined')
                    Sys.Application.notifyScriptLoaded();
            var pageManager = Sys.WebForms.PageRequestManager.getInstance();
            var uiId = '';
            pageManager.add_beginRequest(myBeginRequestCallback);
            function myBeginRequestCallback(sender, args)
            {
                   var postbackElem = args.get_postBackElement();
                   uiId = postbackElem.id;
                   postbackElem.disabled = true;
                   //document.getElementById('divshow').disabled="disabled";
                   
                   

            }

            pageManager.add_endRequest(myEndRequestCallback);
             function myEndRequestCallback(sender, args)
            {
                var hidbox = $get('<%=hidMessage.ClientID %>');
                $get(uiId).disabled = false;
                //document.getElementById('divshow').disabled="";
                if(hidbox.value=="No")
                {
                    alert("No Record Found");
                }
                if(hidbox.value=="Title")
                {
                    var msg=document.getElementById('<%=hidOrgMsg.ClientID %>').value
                    alert(msg);
                    document.getElementById('<%=hidOrgMsg.ClientID %>').value="";
                    hidbox.value="";
                }
            }
        </script>

    </div>
  </div>
</asp:Content>
