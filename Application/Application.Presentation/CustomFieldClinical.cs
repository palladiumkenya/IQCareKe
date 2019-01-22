using Interface.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;



//using Extendedtextbox; 

namespace Application.Presentation
{
    public class CustomFieldClinical
    {
        #region Constructor

        public CustomFieldClinical()
        {
        }

        #endregion


        

        # region "Create Custom Fields"

        public void CreateCustomControlsForms(Panel thePnl, DataSet theControlDS, string theFrmPrefix)
        {
            string pnlName = "PnlCustomList";
            int theCol = 0;
            thePnl.Controls.Add(new LiteralControl("<table border='0' cellpadding=6 cellspacing=1 width=100%>")); 
            thePnl.Controls.Add(new LiteralControl("<tbody>"));
            if (theControlDS.Tables[0].Rows.Count < 1)
                return;

            foreach (DataRow theDR in theControlDS.Tables[0].Rows)
            {
                if (theCol == 0)
                    thePnl.Controls.Add(new LiteralControl("<tr >"));

                /// TextBox SingleLine ///
                if (theDR["ControlID"].ToString() == "1")
                {
                    thePnl.Controls.Add(new LiteralControl("<td class='border whitebg'>"));
                    Label theLabel = new Label();
                    theLabel.ID = pnlName + "Lbl" + (theDR[0].ToString().Replace("_"," ")).Replace(theFrmPrefix,"");
                    theLabel.Text = (theDR[0].ToString().Replace("_", " ")).Replace(theFrmPrefix, "").ToString();
                    theLabel.Width = 200;
                    thePnl.Controls.Add(theLabel);
                    thePnl.Controls.Add(new LiteralControl("&nbsp;"));

                    TextBox theSingleText = new TextBox();
                    theSingleText.ID = pnlName + "TXT" + theDR[0].ToString().Replace("_"," ");
                    theSingleText.Width = 200;
                    theSingleText.MaxLength = 50;
                    theLabel.CssClass = "labelright";
                    theLabel.Font.Bold = true;
                    thePnl.Controls.Add(theSingleText);
                    thePnl.Controls.Add(new LiteralControl("</td >"));
                }
                else if (theDR["ControlId"].ToString() == "3")   /// Numeric
                {
                    thePnl.Controls.Add(new LiteralControl("<td class='border whitebg'align='left'>"));
                    Label theLabel = new Label();
                    theLabel.ID = pnlName + "Lbl" + (theDR[0].ToString().Replace("_"," ")).Replace(theFrmPrefix,"");
                    theLabel.Text = (theDR[0].ToString().Replace("_", " ")).Replace(theFrmPrefix, "").ToString();
                    theLabel.Width = 200;
                    theLabel.CssClass = "labelright";
                    theLabel.Font.Bold = true;
                    thePnl.Controls.Add(theLabel);
                    thePnl.Controls.Add(new LiteralControl("&nbsp;"));

                    TextBox theNumberText = new TextBox();
                    theNumberText.ID = pnlName + "NUM" + theDR[0].ToString().Replace("_"," ");
                    theNumberText.Width = 100;
                    if(theDR["MaxControl"]!= DBNull.Value)
                        theNumberText.MaxLength = Convert.ToInt32(theDR["MaxControl"]);
                    if(theDR["Unit"]!= DBNull.Value)

                            theNumberText.MaxLength = theNumberText.MaxLength + Convert.ToInt32(theDR["Unit"].ToString().Length) + 1;
                            Control ctl = (TextBox)theNumberText;
                            if (Convert.ToInt32(theDR["Unit"].ToString().Length) < 1)
                                theNumberText.Attributes.Add("onkeyup", "chkInteger('ctl00_IQCareContentPlaceHolder_" + ((TextBox)ctl).ClientID + "')");
                            else
                                theNumberText.Attributes.Add("onkeyup", "chkDecimal('ctl00_IQCareContentPlaceHolder_" + ((TextBox)ctl).ClientID + "')");
                        

                    thePnl.Controls.Add(theNumberText);
                    thePnl.Controls.Add(new LiteralControl("</td >"));
                }
                else if (theDR["ControlId"].ToString() == "4") /// Dropdown
                {
                    thePnl.Controls.Add(new LiteralControl("<td class='border whitebg' align='left'>"));
                    Label theLabel = new Label();
                    theLabel.ID = pnlName + "Lbl" + (theDR[0].ToString().Replace("_", " ")).Replace(theFrmPrefix,"");
                    theLabel.Text = (theDR[0].ToString().Replace("_", " ")).Replace(theFrmPrefix, "").ToString();
                    theLabel.Width = 200;
                    theLabel.CssClass = "labelright";
                    theLabel.Font.Bold = true;
                    thePnl.Controls.Add(theLabel);
                    thePnl.Controls.Add(new LiteralControl("&nbsp;"));

                    DropDownList ddlSelectList = new DropDownList();
                    ddlSelectList.ID = pnlName + "SELECTLIST" + theDR[0].ToString().Replace("_", " ");
                    ddlSelectList.Width = 180;
                    BindFunctions BindManager = new BindFunctions();
                    IQCareUtils theUtils = new IQCareUtils();
                    DataView theDV = new DataView(theControlDS.Tables[1]);
                    theDV.RowFilter = "MstCodeId = " + theDR["mstCodeId"].ToString();
                    DataTable theDT = theUtils.CreateTableFromDataView(theDV);
                    ddlSelectList.DataSource = theDT;
                    BindManager.BindCombo(ddlSelectList, theDT, "Name", "ID");
                    thePnl.Controls.Add(ddlSelectList);
                    thePnl.Controls.Add(new LiteralControl("</td >"));
                }
                else if (theDR["ControlId"].ToString() =="5") ///Date
                {
                    thePnl.Controls.Add(new LiteralControl("<td class='border whitebg' align='left'>"));
                    Label theLabel = new Label();
                    theLabel.ID = pnlName + "Lbl" + (theDR[0].ToString().Replace("_", " ")).Replace(theFrmPrefix, "");
                    theLabel.Text = (theDR[0].ToString().Replace("_", " ")).Replace(theFrmPrefix, "").ToString();
                    theLabel.Width = 200;
                    theLabel.CssClass = "labelright";
                    theLabel.Font.Bold = true;
                    thePnl.Controls.Add(theLabel);
                    thePnl.Controls.Add(new LiteralControl("&nbsp;"));

                    TextBox theDateText = new TextBox();
                    theDateText.ID = pnlName + "DT" + theDR[0].ToString().Replace("_", " ");
                    Control ctl = (TextBox)theDateText;
                    theDateText.Width = 70;
                    theDateText.MaxLength = 11;


                    

                    string s = HttpContext.Current.Session["AppCurrentDateClass"].ToString();

                    theDateText.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
               //     theDateText.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");
                    //theDateText.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3'); isCheckValidDate('" + HttpContext.Current.Session["AppCurrentDateClass"] + "', '" + theDateText.ClientID + "', '" + theDateText.ClientID + "');");
                    theDateText.Attributes.Add("OnBlur", "DateFormat(this, this.value,event,true,'3');");

                    thePnl.Controls.Add(theDateText);

                    thePnl.Controls.Add(new LiteralControl("&nbsp;"));

                    Image theDateImage = new Image();
                    theDateImage.ID = pnlName + "img" + theDR[0].ToString().Replace("_", " ");
                    theDateImage.Height = 22;
                    theDateImage.Width = 22;
                    theDateImage.ToolTip = "Date Helper";
                    theDateImage.ImageUrl = "~/images/cal_icon.gif";
                    theDateImage.Attributes.Add("onClick", "w_displayDatePicker('" + ((TextBox)ctl).ClientID + "');");
                    thePnl.Controls.Add(theDateImage);

                    Label theformatlbl = new Label();
                    theformatlbl.ID = pnlName + "lblfmt" + (theDR[0].ToString().Replace("_", " ")).Replace(theFrmPrefix, "");
                    theformatlbl.Text = " (DD-MMM-YYYY)";
                    theformatlbl.CssClass = "smallerlabel";
                    thePnl.Controls.Add(theformatlbl);
                    thePnl.Controls.Add(new LiteralControl("</td >"));
                }
                else if (theDR["ControlId"].ToString() == "6")  /// Radio Button
                {
                    thePnl.Controls.Add(new LiteralControl("<td class='border whitebg' align='left' nowrap='noWrap'>"));
                    Label theLabel = new Label();
                    theLabel.ID = pnlName + "Lbl" + (theDR[0].ToString().Replace("_", " ")).Replace(theFrmPrefix, "");
                    theLabel.Text = (theDR[0].ToString().Replace("_", " ")).Replace(theFrmPrefix, "").ToString();
                    theLabel.Width = 200;
                    theLabel.CssClass = "labelright";
                    theLabel.Font.Bold = true;
                    thePnl.Controls.Add(theLabel);
                    thePnl.Controls.Add(new LiteralControl("&nbsp;"));

                    HtmlInputRadioButton theYesNoRadio1 = new HtmlInputRadioButton();
                    theYesNoRadio1.ID = pnlName + "RADIO1" + (theDR[0].ToString().Replace("_", " ")); 
                    theYesNoRadio1.Value = "Yes";
                    theYesNoRadio1.Name = "Radio" + theDR[0].ToString().Replace("_", " ");
                    theYesNoRadio1.Attributes.Add("onclick", "down(this)");
                    theYesNoRadio1.Attributes.Add("onfocus", "up(this)");
                    thePnl.Controls.Add(theYesNoRadio1);

                    Label theYeslbl = new Label();
                    theYeslbl.ID = pnlName + "lblYes" + (theDR[0].ToString().Replace("_", " ")).Replace(theFrmPrefix, "");
                    theYeslbl.Text = "Yes";
                    theYeslbl.Width = 20;
                    theYeslbl.CssClass = "labelright";
                    theYeslbl.Font.Bold = true;
                    thePnl.Controls.Add(theYeslbl);

                    HtmlInputRadioButton theYesNoRadio2 = new HtmlInputRadioButton();
                    theYesNoRadio2.ID = pnlName + "RADIO2" + (theDR[0].ToString().Replace("_", " ")); 
                    theYesNoRadio2.Value = "No";
                    theYesNoRadio2.Name = "Radio" + theDR[0].ToString().Replace("_", " ");
                    theYesNoRadio2.Attributes.Add("onclick", "down(this)");
                    theYesNoRadio2.Attributes.Add("onfocus", "up(this)"); 
                    thePnl.Controls.Add(theYesNoRadio2);

                    Label theNolbl = new Label();
                    theNolbl.ID = pnlName + "lblNo" + (theDR[0].ToString().Replace("_", " ")).Replace(theFrmPrefix, "");
                    theNolbl.Text = "No";
                    theNolbl.Width = 10;
                    theNolbl.CssClass = "labelright";
                    theNolbl.Font.Bold = true;
                    thePnl.Controls.Add(theNolbl);
                    thePnl.Controls.Add(new LiteralControl("</td >"));
                }
                else if (theDR["ControlId"].ToString() == "8")  /// MultiLine TextBox
                {
                    if (theCol == 1)
                    {
                        thePnl.Controls.Add(new LiteralControl("</td>"));
                        thePnl.Controls.Add(new LiteralControl("<td></td>"));
                        thePnl.Controls.Add(new LiteralControl("</tr>"));
                        thePnl.Controls.Add(new LiteralControl("<tr>"));
                    }
                    
                    {
                        thePnl.Controls.Add(new LiteralControl("<td class='border whitebg' align='left' colspan='2'>"));
                        Label theLabel = new Label();
                        theLabel.ID = pnlName + "Lbl" + (theDR[0].ToString().Replace("_", " ")).Replace(theFrmPrefix, "");
                        theLabel.Text = (theDR[0].ToString().Replace("_", " ")).Replace(theFrmPrefix, "").ToString();
                        theLabel.Width = 200;
                        theLabel.CssClass = "labelright";
                        theLabel.Font.Bold = true;
                        thePnl.Controls.Add(theLabel);
                        thePnl.Controls.Add(new LiteralControl("&nbsp;"));

                        TextBox theMultiText = new TextBox();
                        theMultiText.ID = pnlName + "TXT" + theDR[0].ToString().Replace("_", " ");
                        theMultiText.Width = 200;
                        theMultiText.TextMode = TextBoxMode.MultiLine;
                        theMultiText.MaxLength = 250;
                        theMultiText.Columns = 60;
                        theMultiText.Rows = 4;
                        thePnl.Controls.Add(theMultiText);
                        thePnl.Controls.Add(new LiteralControl("</td>"));
                        theCol = 1;
                    }
                   
                    

                }
                else if (theDR["ControlId"].ToString() == "9") ///  MultiSelect List 
                {
                    thePnl.Controls.Add(new LiteralControl("<td class='border whitebg' align='left'>"));
                    Label theLabel = new Label();
                    theLabel.ID = pnlName + "Lbl" + (theDR[0].ToString().Replace("_", " ")).Replace(theFrmPrefix, "");
                    theLabel.Text = (theDR[0].ToString().Replace("_", " ")).Replace(theFrmPrefix, "").ToString();
                    theLabel.Width = 200;
                    theLabel.CssClass = "labelright";
                    theLabel.Font.Bold = true;
                    thePnl.Controls.Add(theLabel);
                    thePnl.Controls.Add(new LiteralControl("&nbsp;"));

                    thePnl.Controls.Add(new LiteralControl("<div class = 'Customdivborder' nowrap='nowrap'>"));
                    CheckBoxList chkMultiList = new CheckBoxList();
                    chkMultiList.ID = pnlName + "MULTISELECTLIST" + theDR[0].ToString().Replace("_", " ");
                    chkMultiList.RepeatLayout = RepeatLayout.Flow;
                    chkMultiList.CssClass = "check";
                    chkMultiList.Width = 300;
                    BindFunctions BindManager = new BindFunctions();
                    IQCareUtils theUtils = new IQCareUtils();
                    DataView theDV = new DataView(theControlDS.Tables[1]);
                    theDV.RowFilter = "MstCodeId = " + theDR["mstCodeId"].ToString();
                    DataTable theDT = theUtils.CreateTableFromDataView(theDV);
                    chkMultiList.DataSource = theDT;
                    BindManager.BindCheckedList(chkMultiList, theDT, "Name", "ID");
                    thePnl.Controls.Add(chkMultiList);
                    thePnl.Controls.Add(new LiteralControl("</div>"));
                    thePnl.Controls.Add(new LiteralControl("</td >"));
                }

                theCol = theCol + 1;
                if (theCol == 2)
                {
                    thePnl.Controls.Add(new LiteralControl("</tr>"));
                    theCol = 0;
                }
            }
            if(theCol <2)
                thePnl.Controls.Add(new LiteralControl("</tr>"));

            thePnl.Controls.Add(new LiteralControl("</tbody>"));
            thePnl.Controls.Add(new LiteralControl("</table>"));

        }

        public DataTable GenerateInsertUpdateStatement(Control theControl, string theInsertUpdate, Int32 theFeatureId, DataSet CustomFieldsDS)
        {
            #region "Local Variables"
            string pnlName = theControl.ID;
            StringBuilder sbParameters = new StringBuilder();
            StringBuilder sbValues = new StringBuilder();
            string strmultiselect = string.Empty;
            string strfName = string.Empty;
            DataTable theDT = new DataTable();
            string thePreFields = "";
            string theMultiPreFields = "";
            string thePreValues = "";
            string theMultiPreValues = "";
            string theWhereClause = "";
            string theYesNoVariable = "";
            int stpos, enpos;
            string theTblName = "";
            #endregion

            theDT.Columns.Add("Id", System.Type.GetType("System.Int32"));
            theDT.Columns.Add("Query",System.Type.GetType("System.String"));
            theDT.Columns[0].AutoIncrement = true;

            if(CustomFieldsDS.Tables[0].Rows.Count>0)
                theTblName = CustomFieldsDS.Tables[0].Rows[0]["FeatureName"].ToString().Replace(" ", "_");
            if (theTblName.ToUpper() == "PAEDIATRIC_PHARMACY")
                theTblName = "Pharmacy";

            foreach (Control x in theControl.Controls)
            {
                /////Region of Identifing predefined fields for tables/////
                #region "SetPreFields"
                ////////////PreSet Values Used/////////////////
                /// #99# --- Ptn_Pk
                /// #88# --- LocationId
                /// #77# --- Visit_Pk
                /// #66# --- Visit_Date
                /// #55# --- Ptn_Pharmacy_Pk
                /// #44# --- OrderedByDate
                /// #33# --- LabId
                /// #22# --- TrackingId
                /// #11# --- CareEndedId
                /// #00# --- HomeVisitId
                ///////////////////////////////////////////////

                if (theFeatureId == 1 || theFeatureId == 2 || theFeatureId == 6 || theFeatureId == 7 || theFeatureId == 56 || theFeatureId == 73 || theFeatureId == 117 || theFeatureId == 163 || theFeatureId == 164 || theFeatureId == 167 || theFeatureId == 168 || theFeatureId == 169) 
                {
                    theMultiPreFields = "Ptn_Pk,LocationId,Visit_pk,Visit_Date";
                    theMultiPreValues = "#99#,#88#,#77#,#66#";
                    if (theInsertUpdate == "Insert")
                    {
                        thePreFields = "Ptn_Pk,LocationId,Visit_pk,Visit_Date";
                        thePreValues = "#99#,#88#,#77#,#66#";
                    }
                    else
                    {
                        thePreFields = "[Visit_Date]=#66#";
                        theWhereClause = "[Visit_Pk] = #77# and [Ptn_Pk]=#99#";
                    }
                }
                else if (theFeatureId == 3 || theFeatureId == 4 || theFeatureId == 72)
                {
                    theMultiPreFields = "Ptn_Pk,LocationId,Ptn_Pharmacy_pk,OrderedbyDate";
                    theMultiPreValues = "#99#,#88#,#55#,#44#";
                    if (theInsertUpdate == "Insert")
                    {
                        thePreFields = "Ptn_Pk,LocationId,Ptn_Pharmacy_pk,OrderedbyDate";
                        thePreValues = "#99#,#88#,#55#,#44#";
                    }
                    else
                    {
                        thePreFields = "[OrderedbyDate]=#44#";
                        theWhereClause = "[Ptn_Pharmacy_pk] = #55# and [Ptn_Pk]=#99#";
                    }
                }
                else if (theFeatureId == 5)
                {
                    theMultiPreFields = "Ptn_Pk,LocationId,LabId,OrderedbyDate";
                    theMultiPreValues = "#99#,#88#,#33#,#44#";
                    if (theInsertUpdate == "Insert")
                    {
                        thePreFields = "Ptn_Pk,LocationId,LabId,OrderedbyDate";
                        thePreValues = "#99#,#88#,#33#,#44#";
                    }
                    else
                    {
                        thePreFields = "[OrderedbyDate]=#44#";
                        theWhereClause = "[LabId] = #33# and [Ptn_Pk]=#99#";
                    }
                }
                else if (theFeatureId == 8)
                {
                    theMultiPreFields = "Ptn_Pk,LocationId,TrackingId,CareEndedId";
                    theMultiPreValues = "#99#,#88#,#22#,#11#";
                    if (theInsertUpdate == "Insert")
                    {
                        thePreFields = "Ptn_Pk,LocationId,TrackingId,CareEndedId";
                        thePreValues = "#99#,#88#,#22#,#11#";
                    }
                    else
                    {
                        thePreFields = "";
                        theWhereClause = "[TrackingId] = #22# and [CareEndedId]=#11# and [Ptn_Pk]=#99#";                    }
                }
                else if (theFeatureId == 9)
                {
                    theMultiPreFields = "Ptn_Pk,LocationId,HomeVisitId";
                    theMultiPreValues = "#99#,#88#,#00#";
                    if (theInsertUpdate == "Insert")
                    {
                        thePreFields = "Ptn_Pk,LocationId,HomeVisitId";
                        thePreValues = "#99#,#88#,#00#";
                    }
                    else
                    {
                        thePreFields = "";
                        theWhereClause = "[HomeVisitId] = #00# and [Ptn_Pk]=#99#"; 
                    }
                }

                #endregion
                //////////////////////////////////////////////////////////

                if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                {
                    if (x.ID.Substring(0, 16).ToString().ToUpper() == pnlName.ToUpper() + "TXT")
                    {
                        strfName = pnlName.ToUpper() + "TXT";
                        stpos = strfName.Length;
                        enpos = x.ID.Length - stpos;
                        strfName = (x.ID.Substring(stpos, enpos).ToString()).Replace(" ","_");
                        if (theInsertUpdate =="Insert")
                        {
                            if(sbParameters.Length==0)
                                sbParameters.Append("["+strfName+"]");
                            else
                                sbParameters.Append(",["+strfName+"]");
                            if(sbValues.Length ==0)
                                sbValues.Append("'"+((TextBox)x).Text.Trim().ToString()+"'");
                            else
                                sbValues.Append(",'" + ((TextBox)x).Text.Trim().ToString() + "'");
                        }
                        else
                        {
                            if(sbValues.Length ==0)
                                sbValues.Append("["+strfName+"] = '"+((TextBox)x).Text.Trim().ToString()+"'");
                            else
                                sbValues.Append(",["+strfName+"] = '"+((TextBox)x).Text.Trim().ToString()+"'");
                        }
                    }
                    else if (x.ID.Substring(0, 16).ToString().ToUpper() == pnlName.ToUpper() + "NUM")
                    {
                        strfName = pnlName.ToUpper() + "NUM";
                        stpos = strfName.Length;
                        enpos = x.ID.Length - stpos;
                        strfName = (x.ID.Substring(stpos, enpos).ToString()).Replace(" ", "_");
                        string theNum = "0";
                        if ((((TextBox)x).Text.Trim()).ToString()!="")
                            theNum = ((TextBox)x).Text;
                        if (theInsertUpdate == "Insert")
                        {
                            if(sbParameters.Length==0)
                                sbParameters.Append("["+strfName+"]");
                            else
                                sbParameters.Append(",["+strfName+"]");
                            if(sbValues.Length ==0)
                                sbValues.Append(theNum);
                            else
                                sbValues.Append(","+theNum);
                        }
                        else
                        {
                            if(sbValues.Length ==0)
                                sbValues.Append("[" + strfName + "] = " + theNum);
                            else
                                sbValues.Append(",[" + strfName + "] = " + theNum);
                        }

                    }
                    else if (x.ID.Substring(0, 15).ToString().ToUpper() == pnlName.ToUpper() + "DT")
                    {
                        strfName = pnlName.ToUpper() + "DT";
                        stpos = strfName.Length;
                        enpos = x.ID.Length - stpos;
                        strfName = (x.ID.Substring(stpos, enpos).ToString()).Replace(" ", "_");
                        string theDate = "";
                        if (((TextBox)x).Text.Trim() == "")
                            theDate = "01-01-1900";
                        else
                            theDate = Convert.ToDateTime(((TextBox)x).Text).ToString();

                        if (theInsertUpdate =="Insert")
                        {
                            if(sbParameters.Length==0)
                                sbParameters.Append("["+strfName+"]");
                            else
                                sbParameters.Append(",["+strfName+"]");
                            if(sbValues.Length ==0)
                                sbValues.Append("'"+ theDate+"'");
                            else
                                sbValues.Append(",'" + theDate + "'");
                        }
                        else
                        {
                            if(sbValues.Length ==0)
                                sbValues.Append("["+strfName+"] = '"+ theDate+"'");
                            else
                                sbValues.Append(",["+strfName+"] = '"+ theDate+"'");
                        }
                    }
                }
                if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputRadioButton))
                {
                    if (x.ID.Substring(0, 19).ToString().ToUpper() == pnlName.ToUpper() + "RADIO1")
                    {
                        strfName = pnlName.ToUpper() + "RADIO1";
                        stpos = strfName.Length;
                        enpos = x.ID.Length - stpos;
                        strfName = (x.ID.Substring(stpos, enpos).ToString()).Replace(" ", "_");
                        theYesNoVariable = strfName;
                        if (((HtmlInputRadioButton)x).Checked == true)
                        {
                            if (theInsertUpdate == "Insert")
                            {
                                if (sbParameters.Length == 0)
                                    sbParameters.Append("[" + strfName + "]");
                                else
                                    sbParameters.Append(",[" + strfName + "]");
                                if (sbValues.Length == 0)
                                    sbValues.Append("1");
                                else
                                    sbValues.Append(",1");
                            }
                            else
                            {
                                if (sbValues.Length == 0)
                                    sbValues.Append("[" + strfName + "] = 1");
                                else
                                    sbValues.Append(",[" + strfName + "] = 1");
                            }
                        }
                    }
                    if (x.ID.Substring(0, 19).ToString().ToUpper() == pnlName.ToUpper() + "RADIO2")
                    {
                        strfName = pnlName.ToUpper() + "RADIO2";
                        stpos = strfName.Length;
                        enpos = x.ID.Length - stpos;
                        strfName = (x.ID.Substring(stpos, enpos).ToString()).Replace(" ", "_");
                        theYesNoVariable = strfName;
                        if (((HtmlInputRadioButton)x).Checked == true)
                        {
                            if (theInsertUpdate == "Insert")
                            {
                                if (sbParameters.Length == 0)
                                    sbParameters.Append("[" + strfName + "]");
                                else
                                    sbParameters.Append(",[" + strfName + "]");
                                if (sbValues.Length == 0)
                                    sbValues.Append("0");
                                else
                                    sbValues.Append(",0");
                            }
                            else
                            {
                                if (sbValues.Length == 0)
                                    sbValues.Append("[" + strfName + "] = 0");
                                else
                                    sbValues.Append(",[" + strfName + "] = 0");
                            }
                        }
                    }
                }
                if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                {
                    if (x.ID.Substring(0, 23).ToString().ToUpper() == pnlName.ToUpper() + "SELECTLIST")
                    {
                        strfName = pnlName.ToUpper() + "SELECTLIST";
                        stpos = strfName.Length;
                        enpos = x.ID.Length - stpos;
                        strfName = (x.ID.Substring(stpos, enpos).ToString()).Replace(" ", "_");
                        if (theInsertUpdate == "Insert")
                        {
                            if(sbParameters.Length==0)
                                sbParameters.Append("["+strfName+"]");
                            else
                                sbParameters.Append(",["+strfName+"]");
                            if(sbValues.Length ==0)
                                sbValues.Append(((DropDownList)x).SelectedValue.ToString());
                            else
                                sbValues.Append("," + ((DropDownList)x).SelectedValue.ToString());
                        }
                        else
                        {
                            if(sbValues.Length ==0)
                                sbValues.Append("["+strfName+"] = "+ ((DropDownList)x).SelectedValue.ToString());
                            else
                                sbValues.Append(",["+strfName+"] = "+ ((DropDownList)x).SelectedValue.ToString());
                        }
                    }
                }
                if (x.GetType() == typeof(System.Web.UI.WebControls.CheckBoxList))
                {
                    if (x.ID.Substring(0, 28).ToString().ToUpper() == pnlName.ToUpper() + "MULTISELECTLIST")
                    {
                        strfName = pnlName.ToUpper() + "MULTISELECTLIST";
                        stpos = strfName.Length;
                        enpos = x.ID.Length - stpos;
                        strfName = (x.ID.Substring(stpos, enpos).ToString()).Replace(" ", "_");

                        DataView theDV = new DataView(CustomFieldsDS.Tables[0]);
                        theDV.RowFilter = "Label = '" + strfName.ToString() + "'";
                        string theMuliTblName = theTblName + "_" + theDV[0]["Label"].ToString().Replace(" ", "_");
                        theDV.Dispose();

                        if (theInsertUpdate == "Update")
                        {
                            #region "CheckValuesinTable"
                            theDV = new DataView(theDT);
                            theDV.RowFilter = "Query = 'Delete from dtl_CustomField_" + theMuliTblName + " where " + theWhereClause + "'";
                            if (theDV.Count < 1)
                            {
                                DataRow theDR = theDT.NewRow();
                                theDR[1] = "Delete from dtl_CustomField_" + theMuliTblName + " where " + theWhereClause;
                                theDT.Rows.Add(theDR);
                            }
                            #endregion
                        }
                        foreach (ListItem li in ((CheckBoxList)x).Items)
                        {
                            if (Convert.ToInt32(li.Selected) == 1)
                            {

                                strmultiselect = "Insert into dtl_CustomField_" + theMuliTblName + "(" + theMultiPreFields + ",[" + strfName + "]) Values(" + theMultiPreValues + "," + li.Value.ToString() + ")";
                                DataRow theDRow = theDT.NewRow();
                                theDRow[1] = strmultiselect;
                                theDT.Rows.Add(theDRow);

                            }
                        }
                    }
                }
 
            }
            string theFinalQuery = "";
            if (theInsertUpdate == "Insert" && sbParameters.ToString().Contains(theYesNoVariable) == false)
            {
                if (sbParameters.Length != 0)
                    sbParameters = sbParameters.Append(",[" + theYesNoVariable + "]");
                else
                    sbParameters = sbParameters.Append("[" + theYesNoVariable + "]");
                if (sbValues.Length != 0)
                    sbValues = sbValues.Append(",null");
                else
                    sbValues = sbValues.Append("null");
            }
            if (theInsertUpdate != "Insert" && sbValues.ToString().Contains(theYesNoVariable) == false)
            {
                if (sbValues.Length != 0)
                    sbValues = sbValues.Append(",["+theYesNoVariable+"]=null");
                else
                    sbValues = sbValues.Append("[" + theYesNoVariable + "]=null");
            }

            if (theInsertUpdate == "Insert")
            {
                if (theTblName != "" && sbParameters.Length > 0)
                    theFinalQuery = "Insert into dtl_CustomField_" + theTblName + "(" + thePreFields + "," + sbParameters + ") Values(" + thePreValues + "," + (sbValues.Replace("''", "null")).Replace("'01-01-1900'", "null") + ")";
            }
            else
            {
                #region "InsertinCaseofNonExistance"
                string[] sbPram;
                sbPram = sbValues.ToString().Split('=', ',');
                string theParams = "";
                string theValues = "";
                for (int i = 0; i < sbPram.Length; i++)
                {
                    if (sbPram[i].ToString().Contains("["))
                    {
                        if (theParams == "")
                            theParams = sbPram[i].ToString();
                        else
                            theParams = theParams + "," + sbPram[i].ToString();
                    }
                    else
                    {
                        if (theValues == "")
                            theValues = sbPram[i].ToString();
                        else
                            theValues = theValues + "," + sbPram[i].ToString();
                    }
                }
                string theNewPreFields = "";
                string theNewPreValues = "";
                if (theFeatureId == 1 || theFeatureId == 2 || theFeatureId == 6 || theFeatureId == 7 || theFeatureId == 56 || theFeatureId == 73 || theFeatureId == 117 || theFeatureId == 163 || theFeatureId == 164 || theFeatureId == 167 || theFeatureId == 168 || theFeatureId == 169)
                {
                    theNewPreFields = "Ptn_Pk,LocationId,Visit_pk,Visit_Date";
                    theNewPreValues = "#99#,#88#,#77#,#66#";
                }
                else if (theFeatureId == 3 || theFeatureId == 4 || theFeatureId == 72)
                {
                    theNewPreFields = "Ptn_Pk,LocationId,Ptn_Pharmacy_pk,OrderedbyDate";
                    theNewPreValues = "#99#,#88#,#55#,#44#";
                }
                else if (theFeatureId == 5)
                {
                    theNewPreFields = "Ptn_Pk,LocationId,LabId,OrderedbyDate";
                    theNewPreValues = "#99#,#88#,#33#,#44#";
                }
                else if (theFeatureId == 8)
                {
                    theNewPreFields = "Ptn_Pk,LocationId,TrackingId,CareEndedId";
                    theNewPreValues = "#99#,#88#,#22#,#11#";
                }
                else if (theFeatureId == 9)
                {
                    theNewPreFields = "Ptn_Pk,LocationId,HomeVisitId";
                    theNewPreValues = "#99#,#88#,#00#";
                }
                #endregion

                if (theTblName != "" && sbValues.Length > 0)
                {
                    //Ajay Khadwal
                    //theFinalQuery = "If exists(Select Ptn_Pk from dtl_CustomField_" + theTblName + " where " + theWhereClause + ") begin Update dtl_CustomField_"
                    //    + theTblName + " Set " + thePreFields + "," + sbValues.Replace("''", "null") + " where " + theWhereClause + " end else begin Insert into dtl_CustomField_"
                    //    + theTblName + "(" + theNewPreFields + "," + theParams + ") Values(" + theNewPreValues + "," + (theValues.Replace("''", "null")).Replace("'01-01-1900'", "null") + ") end";
        
                    theFinalQuery = "If exists(Select Ptn_Pk from dtl_CustomField_" + theTblName + " where " + theWhereClause + ") begin Update dtl_CustomField_"
                + theTblName + " Set " + (sbValues.Replace("''", "null")).Replace("'01-01-1900'", "null") + " where " + theWhereClause + " end else begin Insert into dtl_CustomField_"
                + theTblName + "(" + theNewPreFields + "," + theParams + ") Values(" + theNewPreValues + "," + (theValues.Replace("''", "null")).Replace("'01-01-1900'", "null") + ") end";
        
                }
            }
            DataRow theDR1 = theDT.NewRow();
            theDR1[1] = theFinalQuery;
            theDT.Rows.Add(theDR1);
            return theDT;
        }

        public void FillCustomFieldData(DataSet theControlDS, DataSet theOldData, Control theControl, string theFrmPrefix)
        {
            #region "Local Variables"
            string pnlName = theControl.ID;
            StringBuilder sbParameters = new StringBuilder();
            StringBuilder sbValues = new StringBuilder();
            string strmultiselect = string.Empty;
            string strfName = string.Empty;
            DataTable theDT = new DataTable();
            int stpos, enpos;
           // string theTblName = "";
            #endregion

            if (theOldData.Tables.Count < 1)
                return;

            foreach (Control x in theControl.Controls)
            {
                if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                {
                    if (theOldData.Tables[0].Rows.Count > 0)
                    {
                        if (x.ID.Substring(0, 16).ToString().ToUpper() == pnlName.ToUpper() + "TXT")
                        {
                            strfName = pnlName.ToUpper() + "TXT";
                            stpos = strfName.Length;
                            enpos = x.ID.Length - stpos;
                            strfName = (x.ID.Substring(stpos, enpos).ToString()).Replace(" ", "_");
                            ((TextBox)x).Text = theOldData.Tables[0].Rows[0][strfName].ToString();
                            
                        }
                        else if (x.ID.Substring(0, 16).ToString().ToUpper() == pnlName.ToUpper() + "NUM")
                        {
                            strfName = pnlName.ToUpper() + "NUM";
                            stpos = strfName.Length;
                            enpos = x.ID.Length - stpos;
                            strfName = (x.ID.Substring(stpos, enpos).ToString()).Replace(" ", "_");
                            ((TextBox)x).Text = theOldData.Tables[0].Rows[0][strfName].ToString();
                        }
                        else if (x.ID.Substring(0, 15).ToString().ToUpper() == pnlName.ToUpper() + "DT")
                        {
                            strfName = pnlName.ToUpper() + "DT";
                            stpos = strfName.Length;
                            enpos = x.ID.Length - stpos;
                            strfName = (x.ID.Substring(stpos, enpos).ToString()).Replace(" ", "_");
                            if (theOldData.Tables[0].Rows[0].IsNull(strfName) == false)
                            {
                                if (Convert.ToDateTime(theOldData.Tables[0].Rows[0][strfName]).ToString("dd-MMM-yyyy") != "01-Jan-1900")
                                    ((TextBox)x).Text = Convert.ToDateTime(theOldData.Tables[0].Rows[0][strfName]).ToString("dd-MMM-yyyy");
                            }
                        }
                    }
                }
                if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputRadioButton))
                {
                    if (theOldData.Tables[0].Rows.Count > 0)
                    {
                        if (x.ID.Substring(0, 19).ToString().ToUpper() == pnlName.ToUpper() + "RADIO1")
                        {
                            strfName = pnlName.ToUpper() + "RADIO1";
                            stpos = strfName.Length;
                            enpos = x.ID.Length - stpos;
                            strfName = (x.ID.Substring(stpos, enpos).ToString()).Replace(" ", "_");
                         
                            if (theOldData.Tables[0].Rows[0][strfName].ToString().ToUpper() == "TRUE")
                                ((HtmlInputRadioButton)x).Checked = true;

                        }
                        if (x.ID.Substring(0, 19).ToString().ToUpper() == pnlName.ToUpper() + "RADIO2")
                        {
                            strfName = pnlName.ToUpper() + "RADIO2";
                            stpos = strfName.Length;
                            enpos = x.ID.Length - stpos;
                            strfName = (x.ID.Substring(stpos, enpos).ToString()).Replace(" ", "_");
                             if (theOldData.Tables[0].Rows[0][strfName].ToString().ToUpper() == "FALSE")
                                ((HtmlInputRadioButton)x).Checked = true;
                        }
                    }
                }
                if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                {
                    if (theOldData.Tables[0].Rows.Count > 0)
                    {
                        if (x.ID.Substring(0, 23).ToString().ToUpper() == pnlName.ToUpper() + "SELECTLIST")
                        {
                            strfName = pnlName.ToUpper() + "SELECTLIST";
                            stpos = strfName.Length;
                            enpos = x.ID.Length - stpos;
                            strfName = (x.ID.Substring(stpos, enpos).ToString()).Replace(" ", "_");
                            ((DropDownList)x).SelectedValue = theOldData.Tables[0].Rows[0][strfName].ToString();
                        }
                    }
                }
                if (x.GetType() == typeof(System.Web.UI.WebControls.CheckBoxList))
                {
                    if (theOldData.Tables[0].Rows.Count > 0)
                    {
                        if (x.ID.Substring(0, 28).ToString().ToUpper() == pnlName.ToUpper() + "MULTISELECTLIST")
                        {
                            strfName = pnlName.ToUpper() + "MULTISELECTLIST";
                            stpos = strfName.Length;
                            enpos = x.ID.Length - stpos;
                            strfName = (x.ID.Substring(stpos, enpos).ToString()).Replace(" ", "_");
                            DataView theDV = new DataView(theOldData.Tables[1]);
                            theDV.RowFilter = "Label = '" + strfName + "'";
                            if (theDV.Count > 0)
                            {
                                IQCareUtils theUtil = new IQCareUtils();
                                DataTable theMultiDT = theUtil.CreateTableFromDataView(theDV);
                                foreach (DataRow theDR in theMultiDT.Rows)
                                {
                                    foreach (ListItem li in ((CheckBoxList)x).Items)
                                    {
                                        if (li.Value == theDR["Id"].ToString())
                                        {
                                            li.Selected = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

            }

        }

        public static void BindUserDropDown(DropDownList theDropDown, out Dictionary<int, string> uList)
        {
            Dictionary<int, string> userList = GetUserList();
            uList = GetUserList();
            if (userList.Count > 0)
            {
                theDropDown.DataSource = userList;
                theDropDown.DataTextField = "Value";
                theDropDown.DataValueField = "Key";
                theDropDown.DataBind();
            }
            theDropDown.Items.Insert(0, new ListItem("Select", "0")); //Add this line by Rahmat on 20-Mar-2017 for by default show select in drop down
        }
        public static void BindUserDropDown(Telerik.Web.UI.RadComboBox theDropDown, out Dictionary<int, string> uList)
        {
            Dictionary<int, string> userList = GetUserList();
            uList = GetUserList();
            if (userList.Count > 0)
            {
                theDropDown.DataSource = userList;
                theDropDown.DataTextField = "Value";
                theDropDown.DataValueField = "Key";
                theDropDown.DataBind();
            }
        }

        public static Dictionary<int, string> GetUsers()
        {
            
                Dictionary<int, string> userList = new Dictionary<int, string>();
                ICommonData commonData = (ICommonData)ObjectFactory.CreateInstance("BusinessProcess.Service.BCommonData,BusinessProcess.Service");
                DataTable dtUsers = commonData.getUserList();
                //DataSet WriteXMLDS = new DataSet();

                try
                {
                    //DataSet dataSet = new DataSet();
                    //string xmlFilesPath = string.Empty;

                    //if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["XMLFilesPath"].ToString()))
                    //{
                    //xmlFilesPath = ConfigurationManager.AppSettings["XMLFilesPath"].ToString();
                    //}
                    //if (!string.IsNullOrEmpty(xmlFilesPath))
                    //{
                    //  userList.Add(0, "Select");
                    //                    string allMaster = xmlFilesPath + "AllMasters.con";
                    //                  dataSet.ReadXml(allMaster);
                    foreach (DataRow row in dtUsers.Rows)
                    {
                        string val = row["UserName"].ToString() + " - " + row["Designation"].ToString();
                        userList.Add(Convert.ToInt32(row["UserID"].ToString()), val);
                    }
                    //}
                }
                catch
                {
                    userList = new Dictionary<int, string>();
                }
                return userList;

            }
       private static Dictionary<int, string> GetUserList()
        {
            Dictionary<int, string> userList = new Dictionary<int, string>();
            ICommonData commonData = (ICommonData)ObjectFactory.CreateInstance("BusinessProcess.Service.BCommonData,BusinessProcess.Service");
            DataTable dtUsers = commonData.getUserList();
            //DataSet WriteXMLDS = new DataSet();

            try
            {
                //DataSet dataSet = new DataSet();
                //string xmlFilesPath = string.Empty;

                //if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["XMLFilesPath"].ToString()))
                //{
                //xmlFilesPath = ConfigurationManager.AppSettings["XMLFilesPath"].ToString();
                //}
                //if (!string.IsNullOrEmpty(xmlFilesPath))
                //{
                //  userList.Add(0, "Select");
                //                    string allMaster = xmlFilesPath + "AllMasters.con";
                //                  dataSet.ReadXml(allMaster);
                foreach (DataRow row in dtUsers.Rows)
                {
                    string val = row["UserName"].ToString() + " - " + row["Designation"].ToString();
                    userList.Add(Convert.ToInt32(row["UserID"].ToString()), val);
                }
                //}
            }
            catch 
            {
                userList = new Dictionary<int, string>();
            }
            return userList;
        }

       

        #region "To be Deleted"
        public void CreateCustomControls(Panel panelCustomList, string pnlName, ref StringBuilder sbParameter, DataRow theRow, ref string tableName, string frmPrefix, Int32 rowindex)
        {

            if ((theRow[1].ToString() != "4") && (theRow[1].ToString() != "9"))
            {
                Label customLabel = new Label();
                customLabel.ID = pnlName + "lbl" + rowindex.ToString();
                customLabel.Text = theRow[0].ToString().Replace("_", " ");
                customLabel.Text = customLabel.Text.Replace(frmPrefix, "");
                sbParameter.Append(",[" + theRow[0].ToString() + "]");
                customLabel.Width = 200;
                customLabel.CssClass = "labelright";
                customLabel.Font.Bold = true;

                panelCustomList.Controls.Add(customLabel);

                panelCustomList.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));

            }
            //single line Text Box
            if (theRow[1].ToString() == "1")
            {
                TextBox theSingleText = new TextBox();
                theSingleText.ID = pnlName + "Txt" + theRow[0].ToString();
                theSingleText.Width = 200;
                theSingleText.MaxLength = 25;
                panelCustomList.Controls.Add(theSingleText);
 
            }
            //Multi line Text Box
            else if (theRow[1].ToString() == "8")
            {

                TextBox theMultiText = new TextBox();
                theMultiText.ID = pnlName + "Txt" + theRow[0].ToString();
                theMultiText.Width = 200;
                theMultiText.TextMode = TextBoxMode.MultiLine;
                theMultiText.MaxLength = 200;
                panelCustomList.Controls.Add(theMultiText);

            }

            //Date Picker
            else if (theRow[1].ToString() == "5")
            {
                TextBox theDateText = new TextBox();
                theDateText.ID = pnlName + "Dt" + theRow[0].ToString();
                Control ctl = (TextBox)theDateText;

                theDateText.Width = 70;
                theDateText.MaxLength = 11;
                panelCustomList.Controls.Add(theDateText);
                theDateText.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
             //   theDateText.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");
                theDateText.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3');  isCheckValidDateForCustom('" + HttpContext.Current.Session["AppCurrentDateClass"] + "', '" + theDateText.ClientID + "', '" + theDateText.ClientID + "');");


                panelCustomList.Controls.Add(new LiteralControl("&nbsp;"));

                Image theDateImage = new Image();
                theDateImage.ID = pnlName + "img" + rowindex.ToString();
                theDateImage.Height = 22;
                theDateImage.Width = 22;
                theDateImage.ToolTip = "Date Helper";

                theDateImage.ImageUrl = "~/images/cal_icon.gif";
                panelCustomList.Controls.Add(theDateImage);

                theDateImage.Attributes.Add("onClick", "w_displayDatePicker('" + ((TextBox)ctl).ClientID + "');");

                Label theformatlbl = new Label();
                theformatlbl.ID = pnlName + "lblfmt" + rowindex.ToString();
                theformatlbl.Text = " (DD-MMM-YYYY)";
                panelCustomList.Controls.Add(theformatlbl);
 
            }
            //Numeric Field 
            else if (theRow[1].ToString() == "3")
            {
                TextBox theNumberText = new TextBox();
                theNumberText.ID = pnlName + "Num" + theRow[0].ToString();
                theNumberText.Width = 100;
                theNumberText.MaxLength = 9;
                Control ctl = (TextBox)theNumberText;
                panelCustomList.Controls.Add(theNumberText);
                theNumberText.Attributes.Add("onkeyup", "chkInteger('" + ((TextBox)ctl).ClientID + "')");
 
            }
            //Radio Button 
            else if (theRow[1].ToString() == "6")
            {
                HtmlInputRadioButton theYesNoRadio1 = new HtmlInputRadioButton();
                theYesNoRadio1.ID = pnlName + "Radio1" + theRow[0].ToString();
                theYesNoRadio1.Value = "Yes";
                theYesNoRadio1.Name = "Radio" + rowindex.ToString();
                panelCustomList.Controls.Add(theYesNoRadio1);
                theYesNoRadio1.Attributes.Add("onclick", "down(this)");
                theYesNoRadio1.Attributes.Add("onfocus", "up(this)");

                Label theYeslbl = new Label();
                theYeslbl.ID = pnlName + "lblYes" + rowindex.ToString();
                theYeslbl.Text = "Yes";
                theYeslbl.Width = 20;
                theYeslbl.CssClass = "labelright";
                theYeslbl.Font.Bold = true;
                panelCustomList.Controls.Add(theYeslbl);


                HtmlInputRadioButton theYesNoRadio2 = new HtmlInputRadioButton();
                theYesNoRadio2.ID = pnlName + "Radio2" + theRow[0].ToString();
                theYesNoRadio2.Value = "No";
                theYesNoRadio2.Name = "Radio" + rowindex.ToString();
                panelCustomList.Controls.Add(theYesNoRadio2);
                theYesNoRadio2.Attributes.Add("onclick", "down(this)");
                theYesNoRadio2.Attributes.Add("onchange", "up(this)");

                Label theNolbl = new Label();
                theNolbl.ID = pnlName + "lblNo" + rowindex.ToString();
                theNolbl.Text = "No";
                theNolbl.Width = 10;
                theNolbl.CssClass = "labelright";
                theNolbl.Font.Bold = true;
                panelCustomList.Controls.Add(theNolbl);

            }
            
            panelCustomList.Controls.Add(new LiteralControl("</TD>"));

            if (rowindex % 2 != 0)
            {
                panelCustomList.Controls.Add(new LiteralControl("</TR>"));

            }
        }
       
#endregion

        #endregion

    }
}
