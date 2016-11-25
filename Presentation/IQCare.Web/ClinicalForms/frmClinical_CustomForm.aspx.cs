using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Application.Common;
using Application.Presentation;
using Interface.Clinical;
using Interface.Laboratory;
using Interface.Security;
using Entities.Lab;
using System.Collections.Generic;
using IQCare.IQControl;
using Interface.Lookup;

namespace IQCare.Web.Clinical
{
    /// /////////////////////////////////////////////////////////////////////
    // Code Written By   : Jayant Kumar Das
    // Written Date      : 01 December 2010
    // Description       : Customised Form  for Creating Dynamic Forms
    //
    /// /////////////////////////////////////////////////////////////////
    ///
    public partial class CustomForm : BasePage, ICallbackEventHandler
    {
        public static Hashtable htcontrolstatus = new Hashtable();
        public DataTable theCurrentRegDT;
        public DataTable theRegimen;
        private string _tableName;
        private ArrayList ARLHeader = new ArrayList();
        private ArrayList ARLMultiSelect = new ArrayList();
        private DataTable AutoDt = new DataTable();
        private DataTable AutoDtPre = new DataTable();
        private string BmiID;
        private Panel DIVCustomItem = new Panel();
        private int DrugType, RegimenType;
        private DataTable gblDTGridViewControls = new DataTable();
        private bool IsHeightAvail = false;

        //todo
        private bool IsSIngleVisit = false;

        private bool IsWeightAvail = false;
        private string ObjFactoryParameter = "BusinessProcess.Clinical.BCustomForm, BusinessProcess.Clinical";
        private string str, strCallback;
        private AjaxControlToolkit.TabContainer tabContainer = new TabContainer();
        private TabPanel tbChildPanel;
        private Boolean theConditional;
        private DataSet theDSLabs;
        private DataSet theDSXML = new DataSet();
        private Boolean theSecondLabelConditional;
        string bindSource ;
        string bindCategory ;
        string controlReferenceId ;

        //[Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        [WebMethod(EnableSession = true), ScriptMethod]
        public static void removecontrolstatus(string id)
        {
            if (htcontrolstatus.ContainsKey(id))
            {
                htcontrolstatus.Remove(id);
            }
        }

        // [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        [WebMethod(EnableSession = true), ScriptMethod]
        public static void SetContrlStatus(string id)
        {
            if (!(htcontrolstatus.ContainsKey(id)))
            {
                htcontrolstatus.Add(id, id);
            }
        }

        public void AddContolStausInHastTable(string controlid)
        {
            if (!(htcontrolstatus.ContainsKey(controlid)))
            {
                htcontrolstatus.Add(controlid, controlid);
            }
        }

        public DataTable dtgridview(DataTable dt)
        {
            DataTable btable = new DataTable();
            btable.Columns.Add("FeatureID", typeof(string));
            btable.Columns.Add("FieldName", typeof(string));
            btable.Columns.Add("IsGridView", typeof(string));
            btable.Columns.Add("SectionID", typeof(string));
            btable.Columns.Add("Fieldlabel", typeof(string));
            foreach (DataRow r in dt.Rows)
            {
                DataRow theDR = btable.NewRow();
                theDR["FeatureID"] = r["FeatureID"].ToString();
                theDR["FieldName"] = r["FieldName"].ToString();
                theDR["IsGridView"] = r["IsGridView"].ToString();
                theDR["SectionID"] = r["SectionID"].ToString();
                theDR["Fieldlabel"] = r["Fieldlabel"].ToString();

                btable.Rows.Add(theDR);
            }
            return btable;
        }

        public string findcolumnfieldname(DataTable dt, string fieldlable)
        {
            string strcolumname = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (fieldlable.ToUpper() == dt.Rows[i]["Fieldlabel"].ToString().ToUpper())
                {
                    strcolumname = "[" + dt.Rows[i]["FieldName"].ToString() + "]";
                }
            }
            return strcolumname;
        }

        public DataTable GetGridTable(DataTable dt)
        {
            DataTable btable = new DataTable();
            btable.Columns.Add("FeatureID", typeof(string));
            btable.Columns.Add("FeatureName", typeof(string));
            btable.Columns.Add("IsGridView", typeof(string));
            btable.Columns.Add("SectionID", typeof(string));
            btable.Columns.Add("SectionName", typeof(string));
            foreach (DataRow r in dt.Rows)
            {
                DataRow theDR = btable.NewRow();
                theDR["FeatureID"] = r["FeatureID"].ToString();
                theDR["FeatureName"] = r["FeatureName"].ToString();
                theDR["IsGridView"] = r["IsGridView"].ToString();
                theDR["SectionID"] = r["SectionID"].ToString();
                theDR["SectionName"] = r["SectionName"].ToString();

                btable.Rows.Add(theDR);
            }
            return btable;
        }

        public void HtmlCheckBoxSelect(object theObj)
        {
            AjaxControlToolkit.TabContainer container = (AjaxControlToolkit.TabContainer)tabContainer;
            container.ActiveTabIndex = Convert.ToInt32(hdnPrevTabIndex.Value);
            CheckBox theButton = ((CheckBox)theObj);
            string[] theControlId = theButton.ID.ToString().Split('-');
            DataSet theDS = (DataSet)Session["AllData"];
            int theValue = 0;
            if (theButton.Checked == true)
                theValue = 1;
            else
                theValue = 0;

            foreach (DataRow theDR in theDS.Tables[17].Rows)
            {
                foreach (object obj in container.Controls)
                {
                    if (obj is AjaxControlToolkit.TabPanel)
                    {
                        AjaxControlToolkit.TabPanel tabPanel = (AjaxControlToolkit.TabPanel)obj;
                        foreach (object ctrl in tabPanel.Controls)
                        {
                            if (ctrl is Control)
                            {
                                Control c = (Control)ctrl;
                                foreach (Control x in ((Control)c).Controls)
                                {
                                    if (x.ID != null)
                                    {
                                        string[] theIdent = x.ID.Split('-');
                                        if (x.ID.Contains("12Hr"))
                                        {
                                            theIdent = x.ID.Replace("12Hr", "").Split('-');
                                        }
                                        else if (x.ID.Contains("24Hr"))
                                        {
                                            theIdent = x.ID.Replace("24Hr", "").Split('-');
                                        }
                                        else if (x.ID.Contains("Min"))
                                        {
                                            theIdent = x.ID.Replace("Min", "").Split('-');
                                        }
                                        else if (x.ID.Contains("AMPM"))
                                        {
                                            theIdent = x.ID.Replace("AMPM", "").Split('-');
                                        }
                                        if (theIdent.Length > 2)
                                        {
                                            if (x.GetType().ToString() == "System.Web.UI.WebControls.TextBox" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                                            {
                                                if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "1")
                                                {
                                                    //((TextBox)x).Enabled = true;
                                                    //ApplyBusinessRules(x, "1", true);
                                                    //ApplyBusinessRules(x, "2", true);
                                                    //ApplyBusinessRules(x, "3", true);
                                                }
                                                else if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "0")
                                                {
                                                    //((TextBox)x).Enabled = false;
                                                    ((TextBox)x).Text = "";
                                                }
                                                if ((theIdent[0] == "TXTDTAuto") || (theIdent[0] == "TXTMultiAuto") || (theIdent[0] == "TXTAuto") || (theIdent[0] == "TXTNUMAuto"))
                                                {
                                                    ((TextBox)x).Enabled = false;
                                                }
                                            }

                                            if (x.GetType().ToString() == "System.Web.UI.WebControls.DropDownList" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                                            {
                                                if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "1")
                                                {
                                                    //((DropDownList)x).Enabled = true;
                                                    //ApplyBusinessRules(x, "4", true);
                                                }
                                                else if (x.ID.ToString() == theIdent[0].ToString() + "-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString() + "24Hr" && theValue.ToString() == "0")
                                                {
                                                    ((DropDownList)x).Enabled = false;
                                                    ((DropDownList)x).SelectedValue = "0";
                                                }
                                                else if (x.ID.ToString() == theIdent[0].ToString() + "-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString() + "12Hr" && theValue.ToString() == "0")
                                                {
                                                    ((DropDownList)x).Enabled = false;
                                                    ((DropDownList)x).SelectedValue = "0";
                                                }
                                                else if (x.ID.ToString() == theIdent[0].ToString() + "-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString() + "Min" && theValue.ToString() == "0")
                                                {
                                                    ((DropDownList)x).Enabled = false;
                                                    ((DropDownList)x).SelectedValue = "0";
                                                }
                                                else if (x.ID.ToString() == theIdent[0].ToString() + "-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString() + "AMPM" && theValue.ToString() == "0")
                                                {
                                                    ((DropDownList)x).Enabled = false;
                                                    ((DropDownList)x).SelectedValue = "AM";
                                                }
                                                else if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "0")
                                                {
                                                    ((DropDownList)x).Enabled = false;
                                                    ((DropDownList)x).SelectedValue = "0";
                                                }
                                            }

                                            if (x.GetType().ToString() == "System.Web.UI.WebControls.Panel" && theIdent[0] == "Pnl_" + theDR["FieldId"].ToString())
                                            {
                                                if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "1")
                                                {
                                                    ((Panel)x).Enabled = true;
                                                    ApplyBusinessRules(x, "9", true);
                                                }
                                                else if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "0")
                                                {
                                                    ((Panel)x).Enabled = false;
                                                }
                                            }

                                            if (x.GetType().ToString() == "System.Web.UI.WebControls.Button" && "BtnDrg-" + theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == "BtnDrg-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                                            {
                                                if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "1")
                                                {
                                                    ((Button)x).Enabled = true;
                                                }
                                                else if (theControlId[4] == theDR["ConFieldId"].ToString() && theValue.ToString() == "0")
                                                {
                                                    DrugType = GetFilterId(theIdent[3], theIdent[1]);
                                                    Session["Selected" + DrugType + ""] = null;
                                                    ((Button)x).Enabled = false;
                                                }
                                            }

                                            if (x.GetType().ToString() == "System.Web.UI.WebControls.Button" && "BtnLab-" + theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == "BtnLab-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                                            {
                                                if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "1")
                                                    ((Button)x).Enabled = true;
                                                else if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theControlId[1].ToString() && theValue.ToString() == "0")
                                                {
                                                    ViewState["AddLab"] = null;
                                                    ((Button)x).Enabled = false;
                                                }
                                            }

                                            if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputButton" && "BtnRegimen-" + theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == "BtnRegimen-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                                            {
                                                if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "1")
                                                    ((HtmlInputButton)x).Visible = true;
                                                else if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theControlId[1].ToString() && theValue.ToString() == "0")
                                                {
                                                    ((HtmlInputButton)x).Visible = false;
                                                }
                                            }

                                            if (x.GetType().ToString() == "System.Web.UI.WebControls.Image" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                                            {
                                                if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "1")
                                                {
                                                    ((Image)x).Visible = true;
                                                    ApplyBusinessRules(x, "5", true);
                                                }
                                                else if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "0")
                                                    ((Image)x).Visible = false;
                                            }

                                            if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputRadioButton" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                                            {
                                                if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "1")
                                                {
                                                    ((HtmlInputRadioButton)x).Visible = true;
                                                    ApplyBusinessRules(x, "6", true);
                                                }
                                                else if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "0")
                                                    ((HtmlInputRadioButton)x).Visible = false;
                                            }

                                            if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputCheckBox" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                                            {
                                                if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "1")
                                                    ((HtmlInputCheckBox)x).Visible = true;
                                                else if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "0")
                                                    ((HtmlInputCheckBox)x).Visible = false;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void HtmlRadioButtonSelect(object sender)
        {
            AjaxControlToolkit.TabContainer container = (AjaxControlToolkit.TabContainer)tabContainer;
            container.ActiveTabIndex = Convert.ToInt32(hdnPrevTabIndex.Value);
            HtmlInputRadioButton theButton = ((HtmlInputRadioButton)sender);
            string[] theControlId = theButton.ID.Split('-');
            DataSet theDS = (DataSet)Session["AllData"];
            int theValue = 0;
            if (theButton.Value == "Yes" && theButton.Checked == true)
                theValue = 1;
            else if (theButton.Value == "Yes" && theButton.Checked == false)
                theValue = 0;
            if (theButton.Value == "No" && theButton.Checked == true)
                theValue = 2;
            else if (theButton.Value == "No" && theButton.Checked == false)
                theValue = 0;

            foreach (DataRow theDR in theDS.Tables[17].Rows)
            {
                foreach (object obj in container.Controls)
                {
                    if (obj is AjaxControlToolkit.TabPanel)
                    {
                        AjaxControlToolkit.TabPanel tabPanel = (AjaxControlToolkit.TabPanel)obj;
                        foreach (object ctrl in tabPanel.Controls)
                        {
                            if (ctrl is Control)
                            {
                                Control c = (Control)ctrl;
                                foreach (Control x in ((Control)c).Controls)
                                {
                                    if (x.ID != null)
                                    {
                                        string[] theIdent = x.ID.Split('-');
                                        if (x.ID.Contains("12Hr"))
                                        {
                                            theIdent = x.ID.Replace("12Hr", "").Split('-');
                                        }
                                        else if (x.ID.Contains("24Hr"))
                                        {
                                            theIdent = x.ID.Replace("24Hr", "").Split('-');
                                        }
                                        else if (x.ID.Contains("Min"))
                                        {
                                            theIdent = x.ID.Replace("Min", "").Split('-');
                                        }
                                        else if (x.ID.Contains("AMPM"))
                                        {
                                            theIdent = x.ID.Replace("AMPM", "").Split('-');
                                        }
                                        if (theIdent.Length > 2)
                                        {
                                            if (x.GetType().ToString() == "System.Web.UI.WebControls.TextBox" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                                            {
                                                if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theValue.ToString())
                                                {
                                                    // ((TextBox)x).Enabled = true;
                                                    // ApplyBusinessRules(x, "1", true);
                                                    // ApplyBusinessRules(x, "2", true);
                                                    // ApplyBusinessRules(x, "3", true);
                                                }
                                                else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theValue.ToString())
                                                {
                                                    //((TextBox)x).Enabled = false;
                                                    ((TextBox)x).Text = "";
                                                }
                                                if ((theIdent[0] == "TXTDTAuto") || (theIdent[0] == "TXTMultiAuto") || (theIdent[0] == "TXTAuto") || (theIdent[0] == "TXTNUMAuto"))
                                                {
                                                    ((TextBox)x).Enabled = false;
                                                }
                                            }

                                            if (x.GetType().ToString() == "System.Web.UI.WebControls.DropDownList" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                                            {
                                                if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theValue.ToString())
                                                {
                                                    ((DropDownList)x).Enabled = true;
                                                    ApplyBusinessRules(x, "4", true);
                                                }
                                                else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theValue.ToString())
                                                {
                                                    ((DropDownList)x).Enabled = false;
                                                    if (((DropDownList)x).ID.Contains("AMPM") == false)
                                                    {
                                                        ((DropDownList)x).SelectedValue = "0";
                                                    }
                                                }
                                                else if (x.ID.ToString() == theIdent[0].ToString() + "-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString() + "24Hr")
                                                {
                                                    ((DropDownList)x).Enabled = true;
                                                    ApplyBusinessRules(x, "4", true);
                                                }
                                                else if (x.ID.ToString() == theIdent[0].ToString() + "-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString() + "12Hr")
                                                {
                                                    ((DropDownList)x).Enabled = true;
                                                    ApplyBusinessRules(x, "4", true);
                                                }
                                                else if (x.ID.ToString() == theIdent[0].ToString() + "-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString() + "Min")
                                                {
                                                    ((DropDownList)x).Enabled = true;
                                                    ApplyBusinessRules(x, "4", true);
                                                }
                                                else if (x.ID.ToString() == theIdent[0].ToString() + "-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString() + "AMPM")
                                                {
                                                    ((DropDownList)x).Enabled = true;
                                                    ApplyBusinessRules(x, "4", true);
                                                }
                                            }
                                            if (x.GetType().ToString() == "System.Web.UI.WebControls.Panel" && theIdent[0] + theIdent[1] + theIdent[2] == "Pnl_" + theDR["PdfTableName"].ToString() + theDR["FieldId"].ToString())
                                            {
                                                if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theValue.ToString())
                                                {
                                                    ((Panel)x).Enabled = true;
                                                    ApplyBusinessRules(x, "9", true);
                                                }
                                                else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theValue.ToString())
                                                {
                                                    ((Panel)x).Enabled = false;
                                                }
                                            }

                                            if (x.GetType().ToString() == "System.Web.UI.WebControls.Button" && "BtnDrg-" + theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == "BtnDrg-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                                            {
                                                if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theValue.ToString())
                                                    ((Button)x).Enabled = true;
                                                else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theValue.ToString())
                                                {
                                                    DrugType = GetFilterId(theIdent[3], theIdent[1]);
                                                    Session["Selected" + DrugType + ""] = null;
                                                    ((Button)x).Enabled = false;
                                                }
                                            }

                                            if (x.GetType().ToString() == "System.Web.UI.WebControls.Button" && "BtnLab-" + theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == "BtnLab-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                                            {
                                                if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theValue.ToString())
                                                {
                                                    ((Button)x).Enabled = true;
                                                    ViewState["btnlabisEnable"] = "1";
                                                }
                                                else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theValue.ToString())
                                                {
                                                    ViewState["btnlabisEnable"] = "2";
                                                    //ViewState["AddLab"] = null;
                                                    ((Button)x).Enabled = false;
                                                }
                                            }

                                            if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputButton" && "BtnRegimen-" + theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == "BtnRegimen-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                                            {
                                                if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theValue.ToString())
                                                    ((HtmlInputButton)x).Visible = true;
                                                else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theValue.ToString())
                                                {
                                                    ((HtmlInputButton)x).Visible = false;
                                                }
                                            }

                                            if (x.GetType().ToString() == "System.Web.UI.WebControls.Image" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                                            {
                                                if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theValue.ToString())
                                                {
                                                    ((Image)x).Visible = true;
                                                    ApplyBusinessRules(x, "5", true);
                                                }
                                                else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theValue.ToString())
                                                    ((Image)x).Visible = false;
                                            }

                                            if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputRadioButton" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                                            {
                                                if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theValue.ToString())
                                                {
                                                    ((HtmlInputRadioButton)x).Visible = true;
                                                    ApplyBusinessRules(x, "6", true);
                                                }
                                                else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theValue.ToString())
                                                    ((HtmlInputRadioButton)x).Visible = false;
                                            }

                                            if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputCheckBox" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                                            {
                                                if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theValue.ToString())
                                                    ((HtmlInputCheckBox)x).Visible = true;
                                                else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theValue.ToString())
                                                    ((HtmlInputCheckBox)x).Visible = false;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void RemoveContolStausInHastTable(string controlid)
        {
            if (htcontrolstatus.ContainsKey(controlid))
            {
                htcontrolstatus.Remove(controlid);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string theUrl;
            theUrl = string.Format("{0}", "frmPatient_Home.aspx");
            Response.Redirect(theUrl);
        }

        protected void btncomplete_Click(object sender, EventArgs e)
        {
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            int TabID = Convert.ToInt32(ID[1]);
            string PrevTabId = hdnPrevTabId.Value;
            hdnPrevTabId.Value = hdnCurrentTabId.Value;
            string SaveTabData = hdnSaveTabData.Value;
            AjaxControlToolkit.TabContainer container = (AjaxControlToolkit.TabContainer)tabContainer;
            ConFieldEnableDisable(container);
            Page_PreRender(sender, e);
            ICustomForm MgrSaveUpdate = (ICustomForm)ObjectFactory.CreateInstance(ObjFactoryParameter);
            DataSet theDS = new DataSet();
            theDS.Tables.Add(ReadLabTable(container, TabID));
            theDS.Tables.Add(ReadARVMedicationTable(container, TabID));
            theDS.Tables.Add(ReadNonARVMedicationTable(container, TabID));

            if (FieldValidation() == false)
            { return; }

            if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
            {
                int PatientID = Convert.ToInt32(Session["PatientId"]);
                ViewState["VisitDate"] = txtvisitDate.Text;
                StringBuilder Insert = SaveCustomFormData(PatientID, theDS, 0, TabID);
                DataSet TempDS = MgrSaveUpdate.SaveUpdate(Insert.ToString(), theDS, TabID);
                Session["PatientVisitId"] = TempDS.Tables[0].Rows[0]["VisitID"].ToString();
                Session["ServiceLocationId"] = TempDS.Tables[0].Rows[0]["LocationID"].ToString();
                SaveCancel();
            }
            else if (Request.QueryString["Name"] == "Delete" && Convert.ToInt32(Session["PatientVisitId"]) > 0)
            {
                int PatientID = Convert.ToInt32(Session["PatientId"]);
                int VisitID = Convert.ToInt32(Session["PatientVisitId"]);
                DeleteForm(PatientID, VisitID);
            }
            else if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
            {
                int FeatureID = Convert.ToInt32(Session["FeatureID"]);
                int PatientID = Convert.ToInt32(Session["PatientId"]);
                int VisitID = Convert.ToInt32(Session["PatientVisitId"]);
                int LocationID = Convert.ToInt32(Session["ServiceLocationId"]);
                StringBuilder Update = UpdateCustomFormData(PatientID, FeatureID, VisitID, LocationID, theDS, 0, TabID);
                DataSet TempDS = MgrSaveUpdate.SaveUpdate(Update.ToString(), theDS, TabID);
                Session["PatientVisitId"] = TempDS.Tables[0].Rows[0]["VisitID"].ToString();
                UpdateCancel();
            }
        }

        protected BoundField CreateBoundColumn(DataColumn c)
        {
            BoundField column = new BoundField();

            column.DataField = c.ColumnName;
            column.HeaderText = c.ColumnName.Replace("_", " ");
            column.DataFormatString = setFormating(c);

            column.ItemStyle.CssClass = "textstyle";
            return column;
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //So that when the user clicks on the row - the corresponding row is edited
            GridView gv = sender as GridView;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string[] secname = (sender as GridView).ID.ToString().Split('_'); ;

                string secid = secname[1];
                if (ViewState["GridCache_" + secid] != null)
                {
                    int columncount = ((DataTable)ViewState["GridCache_" + secid]).Columns.Count;

                    for (int i = 0; i < gv.Columns.Count; i++)
                    {
                        e.Row.Cells[i].Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.BackColor='#666699';");
                        e.Row.Cells[i].Attributes.Add("onmouseout", "this.style.backgroundColor='';");
                        e.Row.Cells[i].Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference((Control)sender, "Select$" + e.Row.RowIndex.ToString()));
                    }
                }
                else
                {
                }
            }
        }

        protected void grdView_RowDeleted(object sender, GridViewDeleteEventArgs e)
        {
            string[] secname = (sender as GridView).ID.ToString().Split('_'); ;
            string secid = secname[1];

            DataTable dtviewstate = (DataTable)ViewState["GridCache_" + secid];
            dtviewstate.Rows.RemoveAt(e.RowIndex);
            AjaxControlToolkit.TabContainer container = (AjaxControlToolkit.TabContainer)tabContainer;
            BindGridView(secid, container, (DataTable)ViewState["GridCache_" + secid]);
        }

        protected void objdView_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            AjaxControlToolkit.TabContainer container = (AjaxControlToolkit.TabContainer)tabContainer;
            string[] secname = (sender as GridView).ID.ToString().Split('_'); ;
            string secid = secname[1];

            int thePage = (sender as GridView).PageIndex;
            int thePageSize = (sender as GridView).PageSize;

            GridViewRow theRow = (sender as GridView).Rows[e.NewSelectedIndex];
            theRow.BackColor = System.Drawing.Color.AliceBlue;
            int theIndex = thePageSize * thePage + theRow.RowIndex;

            System.Data.DataTable theDT = new System.Data.DataTable();
            theDT = (DataTable)ViewState["GridCache_" + secid];

            foreach (object obj in container.Controls)
            {
                if (obj is AjaxControlToolkit.TabPanel)
                {
                    AjaxControlToolkit.TabPanel tabPanel = (AjaxControlToolkit.TabPanel)obj;
                    foreach (object ctrl in tabPanel.Controls)
                    {
                        if (ctrl is Control)
                        {
                            Control c = (Control)ctrl;
                            foreach (object ctl in c.Controls)
                            {
                                string btnname = "BtnAdd-" + secid.ToString();
                                if (ctl != null)
                                {
                                    if (ctl is Button)
                                    {
                                        if (((Button)ctl).ID.ToString() == btnname)
                                        {
                                            ((Button)ctl).Text = "Update";
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //foreach (Control ctl in DIVCustomItem.Controls)
            //{
            //}

            int r = theIndex;
            //Fill data in Textboxes from grid
            //Edit the selected row
            DataTable theGVSectionControl = ((DataTable)(ViewState["gblDTGridViewControls"])).DefaultView.ToTable(true, "FieldName", "FieldLabel", "SectionId").Copy();
            DataView theDVSectionControl = new DataView(theGVSectionControl);
            theDVSectionControl.RowFilter = "SectionId=" + secid;
            if (theDT.Rows.Count > 0)
            {
                for (int i = 0; i < theDVSectionControl.Count; i++)
                {
                    setGridViewSectionControl(container, theDT, theIndex, theDVSectionControl[i]["FieldName"].ToString(), theDVSectionControl[i]["FieldLabel"].ToString());
                }
                ViewState["SaveFlag_" + secid] = "Edit";
                ViewState["SelectedRow_" + secid] = theIndex;
            }
        }

        /// <summary>
        /// Handles the Init event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Init(object sender, EventArgs e)
        {
            Master.PageScriptManager.AsyncPostBackError += new EventHandler<AsyncPostBackErrorEventArgs>(PageScriptManager_AsyncPostBackError);
        }
        
        private int ModuleId
        {
            get
            {
                return Convert.ToInt32(Session["TechnicalAreaId"]);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int FeatureID = 0, PatientID = 0, VisitID, LocationID;
                if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
                {
                    FeatureID = Convert.ToInt32(Session["FeatureID"]);
                }
                else
                {
                    FeatureID = Convert.ToInt32(Session["FeatureID"]);
                }
                PatientID = Convert.ToInt32(Session["PatientId"]);
                LoadPredefinedLabel_Field(FeatureID, PatientID);
                VisitID = Convert.ToInt32(Session["PatientVisitId"]);
                LocationID = Convert.ToInt32(Session["ServiceLocationId"]);
                GetICallBackFunction();
                DataSet dsvisit = new DataSet();
                dsvisit = (DataSet)Session["AllData"];
                if (Convert.ToInt32(dsvisit.Tables[14].Rows[0]["MultiVisit"]) == 1)
                {
                    OnBlur();
                }

                if (IsPostBack != true)
                {
                    tabContainer.OnClientActiveTabChanged = "ValidateSave";
                    hdnCurrentTabId.Value = tabContainer.ActiveTab.ID;
                    hdnPrevTabId.Value = tabContainer.ActiveTab.ID;
                    hdnCurrenTabName.Value = tabContainer.ActiveTab.HeaderText;
                    hdnPrevTabName.Value = tabContainer.ActiveTab.HeaderText;
                    ViewState["ActiveTabIndex"] = tabContainer.ActiveTabIndex;
                    hdnPrevTabIndex.Value = Convert.ToString(tabContainer.ActiveTabIndex);
                    hdnCurrenTabIndex.Value = Convert.ToString(tabContainer.ActiveTabIndex);
                    Session["SaveFlag"] = "Add";
                    if (ViewState["LabRanges"] == null)
                    {
                        //ILabFunctions LabResultManager = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
                        //theDSLabs = LabResultManager.GetLabValues();
                        //ViewState["LabRanges"] = theDSLabs;
                        //ViewState["LabMaster"] = theDSLabs.Tables[2];
                    }
                    if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
                    {
                        ICustomForm MgrValidate = (ICustomForm)ObjectFactory.CreateInstance(ObjFactoryParameter);
                        DataSet theDS = MgrValidate.Validate(theHeader.InnerText, "01-01-1900", this.PatientId,this.ModuleId);
                        AuthenticationRight(FeatureID, "Add");
                        hdnVisitId.Value = "0";
                    }
                    else if (Request.QueryString["Name"] == "Delete" && Convert.ToInt32(Session["PatientVisitId"]) > 0)
                    {
                        btnsave.Visible = true;
                        btnsave.Text = "Delete";
                        foreach (DataRow theDRBindValue in dsvisit.Tables[23].Rows)
                        {
                            //BindValue(PatientID, VisitID, LocationID, DIVCustomItem, Convert.ToInt32(theDRBindValue["TabId"]));
                            BindValue(PatientID, VisitID, LocationID, tabContainer, dsvisit.Tables[23]);
                        }
                        AuthenticationRight(FeatureID, "Delete");
                        MsgBuilder theBuilder = new MsgBuilder();
                        theBuilder.DataElements["FormName"] = "This";
                        IQCareMsgBox.ShowConfirm("DeleteForm", theBuilder, btnsave);
                    }
                    else if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
                    {
                        hdnVisitId.Value = VisitID.ToString();
                        BindValue(PatientID, VisitID, LocationID, tabContainer, dsvisit.Tables[23]);

                        AuthenticationRight(FeatureID, "Edit");
                    }

                    txtvisitDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
                    txtvisitDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3'); SendCodeName('" + txtvisitDate.ClientID + "');");
                    (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Clinical >>";
                    (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = theHeader.InnerText;
                    (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = theHeader.InnerText;

                    (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text = "0";
                    Session["PtnRegCTC"] = "";
                    //Drug Data
                    Session["CustomfrmDrug"] = "CustomfrmDrug";
                    Session["CustomfrmLab"] = "CustomfrmLab";
                    BindDropdown(ddSignature);
                    ClientScript.RegisterStartupScript(GetType(), "CurrentTabValue", "StringASCII(" + hdnPrevTabId.Value + ");", true);
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();// +err.StackTrace + err.Data.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            AjaxControlToolkit.TabContainer container = tabContainer;
            /////HTML Controls PostBack//////
            ConFieldEnableDisable(container);
        }

        private string apostropheHandler(String input)
        {
            return input.Replace("'", "''");
        }

        private void ApplyBusinessRules(object theControl, string ControlID, bool theConField)
        {
            try
            {
                //bool isDateSet = false;
                tabContainer.ID = "TAB";
                DataTable theDT = (DataTable)ViewState["BusRule"];
                string Max = "", Min = "", Column = "";//, AgeFrom = "", AgeTo = "";
                string MaxNormalval = "", MinNormalVal = "";
                bool theEnable = theConField;
                string[] Field, wihifield;
                if (ControlID == "9")
                {
                    Field = ((Control)theControl).ID.Split('_');
                }
                else
                {
                    Field = ((Control)theControl).ID.Split('-');
                }
                foreach (DataRow DR in theDT.Rows)
                {
                    if (Field[0] == "Pnl")
                    {
                        string[] PnlBus;
                        int splitlength = ((Control)theControl).ID.Split('_').Length - 1;
                        PnlBus = Field[splitlength].Split('-');

                        if (PnlBus[1] == Convert.ToString(DR["FieldId"]) && Convert.ToString(DR["BusRuleId"]) == "14"
                            && Session["PatientSex"].ToString() != "Male")
                            theEnable = false;

                        if (PnlBus[1] == Convert.ToString(DR["FieldId"]) && Convert.ToString(DR["BusRuleId"]) == "15"
                            && Session["PatientSex"].ToString() != "Female")
                            theEnable = false;

                        if (PnlBus[1] == Convert.ToString(DR["FieldId"]) && Convert.ToString(DR["BusRuleId"]) == "16")
                        {
                            if ((DR["Value"] != DBNull.Value) && (DR["Value1"] != DBNull.Value))
                            {
                                if (Convert.ToDecimal(Session["PatientAge"]) >= Convert.ToDecimal(DR["Value"]) && Convert.ToDecimal(Session["PatientAge"]) <= Convert.ToDecimal(DR["Value1"]))
                                {
                                }
                                else
                                    theEnable = false;
                            }
                        }
                    }
                    else if (Field[0] == "BtnLab")
                    {
                        if (Field[1] == Convert.ToString(DR["FieldName"]) && Convert.ToString(DR["BusRuleId"]) == "14" && Session["PatientSex"].ToString() != "Male")
                        {
                            theEnable = false;
                        }

                        if (Field[1] == Convert.ToString(DR["FieldName"]) && Convert.ToString(DR["BusRuleId"]) == "15" && Session["PatientSex"].ToString() != "Female")
                        {
                            theEnable = false;
                        }

                        if (Field[1] == Convert.ToString(DR["FieldName"]) && Convert.ToString(DR["BusRuleId"]) == "16")
                        {
                            if ((DR["Value"] != DBNull.Value) && (DR["Value1"] != DBNull.Value))
                            {
                                if (Convert.ToDecimal(Session["PatientAge"]) >= Convert.ToDecimal(DR["Value"]) && Convert.ToDecimal(Session["PatientAge"]) <= Convert.ToDecimal(DR["Value1"]))
                                {
                                }
                                else
                                {
                                    theEnable = false;
                                }
                            }
                        }
                    }
                    else if (Field[0].ToUpper() == "TXTDT")
                    {
                        // Date format for like "MMM-yyyy"
                        //if (Field[1] == Convert.ToString(DR["FieldName"]) && Convert.ToString(DR["BusRuleId"]) == "21")
                        //{
                        //    //isDateSet = true;
                        //    TextBox theDateText = (TextBox)theControl;
                        //    theDateText.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'4')");
                        //    theDateText.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'4')");
                        //    DIVCustomItem.Controls.Add(new LiteralControl("<span class='smallerlabel'>(MMM-YYYY)</span>"));
                        //}
                    }
                    else
                    {
                        if (Field[1] == Convert.ToString(DR["FieldName"]) && Field[2] == Convert.ToString(DR["TableName"]) && Field[3] == Convert.ToString(DR["FieldId"]) && Convert.ToString(DR["BusRuleId"]) == "2")
                        {
                            Max = Convert.ToString(DR["Value"]);
                            Column = Convert.ToString(DR["FieldLabel"]);
                        }
                        if (Field[1] == Convert.ToString(DR["FieldName"]) && Field[2] == Convert.ToString(DR["TableName"]) && Field[3] == Convert.ToString(DR["FieldId"]) && Convert.ToString(DR["BusRuleId"]) == "3")
                        {
                            Min = Convert.ToString(DR["Value"]);
                        }
                        // todo
                        if (Field[1] == Convert.ToString(DR["FieldName"]) && Field[2] == Convert.ToString(DR["TableName"]) && Field[3] == Convert.ToString(DR["FieldId"]) && Convert.ToString(DR["BusRuleId"]) == "26")
                        {
                            MaxNormalval = Convert.ToString(DR["Value"]);
                        }
                        if (Field[1] == Convert.ToString(DR["FieldName"]) && Field[2] == Convert.ToString(DR["TableName"]) && Field[3] == Convert.ToString(DR["FieldId"]) && Convert.ToString(DR["BusRuleId"]) == "27")
                        {
                            MinNormalVal = Convert.ToString(DR["Value"]);
                        }

                        if (Field[1] == Convert.ToString(DR["FieldName"]) && Field[2] == Convert.ToString(DR["TableName"]) && Field[3] == Convert.ToString(DR["FieldId"])
                            && Convert.ToString(DR["BusRuleId"]) == "14" && Session["PatientSex"].ToString() != "Male")
                            theEnable = false;

                        if (Field[1] == Convert.ToString(DR["FieldName"]) && Field[2] == Convert.ToString(DR["TableName"]) && Field[3] == Convert.ToString(DR["FieldId"])
                        && Convert.ToString(DR["BusRuleId"]) == "15" && Session["PatientSex"].ToString() != "Female")
                            theEnable = false;

                        if (Field[1] == Convert.ToString(DR["FieldName"]) && Field[2] == Convert.ToString(DR["TableName"]) && Field[3] == Convert.ToString(DR["FieldId"])
                        && Convert.ToString(DR["BusRuleId"]) == "16")
                        {
                            if (Convert.ToDecimal(Session["PatientAge"]) >= Convert.ToDecimal(DR["Value"]) && Convert.ToDecimal(Session["PatientAge"]) <= Convert.ToDecimal(DR["Value1"]))
                            {
                            }
                            else
                                theEnable = false;
                        }
                    }
                }

                if (theControl.GetType().ToString() == "System.Web.UI.WebControls.TextBox")
                {
                    Field = ((Control)theControl).ID.Split('_');
                    wihifield = Field[0].Split('-');
                    TextBox tbox = (TextBox)theControl;
                    //tbox.Enabled = theEnable;
                    if (ControlID == "1")
                    { }
                    else if (ControlID == "2" && Field[0] == "TXT")
                    {
                        if (!tbox.ClientID.Contains("ctl00_IQCareContentPlaceHolder_"))
                        {
                            tbox.Attributes.Add("onkeyup", "chkDecimal('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + tbox.ClientID + "')");
                        }
                    }
                    else if (ControlID == "3" && Field[0] == "TXTNUM")
                    {
                        if (!tbox.ClientID.Contains("ctl00_IQCareContentPlaceHolder_"))
                        {
                            tbox.Attributes.Add("onkeyup", "chkDecimal('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + tbox.ClientID + "')");
                        }
                    }
                    else if (ControlID == "5" && Field[0] == "TXTDT")
                    {
                        tbox.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
                        tbox.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");
                    }
                    if (Max != "" && Min != "" && MaxNormalval != "" && MinNormalVal != "")
                    {
                        if (!tbox.ClientID.Contains("ctl00_IQCareContentPlaceHolder_"))
                        {
                            if (wihifield[1].ToUpper() == "HEIGHT" || wihifield[1].ToUpper() == "WEIGHT")
                            {
                                tbox.Attributes.Add("onblur", "CalcualteBMIGet(); isCheckNormal('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + tbox.ClientID + "', '" + Column + "', '" + Min + "', '" + Max + "', '" + MinNormalVal + "', '" + MaxNormalval + "')");
                            }
                            else
                                tbox.Attributes.Add("onblur", "isCheckNormal('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + tbox.ClientID + "', '" + Column + "', '" + Min + "', '" + Max + "', '" + MinNormalVal + "', '" + MaxNormalval + "')");
                        }
                    }
                    else if (Max != "" && Min != "")
                    {
                        if (!tbox.ClientID.Contains("ctl00_IQCareContentPlaceHolder_"))
                        {
                            tbox.Attributes.Add("onblur", "isBetween('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + tbox.ClientID + "', '" + Column + "', '" + Min + "', '" + Max + "')");
                        }
                    }
                    else if (Max != "")
                    {
                        if (!tbox.ClientID.Contains("ctl00_IQCareContentPlaceHolder_"))
                        {
                            tbox.Attributes.Add("onblur", "checkMax('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + tbox.ClientID + "', '" + Column + "', '" + Max + "')");
                        }
                    }
                    else if (Min != "")
                    {
                        if (!tbox.ClientID.Contains("ctl00_IQCareContentPlaceHolder_"))
                        {
                            tbox.Attributes.Add("onblur", "checkMin('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + tbox.ClientID + "', '" + Column + "', '" + Min + "')");
                        }
                    }
                    //else if (Max != "" && Min != "" && MaxNormalval != "" && MinNormalVal != "")
                    //{
                    //    if (!tbox.ClientID.Contains("ctl00_IQCareContentPlaceHolder_"))
                    //    {
                    //        tbox.Attributes.Add("onblur", "isCheckNormal('ctl00_IQCareContentPlaceHolder_" + tabcontainer.ID + "_" + tbChildPanel.ID + "_" + tbox.ClientID + "', '" + Column + "', '" + Min + "', '" + Max + "', '" + MinNormalVal + "', '" + MaxNormalval + "')");
                    //    }
                    //}
                }
                else if (theControl.GetType().ToString() == "System.Web.UI.WebControls.DropDownList")
                {
                    //DropDownList ddList = (DropDownList)theControl;
                    //ddList.Enabled = theEnable;
                }
                else if (theControl.GetType().ToString() == "System.Web.UI.WebControls.CheckBox")
                {
                    CheckBox Multichk = (CheckBox)theControl;
                    Multichk.Enabled = theEnable;
                }
                else if (theControl.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputRadioButton")
                {
                    HtmlInputRadioButton Rdobtn = (HtmlInputRadioButton)theControl;
                    //Rdobtn.Visible = theEnable;
                }
                else if (theControl.GetType().ToString() == "System.Web.UI.WebControls.Image")
                {
                    Image img = (Image)theControl;
                    img.Visible = theEnable;
                }
                else if (theControl.GetType().ToString() == "System.Web.UI.WebControls.Panel")
                {
                    Panel pnl = (Panel)theControl;
                    pnl.Enabled = theEnable;
                }
                else if (theControl.GetType().ToString() == "System.Web.UI.WebControls.Button")
                {
                    Button tbtn = (Button)theControl;
                    tbtn.Enabled = theEnable;
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
        }

        private DataTable ARVDrug()
        {
            DataTable dtARVDrug = new DataTable();
            dtARVDrug.Columns.Add("DrugId", Type.GetType("System.Int32"));
            dtARVDrug.Columns.Add("GenericID", Type.GetType("System.Int32"));
            dtARVDrug.Columns.Add("Dose", Type.GetType("System.String"));
            dtARVDrug.Columns.Add("FrequencyID", Type.GetType("System.String"));
            dtARVDrug.Columns.Add("Duration", Type.GetType("System.Decimal"));
            dtARVDrug.Columns.Add("QtyPrescribed", Type.GetType("System.Decimal"));
            dtARVDrug.Columns.Add("QtyDispensed", Type.GetType("System.Decimal"));
            dtARVDrug.Columns.Add("ARFinance", Type.GetType("System.Int32"));
            dtARVDrug.Columns.Add("DrugTypeId", Type.GetType("System.Int32"));
            return dtARVDrug;
        }

        private void AuthenticationRight(int FeatureID, string Mode)
        {
            AuthenticationManager Authentication = new AuthenticationManager();
            if (Authentication.HasFunctionRight(FeatureID, FunctionAccess.Print, (DataTable)Session["UserRight"]) == false)
            {
                btnPrint.Enabled = false;
            }
            if (Mode == "Add")
            {
                if (Authentication.HasFunctionRight(FeatureID, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
                {
                    btnsave.Enabled = false;
                }
            }
            else if (Mode == "Edit")
            {
                if (Authentication.HasFunctionRight(FeatureID, FunctionAccess.Update, (DataTable)Session["UserRight"]) == false)
                {
                    btnsave.Enabled = false;
                }
            }
            else if (Mode == "Delete")
            {
                if (Authentication.HasFunctionRight(FeatureID, FunctionAccess.Delete, (DataTable)Session["UserRight"]) == false)
                {
                    btnsave.Enabled = false;
                }
            }
        }

        private void AutopopulateHiddenvalue(string str)
        {
            if (str != "")
            {
                DataView AutoDV = new DataView(AutoDt);
                AutoDV.RowFilter = "visitdate < " + Convert.ToDateTime(str);
            }
        }
        private ILabRequest requestMgr = (ILabRequest)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabRequest, BusinessProcess.Laboratory");
        private ILabManager labMgr = (ILabManager)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabManager, BusinessProcess.Laboratory");
        /// <summary>
        /// Binds the custom controls.
        /// </summary>
        /// <param name="test">The test.</param>
        void BindCustomControls(LabTest test)
        {
            LabTestGroup group = labMgr.GetGroupLabTest(test.Id);

            List<TestParameter> testParameters = labMgr.GetLabTestParameters(test.Id);
            if (null != group && null != group.ComponentTest)
            {
                foreach (LabTest _t in group.ComponentTest)
                {
                    List<TestParameter> _p = labMgr.GetLabTestParameters(_t.Id);
                    if (null != _p)
                    {
                        testParameters.AddRange(_p);
                    }
                }
            }
            foreach (TestParameter param in testParameters)
            {
                this.AddLabControls(param);
            }

        }
        /// <summary>
        /// Adds the lab controls.
        /// </summary>
        /// <param name="param">The parameter.</param>
        void AddLabControls(TestParameter param)
        {
            Panel thePnl = new Panel();
            thePnl.ID = "pnl-Lab-" + param.Id.ToString();
            thePnl.Height = 20;
            thePnl.Width = 850;
            thePnl.Controls.Clear();

            Label theSpace = new Label();
            theSpace.ID = "theSpaceLab" + param.Id.ToString();
            theSpace.Width = 5;
            theSpace.Text = "";
            thePnl.Controls.Add(theSpace);

            Label theParamName = new Label();
            theParamName.ID = "theNameLab" + param.Id.ToString();
            theParamName.Width = 400; //140;
            theParamName.Text = param.Name;
            thePnl.Controls.Add(theParamName);

            Label theSpace2 = new Label();
            theSpace2.ID = "theSpace2Lab" + param.Id.ToString();
            theSpace2.Width = 20;
            theSpace2.Text = "";
            thePnl.Controls.Add(theSpace2);
            if (param.DataType == "TEXT")
            {
                TextBox theLabResult = new TextBox();
                theLabResult.ID = "LabResult" + param.Id.ToString();
                theLabResult.Width = 320;
                thePnl.Controls.Add(theLabResult);
            }
            else if (param.DataType == "NUMERIC")
            {
                TextBox theLabResult = new TextBox();
                theLabResult.ID = "LabResult" + param.Id.ToString();
                theLabResult.Width = 120;
                thePnl.Controls.Add(theLabResult);

                List<ParameterResultConfig> config = labMgr.GetParameterConfig(param.Id);

                theLabResult.Attributes.Add("onkeyup", "chkDecimal('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theLabResult.ClientID + "');");


            }
            else if (param.DataType == "SELECTLIST")
            {
                List<ParameterResultOption> options = labMgr.GetParameterResultOption(param.Id);

                DropDownList theddlLabResult = new DropDownList();
                theddlLabResult.ID = "ddlLabResult" + param.Id.ToString();
                theddlLabResult.Width = 120;
                theddlLabResult.DataValueField = "Id";
                theddlLabResult.DataTextField = "Text";
                theddlLabResult.DataSource = options;
                theddlLabResult.DataBind();
                theddlLabResult.Items.Insert(0, "Select");
                thePnl.Controls.Add(theddlLabResult);
            }
            else
            {
                return;
            }
            Label theSpace3 = new Label();
            theSpace3.ID = "theSpace3Lab" + param.Id.ToString();
            theSpace3.Width = 20;
            theSpace3.Text = "";
            thePnl.Controls.Add(theSpace3);

            Label theTestId = new Label();
            theTestId.ID = "lblTestIdLab" + param.Id.ToString() + "=" + param.LabTestId.ToString();
            theTestId.Text = "";
            thePnl.Controls.Add(theTestId);
            DIVCustomItem.Controls.Add(thePnl);

        }
       
        
        private void BindCustomControls(DataRow theDR)
        {
            try
            {
                Panel thePnl = new Panel();
                thePnl.ID = "pnl-Lab-" + theDR["SubTestId"].ToString();
                thePnl.Height = 20;
                thePnl.Width = 850;
                thePnl.Controls.Clear();

                /////// Space//////
                Label theSpace = new Label();
                theSpace.ID = "theSpaceLab" + theDR["SubTestId"].ToString();
                theSpace.Width = 5;
                theSpace.Text = "";
                thePnl.Controls.Add(theSpace);

                ////////////////////
                Label theTestName = new Label();
                theTestName.ID = "theNameLab" + theDR["SubTestId"].ToString();
                theTestName.Width = 400; //140;
                theTestName.Text = theDR["SubTestName"].ToString();
                thePnl.Controls.Add(theTestName);

                Label theSpace2 = new Label();
                theSpace2.ID = "theSpace2Lab" + theDR["SubTestId"].ToString();
                theSpace2.Width = 20;
                theSpace2.Text = "";
                thePnl.Controls.Add(theSpace2);

                if (ViewState["LabRanges"] == null)
                    ViewState["LabRanges"] = theDSLabs;

                DataSet theDSselectList = (DataSet)ViewState["LabRanges"];
                DataView theDVselectList = new DataView(theDSselectList.Tables[1]);
                theDVselectList.RowFilter = "SubTestId = " + theDR["SubTestId"].ToString();
                if (theDVselectList.Count != 0)
                {
                    DropDownList theddlLabResult = new DropDownList();
                    theddlLabResult.ID = "ddlLabResult" + theDR["SubTestId"].ToString();
                    theddlLabResult.Width = 120;
                    for (int i = 0; i < theDVselectList.Count; i++)
                    {
                        theddlLabResult.Items.Add(theDVselectList[i].Row["Result"].ToString());
                    }
                    theddlLabResult.Items.Insert(0, "Select");
                    thePnl.Controls.Add(theddlLabResult);
                }
                else
                {
                    TextBox theLabResult = new TextBox();
                    theLabResult.ID = "LabResult" + theDR["SubTestId"].ToString();
                    theLabResult.Width = 120;
                    thePnl.Controls.Add(theLabResult);

                    DataView theDV = new DataView(theDSselectList.Tables[0]);

                    //  theDV.RowFilter = "SubTestNameLab = '" + theDR["SubTestName"].ToString() + "'";
                    theDV.RowFilter = "SubTestName = '" + theDR["SubTestName"].ToString() + "'";
                    if (theDV.Count != 0)
                    {
                        theLabResult.Attributes.Add("onkeyup", "chkDecimal('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theLabResult.ClientID + "'); AddBoundary('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theLabResult.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                        theLabResult.Attributes.Add("onblur", "CheckValue('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theLabResult.ClientID + "','" + theDV[0]["MinBoundaryValue"] + "');CheckValue2('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theLabResult.ClientID + "','" + theDV[0]["MaxBoundaryValue"] + "')");
                    }
                }

                ////////////Space////////////////////////
                Label theSpace3 = new Label();
                theSpace3.ID = "theSpace3Lab" + theDR["SubTestId"].ToString();
                theSpace3.Width = 20;
                theSpace3.Text = "";
                thePnl.Controls.Add(theSpace3);

                Label theTestId = new Label();
                theTestId.ID = "lblTestIdLab" + theDR["SubTestId"].ToString() + "=" + theDR["LabTestId"].ToString();
                theTestId.Text = "";
                thePnl.Controls.Add(theTestId);
                DIVCustomItem.Controls.Add(thePnl);
            }
            catch { throw; }
            finally { }
        }


        private DataTable UserList
        {
            get
            {
                DataSet theDS = new DataSet();
                theDS.ReadXml(MapPath("..\\XMLFiles\\ALLMasters.con"));

                IQCareUtils theUtils = new IQCareUtils();
                DataTable dt = new DataTable("Users");
                if (theDS.Tables["Users"] != null)
                {
                    DataView theDV = new DataView(theDS.Tables["Users"]);
                    if (theDV.Table != null)
                    {
                        dt = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    }
                }
                return dt;
            }
        }
        private int EmployeeId
        {
            get
            {
                return Convert.ToInt32(Session["AppUserEmployeeId"].ToString());
            }
        }
       
        private void BindDropdown(DropDownList dropdownId)
        {
            // DataSet theDS = new DataSet();
            //  theDS.ReadXml(MapPath("..\\XMLFiles\\ALLMasters.con"));
            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();

            DataView theDV = new DataView(this.UserList);

            ddSignature.DataSource = null;
            ddSignature.Items.Clear();

            // if (theDS.Tables["Mst_Employee"] != null)
            // {
            //  DataView theDV = new DataView(theDS.Tables["Mst_Employee"]);
            string rowFilter = "EmployeeId Is Not Null Or EmployeeId > 0 And UserDeleteFlag = 0";

            if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
            {
                if (this.EmployeeId > 0)
                {
                    rowFilter = "EmployeeId = " + this.EmployeeId;
                }
            }
            theDV.RowFilter = rowFilter;
            DataTable theDT = theUtils.CreateTableFromDataView(theDV);
            BindManager.BindCombo(dropdownId, theDT, "Name", "UserId", "", this.UserId.ToString());

        }
        private void BindDropdown(DropDownList DropDownID, string userId)
        {

            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();
            //if (theDS.Tables["Mst_Employee"] != null)
            //{
            DataView theDV = new DataView(this.UserList);

            string rowFilter = "EmployeeId Is Not Null Or EmployeeId > 0 And UserDeleteFlag = 0";
            if (this.EmployeeId > 0)
            {
                userId = this.UserId.ToString();
            }
            if (userId != "")
            {
                rowFilter = "UserId = " + userId;
            }
            theDV.RowFilter = rowFilter;
            if (theDV.Table != null)
            {
                DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);

                BindManager.BindCombo(DropDownID, theDT, "Name", "UserId", "", userId);
                ListItem item = DropDownID.Items.FindByValue(userId);
                if (item == null)
                {
                    item = DropDownID.Items.FindByValue(this.UserId.ToString());
                }
                if (item != null)
                {
                    item.Selected = true;
                }
            }

        }

        private void BindDrugControls(int DrugId, int Generic, int DrugType, int Flag)
        {
            #region "ARV Drugs"

            if ((DrugType == 37 || DrugType == 36) && Flag == 0) //// DrugType-36 OI Med,37 ARV Med////
            {
                Panel thePnl = new Panel();
                if (Generic == 0)
                {
                    thePnl.ID = "pnlDrugARV_" + DrugId;
                }
                else
                {
                    thePnl.ID = "pnlGenericARV_" + Generic;
                }
                thePnl.Height = 20;
                thePnl.Width = 840;
                thePnl.Controls.Clear();

                Label lblStSp = new Label();
                lblStSp.Width = 5;
                lblStSp.ID = "stSpace" + DrugId + "" + Generic;
                lblStSp.Text = "";
                thePnl.Controls.Add(lblStSp);

                DataView theDV;
                DataSet theDS = (DataSet)Session["AllData"];
                DataTable DT = new DataTable();
                if (Generic == 0)
                {
                    theDV = new DataView(theDS.Tables[10]);
                    if (DrugId.ToString().LastIndexOf("8888") > 0)
                    {
                        DrugId = Convert.ToInt32(DrugId.ToString().Substring(0, DrugId.ToString().Length - 4));
                    }
                    theDV.RowFilter = "Drug_Pk = " + DrugId;
                }
                else
                {
                    theDV = new DataView(theDS.Tables[11]);
                    if (DrugId.ToString().LastIndexOf("9999") > 0)
                    {
                        DrugId = Convert.ToInt32(DrugId.ToString().Substring(0, DrugId.ToString().Length - 4));
                    }
                    theDV.RowFilter = "GenericId = " + Generic;
                }

                Label theDrugNm = new Label();
                if (Generic == 0)
                {
                    theDrugNm.ID = "ARVdrgNm" + DrugId;
                }
                else
                {
                    theDrugNm.ID = "ARVGenericNm" + Generic;
                }
                theDrugNm.Text = theDV[0][1].ToString();
                theDrugNm.Width = 400;
                thePnl.Controls.Add(theDrugNm);

                /////// Space//////
                Label theSpace = new Label();
                theSpace.ID = "theSpace_" + DrugId + "" + Generic;
                theSpace.Width = 10;
                theSpace.Text = "";
                ////////////////////

                thePnl.Controls.Add(theSpace);

                BindFunctions theBindMgr = new BindFunctions();
                DropDownList theDrugFrequency = new DropDownList();
                if (Generic == 0)
                {
                    theDrugFrequency.ID = "ARVdrgFrequency" + DrugId;
                }
                else { theDrugFrequency.ID = "ARVGenericFrequency" + Generic; }
                theDrugFrequency.Width = 80;

                #region "BindCombo"

                DataTable theDTF = new DataTable();
                DataView theDVFrequency = new DataView(theDS.Tables[21]);
                DataTable theDTFrequency = new DataTable();
                if (theDVFrequency.Count > 0)
                {
                    IQCareUtils theUtils = new IQCareUtils();
                    theDTFrequency = theUtils.CreateTableFromDataView(theDVFrequency);
                    theBindMgr.BindCombo(theDrugFrequency, theDTFrequency, "FrequencyName", "FrequencyId");
                }

                #endregion "BindCombo"

                thePnl.Controls.Add(theDrugFrequency);

                ////////////Space////////////////////////
                Label theSpace2 = new Label();
                theSpace2.ID = "theSpace2" + DrugId + "" + Generic;
                theSpace2.Width = 15;
                theSpace2.Text = "";
                thePnl.Controls.Add(theSpace2);
                ////////////////////////////////////////

                TextBox theQtyPrescribed = new TextBox();
                if (Generic == 0)
                {
                    theQtyPrescribed.ID = "ARVdrgQtyPrescribed" + DrugId;
                }
                else
                {
                    theQtyPrescribed.ID = "ARVGenericQtyPrescribed" + Generic;
                }
                theQtyPrescribed.Width = 100;
                thePnl.Controls.Add(theQtyPrescribed);
                theQtyPrescribed.Attributes.Add("onkeyup", "chkNumeric('ctl00_IQCareContentPlaceHolder_" + theQtyPrescribed.ClientID + "')");

                ////////////Space////////////////////////
                Label theSpace4 = new Label();
                theSpace4.ID = "theSpace4" + DrugId + "" + Generic;
                theSpace4.Width = 20;
                theSpace4.Text = "";
                thePnl.Controls.Add(theSpace4);
                ////////////////////////////////////////

                TextBox theQtyDispensed = new TextBox();
                if (Generic == 0)
                {
                    theQtyDispensed.ID = "ARVdrgQtyDispensed" + DrugId;
                }
                else
                {
                    theQtyDispensed.ID = "ARVGenericQtyDispensed" + Generic;
                }
                theQtyDispensed.Width = 100;
                if (Session["SCMModule"] != null)
                    theQtyDispensed.Enabled = false;
                thePnl.Controls.Add(theQtyDispensed);
                theQtyDispensed.Attributes.Add("onkeyup", "chkNumeric('ctl00_IQCareContentPlaceHolder_" + theQtyDispensed.ClientID + "')");

                ////////////Space////////////////////////
                Label theSpace5 = new Label();
                theSpace5.ID = "theSpace5" + DrugId + "" + Generic;
                theSpace5.Width = 20;
                theSpace5.Text = "";
                thePnl.Controls.Add(theSpace5);
                ////////////////////////////////////////
                CheckBox theFinChk = new CheckBox();
                if (Generic == 0)
                {
                    theFinChk.ID = "ARVDrugFinChk-" + DrugId;
                }
                else { theFinChk.ID = "ARVGenericFinChk-" + Generic; }
                theFinChk.Width = 10;
                theFinChk.Text = "";
                thePnl.Controls.Add(theFinChk);
                ////////////Space///////////////////////
                Label theSpace6 = new Label();
                theSpace6.ID = "theSpace6" + DrugId + "" + Generic;
                theSpace6.Width = 20;
                theSpace6.Text = "";
                thePnl.Controls.Add(theSpace6);
                DIVCustomItem.Controls.Add(thePnl);
            }
            else if ((DrugType == 37 || DrugType == 36) && Flag == 1)
            {
                Panel thePnl = new Panel();
                if (Generic == 0)
                {
                    thePnl.ID = "pnlDrugARV_" + DrugId;
                }
                else
                {
                    thePnl.ID = "pnlGenericARV_" + Generic;
                }
                thePnl.Height = 20;
                thePnl.Width = 840;
                thePnl.Controls.Clear();

                Label lblStSp = new Label();
                lblStSp.Width = 5;
                lblStSp.ID = "stSpace" + DrugId + "" + Generic;
                lblStSp.Text = "";
                thePnl.Controls.Add(lblStSp);

                DataView theDV;
                DataSet theDS = (DataSet)Session["AllData"];
                DataTable DT = new DataTable();
                if (Generic == 0)
                {
                    theDV = new DataView(theDS.Tables[10]);
                    if (DrugId.ToString().LastIndexOf("8888") > 0)
                    {
                        DrugId = Convert.ToInt32(DrugId.ToString().Substring(0, DrugId.ToString().Length - 4));
                    }
                    theDV.RowFilter = "Drug_Pk = " + DrugId;
                }
                else
                {
                    theDV = new DataView(theDS.Tables[11]);
                    if (DrugId.ToString().LastIndexOf("9999") > 0)
                    {
                        DrugId = Convert.ToInt32(DrugId.ToString().Substring(0, DrugId.ToString().Length - 4));
                    }
                    theDV.RowFilter = "GenericId = " + Generic;
                }

                Label theDrugNm = new Label();
                if (Generic == 0)
                {
                    theDrugNm.ID = "ARVdrgNm" + DrugId;
                }
                else
                {
                    theDrugNm.ID = "ARVGenericNm" + Generic;
                }
                theDrugNm.Text = theDV[0][1].ToString();
                theDrugNm.Width = 400;
                thePnl.Controls.Add(theDrugNm);

                /////// Space//////
                Label theSpace = new Label();
                theSpace.ID = "theSpace_" + DrugId + "" + Generic;
                theSpace.Width = 10;
                theSpace.Text = "";
                ////////////////////

                thePnl.Controls.Add(theSpace);

                BindFunctions theBindMgr = new BindFunctions();
                DropDownList theDrugFrequency = new DropDownList();
                if (Generic == 0)
                {
                    theDrugFrequency.ID = "ARVdrgFrequency" + DrugId;
                }
                else { theDrugFrequency.ID = "ARVGenericFrequency" + Generic; }
                theDrugFrequency.Width = 80;

                #region "BindCombo"

                DataTable theDTF = new DataTable();
                DataView theDVFrequency = new DataView(theDS.Tables[21]);

                DataTable theDTFrequency = new DataTable();
                if (theDVFrequency.Count > 0)
                {
                    IQCareUtils theUtils = new IQCareUtils();
                    theDTFrequency = theUtils.CreateTableFromDataView(theDVFrequency);
                    theBindMgr.BindCombo(theDrugFrequency, theDTFrequency, "FrequencyName", "FrequencyId");
                }

                #endregion "BindCombo"

                thePnl.Controls.Add(theDrugFrequency);

                ////////////Space////////////////////////
                Label theSpace2 = new Label();
                theSpace2.ID = "theSpace2" + DrugId + "" + Generic; ;
                theSpace2.Width = 15;
                theSpace2.Text = "";
                thePnl.Controls.Add(theSpace2);
                ////////////////////////////////////////
                TextBox theQtyPrescribed = new TextBox();
                if (Generic == 0)
                {
                    theQtyPrescribed.ID = "ARVdrgQtyPrescribed" + DrugId;
                }
                else
                {
                    theQtyPrescribed.ID = "ARVGenericQtyPrescribed" + Generic;
                }
                theQtyPrescribed.Width = 100;
                //theQtyPrescribed.Load += new EventHandler(Control_Load);
                thePnl.Controls.Add(theQtyPrescribed);
                theQtyPrescribed.Attributes.Add("onkeyup", "chkNumeric('ctl00_IQCareContentPlaceHolder_" + theQtyPrescribed.ClientID + "')");

                ////////////Space////////////////////////
                Label theSpace4 = new Label();
                theSpace4.ID = "theSpace4" + DrugId + "" + Generic;
                theSpace4.Width = 20;
                theSpace4.Text = "";
                thePnl.Controls.Add(theSpace4);
                ////////////////////////////////////////

                TextBox theQtyDispensed = new TextBox();
                if (Generic == 0)
                {
                    theQtyDispensed.ID = "ARVdrgQtyDispensed" + DrugId;
                }
                else
                {
                    theQtyDispensed.ID = "ARVGenericQtyDispensed" + Generic;
                }
                theQtyDispensed.Width = 100;
                if (Session["SCMModule"] != null)
                    theQtyDispensed.Enabled = false;
                thePnl.Controls.Add(theQtyDispensed);
                theQtyDispensed.Attributes.Add("onkeyup", "chkNumeric('ctl00_IQCareContentPlaceHolder_" + theQtyDispensed.ClientID + "')");

                ////////////Space////////////////////////
                Label theSpace5 = new Label();
                theSpace5.ID = "theSpace5" + DrugId + "" + Generic;
                theSpace5.Width = 20;
                theSpace5.Text = "";
                thePnl.Controls.Add(theSpace5);
                ////////////////////////////////////////
                CheckBox theFinChk = new CheckBox();
                if (Generic == 0)
                {
                    theFinChk.ID = "ARVDrugFinChk" + DrugId;
                }
                else { theFinChk.ID = "ARVGenericFinChk" + Generic; }
                theFinChk.Width = 10;
                theFinChk.Text = "";
                thePnl.Controls.Add(theFinChk);
                ////////////Space///////////////////////
                Label theSpace6 = new Label();
                theSpace6.ID = "theSpace6" + DrugId + "" + Generic;
                theSpace6.Width = 20;
                theSpace6.Text = "";
                thePnl.Controls.Add(theSpace6);
                DIVCustomItem.Controls.Add(thePnl);
            }

            #endregion "ARV Drugs"

            #region "Non ARV Drugs"

            else
            {
                Panel thePnl = new Panel();
                thePnl.Controls.Clear();
                if (Generic == 0)
                {
                    thePnl.ID = "pnlDrug" + DrugId;
                }
                else
                {
                    thePnl.ID = "pnlGeneric" + Generic;
                }
                thePnl.Height = 20;
                thePnl.Width = 840;
                thePnl.Controls.Clear();

                Label lblStSp = new Label();
                lblStSp.Width = 5;
                lblStSp.ID = "stSpace" + DrugId + "^" + Generic;
                lblStSp.Text = "";
                thePnl.Controls.Add(lblStSp);

                DataView theDV;
                DataSet theDS = (DataSet)(DataSet)Session["AllData"];
                if (Generic == 0)
                {
                    theDV = new DataView(theDS.Tables[10]);
                    theDV.RowFilter = "Drug_Pk = " + DrugId;
                }
                else
                {
                    theDV = new DataView(theDS.Tables[11]);
                    if (DrugId.ToString().LastIndexOf("9999") > 0)
                    {
                        DrugId = Convert.ToInt32(DrugId.ToString().Substring(0, DrugId.ToString().Length - 4));
                    }
                    theDV.RowFilter = "GenericId = " + Generic;
                }

                Label theDrugNm = new Label();
                if (Generic == 0)
                {
                    theDrugNm.ID = "DrugNm" + DrugId;
                }
                else
                {
                    theDrugNm.ID = "GenericNm" + Generic;
                }

                theDrugNm.Text = theDV[0][1].ToString();
                theDrugNm.Width = 350;
                thePnl.Controls.Add(theDrugNm);

                /////// Space//////
                Label theSpace = new Label();
                theSpace.ID = "theSpace" + DrugId + "^" + Generic;
                theSpace.Width = 20;
                theSpace.Text = "";
                thePnl.Controls.Add(theSpace);
                ////////////////////

                BindFunctions theBindMgr = new BindFunctions();

                DropDownList theFrequency = new DropDownList();
                if (Generic == 0)
                {
                    theFrequency.ID = "drugFrequency" + DrugId;
                }
                else
                {
                    theFrequency.ID = "GenericFrequency" + Generic;
                }
                theFrequency.Width = 80;
                DataTable DTFreq = new DataTable();
                DTFreq = theDS.Tables[12];
                theBindMgr.BindCombo(theFrequency, DTFreq, "FrequencyName", "FrequencyId");
                thePnl.Controls.Add(theFrequency);

                /////// Space//////
                Label theSpace3 = new Label();
                theSpace3.ID = "theSpace3*" + DrugId + "^" + Generic;
                theSpace3.Width = 10;
                theSpace3.Text = "";
                thePnl.Controls.Add(theSpace3);
                ////////////////////

                TextBox theQtyPrescribed = new TextBox();
                if (Generic == 0)
                {
                    theQtyPrescribed.ID = "drugQtyPrescribed" + DrugId;
                }
                else
                {
                    theQtyPrescribed.ID = "genericQtyPrescribed" + Generic;
                }
                theQtyPrescribed.Width = 90;
                theQtyPrescribed.Text = "";
                tabContainer.ID = "TAB";
                theQtyPrescribed.Attributes.Add("onkeyup", "chkDecimal('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theQtyPrescribed.ClientID + "')");
                //theQtyPrescribed.Load += new EventHandler(Control_Load);
                thePnl.Controls.Add(theQtyPrescribed);

                ////////////Space////////////////////////
                Label theSpace5 = new Label();
                theSpace5.ID = "theSpace5*" + DrugId + "^" + Generic;
                theSpace5.Width = 10;
                theSpace5.Text = "";
                thePnl.Controls.Add(theSpace5);
                ////////////////////////////////////////

                TextBox theQtyDispensed = new TextBox();
                if (Generic == 0)
                {
                    theQtyDispensed.ID = "drugQtyDispensed" + DrugId;
                }
                else { theQtyDispensed.ID = "genericQtyDispensed" + Generic; }
                theQtyDispensed.Width = 90;
                theQtyDispensed.Text = "";
                tabContainer.ID = "TAB";
                theQtyDispensed.Attributes.Add("onkeyup", "chkDecimal('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theQtyDispensed.ClientID + "')");
                //theQtyDispensed.Load += new EventHandler(Control_Load);
                if (Session["SCMModule"] != null)
                    theQtyDispensed.Enabled = false;
                thePnl.Controls.Add(theQtyDispensed);

                ////////////Space////////////////////////
                Label theSpace6 = new Label();
                theSpace6.ID = "theSpace6*" + DrugId + "^" + Generic;
                theSpace6.Width = 25;
                theSpace6.Text = "";
                thePnl.Controls.Add(theSpace6);
                ////////////////////////////////////////

                CheckBox theFinChk = new CheckBox();
                if (Generic == 0)
                {
                    theFinChk.ID = "FinChkDrug" + DrugId;
                }
                else
                {
                    theFinChk.ID = "FinChkGeneric" + Generic;
                }
                theFinChk.Width = 10;
                theFinChk.Text = "";
                thePnl.Controls.Add(theFinChk);

                ////////////Space////////////////////////
                Label theSpace7 = new Label();
                theSpace7.ID = "theSpace7*" + DrugId + "^" + Generic;
                theSpace7.Width = 15;
                theSpace7.Text = "";
                thePnl.Controls.Add(theSpace7);
                ////////////////////////////////////////
                DIVCustomItem.Controls.Add(thePnl);
            }

            #endregion "Non ARV Drugs"
        }

        private void BindGridView(string section, Control theControl, DataTable dt)
        {
            foreach (object obj in theControl.Controls)
            {
                if (obj is AjaxControlToolkit.TabPanel)
                {
                    AjaxControlToolkit.TabPanel tabPanel = (AjaxControlToolkit.TabPanel)obj;
                    foreach (object ctrl in tabPanel.Controls)
                    {
                        if (ctrl is Control)
                        {
                            Control c = (Control)ctrl;
                            foreach (object x in c.Controls)
                            {
                                if (x.GetType() == typeof(GridView))
                                {
                                    if (((GridView)x).ID.Contains("Dview_" + section))
                                    {
                                        ((GridView)x).DataSource = dt;
                                        ((GridView)x).DataBind();
                                    }
                                }
                            }
                        }
                    }
                }
            }


        }

        private void BindTime12ControlValue(string[] TimeMinute, string ID)
        {
            string[] AMPM = new string[1]; ;
            if (TimeMinute[1].Contains("AM"))
            {
                AMPM = TimeMinute[1].Replace(' ', ':').Split(':');
            }
            if (TimeMinute[1].Contains("PM"))
            {
                AMPM = TimeMinute[1].Replace(' ', ':').Split(':');
            }

            foreach (object obj in tabContainer.Controls)
            {
                if (obj is AjaxControlToolkit.TabPanel)
                {
                    AjaxControlToolkit.TabPanel tabPanel = (AjaxControlToolkit.TabPanel)obj;
                    foreach (object ctrl in tabPanel.Controls)
                    {
                        if (ctrl is Control)
                        {
                            Control c = (Control)ctrl;
                            foreach (object x in c.Controls)
                            {
                                if (x.GetType() == typeof(DropDownList))
                                {
                                    if (((DropDownList)x).ID == ID && ((DropDownList)x).ID.Contains("AM"))
                                    {
                                        ((DropDownList)x).SelectedValue = Convert.ToString(AMPM[1]);
                                    }
                                    else if (((DropDownList)x).ID == ID && ((DropDownList)x).ID.Contains("PM"))
                                    {
                                        ((DropDownList)x).SelectedValue = Convert.ToString(AMPM[1]);
                                    }
                                    else if (((DropDownList)x).ID == ID && ((DropDownList)x).ID.Contains("Min"))
                                    {
                                        ((DropDownList)x).SelectedValue = Convert.ToString(AMPM[0]);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void BindTime24ControlValue(string[] TimeMinute, string ID)
        {
            foreach (object obj in tabContainer.Controls)
            {
                if (obj is AjaxControlToolkit.TabPanel)
                {
                    AjaxControlToolkit.TabPanel tabPanel = (AjaxControlToolkit.TabPanel)obj;
                    foreach (object ctrl in tabPanel.Controls)
                    {
                        if (ctrl is Control)
                        {
                            Control c = (Control)ctrl;
                            foreach (object x in c.Controls)
                            {
                                if (x.GetType() == typeof(DropDownList))
                                {
                                    if (((DropDownList)x).ID == ID)
                                    {
                                        ((DropDownList)x).SelectedValue = Convert.ToString(TimeMinute[1]);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        private void BindIQLookupTextBoxValue(ref string strTableName, IQLookupTextBox iqTextBox, ref int tableCount, ref int columnCount, ref DataSet valuesDataset)
        {

            string[] remStr = iqTextBox.ID.Split('-');
            string str = remStr[0] + "-" + remStr[1] + "-" + remStr[2];
            string strTabSig = remStr[0] + "-" + remStr[1] + "-" + remStr[2] + "-" + remStr[4];

            string strName = "TXTLookup-" + valuesDataset.Tables[tableCount].Columns[columnCount].ToString() + "-" + strTableName;

            if (strName == str)
            {
                if (valuesDataset.Tables[tableCount].Rows.Count > 0)
                {
                    iqTextBox.SelectedValue = Convert.ToString(valuesDataset.Tables[tableCount].Rows[0][columnCount]);
                    ILookupService lkMgr = (ILookupService)ObjectFactory.CreateInstance("BusinessProcess.Lookup.BLookup, BusinessProcess.Lookup");
                    Entities.Lookup.Item item = lkMgr.GetLookUpItem(Convert.ToInt32(iqTextBox.SelectedValue), iqTextBox.LookupName, iqTextBox.LookupCategory);
                    iqTextBox.ValueText = item.Name;


                    DataView theDVConditionalField = new DataView(((DataSet)Session["AllData"]).Tables[17]);
                    string[] theId = iqTextBox.ID.Split('-');
                    theDVConditionalField.RowFilter = "ConFieldId=" + theId.GetValue(3);
                    if (theDVConditionalField.Count > 0)
                    {
                        //EventArgs s = new EventArgs();
                        //ddlSelectList_SelectedIndexChanged((DropDownList)x, s);
                    }
                }
            }
        }
        private void BindValue(int PatientID, int VisitID, int LocationID, Control theControl, DataTable DTTabId)
        {
            ICustomForm MgrBindValue = (ICustomForm)ObjectFactory.CreateInstance(ObjFactoryParameter);
            AjaxControlToolkit.TabContainer container = (AjaxControlToolkit.TabContainer)tabContainer;
            DataTable theDT = SetControlIDs(container);
            DataTable TempDT = theDT.DefaultView.ToTable(true, "TableName").Copy();
            String GetVisitDate = "Select VisitDate, Signature,DataQuality from ord_visit where Ptn_Pk=" + PatientID + " and Visit_Id=" + VisitID + " and LocationID=" + LocationID + "";
            DataSet theDS = new DataSet();
            DataSet TmpDS = MgrBindValue.Common_GetSaveUpdate(GetVisitDate);
            try
            {
                if (!IsPostBack)
                {
                    txtvisitDate.Text = string.Format("{0:dd-MMM-yyyy}", TmpDS.Tables[0].Rows[0]["VisitDate"]);
                    ViewState["VisitDate"] = txtvisitDate.Text;
                    if (Convert.ToInt32(TmpDS.Tables[0].Rows[0]["DataQuality"]) == 1)
                    {
                        btncomplete.CssClass = "greenbutton";
                    }

                    if (TmpDS.Tables[0].Rows[0]["Signature"].ToString() != "")
                    {
                        BindDropdown(ddSignature, TmpDS.Tables[0].Rows[0]["Signature"].ToString());
                        ddSignature.SelectedValue = TmpDS.Tables[0].Rows[0]["Signature"].ToString();
                    }

                    DataTable dtgGetDataView = GetGridTable((DataTable)ViewState["LnkTable"]);
                    //DataTable dtgGetDataView = ((DataTable)ViewState["LnkTable"]).DefaultView.ToTable(true, "FeatureID", "SectionID", "SectionName", "IsGridView", "FeatureName").Copy();
                    DataView dvGetDataView = new DataView(dtgGetDataView);
                    dvGetDataView.RowFilter = "IsGridView = 1";
                    if (dvGetDataView.Count > 0)
                    {
                        foreach (DataRow TempDR in dvGetDataView.ToTable().Rows)
                        {
                            string GetValue = "";
                            string TableName = "DTL_CUSTOMFORM_" + TempDR["SectionName"].ToString() + "_" + TempDR["FeatureName"].ToString().Trim().Replace(' ', '_');
                            GetValue = "Select * from [" + TableName + "] where FormID=" + TempDR["FeatureID"].ToString() + "and   SectionID=" + TempDR["SectionID"].ToString() + " and Ptn_pk=" + PatientID + " and Visit_Pk=" + VisitID + " and LocationId=" + LocationID + "";
                            DataSet TempDS = MgrBindValue.Common_GetSaveUpdate(GetValue);

                            foreach (object obj in container.Controls)
                            {
                                if (obj is AjaxControlToolkit.TabPanel)
                                {
                                    AjaxControlToolkit.TabPanel tabPanel = (AjaxControlToolkit.TabPanel)obj;
                                    foreach (object ctrl in tabPanel.Controls)
                                    {
                                        if (ctrl is Control)
                                        {
                                            Control c = (Control)ctrl;
                                            foreach (object y in c.Controls)
                                            {
                                                if (y.GetType() == typeof(GridView))
                                                {
                                                    if (((GridView)y).ID.Contains("Dview_" + TempDR["SectionID"].ToString()))
                                                    {
                                                        if (((GridView)y).DataSource == null)
                                                        {
                                                            TempDS.Tables[0].Columns.Remove("ID");
                                                            TempDS.Tables[0].Columns.Remove("Ptn_pk");
                                                            TempDS.Tables[0].Columns.Remove("Visit_Pk");
                                                            TempDS.Tables[0].Columns.Remove("LocationId");
                                                            TempDS.Tables[0].Columns.Remove("UserId");
                                                            TempDS.Tables[0].Columns.Remove("SectionId");
                                                            TempDS.Tables[0].Columns.Remove("FormID");
                                                            TempDS.Tables[0].Columns.Remove("CreateDate");
                                                            TempDS.Tables[0].Columns.Remove("UpdateDate");

                                                            for (int x = 0; x < TempDS.Tables[0].Columns.Count; x++)
                                                            {
                                                                foreach (DataRow row in ((DataTable)ViewState["gblDTGridViewControls"]).Rows)
                                                                {
                                                                    if (TempDS.Tables[0].Columns[x].ColumnName == row["FieldName"].ToString())
                                                                    {
                                                                        TempDS.Tables[0].Columns[x].ColumnName = row["FieldLabel"].ToString();
                                                                    }
                                                                    if (TempDS.Tables[0].Columns[x].DataType.ToString() == "System.DateTime")
                                                                    {
                                                                        //TempDS.Tables[0].Columns[x]
                                                                    }
                                                                }
                                                            }
                                                            DataTable dtGetRecord = new DataTable();
                                                            dtGetRecord = TempDS.Tables[0].Clone();
                                                            for (int q = 0; q < dtGetRecord.Columns.Count; q++)
                                                            {
                                                                if (dtGetRecord.Columns[q].DataType.ToString() == "System.Int32")
                                                                {
                                                                    dtGetRecord.Columns[q].DataType = Type.GetType("System.String");
                                                                }
                                                            }
                                                            string SectionID = TempDR["SectionID"].ToString();
                                                            DataTable dtlnktable = ((DataTable)ViewState["LnkTable"]).Copy();

                                                            for (int i = 0; i < TempDS.Tables[0].Rows.Count; i++)
                                                            {
                                                                DataRow dr = dtGetRecord.NewRow();
                                                                for (int x = 0; x < TempDS.Tables[0].Columns.Count; x++)
                                                                {
                                                                    bool isddl = false;

                                                                    DataView dvGridViewDDL = new DataView(dtlnktable);
                                                                    string COLNAME = TempDS.Tables[0].Columns[x].ColumnName.ToString();
                                                                    dvGridViewDDL.RowFilter = "FieldLabel= '" + COLNAME + "' AND IsGridView = 1 and  ControlId = 4";
                                                                    if (dvGridViewDDL.Count > 0)
                                                                    {
                                                                        if (ViewState["GridViewDDL-" + dvGridViewDDL[0]["FieldName"].ToString()] != null)
                                                                        {
                                                                            DataTable dtDDL = new DataTable();
                                                                            dtDDL = (DataTable)ViewState["GridViewDDL-" + dvGridViewDDL[0]["FieldName"].ToString()];
                                                                            DataView dvddl = new DataView(dtDDL);
                                                                            string DDLVALUE = TempDS.Tables[0].Rows[i][x].ToString();
                                                                            if (!string.IsNullOrEmpty(DDLVALUE))
                                                                            {
                                                                                dvddl.RowFilter = "ID  ='" + DDLVALUE + "'";
                                                                                DataTable dtdlnew = dvddl.ToTable();
                                                                                if (dtdlnew.Rows.Count > 0)
                                                                                {
                                                                                    dr[x] = dvddl[0]["Name"];
                                                                                }
                                                                                else
                                                                                {
                                                                                    dr[x] = DBNull.Value;
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                dr[x] = DBNull.Value;
                                                                            }
                                                                            isddl = true;
                                                                        }
                                                                    }
                                                                    if (!isddl)
                                                                    {
                                                                        if (TempDS.Tables[0].Columns[x].DataType.ToString() == "System.DateTime")
                                                                        {
                                                                            if (Convert.ToString(TempDS.Tables[0].Rows[i][x]).Contains("1/1/1900"))
                                                                            {
                                                                                dr[x] = DBNull.Value;
                                                                            }
                                                                            else
                                                                            {
                                                                                dr[x] = TempDS.Tables[0].Rows[i][x];
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            dr[x] = TempDS.Tables[0].Rows[i][x];
                                                                        }
                                                                    }
                                                                }
                                                                dtGetRecord.Rows.Add(dr);
                                                            }
                                                            ViewState["GridCache_" + TempDR["SectionID"].ToString()] = dtGetRecord;
                                                            BindGridView(TempDR["SectionID"].ToString(), container, dtGetRecord);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                StringBuilder SBGetValue = new StringBuilder();
                foreach (DataRow TempDR in TempDT.Rows)
                {
                    if (TempDR["TableName"].ToString() == "DTL_CUSTOMFIELD")
                    {
                        string TableName = "DTL_FBCUSTOMFIELD_" + theHeader.InnerText.Replace(' ', '_');
                        SBGetValue.Append("Select * from [" + TableName + "] where Ptn_pk=" + PatientID + " and Visit_Pk=" + VisitID + " and LocationId=" + LocationID + ";");
                    }
                    else if (TempDR["TableName"].ToString().ToUpper() == "DTL_CUSTOMFORM")
                    {
                    }
                    else if (TempDR["TableName"].ToString().ToUpper() == "REGIMEN")
                    {
                    }
                    else
                    {
                        if (Convert.ToString(TempDR["TableName"]) == "dtl_PatientCareEnded".ToUpper())
                        {
                            SBGetValue.Append("Select * from [" + TempDR["TableName"] + "] where Ptn_pk=" + PatientID + " and LocationId=" + LocationID + ";");
                        }
                        else if (Convert.ToString(TempDR["TableName"]) == "dtl_PatientARVInfo".ToUpper() || Convert.ToString(TempDR["TableName"]) == "dtl_PatientContacts".ToUpper())
                        {
                            SBGetValue.Append("Select * from [" + TempDR["TableName"] + "] where Ptn_pk=" + PatientID + " and Visitid=" + VisitID + " and LocationId=" + LocationID + ";");
                        }
                        else if (Convert.ToString(TempDR["TableName"]) == "mst_patient".ToUpper())
                        {
                            SBGetValue.Append("Select * from [" + TempDR["TableName"] + "] where Ptn_pk=" + PatientID + " and LocationId=" + LocationID + ";");
                        }
                        else if (Convert.ToString(TempDR["TableName"]).ToUpper() == "dtl_ICD10Field".ToUpper())
                        {
                            SBGetValue.Append("Select +'%'+Convert(Varchar,ISNULL(BlockId,0)) +'%'+ Convert(Varchar,ISNULL(SubBlockId,0))+'%'+Convert(Varchar,ISNULL(ICDCodeId,0))+'%'+convert(varchar, Predefined)[CodeId],");
                            SBGetValue.Append("Case When Predefined = 0 then '8888'+Convert(Varchar,FieldId) When Predefined = 1 then '9999'+Convert(Varchar,FieldId)end[Field], * from [" + TempDR["TableName"] + "]");
                            SBGetValue.Append("where Ptn_pk=" + PatientID + " and Visit_Pk=" + VisitID + " and LocationId=" + LocationID + ";");
                        }
                        else if (Convert.ToString(TempDR["TableName"]) == "LNK_FORMTABORDVISIT")
                        {
                            SBGetValue.Append("Select * from [" + TempDR["TableName"] + "] where Visit_Pk=" + VisitID + ";");
                        }
                        else
                        {
                            SBGetValue.Append("Select * from [" + TempDR["TableName"] + "] where Ptn_pk=" + PatientID + " and Visit_Pk=" + VisitID + " and LocationId=" + LocationID + ";");
                        }
                    }
                }

                DataSet TempDSValue = new DataSet();
                if (!string.IsNullOrEmpty(SBGetValue.ToString()))
                {
                    TempDSValue = MgrBindValue.Common_GetSaveUpdate(SBGetValue.ToString());
                }
                DataTable theBUssDT = (DataTable)ViewState["BusRule"];
                foreach (DataRow TempDR in TempDT.Rows)
                {
                    if (Convert.ToString(TempDR["TableName"]) == "dtl_ICD10Field")
                    {
                        foreach (DataRow theDRICD10 in TempDSValue.Tables[0].Rows)
                        {
                            foreach (object obj in container.Controls)
                            {
                                if (obj is AjaxControlToolkit.TabPanel)
                                {
                                    AjaxControlToolkit.TabPanel tabPanel = (AjaxControlToolkit.TabPanel)obj;
                                    foreach (object ctrl in tabPanel.Controls)
                                    {
                                        if (ctrl is Control)
                                        {
                                            Control c = (Control)ctrl;
                                            foreach (object x in c.Controls)
                                            {
                                                if (x.GetType() == typeof(CheckBox))
                                                {
                                                    string[] remStr = ((CheckBox)x).ID.Split('-');
                                                    string str = remStr[0] + "-" + remStr[1] + "-" + remStr[2];
                                                    if ("CHKMULTI-" + theDRICD10["Field"] + theDRICD10["CodeId"] + "-" + TempDR["TableName"] == str)//((CheckBox)x).ID.Substring(0, ((CheckBox)x).ID.LastIndexOf('-')))
                                                    {
                                                        ((CheckBox)x).Checked = true;
                                                    }
                                                }
                                                if (x.GetType() == typeof(TextBox))
                                                {
                                                    string[] remStr = ((TextBox)x).ID.Split('-');
                                                    string str = remStr[0] + "-" + remStr[1] + "-" + remStr[2];
                                                    if ("TXTDT-" + theDRICD10["Field"] + theDRICD10["CodeId"].ToString().Replace('%', '^') + "OnSetDate" + "-" + TempDR["TableName"] == str)//((TextBox)x).ID.Substring(0, ((TextBox)x).ID.LastIndexOf('-')))
                                                    {
                                                        string strdateformat = string.Format("{0:dd-MMM-yyyy}", theDRICD10["DateOnSet"]);
                                                        if (strdateformat.Trim() != "01-Jan-1900")
                                                        {
                                                            ((TextBox)x).Text = string.Format("{0:dd-MMM-yyyy}", theDRICD10["DateOnSet"]);
                                                        }
                                                    }

                                                    if ("TXTComment-" + theDRICD10["Field"] + theDRICD10["CodeId"].ToString().Replace('%', '~') + "ICDComment" + "-" + TempDR["TableName"] == str)// ((TextBox)x).ID.Substring(0, ((TextBox)x).ID.LastIndexOf('-')))
                                                    {
                                                        ((TextBox)x).Text = Convert.ToString(theDRICD10["Comments"]);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (TempDSValue.Tables.Count > 0)
                        {
                            for (int n = 0; n < TempDSValue.Tables.Count; n++)
                            {
                                for (int i = 0; i <= TempDSValue.Tables[n].Columns.Count - 1; i++)
                                {
                                    foreach (object obj in container.Controls)
                                    {
                                        if (obj is AjaxControlToolkit.TabPanel)
                                        {
                                            AjaxControlToolkit.TabPanel tabPanel = (AjaxControlToolkit.TabPanel)obj;
                                            foreach (object ctrl in tabPanel.Controls)
                                            {
                                                if (ctrl is Control)
                                                {
                                                    Control c = (Control)ctrl;
                                                    foreach (object x in c.Controls)
                                                    {
                                                        if (x.GetType() == typeof(TextBox))
                                                        {
                                                            string[] remStr = ((TextBox)x).ID.Split('-');
                                                            string str = remStr[0] + "-" + remStr[1] + "-" + remStr[2];
                                                            _tableName = TempDR["TableName"].ToString();

                                                            if ("TXTMulti-" + TempDSValue.Tables[n].Columns[i].ToString() + "-" + TempDR["TableName"] == str)
                                                            {
                                                                if (TempDSValue.Tables[n].Rows.Count > 0)
                                                                {
                                                                    ((TextBox)x).Text = Convert.ToString(TempDSValue.Tables[n].Rows[0][i]);
                                                                }
                                                            }
                                                            if ("TXTSingle-" + TempDSValue.Tables[n].Columns[i].ToString() + "-" + TempDR["TableName"] == str)
                                                            {
                                                                if (TempDSValue.Tables[n].Rows.Count > 0)
                                                                {
                                                                    ((TextBox)x).Text = Convert.ToString(TempDSValue.Tables[n].Rows[0][i]);
                                                                }
                                                            }

                                                            if ("TXT-" + TempDSValue.Tables[n].Columns[i].ToString() + "-" + TempDR["TableName"] == str)
                                                            {
                                                                if (TempDSValue.Tables[n].Rows.Count > 0)
                                                                {
                                                                    ((TextBox)x).Text = Convert.ToString(TempDSValue.Tables[n].Rows[0][i]);

                                                                    if (((TextBox)x).Text != "")
                                                                    {
                                                                        bool FlagVal = CheckAbNormalStatus(((TextBox)x).ID, ((TextBox)x).Text);
                                                                        if (FlagVal == true)
                                                                        {
                                                                            ((TextBox)x).ForeColor = System.Drawing.Color.Red;
                                                                        }
                                                                        else
                                                                        {
                                                                            ((TextBox)x).ForeColor = System.Drawing.Color.Black;
                                                                        }
                                                                    }


                                                                }
                                                            }

                                                            if ("TXTNUM-" + TempDSValue.Tables[n].Columns[i].ToString() + "-" + TempDR["TableName"] == str)
                                                            {
                                                                if (TempDSValue.Tables[n].Rows.Count > 0)
                                                                {
                                                                    ((TextBox)x).Text = Convert.ToString(TempDSValue.Tables[n].Rows[0][i]);

                                                                    if (((TextBox)x).Text != "")
                                                                    {
                                                                        bool FlagVal = CheckAbNormalStatus(((TextBox)x).ID, ((TextBox)x).Text);
                                                                        if (FlagVal == true)
                                                                        {
                                                                            ((TextBox)x).ForeColor = System.Drawing.Color.Red;
                                                                        }
                                                                        else
                                                                        {
                                                                            ((TextBox)x).ForeColor = System.Drawing.Color.Black;
                                                                        }
                                                                    }


                                                                }
                                                            }

                                                            if ("TXTDT-" + TempDSValue.Tables[n].Columns[i].ToString() + "-" + TempDR["TableName"] == str)
                                                            {
                                                                if (TempDSValue.Tables[n].Rows.Count > 0)
                                                                {
                                                                    // Date formate for like "MMM-yyyy"
                                                                    DataView dvBusDtl = theBUssDT.DefaultView;
                                                                    dvBusDtl.RowFilter = "BusRuleId = 21";
                                                                    DataTable dtFilter = dvBusDtl.ToTable();

                                                                    bool isDateValidate = true;
                                                                    if (dtFilter.Rows.Count > 0)
                                                                    {
                                                                        for (int rowcount = 0; rowcount < dtFilter.Rows.Count; rowcount++)
                                                                        {
                                                                            if (dtFilter.Rows[rowcount]["FieldName"].ToString().Trim() == TempDSValue.Tables[n].Columns[i].ToString().Trim() && dtFilter.Rows[rowcount]["TableName"].ToString().Trim() == TempDR["TableName"].ToString().Trim())
                                                                            {
                                                                                isDateValidate = false;
                                                                                ((TextBox)x).Text = String.Format("{0:MMM-yyyy}", TempDSValue.Tables[n].Rows[0][i]);
                                                                            }
                                                                            else
                                                                            {
                                                                                if (isDateValidate == true)
                                                                                {
                                                                                    ((TextBox)x).Text = String.Format("{0:dd-MMM-yyyy}", TempDSValue.Tables[n].Rows[0][i]);
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        ((TextBox)x).Text = string.Format("{0:dd-MMM-yyyy}", TempDSValue.Tables[n].Rows[0][i]);
                                                                    }
                                                                }
                                                            }
                                                            if ("TXTReg-" + TempDSValue.Tables[n].Columns[i].ToString() + "-" + TempDR["TableName"] == str)
                                                            {
                                                                if (TempDSValue.Tables[n].Rows.Count > 0)
                                                                {
                                                                    ((TextBox)x).Text = Convert.ToString(TempDSValue.Tables[n].Rows[0][i]);
                                                                    string[] regimen = ((TextBox)x).ID.Split('=');
                                                                    string[] controlid = regimen[0].Split('-');
                                                                    RegimenSessionSetting(Convert.ToInt32(regimen[1].Remove(regimen[1].IndexOf("-"), regimen[1].Length - regimen[1].IndexOf("-"))), controlid[3].ToString(), ((TextBox)x).Text);
                                                                }
                                                            }
                                                        }
                                                        else if (x.GetType() == typeof(DropDownList))
                                                        {
                                                            string[] remStr = ((DropDownList)x).ID.Split('-');
                                                            string str = remStr[0] + "-" + remStr[1] + "-" + remStr[2];
                                                            string strTabSig = remStr[0] + "-" + remStr[1] + "-" + remStr[2] + "-" + remStr[4];
                                                            if ("SELECTLIST-" + TempDSValue.Tables[n].Columns[i].ToString() + "-" + TempDR["TableName"] == str && ((DropDownList)x).ID.Contains("24Hr"))
                                                            {
                                                                if (TempDSValue.Tables[n].Rows.Count > 0)
                                                                {
                                                                    if (TempDSValue.Tables[n].Rows[0][i] != DBNull.Value)
                                                                    {
                                                                        String[] TimeMinute = Convert.ToString(TempDSValue.Tables[n].Rows[0][i]).Split(':');
                                                                        ((DropDownList)x).SelectedValue = Convert.ToString(TimeMinute[0]);
                                                                        string ID = ((DropDownList)x).ID.Replace("24Hr", "Min");
                                                                        BindTime24ControlValue(TimeMinute, ID);
                                                                    }
                                                                }
                                                            }
                                                            else if ("SELECTLIST-" + TempDSValue.Tables[n].Columns[i].ToString() + "-" + TempDR["TableName"] == str && ((DropDownList)x).ID.Contains("12Hr"))
                                                            {
                                                                if (TempDSValue.Tables[n].Rows.Count > 0)
                                                                {
                                                                    String[] TimeMinute = Convert.ToString(TempDSValue.Tables[n].Rows[0][i]).Split(':');
                                                                    ((DropDownList)x).SelectedValue = Convert.ToString(TimeMinute[0]);
                                                                    string ID = ((DropDownList)x).ID.Replace("12Hr", "Min");
                                                                    BindTime12ControlValue(TimeMinute, ID);
                                                                }
                                                            }
                                                            else if ("SELECTLIST-" + TempDSValue.Tables[n].Columns[i].ToString() + "-" + TempDR["TableName"] == str && ((DropDownList)x).ID.Contains("AMPM"))
                                                            {
                                                                if (TempDSValue.Tables[n].Rows.Count > 0)
                                                                {
                                                                    String[] TimeMinute = Convert.ToString(TempDSValue.Tables[n].Rows[0][i]).Split(':');
                                                                    ((DropDownList)x).SelectedValue = Convert.ToString(TimeMinute[0]);
                                                                    string ID = ((DropDownList)x).ID.Replace("12Hr", "Min");
                                                                    BindTime12ControlValue(TimeMinute, ID);
                                                                }
                                                            }
                                                            else if ("SELECTLIST-" + TempDSValue.Tables[n].Columns[i].ToString() + "-" + TempDR["TableName"] == str && ((DropDownList)x).ID.Contains("AMPM") == false && ((DropDownList)x).ID.Contains("12Hr") == false && ((DropDownList)x).ID.Contains("24Hr") == false && ((DropDownList)x).ID.Contains("Min") == false)
                                                            {
                                                                if (TempDSValue.Tables[n].Rows.Count > 0)
                                                                {
                                                                    ((DropDownList)x).SelectedValue = Convert.ToString(TempDSValue.Tables[n].Rows[0][i]);
                                                                    DataView theDVConditionalField = new DataView(((DataSet)Session["AllData"]).Tables[17]);
                                                                    string[] theId = ((DropDownList)x).ID.Split('-');
                                                                    theDVConditionalField.RowFilter = "ConFieldId=" + theId.GetValue(3);
                                                                    if (theDVConditionalField.Count > 0)
                                                                    {
                                                                        EventArgs s = new EventArgs();
                                                                        ddlSelectList_SelectedIndexChanged((DropDownList)x, s);
                                                                    }
                                                                }
                                                            }

                                                            //else if ("SELECTLIST-TAB" + TempDSValue.Tables[n].Columns[1].ToString() + "-" + TempDR["TableName"] + "-" + TabId == strTabSig && ((DropDownList)x).ID.Contains("LNK_FORMTABORDVISIT") == true)
                                                            else if ("SELECTLIST-TAB" + TempDSValue.Tables[n].Columns[1].ToString() + "-" + TempDR["TableName"] == str && ((DropDownList)x).ID.Contains("LNK_FORMTABORDVISIT") == true)
                                                            {
                                                                if (TempDSValue.Tables[n].Rows.Count > 0)
                                                                {
                                                                    foreach (DataRow theDRTabId in DTTabId.Rows)
                                                                    {
                                                                        if ("SELECTLIST-TAB" + TempDSValue.Tables[n].Columns[1].ToString() + "-" + TempDR["TableName"] + "-" + Convert.ToInt32(theDRTabId["TabId"]) == strTabSig && ((DropDownList)x).ID.Contains("LNK_FORMTABORDVISIT") == true)
                                                                        {
                                                                            foreach (DataRow theDRSig in TempDSValue.Tables[n].Rows)
                                                                            {
                                                                                if (Convert.ToInt32(theDRTabId["TabId"]) == Convert.ToInt32(theDRSig["TabId"]))
                                                                                {
                                                                                    ((DropDownList)x).SelectedValue = Convert.ToString(theDRSig["Signature"]);
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        else if (x.GetType() == typeof(HtmlInputRadioButton))
                                                        {
                                                            string[] remStr = ((HtmlInputRadioButton)x).ID.Split('-');
                                                            string str = remStr[0] + "-" + remStr[1] + "-" + remStr[2];
                                                            if (TempDSValue.Tables[n].Columns[i].ToString() == ((HtmlInputRadioButton)x).Name)
                                                            {
                                                                for (int k = 0; k < TempDSValue.Tables[n].Rows.Count; k++)
                                                                {
                                                                    if (TempDSValue.Tables[n].Rows[k][TempDSValue.Tables[n].Columns[i]].ToString() == "True" || TempDSValue.Tables[n].Rows[k][TempDSValue.Tables[n].Columns[i]].ToString() == "1")
                                                                    {
                                                                        if ("RADIO1-" + TempDSValue.Tables[n].Columns[i].ToString() + "-" + TempDR["TableName"] == str)
                                                                        {
                                                                            ((HtmlInputRadioButton)x).Checked = true;
                                                                            DataView theDVConditionalField = new DataView(((DataSet)Session["AllData"]).Tables[17]);
                                                                            string[] theId = ((HtmlInputRadioButton)x).ID.Split('-');
                                                                            theDVConditionalField.RowFilter = "ConFieldId=" + theId.GetValue(3);
                                                                            if (theDVConditionalField.Count > 0)
                                                                            {
                                                                                EventArgs s = new EventArgs();
                                                                                this.HtmlRadioButtonSelect(x);
                                                                            }
                                                                        }
                                                                    }
                                                                    else if (TempDSValue.Tables[n].Rows[k][TempDSValue.Tables[n].Columns[i]].ToString() == "False" || TempDSValue.Tables[n].Rows[k][TempDSValue.Tables[n].Columns[i]].ToString() == "0")
                                                                    {
                                                                        if ("RADIO2-" + TempDSValue.Tables[n].Columns[i].ToString() + "-" + TempDR["TableName"] == str)
                                                                        {
                                                                            ((HtmlInputRadioButton)x).Checked = true;
                                                                            DataView theDVConditionalField = new DataView(((DataSet)Session["AllData"]).Tables[17]);
                                                                            string[] theId = ((HtmlInputRadioButton)x).ID.Split('-');
                                                                            theDVConditionalField.RowFilter = "ConFieldId=" + theId.GetValue(3);
                                                                            if (theDVConditionalField.Count > 0)
                                                                            {
                                                                                EventArgs s = new EventArgs();
                                                                                this.HtmlRadioButtonSelect(x);
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        else if (x.GetType() == typeof(HtmlInputCheckBox))
                                                        {
                                                            string[] remStr = ((HtmlInputCheckBox)x).ID.Split('-');
                                                            string str = remStr[0] + "-" + remStr[1] + "-" + remStr[2];
                                                            if ("Chk-" + TempDSValue.Tables[n].Columns[i].ToString() + "-" + TempDR["TableName"] == str)
                                                            {
                                                                for (int k = 0; k < TempDSValue.Tables[n].Rows.Count; k++)
                                                                {
                                                                    if (TempDSValue.Tables[n].Rows[k][TempDSValue.Tables[n].Columns[i]].ToString() == "True")
                                                                    {
                                                                        ((HtmlInputCheckBox)x).Checked = true;
                                                                    }
                                                                    else { ((HtmlInputCheckBox)x).Checked = false; }
                                                                }
                                                            }
                                                        }
                                                        else if (x.GetType() == typeof(IQCare.IQControl.IQLookupTextBox))
                                                        {
                                                            IQLookupTextBox iq = (IQLookupTextBox)x;
                                                            string strTableName = TempDR["TableName"].ToString();
                                                            this.BindIQLookupTextBoxValue(ref strTableName, iq, ref n, ref i, ref TempDSValue);


                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                //Multiselect
                DataTable theMultiDT = new DataTable();
                theMultiDT.Columns.Add(new DataColumn("TableName", typeof(String)));
                DataSet TmpDSMulti = new DataSet();
                StringBuilder SBGetValueMultiselect = new StringBuilder();
                foreach (object obj in container.Controls)
                {
                    if (obj is AjaxControlToolkit.TabPanel)
                    {
                        AjaxControlToolkit.TabPanel tabPanel = (AjaxControlToolkit.TabPanel)obj;
                        foreach (object ctrl in tabPanel.Controls)
                        {
                            if (ctrl is Control)
                            {
                                Control c = (Control)ctrl;
                                foreach (object multiselect in c.Controls)
                                {
                                    if (multiselect.GetType() == typeof(Panel))
                                    {
                                        string Id = string.Empty;
                                        foreach (Control z in ((Control)multiselect).Controls)
                                        {
                                            if (z.GetType() == typeof(CheckBox) && ((CheckBox)z).ID.Contains("FinChkDrug") == false)
                                            {
                                                string[] Table = ((CheckBox)z).ID.Split('-');
                                                string TableName = Table[3];
                                                Id = Table[1];
                                                if (TableName == "DTL_CUSTOMFIELD")
                                                {
                                                    DataRow theMultiDR = theMultiDT.NewRow();
                                                    TableName = "DTL_FB_" + Table[2] + "";
                                                    TableName = TableName.Trim().Replace(' ', '_');
                                                    theMultiDR["TableName"] = TableName;
                                                    theMultiDT.Rows.Add(theMultiDR);
                                                }
                                                else if (TableName.ToUpper() == "DTL_CUSTOMFORM")
                                                {
                                                }
                                                else
                                                {
                                                    DataRow theMultiDR = theMultiDT.NewRow();
                                                    theMultiDR["TableName"] = TableName;
                                                    theMultiDT.Rows.Add(theMultiDR);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                DataTable theLocalDT = theMultiDT.DefaultView.ToTable(true, "TableName").Copy();
                for (int i = 0; i < theLocalDT.Rows.Count; i++)
                {
                    SBGetValueMultiselect.Append("Select * from " + theLocalDT.Rows[i]["TableName"].ToString() + " where Ptn_pk=" + PatientID + " and Visit_Pk=" + VisitID + " and LocationId=" + LocationID + ";");
                }

                TmpDSMulti = MgrBindValue.Common_GetSaveUpdate(SBGetValueMultiselect.ToString());
                for (int m = 0; m < TmpDSMulti.Tables.Count; m++)
                {
                    if (TmpDSMulti.Tables[m].Rows.Count > 0)
                    {
                        foreach (object obj in container.Controls)
                        {
                            if (obj is AjaxControlToolkit.TabPanel)
                            {
                                AjaxControlToolkit.TabPanel tabPanel = (AjaxControlToolkit.TabPanel)obj;
                                foreach (object ctrl in tabPanel.Controls)
                                {
                                    if (ctrl is Control)
                                    {
                                        Control c = (Control)ctrl;
                                        foreach (object multiselect in c.Controls)
                                        {
                                            if (multiselect.GetType() == typeof(Panel))
                                            {
                                                string Id = string.Empty;
                                                foreach (Control z in ((Control)multiselect).Controls)
                                                {
                                                    if (z.GetType() == typeof(CheckBox))
                                                    {
                                                        string[] Table = ((CheckBox)z).ID.Split('-');
                                                        string TableName = Table[3];
                                                        string str = Table[0] + "-" + Table[1] + "-" + Table[2] + "-" + Table[3];
                                                        if (Table.Length == 5)
                                                        {
                                                            if (Table[3] == "DTL_CUSTOMFIELD")
                                                            {
                                                                foreach (DataRow theDR in TmpDSMulti.Tables[m].Rows)
                                                                {
                                                                    for (int i = 0; i <= TmpDSMulti.Tables[m].Columns.Count - 1; i++)
                                                                    {
                                                                        if ("CHKMULTI-" + theDR[TmpDSMulti.Tables[m].Columns[i].ToString()] + "-" + TmpDSMulti.Tables[m].Columns[i].ToString() + "-" + "DTL_CUSTOMFIELD" == str)
                                                                        {
                                                                            ((CheckBox)z).Checked = true;
                                                                            DataView theDVConditionalField = new DataView(((DataSet)Session["AllData"]).Tables[17]);
                                                                            string[] theId = ((CheckBox)z).ID.Split('-');
                                                                            theDVConditionalField.RowFilter = "ConFieldId=" + theId.GetValue(4);
                                                                            if (theDVConditionalField.Count > 0)
                                                                            {
                                                                                EventArgs s = new EventArgs();
                                                                                this.HtmlCheckBoxSelect(z);
                                                                            }
                                                                            if (((CheckBox)z).Text == "Other" && "CHKMULTI-" + theDR[TmpDSMulti.Tables[m].Columns[i].ToString()] + "-" + TmpDSMulti.Tables[m].Columns[i].ToString() + "-" + "DTL_CUSTOMFIELD" == str)
                                                                            {
                                                                                string script = "";
                                                                                script = "<script language = 'javascript' defer ='defer' id = 'Other'" + Id + ">\n";
                                                                                script += "show('" + ((CheckBox)z).ID + "-" + Table[2] + "');\n";
                                                                                script += "</script>\n";
                                                                                //ClientScript.RegisterStartupScript(this.GetType(),"Other" + Id + "", script);
                                                                                Page.ClientScript.RegisterStartupScript(this.GetType(), "Other" + Id + "", script);
                                                                                ViewState["Otherchk"] = ((CheckBox)z).Text;
                                                                                ViewState["Othertxt"] = theDR[6];
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            else if (TableName.ToUpper() == "DTL_CUSTOMFORM")
                                                            {
                                                            }
                                                            else
                                                            {
                                                                foreach (DataRow theDR in TmpDSMulti.Tables[m].Rows)
                                                                {
                                                                    for (int i = 0; i <= TmpDSMulti.Tables[m].Columns.Count - 1; i++)
                                                                    {
                                                                        if ("CHKMULTI-" + theDR[TmpDSMulti.Tables[m].Columns[i].ToString()] + "-" + TmpDSMulti.Tables[m].Columns[i].ToString() + "-" + Table[3].ToString() == str)
                                                                        {
                                                                            if (((CheckBox)z).Text == "Other")
                                                                            {
                                                                                ((CheckBox)z).Checked = true;
                                                                                DataView theDVConditionalField = new DataView(((DataSet)Session["AllData"]).Tables[17]);
                                                                                string[] theId = ((CheckBox)z).ID.Split('-');
                                                                                theDVConditionalField.RowFilter = "ConFieldId=" + theId.GetValue(4);
                                                                                if (theDVConditionalField.Count > 0)
                                                                                {
                                                                                    EventArgs s = new EventArgs();
                                                                                    this.HtmlCheckBoxSelect(z);
                                                                                }

                                                                                string script = "";
                                                                                script = "<script language = 'javascript' defer ='defer' id = 'Other'" + Id + ">\n";
                                                                                script += "show('" + Table[2] + "');\n";
                                                                                script += "</script>\n";
                                                                                ClientScript.RegisterStartupScript(this.GetType(), "Other" + Id + "", script);
                                                                                ViewState["Otherchk"] = ((CheckBox)z).Text;
                                                                                string filePath = Server.MapPath("~/XMLFiles/MultiSelectCustomForm.xml");
                                                                                DataSet dsMultiSelectList = new DataSet();
                                                                                dsMultiSelectList.ReadXml(filePath);
                                                                                DataTable DT = dsMultiSelectList.Tables[0];
                                                                                foreach (DataRow DR in DT.Rows)
                                                                                {
                                                                                    if (DR[0].ToString().ToUpper() == TableName)
                                                                                    {
                                                                                        ViewState["Othertxt"] = theDR["" + DR[2].ToString() + ""];
                                                                                    }
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                ((CheckBox)z).Checked = true;
                                                                                DataView theDVConditionalField = new DataView(((DataSet)Session["AllData"]).Tables[17]);
                                                                                string[] theId = ((CheckBox)z).ID.Split('-');
                                                                                theDVConditionalField.RowFilter = "ConFieldId=" + theId.GetValue(4);
                                                                                if (theDVConditionalField.Count > 0)
                                                                                {
                                                                                    EventArgs s = new EventArgs();
                                                                                    this.HtmlCheckBoxSelect(z);
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    if (z.GetType() == typeof(HtmlInputText))
                                                    {
                                                        if (Convert.ToString(ViewState["Otherchk"]) == "Other")
                                                        {
                                                            ((HtmlInputText)z).Value = Convert.ToString(ViewState["Othertxt"]);
                                                        }
                                                    }

                                                    if (z.GetType() == typeof(TextBox))
                                                    {
                                                        string[] TableName = ((TextBox)z).ID.Split('-');
                                                        string str = TableName[0] + "-" + TableName[1] + "-" + TableName[2] + "-" + TableName[3];
                                                        if (TableName.Length == 6)
                                                        {
                                                            foreach (DataRow theDR in TmpDSMulti.Tables[m].Rows)
                                                            {
                                                                if (TmpDSMulti.Tables[m].Rows.Count > 0)
                                                                {
                                                                    for (int i = 0; i <= TmpDSMulti.Tables[m].Columns.Count - 1; i++)
                                                                    {
                                                                        if ("TXTDT1-" + theDR[TmpDSMulti.Tables[m].Columns[i].ToString()] + "-" + TmpDSMulti.Tables[m].Columns[i].ToString() + "-" + TableName[3].ToString() == str)
                                                                        {
                                                                            if (!string.IsNullOrEmpty(theDR["DateField1"].ToString()) && Convert.ToDateTime(theDR["DateField1"]) != DateTime.Parse("1/1/1900"))
                                                                                ((TextBox)z).Text = Convert.ToDateTime(theDR["DateField1"]).ToString("dd-MMM-yyyy");
                                                                            else
                                                                                ((TextBox)z).Text = string.Empty;
                                                                        }
                                                                        else if ("TXTDT2-" + theDR[TmpDSMulti.Tables[m].Columns[i].ToString()] + "-" + TmpDSMulti.Tables[m].Columns[i].ToString() + "-" + TableName[3].ToString() == str)
                                                                        {
                                                                            if (!string.IsNullOrEmpty(theDR["DateField2"].ToString()) && Convert.ToDateTime(theDR["DateField2"]) != DateTime.Parse("1/1/1900"))
                                                                                ((TextBox)z).Text = Convert.ToDateTime(theDR["DateField2"]).ToString("dd-MMM-yyyy");
                                                                            else
                                                                                ((TextBox)z).Text = string.Empty;
                                                                        }
                                                                        else if ("TXTNUM-" + theDR[TmpDSMulti.Tables[m].Columns[i].ToString()] + "-" + TmpDSMulti.Tables[m].Columns[i].ToString() + "-" + TableName[3].ToString() == str)
                                                                        {
                                                                            if (!string.IsNullOrEmpty(theDR["NumericField"].ToString()) && theDR["NumericField"].ToString() != "0")
                                                                                ((TextBox)z).Text = theDR["NumericField"].ToString();
                                                                            else
                                                                                ((TextBox)z).Text = string.Empty;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    if (z.GetType() == typeof(HtmlInputText))
                                                    {
                                                        if (Convert.ToString(ViewState["Otherchk"]) == "Other")
                                                        {
                                                            ((HtmlInputText)z).Value = Convert.ToString(ViewState["Othertxt"]);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
        }

        private void btnDynDQ_Click(object sender, EventArgs e)
        {
            string[] ID = ((Button)sender).ID.Split('-');
            int TabID = Convert.ToInt32(ID[1]);
            ViewState["TabId"] = TabID;
            AjaxControlToolkit.TabContainer container = (AjaxControlToolkit.TabContainer)tabContainer;
            //ConFieldEnableDisable(container);
            //Page_PreRender(sender, e);
            ICustomForm MgrSaveUpdate = (ICustomForm)ObjectFactory.CreateInstance(ObjFactoryParameter);
            DataSet theDS = new DataSet();
            theDS.Tables.Add(ReadLabTable(container, TabID));
            theDS.Tables.Add(ReadARVMedicationTable(container, TabID));
            theDS.Tables.Add(ReadNonARVMedicationTable(container, TabID));

            if (FieldValidation() == false)
            {
                hdnPrevTabId.Value = TabID.ToString();
                container.ActiveTabIndex = Convert.ToInt32(hdnPrevTabIndex.Value);
                return;
            }
            string msg = ValidationMessage(theDS, TabID);
            if (msg.Length > 51)
            {
                hdnPrevTabId.Value = TabID.ToString();
                container.ActiveTabIndex = Convert.ToInt32(hdnPrevTabIndex.Value);
                MsgBuilder theBuilder1 = new MsgBuilder();
                theBuilder1.DataElements["MessageText"] = msg;
                IQCareMsgBox.Show("#C1", theBuilder1, this);
                return;
            }

            if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
            {
                int PatientID = Convert.ToInt32(Session["PatientId"]);
                ViewState["VisitDate"] = txtvisitDate.Text;
                StringBuilder Insert = SaveCustomFormData(PatientID, theDS, 1, TabID);
                DataSet TempDS = MgrSaveUpdate.SaveUpdate(Insert.ToString(), theDS, TabID);
                Session["PatientVisitId"] = TempDS.Tables[0].Rows[0]["VisitID"].ToString();
                hdnVisitId.Value = Session["PatientVisitId"].ToString();
                Session["ServiceLocationId"] = TempDS.Tables[0].Rows[0]["LocationID"].ToString();
                DQCheck(PatientID, Convert.ToInt32(Session["PatientVisitId"]), Convert.ToInt32(Session["ServiceLocationId"]));
                hdnCurrenTabIndex.Value = Convert.ToString(container.ActiveTabIndex);
                SaveCancel();
            }
            else if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
            {
                int FeatureID = Convert.ToInt32(Session["FeatureID"]);
                int PatientID = Convert.ToInt32(Session["PatientId"]);
                int VisitID = Convert.ToInt32(Session["PatientVisitId"]);
                int LocationID = Convert.ToInt32(Session["ServiceLocationId"]);
                StringBuilder Update = UpdateCustomFormData(PatientID, FeatureID, VisitID, LocationID, theDS, 1, TabID);
                DataSet TempDS = MgrSaveUpdate.SaveUpdate(Update.ToString(), theDS, TabID);
                Session["PatientVisitId"] = TempDS.Tables[0].Rows[0]["VisitID"].ToString();
                hdnVisitId.Value = Session["PatientVisitId"].ToString();
                DQCheck(PatientID, Convert.ToInt32(Session["PatientVisitId"]), LocationID);
                hdnCurrenTabIndex.Value = Convert.ToString(container.ActiveTabIndex);
                UpdateCancel();
            }
            hdnPrevTabId.Value = hdnCurrentTabId.Value;
            ClientScript.RegisterStartupScript(GetType(), "CurrentTabValue1", "StringASCII(" + hdnPrevTabId.Value + ");", true);
            hdnPrevTabIndex.Value = Convert.ToString(container.ActiveTabIndex);
        }

        ///////////////////////////////////////////////////////////////////
        private void btnDynSave_Click(object sender, EventArgs e)
        {
            string[] ID = ((Button)sender).ID.Split('-');
            int TabID = Convert.ToInt32(ID[1]);
            ViewState["TabId"] = TabID;
            AjaxControlToolkit.TabContainer container = (AjaxControlToolkit.TabContainer)tabContainer;
            //ConFieldEnableDisable(container);
            //Page_PreRender(sender, e);
            ICustomForm MgrSaveUpdate = (ICustomForm)ObjectFactory.CreateInstance(ObjFactoryParameter);
            DataSet theDS = new DataSet();
            theDS.Tables.Add(ReadLabTable(container, TabID));
            theDS.Tables.Add(ReadARVMedicationTable(container, TabID));
            theDS.Tables.Add(ReadNonARVMedicationTable(container, TabID));
            int a = container.ActiveTabIndex;
            if (FieldValidation() == false)
            {
                hdnPrevTabId.Value = TabID.ToString();
                container.ActiveTabIndex = Convert.ToInt32(hdnPrevTabIndex.Value);
                return;
            }
            string msg = ValidationMessage(theDS, TabID);
            if (msg.Length > 51)
            {
                hdnPrevTabId.Value = TabID.ToString();
                container.ActiveTabIndex = Convert.ToInt32(hdnPrevTabIndex.Value);
                MsgBuilder theBuilder1 = new MsgBuilder();
                theBuilder1.DataElements["MessageText"] = msg;
                IQCareMsgBox.Show("#C1", theBuilder1, this);
                return;
            }

            if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
            {
                int PatientID = Convert.ToInt32(Session["PatientId"]);
                ViewState["VisitDate"] = txtvisitDate.Text;
                StringBuilder Insert = SaveCustomFormData(PatientID, theDS, 0, TabID);
                DataSet TempDS = MgrSaveUpdate.SaveUpdate(Insert.ToString(), theDS, TabID);
                Session["PatientVisitId"] = TempDS.Tables[0].Rows[0]["VisitID"].ToString();
                Session["ServiceLocationId"] = TempDS.Tables[0].Rows[0]["LocationID"].ToString();
                hdnVisitId.Value = Session["PatientVisitId"].ToString();
                hdnCurrenTabIndex.Value = Convert.ToString(container.ActiveTabIndex);
                SaveCancel();
            }
            else if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
            {
                int FeatureID = Convert.ToInt32(Session["FeatureID"]);
                int PatientID = Convert.ToInt32(Session["PatientId"]);
                int VisitID = Convert.ToInt32(Session["PatientVisitId"]);
                int LocationID = Convert.ToInt32(Session["ServiceLocationId"]);
                StringBuilder Update = UpdateCustomFormData(PatientID, FeatureID, VisitID, LocationID, theDS, 0, TabID);
                DataSet TempDS = MgrSaveUpdate.SaveUpdate(Update.ToString(), theDS, TabID);
                Session["PatientVisitId"] = TempDS.Tables[0].Rows[0]["VisitID"].ToString();
                hdnVisitId.Value = Session["PatientVisitId"].ToString();
                hdnCurrenTabIndex.Value = Convert.ToString(container.ActiveTabIndex);
                UpdateCancel();
            }

            hdnPrevTabId.Value = hdnCurrentTabId.Value;
            ClientScript.RegisterStartupScript(GetType(), "CurrentTabValue1", "StringASCII(" + hdnPrevTabId.Value + ");", true);
            hdnPrevTabIndex.Value = Convert.ToString(container.ActiveTabIndex);
        }

        private bool CheckAbNormalStatus(string ID, string Value)
        {
            bool status = false;
            string ar = ID;
            string[] arVal = ar.Split('-');
            DataTable theDTNew = (DataTable)ViewState["BusRule"];
            DataView FilterAbVal = theDTNew.DefaultView;
            FilterAbVal.RowFilter = "FieldID=" + arVal[3].ToString();
            DataTable dtNewVal = FilterAbVal.ToTable();
            if (dtNewVal.Rows.Count == 4)
            {
                string MaxValue = "", MinValue = "", MaxNormal = "", MinNormal = "", TextValue = "";
                Int32 MaxValue1 = 0, MinValue1 = 0, MaxNormal1 = 0, MinNormal1 = 0;
                double TextValue1 = 0;
                foreach (DataRow dr in dtNewVal.Rows)
                {
                    if (dr["BusRuleID"].ToString() == "2")
                    {
                        MaxValue = dr["Value"].ToString();
                        MaxValue1 = Convert.ToInt32(dr["Value"].ToString());
                    }
                    if (dr["BusRuleID"].ToString() == "3")
                    {
                        MinValue = dr["Value"].ToString();
                        MinValue1 = Convert.ToInt32(dr["Value"].ToString());
                    }
                    if (dr["BusRuleID"].ToString() == "26")
                    {
                        MaxNormal = dr["Value"].ToString();
                        MaxNormal1 = Convert.ToInt32(dr["Value"].ToString());
                    }
                    if (dr["BusRuleID"].ToString() == "27")
                    {
                        MinNormal = dr["Value"].ToString();
                        MinNormal1 = Convert.ToInt32(dr["Value"].ToString());
                    }
                    TextValue = Convert.ToString(Value);
                    TextValue1 = Convert.ToDouble(Value);
                }

                if (MaxValue != "" && MinValue != "" && MaxNormal != "" && MinNormal != "")
                {
                    if ((TextValue1 <= MaxValue1) && (TextValue1 > MaxNormal1))
                    {
                        status = true;
                        // ((TextBox)y).ForeColor = System.Drawing.Color.Red;
                    }
                    else if ((TextValue1 < MinNormal1) && (TextValue1 >= MinValue1))
                    {
                        status = true;
                        //((TextBox)y).ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        status = false;
                        // ((TextBox)y).ForeColor = System.Drawing.Color.Black;
                    }
                }
            }
            return status;
        }

        private void CheckControl(Control theCntrl, string[] theId)
        {
            string theCntrlType = theId[0];
            //Calling for HTML check box Event
            foreach (object obj in theCntrl.Controls)
            {
                if (obj is AjaxControlToolkit.TabPanel)
                {
                    AjaxControlToolkit.TabPanel tabPanel = (AjaxControlToolkit.TabPanel)obj;
                    foreach (object ctrl in tabPanel.Controls)
                    {
                        if (ctrl is Control)
                        {
                            Control c = (Control)ctrl;
                            foreach (object y in c.Controls)
                            {
                                foreach (Control x in ((Control)y).Controls)
                                {
                                    if (x.GetType().ToString() == "System.Web.UI.WebControls.Panel")
                                        CheckControl(x, theId);
                                    else
                                    {
                                        if (x.ID == theId[1] && x.GetType().ToString() == theCntrlType && theCntrlType == "System.Web.UI.WebControls.CheckBox")
                                        {
                                            HtmlCheckBoxSelect(x);
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //Calling for HTML Radio Button Event
            foreach (object obj in theCntrl.Controls)
            {
                if (obj is AjaxControlToolkit.TabPanel)
                {
                    AjaxControlToolkit.TabPanel tabPanel = (AjaxControlToolkit.TabPanel)obj;
                    foreach (object ctrl in tabPanel.Controls)
                    {
                        if (ctrl is Control)
                        {
                            Control c = (Control)ctrl;
                            foreach (Control x in ((Control)c).Controls)
                            {
                                if (x.GetType().ToString() == "System.Web.UI.WebControls.Panel")
                                    CheckControl(x, theId);
                                else
                                {
                                    if (x.ID == theId[1] && x.GetType().ToString() == theCntrlType && theCntrlType == "System.Web.UI.HtmlControls.HtmlInputRadioButton")
                                    {
                                        HtmlRadioButtonSelect(x);
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void ConFieldEnableDisable(Control theControl)
        {
            foreach (object obj in theControl.Controls)
            {
                if (obj is AjaxControlToolkit.TabPanel)
                {
                    AjaxControlToolkit.TabPanel tabPanel = (AjaxControlToolkit.TabPanel)obj;
                    foreach (object ctrl in tabPanel.Controls)
                    {
                        if (ctrl is Control)
                        {
                            Control c = (Control)ctrl;
                            foreach (object y in c.Controls)
                            {
                                foreach (Control x in ((Control)y).Controls)
                                {
                                    //if (x.GetType().ToString() == "System.Web.UI.WebControls.Panel")
                                    //{
                                    //    ConFieldEnableDisable(x);
                                    //}
                                    //else
                                    //{
                                    if (x.GetType().ToString() == "System.Web.UI.WebControls.CheckBox")
                                    {
                                        if (((CheckBox)x).Checked == true)
                                        {
                                            DataView theDVConditionalField = new DataView(((DataSet)Session["AllData"]).Tables[17]);
                                            string[] theId = ((CheckBox)x).ID.Split('-');
                                            if (theId.Length == 5)
                                            {
                                                theDVConditionalField.RowFilter = "ConditionalFieldSectionId=" + theId.GetValue(1);
                                                if (theDVConditionalField.Count > 0)
                                                {
                                                    foreach (DataRow theDRCon in theDVConditionalField.ToTable().Rows)
                                                    {
                                                        System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(Page), "" + theDRCon["conControlId"] + "", "EnableControlTrue('" + theDRCon["conControlId"] + "');", true);
                                                        RemoveContolStausInHastTable(theDRCon["conControlId"].ToString());
                                                    }
                                                    EventArgs s = new EventArgs();
                                                    this.HtmlCheckBoxSelect(x);
                                                }
                                            }
                                        }
                                    }
                                    // }
                                }

                                if (y.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputRadioButton")
                                {
                                    if (((HtmlInputRadioButton)y).Checked == true && ((HtmlInputRadioButton)y).Value == "Yes")
                                    {
                                        DataView theDVConditionalField = new DataView(((DataSet)Session["AllData"]).Tables[17]);
                                        string[] theId = ((HtmlInputRadioButton)y).ID.Split('-');
                                        theDVConditionalField.RowFilter = "ConFieldId=" + theId.GetValue(3);
                                        if (theDVConditionalField.Count > 0)
                                        {
                                            foreach (DataRow theDRCon in theDVConditionalField.ToTable().Rows)
                                            {
                                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "" + theDRCon["conControlId"] + "", "EnableControlTrue('" + theDRCon["conControlId"] + "');", true);
                                                RemoveContolStausInHastTable(theDRCon["conControlId"].ToString());
                                            }
                                            EventArgs s = new EventArgs();
                                            this.HtmlRadioButtonSelect(y);
                                        }
                                    }

                                }
                                else if (y.GetType().ToString() == "System.Web.UI.WebControls.DropDownList")
                                {
                                    if (((DropDownList)y).SelectedValue != "0")
                                    {
                                        DataView theDVConditionalField = new DataView(((DataSet)Session["AllData"]).Tables[17]);
                                        string[] theId = ((DropDownList)y).ID.Split('-');
                                        if (!((DropDownList)y).SelectedValue.Contains("AM"))
                                        {
                                            if (!((DropDownList)y).SelectedValue.Contains("PM"))
                                            {
                                                theDVConditionalField.RowFilter = "ConditionalFieldSectionId=" + ((DropDownList)y).SelectedValue;
                                                if (theDVConditionalField.Count > 0)
                                                {
                                                    foreach (DataRow theDRCon in theDVConditionalField.ToTable().Rows)
                                                    {
                                                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "" + theDRCon["conControlId"] + "", "EnableControlTrue('" + theDRCon["conControlId"] + "');", true);
                                                        RemoveContolStausInHastTable(theDRCon["conControlId"].ToString());
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else if (y.GetType() == typeof(TextBox))
                                {
                                    if (((TextBox)y).Text != "")
                                    {
                                        bool FlagVal = CheckAbNormalStatus(((TextBox)y).ID, ((TextBox)y).Text);
                                        if (FlagVal == true)
                                        {
                                            ((TextBox)y).ForeColor = System.Drawing.Color.Red;
                                        }
                                        else
                                        {
                                            ((TextBox)y).ForeColor = System.Drawing.Color.Black;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private DataTable CreateColumntheDTICD10()
        {
            DataTable theDTICD10 = new DataTable();
            theDTICD10.Columns.Add("FieldId", typeof(int));
            theDTICD10.Columns.Add("BlockId", typeof(int));
            theDTICD10.Columns.Add("SubBlockId", typeof(int));
            theDTICD10.Columns.Add("Id", typeof(int));
            theDTICD10.Columns.Add("CodeId", typeof(string));
            theDTICD10.Columns.Add("Name", typeof(string));
            return theDTICD10;
        }

        private void CreateDateImage(object theControl, string ControlID, bool theConField, bool MMMYYYY)
        {
            string[] Field = ((Control)theControl).ID.Split('-');
            DataTable theDT = (DataTable)ViewState["BusRule"];
            TextBox theDateText = (TextBox)theControl;
            foreach (DataRow DR in theDT.Rows)
            {
                if (Field[1] == Convert.ToString(DR["FieldName"]) && Convert.ToString(DR["BusRuleId"]) == "21" && MMMYYYY == false)
                {
                    theDateText = (TextBox)theControl;
                    theDateText.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'4')");
                    theDateText.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'4')");
                    DIVCustomItem.Controls.Add(new LiteralControl("<span class='smallerlabel'>(MMM-YYYY)</span>"));
                    MMMYYYY = true;
                }
            }
            if (MMMYYYY == false)
            {
                theDateText = (TextBox)theControl;
                theDateText.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
                theDateText.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");
                Image theDateImage = new Image();
                theDateImage.ID = "img" + theDateText.ID;
                theDateImage.Height = 22;
                theDateImage.Width = 22;
                //theDateImage.Visible = theEnable;
                theDateImage.ToolTip = "Date Helper";
                theDateImage.ImageUrl = "~/images/cal_icon.gif";
                tabContainer.ID = "TAB";
                theDateImage.Attributes.Add("onClick", "w_displayDatePicker('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + ((TextBox)theControl).ClientID + "');");
                DIVCustomItem.Controls.Add(new LiteralControl("&nbsp;"));
                DIVCustomItem.Controls.Add(theDateImage);
                // ApplyBusinessRules(theDateImage, ControlID, theEnable);
                DIVCustomItem.Controls.Add(new LiteralControl("<span class='smallerlabel'>(DD-MMM-YYYY)</span>"));
            }
        }

        private DataTable CreateSelectedTable()
        {
            DataTable theDT = new DataTable();
            theDT.Columns.Add("DrugId",Type.GetType("System.Int32"));
            theDT.Columns.Add("DrugName", Type.GetType("System.String"));
            theDT.Columns.Add("Generic", Type.GetType("System.Int32"));
            theDT.Columns.Add("DrugTypeID", Type.GetType("System.Int32"));
            theDT.Columns.Add("Abbr", Type.GetType("System.String"));
            theDT.PrimaryKey = new DataColumn[] { theDT.Columns[0] };
            return theDT;
        }

        private void CreateTab(DataTable theDT)
        {
            tabContainer = new AjaxControlToolkit.TabContainer();
            tabContainer.CssClass = "ajax__tab_technorati-theme";
            //tabcontainer.Height = Unit.Pixel(200);
            foreach (DataRow theDR in theDT.Rows)
            {
                tbChildPanel = new TabPanel();
                tbChildPanel.HeaderText = theDR[1].ToString();
                tbChildPanel.ID = theDR[0].ToString();
                tabContainer.Tabs.Add(tbChildPanel);
            }
        }

        private DataTable CreateTable(string[] value)
        {
            DataTable TmpDT = new DataTable();
            DataColumn ID = new DataColumn();
            ID.DataType = Type.GetType("System.Int32");
            ID.ColumnName = "ID";
            TmpDT.Columns.Add(ID);

            DataColumn Name = new DataColumn();
            Name.DataType = Type.GetType("System.String");
            Name.ColumnName = "Name";
            TmpDT.Columns.Add(Name);

            DataRow tmpdr;

            for (int i = 1; i < value.Length + 1; i++)
            {
                tmpdr = TmpDT.NewRow();
                tmpdr[0] = i;
                tmpdr[1] = value[i - 1];
                TmpDT.Rows.Add(tmpdr);
            }
            return TmpDT;
        }

        private void CustomFormAddRegimen(int RegTypeID, string strregime, string sessionName, string controlID)
        {
            try
            {

                DataView theDV = new DataView((DataTable)Session["MasterCustomfrmReg"]);
                theDV.RowFilter = "DrugTypeId=" + RegimenType + " and Generic<>0";
                DataTable theDTCustomFrmReg = theDV.ToTable();
                // DataTable theDTCustomFrmReg = (DataTable)Session["Reg" + controlID + RegTypeID + ""];
                string[] eachregime = strregime.Split('/');
                DataRow theDR;
                DataTable theDT = PtnCustomformselectedDataTable();
                for (int i = 0; i < eachregime.Length; i++)
                {
                    DataView dv = new DataView(theDTCustomFrmReg);
                    dv.RowFilter = "Abbr ='" + eachregime[i] + "'";
                    if (dv.Count > 0)
                    {
                    }
                    else
                    {
                        dv = new DataView(theDTCustomFrmReg);
                        dv.RowFilter = "DrugName ='" + eachregime[i] + "'";
                    }
                    theDR = theDT.NewRow();
                    theDR[0] = Convert.ToInt32(dv[0][0].ToString());
                    theDR[1] = dv[0][1].ToString();
                    theDR[2] = dv[0][2].ToString();
                    theDR[4] = dv[0][4].ToString();
                    theDT.Rows.Add(theDR);
                }
                Session[sessionName] = theDT;
            }
            catch
            {
            }
            finally { }
        }

        private void CustomFormRegimen(int RegTypeID)
        {
            BindFunctions theBind = new BindFunctions();
            DataTable theDTCustomFrmReg = (DataTable)Session["Reg" + ViewState["ControlId"].ToString() + RegTypeID + ""];
            DataTable theDTCustomFrmSelectedReg = (DataTable)Session["SelectedReg" + ViewState["ControlId"].ToString() + RegTypeID + ""];
            foreach (DataRow theDR in theDTCustomFrmReg.Rows)
            {
                if (Convert.ToInt32(theDR["Generic"]) > 0) // if generic
                {
                    if ((theDR["Abbr"].ToString() != "") && (theDR["DrugName"].ToString().LastIndexOf(']')) < 1)
                    {
                        theDR["DrugName"] = theDR["DrugName"].ToString() + "-[" + theDR["Abbr"].ToString() + "]";
                    }
                    else
                    {
                        theDR["DrugName"] = theDR["DrugName"].ToString();
                    }
                }
            }
            foreach (DataRow theSelectedDR in theDTCustomFrmSelectedReg.Rows)
            {
                if (Convert.ToInt32(theSelectedDR["Generic"]) > 0) // if generic
                {
                    if (theSelectedDR["DrugAbbr"].ToString() != "" && (theSelectedDR["DrugName"].ToString().LastIndexOf(']')) < 1)
                    {
                        theSelectedDR["DrugName"] = theSelectedDR["DrugName"].ToString() + "-[" + theSelectedDR["DrugAbbr"].ToString() + "]";
                    }
                    else
                    {
                        theSelectedDR["DrugName"] = theSelectedDR["DrugName"].ToString();
                    }
                }
            }
        }

        private void ddlSelectList_SelectedIndexChanged(object sender, EventArgs e)
        {
            AjaxControlToolkit.TabContainer container = (AjaxControlToolkit.TabContainer)tabContainer;
            container.ActiveTabIndex = Convert.ToInt32(hdnPrevTabIndex.Value);
            DropDownList theDList = ((DropDownList)sender);
            DataSet theDS = (DataSet)Session["AllData"];
            string[] theCntrl = theDList.ID.Split('-');

            foreach (DataRow theDR in theDS.Tables[17].Rows) //Dtcon.Rows)
            {
                foreach (object obj in tabContainer.Controls)
                {
                    if (obj is AjaxControlToolkit.TabPanel)
                    {
                        AjaxControlToolkit.TabPanel tabPanel = (AjaxControlToolkit.TabPanel)obj;
                        foreach (object ctrl in tabPanel.Controls)
                        {
                            if (ctrl is Control)
                            {
                                Control c = (Control)ctrl;
                                foreach (Control x in ((Control)c).Controls)
                                {
                                    if (x.ID != null)
                                    {
                                        string[] theIdent = x.ID.Split('-');
                                        if (x.ID.Contains("12Hr"))
                                        {
                                            theIdent = x.ID.Replace("12Hr", "").Split('-');
                                        }
                                        else if (x.ID.Contains("24Hr"))
                                        {
                                            theIdent = x.ID.Replace("24Hr", "").Split('-');
                                        }
                                        else if (x.ID.Contains("Min"))
                                        {
                                            theIdent = x.ID.Replace("Min", "").Split('-');
                                        }
                                        else if (x.ID.Contains("AMPM"))
                                        {
                                            theIdent = x.ID.Replace("AMPM", "").Split('-');
                                        }
                                        if ((theIdent.Length > 2) || (x.ID.StartsWith("Pnl_")))
                                        {
                                            if (x.GetType().ToString() == "System.Web.UI.WebControls.TextBox" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                                            {
                                                if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theDList.SelectedValue.ToString())
                                                {
                                                    ((TextBox)x).Enabled = true;
                                                    ApplyBusinessRules(x, "1", true);
                                                    ApplyBusinessRules(x, "2", true);
                                                    ApplyBusinessRules(x, "3", true);
                                                    ApplyBusinessRules(x, "5", true);
                                                }
                                                else if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theDList.SelectedValue.ToString())
                                                {
                                                    ((TextBox)x).Enabled = false;
                                                    ((TextBox)x).Text = "";
                                                }

                                                if ((theIdent[0] == "TXTDTAuto") || (theIdent[0] == "TXTMultiAuto") || (theIdent[0] == "TXTAuto") || (theIdent[0] == "TXTNUMAuto"))
                                                {
                                                    ((TextBox)x).Enabled = false;
                                                }

                                                if (theIdent[0] == "TXTReg")
                                                {
                                                    ((TextBox)x).Enabled = true;
                                                }
                                            }

                                            if (x.GetType().ToString() == "System.Web.UI.WebControls.DropDownList" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                                            {
                                                if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theDList.SelectedValue.ToString())
                                                {
                                                    if (x.ID.ToString() == theIdent[0].ToString() + "-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString() + "-" + theDR["TabId"].ToString())
                                                    {
                                                        if (theIdent[0].ToString() == "SELECTLISTAuto")
                                                        {
                                                            ((DropDownList)x).Enabled = false;
                                                        }
                                                        else
                                                        {
                                                            ((DropDownList)x).Enabled = true;
                                                            ApplyBusinessRules(x, "4", true);
                                                        }
                                                    }
                                                    else if (x.ID.ToString() == theIdent[0].ToString() + "-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString() + "24Hr")
                                                    {
                                                        ((DropDownList)x).Enabled = true;
                                                        ApplyBusinessRules(x, "4", true);
                                                    }
                                                    else if (x.ID.ToString() == theIdent[0].ToString() + "-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString() + "12Hr")
                                                    {
                                                        ((DropDownList)x).Enabled = true;
                                                        ApplyBusinessRules(x, "4", true);
                                                    }
                                                    else if (x.ID.ToString() == theIdent[0].ToString() + "-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString() + "Min")
                                                    {
                                                        ((DropDownList)x).Enabled = true;
                                                        ApplyBusinessRules(x, "4", true);
                                                    }
                                                    else if (x.ID.ToString() == theIdent[0].ToString() + "-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString() + "AMPM")
                                                    {
                                                        ((DropDownList)x).Enabled = true;
                                                        ApplyBusinessRules(x, "4", true);
                                                    }
                                                }
                                                else if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theDList.SelectedValue.ToString())
                                                {
                                                    ((DropDownList)x).Enabled = false;
                                                    if (((DropDownList)x).ID.Contains("AMPM") == false)
                                                    {
                                                        ((DropDownList)x).SelectedValue = "0";
                                                    }
                                                }
                                            }
                                            if (x.GetType().ToString() == "System.Web.UI.WebControls.Panel" && theIdent[0] + theIdent[1] + theIdent[2] == "Pnl_" + theDR["PdfTableName"].ToString() + theDR["FieldId"].ToString())
                                            {
                                                if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theDList.SelectedValue.ToString())
                                                {
                                                    ((Panel)x).Enabled = true;
                                                    ApplyBusinessRules(x, "9", true);
                                                }
                                                else if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theDList.SelectedValue.ToString())
                                                {
                                                    ((Panel)x).Enabled = false;
                                                }
                                            }

                                            if (x.GetType().ToString() == "System.Web.UI.WebControls.Button" && "BtnDrg-" + theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == "BtnDrg-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                                            {
                                                if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theDList.SelectedValue.ToString())
                                                    ((Button)x).Enabled = true;
                                                else if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theDList.SelectedValue.ToString())
                                                {
                                                    DrugType = GetFilterId(theIdent[3], theIdent[1]);
                                                    Session["Selected" + DrugType + ""] = null;
                                                    ((Button)x).Enabled = false;
                                                }
                                            }

                                            if (x.GetType().ToString() == "System.Web.UI.WebControls.Button" && "BtnLab-" + theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == "BtnLab-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                                            {
                                                if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theDList.SelectedValue.ToString())
                                                    ((Button)x).Enabled = true;
                                                else if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theDList.SelectedValue.ToString())
                                                {
                                                    ViewState["AddLab"] = null;
                                                    ((Button)x).Enabled = false;
                                                }
                                            }

                                            if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputButton" && "BtnRegimen-" + theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == "BtnRegimen-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                                            {
                                                if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theDList.SelectedValue.ToString())
                                                    ((HtmlInputButton)x).Visible = true;
                                                else if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theDList.SelectedValue.ToString())
                                                {
                                                    ((HtmlInputButton)x).Visible = false;
                                                }
                                            }

                                            if (x.GetType().ToString() == "System.Web.UI.WebControls.Image" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                                            {
                                                if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theDList.SelectedValue.ToString())
                                                {
                                                    ((Image)x).Visible = true;
                                                    ApplyBusinessRules(x, "5", true);
                                                }
                                                else if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theDList.SelectedValue.ToString())
                                                    ((Image)x).Visible = false;
                                            }

                                            if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputRadioButton" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                                            {
                                                if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theDList.SelectedValue.ToString())
                                                {
                                                    ((HtmlInputRadioButton)x).Visible = true;
                                                    ApplyBusinessRules(x, "6", true);
                                                }
                                                else if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theDList.SelectedValue.ToString())
                                                    ((HtmlInputRadioButton)x).Visible = false;
                                            }

                                            if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputCheckBox" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                                            {
                                                if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theDList.SelectedValue.ToString())
                                                    ((HtmlInputCheckBox)x).Visible = true;
                                                else if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theDList.SelectedValue.ToString())
                                                    ((HtmlInputCheckBox)x).Visible = false;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void DeleteForm(int PatientID, int VisitID)
        {
            ICustomForm CustomManager = (ICustomForm)ObjectFactory.CreateInstance(ObjFactoryParameter);
            int theResultRow = (int)CustomManager.DeleteForm("Custom", VisitID, PatientID, Convert.ToInt32(Session["AppUserId"].ToString()));

            if (theResultRow == 0)
            {
                IQCareMsgBox.Show("RemoveFormError", this);
                return;
            }
            else
            {
                string theUrl;
                theUrl = string.Format("{0}?PatientId={1}", "frmPatient_Home.aspx", Convert.ToString(PatientID));
                Response.Redirect(theUrl);
            }
        }

        private void DQCancel()
        {
            ViewState["btcolor"] = '1';
            string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
            script += "var ans;\n";
            script += "ans=window.confirm('DQ Checked complete.\\nForm Marked as DQ Checked.\\n Do you want to close?');\n";
            script += "if (ans==true)\n";
            script += "{\n";
            if (base.Session["Redirect"].ToString() == "0")
            {
                script += "window.location.href='frmPatient_Home.aspx';\n";
            }
            else
            {
                script += "window.location.href='frmPatient_History.aspx?sts=" + 0 + "';\n";
            }
            script += "}\n";
            script += "</script>\n";
            //ClientScript.RegisterStartupScript(this.GetType(),"confirm", script);
            ClientScript.RegisterStartupScript(this.GetType(), "confirm", script);
        }

        private void DQCheck(int PatientID, int VisitID, int LocationID)
        {
            ICustomForm MgrBindValue = (ICustomForm)ObjectFactory.CreateInstance(ObjFactoryParameter);
            string GetValue = "Select * from LNK_FORMTABORDVISIT where Visit_Pk=" + VisitID + "";
            DataSet TempDS = MgrBindValue.Common_GetSaveUpdate(GetValue);
            DataTable theDTDQStatus = ((DataSet)Session["AllData"]).Tables[23];
            int TabCount = theDTDQStatus.Rows.Count;
            int SavedTabCount = 0;
            for (int i = 0; i < TempDS.Tables[0].Rows.Count; i++)
            {
                if (TempDS.Tables[0].Rows[i]["DataQuality"] != System.DBNull.Value)
                {
                    if (Convert.ToInt32(TempDS.Tables[0].Rows[i]["DataQuality"]) == 1)
                    {
                        SavedTabCount++;
                    }
                }
            }
            if (TabCount == SavedTabCount && TempDS.Tables[0].Rows.Count > 0)
            {
                GetValue = "";
                GetValue = " Update Ord_visit Set DataQuality=1 where ptn_pk=" + PatientID + " and Visit_Id=" + VisitID + " and LocationID=" + LocationID + "";
                TempDS = MgrBindValue.Common_GetSaveUpdate(GetValue);
            }
            else if (TrSignatureAll.Visible == true)
            {
                GetValue = "";
                GetValue = " Update Ord_visit Set DataQuality=1 where ptn_pk=" + PatientID + " and Visit_Id=" + VisitID + " and LocationID=" + LocationID + "";
                TempDS = MgrBindValue.Common_GetSaveUpdate(GetValue);
            }
            else
            {
                GetValue = "";
                GetValue = " Update Ord_visit Set DataQuality=0 where ptn_pk=" + PatientID + " and Visit_Id=" + VisitID + " and LocationID=" + LocationID + "";
                TempDS = MgrBindValue.Common_GetSaveUpdate(GetValue);
            }
        }

        private String DQMessage(DataSet theDS)
        {
            IIQCareSystem DQIQCareSecurity = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
            DateTime theCurrentDate = (DateTime)DQIQCareSecurity.SystemDate();
            string strmsg = "Following values are required to complete the data quality check:\\n\\n";
            DataTable theDT = (DataTable)ViewState["BusRule"];
            String Radio1 = "", Radio2 = "", MultiSelectName = "", MultiSelectLabel = "";
            int TotCount = 0, FalseCount = 0;
            try
            {
                if (txtvisitDate.Text.Trim() == "")
                {
                    string scriptblankvisitdate = "<script language = 'javascript' defer ='defer' id = 'Colorlblvisitdate'>\n";
                    scriptblankvisitdate += "To_Change_Color('lblvisitdate');\n";
                    scriptblankvisitdate += "</script>\n";
                    ClientScript.RegisterStartupScript(this.GetType(), "Colorlblvisitdate", scriptblankvisitdate);
                    strmsg += " Visit Date is Blank";
                    strmsg = strmsg + "\\n";
                }
                AjaxControlToolkit.TabContainer container = (AjaxControlToolkit.TabContainer)tabContainer;
                foreach (object obj in container.Controls)
                {
                    if (obj is AjaxControlToolkit.TabPanel)
                    {
                        AjaxControlToolkit.TabPanel tabPanel = (AjaxControlToolkit.TabPanel)obj;
                        foreach (object ctrl in tabPanel.Controls)
                        {
                            if (ctrl is Control)
                            {
                                Control c = (Control)ctrl;
                                //foreach (Control x in DIVCustomItem.Controls)
                                foreach (object x in c.Controls)
                                {
                                    if (x.GetType() == typeof(TextBox))
                                    {
                                        string[] Field = ((TextBox)x).ID.Split('-');

                                        foreach (DataRow theDR in theDT.Rows)
                                        {
                                            if ((((TextBox)x).ID.Contains("=") == true) && (((TextBox)x).Enabled == true))
                                            {
                                                string[] Field10 = ((TextBox)x).ID.Replace('=', '-').Split('-');
                                                if (Field10[1] == Convert.ToString(theDR["FieldName"]) && Field10[2] == Convert.ToString(theDR["TableName"]) && Field10[3] == Convert.ToString(theDR["FieldId"]) && (Convert.ToString(theDR["BusRuleId"]) == "13" || Convert.ToString(theDR["BusRuleId"]) == "1"))
                                                {
                                                    if (((TextBox)x).Text == "")
                                                    {
                                                        string scriptblankmultitext = "<script language = 'javascript' defer ='defer' id = 'Color" + theDR["FieldLabel"] + theDR["FieldId"] + "'>\n";
                                                        scriptblankmultitext += "To_Change_Color('lbl" + theDR["FieldLabel"] + "-" + theDR["FieldId"] + "');\n";
                                                        scriptblankmultitext += "</script>\n";
                                                        ClientScript.RegisterStartupScript(this.GetType(), "Color" + theDR["FieldLabel"] + theDR["FieldId"], scriptblankmultitext);
                                                        strmsg += theDR["FieldLabel"] + " is Blank";
                                                        strmsg = strmsg + "\\n";
                                                    }
                                                }
                                            }
                                            if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["TableName"]) && Field[3] == Convert.ToString(theDR["FieldId"]) && (Convert.ToString(theDR["BusRuleId"]) == "13" || Convert.ToString(theDR["BusRuleId"]) == "1"))
                                            {
                                                if ((((TextBox)x).Text == "") && (((TextBox)x).Enabled == true))
                                                {
                                                    //if (Convert.ToString(theDR["BusRuleId"]) != "1")
                                                    //{
                                                    string scriptblanktext = "<script language = 'javascript' defer ='defer' id = 'Color" + theDR["FieldLabel"] + theDR["FieldId"] + "'>\n";
                                                    scriptblanktext += "To_Change_Color('lbl" + theDR["FieldLabel"] + "-" + theDR["FieldId"] + "');\n";
                                                    scriptblanktext += "</script>\n";
                                                    ClientScript.RegisterStartupScript(this.GetType(), "Color" + theDR["FieldLabel"] + theDR["FieldId"], scriptblanktext);
                                                    //}
                                                    strmsg += theDR["FieldLabel"] + " is Blank";
                                                    strmsg = strmsg + "\\n";
                                                }
                                            }

                                        }
                                    }

                                    if (x.GetType() == typeof(HtmlInputRadioButton))
                                    {
                                        string[] Field = ((HtmlInputRadioButton)x).ID.Split('-');
                                        if (Field[0] == "RADIO1" && ((HtmlInputRadioButton)x).Checked == false)
                                        {
                                            Radio1 = Field[3];
                                        }
                                        if (Field[0] == "RADIO2" && ((HtmlInputRadioButton)x).Checked == false)
                                        {
                                            Radio2 = Field[3];
                                        }

                                        foreach (DataRow theDR in theDT.Rows)
                                        {
                                            if (Radio1 == Field[3] && Radio2 == Field[3])
                                            {
                                                if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["TableName"]) && Field[3] == Convert.ToString(theDR["FieldId"]) && (Convert.ToString(theDR["BusRuleId"]) == "13" || Convert.ToString(theDR["BusRuleId"]) == "1"))
                                                {
                                                    //if (Convert.ToString(theDR["BusRuleId"]) != "1")
                                                    //{
                                                    string scriptradio = "<script language = 'javascript' defer ='defer' id = 'Color" + theDR["FieldLabel"] + theDR["FieldId"] + "'>\n";
                                                    scriptradio += "To_Change_Color('lbl" + theDR["FieldLabel"] + "-" + theDR["FieldId"] + "');\n";
                                                    scriptradio += "</script>\n";
                                                    ClientScript.RegisterStartupScript(this.GetType(), "Color" + theDR["FieldLabel"] + theDR["FieldId"], scriptradio);
                                                    //}
                                                    strmsg += theDR["FieldLabel"] + " is not Selected ";
                                                    strmsg = strmsg + "\\n";
                                                }
                                            }
                                        }
                                    }
                                    if (x.GetType() == typeof(DropDownList))
                                    {
                                        string[] Field = ((DropDownList)x).ID.Split('-');
                                        foreach (DataRow theDR in theDT.Rows)
                                        {
                                            if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["TableName"]) && Field[3] == Convert.ToString(theDR["FieldId"]) && (Convert.ToString(theDR["BusRuleId"]) == "13" || Convert.ToString(theDR["BusRuleId"]) == "1"))
                                            {
                                                if ((((DropDownList)x).SelectedValue == "0") && (Field[0].ToString() != "SELECTLISTAuto") && ((DropDownList)x).Enabled == true)
                                                {
                                                    //if (Convert.ToString(theDR["BusRuleId"]) != "1")
                                                    //{
                                                    string scriptdropdown = "<script language = 'javascript' defer ='defer' id = 'Color" + theDR["FieldLabel"] + theDR["FieldId"] + "'>\n";
                                                    scriptdropdown += "To_Change_Color('lbl" + theDR["FieldLabel"] + "-" + theDR["FieldId"] + "');\n";
                                                    scriptdropdown += "</script>\n";
                                                    // ClientScript.RegisterStartupScript(this.GetType(),"Color" + theDR["FieldLabel"] + theDR["FieldId"], scriptdropdown);
                                                    ClientScript.RegisterStartupScript(this.GetType(), "Color" + theDR["FieldLabel"] + theDR["FieldId"], scriptdropdown);
                                                    //}
                                                    strmsg += theDR["FieldLabel"] + " is not Selected";
                                                    strmsg = strmsg + "\\n";
                                                }
                                            }
                                        }
                                    }

                                    if (x.GetType() == typeof(HtmlInputCheckBox))
                                    {
                                        string[] Field = ((HtmlInputCheckBox)x).ID.Split('-');
                                        foreach (DataRow theDR in theDT.Rows)
                                        {
                                            if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["TableName"]) && Field[3] == Convert.ToString(theDR["FieldId"]) && (Convert.ToString(theDR["BusRuleId"]) == "13" || Convert.ToString(theDR["BusRuleId"]) == "1"))
                                            {
                                                if (((HtmlInputCheckBox)x).Checked == false)
                                                {
                                                    //if (Convert.ToString(theDR["BusRuleId"]) != "1")
                                                    //{
                                                    string scriptHtmlchkbox = "<script language = 'javascript' defer ='defer' id = 'Color" + theDR["FieldLabel"] + theDR["FieldId"] + "'>\n";
                                                    scriptHtmlchkbox += "To_Change_Color('lbl" + theDR["FieldLabel"] + "-" + theDR["FieldId"] + "');\n";
                                                    scriptHtmlchkbox += "</script>\n";
                                                    //ClientScript.RegisterStartupScript(this.GetType(),"Color" + theDR["FieldLabel"] + theDR["FieldId"], scriptHtmlchkbox);

                                                    ClientScript.RegisterStartupScript(this.GetType(), "Color" + theDR["FieldLabel"] + theDR["FieldId"], scriptHtmlchkbox);
                                                    //}
                                                    strmsg += theDR["FieldLabel"] + " is not Selected ";
                                                    strmsg = strmsg + "\\n";
                                                }
                                            }
                                        }
                                    }

                                    //if (x.GetType() == typeof(System.Web.UI.WebControls.Panel) && x.ID.StartsWith("Pnl_") == true)
                                    if (x.GetType() == typeof(Panel) && ((Panel)x).ID.StartsWith("Pnl_") == true)
                                    {
                                        string[] Field = ((Panel)x).ID.Split('_');
                                        foreach (DataRow theDR in theDT.Rows)
                                        {
                                            if (Field[1] == theDR["FieldId"].ToString() && ((Panel)x).ToolTip.ToString() == theDR["FieldLabel"].ToString() && (Convert.ToString(theDR["BusRuleId"]) == "13" || Convert.ToString(theDR["BusRuleId"]) == "1"))
                                            {
                                                int NoChecks = 0;
                                                foreach (Control theCntrl in ((Panel)x).Controls)
                                                {
                                                    if (theCntrl.GetType().ToString() == "System.Web.UI.WebControls.CheckBox")
                                                    {
                                                        if (((CheckBox)theCntrl).Checked == true)
                                                            NoChecks = NoChecks + 1;
                                                    }
                                                }

                                                if (NoChecks == 0)
                                                {
                                                    string scriptMultiSelect = "<script language = 'javascript' defer ='defer' id = 'Color" + theDR["FieldLabel"] + theDR["FieldId"] + "'>\n";
                                                    scriptMultiSelect += "To_Change_Color('lbl" + theDR["FieldLabel"] + "-" + theDR["FieldId"] + "');\n";
                                                    scriptMultiSelect += "</script>\n";
                                                    ClientScript.RegisterStartupScript(this.GetType(), "Color" + theDR["FieldLabel"] + theDR["FieldId"], scriptMultiSelect);
                                                    //}
                                                    strmsg += theDR["FieldLabel"] + " is not Selected ";
                                                    strmsg = strmsg + "\\n";
                                                }
                                            }
                                        }
                                    }

                                    if (x.GetType() == typeof(HiddenField))
                                    {
                                        string[] Field = ((HiddenField)x).ID.Split('-');

                                        if (Field.Length == 4)
                                        {
                                            foreach (DataRow theDR in theDT.Rows)
                                            {
                                                if (Field[3] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["FieldId"]) && (Convert.ToString(theDR["BusRuleId"]) == "13" || Convert.ToString(theDR["BusRuleId"]) == "1"))
                                                {
                                                    if (theDS.Tables[0].Rows.Count == 0)
                                                    {
                                                        //if (Convert.ToString(theDR["BusRuleId"]) != "1")
                                                        //{
                                                        string scripthiddenfields = "<script language = 'javascript' defer ='defer' id = 'Color" + theDR["FieldLabel"] + theDR["FieldId"] + "'>\n";
                                                        scripthiddenfields += "To_Change_Color('lbl" + theDR["FieldLabel"] + "-" + theDR["FieldId"] + "');\n";
                                                        scripthiddenfields += "</script>\n";
                                                        //ClientScript.RegisterStartupScript(this.GetType(),"Color" + theDR["FieldLabel"] + theDR["FieldId"], scripthiddenfields);
                                                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Color" + theDR["FieldLabel"] + theDR["FieldId"], scripthiddenfields);
                                                        //}
                                                        strmsg += theDR["FieldLabel"] + " is " + theDR["Name"];
                                                        strmsg = strmsg + "\\n";
                                                    }
                                                }
                                            }
                                        }

                                        if (Field.Length == 5)
                                        {
                                            foreach (DataRow theDR in theDT.Rows)
                                            {
                                                if (Field[3] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["FieldId"]) && Convert.ToString(theDR["BusRuleId"]) == "13" && Convert.ToString(theDR["Value"]) == "37")
                                                {
                                                    if (theDS.Tables[1].Rows.Count == 0)
                                                    {
                                                        if (theDR["Value"].ToString() != "")
                                                        {
                                                            DataView theDV = new DataView((DataTable)Session["DrugTypeName"]);
                                                            theDV.RowFilter = "DrugTypeID=" + Convert.ToInt32(theDR["Value"]).ToString();
                                                            DataTable theDrugNameDT = theDV.ToTable();
                                                            strmsg += theDrugNameDT.Rows[0]["DrugTypeName"] + " is Required Field";
                                                            strmsg = strmsg + "\\n";
                                                        }
                                                    }
                                                }
                                                else if (Field[3] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["FieldId"]) && (Convert.ToString(theDR["BusRuleId"]) == "13" || Convert.ToString(theDR["BusRuleId"]) == "1") && Convert.ToString(theDR["Value"]) != "37")
                                                {
                                                    if (theDS.Tables[2].Rows.Count == 0)
                                                    {
                                                        if (theDR["Value"].ToString() != "")
                                                        {
                                                            DataView theDV = new DataView((DataTable)Session["DrugTypeName"]);
                                                            theDV.RowFilter = "DrugTypeID=" + Convert.ToInt32(theDR["Value"].ToString()).ToString();
                                                            DataTable theDrugNameDT = theDV.ToTable();
                                                            strmsg += theDrugNameDT.Rows[0]["DrugTypeName"] + " is Required Field";
                                                            strmsg = strmsg + "\\n";
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }//
                            }
                        }
                    }
                }
                //MultiSelect Validation
                foreach (object obj in container.Controls)
                {
                    if (obj is AjaxControlToolkit.TabPanel)
                    {
                        AjaxControlToolkit.TabPanel tabPanel = (AjaxControlToolkit.TabPanel)obj;
                        foreach (object ctrl in tabPanel.Controls)
                        {
                            if (ctrl is Control)
                            {
                                Control c = (Control)ctrl;
                                foreach (Control y in ((Control)ctrl).Controls)
                                {
                                    if (y.GetType() == typeof(System.Web.UI.WebControls.Panel) && ((Panel)y).ID.StartsWith("Pnl_") == true)
                                    {
                                        string[] Field = ((Panel)y).ID.Split('-');
                                        foreach (Control z in y.Controls)
                                        {
                                            if (z.GetType() == typeof(System.Web.UI.WebControls.CheckBox))
                                            {
                                                TotCount++;
                                                if (((CheckBox)z).Checked == false)
                                                {
                                                    FalseCount++;
                                                }
                                            }
                                        }
                                        foreach (DataRow theMultiDR in theDT.Rows)
                                        {
                                            if (Convert.ToString(theMultiDR["ControlId"]) == "9" && Field[2] == Convert.ToString(theMultiDR["FieldID"]) && (Convert.ToInt32(theMultiDR["BusRuleId"]) == 13 || Convert.ToInt32(theMultiDR["BusRuleId"]) == 1))
                                            {
                                                MultiSelectName = Convert.ToString(theMultiDR["Name"]);
                                                MultiSelectLabel = Convert.ToString(theMultiDR["FieldLabel"]);
                                                if (TotCount == FalseCount)
                                                {
                                                    string scriptMultiSelect = "<script language = 'javascript' defer ='defer' id = 'Color" + theMultiDR["FieldLabel"] + theMultiDR["FieldId"] + "'>\n";
                                                    scriptMultiSelect += "To_Change_Color('lbl" + theMultiDR["FieldLabel"] + "-" + theMultiDR["FieldId"] + "');\n";
                                                    scriptMultiSelect += "</script>\n";
                                                    //ClientScript.RegisterStartupScript(this.GetType(),"Color" + theMultiDR["FieldLabel"] + theMultiDR["FieldId"], scriptMultiSelect);

                                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Color" + theMultiDR["FieldLabel"] + theMultiDR["FieldId"], scriptMultiSelect);
                                                    strmsg += MultiSelectLabel + " is not Selected ";
                                                    strmsg = strmsg + "\\n";
                                                }
                                            }
                                        }

                                        TotCount = 0; FalseCount = 0;
                                        MultiSelectName = ""; MultiSelectLabel = "";
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
            finally { }

            return strmsg;
        }

        private void DrugDataBinding(String BtnId, int DrugTypeId)
        {
            int VisitID = Convert.ToInt32(Session["PatientVisitId"]);
            int PatientID = Convert.ToInt32(Session["PatientId"]);
            DataSet theDSDrug = new DataSet();
            ICustomForm MgrBindValue = (ICustomForm)ObjectFactory.CreateInstance(ObjFactoryParameter);
            StringBuilder StrDrug = new StringBuilder();
            StrDrug.Append("Select a.ptn_pharmacy_pk, a.Ptn_pk, a.VisitID, a.LocationID, a.OrderedBy,");
            StrDrug.Append(" a.OrderedByDate, a.DispensedBy, a.DispensedByDate, a.Signature, a.UserID,");
            StrDrug.Append(" b.ptn_pharmacy_pk,b.Drug_Pk, b.GenericID, b.StrengthID, b.FrequencyID, convert(decimal,b.SingleDose)[SingleDose],");
            StrDrug.Append(" b.Duration, b.OrderedQuantity, b.DispensedQuantity, b.Financed, c.DrugTypeId ");
            StrDrug.Append(" from dbo.ord_PatientPharmacyOrder a inner join dbo.dtl_PatientPharmacyOrder b on a.ptn_pharmacy_pk = b.ptn_pharmacy_pk");
            StrDrug.Append(" Inner join Vw_Drug c on b.Drug_Pk = c.Drug_Pk");
            StrDrug.Append(" where a.ptn_pharmacy_pk =");
            StrDrug.Append(" (Select ptn_pharmacy_pk from ord_PatientPharmacyOrder where VisitID='" + VisitID + "'");
            StrDrug.Append(" and Ptn_pk='" + PatientID + "')");
            StrDrug.Append(" UNION ");
            StrDrug.Append("Select a.ptn_pharmacy_pk, a.Ptn_pk, a.VisitID, a.LocationID, a.OrderedBy,");
            StrDrug.Append(" a.OrderedByDate, a.DispensedBy, a.DispensedByDate, a.Signature, a.UserID,");
            StrDrug.Append(" b.ptn_pharmacy_pk,b.Drug_Pk, b.GenericID, b.StrengthID, b.FrequencyID, convert(decimal,b.SingleDose)[SingleDose],");
            StrDrug.Append(" b.Duration, b.OrderedQuantity, b.DispensedQuantity, b.Financed, c.DrugTypeId ");
            StrDrug.Append(" from dbo.ord_PatientPharmacyOrder a inner join dbo.dtl_PatientPharmacyOrder b on a.ptn_pharmacy_pk = b.ptn_pharmacy_pk");
            StrDrug.Append(" Inner join Vw_Generic c on b.GenericId = c.GenericId");
            StrDrug.Append(" where a.ptn_pharmacy_pk =");
            StrDrug.Append(" (Select ptn_pharmacy_pk from ord_PatientPharmacyOrder where VisitID='" + VisitID + "'");
            StrDrug.Append(" and Ptn_pk='" + PatientID + "')");
            StrDrug.Append(" Select a.ptn_pharmacy_pk, a.Ptn_pk, a.VisitID, a.LocationID, a.OrderedBy,");
            StrDrug.Append(" a.OrderedByDate, a.DispensedBy, a.DispensedByDate, a.Signature, a.UserID,");
            StrDrug.Append(" b.ptn_pharmacy_pk,b.Drug_Pk, b.GenericID, convert(decimal,b.Dose)[Dose], b.UnitId, b.FrequencyID, convert(decimal,b.SingleDose)[SingleDose],");
            StrDrug.Append(" b.Duration, b.OrderedQuantity, b.DispensedQuantity, b.Financed, c.DrugTypeId");
            StrDrug.Append(" from dbo.ord_PatientPharmacyOrder a inner join dbo.dtl_PatientPharmacyOrderNonARV b on a.ptn_pharmacy_pk = b.ptn_pharmacy_pk ");
            StrDrug.Append(" inner join lnk_drugtypegeneric c on c.GenericId=b.GenericId");
            StrDrug.Append(" where a.ptn_pharmacy_pk =");
            StrDrug.Append(" (Select ptn_pharmacy_pk from ord_PatientPharmacyOrder where VisitID='" + VisitID + "'");
            StrDrug.Append(" and Ptn_pk='" + PatientID + "')");
            StrDrug.Append(" UNION ");
            StrDrug.Append("Select a.ptn_pharmacy_pk, a.Ptn_pk, a.VisitID, a.LocationID, a.OrderedBy,");
            StrDrug.Append(" a.OrderedByDate, a.DispensedBy, a.DispensedByDate, a.Signature, a.UserID,");
            StrDrug.Append(" b.ptn_pharmacy_pk,b.Drug_Pk, b.GenericID, convert(decimal,b.Dose)[Dose], b.UnitId, b.FrequencyID, convert(decimal,b.SingleDose)[SingleDose],");
            StrDrug.Append(" b.Duration, b.OrderedQuantity, b.DispensedQuantity, b.Financed, c.DrugTypeId");
            StrDrug.Append(" from dbo.ord_PatientPharmacyOrder a inner join dbo.dtl_PatientPharmacyOrderNonARV b on a.ptn_pharmacy_pk = b.ptn_pharmacy_pk");
            StrDrug.Append(" inner join vw_drug c on b.Drug_Pk=c.Drug_pk");
            StrDrug.Append(" where a.ptn_pharmacy_pk =");
            StrDrug.Append(" (Select ptn_pharmacy_pk from ord_PatientPharmacyOrder where VisitID='" + VisitID + "'");
            StrDrug.Append(" and Ptn_pk='" + PatientID + "')");
            theDSDrug = MgrBindValue.Common_GetSaveUpdate(StrDrug.ToString());

            DataTable dtARVDrug = ARVDrug();
            foreach (DataRow thedr in theDSDrug.Tables[0].Rows)
            {
                DataRow tmpDR = dtARVDrug.NewRow();
                tmpDR[0] = thedr["Drug_Pk"];
                tmpDR[1] = thedr["GenericID"];
                tmpDR[2] = thedr["SingleDose"];
                tmpDR[3] = thedr["FrequencyID"];
                tmpDR[4] = thedr["Duration"];
                tmpDR[5] = thedr["OrderedQuantity"];
                tmpDR[6] = thedr["DispensedQuantity"];
                tmpDR[7] = thedr["Financed"];
                tmpDR[8] = thedr["DrugTypeId"];
                dtARVDrug.Rows.Add(tmpDR);
            }
            DataView theDVARV = new DataView(dtARVDrug);
            //theDVARV.RowFilter = "DrugTypeId=" + DrugTypeId;
            theDVARV.RowFilter = "DrugTypeId=37";
            DataTable theARVDT = theDVARV.ToTable();

            //Setting Session
            if (!IsPostBack)
            {
                if (DrugTypeId == 37)
                {
                    DataTable theDTARVDrug = PtnCustomformselectedDataTableDrug(theARVDT, DrugTypeId);
                    Session["Selected" + DrugType + ""] = theDTARVDrug;
                }
            }

            foreach (DataRow drgdr in theARVDT.Rows)
            {
                int DrugId = Convert.ToInt32(drgdr["GenericID"]) == 0 ? Convert.ToInt32(drgdr["DrugId"]) : Convert.ToInt32(drgdr["GenericID"]);
                if ((DataTable)Session["Selected" + DrugType + ""] != null)
                {
                    foreach (DataRow drgdrII in ((DataTable)Session["Selected" + DrugType + ""]).Rows)
                    {
                        if (DrugId == Convert.ToInt32(drgdrII["DrugId"]) && Convert.ToInt32(drgdrII["Flag"]) == 1)
                        {
                            BindDrugControls(Convert.ToInt32(drgdrII["DrugId"]), Convert.ToInt32(drgdrII["Generic"]), Convert.ToInt32(drgdrII["DrugTypeId"]), Convert.ToInt32(drgdrII["Flag"]));
                        }
                    }
                }
            }

            foreach (DataRow drgdr1 in theARVDT.Rows)
            {
                FillDrugData(DIVCustomItem, drgdr1);
            }

            ///Section for NON ARV Drug
            DataTable dtNonARVDrug = NonARVDrug();
            //foreach (DataRow thedr in theDSDrug.Tables[1].Rows)
            foreach (DataRow thedr in theDSDrug.Tables[0].Rows)
            {
                DataRow theRow = dtNonARVDrug.NewRow();
                theRow[0] = thedr["Drug_pk"];
                theRow[1] = thedr["GenericId"];
                //theRow[2] = thedr["UnitId"];
                theRow[2] = 0;
                theRow[3] = thedr["FrequencyID"];
                theRow[4] = thedr["SingleDose"];
                theRow[5] = thedr["Duration"];
                theRow[6] = thedr["OrderedQuantity"];
                theRow[7] = thedr["DispensedQuantity"];
                theRow[8] = thedr["Financed"];
                theRow[9] = thedr["DrugTypeId"];
                dtNonARVDrug.Rows.Add(theRow);
            }

            DataView theDV = new DataView(dtNonARVDrug);
            theDV.RowFilter = "DrugTypeId<>37";
            DataTable theNonARVDT = theDV.ToTable();

            if (!IsPostBack)
            {
                //Setting Session
                //if (DrugTypeId != 37 && DrugTypeId!=36)
                if (DrugTypeId != 37)
                {
                    DataTable theDTNonARVDrug = PtnCustomformselectedDataTableDrug(theNonARVDT, DrugTypeId);
                    Session["Selected" + DrugType + ""] = theDTNonARVDrug;
                }
            }

            foreach (DataRow drgdr in theNonARVDT.Rows)
            {
                int DrugId = Convert.ToInt32(drgdr["GenericID"]) == 0 ? Convert.ToInt32(drgdr["DrugId"]) : Convert.ToInt32(drgdr["GenericID"]);
                if ((DataTable)Session["Selected" + DrugType + ""] != null)
                {
                    foreach (DataRow drgdrII in ((DataTable)Session["Selected" + DrugType + ""]).Rows)
                    {
                        if (DrugId == Convert.ToInt32(drgdrII["DrugId"]) && Convert.ToInt32(drgdrII["Flag"]) == 1)
                        {
                            BindDrugControls(Convert.ToInt32(drgdrII["DrugId"]), Convert.ToInt32(drgdrII["Generic"]), Convert.ToInt32(drgdrII["DrugTypeId"]), Convert.ToInt32(drgdrII["Flag"]));
                        }
                    }
                }
            }
            foreach (DataRow drgdr1 in theNonARVDT.Rows)
            {
                FillDrugData(DIVCustomItem, drgdr1);
            }
        }

        private void DrugsHeading(int DrugType)
        {
            Panel thelblPnl = new Panel();

            #region "ARV Medication"

            if (thelblPnl.Controls.Count < 1 && (DrugType == 37 || DrugType == 36))
            {
                Panel PnlHeading = new Panel();
                PnlHeading.ID = "pnlARV" + DrugType;
                PnlHeading.Height = 20;
                PnlHeading.Width = 840;
                PnlHeading.Font.Bold = true;
                thelblPnl.Controls.Clear();

                Label theSP = new Label();
                theSP.ID = "lblDrgSp" + DrugType;
                theSP.Width = 5;
                theSP.Text = "";
                PnlHeading.Controls.Add(theSP);

                Label theLabel1 = new Label();
                theLabel1.ID = "lblDrgNm" + DrugType;
                theLabel1.Text = "Drug Name";
                theLabel1.Width = 410;
                PnlHeading.Controls.Add(theLabel1);

                //Label theLabel2 = new Label();
                //theLabel2.ID = "lblDrgDose" + DrugType;
                //theLabel2.Text = "Dose";
                //theLabel2.Width = 100;
                //PnlHeading.Controls.Add(theLabel2);

                Label theLabel4 = new Label();
                theLabel4.ID = "lblDrgFrequency" + DrugType;
                theLabel4.Text = "Frequency";
                theLabel4.Width = 95;
                PnlHeading.Controls.Add(theLabel4);

                //Label theLabel5 = new Label();
                //theLabel5.ID = "lblDrgDuration" + DrugType;
                //theLabel5.Text = "Duration";
                //theLabel5.Width = 120;
                //theLabel5.CssClass = "required";
                //PnlHeading.Controls.Add(theLabel5);

                Label theLabel6 = new Label();
                theLabel6.ID = "lblDrgPrescribed" + DrugType;
                theLabel6.Text = "Qty. Prescribed";
                theLabel6.Width = 120;
                PnlHeading.Controls.Add(theLabel6);

                Label theLabel7 = new Label();
                theLabel7.ID = "lblDrgDispensed" + DrugType;
                theLabel7.Text = "Qty. Dispensed";
                theLabel7.Width = 110;
                PnlHeading.Controls.Add(theLabel7);

                Label theFinLbl = new Label();
                theFinLbl.ID = "lblAddARVFin" + DrugType;
                theFinLbl.Text = "Prophylaxis";
                PnlHeading.Controls.Add(theFinLbl);
                DIVCustomItem.Controls.Add(PnlHeading);
            }

            #endregion "ARV Medication"

            #region "Non-ARV Medication"

            else if (thelblPnl.Controls.Count < 1 && (DrugType != 37 && DrugType != 36))
            {
                /////////////////////////////////////////////////
                Panel theheaderPnl = new Panel();
                theheaderPnl.ID = "pnlHeaderOtherDrug" + DrugType; ;
                theheaderPnl.Height = 20;
                theheaderPnl.Width = 840;
                theheaderPnl.Font.Bold = true;
                theheaderPnl.Controls.Clear();

                Label theSP = new Label();
                theSP.ID = "lblDrgSp" + DrugType; ;
                theSP.Width = 5;
                theSP.Text = "";
                theheaderPnl.Controls.Add(theSP);

                Label theLabel1 = new Label();
                theLabel1.ID = "lblDrgNm" + DrugType; ;
                theLabel1.Text = "Drug Name";
                theLabel1.Width = 360;
                theheaderPnl.Controls.Add(theLabel1);

                Label theSP1 = new Label();
                theSP1.ID = "lblDrgSp1" + DrugType; ;
                theSP1.Width = 10;
                theSP1.Text = "";
                theheaderPnl.Controls.Add(theSP1);

                //Label theLabel2 = new Label();
                //theLabel2.ID = "lblDrgDose" + DrugType; ;
                //theLabel2.Text = "Dose";
                //theLabel2.Width = 62;
                //theheaderPnl.Controls.Add(theLabel2);

                //Label theSP2 = new Label();
                //theSP2.ID = "lblDrgSp2" + DrugType; ;
                //theSP2.Width = 30;
                //theSP2.Text = "";
                //theheaderPnl.Controls.Add(theSP2);

                //Label theLabel3 = new Label();
                //theLabel3.ID = "lblDrgUnits" + DrugType; ;
                //theLabel3.Text = "Unit";
                //theLabel3.Width = 88;
                //theheaderPnl.Controls.Add(theLabel3);

                Label theLabel4 = new Label();
                theLabel4.ID = "lblDrgFrequency" + DrugType; ;
                theLabel4.Text = "Frequency";
                theLabel4.Width = 90;
                theheaderPnl.Controls.Add(theLabel4);

                //Label theLabel5 = new Label();
                //theLabel5.ID = "lblDrgDuration" + DrugType; ;
                //theLabel5.Text = "Duration";
                //theLabel5.Width = 100;
                //theLabel5.CssClass = "required";
                //theheaderPnl.Controls.Add(theLabel5);

                Label theLabel6 = new Label();
                theLabel6.ID = "lblDrgPrescribed" + DrugType; ;
                theLabel6.Text = "Qty. Prescribed";
                theLabel6.Width = 100;
                theheaderPnl.Controls.Add(theLabel6);

                Label theLabel7 = new Label();
                theLabel7.ID = "lblDrgDispensed" + DrugType; ;
                theLabel7.Text = "Qty. Dispensed";
                theLabel7.Width = 100;
                theheaderPnl.Controls.Add(theLabel7);

                Label theLabel8 = new Label();
                theLabel8.ID = "lblDrgFinanced" + DrugType; ;
                theLabel8.Text = "Prophylaxis";
                theLabel8.Width = 10;
                theheaderPnl.Controls.Add(theLabel8);
                DIVCustomItem.Controls.Add(theheaderPnl);
            }

            #endregion "Non-ARV Medication"
        }

        private bool FieldValidation()
        {
            IIQCareSystem IQCareSecurity = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
            DateTime theCurrentDate = IQCareSecurity.SystemDate();
            AjaxControlToolkit.TabContainer container = (AjaxControlToolkit.TabContainer)tabContainer;
            DataTable dtCustomitems = SetControlIDs(container);

            int iCheckBoxCount = 0;
            int iOnSelectDateCount = 0;
            int iCommentBoxCount = 0;
            

            // todo
            if (IsSIngleVisit == false)
            {
                if (txtvisitDate.Text.Trim() == "")
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["Control"] = "Visit Date";
                    IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                    txtvisitDate.Focus();
                    return false;
                }
            }
            else
            {
                if (txtvisitDate.Text.Trim() == "")
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["Control"] = "Date encounter";
                    IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                    txtvisitDate.Focus();
                    return false;
                }

                if (Session["RegDate"] != null && txtvisitDate.Text != "")
                {
                    if (Convert.ToDateTime(txtvisitDate.Text) < Convert.ToDateTime(Session["RegDate"]))
                    {
                        txtvisitDate.Focus();
                        MsgBuilder totalMsgBuilder = new MsgBuilder();
                        totalMsgBuilder.DataElements["MessageText"] = "Encounter Date should not be less then registration date";
                        IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
                        return false;
                    }
                }
            }
            // todo
            //if (txtvisitDate.Text != "01-01-1900")
          
            if (IsSIngleVisit == false)
            {
                ICustomForm MgrValidate = (ICustomForm)ObjectFactory.CreateInstance(ObjFactoryParameter);
                DataSet theDS = MgrValidate.Validate(theHeader.InnerText, txtvisitDate.Text, this.PatientId, this.ModuleId);
                DateTime VisitDate = Convert.ToDateTime((txtvisitDate.Text));
                DateTime RegisDate = Convert.ToDateTime(theDS.Tables[1].Rows[0]["StartDate"]);
                if (VisitDate > theCurrentDate)
                {
                    IQCareMsgBox.Show("CompareDate5", this);
                    txtvisitDate.Focus();
                    return false;
                }

                if (VisitDate < RegisDate)
                {
                    IQCareMsgBox.Show("PMTCTCustomRegDateValidate", this);
                    txtvisitDate.Focus();
                    return false;
                }

                if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
                {
                    if (txtvisitDate.Text != "")
                    {
                        if (txtvisitDate.Text != Convert.ToString(ViewState["VisitDate"]))
                        {
                            if (Convert.ToInt32(theDS.Tables[0].Rows[0]["Visit"]) >= 1)
                            {
                                IQCareMsgBox.Show("PMTCTDuplicateDate", this);
                                return false;
                            }
                        }
                    }
                }
                else
                {
                    if (txtvisitDate.Text != "")
                    {
                        if (Convert.ToInt32(theDS.Tables[0].Rows[0]["Visit"]) >= 1)
                        {
                            IQCareMsgBox.Show("PMTCTDuplicateDate", this);
                            return false;
                        }
                    }
                }
            }
            // Code By Deepak
            foreach (DataRow dr in dtCustomitems.Rows)
            {
                if (dr[3].ToString().Contains("dtl_ICD10Field") && dr[0].ToString().Contains("%") && dr[2].ToString().Contains("1"))
                {
                    iCheckBoxCount++;
                }
                if (dr[3].ToString().Contains("dtl_ICD10Field") && dr[0].ToString().Contains("OnSetDate") && dr[2].ToString() != string.Empty)
                {
                    iOnSelectDateCount++;
                }
                if (dr[3].ToString().Contains("dtl_ICD10Field") && dr[0].ToString().Contains("Comment") && dr[2].ToString() != string.Empty)
                {
                    iCommentBoxCount++;
                }
            }

            //if (iCheckBoxCount != iOnSelectDateCount && iOnSelectDateCount > iCheckBoxCount)
            //{
            //    IQCareMsgBox.Show("CheckICD10", this);
            //    return false;
            //}
            //else if (iCheckBoxCount != iCommentBoxCount && iCommentBoxCount > iCheckBoxCount)
            //{
            //    IQCareMsgBox.Show("CheckICD10", this);
            //    return false;
            //}
            return true;
        }

        private void FillDrugData(Control Cntrl, DataRow theDR)
        {
            foreach (Control z in Cntrl.Controls)
            {
                if (z.GetType() == typeof(Panel))
                {
                    foreach (Control x in z.Controls)
                    {
                        if (x.GetType() == typeof(DropDownList))
                        {
                            if (x.ID.StartsWith("theUnitDrug" + theDR["DrugId"] + ""))
                            {
                                ((DropDownList)x).Text = theDR["UnitId"].ToString();
                            }

                            if (x.ID.StartsWith("theUnitGeneric" + theDR["GenericId"] + ""))
                            {
                                ((DropDownList)x).Text = theDR["UnitId"].ToString();
                            }

                            if (x.ID.StartsWith("drugFrequency" + theDR["DrugId"] + ""))
                            {
                                ((DropDownList)x).Text = theDR["FrequencyID"].ToString();
                            }
                            if (x.ID.StartsWith("GenericFrequency" + theDR["GenericId"] + ""))
                            {
                                ((DropDownList)x).Text = theDR["FrequencyId"].ToString();
                            }

                            if (x.ID.StartsWith("ARVdrgStrength" + theDR["DrugId"] + ""))
                            {
                                ((DropDownList)x).Text = Convert.ToString(theDR["Dose"]);
                            }
                            if (x.ID.StartsWith("ARVGenericStrength" + theDR["GenericId"] + ""))
                            {
                                ((DropDownList)x).Text = Convert.ToString(theDR["Dose"]);
                            }

                            if (x.ID.StartsWith("ARVdrgFrequency" + theDR["DrugId"] + ""))
                            {
                                ((DropDownList)x).Text = theDR["FrequencyId"].ToString();
                            }
                            if (x.ID.StartsWith("ARVGenericFrequency" + theDR["GenericId"] + ""))
                            {
                                ((DropDownList)x).Text = theDR["FrequencyId"].ToString();
                            }
                        }
                        else if (x.GetType() == typeof(TextBox))
                        {
                            if (x.ID.StartsWith("DrugDuration" + theDR["DrugId"] + ""))
                            {
                                ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["Duration"]), 0));
                            }

                            if (x.ID.StartsWith("GenericDuration" + theDR["GenericId"] + ""))
                            {
                                ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["Duration"]), 0));
                            }

                            if (x.ID.StartsWith("drugQtyPrescribed" + theDR["DrugId"] + ""))
                            {
                                //((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["Qtyordered"]), 0));
                                if (Convert.ToInt32(theDR["DrugTypeId"]) == 37)
                                {
                                    ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["QtyPrescribed"]), 0));
                                }
                                else
                                {
                                    ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["Qtyordered"]), 0));
                                }
                            }

                            if (x.ID.StartsWith("genericQtyPrescribed" + theDR["GenericId"] + ""))
                            {
                                //((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["Qtyordered"]), 0));
                                if (Convert.ToInt32(theDR["DrugTypeId"]) == 37)
                                {
                                    ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["QtyPrescribed"]), 0));
                                }
                                else
                                {
                                    ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["Qtyordered"]), 0));
                                }
                            }

                            if (x.ID.StartsWith("drugQtyDispensed" + theDR["DrugId"] + ""))
                            {
                                ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["Qtydispensed"]), 0));
                            }

                            if (x.ID.StartsWith("genericQtyDispensed" + theDR["GenericId"] + ""))
                            {
                                ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["Qtydispensed"]), 0));
                            }
                            if (x.ID.StartsWith("theDoseDrug" + theDR["DrugId"] + ""))
                            {
                                ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["Singledose"]), 0));
                            }
                            if (x.ID.StartsWith("theDoseGeneric" + theDR["GenericId"] + ""))
                            {
                                ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["Singledose"]), 0));
                            }

                            if (x.ID.StartsWith("theDoseGeneric" + theDR["DrugId"] + ""))
                            {
                                ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["Duration"]), 0));
                            }
                            if (x.ID.StartsWith("ARVdrgDuration" + theDR["DrugId"] + ""))
                            {
                                ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["Duration"]), 0));
                            }

                            if (x.ID.StartsWith("ARVGenericDuration" + theDR["GenericId"] + ""))
                            {
                                ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["Duration"]), 0));
                            }

                            if (x.ID.StartsWith("ARVdrgQtyPrescribed" + theDR["DrugId"] + ""))
                            {
                                ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["QtyPrescribed"]), 0));
                            }

                            if (x.ID.StartsWith("ARVGenericQtyPrescribed" + theDR["GenericId"] + ""))
                            {
                                ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["QtyPrescribed"]), 0));
                            }

                            if (x.ID.StartsWith("drgQtyDispensed" + theDR["DrugId"] + ""))
                            {
                                ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["QtyDispensed"]), 0));
                            }
                            if (x.ID.StartsWith("ARVdrgQtyDispensed" + theDR["DrugId"] + ""))
                            {
                                ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["QtyDispensed"]), 0));
                            }

                            if (x.ID.StartsWith("ARVGenericQtyDispensed" + theDR["GenericId"] + ""))
                            {
                                ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["QtyDispensed"]), 0));
                            }
                        }
                        else if (x.GetType() == typeof(CheckBox))
                        {
                            if (x.ID.StartsWith("FinChkDrug" + theDR["DrugId"] + ""))
                            {
                                ((CheckBox)x).Checked = Convert.ToBoolean(theDR["ARFinance"]);
                            }
                            if (x.ID.StartsWith("FinChkGeneric" + theDR["GenericId"] + ""))
                            {
                                ((CheckBox)x).Checked = Convert.ToBoolean(theDR["ARFinance"]);
                            }
                            if (x.ID.StartsWith("ARVDrugFinChk" + theDR["DrugId"] + ""))
                            {
                                ((CheckBox)x).Checked = Convert.ToBoolean(theDR["ARFinance"]);
                            }

                            if (x.ID.StartsWith("ARVGenericFinChk" + theDR["GenericId"] + ""))
                            {
                                ((CheckBox)x).Checked = Convert.ToBoolean(theDR["ARFinance"]);
                            }
                        }
                    }
                }
            }
        }

        private void FillLabData(Control Cntrl, DataRow theDR)
        {
            int y = 0;
            foreach (Control z in Cntrl.Controls)
            {
                if (z.GetType() == typeof(Panel))
                {
                    foreach (Control x in z.Controls)
                    {
                        if (x.GetType() == typeof(TextBox))
                        {
                            if (x.ID.StartsWith("LabResult"))
                                y = Convert.ToInt32(x.ID.Substring(9, x.ID.Length - 9));
                            if (y == Convert.ToInt32(theDR["SubTestId"]))
                            {
                                ((TextBox)x).Text = theDR["TestResults1"].ToString();
                            }
                        }
                        else if (x.GetType() == typeof(DropDownList))
                        {
                            if (x.ID.StartsWith("ddlLabResult"))
                                y = Convert.ToInt32(x.ID.Substring(12, x.ID.Length - 12));
                            if (y == Convert.ToInt32(theDR["SubTestId"]))
                            {
                                ((DropDownList)x).SelectedValue = theDR["TestResults1"].ToString();
                            }
                        }

                        //else if (x.GetType() == typeof(System.Web.UI.WebControls.CheckBox))
                        //{
                        //    if (x.ID.ToUpper().StartsWith("FinChkLab"))
                        //        y = Convert.ToInt32(x.ID.Substring(6, x.ID.Length - 6));
                        //    if (y == Convert.ToInt32(theDR["SubTestId"]))
                        //    {
                        //        ((CheckBox)x).Checked = Convert.ToBoolean(theDR["Financed"]);
                        //    }
                        //}
                    }
                }
            }
        }

        private string FillRegimen(DataTable theDT)
        {
            string theRegimen = "";
            foreach (DataRow theDR in theDT.Rows)
            {
                if (Convert.ToString(theDR["DrugAbbr"]) != "")
                {
                    if (theRegimen == "")
                    {
                        theRegimen = Convert.ToString(theDR["DrugAbbr"]);
                    }
                    else
                    {
                        theRegimen = theRegimen + "/" + Convert.ToString(theDR["DrugAbbr"]);
                    }
                }
                else
                {
                    if (theRegimen == "")
                    {
                        theRegimen = Convert.ToString(theDR["DrugName"]);
                    }
                    else
                    {
                        theRegimen = theRegimen + "/" + Convert.ToString(theDR["DrugName"]);
                    }
                }
            }
            return theRegimen;
        }

        private Type GetControlDataType(int typeid)
        {
            Type typeofdata = typeof(string);
            if (typeid == 3 || typeid == 4 || typeid == 14 || typeid == 15)
            {
                typeofdata = typeof(int);
            }
            if (typeid == 2)
            {
                typeofdata = typeof(decimal);
            }
            if (typeid == 5)
            {
                typeofdata = typeof(DateTime);
            }
            if (typeid == 7 || typeid == 6)
            {
                typeofdata = typeof(bool);
            }
            return typeofdata;
        }

        private int GetFilterId(string fieldId, string fieldLabel)
        {
            int DrugTypeId = 0;
            DataTable theDT = (DataTable)ViewState["BusRule"];
            foreach (DataRow DR in theDT.Rows)
            {
                if (Convert.ToString(DR["FieldID"]) == fieldId && Convert.ToString(DR["FieldName"]) == fieldLabel && (Convert.ToString(DR["BusRuleId"]) == "11" || Convert.ToString(DR["BusRuleId"]) == "10"))
                {
                    DrugTypeId = Convert.ToInt32(DR["Value"]);
                }
            }
            return DrugTypeId;
        }

        private string GetGridViewControlValue(Control theControl, string columnName, DataTable dt)
        {
            string ret = string.Empty;

            string[] regimen;
            string[] controlid;

            foreach (object obj in theControl.Controls)
            {
                if (obj is AjaxControlToolkit.TabPanel)
                {
                    AjaxControlToolkit.TabPanel tabPanel = (AjaxControlToolkit.TabPanel)obj;
                    foreach (object ctrl in tabPanel.Controls)
                    {
                        if (ctrl is Control)
                        {
                            Control c = (Control)ctrl;
                            foreach (object x in c.Controls)
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    if (x.GetType() == typeof(TextBox))
                                    {
                                        if (((TextBox)x).ID.Contains("DTL_CUSTOMFORM"))
                                        {
                                            if (dt.Rows[i]["FieldLabel"].ToString().ToUpper() == columnName.ToUpper())
                                            {
                                                if (((TextBox)x).ID.Contains("-" + dt.Rows[i]["FieldName"].ToString() + "-"))
                                                {
                                                    ret = ((TextBox)x).Text;

                                                    ((TextBox)x).Text = "";

                                                    if (((TextBox)x).ID.Contains("TXTReg-"))
                                                    {
                                                        regimen = ((TextBox)x).ID.Split('=');
                                                        controlid = regimen[0].Split('-');
                                                        if ((regimen.Length > 1) && (controlid.Length > 2))
                                                        {
                                                            if (Session["SelectedReg" + controlid[3].ToString() + regimen[1].ToString() + ""] != null)
                                                            {
                                                                Session["SelectedReg" + controlid[3].ToString() + regimen[1].ToString() + ""] = null;
                                                            }
                                                        }
                                                    }
                                                    return ret;
                                                }
                                            }
                                        }
                                    }
                                    if (x.GetType() == typeof(HtmlInputRadioButton))
                                    {
                                        if (((HtmlInputRadioButton)x).ID.Contains("DTL_CUSTOMFORM"))
                                        {
                                            if (((HtmlInputRadioButton)x).ID.Contains("-" + dt.Rows[i]["FieldName"].ToString() + "-"))
                                            {
                                                if (((HtmlInputRadioButton)x).ID.Contains("RADIO1-"))
                                                {
                                                    if (dt.Rows[i]["FieldLabel"].ToString().ToUpper() == columnName.ToUpper())
                                                    {
                                                        if (((HtmlInputRadioButton)x).Checked == true)
                                                        {
                                                            if (((HtmlInputRadioButton)x).Visible == true)
                                                                ret = "true";
                                                            else
                                                                ret = "";

                                                            ((HtmlInputRadioButton)x).Checked = false;

                                                            return ret;
                                                        }
                                                    }
                                                }
                                            }
                                            if (((HtmlInputRadioButton)x).ID.Contains("RADIO2-"))
                                            {
                                                if (((HtmlInputRadioButton)x).ID.Contains("-" + dt.Rows[i]["FieldName"].ToString() + "-"))
                                                {
                                                    if (dt.Rows[i]["FieldLabel"].ToString().ToUpper() == columnName.ToUpper())
                                                    {
                                                        if (((HtmlInputRadioButton)x).Checked == true)
                                                        {
                                                            if (((HtmlInputRadioButton)x).Visible == true)
                                                                ret = "false";
                                                            else
                                                                ret = "";

                                                            ((HtmlInputRadioButton)x).Checked = false;

                                                            return ret;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (x.GetType() == typeof(DropDownList))
                                    {
                                        if (((DropDownList)x).ID.Contains("DTL_CUSTOMFORM"))
                                        {
                                            if (((DropDownList)x).ID.Contains("-" + dt.Rows[i]["FieldName"].ToString() + "-"))
                                            {
                                                if (dt.Rows[i]["FieldLabel"].ToString().ToUpper() == columnName.ToUpper())
                                                {
                                                    if (((DropDownList)x).Enabled == true)
                                                    {
                                                        if (((DropDownList)x).SelectedIndex == 0)
                                                        {
                                                            if (ret == "")
                                                            {
                                                                ret = "";
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (ret == "")
                                                            {
                                                                ret = ((DropDownList)x).SelectedItem.Text;
                                                            }
                                                            else
                                                            {
                                                                ret = ret + "-" + ((DropDownList)x).SelectedItem.Text;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ret = "";
                                                    }

                                                    ((DropDownList)x).SelectedIndex = 0;

                                                    //return ret;
                                                }
                                            }
                                        }
                                    }

                                    if (x.GetType() == typeof(HtmlInputCheckBox))
                                    {
                                        if (((HtmlInputCheckBox)x).ID.Contains("DTL_CUSTOMFORM"))
                                        {
                                            if (((HtmlInputCheckBox)x).ID.Contains("-" + dt.Rows[i]["FieldName"].ToString() + "-"))
                                            {
                                                if (dt.Rows[i]["FieldLabel"].ToString().ToUpper() == columnName.ToUpper())
                                                {
                                                    if (((HtmlInputCheckBox)x).Visible == true)
                                                    {
                                                        if (((HtmlInputCheckBox)x).Checked == true)
                                                        {
                                                            ret = "true";
                                                        }
                                                        else
                                                        {
                                                            ret = "false";
                                                        }

                                                        ((HtmlInputCheckBox)x).Checked = false;

                                                        return ret;
                                                    }
                                                    else
                                                    {
                                                        ret = "";

                                                        return ret;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return ret;
        }

        private void GetICallBackFunction()
        {
            str = "";
            ClientScriptManager m = Page.ClientScript;
            str = m.GetCallbackEventReference(this, "args", "ReceiveServerData", "'this is context from server'");
            strCallback = "function CallServer(args,context){" + str + "; }";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "CallServer", strCallback, true);
        }

        private void ICD10Heading(string FieldId)
        {
            Panel thelblPnl = new Panel();

            #region "ICD10"

            if (thelblPnl.Controls.Count < 1)
            {
                Panel PnlHeading = new Panel();
                PnlHeading.ID = "pnl-ICD10-" + FieldId;
                PnlHeading.Height = 20;
                PnlHeading.Width = 840;
                PnlHeading.Font.Bold = true;
                thelblPnl.Controls.Clear();

                Label theSP = new Label();
                theSP.ID = "lblICD10" + FieldId;
                theSP.Width = 5;
                theSP.Text = "";
                PnlHeading.Controls.Add(theSP);

                Label theLabel1 = new Label();
                theLabel1.ID = "lblICD10Nm" + FieldId;
                theLabel1.Text = "ICD10";
                theLabel1.Width = 400;
                PnlHeading.Controls.Add(theLabel1);

                Label theLabel2 = new Label();
                theLabel2.ID = "lblDateOnset" + FieldId;
                theLabel2.Text = " Date of Onset";
                theLabel2.Width = 250;
                PnlHeading.Controls.Add(theLabel2);

                Label theLabel3 = new Label();
                theLabel3.ID = "lblComments" + FieldId;
                theLabel3.Text = "Comments";
                theLabel3.Width = 120;
                PnlHeading.Controls.Add(theLabel3);
                DIVCustomItem.Controls.Add(PnlHeading);
            }

            #endregion "ICD10"
        }

        private StringBuilder InsertGridView(int PatientID, int FeatureID, int SectionID, string SectionName, int VisitID, string FeatureName)
        {
            StringBuilder Sbinsert = new StringBuilder();
            DataTable dtlnktable = ((DataTable)ViewState["LnkTable"]).Copy();
            DataTable lnkSectionFieldName = dtgridview(dtlnktable);
            //DataTable lnkSectionFieldName = ((DataTable)ViewState["LnkTable"]).DefaultView.ToTable(true, "FeatureID", "FieldName", "IsGridView", "SectionID","Fieldlabel").Copy();
            DataView dvSectionFieldName = new DataView(lnkSectionFieldName);
            dvSectionFieldName.RowFilter = "SectionId=" + SectionID + " and IsGridView = 1";
            AjaxControlToolkit.TabContainer container = (AjaxControlToolkit.TabContainer)tabContainer;
            foreach (object obj in container.Controls)
            {
                if (obj is AjaxControlToolkit.TabPanel)
                {
                    AjaxControlToolkit.TabPanel tabPanel = (AjaxControlToolkit.TabPanel)obj;
                    foreach (object ctrl in tabPanel.Controls)
                    {
                        if (ctrl is Control)
                        {
                            Control c = (Control)ctrl;
                            foreach (object z in c.Controls)
                            {
                                if (z.GetType() == typeof(GridView))
                                {
                                    StringBuilder sbColumns = new StringBuilder(); ;

                                    if (((GridView)z).ID.Contains("Dview_" + SectionID))
                                    {
                                        string Table = "DTL_CUSTOMFORM_" + SectionName + "_" + FeatureName.ToString().Trim().Replace(' ', '_'); ;
                                        if (VisitID > 0)
                                        {
                                            Sbinsert.Append(" Delete  from [" + Table + "] where FormID = " + FeatureID + " and  Ptn_Pk=" + PatientID + " and Visit_pk=" + VisitID + " and LocationID=" + Session["AppLocationId"].ToString() + "; ");
                                        }
                                        //for (int i = 0; i < dvSectionFieldName.ToTable().Rows.Count; i++)
                                        //{
                                        //    sbColumns.Append(",[" + dvSectionFieldName.ToTable().Rows[i]["FieldName"].ToString() + "]");
                                        //}
                                        if (ViewState["GridCache_" + SectionID] != null)
                                        {
                                            for (int y = 0; y < ((DataTable)ViewState["GridCache_" + SectionID]).Columns.Count; y++)
                                            {
                                                string COLNAME = ((DataTable)ViewState["GridCache_" + SectionID]).Columns[y].ColumnName.ToString();
                                                string strfieldname = findcolumnfieldname(dvSectionFieldName.ToTable(), COLNAME);
                                                sbColumns.Append("," + strfieldname + "");
                                            }

                                            for (int j = 0; j < ((DataTable)ViewState["GridCache_" + SectionID]).Rows.Count; j++)
                                            {
                                                StringBuilder sbSelect = new StringBuilder();
                                                StringBuilder sbRows = new StringBuilder();
                                                Sbinsert.Append(" Insert into [" + Table + "]([ptn_pk], [Visit_Pk], [LocationID],[SectionId],[FormID]");
                                                Sbinsert.Append(sbColumns);
                                                Sbinsert.Append(", [UserID], [CreateDate])");

                                                for (int y = 0; y < ((DataTable)ViewState["GridCache_" + SectionID]).Columns.Count; y++)
                                                {
                                                    bool isddl = false;
                                                    DataView dvGridViewDDL = new DataView(dtlnktable);
                                                    string COLNAME = ((DataTable)ViewState["GridCache_" + SectionID]).Columns[y].ColumnName.ToString();
                                                    dvGridViewDDL.RowFilter = "FieldLabel= '" + COLNAME + "' AND IsGridView = 1 and ControlId =4";
                                                    if (dvGridViewDDL.Count > 0)
                                                    {
                                                        if (ViewState["GridViewDDL-" + dvGridViewDDL[0]["FieldName"].ToString()] != null)
                                                        {
                                                            DataTable dtDDL = new DataTable();
                                                            dtDDL = (DataTable)ViewState["GridViewDDL-" + dvGridViewDDL[0]["FieldName"].ToString()];
                                                            DataView dvddl = new DataView(dtDDL);
                                                            string DDLVALUE = Convert.ToString(((DataTable)ViewState["GridCache_" + SectionID]).Rows[j][y]);
                                                            dvddl.RowFilter = "Name  ='" + DDLVALUE + "'";
                                                            if (dvddl.Count == 0)
                                                            {
                                                                sbRows.Append(",null");
                                                            }
                                                            else
                                                            {
                                                                sbRows.Append(",'" + dvddl[0]["ID"] + "'");
                                                            }
                                                            isddl = true;
                                                            dtDDL.Dispose();
                                                        }
                                                    }
                                                    if (!isddl)
                                                    {
                                                        if (String.IsNullOrEmpty(Convert.ToString(((DataTable)ViewState["GridCache_" + SectionID]).Rows[j][y])))
                                                        {
                                                            sbRows.Append(",null");
                                                        }
                                                        else
                                                        {
                                                            sbRows.Append(",'" + apostropheHandler(Convert.ToString(((DataTable)ViewState["GridCache_" + SectionID]).Rows[j][y])) + "'");
                                                        }
                                                    }
                                                }

                                                if (VisitID == 0)
                                                {
                                                    sbSelect.Append(" select " + PatientID + ", @thisVisitId , " + Session["AppLocationId"].ToString() + " , " + SectionID.ToString() + "," + FeatureID.ToString());
                                                }
                                                else
                                                {
                                                    sbSelect.Append(" select " + PatientID + ", " + VisitID + " , " + Session["AppLocationId"].ToString() + " , " + SectionID.ToString() + "," + FeatureID.ToString());
                                                }
                                                sbSelect.Append(sbRows);
                                                sbSelect.Append(" , " + Session["AppUserId"].ToString() + " , Getdate() ");
                                                Sbinsert.Append(sbSelect);
                                                Sbinsert.Append(" ; ");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return Sbinsert;
        }

        private StringBuilder InsertMultiSelectList(int PatientID, string FieldName, int FeatureID, string Multi_SelectTable, Int32 theControlId, Int32 theFieldId, int TabId)
        {
            StringBuilder Insertcbl = new StringBuilder();
            AjaxControlToolkit.TabContainer container = (AjaxControlToolkit.TabContainer)tabContainer;
            foreach (object obj in container.Controls)
            {
                if (obj is AjaxControlToolkit.TabPanel)
                {
                    AjaxControlToolkit.TabPanel tabPanel = (AjaxControlToolkit.TabPanel)obj;
                    if (Convert.ToInt32(tabPanel.ID) == TabId)
                    {
                        foreach (object ctrl in tabPanel.Controls)
                        {
                            if (ctrl is Control)
                            {
                                Control c = (Control)ctrl;
                                foreach (object y in c.Controls)
                                {
                                    if (y.GetType() == typeof(System.Web.UI.WebControls.Panel))
                                    {
                                        string strCheckBox = string.Empty;
                                        string strDate1 = string.Empty;
                                        string strDate2 = string.Empty;
                                        string strNumeric = string.Empty;
                                        string[] TableName1 = null;
                                        string Table1 = string.Empty;

                                        foreach (Control x in ((Control)y).Controls)
                                        {
                                            if (x.GetType() == typeof(System.Web.UI.WebControls.CheckBox))
                                            {
                                                string[] TableName = ((CheckBox)x).ID.Split('-');
                                                TableName1 = ((CheckBox)x).ID.Split('-');
                                                if (TableName.Length == 5)
                                                {
                                                    Table1 = TableName[3];

                                                    if (Table1 == Multi_SelectTable)
                                                    {
                                                        if (Table1 == "DTL_CUSTOMFIELD")
                                                        {
                                                            Table1 = "DTL_FB_" + TableName[2] + "";
                                                            Table1 = Table1.Replace(' ', '_');
                                                        }
                                                        if (Table1 != "DTL_CUSTOMFIELD" && Convert.ToInt32(TableName[4]) == theFieldId)
                                                        {
                                                            if (((CheckBox)x).Checked == true && ((CheckBox)x).Text != "Other")
                                                            {
                                                                if (strCheckBox == string.Empty)
                                                                {
                                                                    strCheckBox = TableName[1];
                                                                }
                                                                else
                                                                {
                                                                    strCheckBox = strCheckBox + ", " + TableName[1];
                                                                }
                                                                //Insertcbl.Append("Insert into [" + Table + "]([ptn_pk], [Visit_Pk], [LocationID], [" + TableName[2] + "], [UserID], [CreateDate])");
                                                                //Insertcbl.Append("values (" + PatientID + ",  IDENT_CURRENT('Ord_Visit')," + Session["AppLocationId"].ToString() + "," + TableName[1] + ",");
                                                                //Insertcbl.Append("" + Session["AppUserId"].ToString() + ", Getdate())");
                                                            }
                                                            else if (((CheckBox)x).Checked == true && ((CheckBox)x).Text == "Other")
                                                            {
                                                                if (strCheckBox == string.Empty)
                                                                {
                                                                    strCheckBox = TableName[1];
                                                                }
                                                                else
                                                                {
                                                                    strCheckBox = strCheckBox + ", " + TableName[1];
                                                                }
                                                                ViewState["OtherNote"] = ((CheckBox)x).Text;
                                                            }
                                                        }
                                                        //For DtlCustom Field Table
                                                        else if (Convert.ToInt32(TableName[4]) == theFieldId)
                                                        {
                                                            if (((CheckBox)x).Checked == true)
                                                            {
                                                                Insertcbl.Append("Insert into [" + Table1 + "]([ptn_pk], [Visit_Pk], [LocationID], [" + TableName[2] + "], [UserID], [CreateDate])");
                                                                Insertcbl.Append("values (" + PatientID + ",  @thisVisitId ," + Session["AppLocationId"].ToString() + "," + TableName[1] + ",");
                                                                Insertcbl.Append("" + Session["AppUserId"].ToString() + ", Getdate())");
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                                            {
                                                string[] TableName = ((TextBox)x).ID.Split('-');
                                                if (TableName[0] == "TXTDT1" || TableName[0] == "TXTDT2" || TableName[0] == "TXTNUM")
                                                {
                                                    if (TableName[0] == "TXTDT1")
                                                    {
                                                        if (strDate1 == string.Empty)
                                                            strDate1 = ((TextBox)x).Text;
                                                        else
                                                            strDate1 = strDate1 + ", " + ((TextBox)x).Text;
                                                    }
                                                    else if (TableName[0] == "TXTDT2")
                                                    {
                                                        if (strDate2 == string.Empty)
                                                            strDate2 = ((TextBox)x).Text;
                                                        else
                                                            strDate2 = strDate2 + ", " + ((TextBox)x).Text;
                                                    }
                                                    else if (TableName[0] == "TXTNUM")
                                                    {
                                                        if (strNumeric == string.Empty)
                                                            strNumeric = ((TextBox)x).Text;
                                                        else
                                                            strNumeric = strNumeric + ", " + ((TextBox)x).Text;
                                                    }
                                                }
                                            }
                                            if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputText))
                                            {
                                                string[] TableName = ((HtmlInputText)x).ID.Split('-');
                                                string Table = TableName[3];
                                                string Other = "";
                                                if (Table == Multi_SelectTable)
                                                {
                                                    if (Table == "DTL_CUSTOMFIELD")
                                                    {
                                                        Table = "DTL_FB_" + TableName[2] + "";
                                                        Table = Table.Replace(' ', '_');
                                                    }
                                                    if (Table != "DTL_CUSTOMFIELD")
                                                    {
                                                        string filePath = Server.MapPath("~/XMLFiles/MultiSelectCustomForm.xml");
                                                        DataSet dsMultiSelectList = new DataSet();
                                                        dsMultiSelectList.ReadXml(filePath);
                                                        DataTable DT = dsMultiSelectList.Tables[0];
                                                        foreach (DataRow DR in DT.Rows)
                                                        {
                                                            if (DR[0].ToString().ToUpper() == Table)
                                                            {
                                                                Other = DR[2].ToString();
                                                            }
                                                        }
                                                        if (theControlId == 15)
                                                        {
                                                            Other = "Other";
                                                        }
                                                        if (Convert.ToString(ViewState["OtherNote"]) != "" && ((HtmlInputText)x).Value != "" && Other != "")
                                                        {
                                                            Insertcbl.Append("Insert into [" + Table + "]([ptn_pk], [Visit_Pk], [LocationID], [" + TableName[2] + "],[" + Other + "], [UserID], [CreateDate],TabId)");
                                                            Insertcbl.Append("values (" + PatientID + ", @thisVisitId ," + Session["AppLocationId"].ToString() + "," + TableName[1] + ",");
                                                            Insertcbl.Append("'" + ((HtmlInputText)x).Value + "', " + Session["AppUserId"].ToString() + ", Getdate()," + TabId + ")");
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        // code here
                                        if (!string.IsNullOrEmpty(strCheckBox))
                                        {
                                            string[] arrDate1 = null;
                                            string[] arrDate2 = null;
                                            string[] arrNumeric = null;
                                            string[] arrCheckBox = null;

                                            if (strCheckBox != string.Empty)
                                                arrCheckBox = strCheckBox.Split(',');
                                            if (strDate1 != string.Empty)
                                                arrDate1 = strDate1.Split(',');
                                            if (strDate2 != string.Empty)
                                                arrDate2 = strDate2.Split(',');
                                            if (strNumeric != string.Empty)
                                                arrNumeric = strNumeric.Split(',');

                                            DateTime dtDate1 = new DateTime();
                                            DateTime dtDate2 = new DateTime();
                                            dtDate1 = System.DateTime.Now;
                                            dtDate2 = System.DateTime.Now;
                                            int intNumeric = 0;
                                            for (int i = 0; i < arrCheckBox.Length; i++)
                                            {
                                                if (arrDate1 != null)
                                                {
                                                    if (!string.IsNullOrEmpty(arrDate1[i]))
                                                        if (arrDate1[i].Trim() != "")
                                                        {
                                                            dtDate1 = Convert.ToDateTime(arrDate1[i]);
                                                        }
                                                }
                                                if (arrDate2 != null)
                                                {
                                                    if (!string.IsNullOrEmpty(arrDate2[i]))
                                                        if (arrDate2[i].Trim() != "")
                                                        {
                                                            dtDate2 = Convert.ToDateTime(arrDate2[i]);
                                                        }
                                                }
                                                if (arrNumeric != null)
                                                {
                                                    if (!string.IsNullOrEmpty(arrNumeric[i]))
                                                        if (arrNumeric[i].Trim() != "")
                                                        {
                                                            intNumeric = Convert.ToInt16(arrNumeric[i]);
                                                        }
                                                        else { intNumeric = 0; }
                                                }
                                                Boolean tabIdexist = false;
                                                string filePath = Server.MapPath("~/XMLFiles/MultiSelectCustomForm.xml");
                                                DataSet dsMultiSelectList = new DataSet();
                                                dsMultiSelectList.ReadXml(filePath);
                                                DataTable DT = dsMultiSelectList.Tables[0];
                                                foreach (DataRow DR in DT.Rows)
                                                {
                                                    if (DR[0].ToString().ToUpper() == Table1)
                                                    {
                                                        tabIdexist = true;
                                                    }
                                                }
                                                //Creating column for old database where Column Name 'Datefield1', 'Datefield2' and Numericfield doesnot exist
                                                StringBuilder createColumn = new StringBuilder();
                                                createColumn.Append("if not exists(select * from INFORMATION_SCHEMA.COLUMNS where table_name='" + Table1 + "' and column_name='DateField1')");
                                                createColumn.Append("Begin Alter table [" + Table1 + "] Add DateField1 datetime End ");
                                                createColumn.Append("if not exists(select * from INFORMATION_SCHEMA.COLUMNS where table_name='" + Table1 + "' and column_name='DateField2')");
                                                createColumn.Append("Begin Alter table [" + Table1 + "] Add DateField2 datetime End ");
                                                createColumn.Append("if not exists(select * from INFORMATION_SCHEMA.COLUMNS where table_name='" + Table1 + "' and column_name='NumericField')");
                                                createColumn.Append("Begin Alter table [" + Table1 + "] Add NumericField int End ");
                                                createColumn.Append("Select 1[Saved]");
                                                ICustomForm MgrSaveUpdate = (ICustomForm)ObjectFactory.CreateInstance(ObjFactoryParameter);
                                                DataSet TempDS = MgrSaveUpdate.Common_GetSaveUpdate(createColumn.ToString());

                                                if (tabIdexist == true)
                                                {
                                                    Insertcbl.Append(" Insert into [" + Table1 + "]([ptn_pk], [Visit_Pk], [LocationID], [" + TableName1[2] + "], [UserID], [CreateDate], DateField1, DateField2, NumericField,TabId)");
                                                    Insertcbl.Append("values (" + PatientID + ",  @thisVisitId ," + Session["AppLocationId"].ToString() + "," + arrCheckBox[i] + ",");
                                                    Insertcbl.Append("" + Session["AppUserId"].ToString() + ", Getdate() " + ", '" + dtDate1 + "', '" + dtDate2 + "', " + intNumeric + ", " + TabId + ")");
                                                }
                                                else
                                                {
                                                    Insertcbl.Append(" Insert into [" + Table1 + "]([ptn_pk], [Visit_Pk], [LocationID], [" + TableName1[2] + "], [UserID], [CreateDate], DateField1, DateField2, NumericField)");
                                                    Insertcbl.Append("values (" + PatientID + ",  @thisVisitId ," + Session["AppLocationId"].ToString() + "," + arrCheckBox[i] + ",");
                                                    Insertcbl.Append("" + Session["AppUserId"].ToString() + ", Getdate() " + ", '" + dtDate1 + "', '" + dtDate2 + "', " + intNumeric + ")");
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return Insertcbl;
        }

        private void LabDataBinding()
        {
            //Lab Order
            int VisitID = Convert.ToInt32(Session["PatientVisitId"]);
            int PatientID = Convert.ToInt32(Session["PatientId"]);
            ICustomForm MgrBindValue = (ICustomForm)ObjectFactory.CreateInstance(ObjFactoryParameter);
            StringBuilder StrLab = new StringBuilder();
            StrLab.Append("select a.LabID,a.LocationID,a.OrderedByName,a.OrderedByDate,a.ReportedByName,");
            StrLab.Append("a.ReportedByDate,a.CheckedByName,a.CheckedByDate,a.PreClinicLabDate, a.LabPeriod,");
            StrLab.Append("b.LabTestID,b.ParameterID[SubTestId],b.TestResults,b.TestResults1,b.TestResultId,b.Financed,");
            StrLab.Append("c.subtestname[SubTestName],d.LabTypeID[LabTypeID],d.LabName,b.Units,e.name as UnitName,");
            StrLab.Append("f.MinBoundaryValue,f.MaxBoundaryValue from ord_PatientlabOrder a,dtl_PatientLabResults b");
            StrLab.Append(" left outer join mst_Decode e on e.Id=b.Units");
            StrLab.Append(" left outer join lnk_labValue f  on  f.UnitId=b.units and f.SubTestId=b.ParameterId,");
            StrLab.Append("lnk_testParameter c, mst_labtest d where a.labid = b.labid and a.labid=");
            StrLab.Append("(Select LabID from Ord_PatientLabOrder where VisitId='" + VisitID + "')");
            StrLab.Append(" and b.parameterid = c.subtestid and c.testid=d.labtestid");
            DataSet theDSLab = MgrBindValue.Common_GetSaveUpdate(StrLab.ToString());

            DataTable dtLabs = new DataTable();
            dtLabs.Columns.Add("LabTestID", System.Type.GetType("System.Int32"));
            dtLabs.Columns.Add("LabName", System.Type.GetType("System.String"));
            dtLabs.Columns.Add("SubTestID", System.Type.GetType("System.Int32"));
            dtLabs.Columns.Add("SubTestName", System.Type.GetType("System.String"));
            dtLabs.Columns.Add("LabTypeId", System.Type.GetType("System.Int32"));
            dtLabs.Columns.Add("Flag", System.Type.GetType("System.Int32"));

            foreach (DataRow thedr in theDSLab.Tables[0].Rows)
            {
                //if (Convert.ToInt32(thedr["LabTypeId"]) == 1)
                //{
                DataRow tmpDR = dtLabs.NewRow();
                tmpDR[0] = thedr["LabTestId"];
                tmpDR[1] = thedr["LabName"];
                tmpDR[2] = thedr["SubTestId"];
                tmpDR[3] = thedr["SubTestName"];
                tmpDR[4] = thedr["LabTypeId"];
                tmpDR[5] = 1;
                dtLabs.Rows.Add(tmpDR);
                //BindCustomControls(thedr);
                //}
            }

            if (!IsPostBack)
            {
                if (ViewState["LabRanges"] == null)
                {
                    ILabFunctions LabResultManager = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");

                    theDSLabs = LabResultManager.GetLabValues(); //pr_Laboratory_GetLabValues_constella
                    ViewState["LabRanges"] = theDSLabs;
                    ViewState["LabMaster"] = theDSLabs.Tables[2];
                }
                //Setting Session
                DataTable theDTLab = PtnCustomformselectedDataTableLab(dtLabs);
                Session["SelectedData"] = theDTLab;
            }

            foreach (DataRow labdr in dtLabs.Rows)
            {
                if ((DataTable)Session["SelectedData"] != null)
                {
                    foreach (DataRow labdrII in ((DataTable)Session["SelectedData"]).Rows)
                    {
                        int Flag = labdrII["Flag"] == System.DBNull.Value ? 0 : 1;
                        if (Convert.ToInt32(labdr["SubTestId"]) == Convert.ToInt32(labdrII["SubTestId"]) && Flag == 1)
                        {
                            BindCustomControls(labdr);
                        }
                    }
                }
            }

            foreach (DataRow thedrdata in theDSLab.Tables[0].Rows)
            {
                FillLabData(DIVCustomItem, thedrdata);
            }
        }

        private void LoadAdditionalLabs(DataTable theDT, Panel thePanel)
        {
            if (theDT != null)
            {
                foreach (DataRow theDR in theDT.Rows)
                {
                    BindCustomControls(theDR);
                }
            }
        }
        private void CreateAutoCompleteField(int controlId, string fieldName, string fieldId, string fieldLabel, string tableName, string tabId, bool theEnable, bool theAutoPopulate, string bindCategory, string bindSource)
        {
            DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
            tabContainer.ID = "TAB";
            IQLookupTextBox selectListTextBox = new IQLookupTextBox();
            if (theAutoPopulate == true)
            {
                DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lblAutoPopulate" + fieldLabel + "-" + fieldId + "'>" + "Previous-" + fieldLabel + " :</label>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));
                IQLookupTextBox autoCompleteTextBox = new IQLookupTextBox();
                autoCompleteTextBox.ID = "TXTLookupAuto-" + fieldName + "-" + tableName + "-" + fieldId + "-" + tabId;
                autoCompleteTextBox.Width = 280;
                //ddlSelectListAuto.MaxLength = 9;
                autoCompleteTextBox.Enabled = false;
                DIVCustomItem.Controls.Add(autoCompleteTextBox);
                TextBox thehiddenTextAuto = new TextBox();
                thehiddenTextAuto.ID = "" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + autoCompleteTextBox.ClientID + "";
                thehiddenTextAuto.Width = 0;
                divhidden.Controls.Add(thehiddenTextAuto);
                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
            }
            if (SetBusinessrule(fieldId, fieldName) == true)
            {
                selectListTextBox.LabelCssClass = "required";
            }
            selectListTextBox.LabelText = fieldLabel;
            selectListTextBox.LookupCategory = bindSource;
            selectListTextBox.LookupName = bindCategory;
            selectListTextBox.ID = "TXTLookup-" + fieldName + "-" + tableName + "-" + fieldId + "-" + tabId;

            selectListTextBox.Width = 280;
            selectListTextBox.ToolTip = fieldName;
            selectListTextBox.ServiceMethod = "GetLookupValue";
            selectListTextBox.ServicePath = "~/WebService/IQLookupWS.asmx";

            DIVCustomItem.Controls.Add(selectListTextBox);
            ApplyBusinessRules(selectListTextBox, controlId.ToString(), theEnable);
            TextBox thehiddenText = new TextBox();
            thehiddenText.ID = "" + this.tabContainer.ID + "_" + tbChildPanel.ID + "_" + selectListTextBox.ClientID + "";
            thehiddenText.Width = 0;
            divhidden.Controls.Add(thehiddenText);
            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
            DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
            DIVCustomItem.Controls.Add(new LiteralControl("</table>"));

        }
        private void LoadFieldTypeControl(string ControlID, string Column, string FieldId, string CodeID, string Label, string Table, string TabID, string BindSource, Boolean theEnable)
        {
            try
            {
                bool theAutoPopulate = false;
                DataTable theBusinessRuleDT = (DataTable)ViewState["BusRule"];
                DataView theBusinessRuleDV = new DataView(theBusinessRuleDT);
                DataView theAutoDV = new DataView();
                theBusinessRuleDV.RowFilter = "BusRuleId=17 and FieldId = " + FieldId.ToString();
                if (theBusinessRuleDV.Count > 0)
                    theAutoPopulate = true;
                if (ControlID == "22")
                {
                    
                    this.CreateAutoCompleteField(Convert.ToInt32(ControlID), Column, FieldId, Label, Table, TabID, theEnable, theAutoPopulate, this.bindCategory, this.bindSource);
                }
                if (ControlID == "1") ///SingleLine Text Box
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
                    if (theAutoPopulate == true)
                    {
                        DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lblAutoPopulate" + Label + "-" + FieldId + "'>" + "Previous-" + Label + " :</label>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));
                        TextBox theSingleTextAuto = new TextBox();
                        theSingleTextAuto.ID = "TXTAuto-" + Column + "-" + Table + "-" + FieldId + "-" + TabID;
                        //hidautoSingleLineID.Value = "TXTAuto-" + Column + "-" + Table + "-" + FieldId;
                        theSingleTextAuto.Width = 180;
                        theSingleTextAuto.MaxLength = 50;
                        //theSingleTextAuto.Text = AutoDt.Rows[0][Column].ToString();
                        theSingleTextAuto.Enabled = false;
                        DIVCustomItem.Controls.Add(theSingleTextAuto);
                        TextBox thehiddenTextAuto = new TextBox();
                        thehiddenTextAuto.ID = "" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theSingleTextAuto.ClientID + "";
                        thehiddenTextAuto.Width = 0;
                        divhidden.Controls.Add(thehiddenTextAuto);
                        DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
                    }

                    if (SetBusinessrule(FieldId, Column) == true)
                    {
                        DIVCustomItem.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + Label + "-" + FieldId + "' >" + Label + " :</label>"));
                    }
                    else
                    {
                        DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                    }
                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));

                    TextBox theSingleText = new TextBox();
                    theSingleText.ID = "TXT-" + Column + "-" + Table + "-" + FieldId + "-" + TabID;
                    theSingleText.Width = 180;
                    theSingleText.MaxLength = 50;
                    if (theEnable == false)
                    {
                        string str = "ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theSingleText.ClientID + "";
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "uniqueId" + Guid.NewGuid(), "EnableControlFalse('" + str + "');", true);
                        if (!IsPostBack)
                        {
                            AddContolStausInHastTable(str);
                        }
                    }
                    //theSingleText.Enabled = theEnable;
                    DIVCustomItem.Controls.Add(theSingleText);
                    tabContainer.ID = "TAB";
                    TextBox thehiddenText = new TextBox();
                    thehiddenText.ID = "" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theSingleText.ClientID + "";
                    thehiddenText.Width = 0;
                    divhidden.Controls.Add(thehiddenText);
                    ApplyBusinessRules(theSingleText, ControlID, theEnable);
                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
                }
                else if (ControlID == "2") ///DecimalTextBox
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));

                    if (theAutoPopulate == true)
                    {
                        DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lblAutoPopulate" + Label + "-" + FieldId + "'>" + "Previous-" + Label + " :</label>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));
                        TextBox theSingleDecimalAuto = new TextBox();
                        theSingleDecimalAuto.ID = "TXTAuto-" + Column + "-" + Table + "-" + FieldId + "-" + TabID;
                        //hidautoDecimalID.Value = "TXTAuto-" + Column + "-" + Table + "-" + FieldId;
                        theSingleDecimalAuto.Width = 180;
                        theSingleDecimalAuto.MaxLength = 50;
                        //theSingleDecimalAuto.Text = AutoDt.Rows[0][Column].ToString();
                        theSingleDecimalAuto.Enabled = false;
                        DIVCustomItem.Controls.Add(theSingleDecimalAuto);
                        TextBox thehiddenTextAuto = new TextBox();
                        thehiddenTextAuto.ID = "" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theSingleDecimalAuto.ClientID + "";
                        thehiddenTextAuto.Width = 0;
                        divhidden.Controls.Add(thehiddenTextAuto);
                        DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
                    }

                    if (SetBusinessrule(FieldId, Column) == true)
                    {
                        DIVCustomItem.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                    }
                    else
                    {
                        DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                    }
                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));

                    TextBox theSingleDecimalText = new TextBox();
                    theSingleDecimalText.ID = "TXT-" + Column + "-" + Table + "-" + FieldId + "-" + TabID;

                    theSingleDecimalText.Width = 180;
                    theSingleDecimalText.MaxLength = 50;
                    if (theEnable == false)
                    {
                        string str = "ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theSingleDecimalText.ClientID + "";
                        System.Web.UI.ScriptManager.RegisterStartupScript(this, typeof(Page), "" + Guid.NewGuid() + "", "EnableControlFalse('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theSingleDecimalText.ClientID + "');", true);
                        if (!IsPostBack)
                        {
                            AddContolStausInHastTable(str);
                        }
                    }
                    //theSingleDecimalText.Enabled = theEnable;
                    DIVCustomItem.Controls.Add(theSingleDecimalText);
                    ApplyBusinessRules(theSingleDecimalText, ControlID, theEnable);
                    tabContainer.ID = "TAB";
                    theSingleDecimalText.Attributes.Add("onkeyup", "chkDecimal('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theSingleDecimalText.ClientID + "')");
                    TextBox thehiddenText = new TextBox();
                    thehiddenText.ID = "" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theSingleDecimalText.ClientID + "";
                    thehiddenText.Width = 0;
                    divhidden.Controls.Add(thehiddenText);

                    //theSingleDecimalText.Enabled = theEnable;
                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</table>"));

                    if (Column.ToUpper() == "HEIGHT")
                    {
                        //theSingleDecimalText.Attributes.Add("OnBlur", "CalcualteBMIGet();");
                        hdnHeight.Value = "ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theSingleDecimalText.ClientID;
                    }
                    if (Column.ToUpper() == "WEIGHT")
                    {
                        //theSingleDecimalText.Attributes.Add("OnBlur", "CalcualteBMIGet();");
                        hdnWeight.Value = "ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theSingleDecimalText.ClientID;
                    }
                }
                else if (ControlID == "3")   /// Numeric (Integer)
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));

                    if (theAutoPopulate == true)
                    {
                        DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lblAutoPopulate" + Label + "-" + FieldId + "'>" + "Previous-" + Label + " :</label>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));
                        TextBox theNumberAuto = new TextBox();
                        theNumberAuto.ID = "TXTNUMAuto-" + Column + "-" + Table + "-" + FieldId + "-" + TabID;
                        theNumberAuto.Width = 100;
                        theNumberAuto.MaxLength = 9;

                        theNumberAuto.Enabled = false;
                        DIVCustomItem.Controls.Add(theNumberAuto);
                        TextBox thehiddenTextAuto = new TextBox();
                        thehiddenTextAuto.ID = "" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theNumberAuto.ClientID + "";
                        thehiddenTextAuto.Width = 0;
                        divhidden.Controls.Add(thehiddenTextAuto);
                        DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
                    }

                    if (SetBusinessrule(FieldId, Column) == true)
                    {
                        DIVCustomItem.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                    }
                    else
                    {
                        DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                    }
                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));
                    TextBox theNumberText = new TextBox();
                    theNumberText.ID = "TXTNUM-" + Column + "-" + Table + "-" + FieldId + "-" + TabID;
                    theNumberText.Width = 100;
                    theNumberText.MaxLength = 9;
                    if (theEnable == false)
                    {
                        string str = "ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theNumberText.ClientID + "";
                        System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(Page), "" + Guid.NewGuid() + "", "EnableControlFalse('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theNumberText.ClientID + "');", true);
                        if (!IsPostBack)
                        {
                            AddContolStausInHastTable(str);
                        }
                    }
                    //theNumberText.Enabled = theEnable;
                    DIVCustomItem.Controls.Add(theNumberText);
                    tabContainer.ID = "TAB";
                    theNumberText.Attributes.Add("onkeyup", "chkInteger('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theNumberText.ClientID + "')");
                    TextBox thehiddenText = new TextBox();
                    thehiddenText.ID = "" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theNumberText.ClientID + "";
                    thehiddenText.Width = 0;
                    divhidden.Controls.Add(thehiddenText);
                    ApplyBusinessRules(theNumberText, ControlID, theEnable);
                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
                }
                else if (ControlID == "4") /// Dropdown
                {
                    bool theCntrlPresent = false;
                    if (theCntrlPresent != true)
                    {
                        DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
                        DropDownList ddlSelectListAuto = new DropDownList();
                        tabContainer.ID = "TAB";
                        if (theAutoPopulate == true)
                        {
                            DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lblAutoPopulate" + Label + "-" + FieldId + "'>" + "Previous-" + Label + " :</label>"));
                            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                            DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));

                            ddlSelectListAuto.ID = "SELECTLISTAuto-" + Column + "-" + Table + "-" + FieldId + "-" + TabID;
                            ddlSelectListAuto.Width = 100;
                            //ddlSelectListAuto.MaxLength = 9;
                            ddlSelectListAuto.Enabled = false;
                            DIVCustomItem.Controls.Add(ddlSelectListAuto);
                            TextBox thehiddenTextAuto = new TextBox();
                            thehiddenTextAuto.ID = "" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + ddlSelectListAuto.ClientID + "";
                            thehiddenTextAuto.Width = 0;
                            divhidden.Controls.Add(thehiddenTextAuto);
                            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                            DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                            DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                            DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
                        }

                        if (SetBusinessrule(FieldId, Column) == true)
                        {
                            DIVCustomItem.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                        }
                        else
                        {
                            DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                        }
                        DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));

                        DropDownList ddlSelectList = new DropDownList();
                        ddlSelectList.ID = "SELECTLIST-" + Column + "-" + Table + "-" + FieldId + "-" + TabID;

                        if (CodeID == "")
                        {
                            CodeID = "0";
                        }
                        DataView theDV = new DataView(theDSXML.Tables[BindSource]);
                        if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
                        {
                            if (BindSource.ToUpper() == "MST_SYMPTOM" || BindSource.ToUpper() == "MST_REASON")
                            {
                                theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and CategoryID=" + CodeID + "";
                            }
                            else if (BindSource.ToUpper() == "MST_HIVDISEASE")
                            {
                                theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and SectionID=" + CodeID + "";
                            }
                            else if (BindSource.ToUpper() == "MST_STOPPEDREASON")
                            {
                                theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ")";
                            }
                            else if (BindSource.ToUpper() == "MST_DECODE" || BindSource.ToUpper() == "MST_PMTCTDECODE" || BindSource.ToUpper() == "MST_MODDECODE")
                            {
                                if (BindSource.ToUpper() == "MST_DECODE")
                                {
                                    theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and CodeID=" + CodeID + " and (ModuleId IS NULL or ModuleId IN(0," + Convert.ToString(Session["TechnicalAreaId"]) + "))";
                                }
                                else
                                {
                                    theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and CodeID=" + CodeID + "";
                                }
                            }
                            else
                            {
                                theDV.RowFilter = "DeleteFlag=0";
                            }
                        }
                        else
                        {
                            if (BindSource.ToUpper() == "MST_SYMPTOM" || BindSource.ToUpper() == "MST_REASON")
                            {
                                theDV.RowFilter = "SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and CategoryID=" + CodeID + "";
                            }
                            else if (BindSource.ToUpper() == "MST_HIVDISEASE")
                            {
                                theDV.RowFilter = "SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and SectionID=" + CodeID + "";
                            }
                            else if (BindSource.ToUpper() == "MST_STOPPEDREASON")
                            {
                                theDV.RowFilter = "SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ")";
                            }
                            else if (BindSource.ToUpper() == "MST_DECODE" || BindSource.ToUpper() == "MST_PMTCTDECODE" || BindSource.ToUpper() == "MST_MODDECODE")
                            {
                                if (BindSource.ToUpper() == "MST_DECODE")
                                {
                                    theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and CodeID=" + CodeID + " and (ModuleId IS NULL or ModuleId IN(0," + Convert.ToString(Session["TechnicalAreaId"]) + "))";
                                }
                                else
                                {
                                    theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and CodeID=" + CodeID + "";
                                }
                            }
                        }

                        if (theDV.Table != null)
                        {
                            IQCareUtils theUtils = new IQCareUtils();
                            BindFunctions BindManager = new BindFunctions();
                            try { theDV.Sort = "SRNO Asc"; }
                            catch { }
                            DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                            BindManager.BindCombo(ddlSelectListAuto, theDT, "Name", "ID");
                            BindManager.BindCombo(ddlSelectList, theDT, "Name", "ID");
                            ViewState["GridViewDDL-" + Column] = (DataTable)ddlSelectList.DataSource;
                            if (theDT.Rows.Count == 0)
                            {
                                ListItem theItem = new ListItem();
                                theItem.Text = "Select";
                                theItem.Value = "0";
                                ddlSelectList.Items.Add(theItem);
                            }
                        }
                        else
                        {
                            ListItem theItem1 = new ListItem();
                            theItem1.Text = "Select";
                            theItem1.Value = "0";
                            ddlSelectList.Items.Add(theItem1);
                        }
                        ddlSelectList.Width = 180;
                        if (theEnable == false)
                        {
                            string str = "ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + ddlSelectList.ClientID + "";
                            System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(Page), "" + Guid.NewGuid() + "", "EnableControlFalse('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + ddlSelectList.ClientID + "');", true);
                            if (!IsPostBack)
                            {
                                AddContolStausInHastTable(str);
                            }
                        }
                        //ddlSelectList.Enabled = theEnable;
                        if (theConditional == true && theEnable == true)
                        {
                            DataTable thDTC = ((DataSet)Session["AllData"]).Tables[17];
                            DataView theDVC = new DataView(thDTC);
                            theDVC.RowFilter = "ConFieldId=" + FieldId + "";
                            StringBuilder EnableDisableControl = new StringBuilder();
                            if (theDVC.ToTable().Rows.Count > 0)
                            {
                                foreach (DataRow theDRC in theDVC.ToTable().Rows)
                                {
                                    EnableDisableControl.Append("EnableValueDropdown('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + ddlSelectList.ID + "','" + theDRC["ConControlId"] + "', '" + theDRC["ConditionalFieldSectionId"] + "');");
                                }
                                ddlSelectList.Attributes.Add("onchange", "" + EnableDisableControl + "");
                            }
                            //ddlSelectList.AutoPostBack = true;
                            //ddlSelectList.SelectedIndexChanged += new EventHandler(ddlSelectList_SelectedIndexChanged);
                        }

                        /////////////////////////////////////////////////
                        if (theSecondLabelConditional == true && theEnable == false)
                        {
                            ddlSelectList.AutoPostBack = false;
                            ddlSelectList.SelectedIndexChanged += new EventHandler(ddlSelectList_SelectedIndexChanged);
                        }

                        ////////////////////////////////////////////////

                        DIVCustomItem.Controls.Add(ddlSelectList);
                        ApplyBusinessRules(ddlSelectList, ControlID, theEnable);
                        TextBox thehiddenText = new TextBox();
                        thehiddenText.ID = "" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + ddlSelectList.ClientID + "";
                        thehiddenText.Width = 0;
                        divhidden.Controls.Add(thehiddenText);
                        //ddlSelectList.Enabled = theEnable;
                        DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
                    }
                }
                else if (ControlID == "5") ///Date
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));

                    if (theAutoPopulate == true)
                    {
                        DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lblAutoPopulate" + Label + "-" + FieldId + "'>" + "Previous-" + Label + " :</label>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));

                        TextBox theDateTextAuto = new TextBox();
                        theDateTextAuto.ID = "TXTDTAuto-" + Column + "-" + Table + "-" + FieldId + "-" + TabID;
                        theDateTextAuto.Width = 100;
                        theDateTextAuto.MaxLength = 9;
                        theDateTextAuto.Enabled = false;
                        DIVCustomItem.Controls.Add(theDateTextAuto);
                        TextBox thehiddenTextAuto = new TextBox();
                        thehiddenTextAuto.ID = "" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theDateTextAuto.ClientID + "";
                        thehiddenTextAuto.Width = 0;
                        divhidden.Controls.Add(thehiddenTextAuto);

                        DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
                    }
                    if (SetBusinessrule(FieldId, Column) == true)
                    {
                        DIVCustomItem.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                    }
                    else
                    {
                        DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                    }
                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));
                    TextBox theDateText = new TextBox();
                    theDateText.ID = "TXTDT-" + Column + "-" + Table + "-" + FieldId + "-" + TabID;
                    Control ctl = (TextBox)theDateText;
                    theDateText.Width = 83;
                    theDateText.MaxLength = 11;
                    if (theEnable == false)
                    {
                        string str = "ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theDateText.ClientID + "";
                        System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(Page), "" + Guid.NewGuid() + "", "EnableControlFalse('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theDateText.ClientID + "');", true);
                        if (!IsPostBack)
                        {
                            AddContolStausInHastTable(str);
                        }
                    }
                    //theDateText.Enabled = theEnable;
                    DIVCustomItem.Controls.Add(theDateText);
                    CreateDateImage(theDateText, ControlID, theEnable, false);
                    ApplyBusinessRules(theDateText, ControlID, theEnable);
                    TextBox thehiddenText = new TextBox();
                    thehiddenText.ID = "" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theDateText.ClientID + "";
                    thehiddenText.Width = 0;
                    divhidden.Controls.Add(thehiddenText);
                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
                }
                else if (ControlID == "6")  /// Radio Button
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
                    if (SetBusinessrule(FieldId, Column) == true)
                    {
                        DIVCustomItem.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                    }
                    else
                    {
                        DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                    }
                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));

                    HtmlInputRadioButton theYesNoRadio1 = new HtmlInputRadioButton();
                    theYesNoRadio1.ID = "RADIO1-" + Column + "-" + Table + "-" + FieldId + "-" + TabID;
                    theYesNoRadio1.Value = "Yes";
                    theYesNoRadio1.Name = "" + Column + "";
                    if (theConditional == true && theEnable == true)
                    {
                        DataTable thDTC = ((DataSet)Session["AllData"]).Tables[17];
                        DataView theDVC = new DataView(thDTC);
                        theDVC.RowFilter = "ConFieldId=" + FieldId + "";
                        StringBuilder EnableValueRadio = new StringBuilder();
                        if (theDVC.ToTable().Rows.Count > 0)
                        {
                            foreach (DataRow theDRC in theDVC.ToTable().Rows)
                            {
                                if (Convert.ToString(theDRC["ConControlId"]) != "")
                                {
                                    EnableValueRadio.Append("EnableValueYes('" + theDRC["ConControlId"] + "');");
                                }
                            }
                            theYesNoRadio1.Attributes.Add("onclick", "down(this); " + EnableValueRadio + "");
                            //theYesNoRadio1.Attributes.Add("onclick", "down(this);SetValue('theHitCntrl','System.Web.UI.HtmlControls.HtmlInputRadioButton%" + theYesNoRadio1.ClientID + "');");
                        }
                        else { theYesNoRadio1.Attributes.Add("onclick", "down(this);"); }
                    }
                    //theYesNoRadio1.Attributes.Add("onclick", "down(this);SetValue('theHitCntrl','System.Web.UI.HtmlControls.HtmlInputRadioButton%" + theYesNoRadio1.ClientID + "');");
                    else
                        theYesNoRadio1.Attributes.Add("onclick", "down(this);");
                    theYesNoRadio1.Attributes.Add("onfocus", "up(this)");
                    DIVCustomItem.Controls.Add(theYesNoRadio1);
                    if (theEnable == false)
                    {
                        string str = "ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theYesNoRadio1.ClientID + "";
                        System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(Page), "" + Guid.NewGuid() + "_Yes" + "", "EnableControlFalse('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theYesNoRadio1.ClientID + "');", true);
                        if (!IsPostBack)
                        {
                            AddContolStausInHastTable(str);
                        }
                    }
                    //theYesNoRadio1.Visible = theEnable;
                    ApplyBusinessRules(theYesNoRadio1, ControlID, theEnable);
                    //theYesNoRadio1.Visible = theEnable;
                    DIVCustomItem.Controls.Add(new LiteralControl("<label align='labelright' id='lblYes-" + FieldId + "'>Yes</label>"));
                    HtmlInputRadioButton thehiddenRd = new HtmlInputRadioButton();
                    thehiddenRd.ID = "" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theYesNoRadio1.ClientID + "";
                    divhidden.Controls.Add(thehiddenRd);
                    HtmlInputRadioButton theYesNoRadio2 = new HtmlInputRadioButton();
                    theYesNoRadio2.ID = "RADIO2-" + Column + "-" + Table + "-" + FieldId + "-" + TabID;
                    theYesNoRadio2.Value = "No";
                    theYesNoRadio2.Name = "" + Column + "";
                    if (theConditional == true && theEnable == true)
                    {
                        DataTable thDTC = ((DataSet)Session["AllData"]).Tables[17];
                        DataView theDVC = new DataView(thDTC);
                        theDVC.RowFilter = "ConFieldId=" + FieldId + "";
                        StringBuilder EnableValueRadio = new StringBuilder();
                        if (theDVC.ToTable().Rows.Count > 0)
                        {
                            foreach (DataRow theDRC in theDVC.ToTable().Rows)
                            {
                                if (Convert.ToString(theDRC["ConControlId"]) != "")
                                {
                                    EnableValueRadio.Append("EnableValueNo('" + theDRC["ConControlId"] + "');");
                                }
                            }
                            theYesNoRadio2.Attributes.Add("onclick", "down(this); " + EnableValueRadio + "");
                        }
                        else { theYesNoRadio2.Attributes.Add("onclick", "down(this);"); }
                    }
                    //theYesNoRadio2.Attributes.Add("onclick", "down(this);SetValue('theHitCntrl','System.Web.UI.HtmlControls.HtmlInputRadioButton%" + theYesNoRadio2.ClientID + "');");
                    else
                        theYesNoRadio2.Attributes.Add("onclick", "down(this);");
                    theYesNoRadio2.Attributes.Add("onfocus", "up(this)");
                    DIVCustomItem.Controls.Add(theYesNoRadio2);
                    HtmlInputRadioButton thehiddenRd1 = new HtmlInputRadioButton();
                    thehiddenRd1.ID = "" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theYesNoRadio2.ClientID + "";
                    divhidden.Controls.Add(thehiddenRd1);
                    if (theEnable == false)
                    {
                        string str = "ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theYesNoRadio2.ClientID + "";
                        System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(Page), "" + Guid.NewGuid() + "_No" + "", "EnableControlFalse('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theYesNoRadio2.ClientID + "');", true);
                        if (!IsPostBack)
                        {
                            AddContolStausInHastTable(str);
                        }
                    }
                    ApplyBusinessRules(theYesNoRadio2, ControlID, theEnable);
                    //theYesNoRadio2.Visible = theEnable;
                    DIVCustomItem.Controls.Add(new LiteralControl("<label align='labelright' id='lblNo-" + FieldId + "'>No</label>"));

                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
                }
                else if (ControlID == "7") //Checkbox
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
                    if (SetBusinessrule(FieldId, Column) == true)
                    {
                        DIVCustomItem.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                    }
                    else
                    {
                        DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                    }
                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));

                    HtmlInputCheckBox theChk = new HtmlInputCheckBox();
                    theChk.ID = "Chk-" + Column + "-" + Table + "-" + FieldId + "-" + TabID;
                    //theChk.ID = "Chk-" + Column + "-" + Table;
                    //theChk.Visible = theEnable;
                    if (theEnable == false)
                    {
                        string str = "ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theChk.ClientID + "";
                        System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(Page), "" + Guid.NewGuid() + "", "EnableControlFalse('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theChk.ClientID + "');", true);
                        if (!IsPostBack)
                        {
                            AddContolStausInHastTable(str);
                        }
                    }
                    DIVCustomItem.Controls.Add(theChk);
                    TextBox thehiddenText = new TextBox();
                    thehiddenText.ID = "" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theChk.ClientID + "";
                    thehiddenText.Width = 0;
                    divhidden.Controls.Add(thehiddenText);
                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
                }
               
                else if (ControlID == "8")  /// MultiLine TextBox
                {
                    bool spanfield = false;
                    theBusinessRuleDV = new DataView(theBusinessRuleDT);
                    theBusinessRuleDV.RowFilter = "BusRuleId=25 and FieldId = " + FieldId.ToString();
                    if (theBusinessRuleDV.Count > 0)
                        spanfield = true;
                    DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));

                    if (theAutoPopulate == true)
                    {
                        if (spanfield == true)
                        {
                            DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:100%' align='left'>"));
                        }
                        else
                            DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lblAutoPopulate" + Label + "-" + FieldId + "'>" + "Previous-" + Label + " :</label>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                        if (spanfield == true)
                        {
                            DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                            DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                            DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:100%'>"));
                        }
                        else
                            DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));

                        TextBox theMultiTextAuto = new TextBox();
                        theMultiTextAuto.ID = "TXTMultiAuto-" + Column + "-" + Table + "-" + FieldId + "-" + TabID;
                        //hidautoDateID.Value = "TXTMultiAuto-" + Column + "-" + Table + "-" + FieldId;

                        if (spanfield == true)
                        {
                            theMultiTextAuto.Width = Unit.Percentage(100);
                        }
                        else
                            theMultiTextAuto.Width = 100;
                        theMultiTextAuto.MaxLength = 9;
                        theMultiTextAuto.Enabled = false;
                        DIVCustomItem.Controls.Add(theMultiTextAuto);
                        TextBox thehiddenText = new TextBox();
                        thehiddenText.ID = "" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theMultiTextAuto.ClientID + "";
                        thehiddenText.Width = 0;
                        divhidden.Controls.Add(thehiddenText);
                        DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                        //DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
                    }
                    if (spanfield == true)
                    {
                        DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:100%' align='left'>"));
                    }
                    else
                        DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));

                    if (SetBusinessrule(FieldId, Column) == true)
                    {
                        DIVCustomItem.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                    }
                    else
                    {
                        DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                    }
                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                    if (spanfield == true)
                    {
                        DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:100%'>"));
                    }
                    else
                        DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));

                    TextBox theMultiText = new TextBox();
                    theMultiText.ID = "TXTMulti-" + Column + "-" + Table + "-" + FieldId + "-" + TabID;

                    //theMultiText.ID = "TXTMulti-" + Column + "-" + Table;
                    if (spanfield == true)
                    {
                        theMultiText.Width = Unit.Percentage(100);
                        theMultiText.Rows = 6;
                    }
                    else
                        theMultiText.Width = 200;
                    theMultiText.TextMode = TextBoxMode.MultiLine;
                    theMultiText.MaxLength = 200;
                    theMultiText.Enabled = theEnable;
                    DIVCustomItem.Controls.Add(theMultiText);
                    TextBox thehiddenText1 = new TextBox();
                    thehiddenText1.ID = "" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theMultiText.ClientID + "";
                    thehiddenText1.Width = 0;
                    divhidden.Controls.Add(thehiddenText1);
                    ApplyBusinessRules(theMultiText, ControlID, theEnable);
                    //theMultiText.Enabled = theEnable;
                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
                }
                else if (ControlID == "9") ///  MultiSelect List
                {
                    DataTable dtBusinessRules = (DataTable)ViewState["BusRule"];
                    if (Convert.ToInt32(Session["busRulChk"]) == 1)
                    {
                        DIVCustomItem.Controls.Add(new LiteralControl("<div class='customdivborder leftallign' runat='server' nowrap='nowrap'>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("<table width=100%>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("<td width='25%'>"));

                        if (SetBusinessrule(FieldId, Column) == true)
                        {
                            DIVCustomItem.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " </label>"));
                        }
                        else
                        {
                            DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " </label>"));
                        }
                        DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                        Int32 theColCount = 1;
                        if (dtBusinessRules.Rows.Count > 0)
                        {
                            foreach (DataRow DR in dtBusinessRules.Rows)
                            {
                                if ((FieldId == DR["FieldID"].ToString() && DR["BusRuleID"].ToString() == "18")
                                    || (FieldId == DR["FieldID"].ToString() && DR["BusRuleID"].ToString() == "19")
                                    || (FieldId == DR["FieldID"].ToString() && DR["BusRuleID"].ToString() == "20"))
                                {
                                    if ((FieldId == DR["FieldID"].ToString() && DR["BusRuleID"].ToString() == "20"))
                                    {
                                        DIVCustomItem.Controls.Add(new LiteralControl("<td width='25%' align='left'>"));
                                        DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lbl" + DR["Value"] + "-" + FieldId + "'>" + DR["Value"] + " </label>"));
                                        DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                        theColCount = theColCount + 1;
                                    }

                                    if (FieldId == DR["FieldID"].ToString() && DR["BusRuleID"].ToString() == "19")
                                    {
                                        string[] arrValue = DR["Value"].ToString().Split('_');
                                        for (int i = 0; i < arrValue.Length; i++)
                                        {
                                            DIVCustomItem.Controls.Add(new LiteralControl("<td width='25%' align='left'>"));
                                            DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lbl" + arrValue[i] + "-" + FieldId + "'>" + arrValue[i] + " </label>"));
                                            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                            theColCount = theColCount + 1;
                                        }
                                    }
                                    else if (((FieldId == DR["FieldID"].ToString() && DR["BusRuleID"].ToString() == "18")))
                                    {
                                        DIVCustomItem.Controls.Add(new LiteralControl("<td width='25%' align='left'>"));
                                        DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lbl" + DR["Value"] + "-" + FieldId + "'>" + DR["Value"] + " </label>"));
                                        DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                    }
                                }
                            }
                        }
                        while (theColCount <= 3)
                        {
                            DIVCustomItem.Controls.Add(new LiteralControl("<td width='25%' align='left'>"));
                            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                            theColCount = theColCount + 1;
                        }

                        DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("<td colspan='4' class='border'>"));

                        //WithPanel
                        Panel PnlMulti = new Panel();
                        PnlMulti.ID = "Pnl_-" + Table + "-" + FieldId;
                        PnlMulti.ToolTip = Label;
                        PnlMulti.Enabled = theEnable;
                        PnlMulti.Controls.Add(new LiteralControl("<div class='customdivborder1 leftallign' runat='server' nowrap='nowrap'>"));

                        if (CodeID == "")
                        {
                            CodeID = "0";
                        }
                        string theCodeFldName = "";
                        DataTable theBindTbl = theDSXML.Tables[BindSource];
                        if (theBindTbl.Columns.Contains("CategoryId") == true)
                            theCodeFldName = "CategoryId";
                        else if (theBindTbl.Columns.Contains("SectionId") == true)
                            theCodeFldName = "SectionId";
                        else
                            theCodeFldName = "CodeId";
                        DataView theDV = new DataView(theDSXML.Tables[BindSource]);
                        if (BindSource.ToUpper() == "MST_DECODE")
                        {
                            theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and CodeID=" + CodeID + " and (ModuleId IS NULL or ModuleId IN(0," + Convert.ToString(Session["TechnicalAreaId"]) + "))";
                        }
                        else
                        {
                            theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and " + theCodeFldName + "=" + CodeID + "";
                        }
                        //theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and " + theCodeFldName + "=" + CodeID + "";
                        IQCareUtils theUtils = new IQCareUtils();
                        BindFunctions BindManager = new BindFunctions();
                        DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                        if (theDT != null)
                        {
                            for (int i = 0; i < theDT.Rows.Count; i++)
                            {
                                // Dates Control creation for multi Select list
                                //Date 1 Control
                                tabContainer.ID = "TAB";
                                TextBox theDate1 = new TextBox();
                                theDate1.ID = "TXTDT1-" + theDT.Rows[i][0] + "-" + Column + "-" + Table + "-" + FieldId + "-" + TabID;
                                Control ctl = (TextBox)theDate1;
                                theDate1.Width = 83;
                                theDate1.MaxLength = 11;
                                theDate1.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");

                                tabContainer.ID = "TAB";
                                string thDTVar = "ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theDate1.ClientID;
                                theDate1.Attributes.Add("onblur", "DateFormat(this, this.value,event,true,'3'); isCheckValidDate('" + Application["AppCurrentDate"] + "', '" + thDTVar + "', '" + thDTVar + "')");

                                Image theDateImage1 = new Image();
                                theDateImage1.ID = "img" + theDate1.ID;
                                theDateImage1.Height = 22;
                                theDateImage1.Width = 22;
                                theDateImage1.Visible = theEnable;
                                theDateImage1.ToolTip = "Date Helper";
                                theDateImage1.ImageUrl = "~/images/cal_icon.gif";
                                theDateImage1.Attributes.Add("onClick", "w_displayDatePicker('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + ((TextBox)ctl).ClientID + "');");
                                theDate1.Visible = false;
                                theDateImage1.Visible = false;
                                //Date 2 Control
                                TextBox theDate2 = new TextBox();
                                theDate2.ID = "TXTDT2-" + theDT.Rows[i][0] + "-" + Column + "-" + Table + "-" + FieldId + "-" + TabID;
                                Control ctl2 = (TextBox)theDate2;
                                theDate2.Width = 83;
                                theDate2.MaxLength = 11;
                                theDate2.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
                                theDate2.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");

                                tabContainer.ID = "TAB";
                                string thDTVarOth = "ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theDate2.ClientID;
                                theDate2.Attributes.Add("onblur", "DateFormat(this, this.value,event,true,'3'); isCheckValidDate('" + Application["AppCurrentDate"] + "', '" + thDTVarOth + "', '" + thDTVarOth + "')");

                                Image theDateImage2 = new Image();
                                theDateImage2.ID = "img" + theDate2.ID;
                                theDateImage2.Height = 22;
                                theDateImage2.Width = 22;
                                theDateImage2.Visible = theEnable;
                                theDateImage2.ToolTip = "Date Helper";
                                theDateImage2.ImageUrl = "~/images/cal_icon.gif";
                                theDateImage2.Attributes.Add("onClick", "w_displayDatePicker('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + ((TextBox)ctl2).ClientID + "');");

                                // Text control for numeric field
                                TextBox theNumeric = new TextBox();
                                theNumeric.ID = "TXTNUM-" + theDT.Rows[i][0] + "-" + Column + "-" + Table + "-" + FieldId + "-" + TabID;
                                theNumeric.Width = 83;
                                theNumeric.MaxLength = 3;
                                theNumeric.Attributes.Add("onkeypress", "return isNumberKey(event);");

                                CheckBox chkbox = new CheckBox();
                                chkbox.ID = Convert.ToString("CHKMULTI-" + theDT.Rows[i][0] + "-" + Column + "-" + Table + "-" + FieldId);
                                chkbox.Text = Convert.ToString(theDT.Rows[i]["Name"]);
                                CheckBox thehiddenchkbox = new CheckBox();
                                thehiddenchkbox.ID = "" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + chkbox.ClientID + "";
                                thehiddenchkbox.Width = 0;
                                divhidden.Controls.Add(thehiddenchkbox);
                                if (chkbox.Text == "Other")
                                {
                                    PnlMulti.Controls.Add(chkbox);
                                    PnlMulti.Controls.Add(new LiteralControl("<div  id='" + Column + "' style='display:block'>Specify: "));
                                    HtmlInputText HTextother = new HtmlInputText();
                                    HTextother.ID = "TXTMULTI-" + theDT.Rows[i][0] + "-" + Column + "-" + Table + "-" + FieldId + "-" + TabID;
                                    HTextother.Size = 10;
                                    PnlMulti.Controls.Add(HTextother);
                                    PnlMulti.Controls.Add(new LiteralControl(HTextother.Value));
                                    PnlMulti.Controls.Add(new LiteralControl("</div>"));
                                    if (theConditional == true && theEnable == true)
                                    {
                                        DataTable thDTC = ((DataSet)Session["AllData"]).Tables[17];
                                        DataView theDVC = new DataView(thDTC);
                                        theDVC.RowFilter = "ConFieldId=" + FieldId + "";
                                        StringBuilder EnableDisableControl = new StringBuilder();
                                        if (theDVC.ToTable().Rows.Count > 0)
                                        {
                                            foreach (DataRow theDRC in theDVC.ToTable().Rows)
                                            {
                                                EnableDisableControl.Append("EnableValuechkbox('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + chkbox.ID + "','" + theDRC["ConControlId"] + "', '" + theDRC["ConditionalFieldSectionId"] + "', '" + theDT.Rows[i][0] + "');");
                                            }
                                            chkbox.Attributes.Add("onclick", "" + EnableDisableControl + "");
                                        }
                                    }
                                    //chkbox.Attributes.Add("onclick", "toggle('" + Column + "');SetValue('theHitCntrl','System.Web.UI.WebControls.CheckBox%" + chkbox.ClientID + "');");
                                    else
                                        chkbox.Attributes.Add("onclick", "toggle('" + Column + "');");
                                    PnlMulti.Controls.Add(new LiteralControl("<br>"));
                                }
                                else
                                {
                                    if (theConditional == true && theEnable == true)
                                    {
                                        DataTable thDTC = ((DataSet)Session["AllData"]).Tables[17];
                                        DataView theDVC = new DataView(thDTC);
                                        theDVC.RowFilter = "ConFieldId=" + FieldId + "";
                                        StringBuilder EnableDisableControl = new StringBuilder();
                                        if (theDVC.ToTable().Rows.Count > 0)
                                        {
                                            foreach (DataRow theDRC in theDVC.ToTable().Rows)
                                            {
                                                EnableDisableControl.Append("EnableValuechkbox('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + chkbox.ID + "','" + theDRC["ConControlId"] + "', '" + theDRC["ConditionalFieldSectionId"] + "', '" + theDT.Rows[i][0] + "');");
                                            }
                                            chkbox.Attributes.Add("onclick", "" + EnableDisableControl + "");
                                        }
                                        //chkbox.Attributes.Add("onclick", "SetValue('theHitCntrl','System.Web.UI.WebControls.CheckBox%" + chkbox.ClientID + "');");
                                    }
                                    //chkbox.Load += new EventHandler(MultiSelect_Load);

                                    PnlMulti.Controls.Add(chkbox);
                                    if (dtBusinessRules.Rows.Count > 0)
                                    {
                                        foreach (DataRow DR in dtBusinessRules.Rows)
                                        {
                                            if ((FieldId == DR["FieldID"].ToString() && DR["BusRuleID"].ToString() == "18"))
                                            {
                                                PnlMulti.Controls.Add(theDate1);
                                                PnlMulti.Controls.Add(new LiteralControl("&nbsp;"));
                                                PnlMulti.Controls.Add(theDateImage1);
                                                PnlMulti.Controls.Add(new LiteralControl("<span class='smallerlabel'>(DD-MMM-YYYY)</span>"));
                                                theDate1.Visible = true;
                                                theDateImage1.Visible = true;
                                            }
                                            else if ((FieldId == DR["FieldID"].ToString() && DR["BusRuleID"].ToString() == "19"))
                                            {
                                                Label theSpace2 = new Label();
                                                if (theDate1.Visible != true && theDateImage1.Visible != true)
                                                {
                                                    theDate1.Visible = true;
                                                    theSpace2.ID = "theSpace2" + chkbox.ID;
                                                    theSpace2.Text = "";
                                                    theSpace2.Width = 25;
                                                    PnlMulti.Controls.Add(theSpace2);
                                                    theDateImage1.Visible = true;
                                                    PnlMulti.Controls.Add(theDate1);
                                                    PnlMulti.Controls.Add(new LiteralControl("&nbsp;"));
                                                    PnlMulti.Controls.Add(theDateImage1);
                                                    PnlMulti.Controls.Add(new LiteralControl("<span class='smallerlabel'>(DD-MMM-YYYY)</span>"));
                                                }
                                                theSpace2.ID = "theSpace2_2" + chkbox.ID;
                                                theSpace2.Text = "";
                                                theSpace2.Width = 25;
                                                PnlMulti.Controls.Add(theSpace2);
                                                PnlMulti.Controls.Add(theDate2);
                                                PnlMulti.Controls.Add(new LiteralControl("&nbsp;"));
                                                PnlMulti.Controls.Add(theDateImage2);
                                                PnlMulti.Controls.Add(new LiteralControl("<span class='smallerlabel'>(DD-MMM-YYYY)</span>"));
                                                //PnlMulti.Controls.Add(new LiteralControl("&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp;"));
                                                //PnlMulti.Controls.Add(new LiteralControl("&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp;"));
                                            }
                                            else if ((FieldId == DR["FieldID"].ToString() && DR["BusRuleID"].ToString() == "20"))
                                            {
                                                Label theSpace3 = new Label();
                                                theSpace3.ID = "theSpace3" + chkbox.ID;
                                                theSpace3.Text = "";
                                                theSpace3.Width = 25;
                                                PnlMulti.Controls.Add(theSpace3);
                                                PnlMulti.Controls.Add(theNumeric);
                                                PnlMulti.Controls.Add(new LiteralControl("&nbsp;"));
                                            }
                                        }
                                    }
                                    chkbox.Width = 210;
                                    PnlMulti.Controls.Add(new LiteralControl("<br>"));
                                }
                            }
                        }
                        PnlMulti.Controls.Add(new LiteralControl("</div>"));

                        DIVCustomItem.Controls.Add(PnlMulti);
                        ApplyBusinessRules(PnlMulti, ControlID, theEnable);
                        //PnlMulti.Enabled = theEnable;
                        DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("</div>"));
                    }
                    else
                    {
                        DIVCustomItem.Controls.Add(new LiteralControl("<table>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
                        if (SetBusinessrule(FieldId, Column) == true)
                        {
                            DIVCustomItem.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                        }
                        else
                        {
                            DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                        }
                        DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%'>"));

                        //WithPanel
                        Panel PnlMulti = new Panel();
                        PnlMulti.ID = "Pnl_-" + Table + "-" + FieldId;
                        PnlMulti.ToolTip = Label;
                        PnlMulti.Enabled = theEnable;
                        PnlMulti.Controls.Add(new LiteralControl("<DIV class ='customdivbordermultiselect'  runat='server' nowrap='nowrap'>"));
                        tabContainer.ID = "TAB";
                        if (CodeID == "")
                        {
                            CodeID = "0";
                        }
                        string theCodeFldName = "";
                        DataTable theBindTbl = theDSXML.Tables[BindSource];
                        if (theBindTbl.Columns.Contains("CategoryId") == true)
                            theCodeFldName = "CategoryId";
                        else if (theBindTbl.Columns.Contains("SectionId") == true)
                            theCodeFldName = "SectionId";
                        else
                            theCodeFldName = "CodeId";
                        DataView theDV = new DataView(theDSXML.Tables[BindSource]);
                        if (BindSource.ToUpper() == "MST_DECODE")
                        {
                            theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and CodeID=" + CodeID + " and (ModuleId IS NULL or ModuleId IN(0," + Convert.ToString(Session["TechnicalAreaId"]) + "))";
                        }
                        else
                        {
                            theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and " + theCodeFldName + "=" + CodeID + "";
                        }
                        IQCareUtils theUtils = new IQCareUtils();
                        BindFunctions BindManager = new BindFunctions();
                        DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                        DataTable thDTC = ((DataSet)Session["AllData"]).Tables[17];
                        DataView theDVC = new DataView(thDTC);
                        if (theDT != null)
                        {
                            for (int i = 0; i < theDT.Rows.Count; i++)
                            {
                                CheckBox chkbox = new CheckBox();
                                chkbox.ID = Convert.ToString("CHKMULTI-" + theDT.Rows[i][0] + "-" + Column + "-" + Table + "-" + FieldId);
                                chkbox.Text = Convert.ToString(theDT.Rows[i]["Name"]);
                                CheckBox thehiddenchkbox = new CheckBox();
                                thehiddenchkbox.ID = "" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + chkbox.ClientID + "";
                                thehiddenchkbox.Width = 0;
                                divhidden.Controls.Add(thehiddenchkbox);
                                if (chkbox.Text == "Other")
                                {
                                    PnlMulti.Controls.Add(chkbox);
                                    PnlMulti.Controls.Add(new LiteralControl("<DIV  class='pad10' id='" + Column + "' style='DISPLAY:none'>Specify: "));
                                    HtmlInputText HTextother = new HtmlInputText();
                                    HTextother.ID = "TXTMULTI-" + theDT.Rows[i][0] + "-" + Column + "-" + Table + "-" + FieldId + "-" + TabID;
                                    HTextother.Size = 10;
                                    PnlMulti.Controls.Add(HTextother);
                                    PnlMulti.Controls.Add(new LiteralControl(HTextother.Value));
                                    PnlMulti.Controls.Add(new LiteralControl("</DIV>"));
                                    if (theConditional == true && theEnable == true)
                                    {
                                        theDVC.RowFilter = "ConFieldId=" + FieldId + "";
                                        StringBuilder EnableDisableControl = new StringBuilder();
                                        if (theDVC.ToTable().Rows.Count > 0)
                                        {
                                            foreach (DataRow theDRC in theDVC.ToTable().Rows)
                                            {
                                                EnableDisableControl.Append("EnableValuechkbox('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + chkbox.ID + "','" + theDRC["ConControlId"] + "', '" + theDRC["ConditionalFieldSectionId"] + "', '" + theDT.Rows[i][0] + "');");
                                            }
                                            chkbox.Attributes.Add("onclick", "" + EnableDisableControl + "");
                                        }
                                        //chkbox.Attributes.Add("onclick", "toggle('" + Column + "');SetValue('theHitCntrl','System.Web.UI.WebControls.CheckBox%" + chkbox.ClientID + "');");
                                    }
                                    else
                                    {
                                        theDVC.RowFilter = "ConFieldId=" + FieldId + "";
                                        StringBuilder EnableDisableControl_1 = new StringBuilder();
                                        if (theDVC.ToTable().Rows.Count > 0)
                                        {
                                            foreach (DataRow theDRC in theDVC.ToTable().Rows)
                                            {
                                                EnableDisableControl_1.Append("EnableValuechkbox('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + chkbox.ID + "','" + theDRC["ConControlId"] + "', '" + theDRC["ConditionalFieldSectionId"] + "', '" + theDT.Rows[i][0] + "');");
                                            }
                                            chkbox.Attributes.Add("onclick", "" + EnableDisableControl_1 + "");
                                        }
                                        PnlMulti.Controls.Add(new LiteralControl("<br>"));
                                    }
                                }
                                else
                                {
                                    if (theConditional == true && theEnable == true)
                                    {
                                        theDVC.RowFilter = "ConFieldId=" + FieldId + "";
                                        StringBuilder EnableDisableControl = new StringBuilder();
                                        if (theDVC.ToTable().Rows.Count > 0)
                                        {
                                            foreach (DataRow theDRC in theDVC.ToTable().Rows)
                                            {
                                                EnableDisableControl.Append("EnableValuechkbox('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + chkbox.ID + "','" + theDRC["ConControlId"] + "', '" + theDRC["ConditionalFieldSectionId"] + "', '" + theDT.Rows[i][0] + "');");
                                            }
                                            chkbox.Attributes.Add("onclick", "" + EnableDisableControl + "");
                                        }
                                    }
                                    else
                                    {
                                        theDVC.RowFilter = "ConFieldId=" + FieldId + "";
                                        StringBuilder EnableDisableControl_1 = new StringBuilder();
                                        if (theDVC.ToTable().Rows.Count > 0)
                                        {
                                            foreach (DataRow theDRC in theDVC.ToTable().Rows)
                                            {
                                                EnableDisableControl_1.Append("EnableValuechkbox('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + chkbox.ID + "','" + theDRC["ConControlId"] + "', '" + theDRC["ConditionalFieldSectionId"] + "', '" + theDT.Rows[i][0] + "');");
                                            }
                                            chkbox.Attributes.Add("onclick", "" + EnableDisableControl_1 + "");
                                        }
                                        //chkbox.Attributes.Add("onclick", "SetValue('theHitCntrl','System.Web.UI.WebControls.CheckBox%" + chkbox.ClientID + "');");
                                        //chkbox.Load += new EventHandler(MultiSelect_Load);
                                    }
                                    PnlMulti.Controls.Add(chkbox);
                                    //ApplyBusinessRules(chkbox, ControlID);
                                    chkbox.Width = 300;
                                    PnlMulti.Controls.Add(new LiteralControl("<br>"));
                                }
                            }
                        }
                        PnlMulti.Controls.Add(new LiteralControl("</DIV>"));
                        DIVCustomItem.Controls.Add(PnlMulti);
                        ApplyBusinessRules(PnlMulti, ControlID, theEnable);
                        //PnlMulti.Enabled = theEnable;
                        DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
                    }
                }
                else if (ControlID == "10") ///  Regimen
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
                    Label theLabel = new Label();
                    HiddenField theHF = new HiddenField();
                    if (SetBusinessrule(FieldId, Column) == true)
                    {
                        DIVCustomItem.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                        ARLHeader.Add(Label);
                    }
                    else
                    {
                        DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                    }
                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));
                    RegimenType = GetFilterId(FieldId, Column);
                    theHF.ID = "hfReg-10-" + FieldId + "-" + Table + "-" + Column + "-" + RegimenType;
                    theHF.Value = Label;
                    DIVCustomItem.Controls.Add(theHF);
                    TextBox theRegText = new TextBox();
                    theRegText.ID = "TXTReg-" + Column + "-" + Table + "-" + FieldId + "=" + RegimenType + "-" + TabID;
                    theRegText.Attributes.Add("readonly", "readonly");
                    //theRegText.Enabled = theEnable;
                    theRegText.Width = 100;
                    theRegText.MaxLength = 200;
                    DIVCustomItem.Controls.Add(theRegText);
                    tabContainer.ID = "TAB";
                    Control ctl = (TextBox)theRegText;
                    IQCareUtils theUtils = new IQCareUtils();
                    if (!IsPostBack)
                    {
                        if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
                        {
                            Session.Remove("SelectedReg" + FieldId + RegimenType + "");
                        }
                    }
                    if (Session["SelectedReg" + FieldId + RegimenType + ""] == null)
                    {
                        DataView theDV = new DataView((DataTable)Session["MasterCustomfrmReg"]);
                        theDV.RowFilter = "DrugTypeId=" + RegimenType + " and Generic<>0";
                        DataTable theDT = theUtils.CreateTableFromDataView(theDV);
                        Session["Reg" + FieldId + RegimenType + ""] = theDT;
                    }

                    HtmlInputButton theBtn = new HtmlInputButton();
                    theBtn.ID = "BtnRegimen-" + Column + "-" + Table + "-" + FieldId + "-" + TabID;
                    theBtn.Visible = theEnable;
                    theBtn.Value = "...";
                    theBtn.Attributes.Add("onclick", "javascript:OpenRegimenDialog('" + RegimenType + "','ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + ((TextBox)ctl).ClientID + "'); return false");
                    DIVCustomItem.Controls.Add(theBtn);
                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
                }
                else if (ControlID == "11") ///  Drug Selection
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:100%' align='left'>"));
                    IQCareUtils theUtils = new IQCareUtils();
                    DrugType = GetFilterId(FieldId, Column);
                    DataView theDVName = new DataView((DataTable)Session["DrugTypeName"]);
                    if (DrugType > 0)
                    {
                        theDVName.RowFilter = "DrugTypeId=" + DrugType + "";
                        HiddenField theHF = new HiddenField();
                        Label theLabel = new Label();
                        theLabel.ID = "lblDrg-" + Column + "-" + DrugType;
                        theHF.ID = "hfDrg-11-" + FieldId + "-" + Column + "-" + DrugType;
                        theLabel.Text = Label + " - " + theDVName.ToTable().Rows[0]["DrugTypeName"].ToString();
                        theLabel.Font.Bold = true;
                        if (SetBusinessrule(FieldId, Column) == true)
                        {
                            DIVCustomItem.Controls.Add(new LiteralControl("<label class='required' align='left' id='lbl" + Label + "-" + FieldId + "'>" + Label + " - " + theDVName.ToTable().Rows[0]["DrugTypeName"].ToString() + " :</label>"));
                            ARLHeader.Add(theLabel.Text);
                        }
                        else
                        {
                            DIVCustomItem.Controls.Add(new LiteralControl("<label align='left' id='lbl" + Label + "-" + FieldId + "'>" + Label + " - " + theDVName.ToTable().Rows[0]["DrugTypeName"].ToString() + " :</label>"));
                        }
                        theHF.Value = Label + " - " + theDVName.ToTable().Rows[0]["DrugTypeName"].ToString();
                        DIVCustomItem.Controls.Add(theHF);
                        DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:100%' align='center'>"));
                        Button theBtn = new Button();
                        theBtn.Width = 100;
                        theBtn.ID = "BtnDrg-" + Column + "-" + Table + "-" + FieldId + "-" + TabID;
                        theBtn.Text = "Drug Selection";

                        if (Session["Selected" + DrugType + ""] == null)
                        {
                            DataView theDV = new DataView((DataTable)Session["MasterCustomfrmReg"]);
                            theDV.RowFilter = "DrugTypeId=" + DrugType + " and Generic=0";
                            DataTable theDT = theUtils.CreateTableFromDataView(theDV);
                            Session["" + DrugType + ""] = theDT;
                        }
                        theBtn.Enabled = theEnable;
                        theBtn.Attributes.Add("onclick", "javascript:OpenPharmacyDialog('" + DrugType + "'); return false");
                        DIVCustomItem.Controls.Add(theBtn);
                        DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:100%' align='left'>"));
                        DrugsHeading(DrugType);
                        ApplyBusinessRules(theBtn, theBtn.ID, theEnable);
                        if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
                        {
                            DrugDataBinding(theBtn.ID, DrugType);
                        }

                        if ((DataTable)Session["Selected" + DrugType + ""] != null)
                        {
                            DataTable theDT = (DataTable)Session["Selected" + DrugType + ""];
                            LoadNewDrugs(theDT);
                        }

                        DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
                    }
                }
                /*
                 * else if (ControlID == "12") ///  Lab Selection
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:100%' align='left'>"));
                    HiddenField theHF = new HiddenField();
                    Label theLabel = new Label();
                    theLabel.ID = "lblLab-" + Column;
                    theHF.ID = "hfLab-12-" + FieldId + "-" + Column;
                    theLabel.Text = Label;
                    if (SetBusinessrule(FieldId, Column) == true)
                    {
                        DIVCustomItem.Controls.Add(new LiteralControl("<label class='required' align='left' id='lbl" + Label + "-" + FieldId + "'>" + theLabel.Text + ":</label>"));
                        ARLHeader.Add(theLabel.Text);
                    }
                    else
                    {
                        DIVCustomItem.Controls.Add(new LiteralControl("<label align='left' id='lbl" + Label + "-" + FieldId + "'>" + theLabel.Text + " :</label>"));
                    }
                    theHF.Value = theLabel.Text;
                    DIVCustomItem.Controls.Add(theHF);
                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:100%' align='center'>"));
                    Button theBtn = new Button();
                    theBtn.Width = 100;
                    theBtn.ID = "BtnLab-" + Column + "-" + Table + "-" + FieldId + "-" + TabID;
                    theBtn.Text = "Lab Test";
                    theBtn.Enabled = theEnable;
                    theBtn.Attributes.Add("onclick", "javascript:AdditionalLab(); return false");
                    DIVCustomItem.Controls.Add(theBtn);
                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:100%' align='left'>"));
                    ApplyBusinessRules(theBtn, theBtn.ID, theEnable);
                    if ((DataSet)Session["AddLab"] != null)
                    {
                        //  ViewState["LabMaster"] = ((DataSet)Session["AddLab"]).Tables[0];
                        // ViewState["AddLab"] = ((DataSet)Session["AddLab"]).Tables[1];
                        ViewState["AddLab"] = Session["AddLab"];
                        Session.Remove("AddLab");
                    }
                    if (Convert.ToString(ViewState["btnlabisEnable"]) == "2")
                    {
                        ViewState["AddLab"] = null;
                    }

                    if ((DataTable)ViewState["AddLab"] != null)
                    {
                        foreach (DataRow theDR in ((DataTable)ViewState["AddLab"]).Rows)
                        {
                            if (theDR["Flag"] == System.DBNull.Value)
                            {
                                BindCustomControls(theDR);
                            }
                        }
                        Session["SelectedData"] = ViewState["AddLab"];
                    }

                    if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
                    {
                        LabDataBinding();
                    }

                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
                }*/
                else if (ControlID == "13")  /// Placeholder
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:100%;Height:25px' align='right'>"));
                    HtmlGenericControl div1 = new HtmlGenericControl("div");
                    div1.ID = "DIVPLC-" + Column + "-" + FieldId;
                    PlaceHolder thePlchlderText = new PlaceHolder();
                    thePlchlderText.ID = "plchlder-" + Column + "-" + FieldId + "-" + TabID;
                    thePlchlderText.Controls.Add(div1);
                    DIVCustomItem.Controls.Add(thePlchlderText);
                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
                }
                else if (ControlID == "18")  /// BMI
                {
                    {
                        DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));

                        if (SetBusinessrule(FieldId, Column) == true)
                        {
                            DIVCustomItem.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                        }
                        else
                        {
                            DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                        }
                        DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));
                        TextBox theSingleDecimalText = new TextBox();
                        theSingleDecimalText.ID = "TXT-" + Column + "-" + Table + "-" + FieldId + "-" + TabID;
                        // theSingleDecimalText.ID = "TXT-BMI-DTL_PATIENTVITALS-" + FieldId + "-" + TabID;
                        //theSingleDecimalText.Text="00";
                        theSingleDecimalText.Width = 180;
                        theSingleDecimalText.MaxLength = 50;
                        theSingleDecimalText.Enabled = theEnable;
                        DIVCustomItem.Controls.Add(theSingleDecimalText);
                        ApplyBusinessRules(theSingleDecimalText, ControlID, theEnable);
                        tabContainer.ID = "TAB";
                        theSingleDecimalText.Attributes.Add("onkeyup", "chkDecimal('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theSingleDecimalText.ClientID + "')");

                        BmiID = "ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theSingleDecimalText.ClientID;

                        TextBox thehiddenText = new TextBox();
                        thehiddenText.ID = "" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theSingleDecimalText.ClientID + "";
                        thehiddenText.Width = 0;
                        divhidden.Controls.Add(thehiddenText);

                        //theSingleDecimalText.Enabled = theEnable;
                        DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("</table>"));

                        Int32 PatientID = Convert.ToInt32(Session["PatientId"]);
                        Int32 VisitID = Convert.ToInt32(Session["PatientVisitId"]);
                        Int32 LocationID = Convert.ToInt32(Session["ServiceLocationId"]);
                        ILabFunctions LabResultManager = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");

                        Int32 statusHW = 0;

                        if (IsHeightAvail == true && IsWeightAvail == true)
                        {
                            statusHW = 1;
                        }

                        DataTable dt = LabResultManager.GetBmiValue(PatientID, LocationID, VisitID, statusHW);
                        if (dt.Rows.Count > 0)
                        {
                            theSingleDecimalText.Text = Convert.ToString(Math.Round(Convert.ToDecimal(dt.Rows[0]["BMI"].ToString()), 1));
                        }

                        theSingleDecimalText.Attributes.Add("OnBlur", "CalcualteBMIGet();");
                        hdnBNI.Value = "ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + theSingleDecimalText.ClientID;
                        //hdnBNI.Value ='ctl00_IQCareContentPlaceHolder'_" + tabcontainer.ID + "_" + tbChildPanel.ID + "_"  +theSingleDecimalText.ClientID + "
                    }
                }
                else if (ControlID == "15")  /// Disease/Symptom Control
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<table>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
                    if (SetBusinessrule(FieldId, Column) == true)
                    {
                        DIVCustomItem.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                    }
                    else
                    {
                        DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                    }
                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));

                    //WithPanel

                    Panel PnlMulti = new Panel();
                    PnlMulti.ID = "Pnl_" + FieldId;
                    PnlMulti.ToolTip = Label;
                    PnlMulti.Enabled = theEnable;
                    PnlMulti.Controls.Add(new LiteralControl("<div class = 'diseasesymptomdivborder'  runat='server' nowrap='nowrap'>"));

                    if (CodeID == "")
                    {
                        CodeID = "0";
                    }
                    DataView theDV = new DataView(theDSXML.Tables[BindSource]);
                    theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ")";
                    IQCareUtils theUtils = new IQCareUtils();
                    BindFunctions BindManager = new BindFunctions();
                    DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    if (theDT != null)
                    {
                        for (int i = 0; i < theDT.Rows.Count; i++)
                        {
                            CheckBox chkbox = new CheckBox();
                            chkbox.ID = Convert.ToString("CHKMULTI-" + theDT.Rows[i][0] + "-" + Column + "-" + Table + "-" + FieldId + "-" + TabID);
                            chkbox.Text = Convert.ToString(theDT.Rows[i]["Name"]);
                            if (chkbox.Text == "Other")
                            {
                                PnlMulti.Controls.Add(chkbox);
                                PnlMulti.Controls.Add(new LiteralControl("<div  class='pad10' id='" + chkbox.ID + '-' + Column + "' style='display:none'>Specify: "));
                                HtmlInputText HTextother = new HtmlInputText();
                                HTextother.ID = "TXTMULTI-" + theDT.Rows[i][0] + "-" + Column + "-" + Table + "-" + FieldId + "-" + TabID;
                                HTextother.Size = 20;
                                PnlMulti.Controls.Add(HTextother);
                                PnlMulti.Controls.Add(new LiteralControl(HTextother.Value));

                                HtmlInputText HTextICDCode = new HtmlInputText();
                                HTextICDCode.ID = "TXTMULTIICDCode-" + theDT.Rows[i][0] + "-" + Column + "-" + Table + "-" + FieldId + "-" + TabID;
                                HTextICDCode.Size = 10;
                                HTextICDCode.Visible = false;
                                PnlMulti.Controls.Add(HTextICDCode);
                                PnlMulti.Controls.Add(new LiteralControl(HTextICDCode.Value));

                                Button theBtn = new Button();
                                theBtn.Width = 100;
                                theBtn.ID = "Btn-" + Column + "-" + i + "-" + FieldId;
                                theBtn.Text = "ICDCode";
                                theBtn.Attributes.Add("onclick", "javascript:OpenTreeViewPopup('" + HTextother.ID + "', '" + HTextICDCode.ID + "'); return false");
                                PnlMulti.Controls.Add(theBtn);

                                PnlMulti.Controls.Add(new LiteralControl("</div>"));
                                if (theConditional == true && theEnable == true)
                                    chkbox.Attributes.Add("onclick", "toggle('" + Column + "');SetValue('theHitCntrl','System.Web.UI.WebControls.CheckBox%" + chkbox.ClientID + "');");
                                else
                                    chkbox.Attributes.Add("onclick", "toggle('" + chkbox.ID + "-" + Column + "');");
                                PnlMulti.Controls.Add(new LiteralControl("<br>"));
                            }
                            else
                            {
                                if (theConditional == true && theEnable == true)
                                    chkbox.Attributes.Add("onclick", "SetValue('theHitCntrl','System.Web.UI.WebControls.CheckBox%" + chkbox.ClientID + "');");

                                //chkbox.Load += new EventHandler(MultiSelect_Load);

                                PnlMulti.Controls.Add(chkbox);
                                //ApplyBusinessRules(chkbox, ControlID);
                                chkbox.Width = 300;
                                PnlMulti.Controls.Add(new LiteralControl("<br>"));
                            }
                        }
                    }
                    PnlMulti.Controls.Add(new LiteralControl("</div>"));

                    DIVCustomItem.Controls.Add(PnlMulti);
                    ApplyBusinessRules(PnlMulti, ControlID, theEnable);
                    //PnlMulti.Enabled = theEnable;
                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
                }
                else if (ControlID == "16")  /// ICD10 Control
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:100%' align='left'>"));

                    Label theLabel = new Label();
                    theLabel.ID = "lblICD10-" + Column;
                    theLabel.Text = Label;
                    theLabel.Font.Bold = true;
                    DIVCustomItem.Controls.Add(new LiteralControl("<label align='left' id='lbl" + Label + "-" + FieldId + "'>" + Label + " </label>"));
                    ICD10Heading(FieldId);
                    DIVCustomItem.Controls.Add(new LiteralControl("<div class = 'customdivbordericd'  runat='server' nowrap='nowrap'>"));
                    BindSource = "VW_ICDList";
                    DataView theDV = new DataView(theDSXML.Tables[BindSource]);
                    theDV.RowFilter = "FieldId=" + FieldId + "and DeleteFlag=0";

                    IQCareUtils theUtils = new IQCareUtils();
                    BindFunctions BindManager = new BindFunctions();
                    DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    if (Session["SelectedICD" + Convert.ToInt32(FieldId) + ""] == null)
                    {
                        Session["SelectedICD" + Convert.ToInt32(FieldId) + ""] = CreateColumntheDTICD10();
                        theDV = new DataView((DataTable)ViewState["ICD10"]);
                        theDV.RowFilter = "FieldId=" + FieldId + " and Visit_pk=" + Convert.ToInt32(Session["PatientVisitId"]) + " and LocationID =" + Convert.ToInt32(Session["ServiceLocationId"]) + "";
                        DataTable theDTTemp = theDV.ToTable();
                        foreach (DataRow theDRICD1 in theDTTemp.Rows)
                        {
                            DataRow[] rows = theDT.Select("Name='" + theDRICD1["Name"].ToString().Replace("'", "") + "'");
                            if (rows.Length == 0)
                            {
                                ((DataTable)Session["SelectedICD" + Convert.ToInt32(FieldId) + ""]).Rows.Add(theDRICD1["FieldId"], theDRICD1["BlockId"], theDRICD1["SubBlockId"], theDRICD1["Id"], theDRICD1["CodeID"], theDRICD1["Name"]);
                                ((DataTable)Session["SelectedICD" + Convert.ToInt32(FieldId) + ""]).AcceptChanges();
                            }
                        }
                    }
                    if (((DataTable)Session["SelectedICD" + Convert.ToInt32(FieldId) + ""]) != null)
                    {
                        theDT.Merge(((DataTable)Session["SelectedICD" + Convert.ToInt32(FieldId) + ""]));
                    }
                    if (theDT != null)
                    {
                        Hashtable ht = new Hashtable();
                        ht.Clear();
                        for (int i = 0; i < theDT.Rows.Count; i++)
                        {
                            DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'>"));
                            if (!(ht.ContainsKey(Convert.ToString("CHKMULTI-" + FieldId + theDT.Rows[i][4] + "-" + "dtl_ICD10Field" + "-" + Column))))
                            {
                                DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                                DIVCustomItem.Controls.Add(new LiteralControl("<td width='50%'>"));
                                CheckBox chkbox = new CheckBox();
                                chkbox.ID = Convert.ToString("CHKMULTI-" + FieldId + theDT.Rows[i][4] + "-" + "dtl_ICD10Field" + "-" + Column + "-" + TabID);
                                ht.Add(chkbox.ID, chkbox.ID);
                                chkbox.Text = Convert.ToString(theDT.Rows[i]["Name"]);
                                DIVCustomItem.Controls.Add(chkbox);
                                //chkbox.Width = 400;
                                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                DIVCustomItem.Controls.Add(new LiteralControl("<td width='30%'>"));
                                TextBox theDateText = new TextBox();
                                theDateText.ID = "TXTDT-" + FieldId + theDT.Rows[i][4].ToString().Replace('%', '^') + "OnSetDate" + "-" + "dtl_ICD10Field" + "-" + Column + "-" + TabID;
                                Control ctl = (TextBox)theDateText;
                                theDateText.Width = 83;
                                theDateText.MaxLength = 11;
                                DIVCustomItem.Controls.Add(theDateText);
                                theDateText.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
                                theDateText.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");
                                DIVCustomItem.Controls.Add(new LiteralControl("&nbsp;"));
                                tabContainer.ID = "TAB";
                                Image theDateImage = new Image();
                                theDateImage.ID = "img" + theDateText.ID;
                                theDateImage.Height = 22;
                                theDateImage.Width = 22;
                                theDateImage.Visible = theEnable;
                                theDateImage.ToolTip = "Date Helper";
                                theDateImage.ImageUrl = "~/images/cal_icon.gif";
                                theDateImage.Attributes.Add("onClick", "w_displayDatePicker('ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + tbChildPanel.ID + "_" + ((TextBox)ctl).ClientID + "');");
                                DIVCustomItem.Controls.Add(theDateImage);
                                DIVCustomItem.Controls.Add(new LiteralControl("<span class='smallerlabel'>(DD-MMM-YYYY)</span>"));
                                DIVCustomItem.Controls.Add(new LiteralControl("&nbsp&nbsp;"));
                                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                DIVCustomItem.Controls.Add(new LiteralControl("<td width='20%'>"));
                                TextBox theCommentText = new TextBox();
                                theCommentText.ID = "TXTComment-" + FieldId + theDT.Rows[i][4].ToString().Replace('%', '~') + "ICDComment" + "-" + "dtl_ICD10Field" + "-" + Column + "-" + TabID;
                                theCommentText.Width = 250;
                                theCommentText.MaxLength = 200;
                                DIVCustomItem.Controls.Add(theCommentText);
                                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                                DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
                                //DIVCustomItem.Controls.Add(new LiteralControl("<br>"));
                            }
                        }
                    }
                    DIVCustomItem.Controls.Add(new LiteralControl("</div>"));

                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:100%' align='center'>"));
                    Button theBtn = new Button();
                    theBtn.Width = 180;
                    theBtn.ID = "BtnICD10-" + FieldId + "-" + "dtl_ICD10Field" + "-" + Column;
                    theBtn.Text = "Other ICD10 Disease/Symptom";
                    theBtn.Enabled = theEnable;
                    theBtn.Attributes.Add("onclick", "javascript:ICD10PopUp('" + FieldId + "'," + Convert.ToInt32(Session["PatientVisitId"]) + "); return false");
                    DIVCustomItem.Controls.Add(theBtn);
                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
                }
                else if (ControlID == "14") //Time Control
                {
                    DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
                    if (SetBusinessrule(FieldId, Column) == true)
                    {
                        DIVCustomItem.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                    }
                    else
                    {
                        DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                    }
                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));
                    IQCareUtils theUtil = new IQCareUtils();
                    DataView theDV = new DataView((DataTable)ViewState["BusRule"]);
                    theDV.RowFilter = "ControlId=14 and FieldId=" + FieldId + "";
                    DataTable theBDV = theDV.ToTable();
                    int Format = 0;
                    Label lblFormat = new Label();
                    foreach (DataRow theDR in theBDV.Rows)
                    {
                        if (Convert.ToString(theDR["BusRuleId"]) == "22")
                        {
                            //24 Hour
                            DropDownList ddlSelectList24Hr = new DropDownList();
                            ddlSelectList24Hr.ID = "SELECTLIST-" + Column + "-" + Table + "-" + FieldId + "24Hr" + "-" + TabID;
                            ddlSelectList24Hr.Width = 60;
                            DataTable theDT24 = theUtil.CreateHourFormat_24();
                            DataRow theDR24 = theDT24.NewRow();
                            theDR24[0] = "0";
                            theDR24[1] = "Select";
                            theDT24.Rows.InsertAt(theDR24, 0);
                            ddlSelectList24Hr.DataSource = theDT24;
                            ddlSelectList24Hr.DataTextField = "Time";
                            ddlSelectList24Hr.DataValueField = "Id";
                            ddlSelectList24Hr.DataBind();
                            ddlSelectList24Hr.SelectedValue = "Select";
                            ddlSelectList24Hr.Enabled = theEnable;
                            DIVCustomItem.Controls.Add(ddlSelectList24Hr);
                            //60 Minutes
                            DropDownList ddlSelectList60Min = new DropDownList();
                            ddlSelectList60Min.ID = "SELECTLIST-" + Column + "-" + Table + "-" + FieldId + "Min" + "-" + TabID;
                            ddlSelectList60Min.Width = 60;
                            DataTable theDT60 = theUtil.CreateMinuteFormat();
                            DataRow theDR60 = theDT60.NewRow();
                            theDR60[0] = "0";
                            theDR60[1] = "Select";
                            theDT60.Rows.InsertAt(theDR60, 0);
                            ddlSelectList60Min.DataSource = theDT60;
                            ddlSelectList60Min.DataTextField = "Time";
                            ddlSelectList60Min.DataValueField = "Id";
                            ddlSelectList60Min.DataBind();
                            ddlSelectList60Min.SelectedValue = "Select";
                            ddlSelectList60Min.Enabled = theEnable;
                            DIVCustomItem.Controls.Add(ddlSelectList60Min);
                            Format = 24;
                            lblFormat.Text = "24Hr";
                            lblFormat.CssClass = "smalllabel";
                            DIVCustomItem.Controls.Add(lblFormat);
                        }
                        else if (Convert.ToString(theDR["BusRuleId"]) == "23")
                        {
                            //12 Hour
                            DropDownList ddlSelectList12Hr = new DropDownList();
                            ddlSelectList12Hr.ID = "SELECTLIST-" + Column + "-" + Table + "-" + FieldId + "12Hr" + "-" + TabID;
                            ddlSelectList12Hr.Width = 60;
                            DataTable theDT12 = theUtil.CreateHourFormat_12();
                            DataRow theDR12 = theDT12.NewRow();
                            theDR12[0] = "0";
                            theDR12[1] = "Select";
                            theDT12.Rows.InsertAt(theDR12, 0);
                            ddlSelectList12Hr.DataSource = theDT12;
                            ddlSelectList12Hr.DataTextField = "Time";
                            ddlSelectList12Hr.DataValueField = "Id";
                            ddlSelectList12Hr.DataBind();
                            ddlSelectList12Hr.SelectedValue = "Select";
                            ddlSelectList12Hr.Enabled = theEnable;
                            DIVCustomItem.Controls.Add(ddlSelectList12Hr);
                            //60 Minutes
                            DropDownList ddlSelectList60Min = new DropDownList();
                            ddlSelectList60Min.ID = "SELECTLIST-" + Column + "-" + Table + "-" + FieldId + "Min" + "-" + TabID;
                            ddlSelectList60Min.Width = 60;
                            DataTable theDT60 = theUtil.CreateMinuteFormat();
                            DataRow theDR60 = theDT60.NewRow();
                            theDR60[0] = "0";
                            theDR60[1] = "Select";
                            theDT60.Rows.InsertAt(theDR60, 0);
                            ddlSelectList60Min.DataSource = theDT60;
                            ddlSelectList60Min.DataTextField = "Time";
                            ddlSelectList60Min.DataValueField = "Id";
                            ddlSelectList60Min.DataBind();
                            ddlSelectList60Min.SelectedValue = "Select";
                            ddlSelectList60Min.Enabled = theEnable;
                            DIVCustomItem.Controls.Add(ddlSelectList60Min);
                            //AM/PM
                            DropDownList ddlSelectListAMPM = new DropDownList();
                            ddlSelectListAMPM.ID = "SELECTLIST-" + Column + "-" + Table + "-" + FieldId + "AMPM" + "-" + TabID;
                            ddlSelectListAMPM.Width = 40;
                            DataTable theDTAMPM = theUtil.CreateAMPM();
                            ddlSelectListAMPM.DataSource = theDTAMPM;
                            ddlSelectListAMPM.DataTextField = "Format";
                            ddlSelectListAMPM.DataValueField = "Id";
                            ddlSelectListAMPM.DataBind();
                            ddlSelectListAMPM.SelectedValue = "0";
                            ddlSelectListAMPM.Enabled = theEnable;
                            DIVCustomItem.Controls.Add(ddlSelectListAMPM);
                            Format = 12;
                            lblFormat.Text = "12Hr";
                            lblFormat.CssClass = "smalllabel";
                            DIVCustomItem.Controls.Add(lblFormat);
                        }
                        else if (Convert.ToString(theDR["BusRuleId"]) == "24")
                        {
                            DIVCustomItem.Controls.Remove(lblFormat);
                            Button theButton = new Button();
                            if (Format == 12)
                            {
                                theButton.ID = "theBtn12AMPM-" + Column + "-" + Table + "-" + FieldId + "-" + TabID;
                                lblFormat.Text = "12Hr";
                                lblFormat.CssClass = "smalllabel";
                                theButton.Text = "Set Time";
                                theButton.Click += new EventHandler(theButton_Click);
                                theButton.Enabled = theEnable;
                                DIVCustomItem.Controls.Add(theButton);
                            }
                            else if (Format == 24)
                            {
                                theButton.ID = "theBtn-" + Column + "-" + Table + "-" + FieldId + "-" + TabID;
                                lblFormat.Text = "24Hr";
                                lblFormat.CssClass = "smalllabel";
                                theButton.Text = "Set Time";
                                theButton.Click += new EventHandler(theButton_Click);
                                theButton.Enabled = theEnable;
                                DIVCustomItem.Controls.Add(theButton);
                            }
                            DIVCustomItem.Controls.Add(lblFormat);
                        }
                    }


                    ////////////////////////////////////////////////
                    //ApplyBusinessRules(ddlSelectList24Hr, ControlID, theEnable);
                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
        }

        private void LoadNewDrugs(DataTable theDT)
        {
            foreach (DataRow theDR in theDT.Rows)
            {
                if (Convert.ToInt32(theDR["Flag"]) == 0)
                {
                    BindDrugControls(Convert.ToInt32(theDR[0]), Convert.ToInt32(theDR[2]), DrugType, Convert.ToInt32(theDR["Flag"]));
                }
            }
        }

        private void LoadPredefinedLabel_Field(int FeatureID, int PatientID)
        {
            theDSXML.ReadXml(MapPath("..\\XMLFiles\\AllMasters.con"));
            ICustomForm CustomFormMgr = (ICustomForm)ObjectFactory.CreateInstance(ObjFactoryParameter);
            DataSet theDS = CustomFormMgr.GetFieldName_and_Label(FeatureID, PatientID);
            ViewState["ICD10"] = theDS.Tables[22];
            DIVCustomItem.Controls.Clear();

            if (theDS.Tables[19].Rows.Count > 0)
            {
                AutoDt = (DataTable)theDS.Tables[19];
            }

            if (theDS.Tables[20].Rows.Count > 0)
            {
                AutoDtPre = (DataTable)theDS.Tables[20];
            }

            if (theDS.Tables[2].Rows.Count > 0)
            {
                DataView theDV = new DataView(theDS.Tables[2]);
                theDV.RowFilter = "BusRuleId = 13";
                if (theDV.Count > 0)
                {
                    //btncomplete.Visible = true;
                }
            }
            //try
            //{
            //    IsSIngleVisit = false;
            //For Single Visit or MultiVisit
            if (Convert.ToInt32(theDS.Tables[14].Rows[0]["MultiVisit"]) == 0)
            {
                // todo
                IsSIngleVisit = true;
                Label1.Text = "First Encounter Date:";
                if (theDS.Tables[15].Rows.Count > 0)
                {
                    txtvisitDate.Text = String.Format("{0:dd-MMM-yyyy}", theDS.Tables[15].Rows[0]["VisitDate"]);
                    hdnVisitData.Value = String.Format("{0:dd-MMM-yyyy}", theDS.Tables[15].Rows[0]["VisitDate"]);
                    Page.ClientScript.RegisterStartupScript(Type.GetType("System.String"), "addScript", "DataEncounter();", true);

                    Session["PatientVisitId"] = theDS.Tables[15].Rows[0]["Visit_Id"];
                    Session["ServiceLocationId"] = theDS.Tables[0].Rows[0]["LocationId"];
                }
            }

            //All tables are put in Session in order to bind strength, UnitID, Frequency etc for Drug.
            Session["AllData"] = theDS;
            //Drug and Regimen Master Data
            if (Session["SelectedCustomfrmRegimen"] == null)
            {
                Session["MasterCustomfrmReg"] = theDS.Tables[4];
                Session["DrugTypeName"] = theDS.Tables[13];
            }
            //LabMasterData
            Session["MasterData"] = theDS.Tables[6];

            //----- Clearing dynamic Drug and ICD10 Session.
            if (!IsPostBack)
            {
                foreach (DataRow r in ((DataTable)Session["MasterCustomfrmReg"]).Rows)
                {
                    if (Session["Selected" + r["DrugTypeId"].ToString() + ""] != null)
                    {
                        Session.Remove("Selected" + r["DrugTypeId"].ToString() + "");
                    }
                }
                Session.Remove("SelectedData");
                foreach (DataRow theDRICDRemoveSession in theDS.Tables[1].Rows)
                {
                    if (Session["SelectedICD" + Convert.ToInt32(theDRICDRemoveSession["FieldId"]) + ""] != null && Convert.ToInt32(theDRICDRemoveSession["ControlId"]) == 16)
                    {
                        Session.Remove("SelectedICD" + Convert.ToInt32(theDRICDRemoveSession["FieldId"]) + "");
                    }
                }
            }
            if (theDS.Tables[1].Rows.Count > 0)
            {
                theHeader.InnerHtml = theDS.Tables[1].Rows[0]["FeatureName"].ToString();
            }

            ViewState["BusRule"] = theDS.Tables[2];

            if (theDS.Tables[0].Rows.Count > 0)
            {
                hdfldDOB.Value = String.Format("{0:dd-MMM-yyyy}", theDS.Tables[0].Rows[0]["DOB"]);
            }
            //For Loading Controls in the form
            if (theDS.Tables[1].Rows.Count > 0)
            {
                ViewState["LnkTable"] = theDS.Tables[1];
            }
            DataTable DT = theDS.Tables[1].DefaultView.ToTable(true, "SectionID", "SectionName", "IsGridView", "TabId").Copy();
            int Numtds = 2, td = 1;
            DIVCustomItem.Controls.Clear();

            DataTable theConditionalFields = theDS.Tables[17].Copy();
            theConditionalFields.Columns.Add("Load", typeof(System.String));
            theConditionalFields.Columns["Load"].DefaultValue = "T";

            foreach (DataRow theMDR in theDS.Tables[17].Rows)
            {
                Int32 theFieldId = Convert.ToInt32(theMDR["FieldId"]);
                bool theRecFnd = false;
                foreach (DataRow theDR in theConditionalFields.Rows)
                {
                    if (Convert.ToInt32(theDR["FieldId"]) == theFieldId && theRecFnd == true)
                        theDR["Load"] = "F";
                    else if (Convert.ToInt32(theDR["FieldId"]) == theFieldId)
                    {
                        theDR["Load"] = "T";
                        theRecFnd = true;
                    }
                }
                theRecFnd = false;
            }
            theConditionalFields.AcceptChanges();
            CreateTab(theDS.Tables[23]);
            int z = 0, SignatureFlag = 0;
            foreach (DataRow distincttabdr in theDS.Tables[23].Rows)
            {
                DIVCustomItem = new Panel();
                DIVCustomItem.CssClass = "border center formbg";
                DIVCustomItem.Controls.Add(new LiteralControl("</br>"));
                foreach (DataRow dr in DT.Rows)
                {
                    bool spanfieldflag = false;
                    if (Convert.ToInt32(distincttabdr["TabId"]) == Convert.ToInt32(dr["TabId"]))
                    {
                        tbChildPanel.ID = dr["TabId"].ToString();
                        SectionHeading(dr["SectionName"].ToString());
                        DIVCustomItem.Controls.Add(new LiteralControl("<table cellspacing='6' cellpadding='0' width='100%' border='0'>"));

                        //bool isHeight = false;
                        //bool isWeight = false;

                        foreach (DataRow DRLnkTable in theDS.Tables[1].Rows)
                        {

                            this.bindSource = DRLnkTable["BindSource"].ToString();
                            this.bindCategory = DRLnkTable["BindCategory"].ToString();
                            this.controlReferenceId = DRLnkTable["ReferenceId"].ToString();

                            DataView dvchkHeight = theDS.Tables[1].DefaultView;
                            dvchkHeight.RowFilter = "FieldName = 'Height'   and Predefined=1";
                            if (dvchkHeight.Count > 0)
                            {
                                IsHeightAvail = true;
                            }
                            DataView dvchkWeight = theDS.Tables[1].DefaultView;
                            dvchkWeight.RowFilter = "FieldName = 'Weight'   and Predefined=1";
                            if (dvchkWeight.Count > 0)
                            {
                                IsWeightAvail = true;
                            }
                            if (dr["SectionID"].ToString() == DRLnkTable["SectionID"].ToString())
                            {
                                #region "CheckConditionalFields"

                                DataView theDVConditionalField = new DataView(theConditionalFields);
                                theDVConditionalField.RowFilter = "ConFieldId=" + DRLnkTable["FieldID"].ToString() + " and ConFieldPredefined=" + DRLnkTable["Predefined"].ToString() + " and Load = 'T'";
                                theDVConditionalField.Sort = "ConditionalFieldSectionId, Seq asc";
                                if (theDVConditionalField.Count > 0)
                                    theConditional = true;
                                else
                                    theConditional = false;

                                #endregion "CheckConditionalFields"

                                #region "CheckSpanFields"

                                DataView theDVspanField = new DataView(theDS.Tables[2]);
                                theDVspanField.RowFilter = "BusRuleId=25 and FieldId = " + DRLnkTable["FieldID"].ToString();

                                if (theDVspanField.Count > 0)
                                    spanfieldflag = true;
                                else
                                    spanfieldflag = false;

                                #endregion "CheckSpanFields"

                                #region "Check if Multi select has business rules 18 19 20"

                                DataView theDVMultiSelect = new DataView(theDS.Tables[2]);
                                theDVMultiSelect.RowFilter = "(BusRuleId=18 or BusRuleId=19 or BusRuleId=20) and FieldId = " + DRLnkTable["FieldID"].ToString();

                                if (theDVMultiSelect.Count > 0)
                                    Session["busRulChk"] = 1;
                                else
                                    Session["busRulChk"] = 0;

                                #endregion "Check if Multi select has business rules 18 19 20"

                                if (td <= Numtds)
                                {
                                    if (td == 1)
                                        DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));

                                    if (((Convert.ToInt32(DRLnkTable["Controlid"]) == 9) && (Convert.ToInt32(Session["busRulChk"]) == 1)) 
                                        || (Convert.ToInt32(DRLnkTable["Controlid"]) == 11) 
                                        || (Convert.ToInt32(DRLnkTable["Controlid"]) == 12) 
                                        || (Convert.ToInt32(DRLnkTable["Controlid"]) == 16)
                                       
                                        )
                                    {
                                        if (td == 1)
                                        {
                                            DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' colspan='2' style='width: 100%'>"));
                                            LoadFieldTypeControl(DRLnkTable["Controlid"].ToString(), DRLnkTable["FieldName"].ToString(), DRLnkTable["FieldID"].ToString(), DRLnkTable["CodeID"].ToString(), DRLnkTable["FieldLabel"].ToString(), DRLnkTable["PDFTableName"].ToString(), DRLnkTable["TabId"].ToString(), DRLnkTable["BindSource"].ToString(), true);
                                            td = 1;
                                        }
                                        else
                                        {
                                            DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' style='width: 50%'>"));
                                            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                            DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                                            DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                                            DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' colspan='2' style='width: 100%'>"));
                                            LoadFieldTypeControl(DRLnkTable["Controlid"].ToString(), DRLnkTable["FieldName"].ToString(), DRLnkTable["FieldID"].ToString(), DRLnkTable["CodeID"].ToString(), DRLnkTable["FieldLabel"].ToString(), DRLnkTable["PDFTableName"].ToString(), DRLnkTable["TabId"].ToString(), DRLnkTable["BindSource"].ToString(), true);
                                            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                            DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                                            td = 1;
                                        }
                                    }
                                    else
                                    {
                                        if ((Convert.ToInt32(DRLnkTable["Controlid"]) == 8) && (spanfieldflag == true))
                                        {
                                            if (td == 1)
                                            {
                                                DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' colspan='2' style='width: 100%'>"));
                                                LoadFieldTypeControl(DRLnkTable["Controlid"].ToString(), DRLnkTable["FieldName"].ToString(), DRLnkTable["FieldID"].ToString(), DRLnkTable["CodeID"].ToString(), DRLnkTable["FieldLabel"].ToString(), DRLnkTable["PDFTableName"].ToString(), DRLnkTable["TabId"].ToString(), DRLnkTable["BindSource"].ToString(), true);
                                                td = 1;
                                            }
                                            else
                                            {
                                                DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' style='width: 50%'>"));
                                                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                                DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                                                DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                                                DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' colspan='2' style='width: 100%'>"));
                                                LoadFieldTypeControl(DRLnkTable["Controlid"].ToString(), DRLnkTable["FieldName"].ToString(), DRLnkTable["FieldID"].ToString(), DRLnkTable["CodeID"].ToString(), DRLnkTable["FieldLabel"].ToString(), DRLnkTable["PDFTableName"].ToString(), DRLnkTable["TabId"].ToString(), DRLnkTable["BindSource"].ToString(), true);
                                                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                                DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                                                td = 1;
                                            }
                                        }
                                        else
                                        {
                                            if (td == 1)
                                            {
                                                DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' style='width: 50%'>"));
                                                LoadFieldTypeControl(DRLnkTable["Controlid"].ToString(), DRLnkTable["FieldName"].ToString(), DRLnkTable["FieldID"].ToString(), DRLnkTable["CodeID"].ToString(), DRLnkTable["FieldLabel"].ToString(), DRLnkTable["PDFTableName"].ToString(), DRLnkTable["TabId"].ToString(), DRLnkTable["BindSource"].ToString(), true);
                                                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                                td++;
                                            }
                                            else
                                            {
                                                DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' style='width: 50%'>"));
                                                LoadFieldTypeControl(DRLnkTable["Controlid"].ToString(), DRLnkTable["FieldName"].ToString(), DRLnkTable["FieldID"].ToString(), DRLnkTable["CodeID"].ToString(), DRLnkTable["FieldLabel"].ToString(), DRLnkTable["PDFTableName"].ToString(), DRLnkTable["TabId"].ToString(), DRLnkTable["BindSource"].ToString(), true);
                                                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                                DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                                                td = 1;
                                            }
                                        }
                                    }
                                }

                                #region "Create Conditional Fields"

                                if (theConditional == true)
                                {
                                    for (int i = 0; i < theDVConditionalField.Count; i++)
                                    {
                                        if (td <= Numtds)
                                        {
                                            theSecondLabelConditional = false;
                                            if (td == 1)
                                                DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));

                                            if ((Convert.ToInt32(theDVConditionalField[i]["Controlid"]) == 11) || (Convert.ToInt32(theDVConditionalField[i]["Controlid"]) == 12))
                                            {
                                                if (td == 1)
                                                {
                                                    DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' colspan='2' style='width: 50%'>"));
                                                    LoadFieldTypeControl(theDVConditionalField[i]["Controlid"].ToString(), theDVConditionalField[i]["FieldName"].ToString(), theDVConditionalField[i]["FieldID"].ToString(),
                                                        theDVConditionalField[i]["CodeID"].ToString(), theDVConditionalField[i]["FieldLabel"].ToString(), theDVConditionalField[i]["PDFTableName"].ToString(),
                                                        theDVConditionalField[i]["TabId"].ToString(), theDVConditionalField[i]["BindSource"].ToString(), false);
                                                    td = 1;
                                                }
                                                else
                                                {
                                                    DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' style='width: 50%'>"));
                                                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                                    DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                                                    DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                                                    DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' colspan='2' style='width: 50%'>"));
                                                    LoadFieldTypeControl(theDVConditionalField[i]["Controlid"].ToString(), theDVConditionalField[i]["FieldName"].ToString(), theDVConditionalField[i]["FieldID"].ToString(),
                                                        theDVConditionalField[i]["CodeID"].ToString(), theDVConditionalField[i]["FieldLabel"].ToString(), theDVConditionalField[i]["PDFTableName"].ToString(),
                                                        theDVConditionalField[i]["TabId"].ToString(), theDVConditionalField[i]["BindSource"].ToString(), false);
                                                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                                    DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                                                    td = 1;
                                                }
                                            }
                                            else
                                            {
                                                if ((Convert.ToInt32(theDVConditionalField[i]["Controlid"]) == 8) && (spanfieldflag == true))
                                                {
                                                    if (td == 1)
                                                    {
                                                        DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' colspan='2' style='width: 50%'>"));
                                                        LoadFieldTypeControl(theDVConditionalField[i]["Controlid"].ToString(), theDVConditionalField[i]["FieldName"].ToString(), theDVConditionalField[i]["FieldID"].ToString(),
                                                            theDVConditionalField[i]["CodeID"].ToString(), theDVConditionalField[i]["FieldLabel"].ToString(), theDVConditionalField[i]["PDFTableName"].ToString(),
                                                            theDVConditionalField[i]["TabId"].ToString(), theDVConditionalField[i]["BindSource"].ToString(), false);
                                                        td = 1;
                                                    }
                                                    else
                                                    {
                                                        DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' style='width: 50%'>"));
                                                        DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                                        DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                                                        DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                                                        DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' colspan='2' style='width: 50%'>"));
                                                        LoadFieldTypeControl(theDVConditionalField[i]["Controlid"].ToString(), theDVConditionalField[i]["FieldName"].ToString(), theDVConditionalField[i]["FieldID"].ToString(),
                                                            theDVConditionalField[i]["CodeID"].ToString(), theDVConditionalField[i]["FieldLabel"].ToString(), theDVConditionalField[i]["PDFTableName"].ToString(),
                                                            theDVConditionalField[i]["TabId"].ToString(), theDVConditionalField[i]["BindSource"].ToString(), false);
                                                        DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                                        DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                                                        td = 1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (td == 1)
                                                    {
                                                        DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' style='width: 50%'>"));
                                                        LoadFieldTypeControl(theDVConditionalField[i]["Controlid"].ToString(), theDVConditionalField[i]["FieldName"].ToString(), theDVConditionalField[i]["FieldID"].ToString(),
                                                            theDVConditionalField[i]["CodeID"].ToString(), theDVConditionalField[i]["FieldLabel"].ToString(), theDVConditionalField[i]["PDFTableName"].ToString(),
                                                            theDVConditionalField[i]["TabId"].ToString(), theDVConditionalField[i]["BindSource"].ToString(), false);
                                                        DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                                        td++;
                                                    }
                                                    else
                                                    {
                                                        DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' style='width: 50%'>"));
                                                        LoadFieldTypeControl(theDVConditionalField[i]["Controlid"].ToString(), theDVConditionalField[i]["FieldName"].ToString(), theDVConditionalField[i]["FieldID"].ToString(),
                                                            theDVConditionalField[i]["CodeID"].ToString(), theDVConditionalField[i]["FieldLabel"].ToString(), theDVConditionalField[i]["PDFTableName"].ToString(),
                                                            theDVConditionalField[i]["TabId"].ToString(), theDVConditionalField[i]["BindSource"].ToString(), false);
                                                        DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                                        DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                                                        td = 1;
                                                    }
                                                }
                                            }
                                        }
                                        ///////////////////////////////////////////////////////////////////////

                                        #region "CheckSecondLabelConditionalFields"

                                        DataView theDVSecondLabelConditionalField = new DataView(theConditionalFields);

                                        theDVSecondLabelConditionalField.RowFilter = "ConFieldId=" + theDVConditionalField[i]["FieldID"].ToString() + " and Load='T'";
                                        theDVSecondLabelConditionalField.Sort = "ConditionalFieldSectionId, Seq asc";
                                        if (theDVSecondLabelConditionalField.Count > 0)
                                            theSecondLabelConditional = true;
                                        else
                                            theSecondLabelConditional = false;

                                        #endregion "CheckSecondLabelConditionalFields"

                                        #region "Create Second Label Conditional Fields"

                                        if (theSecondLabelConditional == true)
                                        {
                                            for (int j = 0; j < theDVSecondLabelConditionalField.Count; j++)
                                            {
                                                if (td <= Numtds)
                                                {
                                                    if (td == 1)
                                                        DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));

                                                    if ((Convert.ToInt32(theDVSecondLabelConditionalField[j]["Controlid"]) == 11) || (Convert.ToInt32(theDVSecondLabelConditionalField[j]["Controlid"]) == 12))
                                                    {
                                                        if (td == 1)
                                                        {
                                                            DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' colspan='2' style='width: 50%'>"));
                                                            LoadFieldTypeControl(theDVSecondLabelConditionalField[j]["Controlid"].ToString(), theDVSecondLabelConditionalField[j]["FieldName"].ToString(), theDVSecondLabelConditionalField[j]["FieldID"].ToString(),
                                                                theDVSecondLabelConditionalField[j]["CodeID"].ToString(), theDVSecondLabelConditionalField[j]["FieldLabel"].ToString(), theDVSecondLabelConditionalField[j]["PDFTableName"].ToString(),
                                                                theDVSecondLabelConditionalField[j]["TabId"].ToString(), theDVSecondLabelConditionalField[j]["BindSource"].ToString(), false);
                                                            td = 1;
                                                        }
                                                        else
                                                        {
                                                            DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' style='width: 50%'>"));
                                                            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                                            DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                                                            DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                                                            DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' colspan='2' style='width: 50%'>"));
                                                            LoadFieldTypeControl(theDVSecondLabelConditionalField[j]["Controlid"].ToString(), theDVSecondLabelConditionalField[j]["FieldName"].ToString(), theDVSecondLabelConditionalField[j]["FieldID"].ToString(),
                                                                theDVSecondLabelConditionalField[j]["CodeID"].ToString(), theDVSecondLabelConditionalField[j]["FieldLabel"].ToString(), theDVSecondLabelConditionalField[j]["PDFTableName"].ToString(),
                                                                theDVSecondLabelConditionalField[j]["TabId"].ToString(), theDVSecondLabelConditionalField[j]["BindSource"].ToString(), false);
                                                            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                                            DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                                                            td = 1;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (td == 1)
                                                        {
                                                            DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' style='width: 50%'>"));
                                                            LoadFieldTypeControl(theDVSecondLabelConditionalField[j]["Controlid"].ToString(), theDVSecondLabelConditionalField[j]["FieldName"].ToString(), theDVSecondLabelConditionalField[j]["FieldID"].ToString(),
                                                                theDVSecondLabelConditionalField[j]["CodeID"].ToString(), theDVSecondLabelConditionalField[j]["FieldLabel"].ToString(), theDVSecondLabelConditionalField[j]["PDFTableName"].ToString(),
                                                                theDVSecondLabelConditionalField[j]["TabId"].ToString(), theDVSecondLabelConditionalField[j]["BindSource"].ToString(), false);
                                                            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                                            td++;
                                                        }
                                                        else
                                                        {
                                                            DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' style='width: 50%'>"));
                                                            LoadFieldTypeControl(theDVSecondLabelConditionalField[j]["Controlid"].ToString(), theDVSecondLabelConditionalField[j]["FieldName"].ToString(), theDVSecondLabelConditionalField[j]["FieldID"].ToString(),
                                                                theDVSecondLabelConditionalField[j]["CodeID"].ToString(), theDVSecondLabelConditionalField[j]["FieldLabel"].ToString(), theDVSecondLabelConditionalField[j]["PDFTableName"].ToString(),
                                                                theDVSecondLabelConditionalField[j]["TabId"].ToString(), theDVSecondLabelConditionalField[j]["BindSource"].ToString(), false);
                                                            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                                            DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                                                            td = 1;
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        #endregion "Create Second Label Conditional Fields"

                                        /////////////////////////////////////////////////////////////////////////////
                                    }
                                }

                                #endregion "Create Conditional Fields"
                            }
                            this.bindCategory = this.bindSource = this.controlReferenceId = "";
                        }
                        if (td == 2)
                        {
                            DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' style='width: 50%'>"));
                            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                            DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                        }
                        td = 1;

                        #region "Grid Section"

                        if (dr["IsGridView"].ToString() == "1")
                        {
                            DataTable theDT = new DataTable();

                            DataColumn dtDataColumn;
                            DataTable thedtGridViewField = new DataTable();
                            thedtGridViewField = theDS.Tables[1].Copy();
                            DataView theDVGridView = new DataView(thedtGridViewField);
                            theDVGridView.RowFilter = "SectionID =" + dr["SectionID"].ToString();
                            if (!IsPostBack)
                            {
                                if (gblDTGridViewControls.Rows.Count > 0)
                                {
                                    gblDTGridViewControls.Merge(theDVGridView.ToTable());
                                }
                                else
                                {
                                    gblDTGridViewControls = theDVGridView.ToTable();
                                }
                                ViewState["gblDTGridViewControls"] = gblDTGridViewControls;
                            }
                            for (int i = 0; i < theDVGridView.Count; i++)
                            {
                                dtDataColumn = new DataColumn();
                                dtDataColumn.DataType = GetControlDataType(Convert.ToInt32(theDVGridView[i]["ControlID"]));// Type.GetType("System.String");
                                dtDataColumn.ColumnName = theDVGridView[i]["FieldLabel"].ToString();
                                theDT.Columns.Add(dtDataColumn);
                            }

                            DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                            DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' colspan='2' style='width: 50%'>"));
                            DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'>"));
                            DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                            DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:100%' align='center'>"));

                            GridView objdView = new GridView();

                            objdView.ID = "Dview_" + dr["SectionID"].ToString();
                            objdView.AutoGenerateColumns = false;

                            objdView.CssClass = "datatable";
                            objdView.SelectedIndexChanging += new GridViewSelectEventHandler(objdView_SelectedIndexChanging);
                            objdView.RowDeleting += new GridViewDeleteEventHandler(grdView_RowDeleted);
                            objdView.RowDataBound += new GridViewRowEventHandler(grdView_RowDataBound);
                            objdView.AllowSorting = true;
                            objdView.BorderWidth = 1;
                            objdView.GridLines = GridLines.None;
                            objdView.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
                            objdView.RowStyle.CssClass = "row";
                            objdView.Width = Unit.Percentage(100);

                            foreach (DataColumn c in theDT.Columns)
                            {
                                if (c.DataType.ToString() == "System.Int32")
                                {
                                    c.DataType = System.Type.GetType("System.String");
                                }
                                objdView.Columns.Add(CreateBoundColumn(c));
                            }

                            CommandField objfield = new CommandField();
                            objfield.ButtonType = ButtonType.Link;
                            objfield.DeleteText = "<img src='../Images/del.gif' alt='Delete this' border='0' />";
                            objfield.ShowDeleteButton = true;
                            objdView.Columns.Add(objfield);

                            Button theBtn = new Button();
                            theBtn.Width = 100;
                            theBtn.ID = "BtnAdd-" + dr["SectionID"].ToString();
                            theBtn.Text = "Add";
                            theBtn.Enabled = true;
                            theBtn.Click += delegate(object sender, EventArgs e)
                            {
                                AjaxControlToolkit.TabContainer container = (AjaxControlToolkit.TabContainer)tabContainer;
                                DataRow row = null;
                                row = theDT.NewRow();
                                string btnName = (sender as Button).ID;
                                string[] strsection = btnName.Split('-');
                                if (ViewState["SaveFlag_" + strsection[1]] == null)
                                {
                                    ViewState["SaveFlag_" + strsection[1]] = "Add";
                                }

                                if (ViewState["SaveFlag_" + strsection[1]].ToString() == "Add")
                                {
                                    int gridrecord = 0;
                                    if (ViewState["GridCache_" + strsection[1]] != null)
                                    {
                                        DataTable dtviewstateAdd = (DataTable)ViewState["GridCache_" + strsection[1]];
                                        gridrecord = dtviewstateAdd.Rows.Count + 1;
                                    }

                                    bool isRecord = false;
                                    for (int i = 0; i < theDT.Columns.Count; i++)
                                    {
                                        string ctlvalue = GetGridViewControlValue(container, theDT.Columns[i].ColumnName, thedtGridViewField);
                                        if (ctlvalue == "")
                                        {
                                            row[i] = DBNull.Value;
                                        }
                                        else
                                        {
                                            row[i] = (object)ctlvalue;
                                            isRecord = true;
                                        }
                                    }
                                    if (isRecord)
                                        theDT.Rows.Add(row);
                                }
                                else if (ViewState["SaveFlag_" + strsection[1]].ToString() == "Edit")
                                {
                                    int r = Convert.ToInt32(ViewState["SelectedRow_" + strsection[1]]);

                                    bool isRecord = false;
                                    DataTable dtviewstate = (DataTable)ViewState["GridCache_" + strsection[1]];
                                    if (dtviewstate.Rows.Count > 0)
                                    {
                                        if (r > -1)
                                        {
                                            dtviewstate.Rows.RemoveAt(r);
                                        }
                                    }
                                    for (int i = 0; i < theDT.Columns.Count; i++)
                                    {
                                        Type typeofdata = typeof(string);

                                        string ctlvalue = GetGridViewControlValue(container, theDT.Columns[i].ColumnName, thedtGridViewField);
                                        if (ctlvalue == "")
                                        {
                                            row[i] = DBNull.Value;
                                        }
                                        else
                                        {
                                            row[i] = (object)ctlvalue;
                                            isRecord = true;
                                        }
                                    }

                                    if (isRecord)
                                        theDT.Rows.Add(row);

                                    ViewState["SaveFlag_" + strsection[1]] = "Add";
                                    (sender as Button).Text = "Add";
                                }
                                if (ViewState["GridCache_" + strsection[1]] != null)
                                {
                                    DataTable dtviewstate = (DataTable)ViewState["GridCache_" + strsection[1]];
                                    dtviewstate.Merge(theDT);
                                    ViewState["GridCache_" + strsection[1]] = dtviewstate;
                                    BindGridView(strsection[1], container, (DataTable)ViewState["GridCache_" + strsection[1]]);
                                }
                                else
                                {
                                    ViewState["GridCache_" + strsection[1]] = (DataTable)theDT;

                                    BindGridView(strsection[1], container, theDT);
                                }
                            };

                            DIVCustomItem.Controls.Add(theBtn);
                            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                            DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));

                            DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                            DIVCustomItem.Controls.Add(new LiteralControl("<td style='width:100%' align='center'>"));

                            DIVCustomItem.Controls.Add(new LiteralControl("<table width='100%'; height:'25px'; align='center'>"));
                            DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                            DIVCustomItem.Controls.Add(new LiteralControl("<td>"));
                            DIVCustomItem.Controls.Add(new LiteralControl("<div class='grid'>"));

                            DIVCustomItem.Controls.Add(new LiteralControl("<div id = 'div-gridview' class='gridview whitebg'>"));
                            DIVCustomItem.Controls.Add(objdView);

                            DIVCustomItem.Controls.Add(new LiteralControl("</div>"));

                            DIVCustomItem.Controls.Add(new LiteralControl("</div>"));

                            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                            DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                            DIVCustomItem.Controls.Add(new LiteralControl("</table>"));

                            objdView.Visible = true;
                            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                            DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                            DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
                            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                            DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                        }
                        DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
                        DIVCustomItem.Controls.Add(new LiteralControl("</br>"));
                    }
                }

                        #endregion "Grid Section"

                #region "Signature Section"

                if (Convert.ToInt32(distincttabdr["Signature"]) == 1)
                {
                    SignatureFlag = 1;
                    DIVCustomItem.Controls.Add(new LiteralControl("<table cellspacing='6' cellpadding='0' width='100%' border='0'>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' colspan='2' style='width: 50%'>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("<label align='center' id='lbl" + distincttabdr["Signature"] + "' >Signature:</label>"));
                    DropDownList theDropDown = new DropDownList();
                    theDropDown.ID = "SELECTLIST-TABSignature-LNK_FORMTABORDVISIT-'00'-" + distincttabdr["TabId"];
                    BindDropdown(theDropDown);
                    DIVCustomItem.Controls.Add(theDropDown);
                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                    DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
                }

                #endregion "Signature Section"

                DIVCustomItem.Controls.Add(new LiteralControl("<table cellspacing='6' cellpadding='0' width='100%' border='0'>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' colspan='2' style='width: 50%'>"));
                Button btnDynSave = new Button();
                btnDynSave.ID = "btnSave-" + distincttabdr["TabId"];
                btnDynSave.Text = "Save";
                btnDynSave.Click += new EventHandler(btnDynSave_Click);
                DIVCustomItem.Controls.Add(btnDynSave);
                Button btnDynDQ = new Button();
                btnDynDQ.ID = "btnDQ-" + distincttabdr["TabId"];
                btnDynDQ.Click += new EventHandler(btnDynDQ_Click);
                btnDynDQ.Text = "Data Quality Check";
                DIVCustomItem.Controls.Add(btnDynDQ);
                Button btnDynPrint = new Button();
                btnDynPrint.ID = "btnPrint-" + distincttabdr["TabId"];
                btnDynPrint.Text = "Print";
                btnDynPrint.Attributes.Add("OnClick", "WindowPrint()");
                DIVCustomItem.Controls.Add(btnDynPrint);
                ///john 28th june 2013
                UserRights(btnDynSave, btnDynDQ, btnDynPrint, FeatureID);
                ///////////////////////////////////////////////////////////////////
                DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
                tabContainer.Tabs[z].Controls.Add(DIVCustomItem);
                PnlforTab.Controls.Add(tabContainer);
                z = z + 1;
            }
            if (SignatureFlag == 0) { TrSignatureAll.Visible = true; }
            //For Saving/Updating Controls in the form Except MultiSelect Items
            ViewState["NoMulti"] = theDS.Tables[3];

        }

        private DataTable NonARVDrug()
        {
            DataTable dtNonARV = new DataTable();
            dtNonARV.Columns.Add("DrugId", System.Type.GetType("System.Int32"));
            dtNonARV.Columns.Add("GenericId", System.Type.GetType("System.Int32"));
            dtNonARV.Columns.Add("UnitId", System.Type.GetType("System.Int32"));
            dtNonARV.Columns.Add("FrequencyID", System.Type.GetType("System.Int32"));
            dtNonARV.Columns.Add("SingleDose", System.Type.GetType("System.Decimal"));
            dtNonARV.Columns.Add("Duration", System.Type.GetType("System.Decimal"));
            dtNonARV.Columns.Add("QtyOrdered", System.Type.GetType("System.Decimal"));
            dtNonARV.Columns.Add("QtyDispensed", System.Type.GetType("System.Decimal"));
            dtNonARV.Columns.Add("ARFinance", System.Type.GetType("System.Int32"));
            dtNonARV.Columns.Add("DrugTypeId", System.Type.GetType("System.Int32"));
            return dtNonARV;
        }

        private DataTable OldRegimenList(string[] str, DataView theDV)
        {
            DataTable theDT = CreateSelectedTable();
            foreach (string reg in str)
            {
                theDV.RowFilter = "Abbr Like '" + reg + "%'";
                if (theDV.Count > 0)
                {
                    DataRow theDR = theDT.NewRow();
                    theDR[0] = theDV[0][0];
                    theDR[1] = theDV[0][1];
                    theDR[2] = theDV[0][2];
                    theDR[3] = theDV[0][3];
                    theDR[4] = theDV[0][4];

                    DataRow theTempeDR;
                    theTempeDR = theDT.Rows.Find(theDV[0][0]);
                    if (theTempeDR == null)
                    {
                        theDT.Rows.Add(theDR);
                    }
                }
            }
            return theDT;
        }

        private void OnBlur()
        {
            string script = "<script language = 'javascript' defer ='defer' id = 'confirmonblur'>\n";
            script += "SendCodeName('" + txtvisitDate.ClientID + "')\n";
            script += "</script>\n";
            // ClientScript.RegisterStartupScript(this.GetType(),"confirmonblur", script);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "confirmonblur", script);
        }

        /// <summary>
        /// Handles the AsyncPostBackError event of the PageScriptManager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="AsyncPostBackErrorEventArgs"/> instance containing the event data.</param>
        private void PageScriptManager_AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
        {
            string message = e.Exception.Message;
            Master.PageScriptManager.AsyncPostBackErrorMessage = message;
        }

        private DataTable PtnCustomformselectedDataTable()
        {
            DataTable theDT = new DataTable();
            theDT.Columns.Add("DrugId", System.Type.GetType("System.Int32"));
            theDT.Columns.Add("DrugName", System.Type.GetType("System.String"));
            theDT.Columns.Add("Generic", System.Type.GetType("System.Int32"));
            theDT.Columns.Add("DrugTypeID", System.Type.GetType("System.Int32"));
            theDT.Columns.Add("DrugAbbr", System.Type.GetType("System.String"));
            theDT.Columns.Add("Flag", System.Type.GetType("System.Int32"));
            return theDT;
        }

        private DataTable PtnCustomformselectedDataTableDrug(DataTable DT, int DrugTypeId)
        {
            DataView theMstDV = new DataView((DataTable)Session["MasterCustomfrmReg"]);
            theMstDV.RowFilter = "DrugTypeId=" + DrugTypeId;
            DataTable theMSTDT = theMstDV.ToTable();

            DataTable theDTDrug = new DataTable();
            theDTDrug.Columns.Add("DrugId", System.Type.GetType("System.Int32"));
            theDTDrug.Columns.Add("DrugName", System.Type.GetType("System.String"));
            theDTDrug.Columns.Add("Generic", System.Type.GetType("System.Int32"));
            theDTDrug.Columns.Add("DrugTypeID", System.Type.GetType("System.Int32"));
            theDTDrug.Columns.Add("DrugAbbr", System.Type.GetType("System.String"));
            theDTDrug.Columns.Add("Flag", System.Type.GetType("System.Int32"));

            foreach (DataRow thedrI in DT.Rows)
            {
                foreach (DataRow thedrII in theMSTDT.Rows)
                {
                    int DrugId = Convert.ToInt32(thedrI["GenericID"]) == 0 ? Convert.ToInt32(thedrI["DrugId"]) : Convert.ToInt32(thedrI["GenericID"]);
                    if (DrugId == Convert.ToInt32(thedrII["DrugId"]))
                    {
                        DataRow TmpDR = theDTDrug.NewRow();
                        TmpDR[0] = thedrII["DrugId"];
                        TmpDR[1] = thedrII["DrugName"];
                        TmpDR[2] = thedrII["Generic"];
                        TmpDR[3] = thedrII["DrugTypeID"];
                        TmpDR[4] = thedrII["Abbr"];
                        TmpDR[5] = 1;
                        theDTDrug.Rows.Add(TmpDR);
                    }
                }
            }
            //DataTable theDT1 = theMSTARVDT;
            foreach (DataRow thedrI in DT.Rows)
            {
                int DrugId = Convert.ToInt32(thedrI["GenericID"]) == 0 ? Convert.ToInt32(thedrI["DrugId"]) : Convert.ToInt32(thedrI["GenericID"]);
                DataRow[] theDR1 = theMSTDT.Select("DrugId=" + DrugId);
                theMSTDT.Rows.Remove(theDR1[0]);
            }
            Session["" + DrugType + ""] = theMSTDT;

            return theDTDrug;
        }

        private DataTable PtnCustomformselectedDataTableLab(DataTable DT)
        {
            DataTable DTMstLab = (DataTable)Session["MasterData"];
            DataTable theDTLab = new DataTable();
            theDTLab.Columns.Add("LabTestID", System.Type.GetType("System.Int32"));
            theDTLab.Columns.Add("LabName", System.Type.GetType("System.String"));
            theDTLab.Columns.Add("SubTestID", System.Type.GetType("System.Int32"));
            theDTLab.Columns.Add("SubTestName", System.Type.GetType("System.String"));
            theDTLab.Columns.Add("LabTypeId", System.Type.GetType("System.Int32"));
            theDTLab.Columns.Add("Flag", System.Type.GetType("System.Int32"));

            foreach (DataRow thedrI in DT.Rows)
            {
                foreach (DataRow thedrII in DTMstLab.Rows)
                {
                    if (Convert.ToInt32(thedrI["SubTestID"]) == Convert.ToInt32(thedrII["SubTestID"]))
                    {
                        DataRow TmpDr = theDTLab.NewRow();
                        TmpDr[0] = thedrII["LabTestID"];
                        TmpDr[1] = thedrII["LabName"];
                        TmpDr[2] = thedrII["SubTestID"];
                        TmpDr[3] = thedrII["SubTestName"];
                        TmpDr[4] = thedrII["LabTypeId"];
                        TmpDr[5] = 1;
                        theDTLab.Rows.Add(TmpDr);
                    }
                }
            }
            foreach (DataRow thedrI in DT.Rows)
            {
                DataRow[] theDR1 = DTMstLab.Select("SubTestId=" + thedrI["SubTestID"]);
                DTMstLab.Rows.Remove(theDR1[0]);
            }
            Session["MasterData"] = DTMstLab;
            return theDTLab;
        }

        private DataTable ReadARVMedicationTable(Control theContainer, int TabId)
        {
            DataView theMstDV = new DataView((DataTable)Session["MasterCustomfrmReg"]);
            theMstDV.RowFilter = "DrugTypeId in (37,36)";
            DataTable theMSTDT = theMstDV.ToTable();

            DataTable dtARV = new DataTable();
            dtARV.Columns.Add("DrugId", System.Type.GetType("System.Int32"));
            dtARV.Columns.Add("GenericId", System.Type.GetType("System.Int32"));
            dtARV.Columns.Add("Dose", System.Type.GetType("System.String"));
            dtARV.Columns.Add("FrequencyId", System.Type.GetType("System.String"));
            dtARV.Columns.Add("Duration", System.Type.GetType("System.Decimal"));
            dtARV.Columns.Add("QtyPrescribed", System.Type.GetType("System.Decimal"));
            dtARV.Columns.Add("QtyDispensed", System.Type.GetType("System.Decimal"));
            dtARV.Columns.Add("ARFinance", System.Type.GetType("System.Int32"));
            dtARV.Columns.Add("DrugType", System.Type.GetType("System.Int32"));
            dtARV.Columns.Add("DrugAbbr", System.Type.GetType("System.String"));
            int DrugId = 0;
            int DrugIdforAbbr = 0;
            int GenericId = 0;
            int Dose = 0;
            int Frequency = 0;
            decimal Duration = 0;
            decimal QtyPrescribed = 0;
            decimal QtyDispensed = 0;
            int ARFinanced = 2;
            //string Abbr = "";
            DataRow theRow;

            foreach (object obj in theContainer.Controls)
            {
                if (obj is AjaxControlToolkit.TabPanel)
                {
                    AjaxControlToolkit.TabPanel tabPanel = (AjaxControlToolkit.TabPanel)obj;
                    foreach (object ctrl in tabPanel.Controls)
                    {
                        if (Convert.ToInt32((((System.Web.UI.Control)(ctrl)).Parent).ID) == TabId)
                        {
                            if (ctrl is Control)
                            {
                                Control c = (Control)ctrl;
                                foreach (object y in c.Controls)
                                {
                                    if (y.GetType() == typeof(System.Web.UI.WebControls.Panel))
                                    {
                                        foreach (Control x in ((Control)y).Controls)
                                        {
                                            if (x.GetType() == typeof(System.Web.UI.WebControls.Label))
                                            {
                                                if (x.ID.StartsWith("ARVdrgNm"))
                                                {
                                                    DrugId = Convert.ToInt32(x.ID.Substring(8));
                                                    GenericId = 0;
                                                }
                                                else if (x.ID.StartsWith("ARVGenericNm"))
                                                {
                                                    GenericId = Convert.ToInt32(x.ID.Substring(12));
                                                    DrugId = 0;
                                                }
                                            }
                                            if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                                            {
                                                if (x.ID.StartsWith("ARVdrgStrength"))
                                                {
                                                    Dose = ((DropDownList)x).Text == "" ? 0 : Convert.ToInt32(((DropDownList)x).Text);
                                                }
                                                else if (x.ID.StartsWith("ARVGenericStrength"))
                                                {
                                                    Dose = ((DropDownList)x).Text == "" ? 0 : Convert.ToInt32(((DropDownList)x).Text);
                                                }

                                                if (x.ID.StartsWith("ARVdrgFrequency"))
                                                {
                                                    Frequency = ((DropDownList)x).Text == "" ? 0 : Convert.ToInt32(((DropDownList)x).Text);
                                                }
                                                else if (x.ID.StartsWith("ARVGenericFrequency"))
                                                {
                                                    Frequency = ((DropDownList)x).Text == "" ? 0 : Convert.ToInt32(((DropDownList)x).Text);
                                                }
                                            }

                                            if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                                            {
                                                if (x.ID.Contains("ARVdrgDuration"))
                                                {
                                                    if (((TextBox)x).Text != "")
                                                    {
                                                        Duration = ((TextBox)x).Text == "" ? 0 : Convert.ToDecimal(((TextBox)x).Text);
                                                    }
                                                }
                                                else if (x.ID.StartsWith("ARVGenericDuration"))
                                                {
                                                    if (((TextBox)x).Text != "")
                                                    {
                                                        Duration = ((TextBox)x).Text == "" ? 0 : Convert.ToDecimal(((TextBox)x).Text);
                                                    }
                                                }

                                                if (x.ID.StartsWith("ARVdrgQtyPrescribed"))
                                                {
                                                    if (((TextBox)x).Text != "")
                                                    {
                                                        QtyPrescribed = ((TextBox)x).Text == "" ? 0 : Convert.ToDecimal(((TextBox)x).Text);
                                                    }
                                                }
                                                else if (x.ID.StartsWith("ARVGenericQtyPrescribed"))
                                                {
                                                    if (((TextBox)x).Text != "")
                                                    {
                                                        QtyPrescribed = ((TextBox)x).Text == "" ? 0 : Convert.ToDecimal(((TextBox)x).Text);
                                                    }
                                                }

                                                if (x.ID.StartsWith("ARVdrgQtyDispensed"))
                                                {
                                                    if (((TextBox)x).Text != "")
                                                    {
                                                        QtyDispensed = ((TextBox)x).Text == "" ? 0 : Convert.ToDecimal(((TextBox)x).Text);
                                                    }
                                                }
                                                else if (x.ID.StartsWith("ARVGenericQtyDispensed"))
                                                {
                                                    if (((TextBox)x).Text != "")
                                                    {
                                                        QtyDispensed = ((TextBox)x).Text == "" ? 0 : Convert.ToDecimal(((TextBox)x).Text);
                                                    }
                                                }
                                            }
                                            if (x.GetType() == typeof(System.Web.UI.WebControls.CheckBox))
                                            {
                                                if (x.ID.StartsWith("ARVDrugFinChk"))
                                                {
                                                    ARFinanced = Convert.ToInt32(((CheckBox)x).Checked);
                                                }
                                                else if (x.ID.StartsWith("ARVGenericFinChk"))
                                                {
                                                    ARFinanced = Convert.ToInt32(((CheckBox)x).Checked);
                                                }
                                            }
                                        }
                                        if ((DrugId != 0 || GenericId != 0) && ARFinanced != 2)
                                        {
                                            if (Dose != 0 || Frequency != 0 || Duration != 0 || QtyPrescribed != 0 || QtyDispensed != 0)
                                            {
                                                DrugIdforAbbr = DrugId == 0 ? GenericId : DrugId;
                                                theMSTDT.Select("DrugId=" + DrugIdforAbbr + "");
                                                DataRow[] filterRows = theMSTDT.Select("DrugId=" + DrugIdforAbbr + "");
                                                theRow = dtARV.NewRow();
                                                theRow["DrugId"] = DrugId;
                                                theRow["GenericId"] = GenericId;
                                                theRow["Dose"] = Dose;
                                                theRow["FrequencyId"] = Frequency;
                                                theRow["Duration"] = Duration;
                                                theRow["QtyPrescribed"] = QtyPrescribed;
                                                theRow["QtyDispensed"] = QtyDispensed;
                                                theRow["ARFinance"] = ARFinanced;
                                                theRow["DrugAbbr"] = filterRows;
                                                dtARV.Rows.Add(theRow);
                                                DrugId = 0;
                                                GenericId = 0;
                                                Dose = 0;
                                                Frequency = 0;
                                                Duration = 0;
                                                QtyPrescribed = 0;
                                                QtyDispensed = 0;
                                                ARFinanced = 0;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return dtARV;
        }

        private DataTable ReadLabTable(Control theContainer, int TabId)
        {
            // This procedure reads the the additional labs on the panel labs  into a datatable
            DataTable dtLabs = new DataTable();
            dtLabs.Columns.Add("LabTestId", System.Type.GetType("System.Int32"));
            dtLabs.Columns.Add("LabParameterId", System.Type.GetType("System.Int32"));
            dtLabs.Columns.Add("LabResult", System.Type.GetType("System.Decimal"));
            dtLabs.Columns.Add("LabResult1", System.Type.GetType("System.String"));
            dtLabs.Columns.Add("LabResultId", System.Type.GetType("System.Int32"));
            dtLabs.Columns.Add("Financed", System.Type.GetType("System.Int32"));
            dtLabs.Columns.Add("UnitId", System.Type.GetType("System.Int32"));

            int theSubTestId = 0;
            int theLabTestId = 0;
            string theResultId = string.Empty;
            int theFinanced = 2;

            DataRow theRow;

            foreach (object obj in theContainer.Controls)
            {
                if (obj is AjaxControlToolkit.TabPanel)
                {
                    AjaxControlToolkit.TabPanel tabPanel = (AjaxControlToolkit.TabPanel)obj;
                    foreach (object ctrl in tabPanel.Controls)
                    {
                        if (Convert.ToInt32((((System.Web.UI.Control)(ctrl)).Parent).ID) == TabId)
                        {
                            if (ctrl is Control)
                            {
                                Control c = (Control)ctrl;
                                foreach (object y in c.Controls)
                                {
                                    if (y.GetType() == typeof(System.Web.UI.WebControls.Panel))
                                    {
                                        foreach (Control x in ((Control)y).Controls)
                                        {
                                            if (x.GetType() == typeof(System.Web.UI.WebControls.Label))
                                            {
                                                if (x.ID.StartsWith("theNameLab"))
                                                {
                                                    theSubTestId = Convert.ToInt32(x.ID.Substring(10));
                                                }
                                            }
                                            if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                                            {
                                                if (x.ID.StartsWith("LabResult"))
                                                {
                                                    theResultId = ((TextBox)x).Text; //Convert.ToInt32(((TextBox)x).Text);
                                                }
                                            }
                                            else if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                                            {
                                                if (x.ID.StartsWith("ddlLabResult"))
                                                {
                                                    theResultId = ((DropDownList)x).SelectedValue;
                                                }
                                            }
                                            if (x.GetType() == typeof(System.Web.UI.WebControls.CheckBox))
                                            {
                                                if (x.ID.StartsWith("FinChkLab"))
                                                {
                                                    theFinanced = Convert.ToInt32(((CheckBox)x).Checked);
                                                }
                                            }

                                            if (x.GetType() == typeof(System.Web.UI.WebControls.Label))
                                            {
                                                if (x.ID.Contains("=") == true)
                                                {
                                                    string[] LabTestId = ((Label)x).ID.Split('=');
                                                    theLabTestId = Convert.ToInt32(LabTestId[1]);
                                                }
                                            }
                                            if (theLabTestId != 0 && theSubTestId != 0 && theResultId != "")
                                            {
                                                theRow = dtLabs.NewRow();
                                                theRow["LabTestId"] = theLabTestId;
                                                theRow["LabParameterId"] = theSubTestId;
                                                //theRow["LabResult"] = 0;
                                                theRow["LabResult"] = 99998888;
                                                theRow["LabResult1"] = theResultId;
                                                theRow["LabResultId"] = 0;
                                                theRow["Financed"] = 0;
                                                dtLabs.Rows.Add(theRow);
                                                theSubTestId = 0;
                                                theLabTestId = 0;
                                                theResultId = string.Empty;
                                                theFinanced = 2;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            /*  foreach (Control y in theContainer.Controls)
              {
                  if (y.GetType() == typeof(System.Web.UI.WebControls.Panel))
                  {
                      foreach (Control x in y.Controls)
                      {
                      }
                  }
              }*/
            return dtLabs;
        }

        private DataTable ReadNonARVMedicationTable(Control theContainer, int TabId)
        {
            DataTable dtNonARV = new DataTable();
            dtNonARV.Columns.Add("DrugId", System.Type.GetType("System.Int32"));
            dtNonARV.Columns.Add("GenericId", System.Type.GetType("System.Int32"));
            dtNonARV.Columns.Add("UnitId", System.Type.GetType("System.Int32"));
            dtNonARV.Columns.Add("FrequencyID", System.Type.GetType("System.Int32"));
            dtNonARV.Columns.Add("SingleDose", System.Type.GetType("System.Decimal"));
            dtNonARV.Columns.Add("Duration", System.Type.GetType("System.Decimal"));
            dtNonARV.Columns.Add("QtyOrdered", System.Type.GetType("System.Decimal"));
            dtNonARV.Columns.Add("QtyDispensed", System.Type.GetType("System.Decimal"));
            dtNonARV.Columns.Add("ARFinance", System.Type.GetType("System.Int32"));
            dtNonARV.Columns.Add("DrugType", System.Type.GetType("System.Int32"));
            int DrugId = 0;
            decimal SingleDose = 0;
            int GenericId = 0;
            int UnitId = 0;
            decimal FrequencyId = 0;
            decimal Duration = 0;
            decimal QtyOrdered = 0;
            decimal QtyDispensed = 0;
            int ARFinanced = 2;
            DataRow theRow;

            foreach (object obj in theContainer.Controls)
            {
                if (obj is AjaxControlToolkit.TabPanel)
                {
                    AjaxControlToolkit.TabPanel tabPanel = (AjaxControlToolkit.TabPanel)obj;
                    foreach (object ctrl in tabPanel.Controls)
                    {
                        if (Convert.ToInt32((((System.Web.UI.Control)(ctrl)).Parent).ID) == TabId)
                        {
                            if (ctrl is Control)
                            {
                                Control c = (Control)ctrl;
                                foreach (object y in c.Controls)
                                {
                                    if (y.GetType() == typeof(System.Web.UI.WebControls.Panel))
                                    {
                                        foreach (Control x in ((Control)y).Controls)
                                        {
                                            if (x.GetType() == typeof(System.Web.UI.WebControls.Label))
                                            {
                                                if (x.ID.StartsWith("DrugNm"))
                                                {
                                                    DrugId = Convert.ToInt32(x.ID.Substring(6));
                                                    GenericId = 0;
                                                }
                                                else if (x.ID.StartsWith("GenericNm"))
                                                {
                                                    GenericId = Convert.ToInt32(x.ID.Substring(9));
                                                    DrugId = 0;
                                                }
                                            }
                                            if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                                            {
                                                if (x.ID.StartsWith("theDoseDrug"))
                                                {
                                                    SingleDose = ((TextBox)x).Text == "" ? 0 : Convert.ToDecimal(((TextBox)x).Text);
                                                }
                                                else if (x.ID.StartsWith("theDoseGeneric"))
                                                {
                                                    SingleDose = ((TextBox)x).Text == "" ? 0 : Convert.ToDecimal(((TextBox)x).Text);
                                                }
                                                if (x.ID.StartsWith("DrugDuration"))
                                                {
                                                    Duration = ((TextBox)x).Text == "" ? 0 : Convert.ToDecimal(((TextBox)x).Text);
                                                }
                                                else if (x.ID.StartsWith("GenericDuration"))
                                                {
                                                    Duration = ((TextBox)x).Text == "" ? 0 : Convert.ToDecimal(((TextBox)x).Text);
                                                }
                                                if (x.ID.StartsWith("drugQtyPrescribed"))
                                                {
                                                    QtyOrdered = ((TextBox)x).Text == "" ? 0 : Convert.ToDecimal(((TextBox)x).Text);
                                                }
                                                else if (x.ID.StartsWith("genericQtyPrescribed"))
                                                {
                                                    QtyOrdered = ((TextBox)x).Text == "" ? 0 : Convert.ToDecimal(((TextBox)x).Text);
                                                }
                                                if (x.ID.StartsWith("drugQtyDispensed"))
                                                {
                                                    QtyDispensed = ((TextBox)x).Text == "" ? 0 : Convert.ToDecimal(((TextBox)x).Text);
                                                }
                                                else if (x.ID.StartsWith("genericQtyDispensed"))
                                                {
                                                    QtyDispensed = ((TextBox)x).Text == "" ? 0 : Convert.ToDecimal(((TextBox)x).Text);
                                                }
                                            }
                                            if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                                            {
                                                if (x.ID.StartsWith("theUnitDrug"))
                                                {
                                                    UnitId = Convert.ToInt32(((DropDownList)x).Text);
                                                }
                                                else if (x.ID.StartsWith("theUnitGeneric"))
                                                {
                                                    UnitId = Convert.ToInt32(((DropDownList)x).Text);
                                                }
                                                if (x.ID.StartsWith("drugFrequency"))
                                                {
                                                    FrequencyId = Convert.ToInt32(((DropDownList)x).Text);
                                                }
                                                else if (x.ID.StartsWith("GenericFrequency"))
                                                {
                                                    FrequencyId = Convert.ToInt32(((DropDownList)x).Text);
                                                }
                                            }
                                            if (x.GetType() == typeof(System.Web.UI.WebControls.CheckBox))
                                            {
                                                if (x.ID.StartsWith("FinChkDrug"))
                                                {
                                                    ARFinanced = Convert.ToInt32(((CheckBox)x).Checked);
                                                }
                                                else if (x.ID.StartsWith("FinChkGeneric"))
                                                {
                                                    ARFinanced = Convert.ToInt32(((CheckBox)x).Checked);
                                                }
                                            }
                                        }

                                        if ((DrugId != 0 || GenericId != 0) && ARFinanced != 2)
                                        {
                                            if (UnitId != 0 || FrequencyId != 0 || SingleDose != 0 || Duration != 0 || QtyOrdered != 0 || QtyDispensed != 0)
                                            {
                                                theRow = dtNonARV.NewRow();
                                                theRow["DrugId"] = DrugId;
                                                theRow["GenericId"] = GenericId;
                                                theRow["UnitId"] = UnitId;
                                                theRow["FrequencyID"] = FrequencyId;
                                                theRow["SingleDose"] = SingleDose;
                                                theRow["Duration"] = Duration;
                                                theRow["QtyOrdered"] = QtyOrdered;
                                                theRow["QtyDispensed"] = QtyDispensed;
                                                theRow["ARFinance"] = ARFinanced;
                                                theRow["DrugType"] = System.DBNull.Value;
                                                dtNonARV.Rows.Add(theRow);
                                                DrugId = 0;
                                                GenericId = 0;
                                                UnitId = 0;
                                                FrequencyId = 0;
                                                SingleDose = 0;
                                                Duration = 0;
                                                QtyOrdered = 0;
                                                QtyDispensed = 0;
                                                ARFinanced = 2;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            //foreach (Control y in theContainer.Controls)
            //{
            //    if (y.GetType() == typeof(System.Web.UI.WebControls.Panel))
            //    {
            //        foreach (Control x in y.Controls)
            //        {
            //        }
            //     }
            //}
            return dtNonARV;
        }

        private void RegimenSessionSetting(int RegimenType, string controlId, string Regimen)
        {
            IQCareUtils theUtils = new IQCareUtils();
            if (Session["Reg" + controlId.ToString() + RegimenType + ""] == null)
            {
                DataView theDV = new DataView((DataTable)Session["MasterCustomfrmReg"]);
                theDV.RowFilter = "DrugTypeId=" + RegimenType + " and Generic<>0";
                DataTable theDT = theUtils.CreateTableFromDataView(theDV);
                Session["Reg" + controlId.ToString() + RegimenType + ""] = theDT;
            }
            if (Session["SelectedReg" + controlId.ToString() + RegimenType + ""] == null)
            {
                //DataView theDV = new DataView((DataTable)Session["MasterCustomfrmReg"]);
                //theDV.RowFilter = "DrugTypeId=" + RegimenType + " and Generic<>0";
                //DataTable theDT = theUtils.CreateTableFromDataView(theDV);
                //Session["Reg" + controlId.ToString() + RegimenType + ""] = theDT;
                //Table for Selected Drugs
                DataTable theSelectedDT = new DataTable();
                theSelectedDT.Columns.Add("DrugId", System.Type.GetType("System.Int32"));
                theSelectedDT.Columns.Add("DrugName", System.Type.GetType("System.String"));
                theSelectedDT.Columns.Add("Generic", System.Type.GetType("System.Int32"));
                theSelectedDT.Columns.Add("DrugTypeID", System.Type.GetType("System.Int32"));
                theSelectedDT.Columns.Add("DrugAbbr", System.Type.GetType("System.String"));
                Session["SelectedReg" + controlId.ToString() + RegimenType + ""] = theSelectedDT;
            }
            DataTable theTmpDT = ((DataTable)Session["Reg" + controlId.ToString() + RegimenType + ""]).Copy();
            string[] ArrRegimen = Regimen.Split('/');
            int colvalue;
            if (RegimenType == 37)
            {
                colvalue = 4;
            }
            else
                colvalue = 1;

            DataTable theDTSelected = (DataTable)Session["SelectedReg" + controlId.ToString() + RegimenType + ""];
            for (int i = 0; i < ArrRegimen.Length; i++)
            {
                foreach (DataRow theDR in theTmpDT.Rows)
                {
                    if (Convert.ToString(theDR[colvalue]) == ArrRegimen[i])
                    {
                        DataRow theTmpDR = theDTSelected.NewRow();
                        theTmpDR[0] = theDR["DrugId"];
                        theTmpDR[1] = theDR["DrugName"];
                        theTmpDR[2] = theDR["Generic"];
                        theTmpDR[3] = theDR["DrugTypeID"];
                        theTmpDR[4] = theDR["Abbr"];
                        theDTSelected.Rows.Add(theTmpDR);
                    }
                }
            }
            Session["SelectedReg" + controlId.ToString() + RegimenType + ""] = theDTSelected;

            //For setting Master Regimen Session
            foreach (DataRow theDR in ((DataTable)Session["Reg" + controlId.ToString() + RegimenType + ""]).Rows)
            {
                foreach (DataRow theDRI in theDTSelected.Rows)
                {
                    if (Convert.ToString(theDR[1]) == Convert.ToString(theDRI[1]))
                    {
                        DataRow[] theDR1 = theTmpDT.Select("DrugId=" + Convert.ToInt32(theDR[0]));
                        theTmpDT.Rows.Remove(theDR1[0]);
                    }
                }
            }
            Session["Reg" + controlId.ToString() + RegimenType + ""] = theTmpDT;
        }

        private string ReturnRegimen(int RegtypeID)
        {
            string theStr = "";
            if (Session["SelectedReg" + RegtypeID + ""] != null)
            {
                DataTable theDT = (DataTable)Session["SelectedReg" + RegtypeID + ""];
                theStr = FillRegimen(theDT);
            }
            return theStr;
        }

        private void SaveCancel()
        {
            AjaxControlToolkit.TabContainer container = (AjaxControlToolkit.TabContainer)tabContainer;
            hdnPrevTabIndex.Value = hdnCurrenTabIndex.Value;
            container.ActiveTabIndex = Convert.ToInt32(hdnCurrenTabIndex.Value);
            //--- For Cancel event, on saving the form ---
            string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
            script += "var ans;\n";
            script += "alert('Data on " + hdnPrevTabName.Value + " saved successfully.');\n";
            script += "</script>\n";
            //ClientScript.RegisterStartupScript(this.GetType(),"confirm", script);

            Page.ClientScript.RegisterStartupScript(this.GetType(), "confirm", script);
            hdnPrevTabName.Value = hdnCurrenTabName.Value;
        }

        private StringBuilder SaveCustomFormData(int PatientID, DataSet DS, int DQSaveChk, int TabId)
        {
            ICustomForm MgrSaveUpdate = (ICustomForm)ObjectFactory.CreateInstance(ObjFactoryParameter);
            AjaxControlToolkit.TabContainer container = (AjaxControlToolkit.TabContainer)tabContainer;
            DataTable theDT = SetControlIDs(container);
            DataView theViewDT = new DataView(theDT);
            theViewDT.RowFilter = "TabId=" + TabId + "";
            theDT = theViewDT.ToTable();
            StringBuilder SbInsert = new StringBuilder();
            string str = "";
            int ICD10Count = 0;
            StringBuilder SbUpdateColMstPatient = new StringBuilder();
            StringBuilder SbUpdateValMstPatient = new StringBuilder();

            if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
            {
                DataView theViewMulti = new DataView(((DataTable)ViewState["LnkTable"]));
                theViewMulti.RowFilter = "TabId=" + TabId + "";
                DataTable theDTMulti = theViewMulti.ToTable();
                DataView theViewother = new DataView(((DataTable)ViewState["NoMulti"]));
                theViewother.RowFilter = "TabId=" + TabId + "";
                DataTable theDTother = theViewother.ToTable();

                #region "Conditional Field Inclusion"

                DataView theConditionalView = new DataView(((DataSet)Session["AllData"]).Tables[17]);
                theConditionalView.RowFilter = "TabId=" + TabId + "";
                DataTable theConditionalDT = theConditionalView.ToTable();

                foreach (DataRow theConDR in theConditionalDT.Rows)
                {
                    DataRow theDTMultiDR = theDTMulti.NewRow();
                    theDTMultiDR["FeatureId"] = theConDR["FeatureId"];
                    theDTMultiDR["FeatureName"] = theConDR["FeatureName"];
                    theDTMultiDR["SectionId"] = theConDR["FieldSectionId"];
                    theDTMultiDR["SectionName"] = theConDR["FieldSectionName"];
                    theDTMultiDR["FieldId"] = theConDR["FieldId"];
                    theDTMultiDR["FieldName"] = theConDR["FieldName"];
                    theDTMultiDR["FieldLabel"] = theConDR["FieldLabel"];
                    theDTMultiDR["Predefined"] = theConDR["Predefined"];
                    theDTMultiDR["PDFTableName"] = theConDR["PDFTableName"];
                    theDTMultiDR["ControlId"] = theConDR["ControlId"];
                    theDTMultiDR["BindSource"] = theConDR["BindSource"];
                    theDTMultiDR["CodeId"] = theConDR["CodeId"];
                    theDTMultiDR["Seq"] = theConDR["Seq"];
                    theDTMultiDR["SeqSection"] = theConDR["SeqSection"];
                    theDTMultiDR["TabId"] = theConDR["TabId"];
                    theDTMultiDR["TabName"] = theConDR["TabName"];
                    theDTMulti.Rows.Add(theDTMultiDR);

                    if (theConDR["ControlId"].ToString() != "9")
                    {
                        DataRow theDTOtherDR = theDTother.NewRow();
                        theDTOtherDR["FeatureId"] = theConDR["FeatureId"];
                        theDTOtherDR["FeatureName"] = theConDR["FeatureName"];
                        theDTOtherDR["SectionId"] = theConDR["FieldSectionId"];
                        theDTOtherDR["SectionName"] = theConDR["FieldSectionName"];
                        theDTOtherDR["FieldId"] = theConDR["FieldId"];
                        theDTOtherDR["FieldName"] = theConDR["FieldName"];
                        theDTOtherDR["FieldLabel"] = theConDR["FieldLabel"];
                        theDTOtherDR["Predefined"] = theConDR["Predefined"];
                        theDTOtherDR["PDFTableName"] = theConDR["PDFTableName"];
                        theDTOtherDR["ControlId"] = theConDR["ControlId"];
                        theDTOtherDR["BindSource"] = theConDR["BindSource"];
                        theDTOtherDR["CodeId"] = theConDR["CodeId"];
                        theDTOtherDR["Seq"] = theConDR["Seq"];
                        theDTOtherDR["TabId"] = theConDR["TabId"];
                        theDTOtherDR["TabName"] = theConDR["TabName"];
                        theDTother.Rows.Add(theDTOtherDR);
                    }
                }

                #endregion "Conditional Field Inclusion"

                DataTable LnkDTUnique = theDTother.DefaultView.ToTable(true, "PDFTableName", "FeatureName").Copy();
                //To Add Signature Dropdown for Each tab
                DataTable theDTORDVisit = ((DataSet)Session["AllData"]).Tables[23];
                String Signature = "0";
                if (Convert.ToInt32(theDTORDVisit.Rows[0]["Signature"]) == 1)
                {
                    LnkDTUnique.Rows.Add("LNK_FORMTABORDVISIT", "" + theHeader.InnerText + "");
                }
                if (Convert.ToInt32(theDTORDVisit.Rows[0]["Signature"]) == 0)
                {
                    Signature = ddSignature.SelectedValue;
                }

                string GetValue = "";
                GetValue = "Select VisitTypeID from mst_visittype where (DeleteFlag = 0 or DeleteFlag is null) and VisitTypeId>12 and VisitName='" + theHeader.InnerText + "'";
                DataSet TempDS = MgrSaveUpdate.Common_GetSaveUpdate(GetValue);

                SbInsert.Append("Insert into [ord_visit](ptn_pk, LocationID, VisitDate, VisitType, DataQuality, UserID, Signature, CreateDate)");

                // todo
                if (IsSIngleVisit == true)
                {
                    string theRegDate = ((DateTime)((DataSet)Session["AllData"]).Tables[18].Rows[0]["StartDate"]).ToString(Session["AppDateFormat"].ToString());
                    SbInsert.Append("Values(" + PatientID + "," + Session["AppLocationId"] + ",'" + txtvisitDate.Text + "'," + TempDS.Tables[0].Rows[0]["VisitTypeID"].ToString());
                    SbInsert.Append(",'" + DQSaveChk.ToString() + "'," + Session["AppUserId"] + ", " + Signature + ", GetDate())");
                }
                else
                {
                    SbInsert.Append("Values(" + PatientID + "," + Session["AppLocationId"] + ", '" + txtvisitDate.Text + "', " + TempDS.Tables[0].Rows[0]["VisitTypeID"].ToString());
                    SbInsert.Append(",'" + DQSaveChk.ToString() + "'," + Session["AppUserId"] + ", " + Signature + ", GetDate())");
                }
                SbInsert.Append("declare @thisVisitId int; Select @thisVisitId = SCOPE_IDENTITY();");
                string theRegVisitDate;

                // todo
                if (IsSIngleVisit == true)
                {
                    theRegVisitDate = ((DateTime)((DataSet)Session["AllData"]).Tables[18].Rows[0]["StartDate"]).ToString(Session["AppDateFormat"].ToString());
                }
                else
                {
                    theRegVisitDate = txtvisitDate.Text;
                }
                SbInsert.Append("INSERT INTO Dtl_PatientBillTransaction(BillId,Ptn_Pk,VisitId,LocationId,TransactionDate,LabId,PharmacyId,");
                SbInsert.Append("ItemId,BatchId,DispensingUnit,Quantity,SellingPrice,CostPrice,Margin,ConsultancyFee,AdminFee,BillAmount,DoctorId,UserId,CreateDate)");
                SbInsert.Append("VALUES(0," + PatientID + ",@thisVisitId ," + Session["AppLocationId"] + ",'" + theRegVisitDate + "',0,0,0,0,0,1,0,0,0,dbo.fn_GetConsultationPerVisit_Futures('" + theRegVisitDate + "'),");
                SbInsert.Append("dbo.fn_GetOverHeadPerVisit_Futures('" + theRegVisitDate + "'),dbo.fn_GetConsultationPerVisit_BillAmount_Futures('" + theRegVisitDate + "')+ dbo.fn_GetOverHeadPerVisit_BillAmount_Futures('" + theRegVisitDate + "'),");
                SbInsert.Append("" + Signature + "," + Session["AppUserId"] + ", getdate())");
                //Generating Query for MultiSelect
                foreach (DataRow DRMultiSelect in theDTMulti.Rows)
                {
                    if (DRMultiSelect["ControlID"].ToString() == "9" || DRMultiSelect["ControlID"].ToString() == "15")
                    {
                        StringBuilder InsertMultiselect = InsertMultiSelectList(PatientID, DRMultiSelect["FieldName"].ToString(), Convert.ToInt32(DRMultiSelect["FeatureID"].ToString()),
                            DRMultiSelect["PDFTableName"].ToString(), Convert.ToInt32(DRMultiSelect["ControlID"]), Convert.ToInt32(DRMultiSelect["FieldId"]), TabId);
                        if (SbInsert[0].ToString().Contains(DRMultiSelect["PDFTableName"].ToString()) == false)
                            SbInsert.Append(InsertMultiselect);
                    }
                    else if (DRMultiSelect["ControlID"].ToString() == "16" && ICD10Count == 0)
                    {
                        int Setflag = 0;
                        StringBuilder SbInsertICD10 = new StringBuilder();
                        StringBuilder SbValuesICD10 = new StringBuilder();
                        foreach (DataRow theICD10Row in theDT.Rows)
                        {
                            if (theICD10Row[3].ToString().Contains("dtl_ICD10Field") && theICD10Row[0].ToString().Contains("%") && theICD10Row[2].ToString().Contains("1") && Setflag == 0)
                            {
                                string[] strICD10 = theICD10Row[0].ToString().Split('%');
                                string FieldId = strICD10[0].StartsWith("8888") ? strICD10[0].Replace("8888", "") : strICD10[0].Replace("9999", "");
                                SbInsertICD10.Append("Insert into [dtl_ICD10Field](ptn_pk, Visit_Pk, LocationID, FieldId, BlockId,SubBlockId,ICDCodeId, Predefined, UserID,CreateDate,TabId,");
                                SbValuesICD10.Append("Values(" + PatientID + ",@thisVisitId ," + Session["AppLocationId"] + "," + FieldId + ", " + strICD10[1] + ", " + strICD10[2] + ", " + strICD10[3] + "," + strICD10[4] + "," + Session["AppUserId"] + ", GetDate()," + TabId + ",");
                                Setflag = 1;
                            }
                            else if (theICD10Row[3].ToString().Contains("dtl_ICD10Field") && theICD10Row[0].ToString().Contains("OnSetDate") && Setflag == 1)
                            {
                                SbInsertICD10.Append("DateOnSet,");
                                SbValuesICD10.Append("'" + theICD10Row[2] + "',");
                            }
                            else if (theICD10Row[3].ToString().Contains("dtl_ICD10Field") && theICD10Row[0].ToString().Contains("Comment") && Setflag == 1)
                            {
                                SbInsertICD10.Append("Comments)");
                                SbValuesICD10.Append("'" + theICD10Row[2] + "')");
                                Setflag = 0;
                                SbInsertICD10.Append(SbValuesICD10);
                                SbValuesICD10 = new StringBuilder();
                            }
                        }
                        SbInsert.Append(SbInsertICD10);
                        ICD10Count = 1;
                    }
                }
                //
                foreach (DataRow DR in LnkDTUnique.Rows)
                {
                    string quotes = "''''";
                    StringBuilder SbValues = new StringBuilder();
                    if (DR[0].ToString() == "DTL_CUSTOMFIELD")
                    {
                        string TableName = "DTL_FBCUSTOMFIELD_" + DR[1].ToString().Replace(' ', '_');
                        SbInsert.Append("Insert into [" + TableName + "](Ptn_pk,Visit_Pk,LocationId,UserID,CreateDate,");
                        SbValues.Append("Values(" + PatientID + ",@thisVisitId ," + Session["AppLocationId"] + "," + Session["AppUserId"] + ", GetDate(),");
                    }
                    else if (DR[0].ToString().ToUpper() == "DTL_CUSTOMFORM")
                    {
                    }
                    else if (DR[0].ToString().ToUpper() == "BMI")
                    {
                    }
                    else if (DR[0] != System.DBNull.Value)
                    {
                        if (Convert.ToString(DR[0]) == "dtl_PatientCareEnded".ToUpper())
                        {
                            SbInsert.Append("Insert into [" + DR[0] + "](Ptn_pk,LocationId,UserID,CreateDate,");
                            SbValues.Append("Values(" + PatientID + "," + Session["AppLocationId"] + "," + Session["AppUserId"] + ", GetDate(),");
                        }
                        else if (Convert.ToString(DR[0]) == "dtl_PatientARVInfo".ToUpper() || Convert.ToString(DR[0]) == "dtl_PatientContacts".ToUpper())
                        {
                            SbInsert.Append("Insert into [" + DR[0] + "](Ptn_pk,Visitid,LocationId,UserID,CreateDate,");
                            SbValues.Append("Values(" + PatientID + ",@thisVisitId ," + Session["AppLocationId"] + "," + Session["AppUserId"] + ", GetDate(),");
                        }
                        else if (Convert.ToString(DR[0]) == "mst_patient".ToUpper())
                        {
                            str = "mst_patient";
                            SbUpdateColMstPatient.Append("Update [" + DR[0] + "] Set ");
                        }
                        else if (Convert.ToString(DR[0]) == "LNK_FORMTABORDVISIT")
                        {
                            SbInsert.Append("Insert into [" + DR[0] + "](Visit_pk, DataQuality, TabId, UserID,CreateDate,Signature,");
                            SbValues.Append("Values(@thisVisitId , " + DQSaveChk + ", " + TabId + "," + Session["AppUserId"] + ", GetDate(),");
                        }
                        else if (Convert.ToString(DR[0]) == "dtl_ICD10Field".ToUpper())
                        {
                        }
                        else
                        {
                            SbInsert.Append("Insert into [" + DR[0] + "](Ptn_pk,Visit_Pk,LocationId,UserID,CreateDate,");
                            SbValues.Append("Values(" + PatientID + ",@thisVisitId ," + Session["AppLocationId"] + "," + Session["AppUserId"] + ", GetDate(),");
                        }
                    }
                    //Generating Query to Insert values other than MultiSelect
                    foreach (DataRow DRlnk in theDT.Rows)
                    {
                        if (DR["PDFTableName"].ToString().ToUpper() == DRlnk["TableName"].ToString().ToUpper() && Convert.ToInt32(DRlnk["TabId"]) == TabId)
                        {
                            if (Convert.ToString(DR["PDFTableName"]) == "mst_patient".ToUpper())
                            {
                                SbUpdateColMstPatient.Append("[" + DRlnk["Column"] + "]=");
                                SbUpdateColMstPatient.Append("'" + apostropheHandler(DRlnk["Value"].ToString()) + "',");
                            }
                            else if (Convert.ToString(DR["PDFTableName"].ToString().ToUpper()) == "DTL_CUSTOMFORM")
                            {
                            }
                            else if (Convert.ToString(DR["PDFTableName"].ToString()) == "dtl_ICD10Field")
                            {
                            }
                            else if (Convert.ToString(DR["PDFTableName"].ToString()) == "LNK_FORMTABORDVISIT")
                            {
                                SbValues.Append("'" + apostropheHandler(DRlnk["Value"].ToString()) + "',");
                                Signature = DRlnk["Value"].ToString();
                            }
                            else if (Convert.ToString(DR["PDFTableName"].ToString()) == "dtl_Adherence_Missed_Reason".ToUpper())
                            {
                                if (DRlnk["Value"].ToString() == "")
                                {
                                }
                                else
                                {
                                    SbInsert.Append("[" + DRlnk["Column"] + "],");
                                    SbValues.Append("'" + apostropheHandler(DRlnk["Value"].ToString()) + "',");
                                }
                            }
                            else
                            {
                                if (DRlnk["Value"].ToString() != "")
                                {
                                    SbInsert.Append("[" + DRlnk["Column"] + "],");
                                    if (DRlnk["Value"].ToString() == "")
                                    {
                                        SbValues.Append(DRlnk["Value"] + "" + quotes + "" + ",");
                                    }
                                    else
                                    {
                                        SbValues.Append("'" + apostropheHandler(DRlnk["Value"].ToString()) + "',");
                                    }
                                }
                            }
                        }
                    }
                    SbInsert.Remove(SbInsert.Length - 1, 1);
                    if (Convert.ToString(DR[0]) != "mst_patient".ToUpper())
                    {
                        SbInsert.Append(" )");
                    }
                    if (DR[0] != System.DBNull.Value)
                    {
                        if (Convert.ToString(DR[0]) != "mst_patient".ToUpper())
                        {
                            if (SbValues.Length > 0)
                            {
                                SbValues.Remove(SbValues.Length - 1, 1);
                                SbValues.Append(" )");
                            }
                        }
                        else
                        {
                            SbValues.Append(" )");
                            SbUpdateColMstPatient.Remove(SbUpdateColMstPatient.Length - 1, 1);
                        }
                    }
                    SbInsert.Append(SbValues);
                    TempDS.Dispose();
                }

                if (str == "mst_patient".ToUpper())
                {
                    SbUpdateValMstPatient.Append(" where Ptn_pk=" + PatientID + " and LocationID=" + Session["AppLocationId"] + " ");
                }

                SbInsert.Append(SbUpdateColMstPatient);
                SbInsert.Append(SbUpdateValMstPatient);
                SbInsert.Append("Select LocationID, @thisVisitId [VisitID] from ord_visit where Visit_ID=@thisVisitId ");
                if (DS.Tables[1].Rows.Count > 0 || DS.Tables[2].Rows.Count > 0)
                {
                    string orderstatus = string.Empty;
                    if (Session["SCMModule"] != null)
                    {
                        orderstatus = "1";
                    }
                    SbInsert.Append("Insert into [ord_patientpharmacyorder](ptn_pk, VisitID, LocationID, OrderedBy, OrderedByDate, UserID, Signature, CreateDate,orderstatus)");
                    SbInsert.Append("Values(" + PatientID + ",@thisVisitId ," + Session["AppLocationId"] + "," + ddSignature.SelectedValue + ", '" + txtvisitDate.Text + "',");
                    SbInsert.Append("" + Session["AppUserId"] + "," + Signature + ", getdate(),'" + orderstatus + "')");
                    SbInsert.Append("Select LocationID, ptn_pharmacy_pk[PharmacyID], UserID from ord_PatientPharmacyOrder where VisitID=@thisVisitId ");
                }
                else { SbInsert.Append("Select '00000'[PharmacyID]"); };

                #region Insert GridView

                //DataTable lnkSection = ((DataTable)ViewState["LnkTable"]).DefaultView.ToTable(true,"FeatureID", "SectionID", "SectionName", "IsGridView","FeatureName").Copy();
                DataTable lnkSection = theDTMulti.DefaultView.ToTable(true, "FeatureID", "SectionID", "SectionName", "IsGridView", "FeatureName").Copy();
                DataView theDVSectionGridView = new DataView(lnkSection);
                theDVSectionGridView.RowFilter = "IsGridView= 1";
                if (theDVSectionGridView.Count > 0)
                {
                    StringBuilder sbInsertGridView = new StringBuilder();
                    foreach (DataRow DRGridView in theDVSectionGridView.ToTable().Rows)
                    {
                        sbInsertGridView.Append(InsertGridView(PatientID, Convert.ToInt32(DRGridView["FeatureID"].ToString()), Convert.ToInt32(DRGridView["SectionID"]), DRGridView["SectionName"].ToString(), 0, DRGridView["FeatureName"].ToString()));
                        sbInsertGridView.Append(";");
                    }
                    SbInsert.Append(sbInsertGridView);
                }
                # endregion

                if (DS.Tables[0].Rows.Count > 0)
                {
                    SbInsert.Append("Insert into [ord_PatientLabOrder](ptn_pk, VisitID, LocationID, OrderedbyName, OrderedbyDate, ReportedbyName, ReportedbyDate, UserID, CreateDate)");
                    SbInsert.Append("Values(" + PatientID + ",@thisVisitId ," + Session["AppLocationId"] + "," + ddSignature.SelectedValue + ", '" + txtvisitDate.Text + "',");
                    SbInsert.Append("" + Signature + ", '" + txtvisitDate.Text + "'," + Session["AppUserId"] + ", getdate())");
                    SbInsert.Append("Select LocationID, LabID[LabID],UserID from ord_PatientLabOrder where VisitID=@thisVisitId ");
                }
                else { SbInsert.Append("Select '00000'[LabID]"); }
            }
            return SbInsert;
        }

        private void SectionHeading(String H2)
        {
            DIVCustomItem.Controls.Add(new LiteralControl("<h2 class='forms' align='left'>" + H2 + "</h2>"));
        }

        private void SectionHeading(String H2, int i)
        {
            DIVCustomItem.Controls.Add(new LiteralControl("<h2 class='forms' align='left'>" + H2 + "</h2>"));
            tabContainer.Tabs[i].Controls.Add(DIVCustomItem);
        }

        private Boolean SetBusinessrule(string FieldID, string FieldLabel)
        {
            DataTable theDT = (DataTable)ViewState["BusRule"];
            foreach (DataRow DR in theDT.Rows)
            {
                if (Convert.ToString(DR["FieldID"]) == FieldID && Convert.ToString(DR["FieldName"]) == FieldLabel && Convert.ToString(DR["BusRuleId"]) == "1")
                {
                    return true;
                }
            }
            return false;
        }

        private DataTable SetControlIDs(Control theControl)
        {
            DataTable TempDT = new DataTable();
            TempDT.Columns.Add("Column", System.Type.GetType("System.String"));
            TempDT.Columns.Add("FieldId", System.Type.GetType("System.String"));
            TempDT.Columns.Add("Value", System.Type.GetType("System.String"));
            TempDT.Columns.Add("ValueText", System.Type.GetType("System.String"));
            TempDT.Columns.Add("TableName", System.Type.GetType("System.String"));
            TempDT.Columns.Add("TabId", System.Type.GetType("System.String"));
            DataRow DRTemp;
            DRTemp = TempDT.NewRow();
            String Time24 = "", Time12 = "", TimeAMPM = "";
            foreach (object obj in theControl.Controls)
            {
                if (obj is AjaxControlToolkit.TabPanel)
                {
                    AjaxControlToolkit.TabPanel tabPanel = (AjaxControlToolkit.TabPanel)obj;
                    foreach (object ctrl in tabPanel.Controls)
                    {
                        if (ctrl is Control)
                        {
                            Control c = (Control)ctrl;
                            foreach (object x in c.Controls)
                            {
                                if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                                {
                                    string[] str = ((TextBox)x).ID.Split('-');
                                    if (str[2] != "BMI")
                                    {
                                        DRTemp = TempDT.NewRow();

                                        DRTemp["Column"] = str[1];

                                        if (((TextBox)x).Enabled == true)
                                        {
                                            if (str[0].ToString() == "TXTDT")
                                            {
                                                if (((TextBox)x).Text.Length > 8)
                                                {
                                                    DRTemp["Value"] = ((TextBox)x).Text;
                                                }
                                                else
                                                {
                                                    if (((TextBox)x).Text != "")
                                                    {
                                                        DRTemp["Value"] = "01-" + ((TextBox)x).Text;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (((TextBox)x).Text != "")
                                                {
                                                    DRTemp["Value"] = ((TextBox)x).Text;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            DRTemp["Value"] = "";
                                        }

                                        DRTemp["TableName"] = str[2];
                                        DRTemp["FieldID"] = str[3];
                                        DRTemp["TabId"] = str[4];
                                        TempDT.Rows.Add(DRTemp);
                                    }
                                }
                                if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputRadioButton))
                                {
                                    DRTemp = TempDT.NewRow();
                                    string[] str = ((HtmlInputRadioButton)x).ID.Split('-');
                                    if (((HtmlInputRadioButton)x).ID == "RADIO1-" + str[1] + "-" + str[2] + "-" + str[3] + "-" + str[4])
                                    {
                                        if (((HtmlInputRadioButton)x).Checked == true)
                                        {
                                            DRTemp["Column"] = str[1];
                                            if (((HtmlInputRadioButton)x).Visible == true)
                                                DRTemp["Value"] = "1";
                                            else
                                                DRTemp["Value"] = "";
                                        }
                                    }
                                    else if (((HtmlInputRadioButton)x).ID == "RADIO2-" + str[1] + "-" + str[2] + "-" + str[3] + "-" + str[4])
                                    {
                                        if (((HtmlInputRadioButton)x).Checked == true)
                                        {
                                            DRTemp["Column"] = str[1];
                                            if (((HtmlInputRadioButton)x).Visible == true)
                                                DRTemp["Value"] = "0";
                                            else
                                                DRTemp["Value"] = "";
                                        }
                                    }

                                    DRTemp["TableName"] = str[2];
                                    DRTemp["FieldID"] = str[3];
                                    DRTemp["TabId"] = str[4];
                                    TempDT.Rows.Add(DRTemp);
                                }
                                if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                                {
                                    DRTemp = TempDT.NewRow();
                                    string[] str = ((DropDownList)x).ID.Split('-');
                                    if (str[3].Contains("12Hr"))
                                    {
                                        Time12 = ((DropDownList)x).SelectedValue;
                                    }
                                    else if (str[3].Contains("24Hr"))
                                    {
                                        Time24 = ((DropDownList)x).SelectedValue;
                                    }
                                    else if (str[3].Contains("Min") && Time12 != "")
                                    {
                                        TimeAMPM = Time12 + ":" + ((DropDownList)x).SelectedValue;
                                        Time12 = "";
                                    }
                                    else if (str[3].Contains("Min") && Time24 != "")
                                    {
                                        Time24 = Time24 + ":" + ((DropDownList)x).SelectedValue;
                                        DRTemp["Column"] = str[1];
                                        DRTemp["Value"] = Time24;
                                        DRTemp["TableName"] = str[2];
                                        DRTemp["FieldID"] = str[3];
                                        DRTemp["TabId"] = str[4];
                                        TempDT.Rows.Add(DRTemp);
                                        Time24 = "";
                                    }
                                    else if (str[3].Contains("AMPM"))
                                    {
                                        TimeAMPM = TimeAMPM + " " + ((DropDownList)x).SelectedValue;
                                        DRTemp["Column"] = str[1];
                                        DRTemp["Value"] = TimeAMPM;
                                        DRTemp["TableName"] = str[2];
                                        DRTemp["FieldID"] = str[3];
                                        DRTemp["TabId"] = str[4];
                                        TempDT.Rows.Add(DRTemp);
                                        TimeAMPM = "";
                                    }
                                    else
                                    {
                                        if (str[0] != "SELECTLISTAuto")
                                        {
                                            DRTemp["Column"] = str[1];
                                            if (((DropDownList)x).Enabled == true)
                                                DRTemp["Value"] = ((DropDownList)x).SelectedValue;
                                            else
                                                DRTemp["Value"] = "";
                                            DRTemp["TableName"] = str[2];
                                            DRTemp["FieldID"] = str[3];
                                            DRTemp["TabId"] = str[4];
                                            TempDT.Rows.Add(DRTemp);
                                        }
                                    }
                                }
                                if (x.GetType() == typeof(System.Web.UI.WebControls.CheckBox))
                                {
                                    DRTemp = TempDT.NewRow();
                                    string[] str = ((CheckBox)x).ID.Split('-');
                                    DRTemp["Column"] = str[1];
                                    if (((CheckBox)x).Visible == true)
                                    {
                                        if (((CheckBox)x).Checked == true)
                                        {
                                            DRTemp["Value"] = 1;
                                        }
                                        else
                                        {
                                            DRTemp["Value"] = "";
                                        }
                                    }
                                    else
                                    {
                                        DRTemp["Value"] = "";
                                    }
                                    DRTemp["TableName"] = str[2];
                                    DRTemp["FieldID"] = str[3];
                                    DRTemp["TabId"] = str[4];
                                    TempDT.Rows.Add(DRTemp);
                                }

                                if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputCheckBox))
                                {
                                    DRTemp = TempDT.NewRow();
                                    string[] str = ((HtmlInputCheckBox)x).ID.Split('-');
                                    DRTemp["Column"] = str[1];
                                    if (((HtmlInputCheckBox)x).Visible == true)
                                    {
                                        if (((HtmlInputCheckBox)x).Checked == true)
                                        {
                                            DRTemp["Value"] = 1;
                                        }
                                        else
                                        {
                                            DRTemp["Value"] = 0;
                                        }
                                    }
                                    else
                                    {
                                        DRTemp["Value"] = "";
                                    }
                                    DRTemp["TableName"] = str[2];
                                    DRTemp["FieldID"] = str[3];
                                    DRTemp["TabId"] = str[4];
                                    TempDT.Rows.Add(DRTemp);
                                }
                                if (x.GetType() == typeof(IQCare.IQControl.IQLookupTextBox))
                                {
                                    DRTemp = TempDT.NewRow();
                                    IQCare.IQControl.IQLookupTextBox iq = (IQCare.IQControl.IQLookupTextBox)x;
                                    string[] str = iq.ID.Split('-');
                                    string strValueName = iq.ValueText;
                                    string strValueId = iq.SelectedValue;
                                    DRTemp["Column"] = str[1];
                                    DRTemp["Value"] = strValueId;
                                    DRTemp["ValueText"] = strValueName;                                    
                                    DRTemp["TableName"] = str[2];
                                    DRTemp["FieldID"] = str[3];
                                    DRTemp["TabId"] = str[4];
                                    TempDT.Rows.Add(DRTemp);
                                }
                            }
                        }
                    }
                }
            }
            return TempDT;
        }

        private string setFormating(DataColumn bc)
        {
            string dataType = null;
            switch (bc.DataType.ToString())
            {
                case "System.Int32":
                    dataType = "";
                    break;

                case "System.Decimal":
                    dataType = "{0:#.##}";
                    break;

                case "System.DateTime":
                    dataType = "{0:dd-MMM-yyyy}";
                    break;

                case "System.String":
                    dataType = "";
                    break;

                default:
                    dataType = "";
                    break;
            }
            return dataType;
        }

        private void setGridViewSectionControl(Control theControl, DataTable dt, int index, string columnName, string dtcol)
        {
            // string ret = string.Empty;
            foreach (object obj in theControl.Controls)
            {
                if (obj is AjaxControlToolkit.TabPanel)
                {
                    AjaxControlToolkit.TabPanel tabPanel = (AjaxControlToolkit.TabPanel)obj;
                    foreach (object ctrl in tabPanel.Controls)
                    {
                        if (ctrl is Control)
                        {
                            Control c = (Control)ctrl;
                            foreach (object x in c.Controls)
                            {
                                if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                                {
                                    if (((TextBox)x).ID.Contains("DTL_CUSTOMFORM"))
                                    {
                                        {
                                            if (((TextBox)x).ID.Contains("-" + columnName + "-"))
                                            {
                                                if (dt.Columns[dtcol].DataType.ToString() == "System.DateTime")
                                                {
                                                    if (dt.Rows[index][dtcol] != DBNull.Value)
                                                        ((TextBox)x).Text = Convert.ToDateTime(dt.Rows[index][dtcol]).ToString("dd-MMM-yyyy");
                                                }
                                                else
                                                {
                                                    ((TextBox)x).Text = dt.Rows[index][dtcol].ToString();
                                                    if (!String.IsNullOrEmpty(Convert.ToString(dt.Rows[index][dtcol])))
                                                    {
                                                        string[] regimen;
                                                        string[] controlid;
                                                        if (((TextBox)x).ID.Contains("TXTReg-"))
                                                        {
                                                            regimen = ((TextBox)x).ID.Split('=');
                                                            controlid = regimen[0].Split('-');
                                                            if ((regimen.Length > 1) && (controlid.Length > 2))
                                                            {
                                                                string sessionname = "SelectedReg" + controlid[3].ToString() + regimen[1].ToString() + "";
                                                                CustomFormAddRegimen(Convert.ToInt32(regimen[1].ToString()), dt.Rows[index][dtcol].ToString(), sessionname, controlid[3].ToString());
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputRadioButton))
                                {
                                    if (((HtmlInputRadioButton)x).ID.Contains("DTL_CUSTOMFORM"))
                                    {
                                        if (((HtmlInputRadioButton)x).ID.Contains("RADIO1-"))
                                        {
                                            if (((HtmlInputRadioButton)x).ID.Contains("-" + columnName + "-"))
                                            {
                                                if (dt.Rows[index][dtcol].ToString() != "")
                                                {
                                                    if (dt.Rows[index][dtcol].ToString().ToUpper() == "TRUE")
                                                    {
                                                        ((HtmlInputRadioButton)x).Checked = true;
                                                    }
                                                }
                                            }
                                        }

                                        if (((HtmlInputRadioButton)x).ID.Contains("RADIO2-"))
                                        {
                                            if (((HtmlInputRadioButton)x).ID.Contains("-" + columnName + "-"))
                                            {
                                                if (dt.Rows[index][dtcol].ToString() != "")
                                                {
                                                    if (dt.Rows[index][dtcol].ToString().ToUpper() == "FALSE")
                                                    {
                                                        ((HtmlInputRadioButton)x).Checked = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                                {
                                    if (((DropDownList)x).ID.Contains("DTL_CUSTOMFORM"))
                                    {
                                        if (((DropDownList)x).ID.Contains("-" + columnName + "-"))
                                        {
                                            ((DropDownList)x).SelectedIndex = ((DropDownList)x).Items.IndexOf(((DropDownList)x).Items.FindByText(dt.Rows[index][dtcol].ToString()));
                                        }
                                    }
                                }

                                if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputCheckBox))
                                {
                                    if (((HtmlInputCheckBox)x).ID.Contains("DTL_CUSTOMFORM"))
                                    {
                                        if (((HtmlInputCheckBox)x).ID.Contains("-" + columnName + "-"))
                                        {
                                            if (dt.Rows[index][dtcol].ToString() != "")
                                            {
                                                ((HtmlInputCheckBox)x).Checked = Convert.ToBoolean(dt.Rows[index][dtcol].ToString());
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void theBtnAdditionalLab_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Handles the Click event of the theBtnDrugSelection control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void theBtnDrugSelection_Click(object sender, EventArgs e)
        {
            string theScript;

            Application.Add("MasterData", Session["MasterDrugTable"]);
            Application.Add("SelectedDrug", (DataTable)Session["AddARV"]);
            theScript = "<script language='javascript' id='DrgPopup'>\n";
            theScript += "window.open('frmDrugSelector.aspx?DrugType=37','DrugSelection','toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=no,resize=no,width=700,height=350,scrollbars=yes');\n";
            theScript += "</script>\n";
            //Page.ClientScript.RegisterStartupScript(this.GetType(),"DrgPopup", theScript);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "DrgPopup", theScript);
        }

        private void theBtnRegimen_Click(object sender, EventArgs e)
        {
            string theScript;
            Session.Add("MasterData", ViewState["MasterData"]);
            Session.Add("SelectedDrug", (DataTable)ViewState["SelectedData"]);
            ViewState.Remove("MasterData");
            theScript = "<script language='javascript' id='DrgPopup'>\n";
            theScript += "window.open('../Pharmacy/frmDrugSelector.aspx?DrugType=37&btnreg=btncustomReg' ,'DrugSelection','toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=no,resize=no,width=700,height=350,scrollbars=yes');\n";
            theScript += "</script>\n";
            //Page.ClientScript.RegisterStartupScript(this.GetType(),"DrgPopup", theScript);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "DrgPopup", theScript);
        }

        private void theButton_Click(object sender, EventArgs e)
        {
            Button theButton = ((Button)sender);

            string[] ID = theButton.ID.Split('-');
            String HrID = "", Min = "", AMPMID = "";
            if (theButton.ID.Contains("12AMPM"))
            {
                HrID = theButton.ID.Replace("" + ID[3] + "", "" + ID[3] + "12Hr").Replace("theBtn12AMPM", "SELECTLIST");
                Min = theButton.ID.Replace("" + ID[3] + "", "" + ID[3] + "Min").Replace("theBtn12AMPM", "SELECTLIST");
                AMPMID = theButton.ID.Replace("" + ID[3] + "", "" + ID[3] + "AMPM").Replace("theBtn12AMPM", "SELECTLIST");
            }
            else
            {
                HrID = theButton.ID.Replace("" + ID[3] + "", "" + ID[3] + "24Hr").Replace("theBtn", "SELECTLIST");
                Min = theButton.ID.Replace("" + ID[3] + "", "" + ID[3] + "Min").Replace("theBtn", "SELECTLIST");
            }
            ICustomForm MgrTime = (ICustomForm)ObjectFactory.CreateInstance(ObjFactoryParameter);
            String[] Time;
            if (theButton.ID.Contains("12AMPM"))
            {
                Time = MgrTime.GetSystemTime(12).Replace(" ", ":").Split(':');
            }
            else
            {
                Time = MgrTime.GetSystemTime(24).Split(':');
            }
            foreach (object obj in tabContainer.Controls)
            {
                if (obj is AjaxControlToolkit.TabPanel)
                {
                    AjaxControlToolkit.TabPanel tabPanel = (AjaxControlToolkit.TabPanel)obj;
                    foreach (object ctrl in tabPanel.Controls)
                    {
                        if (ctrl is Control)
                        {
                            Control c = (Control)ctrl;
                            foreach (object x in c.Controls)
                            {
                                if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                                {
                                    if (((DropDownList)x).ID == HrID && ((DropDownList)x).ID.Contains("12Hr"))
                                    {
                                        ((DropDownList)x).SelectedValue = Convert.ToString(Time[0]);
                                    }
                                    else if (((DropDownList)x).ID == HrID && ((DropDownList)x).ID.Contains("24Hr"))
                                    {
                                        ((DropDownList)x).SelectedValue = Convert.ToString(Time[0]);
                                    }
                                    else if (((DropDownList)x).ID == Min && ((DropDownList)x).ID.Contains("Min"))
                                    {
                                        ((DropDownList)x).SelectedValue = Convert.ToString(Time[1]);
                                    }
                                    else if (((DropDownList)x).ID == AMPMID && ((DropDownList)x).ID.Contains("AMPM"))
                                    {
                                        ((DropDownList)x).SelectedValue = Convert.ToString(Time[2]);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /******** Fill Regimen *********/

        private void theDuration_Load(object sender, EventArgs e)
        {
            TextBox tbox = (TextBox)sender;
        }

        private void UpdateCancel()
        {
            AjaxControlToolkit.TabContainer container = (AjaxControlToolkit.TabContainer)tabContainer;
            container.ActiveTabIndex = Convert.ToInt32(hdnCurrenTabIndex.Value);
            hdnPrevTabIndex.Value = hdnCurrenTabIndex.Value;
            //--- For Cancel event, on updating the form ---
            string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
            script += "var ans;\n";
            script += "alert('Data on " + hdnPrevTabName.Value + " updated successfully.');\n";
            script += "</script>\n";
            //ClientScript.RegisterStartupScript(this.GetType(),"confirm", script);

            Page.ClientScript.RegisterStartupScript(this.GetType(), "confirm", script);
            hdnPrevTabName.Value = hdnCurrenTabName.Value;
        }

        private StringBuilder UpdateCustomFormData(int PatientID, int FeatureID, int VisitID, int LocationID, DataSet DS, int DQChk, int TabId)
        {
            ICustomForm MgrSaveUpdate = (ICustomForm)ObjectFactory.CreateInstance(ObjFactoryParameter);
            AjaxControlToolkit.TabContainer container = (AjaxControlToolkit.TabContainer)tabContainer;
            DataTable theDT = SetControlIDs(container);
            DataView theViewDT = new DataView(theDT);
            theViewDT.RowFilter = "TabId=" + TabId + "";
            theDT = theViewDT.ToTable();

            StringBuilder SbUpdateParam = new StringBuilder();
            StringBuilder SbUpdateColMstPatient = new StringBuilder();
            StringBuilder SbUpdateValMstPatient = new StringBuilder();
            //  string str = "";
            int ICD10Count = 0;
            DataView theViewMulti = new DataView(((DataTable)ViewState["LnkTable"]));
            theViewMulti.RowFilter = "TabId=" + TabId + "";
            DataTable theDTMulti = theViewMulti.ToTable();
            DataView theViewother = new DataView(((DataTable)ViewState["NoMulti"]));
            theViewother.RowFilter = "TabId=" + TabId + "";
            DataTable theDTother = theViewother.ToTable();

            #region "Conditional Field Inclusion"

            DataView theConditionalView = new DataView(((DataSet)Session["AllData"]).Tables[17]);
            theConditionalView.RowFilter = "TabId=" + TabId + "";
            DataTable theConditionalDT = theConditionalView.ToTable();

            foreach (DataRow theConDR in theConditionalDT.Rows)
            {
                DataRow theDTMultiDR = theDTMulti.NewRow();
                theDTMultiDR["FeatureId"] = theConDR["FeatureId"];
                theDTMultiDR["FeatureName"] = theConDR["FeatureName"];
                theDTMultiDR["SectionId"] = theConDR["FieldSectionId"];
                theDTMultiDR["SectionName"] = theConDR["FieldSectionName"];
                theDTMultiDR["FieldId"] = theConDR["FieldId"];
                theDTMultiDR["FieldName"] = theConDR["FieldName"];
                theDTMultiDR["FieldLabel"] = theConDR["FieldLabel"];
                theDTMultiDR["Predefined"] = theConDR["Predefined"];
                theDTMultiDR["PDFTableName"] = theConDR["PDFTableName"];
                theDTMultiDR["ControlId"] = theConDR["ControlId"];
                theDTMultiDR["BindSource"] = theConDR["BindSource"];
                theDTMultiDR["CodeId"] = theConDR["CodeId"];
                theDTMultiDR["Seq"] = theConDR["Seq"];
                theDTMultiDR["SeqSection"] = theConDR["SeqSection"];
                theDTMultiDR["TabId"] = theConDR["TabId"];
                theDTMultiDR["TabName"] = theConDR["TabName"];
                theDTMulti.Rows.Add(theDTMultiDR);

                if (theConDR["ControlId"].ToString() != "9")
                {
                    DataRow theDTOtherDR = theDTother.NewRow();
                    theDTOtherDR["FeatureId"] = theConDR["FeatureId"];
                    theDTOtherDR["FeatureName"] = theConDR["FeatureName"];
                    theDTOtherDR["SectionId"] = theConDR["FieldSectionId"];
                    theDTOtherDR["SectionName"] = theConDR["FieldSectionName"];
                    theDTOtherDR["FieldId"] = theConDR["FieldId"];
                    theDTOtherDR["FieldName"] = theConDR["FieldName"];
                    theDTOtherDR["FieldLabel"] = theConDR["FieldLabel"];
                    theDTOtherDR["Predefined"] = theConDR["Predefined"];
                    theDTOtherDR["PDFTableName"] = theConDR["PDFTableName"];
                    theDTOtherDR["ControlId"] = theConDR["ControlId"];
                    theDTOtherDR["BindSource"] = theConDR["BindSource"];
                    theDTOtherDR["CodeId"] = theConDR["CodeId"];
                    theDTOtherDR["Seq"] = theConDR["Seq"];
                    theDTOtherDR["TabId"] = theConDR["TabId"];
                    theDTOtherDR["TabName"] = theConDR["TabName"];
                    theDTother.Rows.Add(theDTOtherDR);
                }
            }

            #endregion "Conditional Field Inclusion"

            DataTable LnkDT = theDTother.DefaultView.ToTable(true, "PDFTableName", "FeatureName").Copy();

            //Generating Query for MultiSelect List
            foreach (DataRow DRMultiSelect in theDTMulti.Rows)
            {
                if (DRMultiSelect["ControlID"].ToString() == "9" || DRMultiSelect["ControlID"].ToString() == "15")
                {
                    StringBuilder DeleteInsertMultiselect = UpdateMultiSelectList(PatientID, FeatureID, VisitID, LocationID, DRMultiSelect["PDFTableName"].ToString(),
                        DRMultiSelect["FieldName"].ToString(), 0, Convert.ToInt32(DRMultiSelect["ControlID"]), TabId);
                    SbUpdateParam.Append(DeleteInsertMultiselect);

                }
                else if (DRMultiSelect["ControlID"].ToString() == "16" && ICD10Count == 0)
                {
                    int Setflag = 0;
                    StringBuilder SbUpdateICD10 = new StringBuilder();
                    StringBuilder SbValuesICD10 = new StringBuilder();
                    string FieldId = DRMultiSelect["FieldID"].ToString().StartsWith("8888") ? DRMultiSelect["FieldID"].ToString().Replace("8888", "") : DRMultiSelect["FieldID"].ToString().Replace("9999", "");
                    SbUpdateICD10.Append("Delete from dtl_ICD10Field where Ptn_pk=" + PatientID + " and LocationId=" + LocationID + " and Visit_pk=" + VisitID + " and FieldId='" + FieldId + "' and TabId=" + TabId + "");
                    foreach (DataRow theICD10Row in theDT.Rows)
                    {
                        if (theICD10Row[3].ToString().Contains("dtl_ICD10Field") && theICD10Row[0].ToString().Contains("%") && theICD10Row[2].ToString().Contains("1") && Setflag == 0)
                        {
                            string[] strICD10 = theICD10Row[0].ToString().Split('%');
                            FieldId = strICD10[0].StartsWith("8888") ? strICD10[0].Replace("8888", "") : strICD10[0].Replace("9999", "");
                            SbUpdateICD10.Append("Insert into [dtl_ICD10Field](ptn_pk, Visit_Pk, LocationID, FieldId, BlockId,SubBlockId,ICDCodeId, Predefined, UserID,CreateDate,TabId,");
                            SbValuesICD10.Append("Values(" + PatientID + "," + VisitID + "," + LocationID + "," + FieldId + ", " + strICD10[1] + ", " + strICD10[2] + ", " + strICD10[3] + "," + strICD10[4] + "," + Session["AppUserId"] + ", GetDate()," + TabId + ",");
                            Setflag = 1;
                        }
                        else if (theICD10Row[3].ToString().Contains("dtl_ICD10Field") && theICD10Row[0].ToString().Contains("OnSetDate") && Setflag == 1)
                        {
                            SbUpdateICD10.Append("DateOnSet,");
                            SbValuesICD10.Append("'" + theICD10Row[2] + "',");
                        }
                        else if (theICD10Row[3].ToString().Contains("dtl_ICD10Field") && theICD10Row[0].ToString().Contains("Comment") && Setflag == 1)
                        {
                            SbUpdateICD10.Append("Comments)");
                            SbValuesICD10.Append("'" + theICD10Row[2] + "')");
                            Setflag = 0;
                            SbUpdateICD10.Append(SbValuesICD10);
                            SbValuesICD10 = new StringBuilder();
                        }
                    }
                    SbUpdateParam.Append(SbUpdateICD10);
                    ICD10Count = 1;
                }
            }

            //To Add Signature Dropdown for Each tab
            DataTable theDTORDVisit = ((DataSet)Session["AllData"]).Tables[23];
            String Signature = "0";
            if (Convert.ToInt32(theDTORDVisit.Rows[0]["Signature"]) == 1)
            {
                LnkDT.Rows.Add("LNK_FORMTABORDVISIT", "" + theHeader.InnerText + "");
            }
            if (Convert.ToInt32(theDTORDVisit.Rows[0]["Signature"]) == 0)
            {
                Signature = ddSignature.SelectedValue;
            }
            //Update statement if already exists
            foreach (DataRow DR in LnkDT.Rows)
            {
                Boolean Valuethere = false;
                foreach (DataRow DRlnk in theDT.Rows)
                {
                    if (DR["PDFTableName"].ToString() == DRlnk["TableName"].ToString() && Convert.ToString(DRlnk["Value"]) != "")
                    {
                        Valuethere = true;
                    }
                }

                StringBuilder builder = new StringBuilder();
                if (DR[0].ToString() == "DTL_CUSTOMFIELD" && Valuethere == true)
                {
                    builder = new StringBuilder(" if exists(Select * from [DTL_FBCUSTOMFIELD_" + DR[1].ToString().Replace(' ', '_') + "] where ptn_pk=" + PatientID + "");
                    builder.Append(" and Visit_pk=" + VisitID + " and LocationID=" + LocationID + ")");
                    builder.Append(" Begin ");
                    builder.Append("UPDATE [DTL_FBCUSTOMFIELD_" + DR[1].ToString().Replace(' ', '_') + "] SET ");
                }
                else if (DR[0].ToString().ToUpper() == "DTL_CUSTOMFORM")
                {
                }
                else if (DR[0].ToString() == "dtl_ICD10Field".ToUpper())
                {
                }
                else if (DR[0].ToString() == "BMI".ToUpper())
                {
                }
                else if (Convert.ToString(DR[0]) == "dtl_PatientCareEnded".ToUpper() && Valuethere == true)
                {
                    builder = builder = new StringBuilder(" if exists(Select * from " + DR[0] + " where ptn_pk=" + PatientID + "");
                    builder.Append(" and LocationID=" + LocationID + ")");
                    builder.Append(" Begin ");
                    builder.Append("UPDATE " + DR[0] + " SET ");
                }
                else if (Convert.ToString(DR[0]) == "dtl_PatientARVInfo".ToUpper() || Convert.ToString(DR[0]) == "dtl_PatientContacts".ToUpper() && Valuethere == true)
                {
                    builder = builder = new StringBuilder(" if exists(Select * from " + DR[0] + " where ptn_pk=" + PatientID + "");
                    builder.Append(" and Visitid=" + VisitID + " and LocationID=" + LocationID + ")");
                    builder.Append(" Begin ");
                    builder.Append("UPDATE " + DR[0] + " SET ");
                }
                else if (Convert.ToString(DR[0]) == "LNK_FORMTABORDVISIT" && Valuethere == true)
                {
                    builder = builder = new StringBuilder(" if exists(Select * from " + DR[0] + " where Visit_pk=" + VisitID + " and TabId=" + TabId + ")");
                    builder.Append(" Begin ");
                    builder.Append("UPDATE " + DR[0] + " SET ");
                }
                else if (Convert.ToString(DR[0]) == "MST_PATIENT".ToUpper() && Valuethere == true)
                {
                    builder = builder = new StringBuilder(" if exists(Select * from " + DR[0] + " where ptn_pk=" + PatientID + "");
                    builder.Append(" and LocationID=" + LocationID + ")");
                    builder.Append(" Begin ");
                    builder.Append("UPDATE " + DR[0] + " SET ");
                }
                else if (Valuethere == true)
                {
                    builder = builder = new StringBuilder(" if exists(Select * from " + DR[0] + " where ptn_pk=" + PatientID + "");
                    builder.Append(" and Visit_pk=" + VisitID + " and LocationID=" + LocationID + ")");
                    builder.Append(" Begin ");
                    builder.Append("UPDATE " + DR[0] + " SET ");
                }
                foreach (DataRow DRlnk in theDT.Rows)
                {
                    if (DR["PDFTableName"].ToString() == DRlnk["TableName"].ToString() && Convert.ToString(DRlnk["value"]) != "")
                    {
                        if (DR["PDFTableName"].ToString() == "LNK_FORMTABORDVISIT")
                        {
                            builder.Append("Signature").Append(" = ").Append("'" + apostropheHandler(DRlnk["Value"].ToString()) + "'").Append(",");
                        }
                        else
                        {
                            builder.Append("[" + DRlnk["column"] + "]").Append(" = ").Append("'" + apostropheHandler(DRlnk["Value"].ToString()) + "'").Append(",");
                        }
                    }
                }
                if (DR[0].ToString() == "dtl_ICD10Field".ToUpper())
                {
                }
                else if (Convert.ToString(DR[0]) == "dtl_PatientCareEnded".ToUpper() && Valuethere == true)
                {
                    builder.Remove(builder.Length - 1, 1);
                    builder.Append(" where Ptn_Pk=" + PatientID + " and LocationID=" + LocationID + "");
                    builder.Append(" End ");
                    SbUpdateParam.Append(builder);
                }
                else if (Convert.ToString(DR[0]) == "dtl_PatientARVInfo".ToUpper() || Convert.ToString(DR[0]) == "dtl_PatientContacts".ToUpper() && Valuethere == true)
                {
                    builder.Remove(builder.Length - 1, 1);
                    builder.Append(" where Ptn_Pk=" + PatientID + " and Visitid=" + VisitID + " and LocationID=" + LocationID + "");
                    builder.Append(" End ");
                    SbUpdateParam.Append(builder);
                }
                else if (Convert.ToString(DR[0]) == "MST_PATIENT" && Valuethere == true)
                {
                    builder.Remove(builder.Length - 1, 1);
                    builder.Append(" where Ptn_Pk=" + PatientID + " and LocationID=" + LocationID + "");
                    builder.Append(" End ");
                    SbUpdateParam.Append(builder);
                }
                else if (Convert.ToString(DR[0]) == "LNK_FORMTABORDVISIT".ToUpper() && Valuethere == true)
                {
                    builder.Remove(builder.Length - 1, 1);
                    builder.Append(" where  Visit_pk=" + VisitID + " and TabId=" + TabId + "");
                    builder.Append(" End ");
                    SbUpdateParam.Append(builder);
                }
                else if (Valuethere == true)
                {
                    builder.Remove(builder.Length - 1, 1);
                    builder.Append(" where Ptn_Pk=" + PatientID + " and Visit_pk=" + VisitID + " and LocationID=" + LocationID + "");
                    builder.Append(" End ");
                    SbUpdateParam.Append(builder);
                }
            }

            //Insert Statement
            foreach (DataRow DR in LnkDT.Rows)
            {
                StringBuilder builder = new StringBuilder();
                if (DR[0].ToString() == "DTL_CUSTOMFIELD")
                {
                    builder = new StringBuilder(" if not exists(Select * from [DTL_FBCUSTOMFIELD_" + DR[1].ToString().Replace(' ', '_') + "] where ptn_pk=" + PatientID + "");
                    builder.Append(" and Visit_pk=" + VisitID + " and LocationID=" + LocationID + ")");
                    builder.Append(" Begin ");
                    builder.Append(" Insert into [DTL_FBCUSTOMFIELD_" + DR[1].ToString().Replace(' ', '_') + "](ptn_pk, Visit_pk, LocationID,UserID,CreateDate,");
                    foreach (DataRow DRlnk in theDT.Rows)
                    {
                        if (DRlnk["value"].ToString() != "" && DR["PDFTableName"].ToString() == DRlnk["TableName"].ToString())
                        {
                            builder.Append("[" + DRlnk["column"] + "]").Append(",");
                        }
                    }
                    builder.Remove(builder.Length - 1, 1).Append(")");
                    builder.Append(" Values(" + PatientID + "," + VisitID + ", " + LocationID + "," + Session["AppUserId"] + ", GetDate(),");
                    foreach (DataRow DRlnk in theDT.Rows)
                    {
                        if (DRlnk["value"].ToString() != "" && DR["PDFTableName"].ToString() == DRlnk["TableName"].ToString())
                        {
                            builder.Append("'" + apostropheHandler(DRlnk["Value"].ToString()) + "'").Append(",");
                        }
                    }
                    builder.Remove(builder.Length - 1, 1).Append(")");
                    builder.Append(" End ");
                    SbUpdateParam.Append(builder);
                }
                else if (DR[0].ToString().ToUpper() == "DTL_CUSTOMFORM")
                {
                }
                else if (DR[0].ToString().ToUpper() == "BMI")
                {
                }
                else if (DR[0].ToString() == "dtl_ICD10Field".ToUpper())
                {
                }
                else if (DR[0].ToString() == "mst_Patient".ToUpper())
                {
                }
                else if (DR[0].ToString() == "")
                {
                }
                else if (Convert.ToString(DR[0]) == "dtl_PatientCareEnded".ToUpper())
                {
                    builder = new StringBuilder(" if not exists(Select * from " + DR[0] + " where ptn_pk=" + PatientID + "");
                    builder.Append(" and LocationID=" + LocationID + ")");
                    builder.Append(" Begin ");
                    builder.Append(" Insert into [" + DR[0] + "](ptn_pk, LocationID,UserID,CreateDate,");
                    foreach (DataRow DRlnk in theDT.Rows)
                    {
                        if (DRlnk["value"].ToString() != "" && DR["PDFTableName"].ToString() == DRlnk["TableName"].ToString())
                        {
                            builder.Append(DRlnk["column"]).Append(",");
                        }
                    }
                    builder.Remove(builder.Length - 1, 1).Append(")");
                    builder.Append(" Values(" + PatientID + ", " + LocationID + "," + Session["AppUserId"] + ", GetDate(),");
                    foreach (DataRow DRlnk in theDT.Rows)
                    {
                        if (DRlnk["value"].ToString() != "" && DR["PDFTableName"].ToString() == DRlnk["TableName"].ToString())
                        {
                            builder.Append("'" + apostropheHandler(DRlnk["Value"].ToString()) + "'").Append(",");
                        }
                    }
                    builder.Remove(builder.Length - 1, 1).Append(")");
                    builder.Append(" End ");
                    SbUpdateParam.Append(builder);
                }
                else if (Convert.ToString(DR[0]) == "dtl_PatientARVInfo".ToUpper() || Convert.ToString(DR[0]) == "dtl_PatientContacts".ToUpper())
                {
                    builder = builder = new StringBuilder(" if not exists(Select * from " + DR[0] + " where ptn_pk=" + PatientID + "");
                    builder.Append(" and Visitid=" + VisitID + " and LocationID=" + LocationID + ")");
                    builder.Append(" Begin ");
                    builder.Append(" Insert into [" + DR[0] + "](ptn_pk, Visitid, LocationID,UserID,CreateDate,");
                    foreach (DataRow DRlnk in theDT.Rows)
                    {
                        if (DRlnk["value"].ToString() != "" && DR["PDFTableName"].ToString() == DRlnk["TableName"].ToString())
                        {
                            builder.Append(DRlnk["column"]).Append(",");
                        }
                    }
                    builder.Remove(builder.Length - 1, 1).Append(")");
                    builder.Append(" Values(" + PatientID + "," + VisitID + ", " + LocationID + "," + Session["AppUserId"] + ", GetDate(),");
                    foreach (DataRow DRlnk in theDT.Rows)
                    {
                        if (DRlnk["value"].ToString() != "" && DR["PDFTableName"].ToString() == DRlnk["TableName"].ToString())
                        {
                            builder.Append("'" + apostropheHandler(DRlnk["Value"].ToString()) + "'").Append(",");
                        }
                    }
                    builder.Remove(builder.Length - 1, 1).Append(")");
                    builder.Append(" End ");
                    SbUpdateParam.Append(builder);
                }
                else if (Convert.ToString(DR[0]) == "LNK_FORMTABORDVISIT")
                {
                    builder = builder = new StringBuilder(" if not exists(Select * from " + DR[0] + " where Visit_pk=" + VisitID + " and TabId=" + TabId + ")");
                    builder.Append(" Begin ");
                    builder.Append(" Insert into [" + DR[0] + "](Visit_pk, UserID,CreateDate,TabId,");
                    foreach (DataRow DRlnk in theDT.Rows)
                    {
                        if (DRlnk["value"].ToString() != "" && DR["PDFTableName"].ToString() == DRlnk["TableName"].ToString())
                        {
                            builder.Append("Signature").Append(",");
                        }
                    }
                    builder.Remove(builder.Length - 1, 1).Append(")");
                    builder.Append(" Values(" + VisitID + ", " + Session["AppUserId"] + ", GetDate()," + TabId + ",");
                    foreach (DataRow DRlnk in theDT.Rows)
                    {
                        if (DRlnk["value"].ToString() != "" && DR["PDFTableName"].ToString() == DRlnk["TableName"].ToString())
                        {
                            builder.Append("'" + apostropheHandler(DRlnk["Value"].ToString()) + "'").Append(",");
                        }
                    }
                    builder.Remove(builder.Length - 1, 1).Append(")");
                    builder.Append(" End ");
                    SbUpdateParam.Append(builder);
                }
                else
                {
                    builder = builder = new StringBuilder(" if not exists(Select * from " + DR[0] + " where ptn_pk=" + PatientID + "");
                    builder.Append(" and Visit_pk=" + VisitID + " and LocationID=" + LocationID + ")");
                    builder.Append(" Begin ");
                    builder.Append(" Insert into [" + DR[0] + "](ptn_pk, Visit_pk, LocationID,UserID,CreateDate,");
                    foreach (DataRow DRlnk in theDT.Rows)
                    {
                        if (DRlnk["value"].ToString() != "" && DR["PDFTableName"].ToString() == DRlnk["TableName"].ToString())
                        {
                            builder.Append(DRlnk["column"]).Append(",");
                        }
                    }
                    builder.Remove(builder.Length - 1, 1).Append(")");
                    builder.Append(" Values(" + PatientID + "," + VisitID + ", " + LocationID + "," + Session["AppUserId"] + ", GetDate(),");
                    foreach (DataRow DRlnk in theDT.Rows)
                    {
                        if (DRlnk["value"].ToString() != "" && DR["PDFTableName"].ToString() == DRlnk["TableName"].ToString())
                        {
                            builder.Append("'" + apostropheHandler(DRlnk["Value"].ToString()) + "'").Append(",");
                        }
                    }
                    builder.Remove(builder.Length - 1, 1).Append(")");
                    builder.Append(" End ");
                    SbUpdateParam.Append(builder);
                }
            }


            // todo
            if (IsSIngleVisit == false)
            {
                SbUpdateParam.Append(" Update Ord_visit Set VisitDate='" + txtvisitDate.Text + "', Signature='" + Signature + "',DataQuality='" + DQChk.ToString() + "'where Ptn_Pk=" + PatientID + " and Visit_Id=" + VisitID + " and LocationID=" + LocationID + "");// and UserId=" + Session["AppUserId"] + "");
            }
            else
            {
                SbUpdateParam.Append(" Update Ord_visit Set Signature='" + Signature + "', DataQuality ='" + DQChk.ToString() + "' where Ptn_Pk=" + PatientID + " and Visit_Id=" + VisitID + " and LocationID=" + LocationID + "");// and UserId=" + Session["AppUserId"] + "");
            }
            string theRegVisitDate;
            // todo
            if (IsSIngleVisit == true)
            {
                theRegVisitDate = ((DateTime)((DataSet)Session["AllData"]).Tables[18].Rows[0]["StartDate"]).ToString(Session["AppDateFormat"].ToString());
            }
            else
            {
                theRegVisitDate = txtvisitDate.Text;
            }

            SbUpdateParam.Append(" Select Visit_Id[VisitID] from ord_visit where Ptn_Pk=" + PatientID + " and Visit_Id=" + VisitID + " and LocationID=" + LocationID + "");
            SbUpdateParam.Append(" Delete from dbo.dtl_PatientPharmacyOrder where ptn_pharmacy_pk = (Select ptn_pharmacy_pk from dbo.ord_PatientPharmacyOrder");
            SbUpdateParam.Append(" where ptn_pk=" + PatientID + " and VisitID=" + VisitID + " and LocationID=" + LocationID + ") and TabId=" + TabId + "");
            SbUpdateParam.Append(" Delete from dbo.dtl_PatientPharmacyOrderNonARV where ptn_pharmacy_pk = (Select ptn_pharmacy_pk from dbo.ord_PatientPharmacyOrder");
            SbUpdateParam.Append(" where ptn_pk=" + PatientID + " and VisitID=" + VisitID + " and LocationID=" + LocationID + ")  and TabId=" + TabId + "");

            //////////////////////////////SCM Section////////////////////////////////////////////////

            SbUpdateParam.Append(" UPDATE Dtl_PatientBillTransaction SET TransactionDate='" + theRegVisitDate + "',ConsultancyFee = dbo.fn_GetConsultationPerVisit_Futures('" + theRegVisitDate + "'),");
            SbUpdateParam.Append(" AdminFee = dbo.fn_GetOverHeadPerVisit_Futures('" + theRegVisitDate + "'),");
            SbUpdateParam.Append(" BillAmount = dbo.fn_GetConsultationPerVisit_BillAmount_Futures('" + theRegVisitDate + "')+ dbo.fn_GetOverHeadPerVisit_BillAmount_Futures('" + theRegVisitDate + "'),");
            SbUpdateParam.Append(" DoctorId = '" + Signature + "',UserId = " + Session["AppUserId"] + ",UpdateDate = getdate()");
            SbUpdateParam.Append(" where VisitID=" + VisitID + "");
            ////////////////////////////////////////////////////////////////////////////////////////
            if (DS.Tables[1].Rows.Count > 0 || DS.Tables[2].Rows.Count > 0)
            {
                SbUpdateParam.Append(" if not exists(Select * from [ord_patientpharmacyorder] where ptn_pk=" + PatientID + "");
                SbUpdateParam.Append(" and VisitID=" + VisitID + " and LocationID=" + LocationID + ")");
                SbUpdateParam.Append(" Begin");
                SbUpdateParam.Append(" Insert into [ord_patientpharmacyorder](ptn_pk, VisitID, LocationID, OrderedBy, OrderedByDate, UserID, Signature, CreateDate)");
                SbUpdateParam.Append(" Values(" + PatientID + "," + VisitID + "," + LocationID + "," + ddSignature.SelectedValue + ", '" + txtvisitDate.Text + "',");
                SbUpdateParam.Append(" " + Session["AppUserId"] + "," + Signature + ", getdate())");
                SbUpdateParam.Append(" End");
                SbUpdateParam.Append(" Select LocationID, ptn_pharmacy_pk[PharmacyID], UserID from ord_PatientPharmacyOrder where VisitID=" + VisitID + "");
            }
            else { SbUpdateParam.Append(" Select '00000'[PharmacyID]"); };

            #region Insert GridView

            //DataTable lnkSection = ((DataTable)ViewState["LnkTable"]).DefaultView.ToTable(true, "FeatureID", "SectionID", "SectionName", "IsGridView","FeatureName").Copy();
            DataTable lnkSection = theDTMulti.DefaultView.ToTable(true, "FeatureID", "SectionID", "SectionName", "IsGridView", "FeatureName").Copy();
            DataView theDVSectionGridView = new DataView(lnkSection);
            theDVSectionGridView.RowFilter = "IsGridView= 1";
            if (theDVSectionGridView.Count > 0)
            {
                StringBuilder sbInsertGridView = new StringBuilder();
                foreach (DataRow DRGridView in theDVSectionGridView.ToTable().Rows)
                {
                    sbInsertGridView.Append(InsertGridView(PatientID, Convert.ToInt32(DRGridView["FeatureID"].ToString()), Convert.ToInt32(DRGridView["SectionID"]), DRGridView["SectionName"].ToString(), VisitID, DRGridView["FeatureName"].ToString()));
                    sbInsertGridView.Append(";");
                }
                SbUpdateParam.Append(sbInsertGridView);
            }
            # endregion

            SbUpdateParam.Append(" Delete from dbo.dtl_PatientLabResults where LabID = (Select LabID from dbo.ord_PatientLabOrder");
            SbUpdateParam.Append(" where ptn_pk=" + PatientID + " and VisitID=" + VisitID + " and LocationID=" + LocationID + ") and TabId=" + TabId + "");
            SbUpdateParam.Append(" Delete from dbo.Dtl_PatientBillTransaction where LabID = (Select LabID from dbo.ord_PatientLabOrder");
            SbUpdateParam.Append(" where ptn_pk=" + PatientID + " and VisitID=" + VisitID + " and LocationID=" + LocationID + ");");
            if (DS.Tables[0].Rows.Count > 0)
            {
                SbUpdateParam.Append(" if not exists(Select * from [ord_PatientLabOrder] where ptn_pk=" + PatientID + "");
                SbUpdateParam.Append(" and VisitID=" + VisitID + " and LocationID=" + LocationID + ")");
                SbUpdateParam.Append(" Begin");
                SbUpdateParam.Append(" Insert into [ord_PatientLabOrder](ptn_pk, VisitID, LocationID, OrderedbyName, OrderedbyDate, ReportedbyName, ReportedbyDate, UserID, CreateDate)");
                SbUpdateParam.Append(" Values(" + PatientID + "," + VisitID + "," + LocationID + "," + ddSignature.SelectedValue + ", '" + txtvisitDate.Text + "',");
                SbUpdateParam.Append("" + Signature + ", '" + txtvisitDate.Text + "'," + Session["AppUserId"] + ", getdate())");
                SbUpdateParam.Append(" End");
                SbUpdateParam.Append(" Select LocationID, LabID[LabID],UserID from ord_PatientLabOrder where VisitID=" + VisitID + "");
            }
            else { SbUpdateParam.Append(" Select '00000'[LabID]"); }

            return SbUpdateParam;
        }

        private StringBuilder UpdateMultiSelectList(int PatientID, int FeatureID, int VisitID, int LocationID, string Multi_SelectTable, string ColumnName, int DeleteFlag, Int32 theControlId, int TabId)
        {
            StringBuilder Updatecbl = new StringBuilder();
            if (DeleteFlag == 0)
            {
                if (Multi_SelectTable == "DTL_CUSTOMFIELD")
                {
                    Multi_SelectTable = "dtl_FB_" + ColumnName + "";
                    Multi_SelectTable = Multi_SelectTable.Trim().Replace(' ', '_');
                }
                Boolean TabExists = false;
                string filePath = Server.MapPath("~/XMLFiles/MultiSelectCustomForm.xml");
                DataSet dsMultiSelectList = new DataSet();
                dsMultiSelectList.ReadXml(filePath);
                DataTable DT = dsMultiSelectList.Tables[0];
                foreach (DataRow DR in DT.Rows)
                {
                    if (DR[0].ToString().ToUpper() == Multi_SelectTable.ToUpper())
                    {
                        TabExists = true;
                    }
                }
                if (Updatecbl.ToString().Contains(Multi_SelectTable.ToString()) == false)
                {
                    if (TabExists)
                    {
                        Updatecbl.Append("Delete from [" + Multi_SelectTable + "] where [ptn_pk]=" + PatientID + " and [Visit_Pk]=" + VisitID + " and [LocationID]=" + LocationID + " and [TabId]=" + TabId + "");
                    }
                    else
                    {
                        Updatecbl.Append("Delete from [" + Multi_SelectTable + "] where [ptn_pk]=" + PatientID + " and [Visit_Pk]=" + VisitID + " and [LocationID]=" + LocationID + "");
                        AjaxControlToolkit.TabContainer container = (AjaxControlToolkit.TabContainer)tabContainer;
                        foreach (object obj in container.Controls)
                        {
                            if (obj is AjaxControlToolkit.TabPanel)
                            {
                                AjaxControlToolkit.TabPanel tabPanel = (AjaxControlToolkit.TabPanel)obj;

                                if (Convert.ToInt32(tabPanel.ID) == TabId)
                                {
                                    foreach (object ctrl in tabPanel.Controls)
                                    {
                                        if (ctrl is Control)
                                        {
                                            Control c = (Control)ctrl;
                                            foreach (object y in c.Controls)
                                            {
                                                if (y.GetType() == typeof(System.Web.UI.WebControls.Panel))
                                                {
                                                    string strCheckBox = string.Empty;
                                                    string strTableName = string.Empty;
                                                    string strDate1 = string.Empty;
                                                    string strDate2 = string.Empty;
                                                    string strNumeric = string.Empty;
                                                    string[] TableName1 = null;
                                                    string Table1 = string.Empty;

                                                    foreach (Control x in ((Control)y).Controls)
                                                    {
                                                        if (((Panel)y).ID == "Pnl_" + theControlId.ToString() && ((Panel)y).Enabled == false)
                                                            return Updatecbl;
                                                        if (x.GetType() == typeof(System.Web.UI.WebControls.CheckBox))
                                                        {
                                                            string[] TableName = ((CheckBox)x).ID.Split('-');
                                                            TableName1 = ((CheckBox)x).ID.Split('-');
                                                            if (TableName.Length == 5)
                                                            {
                                                                string Table = TableName[3];
                                                                Table1 = TableName[3];
                                                                if (Table1 == "DTL_CUSTOMFIELD")
                                                                {
                                                                    Table1 = "DTL_FB_" + TableName[2] + "";
                                                                    Table1 = Table1.Replace(' ', '_');
                                                                }
                                                                if (Table1 != "DTL_CUSTOMFIELD")
                                                                {
                                                                    if (((CheckBox)x).Checked == true && ((CheckBox)x).Text != "Other")
                                                                    {
                                                                        if (strCheckBox == string.Empty)
                                                                        {
                                                                            strCheckBox = TableName[1];
                                                                            strTableName = Table1;
                                                                        }
                                                                        else
                                                                        {
                                                                            strCheckBox = strCheckBox + ", " + TableName[1];
                                                                            strTableName = strTableName + "," + Table1;
                                                                        }
                                                                    }
                                                                    else if (((CheckBox)x).Checked == true && ((CheckBox)x).Text == "Other")
                                                                    {
                                                                        if (strCheckBox == string.Empty)
                                                                        {
                                                                            strCheckBox = TableName[1];
                                                                            strTableName = Table1;
                                                                        }
                                                                        else
                                                                        {
                                                                            strCheckBox = strCheckBox + ", " + TableName[1];
                                                                            strTableName = strTableName + "," + Table1;
                                                                        }
                                                                        ViewState["OtherNote"] = ((CheckBox)x).Text;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (((CheckBox)x).Checked == true)
                                                                    {
                                                                        Updatecbl.Append("Insert into [" + Multi_SelectTable + "]([ptn_pk], [Visit_Pk], [LocationID], [" + TableName[2] + "], [UserID], [CreateDate])");
                                                                        Updatecbl.Append("values (" + PatientID + ",  " + VisitID + ", " + LocationID + "," + TableName[1] + ",");
                                                                        Updatecbl.Append("" + Session["AppUserId"].ToString() + ", Getdate())");
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                                                        {
                                                            string[] TableName = ((TextBox)x).ID.Split('-');
                                                            if (TableName[0] == "TXTDT1" || TableName[0] == "TXTDT2" || TableName[0] == "TXTNUM")
                                                            {
                                                                if (TableName[0] == "TXTDT1")
                                                                {
                                                                    if (strDate1 == string.Empty)
                                                                    {
                                                                        if (((TextBox)x).Text != "")
                                                                        {
                                                                            strDate1 = ((TextBox)x).Text;
                                                                        }
                                                                        else
                                                                        {
                                                                            strDate1 = "1/1/1900";
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        strDate1 = strDate1 + ", " + ((TextBox)x).Text;
                                                                    }
                                                                }
                                                                else if (TableName[0] == "TXTDT2")
                                                                {
                                                                    if (strDate2 == string.Empty)
                                                                    {
                                                                        if (((TextBox)x).Text != "")
                                                                        {
                                                                            strDate2 = ((TextBox)x).Text;
                                                                        }
                                                                        else
                                                                        {
                                                                            strDate2 = "1/1/1900";
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        strDate2 = strDate2 + ", " + ((TextBox)x).Text;
                                                                    }
                                                                }
                                                                else if (TableName[0] == "TXTNUM")
                                                                {
                                                                    if (strNumeric == string.Empty)
                                                                        strNumeric = ((TextBox)x).Text;
                                                                    else
                                                                        strNumeric = strNumeric + ", " + ((TextBox)x).Text;
                                                                }
                                                            }
                                                        }
                                                        if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputText))
                                                        {
                                                            string[] TableName = ((HtmlInputText)x).ID.Split('-');
                                                            string Table = TableName[3];
                                                            string Other = "";
                                                            if (Table == "DTL_CUSTOMFIELD")
                                                            {
                                                                Table = "DTL_FB_" + TableName[2] + "";
                                                                Table = Table.Replace(' ', '_');
                                                            }
                                                            if (Table != "DTL_CUSTOMFIELD")
                                                            {
                                                                foreach (DataRow DR in DT.Rows)
                                                                {
                                                                    if (DR[0].ToString().ToUpper() == Table)
                                                                    {
                                                                        Other = DR[2].ToString();
                                                                    }
                                                                }
                                                                if (theControlId == 15)
                                                                {
                                                                    Other = "Other";
                                                                }

                                                                if (Convert.ToString(ViewState["OtherNote"]) != "" && ((HtmlInputText)x).Value != "" && Other != "")
                                                                {
                                                                    Updatecbl.Append("Insert into [" + Table + "]([ptn_pk], [Visit_Pk], [LocationID], [" + TableName[2] + "],[" + Other + "], [UserID], [CreateDate], DateField1, DateField2, NumericField,TabId)");
                                                                    Updatecbl.Append("values (" + PatientID + ", " + VisitID + ", " + LocationID + "," + TableName[1] + ",");
                                                                    Updatecbl.Append("'" + ((HtmlInputText)x).Value + "', " + Session["AppUserId"].ToString() + ", Getdate(), '" + Convert.ToDateTime("1/1/1900") + "',  '" + Convert.ToDateTime("1/1/1900") + "', 0, " + TabId + ")");
                                                                    ViewState["OtherNote"] = null;
                                                                }
                                                            }
                                                        }
                                                    }

                                                    // code here
                                                    if (!string.IsNullOrEmpty(strCheckBox))
                                                    {
                                                        string[] arrDate1 = null;
                                                        string[] arrDate2 = null;
                                                        string[] arrNumeric = null;
                                                        string[] arrCheckBox = null;
                                                        string[] arrTableName = null;

                                                        if (strCheckBox != string.Empty)
                                                            arrCheckBox = strCheckBox.Split(',');
                                                        if (strDate1 != string.Empty)
                                                            arrDate1 = strDate1.Split(',');
                                                        if (strDate2 != string.Empty)
                                                            arrDate2 = strDate2.Split(',');
                                                        if (strNumeric != string.Empty)
                                                            arrNumeric = strNumeric.Split(',');
                                                        if (strCheckBox != string.Empty)
                                                            arrTableName = strTableName.Split(',');

                                                        DateTime dtDate1 = new DateTime();
                                                        DateTime dtDate2 = new DateTime();

                                                        int intNumeric = 0;
                                                        for (int i = 0; i < arrCheckBox.Length; i++)
                                                        {
                                                            dtDate1 = Convert.ToDateTime("1/1/1900");
                                                            dtDate2 = Convert.ToDateTime("1/1/1900");
                                                            if (arrDate1 != null)
                                                            {
                                                                if (!string.IsNullOrEmpty(arrDate1[i]) && arrDate1[i] != " ")
                                                                    dtDate1 = Convert.ToDateTime(arrDate1[i]);
                                                            }

                                                            if (arrDate2 != null)
                                                            {
                                                                if (!string.IsNullOrEmpty(arrDate2[i]) && arrDate2[i] != " ")
                                                                    dtDate2 = Convert.ToDateTime(arrDate2[i]);
                                                            }
                                                            if (arrNumeric != null)
                                                            {
                                                                if (!string.IsNullOrEmpty(arrNumeric[i]) && arrNumeric[i] != " ")
                                                                    intNumeric = Convert.ToInt16(arrNumeric[i]);
                                                            }

                                                            Boolean tabIdexist = false;
                                                            foreach (DataRow DR in DT.Rows)
                                                            {
                                                                if (DR[0].ToString().ToUpper() == Table1)
                                                                {
                                                                    tabIdexist = true;
                                                                }
                                                            }

                                                            //Creating column for old database where Column Name 'Datefield1', 'Datefield2' and Numericfield doesnot exist
                                                            StringBuilder createColumn = new StringBuilder();
                                                            createColumn.Append("if not exists(select * from INFORMATION_SCHEMA.COLUMNS where table_name='" + Table1 + "' and column_name='DateField1')");
                                                            createColumn.Append("Begin Alter table [" + Table1 + "] Add DateField1 datetime End ");
                                                            createColumn.Append("if not exists(select * from INFORMATION_SCHEMA.COLUMNS where table_name='" + Table1 + "' and column_name='DateField2')");
                                                            createColumn.Append("Begin Alter table [" + Table1 + "] Add DateField2 datetime End ");
                                                            createColumn.Append("if not exists(select * from INFORMATION_SCHEMA.COLUMNS where table_name='" + Table1 + "' and column_name='NumericField')");
                                                            createColumn.Append("Begin Alter table [" + Table1 + "] Add NumericField int End ");
                                                            createColumn.Append("Select 1[Saved]");
                                                            ICustomForm MgrSaveUpdate = (ICustomForm)ObjectFactory.CreateInstance(ObjFactoryParameter);
                                                            DataSet TempDS = MgrSaveUpdate.Common_GetSaveUpdate(createColumn.ToString());

                                                            if (tabIdexist)
                                                            {
                                                                //    Updatecbl.Append("Insert into [" + Table1 + "]([ptn_pk], [Visit_Pk], [LocationID], [" + TableName1[2] + "], [UserID], [CreateDate], DateField1, DateField2, NumericField, TabId)");
                                                                //    Updatecbl.Append("values (" + PatientID + ",  " + VisitID + ", " + LocationID + ", " + arrCheckBox[i] + ",");
                                                                //    Updatecbl.Append("" + Session["AppUserId"].ToString() + ", Getdate() " + ", '" + dtDate1 + "', '" + dtDate2 + "', " + intNumeric + ", " + TabId + ")");
                                                            }
                                                            else
                                                            {
                                                                string[] spltbl = strTableName.Split(',');
                                                                if (spltbl[0].ToLower() == Multi_SelectTable.ToLower())
                                                                {
                                                                    Updatecbl.Append("Insert into [" + Multi_SelectTable + "]([ptn_pk], [Visit_Pk], [LocationID], [" + TableName1[2] + "], [UserID], [CreateDate], DateField1, DateField2, NumericField)");
                                                                    Updatecbl.Append("values (" + PatientID + ",  " + VisitID + ", " + LocationID + ", " + arrCheckBox[i] + ",");
                                                                    Updatecbl.Append("" + Session["AppUserId"].ToString() + ", Getdate() " + ", '" + dtDate1 + "', '" + dtDate2 + "', " + intNumeric + ")");
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return Updatecbl;
        }

        //john 28th june 2013
        private void UserRights(Button save, Button DQ, Button print, int FeatureID)
        {
            AuthenticationManager Authentication = new AuthenticationManager();
            if (Authentication.HasFunctionRight(FeatureID, FunctionAccess.View, (DataTable)Session["UserRight"]) == true)
            {
                save.Enabled = false;
                DQ.Enabled = false;
            }
            else
            {
                save.Enabled = true;
                DQ.Enabled = true;
            }

            if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
            {
                if (Authentication.HasFunctionRight(FeatureID, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
                {
                    save.Enabled = false;
                    DQ.Enabled = false;
                }
                else
                {
                    save.Enabled = true;
                    DQ.Enabled = true;
                }
            }
            else if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
            {
                if (Authentication.HasFunctionRight(FeatureID, FunctionAccess.Update, (DataTable)Session["UserRight"]) == false)
                {
                    save.Enabled = false;
                    DQ.Enabled = false;
                }
                else
                {
                    save.Enabled = true;
                    DQ.Enabled = true;
                }
            }

            if (Authentication.HasFunctionRight(FeatureID, FunctionAccess.Print, (DataTable)Session["UserRight"]) == false)
            {
                print.Enabled = false;
            }
        }

        private String ValidationMessage(DataSet theDS, int TabId)
        {
            IIQCareSystem IQCareSecurity = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
            DateTime theCurrentDate = (DateTime)IQCareSecurity.SystemDate();
            string strmsg = "Following values are required to complete this:\\n\\n";
            DataView theViewValidation = new DataView(((DataTable)ViewState["BusRule"]));
            theViewValidation.RowFilter = "TabId=" + TabId + "";
            DataTable theDT = theViewValidation.ToTable();

            String Radio1 = "", Radio2 = "", MultiSelectName = "", MultiSelectLabel = "";
            int TotCount = 0, FalseCount = 0, TextBoxDate1FalseCount = 0, TextBoxDate2FalseCount = 0, TextBoxNumericFalseCount = 0;
            try
            {
                AjaxControlToolkit.TabContainer container = (AjaxControlToolkit.TabContainer)tabContainer;
                foreach (object obj in container.Controls)
                {
                    if (obj is AjaxControlToolkit.TabPanel)
                    {
                        AjaxControlToolkit.TabPanel tabPanel = (AjaxControlToolkit.TabPanel)obj;
                        foreach (object ctrl in tabPanel.Controls)
                        {
                            if (ctrl is Control)
                            {
                                Control c = (Control)ctrl;
                                foreach (object x in c.Controls)
                                {
                                    if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                                    {
                                        string[] Field = ((TextBox)x).ID.Split('-');

                                        foreach (DataRow theDR in theDT.Rows)
                                        {
                                            if ((((TextBox)x).ID.Contains("=") == true) && (((TextBox)x).Enabled == true))
                                            {
                                                string[] Field10 = ((TextBox)x).ID.Replace('=', '-').Split('-');
                                                if (Field10[1] == Convert.ToString(theDR["FieldName"]) && Field10[2] == Convert.ToString(theDR["TableName"]) && Field10[3] == Convert.ToString(theDR["FieldId"]) && Convert.ToString(theDR["BusRuleId"]) == "1")
                                                {
                                                    if (((TextBox)x).Text == "")
                                                    {
                                                        string str = "ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + Field[4].ToString() + "_" + ((TextBox)x).ID + "";
                                                        if (!(htcontrolstatus.ContainsKey(str)))
                                                        {
                                                            strmsg += theDR["FieldLabel"] + " is " + theDR["Name"];
                                                            strmsg = strmsg + "\\n";
                                                        }
                                                    }
                                                }
                                            }

                                            if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["TableName"]) && Field[3] == Convert.ToString(theDR["FieldId"]) && Convert.ToString(theDR["BusRuleId"]) == "1")
                                            {
                                                if ((((TextBox)x).Text == "") && (((TextBox)x).Enabled == true))
                                                {
                                                    string str = "ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + Field[4].ToString() + "_" + ((TextBox)x).ID + "";
                                                    if (!(htcontrolstatus.ContainsKey(str)))
                                                    {
                                                        strmsg += theDR["FieldLabel"] + " is " + theDR["Name"];
                                                        strmsg = strmsg + "\\n";
                                                    }
                                                }
                                            }

                                            //Date Greater than Today's Date
                                            if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["TableName"]) && Field[3] == Convert.ToString(theDR["FieldId"]) && Convert.ToString(theDR["BusRuleId"]) == "7")
                                            {
                                                if (((TextBox)x).Text != "")
                                                {
                                                    DateTime GetDate = Convert.ToDateTime(((TextBox)x).Text);
                                                    if (GetDate < Convert.ToDateTime(theCurrentDate.ToShortDateString()))
                                                    {
                                                        strmsg += theDR["Name"] + " for " + theDR["FieldLabel"];
                                                        strmsg = strmsg + "\\n";
                                                    }
                                                }
                                            }
                                            //Date Less than Today's Date
                                            if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["TableName"]) && Field[3] == Convert.ToString(theDR["FieldId"]) && Convert.ToString(theDR["BusRuleId"]) == "8")
                                            {
                                                if (((TextBox)x).Text != "")
                                                {
                                                    DateTime GetDate = Convert.ToDateTime(((TextBox)x).Text);

                                                    if (GetDate >= Convert.ToDateTime(theCurrentDate.ToShortDateString()))
                                                    {
                                                        strmsg += theDR["Name"] + " for " + theDR["FieldLabel"];
                                                        strmsg = strmsg + "\\n";
                                                    }
                                                }
                                            }
                                            //Date greater than Date of Birth
                                            if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["TableName"]) && Field[3] == Convert.ToString(theDR["FieldId"]) && Convert.ToString(theDR["BusRuleId"]) == "9")
                                            {
                                                DateTime GetDOB = Convert.ToDateTime(hdfldDOB.Value);
                                                if (((TextBox)x).Text != "")
                                                {
                                                    DateTime GetDate = Convert.ToDateTime(((TextBox)x).Text);
                                                    if (GetDate <= GetDOB)
                                                    {
                                                        strmsg += theDR["Name"] + " for " + theDR["FieldLabel"];
                                                        strmsg = strmsg + "\\n";
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputRadioButton))
                                    {
                                        string[] Field = ((HtmlInputRadioButton)x).ID.Split('-');
                                        if (Field[0] == "RADIO1" && ((HtmlInputRadioButton)x).Checked == false)
                                        {
                                            Radio1 = Field[3];
                                        }
                                        if (Field[0] == "RADIO2" && ((HtmlInputRadioButton)x).Checked == false)
                                        {
                                            Radio2 = Field[3];
                                        }

                                        foreach (DataRow theDR in theDT.Rows)
                                        {
                                            if (Radio1 == Field[3] && Radio2 == Field[3])
                                            {
                                                if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["TableName"]) && Field[3] == Convert.ToString(theDR["FieldId"]) && Convert.ToString(theDR["BusRuleId"]) == "1")
                                                {
                                                    strmsg += theDR["FieldLabel"] + " is " + theDR["Name"];
                                                    strmsg = strmsg + "\\n";
                                                }
                                            }
                                        }
                                    }
                                    if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                                    {
                                        string[] Field = ((DropDownList)x).ID.Split('-');
                                        foreach (DataRow theDR in theDT.Rows)
                                        {
                                            if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["TableName"]) && Field[3] == Convert.ToString(theDR["FieldId"]) && Convert.ToString(theDR["BusRuleId"]) == "1" && Field[0].ToString() != "SELECTLISTAuto" && Field[3].Contains("12Hr") == false && Field[3].Contains("24Hr") == false && Field[3].Contains("Min") == false && Field[3].Contains("AMPM") == false)
                                            {
                                                if ((((DropDownList)x).SelectedValue == "0") && (Field[0].ToString() != "SELECTLISTAuto") && ((DropDownList)x).Enabled == true)
                                                {
                                                    string str = "ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + Field[4].ToString() + "_" + ((DropDownList)x).ID + "";
                                                    if (!(htcontrolstatus.ContainsKey(str)))
                                                    {
                                                        strmsg += theDR["FieldLabel"] + " is " + theDR["Name"];
                                                        strmsg = strmsg + "\\n";
                                                        //ScriptManager.RegisterClientScriptBlock(, this.GetType(), "test1", "alert('test 1');", true);
                                                        //ClientScript.RegisterStartupScript(GetType(), "CurrentTabValue2", "fncontrolstatus('ctl00_IQCareContentPlaceHolder_" + tabcontainer.ID + "_" + Field[4].ToString() + "_" + ((DropDownList)x).ID + "');", true);
                                                    }
                                                }
                                            }
                                            else if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["TableName"]) && Convert.ToString(theDR["BusRuleId"]) == "1" && Field[3].Contains("12Hr"))
                                            {
                                                if ((((DropDownList)x).SelectedValue == "0") && ((DropDownList)x).Enabled == true)
                                                {
                                                    string str = "ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + Field[4].ToString() + "_" + ((DropDownList)x).ID + "";
                                                    if (!(htcontrolstatus.ContainsKey(str)))
                                                    {
                                                        strmsg += theDR["FieldLabel"] + " is " + theDR["Name"];
                                                        strmsg = strmsg + "\\n";
                                                    }
                                                }
                                            }
                                            else if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["TableName"]) && Convert.ToString(theDR["BusRuleId"]) == "1" && Field[3].Contains("24Hr"))
                                            {
                                                if ((((DropDownList)x).SelectedValue == "0") && ((DropDownList)x).Enabled == true)
                                                {
                                                    string str = "ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + Field[4].ToString() + "_" + ((DropDownList)x).ID + "";
                                                    if (!(htcontrolstatus.ContainsKey(str)))
                                                    {
                                                        strmsg += theDR["FieldLabel"] + " is " + theDR["Name"];
                                                        strmsg = strmsg + "\\n";
                                                    }
                                                }
                                            }
                                            else if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["TableName"]) && Convert.ToString(theDR["BusRuleId"]) == "1" && Field[3].Contains("Min"))
                                            {
                                                if ((((DropDownList)x).SelectedValue == "0") && ((DropDownList)x).Enabled == true)
                                                {
                                                    string str = "ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + Field[4].ToString() + "_" + ((DropDownList)x).ID + "";
                                                    if (!(htcontrolstatus.ContainsKey(str)))
                                                    {
                                                        strmsg += theDR["FieldLabel"] + " is " + theDR["Name"];
                                                        strmsg = strmsg + "\\n";
                                                    }
                                                }
                                            }
                                            else if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["TableName"]) && Convert.ToString(theDR["BusRuleId"]) == "1" && Field[3].Contains("AMPM"))
                                            {
                                                if ((((DropDownList)x).SelectedValue == "0") && ((DropDownList)x).Enabled == true)
                                                {
                                                    string str = "ctl00_IQCareContentPlaceHolder_" + tabContainer.ID + "_" + Field[4].ToString() + "_" + ((DropDownList)x).ID + "";
                                                    if (!(htcontrolstatus.ContainsKey(str)))
                                                    {
                                                        strmsg += theDR["FieldLabel"] + " is " + theDR["Name"];
                                                        strmsg = strmsg + "\\n";
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputCheckBox))
                                    {
                                        string[] Field = ((HtmlInputCheckBox)x).ID.Split('-');
                                        foreach (DataRow theDR in theDT.Rows)
                                        {
                                            if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["TableName"]) && Field[3] == Convert.ToString(theDR["FieldId"]) && Convert.ToString(theDR["BusRuleId"]) == "1")
                                            {
                                                if (((HtmlInputCheckBox)x).Checked == false)
                                                {
                                                    strmsg += theDR["FieldLabel"] + " is " + theDR["Name"];
                                                    strmsg = strmsg + "\\n";
                                                }
                                            }
                                        }
                                    }

                                    if (x.GetType() == typeof(System.Web.UI.WebControls.HiddenField))
                                    {
                                        string[] Field = ((HiddenField)x).ID.Split('-');

                                        if (Field.Length == 4)
                                        {
                                            foreach (DataRow theDR in theDT.Rows)
                                            {
                                                if (Field[3] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["FieldId"]) && Convert.ToString(theDR["BusRuleId"]) == "1")
                                                {
                                                    if (theDS.Tables[0].Rows.Count == 0)
                                                    {
                                                        strmsg += theDR["FieldLabel"] + " is " + theDR["Name"];
                                                        strmsg = strmsg + "\\n";
                                                    }
                                                }
                                            }
                                        }
                                        if (Field.Length == 5)
                                        {
                                            foreach (DataRow theDR in theDT.Rows)
                                            {
                                                if (Field[3] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["FieldId"]) && Convert.ToString(theDR["BusRuleId"]) == "1"
                                                    && (Convert.ToString(theDR["Value"]) == "37" || theDR["Value"].ToString() == "36"))
                                                {
                                                    if (theDS.Tables[1].Rows.Count == 0)
                                                    {
                                                        DataView theDV = new DataView((DataTable)Session["DrugTypeName"]);
                                                        theDV.RowFilter = "DrugTypeID=" + theDR["Value"];
                                                        DataTable theDrugNameDT = theDV.ToTable();
                                                        strmsg += theDrugNameDT.Rows[0]["DrugTypeName"] + " is Required Field";
                                                        strmsg = strmsg + "\\n";
                                                    }
                                                }
                                                else if (Field[3] == Convert.ToString(theDR["FieldName"]) && Field[2] == Convert.ToString(theDR["FieldId"]) && Convert.ToString(theDR["BusRuleId"]) == "1"
                                                    && (Convert.ToString(theDR["Value"]) == "37" || theDR["Value"].ToString() == "36"))
                                                {
                                                    if (theDS.Tables[2].Rows.Count == 0)
                                                    {
                                                        DataView theDV = new DataView((DataTable)Session["DrugTypeName"]);
                                                        theDV.RowFilter = "DrugTypeID=" + theDR["Value"];
                                                        DataTable theDrugNameDT = theDV.ToTable();
                                                        strmsg += theDrugNameDT.Rows[0]["DrugTypeName"] + " is Required Field";
                                                        strmsg = strmsg + "\\n";
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                //MultiSelect Validation
                int DrugsCount = 0;
                foreach (object obj in container.Controls)
                {
                    if (obj is AjaxControlToolkit.TabPanel)
                    {
                        AjaxControlToolkit.TabPanel tabPanel = (AjaxControlToolkit.TabPanel)obj;
                        foreach (object ctrl in tabPanel.Controls)
                        {
                            if (ctrl is Control)
                            {
                                Control c = (Control)ctrl;
                                foreach (Control y in ((Control)ctrl).Controls)
                                {
                                    if (y.GetType() == typeof(System.Web.UI.WebControls.Panel))
                                    {
                                        foreach (Control z in y.Controls)
                                        {
                                            if (z.GetType() == typeof(System.Web.UI.WebControls.Label))
                                            {
                                                if (z.ID.Contains("ARVdrgNm") == true || z.ID.Contains("ARVGenericNm") == true || z.ID.Contains("DrugNm") == true || z.ID.Contains("GenericNm") == true)
                                                {
                                                    DrugsCount++;
                                                }
                                            }
                                            if (z.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                                            {
                                                if (z.ID.Contains("ARVdrgDuration") == true || z.ID.Contains("ARVGenericDuration") == true || z.ID.Contains("DrugDuration") == true || z.ID.Contains("GenericDuration") == true)
                                                {
                                                    if (((TextBox)z).Text == "")
                                                    {
                                                        strmsg += "Drug Duration Cannot be Blank";
                                                        strmsg = strmsg + "\\n";
                                                    }
                                                }
                                                else if (z.ID.Contains("GenericFrequency") == true || z.ID.Contains("genericQtyPrescribed") == true || z.ID.Contains("genericQtyDispensed") == true || z.ID.Contains("FinChkGeneric") == true)
                                                {
                                                    if (((TextBox)z).Text == "")
                                                    {
                                                        strmsg += "Drug Duration Cannot be Blank";
                                                        strmsg = strmsg + "\\n";
                                                    }
                                                }
                                                else if (z.ID.Contains("TXTDT1") == true || z.ID.Contains("TXTDT2") == true || z.ID.Contains("TXTNUM") == true)
                                                {
                                                    if (z.ID.Contains("TXTDT1"))
                                                    {
                                                        if (((TextBox)z).Text == string.Empty)
                                                        {
                                                            TextBoxDate1FalseCount++;
                                                        }
                                                    }
                                                    else if (z.ID.Contains("TXTDT2"))
                                                    {
                                                        if (((TextBox)z).Text == string.Empty)
                                                        {
                                                            TextBoxDate2FalseCount++;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (((TextBox)z).Text == string.Empty)
                                                        {
                                                            TextBoxNumericFalseCount++;
                                                        }
                                                    }
                                                }
                                            }

                                            if (z.GetType() == typeof(System.Web.UI.WebControls.CheckBox))
                                            {
                                                TotCount++;
                                                if (((CheckBox)z).Checked == false && ((CheckBox)z).Text.ToUpper().Trim() != "OTHER")
                                                {
                                                    FalseCount++;
                                                }
                                            }
                                        }
                                        if (TextBoxDate1FalseCount > 0 || TextBoxDate2FalseCount > 0 || TextBoxNumericFalseCount > 0)
                                        {
                                            if (FalseCount != 0)
                                            {
                                                //if (TextBoxDate1FalseCount >= FalseCount && TextBoxDate1FalseCount!=0)
                                                //{
                                                //    strmsg += "Please select Checkbox.";
                                                //    strmsg = strmsg + "\\n";
                                                //}
                                                //else if (TextBoxDate2FalseCount >= FalseCount && TextBoxDate2FalseCount !=0)
                                                //{
                                                //    strmsg += "Please select Checkbox.";
                                                //    strmsg = strmsg + "\\n";
                                                //}
                                                //else if (TextBoxNumericFalseCount >= FalseCount && TextBoxNumericFalseCount !=0)
                                                //{
                                                //    strmsg += "Please select Checkbox.";
                                                //    strmsg = strmsg + "\\n";
                                                //}
                                            }
                                        }
                                        foreach (DataRow theDR in theDT.Rows)
                                        {
                                            string[] arrID = ((Panel)y).ID.Split('-');
                                            if (Convert.ToString(theDR["ControlId"]) == "9" && arrID[2] == Convert.ToString(theDR["FieldID"]) && Convert.ToInt32(theDR["BusRuleId"]) <= 13)
                                            {
                                                MultiSelectName = Convert.ToString(theDR["Name"]);
                                                MultiSelectLabel = Convert.ToString(theDR["FieldLabel"]);
                                                if (TotCount == FalseCount)
                                                {
                                                    strmsg += MultiSelectLabel + " is " + MultiSelectName;
                                                    strmsg = strmsg + "\\n";
                                                }
                                                if (TextBoxDate1FalseCount > 0 || TextBoxDate2FalseCount > 0 || TextBoxNumericFalseCount > 0)
                                                {
                                                    strmsg += MultiSelectLabel + " is " + MultiSelectName;
                                                    strmsg = strmsg + "\\n";
                                                }
                                            }
                                        }
                                        TotCount = 0; FalseCount = 0; TextBoxDate1FalseCount = 0; TextBoxDate2FalseCount = 0; TextBoxNumericFalseCount = 0;
                                        MultiSelectName = ""; MultiSelectLabel = "";
                                    }
                                }
                            }
                        }
                    }
                }

                foreach (DataRow DR in theDT.Rows)
                {
                    if (Convert.ToString(DR["BusRuleId"]) == "1" && Convert.ToString(DR["ControlId"]) == "11")
                    {
                        if (DrugsCount == 0)
                        {
                            strmsg = strmsg + "Please Select Drug Selection.";
                        }
                    }
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
            finally { }

            return strmsg;
        }

        #region ICallbackEventHandler Members

        public string GetCallbackResult()
        {
            string thestr = str;
            return thestr;
        }

        public DataSet GetRaiseEventValue(int PatientID, int VisitID, int LocationID, Control theControl)
        {
            AjaxControlToolkit.TabContainer container = (AjaxControlToolkit.TabContainer)tabContainer;
            DataSet theDSAuto = new DataSet();
            DataTable theDTAuto = new DataTable("theDTAuto");
            theDTAuto.Columns.Add(new DataColumn("ID", typeof(String)));
            theDTAuto.Columns.Add(new DataColumn("Value", typeof(String)));
            theDTAuto.Columns.Add(new DataColumn("Ctrl", typeof(String)));
            DataRow theDR;
            ICustomForm MgrBindValue = (ICustomForm)ObjectFactory.CreateInstance(ObjFactoryParameter);
            DataTable theDT_I = SetControlIDs(tabContainer);
            DataTable TempDT = theDT_I.DefaultView.ToTable(true, "TableName").Copy();
            DataSet theDS = new DataSet();
            DataSet TempDS = new DataSet();
            try
            {
                foreach (DataRow TempDR in TempDT.Rows)
                {
                    string GetValue = "";
                    if (TempDR["TableName"].ToString() == "DTL_CUSTOMFIELD")
                    {
                        string TableName = "DTL_FBCUSTOMFIELD_" + theHeader.InnerText.Replace(' ', '_');
                        GetValue = "Select * from [" + TableName + "] where Ptn_pk=" + PatientID + " and Visit_Pk=" + VisitID + " and LocationId=" + LocationID + "";
                    }
                    else
                    {
                        if (Convert.ToString(TempDR["TableName"]) == "dtl_PatientARVInfo".ToUpper() || Convert.ToString(TempDR["TableName"]) == "dtl_PatientContacts".ToUpper())
                        {
                            GetValue = "Select * from [" + TempDR["TableName"] + "] where Ptn_pk=" + PatientID + " and Visitid=" + VisitID + " and LocationId=" + LocationID + "";
                        }
                        else if (Convert.ToString(TempDR["TableName"]) == "mst_patient".ToUpper())
                        {
                            GetValue = "Select * from [" + TempDR["TableName"] + "] where Ptn_pk=" + PatientID + " and LocationId=" + LocationID + "";
                        }
                        else if (TempDR["TableName"].ToString().ToUpper() == "DTL_CUSTOMFORM".ToUpper())
                        {
                        }
                        else if (Convert.ToString(TempDR["TableName"]) == "dtl_PatientCareEnded".ToUpper())
                        {
                            GetValue = "Select * from [" + TempDR["TableName"] + "] where Ptn_pk=" + PatientID + " and LocationId=" + LocationID + "";
                        }
                        else if (Convert.ToString(TempDR["TableName"]) == "lnk_FormTabOrdVisit".ToUpper())
                        {
                            GetValue = "Select * from [" + TempDR["TableName"] + "] where Visit_pk=" + VisitID + "";
                        }
                        else
                        {
                            GetValue = "Select * from [" + TempDR["TableName"] + "] where Ptn_pk=" + PatientID + " and Visit_Pk=" + VisitID + " and LocationId=" + LocationID + "";
                        }
                    }
                    if (GetValue.Length > 2)
                    {
                        TempDS = MgrBindValue.Common_GetSaveUpdate(GetValue);
                    }
                    if (TempDS.Tables.Count > 0)
                    {
                        for (int i = 0; i <= TempDS.Tables[0].Columns.Count - 1; i++)
                        {
                            foreach (object obj in container.Controls)
                            {
                                if (obj is AjaxControlToolkit.TabPanel)
                                {
                                    AjaxControlToolkit.TabPanel tabPanel = (AjaxControlToolkit.TabPanel)obj;
                                    foreach (object ctrl in tabPanel.Controls)
                                    {
                                        if (ctrl is Control)
                                        {
                                            Control c = (Control)ctrl;
                                            foreach (object x in c.Controls)
                                            {
                                                if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                                                {
                                                    string[] remStr = ((TextBox)x).ID.Split('-');
                                                    string str = remStr[0] + "-" + remStr[1] + "-" + remStr[2];
                                                    if ("TXTMultiAuto-" + TempDS.Tables[0].Columns[i].ToString() + "-" + TempDR["TableName"] == str)//((TextBox)x).ID.Substring(0, ((TextBox)x).ID.LastIndexOf('-')))
                                                    {
                                                        if (TempDS.Tables[0].Rows.Count > 0)
                                                        {
                                                            theDR = theDTAuto.NewRow();
                                                            theDR["ID"] = "TAB_" + tabPanel.ID + "_" + ((TextBox)x).ID;
                                                            if (TempDS.Tables[0].Rows[0][i].ToString() == "")
                                                            {
                                                                theDR["Value"] = "0";
                                                            }
                                                            else
                                                            {
                                                                theDR["Value"] = Convert.ToString(TempDS.Tables[0].Rows[0][i]);
                                                            }

                                                            theDR["Ctrl"] = "TextBox";
                                                            theDTAuto.Rows.Add(theDR);
                                                        }
                                                    }
                                                    if ("TXTSingleAuto-" + TempDS.Tables[0].Columns[i].ToString() + "-" + TempDR["TableName"] == str)//((TextBox)x).ID.Substring(0, ((TextBox)x).ID.LastIndexOf('-')))
                                                    {
                                                        if (TempDS.Tables[0].Rows.Count > 0)
                                                        {
                                                            theDR = theDTAuto.NewRow();
                                                            theDR["ID"] = "TAB_" + tabPanel.ID + "_" + ((TextBox)x).ID;

                                                            if (TempDS.Tables[0].Rows[0][i].ToString() == "")
                                                            {
                                                                theDR["Value"] = "0";
                                                            }
                                                            else
                                                            {
                                                                theDR["Value"] = Convert.ToString(TempDS.Tables[0].Rows[0][i]);
                                                            }

                                                            //theDR["Value"] = Convert.ToString(TempDS.Tables[0].Rows[0][i]);
                                                            theDR["Ctrl"] = "TextBox";
                                                            theDTAuto.Rows.Add(theDR);
                                                        }
                                                    }

                                                    if ("TXTAuto-" + TempDS.Tables[0].Columns[i].ToString() + "-" + TempDR["TableName"] == str)//((TextBox)x).ID.Substring(0, ((TextBox)x).ID.LastIndexOf('-')))
                                                    {
                                                        if (TempDS.Tables[0].Rows.Count > 0)
                                                        {
                                                            theDR = theDTAuto.NewRow();
                                                            theDR["ID"] = "TAB_" + tabPanel.ID + "_" + ((TextBox)x).ID;
                                                            if (TempDS.Tables[0].Rows[0][i].ToString() == "")
                                                            {
                                                                theDR["Value"] = "0";
                                                            }
                                                            else
                                                            {
                                                                theDR["Value"] = Convert.ToString(TempDS.Tables[0].Rows[0][i]);
                                                            }

                                                            //theDR["Value"] = Convert.ToString(TempDS.Tables[0].Rows[0][i]);
                                                            theDR["Ctrl"] = "TextBox";
                                                            theDTAuto.Rows.Add(theDR);
                                                        }
                                                    }
                                                    if ("TXTNUMAuto-" + TempDS.Tables[0].Columns[i].ToString() + "-" + TempDR["TableName"] == str)//((TextBox)x).ID.Substring(0, ((TextBox)x).ID.LastIndexOf('-')))
                                                    {
                                                        if (TempDS.Tables[0].Rows.Count > 0)
                                                        {
                                                            theDR = theDTAuto.NewRow();
                                                            theDR["ID"] = "TAB_" + tabPanel.ID + "_" + ((TextBox)x).ID;
                                                            if (TempDS.Tables[0].Rows[0][i].ToString() == "")
                                                            {
                                                                theDR["Value"] = "0";
                                                            }
                                                            else
                                                            {
                                                                theDR["Value"] = Convert.ToString(TempDS.Tables[0].Rows[0][i]);
                                                            }
                                                            //theDR["Value"] = Convert.ToString(TempDS.Tables[0].Rows[0][i]);
                                                            theDR["Ctrl"] = "TextBox";
                                                            theDTAuto.Rows.Add(theDR);
                                                        }
                                                    }

                                                    if ("TXTDTAuto-" + TempDS.Tables[0].Columns[i].ToString() + "-" + TempDR["TableName"] == str)//((TextBox)x).ID.Substring(0, ((TextBox)x).ID.LastIndexOf('-')))
                                                    {
                                                        if (TempDS.Tables[0].Rows.Count > 0)
                                                        {
                                                            theDR = theDTAuto.NewRow();
                                                            theDR["ID"] = "TAB_" + tabPanel.ID + "_" + ((TextBox)x).ID;
                                                            if (TempDS.Tables[0].Rows[0][i].ToString() == "")
                                                            {
                                                                theDR["Value"] = "0";
                                                            }
                                                            else
                                                            {
                                                                // theDR["Value"] = string.Format("ddMMyyyy", TempDS.Tables[0].Rows[0][i].ToString());
                                                                theDR["Value"] = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(TempDS.Tables[0].Rows[0][i]));

                                                                //theDR["Value"] = Convert.ToString(TempDS.Tables[0].Rows[0][i]).ToString();
                                                            }
                                                            //theDR["Value"] = Convert.ToString(TempDS.Tables[0].Rows[0][i]);
                                                            theDR["Ctrl"] = "TextBox";
                                                            theDTAuto.Rows.Add(theDR);
                                                        }
                                                    }
                                                    if ("TXTRegAuto-" + TempDS.Tables[0].Columns[i].ToString() + "-" + TempDR["TableName"] == str)//((TextBox)x).ID.Substring(0, ((TextBox)x).ID.LastIndexOf('-')))
                                                    {
                                                        if (TempDS.Tables[0].Rows.Count > 0)
                                                        {
                                                            theDR = theDTAuto.NewRow();
                                                            theDR["ID"] = "TAB_" + tabPanel.ID + "_" + ((TextBox)x).ID;
                                                            if (TempDS.Tables[0].Rows[0][i].ToString() == "")
                                                            {
                                                                theDR["Value"] = "0";
                                                            }
                                                            else
                                                            {
                                                                theDR["Value"] = Convert.ToString(TempDS.Tables[0].Rows[0][i]);
                                                            }
                                                            //theDR["Value"] = Convert.ToString(TempDS.Tables[0].Rows[0][i]);
                                                            theDR["Ctrl"] = "TextBox";
                                                            theDTAuto.Rows.Add(theDR);
                                                        }
                                                    }
                                                }
                                                else if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                                                {
                                                    string[] remStr = ((DropDownList)x).ID.Split('-');
                                                    string str = remStr[0] + "-" + remStr[1] + "-" + remStr[2];
                                                    if ("SELECTLISTAuto-" + TempDS.Tables[0].Columns[i].ToString() + "-" + TempDR["TableName"] == str)//((DropDownList)x).ID.Substring(0, ((DropDownList)x).ID.LastIndexOf('-')))
                                                    {
                                                        if (TempDS.Tables[0].Rows.Count > 0)
                                                        {
                                                            theDR = theDTAuto.NewRow();
                                                            //((DropDownList)x).Enabled = true;
                                                            theDR["ID"] = "TAB_" + tabPanel.ID + "_" + ((DropDownList)x).ID;
                                                            if (TempDS.Tables[0].Rows[0][i].ToString() == "")
                                                            {
                                                                theDR["Value"] = "0";
                                                            }
                                                            else
                                                            {
                                                                theDR["Value"] = Convert.ToString(TempDS.Tables[0].Rows[0][i]);
                                                            }
                                                            //theDR["Value"] = Convert.ToString(TempDS.Tables[0].Rows[0][i]);
                                                            theDR["Ctrl"] = "DropDown";
                                                            theDTAuto.Rows.Add(theDR);
                                                            ((DropDownList)x).Enabled = true;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            //foreach (Control x in DIVCustomItem.Controls)
                            //{
                            //}
                        }
                    }
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
            finally
            {
            }

            theDSAuto.Tables.Add(theDTAuto);
            return theDSAuto;
        }

        public void RaiseCallbackEvent(string eventArgument)
        {
            try
            {
                //if (IsPostBack != true)
                //{
                if (AutoDt.TableName == "")
                {
                    return;
                }

                DateTime theDT = Convert.ToDateTime(eventArgument.Trim().ToString());
                if ((!String.IsNullOrEmpty(eventArgument.Trim().ToString())) && (AutoDt.Rows.Count > 0) && (Convert.ToInt32(AutoDt.Rows[0][0]) > 0))
                {
                    DataRow[] theDR = AutoDt.Select("VisitDate < '" + theDT + "'");
                    DataView AutoDV = new DataView(AutoDt);

                    //AutoDV.RowFilter = "VisitDate < '" + theDT + "'";//Getting value Max Date
                    AutoDV.RowFilter = "VisitDate < " + "#" + theDT.ToString("MM/dd/yyyy") + "#";//Getting value Max Date
                    //dataView.RowFilter = "Date>=" + "#" + theDT + "#"
                    AutoDV.Sort = "VisitDate DESC";

                    //DataView AutoDV1 = new DataView(AutoDV.Table);
                    //AutoDV1.RowFilter = "VisitDate=Max(VisitDate)";
                    IQCareUtils theUtils = new IQCareUtils();
                    DataTable dt = new DataTable();
                    if (AutoDV.Table != null)
                    {
                        dt = theUtils.CreateTableFromDataView(AutoDV);
                    }
                    //if ((dt.Rows.Count > 0) &&  (Convert.ToInt32(Session["PatientVisitId"]) == 0))
                    if (dt.Rows.Count > 0)
                    {
                        DataSet theDSAuto = GetRaiseEventValue(Convert.ToInt32(dt.Rows[0]["ptn_pk"]), Convert.ToInt32(dt.Rows[0]["visit_pk"]), Convert.ToInt32(dt.Rows[0]["LocationID"]), DIVCustomItem);
                        str = theDSAuto.GetXml();
                        ClientScript.RegisterStartupScript(GetType(), "CurrentTabValue", "StringASCII(" + hdnPrevTabId.Value + ");", true);
                    }
                }
                else
                {
                    int FeatureID1 = Convert.ToInt32(Session["FeatureID"]);
                    int PatientId1 = Convert.ToInt32(Session["PatientId"]);
                    int VisitID1 = Convert.ToInt32(Session["PatientVisitId"]);

                    int LocationID1 = Convert.ToInt32(Session["AppLocationId"]);
                    DataView AutoDVpre = new DataView(AutoDtPre);

                    if (AutoDVpre.Count > 0)
                    {
                        AutoDVpre.RowFilter = "VisitDate < " + "#" + theDT + "#";//Getting value Max Date
                        AutoDVpre.Sort = "VisitDate DESC";
                        AutoDVpre.Sort = "VisitID DESC";

                        IQCareUtils theUtils = new IQCareUtils();
                        DataTable dtpre = new DataTable();
                        if (AutoDVpre.Table != null)
                        {
                            dtpre = theUtils.CreateTableFromDataView(AutoDVpre);
                        }

                        if (dtpre.Rows.Count > 0)
                        {
                            DataSet theDSAutopre = GetRaiseEventValue(Convert.ToInt32(PatientId1), Convert.ToInt32(dtpre.Rows[0]["VisitID"]), Convert.ToInt32(LocationID1), DIVCustomItem);
                            str = theDSAutopre.GetXml();
                            ClientScript.RegisterStartupScript(GetType(), "CurrentTabValue", "StringASCII(" + hdnPrevTabId.Value + ");", true);
                        }
                    }
                }
            }
            finally
            {
                //CallBackmgr = null;
            }
        }

        #endregion ICallbackEventHandler Members
    }
}